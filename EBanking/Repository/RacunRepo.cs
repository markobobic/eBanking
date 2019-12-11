using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EBanking.Models;

namespace EBanking.Repository
{
    public class RacunRepo : IRacunRepo
    {
        public string ConnectionString { get; set; }

        public RacunRepo()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public int Create(Racun racun)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into racun(nosilac_racuna,broj_racuna,aktivan_racun,online_banking) " +
                            "values (@nosilac,@broj,@aktivan,@online); ";
                        cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                        cmd.Parameters.AddWithValue("@nosilac", racun.NosilacRacuna);
                        cmd.Parameters.AddWithValue("@broj", racun.BrojRacuna);
                        cmd.Parameters.AddWithValue("@aktivan", racun.Aktivan ? 1 : 0);
                        cmd.Parameters.AddWithValue("@online", racun.OnlineBanking ? 1 : 0);

                        connection.Open();

                        var id = cmd.ExecuteScalar();

                        if (id != null)
                        {
                            return int.Parse(id.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return -1;
        }

        public bool Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "delete from racun where id_racuna =@id";
                        cmd.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        var affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return false;
        }

        public IEnumerable<Racun> GetAll()
        {
            List<Racun> racuni = new List<Racun>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM racun";

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            Racun racun = new Racun();
                            racun.Id = (int)dr["id_racuna"];
                            racun.NosilacRacuna = (string)dr["nosilac_racuna"];

                            racun.BrojRacuna = (string)dr["broj_racuna"];
                            racun.Aktivan = Convert.ToByte(dr["aktivan_racun"]) == 0 ? false : true;
                            racun.OnlineBanking = Convert.ToByte(dr["online_banking"]) == 0 ? false : true;
                            racun.UkupnoStanje = GetSum(racun.Id);
                            racuni.Add(racun);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return racuni;
        }

        public Racun GetById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM racun where id_racuna = @id";
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            Racun racun = new Racun();
                            racun.Id = (int)dt.Rows[0]["id_racuna"];
                            racun.NosilacRacuna = (string)dt.Rows[0]["nosilac_racuna"];

                            racun.BrojRacuna = (string)dt.Rows[0]["broj_racuna"];
                            racun.Aktivan = Convert.ToByte(dt.Rows[0]["aktivan_racun"]) == 0 ? false : true;
                            racun.OnlineBanking = Convert.ToByte(dt.Rows[0]["online_banking"]) == 0 ? false : true;
                            racun.UkupnoStanje = GetSum(racun.Id);

                            return racun;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return null;
        }

        public bool Update(Racun racun)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE racun set nosilac_racuna=@nosilac,broj_racuna=@broj,aktivan_racun=@aktivan,online_banking=@online where id_racuna =@id;";

                        cmd.Parameters.AddWithValue("@nosilac", racun.NosilacRacuna);
                        cmd.Parameters.AddWithValue("@broj", racun.BrojRacuna);
                        cmd.Parameters.AddWithValue("@aktivan", racun.Aktivan ? 1 : 0);
                        cmd.Parameters.AddWithValue("@online", racun.OnlineBanking ? 1 : 0);
                        cmd.Parameters.AddWithValue("@id", racun.Id);

                        connection.Open();

                        var affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return false;
        }

        public bool Deactivate(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE racun set aktivan_racun=0 where id_racuna =@id";

                        cmd.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        var affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return false;
        }

        public bool Activate(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE racun set aktivan_racun=1 where id_racuna =@id";

                        cmd.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        var affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return false;
        }

        public decimal GetSum(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select sum(iznos_uplate) from racun,uplatnica where racun.id_racuna=uplatnica.id_racuna and racun.id_racuna=@id group by racun.id_racuna";
                        cmd.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        var suma = cmd.ExecuteScalar();

                        if (suma != null)
                        {
                            return decimal.Parse(suma.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return 0;
        }
    }
}
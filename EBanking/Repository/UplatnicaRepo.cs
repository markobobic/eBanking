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
    public class UplatnicaRepo : IUplatnicaRepo
    {
        public string ConnectionString { get; set; }
        private RacunRepo rRepo = new RacunRepo();

        public UplatnicaRepo()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public int Create(Uplatnica uplatnica)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "insert into uplatnica(id_racuna,iznos_uplate,datum_prometa,svrha_uplate,uplatilac) " +
                            "values (@id_racuna,@iznos,@datum,@svrha,@uplatilac); ";
                        cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                        cmd.Parameters.AddWithValue("@id_racuna", uplatnica.Racun.Id);
                        cmd.Parameters.AddWithValue("@iznos", uplatnica.IznosUplate);
                        cmd.Parameters.AddWithValue("@datum", uplatnica.DatumPrometa);
                        cmd.Parameters.AddWithValue("@svrha", uplatnica.SvrhaUplate);
                        cmd.Parameters.AddWithValue("@uplatilac", uplatnica.Uplatilac);

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
                        cmd.CommandText = "delete from uplatnica where id_uplatnice =@id";
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

        public Uplatnica GetById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from uplatnica where id_uplatnice=@id ";
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 1)
                        {
                            Racun racun = rRepo.GetById((int)dt.Rows[0]["id_racuna"]);

                            Uplatnica uplatnica = new Uplatnica();
                            uplatnica.Racun = racun;
                            uplatnica.Id = (int)dt.Rows[0]["id_uplatnice"];
                            uplatnica.IznosUplate = (decimal)dt.Rows[0]["iznos_uplate"];
                            uplatnica.DatumPrometa = (DateTime)dt.Rows[0]["datum_prometa"];
                            uplatnica.SvrhaUplate = (string)dt.Rows[0]["svrha_uplate"];
                            uplatnica.Uplatilac = (string)dt.Rows[0]["uplatilac"];
                            uplatnica.Hitno = Convert.ToByte(dt.Rows[0]["hitno"]) == 0 ? false : true;

                            return uplatnica;
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

        public IEnumerable<Uplatnica> GetByRacunId(int id)
        {
            List<Uplatnica> uplatniceRacuna = new List<Uplatnica>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from racun,uplatnica where racun.id_racuna=uplatnica.id_racuna and racun.id_racuna=@id";
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            Uplatnica uplatnica = new Uplatnica();

                            uplatnica.Id = (int)dr["id_uplatnice"];
                            uplatnica.IznosUplate = (decimal)dr["iznos_uplate"];
                            uplatnica.DatumPrometa = (DateTime)dr["datum_prometa"];
                            uplatnica.SvrhaUplate = (string)dr["svrha_uplate"];
                            uplatnica.Uplatilac = (string)dr["uplatilac"];
                            uplatnica.Hitno = Convert.ToByte(dr["hitno"]) == 0 ? false : true;

                            Racun racun = rRepo.GetById((int)dr["id_racuna"]);
                            uplatnica.Racun = racun;
                            uplatniceRacuna.Add(uplatnica);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return uplatniceRacuna;
        }

        public bool Update(Uplatnica uplatnica)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE uplatnica set iznos_uplate=@iznos,datum_prometa=@datum,svrha_uplate=@svrha,uplatilac=@uplatilac where id_uplatnice=@id";

                        cmd.Parameters.AddWithValue("@iznos", uplatnica.IznosUplate);
                        cmd.Parameters.AddWithValue("@datum", uplatnica.DatumPrometa);
                        cmd.Parameters.AddWithValue("@svrha", uplatnica.SvrhaUplate);
                        cmd.Parameters.AddWithValue("@uplatilac", uplatnica.Uplatilac);
                        cmd.Parameters.AddWithValue("@id", uplatnica.Id);

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

        public IEnumerable<Uplatnica> GetAll()
        {
            List<Uplatnica> uplatnice = new List<Uplatnica>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "select * from racun,uplatnica where racun.id_racuna=uplatnica.id_racuna";

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            Uplatnica uplatnica = new Uplatnica();

                            uplatnica.Id = (int)dr["id_uplatnice"];
                            uplatnica.IznosUplate = (decimal)dr["iznos_uplate"];
                            uplatnica.DatumPrometa = (DateTime)dr["datum_prometa"];
                            uplatnica.SvrhaUplate = (string)dr["svrha_uplate"];
                            uplatnica.Uplatilac = (string)dr["uplatilac"];
                            uplatnica.Hitno = Convert.ToByte(dr["hitno"]) == 0 ? false : true;

                            Racun racun = rRepo.GetById((int)dr["id_racuna"]);
                            uplatnica.Racun = racun;
                            uplatnice.Add(uplatnica);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return uplatnice;
        }
    }
}
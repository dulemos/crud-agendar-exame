using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    public class PacienteDAL:IPacienteDAL
    {
        string connectionString = @"Data Source=DESKTOP-QOSU791\SQLEXPRESS;Initial Catalog=AgendaConsulta;Integrated Security=True;Pooling=False";
        public IEnumerable<Paciente> GetAllPacientes()
        {
            List<Paciente> Istpaciente = new List<Paciente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from Paciente", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.Id = Convert.ToInt32(rdr["Id"]);
                    paciente.Nome = rdr["Nome"].ToString();
                    paciente.Email = rdr["Email"].ToString();
                    paciente.Password = rdr["Password"].ToString();

                    Istpaciente.Add(paciente);
                }
                con.Close();
            }
            return Istpaciente;
        }

        public void AddPaciente(Paciente paciente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "INSERT INTO PACIENTE (Nome, Email, Password) VALUES(@Nome, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Nome", paciente.Nome);
                cmd.Parameters.AddWithValue("@Email", paciente.Email);
                cmd.Parameters.AddWithValue("@Password", paciente.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdatePaciente(Paciente paciente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string ComandoSQL = "UPDATE Paciente set Nome = @Nome, Email = @Email, Password = @Password;";
                SqlCommand cmd = new SqlCommand(ComandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Nome", paciente.Nome);
                cmd.Parameters.AddWithValue("@Email", paciente.Email);
                cmd.Parameters.AddWithValue("@Password", paciente.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        public Paciente GetPaciente(int? Id)
        {
            Paciente paciente = new Paciente();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSql = "SELECT Id, Nome, Email FROM Paciente WHERE Id =" + Id;
                SqlCommand cmd = new SqlCommand(comandoSql, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    paciente.Id = Convert.ToInt32(reader["Id"]);
                    paciente.Nome = reader["Nome"].ToString();
                    paciente.Email = reader["Email"].ToString();
                }
            }
            return paciente;
        }

        public void DeletePaciente(int? Id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string ComandoSQL = "DELETE FROM Paciente where Id = @Id";
                SqlCommand cmd = new SqlCommand(ComandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", Id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Paciente GetPacienteByEmail(string? Email)
        {
            Paciente paciente = new Paciente();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string SqlQuery = "SELECT Id, Email, Password FROM Paciente WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(SqlQuery, con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Email", Email);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    paciente.Id = Convert.ToInt32(reader["Id"]);
                    paciente.Email = reader["Email"].ToString();
                    paciente.Password = reader["Password"].ToString();
                }

                return paciente;
            }
        }
    }
}

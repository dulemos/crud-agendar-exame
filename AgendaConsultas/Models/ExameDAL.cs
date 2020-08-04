using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    public class ExameDAL : IExameDAL
    {
        string connectionString = @"Data Source=DESKTOP-QOSU791\SQLEXPRESS;Initial Catalog=AgendaConsulta;Integrated Security=True;Pooling=False";
        public IEnumerable<Exame> GetAllExamesByPacienteId(int pacienteId)
        {
            List<Exame> exames = new List<Exame>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Exame WHERE PacienteId = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", pacienteId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Exame exame = new Exame();
                    exame.Id = Convert.ToInt32(rdr["Id"]);
                    exame.PacienteId = Convert.ToInt32(rdr["PacienteId"]);
                    exame.DataId = Convert.ToInt32(rdr["DataId"]);

                    exames.Add(exame);
                }
                con.Close();
            }
            return exames;
        }
        public void AddExame(int idPaciente, int idData)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Exame (PacienteId, DataId) VALUES (@idPaciente, @idData)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                cmd.Parameters.AddWithValue("@idData", idData);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteExame(int ExameId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Exame where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", ExameId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Exame GetExame(int Id)
        {
            Exame exame = new Exame(); 
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Exame WHERE Id = " + Id;
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    exame.Id = Convert.ToInt32(rdr["Id"]);
                    exame.PacienteId = Convert.ToInt32(rdr["PacienteId"]);
                    exame.DataId = Convert.ToInt32(rdr["DataId"]);
                }
                con.Close();
            }
            return exame;
        }

        public void UpdateExame(Exame exame)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Exame set PacienteId = @PacienteId, DataId = @DataId WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PacienteId", exame.PacienteId);
                cmd.Parameters.AddWithValue("@DataId", exame.DataId);
                cmd.Parameters.AddWithValue("@Id", exame.Id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}

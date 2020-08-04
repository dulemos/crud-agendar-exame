using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    public class DataDisponivelDAL : IDataDisponivelDAL
    {
        string connectionString = @"Data Source=DESKTOP-QOSU791\SQLEXPRESS;Initial Catalog=AgendaConsulta;Integrated Security=True;Pooling=False";

        public IEnumerable<DataDisponivel> GetAllDatasDisponiveis()
        {
            List<DataDisponivel> lstDatasDisponiveis = new List<DataDisponivel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM DatasDisponiveis WHERE Disponivel <> 'FALSE'", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DataDisponivel dataDisponivel = new DataDisponivel();
                    dataDisponivel.Id = Convert.ToInt32(rdr["Id"]);
                    dataDisponivel.Data = Convert.ToDateTime(rdr["Data"]);

                    lstDatasDisponiveis.Add(dataDisponivel);
                }
                con.Close();
            }
            return lstDatasDisponiveis;
        }

        public void UpdateDataDisponivel(DataDisponivel dataDisponivel)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE DatasDisponiveis set Data = @data";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@data", dataDisponivel.Data);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void AddDataDisponivel(DataDisponivel dataDisponivel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into DatasDisponiveis (Data) Values(@Data)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Data", dataDisponivel.Data);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public DataDisponivel GetDataDisponivel(int? id)
        {
            DataDisponivel dataDisponivel = new DataDisponivel();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM DatasDisponiveis WHERE Id =" + id;
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataDisponivel.Id = Convert.ToInt32(rdr["Id"]);
                    dataDisponivel.Data = Convert.ToDateTime(rdr["Data"]);
                }

            }
            return dataDisponivel;
        }

        public void DeleteDataDisponivel(int? id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM DatasDisponiveis WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}

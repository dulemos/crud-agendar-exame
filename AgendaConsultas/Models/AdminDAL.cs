using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaConsultas.Models
{
    public class AdminDAL : IAdminDAL
    {
        string connectionString = @"Data Source=DESKTOP-QOSU791\SQLEXPRESS;Initial Catalog=AgendaConsulta;Integrated Security=True;Pooling=False";

        public Admin GetAdminByEmail(string email)
        {
            Admin admin = new Admin();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Admin WHERE email = @email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@email", email);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    admin.Id = Convert.ToInt32(rdr["Id"]);
                    admin.email = rdr["email"].ToString();
                    admin.password = rdr["password"].ToString();
                }
                con.Close();
            }
            return admin;
        }
    }
}

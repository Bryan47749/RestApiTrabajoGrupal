using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebService_WebAPI.Models
{
    public class CategoryDAL
    {
        public CategoryDAL() { }
        private string GetConnectionString()
        {
            return
           ConfigurationManager.ConnectionStrings["HalloweenConnectionString"].ConnectionString;
        }
        public List<Category> GetCategories()
        {
            List<Category> lista = new List<Category>();
            string sql = "SELECT CategoryID, ShortName, LongName " +
            "FROM Categories " +
           "ORDER BY ShortName";

            using (SqlConnection connection = new
           SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    SqlDataReader reader =
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryID = reader["CategoryID"].ToString();
                        category.ShortName = reader["ShortName"].ToString();
                        category.LongName = reader["LongName"].ToString();
                        lista.Add(category);
                    }
                    reader.Close();
                }
            }
            return lista;
        }

        public Category GetCategoryById(string id)
        {
            Category category = null;
            string sql = "SELECT CategoryID, ShortName, LongName " +
            "FROM Categories " +
            "WHERE CategoryID = @CategoryID";
            using (SqlConnection connection = new
           SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CategoryID", id);
                    connection.Open();
                    SqlDataReader reader =
                   command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        category = new Category();
                        category.CategoryID = reader["CategoryID"].ToString();
                        category.ShortName = reader["ShortName"].ToString();
                        category.LongName = reader["LongName"].ToString();
                    }
                    reader.Close();
                }
            }
            return category;
        }
        public List<Category> GetCategoryByShortName(string name)
        {
            List<Category> lista = new List<Category>();
            string sql = "SELECT CategoryID, ShortName, LongName " +
            "FROM Categories " +
           "WHERE ShortName = @ShortName";
            using (SqlConnection connection = new
            SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ShortName", name);
                    connection.Open();
                    SqlDataReader reader =
                   command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryID = reader["CategoryID"].ToString();
                        category.ShortName = reader["ShortName"].ToString();
                        category.LongName = reader["LongName"].ToString();
                        lista.Add(category);
                    }
                    reader.Close();
                }
            }
            return lista;
        }
        public int InsertCategory(Category category)
        {

            int afectadas = 0;
            try
            {
                string sql = "INSERT INTO Categories " +
            "(CategoryID, ShortName, LongName) " +
            "VALUES (@CategoryID, @ShortName, @LongName)";
                using (SqlConnection connection = new
               SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryID",
                       category.CategoryID);
                        command.Parameters.AddWithValue("@ShortName", category.ShortName);
                        command.Parameters.AddWithValue("@LongName", category.LongName);
                        connection.Open();
                        afectadas = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return afectadas;
            }
            catch (Exception ex)
            {

                return 0;
            }

            
        }
        public int UpdateCategory(Category category)
        {
            int afectadas;
            string sql = "UPDATE Categories " +
            "SET ShortName = @ShortName, LongName = @LongName " +
            "WHERE CategoryID = @CategoryID";
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ShortName", category.ShortName);
                    command.Parameters.AddWithValue("@LongName", category.LongName);
                    command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    connection.Open();
                    afectadas = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return afectadas;
        }
        public int DeleteCategory(Category category)
        {
            try
            {
                int afectadas;
                string sql = "DELETE FROM Categories " +
                "WHERE CategoryID = @CategoryID";
                using (SqlConnection connection = new
               SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryID",
                       category.CategoryID);
                        connection.Open();
                        afectadas = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return afectadas;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
    }
}


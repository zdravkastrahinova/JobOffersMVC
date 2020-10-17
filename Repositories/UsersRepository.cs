using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JobOffersMVC.Repositories
{
    public class UsersRepository
    {
        // CRUD 
        // Create
        // Read
        // Update
        // Delete
        private readonly string connectionString = @"Data Source=DESKTOP-L6DMAUU;
Initial Catalog=JobOffersDB;
Integrated Security=True;
Connect Timeout=30;
Encrypt=False;
TrustServerCertificate=False;
ApplicationIntent=ReadWrite;
MultiSubnetFailover=False";

        private SqlConnection connection;
        private SqlCommand command;

        public UsersRepository()
        {
            this.connection = new SqlConnection(this.connectionString);
        }

        public List<User> GetAll()
        {
            this.command = connection.CreateCommand();
            this.command.CommandText = "Select * from Users";

            this.connection.Open();

            IDataReader reader = this.command.ExecuteReader();

            List<User> users = new List<User>();

            while (reader.Read())
            {
                User user = new User();
                user.Id = (int) reader["Id"];
                user.Username = reader["Username"].ToString();
                user.Email = reader["Email"].ToString();
                user.Password = reader["Password"].ToString();
                user.FirstName = reader["FirstName"].ToString();
                user.LastName = reader["LastName"].ToString();

                users.Add(user);
            }

            return users;
        }

        public User GetById(int id)
        {
            this.command = this.connection.CreateCommand();

            this.command.Parameters.Add(new SqlParameter("@Id", id));
            this.command.CommandText = "SELECT * FROM Users WHERE Id=@Id";

            this.connection.Open();

            try
            {
                IDataReader reader = this.command.ExecuteReader();
                reader.Read();

                User user = this.Read(reader);

                return user;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            this.command = this.connection.CreateCommand();

            this.command.Parameters.Add(new SqlParameter("@Username", username));
            this.command.Parameters.Add(new SqlParameter("@Password", password));
            this.command.CommandText = "Select * from Users where Username=@Username and Password=@Password";

            this.connection.Open();

            IDataReader reader = this.command.ExecuteReader();

            User user = new User();

            try
            {
                reader.Read();
                user.Id = (int) reader["Id"];
                user.Username = reader["Username"].ToString();
                user.Email = reader["Email"].ToString();
                user.Password = reader["Password"].ToString();
                user.FirstName = reader["FirstName"].ToString();
                user.LastName = reader["LastName"].ToString();

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Insert(User user)
        {
            this.command = this.connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Username", user.Username));
            command.Parameters.Add(new SqlParameter("@Password", user.Password));
            command.Parameters.Add(new SqlParameter("@Email", user.Email));
            command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
            command.Parameters.Add(new SqlParameter("@LastName", user.LastName));

            command.CommandText
                = "INSERT INTO Users VALUES(@Username, @Password, @Email, @FirstName, @LastName)";

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Update(User user)
        {
            this.command = this.connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Id", user.Id));
            command.Parameters.Add(new SqlParameter("@Username", user.Username));
            command.Parameters.Add(new SqlParameter("@Password", user.Password));
            command.Parameters.Add(new SqlParameter("@Email", user.Email));
            command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
            command.Parameters.Add(new SqlParameter("@LastName", user.LastName));

            command.CommandText = 
                "UPDATE Users " +
                "SET Username=@Username, Password=@Password, Email=@Email, FirstName=@FirstName, LastName=@LastName " +
                "WHERE Id=@Id";

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete(int id)
        {
            this.command = this.connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Id", id));
            command.CommandText = "DELETE FROM Users WHERE Id=@Id";

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }

        private User Read(IDataReader reader)
        {
            User user = new User();

            user.Id = (int) reader["Id"];
            user.Username = reader["Username"].ToString();
            user.Email = reader["Email"].ToString();
            user.Password = reader["Password"].ToString();
            user.FirstName = reader["FirstName"].ToString();
            user.LastName = reader["LastName"].ToString();

            return user;
        }
    }
}
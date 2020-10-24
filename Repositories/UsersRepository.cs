using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JobOffersMVC.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository()
            : base("Users")
        { }

        public override void Read(User item, IDataReader reader)
        {
            item.Id = (int)reader["Id"];
            item.Username = reader["Username"].ToString();
            item.Email = reader["Email"].ToString();
            item.Password = reader["Password"].ToString();
            item.FirstName = reader["FirstName"].ToString();
            item.LastName = reader["LastName"].ToString();
        }

        public override void BuildCommandParameters(User item, SqlCommand cmd)
        {
            cmd.Parameters.Add(new SqlParameter("@Username", item.Username));
            cmd.Parameters.Add(new SqlParameter("@Password", item.Password));
            cmd.Parameters.Add(new SqlParameter("@Email", item.Email));
            cmd.Parameters.Add(new SqlParameter("@FirstName", item.FirstName));
            cmd.Parameters.Add(new SqlParameter("@LastName", item.LastName));
        }

        public override string GetInsertCommandText()
        {
            return "INSERT INTO Users VALUES(@Username, @Password, @Email, @FirstName, @LastName)";
        }

        public override string GetUpdateCommandText()
        {
            return "UPDATE Users " +
                   "SET Username=@Username, Password=@Password, Email=@Email, FirstName=@FirstName, LastName=@LastName " +
                   "WHERE Id=@Id";
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            this.command = this.connection.CreateCommand();

            this.command.Parameters.Add(new SqlParameter("@Username", username));
            this.command.Parameters.Add(new SqlParameter("@Password", password));
            this.command.CommandText = "Select * from Users where Username=@Username and Password=@Password";

            this.connection.Open();

            try
            {
                IDataReader reader = this.command.ExecuteReader();
                reader.Read();

                User user = new User();
                Read(user, reader);

                return user;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
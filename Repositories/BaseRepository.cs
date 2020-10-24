using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Models;

namespace JobOffersMVC.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel, new()
    {
        private readonly string connectionString =
            @"Data Source=DESKTOP-L6DMAUU;Initial Catalog=JobOffersDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly string tableName;

        protected SqlConnection connection;
        protected SqlCommand command;

        public BaseRepository(string tableName)
        {
            connection = new SqlConnection(this.connectionString);
            this.tableName = tableName;
        }

        public abstract void Read(T item, IDataReader reader);
        public abstract void BuildCommandParameters(T item, SqlCommand cmd);
        public abstract string GetInsertCommandText();
        public abstract string GetUpdateCommandText();

        public List<T> GetAll()
        {
            command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM " + tableName;

            connection.Open();

            List<T> items = new List<T>();

            try
            {
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    T item = new T();
                    Read(item, reader);
                    items.Add(item);
                }
            }
            finally
            {
                connection.Close();
            }

            return items;
        }

        public T GetById(int id)
        {
            command = connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Id", id));
            command.CommandText = "SELECT * FROM " + tableName + " WHERE Id=@Id";

            connection.Open();

            try
            {
                IDataReader reader = command.ExecuteReader();
                reader.Read();

                T item = new T();
                Read(item, reader);

                return item;
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

        public void Insert(T item)
        {
            command = connection.CreateCommand();

            BuildCommandParameters(item, command);

            command.CommandText = GetInsertCommandText();

            connection.Open();

            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(T item)
        {
            command = connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Id", item.Id));
            BuildCommandParameters(item, command);

            command.CommandText = GetUpdateCommandText();

            connection.Open();

            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            command = connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Id", id));

            command.CommandText = "DELETE FROM " + tableName + " WHERE Id=@Id";

            connection.Open();

            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

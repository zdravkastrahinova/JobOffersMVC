using JobOffersMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JobOffersMVC.Repositories
{
    public class JobOffersRepository : BaseRepository<JobOffer>
    {
        public JobOffersRepository()
            : base("JobOffers")
        { }

        public override void Read(JobOffer jobOffer, IDataReader reader)
        {
            jobOffer.Id = (int)reader["Id"];
            jobOffer.Title = reader["Title"].ToString();
            jobOffer.Description = reader["Description"].ToString();
            jobOffer.UserId = (int)reader["UserId"];
        }

        public override void BuildCommandParameters(JobOffer item, SqlCommand cmd)
        {
            cmd.Parameters.Add(new SqlParameter("@Title", item.Title));
            cmd.Parameters.Add(new SqlParameter("@Description", item.Description));
            cmd.Parameters.Add(new SqlParameter("@UserId", item.UserId));
        }

        public override string GetInsertCommandText()
        {
            return "INSERT INTO JobOffers VALUES(@Title, @Description, @UserId)";
        }

        public override string GetUpdateCommandText()
        {
            return "UPDATE JobOffers " +
                   "SET Title=@Title, Description=@Description " +
                   "WHERE Id=@Id AND UserId=@UserId";
        }

        public List<JobOffer> GetAllByUserId(int userId)
        {
            command = connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@UserId", userId));
            command.CommandText = "SELECT * FROM JobOffers WHERE UserId=@UserId";

            connection.Open();

            List<JobOffer> jobOffers = new List<JobOffer>();

            try
            {
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    JobOffer jobOffer = new JobOffer();
                    Read(jobOffer, reader);
                    jobOffers.Add(jobOffer);
                }
            }
            finally
            {
                connection.Close();
            }

            return jobOffers;
        }

        public JobOffer GetById(int id, int userId)
        {
            command = connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Id", id));
            command.Parameters.Add(new SqlParameter("@UserId", userId));

            command.CommandText = "SELECT * FROM JobOffers WHERE Id=@Id AND UserId=@UserId";

            connection.Open();

            try
            {
                IDataReader reader = command.ExecuteReader();
                reader.Read();

                JobOffer jobOffer = new JobOffer();
                Read(jobOffer, reader);

                return jobOffer;
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

        public void Delete(int id, int userId)
        {
            command = connection.CreateCommand();

            command.Parameters.Add(new SqlParameter("@Id", id));
            command.Parameters.Add(new SqlParameter("@UserId", userId));

            command.CommandText = "DELETE FROM JobOffers WHERE Id=@Id AND UserId=@UserId";

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
using Mentor.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.DAL
{
    public class MentorTrainingDAL
    {
        string conString = DataUtil.connectionString;
        public void Training_Insert_Update(MentorTraining training)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_MenteeTraining_Insert_Update.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            string temp;
            if (training.MentorTrainingId > 0)
            {
                temp = training.MentorTrainingId.ToString();
            }
            else
            {
                temp = null;
            }

            cmd.Parameters.AddWithValue("@MenteeTrainingId", temp);
            cmd.Parameters.AddWithValue("@MentorId", training.MentorId);
            cmd.Parameters.AddWithValue("@MemberCareerLevelId", training.CareerLevel);
            cmd.Parameters.AddWithValue("@MemberDomainId", training.Domain);
            cmd.Parameters.AddWithValue("@CategoryId", training.Category);
            cmd.Parameters.AddWithValue("@SubCategoryId", training.SubCategory);
            cmd.Parameters.AddWithValue("@Title", training.Title);
            cmd.Parameters.AddWithValue("@PerHourRate", training.TrainingPerHourRate);
            cmd.Parameters.AddWithValue("@TrainingDurationId", training.Duration);
            cmd.Parameters.AddWithValue("@RequiredExperienceId", training.Experience);
            cmd.Parameters.AddWithValue("@City", training.City);
            cmd.Parameters.AddWithValue("@TrainingDescription", training.Description);
            cmd.Parameters.AddWithValue("@TrainingStartDate", training.TrainingStartDate);
            cmd.Parameters.AddWithValue("@IsPublic", training.IsPublic);

            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.Int);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            var Response = cmd.Parameters.Add("@status", SqlDbType.VarChar);
            Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }
        public void Insert_Update_TrainingSchedule(List<SelectedSlotsList> schedule, int MemberId, int TrainingId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_ScheduleMenteeTraining.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (schedule != null)
            {
                var table = new DataTable();
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Day", typeof(string));
                table.Columns.Add("ScheduleId", typeof(int));
                table.Columns.Add("SlotId", typeof(int));
                int i = 1;
                foreach (var level in schedule)
                {
                    var row = table.NewRow();
                    row["ID"] = i;
                    row["Day"] = level.SlotDay;
                    row["ScheduleId"] = level.MemberMeetingScheduleId;
                    row["SlotId"] = level.MemberMeetingSlotId;

                    table.Rows.Add(row);
                    i++;
                }

                var parameter = cmd.CreateParameter();
                parameter.TypeName = "dbo.TrainingScheduleTableType";
                parameter.Value = table;
                parameter.ParameterName = "@TSTT";

                cmd.Parameters.Add(parameter);
            }
            cmd.Parameters.AddWithValue("@MenteeTrainingId", TrainingId);
            cmd.Parameters.AddWithValue("@MentorId", MemberId);

            var ResponseId = cmd.Parameters.Add("@Message", SqlDbType.Int);
            ResponseId.Direction = ParameterDirection.ReturnValue;
            var Response = cmd.Parameters.Add("@status", SqlDbType.VarChar);
            Response.Direction = ParameterDirection.ReturnValue;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }
        public MentorTraining GetLastTrainingEntry()
        {
            //MentorTraining training = new MentorTraining();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string query = "SELECT TOP 1 * FROM MenteeTraining ORDER BY MenteeTrainingId DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            int MentorTrainingId = 0;
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                MentorTrainingId = Convert.ToInt32(read["MenteeTrainingId"]);
            }
            con.Close();
            return GetTraining(MentorTrainingId);
        }
        public MentorTraining GetTraining(int TrainingId)
        {
            MentorTraining training = new MentorTraining();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MenteeTraining.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MenteeTrainingId", TrainingId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                training.MentorId = Convert.ToInt32(read["MentorId_fk"]);
                training.MentorTrainingId = Convert.ToInt32(read["MenteeTrainingId"]);
                training.CareerLevel = read["MemberCareerLevelId"].ToString();
                training.Domain = read["MemberDomainId"].ToString();
                training.Category = read["CategoryId"].ToString();
                training.SubCategory = read["SubCategoryId"].ToString();
                training.Title = read["Title"].ToString();
                training.TrainingPerHourRate = read["PerHourRate"].ToString();
                training.Duration = read["TrainingDurationId"].ToString();
                training.DurationName = read["TrainingDuration"].ToString();
                training.Experience = read["RequiredExperienceId"].ToString();
                training.Description = read["TrainingDescription"].ToString();
                training.City = read["City"].ToString();
                training.TrainingStartDate = Convert.ToDateTime(read["TrainingStartDate"]);
                training.StartDate = training.TrainingStartDate.ToShortDateString();
                training.IsPublic = Convert.ToBoolean(read["IsPublic"]);
            }
            con.Close();
            return training;
        }
        public MentorTraining GetTrainingView(string TrainingId)
        {
            MentorTraining training = new MentorTraining();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MenteeTraining.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MenteeTrainingId", Convert.ToInt32(TrainingId));
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                training.MentorId = Convert.ToInt32(read["MentorId_fk"]);
                training.MentorTrainingId = Convert.ToInt32(read["MenteeTrainingId"]);
                training.CareerLevel = read["MemberCareerLevel"].ToString();
                training.Domain = read["MemberDomainName"].ToString();
                training.Category = read["CategoryName"].ToString();
                training.SubCategory = read["SubCategoryName"].ToString();
                training.Title = read["Title"].ToString();
                training.MemberName = read["Name"].ToString();
                training.TrainingPerHourRate = read["PerHourRate"].ToString();
                training.TotalHours = read["TrainingHour"].ToString();
                training.Duration = read["TrainingDuration"].ToString();
                training.Experience = read["RequiredExperience"].ToString();
                training.Description = read["TrainingDescription"].ToString();
                training.City = read["City"].ToString();
                training.TrainingStartDate = Convert.ToDateTime(read["TrainingStartDate"]);
                training.StartDate = training.TrainingStartDate.ToShortDateString();
                training.IsPublic = Convert.ToBoolean(read["IsPublic"]);
                training.Price = (Convert.ToInt32(read["PerHourRate"]) * Convert.ToInt32(read["TrainingHour"])).ToString();
            }
            con.Close();
            return training;
        }
        public MentorTraining GetTrainingDay(string TrainingId)
        {
            MentorTraining training = new MentorTraining();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_HoursPerDay.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MenteeTrainingId", Convert.ToInt32(TrainingId));
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {

                training.HoursPerDay = read["PerDayHours"].ToString();

                training.Day = read["Day"].ToString();

            }
            con.Close();
            return training;
        }

        public List<MentorTraining> GetTrainingList(int MentorId)
        {
            List<MentorTraining> training = new List<MentorTraining>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MenteeTraining.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MentorId", MentorId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                training.Add(new MentorTraining
                {
                    MentorId = Convert.ToInt32(read["MentorId_fk"]),
                    MentorTrainingId = Convert.ToInt32(read["MenteeTrainingId"]),
                    CareerLevel = read["MemberCareerLevel"].ToString(),
                    Domain = read["MemberDomainName"].ToString(),
                    Category = read["CategoryName"].ToString(),
                    SubCategory = read["SubCategoryName"].ToString(),
                    Title = read["Title"].ToString(),
                    TrainingPerHourRate = read["PerHourRate"].ToString(),
                    Duration = read["TrainingDuration"].ToString(),
                    Experience = read["RequiredExperience"].ToString(),
                    Description = read["TrainingDescription"].ToString(),
                    TotalHours = read["TrainingHour"].ToString(),
                    City = read["City"].ToString(),
                    TrainingStartDate = Convert.ToDateTime(read["TrainingStartDate"]),
                    IsPublic = Convert.ToBoolean(read["IsPublic"])
                });
            }
            con.Close();
            return training;
        }
        public List<SelectedSlotsList> GetTrainingSchedule(string TrainingId, string MentorId)
        {
            List<SelectedSlotsList> trainingSchedule = new List<SelectedSlotsList>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MenteeTrainingSchedule.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainingId", TrainingId);
            cmd.Parameters.AddWithValue("@MentorId", MentorId);
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                trainingSchedule.Add(new SelectedSlotsList
                {
                    MemberMeetingScheduleId = Convert.ToInt32(read["MemberMeetingScheduleId_fk"].ToString()),
                    MemberMeetingSlotId = Convert.ToInt32(read["MemberMeetingSlotId_fk"].ToString()),
                    Date = read["MeetingDate"].ToString(),
                    SlotDay = read["Day"].ToString(),
                    SlotStartTime = read["StartTime"].ToString(),
                    SlotEndTime = read["EndTime"].ToString(),
                });
            }
            return trainingSchedule;
        }
        public void DeleteTrainingSlot(string TrainingId, string ScheduleId, string SlotId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Delete_TrainingSchedule.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrainingId", TrainingId);
            cmd.Parameters.AddWithValue("@ScheduleId", ScheduleId);
            cmd.Parameters.AddWithValue("@SlotId", SlotId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }
        public void DeleteTraining(string TrainingId, string MentorId)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_Delete_MenteeTraining.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MenteeTrainingId", TrainingId);
            cmd.Parameters.AddWithValue("@MentorId", MentorId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }

        }
        public List<SelectedSlotsList> GetAllSlots(int mid)
        {
            List<SelectedSlotsList> slots = new List<SelectedSlotsList>();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_MentorScheduleSlot.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MentorId", mid);


            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    slots.Add(new SelectedSlotsList
                    {
                        MemberMeetingScheduleId = Convert.ToInt32(read["MemberMeetingScheduleId_fk"].ToString()),
                        MemberMeetingSlotId = Convert.ToInt32(read["MemberMeetingSlotId"].ToString()),
                        TrainingId = read["MenteeTrainingId_fk"].ToString(),
                        SlotDay = read["Day"].ToString(),
                        SlotStartTime = read["StartTime"].ToString(),
                        SlotEndTime = read["EndTime"].ToString(),
                        isReserved = read["IsResrved"].ToString()
                    });

                }

            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            con.Close();
            return slots;
        }
        public List<SelectedList> GetSelectedExpList()
        {
            List<SelectedList> expList = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_RequiredExperience.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            /* levels.Add(new SelectedList1
             {
                 //Value = "0",
                 Value = 0,
                 Text = "Select Experience Required"
             }); */
            while (read.Read())
            {
                expList.Add(new SelectedList
                {
                    Value = read["RequiredExperienceId"].ToString(),
                    Text = read["RequiredExperience"].ToString()
                });
            }
            con.Close();
            return expList;
        }
        public List<SelectedList> GetTrainingDurationList()
        {
            List<SelectedList> duration = new List<SelectedList>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_TrainingDuration.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            //duration.Add(new SelectedList
            // {
            //     Value = "0",
            //     Text = "Select Training Duration"
            // });
            while (read.Read())
            {
                duration.Add(new SelectedList
                {
                    Value = read["TrainingDurationId"].ToString(),
                    Text = read["TrainingDuration"].ToString()
                });
            }
            con.Close();
            return duration;
        }
        //Mentee View All Trainings
        public List<MentorTraining> TrainingList(int careerid, int domianid, int categoryid, int subcategoryid, int min, int max)
        {
            float fmin = min;
            float fmax = max;

            List<MentorTraining> filtermentor = new List<MentorTraining>();

            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Public_MenteeTraining.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberCareerLevelId", new GenericFunctionsDAL().checknullint(careerid));
            cmd.Parameters.AddWithValue("@MemberDomainId", new GenericFunctionsDAL().checknullint(domianid));
            cmd.Parameters.AddWithValue("@CategoryId", new GenericFunctionsDAL().checknullint(categoryid));
            cmd.Parameters.AddWithValue("@SubCategoryId", new GenericFunctionsDAL().checknullint(subcategoryid));
            cmd.Parameters.AddWithValue("@MinRate", new GenericFunctionsDAL().checknullfloat(fmin));
            cmd.Parameters.AddWithValue("@MaxRate", new GenericFunctionsDAL().checknullfloat(fmax));
            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    filtermentor.Add(new MentorTraining
                    {
                        MentorTrainingId = Convert.ToInt32(read["MenteeTrainingId"].ToString()),
                        MentorId = Convert.ToInt32(read["MentorId_fk"].ToString()),
                        MentorName = read["Name"].ToString(),
                        CareerLevel = read["MemberCareerLevel"].ToString(),
                        Domain = read["MemberDomainName"].ToString(),
                        SubCategory = read["SubCategoryName"].ToString(),
                        Category = read["CategoryName"].ToString(),
                        Title = read["Title"].ToString(),
                        // MemberRate =float.Parse(read["PerHourRate"].ToString()),
                        TrainingPerHourRate = read["PerHourRate"].ToString(),
                        TotalHours = read["TrainingHour"].ToString(),
                        Description = read["TrainingDescription"].ToString(),
                        TrainingStartDate = Convert.ToDateTime(read["TrainingStartDate"].ToString()),
                        City = read["City"].ToString()

                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            con.Close();
            return filtermentor;
        }
        //Populate Ticker Table ON IndexPage
        public List<MentorTraining> FilteredTrainingList_Index(int careerid, int domianid, int categoryid, int subcategoryid)
        {

            List<MentorTraining> searchList = new List<MentorTraining>();
            string conString = DataUtil.connectionString;
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(StoredProcedure.Names.SP_GET_Public_MenteeTraining.ToString(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberCareerLevelId", new GenericFunctionsDAL().checknullint(careerid));
            cmd.Parameters.AddWithValue("@MemberDomainId", new GenericFunctionsDAL().checknullint(domianid));
            cmd.Parameters.AddWithValue("@CategoryId", new GenericFunctionsDAL().checknullint(categoryid));
            cmd.Parameters.AddWithValue("@SubCategoryId", new GenericFunctionsDAL().checknullint(subcategoryid));

            con.Open();
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    MentorTraining searchLists = new MentorTraining();

                    searchLists.Title = read["Title"].ToString();
                    searchLists.StartDate = read["TrainingStartDate"].ToString();
                    searchLists.TotalHours = read["TrainingHour"].ToString();

                    searchList.Add(searchLists);
                }
            }
            else
            {
                Console.WriteLine("No rows found");
            }
            con.Close();
            return searchList;

        }
    }
}

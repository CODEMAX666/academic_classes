using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.DAL
{
    public class StoredProcedure
    {
        public enum Names
        {
            SP_Member_Insert_Update,
            SP_GET_Member,
            SP_GET_MemberDomain,
            SP_GET_MemberCareerLevel,
            SP_GET_DegreeLevel,
            SP_GET_DegreeTitle,
            SP_MemberEduacation_Insert_Update,
            SP_CreateMember,
            SP_CreateMemberAuthentication,
            SP_Login,
            SP_GetId,
            SP_GET_Category,
            SP_GET_SubCategory_1,
            SP_Search_Mentor,
            SP_MemberMeetingSchedule_Insert_Update,
            SP_GET_MemberMeetingSchedule,
            MemberVSPaymentMethod_Insert,
            SP_DeleteScheduling,
            SP_GET_MemberMeetingSlot,
            SP_MemberTransaction_Insert,
            SP_MemberEducation_Insert_Update,
            SP_GET_MemberEducation,
            SP_GET_SubCategory,
            SP_MemberExperience_Insert_Update,
            SP_GET_MemberExperience,
            SP_MemberInterest_Insert_Update,
            SP_GET_MemberInterest,
            SP_GET_Member_Booking_Detail,
            SP_AddMemberBooking,
            SP_GET_MemberTransaction,
            SP_MarketJobs_Insert_Update_Delete,
            SP_GET_MarketJobs,
            SP_GET_MarketJob,
            SP_GET_RequiredExperience,
            SP_GET_MemberMenteeCareerLevel,
            SP_GET_MenteePackage,
            SP_GET_MentorOwnedPackage,
            SP_MentorOwnPackage_Insert_Update,
            SP_MenteeRefferJobRequest_Insert_Update,
            SP_GET_MentorRefferJob,
            SP_GET_MarketJobstest,
            SP_GET_JobRefferStatus,
            SP_GET_MenteeTrainingAvailable,
            SP_GET_MemberProfile_insert,
            SP_GET_TrainingDuration,
            SP_GET_MemberProfile,
            SP_MenteeTraining_Insert_Update,
            SP_GET_MenteeTraining,
            SP_ScheduleMenteeTraining,
            SP_GET_MenteeTrainingSchedule,
            SP_Delete_TrainingSchedule,
            SP_GET_MentorScheduleSlot,
            SP_Delete_MenteeTraining,
            SP_GET_MentorList,
            SP_GET_MenteeList,
            SP_Mark_Mentee_FollowStatus_Pending,
            SP_Mark_Mentee_FollowStatus_Following,
            SP_Mark_Mentee_FollowStatus_UnfollowByMentor,
            SP_Mark_Mentee_FollowStatus_UnfollowByMentee,
            SP_MenteeMentorFollowship,
            SP_GET_Public_MenteeTraining,
            SP_GET_Public_MarketJobs,
            SP_GET_SubCategoryName,
            SP_Mark_Mentee_FollowStatus_Reject,
            SP_GET_HoursPerDay,
            SP_GET_MentorBooking,
            SP_Mark_BookingStatus_CancelledbyMentee,
            SP_Mark_BookingStatus_CancelledbyMentor
        }
    }
}
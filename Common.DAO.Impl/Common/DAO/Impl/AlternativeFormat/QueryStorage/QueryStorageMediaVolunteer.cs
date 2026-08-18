using System;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat.QueryStorage
{
	// Token: 0x0200016F RID: 367
	internal static class QueryStorageMediaVolunteer
	{
		// Token: 0x0400066C RID: 1644
		internal const string DQ_DELETE_JOB_VOLUNTEER_WORKING_HOURS = "delete from AlternativeFormat_VolunteerWorkingHours where JobVolunteerWorkingHoursId=@jobvolunteerworkinghoursid";

		// Token: 0x0400066D RID: 1645
		internal const string UQ_UPDATE_MEDIA_JOB_VOLUNTEER_WORKING_HOURS = "UPDATE [AlternativeFormat_VolunteerWorkingHours]\r\n               SET [StartWorkingTime] = @startworkingtime\r\n                  ,[EndWorkingTime] = @endworkingtime\r\n                  ,[WhoAddWorkingHours] = @whoaddworkinghours\r\n                  ,[VolunteerWorkingHoursNotes] = @volunteerworkinghoursnotes\r\n             WHERE JobVolunteerWorkingHoursId=@jobvolunteerworkinghoursid";

		// Token: 0x0400066E RID: 1646
		internal const string IQ_ADD_MEDIA_JOB_VOLUNTEER_WORKING_HOURS = "INSERT INTO [AlternativeFormat_VolunteerWorkingHours]\r\n                       ([VolunteerId]\r\n                       ,[MediaJobId]\r\n                       ,[StartWorkingTime]\r\n                       ,[EndWorkingTime]\r\n                       ,[WhoAddWorkingHours]\r\n                       ,[VolunteerWorkingHoursNotes])\r\n                 VALUES\r\n                       (@volunteerid\r\n                       ,@mediajobid\r\n                       ,@startworkingtime\r\n                       ,@endworkingtime\r\n                       ,@whoaddworkinghours\r\n                       ,@volunteerworkinghoursnotes)\r\n\r\n            SET @jobvolunteerworkinghoursid = scope_identity()";

		// Token: 0x0400066F RID: 1647
		internal const string UQ_UPDATE_MEDIA_JOB_VOLUNTEER_NOTES = "update AlternativeFormat_MediaJob_x_Volunteer\r\n                set JobVolunteerNotes=@jobvolunteernotes\r\n                where VolunteerId=@volunteerid and MediaJobId=@mediajobid";

		// Token: 0x04000670 RID: 1648
		internal const string UQ_UPDATE_MEDIA_JOB_VOLUNTEER_ACTIVE_STATUS = "update AlternativeFormat_MediaJob_x_Volunteer\r\n                set IsActive=@isactive\r\n                where VolunteerId=@volunteerid and MediaJobId=@mediajobid";

		// Token: 0x04000671 RID: 1649
		internal const string UQ_UPDATE_MEDIA_JOB_VOLUNTEER_ACTIVE_STATUS2 = "update AlternativeFormat_MediaJob_x_Volunteer\r\n                set IsActive=@isactive\r\n                where JobVolunteerId=@jobvolunteerid";

		// Token: 0x04000672 RID: 1650
		internal const string UQ_UPDATE_MEDIA_JOB_VOLUNTEER_ACTIVE_STATUS3 = "update AlternativeFormat_MediaJob_x_Volunteer\r\n                set IsActive=@isactive\r\n                where JobVolunteerId IN (SELECT OrderId as JobVolunteerId from SplitOrderIds(@jobvolunteeridlist, ','))";

		// Token: 0x04000673 RID: 1651
		internal const string IQ_CREATE_MEDIA_JOB_VOLUNTEER = "set @jobvolunteerid = (SELECT JobVolunteerId FROM [AlternativeFormat_MediaJob_x_Volunteer] WHERE VolunteerId = @volunteerid AND MediaJobId = @mediajobid)\r\n\r\nIF (@jobvolunteerid = 0)\r\nBEGIN\r\n\tINSERT INTO [AlternativeFormat_MediaJob_x_Volunteer]\r\n\t\t\t\t([VolunteerId]\r\n\t\t\t\t,[MediaJobId]\r\n\t\t\t\t,[WhoAssignedVolunteer]\r\n\t\t\t\t,[JobVolunteerNotes])\r\n\t\t\tVALUES\r\n\t\t\t\t(@volunteerid\r\n\t\t\t\t,@mediajobid\r\n\t\t\t\t,@whoassignedvolunteer\r\n\t\t\t\t,@jobvolunteernotes)\r\n\r\n\tset @jobvolunteerid = SCOPE_IDENTITY()\r\nEND\r\nELSE\r\nBEGIN\r\n\tIF EXISTS (SELECT 1 FROM [AlternativeFormat_MediaJob_x_Volunteer] WHERE IsActive = 0 AND JobVolunteerId = @jobvolunteerid)\r\n\tBEGIN\r\n\t\tUPDATE [AlternativeFormat_MediaJob_x_Volunteer] \r\n\t\tSET IsActive = 1, \r\n\t\tJobVolunteerNotes = jobvolunteernotes,\r\n\t\tWhoAssignedVolunteer = @whoassignedvolunteer\r\n\t\tWHERE JobVolunteerId = @jobvolunteerid\r\n\tEND\r\nEND";
	}
}

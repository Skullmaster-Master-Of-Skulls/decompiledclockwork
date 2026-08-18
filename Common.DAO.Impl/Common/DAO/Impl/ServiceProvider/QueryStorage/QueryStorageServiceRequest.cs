using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider.QueryStorage
{
	// Token: 0x02000062 RID: 98
	public class QueryStorageServiceRequest
	{
		// Token: 0x040000F5 RID: 245
		internal const string QS_REQUESTS = "EXEC SPRequestsWithoutSubItems @startdate,@enddate,@includeassigned,@includeunassigned,@sptypes";

		// Token: 0x040000F6 RID: 246
		internal const string QS_REQUEST_BY_ID = "EXEC SPRequestWithSubItems @requestid";

		// Token: 0x040000F7 RID: 247
		internal const string QI_ASSIGN_COURSE_TO_REQUEST = "DECLARE @id int\r\nSET @id=(SELECT sprequestcourseid FROM sprequestcourse WHERE sprequestcourseid=@sprequestcourseid AND NOT SPRequestCourseAssignmentId IS NULL)\r\nIF @id IS NULL\r\nBEGIN\r\n    INSERT INTO sprequestcourseassignment(CourseAssignmentSPProviderId,CourseAssignmentLuCourseId,CourseAssignmentNotes,CourseAssignmentIsActive,CourseAssignmentDateCancelled)\r\n        VALUES (@spproviderid,@lucid,@notes,@isactive,@datecancelled);\r\n    SET @id=(SELECT CAST(SCOPE_IDENTITY() AS int)\r\n    UPDATE sprequestcourse SET sprequestcourseassignmentid=@id WHERE sprequestcourseid=@sprequestcourseid\r\nEND\r\nELSE \r\n    UPDATE sprequestcourseassignment SET CourseAssignmentSPProviderId=@providerid,CourseAssignmentLuCourseId=@lucid,\r\n        CourseAssignmentNotes=@notes,CourseAssignmentIsActive=@isactive,CourseAssignmentDateCancelled=@datecancelled\r\n    WHERE sprequestcourseassignmentid=@id\r\nEND\r\nSELECT @id";

		// Token: 0x040000F8 RID: 248
		internal const string QI_ASSIGN_EVENT_TO_REQUEST = "DECLARE @id int\r\nSET @id=(SELECT sprequesteventid FROM sprequestevent WHERE sprequesteventid=@sprequesteventid AND NOT SPRequesteventAssignmentId IS NULL)\r\nIF @id IS NULL\r\nBEGIN\r\n    INSERT INTO sprequesteventassignment(eventAssignmentSPProviderId,eventAssignmentNotes,eventAssignmentIsActive,eventAssignmentDateCancelled)\r\n        VALUES (@spproviderid,@notes,@isactive,@datecancelled);\r\n    SET @id=(SELECT CAST(SCOPE_IDENTITY() AS int)\r\n    UPDATE sprequestevent SET sprequesteventassignmentid=@id WHERE sprequesteventid=@sprequesteventid\r\nEND\r\nELSE \r\n    UPDATE sprequesteventassignment SET eventAssignmentSPProviderId=@providerid,\r\n        eventAssignmentNotes=@notes,eventAssignmentIsActive=@isactive,eventAssignmentDateCancelled=@datecancelled\r\n    WHERE sprequesteventassignmentid=@id\r\nEND\r\nSELECT @id";

		// Token: 0x040000F9 RID: 249
		internal const string QI_REQUEST = "INSERT INTO sprequest (spprovidertypeid,personid,dateentered,whoentered,notes,specialinstructions,sprequeststatustypeid,sprequestassignmentstatustypeid,spurgencyleveltypeid,isactive)\r\nVALUES (@spprovidertypeid,@personid,getdate(),@whoentered,@notes,@specialinstructions,@sprequeststatustypeid,@sprequestassignmentstatustypeid,@spurgencyleveltypeid,@isactive); \r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS sprequestid";

		// Token: 0x040000FA RID: 250
		internal const string QI_REQUESTEVENT = "INSERT INTO SPRequestEvent (SPRequestId,requesteventstartdatetime,requesteventenddatetime,sprequeststatustypeid,SPRequestAssignmentStatusTypeId,SPUrgencyLevelTypeId,requesteventNotes,sprequesteventassignmentid,requesteventIsRequired)\r\nVALUES (@SPRequestId,@requesteventstartdatetime,@requesteventenddatetime,@SPRequestStatusTypeId,@SPRequestAssignmentStatusTypeId,@SPUrgencyLevelTypeId,@requesteventnotes,@sprequesteventassignmentid,@requesteventIsRequired);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS sprequesteventid";

		// Token: 0x040000FB RID: 251
		internal const string QI_REQUESTCOURSE = "INSERT INTO SPRequestCourse (SPRequestId,lucourseid,SPRequestStatusTypeId,SPRequestAssignmentStatusTypeId,SPUrgencyLevelTypeId,Notes,SPRequestCourseAssignmentId,IsRequired)\r\nVALUES (@SPRequestId,@LuCourseId,@SPRequestStatusTypeId,@SPRequestAssignmentStatusTypeId,@SPUrgencyLevelTypeId,@Notes,@SPRequestCourseAssignmentId,@IsRequired);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS spcourseid";

		// Token: 0x040000FC RID: 252
		internal const string QU_MERGE_REQUESTS = "UPDATE serviceproviderrequests SET personid=@pidnew WHERE personid=@pidold";

		// Token: 0x040000FD RID: 253
		internal const string QU_REQUESTCOURSE = "UPDATE    SPRequestCourse SET lucourseid=@lucid,sprequeststatustypeid=@sprequeststatustypeid,sprequestassignmentstatustypeid=@sprequestassignmentstatustypeid,\r\n            spurgencyleveltypeid=@spurgencyleveltypeid,notes=@notes,sprequestcourseassignmentid=@sprequestcourseassignmentid,isrequired=@isrequired\r\nWHERE       sprequestcourseid=@sprequestcourseid";

		// Token: 0x040000FE RID: 254
		internal const string QU_REQUESTEVENT = "UPDATE    sprequestevent SET requesteventstartdatetime=@requesteventstartdatetime,requesteventenddatetime=@requesteventenddatetime,sprequeststatustypeid=@sprequeststatustypeid,\r\n            sprequestassignmentstatustypeid=@sprequestassignmentstatustypeid,spurgencyleveltypeid=@spurgencyleveltypeid,requesteventnotes=@requesteventnotes,\r\n            sprequesteventassignmentid=@sprequesteventassignmentid,requesteventisrequired=@requesteventisrequired\r\nWHERE       sprequesteventid=@sprequesteventid";

		// Token: 0x040000FF RID: 255
		internal const string QU_REQUEST = "UPDATE    SPRequest SET notes=@notes,specialinstructions=@specialinstructions,isactive=@isactive,\r\n            sprequeststatustypeid=@sprequeststatustypeid,sprequestassignmentstatustypeid=@sprequestassignmentstatustypeid,spurgencyleveltypeid=@spurgencyleveltypeid\r\nWHERE sprequestid=@sprequestid";

		// Token: 0x04000100 RID: 256
		internal const string QD_COURSE_ASSIGNMENT = "SET @id=(SELECT sprequestcourseassignmentid FROM sprequestcourse WHERE sprequestcourseid=@sprequestcourseid)\r\nIF NOT @id IS NULL\r\nBEGIN\r\n    UPDATE sprequestcourse SET sprequestcourseassignmentid=NULL WHERE sprequestcourseid=@sprequestcourseid\r\n    DELETE FROM sprequestcourseassignment WHERE sprequestcourseassignmentid=@id\r\nEND";

		// Token: 0x04000101 RID: 257
		internal const string QD_EVENT_ASSIGNMENT = "SET @id=(SELECT sprequesteventassignmentid FROM sprequestevent WHERE sprequesteventid=@sprequesteventid)\r\nIF NOT @id IS NULL\r\nBEGIN\r\n    UPDATE sprequestevent SET sprequesteventassignmentid=NULL WHERE sprequesteventid=@sprequesteventid\r\n    DELETE FROM sprequesteventassignment WHERE sprequesteventassignmentid=@id\r\nEND";

		// Token: 0x04000102 RID: 258
		internal const string QD_REQUEST = "DELETE FROM sprequest WHERE sprequestid=@sprequestid";

		// Token: 0x04000103 RID: 259
		internal const string QD_REQUESTCOURSE_BY_REQUESTID = "DELETE FROM sprequestcourse WHERE sprequestid=@sprequestid";

		// Token: 0x04000104 RID: 260
		internal const string QD_REQUESTEVENT_BY_REQUESTID = "DELETE FROM sprequestevent WHERE sprequestid=@sprequestid";

		// Token: 0x04000105 RID: 261
		internal const string QD_REQUESTEVENTASSIGNMENT_BY_REQUESTID = "DELETE FROM sprequesteventassignment WHERE sprequesteventassignmentid IN (SELECT sprequesteventassignmentid FROM sprequestevent WHERE sprequestid=@sprequestid AND NOT sprequesteventassignmentid IS NULL )";

		// Token: 0x04000106 RID: 262
		internal const string QD_REQUESTCOURSEASSIGNMENT_BY_REQUESTID = "DELETE FROM sprequestcourseassignment WHERE sprequestcourseassignmentid IN (SELECT sprequestcourseassignmentid FROM sprequestcourse WHERE sprequestid=@sprequestid AND NOT sprequestcourseassignmentid IS NULL )";
	}
}

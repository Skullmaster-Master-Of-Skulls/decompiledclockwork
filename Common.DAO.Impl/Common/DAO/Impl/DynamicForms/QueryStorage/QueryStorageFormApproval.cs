using System;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.QueryStorage
{
	// Token: 0x020000E9 RID: 233
	internal static class QueryStorageFormApproval
	{
		// Token: 0x040003FB RID: 1019
		internal const string QS_FORMAPPROVAL = "SELECT fa.FormApprovalId,fa.screennum,fa.personid,fa.appointmentid,\r\nfa.datecreated,fa.whouploaded AS createdpersonid,p3.firstname AS createdfirstname,p3.middlename AS createdmiddlename,p3.lastname AS createdlastname,p3.student_no AS createdstudent_no,\r\nfa.CurrentStateId,fa.ApprovedFormApprovalSignatureId,fa.SubmittedFormApprovalSignatureId,\r\np.firstname,p.middlename,p.lastname,p.student_no,p.isactive,\r\nfac.FormApprovalCommentId,fac.datecreated AS commentdatecreated,fac.commenttext,\r\nfac.whocreated AS commentpersonid,p2.firstname AS commentfirstname,p2.middlename AS commentmiddlename,\r\np2.lastname AS commentlastname,p2.student_no AS commentstudent_no\r\nFROM    FormApproval fa LEFT JOIN people p ON p.personid=fa.personid\r\nLEFT JOIN FormApprovalComment fac ON fac.FormApprovalId=fa.FormApprovalId\r\nLEFT JOIN people p2 ON p2.personid=fac.whocreated\r\nLEFT JOIN people p3 ON p3.personid=fa.whouploaded\r\nWHERE fa.screennum=@screennum AND fa.personid=@pid AND fa.appointmentid=@appid\r\nORDER BY fac.datecreated DESC";

		// Token: 0x040003FC RID: 1020
		internal const string QI_FORMAPPROVAL_COMMENT = "INSERT INTO FormApprovalComment (FormApprovalId,WhoCreated,CommentText) VALUES (@formApprovalId,@whoamipid,@commentText)";

		// Token: 0x040003FD RID: 1021
		internal const string QU_FORMAPPROVAL_STATUS = "UPDATE FormApproval SET CurrentStateId=@newStatus WHERE FormApprovalId=@formApprovalId";

		// Token: 0x040003FE RID: 1022
		internal const string QS_SCREENNUM_BY_FORMAPPROVALID = "SELECT screennum FROM FormApproval WHERE FormApprovalId=@formApprovalId";

		// Token: 0x040003FF RID: 1023
		internal const string QS_PENDING_ITEMS_BY_USER = "DECLARE @approvedStatusCode int = 4\r\nSELECT orderid AS screennum INTO #tscreennums FROM splitorderids(@screennums,',')\r\n\r\nSELECT\tfa.FormApprovalId,fa.screennum,s.[description] AS screentitle,\r\n\t\tfa.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\tfa.appointmentid,app.startdate,app.ishidden,app.islocked,app.personid AS whobookedpersonid,\r\n\t\tfa.datecreated,\r\n\t\tfa.CurrentStateId,\r\n\t\tMAX(fac.DateCreated) AS LastModifiedDate\r\nFROM\tFormApproval fa LEFT JOIN people p ON p.personid=fa.personid\r\n\t\tLEFT JOIN appointments app ON app.appointmentid=fa.appointmentid\r\n\t\tLEFT JOIN screens s ON s.screennum=fa.screennum\r\n\t\tLEFT JOIN FormApprovalComment fac ON fac.FormApprovalId=fa.FormApprovalId\r\nWHERE\tfa.screennum IN (SELECT screennum FROM #tscreennums) AND NOT fa.CurrentStateId=@approvedStatusCode\r\nGROUP BY fa.FormApprovalId,fa.screennum,s.[description],\r\n\t\tfa.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\tfa.appointmentid,app.startdate,app.ishidden,app.islocked,app.personid,\r\n\t\tfa.datecreated,\r\n\t\tfa.CurrentStateId\r\nORDER BY LastModifiedDate,s.[description]\r\n\r\nDROP TABLE #tscreennums";

		// Token: 0x04000400 RID: 1024
		internal const string QS_FORMAPPROVAL_STATUS_BY_PID_APPID_SCREENNUM = "SELECT fa.CurrentStateId FROM FormApproval fa WHERE fa.personid=@pid AND fa.appointmentid=@appid AND fa.screennum=@screennum";
	}
}

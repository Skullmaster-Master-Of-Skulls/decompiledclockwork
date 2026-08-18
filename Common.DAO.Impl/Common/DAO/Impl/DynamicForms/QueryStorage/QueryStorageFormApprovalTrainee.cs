using System;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.QueryStorage
{
	// Token: 0x020000EB RID: 235
	internal static class QueryStorageFormApprovalTrainee
	{
		// Token: 0x04000404 RID: 1028
		internal const string QI_FORMAPPROVAL = "DECLARE @existingId uniqueidentifier = (SELECT TOP 1 FormApprovalId FROM FormApproval WHERE screennum=@screennum AND personid=@pid AND appointmentid=@appid)\r\nIF NOT @existingId IS NULL \r\nBEGIN\r\n    SET @formApprovalId = @existingId\r\nEND\r\nELSE\r\nBEGIN\r\n    SET @formApprovalId = newid()\r\n    INSERT INTO FormApproval (FormApprovalId,screennum,personid,appointmentid,DateCreated,WhoUploaded,CurrentStateId)\r\n    VALUES (@formApprovalId,@screennum,@pid,@appid,getdate(),@whoamipid,@currentstate)\r\nEND";

		// Token: 0x04000405 RID: 1029
		internal const string QI_FORMAPPROVAL_TRAINEE_SIGNATURE_CREATE_OR_UPDATE = "DECLARE @existingFormApprovalSignatureId uniqueidentifier = (SELECT TOP 1 SubmittedFormApprovalSignatureId FROM FormApproval WHERE FormApprovalId=@formApprovalId)\r\nIF NOT @existingFormApprovalSignatureId IS NULL \r\nBEGIN\r\n    DELETE FROM FormApprovalSignature WHERE FormApprovalSignatureId=@existingFormApprovalSignatureId\r\nEND\r\n\r\nSET @formApprovalSignatureId = newid()\r\nINSERT INTO FormApprovalSignature (FormApprovalSignatureId,personid,signatureText,signatureImage,DateCreated) VALUES (@formApprovalSignatureId,@whoamipid,@signatureText,@signatureImage,getdate())\r\n\r\nUPDATE FormApproval SET SubmittedFormApprovalSignatureId=@formApprovalSignatureId,CurrentStateId=@currentstate WHERE formApprovalId=@formApprovalId";

		// Token: 0x04000406 RID: 1030
		internal const string QS_TRAINEE_SIGNATURE = "SELECT    fa.FormApprovalId,s.FormApprovalSignatureId,s.signatureText,s.signatureImage,s.DateCreated,\r\n    s.personid,p.lastname,p.firstname,p.middlename,p.student_no\r\nFROM    FormApproval fa LEFT JOIN FormApprovalSignature s ON s.FormApprovalSignatureId=fa.SubmittedFormApprovalSignatureId\r\n        LEFT JOIN people p ON p.personid=s.personid\r\nWHERE   fa.FormApprovalId=@formApprovalId";
	}
}

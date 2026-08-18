using System;

namespace TechnoPro.Common.DAO.Impl.DynamicForms.QueryStorage
{
	// Token: 0x020000EA RID: 234
	internal static class QueryStorageFormApprovalSupervisor
	{
		// Token: 0x04000401 RID: 1025
		internal const string QI_FORMAPPROVAL_SUPERVISOR_SIGNATURE_CREATE_OR_UPDATE = "DECLARE @existingFormApprovalSignatureId uniqueidentifier = (SELECT TOP 1 ApprovedFormApprovalSignatureId FROM FormApproval WHERE FormApprovalId=@formApprovalId)\r\nIF NOT @existingFormApprovalSignatureId IS NULL \r\nBEGIN\r\n    DELETE FROM FormApprovalSignature WHERE FormApprovalSignatureId=@existingFormApprovalSignatureId\r\nEND\r\n\r\nSET @formApprovalSignatureId = newid()\r\nINSERT INTO FormApprovalSignature (FormApprovalSignatureId,personid,signatureText,signatureImage,DateCreated) VALUES (@formApprovalSignatureId,@whoamipid,@signatureText,@signatureImage,getdate())\r\n\r\nUPDATE FormApproval SET ApprovedFormApprovalSignatureId=@formApprovalSignatureId,CurrentStateId=@currentstate WHERE formApprovalId=@formApprovalId";

		// Token: 0x04000402 RID: 1026
		internal const string QD_REMOVE_SUPERVISOR_SIGNATURE = "DECLARE @existingFormApprovalSignatureId uniqueidentifier = (SELECT TOP 1 ApprovedFormApprovalSignatureId FROM FormApproval WHERE FormApprovalId=@formApprovalId)\r\nIF NOT @existingFormApprovalSignatureId IS NULL \r\nBEGIN\r\n    DELETE FROM FormApprovalSignature WHERE FormApprovalSignatureId=@existingFormApprovalSignatureId\r\nEND\r\n\r\nUPDATE FormApproval SET ApprovedFormApprovalSignatureId=NULL,CurrentStateId=@currentstate WHERE formApprovalId=@formApprovalId";

		// Token: 0x04000403 RID: 1027
		internal const string QS_SUPERVISOR_SIGNATURE = "SELECT    fa.FormApprovalId,s.FormApprovalSignatureId,s.signatureText,s.signatureImage,s.DateCreated,\r\n    s.personid,p.lastname,p.firstname,p.middlename,p.student_no\r\nFROM    FormApproval fa LEFT JOIN FormApprovalSignature s ON s.FormApprovalSignatureId=fa.ApprovedFormApprovalSignatureId\r\n        LEFT JOIN people p ON p.personid=s.personid\r\nWHERE   fa.FormApprovalId=@formApprovalId";
	}
}

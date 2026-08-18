using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.ICore.DynamicForms.FormApproval
{
	// Token: 0x020000A0 RID: 160
	public interface IFormApprovalSupervisorManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004BB RID: 1211
		FormApprovalForAppointment LoadFormApproval(int screenNum, int studentPersonId, int appId);

		// Token: 0x060004BC RID: 1212
		void AddFormApprovalComment(Guid formApprovalId, FormApprovalCommentText comment);

		// Token: 0x060004BD RID: 1213
		void ApproveForm(Guid formApprovalId, FormApprovalCommentText comment, FormApprovalSignature supervisorSignature);

		// Token: 0x060004BE RID: 1214
		void SendFormBackToTrainee(Guid formApprovalId, FormApprovalCommentText comment);

		// Token: 0x060004BF RID: 1215
		void UnApproveFormApproval(Guid formApprovalId, FormApprovalCommentText comment);

		// Token: 0x060004C0 RID: 1216
		FormApprovalSignature LoadSupervisorSignature(Guid formApprovalId);
	}
}

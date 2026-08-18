using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.ICore.DynamicForms.FormApproval
{
	// Token: 0x020000A1 RID: 161
	public interface IFormApprovalTraineeManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004C1 RID: 1217
		FormApprovalForAppointment LoadFormApproval(int screenNum, int studentPersonId, int appId);

		// Token: 0x060004C2 RID: 1218
		void AddFormApprovalComment(Guid formApprovalId, FormApprovalCommentText comment);

		// Token: 0x060004C3 RID: 1219
		void ReSubmitFormApprovalForm(Guid formApprovalId, FormApprovalCommentText comment);

		// Token: 0x060004C4 RID: 1220
		Guid CreateFormApprovalForm(int screenNum, int studentPersonId, int appId, FormApprovalCommentText comment, FormApprovalSignature traineeSignature);

		// Token: 0x060004C5 RID: 1221
		FormApprovalSignature LoadTraineeSignature(Guid formApprovalId);
	}
}

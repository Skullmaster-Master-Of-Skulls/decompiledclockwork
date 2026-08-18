using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.DAO.DynamicForms.FormApproval
{
	// Token: 0x0200008A RID: 138
	public interface IFormApprovalTraineeDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600039E RID: 926
		Guid CreateFormApproval(int screenNum, int studentPersonId, int appId);

		// Token: 0x0600039F RID: 927
		Guid CreateOrUpdateFormApprovalTraineeSignature(Guid formApprovalId, FormApprovalSignature traineeSignature);

		// Token: 0x060003A0 RID: 928
		FormApprovalSignature LoadTraineeSignature(Guid formApprovalId);
	}
}

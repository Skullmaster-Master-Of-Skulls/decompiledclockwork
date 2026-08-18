using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.DAO.DynamicForms.FormApproval
{
	// Token: 0x02000089 RID: 137
	public interface IFormApprovalSupervisorDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600039B RID: 923
		Guid CreateOrUpdateFormApprovalSupervisorSignature(Guid formApprovalId, FormApprovalSignature supervisorSignature);

		// Token: 0x0600039C RID: 924
		void RemoveFormApprovalSupervisorSignature(Guid formApprovalId);

		// Token: 0x0600039D RID: 925
		FormApprovalSignature LoadSupervisorSignature(Guid formApprovalId);
	}
}

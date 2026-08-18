using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.DAO.DynamicForms.FormApproval
{
	// Token: 0x02000088 RID: 136
	public interface IFormApprovalDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000395 RID: 917
		void AddFormApprovalComment(Guid formApprovalId, string commentText);

		// Token: 0x06000396 RID: 918
		void UpdateFormApprovalCurrentStatus(Guid formApprovalId, eFormApprovalState newStatus);

		// Token: 0x06000397 RID: 919
		FormApprovalForAppointment LoadFormApproval(int screenNum, int studentPersonId, int appId);

		// Token: 0x06000398 RID: 920
		int GetScreenNumForFormApproval(Guid formApprovalId);

		// Token: 0x06000399 RID: 921
		IList<FormApprovalPendingItem> LoadPendingFormApprovalItemsForUser(int pid, int[] screenNums);

		// Token: 0x0600039A RID: 922
		eFormApprovalState LoadFormApprovalStatus(int studentPersonId, int appId, int screenNum);
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.ICore.DynamicForms.FormApproval
{
	// Token: 0x0200009F RID: 159
	public interface IFormApprovalManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004B4 RID: 1204
		FormApprovalScreenUserOptions GetFormApprovalScreenUserForLoggedInUserOptions(int screenNum);

		// Token: 0x060004B5 RID: 1205
		int GetScreenNumForFormApproval(Guid formApprovalId);

		// Token: 0x060004B6 RID: 1206
		IList<FormApprovalPendingItem> LoadPendingFormApprovalItemsForCurrentUser();

		// Token: 0x060004B7 RID: 1207
		eFormApprovalState LoadFormApprovalStatus(int studentPersonId, int appId, int screenNum);

		// Token: 0x060004B8 RID: 1208
		FormApprovalPendingItem LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Guid formApprovalId);

		// Token: 0x060004B9 RID: 1209
		bool AreAnyFormApprovalScreensEnabledForLoggedInUser();

		// Token: 0x060004BA RID: 1210
		IDictionary<int, bool> GetActiveFormApprovalScreenNumsWithAdminStatus(int personId);
	}
}

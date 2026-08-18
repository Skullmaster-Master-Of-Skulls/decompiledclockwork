using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x02000061 RID: 97
	public interface IFormApprovalClientManager : IWebService
	{
		// Token: 0x060002F4 RID: 756
		FormApprovalScreenUserOptionsDTO GetFormApprovalScreenUserForLoggedInUserOptions(int screenNum);

		// Token: 0x060002F5 RID: 757
		IList<FormApprovalPendingItemDTO> LoadPendingFormApprovalItemsForCurrentUser();

		// Token: 0x060002F6 RID: 758
		eFormApprovalState LoadFormApprovalStatus(int studentPersonId, int appId, int screenNum);

		// Token: 0x060002F7 RID: 759
		FormApprovalPendingItemDTO LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Guid formApprovalId);

		// Token: 0x060002F8 RID: 760
		bool AreAnyFormApprovalScreensEnabledForLoggedInUser();

		// Token: 0x060002F9 RID: 761
		IDictionary<int, bool> GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser();
	}
}

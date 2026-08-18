using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200008F RID: 143
	internal class FormApprovalClientBaseProxy : ClientBase<IFormApproval>, IFormApproval, IService
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00010AAC File Offset: 0x0000ECAC
		public FormApprovalClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00010AB7 File Offset: 0x0000ECB7
		public FormApprovalClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		public GetFormApprovalScreenUserForLoggedInUserOptionsResp GetFormApprovalScreenUserForLoggedInUserOptions(GetFormApprovalScreenUserForLoggedInUserOptionsReq Request)
		{
			return base.Channel.GetFormApprovalScreenUserForLoggedInUserOptions(Request);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		public LoadPendingFormApprovalItemsForCurrentUserResp LoadPendingFormApprovalItemsForCurrentUser(LoadPendingFormApprovalItemsForCurrentUserReq Request)
		{
			return base.Channel.LoadPendingFormApprovalItemsForCurrentUser(Request);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00010B04 File Offset: 0x0000ED04
		public LoadFormApprovalStatusResp LoadFormApprovalStatus(LoadFormApprovalStatusReq Request)
		{
			return base.Channel.LoadFormApprovalStatus(Request);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00010B24 File Offset: 0x0000ED24
		public LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdResp LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq Request)
		{
			return base.Channel.LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Request);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00010B44 File Offset: 0x0000ED44
		public AreAnyFormApprovalScreensEnabledForLoggedInUserResp AreAnyFormApprovalScreensEnabledForLoggedInUser(AreAnyFormApprovalScreensEnabledForLoggedInUserReq Request)
		{
			return base.Channel.AreAnyFormApprovalScreensEnabledForLoggedInUser(Request);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00010B64 File Offset: 0x0000ED64
		public GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserResp GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser(GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserReq Request)
		{
			return base.Channel.GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser(Request);
		}
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200008E RID: 142
	public class FormApprovalReusableClientProxy : WCFTokenBasedReusableClientProxy<IFormApproval>, IFormApproval, IService
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x00010942 File Offset: 0x0000EB42
		public FormApprovalReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001094D File Offset: 0x0000EB4D
		public FormApprovalReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001095C File Offset: 0x0000EB5C
		public GetFormApprovalScreenUserForLoggedInUserOptionsResp GetFormApprovalScreenUserForLoggedInUserOptions(GetFormApprovalScreenUserForLoggedInUserOptionsReq Request)
		{
			return this.WrapServiceMethod<GetFormApprovalScreenUserForLoggedInUserOptionsResp>(() => this.Proxy.GetFormApprovalScreenUserForLoggedInUserOptions(Request));
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00010994 File Offset: 0x0000EB94
		public LoadPendingFormApprovalItemsForCurrentUserResp LoadPendingFormApprovalItemsForCurrentUser(LoadPendingFormApprovalItemsForCurrentUserReq Request)
		{
			return this.WrapServiceMethod<LoadPendingFormApprovalItemsForCurrentUserResp>(() => this.Proxy.LoadPendingFormApprovalItemsForCurrentUser(Request));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000109CC File Offset: 0x0000EBCC
		public LoadFormApprovalStatusResp LoadFormApprovalStatus(LoadFormApprovalStatusReq Request)
		{
			return this.WrapServiceMethod<LoadFormApprovalStatusResp>(() => this.Proxy.LoadFormApprovalStatus(Request));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00010A04 File Offset: 0x0000EC04
		public LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdResp LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq Request)
		{
			return this.WrapServiceMethod<LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdResp>(() => this.Proxy.LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Request));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public AreAnyFormApprovalScreensEnabledForLoggedInUserResp AreAnyFormApprovalScreensEnabledForLoggedInUser(AreAnyFormApprovalScreensEnabledForLoggedInUserReq Request)
		{
			return this.WrapServiceMethod<AreAnyFormApprovalScreensEnabledForLoggedInUserResp>(() => this.Proxy.AreAnyFormApprovalScreensEnabledForLoggedInUser(Request));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00010A74 File Offset: 0x0000EC74
		public GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserResp GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser(GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserReq Request)
		{
			return this.WrapServiceMethod<GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserResp>(() => this.Proxy.GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUser(Request));
		}
	}
}

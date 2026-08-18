using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200010E RID: 270
	public class StaffCommonInfoReusableClientProxy : WCFTokenBasedReusableClientProxy<IStaffCommonInfo>, IStaffCommonInfo, IService
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001B12E File Offset: 0x0001932E
		public StaffCommonInfoReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001B139 File Offset: 0x00019339
		public StaffCommonInfoReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001B148 File Offset: 0x00019348
		public LoadStaffStoredSignatureResp LoadStaffStoredSignature(LoadStaffStoredSignatureReq Request)
		{
			return this.WrapServiceMethod<LoadStaffStoredSignatureResp>(() => this.Proxy.LoadStaffStoredSignature(Request));
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001B180 File Offset: 0x00019380
		public SaveStaffStoredSignatureResp SaveStaffStoredSignature(SaveStaffStoredSignatureReq Request)
		{
			return this.WrapServiceMethod<SaveStaffStoredSignatureResp>(() => this.Proxy.SaveStaffStoredSignature(Request));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001B1B8 File Offset: 0x000193B8
		public LoadAssignedAdvisorSignatureDataResp LoadAssignedAdvisorSignatureData(LoadAssignedAdvisorSignatureDataReq Request)
		{
			return this.WrapServiceMethod<LoadAssignedAdvisorSignatureDataResp>(() => this.Proxy.LoadAssignedAdvisorSignatureData(Request));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001B1F0 File Offset: 0x000193F0
		public LoadStaffSignatureDataResp LoadStaffStoredSignatureData(LoadStaffSignatureDataReq Request)
		{
			return this.WrapServiceMethod<LoadStaffSignatureDataResp>(() => this.Proxy.LoadStaffStoredSignatureData(Request));
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001B228 File Offset: 0x00019428
		public SaveAssignedAdvisorStoredSignatureResp SaveAssignedAdvisorStoredSignature(SaveAssignedAdvisorStoredSignatureReq Request)
		{
			return this.WrapServiceMethod<SaveAssignedAdvisorStoredSignatureResp>(() => this.Proxy.SaveAssignedAdvisorStoredSignature(Request));
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001B260 File Offset: 0x00019460
		public SaveAssignedAdvisorStoredSignatureWithImageBytesResp SaveAssignedAdvisorStoredSignatureWithImageBytes(SaveAssignedAdvisorStoredSignatureWithImageBytesReq Request)
		{
			return this.WrapServiceMethod<SaveAssignedAdvisorStoredSignatureWithImageBytesResp>(() => this.Proxy.SaveAssignedAdvisorStoredSignatureWithImageBytes(Request));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001B298 File Offset: 0x00019498
		public LoadStaffWithCommonInfoByIdResp LoadStaffWithCommonInfoById(LoadStaffWithCommonInfoByIdReq Request)
		{
			return this.WrapServiceMethod<LoadStaffWithCommonInfoByIdResp>(() => this.Proxy.LoadStaffWithCommonInfoById(Request));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public void UpdateCommonInfo(UpdateCommonInfoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateCommonInfo(Request);
			});
		}
	}
}

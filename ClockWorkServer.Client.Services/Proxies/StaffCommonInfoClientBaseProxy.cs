using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200010F RID: 271
	internal class StaffCommonInfoClientBaseProxy : ClientBase<IStaffCommonInfo>, IStaffCommonInfo, IService
	{
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001B305 File Offset: 0x00019505
		public StaffCommonInfoClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001B310 File Offset: 0x00019510
		public StaffCommonInfoClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001B31C File Offset: 0x0001951C
		public LoadStaffStoredSignatureResp LoadStaffStoredSignature(LoadStaffStoredSignatureReq Request)
		{
			return base.Channel.LoadStaffStoredSignature(Request);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001B33C File Offset: 0x0001953C
		public SaveStaffStoredSignatureResp SaveStaffStoredSignature(SaveStaffStoredSignatureReq Request)
		{
			return base.Channel.SaveStaffStoredSignature(Request);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001B35C File Offset: 0x0001955C
		public LoadAssignedAdvisorSignatureDataResp LoadAssignedAdvisorSignatureData(LoadAssignedAdvisorSignatureDataReq Request)
		{
			return base.Channel.LoadAssignedAdvisorSignatureData(Request);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001B37C File Offset: 0x0001957C
		public LoadStaffSignatureDataResp LoadStaffStoredSignatureData(LoadStaffSignatureDataReq Request)
		{
			return base.Channel.LoadStaffStoredSignatureData(Request);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001B39C File Offset: 0x0001959C
		public SaveAssignedAdvisorStoredSignatureResp SaveAssignedAdvisorStoredSignature(SaveAssignedAdvisorStoredSignatureReq Request)
		{
			return base.Channel.SaveAssignedAdvisorStoredSignature(Request);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001B3BC File Offset: 0x000195BC
		public SaveAssignedAdvisorStoredSignatureWithImageBytesResp SaveAssignedAdvisorStoredSignatureWithImageBytes(SaveAssignedAdvisorStoredSignatureWithImageBytesReq Request)
		{
			return base.Channel.SaveAssignedAdvisorStoredSignatureWithImageBytes(Request);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001B3DC File Offset: 0x000195DC
		public LoadStaffWithCommonInfoByIdResp LoadStaffWithCommonInfoById(LoadStaffWithCommonInfoByIdReq Request)
		{
			return base.Channel.LoadStaffWithCommonInfoById(Request);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001B3FA File Offset: 0x000195FA
		public void UpdateCommonInfo(UpdateCommonInfoReq Request)
		{
			base.Channel.UpdateCommonInfo(Request);
		}
	}
}

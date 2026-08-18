using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000061 RID: 97
	public class DailyJobReusableClientProxy : WCFTokenBasedReusableClientProxy<IDailyJob>, IDailyJob, IService
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000C436 File Offset: 0x0000A636
		public DailyJobReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000C441 File Offset: 0x0000A641
		public DailyJobReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000C450 File Offset: 0x0000A650
		public void ChangeTaskActiveStatus(ChangeTaskActiveStatusReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ChangeTaskActiveStatus(Request);
			});
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000C488 File Offset: 0x0000A688
		public CreateDailyJobTaskResp CreateDailyJobTask(CreateDailyJobTaskReq Request)
		{
			return this.WrapServiceMethod<CreateDailyJobTaskResp>(() => this.Proxy.CreateDailyJobTask(Request));
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		public void DeleteDailyJobTask(DeleteDailyJobTaskReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteDailyJobTask(Request);
			});
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public LoadDailyJobTaskByIdResp LoadDailyJobTaskById(LoadDailyJobTaskByIdReq Request)
		{
			return this.WrapServiceMethod<LoadDailyJobTaskByIdResp>(() => this.Proxy.LoadDailyJobTaskById(Request));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000C530 File Offset: 0x0000A730
		public LoadDailyJobTasksByGroupResp LoadDailyJobTasksByGroup(LoadDailyJobTasksByGroupReq Request)
		{
			return this.WrapServiceMethod<LoadDailyJobTasksByGroupResp>(() => this.Proxy.LoadDailyJobTasksByGroup(Request));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000C568 File Offset: 0x0000A768
		public RunDailyJobResp RunDailyJob(RunDailyJobReq Request)
		{
			return this.WrapServiceMethod<RunDailyJobResp>(() => this.Proxy.RunDailyJob(Request));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public void UpdateDailyJobTask(UpdateDailyJobTaskReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateDailyJobTask(Request);
			});
		}
	}
}

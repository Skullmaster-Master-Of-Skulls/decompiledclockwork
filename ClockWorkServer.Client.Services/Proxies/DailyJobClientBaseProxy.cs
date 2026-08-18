using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000062 RID: 98
	internal class DailyJobClientBaseProxy : ClientBase<IDailyJob>, IDailyJob, IService
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0000C5D5 File Offset: 0x0000A7D5
		public DailyJobClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public DailyJobClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public void ChangeTaskActiveStatus(ChangeTaskActiveStatusReq Request)
		{
			base.Channel.ChangeTaskActiveStatus(Request);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public CreateDailyJobTaskResp CreateDailyJobTask(CreateDailyJobTaskReq Request)
		{
			return base.Channel.CreateDailyJobTask(Request);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C61A File Offset: 0x0000A81A
		public void DeleteDailyJobTask(DeleteDailyJobTaskReq Request)
		{
			base.Channel.DeleteDailyJobTask(Request);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C62C File Offset: 0x0000A82C
		public LoadDailyJobTaskByIdResp LoadDailyJobTaskById(LoadDailyJobTaskByIdReq Request)
		{
			return base.Channel.LoadDailyJobTaskById(Request);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000C64C File Offset: 0x0000A84C
		public LoadDailyJobTasksByGroupResp LoadDailyJobTasksByGroup(LoadDailyJobTasksByGroupReq Request)
		{
			return base.Channel.LoadDailyJobTasksByGroup(Request);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000C66C File Offset: 0x0000A86C
		public RunDailyJobResp RunDailyJob(RunDailyJobReq Request)
		{
			return base.Channel.RunDailyJob(Request);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000C68A File Offset: 0x0000A88A
		public void UpdateDailyJobTask(UpdateDailyJobTaskReq Request)
		{
			base.Channel.UpdateDailyJobTask(Request);
		}
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200014B RID: 331
	public class TaskReusableClientProxy : WCFTokenBasedReusableClientProxy<ITask>, ITask, IService
	{
		// Token: 0x06000CA8 RID: 3240 RVA: 0x0001F816 File Offset: 0x0001DA16
		public TaskReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0001F821 File Offset: 0x0001DA21
		public TaskReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0001F830 File Offset: 0x0001DA30
		public void ChangeTaskCompletedStatus(ChangeTaskCompletedStatusReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ChangeTaskCompletedStatus(Request);
			});
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0001F868 File Offset: 0x0001DA68
		public CreateTaskResp CreateTask(CreateTaskReq Request)
		{
			return this.WrapServiceMethod<CreateTaskResp>(() => this.Proxy.CreateTask(Request));
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		public void DeleteTask(DeleteTaskReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteTask(Request);
			});
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		public void UpdateTask(UpdateTaskReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateTask(Request);
			});
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0001F910 File Offset: 0x0001DB10
		public LoadCompletedTasksResp LoadCompletedTasks(LoadCompletedTasksReq Request)
		{
			return this.WrapServiceMethod<LoadCompletedTasksResp>(() => this.Proxy.LoadCompletedTasks(Request));
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0001F948 File Offset: 0x0001DB48
		public LoadTasksResp LoadTasks(LoadTasksReq Request)
		{
			return this.WrapServiceMethod<LoadTasksResp>(() => this.Proxy.LoadTasks(Request));
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0001F980 File Offset: 0x0001DB80
		public LoadTaskByIdResp LoadTaskById(LoadTaskByIdReq Request)
		{
			return this.WrapServiceMethod<LoadTaskByIdResp>(() => this.Proxy.LoadTaskById(Request));
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0001F9B8 File Offset: 0x0001DBB8
		public void ChangeRemoveFromListStatus(ChangeRemoveFromListStatusReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ChangeRemoveFromListStatus(Request);
			});
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0001F9F0 File Offset: 0x0001DBF0
		public LoadCompletedTasksAsTreeResp LoadCompletedTasksAsTree(LoadCompletedTasksAsTreeReq Request)
		{
			return this.WrapServiceMethod<LoadCompletedTasksAsTreeResp>(() => this.Proxy.LoadCompletedTasksAsTree(Request));
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0001FA28 File Offset: 0x0001DC28
		public LoadTasksAsTreeResp LoadTasksAsTree(LoadTasksAsTreeReq Request)
		{
			return this.WrapServiceMethod<LoadTasksAsTreeResp>(() => this.Proxy.LoadTasksAsTree(Request));
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0001FA60 File Offset: 0x0001DC60
		public LoadTaskNotesByTaskIdResp LoadTaskNotesByTaskId(LoadTaskNotesByTaskIdReq Request)
		{
			return this.WrapServiceMethod<LoadTaskNotesByTaskIdResp>(() => this.Proxy.LoadTaskNotesByTaskId(Request));
		}
	}
}

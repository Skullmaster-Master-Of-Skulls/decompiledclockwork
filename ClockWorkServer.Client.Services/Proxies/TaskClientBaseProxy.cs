using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200014C RID: 332
	internal class TaskClientBaseProxy : ClientBase<ITask>, ITask, IService
	{
		// Token: 0x06000CB5 RID: 3253 RVA: 0x0001FA98 File Offset: 0x0001DC98
		public TaskClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0001FAA3 File Offset: 0x0001DCA3
		public TaskClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0001FAAF File Offset: 0x0001DCAF
		public void ChangeTaskCompletedStatus(ChangeTaskCompletedStatusReq Request)
		{
			base.Channel.ChangeTaskCompletedStatus(Request);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0001FAC0 File Offset: 0x0001DCC0
		public CreateTaskResp CreateTask(CreateTaskReq Request)
		{
			return base.Channel.CreateTask(Request);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0001FADE File Offset: 0x0001DCDE
		public void DeleteTask(DeleteTaskReq Request)
		{
			base.Channel.DeleteTask(Request);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0001FAEE File Offset: 0x0001DCEE
		public void UpdateTask(UpdateTaskReq Request)
		{
			base.Channel.UpdateTask(Request);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0001FB00 File Offset: 0x0001DD00
		public LoadCompletedTasksResp LoadCompletedTasks(LoadCompletedTasksReq Request)
		{
			return base.Channel.LoadCompletedTasks(Request);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0001FB20 File Offset: 0x0001DD20
		public LoadTasksResp LoadTasks(LoadTasksReq Request)
		{
			return base.Channel.LoadTasks(Request);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0001FB40 File Offset: 0x0001DD40
		public LoadTaskByIdResp LoadTaskById(LoadTaskByIdReq Request)
		{
			return base.Channel.LoadTaskById(Request);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0001FB5E File Offset: 0x0001DD5E
		public void ChangeRemoveFromListStatus(ChangeRemoveFromListStatusReq Request)
		{
			base.Channel.ChangeRemoveFromListStatus(Request);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0001FB70 File Offset: 0x0001DD70
		public LoadCompletedTasksAsTreeResp LoadCompletedTasksAsTree(LoadCompletedTasksAsTreeReq Request)
		{
			return base.Channel.LoadCompletedTasksAsTree(Request);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0001FB90 File Offset: 0x0001DD90
		public LoadTasksAsTreeResp LoadTasksAsTree(LoadTasksAsTreeReq Request)
		{
			return base.Channel.LoadTasksAsTree(Request);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
		public LoadTaskNotesByTaskIdResp LoadTaskNotesByTaskId(LoadTaskNotesByTaskIdReq Request)
		{
			return base.Channel.LoadTaskNotesByTaskId(Request);
		}
	}
}

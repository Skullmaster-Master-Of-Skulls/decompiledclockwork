using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Tasks
{
	// Token: 0x0200000E RID: 14
	public class TaskRestClientManager : BearerTokenRestProxy<ITaskClientManager>, ITaskClientManager, IWebService
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003235 File Offset: 0x00001435
		public TaskRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000323F File Offset: 0x0000143F
		public TaskRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000324A File Offset: 0x0000144A
		public IList<TaskDTO> LoadTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPartDTO PartsToLoad)
		{
			return base.GetMany<TaskDTO>(string.Format("task?includeprivate={0}&includeshared={1}&includeassigned={2}&parts={3}", new object[]
			{
				IncludePrivateTasks,
				IncludeSharedTasks,
				IncludeAssignedTasks,
				PartsToLoad
			}), true);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003288 File Offset: 0x00001488
		public IList<TaskDTO> LoadCompletedTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<TaskDTO>(string.Format("task/completed/range/{0}/{1}?includeprivate={2}&includeshared={3}&includeassigned={4}", new object[]
			{
				StartDate,
				EndDate,
				IncludePrivateTasks,
				IncludeSharedTasks,
				IncludeAssignedTasks
			}), true);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000032DC File Offset: 0x000014DC
		public Forest<TaskOrGroupDTO> LoadTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPartDTO PartsToLoad, out List<TaskDTO> Tasks, out List<TaskGroupDTO> Groups)
		{
			LoadTasksAsTreeResp loadTasksAsTreeResp = base.Get<LoadTasksAsTreeResp>(string.Format("task/astree?includeprivate={0}&includeshared={1}&includeassigned={2}&parts={3}", new object[]
			{
				IncludePrivateTasks,
				IncludeSharedTasks,
				IncludeAssignedTasks,
				PartsToLoad
			}), true);
			Tasks = loadTasksAsTreeResp.Tasks;
			Groups = loadTasksAsTreeResp.Groups;
			return loadTasksAsTreeResp.Tree;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003340 File Offset: 0x00001540
		public Forest<TaskOrGroupDTO> LoadCompletedTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate, out List<TaskDTO> Tasks, out List<TaskGroupDTO> Groups)
		{
			LoadTasksAsTreeResp loadTasksAsTreeResp = base.Get<LoadTasksAsTreeResp>(string.Format("task/completedastree/range/{0}/{1}?includeprivate={2}&includeshared={3}&includeassigned={4}", new object[]
			{
				StartDate,
				EndDate,
				IncludePrivateTasks,
				IncludeSharedTasks,
				IncludeAssignedTasks
			}), true);
			Tasks = loadTasksAsTreeResp.Tasks;
			Groups = loadTasksAsTreeResp.Groups;
			return loadTasksAsTreeResp.Tree;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033AC File Offset: 0x000015AC
		public int CreateTask(TaskDTO Task)
		{
			return base.Post<TaskDTO, int>(Task, "task");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033BA File Offset: 0x000015BA
		public void DeleteTask(int TaskId)
		{
			base.Delete(string.Format("task/taskid/{0}", TaskId));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000033D2 File Offset: 0x000015D2
		public void UpdateTask(TaskDTO Task)
		{
			base.Put<TaskDTO>(Task, "task");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033E0 File Offset: 0x000015E0
		public void ChangeTaskCompletedStatus(int TaskId, bool NewCompletedStatus)
		{
			ChangeTaskCompletedStatusReq changeTaskCompletedStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeTaskCompletedStatusReq>();
			changeTaskCompletedStatusReq.TaskId = TaskId;
			changeTaskCompletedStatusReq.NewCompletedStatus = NewCompletedStatus;
			base.Post<ChangeTaskCompletedStatusReq>(changeTaskCompletedStatusReq, "task/changetaskcompletedstatus");
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003412 File Offset: 0x00001612
		public TaskDTO LoadTaskById(int TaskId)
		{
			return base.Get<TaskDTO>(string.Format("task/taskid/{0}", TaskId), true);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000342C File Offset: 0x0000162C
		public void ChangeRemoveFromListStatus(int TaskId, bool NewRemoveFromListStatus)
		{
			ChangeRemoveFromListStatusReq changeRemoveFromListStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeRemoveFromListStatusReq>();
			changeRemoveFromListStatusReq.TaskId = TaskId;
			changeRemoveFromListStatusReq.NewRemoveFromListStatus = NewRemoveFromListStatus;
			base.Post<ChangeRemoveFromListStatusReq>(changeRemoveFromListStatusReq, "task/changeremovefromliststatus");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000345E File Offset: 0x0000165E
		public IList<TaskNoteDTO> LoadTaskNotesByTaskId(int TaskId)
		{
			return base.GetMany<TaskNoteDTO>(string.Format("task/notes/taskid/{0}", TaskId), true);
		}
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Tasks;
using TechnoPro.Common.DAO.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Tasks
{
	// Token: 0x02000037 RID: 55
	public class TaskManager : ITaskManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000BDD3 File Offset: 0x00009FD3
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000BDDB File Offset: 0x00009FDB
		public ITaskDAO dao { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		private ITaskGroupManager taskGroupManager
		{
			get
			{
				ITaskGroupManager result;
				if ((result = this.tgm) == null)
				{
					result = (this.tgm = new TaskGroupManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000BE0F File Offset: 0x0000A00F
		public TaskManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new TaskDAO(opContext);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000BE2E File Offset: 0x0000A02E
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000BE36 File Offset: 0x0000A036
		public OperationContext OpContext { get; set; }

		// Token: 0x06000230 RID: 560 RVA: 0x0000BE3F File Offset: 0x0000A03F
		public void ChangeRemoveFromListStatus(int TaskId, bool NewRemoveFromListStatus)
		{
			this.dao.ChangeRemoveFromListStatus(TaskId, NewRemoveFromListStatus);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000BE50 File Offset: 0x0000A050
		public Task LoadTaskById(int TaskId)
		{
			return this.dao.LoadTaskById(TaskId);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000BE70 File Offset: 0x0000A070
		public int CreateTask(Task Task)
		{
			return this.dao.CreateTask(Task);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000BE8E File Offset: 0x0000A08E
		public void DeleteTask(int TaskId)
		{
			this.dao.DeleteTask(TaskId);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public void UpdateTask(Task Task)
		{
			this.dao.UpdateTask(Task);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000BEAE File Offset: 0x0000A0AE
		public void ChangeTaskCompletedStatus(int TaskId, bool NewCompletedStatus)
		{
			this.dao.ChangeTaskCompletedStatus(TaskId, NewCompletedStatus);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		public List<Task> LoadTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPart PartsToLoad)
		{
			return this.dao.LoadTasks(IncludePrivateTasks, IncludeSharedTasks, IncludeAssignedTasks, PartsToLoad);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public List<Task> LoadCompletedTasks(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadCompletedTasks(IncludePrivateTasks, IncludeSharedTasks, IncludeAssignedTasks, StartDate, EndDate);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000BF08 File Offset: 0x0000A108
		public List<TaskNote> LoadTaskNotesByTaskId(int TaskId)
		{
			return this.dao.LoadTaskNotesByTaskId(TaskId);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000BF28 File Offset: 0x0000A128
		public Forest<TaskOrGroup> LoadTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, eTaskPart PartsToLoad, out List<Task> Tasks, out List<TaskGroup> Groups)
		{
			Tasks = this.LoadTasks(IncludePrivateTasks, IncludeSharedTasks, IncludeAssignedTasks, PartsToLoad);
			ITaskGroupManager taskGroupManager = this.taskGroupManager;
			Groups = taskGroupManager.LoadGroups(IncludePrivateTasks, IncludeSharedTasks);
			return this.ConvertTaskListToForest(Tasks, Groups);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000BF68 File Offset: 0x0000A168
		public Forest<TaskOrGroup> LoadCompletedTasksAsTree(bool IncludePrivateTasks, bool IncludeSharedTasks, bool IncludeAssignedTasks, DateTime StartDate, DateTime EndDate, out List<Task> Tasks, out List<TaskGroup> Groups)
		{
			Tasks = this.LoadCompletedTasks(IncludePrivateTasks, IncludeSharedTasks, IncludeAssignedTasks, StartDate, EndDate);
			ITaskGroupManager taskGroupManager = this.taskGroupManager;
			Groups = taskGroupManager.LoadGroups(IncludePrivateTasks, IncludeSharedTasks);
			return this.ConvertTaskListToForest(Tasks, Groups);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		private void AddGroupsToTaskTree(ref Dictionary<int, TreeNode<TaskOrGroup>> groupCache, ref Forest<TaskOrGroup> tree, TreeNode<TaskOrGroup> parentNode, int currentParentTaskGroupId, List<TaskGroup> groups)
		{
			bool flag = currentParentTaskGroupId < 1;
			List<TaskGroup> list;
			if (flag)
			{
				list = groups.FindAll((TaskGroup g) => g.ParentTaskGroupId < 1);
			}
			else
			{
				list = groups.FindAll((TaskGroup g) => g.ParentTaskGroupId == currentParentTaskGroupId);
			}
			foreach (TaskGroup taskGroup in list)
			{
				TreeNode<TaskOrGroup> treeNode = tree.AppendNode(parentNode, new TaskOrGroup
				{
					Group = taskGroup
				});
				this.AddGroupsToTaskTree(ref groupCache, ref tree, treeNode, taskGroup.TaskGroupId, groups);
				groupCache.Add(taskGroup.TaskGroupId, treeNode);
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000C08C File Offset: 0x0000A28C
		private Forest<TaskOrGroup> ConvertTaskListToForest(List<Task> tasks, List<TaskGroup> groups)
		{
			Forest<TaskOrGroup> forest = new Forest<TaskOrGroup>();
			Dictionary<int, TreeNode<TaskOrGroup>> dictionary = new Dictionary<int, TreeNode<TaskOrGroup>>();
			this.AddGroupsToTaskTree(ref dictionary, ref forest, null, -1, groups);
			Dictionary<int, TreeNode<TaskOrGroup>> dictionary2 = new Dictionary<int, TreeNode<TaskOrGroup>>();
			int j;
			for (int i = 0; i < tasks.Count; i = j)
			{
				int num = (tasks[i].TaskGroup == null) ? 0 : tasks[i].TaskGroup.TaskGroupId;
				int num2 = (tasks[i].PrimaryTaskId != null) ? tasks[i].PrimaryTaskId.Value : 0;
				for (j = i + 1; j < tasks.Count; j++)
				{
					int num3 = (tasks[j].TaskGroup == null) ? 0 : tasks[j].TaskGroup.TaskGroupId;
					int num4 = (tasks[j].PrimaryTaskId != null) ? tasks[j].PrimaryTaskId.Value : 0;
					bool flag = num3 != num || num4 != num2;
					if (flag)
					{
						break;
					}
				}
				bool flag2 = num2 > 0;
				TreeNode<TaskOrGroup> parentNode;
				if (flag2)
				{
					parentNode = dictionary2[num2];
				}
				else
				{
					bool flag3 = num < 1;
					if (flag3)
					{
						parentNode = null;
					}
					else
					{
						bool flag4 = dictionary.ContainsKey(num);
						if (flag4)
						{
							parentNode = dictionary[num];
						}
						else
						{
							parentNode = null;
						}
					}
				}
				for (int k = i; k < j; k++)
				{
					TreeNode<TaskOrGroup> value = forest.AppendNode(parentNode, new TaskOrGroup
					{
						Task = tasks[k]
					});
					dictionary2.Add(tasks[k].TaskId, value);
				}
			}
			dictionary2.Clear();
			dictionary.Clear();
			return forest;
		}

		// Token: 0x04000072 RID: 114
		private ITaskGroupManager tgm;
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tasks;
using TechnoPro.Common.ClientManager.ICore.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Tasks
{
	// Token: 0x0200000D RID: 13
	public class TaskGroupRestClientManager : BearerTokenRestProxy<ITaskGroupClientManager>, ITaskGroupClientManager, IWebService
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000031CD File Offset: 0x000013CD
		public TaskGroupRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000031D7 File Offset: 0x000013D7
		public TaskGroupRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031E2 File Offset: 0x000013E2
		public int CreateNewTaskGroup(TaskGroupDTO Group)
		{
			return base.Post<TaskGroupDTO, int>(Group, "taskgroup");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031F0 File Offset: 0x000013F0
		public void DeleteTaskGroup(int TaskGroupId)
		{
			base.Delete(string.Format("taskgroup/taskgroupid/{0}", TaskGroupId));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003208 File Offset: 0x00001408
		public void UpdateTaskGroup(TaskGroupDTO Group)
		{
			base.Put<TaskGroupDTO>(Group, "taskgroup");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003216 File Offset: 0x00001416
		public IList<TaskGroupDTO> LoadGroups(bool IncludePrivate, bool IncludeShared)
		{
			return base.GetMany<TaskGroupDTO>(string.Format("taskgroup?includeprivate={0}&includeshared={1}", IncludePrivate, IncludeShared), true);
		}
	}
}

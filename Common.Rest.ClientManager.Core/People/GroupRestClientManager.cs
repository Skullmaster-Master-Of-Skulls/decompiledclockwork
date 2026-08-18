using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x02000025 RID: 37
	public class GroupRestClientManager : BearerTokenRestProxy<IGroupClientManager>, IGroupClientManager, IWebService
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000053D8 File Offset: 0x000035D8
		public GroupRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000053E2 File Offset: 0x000035E2
		public GroupRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000053ED File Offset: 0x000035ED
		public GroupDTO LoadGroupByTitle(string groupTitle, string altGroupTitle)
		{
			return base.Get<GroupDTO>(string.Format("group/grouptitle/{0}?altgrouptitle={1}", groupTitle, altGroupTitle), true);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005402 File Offset: 0x00003602
		public int CreateGroupByTitle(string groupTitle)
		{
			return base.Post<string, int>(groupTitle, "group");
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005410 File Offset: 0x00003610
		public GroupDTO LoadGroupById(int GroupId)
		{
			return base.Get<GroupDTO>(string.Format("group/groupid/{0}", GroupId), true);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005429 File Offset: 0x00003629
		public IList<GroupDTO> LoadAllowedGroups(bool OnlyReturnVisibleInCalendarGroups)
		{
			return base.GetMany<GroupDTO>(string.Format("group/allowed?onlyreturnvisibleincalendargroups={0}", OnlyReturnVisibleInCalendarGroups), true);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005442 File Offset: 0x00003642
		public IList<GroupContainerDTO> LoadAllGroupContainers()
		{
			return base.GetMany<GroupContainerDTO>("group/containers", true);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005450 File Offset: 0x00003650
		public IList<GroupForEditDTO> LoadAllGroupForEdits()
		{
			return base.GetMany<GroupForEditDTO>("group/foredits", true);
		}
	}
}

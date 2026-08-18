using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x02000023 RID: 35
	public class AdminGroupRestClientManager : BearerTokenRestProxy<IAdminGroupClientManager>, IAdminGroupClientManager, IWebService
	{
		// Token: 0x06000133 RID: 307 RVA: 0x000051EC File Offset: 0x000033EC
		public AdminGroupRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000051F6 File Offset: 0x000033F6
		public AdminGroupRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005201 File Offset: 0x00003401
		public LoadAllGroupsAndContainersResp LoadAllGroupsAndContainers()
		{
			return base.Get<LoadAllGroupsAndContainersResp>("admingroup/allgroupsandcontainers", true);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000520F File Offset: 0x0000340F
		public int CreateGroup(GroupDTO group)
		{
			return base.Post<GroupDTO, int>(group, "admingroup");
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000521D File Offset: 0x0000341D
		public void UpdateGroup(GroupDTO group)
		{
			base.Put<GroupDTO>(group, "admingroup");
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000522B File Offset: 0x0000342B
		public void DeleteGroup(int groupId)
		{
			base.Delete(string.Format("admingroup/groupid/{0}", groupId));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005244 File Offset: 0x00003444
		public void UpdateGroupOrder(int groupId, int newOrderNum)
		{
			UpdateGroupOrderReq updateGroupOrderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateGroupOrderReq>();
			updateGroupOrderReq.GroupId = groupId;
			updateGroupOrderReq.NewOrderNum = newOrderNum;
			base.Put<UpdateGroupOrderReq>(updateGroupOrderReq, "admingroup/order");
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005278 File Offset: 0x00003478
		public void UpdateGroupContainerTitle(string oldContainerTitle, string newContainerTitle)
		{
			UpdateGroupContainerTitleReq updateGroupContainerTitleReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateGroupContainerTitleReq>();
			updateGroupContainerTitleReq.OldContainerTitle = oldContainerTitle;
			updateGroupContainerTitleReq.NewContainerTitle = newContainerTitle;
			base.Put<UpdateGroupContainerTitleReq>(updateGroupContainerTitleReq, "admingroup/containertitle");
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000052AC File Offset: 0x000034AC
		public void AddMembersToGroup(int groupId, IEnumerable<int> pids)
		{
			AddMembersToGroupReq addMembersToGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddMembersToGroupReq>();
			addMembersToGroupReq.GroupId = groupId;
			addMembersToGroupReq.PersonIds = ((pids != null) ? pids.ToArray<int>() : null);
			base.Post<AddMembersToGroupReq>(addMembersToGroupReq, "admingroup/addmembers");
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000052EC File Offset: 0x000034EC
		public void RemoveMembersFromGroup(int groupId, IEnumerable<int> pids)
		{
			RemoveMembersFromGroupReq removeMembersFromGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveMembersFromGroupReq>();
			removeMembersFromGroupReq.GroupId = groupId;
			removeMembersFromGroupReq.PersonIds = ((pids != null) ? pids.ToArray<int>() : null);
			base.Post<RemoveMembersFromGroupReq>(removeMembersFromGroupReq, "admingroup/removemembers");
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005329 File Offset: 0x00003529
		public int LoadGroupMemberCount(int groupId)
		{
			return base.Get<int>(string.Format("admingroup/membercount/groupid/{0}", groupId), true);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005344 File Offset: 0x00003544
		public void UpdateGroupsOrders(IDictionary<int, int> groupidsWithOrderNums)
		{
			UpdateGroupsOrdersReq updateGroupsOrdersReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateGroupsOrdersReq>();
			updateGroupsOrdersReq.GroupIdsWithOrderNums = groupidsWithOrderNums;
			base.Put<UpdateGroupsOrdersReq>(updateGroupsOrdersReq, "admingroup/orders");
		}
	}
}

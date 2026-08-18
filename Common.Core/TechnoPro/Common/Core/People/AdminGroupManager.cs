using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;
using TechnoPro.Common.Public.Exceptions.RequestDenied;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A2 RID: 162
	public class AdminGroupManager : IAdminGroupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x0002197A File Offset: 0x0001FB7A
		public AdminGroupManager()
		{
			this.CheckHasManageGroupPermissions();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002198B File Offset: 0x0001FB8B
		public AdminGroupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.CheckHasManageGroupPermissions();
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000219A4 File Offset: 0x0001FBA4
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x000219AC File Offset: 0x0001FBAC
		public OperationContext OpContext { get; set; }

		// Token: 0x060005A7 RID: 1447 RVA: 0x000219B8 File Offset: 0x0001FBB8
		private void CheckHasManageGroupPermissions()
		{
			PeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
			bool flag = !peopleGroupManager.HasManageUserRoomPermissions(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000219F8 File Offset: 0x0001FBF8
		public GroupsAndContainers LoadAllGroupsAndContainers()
		{
			IGroupManager groupManager = new GroupManager(this.OpContext);
			IList<GroupContainer> list = groupManager.LoadAllGroupContainers();
			List<GroupContainer> list2 = (list != null) ? list.ToList<GroupContainer>() : null;
			if (list2 != null)
			{
				list2.Sort((GroupContainer g1, GroupContainer g2) => (g1.FullDescription ?? "").CompareTo(g2.FullDescription ?? ""));
			}
			IPeopleGroupDAO peopleGroupDAO = new PeopleGroupDAO(this.OpContext);
			IList<Group> list3 = peopleGroupDAO.LoadAllGroups();
			List<Group> list4 = (list3 != null) ? list3.ToList<Group>() : null;
			if (list4 != null)
			{
				list4.Sort(delegate(Group g1, Group g2)
				{
					int num = g1.OrderNum.CompareTo(g2.OrderNum);
					bool flag = num != 0;
					int result;
					if (flag)
					{
						result = num;
					}
					else
					{
						result = (g1.Description ?? "").CompareTo(g2.Description ?? "");
					}
					return result;
				});
			}
			return new GroupsAndContainers
			{
				Groups = list4,
				GroupContainers = list2
			};
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00021AB8 File Offset: 0x0001FCB8
		public int CreateGroup(Group group)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			return adminGroupDAO.CreateGroup(group);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		public void UpdateGroup(Group group)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			adminGroupDAO.UpdateGroup(group);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00021B04 File Offset: 0x0001FD04
		public void UpdateGroupsOrders(IDictionary<int, int> groupIdsWithOrderNums)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			foreach (KeyValuePair<int, int> keyValuePair in groupIdsWithOrderNums)
			{
				adminGroupDAO.UpdateGroupOrder(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00021B6C File Offset: 0x0001FD6C
		public void UpdateGroupOrder(int groupId, int newOrderNum)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			adminGroupDAO.UpdateGroupOrder(groupId, newOrderNum);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00021B90 File Offset: 0x0001FD90
		public void UpdateGroupContainerTitle(string oldContainerTitle, string newContainerTitle)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			adminGroupDAO.UpdateGroupContainerTitle(oldContainerTitle, newContainerTitle);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		public IList<PersonBase> LoadGroupMembers(bool onlyShowDeleted, params int[] gids)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			return this.LoadGroupMembers(onlyShowDeleted, gids);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00021BDC File Offset: 0x0001FDDC
		public int LoadGroupMemberCount(int groupId)
		{
			IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
			return peopleGroupManager.LoadGroupMemberCount(groupId);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00021C04 File Offset: 0x0001FE04
		public void DeleteGroup(int groupId)
		{
			IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
			int num = peopleGroupManager.LoadGroupMemberCount(groupId);
			bool flag = num > 0;
			if (flag)
			{
				throw new AbortedDueToRuleBreak(string.Concat(new string[]
				{
					"Group ",
					groupId.ToString(),
					" has ",
					num.ToString(),
					" member(s) in it - cannot delete groups with existing members."
				}));
			}
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			adminGroupDAO.DeleteGroup(groupId);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00021C80 File Offset: 0x0001FE80
		public void AddMembersToGroup(int groupId, int[] pids)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			adminGroupDAO.AddMembersToGroup(groupId, pids);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		public void RemoveMembersFromGroup(int groupId, int[] pids)
		{
			IAdminGroupDAO adminGroupDAO = new AdminGroupDAO(this.OpContext);
			adminGroupDAO.RemoveMembersFromGroup(groupId, pids);
		}
	}
}

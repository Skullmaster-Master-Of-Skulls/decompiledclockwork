using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A5 RID: 165
	public class PeopleGroupManager : IPeopleGroupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x000220CD File Offset: 0x000202CD
		public PeopleGroupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new PeopleGroupDAO(opContext);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000220EB File Offset: 0x000202EB
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000220F3 File Offset: 0x000202F3
		public OperationContext OpContext { get; set; }

		// Token: 0x060005CA RID: 1482 RVA: 0x000220FC File Offset: 0x000202FC
		public IList<int> GetGroupIdsByPersonId(int PersonId)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = "uGroupIds_" + PersonId.ToString();
			IList<int> list = (IList<int>)cacheStorageManager[key];
			bool flag = list == null;
			if (flag)
			{
				list = this.dao.GetGroupIdsByPersonId(PersonId);
				cacheStorageManager.Insert(key, list, TimeSpan.FromHours(1.0));
			}
			return list;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00022164 File Offset: 0x00020364
		[DebuggerStepThrough]
		public Task<IList<int>> GetGroupIdsByPersonIdAsync(int PersonId)
		{
			PeopleGroupManager.<GetGroupIdsByPersonIdAsync>d__7 <GetGroupIdsByPersonIdAsync>d__ = new PeopleGroupManager.<GetGroupIdsByPersonIdAsync>d__7();
			<GetGroupIdsByPersonIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<int>>.Create();
			<GetGroupIdsByPersonIdAsync>d__.<>4__this = this;
			<GetGroupIdsByPersonIdAsync>d__.PersonId = PersonId;
			<GetGroupIdsByPersonIdAsync>d__.<>1__state = -1;
			<GetGroupIdsByPersonIdAsync>d__.<>t__builder.Start<PeopleGroupManager.<GetGroupIdsByPersonIdAsync>d__7>(ref <GetGroupIdsByPersonIdAsync>d__);
			return <GetGroupIdsByPersonIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000221B0 File Offset: 0x000203B0
		public bool HasManageUserRoomPermissions(int personId)
		{
			bool flag = this.GetGroupIdsByPersonId(personId).Contains(10);
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = flag;
			}
			else
			{
				IPermissionManager permissionManager = new PermissionManager(this.OpContext);
				result = (permissionManager.IsUserAllowed(personId, UserPermissionEnum.UseAdminProgram) && (permissionManager.IsUserAllowed(personId, UserPermissionEnum.Admin_UsersResources) || permissionManager.IsUserAllowed(personId, UserPermissionEnum.Admin_ManageStudents)));
			}
			return result;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002220C File Offset: 0x0002040C
		public bool IsAdmin(int personId)
		{
			return this.GetGroupIdsByPersonId(personId).Contains(10);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0002222C File Offset: 0x0002042C
		public IList<PersonBase> LoadUsersByGroupTitle(string GroupTitle, string AlternateGroupTitle)
		{
			IGroupManager groupManager = new GroupManager(this.OpContext);
			Group group = groupManager.LoadGroupByTitle(GroupTitle);
			bool flag = (group == null || group.GroupId < 1) && !string.IsNullOrEmpty(AlternateGroupTitle);
			if (flag)
			{
				group = groupManager.LoadGroupByTitle(AlternateGroupTitle);
			}
			bool flag2 = group == null || group.GroupId < 1;
			IList<PersonBase> result;
			if (flag2)
			{
				result = new List<PersonBase>();
			}
			else
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				result = peopleManager.LoadGroupMembers(group.GroupId);
			}
			return result;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000222B0 File Offset: 0x000204B0
		public IList<PersonBase> LoadusersByGroupTitleAndPersonIdList(IList<int> PersonIds, string GroupTitle, string AlternateGroupTitle)
		{
			IGroupManager groupManager = new GroupManager(this.OpContext);
			Group group = groupManager.LoadGroupByTitle(GroupTitle);
			bool flag = (group == null || group.GroupId < 1) && !string.IsNullOrEmpty(AlternateGroupTitle);
			if (flag)
			{
				group = groupManager.LoadGroupByTitle(AlternateGroupTitle);
			}
			bool flag2 = group == null || group.GroupId < 1;
			IList<PersonBase> result;
			if (flag2)
			{
				result = new List<PersonBase>();
			}
			else
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				result = peopleManager.LoadGroupMembersByPersonIds(group.GroupId, PersonIds);
			}
			return result;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00022334 File Offset: 0x00020534
		public int LoadGroupMemberCount(int groupId)
		{
			IPeopleGroupDAO peopleGroupDAO = new PeopleGroupDAO(this.OpContext);
			return peopleGroupDAO.LoadGroupMemberCount(groupId);
		}

		// Token: 0x04000126 RID: 294
		private IPeopleGroupDAO dao;
	}
}

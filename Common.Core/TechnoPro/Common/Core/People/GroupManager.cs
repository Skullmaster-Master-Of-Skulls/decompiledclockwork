using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A4 RID: 164
	public class GroupManager : IGroupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x00021ECD File Offset: 0x000200CD
		public GroupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new GroupDAO(opContext);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00021EEB File Offset: 0x000200EB
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00021EF3 File Offset: 0x000200F3
		public OperationContext OpContext { get; set; }

		// Token: 0x060005BF RID: 1471 RVA: 0x00021EFC File Offset: 0x000200FC
		public Group LoadGroupById(int GroupId)
		{
			return this.dao.LoadGroupById(GroupId);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00021F1C File Offset: 0x0002011C
		public Group LoadGroupByTitle(string GroupTitle)
		{
			return this.dao.LoadGroupByTitle(GroupTitle);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00021F3C File Offset: 0x0002013C
		public int CreateGroupByTitle(string GroupTitle)
		{
			return this.dao.CreateGroupByTitle(GroupTitle);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00021F5C File Offset: 0x0002015C
		public int TryToLoadGroupOrCreateFirstIfNoneFound(params string[] groupTitles)
		{
			return this.dao.TryToLoadGroupOrCreateFirstIfNoneFound(groupTitles);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00021F7C File Offset: 0x0002017C
		private IList<int> GetAllowedGroupIds()
		{
			eSettingCode[] array = new eSettingCode[5];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.5CE84F0B4D97621FA98366E02D2588DF623889A051A975C2A60E71DF56C012C3).FieldHandle);
			eSettingCode[] array2 = array;
			List<int> allowedGroupIds = new List<int>();
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			Func<int, bool> <>9__0;
			foreach (eSettingCode settingCode in array2)
			{
				List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, settingCode);
				bool flag = settingValue_ConcatenatedIntList != null;
				if (flag)
				{
					List<int> allowedGroupIds2 = allowedGroupIds;
					IEnumerable<int> source = settingValue_ConcatenatedIntList;
					Func<int, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((int g) => !allowedGroupIds.Contains(g)));
					}
					allowedGroupIds2.AddRange(source.Where(predicate));
				}
			}
			return allowedGroupIds;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00022038 File Offset: 0x00020238
		public IList<Group> LoadAllowedGroups(bool includeVisibleInCalendarGroupsOnly)
		{
			IList<int> allowedGids = this.GetAllowedGroupIds();
			IPeopleGroupDAO peopleGroupDAO = new PeopleGroupDAO(this.OpContext);
			IList<Group> source = peopleGroupDAO.LoadAllGroups();
			return (from g in source
			where (!includeVisibleInCalendarGroupsOnly || g.VisibleInCalendar) && allowedGids.Contains(g.GroupId)
			select g).ToList<Group>();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00022090 File Offset: 0x00020290
		public IList<GroupContainer> LoadAllGroupContainers()
		{
			return this.dao.LoadAllGroupContainers();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000220B0 File Offset: 0x000202B0
		public IList<GroupForEdit> LoadAllGroupForEdits()
		{
			return this.dao.LoadAllGroupForEdits();
		}

		// Token: 0x04000124 RID: 292
		private IGroupDAO dao;
	}
}

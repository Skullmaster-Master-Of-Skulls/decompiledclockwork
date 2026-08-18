using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x02000050 RID: 80
	public interface IGroupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001F7 RID: 503
		Group LoadGroupById(int GroupId);

		// Token: 0x060001F8 RID: 504
		Group LoadGroupByTitle(string GroupTitle);

		// Token: 0x060001F9 RID: 505
		int CreateGroupByTitle(string GroupTitle);

		// Token: 0x060001FA RID: 506
		int TryToLoadGroupOrCreateFirstIfNoneFound(params string[] groupTitles);

		// Token: 0x060001FB RID: 507
		IList<Group> LoadAllowedGroups(bool includeVisibleInCalendarGroupsOnly);

		// Token: 0x060001FC RID: 508
		IList<GroupContainer> LoadAllGroupContainers();

		// Token: 0x060001FD RID: 509
		IList<GroupForEdit> LoadAllGroupForEdits();
	}
}

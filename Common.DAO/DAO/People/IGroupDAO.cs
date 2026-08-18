using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x0200003E RID: 62
	public interface IGroupDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000125 RID: 293
		Group LoadGroupById(int GroupId);

		// Token: 0x06000126 RID: 294
		Group LoadGroupByTitle(string GroupTitle);

		// Token: 0x06000127 RID: 295
		int CreateGroupByTitle(string GroupTitle);

		// Token: 0x06000128 RID: 296
		int TryToLoadGroupOrCreateFirstIfNoneFound(params string[] groupTitles);

		// Token: 0x06000129 RID: 297
		IList<GroupContainer> LoadAllGroupContainers();

		// Token: 0x0600012A RID: 298
		IList<GroupForEdit> LoadAllGroupForEdits();
	}
}

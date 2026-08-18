using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee;

namespace TechnoPro.Common.DAO.Notetaking
{
	// Token: 0x02000047 RID: 71
	public interface INotetakeeDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600017F RID: 383
		IList<NotetakeeCourseRegistration> LoadNotetakeeCourseRegistrations(int studentPid, DateTime startDate, DateTime endDate, bool loadSelfRegData, bool includeDroppedCourses = false);

		// Token: 0x06000180 RID: 384
		IList<int> FindLuCourseidsWhereAtLeastOneNotetakerIsAvailable(int equivalentCourseNum, IList<int> lucids);
	}
}

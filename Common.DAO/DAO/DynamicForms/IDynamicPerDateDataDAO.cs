using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x02000084 RID: 132
	public interface IDynamicPerDateDataDAO
	{
		// Token: 0x06000367 RID: 871
		IList<PerDateEntry> LoadPerDateEntries(int StudentPersonId, int ScreenNum);

		// Token: 0x06000368 RID: 872
		IList<PerDateEntryWithChildEntries> LoadPerDateEntriesWithChildEntries(int StudentPersonId, int ParentScreenNum, int ChildScreenNum);

		// Token: 0x06000369 RID: 873
		IList<PersonBase> LoadUniqueStudentsWithPerDateDataEnteredByForm(int ScreenNum);

		// Token: 0x0600036A RID: 874
		int CreatePerDateEntry(PerDateEntry perDateEntry);

		// Token: 0x0600036B RID: 875
		PerDateEntry GetExistingPerDateEntry(int StudentPersonId, int ScreenNum, DateTime StartDate, DateTime EndDate);
	}
}

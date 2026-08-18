using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x0200009C RID: 156
	public interface IDynamicPerDateDataManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000497 RID: 1175
		IList<PerDateEntry> LoadPerDateEntries(int StudentPersonId, int ScreenNum);

		// Token: 0x06000498 RID: 1176
		int CreatePerDateEntry(PerDateEntry perDateEntry);

		// Token: 0x06000499 RID: 1177
		IList<PersonBase> LoadUniqueStudentsWithPerDateDataEnteredByForm(int ScreenNum);

		// Token: 0x0600049A RID: 1178
		PerDateEntry GetExistingPerDateEntry(int StudentPersonId, int ScreenNum, Session Session);

		// Token: 0x0600049B RID: 1179
		IList<PerDateEntryWithChildEntries> LoadPerDateEntriesWithChildEntries(int StudentPersonId, DynamicForm Form);
	}
}

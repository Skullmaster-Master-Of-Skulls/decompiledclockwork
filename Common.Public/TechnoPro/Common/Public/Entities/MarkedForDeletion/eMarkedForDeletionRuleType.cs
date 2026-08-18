using System;

namespace TechnoPro.Common.Public.Entities.MarkedForDeletion
{
	// Token: 0x020002AE RID: 686
	public enum eMarkedForDeletionRuleType
	{
		// Token: 0x04001163 RID: 4451
		Unknown,
		// Token: 0x04001164 RID: 4452
		EntryOrCreationDate,
		// Token: 0x04001165 RID: 4453
		LastActivityDate,
		// Token: 0x04001166 RID: 4454
		Custom,
		// Token: 0x04001167 RID: 4455
		AfterCourseEndDate,
		// Token: 0x04001168 RID: 4456
		IsOrphaned
	}
}

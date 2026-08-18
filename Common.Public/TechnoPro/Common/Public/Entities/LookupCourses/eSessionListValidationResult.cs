using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002E4 RID: 740
	[Serializable]
	public enum eSessionListValidationResult
	{
		// Token: 0x04001341 RID: 4929
		Unknown,
		// Token: 0x04001342 RID: 4930
		Succeeded,
		// Token: 0x04001343 RID: 4931
		Gap,
		// Token: 0x04001344 RID: 4932
		Overlap,
		// Token: 0x04001345 RID: 4933
		Empty,
		// Token: 0x04001346 RID: 4934
		InvalidDate
	}
}

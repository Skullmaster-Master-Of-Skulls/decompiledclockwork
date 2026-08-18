using System;

namespace TechnoPro.Common.UI.Web.Entity.Survey
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public enum eSurveyNotAvailableReason
	{
		// Token: 0x040000DD RID: 221
		None,
		// Token: 0x040000DE RID: 222
		InvalidSurveyId,
		// Token: 0x040000DF RID: 223
		NotFound,
		// Token: 0x040000E0 RID: 224
		Disabled,
		// Token: 0x040000E1 RID: 225
		HasntStartedYet,
		// Token: 0x040000E2 RID: 226
		AlreadyEnded,
		// Token: 0x040000E3 RID: 227
		Deleted,
		// Token: 0x040000E4 RID: 228
		MissingForm,
		// Token: 0x040000E5 RID: 229
		StudentAlreadyFilledOutOnce
	}
}

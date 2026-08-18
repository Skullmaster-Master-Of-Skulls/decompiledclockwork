using System;

namespace TechnoPro.Common.UI.Web.Entity.OnlineForms
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public enum eOnlineFormNotAvailableReason
	{
		// Token: 0x040000EE RID: 238
		None,
		// Token: 0x040000EF RID: 239
		InvalidOnlineFormId,
		// Token: 0x040000F0 RID: 240
		NotFound,
		// Token: 0x040000F1 RID: 241
		Disabled,
		// Token: 0x040000F2 RID: 242
		HasntStartedYet,
		// Token: 0x040000F3 RID: 243
		AlreadyEnded,
		// Token: 0x040000F4 RID: 244
		Deleted,
		// Token: 0x040000F5 RID: 245
		MissingForm,
		// Token: 0x040000F6 RID: 246
		StudentAlreadyFilledOutOnce
	}
}

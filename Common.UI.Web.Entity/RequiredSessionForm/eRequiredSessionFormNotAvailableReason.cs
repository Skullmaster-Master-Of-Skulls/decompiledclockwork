using System;

namespace TechnoPro.Common.UI.Web.Entity.RequiredSessionForm
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public enum eRequiredSessionFormNotAvailableReason
	{
		// Token: 0x040000E7 RID: 231
		None,
		// Token: 0x040000E8 RID: 232
		InvalidFormId,
		// Token: 0x040000E9 RID: 233
		NotFound,
		// Token: 0x040000EA RID: 234
		Disabled,
		// Token: 0x040000EB RID: 235
		Deleted = 6,
		// Token: 0x040000EC RID: 236
		MissingForm
	}
}

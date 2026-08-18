using System;

namespace System.Data.OleDb
{
	// Token: 0x02000238 RID: 568
	internal enum OleDbPropertyStatus
	{
		// Token: 0x04001474 RID: 5236
		Ok,
		// Token: 0x04001475 RID: 5237
		NotSupported,
		// Token: 0x04001476 RID: 5238
		BadValue,
		// Token: 0x04001477 RID: 5239
		BadOption,
		// Token: 0x04001478 RID: 5240
		BadColumn,
		// Token: 0x04001479 RID: 5241
		NotAllSettable,
		// Token: 0x0400147A RID: 5242
		NotSettable,
		// Token: 0x0400147B RID: 5243
		NotSet,
		// Token: 0x0400147C RID: 5244
		Conflicting,
		// Token: 0x0400147D RID: 5245
		NotAvailable
	}
}

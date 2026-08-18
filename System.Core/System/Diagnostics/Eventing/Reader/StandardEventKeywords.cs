using System;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002D1 RID: 721
	[Flags]
	public enum StandardEventKeywords : long
	{
		// Token: 0x04000CCB RID: 3275
		None = 0L,
		// Token: 0x04000CCC RID: 3276
		ResponseTime = 281474976710656L,
		// Token: 0x04000CCD RID: 3277
		WdiContext = 562949953421312L,
		// Token: 0x04000CCE RID: 3278
		WdiDiagnostic = 1125899906842624L,
		// Token: 0x04000CCF RID: 3279
		Sqm = 2251799813685248L,
		// Token: 0x04000CD0 RID: 3280
		AuditFailure = 4503599627370496L,
		// Token: 0x04000CD1 RID: 3281
		AuditSuccess = 9007199254740992L,
		// Token: 0x04000CD2 RID: 3282
		[Obsolete("Incorrect value: use CorrelationHint2 instead", false)]
		CorrelationHint = 4503599627370496L,
		// Token: 0x04000CD3 RID: 3283
		CorrelationHint2 = 18014398509481984L,
		// Token: 0x04000CD4 RID: 3284
		EventLogClassic = 36028797018963968L
	}
}

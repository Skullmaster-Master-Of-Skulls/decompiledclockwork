using System;

namespace System.Data.SqlTypes
{
	// Token: 0x02000354 RID: 852
	internal sealed class SQLResource
	{
		// Token: 0x06002E80 RID: 11904 RVA: 0x002D1648 File Offset: 0x002D0A48
		private SQLResource()
		{
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x002D1668 File Offset: 0x002D0A68
		internal static string InvalidOpStreamClosed(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamClosed", new object[]
			{
				method
			});
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x002D1698 File Offset: 0x002D0A98
		internal static string InvalidOpStreamNonWritable(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamNonWritable", new object[]
			{
				method
			});
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x002D16C8 File Offset: 0x002D0AC8
		internal static string InvalidOpStreamNonReadable(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamNonReadable", new object[]
			{
				method
			});
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x002D16F8 File Offset: 0x002D0AF8
		internal static string InvalidOpStreamNonSeekable(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamNonSeekable", new object[]
			{
				method
			});
		}

		// Token: 0x04001D21 RID: 7457
		internal static readonly string NullString = Res.GetString("SqlMisc_NullString");

		// Token: 0x04001D22 RID: 7458
		internal static readonly string MessageString = Res.GetString("SqlMisc_MessageString");

		// Token: 0x04001D23 RID: 7459
		internal static readonly string ArithOverflowMessage = Res.GetString("SqlMisc_ArithOverflowMessage");

		// Token: 0x04001D24 RID: 7460
		internal static readonly string DivideByZeroMessage = Res.GetString("SqlMisc_DivideByZeroMessage");

		// Token: 0x04001D25 RID: 7461
		internal static readonly string NullValueMessage = Res.GetString("SqlMisc_NullValueMessage");

		// Token: 0x04001D26 RID: 7462
		internal static readonly string TruncationMessage = Res.GetString("SqlMisc_TruncationMessage");

		// Token: 0x04001D27 RID: 7463
		internal static readonly string DateTimeOverflowMessage = Res.GetString("SqlMisc_DateTimeOverflowMessage");

		// Token: 0x04001D28 RID: 7464
		internal static readonly string ConcatDiffCollationMessage = Res.GetString("SqlMisc_ConcatDiffCollationMessage");

		// Token: 0x04001D29 RID: 7465
		internal static readonly string CompareDiffCollationMessage = Res.GetString("SqlMisc_CompareDiffCollationMessage");

		// Token: 0x04001D2A RID: 7466
		internal static readonly string InvalidFlagMessage = Res.GetString("SqlMisc_InvalidFlagMessage");

		// Token: 0x04001D2B RID: 7467
		internal static readonly string NumeToDecOverflowMessage = Res.GetString("SqlMisc_NumeToDecOverflowMessage");

		// Token: 0x04001D2C RID: 7468
		internal static readonly string ConversionOverflowMessage = Res.GetString("SqlMisc_ConversionOverflowMessage");

		// Token: 0x04001D2D RID: 7469
		internal static readonly string InvalidDateTimeMessage = Res.GetString("SqlMisc_InvalidDateTimeMessage");

		// Token: 0x04001D2E RID: 7470
		internal static readonly string TimeZoneSpecifiedMessage = Res.GetString("SqlMisc_TimeZoneSpecifiedMessage");

		// Token: 0x04001D2F RID: 7471
		internal static readonly string InvalidArraySizeMessage = Res.GetString("SqlMisc_InvalidArraySizeMessage");

		// Token: 0x04001D30 RID: 7472
		internal static readonly string InvalidPrecScaleMessage = Res.GetString("SqlMisc_InvalidPrecScaleMessage");

		// Token: 0x04001D31 RID: 7473
		internal static readonly string FormatMessage = Res.GetString("SqlMisc_FormatMessage");

		// Token: 0x04001D32 RID: 7474
		internal static readonly string NotFilledMessage = Res.GetString("SqlMisc_NotFilledMessage");

		// Token: 0x04001D33 RID: 7475
		internal static readonly string AlreadyFilledMessage = Res.GetString("SqlMisc_AlreadyFilledMessage");

		// Token: 0x04001D34 RID: 7476
		internal static readonly string ClosedXmlReaderMessage = Res.GetString("SqlMisc_ClosedXmlReaderMessage");
	}
}

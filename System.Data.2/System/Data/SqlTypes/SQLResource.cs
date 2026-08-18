using System;

namespace System.Data.SqlTypes
{
	// Token: 0x02000165 RID: 357
	internal sealed class SQLResource
	{
		// Token: 0x060016D5 RID: 5845 RVA: 0x000A66D0 File Offset: 0x000A5AD0
		private SQLResource()
		{
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000A66E4 File Offset: 0x000A5AE4
		internal static string InvalidOpStreamClosed(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamClosed", new object[]
			{
				method
			});
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000A6708 File Offset: 0x000A5B08
		internal static string InvalidOpStreamNonWritable(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamNonWritable", new object[]
			{
				method
			});
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000A672C File Offset: 0x000A5B2C
		internal static string InvalidOpStreamNonReadable(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamNonReadable", new object[]
			{
				method
			});
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000A6750 File Offset: 0x000A5B50
		internal static string InvalidOpStreamNonSeekable(string method)
		{
			return Res.GetString("SqlMisc_InvalidOpStreamNonSeekable", new object[]
			{
				method
			});
		}

		// Token: 0x04000E0A RID: 3594
		internal static readonly string NullString = Res.GetString("SqlMisc_NullString");

		// Token: 0x04000E0B RID: 3595
		internal static readonly string MessageString = Res.GetString("SqlMisc_MessageString");

		// Token: 0x04000E0C RID: 3596
		internal static readonly string ArithOverflowMessage = Res.GetString("SqlMisc_ArithOverflowMessage");

		// Token: 0x04000E0D RID: 3597
		internal static readonly string DivideByZeroMessage = Res.GetString("SqlMisc_DivideByZeroMessage");

		// Token: 0x04000E0E RID: 3598
		internal static readonly string NullValueMessage = Res.GetString("SqlMisc_NullValueMessage");

		// Token: 0x04000E0F RID: 3599
		internal static readonly string TruncationMessage = Res.GetString("SqlMisc_TruncationMessage");

		// Token: 0x04000E10 RID: 3600
		internal static readonly string DateTimeOverflowMessage = Res.GetString("SqlMisc_DateTimeOverflowMessage");

		// Token: 0x04000E11 RID: 3601
		internal static readonly string ConcatDiffCollationMessage = Res.GetString("SqlMisc_ConcatDiffCollationMessage");

		// Token: 0x04000E12 RID: 3602
		internal static readonly string CompareDiffCollationMessage = Res.GetString("SqlMisc_CompareDiffCollationMessage");

		// Token: 0x04000E13 RID: 3603
		internal static readonly string InvalidFlagMessage = Res.GetString("SqlMisc_InvalidFlagMessage");

		// Token: 0x04000E14 RID: 3604
		internal static readonly string NumeToDecOverflowMessage = Res.GetString("SqlMisc_NumeToDecOverflowMessage");

		// Token: 0x04000E15 RID: 3605
		internal static readonly string ConversionOverflowMessage = Res.GetString("SqlMisc_ConversionOverflowMessage");

		// Token: 0x04000E16 RID: 3606
		internal static readonly string InvalidDateTimeMessage = Res.GetString("SqlMisc_InvalidDateTimeMessage");

		// Token: 0x04000E17 RID: 3607
		internal static readonly string TimeZoneSpecifiedMessage = Res.GetString("SqlMisc_TimeZoneSpecifiedMessage");

		// Token: 0x04000E18 RID: 3608
		internal static readonly string InvalidArraySizeMessage = Res.GetString("SqlMisc_InvalidArraySizeMessage");

		// Token: 0x04000E19 RID: 3609
		internal static readonly string InvalidPrecScaleMessage = Res.GetString("SqlMisc_InvalidPrecScaleMessage");

		// Token: 0x04000E1A RID: 3610
		internal static readonly string FormatMessage = Res.GetString("SqlMisc_FormatMessage");

		// Token: 0x04000E1B RID: 3611
		internal static readonly string NotFilledMessage = Res.GetString("SqlMisc_NotFilledMessage");

		// Token: 0x04000E1C RID: 3612
		internal static readonly string AlreadyFilledMessage = Res.GetString("SqlMisc_AlreadyFilledMessage");

		// Token: 0x04000E1D RID: 3613
		internal static readonly string ClosedXmlReaderMessage = Res.GetString("SqlMisc_ClosedXmlReaderMessage");
	}
}

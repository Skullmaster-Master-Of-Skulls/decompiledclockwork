using System;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009F8 RID: 2552
	public static class Modes
	{
		// Token: 0x020009F9 RID: 2553
		public enum CodeMode
		{
			// Token: 0x04001794 RID: 6036
			Byte,
			// Token: 0x04001795 RID: 6037
			Numeric,
			// Token: 0x04001796 RID: 6038
			Alphanumeric,
			// Token: 0x04001797 RID: 6039
			Kanji
		}

		// Token: 0x020009FA RID: 2554
		public enum ErrorCorrectionLevel
		{
			// Token: 0x04001799 RID: 6041
			L = 1,
			// Token: 0x0400179A RID: 6042
			M,
			// Token: 0x0400179B RID: 6043
			Q,
			// Token: 0x0400179C RID: 6044
			H
		}

		// Token: 0x020009FB RID: 2555
		public enum ECIMode
		{
			// Token: 0x0400179E RID: 6046
			None,
			// Token: 0x0400179F RID: 6047
			ISO8859_7 = 9,
			// Token: 0x040017A0 RID: 6048
			ISO8859_6 = 8,
			// Token: 0x040017A1 RID: 6049
			ISO8859_5 = 7,
			// Token: 0x040017A2 RID: 6050
			ISO8859_4 = 6,
			// Token: 0x040017A3 RID: 6051
			ISO8859_3 = 5,
			// Token: 0x040017A4 RID: 6052
			ISO8859_2 = 4,
			// Token: 0x040017A5 RID: 6053
			ISO8859_1En = 3,
			// Token: 0x040017A6 RID: 6054
			CP437 = 2,
			// Token: 0x040017A7 RID: 6055
			ISO8859_1 = 1,
			// Token: 0x040017A8 RID: 6056
			ISO8859_8 = 10,
			// Token: 0x040017A9 RID: 6057
			ISO8859_9,
			// Token: 0x040017AA RID: 6058
			ISO8859_11 = 13,
			// Token: 0x040017AB RID: 6059
			ISO8859_13 = 15,
			// Token: 0x040017AC RID: 6060
			ISO8859_15 = 17,
			// Token: 0x040017AD RID: 6061
			Windows1250 = 21,
			// Token: 0x040017AE RID: 6062
			Windows1251,
			// Token: 0x040017AF RID: 6063
			Windows1252,
			// Token: 0x040017B0 RID: 6064
			Windows1256,
			// Token: 0x040017B1 RID: 6065
			ISO646US = 27
		}

		// Token: 0x020009FC RID: 2556
		public enum FNC1Mode
		{
			// Token: 0x040017B3 RID: 6067
			None,
			// Token: 0x040017B4 RID: 6068
			FNC1FirstPosition,
			// Token: 0x040017B5 RID: 6069
			FNC1SecondPosition
		}
	}
}

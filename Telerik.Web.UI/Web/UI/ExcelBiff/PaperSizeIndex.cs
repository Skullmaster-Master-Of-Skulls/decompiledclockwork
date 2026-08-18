using System;
using System.Drawing;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ACB RID: 2763
	internal sealed class PaperSizeIndex
	{
		// Token: 0x06006860 RID: 26720 RVA: 0x00187030 File Offset: 0x00185230
		static PaperSizeIndex()
		{
			PaperSizeIndex.mmWidth = new int[]
			{
				297,
				210,
				148,
				250,
				215,
				110,
				162,
				324,
				229,
				114,
				114,
				250,
				176,
				176,
				110,
				250,
				200,
				220,
				236,
				210,
				227,
				305,
				210,
				148,
				182,
				322,
				174,
				201,
				420,
				297,
				322
			};
			PaperSizeIndex.mmHeight = new int[]
			{
				420,
				297,
				210,
				353,
				275,
				220,
				229,
				458,
				324,
				162,
				229,
				353,
				250,
				125,
				230,
				353,
				148,
				220,
				322,
				297,
				356,
				487,
				330,
				210,
				257,
				445,
				235,
				276,
				594,
				420,
				445
			};
			PaperSizeIndex.inchSizeIndex = new int[]
			{
				1,
				3,
				4,
				5,
				6,
				7,
				14,
				16,
				19,
				20,
				21,
				22,
				23,
				24,
				25,
				26,
				37,
				38,
				39,
				40,
				41,
				44,
				45,
				46,
				50,
				51,
				52,
				54,
				56,
				59
			};
			PaperSizeIndex.inchWidth = new float[]
			{
				8.5f,
				11f,
				17f,
				8.5f,
				5.5f,
				7.5f,
				8.5f,
				10f,
				3.875f,
				4.125f,
				4.5f,
				4.75f,
				5f,
				11f,
				22f,
				34f,
				3.875f,
				3.625f,
				14.875f,
				8.5f,
				8.5f,
				9f,
				10f,
				15f,
				9.275f,
				9.275f,
				11.69f,
				8.275f,
				9.275f,
				8.5f
			};
			PaperSizeIndex.inchHeight = new float[]
			{
				11f,
				17f,
				11f,
				14f,
				8.5f,
				10.5f,
				13f,
				14f,
				8.875f,
				9.5f,
				10.375f,
				11f,
				11.5f,
				22f,
				34f,
				44f,
				7.5f,
				6.5f,
				11f,
				12f,
				13f,
				11f,
				11f,
				11f,
				12f,
				15f,
				18f,
				11f,
				12f,
				12.69f
			};
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x001870C8 File Offset: 0x001852C8
		internal static int GetPaperSizeIndex(SizeF sizeMm)
		{
			for (int i = 0; i < PaperSizeIndex.mmWidth.Length; i++)
			{
				if ((double)Math.Abs(sizeMm.Height - (float)PaperSizeIndex.mmHeight[i]) < 0.1 && (double)Math.Abs(sizeMm.Width - (float)PaperSizeIndex.mmWidth[i]) < 0.1)
				{
					return PaperSizeIndex.mmSizeIndex[i];
				}
			}
			float num = sizeMm.Height / 25.4f;
			float num2 = sizeMm.Width / 25.4f;
			for (int j = 0; j < PaperSizeIndex.inchWidth.Length; j++)
			{
				if ((double)Math.Abs(num - PaperSizeIndex.inchHeight[j]) < 0.1 && (double)Math.Abs(num2 - PaperSizeIndex.inchWidth[j]) < 0.1)
				{
					return PaperSizeIndex.inchSizeIndex[j];
				}
			}
			return 0;
		}

		// Token: 0x04001B99 RID: 7065
		private static float[] inchWidth;

		// Token: 0x04001B9A RID: 7066
		private static float[] inchHeight;

		// Token: 0x04001B9B RID: 7067
		private static int[] inchSizeIndex;

		// Token: 0x04001B9C RID: 7068
		private static int[] mmWidth;

		// Token: 0x04001B9D RID: 7069
		private static int[] mmHeight;

		// Token: 0x04001B9E RID: 7070
		private static int[] mmSizeIndex = new int[]
		{
			8,
			9,
			11,
			12,
			15,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			42,
			43,
			47,
			53,
			55,
			57,
			58,
			60,
			61,
			62,
			63,
			64,
			65,
			66,
			67,
			68
		};
	}
}

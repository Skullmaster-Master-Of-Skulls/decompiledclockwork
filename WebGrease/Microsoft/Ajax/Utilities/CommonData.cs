using System;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000022 RID: 34
	internal static class CommonData
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00006D14 File Offset: 0x00004F14
		public static Regex ReplacementToken
		{
			get
			{
				if (CommonData.s_replacementToken == null)
				{
					CommonData.s_replacementToken = new Regex("%(?<token>[\\w\\.-]+)(?:\\:(?<fallback>\\w*))?%", RegexOptions.Compiled | RegexOptions.CultureInvariant);
				}
				return CommonData.s_replacementToken;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00006D36 File Offset: 0x00004F36
		public static Regex DecimalFormat
		{
			get
			{
				if (CommonData.s_decimalFormat == null)
				{
					CommonData.s_decimalFormat = new Regex("^\\s*(?:\\+|(?<neg>\\-))?0*(?<mag>(?<sig>\\d*[1-9])(?<zer>0*))?(\\.(?<man>\\d*[1-9])?0*)?(?<exp>E\\+?(?<eng>\\-?)0*(?<pow>[1-9]\\d*))?$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
				}
				return CommonData.s_decimalFormat;
			}
		}

		// Token: 0x0400007F RID: 127
		private static Regex s_replacementToken;

		// Token: 0x04000080 RID: 128
		private static Regex s_decimalFormat;
	}
}

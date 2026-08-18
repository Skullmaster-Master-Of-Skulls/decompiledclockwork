using System;
using System.Globalization;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000175 RID: 373
	public static class StringAdapter
	{
		// Token: 0x06001045 RID: 4165 RVA: 0x00077EE8 File Offset: 0x000760E8
		public static int IndexOfCaseAndAccentInsensitive(this string text, string searchString)
		{
			return CultureInfo.InvariantCulture.CompareInfo.IndexOf(text, searchString, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00077F0C File Offset: 0x0007610C
		public static int IndexOfCaseInsensitive(this string text, string searchString)
		{
			return text.IndexOf(searchString, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00077F28 File Offset: 0x00076128
		public static bool EqualsCaseAndAccentInsensitive(this string text, string text2)
		{
			bool flag = text == null && text2 == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = text == null || text2 == null;
				result = (!flag2 && CultureInfo.InvariantCulture.CompareInfo.Compare(text, text2, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) == 0);
			}
			return result;
		}
	}
}

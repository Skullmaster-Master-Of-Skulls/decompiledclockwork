using System;

namespace System.Data.Design
{
	// Token: 0x02000266 RID: 614
	internal sealed class StringUtil
	{
		// Token: 0x060017A1 RID: 6049 RVA: 0x0000362F File Offset: 0x0000182F
		private StringUtil()
		{
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00081C80 File Offset: 0x0007FE80
		internal static bool Empty(string str)
		{
			return str == null || 0 >= str.Length;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00081C93 File Offset: 0x0007FE93
		internal static bool EmptyOrSpace(string str)
		{
			return str == null || 0 >= str.Trim().Length;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00081CAB File Offset: 0x0007FEAB
		internal static bool EqualValue(string str1, string str2)
		{
			return StringUtil.EqualValue(str1, str2, false);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00081CB8 File Offset: 0x0007FEB8
		internal static bool EqualValue(string str1, string str2, bool caseInsensitive)
		{
			if (str1 != null && str2 != null)
			{
				StringComparison comparisonType = caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
				return string.Equals(str1, str2, comparisonType);
			}
			return str1 == str2;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00081CE3 File Offset: 0x0007FEE3
		internal static bool NotEmpty(string str)
		{
			return !StringUtil.Empty(str);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00081CEE File Offset: 0x0007FEEE
		public static bool NotEmptyAfterTrim(string str)
		{
			return !StringUtil.EmptyOrSpace(str);
		}
	}
}

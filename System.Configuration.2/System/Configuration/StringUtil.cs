using System;

namespace System.Configuration
{
	// Token: 0x02000090 RID: 144
	internal static class StringUtil
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C944 File Offset: 0x0001AB44
		internal static bool EqualsNE(string s1, string s2)
		{
			if (s1 == null)
			{
				s1 = string.Empty;
			}
			if (s2 == null)
			{
				s2 = string.Empty;
			}
			return string.Equals(s1, s2, StringComparison.Ordinal);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C962 File Offset: 0x0001AB62
		internal static bool EqualsIgnoreCase(string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001C96C File Offset: 0x0001AB6C
		internal static bool StartsWith(string s1, string s2)
		{
			return s2 != null && string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.Ordinal) == 0;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001C986 File Offset: 0x0001AB86
		internal static bool StartsWithIgnoreCase(string s1, string s2)
		{
			return s2 != null && string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001C9A0 File Offset: 0x0001ABA0
		internal static string[] ObjectArrayToStringArray(object[] objectArray)
		{
			string[] array = new string[objectArray.Length];
			objectArray.CopyTo(array, 0);
			return array;
		}
	}
}

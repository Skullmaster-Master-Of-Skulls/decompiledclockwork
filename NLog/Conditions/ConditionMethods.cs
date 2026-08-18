using System;

namespace NLog.Conditions
{
	// Token: 0x02000034 RID: 52
	[ConditionMethods]
	public static class ConditionMethods
	{
		// Token: 0x060000DD RID: 221 RVA: 0x0000389C File Offset: 0x00001A9C
		[ConditionMethod("equals")]
		public static bool Equals2(object firstValue, object secondValue)
		{
			return firstValue.Equals(secondValue);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000038A8 File Offset: 0x00001AA8
		[ConditionMethod("strequals")]
		public static bool Equals2(string firstValue, string secondValue, bool ignoreCase = false)
		{
			return firstValue.Equals(secondValue, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000038C8 File Offset: 0x00001AC8
		[ConditionMethod("contains")]
		public static bool Contains(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.IndexOf(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000038EC File Offset: 0x00001AEC
		[ConditionMethod("starts-with")]
		public static bool StartsWith(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.StartsWith(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000390C File Offset: 0x00001B0C
		[ConditionMethod("ends-with")]
		public static bool EndsWith(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.EndsWith(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003929 File Offset: 0x00001B29
		[ConditionMethod("length")]
		public static int Length(string text)
		{
			return text.Length;
		}
	}
}

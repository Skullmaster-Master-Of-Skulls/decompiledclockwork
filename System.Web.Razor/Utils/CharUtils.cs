using System;

namespace System.Web.Razor.Utils
{
	// Token: 0x02000084 RID: 132
	internal static class CharUtils
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00016366 File Offset: 0x00014566
		internal static bool IsNonNewLineWhitespace(char c)
		{
			return char.IsWhiteSpace(c) && !CharUtils.IsNewLine(c);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001637B File Offset: 0x0001457B
		internal static bool IsNewLine(char c)
		{
			return c == '\r' || c == '\n' || c == '\u2028' || c == '\u2029';
		}
	}
}

using System;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000073 RID: 115
	public static class VBHelpers
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00013459 File Offset: 0x00011659
		public static bool IsSingleQuote(char character)
		{
			return character == '\'' || character == '‘' || character == '’';
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00013472 File Offset: 0x00011672
		public static bool IsDoubleQuote(char character)
		{
			return character == '"' || character == '“' || character == '”';
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001348B File Offset: 0x0001168B
		public static bool IsOctalDigit(char character)
		{
			return character >= '0' && character <= '7';
		}
	}
}

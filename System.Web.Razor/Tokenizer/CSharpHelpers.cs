using System;
using System.Globalization;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000074 RID: 116
	public static class CSharpHelpers
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0001349C File Offset: 0x0001169C
		public static bool IsIdentifierStart(char character)
		{
			return char.IsLetter(character) || character == '_' || char.GetUnicodeCategory(character) == UnicodeCategory.LetterNumber;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000134B7 File Offset: 0x000116B7
		public static bool IsIdentifierPart(char character)
		{
			return char.IsDigit(character) || CSharpHelpers.IsIdentifierStart(character) || CSharpHelpers.IsIdentifierPartByUnicodeCategory(character);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000134D1 File Offset: 0x000116D1
		public static bool IsRealLiteralSuffix(char character)
		{
			return character == 'F' || character == 'f' || character == 'D' || character == 'd' || character == 'M' || character == 'm';
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000134F4 File Offset: 0x000116F4
		private static bool IsIdentifierPartByUnicodeCategory(char character)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(character);
			return unicodeCategory == UnicodeCategory.NonSpacingMark || unicodeCategory == UnicodeCategory.SpacingCombiningMark || unicodeCategory == UnicodeCategory.ConnectorPunctuation || unicodeCategory == UnicodeCategory.Format;
		}
	}
}

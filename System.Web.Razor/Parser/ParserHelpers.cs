using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000043 RID: 67
	public static class ParserHelpers
	{
		// Token: 0x06000332 RID: 818 RVA: 0x0000D997 File Offset: 0x0000BB97
		public static bool IsNewLine(char value)
		{
			return value == '\r' || value == '\n' || value == '\u0085' || value == '\u2028' || value == '\u2029';
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000D9BD File Offset: 0x0000BBBD
		public static bool IsNewLine(string value)
		{
			return (value.Length == 1 && ParserHelpers.IsNewLine(value[0])) || string.Equals(value, "\r\n", StringComparison.Ordinal);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		public static bool IsWhitespace(char value)
		{
			return value == ' ' || value == '\f' || value == '\t' || value == '\v' || char.GetUnicodeCategory(value) == UnicodeCategory.SpaceSeparator;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000DA06 File Offset: 0x0000BC06
		public static bool IsWhitespaceOrNewLine(char value)
		{
			return ParserHelpers.IsWhitespace(value) || ParserHelpers.IsNewLine(value);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public static bool IsIdentifier(string value)
		{
			return ParserHelpers.IsIdentifier(value, true);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000DA24 File Offset: 0x0000BC24
		public static bool IsIdentifier(string value, bool requireIdentifierStart)
		{
			IEnumerable<char> source = value;
			if (requireIdentifierStart)
			{
				source = source.Skip(1);
			}
			return (!requireIdentifierStart || ParserHelpers.IsIdentifierStart(value[0])) && source.All(new Func<char, bool>(ParserHelpers.IsIdentifierPart));
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000DA63 File Offset: 0x0000BC63
		public static bool IsHexDigit(char value)
		{
			return (value >= '0' && value <= '9') || (value >= 'A' && value <= 'F') || (value >= 'a' && value <= 'f');
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000DA8A File Offset: 0x0000BC8A
		public static bool IsIdentifierStart(char value)
		{
			return value == '_' || ParserHelpers.IsLetter(value);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000DA99 File Offset: 0x0000BC99
		public static bool IsIdentifierPart(char value)
		{
			return ParserHelpers.IsLetter(value) || ParserHelpers.IsDecimalDigit(value) || ParserHelpers.IsConnecting(value) || ParserHelpers.IsCombining(value) || ParserHelpers.IsFormatting(value);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		public static bool IsTerminatingCharToken(char value)
		{
			return ParserHelpers.IsNewLine(value) || value == '\'';
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public static bool IsTerminatingQuotedStringToken(char value)
		{
			return ParserHelpers.IsNewLine(value) || value == '"';
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000DAE5 File Offset: 0x0000BCE5
		public static bool IsDecimalDigit(char value)
		{
			return char.GetUnicodeCategory(value) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		public static bool IsLetterOrDecimalDigit(char value)
		{
			return ParserHelpers.IsLetter(value) || ParserHelpers.IsDecimalDigit(value);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000DB04 File Offset: 0x0000BD04
		public static bool IsLetter(char value)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(value);
			return unicodeCategory == UnicodeCategory.UppercaseLetter || unicodeCategory == UnicodeCategory.LowercaseLetter || unicodeCategory == UnicodeCategory.TitlecaseLetter || unicodeCategory == UnicodeCategory.ModifierLetter || unicodeCategory == UnicodeCategory.OtherLetter || unicodeCategory == UnicodeCategory.LetterNumber;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000DB32 File Offset: 0x0000BD32
		public static bool IsFormatting(char value)
		{
			return char.GetUnicodeCategory(value) == UnicodeCategory.Format;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000DB40 File Offset: 0x0000BD40
		public static bool IsCombining(char value)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(value);
			return unicodeCategory == UnicodeCategory.SpacingCombiningMark || unicodeCategory == UnicodeCategory.NonSpacingMark;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000DB5E File Offset: 0x0000BD5E
		public static bool IsConnecting(char value)
		{
			return char.GetUnicodeCategory(value) == UnicodeCategory.ConnectorPunctuation;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public static string SanitizeClassName(string inputName)
		{
			if (!ParserHelpers.IsIdentifierStart(inputName[0]) && ParserHelpers.IsIdentifierPart(inputName[0]))
			{
				inputName = "_" + inputName;
			}
			return new string(inputName.Select(delegate(char value)
			{
				if (!ParserHelpers.IsIdentifierPart(value))
				{
					return '_';
				}
				return value;
			}).ToArray<char>());
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000DBDB File Offset: 0x0000BDDB
		public static bool IsEmailPart(char character)
		{
			return char.IsLetter(character) || char.IsDigit(character) || character == '_';
		}
	}
}

using System;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B08 RID: 2824
	internal static class IdentifierValidator
	{
		// Token: 0x060069C5 RID: 27077 RVA: 0x0018D584 File Offset: 0x0018B784
		public static bool IsValid(string identifier)
		{
			return IdentifierValidator.IsValidTypeNameOrIdentifier(identifier, false);
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x0018D590 File Offset: 0x0018B790
		private static bool IsValidTypeNameOrIdentifier(string value, bool isTypeName)
		{
			bool flag = true;
			if (value.Length == 0)
			{
				return false;
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (char.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					flag = false;
					break;
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.ConnectorPunctuation:
					if (flag && c != '_')
					{
						return false;
					}
					flag = false;
					break;
				case UnicodeCategory.EnclosingMark:
				case UnicodeCategory.OtherNumber:
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Control:
				case UnicodeCategory.Format:
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_8C;
				default:
					goto IL_8C;
				}
				IL_9B:
				i++;
				continue;
				IL_8C:
				if (!isTypeName || !IdentifierValidator.IsSpecialTypeChar(c, ref flag))
				{
					return false;
				}
				goto IL_9B;
			}
			return true;
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x0018D64C File Offset: 0x0018B84C
		private static bool IsSpecialTypeChar(char ch, ref bool nextMustBeStartChar)
		{
			if (ch <= '>')
			{
				switch (ch)
				{
				case '$':
				case '&':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
					break;
				case '%':
				case '\'':
				case '(':
				case ')':
					return false;
				default:
					switch (ch)
					{
					case ':':
					case '<':
					case '>':
						break;
					case ';':
					case '=':
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else
			{
				switch (ch)
				{
				case '[':
				case ']':
					break;
				case '\\':
					return false;
				default:
					if (ch != '`')
					{
						return false;
					}
					return true;
				}
			}
			nextMustBeStartChar = true;
			return true;
		}
	}
}

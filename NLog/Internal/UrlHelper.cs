using System;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x020000B7 RID: 183
	internal class UrlHelper
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		internal static string UrlEncode(string str, bool spaceAsPlus)
		{
			StringBuilder stringBuilder = new StringBuilder(str.Length + 20);
			foreach (char c in str)
			{
				if (c == ' ' && spaceAsPlus)
				{
					stringBuilder.Append('+');
				}
				else if (UrlHelper.IsSafeUrlCharacter(c))
				{
					stringBuilder.Append(c);
				}
				else if (c < 'Ā')
				{
					stringBuilder.Append('%');
					stringBuilder.Append(UrlHelper.hexChars[(int)(c >> 4 & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)(c & '\u000f')]);
				}
				else
				{
					stringBuilder.Append('%');
					stringBuilder.Append('u');
					stringBuilder.Append(UrlHelper.hexChars[(int)(c >> 12 & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)(c >> 8 & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)(c >> 4 & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)(c & '\u000f')]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		private static bool IsSafeUrlCharacter(char ch)
		{
			return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9') || UrlHelper.safeUrlPunctuation.IndexOf(ch) >= 0;
		}

		// Token: 0x04000128 RID: 296
		private static string safeUrlPunctuation = ".()*-_!'";

		// Token: 0x04000129 RID: 297
		private static string hexChars = "0123456789abcdef";
	}
}

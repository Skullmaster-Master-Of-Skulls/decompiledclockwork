using System;

namespace System.Web.Util
{
	// Token: 0x02000206 RID: 518
	internal static class HttpEncoderUtility
	{
		// Token: 0x06001982 RID: 6530 RVA: 0x0004FAE0 File Offset: 0x0004DCE0
		public static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h < 'A' || h > 'F')
			{
				return -1;
			}
			return (int)(h - 'A' + '\n');
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0004FB16 File Offset: 0x0004DD16
		public static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0004FB2C File Offset: 0x0004DD2C
		public static bool IsUrlSafeChar(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0004FB8B File Offset: 0x0004DD8B
		internal static string UrlEncodeSpaces(string str)
		{
			if (str != null && str.IndexOf(' ') >= 0)
			{
				str = str.Replace(" ", "%20");
			}
			return str;
		}
	}
}

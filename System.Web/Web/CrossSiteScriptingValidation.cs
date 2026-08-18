using System;
using System.CodeDom.Compiler;

namespace System.Web
{
	// Token: 0x02000015 RID: 21
	internal static class CrossSiteScriptingValidation
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002DBA File Offset: 0x00001DBA
		private static bool IsAtoZ(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DD8 File Offset: 0x00001DD8
		internal static bool IsDangerousUrl(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return false;
			}
			s = s.Trim();
			int length = s.Length;
			if (length > 4 && (s[0] == 'h' || s[0] == 'H') && (s[1] == 't' || s[1] == 'T') && (s[2] == 't' || s[2] == 'T') && (s[3] == 'p' || s[3] == 'P') && (s[4] == ':' || (length > 5 && (s[4] == 's' || s[4] == 'S') && s[5] == ':')))
			{
				return false;
			}
			int num = s.IndexOf(':');
			return num != -1;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E9F File Offset: 0x00001E9F
		internal static bool IsValidJavascriptId(string id)
		{
			return string.IsNullOrEmpty(id) || CodeGenerator.IsValidLanguageIndependentIdentifier(id);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002EB4 File Offset: 0x00001EB4
		internal static bool IsDangerousString(string s, out int matchIndex)
		{
			matchIndex = 0;
			int startIndex = 0;
			for (;;)
			{
				int num = s.IndexOfAny(CrossSiteScriptingValidation.startingChars, startIndex);
				if (num < 0)
				{
					break;
				}
				if (num == s.Length - 1)
				{
					return false;
				}
				matchIndex = num;
				char c = s[num];
				if (c != '&')
				{
					if (c == '<' && (CrossSiteScriptingValidation.IsAtoZ(s[num + 1]) || s[num + 1] == '!' || s[num + 1] == '/' || s[num + 1] == '?'))
					{
						return true;
					}
				}
				else if (s[num + 1] == '#')
				{
					return true;
				}
				startIndex = num + 1;
			}
			return false;
		}

		// Token: 0x04000D0F RID: 3343
		private static char[] startingChars = new char[]
		{
			'<',
			'&'
		};
	}
}

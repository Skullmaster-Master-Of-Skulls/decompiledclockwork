using System;
using System.CodeDom.Compiler;

namespace System.Web
{
	// Token: 0x02000055 RID: 85
	internal static class CrossSiteScriptingValidation
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00007D5F File Offset: 0x00005F5F
		private static bool IsAtoZ(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00007D7C File Offset: 0x00005F7C
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

		// Token: 0x060005D1 RID: 1489 RVA: 0x00007E43 File Offset: 0x00006043
		internal static bool IsValidJavascriptId(string id)
		{
			return string.IsNullOrEmpty(id) || CodeGenerator.IsValidLanguageIndependentIdentifier(id);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00007E58 File Offset: 0x00006058
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

		// Token: 0x04000161 RID: 353
		private static char[] startingChars = new char[]
		{
			'<',
			'&'
		};
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebSockets
{
	// Token: 0x020001BD RID: 445
	internal static class SubProtocolUtil
	{
		// Token: 0x060016FE RID: 5886 RVA: 0x00048373 File Offset: 0x00046573
		public static bool IsValidSubProtocolName(string subprotocol)
		{
			return !string.IsNullOrEmpty(subprotocol) && subprotocol.All(new Func<char, bool>(SubProtocolUtil.IsValidSubProtocolChar));
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00048391 File Offset: 0x00046591
		private static bool IsValidSubProtocolChar(char c)
		{
			return '!' <= c && c <= '~' && !SubProtocolUtil.IsSeparatorChar(c);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000483A8 File Offset: 0x000465A8
		private static bool IsSeparatorChar(char c)
		{
			if (c <= ',')
			{
				if (c <= ' ')
				{
					if (c != '\t' && c != ' ')
					{
						return false;
					}
				}
				else if (c != '"')
				{
					switch (c)
					{
					case '(':
					case ')':
					case ',':
						break;
					case '*':
					case '+':
						return false;
					default:
						return false;
					}
				}
			}
			else if (c <= '@')
			{
				if (c != '/')
				{
					switch (c)
					{
					case ':':
					case ';':
					case '<':
					case '=':
					case '>':
					case '?':
					case '@':
						break;
					default:
						return false;
					}
				}
			}
			else
			{
				switch (c)
				{
				case '[':
				case '\\':
				case ']':
					break;
				default:
					if (c != '{' && c != '}')
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00048444 File Offset: 0x00046644
		public static List<string> ParseHeader(string headerValue)
		{
			if (headerValue == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (string text in headerValue.Split(SubProtocolUtil._splitChars))
			{
				string text2 = text.Trim(SubProtocolUtil._lwsTrimChars);
				if (text2.Length != 0)
				{
					if (!SubProtocolUtil.IsValidSubProtocolName(text2))
					{
						return null;
					}
					list.Add(text2);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Distinct(StringComparer.Ordinal).Count<string>() != list.Count)
			{
				return null;
			}
			return list;
		}

		// Token: 0x040016C1 RID: 5825
		private static readonly char[] _lwsTrimChars = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x040016C2 RID: 5826
		private static readonly char[] _splitChars = new char[]
		{
			','
		};
	}
}

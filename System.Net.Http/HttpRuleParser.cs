using System;
using System.Globalization;
using System.Text;

namespace System.Net.Http
{
	// Token: 0x0200000A RID: 10
	internal static class HttpRuleParser
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00003FC8 File Offset: 0x000021C8
		static HttpRuleParser()
		{
			HttpRuleParser.tokenChars = new bool[128];
			for (int i = 33; i < 127; i++)
			{
				HttpRuleParser.tokenChars[i] = true;
			}
			HttpRuleParser.tokenChars[40] = false;
			HttpRuleParser.tokenChars[41] = false;
			HttpRuleParser.tokenChars[60] = false;
			HttpRuleParser.tokenChars[62] = false;
			HttpRuleParser.tokenChars[64] = false;
			HttpRuleParser.tokenChars[44] = false;
			HttpRuleParser.tokenChars[59] = false;
			HttpRuleParser.tokenChars[58] = false;
			HttpRuleParser.tokenChars[92] = false;
			HttpRuleParser.tokenChars[34] = false;
			HttpRuleParser.tokenChars[47] = false;
			HttpRuleParser.tokenChars[91] = false;
			HttpRuleParser.tokenChars[93] = false;
			HttpRuleParser.tokenChars[63] = false;
			HttpRuleParser.tokenChars[61] = false;
			HttpRuleParser.tokenChars[123] = false;
			HttpRuleParser.tokenChars[125] = false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000412C File Offset: 0x0000232C
		internal static bool IsTokenChar(char character)
		{
			return character <= '\u007f' && HttpRuleParser.tokenChars[(int)character];
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000413C File Offset: 0x0000233C
		internal static int GetTokenLength(string input, int startIndex)
		{
			if (startIndex >= input.Length)
			{
				return 0;
			}
			for (int i = startIndex; i < input.Length; i++)
			{
				if (!HttpRuleParser.IsTokenChar(input[i]))
				{
					return i - startIndex;
				}
			}
			return input.Length - startIndex;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004180 File Offset: 0x00002380
		internal static int GetWhitespaceLength(string input, int startIndex)
		{
			if (startIndex >= input.Length)
			{
				return 0;
			}
			for (int i = startIndex; i < input.Length; i++)
			{
				char c = input[i];
				if (c != ' ' && c != '\t')
				{
					if (c == '\r' && i + 2 < input.Length && input[i + 1] == '\n')
					{
						char c2 = input[i + 2];
						if (c2 == ' ' || c2 == '\t')
						{
							i += 3;
							continue;
						}
					}
					return i - startIndex;
				}
			}
			return input.Length - startIndex;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004200 File Offset: 0x00002400
		internal static bool ContainsInvalidNewLine(string value)
		{
			return HttpRuleParser.ContainsInvalidNewLine(value, 0);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000420C File Offset: 0x0000240C
		internal static bool ContainsInvalidNewLine(string value, int startIndex)
		{
			for (int i = startIndex; i < value.Length; i++)
			{
				if (value[i] == '\r')
				{
					int num = i + 1;
					if (num < value.Length && value[num] == '\n')
					{
						i = num + 1;
						if (i == value.Length)
						{
							return true;
						}
						char c = value[i];
						if (c != ' ' && c != '\t')
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004274 File Offset: 0x00002474
		internal static int GetNumberLength(string input, int startIndex, bool allowDecimal)
		{
			int i = startIndex;
			bool flag = !allowDecimal;
			if (input[i] == '.')
			{
				return 0;
			}
			while (i < input.Length)
			{
				char c = input[i];
				if (c >= '0' && c <= '9')
				{
					i++;
				}
				else
				{
					if (flag || c != '.')
					{
						break;
					}
					flag = true;
					i++;
				}
			}
			return i - startIndex;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000042C8 File Offset: 0x000024C8
		internal static int GetHostLength(string input, int startIndex, bool allowToken, out string host)
		{
			host = null;
			if (startIndex >= input.Length)
			{
				return 0;
			}
			int i = startIndex;
			bool flag = true;
			while (i < input.Length)
			{
				char c = input[i];
				if (c == '/')
				{
					return 0;
				}
				if (c == ' ' || c == '\t' || c == '\r' || c == ',')
				{
					break;
				}
				flag = (flag && HttpRuleParser.IsTokenChar(c));
				i++;
			}
			int num = i - startIndex;
			if (num == 0)
			{
				return 0;
			}
			string text = input.Substring(startIndex, num);
			if ((!allowToken || !flag) && !HttpRuleParser.IsValidHostName(text))
			{
				return 0;
			}
			host = text;
			return num;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004354 File Offset: 0x00002554
		internal static HttpParseResult GetCommentLength(string input, int startIndex, out int length)
		{
			int num = 0;
			return HttpRuleParser.GetExpressionLength(input, startIndex, '(', ')', true, ref num, out length);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004374 File Offset: 0x00002574
		internal static HttpParseResult GetQuotedStringLength(string input, int startIndex, out int length)
		{
			int num = 0;
			return HttpRuleParser.GetExpressionLength(input, startIndex, '"', '"', false, ref num, out length);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004392 File Offset: 0x00002592
		internal static HttpParseResult GetQuotedPairLength(string input, int startIndex, out int length)
		{
			length = 0;
			if (input[startIndex] != '\\')
			{
				return HttpParseResult.NotParsed;
			}
			if (startIndex + 2 > input.Length || input[startIndex + 1] > '\u007f')
			{
				return HttpParseResult.InvalidFormat;
			}
			length = 2;
			return HttpParseResult.Parsed;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000043C4 File Offset: 0x000025C4
		internal static string DateToString(DateTimeOffset dateTime)
		{
			return dateTime.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000043EA File Offset: 0x000025EA
		internal static bool TryStringToDate(string input, out DateTimeOffset result)
		{
			return DateTimeOffset.TryParseExact(input, HttpRuleParser.dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out result);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004404 File Offset: 0x00002604
		private static HttpParseResult GetExpressionLength(string input, int startIndex, char openChar, char closeChar, bool supportsNesting, ref int nestedCount, out int length)
		{
			length = 0;
			if (input[startIndex] != openChar)
			{
				return HttpParseResult.NotParsed;
			}
			int i = startIndex + 1;
			while (i < input.Length)
			{
				int num = 0;
				if (i + 2 < input.Length && HttpRuleParser.GetQuotedPairLength(input, i, out num) == HttpParseResult.Parsed)
				{
					i += num;
				}
				else
				{
					if (supportsNesting && input[i] == openChar)
					{
						nestedCount++;
						try
						{
							if (nestedCount > 5)
							{
								return HttpParseResult.InvalidFormat;
							}
							int num2 = 0;
							switch (HttpRuleParser.GetExpressionLength(input, i, openChar, closeChar, supportsNesting, ref nestedCount, out num2))
							{
							case HttpParseResult.Parsed:
								i += num2;
								break;
							case HttpParseResult.InvalidFormat:
								return HttpParseResult.InvalidFormat;
							}
						}
						finally
						{
							nestedCount--;
						}
					}
					if (input[i] == closeChar)
					{
						length = i - startIndex + 1;
						return HttpParseResult.Parsed;
					}
					i++;
				}
			}
			return HttpParseResult.InvalidFormat;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000044E0 File Offset: 0x000026E0
		private static bool IsValidHostName(string host)
		{
			Uri uri;
			return Uri.TryCreate("http://u@" + host + "/", UriKind.Absolute, out uri);
		}

		// Token: 0x04000076 RID: 118
		private static readonly bool[] tokenChars;

		// Token: 0x04000077 RID: 119
		private const int maxNestedCount = 5;

		// Token: 0x04000078 RID: 120
		private static readonly string[] dateFormats = new string[]
		{
			"ddd, d MMM yyyy H:m:s 'GMT'",
			"ddd, d MMM yyyy H:m:s",
			"d MMM yyyy H:m:s 'GMT'",
			"d MMM yyyy H:m:s",
			"ddd, d MMM yy H:m:s 'GMT'",
			"ddd, d MMM yy H:m:s",
			"d MMM yy H:m:s 'GMT'",
			"d MMM yy H:m:s",
			"dddd, d'-'MMM'-'yy H:m:s 'GMT'",
			"dddd, d'-'MMM'-'yy H:m:s",
			"ddd MMM d H:m:s yyyy",
			"ddd, d MMM yyyy H:m:s zzz",
			"ddd, d MMM yyyy H:m:s",
			"d MMM yyyy H:m:s zzz",
			"d MMM yyyy H:m:s"
		};

		// Token: 0x04000079 RID: 121
		internal const char CR = '\r';

		// Token: 0x0400007A RID: 122
		internal const char LF = '\n';

		// Token: 0x0400007B RID: 123
		internal const int MaxInt64Digits = 19;

		// Token: 0x0400007C RID: 124
		internal const int MaxInt32Digits = 10;

		// Token: 0x0400007D RID: 125
		internal static readonly Encoding DefaultHttpEncoding = Encoding.GetEncoding(28591);
	}
}

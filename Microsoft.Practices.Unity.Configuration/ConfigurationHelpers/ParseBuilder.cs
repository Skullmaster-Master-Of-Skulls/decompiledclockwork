using System;
using System.Collections.Generic;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200000E RID: 14
	internal class ParseBuilder
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000027E0 File Offset: 0x000009E0
		public static ParseResult Any(InputStream input)
		{
			if (input.AtEnd)
			{
				return ParseBuilder.MatchFailed;
			}
			ParseResult result = new ParseResult(input.CurrentChar.ToString());
			input.Consume(1);
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002844 File Offset: 0x00000A44
		public static Func<InputStream, ParseResult> Match(char charToMatch)
		{
			return delegate(InputStream input)
			{
				if (!input.AtEnd && input.CurrentChar == charToMatch)
				{
					return ParseBuilder.MatchAndConsumeCurrentChar(input);
				}
				return ParseBuilder.MatchFailed;
			};
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028E4 File Offset: 0x00000AE4
		public static Func<InputStream, ParseResult> Match(string s)
		{
			return delegate(InputStream input)
			{
				int currentPosition = input.CurrentPosition;
				foreach (char c in s)
				{
					if (input.AtEnd || input.CurrentChar != c)
					{
						input.BackupTo(currentPosition);
						return ParseBuilder.MatchFailed;
					}
					input.Consume(1);
				}
				return new ParseResult(s);
			};
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000293C File Offset: 0x00000B3C
		public static Func<InputStream, ParseResult> Match(Func<char, bool> predicate)
		{
			return delegate(InputStream input)
			{
				if (!input.AtEnd && predicate(input.CurrentChar))
				{
					return ParseBuilder.MatchAndConsumeCurrentChar(input);
				}
				return ParseBuilder.MatchFailed;
			};
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029E8 File Offset: 0x00000BE8
		public static Func<InputStream, ParseResult> ZeroOrMore(Func<InputStream, ParseResult> inner)
		{
			return delegate(InputStream input)
			{
				List<ParseResult> list = new List<ParseResult>();
				ParseResult parseResult = inner(input);
				if (!parseResult.Matched)
				{
					return new ParseResult(true);
				}
				list.Add(parseResult);
				string text = parseResult.MatchedString;
				parseResult = inner(input);
				while (parseResult.Matched)
				{
					text += parseResult.MatchedString;
					list.Add(parseResult);
					parseResult = inner(input);
				}
				return new ParseResult(text, list);
			};
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A44 File Offset: 0x00000C44
		public static Func<InputStream, ParseResult> ZeroOrOne(Func<InputStream, ParseResult> expression)
		{
			return delegate(InputStream input)
			{
				ParseResult parseResult = expression(input);
				if (parseResult.Matched)
				{
					return parseResult;
				}
				return new ParseResult(true);
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public static Func<InputStream, ParseResult> OneOrMore(Func<InputStream, ParseResult> expression)
		{
			return delegate(InputStream input)
			{
				int currentPosition = input.CurrentPosition;
				List<ParseResult> list = new List<ParseResult>();
				ParseResult parseResult = expression(input);
				if (!parseResult.Matched)
				{
					input.BackupTo(currentPosition);
					return ParseBuilder.MatchFailed;
				}
				string text = string.Empty;
				while (parseResult.Matched)
				{
					list.Add(parseResult);
					text += parseResult.MatchedString;
					parseResult = expression(input);
				}
				return new ParseResult(text, list);
			};
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public static Func<InputStream, ParseResult> Sequence(params Func<InputStream, ParseResult>[] expressions)
		{
			return delegate(InputStream input)
			{
				int currentPosition = input.CurrentPosition;
				List<ParseResult> list = new List<ParseResult>(expressions.Length);
				string text = string.Empty;
				foreach (Func<InputStream, ParseResult> func in expressions)
				{
					ParseResult parseResult = func(input);
					if (!parseResult.Matched)
					{
						input.BackupTo(currentPosition);
						return ParseBuilder.MatchFailed;
					}
					list.Add(parseResult);
					text += parseResult.MatchedString;
				}
				return new ParseResult(text, list);
			};
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C1C File Offset: 0x00000E1C
		public static Func<InputStream, ParseResult> FirstOf(params Func<InputStream, ParseResult>[] expressions)
		{
			return delegate(InputStream input)
			{
				foreach (Func<InputStream, ParseResult> func in expressions)
				{
					ParseResult parseResult = func(input);
					if (parseResult.Matched)
					{
						return parseResult;
					}
				}
				return ParseBuilder.MatchFailed;
			};
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C84 File Offset: 0x00000E84
		public static Func<InputStream, ParseResult> Not(Func<InputStream, ParseResult> expression)
		{
			return delegate(InputStream input)
			{
				int currentPosition = input.CurrentPosition;
				ParseResult parseResult = expression(input);
				input.BackupTo(currentPosition);
				return new ParseResult(!parseResult.Matched);
			};
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CAA File Offset: 0x00000EAA
		public static ParseResult EOF(InputStream input)
		{
			return new ParseResult(input.AtEnd);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public static Func<InputStream, ParseResult> WithAction(Func<InputStream, ParseResult> expression, Action<ParseResult> onMatched)
		{
			return delegate(InputStream input)
			{
				ParseResult parseResult = expression(input);
				if (parseResult.Matched)
				{
					onMatched(parseResult);
				}
				return parseResult;
			};
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D58 File Offset: 0x00000F58
		public static Func<InputStream, ParseResult> WithAction(Func<InputStream, ParseResult> expression, Func<ParseResult, ParseResult> onMatched)
		{
			return delegate(InputStream input)
			{
				ParseResult parseResult = expression(input);
				if (parseResult.Matched)
				{
					return onMatched(parseResult);
				}
				return parseResult;
			};
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D88 File Offset: 0x00000F88
		private static ParseResult MatchAndConsumeCurrentChar(InputStream input)
		{
			ParseResult result = new ParseResult(input.CurrentChar.ToString());
			input.Consume(1);
			return result;
		}

		// Token: 0x0400000C RID: 12
		private static readonly ParseResult MatchFailed = new ParseResult(false);
	}
}

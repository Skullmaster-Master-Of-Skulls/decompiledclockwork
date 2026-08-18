using System;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x0200004B RID: 75
	[__DynamicallyInvokable]
	public class WarningHeaderValue : ICloneable
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000EF94 File Offset: 0x0000D194
		[__DynamicallyInvokable]
		public int Code
		{
			[__DynamicallyInvokable]
			get
			{
				return this.code;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000EF9C File Offset: 0x0000D19C
		[__DynamicallyInvokable]
		public string Agent
		{
			[__DynamicallyInvokable]
			get
			{
				return this.agent;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		[__DynamicallyInvokable]
		public string Text
		{
			[__DynamicallyInvokable]
			get
			{
				return this.text;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		[__DynamicallyInvokable]
		public DateTimeOffset? Date
		{
			[__DynamicallyInvokable]
			get
			{
				return this.date;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		[__DynamicallyInvokable]
		public WarningHeaderValue(int code, string agent, string text)
		{
			WarningHeaderValue.CheckCode(code);
			WarningHeaderValue.CheckAgent(agent);
			HeaderUtilities.CheckValidQuotedString(text, "text");
			this.code = code;
			this.agent = agent;
			this.text = text;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		[__DynamicallyInvokable]
		public WarningHeaderValue(int code, string agent, string text, DateTimeOffset date)
		{
			WarningHeaderValue.CheckCode(code);
			WarningHeaderValue.CheckAgent(agent);
			HeaderUtilities.CheckValidQuotedString(text, "text");
			this.code = code;
			this.agent = agent;
			this.text = text;
			this.date = new DateTimeOffset?(date);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000F034 File Offset: 0x0000D234
		private WarningHeaderValue()
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000F03C File Offset: 0x0000D23C
		private WarningHeaderValue(WarningHeaderValue source)
		{
			this.code = source.code;
			this.agent = source.agent;
			this.text = source.text;
			this.date = source.date;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F074 File Offset: 0x0000D274
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.code.ToString("000", NumberFormatInfo.InvariantInfo));
			stringBuilder.Append(' ');
			stringBuilder.Append(this.agent);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.text);
			if (this.date != null)
			{
				stringBuilder.Append(" \"");
				stringBuilder.Append(HttpRuleParser.DateToString(this.date.Value));
				stringBuilder.Append('"');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F110 File Offset: 0x0000D310
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			WarningHeaderValue warningHeaderValue = obj as WarningHeaderValue;
			if (warningHeaderValue == null)
			{
				return false;
			}
			if (this.code != warningHeaderValue.code || string.Compare(this.agent, warningHeaderValue.agent, StringComparison.OrdinalIgnoreCase) != 0 || string.CompareOrdinal(this.text, warningHeaderValue.text) != 0)
			{
				return false;
			}
			if (this.date != null)
			{
				return warningHeaderValue.date != null && this.date.Value == warningHeaderValue.date.Value;
			}
			return warningHeaderValue.date == null;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.code.GetHashCode() ^ this.agent.ToLowerInvariant().GetHashCode() ^ this.text.GetHashCode();
			if (this.date != null)
			{
				num ^= this.date.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F208 File Offset: 0x0000D408
		[__DynamicallyInvokable]
		public static WarningHeaderValue Parse(string input)
		{
			int num = 0;
			return (WarningHeaderValue)GenericHeaderParser.SingleValueWarningParser.ParseValue(input, null, ref num);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F22C File Offset: 0x0000D42C
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out WarningHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueWarningParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (WarningHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F25C File Offset: 0x0000D45C
		internal static int GetWarningLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			int num = startIndex;
			int num2;
			if (!WarningHeaderValue.TryReadCode(input, ref num, out num2))
			{
				return 0;
			}
			string text;
			if (!WarningHeaderValue.TryReadAgent(input, num, ref num, out text))
			{
				return 0;
			}
			int num3 = 0;
			int startIndex2 = num;
			if (HttpRuleParser.GetQuotedStringLength(input, num, out num3) != HttpParseResult.Parsed)
			{
				return 0;
			}
			num += num3;
			DateTimeOffset? dateTimeOffset = null;
			if (!WarningHeaderValue.TryReadDate(input, ref num, out dateTimeOffset))
			{
				return 0;
			}
			parsedValue = new WarningHeaderValue
			{
				code = num2,
				agent = text,
				text = input.Substring(startIndex2, num3),
				date = dateTimeOffset
			};
			return num - startIndex;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F304 File Offset: 0x0000D504
		private static bool TryReadAgent(string input, int startIndex, ref int current, out string agent)
		{
			agent = null;
			int hostLength = HttpRuleParser.GetHostLength(input, startIndex, true, out agent);
			if (hostLength == 0)
			{
				return false;
			}
			current += hostLength;
			int whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, current);
			current += whitespaceLength;
			return whitespaceLength != 0 && current != input.Length;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000F348 File Offset: 0x0000D548
		private static bool TryReadCode(string input, ref int current, out int code)
		{
			code = 0;
			int numberLength = HttpRuleParser.GetNumberLength(input, current, false);
			if (numberLength == 0 || numberLength > 3)
			{
				return false;
			}
			if (!HeaderUtilities.TryParseInt32(input.Substring(current, numberLength), out code))
			{
				return false;
			}
			current += numberLength;
			int whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, current);
			current += whitespaceLength;
			return whitespaceLength != 0 && current != input.Length;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		private static bool TryReadDate(string input, ref int current, out DateTimeOffset? date)
		{
			date = null;
			int whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, current);
			current += whitespaceLength;
			if (current < input.Length && input[current] == '"')
			{
				if (whitespaceLength == 0)
				{
					return false;
				}
				current++;
				int num = current;
				while (current < input.Length && input[current] != '"')
				{
					current++;
				}
				if (current == input.Length || current == num)
				{
					return false;
				}
				DateTimeOffset value;
				if (!HttpRuleParser.TryStringToDate(input.Substring(num, current - num), out value))
				{
					return false;
				}
				date = new DateTimeOffset?(value);
				current++;
				current += HttpRuleParser.GetWhitespaceLength(input, current);
			}
			return true;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F450 File Offset: 0x0000D650
		object ICloneable.Clone()
		{
			return new WarningHeaderValue(this);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000F458 File Offset: 0x0000D658
		private static void CheckCode(int code)
		{
			if (code < 0 || code > 999)
			{
				throw new ArgumentOutOfRangeException("code");
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000F474 File Offset: 0x0000D674
		private static void CheckAgent(string agent)
		{
			if (string.IsNullOrEmpty(agent))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "agent");
			}
			string text = null;
			if (HttpRuleParser.GetHostLength(agent, 0, true, out text) != agent.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					agent
				}));
			}
		}

		// Token: 0x04000188 RID: 392
		private int code;

		// Token: 0x04000189 RID: 393
		private string agent;

		// Token: 0x0400018A RID: 394
		private string text;

		// Token: 0x0400018B RID: 395
		private DateTimeOffset? date;
	}
}

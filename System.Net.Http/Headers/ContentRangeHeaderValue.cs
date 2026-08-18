using System;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x02000029 RID: 41
	[__DynamicallyInvokable]
	public class ContentRangeHeaderValue : ICloneable
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008A44 File Offset: 0x00006C44
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00008A4C File Offset: 0x00006C4C
		[__DynamicallyInvokable]
		public string Unit
		{
			[__DynamicallyInvokable]
			get
			{
				return this.unit;
			}
			[__DynamicallyInvokable]
			set
			{
				HeaderUtilities.CheckValidToken(value, "value");
				this.unit = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008A60 File Offset: 0x00006C60
		[__DynamicallyInvokable]
		public long? From
		{
			[__DynamicallyInvokable]
			get
			{
				return this.from;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008A68 File Offset: 0x00006C68
		[__DynamicallyInvokable]
		public long? To
		{
			[__DynamicallyInvokable]
			get
			{
				return this.to;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008A70 File Offset: 0x00006C70
		[__DynamicallyInvokable]
		public long? Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00008A78 File Offset: 0x00006C78
		[__DynamicallyInvokable]
		public bool HasLength
		{
			[__DynamicallyInvokable]
			get
			{
				return this.length != null;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00008A85 File Offset: 0x00006C85
		[__DynamicallyInvokable]
		public bool HasRange
		{
			[__DynamicallyInvokable]
			get
			{
				return this.from != null;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008A94 File Offset: 0x00006C94
		[__DynamicallyInvokable]
		public ContentRangeHeaderValue(long from, long to, long length)
		{
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (to < 0L || to > length)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			if (from < 0L || from > to)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			this.from = new long?(from);
			this.to = new long?(to);
			this.length = new long?(length);
			this.unit = "bytes";
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008B0E File Offset: 0x00006D0E
		[__DynamicallyInvokable]
		public ContentRangeHeaderValue(long length)
		{
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.length = new long?(length);
			this.unit = "bytes";
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008B40 File Offset: 0x00006D40
		[__DynamicallyInvokable]
		public ContentRangeHeaderValue(long from, long to)
		{
			if (to < 0L)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			if (from < 0L || from > to)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			this.from = new long?(from);
			this.to = new long?(to);
			this.unit = "bytes";
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008B9A File Offset: 0x00006D9A
		private ContentRangeHeaderValue()
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008BA2 File Offset: 0x00006DA2
		private ContentRangeHeaderValue(ContentRangeHeaderValue source)
		{
			this.from = source.from;
			this.to = source.to;
			this.length = source.length;
			this.unit = source.unit;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008BDC File Offset: 0x00006DDC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			ContentRangeHeaderValue contentRangeHeaderValue = obj as ContentRangeHeaderValue;
			if (contentRangeHeaderValue == null)
			{
				return false;
			}
			long? num = this.from;
			long? num2 = contentRangeHeaderValue.from;
			if (num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null))
			{
				num2 = this.to;
				num = contentRangeHeaderValue.to;
				if (num2.GetValueOrDefault() == num.GetValueOrDefault() & num2 != null == (num != null))
				{
					num = this.length;
					num2 = contentRangeHeaderValue.length;
					if (num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null))
					{
						return string.Compare(this.unit, contentRangeHeaderValue.unit, StringComparison.OrdinalIgnoreCase) == 0;
					}
				}
			}
			return false;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008CA0 File Offset: 0x00006EA0
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.unit.ToLowerInvariant().GetHashCode();
			if (this.HasRange)
			{
				num = (num ^ this.from.GetHashCode() ^ this.to.GetHashCode());
			}
			if (this.HasLength)
			{
				num ^= this.length.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008D0C File Offset: 0x00006F0C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.unit);
			stringBuilder.Append(' ');
			if (this.HasRange)
			{
				stringBuilder.Append(this.from.Value.ToString(NumberFormatInfo.InvariantInfo));
				stringBuilder.Append('-');
				stringBuilder.Append(this.to.Value.ToString(NumberFormatInfo.InvariantInfo));
			}
			else
			{
				stringBuilder.Append('*');
			}
			stringBuilder.Append('/');
			if (this.HasLength)
			{
				stringBuilder.Append(this.length.Value.ToString(NumberFormatInfo.InvariantInfo));
			}
			else
			{
				stringBuilder.Append('*');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008DCC File Offset: 0x00006FCC
		[__DynamicallyInvokable]
		public static ContentRangeHeaderValue Parse(string input)
		{
			int num = 0;
			return (ContentRangeHeaderValue)GenericHeaderParser.ContentRangeParser.ParseValue(input, null, ref num);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008DF0 File Offset: 0x00006FF0
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out ContentRangeHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.ContentRangeParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (ContentRangeHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008E20 File Offset: 0x00007020
		internal static int GetContentRangeLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			int tokenLength = HttpRuleParser.GetTokenLength(input, startIndex);
			if (tokenLength == 0)
			{
				return 0;
			}
			string text = input.Substring(startIndex, tokenLength);
			int num = startIndex + tokenLength;
			int whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, num);
			if (whitespaceLength == 0)
			{
				return 0;
			}
			num += whitespaceLength;
			if (num == input.Length)
			{
				return 0;
			}
			int fromStartIndex = num;
			int fromLength = 0;
			int toStartIndex = 0;
			int toLength = 0;
			if (!ContentRangeHeaderValue.TryGetRangeLength(input, ref num, out fromLength, out toStartIndex, out toLength))
			{
				return 0;
			}
			if (num == input.Length || input[num] != '/')
			{
				return 0;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length)
			{
				return 0;
			}
			int lengthStartIndex = num;
			int lengthLength = 0;
			if (!ContentRangeHeaderValue.TryGetLengthLength(input, ref num, out lengthLength))
			{
				return 0;
			}
			if (!ContentRangeHeaderValue.TryCreateContentRange(input, text, fromStartIndex, fromLength, toStartIndex, toLength, lengthStartIndex, lengthLength, out parsedValue))
			{
				return 0;
			}
			return num - startIndex;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008EF8 File Offset: 0x000070F8
		private static bool TryGetLengthLength(string input, ref int current, out int lengthLength)
		{
			lengthLength = 0;
			if (input[current] == '*')
			{
				current++;
			}
			else
			{
				lengthLength = HttpRuleParser.GetNumberLength(input, current, false);
				if (lengthLength == 0 || lengthLength > 19)
				{
					return false;
				}
				current += lengthLength;
			}
			current += HttpRuleParser.GetWhitespaceLength(input, current);
			return true;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008F48 File Offset: 0x00007148
		private static bool TryGetRangeLength(string input, ref int current, out int fromLength, out int toStartIndex, out int toLength)
		{
			fromLength = 0;
			toStartIndex = 0;
			toLength = 0;
			if (input[current] == '*')
			{
				current++;
			}
			else
			{
				fromLength = HttpRuleParser.GetNumberLength(input, current, false);
				if (fromLength == 0 || fromLength > 19)
				{
					return false;
				}
				current += fromLength;
				current += HttpRuleParser.GetWhitespaceLength(input, current);
				if (current == input.Length || input[current] != '-')
				{
					return false;
				}
				current++;
				current += HttpRuleParser.GetWhitespaceLength(input, current);
				if (current == input.Length)
				{
					return false;
				}
				toStartIndex = current;
				toLength = HttpRuleParser.GetNumberLength(input, current, false);
				if (toLength == 0 || toLength > 19)
				{
					return false;
				}
				current += toLength;
			}
			current += HttpRuleParser.GetWhitespaceLength(input, current);
			return true;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000900C File Offset: 0x0000720C
		private static bool TryCreateContentRange(string input, string unit, int fromStartIndex, int fromLength, int toStartIndex, int toLength, int lengthStartIndex, int lengthLength, out object parsedValue)
		{
			parsedValue = null;
			long num = 0L;
			if (fromLength > 0 && !HeaderUtilities.TryParseInt64(input.Substring(fromStartIndex, fromLength), out num))
			{
				return false;
			}
			long num2 = 0L;
			if (toLength > 0 && !HeaderUtilities.TryParseInt64(input.Substring(toStartIndex, toLength), out num2))
			{
				return false;
			}
			if (fromLength > 0 && toLength > 0 && num > num2)
			{
				return false;
			}
			long num3 = 0L;
			if (lengthLength > 0 && !HeaderUtilities.TryParseInt64(input.Substring(lengthStartIndex, lengthLength), out num3))
			{
				return false;
			}
			if (toLength > 0 && lengthLength > 0 && num2 >= num3)
			{
				return false;
			}
			ContentRangeHeaderValue contentRangeHeaderValue = new ContentRangeHeaderValue();
			contentRangeHeaderValue.unit = unit;
			if (fromLength > 0)
			{
				contentRangeHeaderValue.from = new long?(num);
				contentRangeHeaderValue.to = new long?(num2);
			}
			if (lengthLength > 0)
			{
				contentRangeHeaderValue.length = new long?(num3);
			}
			parsedValue = contentRangeHeaderValue;
			return true;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000090CF File Offset: 0x000072CF
		object ICloneable.Clone()
		{
			return new ContentRangeHeaderValue(this);
		}

		// Token: 0x04000100 RID: 256
		private string unit;

		// Token: 0x04000101 RID: 257
		private long? from;

		// Token: 0x04000102 RID: 258
		private long? to;

		// Token: 0x04000103 RID: 259
		private long? length;
	}
}

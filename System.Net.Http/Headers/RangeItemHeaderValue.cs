using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000042 RID: 66
	[__DynamicallyInvokable]
	public class RangeItemHeaderValue : ICloneable
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000DC07 File Offset: 0x0000BE07
		[__DynamicallyInvokable]
		public long? From
		{
			[__DynamicallyInvokable]
			get
			{
				return this.from;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000DC0F File Offset: 0x0000BE0F
		[__DynamicallyInvokable]
		public long? To
		{
			[__DynamicallyInvokable]
			get
			{
				return this.to;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000DC18 File Offset: 0x0000BE18
		[__DynamicallyInvokable]
		public RangeItemHeaderValue(long? from, long? to)
		{
			if (from == null && to == null)
			{
				throw new ArgumentException(SR.net_http_headers_invalid_range);
			}
			if (from != null && from.Value < 0L)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			if (to != null && to.Value < 0L)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			if (from != null && to != null && from.Value > to.Value)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			this.from = from;
			this.to = to;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000DCC1 File Offset: 0x0000BEC1
		private RangeItemHeaderValue(RangeItemHeaderValue source)
		{
			this.from = source.from;
			this.to = source.to;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.from == null)
			{
				return "-" + this.to.Value.ToString(NumberFormatInfo.InvariantInfo);
			}
			if (this.to == null)
			{
				return this.from.Value.ToString(NumberFormatInfo.InvariantInfo) + "-";
			}
			return this.from.Value.ToString(NumberFormatInfo.InvariantInfo) + "-" + this.to.Value.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			RangeItemHeaderValue rangeItemHeaderValue = obj as RangeItemHeaderValue;
			if (rangeItemHeaderValue == null)
			{
				return false;
			}
			long? num = this.from;
			long? num2 = rangeItemHeaderValue.from;
			if (num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null))
			{
				num2 = this.to;
				num = rangeItemHeaderValue.to;
				return num2.GetValueOrDefault() == num.GetValueOrDefault() & num2 != null == (num != null);
			}
			return false;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DE08 File Offset: 0x0000C008
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.from == null)
			{
				return this.to.GetHashCode();
			}
			if (this.to == null)
			{
				return this.from.GetHashCode();
			}
			return this.from.GetHashCode() ^ this.to.GetHashCode();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DE78 File Offset: 0x0000C078
		internal static int GetRangeItemListLength(string input, int startIndex, ICollection<RangeItemHeaderValue> rangeCollection)
		{
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			bool flag = false;
			int num = HeaderUtilities.GetNextNonEmptyOrWhitespaceIndex(input, startIndex, true, out flag);
			if (num == input.Length)
			{
				return 0;
			}
			RangeItemHeaderValue item = null;
			for (;;)
			{
				int rangeItemLength = RangeItemHeaderValue.GetRangeItemLength(input, num, out item);
				if (rangeItemLength == 0)
				{
					break;
				}
				rangeCollection.Add(item);
				num += rangeItemLength;
				num = HeaderUtilities.GetNextNonEmptyOrWhitespaceIndex(input, num, true, out flag);
				if (num < input.Length && !flag)
				{
					return 0;
				}
				if (num == input.Length)
				{
					goto Block_6;
				}
			}
			return 0;
			Block_6:
			return num - startIndex;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		internal static int GetRangeItemLength(string input, int startIndex, out RangeItemHeaderValue parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			int numberLength = HttpRuleParser.GetNumberLength(input, startIndex, false);
			if (numberLength > 19)
			{
				return 0;
			}
			int num = startIndex + numberLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length || input[num] != '-')
			{
				return 0;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			int startIndex2 = num;
			int num2 = 0;
			if (num < input.Length)
			{
				num2 = HttpRuleParser.GetNumberLength(input, num, false);
				if (num2 > 19)
				{
					return 0;
				}
				num += num2;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
			}
			if (numberLength == 0 && num2 == 0)
			{
				return 0;
			}
			long num3 = 0L;
			if (numberLength > 0 && !HeaderUtilities.TryParseInt64(input.Substring(startIndex, numberLength), out num3))
			{
				return 0;
			}
			long num4 = 0L;
			if (num2 > 0 && !HeaderUtilities.TryParseInt64(input.Substring(startIndex2, num2), out num4))
			{
				return 0;
			}
			if (numberLength > 0 && num2 > 0 && num3 > num4)
			{
				return 0;
			}
			parsedValue = new RangeItemHeaderValue((numberLength == 0) ? null : new long?(num3), (num2 == 0) ? null : new long?(num4));
			return num - startIndex;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000E015 File Offset: 0x0000C215
		object ICloneable.Clone()
		{
			return new RangeItemHeaderValue(this);
		}

		// Token: 0x04000174 RID: 372
		private long? from;

		// Token: 0x04000175 RID: 373
		private long? to;
	}
}

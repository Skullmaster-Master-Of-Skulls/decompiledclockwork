using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x02000041 RID: 65
	[__DynamicallyInvokable]
	public class RangeHeaderValue : ICloneable
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000D8E9 File Offset: 0x0000BAE9
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000D8F1 File Offset: 0x0000BAF1
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000D905 File Offset: 0x0000BB05
		[__DynamicallyInvokable]
		public ICollection<RangeItemHeaderValue> Ranges
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.ranges == null)
				{
					this.ranges = new ObjectCollection<RangeItemHeaderValue>();
				}
				return this.ranges;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D920 File Offset: 0x0000BB20
		[__DynamicallyInvokable]
		public RangeHeaderValue()
		{
			this.unit = "bytes";
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000D933 File Offset: 0x0000BB33
		[__DynamicallyInvokable]
		public RangeHeaderValue(long? from, long? to)
		{
			this.unit = "bytes";
			this.Ranges.Add(new RangeItemHeaderValue(from, to));
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000D958 File Offset: 0x0000BB58
		private RangeHeaderValue(RangeHeaderValue source)
		{
			this.unit = source.unit;
			if (source.ranges != null)
			{
				foreach (RangeItemHeaderValue rangeItemHeaderValue in source.ranges)
				{
					this.Ranges.Add((RangeItemHeaderValue)((ICloneable)rangeItemHeaderValue).Clone());
				}
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.unit);
			stringBuilder.Append('=');
			bool flag = true;
			foreach (RangeItemHeaderValue rangeItemHeaderValue in this.Ranges)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(rangeItemHeaderValue.From);
				stringBuilder.Append('-');
				stringBuilder.Append(rangeItemHeaderValue.To);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000DA74 File Offset: 0x0000BC74
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			RangeHeaderValue rangeHeaderValue = obj as RangeHeaderValue;
			return rangeHeaderValue != null && string.Compare(this.unit, rangeHeaderValue.unit, StringComparison.OrdinalIgnoreCase) == 0 && HeaderUtilities.AreEqualCollections<RangeItemHeaderValue>(this.Ranges, rangeHeaderValue.Ranges);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.unit.ToLowerInvariant().GetHashCode();
			foreach (RangeItemHeaderValue rangeItemHeaderValue in this.Ranges)
			{
				num ^= rangeItemHeaderValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DB18 File Offset: 0x0000BD18
		[__DynamicallyInvokable]
		public static RangeHeaderValue Parse(string input)
		{
			int num = 0;
			return (RangeHeaderValue)GenericHeaderParser.RangeParser.ParseValue(input, null, ref num);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out RangeHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.RangeParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (RangeHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		internal static int GetRangeLength(string input, int startIndex, out object parsedValue)
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
			RangeHeaderValue rangeHeaderValue = new RangeHeaderValue();
			rangeHeaderValue.unit = input.Substring(startIndex, tokenLength);
			int num = startIndex + tokenLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length || input[num] != '=')
			{
				return 0;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			int rangeItemListLength = RangeItemHeaderValue.GetRangeItemListLength(input, num, rangeHeaderValue.Ranges);
			if (rangeItemListLength == 0)
			{
				return 0;
			}
			num += rangeItemListLength;
			parsedValue = rangeHeaderValue;
			return num - startIndex;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		object ICloneable.Clone()
		{
			return new RangeHeaderValue(this);
		}

		// Token: 0x04000172 RID: 370
		private string unit;

		// Token: 0x04000173 RID: 371
		private ICollection<RangeItemHeaderValue> ranges;
	}
}

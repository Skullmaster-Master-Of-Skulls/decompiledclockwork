using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000043 RID: 67
	[__DynamicallyInvokable]
	public class RetryConditionHeaderValue : ICloneable
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000E01D File Offset: 0x0000C21D
		[__DynamicallyInvokable]
		public DateTimeOffset? Date
		{
			[__DynamicallyInvokable]
			get
			{
				return this.date;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000E025 File Offset: 0x0000C225
		[__DynamicallyInvokable]
		public TimeSpan? Delta
		{
			[__DynamicallyInvokable]
			get
			{
				return this.delta;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E02D File Offset: 0x0000C22D
		[__DynamicallyInvokable]
		public RetryConditionHeaderValue(DateTimeOffset date)
		{
			this.date = new DateTimeOffset?(date);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000E041 File Offset: 0x0000C241
		[__DynamicallyInvokable]
		public RetryConditionHeaderValue(TimeSpan delta)
		{
			if (delta.TotalSeconds > 2147483647.0)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			this.delta = new TimeSpan?(delta);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E072 File Offset: 0x0000C272
		private RetryConditionHeaderValue(RetryConditionHeaderValue source)
		{
			this.delta = source.delta;
			this.date = source.date;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E092 File Offset: 0x0000C292
		private RetryConditionHeaderValue()
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000E09C File Offset: 0x0000C29C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.delta != null)
			{
				return ((int)this.delta.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
			}
			return HttpRuleParser.DateToString(this.date.Value);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			RetryConditionHeaderValue retryConditionHeaderValue = obj as RetryConditionHeaderValue;
			if (retryConditionHeaderValue == null)
			{
				return false;
			}
			if (this.delta != null)
			{
				return retryConditionHeaderValue.delta != null && this.delta.Value == retryConditionHeaderValue.delta.Value;
			}
			return retryConditionHeaderValue.date != null && this.date.Value == retryConditionHeaderValue.date.Value;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000E164 File Offset: 0x0000C364
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.delta == null)
			{
				return this.date.Value.GetHashCode();
			}
			return this.delta.Value.GetHashCode();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		[__DynamicallyInvokable]
		public static RetryConditionHeaderValue Parse(string input)
		{
			int num = 0;
			return (RetryConditionHeaderValue)GenericHeaderParser.RetryConditionParser.ParseValue(input, null, ref num);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out RetryConditionHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.RetryConditionParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (RetryConditionHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E208 File Offset: 0x0000C408
		internal static int GetRetryConditionLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			DateTimeOffset minValue = DateTimeOffset.MinValue;
			int num = -1;
			char c = input[startIndex];
			int num2;
			if (c >= '0' && c <= '9')
			{
				int numberLength = HttpRuleParser.GetNumberLength(input, startIndex, false);
				if (numberLength == 0 || numberLength > 10)
				{
					return 0;
				}
				num2 = startIndex + numberLength;
				num2 += HttpRuleParser.GetWhitespaceLength(input, num2);
				if (num2 != input.Length)
				{
					return 0;
				}
				if (!HeaderUtilities.TryParseInt32(input.Substring(startIndex, numberLength), out num))
				{
					return 0;
				}
			}
			else
			{
				if (!HttpRuleParser.TryStringToDate(input.Substring(startIndex), out minValue))
				{
					return 0;
				}
				num2 = input.Length;
			}
			RetryConditionHeaderValue retryConditionHeaderValue = new RetryConditionHeaderValue();
			if (num == -1)
			{
				retryConditionHeaderValue.date = new DateTimeOffset?(minValue);
			}
			else
			{
				retryConditionHeaderValue.delta = new TimeSpan?(new TimeSpan(0, 0, num));
			}
			parsedValue = retryConditionHeaderValue;
			return num2 - startIndex;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E2DD File Offset: 0x0000C4DD
		object ICloneable.Clone()
		{
			return new RetryConditionHeaderValue(this);
		}

		// Token: 0x04000176 RID: 374
		private DateTimeOffset? date;

		// Token: 0x04000177 RID: 375
		private TimeSpan? delta;
	}
}

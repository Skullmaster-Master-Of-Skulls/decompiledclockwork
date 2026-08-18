using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000044 RID: 68
	[__DynamicallyInvokable]
	public class StringWithQualityHeaderValue : ICloneable
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000E2E5 File Offset: 0x0000C4E5
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000E2ED File Offset: 0x0000C4ED
		[__DynamicallyInvokable]
		public double? Quality
		{
			[__DynamicallyInvokable]
			get
			{
				return this.quality;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E2F5 File Offset: 0x0000C4F5
		[__DynamicallyInvokable]
		public StringWithQualityHeaderValue(string value)
		{
			HeaderUtilities.CheckValidToken(value, "value");
			this.value = value;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E310 File Offset: 0x0000C510
		[__DynamicallyInvokable]
		public StringWithQualityHeaderValue(string value, double quality)
		{
			HeaderUtilities.CheckValidToken(value, "value");
			if (quality < 0.0 || quality > 1.0)
			{
				throw new ArgumentOutOfRangeException("quality");
			}
			this.value = value;
			this.quality = new double?(quality);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E364 File Offset: 0x0000C564
		private StringWithQualityHeaderValue(StringWithQualityHeaderValue source)
		{
			this.value = source.value;
			this.quality = source.quality;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E384 File Offset: 0x0000C584
		private StringWithQualityHeaderValue()
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E38C File Offset: 0x0000C58C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.quality != null)
			{
				return this.value + "; q=" + this.quality.Value.ToString("0.0##", NumberFormatInfo.InvariantInfo);
			}
			return this.value;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			StringWithQualityHeaderValue stringWithQualityHeaderValue = obj as StringWithQualityHeaderValue;
			if (stringWithQualityHeaderValue == null)
			{
				return false;
			}
			if (string.Compare(this.value, stringWithQualityHeaderValue.value, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (this.quality != null)
			{
				return stringWithQualityHeaderValue.quality != null && this.quality.Value == stringWithQualityHeaderValue.quality.Value;
			}
			return stringWithQualityHeaderValue.quality == null;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E450 File Offset: 0x0000C650
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.value.ToLowerInvariant().GetHashCode();
			if (this.quality != null)
			{
				num ^= this.quality.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E494 File Offset: 0x0000C694
		[__DynamicallyInvokable]
		public static StringWithQualityHeaderValue Parse(string input)
		{
			int num = 0;
			return (StringWithQualityHeaderValue)GenericHeaderParser.SingleValueStringWithQualityParser.ParseValue(input, null, ref num);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out StringWithQualityHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueStringWithQualityParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (StringWithQualityHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		internal static int GetStringWithQualityLength(string input, int startIndex, out object parsedValue)
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
			StringWithQualityHeaderValue stringWithQualityHeaderValue = new StringWithQualityHeaderValue();
			stringWithQualityHeaderValue.value = input.Substring(startIndex, tokenLength);
			int num = startIndex + tokenLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length || input[num] != ';')
			{
				parsedValue = stringWithQualityHeaderValue;
				return num - startIndex;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (!StringWithQualityHeaderValue.TryReadQuality(input, stringWithQualityHeaderValue, ref num))
			{
				return 0;
			}
			parsedValue = stringWithQualityHeaderValue;
			return num - startIndex;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E578 File Offset: 0x0000C778
		private static bool TryReadQuality(string input, StringWithQualityHeaderValue result, ref int index)
		{
			int num = index;
			if (num == input.Length || (input[num] != 'q' && input[num] != 'Q'))
			{
				return false;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length || input[num] != '=')
			{
				return false;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length)
			{
				return false;
			}
			int numberLength = HttpRuleParser.GetNumberLength(input, num, true);
			if (numberLength == 0)
			{
				return false;
			}
			double num2 = 0.0;
			if (!double.TryParse(input.Substring(num, numberLength), NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out num2))
			{
				return false;
			}
			if (num2 < 0.0 || num2 > 1.0)
			{
				return false;
			}
			result.quality = new double?(num2);
			num += numberLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			index = num;
			return true;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E650 File Offset: 0x0000C850
		object ICloneable.Clone()
		{
			return new StringWithQualityHeaderValue(this);
		}

		// Token: 0x04000178 RID: 376
		private string value;

		// Token: 0x04000179 RID: 377
		private double? quality;
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003A RID: 58
	[__DynamicallyInvokable]
	public class NameValueHeaderValue : ICloneable
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000C9EB File Offset: 0x0000ABEB
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C9F3 File Offset: 0x0000ABF3
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
			[__DynamicallyInvokable]
			set
			{
				NameValueHeaderValue.CheckValueFormat(value);
				this.value = value;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000CA0A File Offset: 0x0000AC0A
		internal NameValueHeaderValue()
		{
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000CA12 File Offset: 0x0000AC12
		[__DynamicallyInvokable]
		public NameValueHeaderValue(string name) : this(name, null)
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		[__DynamicallyInvokable]
		public NameValueHeaderValue(string name, string value)
		{
			NameValueHeaderValue.CheckNameValueFormat(name, value);
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CA39 File Offset: 0x0000AC39
		[__DynamicallyInvokable]
		protected NameValueHeaderValue(NameValueHeaderValue source)
		{
			this.name = source.name;
			this.value = source.value;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int hashCode = this.name.ToLowerInvariant().GetHashCode();
			if (string.IsNullOrEmpty(this.value))
			{
				return hashCode;
			}
			if (this.value[0] == '"')
			{
				return hashCode ^ this.value.GetHashCode();
			}
			return hashCode ^ this.value.ToLowerInvariant().GetHashCode();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000CABC File Offset: 0x0000ACBC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			NameValueHeaderValue nameValueHeaderValue = obj as NameValueHeaderValue;
			if (nameValueHeaderValue == null)
			{
				return false;
			}
			if (string.Compare(this.name, nameValueHeaderValue.name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.value))
			{
				return string.IsNullOrEmpty(nameValueHeaderValue.value);
			}
			if (this.value[0] == '"')
			{
				return string.CompareOrdinal(this.value, nameValueHeaderValue.value) == 0;
			}
			return string.Compare(this.value, nameValueHeaderValue.value, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CB40 File Offset: 0x0000AD40
		[__DynamicallyInvokable]
		public static NameValueHeaderValue Parse(string input)
		{
			int num = 0;
			return (NameValueHeaderValue)GenericHeaderParser.SingleValueNameValueParser.ParseValue(input, null, ref num);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000CB64 File Offset: 0x0000AD64
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out NameValueHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueNameValueParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (NameValueHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000CB93 File Offset: 0x0000AD93
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.value))
			{
				return this.name + "=" + this.value;
			}
			return this.name;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		internal static void ToString(ICollection<NameValueHeaderValue> values, char separator, bool leadingSeparator, StringBuilder destination)
		{
			if (values == null || values.Count == 0)
			{
				return;
			}
			foreach (NameValueHeaderValue nameValueHeaderValue in values)
			{
				if (leadingSeparator || destination.Length > 0)
				{
					destination.Append(separator);
					destination.Append(' ');
				}
				destination.Append(nameValueHeaderValue.ToString());
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000CC38 File Offset: 0x0000AE38
		internal static string ToString(ICollection<NameValueHeaderValue> values, char separator, bool leadingSeparator)
		{
			if (values == null || values.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			NameValueHeaderValue.ToString(values, separator, leadingSeparator, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000CC68 File Offset: 0x0000AE68
		internal static int GetHashCode(ICollection<NameValueHeaderValue> values)
		{
			if (values == null || values.Count == 0)
			{
				return 0;
			}
			int num = 0;
			foreach (NameValueHeaderValue nameValueHeaderValue in values)
			{
				num ^= nameValueHeaderValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		internal static int GetNameValueLength(string input, int startIndex, out NameValueHeaderValue parsedValue)
		{
			return NameValueHeaderValue.GetNameValueLength(input, startIndex, NameValueHeaderValue.defaultNameValueCreator, out parsedValue);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		internal static int GetNameValueLength(string input, int startIndex, Func<NameValueHeaderValue> nameValueCreator, out NameValueHeaderValue parsedValue)
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
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length || input[num] != '=')
			{
				parsedValue = nameValueCreator();
				parsedValue.name = text;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
				return num - startIndex;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			int valueLength = NameValueHeaderValue.GetValueLength(input, num);
			if (valueLength == 0)
			{
				return 0;
			}
			parsedValue = nameValueCreator();
			parsedValue.name = text;
			parsedValue.value = input.Substring(num, valueLength);
			num += valueLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			return num - startIndex;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000CD98 File Offset: 0x0000AF98
		internal static int GetNameValueListLength(string input, int startIndex, char delimiter, ICollection<NameValueHeaderValue> nameValueCollection)
		{
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			int num = startIndex + HttpRuleParser.GetWhitespaceLength(input, startIndex);
			for (;;)
			{
				NameValueHeaderValue item = null;
				int nameValueLength = NameValueHeaderValue.GetNameValueLength(input, num, NameValueHeaderValue.defaultNameValueCreator, out item);
				if (nameValueLength == 0)
				{
					break;
				}
				nameValueCollection.Add(item);
				num += nameValueLength;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
				if (num == input.Length || input[num] != delimiter)
				{
					goto IL_5B;
				}
				num++;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
			}
			return 0;
			IL_5B:
			return num - startIndex;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CE14 File Offset: 0x0000B014
		internal static NameValueHeaderValue Find(ICollection<NameValueHeaderValue> values, string name)
		{
			if (values == null || values.Count == 0)
			{
				return null;
			}
			foreach (NameValueHeaderValue nameValueHeaderValue in values)
			{
				if (string.Compare(nameValueHeaderValue.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return nameValueHeaderValue;
				}
			}
			return null;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CE78 File Offset: 0x0000B078
		internal static int GetValueLength(string input, int startIndex)
		{
			if (startIndex >= input.Length)
			{
				return 0;
			}
			int tokenLength = HttpRuleParser.GetTokenLength(input, startIndex);
			if (tokenLength == 0 && HttpRuleParser.GetQuotedStringLength(input, startIndex, out tokenLength) != HttpParseResult.Parsed)
			{
				return 0;
			}
			return tokenLength;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000CEA9 File Offset: 0x0000B0A9
		private static void CheckNameValueFormat(string name, string value)
		{
			HeaderUtilities.CheckValidToken(name, "name");
			NameValueHeaderValue.CheckValueFormat(value);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		private static void CheckValueFormat(string value)
		{
			if (!string.IsNullOrEmpty(value) && NameValueHeaderValue.GetValueLength(value, 0) != value.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					value
				}));
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		private static NameValueHeaderValue CreateNameValue()
		{
			return new NameValueHeaderValue();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000CEFB File Offset: 0x0000B0FB
		object ICloneable.Clone()
		{
			return new NameValueHeaderValue(this);
		}

		// Token: 0x04000162 RID: 354
		private static readonly Func<NameValueHeaderValue> defaultNameValueCreator = new Func<NameValueHeaderValue>(NameValueHeaderValue.CreateNameValue);

		// Token: 0x04000163 RID: 355
		private string name;

		// Token: 0x04000164 RID: 356
		private string value;
	}
}

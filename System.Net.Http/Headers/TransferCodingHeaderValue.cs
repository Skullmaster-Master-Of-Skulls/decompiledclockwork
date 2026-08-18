using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x02000047 RID: 71
	[__DynamicallyInvokable]
	public class TransferCodingHeaderValue : ICloneable
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000E795 File Offset: 0x0000C995
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000E79D File Offset: 0x0000C99D
		[__DynamicallyInvokable]
		public ICollection<NameValueHeaderValue> Parameters
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ObjectCollection<NameValueHeaderValue>();
				}
				return this.parameters;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		internal TransferCodingHeaderValue()
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		[__DynamicallyInvokable]
		protected TransferCodingHeaderValue(TransferCodingHeaderValue source)
		{
			this.value = source.value;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add((NameValueHeaderValue)((ICloneable)nameValueHeaderValue).Clone());
				}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E838 File Offset: 0x0000CA38
		[__DynamicallyInvokable]
		public TransferCodingHeaderValue(string value)
		{
			HeaderUtilities.CheckValidToken(value, "value");
			this.value = value;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E854 File Offset: 0x0000CA54
		[__DynamicallyInvokable]
		public static TransferCodingHeaderValue Parse(string input)
		{
			int num = 0;
			return (TransferCodingHeaderValue)TransferCodingHeaderParser.SingleValueParser.ParseValue(input, null, ref num);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E878 File Offset: 0x0000CA78
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out TransferCodingHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (TransferCodingHeaderParser.SingleValueParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (TransferCodingHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		internal static int GetTransferCodingLength(string input, int startIndex, Func<TransferCodingHeaderValue> transferCodingCreator, out TransferCodingHeaderValue parsedValue)
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
			TransferCodingHeaderValue transferCodingHeaderValue;
			if (num >= input.Length || input[num] != ';')
			{
				transferCodingHeaderValue = transferCodingCreator();
				transferCodingHeaderValue.value = text;
				parsedValue = transferCodingHeaderValue;
				return num - startIndex;
			}
			transferCodingHeaderValue = transferCodingCreator();
			transferCodingHeaderValue.value = text;
			num++;
			int nameValueListLength = NameValueHeaderValue.GetNameValueListLength(input, num, ';', transferCodingHeaderValue.Parameters);
			if (nameValueListLength == 0)
			{
				return 0;
			}
			parsedValue = transferCodingHeaderValue;
			return num + nameValueListLength - startIndex;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E94C File Offset: 0x0000CB4C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.value + NameValueHeaderValue.ToString(this.parameters, ';', true);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E968 File Offset: 0x0000CB68
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			TransferCodingHeaderValue transferCodingHeaderValue = obj as TransferCodingHeaderValue;
			return transferCodingHeaderValue != null && string.Compare(this.value, transferCodingHeaderValue.value, StringComparison.OrdinalIgnoreCase) == 0 && HeaderUtilities.AreEqualCollections<NameValueHeaderValue>(this.parameters, transferCodingHeaderValue.parameters);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.value.ToLowerInvariant().GetHashCode() ^ NameValueHeaderValue.GetHashCode(this.parameters);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000E9C6 File Offset: 0x0000CBC6
		object ICloneable.Clone()
		{
			return new TransferCodingHeaderValue(this);
		}

		// Token: 0x04000180 RID: 384
		private ICollection<NameValueHeaderValue> parameters;

		// Token: 0x04000181 RID: 385
		private string value;
	}
}

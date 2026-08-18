using System;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003E RID: 62
	internal class ProductInfoHeaderParser : HttpHeaderParser
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000D3D2 File Offset: 0x0000B5D2
		private ProductInfoHeaderParser(bool supportsMultipleValues) : base(supportsMultipleValues, " ")
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public override bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(value) || index == value.Length)
			{
				return false;
			}
			int num = index + HttpRuleParser.GetWhitespaceLength(value, index);
			if (num == value.Length)
			{
				return false;
			}
			ProductInfoHeaderValue productInfoHeaderValue = null;
			int productInfoLength = ProductInfoHeaderValue.GetProductInfoLength(value, num, out productInfoHeaderValue);
			if (productInfoLength == 0)
			{
				return false;
			}
			num += productInfoLength;
			if (num < value.Length)
			{
				char c = value[num - 1];
				if (c != ' ' && c != '\t')
				{
					return false;
				}
			}
			index = num;
			parsedValue = productInfoHeaderValue;
			return true;
		}

		// Token: 0x0400016B RID: 363
		private const string separator = " ";

		// Token: 0x0400016C RID: 364
		internal static readonly ProductInfoHeaderParser SingleValueParser = new ProductInfoHeaderParser(false);

		// Token: 0x0400016D RID: 365
		internal static readonly ProductInfoHeaderParser MultipleValueParser = new ProductInfoHeaderParser(true);
	}
}

using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003F RID: 63
	[__DynamicallyInvokable]
	public class ProductInfoHeaderValue : ICloneable
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000D470 File Offset: 0x0000B670
		[__DynamicallyInvokable]
		public ProductHeaderValue Product
		{
			[__DynamicallyInvokable]
			get
			{
				return this.product;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000D478 File Offset: 0x0000B678
		[__DynamicallyInvokable]
		public string Comment
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comment;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D480 File Offset: 0x0000B680
		[__DynamicallyInvokable]
		public ProductInfoHeaderValue(string productName, string productVersion) : this(new ProductHeaderValue(productName, productVersion))
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D48F File Offset: 0x0000B68F
		[__DynamicallyInvokable]
		public ProductInfoHeaderValue(ProductHeaderValue product)
		{
			if (product == null)
			{
				throw new ArgumentNullException("product");
			}
			this.product = product;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D4AC File Offset: 0x0000B6AC
		[__DynamicallyInvokable]
		public ProductInfoHeaderValue(string comment)
		{
			HeaderUtilities.CheckValidComment(comment, "comment");
			this.comment = comment;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D4C6 File Offset: 0x0000B6C6
		private ProductInfoHeaderValue(ProductInfoHeaderValue source)
		{
			this.product = source.product;
			this.comment = source.comment;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D4E6 File Offset: 0x0000B6E6
		private ProductInfoHeaderValue()
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D4EE File Offset: 0x0000B6EE
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.product == null)
			{
				return this.comment;
			}
			return this.product.ToString();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D50C File Offset: 0x0000B70C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			ProductInfoHeaderValue productInfoHeaderValue = obj as ProductInfoHeaderValue;
			if (productInfoHeaderValue == null)
			{
				return false;
			}
			if (this.product == null)
			{
				return string.CompareOrdinal(this.comment, productInfoHeaderValue.comment) == 0;
			}
			return this.product.Equals(productInfoHeaderValue.product);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D553 File Offset: 0x0000B753
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.product == null)
			{
				return this.comment.GetHashCode();
			}
			return this.product.GetHashCode();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D574 File Offset: 0x0000B774
		[__DynamicallyInvokable]
		public static ProductInfoHeaderValue Parse(string input)
		{
			int num = 0;
			object obj = ProductInfoHeaderParser.SingleValueParser.ParseValue(input, null, ref num);
			if (num < input.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					input.Substring(num)
				}));
			}
			return (ProductInfoHeaderValue)obj;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out ProductInfoHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (!ProductInfoHeaderParser.SingleValueParser.TryParseValue(input, null, ref num, out obj))
			{
				return false;
			}
			if (num < input.Length)
			{
				return false;
			}
			parsedValue = (ProductInfoHeaderValue)obj;
			return true;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D604 File Offset: 0x0000B804
		internal static int GetProductInfoLength(string input, int startIndex, out ProductInfoHeaderValue parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			string text = null;
			ProductHeaderValue productHeaderValue = null;
			int num2;
			if (input[startIndex] == '(')
			{
				int num = 0;
				if (HttpRuleParser.GetCommentLength(input, startIndex, out num) != HttpParseResult.Parsed)
				{
					return 0;
				}
				text = input.Substring(startIndex, num);
				num2 = startIndex + num;
				num2 += HttpRuleParser.GetWhitespaceLength(input, num2);
			}
			else
			{
				int productLength = ProductHeaderValue.GetProductLength(input, startIndex, out productHeaderValue);
				if (productLength == 0)
				{
					return 0;
				}
				num2 = startIndex + productLength;
			}
			parsedValue = new ProductInfoHeaderValue();
			parsedValue.product = productHeaderValue;
			parsedValue.comment = text;
			return num2 - startIndex;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D690 File Offset: 0x0000B890
		object ICloneable.Clone()
		{
			return new ProductInfoHeaderValue(this);
		}

		// Token: 0x0400016E RID: 366
		private ProductHeaderValue product;

		// Token: 0x0400016F RID: 367
		private string comment;
	}
}

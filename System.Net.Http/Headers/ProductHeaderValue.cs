using System;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003D RID: 61
	[__DynamicallyInvokable]
	public class ProductHeaderValue : ICloneable
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000D1A3 File Offset: 0x0000B3A3
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000D1AB File Offset: 0x0000B3AB
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.version;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D1B3 File Offset: 0x0000B3B3
		[__DynamicallyInvokable]
		public ProductHeaderValue(string name) : this(name, null)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000D1BD File Offset: 0x0000B3BD
		[__DynamicallyInvokable]
		public ProductHeaderValue(string name, string version)
		{
			HeaderUtilities.CheckValidToken(name, "name");
			if (!string.IsNullOrEmpty(version))
			{
				HeaderUtilities.CheckValidToken(version, "version");
				this.version = version;
			}
			this.name = name;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000D1F1 File Offset: 0x0000B3F1
		private ProductHeaderValue(ProductHeaderValue source)
		{
			this.name = source.name;
			this.version = source.version;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000D211 File Offset: 0x0000B411
		private ProductHeaderValue()
		{
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000D219 File Offset: 0x0000B419
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.version))
			{
				return this.name;
			}
			return this.name + "/" + this.version;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000D248 File Offset: 0x0000B448
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			ProductHeaderValue productHeaderValue = obj as ProductHeaderValue;
			return productHeaderValue != null && string.Compare(this.name, productHeaderValue.name, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.version, productHeaderValue.version, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000D28C File Offset: 0x0000B48C
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.name.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.version))
			{
				num ^= this.version.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		[__DynamicallyInvokable]
		public static ProductHeaderValue Parse(string input)
		{
			int num = 0;
			return (ProductHeaderValue)GenericHeaderParser.SingleValueProductParser.ParseValue(input, null, ref num);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out ProductHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueProductParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (ProductHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000D320 File Offset: 0x0000B520
		internal static int GetProductLength(string input, int startIndex, out ProductHeaderValue parsedValue)
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
			ProductHeaderValue productHeaderValue = new ProductHeaderValue();
			productHeaderValue.name = input.Substring(startIndex, tokenLength);
			int num = startIndex + tokenLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num == input.Length || input[num] != '/')
			{
				parsedValue = productHeaderValue;
				return num - startIndex;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			int tokenLength2 = HttpRuleParser.GetTokenLength(input, num);
			if (tokenLength2 == 0)
			{
				return 0;
			}
			productHeaderValue.version = input.Substring(num, tokenLength2);
			num += tokenLength2;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			parsedValue = productHeaderValue;
			return num - startIndex;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000D3CA File Offset: 0x0000B5CA
		object ICloneable.Clone()
		{
			return new ProductHeaderValue(this);
		}

		// Token: 0x04000169 RID: 361
		private string name;

		// Token: 0x0400016A RID: 362
		private string version;
	}
}

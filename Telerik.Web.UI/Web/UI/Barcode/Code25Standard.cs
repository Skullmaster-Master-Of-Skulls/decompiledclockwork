using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D0 RID: 2512
	internal class Code25Standard : Code25
	{
		// Token: 0x06006045 RID: 24645 RVA: 0x00127AF0 File Offset: 0x00125CF0
		public Code25Standard()
		{
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('0', "10101110111010");
			this.encoding.Add('1', "11101010101110");
			this.encoding.Add('2', "10111010101110");
			this.encoding.Add('3', "11101110101010");
			this.encoding.Add('4', "10101110101110");
			this.encoding.Add('5', "11101011101010");
			this.encoding.Add('6', "10111011101010");
			this.encoding.Add('7', "10101011101110");
			this.encoding.Add('8', "11101010111010");
			this.encoding.Add('9', "10111010111010");
			this.encoding.Add('[', "11011010");
			this.encoding.Add(']', "1101011");
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x00127BFC File Offset: 0x00125DFC
		internal override string GetEncoding(string value)
		{
			value = base.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			base.CheckSum = base.GetChecksum(value).ToString();
			if (base.CalculateCheckSum)
			{
				value += base.CheckSum;
			}
			if (!value.StartsWith(this.prefix))
			{
				value = this.prefix + value;
			}
			if (!value.EndsWith(this.suffix))
			{
				value += this.suffix;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				stringBuilder.Append(this.encoding[value[i]]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001754 RID: 5972
		private string prefix = "[";

		// Token: 0x04001755 RID: 5973
		private string suffix = "]";

		// Token: 0x04001756 RID: 5974
		private Dictionary<char, string> encoding;
	}
}

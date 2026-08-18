using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009C9 RID: 2505
	internal class Code11 : Symbology1D
	{
		// Token: 0x0600601F RID: 24607 RVA: 0x00124DCC File Offset: 0x00122FCC
		public Code11()
		{
			this.charset = new List<char>();
			this.charset.Add('0');
			this.charset.Add('1');
			this.charset.Add('2');
			this.charset.Add('3');
			this.charset.Add('4');
			this.charset.Add('5');
			this.charset.Add('6');
			this.charset.Add('7');
			this.charset.Add('8');
			this.charset.Add('9');
			this.charset.Add('-');
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('0', "101011");
			this.encoding.Add('1', "1101011");
			this.encoding.Add('2', "1001011");
			this.encoding.Add('3', "1100101");
			this.encoding.Add('4', "1011011");
			this.encoding.Add('5', "1101101");
			this.encoding.Add('6', "1001101");
			this.encoding.Add('7', "1010011");
			this.encoding.Add('8', "1101001");
			this.encoding.Add('9', "110101");
			this.encoding.Add('-', "101101");
			this.encoding.Add('*', "1011001");
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x00124F5C File Offset: 0x0012315C
		public string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (this.charset.Contains(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x00124FA4 File Offset: 0x001231A4
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			base.CheckSum = this.GetChecksum(value);
			if (base.CalculateCheckSum)
			{
				value += base.CheckSum;
			}
			if (!value.StartsWith(Code11.Prefix))
			{
				value = Code11.Prefix + value;
			}
			if (!value.EndsWith(Code11.Suffix))
			{
				value += Code11.Suffix;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(Symbology1D.GapChar);
				}
				stringBuilder.Append(this.encoding[value[i]]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x0012506C File Offset: 0x0012326C
		internal string GetChecksum(string value)
		{
			int length = value.Length;
			value += this.GetChecksum(value, 10, 11);
			if (length >= 10)
			{
				value += this.GetChecksum(value, 9, 11);
			}
			return value.Substring(length);
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x001250BC File Offset: 0x001232BC
		private char GetChecksum(string value, int length, int modulo)
		{
			int num = 0;
			int num2 = 1;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				int num3 = this.charset.IndexOf(value[i]);
				num += num3 * num2++;
				if (num2 > length)
				{
					num2 = 1;
				}
			}
			num %= modulo;
			return this.charset[num];
		}

		// Token: 0x0400173F RID: 5951
		public static readonly string Prefix = "*";

		// Token: 0x04001740 RID: 5952
		public static readonly string Suffix = "*";

		// Token: 0x04001741 RID: 5953
		private List<char> charset;

		// Token: 0x04001742 RID: 5954
		private Dictionary<char, string> encoding;
	}
}

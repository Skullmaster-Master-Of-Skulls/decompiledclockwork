using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009DC RID: 2524
	internal class UPCSupplement5 : Symbology1D
	{
		// Token: 0x06006099 RID: 24729 RVA: 0x0012C054 File Offset: 0x0012A254
		public UPCSupplement5()
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
			this.parity = new Dictionary<char, string>();
			this.parity.Add('0', "00111");
			this.parity.Add('1', "01011");
			this.parity.Add('2', "01101");
			this.parity.Add('3', "01110");
			this.parity.Add('4', "10011");
			this.parity.Add('5', "11001");
			this.parity.Add('6', "11100");
			this.parity.Add('7', "10101");
			this.parity.Add('8', "10110");
			this.parity.Add('9', "11010");
			this.encoding = new Dictionary<string, string>();
			this.encoding.Add(string.Empty, "1011");
			this.encoding.Add("00", "0100111");
			this.encoding.Add("01", "0110011");
			this.encoding.Add("02", "0011011");
			this.encoding.Add("03", "0100001");
			this.encoding.Add("04", "0011101");
			this.encoding.Add("05", "0111001");
			this.encoding.Add("06", "0000101");
			this.encoding.Add("07", "0010001");
			this.encoding.Add("08", "0001001");
			this.encoding.Add("09", "0010111");
			this.encoding.Add("10", "0001101");
			this.encoding.Add("11", "0011001");
			this.encoding.Add("12", "0010011");
			this.encoding.Add("13", "0111101");
			this.encoding.Add("14", "0100011");
			this.encoding.Add("15", "0110001");
			this.encoding.Add("16", "0101111");
			this.encoding.Add("17", "0111011");
			this.encoding.Add("18", "0110111");
			this.encoding.Add("19", "0001011");
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x0012C380 File Offset: 0x0012A580
		public string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder.Length > 5)
			{
				stringBuilder = new StringBuilder(stringBuilder.ToString().Substring(0, 5));
			}
			else if (stringBuilder.Length < 5)
			{
				stringBuilder = new StringBuilder(this.GetSymbols(stringBuilder.ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x0012C3FC File Offset: 0x0012A5FC
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			char key = base.CalculateCheckSum ? this.GetChecksum(value) : value[value.Length - 1];
			string text = this.parity[key];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.encoding[string.Empty]);
			for (int i = 0; i < value.Length; i++)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(Symbology1D.GapChar);
					stringBuilder.Append(Symbology1D.BarChar);
				}
				string key2 = text[i].ToString() + value[i].ToString();
				stringBuilder.Append(this.encoding[key2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x0012C4DD File Offset: 0x0012A6DD
		public char GetChecksum(string value)
		{
			return this.GetChecksum(value, 3, 9, 10);
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x0012C4EB File Offset: 0x0012A6EB
		protected string GetSymbols(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.PadLeft(5, this.padding);
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x0012C508 File Offset: 0x0012A708
		private char GetChecksum(string value, int first, int second, int modulo)
		{
			int num = 0;
			int num2 = first;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				int num3 = this.charset.IndexOf(value[i]);
				num += num3 * num2;
				if (num2 == first)
				{
					num2 = second;
				}
				else
				{
					num2 = first;
				}
			}
			num %= modulo;
			return this.charset[num];
		}

		// Token: 0x04001782 RID: 6018
		private char padding = '0';

		// Token: 0x04001783 RID: 6019
		private List<char> charset;

		// Token: 0x04001784 RID: 6020
		private Dictionary<char, string> parity;

		// Token: 0x04001785 RID: 6021
		private Dictionary<string, string> encoding;
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D3 RID: 2515
	internal class Code93 : Symbology1D
	{
		// Token: 0x06006051 RID: 24657 RVA: 0x00128DE8 File Offset: 0x00126FE8
		public Code93()
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
			this.charset.Add('A');
			this.charset.Add('B');
			this.charset.Add('C');
			this.charset.Add('D');
			this.charset.Add('E');
			this.charset.Add('F');
			this.charset.Add('G');
			this.charset.Add('H');
			this.charset.Add('I');
			this.charset.Add('J');
			this.charset.Add('K');
			this.charset.Add('L');
			this.charset.Add('M');
			this.charset.Add('N');
			this.charset.Add('O');
			this.charset.Add('P');
			this.charset.Add('Q');
			this.charset.Add('R');
			this.charset.Add('S');
			this.charset.Add('T');
			this.charset.Add('U');
			this.charset.Add('V');
			this.charset.Add('W');
			this.charset.Add('X');
			this.charset.Add('Y');
			this.charset.Add('Z');
			this.charset.Add('-');
			this.charset.Add('.');
			this.charset.Add(' ');
			this.charset.Add('$');
			this.charset.Add('/');
			this.charset.Add('+');
			this.charset.Add('%');
			this.charset.Add('@');
			this.charset.Add('#');
			this.charset.Add('&');
			this.charset.Add('~');
			this.charset.Add('*');
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('0', "100010100");
			this.encoding.Add('1', "101001000");
			this.encoding.Add('2', "101000100");
			this.encoding.Add('3', "101000010");
			this.encoding.Add('4', "100101000");
			this.encoding.Add('5', "100100100");
			this.encoding.Add('6', "100100010");
			this.encoding.Add('7', "101010000");
			this.encoding.Add('8', "100010010");
			this.encoding.Add('9', "100001010");
			this.encoding.Add('A', "110101000");
			this.encoding.Add('B', "110100100");
			this.encoding.Add('C', "110100010");
			this.encoding.Add('D', "110010100");
			this.encoding.Add('E', "110010010");
			this.encoding.Add('F', "110001010");
			this.encoding.Add('G', "101101000");
			this.encoding.Add('H', "101100100");
			this.encoding.Add('I', "101100010");
			this.encoding.Add('J', "100110100");
			this.encoding.Add('K', "100011010");
			this.encoding.Add('L', "101011000");
			this.encoding.Add('M', "101001100");
			this.encoding.Add('N', "101000110");
			this.encoding.Add('O', "100101100");
			this.encoding.Add('P', "100010110");
			this.encoding.Add('Q', "110110100");
			this.encoding.Add('R', "110110010");
			this.encoding.Add('S', "110101100");
			this.encoding.Add('T', "110100110");
			this.encoding.Add('U', "110010110");
			this.encoding.Add('V', "110011010");
			this.encoding.Add('W', "101101100");
			this.encoding.Add('X', "101100110");
			this.encoding.Add('Y', "100110110");
			this.encoding.Add('Z', "100111010");
			this.encoding.Add('-', "100101110");
			this.encoding.Add('.', "111010100");
			this.encoding.Add(' ', "111010010");
			this.encoding.Add('$', "111001010");
			this.encoding.Add('/', "101101110");
			this.encoding.Add('+', "101110110");
			this.encoding.Add('%', "110101110");
			this.encoding.Add('@', "100100110");
			this.encoding.Add('#', "111011010");
			this.encoding.Add('&', "111010110");
			this.encoding.Add('~', "100110010");
			this.encoding.Add('*', "101011110");
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001293F8 File Offset: 0x001275F8
		public virtual string ValidateValue(string value)
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

		// Token: 0x06006053 RID: 24659 RVA: 0x00129440 File Offset: 0x00127640
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
			stringBuilder.Append(Symbology1D.BarChar);
			return stringBuilder.ToString();
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x00129504 File Offset: 0x00127704
		public string GetChecksum(string value)
		{
			int length = value.Length;
			value += this.GetChecksum(value, 20, 47);
			value += this.GetChecksum(value, 15, 47);
			return value.Substring(length);
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x00129550 File Offset: 0x00127750
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

		// Token: 0x0400175C RID: 5980
		private string prefix = "*";

		// Token: 0x0400175D RID: 5981
		private string suffix = "*";

		// Token: 0x0400175E RID: 5982
		private List<char> charset;

		// Token: 0x0400175F RID: 5983
		private Dictionary<char, string> encoding;
	}
}

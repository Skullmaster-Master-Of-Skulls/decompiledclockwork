using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D1 RID: 2513
	internal class Code39 : Symbology1D
	{
		// Token: 0x06006047 RID: 24647 RVA: 0x00127CBC File Offset: 0x00125EBC
		[Description("Initializes a new instance of Code39 type.")]
		public Code39()
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
			this.charset.Add('*');
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('0', "101001101101");
			this.encoding.Add('1', "110100101011");
			this.encoding.Add('2', "101100101011");
			this.encoding.Add('3', "110110010101");
			this.encoding.Add('4', "101001101011");
			this.encoding.Add('5', "110100110101");
			this.encoding.Add('6', "101100110101");
			this.encoding.Add('7', "101001011011");
			this.encoding.Add('8', "110100101101");
			this.encoding.Add('9', "101100101101");
			this.encoding.Add('A', "110101001011");
			this.encoding.Add('B', "101101001011");
			this.encoding.Add('C', "110110100101");
			this.encoding.Add('D', "101011001011");
			this.encoding.Add('E', "110101100101");
			this.encoding.Add('F', "101101100101");
			this.encoding.Add('G', "101010011011");
			this.encoding.Add('H', "110101001101");
			this.encoding.Add('I', "101101001101");
			this.encoding.Add('J', "101011001101");
			this.encoding.Add('K', "110101010011");
			this.encoding.Add('L', "101101010011");
			this.encoding.Add('M', "110110101001");
			this.encoding.Add('N', "101011010011");
			this.encoding.Add('O', "110101101001");
			this.encoding.Add('P', "101101101001");
			this.encoding.Add('Q', "101010110011");
			this.encoding.Add('R', "110101011001");
			this.encoding.Add('S', "101101011001");
			this.encoding.Add('T', "101011011001");
			this.encoding.Add('U', "110010101011");
			this.encoding.Add('V', "100110101011");
			this.encoding.Add('W', "110011010101");
			this.encoding.Add('X', "100101101011");
			this.encoding.Add('Y', "110010110101");
			this.encoding.Add('Z', "100110110101");
			this.encoding.Add('-', "100101011011");
			this.encoding.Add('.', "110010101101");
			this.encoding.Add(' ', "100110101101");
			this.encoding.Add('$', "100100100101");
			this.encoding.Add('/', "100100101001");
			this.encoding.Add('+', "100101001001");
			this.encoding.Add('%', "101001001001");
			this.encoding.Add('*', "100101101101");
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x0012823C File Offset: 0x0012643C
		public override List<RectangleF> GenerateGeometry(string barCodeEncodedText)
		{
			List<RectangleF> list = new List<RectangleF>();
			int num = barCodeEncodedText.Length / 12;
			float num2 = 1f / (float.Parse(barCodeEncodedText.Length.ToString()) + (float)num);
			int num3 = 0;
			for (int i = 0; i < barCodeEncodedText.Length; i++)
			{
				if (barCodeEncodedText[i] == '1')
				{
					num3++;
				}
				if (num3 > 0 && (i == barCodeEncodedText.Length - 1 || barCodeEncodedText[i + 1] != '1' || (i + 1) % 12 == 0))
				{
					list.Add(new RectangleF((float)(i + 1 + i / 12) * num2 - (float)num3 * num2, 0f, num2 * (float)num3, 1f));
					num3 = 0;
				}
			}
			return list;
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x001282F4 File Offset: 0x001264F4
		internal override string GetEncoding(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			base.CheckSum = this.GetChecksum(value).ToString();
			if (base.CalculateCheckSum)
			{
				value += base.CheckSum;
			}
			if (!value.StartsWith(Code39.Prefix))
			{
				value = Code39.Prefix + value;
			}
			if (!value.EndsWith(Code39.Suffix))
			{
				value += Code39.Suffix;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (this.encoding.ContainsKey(value[i]))
				{
					stringBuilder.Append(this.encoding[value[i]]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x001283B8 File Offset: 0x001265B8
		internal char GetChecksum(string value)
		{
			return this.GetChecksum(value, 43);
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x001283C4 File Offset: 0x001265C4
		internal char GetChecksum(string value, int module)
		{
			int num = 0;
			for (int i = 0; i < value.Length; i++)
			{
				num += this.charset.IndexOf(value[i]);
			}
			num %= module;
			return this.charset[num];
		}

		// Token: 0x04001757 RID: 5975
		public static readonly string Prefix = "*";

		// Token: 0x04001758 RID: 5976
		public static readonly string Suffix = "*";

		// Token: 0x04001759 RID: 5977
		private List<char> charset;

		// Token: 0x0400175A RID: 5978
		private Dictionary<char, string> encoding;
	}
}

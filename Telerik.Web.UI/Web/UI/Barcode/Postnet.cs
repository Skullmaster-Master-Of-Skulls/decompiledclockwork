using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D9 RID: 2521
	internal class Postnet : Symbology1D
	{
		// Token: 0x06006085 RID: 24709 RVA: 0x0012AE58 File Offset: 0x00129058
		[Description("Initializes a new instance of Postnet type.")]
		public Postnet()
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
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('0', "11000");
			this.encoding.Add('1', "00011");
			this.encoding.Add('2', "00101");
			this.encoding.Add('3', "00110");
			this.encoding.Add('4', "01001");
			this.encoding.Add('5', "01010");
			this.encoding.Add('6', "01100");
			this.encoding.Add('7', "10001");
			this.encoding.Add('8', "10010");
			this.encoding.Add('9', "10100");
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x0012AFB8 File Offset: 0x001291B8
		public override List<RectangleF> GenerateGeometry(string barCodeEncodedText)
		{
			List<RectangleF> list = new List<RectangleF>();
			float num = 1f / (float.Parse(barCodeEncodedText.Length.ToString()) * 2f - 1f);
			float num2 = 0f;
			for (int i = 0; i < barCodeEncodedText.Length; i++)
			{
				if (barCodeEncodedText[i] == '1')
				{
					list.Add(new RectangleF(num2, 0f, num, 1f));
				}
				else
				{
					list.Add(new RectangleF(num2, 0.5f, num, 0.5f));
				}
				num2 += num * 2f;
			}
			return list;
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x0012B050 File Offset: 0x00129250
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
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("1");
			for (int i = 0; i < value.Length; i++)
			{
				stringBuilder.Append(this.encoding[value[i]]);
			}
			stringBuilder.Append("1");
			return stringBuilder.ToString();
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x0012B0E8 File Offset: 0x001292E8
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
			return stringBuilder.ToString();
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x0012B12A File Offset: 0x0012932A
		protected string GetChecksum(string value)
		{
			return this.GetChecksum(value, 10);
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x0012B138 File Offset: 0x00129338
		protected string GetChecksum(string value, int modulo)
		{
			int num = 0;
			for (int i = 0; i < value.Length; i++)
			{
				num += this.charset.IndexOf(value[i]);
			}
			num %= modulo;
			if (num != 0)
			{
				num = modulo - num;
			}
			return this.charset[num].ToString();
		}

		// Token: 0x04001779 RID: 6009
		private List<char> charset;

		// Token: 0x0400177A RID: 6010
		private Dictionary<char, string> encoding;
	}
}

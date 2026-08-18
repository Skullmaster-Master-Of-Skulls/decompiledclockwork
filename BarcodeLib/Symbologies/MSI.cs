using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000019 RID: 25
	internal class MSI : BarcodeCommon, IBarcode
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public MSI(string input, TYPE EncodedType)
		{
			this.Encoded_Type = EncodedType;
			this.Raw_Data = input;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000D760 File Offset: 0x0000B960
		private string Encode_MSI()
		{
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EMSI-1: Numeric Data Only");
			}
			string text = this.Raw_Data;
			if (this.Encoded_Type == TYPE.MSI_Mod10 || this.Encoded_Type == TYPE.MSI_2Mod10)
			{
				string text2 = "";
				string text3 = "";
				for (int i = text.Length - 1; i >= 0; i -= 2)
				{
					text2 = text[i].ToString() + text2;
					if (i - 1 >= 0)
					{
						text3 = text[i - 1].ToString() + text3;
					}
				}
				text2 = Convert.ToString(int.Parse(text2) * 2);
				int num = 0;
				int num2 = 0;
				string text4 = text3;
				for (int j = 0; j < text4.Length; j++)
				{
					num += int.Parse(text4[j].ToString());
				}
				text4 = text2;
				for (int j = 0; j < text4.Length; j++)
				{
					num2 += int.Parse(text4[j].ToString());
				}
				text += (10 - (num2 + num) % 10).ToString();
			}
			if (this.Encoded_Type == TYPE.MSI_Mod11 || this.Encoded_Type == TYPE.MSI_Mod11_Mod10)
			{
				int num3 = 0;
				int num4 = 2;
				for (int k = text.Length - 1; k >= 0; k--)
				{
					if (num4 > 7)
					{
						num4 = 2;
					}
					num3 += int.Parse(text[k].ToString()) * num4++;
				}
				text += (11 - num3 % 11).ToString();
			}
			if (this.Encoded_Type == TYPE.MSI_2Mod10 || this.Encoded_Type == TYPE.MSI_Mod11_Mod10)
			{
				string text5 = "";
				string text6 = "";
				for (int l = text.Length - 1; l >= 0; l -= 2)
				{
					text5 = text[l].ToString() + text5;
					if (l - 1 >= 0)
					{
						text6 = text[l - 1].ToString() + text6;
					}
				}
				text5 = Convert.ToString(int.Parse(text5) * 2);
				int num5 = 0;
				int num6 = 0;
				string text4 = text6;
				for (int j = 0; j < text4.Length; j++)
				{
					num5 += int.Parse(text4[j].ToString());
				}
				text4 = text5;
				for (int j = 0; j < text4.Length; j++)
				{
					num6 += int.Parse(text4[j].ToString());
				}
				text += (10 - (num6 + num5) % 10).ToString();
			}
			string str = "110";
			foreach (char c in text)
			{
				str += this.MSI_Code[int.Parse(c.ToString())];
			}
			return str + "1001";
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000DA73 File Offset: 0x0000BC73
		public string Encoded_Value
		{
			get
			{
				return this.Encode_MSI();
			}
		}

		// Token: 0x04000071 RID: 113
		private string[] MSI_Code = new string[]
		{
			"100100100100",
			"100100100110",
			"100100110100",
			"100100110110",
			"100110100100",
			"100110100110",
			"100110110100",
			"100110110110",
			"110100100100",
			"110100100110"
		};

		// Token: 0x04000072 RID: 114
		private TYPE Encoded_Type;
	}
}

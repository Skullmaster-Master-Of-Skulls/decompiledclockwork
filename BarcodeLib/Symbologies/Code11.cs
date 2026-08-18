using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200000C RID: 12
	internal class Code11 : BarcodeCommon, IBarcode
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000044EC File Offset: 0x000026EC
		public Code11(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004578 File Offset: 0x00002778
		private string Encode_Code11()
		{
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data.Replace("-", "")))
			{
				base.Error("EC11-1: Numeric data and '-' Only");
			}
			int num = 1;
			int num2 = 0;
			string text = this.Raw_Data;
			for (int i = this.Raw_Data.Length - 1; i >= 0; i--)
			{
				if (num == 10)
				{
					num = 1;
				}
				if (this.Raw_Data[i] != '-')
				{
					num2 += int.Parse(this.Raw_Data[i].ToString()) * num++;
				}
				else
				{
					num2 += 10 * num++;
				}
			}
			text += (num2 % 11).ToString();
			if (this.Raw_Data.Length >= 10)
			{
				num = 1;
				int num3 = 0;
				for (int j = text.Length - 1; j >= 0; j--)
				{
					if (num == 9)
					{
						num = 1;
					}
					if (text[j] != '-')
					{
						num3 += int.Parse(text[j].ToString()) * num++;
					}
					else
					{
						num3 += 10 * num++;
					}
				}
				text += (num3 % 11).ToString();
			}
			string str = "0";
			string str2 = this.C11_Code[11] + str;
			foreach (char c in text)
			{
				int num4 = (c == '-') ? 10 : int.Parse(c.ToString());
				str2 += this.C11_Code[num4];
				str2 += str;
			}
			return str2 + this.C11_Code[11];
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004738 File Offset: 0x00002938
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Code11();
			}
		}

		// Token: 0x04000053 RID: 83
		private string[] C11_Code = new string[]
		{
			"101011",
			"1101011",
			"1001011",
			"1100101",
			"1011011",
			"1101101",
			"1001101",
			"1010011",
			"1101001",
			"110101",
			"101101",
			"1011001"
		};
	}
}

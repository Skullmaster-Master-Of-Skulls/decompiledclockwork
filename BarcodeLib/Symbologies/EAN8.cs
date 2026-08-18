using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000015 RID: 21
	internal class EAN8 : BarcodeCommon, IBarcode
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public EAN8(string input)
		{
			this.Raw_Data = input;
			this.CheckDigit();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		private string Encode_EAN8()
		{
			if (this.Raw_Data.Length != 8 && this.Raw_Data.Length != 7)
			{
				base.Error("EEAN8-1: Invalid data length. (7 or 8 numbers only)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EEAN8-2: Numeric only.");
			}
			string str = "101";
			for (int i = 0; i < this.Raw_Data.Length / 2; i++)
			{
				str += this.EAN_CodeA[int.Parse(this.Raw_Data[i].ToString())];
			}
			str += "01010";
			for (int j = this.Raw_Data.Length / 2; j < this.Raw_Data.Length; j++)
			{
				str += this.EAN_CodeC[int.Parse(this.Raw_Data[j].ToString())];
			}
			return str + "101";
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		private void CheckDigit()
		{
			if (this.Raw_Data.Length == 7)
			{
				int num = 0;
				int num2 = 0;
				for (int i = 0; i <= 6; i += 2)
				{
					num2 += int.Parse(this.Raw_Data.Substring(i, 1)) * 3;
				}
				for (int j = 1; j <= 5; j += 2)
				{
					num += int.Parse(this.Raw_Data.Substring(j, 1));
				}
				int num3 = (num + num2) % 10;
				num3 = 10 - num3;
				if (num3 == 10)
				{
					num3 = 0;
				}
				this.Raw_Data += num3.ToString();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000D357 File Offset: 0x0000B557
		public string Encoded_Value
		{
			get
			{
				return this.Encode_EAN8();
			}
		}

		// Token: 0x0400006E RID: 110
		private string[] EAN_CodeA = new string[]
		{
			"0001101",
			"0011001",
			"0010011",
			"0111101",
			"0100011",
			"0110001",
			"0101111",
			"0111011",
			"0110111",
			"0001011"
		};

		// Token: 0x0400006F RID: 111
		private string[] EAN_CodeC = new string[]
		{
			"1110010",
			"1100110",
			"1101100",
			"1000010",
			"1011100",
			"1001110",
			"1010000",
			"1000100",
			"1001000",
			"1110100"
		};
	}
}

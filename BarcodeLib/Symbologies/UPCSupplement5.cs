using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200001F RID: 31
	internal class UPCSupplement5 : BarcodeCommon, IBarcode
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x0000F254 File Offset: 0x0000D454
		public UPCSupplement5(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000F388 File Offset: 0x0000D588
		private string Encode_UPCSupplemental_5()
		{
			if (this.Raw_Data.Length != 5)
			{
				base.Error("EUPC-SUP5-1: Invalid data length. (Length = 5 required)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EUPCA-2: Numeric Data Only");
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i <= 4; i += 2)
			{
				num2 += int.Parse(this.Raw_Data.Substring(i, 1)) * 3;
			}
			for (int j = 1; j < 4; j += 2)
			{
				num += int.Parse(this.Raw_Data.Substring(j, 1)) * 9;
			}
			int num3 = (num + num2) % 10;
			string text = this.UPC_SUPP_5[num3];
			string text2 = "";
			int num4 = 0;
			foreach (char c in text)
			{
				if (num4 == 0)
				{
					text2 += "1011";
				}
				else
				{
					text2 += "01";
				}
				if (c == 'a')
				{
					text2 += this.EAN_CodeA[int.Parse(this.Raw_Data[num4].ToString())];
				}
				else if (c == 'b')
				{
					text2 += this.EAN_CodeB[int.Parse(this.Raw_Data[num4].ToString())];
				}
				num4++;
			}
			return text2;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000F4E1 File Offset: 0x0000D6E1
		public string Encoded_Value
		{
			get
			{
				return this.Encode_UPCSupplemental_5();
			}
		}

		// Token: 0x04000081 RID: 129
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

		// Token: 0x04000082 RID: 130
		private string[] EAN_CodeB = new string[]
		{
			"0100111",
			"0110011",
			"0011011",
			"0100001",
			"0011101",
			"0111001",
			"0000101",
			"0010001",
			"0001001",
			"0010111"
		};

		// Token: 0x04000083 RID: 131
		private string[] UPC_SUPP_5 = new string[]
		{
			"bbaaa",
			"babaa",
			"baaba",
			"baaab",
			"abbaa",
			"aabba",
			"aaabb",
			"ababa",
			"abaab",
			"aabab"
		};
	}
}

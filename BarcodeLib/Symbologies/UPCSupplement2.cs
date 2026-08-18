using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200001E RID: 30
	internal class UPCSupplement2 : BarcodeCommon, IBarcode
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x0000F028 File Offset: 0x0000D228
		public UPCSupplement2(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000F12C File Offset: 0x0000D32C
		private string Encode_UPCSupplemental_2()
		{
			if (this.Raw_Data.Length != 2)
			{
				base.Error("EUPC-SUP2-1: Invalid data length. (Length = 2 required)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EUPC-SUP2-2: Numeric Data Only");
			}
			string text = "";
			try
			{
				text = this.UPC_SUPP_2[int.Parse(this.Raw_Data.Trim()) % 4];
			}
			catch
			{
				base.Error("EUPC-SUP2-3: Invalid Data. (Numeric only)");
			}
			string text2 = "1011";
			int index = 0;
			foreach (char c in text)
			{
				if (c == 'a')
				{
					text2 += this.EAN_CodeA[int.Parse(this.Raw_Data[index].ToString())];
				}
				else if (c == 'b')
				{
					text2 += this.EAN_CodeB[int.Parse(this.Raw_Data[index].ToString())];
				}
				if (index++ == 0)
				{
					text2 += "01";
				}
			}
			return text2;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000F24C File Offset: 0x0000D44C
		public string Encoded_Value
		{
			get
			{
				return this.Encode_UPCSupplemental_2();
			}
		}

		// Token: 0x0400007E RID: 126
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

		// Token: 0x0400007F RID: 127
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

		// Token: 0x04000080 RID: 128
		private string[] UPC_SUPP_2 = new string[]
		{
			"aa",
			"ab",
			"ba",
			"bb"
		};
	}
}

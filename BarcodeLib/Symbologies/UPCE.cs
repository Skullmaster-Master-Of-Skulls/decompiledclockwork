using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200001D RID: 29
	internal class UPCE : BarcodeCommon, IBarcode
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0000EB18 File Offset: 0x0000CD18
		public UPCE(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000ED08 File Offset: 0x0000CF08
		private string Encode_UPCE()
		{
			if (this.Raw_Data.Length != 6 && this.Raw_Data.Length != 8 && this.Raw_Data.Length != 12)
			{
				base.Error("EUPCE-1: Invalid data length. (8 or 12 numbers only)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EUPCE-2: Numeric only.");
			}
			int num = int.Parse(this.Raw_Data[0].ToString());
			if (num != 0 && num != 1)
			{
				base.Error("EUPCE-3: Invalid Number System (only 0 & 1 are valid)");
			}
			int num2 = int.Parse(this.Raw_Data[this.Raw_Data.Length - 1].ToString());
			if (this.Raw_Data.Length == 12)
			{
				string text = "";
				string text2 = this.Raw_Data.Substring(1, 5);
				string text3 = this.Raw_Data.Substring(6, 5);
				if (text2.EndsWith("000") || text2.EndsWith("100") || (text2.EndsWith("200") && int.Parse(text3) <= 999))
				{
					text += text2.Substring(0, 2);
					text += text3.Substring(2, 3);
					text += text2[2].ToString();
				}
				else if (text2.EndsWith("00") && int.Parse(text3) <= 99)
				{
					text += text2.Substring(0, 3);
					text += text3.Substring(3, 2);
					text += "3";
				}
				else if (text2.EndsWith("0") && int.Parse(text3) <= 9)
				{
					text += text2.Substring(0, 4);
					text += text3[4].ToString();
					text += "4";
				}
				else if (!text2.EndsWith("0") && int.Parse(text3) <= 9 && int.Parse(text3) >= 5)
				{
					text += text2;
					text += text3[4].ToString();
				}
				else
				{
					base.Error("EUPCE-4: Illegal UPC-A entered for conversion.  Unable to convert.");
				}
				this.Raw_Data = text;
			}
			string text4;
			if (num == 0)
			{
				text4 = this.UPCE_Code_0[num2];
			}
			else
			{
				text4 = this.UPCE_Code_1[num2];
			}
			string str = "101";
			int num3 = 0;
			foreach (char c in text4)
			{
				int num4 = int.Parse(this.Raw_Data[num3++].ToString());
				if (c == 'a')
				{
					str += this.EAN_CodeA[num4];
				}
				else if (c == 'b')
				{
					str += this.EAN_CodeB[num4];
				}
			}
			str += "01010";
			return str + "1";
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000F01E File Offset: 0x0000D21E
		public string Encoded_Value
		{
			get
			{
				return this.Encode_UPCE();
			}
		}

		// Token: 0x04000079 RID: 121
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

		// Token: 0x0400007A RID: 122
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

		// Token: 0x0400007B RID: 123
		private string[] EAN_Pattern = new string[]
		{
			"aaaaaa",
			"aababb",
			"aabbab",
			"aabbba",
			"abaabb",
			"abbaab",
			"abbbaa",
			"ababab",
			"ababba",
			"abbaba"
		};

		// Token: 0x0400007C RID: 124
		private string[] UPCE_Code_0 = new string[]
		{
			"bbbaaa",
			"bbabaa",
			"bbaaba",
			"bbaaab",
			"babbaa",
			"baabba",
			"baaabb",
			"bababa",
			"babaab",
			"baabab"
		};

		// Token: 0x0400007D RID: 125
		private string[] UPCE_Code_1 = new string[]
		{
			"aaabbb",
			"aababb",
			"aabbab",
			"aabbba",
			"abaabb",
			"abbaab",
			"abbbaa",
			"ababab",
			"ababba",
			"abbaba"
		};
	}
}

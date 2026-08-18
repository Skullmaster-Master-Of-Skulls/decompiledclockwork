using System;
using System.Data;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200000F RID: 15
	internal class Code93 : BarcodeCommon, IBarcode
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public Code93(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007D08 File Offset: 0x00005F08
		private string Encode_Code93()
		{
			this.init_Code93();
			string text = this.Add_CheckDigits(this.Raw_Data);
			string text2 = this.C93_Code.Select("Character = '*'")[0]["Encoding"].ToString();
			foreach (char c in text)
			{
				try
				{
					text2 += this.C93_Code.Select("Character = '" + c.ToString() + "'")[0]["Encoding"].ToString();
				}
				catch
				{
					base.Error("EC93-1: Invalid data.");
				}
			}
			text2 += this.C93_Code.Select("Character = '*'")[0]["Encoding"].ToString();
			text2 += "1";
			this.C93_Code.Clear();
			return text2;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00007DFC File Offset: 0x00005FFC
		private void init_Code93()
		{
			this.C93_Code.Rows.Clear();
			this.C93_Code.Columns.Clear();
			this.C93_Code.Columns.Add("Value");
			this.C93_Code.Columns.Add("Character");
			this.C93_Code.Columns.Add("Encoding");
			this.C93_Code.Rows.Add(new object[]
			{
				"0",
				"0",
				"100010100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"1",
				"1",
				"101001000"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"2",
				"2",
				"101000100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"3",
				"3",
				"101000010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"4",
				"4",
				"100101000"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"5",
				"5",
				"100100100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"6",
				"6",
				"100100010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"7",
				"7",
				"101010000"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"8",
				"8",
				"100010010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"9",
				"9",
				"100001010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"10",
				"A",
				"110101000"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"11",
				"B",
				"110100100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"12",
				"C",
				"110100010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"13",
				"D",
				"110010100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"14",
				"E",
				"110010010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"15",
				"F",
				"110001010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"16",
				"G",
				"101101000"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"17",
				"H",
				"101100100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"18",
				"I",
				"101100010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"19",
				"J",
				"100110100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"20",
				"K",
				"100011010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"21",
				"L",
				"101011000"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"22",
				"M",
				"101001100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"23",
				"N",
				"101000110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"24",
				"O",
				"100101100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"25",
				"P",
				"100010110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"26",
				"Q",
				"110110100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"27",
				"R",
				"110110010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"28",
				"S",
				"110101100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"29",
				"T",
				"110100110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"30",
				"U",
				"110010110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"31",
				"V",
				"110011010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"32",
				"W",
				"101101100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"33",
				"X",
				"101100110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"34",
				"Y",
				"100110110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"35",
				"Z",
				"100111010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"36",
				"-",
				"100101110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"37",
				".",
				"111010100"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"38",
				" ",
				"111010010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"39",
				"$",
				"111001010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"40",
				"/",
				"101101110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"41",
				"+",
				"101110110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"42",
				"%",
				"110101110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"43",
				"(",
				"100100110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"44",
				")",
				"111011010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"45",
				"#",
				"111010110"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"46",
				"@",
				"100110010"
			});
			this.C93_Code.Rows.Add(new object[]
			{
				"-",
				"*",
				"101011110"
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000873C File Offset: 0x0000693C
		private string Add_CheckDigits(string input)
		{
			int[] array = new int[input.Length];
			int num = 1;
			for (int i = input.Length - 1; i >= 0; i--)
			{
				if (num > 20)
				{
					num = 1;
				}
				array[i] = num;
				num++;
			}
			int[] array2 = new int[input.Length + 1];
			num = 1;
			for (int j = input.Length; j >= 0; j--)
			{
				if (num > 15)
				{
					num = 1;
				}
				array2[j] = num;
				num++;
			}
			int num2 = 0;
			for (int k = 0; k < input.Length; k++)
			{
				num2 += array[k] * int.Parse(this.C93_Code.Select("Character = '" + input[k].ToString() + "'")[0]["Value"].ToString());
			}
			int num3 = num2 % 47;
			input += this.C93_Code.Select("Value = '" + num3.ToString() + "'")[0]["Character"].ToString();
			num2 = 0;
			for (int l = 0; l < input.Length; l++)
			{
				num2 += array2[l] * int.Parse(this.C93_Code.Select("Character = '" + input[l].ToString() + "'")[0]["Value"].ToString());
			}
			num3 = num2 % 47;
			input += this.C93_Code.Select("Value = '" + num3.ToString() + "'")[0]["Character"].ToString();
			return input;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000088F4 File Offset: 0x00006AF4
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Code93();
			}
		}

		// Token: 0x0400005D RID: 93
		private DataTable C93_Code = new DataTable("C93_Code");
	}
}

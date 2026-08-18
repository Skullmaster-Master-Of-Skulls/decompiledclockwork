using System;
using System.Collections.Generic;
using System.Data;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200000D RID: 13
	internal class Code128 : BarcodeCommon, IBarcode
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004740 File Offset: 0x00002940
		public Code128(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004775 File Offset: 0x00002975
		public Code128(string input, Code128.TYPES type)
		{
			this.type = type;
			this.Raw_Data = input;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000047B1 File Offset: 0x000029B1
		private string Encode_Code128()
		{
			this.init_Code128();
			return this.GetEncoding();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000047C0 File Offset: 0x000029C0
		private void init_Code128()
		{
			this.C128_Code.CaseSensitive = true;
			this.C128_Code.Columns.Add("Value", typeof(string));
			this.C128_Code.Columns.Add("A", typeof(string));
			this.C128_Code.Columns.Add("B", typeof(string));
			this.C128_Code.Columns.Add("C", typeof(string));
			this.C128_Code.Columns.Add("Encoding", typeof(string));
			this.C128_Code.Rows.Add(new object[]
			{
				"0",
				" ",
				" ",
				"00",
				"11011001100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"1",
				"!",
				"!",
				"01",
				"11001101100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"2",
				"\"",
				"\"",
				"02",
				"11001100110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"3",
				"#",
				"#",
				"03",
				"10010011000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"4",
				"$",
				"$",
				"04",
				"10010001100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"5",
				"%",
				"%",
				"05",
				"10001001100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"6",
				"&",
				"&",
				"06",
				"10011001000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"7",
				"'",
				"'",
				"07",
				"10011000100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"8",
				"(",
				"(",
				"08",
				"10001100100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"9",
				")",
				")",
				"09",
				"11001001000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"10",
				"*",
				"*",
				"10",
				"11001000100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"11",
				"+",
				"+",
				"11",
				"11000100100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"12",
				",",
				",",
				"12",
				"10110011100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"13",
				"-",
				"-",
				"13",
				"10011011100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"14",
				".",
				".",
				"14",
				"10011001110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"15",
				"/",
				"/",
				"15",
				"10111001100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"16",
				"0",
				"0",
				"16",
				"10011101100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"17",
				"1",
				"1",
				"17",
				"10011100110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"18",
				"2",
				"2",
				"18",
				"11001110010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"19",
				"3",
				"3",
				"19",
				"11001011100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"20",
				"4",
				"4",
				"20",
				"11001001110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"21",
				"5",
				"5",
				"21",
				"11011100100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"22",
				"6",
				"6",
				"22",
				"11001110100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"23",
				"7",
				"7",
				"23",
				"11101101110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"24",
				"8",
				"8",
				"24",
				"11101001100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"25",
				"9",
				"9",
				"25",
				"11100101100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"26",
				":",
				":",
				"26",
				"11100100110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"27",
				";",
				";",
				"27",
				"11101100100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"28",
				"<",
				"<",
				"28",
				"11100110100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"29",
				"=",
				"=",
				"29",
				"11100110010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"30",
				">",
				">",
				"30",
				"11011011000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"31",
				"?",
				"?",
				"31",
				"11011000110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"32",
				"@",
				"@",
				"32",
				"11000110110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"33",
				"A",
				"A",
				"33",
				"10100011000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"34",
				"B",
				"B",
				"34",
				"10001011000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"35",
				"C",
				"C",
				"35",
				"10001000110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"36",
				"D",
				"D",
				"36",
				"10110001000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"37",
				"E",
				"E",
				"37",
				"10001101000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"38",
				"F",
				"F",
				"38",
				"10001100010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"39",
				"G",
				"G",
				"39",
				"11010001000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"40",
				"H",
				"H",
				"40",
				"11000101000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"41",
				"I",
				"I",
				"41",
				"11000100010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"42",
				"J",
				"J",
				"42",
				"10110111000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"43",
				"K",
				"K",
				"43",
				"10110001110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"44",
				"L",
				"L",
				"44",
				"10001101110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"45",
				"M",
				"M",
				"45",
				"10111011000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"46",
				"N",
				"N",
				"46",
				"10111000110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"47",
				"O",
				"O",
				"47",
				"10001110110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"48",
				"P",
				"P",
				"48",
				"11101110110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"49",
				"Q",
				"Q",
				"49",
				"11010001110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"50",
				"R",
				"R",
				"50",
				"11000101110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"51",
				"S",
				"S",
				"51",
				"11011101000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"52",
				"T",
				"T",
				"52",
				"11011100010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"53",
				"U",
				"U",
				"53",
				"11011101110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"54",
				"V",
				"V",
				"54",
				"11101011000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"55",
				"W",
				"W",
				"55",
				"11101000110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"56",
				"X",
				"X",
				"56",
				"11100010110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"57",
				"Y",
				"Y",
				"57",
				"11101101000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"58",
				"Z",
				"Z",
				"58",
				"11101100010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"59",
				"[",
				"[",
				"59",
				"11100011010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"60",
				"\\",
				"\\",
				"60",
				"11101111010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"61",
				"]",
				"]",
				"61",
				"11001000010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"62",
				"^",
				"^",
				"62",
				"11110001010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"63",
				"_",
				"_",
				"63",
				"10100110000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"64",
				"\0",
				"`",
				"64",
				"10100001100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"65",
				Convert.ToChar(1).ToString(),
				"a",
				"65",
				"10010110000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"66",
				Convert.ToChar(2).ToString(),
				"b",
				"66",
				"10010000110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"67",
				Convert.ToChar(3).ToString(),
				"c",
				"67",
				"10000101100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"68",
				Convert.ToChar(4).ToString(),
				"d",
				"68",
				"10000100110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"69",
				Convert.ToChar(5).ToString(),
				"e",
				"69",
				"10110010000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"70",
				Convert.ToChar(6).ToString(),
				"f",
				"70",
				"10110000100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"71",
				Convert.ToChar(7).ToString(),
				"g",
				"71",
				"10011010000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"72",
				Convert.ToChar(8).ToString(),
				"h",
				"72",
				"10011000010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"73",
				Convert.ToChar(9).ToString(),
				"i",
				"73",
				"10000110100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"74",
				Convert.ToChar(10).ToString(),
				"j",
				"74",
				"10000110010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"75",
				Convert.ToChar(11).ToString(),
				"k",
				"75",
				"11000010010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"76",
				Convert.ToChar(12).ToString(),
				"l",
				"76",
				"11001010000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"77",
				Convert.ToChar(13).ToString(),
				"m",
				"77",
				"11110111010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"78",
				Convert.ToChar(14).ToString(),
				"n",
				"78",
				"11000010100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"79",
				Convert.ToChar(15).ToString(),
				"o",
				"79",
				"10001111010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"80",
				Convert.ToChar(16).ToString(),
				"p",
				"80",
				"10100111100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"81",
				Convert.ToChar(17).ToString(),
				"q",
				"81",
				"10010111100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"82",
				Convert.ToChar(18).ToString(),
				"r",
				"82",
				"10010011110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"83",
				Convert.ToChar(19).ToString(),
				"s",
				"83",
				"10111100100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"84",
				Convert.ToChar(20).ToString(),
				"t",
				"84",
				"10011110100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"85",
				Convert.ToChar(21).ToString(),
				"u",
				"85",
				"10011110010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"86",
				Convert.ToChar(22).ToString(),
				"v",
				"86",
				"11110100100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"87",
				Convert.ToChar(23).ToString(),
				"w",
				"87",
				"11110010100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"88",
				Convert.ToChar(24).ToString(),
				"x",
				"88",
				"11110010010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"89",
				Convert.ToChar(25).ToString(),
				"y",
				"89",
				"11011011110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"90",
				Convert.ToChar(26).ToString(),
				"z",
				"90",
				"11011110110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"91",
				Convert.ToChar(27).ToString(),
				"{",
				"91",
				"11110110110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"92",
				Convert.ToChar(28).ToString(),
				"|",
				"92",
				"10101111000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"93",
				Convert.ToChar(29).ToString(),
				"}",
				"93",
				"10100011110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"94",
				Convert.ToChar(30).ToString(),
				"~",
				"94",
				"10001011110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"95",
				Convert.ToChar(31).ToString(),
				Convert.ToChar(127).ToString(),
				"95",
				"10111101000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"96",
				Convert.ToChar(202).ToString(),
				Convert.ToChar(202).ToString(),
				"96",
				"10111100010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"97",
				Convert.ToChar(201).ToString(),
				Convert.ToChar(201).ToString(),
				"97",
				"11110101000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"98",
				"SHIFT",
				"SHIFT",
				"98",
				"11110100010"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"99",
				"CODE_C",
				"CODE_C",
				"99",
				"10111011110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"100",
				"CODE_B",
				Convert.ToChar(203).ToString(),
				"CODE_B",
				"10111101110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"101",
				Convert.ToChar(203).ToString(),
				"CODE_A",
				"CODE_A",
				"11101011110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"102",
				Convert.ToChar(200).ToString(),
				Convert.ToChar(200).ToString(),
				Convert.ToChar(200).ToString(),
				"11110101110"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"103",
				"START_A",
				"START_A",
				"START_A",
				"11010000100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"104",
				"START_B",
				"START_B",
				"START_B",
				"11010010000"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"105",
				"START_C",
				"START_C",
				"START_C",
				"11010011100"
			});
			this.C128_Code.Rows.Add(new object[]
			{
				"",
				"STOP",
				"STOP",
				"STOP",
				"11000111010"
			});
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000647C File Offset: 0x0000467C
		private List<DataRow> FindStartorCodeCharacter(string s, ref int col)
		{
			List<DataRow> list = new List<DataRow>();
			if (s.Length > 1 && (char.IsNumber(s[0]) || s[0] == Convert.ToChar(200)) && (char.IsNumber(s[1]) || s[0] == Convert.ToChar(200)))
			{
				if (this.StartCharacter == null)
				{
					this.StartCharacter = this.C128_Code.Select("A = 'START_C'")[0];
					list.Add(this.StartCharacter);
				}
				else
				{
					list.Add(this.C128_Code.Select("A = 'CODE_C'")[0]);
				}
				col = 1;
			}
			else
			{
				bool flag = false;
				bool flag2 = false;
				foreach (object obj in this.C128_Code.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					try
					{
						if (!flag && s == dataRow["A"].ToString())
						{
							flag = true;
							col = 2;
							if (this.StartCharacter == null)
							{
								this.StartCharacter = this.C128_Code.Select("A = 'START_A'")[0];
								list.Add(this.StartCharacter);
							}
							else
							{
								list.Add(this.C128_Code.Select("B = 'CODE_A'")[0]);
							}
						}
						else if (!flag2 && s == dataRow["B"].ToString())
						{
							flag2 = true;
							col = 1;
							if (this.StartCharacter == null)
							{
								this.StartCharacter = this.C128_Code.Select("A = 'START_B'")[0];
								list.Add(this.StartCharacter);
							}
							else
							{
								list.Add(this.C128_Code.Select("A = 'CODE_B'")[0]);
							}
						}
						else if (flag && flag2)
						{
							break;
						}
					}
					catch (Exception ex)
					{
						base.Error("EC128-1: " + ex.Message);
					}
				}
				if (list.Count <= 0)
				{
					base.Error("EC128-2: Could not determine start character.");
				}
			}
			return list;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000066B8 File Offset: 0x000048B8
		private string CalculateCheckDigit()
		{
			string text = this._FormattedData[0];
			uint num = 0U;
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)this._FormattedData.Count))
			{
				string str = this._FormattedData[(int)num2].Replace("'", "''");
				DataRow[] array = this.C128_Code.Select("A = '" + str + "'");
				if (array.Length == 0)
				{
					array = this.C128_Code.Select("B = '" + str + "'");
				}
				if (array.Length == 0)
				{
					array = this.C128_Code.Select("C = '" + str + "'");
				}
				uint num3 = uint.Parse(array[0]["Value"].ToString()) * ((num2 == 0U) ? 1U : num2);
				num += num3;
				num2 += 1U;
			}
			uint num4 = num % 103U;
			return this.C128_Code.Select("Value = '" + num4.ToString() + "'")[0]["Encoding"].ToString();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000067D0 File Offset: 0x000049D0
		private void BreakUpDataForEncoding()
		{
			string text = "";
			string text2 = this.Raw_Data;
			if (this.type == Code128.TYPES.A || this.type == Code128.TYPES.B)
			{
				foreach (char c in this.Raw_Data)
				{
					this._FormattedData.Add(c.ToString());
				}
				return;
			}
			if (this.type == Code128.TYPES.C)
			{
				if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
				{
					base.Error("EC128-6: Only numeric values can be encoded with C128-C.");
				}
				if (this.Raw_Data.Length % 2 > 0)
				{
					text2 = "0" + this.Raw_Data;
				}
			}
			foreach (char c2 in text2)
			{
				if (char.IsNumber(c2))
				{
					if (text == "")
					{
						text += c2.ToString();
					}
					else
					{
						text += c2.ToString();
						this._FormattedData.Add(text);
						text = "";
					}
				}
				else
				{
					if (text != "")
					{
						this._FormattedData.Add(text);
						text = "";
					}
					this._FormattedData.Add(c2.ToString());
				}
			}
			if (text != "")
			{
				this._FormattedData.Add(text);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000692C File Offset: 0x00004B2C
		private void InsertStartandCodeCharacters()
		{
			DataRow dataRow = null;
			string text = "";
			if (this.type == Code128.TYPES.DYNAMIC)
			{
				try
				{
					for (int i = 0; i < this._FormattedData.Count; i++)
					{
						int columnIndex = 0;
						List<DataRow> list = this.FindStartorCodeCharacter(this._FormattedData[i], ref columnIndex);
						bool flag = false;
						foreach (DataRow dataRow2 in list)
						{
							if (dataRow2["A"].ToString().EndsWith(text) || dataRow2["B"].ToString().EndsWith(text) || dataRow2["C"].ToString().EndsWith(text))
							{
								flag = true;
								break;
							}
						}
						if (text == "" || !flag)
						{
							dataRow = list[0];
							bool flag2 = true;
							while (flag2)
							{
								try
								{
									text = dataRow[columnIndex].ToString().Split(new char[]
									{
										'_'
									})[1];
									flag2 = false;
								}
								catch
								{
									flag2 = true;
									if (columnIndex++ > dataRow.ItemArray.Length)
									{
										base.Error("No start character found in CurrentCodeSet.");
									}
								}
							}
							this._FormattedData.Insert(i++, dataRow[columnIndex].ToString());
						}
					}
				}
				catch (Exception ex)
				{
					base.Error("EC128-3: Could not insert start and code characters.\n Message: " + ex.Message);
				}
				return;
			}
			switch (this.type)
			{
			case Code128.TYPES.A:
				this._FormattedData.Insert(0, "START_A");
				return;
			case Code128.TYPES.B:
				this._FormattedData.Insert(0, "START_B");
				return;
			case Code128.TYPES.C:
				this._FormattedData.Insert(0, "START_C");
				return;
			default:
				base.Error("EC128-4: Unknown start type in fixed type encoding.");
				return;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006B50 File Offset: 0x00004D50
		private string GetEncoding()
		{
			this.BreakUpDataForEncoding();
			this.InsertStartandCodeCharacters();
			this.CalculateCheckDigit();
			string text = "";
			foreach (string text2 in this._FormattedData)
			{
				string str = text2.Replace("'", "''");
				DataRow[] array;
				switch (this.type)
				{
				case Code128.TYPES.DYNAMIC:
					array = this.C128_Code.Select("A = '" + str + "'");
					if (array.Length == 0)
					{
						array = this.C128_Code.Select("B = '" + str + "'");
						if (array.Length == 0)
						{
							array = this.C128_Code.Select("C = '" + str + "'");
						}
					}
					break;
				case Code128.TYPES.A:
					array = this.C128_Code.Select("A = '" + str + "'");
					break;
				case Code128.TYPES.B:
					array = this.C128_Code.Select("B = '" + str + "'");
					break;
				case Code128.TYPES.C:
					array = this.C128_Code.Select("C = '" + str + "'");
					break;
				default:
					array = null;
					break;
				}
				if (array == null || array.Length == 0)
				{
					base.Error("EC128-5: Could not find encoding of a value( " + str + " ) in C128 type " + this.type.ToString());
				}
				text += array[0]["Encoding"].ToString();
				this._EncodedData.Add(array[0]["Encoding"].ToString());
			}
			text += this.CalculateCheckDigit();
			this._EncodedData.Add(this.CalculateCheckDigit());
			text += this.C128_Code.Select("A = 'STOP'")[0]["Encoding"].ToString();
			this._EncodedData.Add(this.C128_Code.Select("A = 'STOP'")[0]["Encoding"].ToString());
			text += "11";
			this._EncodedData.Add("11");
			return text;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00006DAC File Offset: 0x00004FAC
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Code128();
			}
		}

		// Token: 0x04000054 RID: 84
		private DataTable C128_Code = new DataTable("C128");

		// Token: 0x04000055 RID: 85
		private List<string> _FormattedData = new List<string>();

		// Token: 0x04000056 RID: 86
		private List<string> _EncodedData = new List<string>();

		// Token: 0x04000057 RID: 87
		private DataRow StartCharacter;

		// Token: 0x04000058 RID: 88
		private Code128.TYPES type;

		// Token: 0x02000026 RID: 38
		public enum TYPES
		{
			// Token: 0x0400009F RID: 159
			DYNAMIC,
			// Token: 0x040000A0 RID: 160
			A,
			// Token: 0x040000A1 RID: 161
			B,
			// Token: 0x040000A2 RID: 162
			C
		}
	}
}

using System;
using System.Collections;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200001C RID: 28
	internal class UPCA : BarcodeCommon, IBarcode
	{
		// Token: 0x060000AE RID: 174 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public UPCA(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		private string Encode_UPCA()
		{
			if (this.Raw_Data.Length != 11 && this.Raw_Data.Length != 12)
			{
				base.Error("EUPCA-1: Data length invalid. (Length must be 11 or 12)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EUPCA-2: Numeric Data Only");
			}
			this.CheckDigit();
			string text = "101";
			text += this.UPC_CodeA[int.Parse(this.Raw_Data[0].ToString())];
			int i;
			for (i = 0; i < 5; i++)
			{
				text += this.UPC_CodeA[int.Parse(this.Raw_Data[i + 1].ToString())];
			}
			text += "01010";
			i = 0;
			while (i < 5)
			{
				text += this.UPC_CodeB[int.Parse(this.Raw_Data[i++ + 6].ToString())];
			}
			text += this.UPC_CodeB[int.Parse(this.Raw_Data[this.Raw_Data.Length - 1].ToString())];
			text += "101";
			this.init_CountryCodes();
			string key = "0" + this.Raw_Data.Substring(0, 1);
			try
			{
				this._Country_Assigning_Manufacturer_Code = this.CountryCodes[key].ToString();
			}
			catch
			{
				base.Error("EUPCA-3: Country assigning manufacturer code not found.");
			}
			finally
			{
				this.CountryCodes.Clear();
			}
			return text;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000DF94 File Offset: 0x0000C194
		private void init_CountryCodes()
		{
			this.CountryCodes.Clear();
			this.CountryCodes.Add("00", "US / CANADA");
			this.CountryCodes.Add("01", "US / CANADA");
			this.CountryCodes.Add("02", "US / CANADA");
			this.CountryCodes.Add("03", "US / CANADA");
			this.CountryCodes.Add("04", "US / CANADA");
			this.CountryCodes.Add("05", "US / CANADA");
			this.CountryCodes.Add("06", "US / CANADA");
			this.CountryCodes.Add("07", "US / CANADA");
			this.CountryCodes.Add("08", "US / CANADA");
			this.CountryCodes.Add("09", "US / CANADA");
			this.CountryCodes.Add("10", "US / CANADA");
			this.CountryCodes.Add("11", "US / CANADA");
			this.CountryCodes.Add("12", "US / CANADA");
			this.CountryCodes.Add("13", "US / CANADA");
			this.CountryCodes.Add("20", "IN STORE");
			this.CountryCodes.Add("21", "IN STORE");
			this.CountryCodes.Add("22", "IN STORE");
			this.CountryCodes.Add("23", "IN STORE");
			this.CountryCodes.Add("24", "IN STORE");
			this.CountryCodes.Add("25", "IN STORE");
			this.CountryCodes.Add("26", "IN STORE");
			this.CountryCodes.Add("27", "IN STORE");
			this.CountryCodes.Add("28", "IN STORE");
			this.CountryCodes.Add("29", "IN STORE");
			this.CountryCodes.Add("30", "FRANCE");
			this.CountryCodes.Add("31", "FRANCE");
			this.CountryCodes.Add("32", "FRANCE");
			this.CountryCodes.Add("33", "FRANCE");
			this.CountryCodes.Add("34", "FRANCE");
			this.CountryCodes.Add("35", "FRANCE");
			this.CountryCodes.Add("36", "FRANCE");
			this.CountryCodes.Add("37", "FRANCE");
			this.CountryCodes.Add("40", "GERMANY");
			this.CountryCodes.Add("41", "GERMANY");
			this.CountryCodes.Add("42", "GERMANY");
			this.CountryCodes.Add("43", "GERMANY");
			this.CountryCodes.Add("44", "GERMANY");
			this.CountryCodes.Add("45", "JAPAN");
			this.CountryCodes.Add("46", "RUSSIAN FEDERATION");
			this.CountryCodes.Add("49", "JAPAN (JAN-13)");
			this.CountryCodes.Add("50", "UNITED KINGDOM");
			this.CountryCodes.Add("54", "BELGIUM / LUXEMBOURG");
			this.CountryCodes.Add("57", "DENMARK");
			this.CountryCodes.Add("64", "FINLAND");
			this.CountryCodes.Add("70", "NORWAY");
			this.CountryCodes.Add("73", "SWEDEN");
			this.CountryCodes.Add("76", "SWITZERLAND");
			this.CountryCodes.Add("80", "ITALY");
			this.CountryCodes.Add("81", "ITALY");
			this.CountryCodes.Add("82", "ITALY");
			this.CountryCodes.Add("83", "ITALY");
			this.CountryCodes.Add("84", "SPAIN");
			this.CountryCodes.Add("87", "NETHERLANDS");
			this.CountryCodes.Add("90", "AUSTRIA");
			this.CountryCodes.Add("91", "AUSTRIA");
			this.CountryCodes.Add("93", "AUSTRALIA");
			this.CountryCodes.Add("94", "NEW ZEALAND");
			this.CountryCodes.Add("99", "COUPONS");
			this.CountryCodes.Add("471", "TAIWAN");
			this.CountryCodes.Add("474", "ESTONIA");
			this.CountryCodes.Add("475", "LATVIA");
			this.CountryCodes.Add("477", "LITHUANIA");
			this.CountryCodes.Add("479", "SRI LANKA");
			this.CountryCodes.Add("480", "PHILIPPINES");
			this.CountryCodes.Add("482", "UKRAINE");
			this.CountryCodes.Add("484", "MOLDOVA");
			this.CountryCodes.Add("485", "ARMENIA");
			this.CountryCodes.Add("486", "GEORGIA");
			this.CountryCodes.Add("487", "KAZAKHSTAN");
			this.CountryCodes.Add("489", "HONG KONG");
			this.CountryCodes.Add("520", "GREECE");
			this.CountryCodes.Add("528", "LEBANON");
			this.CountryCodes.Add("529", "CYPRUS");
			this.CountryCodes.Add("531", "MACEDONIA");
			this.CountryCodes.Add("535", "MALTA");
			this.CountryCodes.Add("539", "IRELAND");
			this.CountryCodes.Add("560", "PORTUGAL");
			this.CountryCodes.Add("569", "ICELAND");
			this.CountryCodes.Add("590", "POLAND");
			this.CountryCodes.Add("594", "ROMANIA");
			this.CountryCodes.Add("599", "HUNGARY");
			this.CountryCodes.Add("600", "SOUTH AFRICA");
			this.CountryCodes.Add("601", "SOUTH AFRICA");
			this.CountryCodes.Add("609", "MAURITIUS");
			this.CountryCodes.Add("611", "MOROCCO");
			this.CountryCodes.Add("613", "ALGERIA");
			this.CountryCodes.Add("619", "TUNISIA");
			this.CountryCodes.Add("622", "EGYPT");
			this.CountryCodes.Add("625", "JORDAN");
			this.CountryCodes.Add("626", "IRAN");
			this.CountryCodes.Add("690", "CHINA");
			this.CountryCodes.Add("691", "CHINA");
			this.CountryCodes.Add("692", "CHINA");
			this.CountryCodes.Add("729", "ISRAEL");
			this.CountryCodes.Add("740", "GUATEMALA");
			this.CountryCodes.Add("741", "EL SALVADOR");
			this.CountryCodes.Add("742", "HONDURAS");
			this.CountryCodes.Add("743", "NICARAGUA");
			this.CountryCodes.Add("744", "COSTA RICA");
			this.CountryCodes.Add("746", "DOMINICAN REPUBLIC");
			this.CountryCodes.Add("750", "MEXICO");
			this.CountryCodes.Add("759", "VENEZUELA");
			this.CountryCodes.Add("770", "COLOMBIA");
			this.CountryCodes.Add("773", "URUGUAY");
			this.CountryCodes.Add("775", "PERU");
			this.CountryCodes.Add("777", "BOLIVIA");
			this.CountryCodes.Add("779", "ARGENTINA");
			this.CountryCodes.Add("780", "CHILE");
			this.CountryCodes.Add("784", "PARAGUAY");
			this.CountryCodes.Add("785", "PERU");
			this.CountryCodes.Add("786", "ECUADOR");
			this.CountryCodes.Add("789", "BRAZIL");
			this.CountryCodes.Add("850", "CUBA");
			this.CountryCodes.Add("858", "SLOVAKIA");
			this.CountryCodes.Add("859", "CZECH REPUBLIC");
			this.CountryCodes.Add("860", "YUGLOSLAVIA");
			this.CountryCodes.Add("869", "TURKEY");
			this.CountryCodes.Add("880", "SOUTH KOREA");
			this.CountryCodes.Add("885", "THAILAND");
			this.CountryCodes.Add("888", "SINGAPORE");
			this.CountryCodes.Add("890", "INDIA");
			this.CountryCodes.Add("893", "VIETNAM");
			this.CountryCodes.Add("899", "INDONESIA");
			this.CountryCodes.Add("955", "MALAYSIA");
			this.CountryCodes.Add("977", "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
			this.CountryCodes.Add("978", "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
			this.CountryCodes.Add("979", "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");
			this.CountryCodes.Add("980", "REFUND RECEIPTS");
			this.CountryCodes.Add("981", "COMMON CURRENCY COUPONS");
			this.CountryCodes.Add("982", "COMMON CURRENCY COUPONS");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000EA58 File Offset: 0x0000CC58
		private void CheckDigit()
		{
			try
			{
				string text = this.Raw_Data.Substring(0, 11);
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < text.Length; i++)
				{
					if (i % 2 == 0)
					{
						num2 += int.Parse(text.Substring(i, 1)) * 3;
					}
					else
					{
						num += int.Parse(text.Substring(i, 1));
					}
				}
				int num3 = (num + num2) % 10;
				num3 = 10 - num3;
				if (num3 == 10)
				{
					num3 = 0;
				}
				this.Raw_Data = text + num3.ToString()[0].ToString();
			}
			catch
			{
				base.Error("EUPCA-4: Error calculating check digit.");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public string Encoded_Value
		{
			get
			{
				return this.Encode_UPCA();
			}
		}

		// Token: 0x04000075 RID: 117
		private string[] UPC_CodeA = new string[]
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

		// Token: 0x04000076 RID: 118
		private string[] UPC_CodeB = new string[]
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

		// Token: 0x04000077 RID: 119
		private string _Country_Assigning_Manufacturer_Code = "N/A";

		// Token: 0x04000078 RID: 120
		private Hashtable CountryCodes = new Hashtable();
	}
}

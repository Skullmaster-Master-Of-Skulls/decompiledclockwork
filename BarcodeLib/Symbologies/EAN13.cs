using System;
using System.Collections;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000014 RID: 20
	internal class EAN13 : BarcodeCommon, IBarcode
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00009F44 File Offset: 0x00008144
		public EAN13(string input)
		{
			this.Raw_Data = input;
			this.CheckDigit();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000A0F4 File Offset: 0x000082F4
		private string Encode_EAN13()
		{
			if (this.Raw_Data.Length < 12 || this.Raw_Data.Length > 13)
			{
				base.Error("EEAN13-1: Data length invalid. (Length must be 12 or 13)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EEAN13-2: Numeric Data Only");
			}
			string text = this.EAN_Pattern[int.Parse(this.Raw_Data[0].ToString())];
			string text2 = "101";
			int i;
			for (i = 0; i < 6; i++)
			{
				if (text[i] == 'a')
				{
					text2 += this.EAN_CodeA[int.Parse(this.Raw_Data[i + 1].ToString())];
				}
				if (text[i] == 'b')
				{
					text2 += this.EAN_CodeB[int.Parse(this.Raw_Data[i + 1].ToString())];
				}
			}
			text2 += "01010";
			i = 1;
			while (i <= 5)
			{
				text2 += this.EAN_CodeC[int.Parse(this.Raw_Data[i++ + 6].ToString())];
			}
			int num = int.Parse(this.Raw_Data[this.Raw_Data.Length - 1].ToString());
			text2 += this.EAN_CodeC[num];
			text2 += "101";
			this.init_CountryCodes();
			this._Country_Assigning_Manufacturer_Code = "N/A";
			string key = this.Raw_Data.Substring(0, 2);
			string key2 = this.Raw_Data.Substring(0, 3);
			try
			{
				this._Country_Assigning_Manufacturer_Code = this.CountryCodes[key2].ToString();
			}
			catch
			{
				try
				{
					this._Country_Assigning_Manufacturer_Code = this.CountryCodes[key].ToString();
				}
				catch
				{
					base.Error("EEAN13-3: Country assigning manufacturer code not found.");
				}
			}
			finally
			{
				this.CountryCodes.Clear();
			}
			return text2;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000A310 File Offset: 0x00008510
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
			this.CountryCodes.Add("100", "UNITED STATES");
			this.CountryCodes.Add("101", "UNITED STATES");
			this.CountryCodes.Add("102", "UNITED STATES");
			this.CountryCodes.Add("103", "UNITED STATES");
			this.CountryCodes.Add("104", "UNITED STATES");
			this.CountryCodes.Add("105", "UNITED STATES");
			this.CountryCodes.Add("106", "UNITED STATES");
			this.CountryCodes.Add("107", "UNITED STATES");
			this.CountryCodes.Add("108", "UNITED STATES");
			this.CountryCodes.Add("109", "UNITED STATES");
			this.CountryCodes.Add("110", "UNITED STATES");
			this.CountryCodes.Add("111", "UNITED STATES");
			this.CountryCodes.Add("112", "UNITED STATES");
			this.CountryCodes.Add("113", "UNITED STATES");
			this.CountryCodes.Add("114", "UNITED STATES");
			this.CountryCodes.Add("115", "UNITED STATES");
			this.CountryCodes.Add("116", "UNITED STATES");
			this.CountryCodes.Add("117", "UNITED STATES");
			this.CountryCodes.Add("118", "UNITED STATES");
			this.CountryCodes.Add("119", "UNITED STATES");
			this.CountryCodes.Add("120", "UNITED STATES");
			this.CountryCodes.Add("121", "UNITED STATES");
			this.CountryCodes.Add("122", "UNITED STATES");
			this.CountryCodes.Add("123", "UNITED STATES");
			this.CountryCodes.Add("124", "UNITED STATES");
			this.CountryCodes.Add("125", "UNITED STATES");
			this.CountryCodes.Add("126", "UNITED STATES");
			this.CountryCodes.Add("127", "UNITED STATES");
			this.CountryCodes.Add("128", "UNITED STATES");
			this.CountryCodes.Add("129", "UNITED STATES");
			this.CountryCodes.Add("130", "UNITED STATES");
			this.CountryCodes.Add("131", "UNITED STATES");
			this.CountryCodes.Add("132", "UNITED STATES");
			this.CountryCodes.Add("133", "UNITED STATES");
			this.CountryCodes.Add("134", "UNITED STATES");
			this.CountryCodes.Add("135", "UNITED STATES");
			this.CountryCodes.Add("136", "UNITED STATES");
			this.CountryCodes.Add("137", "UNITED STATES");
			this.CountryCodes.Add("138", "UNITED STATES");
			this.CountryCodes.Add("139", "UNITED STATES");
			this.CountryCodes.Add("300", "FRANCE AND MONACO");
			this.CountryCodes.Add("301", "FRANCE AND MONACO");
			this.CountryCodes.Add("302", "FRANCE AND MONACO");
			this.CountryCodes.Add("303", "FRANCE AND MONACO");
			this.CountryCodes.Add("304", "FRANCE AND MONACO");
			this.CountryCodes.Add("305", "FRANCE AND MONACO");
			this.CountryCodes.Add("306", "FRANCE AND MONACO");
			this.CountryCodes.Add("307", "FRANCE AND MONACO");
			this.CountryCodes.Add("308", "FRANCE AND MONACO");
			this.CountryCodes.Add("309", "FRANCE AND MONACO");
			this.CountryCodes.Add("310", "FRANCE AND MONACO");
			this.CountryCodes.Add("311", "FRANCE AND MONACO");
			this.CountryCodes.Add("312", "FRANCE AND MONACO");
			this.CountryCodes.Add("313", "FRANCE AND MONACO");
			this.CountryCodes.Add("314", "FRANCE AND MONACO");
			this.CountryCodes.Add("315", "FRANCE AND MONACO");
			this.CountryCodes.Add("316", "FRANCE AND MONACO");
			this.CountryCodes.Add("317", "FRANCE AND MONACO");
			this.CountryCodes.Add("318", "FRANCE AND MONACO");
			this.CountryCodes.Add("319", "FRANCE AND MONACO");
			this.CountryCodes.Add("320", "FRANCE AND MONACO");
			this.CountryCodes.Add("321", "FRANCE AND MONACO");
			this.CountryCodes.Add("322", "FRANCE AND MONACO");
			this.CountryCodes.Add("323", "FRANCE AND MONACO");
			this.CountryCodes.Add("324", "FRANCE AND MONACO");
			this.CountryCodes.Add("325", "FRANCE AND MONACO");
			this.CountryCodes.Add("326", "FRANCE AND MONACO");
			this.CountryCodes.Add("327", "FRANCE AND MONACO");
			this.CountryCodes.Add("328", "FRANCE AND MONACO");
			this.CountryCodes.Add("329", "FRANCE AND MONACO");
			this.CountryCodes.Add("330", "FRANCE AND MONACO");
			this.CountryCodes.Add("331", "FRANCE AND MONACO");
			this.CountryCodes.Add("332", "FRANCE AND MONACO");
			this.CountryCodes.Add("333", "FRANCE AND MONACO");
			this.CountryCodes.Add("334", "FRANCE AND MONACO");
			this.CountryCodes.Add("335", "FRANCE AND MONACO");
			this.CountryCodes.Add("336", "FRANCE AND MONACO");
			this.CountryCodes.Add("337", "FRANCE AND MONACO");
			this.CountryCodes.Add("338", "FRANCE AND MONACO");
			this.CountryCodes.Add("339", "FRANCE AND MONACO");
			this.CountryCodes.Add("340", "FRANCE AND MONACO");
			this.CountryCodes.Add("341", "FRANCE AND MONACO");
			this.CountryCodes.Add("342", "FRANCE AND MONACO");
			this.CountryCodes.Add("343", "FRANCE AND MONACO");
			this.CountryCodes.Add("344", "FRANCE AND MONACO");
			this.CountryCodes.Add("345", "FRANCE AND MONACO");
			this.CountryCodes.Add("346", "FRANCE AND MONACO");
			this.CountryCodes.Add("347", "FRANCE AND MONACO");
			this.CountryCodes.Add("348", "FRANCE AND MONACO");
			this.CountryCodes.Add("349", "FRANCE AND MONACO");
			this.CountryCodes.Add("350", "FRANCE AND MONACO");
			this.CountryCodes.Add("351", "FRANCE AND MONACO");
			this.CountryCodes.Add("352", "FRANCE AND MONACO");
			this.CountryCodes.Add("353", "FRANCE AND MONACO");
			this.CountryCodes.Add("354", "FRANCE AND MONACO");
			this.CountryCodes.Add("355", "FRANCE AND MONACO");
			this.CountryCodes.Add("356", "FRANCE AND MONACO");
			this.CountryCodes.Add("357", "FRANCE AND MONACO");
			this.CountryCodes.Add("358", "FRANCE AND MONACO");
			this.CountryCodes.Add("359", "FRANCE AND MONACO");
			this.CountryCodes.Add("360", "FRANCE AND MONACO");
			this.CountryCodes.Add("361", "FRANCE AND MONACO");
			this.CountryCodes.Add("362", "FRANCE AND MONACO");
			this.CountryCodes.Add("363", "FRANCE AND MONACO");
			this.CountryCodes.Add("364", "FRANCE AND MONACO");
			this.CountryCodes.Add("365", "FRANCE AND MONACO");
			this.CountryCodes.Add("366", "FRANCE AND MONACO");
			this.CountryCodes.Add("367", "FRANCE AND MONACO");
			this.CountryCodes.Add("368", "FRANCE AND MONACO");
			this.CountryCodes.Add("369", "FRANCE AND MONACO");
			this.CountryCodes.Add("370", "FRANCE AND MONACO");
			this.CountryCodes.Add("371", "FRANCE AND MONACO");
			this.CountryCodes.Add("372", "FRANCE AND MONACO");
			this.CountryCodes.Add("373", "FRANCE AND MONACO");
			this.CountryCodes.Add("374", "FRANCE AND MONACO");
			this.CountryCodes.Add("375", "FRANCE AND MONACO");
			this.CountryCodes.Add("376", "FRANCE AND MONACO");
			this.CountryCodes.Add("377", "FRANCE AND MONACO");
			this.CountryCodes.Add("378", "FRANCE AND MONACO");
			this.CountryCodes.Add("379", "FRANCE AND MONACO");
			this.CountryCodes.Add("380", "BULGARIA");
			this.CountryCodes.Add("383", "SLOVENIJA");
			this.CountryCodes.Add("385", "CROATIA");
			this.CountryCodes.Add("387", "BOSNIA-HERZEGOVINA");
			this.CountryCodes.Add("389", "MONTENEGRO");
			this.CountryCodes.Add("400", "GERMANY");
			this.CountryCodes.Add("401", "GERMANY");
			this.CountryCodes.Add("402", "GERMANY");
			this.CountryCodes.Add("403", "GERMANY");
			this.CountryCodes.Add("404", "GERMANY");
			this.CountryCodes.Add("405", "GERMANY");
			this.CountryCodes.Add("406", "GERMANY");
			this.CountryCodes.Add("407", "GERMANY");
			this.CountryCodes.Add("408", "GERMANY");
			this.CountryCodes.Add("409", "GERMANY");
			this.CountryCodes.Add("410", "GERMANY");
			this.CountryCodes.Add("411", "GERMANY");
			this.CountryCodes.Add("412", "GERMANY");
			this.CountryCodes.Add("413", "GERMANY");
			this.CountryCodes.Add("414", "GERMANY");
			this.CountryCodes.Add("415", "GERMANY");
			this.CountryCodes.Add("416", "GERMANY");
			this.CountryCodes.Add("417", "GERMANY");
			this.CountryCodes.Add("418", "GERMANY");
			this.CountryCodes.Add("419", "GERMANY");
			this.CountryCodes.Add("420", "GERMANY");
			this.CountryCodes.Add("421", "GERMANY");
			this.CountryCodes.Add("422", "GERMANY");
			this.CountryCodes.Add("423", "GERMANY");
			this.CountryCodes.Add("424", "GERMANY");
			this.CountryCodes.Add("425", "GERMANY");
			this.CountryCodes.Add("426", "GERMANY");
			this.CountryCodes.Add("427", "GERMANY");
			this.CountryCodes.Add("428", "GERMANY");
			this.CountryCodes.Add("429", "GERMANY");
			this.CountryCodes.Add("430", "GERMANY");
			this.CountryCodes.Add("431", "GERMANY");
			this.CountryCodes.Add("432", "GERMANY");
			this.CountryCodes.Add("433", "GERMANY");
			this.CountryCodes.Add("434", "GERMANY");
			this.CountryCodes.Add("435", "GERMANY");
			this.CountryCodes.Add("436", "GERMANY");
			this.CountryCodes.Add("437", "GERMANY");
			this.CountryCodes.Add("438", "GERMANY");
			this.CountryCodes.Add("439", "GERMANY");
			this.CountryCodes.Add("440", "GERMANY");
			this.CountryCodes.Add("450", "JAPAN");
			this.CountryCodes.Add("451", "JAPAN");
			this.CountryCodes.Add("452", "JAPAN");
			this.CountryCodes.Add("453", "JAPAN");
			this.CountryCodes.Add("454", "JAPAN");
			this.CountryCodes.Add("455", "JAPAN");
			this.CountryCodes.Add("456", "JAPAN");
			this.CountryCodes.Add("457", "JAPAN");
			this.CountryCodes.Add("458", "JAPAN");
			this.CountryCodes.Add("459", "JAPAN");
			this.CountryCodes.Add("460", "RUSSIA");
			this.CountryCodes.Add("461", "RUSSIA");
			this.CountryCodes.Add("462", "RUSSIA");
			this.CountryCodes.Add("463", "RUSSIA");
			this.CountryCodes.Add("464", "RUSSIA");
			this.CountryCodes.Add("465", "RUSSIA");
			this.CountryCodes.Add("466", "RUSSIA");
			this.CountryCodes.Add("467", "RUSSIA");
			this.CountryCodes.Add("468", "RUSSIA");
			this.CountryCodes.Add("469", "RUSSIA");
			this.CountryCodes.Add("470", "KYRGYZSTAN");
			this.CountryCodes.Add("471", "TAIWAN");
			this.CountryCodes.Add("474", "ESTONIA");
			this.CountryCodes.Add("475", "LATVIA");
			this.CountryCodes.Add("476", "AZERBAIJAN");
			this.CountryCodes.Add("477", "LITHUANIA");
			this.CountryCodes.Add("478", "UZBEKISTAN");
			this.CountryCodes.Add("479", "SRI LANKA");
			this.CountryCodes.Add("480", "PHILIPPINES");
			this.CountryCodes.Add("481", "BELARUS");
			this.CountryCodes.Add("482", "UKRAINE");
			this.CountryCodes.Add("483", "TURKMENISTAN");
			this.CountryCodes.Add("484", "MOLDOVA");
			this.CountryCodes.Add("485", "ARMENIA");
			this.CountryCodes.Add("486", "GEORGIA");
			this.CountryCodes.Add("487", "KAZAKHSTAN");
			this.CountryCodes.Add("488", "TAJIKISTAN");
			this.CountryCodes.Add("489", "HONG KONG");
			this.CountryCodes.Add("490", "JAPAN");
			this.CountryCodes.Add("491", "JAPAN");
			this.CountryCodes.Add("492", "JAPAN");
			this.CountryCodes.Add("493", "JAPAN");
			this.CountryCodes.Add("494", "JAPAN");
			this.CountryCodes.Add("495", "JAPAN");
			this.CountryCodes.Add("496", "JAPAN");
			this.CountryCodes.Add("497", "JAPAN");
			this.CountryCodes.Add("498", "JAPAN");
			this.CountryCodes.Add("499", "JAPAN");
			this.CountryCodes.Add("500", "UNITED KINGDOM");
			this.CountryCodes.Add("501", "UNITED KINGDOM");
			this.CountryCodes.Add("502", "UNITED KINGDOM");
			this.CountryCodes.Add("503", "UNITED KINGDOM");
			this.CountryCodes.Add("504", "UNITED KINGDOM");
			this.CountryCodes.Add("505", "UNITED KINGDOM");
			this.CountryCodes.Add("506", "UNITED KINGDOM");
			this.CountryCodes.Add("507", "UNITED KINGDOM");
			this.CountryCodes.Add("508", "UNITED KINGDOM");
			this.CountryCodes.Add("509", "UNITED KINGDOM");
			this.CountryCodes.Add("520", "GREECE");
			this.CountryCodes.Add("521", "GREECE");
			this.CountryCodes.Add("528", "LEBANON");
			this.CountryCodes.Add("529", "CYPRUS");
			this.CountryCodes.Add("530", "ALBANIA");
			this.CountryCodes.Add("531", "MACEDONIA");
			this.CountryCodes.Add("535", "MALTA");
			this.CountryCodes.Add("539", "IRELAND");
			this.CountryCodes.Add("540", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("541", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("542", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("543", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("544", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("545", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("546", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("547", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("548", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("549", "BELGIUM AND LUXEMBOURG");
			this.CountryCodes.Add("560", "PORTUGAL");
			this.CountryCodes.Add("569", "ICELAND");
			this.CountryCodes.Add("570", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("571", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("572", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("573", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("574", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("575", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("576", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("577", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("578", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("579", " DENMARK, FAROE ISLANDS AND GREENLAND");
			this.CountryCodes.Add("590", "POLAND");
			this.CountryCodes.Add("594", "ROMANIA");
			this.CountryCodes.Add("599", "HUNGARY");
			this.CountryCodes.Add("600", "SOUTH AFRICA");
			this.CountryCodes.Add("601", "SOUTH AFRICA");
			this.CountryCodes.Add("603", "GHANA");
			this.CountryCodes.Add("604", "SENEGAL");
			this.CountryCodes.Add("608", "BAHRAIN");
			this.CountryCodes.Add("609", "MAURITIUS");
			this.CountryCodes.Add("611", "MOROCCO");
			this.CountryCodes.Add("613", "ALGERIA");
			this.CountryCodes.Add("615", "NIGERIA");
			this.CountryCodes.Add("616", "KENYA");
			this.CountryCodes.Add("618", "IVORY COAST");
			this.CountryCodes.Add("619", "TUNISIA");
			this.CountryCodes.Add("620", "TANZANIA");
			this.CountryCodes.Add("621", "SYRIA");
			this.CountryCodes.Add("622", "EGYPT");
			this.CountryCodes.Add("623", "BRUNEI");
			this.CountryCodes.Add("624", "LIBYA");
			this.CountryCodes.Add("625", "JORDAN");
			this.CountryCodes.Add("626", "IRAN");
			this.CountryCodes.Add("627", "KUWAIT");
			this.CountryCodes.Add("628", "SAUDI ARABIA");
			this.CountryCodes.Add("629", "EMIRATES");
			this.CountryCodes.Add("640", "FINLAND");
			this.CountryCodes.Add("641", "FINLAND");
			this.CountryCodes.Add("642", "FINLAND");
			this.CountryCodes.Add("643", "FINLAND");
			this.CountryCodes.Add("644", "FINLAND");
			this.CountryCodes.Add("645", "FINLAND");
			this.CountryCodes.Add("646", "FINLAND");
			this.CountryCodes.Add("647", "FINLAND");
			this.CountryCodes.Add("648", "FINLAND");
			this.CountryCodes.Add("649", "FINLAND");
			this.CountryCodes.Add("690", "CHINA");
			this.CountryCodes.Add("691", "CHINA");
			this.CountryCodes.Add("692", "CHINA");
			this.CountryCodes.Add("693", "CHINA");
			this.CountryCodes.Add("694", "CHINA");
			this.CountryCodes.Add("695", "CHINA");
			this.CountryCodes.Add("696", "CHINA");
			this.CountryCodes.Add("697", "CHINA");
			this.CountryCodes.Add("698", "CHINA");
			this.CountryCodes.Add("699", "CHINA");
			this.CountryCodes.Add("700", "NORWAY");
			this.CountryCodes.Add("701", "NORWAY");
			this.CountryCodes.Add("702", "NORWAY");
			this.CountryCodes.Add("703", "NORWAY");
			this.CountryCodes.Add("704", "NORWAY");
			this.CountryCodes.Add("705", "NORWAY");
			this.CountryCodes.Add("706", "NORWAY");
			this.CountryCodes.Add("707", "NORWAY");
			this.CountryCodes.Add("708", "NORWAY");
			this.CountryCodes.Add("709", "NORWAY");
			this.CountryCodes.Add("729", "ISRAEL");
			this.CountryCodes.Add("730", "SWEDEN");
			this.CountryCodes.Add("731", "SWEDEN");
			this.CountryCodes.Add("732", "SWEDEN");
			this.CountryCodes.Add("733", "SWEDEN");
			this.CountryCodes.Add("734", "SWEDEN");
			this.CountryCodes.Add("735", "SWEDEN");
			this.CountryCodes.Add("736", "SWEDEN");
			this.CountryCodes.Add("737", "SWEDEN");
			this.CountryCodes.Add("738", "SWEDEN");
			this.CountryCodes.Add("739", "SWEDEN");
			this.CountryCodes.Add("740", "GUATEMALA");
			this.CountryCodes.Add("741", "EL SALVADOR");
			this.CountryCodes.Add("742", "HONDURAS");
			this.CountryCodes.Add("743", "NICARAGUA");
			this.CountryCodes.Add("744", "COSTA RICA");
			this.CountryCodes.Add("745", "PANAMA");
			this.CountryCodes.Add("746", "DOMINICAN REPUBLIC");
			this.CountryCodes.Add("750", "MEXICO");
			this.CountryCodes.Add("754", "CANADA");
			this.CountryCodes.Add("755", "CANADA");
			this.CountryCodes.Add("759", "VENEZUELA");
			this.CountryCodes.Add("760", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("761", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("762", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("763", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("764", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("765", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("766", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("767", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("768", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("769", "SWITZERLAND AND LIECHTENSTEIN");
			this.CountryCodes.Add("770", "COLOMBIA");
			this.CountryCodes.Add("773", "URUGUAY");
			this.CountryCodes.Add("775", "PERU");
			this.CountryCodes.Add("777", "BOLIVIA");
			this.CountryCodes.Add("778", "ARGENTINA");
			this.CountryCodes.Add("779", "ARGENTINA");
			this.CountryCodes.Add("780", "CHILE");
			this.CountryCodes.Add("784", "PARAGUAY");
			this.CountryCodes.Add("785", "PERU");
			this.CountryCodes.Add("786", "ECUADOR");
			this.CountryCodes.Add("789", "BRAZIL");
			this.CountryCodes.Add("790", "BRAZIL");
			this.CountryCodes.Add("800", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("801", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("802", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("803", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("804", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("805", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("806", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("807", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("808", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("809", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("810", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("811", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("812", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("813", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("814", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("815", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("816", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("817", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("818", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("819", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("820", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("821", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("822", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("823", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("824", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("825", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("826", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("827", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("828", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("829", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("830", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("831", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("832", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("833", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("834", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("835", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("836", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("837", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("838", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("839", "ITALY, SAN MARINO AND VATICAN CITY");
			this.CountryCodes.Add("840", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("841", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("842", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("843", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("844", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("845", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("846", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("847", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("848", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("849", "SPAIN AND ANDORRA");
			this.CountryCodes.Add("850", "CUBA");
			this.CountryCodes.Add("858", "SLOVAKIA");
			this.CountryCodes.Add("859", "CZECH REPUBLIC");
			this.CountryCodes.Add("860", "SERBIA");
			this.CountryCodes.Add("865", "MONGOLIA");
			this.CountryCodes.Add("867", "NORTH KOREA");
			this.CountryCodes.Add("868", "TURKEY");
			this.CountryCodes.Add("869", "TURKEY");
			this.CountryCodes.Add("870", "NETHERLANDS");
			this.CountryCodes.Add("871", "NETHERLANDS");
			this.CountryCodes.Add("872", "NETHERLANDS");
			this.CountryCodes.Add("873", "NETHERLANDS");
			this.CountryCodes.Add("874", "NETHERLANDS");
			this.CountryCodes.Add("875", "NETHERLANDS");
			this.CountryCodes.Add("876", "NETHERLANDS");
			this.CountryCodes.Add("877", "NETHERLANDS");
			this.CountryCodes.Add("878", "NETHERLANDS");
			this.CountryCodes.Add("879", "NETHERLANDS");
			this.CountryCodes.Add("880", "SOUTH KOREA");
			this.CountryCodes.Add("884", "CAMBODIA");
			this.CountryCodes.Add("885", "THAILAND");
			this.CountryCodes.Add("888", "SINGAPORE");
			this.CountryCodes.Add("890", "INDIA");
			this.CountryCodes.Add("893", "VIETNAM");
			this.CountryCodes.Add("896", "PAKISTAN");
			this.CountryCodes.Add("899", "INDONESIA");
			this.CountryCodes.Add("900", "AUSTRIA");
			this.CountryCodes.Add("901", "AUSTRIA");
			this.CountryCodes.Add("902", "AUSTRIA");
			this.CountryCodes.Add("903", "AUSTRIA");
			this.CountryCodes.Add("904", "AUSTRIA");
			this.CountryCodes.Add("905", "AUSTRIA");
			this.CountryCodes.Add("906", "AUSTRIA");
			this.CountryCodes.Add("907", "AUSTRIA");
			this.CountryCodes.Add("908", "AUSTRIA");
			this.CountryCodes.Add("909", "AUSTRIA");
			this.CountryCodes.Add("930", "AUSTRALIA");
			this.CountryCodes.Add("931", "AUSTRALIA");
			this.CountryCodes.Add("932", "AUSTRALIA");
			this.CountryCodes.Add("933", "AUSTRALIA");
			this.CountryCodes.Add("934", "AUSTRALIA");
			this.CountryCodes.Add("935", "AUSTRALIA");
			this.CountryCodes.Add("936", "AUSTRALIA");
			this.CountryCodes.Add("937", "AUSTRALIA");
			this.CountryCodes.Add("938", "AUSTRALIA");
			this.CountryCodes.Add("939", "AUSTRALIA");
			this.CountryCodes.Add("940", "NEW ZEALAND");
			this.CountryCodes.Add("941", "NEW ZEALAND");
			this.CountryCodes.Add("942", "NEW ZEALAND");
			this.CountryCodes.Add("943", "NEW ZEALAND");
			this.CountryCodes.Add("944", "NEW ZEALAND");
			this.CountryCodes.Add("945", "NEW ZEALAND");
			this.CountryCodes.Add("946", "NEW ZEALAND");
			this.CountryCodes.Add("947", "NEW ZEALAND");
			this.CountryCodes.Add("948", "NEW ZEALAND");
			this.CountryCodes.Add("949", "NEW ZEALAND");
			this.CountryCodes.Add("950", "GS1 GLOBAL OFFICE SPECIAL APPLICATIONS");
			this.CountryCodes.Add("951", "EPC GLOBAL SPECIAL APPLICATIONS");
			this.CountryCodes.Add("955", "MALAYSIA");
			this.CountryCodes.Add("958", "MACAU");
			this.CountryCodes.Add("960", "GS1 UK: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("961", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("962", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("963", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("964", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("965", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("966", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("967", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("968", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("969", "GS1 GLOBAL OFFICE: GTIN-8 ALLOCATIONS");
			this.CountryCodes.Add("977", "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
			this.CountryCodes.Add("978", "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
			this.CountryCodes.Add("979", "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");
			this.CountryCodes.Add("980", "REFUND RECEIPTS");
			this.CountryCodes.Add("981", "COMMON CURRENCY COUPONS");
			this.CountryCodes.Add("982", "COMMON CURRENCY COUPONS");
			this.CountryCodes.Add("983", "COMMON CURRENCY COUPONS");
			this.CountryCodes.Add("984", "COMMON CURRENCY COUPONS");
			this.CountryCodes.Add("990", "COUPONS");
			this.CountryCodes.Add("991", "COUPONS");
			this.CountryCodes.Add("992", "COUPONS");
			this.CountryCodes.Add("993", "COUPONS");
			this.CountryCodes.Add("994", "COUPONS");
			this.CountryCodes.Add("995", "COUPONS");
			this.CountryCodes.Add("996", "COUPONS");
			this.CountryCodes.Add("997", "COUPONS");
			this.CountryCodes.Add("998", "COUPONS");
			this.CountryCodes.Add("999", "COUPONS");
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000D034 File Offset: 0x0000B234
		private void CheckDigit()
		{
			try
			{
				string text = this.Raw_Data.Substring(0, 12);
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < text.Length; i++)
				{
					if (i % 2 == 0)
					{
						num2 += int.Parse(text.Substring(i, 1));
					}
					else
					{
						num += int.Parse(text.Substring(i, 1)) * 3;
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
				base.Error("EEAN13-4: Error calculating check digit.");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		public string Encoded_Value
		{
			get
			{
				return this.Encode_EAN13();
			}
		}

		// Token: 0x04000068 RID: 104
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

		// Token: 0x04000069 RID: 105
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

		// Token: 0x0400006A RID: 106
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

		// Token: 0x0400006B RID: 107
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

		// Token: 0x0400006C RID: 108
		private Hashtable CountryCodes = new Hashtable();

		// Token: 0x0400006D RID: 109
		private string _Country_Assigning_Manufacturer_Code = "N/A";
	}
}

using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DataStructure.Money
{
	// Token: 0x02000019 RID: 25
	internal class CurrencyRepository
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003459 File Offset: 0x00001659
		public static Currency Get(CurrencyCodeKind currencyCode)
		{
			if (CurrencyRepository._currencyDictionary.ContainsKey(currencyCode))
			{
				return CurrencyRepository._currencyDictionary[currencyCode];
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003475 File Offset: 0x00001675
		public static bool Exists(CurrencyCodeKind currencyCode)
		{
			return CurrencyRepository._currencyDictionary.ContainsKey(currencyCode);
		}

		// Token: 0x040000CC RID: 204
		private static Dictionary<CurrencyCodeKind, Currency> _currencyDictionary = new Dictionary<CurrencyCodeKind, Currency>
		{
			{
				CurrencyCodeKind.AED,
				new Currency(CurrencyCodeKind.AED, "United Arab Emirates dirham", "¤", 2)
			},
			{
				CurrencyCodeKind.AFN,
				new Currency(CurrencyCodeKind.AFN, "Afghani", "¤", 2)
			},
			{
				CurrencyCodeKind.ALL,
				new Currency(CurrencyCodeKind.ALL, "Lek", "¤", 2)
			},
			{
				CurrencyCodeKind.AMD,
				new Currency(CurrencyCodeKind.AMD, "Armenian dram", "¤", 2)
			},
			{
				CurrencyCodeKind.ANG,
				new Currency(CurrencyCodeKind.ANG, "Netherlands Antillean guilder", "ƒ", 2)
			},
			{
				CurrencyCodeKind.AOA,
				new Currency(CurrencyCodeKind.AOA, "Kwanza", "Kz", 2)
			},
			{
				CurrencyCodeKind.ARS,
				new Currency(CurrencyCodeKind.ARS, "Argentine peso", "$", 2, CurrencyRoundingKind.Argentinian)
			},
			{
				CurrencyCodeKind.AUD,
				new Currency(CurrencyCodeKind.AUD, "Australian dollar", "$", 2)
			},
			{
				CurrencyCodeKind.AWG,
				new Currency(CurrencyCodeKind.AWG, "Aruban guilder", "ƒ", 2)
			},
			{
				CurrencyCodeKind.AZN,
				new Currency(CurrencyCodeKind.AZN, "Azerbaijanian manat", "m", 2)
			},
			{
				CurrencyCodeKind.BAM,
				new Currency(CurrencyCodeKind.BAM, "Convertible marks", "KM", 2)
			},
			{
				CurrencyCodeKind.BBD,
				new Currency(CurrencyCodeKind.BBD, "Barbados dollar", "Bds$", 2)
			},
			{
				CurrencyCodeKind.BDT,
				new Currency(CurrencyCodeKind.BDT, "Bangladeshi taka", "¤", 2)
			},
			{
				CurrencyCodeKind.BGN,
				new Currency(CurrencyCodeKind.BGN, "Bulgarian lev", "¤", 2)
			},
			{
				CurrencyCodeKind.BHD,
				new Currency(CurrencyCodeKind.BHD, "Bahraini dinar", "BD", 3)
			},
			{
				CurrencyCodeKind.BIF,
				new Currency(CurrencyCodeKind.BIF, "Burundian franc", "Fbu", 0)
			},
			{
				CurrencyCodeKind.BMD,
				new Currency(CurrencyCodeKind.BMD, "Bermudian dollar", "BD$", 2)
			},
			{
				CurrencyCodeKind.BND,
				new Currency(CurrencyCodeKind.BND, "Brunei dollar", "B$", 2)
			},
			{
				CurrencyCodeKind.BOB,
				new Currency(CurrencyCodeKind.BOB, "Boliviano", "Bs.", 2)
			},
			{
				CurrencyCodeKind.BOV,
				new Currency(CurrencyCodeKind.BOV, "Bolivian Mvdol (funds code)", "¤", 2)
			},
			{
				CurrencyCodeKind.BRL,
				new Currency(CurrencyCodeKind.BRL, "Brazilian real", "R$", 2)
			},
			{
				CurrencyCodeKind.BSD,
				new Currency(CurrencyCodeKind.BSD, "Bahamian dollar", "B$", 2)
			},
			{
				CurrencyCodeKind.BTN,
				new Currency(CurrencyCodeKind.BTN, "Ngultrum", "Nu.", 2)
			},
			{
				CurrencyCodeKind.BWP,
				new Currency(CurrencyCodeKind.BWP, "Pula", "P", 2)
			},
			{
				CurrencyCodeKind.BYR,
				new Currency(CurrencyCodeKind.BYR, "Belarussian ruble", "Br", 0)
			},
			{
				CurrencyCodeKind.BZD,
				new Currency(CurrencyCodeKind.BZD, "Belize dollar", "BZ$", 2)
			},
			{
				CurrencyCodeKind.CAD,
				new Currency(CurrencyCodeKind.CAD, "Canadian dollar", "$", 2)
			},
			{
				CurrencyCodeKind.CDF,
				new Currency(CurrencyCodeKind.CDF, "Franc Congolais", "F", 2)
			},
			{
				CurrencyCodeKind.CHE,
				new Currency(CurrencyCodeKind.CHE, "WIR euro (complementary currency)", "¤", 2)
			},
			{
				CurrencyCodeKind.CHF,
				new Currency(CurrencyCodeKind.CHF, "Swiss franc", "CHF", 2, CurrencyRoundingKind.Swiss)
			},
			{
				CurrencyCodeKind.CHW,
				new Currency(CurrencyCodeKind.CHW, "WIR franc (complementary currency)", "¤", 2)
			},
			{
				CurrencyCodeKind.CLF,
				new Currency(CurrencyCodeKind.CLF, "Unidad de Fomento (funds code)", "¤", 0)
			},
			{
				CurrencyCodeKind.CLP,
				new Currency(CurrencyCodeKind.CLP, "Chilean peso", "$", 0)
			},
			{
				CurrencyCodeKind.CNY,
				new Currency(CurrencyCodeKind.CNY, "Renminbi", "¥", 2)
			},
			{
				CurrencyCodeKind.COP,
				new Currency(CurrencyCodeKind.COP, "Colombian peso", "$", 2)
			},
			{
				CurrencyCodeKind.COU,
				new Currency(CurrencyCodeKind.COU, "Unidad de Valor Real", "¤", 2)
			},
			{
				CurrencyCodeKind.CRC,
				new Currency(CurrencyCodeKind.CRC, "Costa Rican colon", "¢", 2)
			},
			{
				CurrencyCodeKind.CUP,
				new Currency(CurrencyCodeKind.CUP, "Cuban peso", "$", 2)
			},
			{
				CurrencyCodeKind.CVE,
				new Currency(CurrencyCodeKind.CVE, "Cape Verde escudo", "$", 2)
			},
			{
				CurrencyCodeKind.CZK,
				new Currency(CurrencyCodeKind.CZK, "Czech koruna", "Kc", 2)
			},
			{
				CurrencyCodeKind.DJF,
				new Currency(CurrencyCodeKind.DJF, "Djibouti franc", "Fdj", 0)
			},
			{
				CurrencyCodeKind.DKK,
				new Currency(CurrencyCodeKind.DKK, "Danish krone", "kr", 2)
			},
			{
				CurrencyCodeKind.DOP,
				new Currency(CurrencyCodeKind.DOP, "Dominican peso", "RD$", 2)
			},
			{
				CurrencyCodeKind.DZD,
				new Currency(CurrencyCodeKind.DZD, "Algerian dinar", "DA", 2)
			},
			{
				CurrencyCodeKind.EEK,
				new Currency(CurrencyCodeKind.EEK, "Kroon", "¤", 2)
			},
			{
				CurrencyCodeKind.EGP,
				new Currency(CurrencyCodeKind.EGP, "Egyptian pound", "LE", 2)
			},
			{
				CurrencyCodeKind.ERN,
				new Currency(CurrencyCodeKind.ERN, "Nakfa", "Nfk", 2)
			},
			{
				CurrencyCodeKind.ETB,
				new Currency(CurrencyCodeKind.ETB, "Ethiopian birr", "Br", 2)
			},
			{
				CurrencyCodeKind.EUR,
				new Currency(CurrencyCodeKind.EUR, "Euro", "€", 2)
			},
			{
				CurrencyCodeKind.FJD,
				new Currency(CurrencyCodeKind.FJD, "Fiji dollar", "FJ$", 2)
			},
			{
				CurrencyCodeKind.FKP,
				new Currency(CurrencyCodeKind.FKP, "Falkland Islands pound", "£", 2)
			},
			{
				CurrencyCodeKind.GBP,
				new Currency(CurrencyCodeKind.GBP, "Pound sterling", "£", 2)
			},
			{
				CurrencyCodeKind.GEL,
				new Currency(CurrencyCodeKind.GEL, "Lari", "¤", 2)
			},
			{
				CurrencyCodeKind.GHS,
				new Currency(CurrencyCodeKind.GHS, "Cedi", "¤", 2)
			},
			{
				CurrencyCodeKind.GIP,
				new Currency(CurrencyCodeKind.GIP, "Gibraltar pound", "£", 2)
			},
			{
				CurrencyCodeKind.GMD,
				new Currency(CurrencyCodeKind.GMD, "Dalasi", "D", 2)
			},
			{
				CurrencyCodeKind.GNF,
				new Currency(CurrencyCodeKind.GNF, "Guinea franc", "FG", 0)
			},
			{
				CurrencyCodeKind.GTQ,
				new Currency(CurrencyCodeKind.GTQ, "Quetzal", "Q", 2)
			},
			{
				CurrencyCodeKind.GYD,
				new Currency(CurrencyCodeKind.GYD, "Guyana dollar", "$", 2)
			},
			{
				CurrencyCodeKind.HKD,
				new Currency(CurrencyCodeKind.HKD, "Hong Kong dollar", "HK$", 2)
			},
			{
				CurrencyCodeKind.HNL,
				new Currency(CurrencyCodeKind.HNL, "Lempira", "L", 2)
			},
			{
				CurrencyCodeKind.HRK,
				new Currency(CurrencyCodeKind.HRK, "Croatian kuna", "kn", 2)
			},
			{
				CurrencyCodeKind.HTG,
				new Currency(CurrencyCodeKind.HTG, "Haiti gourde", "G", 2)
			},
			{
				CurrencyCodeKind.HUF,
				new Currency(CurrencyCodeKind.HUF, "Forint", "Ft", 2)
			},
			{
				CurrencyCodeKind.IDR,
				new Currency(CurrencyCodeKind.IDR, "Rupiah", "Rp", 2)
			},
			{
				CurrencyCodeKind.ILS,
				new Currency(CurrencyCodeKind.ILS, "Israeli new sheqel", "?", 2)
			},
			{
				CurrencyCodeKind.INR,
				new Currency(CurrencyCodeKind.INR, "Indian rupee", "Rs", 2)
			},
			{
				CurrencyCodeKind.IQD,
				new Currency(CurrencyCodeKind.IQD, "Iraqi dinar", "¤", 3)
			},
			{
				CurrencyCodeKind.IRR,
				new Currency(CurrencyCodeKind.IRR, "Iranian rial", "¤", 2)
			},
			{
				CurrencyCodeKind.ISK,
				new Currency(CurrencyCodeKind.ISK, "Iceland krona", "kr", 0)
			},
			{
				CurrencyCodeKind.JMD,
				new Currency(CurrencyCodeKind.JMD, "Jamaican dollar", "$", 2)
			},
			{
				CurrencyCodeKind.JOD,
				new Currency(CurrencyCodeKind.JOD, "Jordanian dinar", "¤", 3)
			},
			{
				CurrencyCodeKind.JPY,
				new Currency(CurrencyCodeKind.JPY, "Japanese yen", "¥", 0)
			},
			{
				CurrencyCodeKind.KES,
				new Currency(CurrencyCodeKind.KES, "Kenyan shilling", "KSh", 2)
			},
			{
				CurrencyCodeKind.KGS,
				new Currency(CurrencyCodeKind.KGS, "Som", "¤", 2)
			},
			{
				CurrencyCodeKind.KHR,
				new Currency(CurrencyCodeKind.KHR, "Riel", "¤", 2)
			},
			{
				CurrencyCodeKind.KMF,
				new Currency(CurrencyCodeKind.KMF, "Comoro franc", "¤", 0)
			},
			{
				CurrencyCodeKind.KPW,
				new Currency(CurrencyCodeKind.KPW, "North Korean won", "?", 2)
			},
			{
				CurrencyCodeKind.KRW,
				new Currency(CurrencyCodeKind.KRW, "South Korean won", "?", 0)
			},
			{
				CurrencyCodeKind.KWD,
				new Currency(CurrencyCodeKind.KWD, "Kuwaiti dinar", "¤", 3)
			},
			{
				CurrencyCodeKind.KYD,
				new Currency(CurrencyCodeKind.KYD, "Cayman Islands dollar", "$", 2)
			},
			{
				CurrencyCodeKind.KZT,
				new Currency(CurrencyCodeKind.KZT, "Tenge", "¤", 2)
			},
			{
				CurrencyCodeKind.LAK,
				new Currency(CurrencyCodeKind.LAK, "Kip", "¤", 2)
			},
			{
				CurrencyCodeKind.LBP,
				new Currency(CurrencyCodeKind.LBP, "Lebanese pound", "¤", 2)
			},
			{
				CurrencyCodeKind.LKR,
				new Currency(CurrencyCodeKind.LKR, "Sri Lanka rupee", "Rs", 2)
			},
			{
				CurrencyCodeKind.LRD,
				new Currency(CurrencyCodeKind.LRD, "Liberian dollar", "L$", 2)
			},
			{
				CurrencyCodeKind.LSL,
				new Currency(CurrencyCodeKind.LSL, "Loti", "¤", 2)
			},
			{
				CurrencyCodeKind.LTL,
				new Currency(CurrencyCodeKind.LTL, "Lithuanian litas", "Lt", 2)
			},
			{
				CurrencyCodeKind.LVL,
				new Currency(CurrencyCodeKind.LVL, "Latvian lats", "Ls", 2)
			},
			{
				CurrencyCodeKind.LYD,
				new Currency(CurrencyCodeKind.LYD, "Libyan dinar", "LD", 3)
			},
			{
				CurrencyCodeKind.MAD,
				new Currency(CurrencyCodeKind.MAD, "Moroccan dirham", "¤", 2)
			},
			{
				CurrencyCodeKind.MDL,
				new Currency(CurrencyCodeKind.MDL, "Moldovan leu", "¤", 2)
			},
			{
				CurrencyCodeKind.MGA,
				new Currency(CurrencyCodeKind.MGA, "Malagasy ariary", "¤", 1)
			},
			{
				CurrencyCodeKind.MKD,
				new Currency(CurrencyCodeKind.MKD, "Denar", "¤", 2)
			},
			{
				CurrencyCodeKind.MMK,
				new Currency(CurrencyCodeKind.MMK, "Kyat", "K", 2)
			},
			{
				CurrencyCodeKind.MNT,
				new Currency(CurrencyCodeKind.MNT, "Tugrik", "¤", 2)
			},
			{
				CurrencyCodeKind.MOP,
				new Currency(CurrencyCodeKind.MOP, "Pataca", "MOP$", 2)
			},
			{
				CurrencyCodeKind.MRO,
				new Currency(CurrencyCodeKind.MRO, "Ouguiya", "UM", 1)
			},
			{
				CurrencyCodeKind.MUR,
				new Currency(CurrencyCodeKind.MUR, "Mauritius rupee", "Rs", 2)
			},
			{
				CurrencyCodeKind.MVR,
				new Currency(CurrencyCodeKind.MVR, "Rufiyaa", "Rf", 2)
			},
			{
				CurrencyCodeKind.MWK,
				new Currency(CurrencyCodeKind.MWK, "Kwacha", "MK", 2)
			},
			{
				CurrencyCodeKind.MXN,
				new Currency(CurrencyCodeKind.MXN, "Mexican peso", "$", 2)
			},
			{
				CurrencyCodeKind.MXV,
				new Currency(CurrencyCodeKind.MXV, "Mexican Unidad de Inversion (UDI) (funds code)", "¤", 2)
			},
			{
				CurrencyCodeKind.MYR,
				new Currency(CurrencyCodeKind.MYR, "Malaysian ringgit", "RM", 2)
			},
			{
				CurrencyCodeKind.MZN,
				new Currency(CurrencyCodeKind.MZN, "Metical", "MTn", 2)
			},
			{
				CurrencyCodeKind.NAD,
				new Currency(CurrencyCodeKind.NAD, "Namibian dollar", "N$", 2)
			},
			{
				CurrencyCodeKind.NGN,
				new Currency(CurrencyCodeKind.NGN, "Naira", "?", 2)
			},
			{
				CurrencyCodeKind.NIO,
				new Currency(CurrencyCodeKind.NIO, "Cordoba oro", "C$", 2)
			},
			{
				CurrencyCodeKind.NOK,
				new Currency(CurrencyCodeKind.NOK, "Norwegian krone", "kr", 2)
			},
			{
				CurrencyCodeKind.NPR,
				new Currency(CurrencyCodeKind.NPR, "Nepalese rupee", "Rs", 2)
			},
			{
				CurrencyCodeKind.NZD,
				new Currency(CurrencyCodeKind.NZD, "New Zealand dollar", "$", 2)
			},
			{
				CurrencyCodeKind.OMR,
				new Currency(CurrencyCodeKind.OMR, "Rial Omani", "¤", 3)
			},
			{
				CurrencyCodeKind.PAB,
				new Currency(CurrencyCodeKind.PAB, "Balboa", "B/.", 2)
			},
			{
				CurrencyCodeKind.PEN,
				new Currency(CurrencyCodeKind.PEN, "Nuevo sol", "S/.", 2)
			},
			{
				CurrencyCodeKind.PGK,
				new Currency(CurrencyCodeKind.PGK, "Kina", "K", 2)
			},
			{
				CurrencyCodeKind.PHP,
				new Currency(CurrencyCodeKind.PHP, "Philippine peso", "P", 2)
			},
			{
				CurrencyCodeKind.PKR,
				new Currency(CurrencyCodeKind.PKR, "Pakistan rupee", "Rs.", 2)
			},
			{
				CurrencyCodeKind.PLN,
				new Currency(CurrencyCodeKind.PLN, "Zloty", "zl", 2)
			},
			{
				CurrencyCodeKind.PYG,
				new Currency(CurrencyCodeKind.PYG, "Guarani", "¤", 0)
			},
			{
				CurrencyCodeKind.QAR,
				new Currency(CurrencyCodeKind.QAR, "Qatari rial", "QR", 2)
			},
			{
				CurrencyCodeKind.RON,
				new Currency(CurrencyCodeKind.RON, "Romanian new leu", "L", 2)
			},
			{
				CurrencyCodeKind.RSD,
				new Currency(CurrencyCodeKind.RSD, "Serbian dinar", "RSD", 2)
			},
			{
				CurrencyCodeKind.RUB,
				new Currency(CurrencyCodeKind.RUB, "Russian ruble", "PP", 2)
			},
			{
				CurrencyCodeKind.RWF,
				new Currency(CurrencyCodeKind.RWF, "Rwanda franc", "RF", 0)
			},
			{
				CurrencyCodeKind.SAR,
				new Currency(CurrencyCodeKind.SAR, "Saudi riyal", "SR", 2)
			},
			{
				CurrencyCodeKind.SBD,
				new Currency(CurrencyCodeKind.SBD, "Solomon Islands dollar", "SI$", 2)
			},
			{
				CurrencyCodeKind.SCR,
				new Currency(CurrencyCodeKind.SCR, "Seychelles rupee", "SR", 2)
			},
			{
				CurrencyCodeKind.SDG,
				new Currency(CurrencyCodeKind.SDG, "Sudanese pound", "¤", 2)
			},
			{
				CurrencyCodeKind.SEK,
				new Currency(CurrencyCodeKind.SEK, "Swedish krona", "kr", 2)
			},
			{
				CurrencyCodeKind.SGD,
				new Currency(CurrencyCodeKind.SGD, "Singapore dollar", "S$", 2)
			},
			{
				CurrencyCodeKind.SHP,
				new Currency(CurrencyCodeKind.SHP, "Saint Helena pound", "£", 2)
			},
			{
				CurrencyCodeKind.SKK,
				new Currency(CurrencyCodeKind.SKK, "Slovak koruna", "Sk", 2)
			},
			{
				CurrencyCodeKind.SLL,
				new Currency(CurrencyCodeKind.SLL, "Leone", "Le", 2)
			},
			{
				CurrencyCodeKind.SOS,
				new Currency(CurrencyCodeKind.SOS, "Somali shilling", "So.", 2)
			},
			{
				CurrencyCodeKind.SRD,
				new Currency(CurrencyCodeKind.SRD, "Surinam dollar", "$", 2)
			},
			{
				CurrencyCodeKind.STD,
				new Currency(CurrencyCodeKind.STD, "Dobra", "Db", 2)
			},
			{
				CurrencyCodeKind.SYP,
				new Currency(CurrencyCodeKind.SYP, "Syrian pound", "¤", 2)
			},
			{
				CurrencyCodeKind.SZL,
				new Currency(CurrencyCodeKind.SZL, "Lilangeni", "L", 2)
			},
			{
				CurrencyCodeKind.THB,
				new Currency(CurrencyCodeKind.THB, "Baht", "¤", 2)
			},
			{
				CurrencyCodeKind.TJS,
				new Currency(CurrencyCodeKind.TJS, "Somoni", "¤", 2)
			},
			{
				CurrencyCodeKind.TMM,
				new Currency(CurrencyCodeKind.TMM, "Manat", "m", 2)
			},
			{
				CurrencyCodeKind.TND,
				new Currency(CurrencyCodeKind.TND, "Tunisian dinar", "DT", 3)
			},
			{
				CurrencyCodeKind.TOP,
				new Currency(CurrencyCodeKind.TOP, "Pa'anga", "T$", 2)
			},
			{
				CurrencyCodeKind.TRY,
				new Currency(CurrencyCodeKind.TRY, "New Turkish lira", "YTL", 2)
			},
			{
				CurrencyCodeKind.TTD,
				new Currency(CurrencyCodeKind.TTD, "Trinidad and Tobago dollar", "$", 2)
			},
			{
				CurrencyCodeKind.TWD,
				new Currency(CurrencyCodeKind.TWD, "New Taiwan dollar", "$", 2)
			},
			{
				CurrencyCodeKind.TZS,
				new Currency(CurrencyCodeKind.TZS, "Tanzanian shilling", "x/y", 2)
			},
			{
				CurrencyCodeKind.UAH,
				new Currency(CurrencyCodeKind.UAH, "Hryvnia", "¤", 2)
			},
			{
				CurrencyCodeKind.UGX,
				new Currency(CurrencyCodeKind.UGX, "Uganda shilling", "USh", 2)
			},
			{
				CurrencyCodeKind.USD,
				new Currency(CurrencyCodeKind.USD, "US dollar", "$", 2)
			},
			{
				CurrencyCodeKind.USN,
				new Currency(CurrencyCodeKind.USN, "United States dollar (next day) (funds code)", "$", 2)
			},
			{
				CurrencyCodeKind.USS,
				new Currency(CurrencyCodeKind.USS, "United States dollar (same day) (funds code)", "$", 2)
			},
			{
				CurrencyCodeKind.UYU,
				new Currency(CurrencyCodeKind.UYU, "Peso Uruguayo", "$", 2)
			},
			{
				CurrencyCodeKind.UZS,
				new Currency(CurrencyCodeKind.UZS, "Uzbekistan som", "¤", 2)
			},
			{
				CurrencyCodeKind.VEF,
				new Currency(CurrencyCodeKind.VEF, "Venezuelan bolívar fuerte", "Bs.F.", 2)
			},
			{
				CurrencyCodeKind.VND,
				new Currency(CurrencyCodeKind.VND, "Vietnamese dong", "¤", 2)
			},
			{
				CurrencyCodeKind.VUV,
				new Currency(CurrencyCodeKind.VUV, "Vatu", "Vt", 0)
			},
			{
				CurrencyCodeKind.WST,
				new Currency(CurrencyCodeKind.WST, "Samoan tala", "WS$", 2)
			},
			{
				CurrencyCodeKind.XAF,
				new Currency(CurrencyCodeKind.XAF, "CFA franc BEAC", "FCFA", 0)
			},
			{
				CurrencyCodeKind.XAG,
				new Currency(CurrencyCodeKind.XAG, "Silver (one troy ounce)", "¤", 0)
			},
			{
				CurrencyCodeKind.XAU,
				new Currency(CurrencyCodeKind.XAU, "Gold (one troy ounce)", "¤", 0)
			},
			{
				CurrencyCodeKind.XBA,
				new Currency(CurrencyCodeKind.XBA, "European Composite Unit (EURCO) (bond market unit)", "¤", 0)
			},
			{
				CurrencyCodeKind.XBB,
				new Currency(CurrencyCodeKind.XBB, "European Monetary Unit (E.M.U.-6) (bond market unit)", "¤", 0)
			},
			{
				CurrencyCodeKind.XBC,
				new Currency(CurrencyCodeKind.XBC, "European Unit of Account 9 (E.U.A.-9) (bond market unit)", "¤", 0)
			},
			{
				CurrencyCodeKind.XBD,
				new Currency(CurrencyCodeKind.XBD, "European Unit of Account 17 (E.U.A.-17) (bond market unit)", "¤", 0)
			},
			{
				CurrencyCodeKind.XCD,
				new Currency(CurrencyCodeKind.XCD, "East Caribbean dollar", "$", 2)
			},
			{
				CurrencyCodeKind.XDR,
				new Currency(CurrencyCodeKind.XDR, "Special Drawing Rights", "¤", 0)
			},
			{
				CurrencyCodeKind.XOF,
				new Currency(CurrencyCodeKind.XOF, "CFA Franc BCEAO", "CFA", 0)
			},
			{
				CurrencyCodeKind.XPD,
				new Currency(CurrencyCodeKind.XPD, "Palladium (one troy ounce)", "¤", 0)
			},
			{
				CurrencyCodeKind.XPF,
				new Currency(CurrencyCodeKind.XPF, "CFP franc", "F", 0)
			},
			{
				CurrencyCodeKind.XPT,
				new Currency(CurrencyCodeKind.XPT, "Platinum (one troy ounce)", "¤", 0)
			},
			{
				CurrencyCodeKind.XTS,
				new Currency(CurrencyCodeKind.XTS, "Code reserved for testing purposes", "¤", 0)
			},
			{
				CurrencyCodeKind.XXX,
				new Currency(CurrencyCodeKind.XXX, "No currency", "¤", 0)
			},
			{
				CurrencyCodeKind.YER,
				new Currency(CurrencyCodeKind.YER, "Yemeni rial", "¤", 2)
			},
			{
				CurrencyCodeKind.ZAR,
				new Currency(CurrencyCodeKind.ZAR, "South African rand", "R", 2)
			},
			{
				CurrencyCodeKind.ZMK,
				new Currency(CurrencyCodeKind.ZMK, "Kwacha", "ZK", 2)
			},
			{
				CurrencyCodeKind.ZWD,
				new Currency(CurrencyCodeKind.ZWD, "Zimbabwe dollar", "$", 2)
			}
		};
	}
}

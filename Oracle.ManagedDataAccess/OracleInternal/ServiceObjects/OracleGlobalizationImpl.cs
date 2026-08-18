using System;
using System.Data;
using System.Globalization;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B2 RID: 434
	internal class OracleGlobalizationImpl : IDisposable, ICloneable
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x000B2114 File Offset: 0x000B0314
		internal OracleGlobalizationImpl()
		{
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000B211C File Offset: 0x000B031C
		internal OracleGlobalizationImpl(int lcid)
		{
			int num = 0;
			OracleGlobalizationImpl.GetLocaleSpecificNLSValues(lcid, ref this.m_language, ref this.m_territory, ref this.m_timeZone, ref num);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000B214C File Offset: 0x000B034C
		internal static string GetNLSLANG(int lcid)
		{
			if (lcid <= 6203)
			{
				if (lcid <= 3098)
				{
					if (lcid <= 2092)
					{
						if (lcid <= 2060)
						{
							switch (lcid)
							{
							case 1025:
								return "ARABIC_SAUDI ARABIA";
							case 1026:
								return "BULGARIAN_BULGARIA";
							case 1027:
								return "CATALAN_CATALONIA";
							case 1028:
								return "TRADITIONAL CHINESE_TAIWAN";
							case 1029:
								return "CZECH_CZECH REPUBLIC";
							case 1030:
								return "DANISH_DENMARK";
							case 1031:
								return "GERMAN_GERMANY";
							case 1032:
								return "GREEK_GREECE";
							case 1033:
								return "AMERICAN_AMERICA";
							case 1034:
								return "SPANISH_SPAIN";
							case 1035:
								return "FINNISH_FINLAND";
							case 1036:
								return "FRENCH_FRANCE";
							case 1037:
								return "HEBREW_ISRAEL";
							case 1038:
								return "HUNGARIAN_HUNGARY";
							case 1039:
								return "ICELANDIC_ICELAND";
							case 1040:
								return "ITALIAN_ITALY";
							case 1041:
								return "JAPANESE_JAPAN";
							case 1042:
								return "KOREAN_KOREA";
							case 1043:
								return "DUTCH_THE NETHERLANDS";
							case 1044:
								return "NORWEGIAN_NORWAY";
							case 1045:
								return "POLISH_POLAND";
							case 1046:
								return "BRAZILIAN PORTUGUESE_BRAZIL";
							case 1047:
							case 1064:
							case 1070:
							case 1072:
							case 1073:
							case 1075:
							case 1084:
							case 1085:
							case 1090:
							case 1096:
							case 1101:
							case 1105:
							case 1107:
							case 1108:
							case 1109:
							case 1112:
							case 1113:
							case 1115:
							case 1116:
							case 1117:
							case 1118:
							case 1119:
							case 1120:
							case 1121:
							case 1122:
							case 1123:
							case 1124:
							case 1126:
							case 1127:
							case 1128:
							case 1129:
							case 1130:
							case 1133:
							case 1134:
							case 1135:
							case 1136:
							case 1137:
							case 1138:
							case 1139:
							case 1140:
							case 1141:
							case 1142:
							case 1143:
							case 1144:
							case 1145:
							case 1146:
							case 1147:
							case 1148:
							case 1149:
							case 1150:
							case 1151:
							case 1152:
								break;
							case 1048:
								return "ROMANIAN_ROMANIA";
							case 1049:
								return "RUSSIAN_RUSSIA";
							case 1050:
								return "CROATIAN_CROATIA";
							case 1051:
								return "SLOVAK_SLOVAKIA";
							case 1052:
								return "AMERICAN_AMERICA";
							case 1053:
								return "SWEDISH_SWEDEN";
							case 1054:
								return "THAI_THAILAND";
							case 1055:
								return "TURKISH_TURKEY";
							case 1056:
								return "AMERICAN_AMERICA";
							case 1057:
								return "INDONESIAN_INDONESIA";
							case 1058:
								return "UKRAINIAN_UKRAINE";
							case 1059:
								return "AMERICAN_AMERICA";
							case 1060:
								return "SLOVENIAN_SLOVENIA";
							case 1061:
								return "ESTONIAN_ESTONIA";
							case 1062:
								return "LATVIAN_LATVIA";
							case 1063:
								return "LITHUANIAN_LITHUANIA";
							case 1065:
								return "AMERICAN_AMERICA";
							case 1066:
								return "VIETNAMESE_VIETNAM";
							case 1067:
								return "AMERICAN_AMERICA";
							case 1068:
								return "AZERBAIJANI_AZERBAIJAN";
							case 1069:
								return "AMERICAN_AMERICA";
							case 1071:
								return "MACEDONIAN_FYR MACEDONIA";
							case 1074:
								return "AMERICAN_SOUTH AFRICA";
							case 1076:
								return "AMERICAN_SOUTH AFRICA";
							case 1077:
								return "AMERICAN_SOUTH AFRICA";
							case 1078:
								return "AMERICAN_SOUTH AFRICA";
							case 1079:
								return "AMERICAN_AMERICA";
							case 1080:
								return "AMERICAN_AMERICA";
							case 1081:
								return "HINDI_INDIA";
							case 1082:
								return "AMERICAN_AMERICA";
							case 1083:
								return "AMERICAN_NORWAY";
							case 1086:
								return "MALAY_MALAYSIA";
							case 1087:
								return "CYRILLIC KAZAKH_KAZAKHSTAN";
							case 1088:
								return "AMERICAN_AMERICA";
							case 1089:
								return "AMERICAN_AMERICA";
							case 1091:
								return "LATIN UZBEK_UZBEKISTAN";
							case 1092:
								return "AMERICAN_AMERICA";
							case 1093:
								return "BANGLA_INDIA";
							case 1094:
								return "PUNJABI_INDIA";
							case 1095:
								return "GUJARATI_INDIA";
							case 1097:
								return "TAMIL_INDIA";
							case 1098:
								return "TELUGU_INDIA";
							case 1099:
								return "KANNADA_INDIA";
							case 1100:
								return "MALAYALAM_INDIA";
							case 1102:
								return "MARATHI_INDIA";
							case 1103:
								return "AMERICAN_AMERICA";
							case 1104:
								return "AMERICAN_AMERICA";
							case 1106:
								return "AMERICAN_UNITED KINGDOM";
							case 1110:
								return "AMERICAN_SPAIN";
							case 1111:
								return "AMERICAN_AMERICA";
							case 1114:
								return "AMERICAN_SYRIA";
							case 1125:
								return "AMERICAN_AMERICA";
							case 1131:
								return "AMERICAN_AMERICA";
							case 1132:
								return "AMERICAN_AMERICA";
							case 1153:
								return "AMERICAN_NEW ZEALAND";
							default:
								if (lcid == 2049)
								{
									return "ARABIC_IRAQ";
								}
								switch (lcid)
								{
								case 2052:
									return "SIMPLIFIED CHINESE_CHINA";
								case 2055:
									return "GERMAN_SWITZERLAND";
								case 2057:
									return "ENGLISH_UNITED KINGDOM";
								case 2058:
									return "MEXICAN SPANISH_MEXICO";
								case 2060:
									return "FRENCH_BELGIUM";
								}
								break;
							}
						}
						else if (lcid <= 2074)
						{
							switch (lcid)
							{
							case 2064:
								return "ITALIAN_SWITZERLAND";
							case 2065:
							case 2066:
							case 2069:
								break;
							case 2067:
								return "DUTCH_BELGIUM";
							case 2068:
								return "NORWEGIAN_NORWAY";
							case 2070:
								return "PORTUGUESE_PORTUGAL";
							default:
								if (lcid == 2074)
								{
									return "LATIN SERBIAN_SERBIA AND MONTENEGRO";
								}
								break;
							}
						}
						else
						{
							if (lcid == 2077)
							{
								return "SWEDISH_FINLAND";
							}
							if (lcid == 2092)
							{
								return "AZERBAIJANI_AZERBAIJAN";
							}
						}
					}
					else if (lcid <= 2115)
					{
						if (lcid == 2107)
						{
							return "AMERICAN_SWEDEN";
						}
						if (lcid == 2110)
						{
							return "MALAY_MALAYSIA";
						}
						if (lcid == 2115)
						{
							return "CYRILLIC UZBEK_UZBEKISTAN";
						}
					}
					else if (lcid <= 3073)
					{
						if (lcid == 2155)
						{
							return "AMERICAN_ECUADOR";
						}
						if (lcid == 3073)
						{
							return "ARABIC_EGYPT";
						}
					}
					else
					{
						switch (lcid)
						{
						case 3076:
							return "TRADITIONAL CHINESE_HONG KONG";
						case 3077:
						case 3078:
						case 3080:
						case 3083:
							break;
						case 3079:
							return "GERMAN_AUSTRIA";
						case 3081:
							return "ENGLISH_AUSTRALIA";
						case 3082:
							return "SPANISH_SPAIN";
						case 3084:
							return "CANADIAN FRENCH_CANADA";
						default:
							if (lcid == 3098)
							{
								return "CYRILLIC SERBIAN_SERBIA AND MONTENEGRO";
							}
							break;
						}
					}
				}
				else if (lcid <= 5121)
				{
					if (lcid <= 4097)
					{
						if (lcid == 3131)
						{
							return "AMERICAN_FINLAND";
						}
						if (lcid == 3179)
						{
							return "AMERICAN_PERU";
						}
						if (lcid == 4097)
						{
							return "ARABIC_LIBYA";
						}
					}
					else if (lcid <= 4122)
					{
						switch (lcid)
						{
						case 4100:
							return "SIMPLIFIED CHINESE_SINGAPORE";
						case 4101:
						case 4102:
						case 4104:
						case 4107:
							break;
						case 4103:
							return "GERMAN_LUXEMBOURG";
						case 4105:
							return "ENGLISH_CANADA";
						case 4106:
							return "LATIN AMERICAN SPANISH_GUATEMALA";
						case 4108:
							return "FRENCH_SWITZERLAND";
						default:
							if (lcid == 4122)
							{
								return "CROATIAN_CROATIA";
							}
							break;
						}
					}
					else
					{
						if (lcid == 4155)
						{
							return "AMERICAN_NORWAY";
						}
						if (lcid == 5121)
						{
							return "ARABIC_ALGERIA";
						}
					}
				}
				else if (lcid <= 5179)
				{
					switch (lcid)
					{
					case 5124:
						return "SIMPLIFIED CHINESE_CHINA";
					case 5125:
					case 5126:
					case 5128:
					case 5131:
						break;
					case 5127:
						return "GERMAN_GERMANY";
					case 5129:
						return "ENGLISH_NEW ZEALAND";
					case 5130:
						return "LATIN AMERICAN SPANISH_COSTA RICA";
					case 5132:
						return "FRENCH_LUXEMBOURG";
					default:
						if (lcid == 5146)
						{
							return "AMERICAN_AMERICA";
						}
						if (lcid == 5179)
						{
							return "AMERICAN_SWEDEN";
						}
						break;
					}
				}
				else if (lcid <= 6156)
				{
					if (lcid == 6145)
					{
						return "ARABIC_MOROCCO";
					}
					switch (lcid)
					{
					case 6153:
						return "ENGLISH_IRELAND";
					case 6154:
						return "LATIN AMERICAN SPANISH_PANAMA";
					case 6156:
						return "FRENCH_FRANCE";
					}
				}
				else
				{
					if (lcid == 6170)
					{
						return "LATIN SERBIAN_SERBIA AND MONTENEGRO";
					}
					if (lcid == 6203)
					{
						return "AMERICAN_NORWAY";
					}
				}
			}
			else if (lcid <= 11274)
			{
				if (lcid <= 8251)
				{
					if (lcid <= 7194)
					{
						if (lcid == 7169)
						{
							return "ARABIC_TUNISIA";
						}
						switch (lcid)
						{
						case 7177:
							return "ENGLISH_SOUTH AFRICA";
						case 7178:
							return "LATIN AMERICAN SPANISH_AMERICA";
						default:
							if (lcid == 7194)
							{
								return "CYRILLIC SERBIAN_SERBIA AND MONTENEGRO";
							}
							break;
						}
					}
					else if (lcid <= 8193)
					{
						if (lcid == 7227)
						{
							return "AMERICAN_SWEDEN";
						}
						if (lcid == 8193)
						{
							return "ARABIC_OMAN";
						}
					}
					else
					{
						switch (lcid)
						{
						case 8201:
							return "ENGLISH_UNITED KINGDOM";
						case 8202:
							return "LATIN AMERICAN SPANISH_VENEZUELA";
						default:
							if (lcid == 8251)
							{
								return "AMERICAN_FINLAND";
							}
							break;
						}
					}
				}
				else if (lcid <= 9275)
				{
					if (lcid == 9217)
					{
						return "ARABIC_YEMEN";
					}
					switch (lcid)
					{
					case 9225:
						return "ENGLISH_UNITED KINGDOM";
					case 9226:
						return "LATIN AMERICAN SPANISH_COLOMBIA";
					default:
						if (lcid == 9275)
						{
							return "AMERICAN_FINLAND";
						}
						break;
					}
				}
				else if (lcid <= 10250)
				{
					if (lcid == 10241)
					{
						return "ARABIC_SYRIA";
					}
					switch (lcid)
					{
					case 10249:
						return "ENGLISH_UNITED KINGDOM";
					case 10250:
						return "LATIN AMERICAN SPANISH_PERU";
					}
				}
				else
				{
					if (lcid == 11265)
					{
						return "ARABIC_JORDAN";
					}
					switch (lcid)
					{
					case 11273:
						return "ENGLISH_UNITED KINGDOM";
					case 11274:
						return "LATIN AMERICAN SPANISH_ARGENTINA";
					}
				}
			}
			else if (lcid <= 15361)
			{
				if (lcid <= 13313)
				{
					if (lcid == 12289)
					{
						return "ARABIC_LEBANON";
					}
					switch (lcid)
					{
					case 12297:
						return "ENGLISH_UNITED KINGDOM";
					case 12298:
						return "LATIN AMERICAN SPANISH_ECUADOR";
					default:
						if (lcid == 13313)
						{
							return "ARABIC_KUWAIT";
						}
						break;
					}
				}
				else if (lcid <= 14337)
				{
					switch (lcid)
					{
					case 13321:
						return "ENGLISH_PHILIPPINES";
					case 13322:
						return "LATIN AMERICAN SPANISH_CHILE";
					default:
						if (lcid == 14337)
						{
							return "ARABIC_UNITED ARAB EMIRATES";
						}
						break;
					}
				}
				else
				{
					if (lcid == 14346)
					{
						return "LATIN AMERICAN SPANISH_AMERICA";
					}
					if (lcid == 15361)
					{
						return "ARABIC_BAHRAIN";
					}
				}
			}
			else if (lcid <= 16394)
			{
				if (lcid == 15370)
				{
					return "LATIN AMERICAN SPANISH_AMERICA";
				}
				if (lcid == 16385)
				{
					return "ARABIC_QATAR";
				}
				if (lcid == 16394)
				{
					return "LATIN AMERICAN SPANISH_AMERICA";
				}
			}
			else if (lcid <= 18442)
			{
				if (lcid == 17418)
				{
					return "LATIN AMERICAN SPANISH_EL SALVADOR";
				}
				if (lcid == 18442)
				{
					return "LATIN AMERICAN SPANISH_AMERICA";
				}
			}
			else
			{
				if (lcid == 19466)
				{
					return "LATIN AMERICAN SPANISH_NICARAGUA";
				}
				if (lcid == 20490)
				{
					return "LATIN AMERICAN SPANISH_PUERTO RICO";
				}
			}
			return "AMERICAN_AMERICA";
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000B2EC8 File Offset: 0x000B10C8
		internal static void GetLocaleSpecificNLSValues(int lcid, ref string language, ref string territory, ref string timezone, ref int zoneID)
		{
			string nlslang = OracleGlobalizationImpl.GetNLSLANG(lcid);
			string[] array = nlslang.Split(new char[]
			{
				'_'
			});
			language = array[0];
			territory = array[1];
			TimeZoneInfo local = TimeZoneInfo.Local;
			if (ZoneIdMap.isValidRegion(local.Id))
			{
				zoneID = ZoneIdMap.GetRegionID(local.Id);
				timezone = TimeStamp.GetZoneName(zoneID);
				return;
			}
			DateTime now = DateTime.Now;
			string arg = Math.Abs(local.GetUtcOffset(now).Hours).ToString("00", CultureInfo.InvariantCulture);
			string arg2 = Math.Abs(local.GetUtcOffset(now).Minutes).ToString("00", CultureInfo.InvariantCulture);
			string arg3 = (local.BaseUtcOffset >= TimeSpan.Zero) ? "+" : "-";
			timezone = string.Format("{0}{1}:{2}", arg3, arg, arg2);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000B2FB8 File Offset: 0x000B11B8
		internal static string CreateAlterSessionBlockForOAUTH(int lcid, ref int zoneID)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			OracleGlobalizationImpl.GetLocaleSpecificNLSValues(lcid, ref empty, ref empty2, ref empty3, ref zoneID);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ALTER SESSION SET ", new object[0]);
			stringBuilder.AppendFormat(" NLS_LANGUAGE='{0}' ", empty);
			stringBuilder.AppendFormat(" NLS_TERRITORY='{0}' ", empty2);
			stringBuilder.AppendFormat(" TIME_ZONE='{0}' ", empty3);
			return stringBuilder.ToString();
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000B302C File Offset: 0x000B122C
		internal void RefreshFrom(OracleGlobalizationImpl oraGlob)
		{
			this.m_calendar = oraGlob.m_calendar;
			this.m_comparison = oraGlob.m_comparison;
			this.m_currency = oraGlob.m_currency;
			this.m_dateFormat = oraGlob.m_dateFormat;
			this.m_dateLanguage = oraGlob.m_dateLanguage;
			this.m_dualCurrency = oraGlob.m_dualCurrency;
			this.m_isoCurrency = oraGlob.m_isoCurrency;
			this.m_language = oraGlob.m_language;
			this.m_lengthSemantics = oraGlob.m_lengthSemantics;
			this.m_nCharConvException = oraGlob.m_nCharConvException;
			this.m_numericCharacters = oraGlob.m_numericCharacters;
			this.m_sort = oraGlob.m_sort;
			this.m_territory = oraGlob.m_territory;
			this.m_timeStampFormat = oraGlob.m_timeStampFormat;
			this.m_timeStampTZFormat = oraGlob.m_timeStampTZFormat;
			this.m_timeZone = oraGlob.m_timeZone;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000B30FC File Offset: 0x000B12FC
		internal void AlterSession(OracleGlobalizationImpl oraGlob, OracleConnection con)
		{
			if (oraGlob == null || (this != oraGlob && oraGlob.Equals(this)))
			{
				return;
			}
			if (this.m_cmd == null)
			{
				this.m_cmd = new OracleCommand
				{
					CommandType = CommandType.Text
				};
			}
			this.m_cmd.Connection = con;
			this.m_cmd.Parameters.Clear();
			bool flag = false;
			bool flag2 = false;
			StringBuilder stringBuilder = new StringBuilder("DECLARE ");
			stringBuilder.AppendFormat(" err_code VARCHAR2(2000); ", new object[0]);
			stringBuilder.AppendFormat(" err_msg VARCHAR2(2000); ", new object[0]);
			stringBuilder.AppendFormat(" BEGIN ", new object[0]);
			if (this == oraGlob)
			{
				this.CreateDerivedSelectBlock(oraGlob, stringBuilder, true, true, true);
				stringBuilder.AppendFormat(" SELECT '0' into :p_err_code from dual; ", new object[0]);
				stringBuilder.AppendFormat(" SELECT '0' into :p_err_msg from dual; ", new object[0]);
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder("");
				this.CreateSingleAlterSessionBlock(stringBuilder2, oraGlob);
				if (stringBuilder2.Length == 0)
				{
					return;
				}
				stringBuilder.AppendFormat(" EXECUTE IMMEDIATE 'ALTER SESSION SET {0} ';", stringBuilder2);
				flag = (string.Compare(oraGlob.m_language, this.m_language, true) != 0);
				flag2 = (string.Compare(oraGlob.m_territory, this.m_territory) != 0);
				this.CreateDerivedSelectBlock(oraGlob, stringBuilder, false, flag, flag2);
				stringBuilder.AppendFormat(" SELECT '0' into :p_err_code from dual; ", new object[0]);
				stringBuilder.AppendFormat(" SELECT '0' into :p_err_msg from dual; ", new object[0]);
				stringBuilder.AppendFormat(" EXCEPTION WHEN OTHERS THEN ", new object[0]);
				stringBuilder.AppendFormat(" err_code := substr(SQLCODE, 1, 2000); ", new object[0]);
				stringBuilder.AppendFormat(" err_msg := substr(SQLERRM, 1, 2000); ", new object[0]);
				this.CreateDerivedSelectBlock(oraGlob, stringBuilder, false, flag, flag2);
				stringBuilder.AppendFormat(" SELECT err_code into :p_err_code from dual; ", new object[0]);
				stringBuilder.AppendFormat(" SELECT err_msg into :p_err_msg from dual; ", new object[0]);
			}
			stringBuilder.AppendFormat(" END;", new object[0]);
			this.m_cmd.CommandText = stringBuilder.ToString();
			OracleParameter oracleParameter = null;
			OracleParameter oracleParameter2 = null;
			OracleParameter oracleParameter3 = null;
			OracleParameter oracleParameter4 = null;
			if (this == oraGlob || (oraGlob.m_calendar != null && string.Compare(oraGlob.m_calendar, this.m_calendar, true) != 0))
			{
				oracleParameter = new OracleParameter("p_nls_calendar", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter);
			}
			if (this == oraGlob || (oraGlob.m_comparison != null && string.Compare(oraGlob.m_comparison, this.m_comparison, true) != 0))
			{
				oracleParameter2 = new OracleParameter("p_nls_comp", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter2);
			}
			if (this == oraGlob || (oraGlob.m_lengthSemantics != null && string.Compare(oraGlob.m_lengthSemantics, this.m_lengthSemantics, true) != 0))
			{
				oracleParameter3 = new OracleParameter("p_nls_length_semantics", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter3);
			}
			if (this == oraGlob || !oraGlob.m_nCharConvException == this.m_nCharConvException)
			{
				oracleParameter4 = new OracleParameter("p_nls_nchar_conv_excep", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter4);
			}
			OracleParameter oracleParameter5 = null;
			OracleParameter oracleParameter6 = null;
			if (flag || this == oraGlob || (oraGlob.m_dateLanguage != null && string.Compare(oraGlob.m_dateLanguage, this.m_dateLanguage, true) != 0))
			{
				oracleParameter5 = new OracleParameter("p_nls_date_lang", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter5);
			}
			if (flag || this == oraGlob || (oraGlob.m_sort != null && string.Compare(oraGlob.m_sort, this.m_sort, true) != 0))
			{
				oracleParameter6 = new OracleParameter("p_nls_sort", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter6);
			}
			OracleParameter oracleParameter7 = null;
			OracleParameter oracleParameter8 = null;
			OracleParameter oracleParameter9 = null;
			OracleParameter oracleParameter10 = null;
			OracleParameter oracleParameter11 = null;
			OracleParameter oracleParameter12 = null;
			OracleParameter oracleParameter13 = null;
			if (flag2 || this == oraGlob || (oraGlob.m_currency != null && string.Compare(oraGlob.m_currency, this.m_currency, true) != 0))
			{
				oracleParameter7 = new OracleParameter("p_nls_currency", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter7);
			}
			if (flag2 || this == oraGlob || (oraGlob.m_dateFormat != null && string.Compare(oraGlob.m_dateFormat, this.m_dateFormat, true) != 0))
			{
				oracleParameter8 = new OracleParameter("p_nls_date_format", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter8);
			}
			if (flag2 || this == oraGlob || (oraGlob.m_isoCurrency != null && string.Compare(oraGlob.m_isoCurrency, this.m_isoCurrency, true) != 0))
			{
				oracleParameter9 = new OracleParameter("p_nls_iso_currency", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter9);
			}
			if (flag2 || this == oraGlob || (oraGlob.m_numericCharacters != null && string.Compare(oraGlob.m_numericCharacters, this.m_numericCharacters, true) != 0))
			{
				oracleParameter10 = new OracleParameter("p_nls_numeric_chars", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter10);
			}
			if (flag2 || this == oraGlob || (oraGlob.m_dualCurrency != null && string.Compare(oraGlob.m_dualCurrency, this.m_dualCurrency, true) != 0))
			{
				oracleParameter11 = new OracleParameter("p_nls_dual_currency", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter11);
			}
			if (flag2 || this == oraGlob || (oraGlob.m_timeStampFormat != null && string.Compare(oraGlob.m_timeStampFormat, this.m_timeStampFormat, true) != 0))
			{
				oracleParameter12 = new OracleParameter("p_nls_timestamp", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter12);
			}
			if (flag2 || this == oraGlob || (oraGlob.m_timeStampTZFormat != null && string.Compare(oraGlob.m_timeStampTZFormat, this.m_timeStampTZFormat, true) != 0))
			{
				oracleParameter13 = new OracleParameter("p_nls_timestamp_tz", OracleDbType.Varchar2, 40, "", ParameterDirection.Output);
				this.m_cmd.Parameters.Add(oracleParameter13);
			}
			OracleParameter oracleParameter14 = new OracleParameter("p_err_code", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output);
			this.m_cmd.Parameters.Add(oracleParameter14);
			OracleParameter oracleParameter15 = new OracleParameter("p_err_msg", OracleDbType.Varchar2, 2000, "", ParameterDirection.Output);
			this.m_cmd.Parameters.Add(oracleParameter15);
			this.m_cmd.ExecuteNonQuery();
			if (flag || this == oraGlob)
			{
				this.m_language = oraGlob.m_language;
			}
			if (flag2 || this == oraGlob)
			{
				this.m_territory = oraGlob.m_territory;
			}
			if (oraGlob.m_timeZone != null && string.Compare(oraGlob.m_timeZone, this.m_timeZone, true) != 0)
			{
				this.m_timeZone = oraGlob.m_timeZone;
			}
			if (this == oraGlob || (oraGlob.m_calendar != null && string.Compare(oraGlob.m_calendar, this.m_calendar, true) != 0))
			{
				this.m_calendar = oracleParameter.Value.ToString();
			}
			if (this == oraGlob || (oraGlob.m_comparison != null && string.Compare(oraGlob.m_comparison, this.m_comparison, true) != 0))
			{
				this.m_comparison = oracleParameter2.Value.ToString();
			}
			if (this == oraGlob || (oraGlob.m_lengthSemantics != null && string.Compare(oraGlob.m_lengthSemantics, this.m_lengthSemantics, true) != 0))
			{
				this.m_lengthSemantics = oracleParameter3.Value.ToString();
			}
			if (this == oraGlob || !oraGlob.m_nCharConvException == this.m_nCharConvException)
			{
				this.m_nCharConvException = oracleParameter4.Value.ToString().ToLowerInvariant().Equals("true");
			}
			if (flag || this == oraGlob || (oraGlob.m_dateLanguage != null && string.Compare(oraGlob.m_dateLanguage, this.m_dateLanguage, true) != 0))
			{
				this.m_dateLanguage = oracleParameter5.Value.ToString();
			}
			if (flag || this == oraGlob || (oraGlob.m_sort != null && string.Compare(oraGlob.m_sort, this.m_sort, true) != 0))
			{
				this.m_sort = oracleParameter6.Value.ToString();
			}
			if (flag2 || this == oraGlob || (oraGlob.m_currency != null && string.Compare(oraGlob.m_currency, this.m_currency, true) != 0))
			{
				this.m_currency = oracleParameter7.Value.ToString();
			}
			if (flag2 || this == oraGlob || (oraGlob.m_dateFormat != null && string.Compare(oraGlob.m_dateFormat, this.m_dateFormat, true) != 0))
			{
				this.m_dateFormat = oracleParameter8.Value.ToString();
			}
			if (flag2 || this == oraGlob || (oraGlob.m_isoCurrency != null && string.Compare(oraGlob.m_isoCurrency, this.m_isoCurrency, true) != 0))
			{
				this.m_isoCurrency = oracleParameter9.Value.ToString();
			}
			if (flag2 || this == oraGlob || (oraGlob.m_numericCharacters != null && string.Compare(oraGlob.m_numericCharacters, this.m_numericCharacters, true) != 0))
			{
				this.m_numericCharacters = oracleParameter10.Value.ToString();
			}
			if (flag2 || this == oraGlob || (oraGlob.m_dualCurrency != null && string.Compare(oraGlob.m_dualCurrency, this.m_dualCurrency, true) != 0))
			{
				this.m_dualCurrency = oracleParameter11.Value.ToString();
			}
			if (flag2 || this == oraGlob || (oraGlob.m_timeStampFormat != null && string.Compare(oraGlob.m_timeStampFormat, this.m_timeStampFormat, true) != 0))
			{
				this.m_timeStampFormat = oracleParameter12.Value.ToString();
			}
			if (flag2 || this == oraGlob || (oraGlob.m_timeStampTZFormat != null && string.Compare(oraGlob.m_timeStampTZFormat, this.m_timeStampTZFormat, true) != 0))
			{
				this.m_timeStampTZFormat = oracleParameter13.Value.ToString();
			}
			string value = oracleParameter14.Value.ToString();
			this.m_error_code = Convert.ToInt32(value);
			this.m_error_msg = oracleParameter15.Value.ToString();
			try
			{
				if (this.m_error_msg != null && !this.m_error_msg.ToLowerInvariant().Equals("0"))
				{
					if (flag || this == oraGlob)
					{
						this.m_language = oraGlob.m_language;
						this.m_dateLanguage = oracleParameter5.Value.ToString();
						this.m_sort = oracleParameter6.Value.ToString();
					}
					if (flag2 || this == oraGlob)
					{
						this.m_territory = oraGlob.m_territory;
						this.m_currency = oracleParameter7.Value.ToString();
						this.m_dateFormat = oracleParameter8.Value.ToString();
						this.m_isoCurrency = oracleParameter9.Value.ToString();
						this.m_numericCharacters = oracleParameter10.Value.ToString();
						this.m_dualCurrency = oracleParameter11.Value.ToString();
						this.m_timeStampFormat = oracleParameter12.Value.ToString();
						this.m_timeStampTZFormat = oracleParameter13.Value.ToString();
					}
					this.m_cmd.Connection = null;
					throw new OracleException(this.m_error_code, string.Empty, string.Empty, this.m_error_msg);
				}
			}
			finally
			{
				foreach (object obj in this.m_cmd.Parameters)
				{
					OracleParameter oracleParameter16 = (OracleParameter)obj;
					oracleParameter16.Dispose();
				}
			}
			this.m_cmd.Connection = null;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000B3C04 File Offset: 0x000B1E04
		internal void CreateSingleAlterSessionBlock(StringBuilder sqlCmd, OracleGlobalizationImpl oraGlob)
		{
			if (oraGlob.m_language != null && string.Compare(oraGlob.m_language, this.m_language, true) != 0)
			{
				sqlCmd.AppendFormat("  NLS_LANGUAGE=\"{0}\" ", oraGlob.m_language);
			}
			if (oraGlob.m_territory != null && string.Compare(oraGlob.m_territory, this.m_territory, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_TERRITORY=\"{0}\" ", oraGlob.m_territory);
			}
			if (oraGlob.m_calendar != null && string.Compare(oraGlob.m_calendar, this.m_calendar, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_CALENDAR=\"{0}\" ", oraGlob.m_calendar);
			}
			if (oraGlob.m_dateLanguage != null && string.Compare(oraGlob.m_dateLanguage, this.m_dateLanguage, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_DATE_LANGUAGE=\"{0}\" ", oraGlob.m_dateLanguage);
			}
			if (oraGlob.m_currency != null && string.Compare(oraGlob.m_currency, this.m_currency, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_CURRENCY=\"{0}\" ", oraGlob.m_currency);
			}
			if (oraGlob.m_dateFormat != null && string.Compare(oraGlob.m_dateFormat, this.m_dateFormat, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_DATE_FORMAT=''{0}'' ", oraGlob.m_dateFormat);
			}
			if (oraGlob.m_isoCurrency != null && string.Compare(oraGlob.m_isoCurrency, this.m_isoCurrency, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_ISO_CURRENCY=\"{0}\" ", oraGlob.m_isoCurrency);
			}
			if (oraGlob.m_numericCharacters != null && string.Compare(oraGlob.m_numericCharacters, this.m_numericCharacters, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_NUMERIC_CHARACTERS=\"{0}\" ", oraGlob.m_numericCharacters);
			}
			if (oraGlob.m_sort != null && string.Compare(oraGlob.m_sort, this.m_sort, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_SORT=\"{0}\" ", oraGlob.m_sort);
			}
			if (oraGlob.m_comparison != null && string.Compare(oraGlob.m_comparison, this.m_comparison, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_COMP=\"{0}\" ", oraGlob.m_comparison);
			}
			if (oraGlob.m_dualCurrency != null && string.Compare(oraGlob.m_dualCurrency, this.m_dualCurrency, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_DUAL_CURRENCY=\"{0}\" ", oraGlob.m_dualCurrency);
			}
			if (oraGlob.m_lengthSemantics != null && string.Compare(oraGlob.m_lengthSemantics, this.m_lengthSemantics, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_LENGTH_SEMANTICS=\"{0}\" ", oraGlob.m_lengthSemantics);
			}
			if (!oraGlob.m_nCharConvException == this.m_nCharConvException)
			{
				sqlCmd.AppendFormat(" NLS_NCHAR_CONV_EXCP=\"{0}\" ", oraGlob.m_nCharConvException);
			}
			if (oraGlob.m_timeStampFormat != null && string.Compare(oraGlob.m_timeStampFormat, this.m_timeStampFormat, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_TIMESTAMP_FORMAT=''{0}'' ", oraGlob.m_timeStampFormat);
			}
			if (oraGlob.m_timeStampTZFormat != null && string.Compare(oraGlob.m_timeStampTZFormat, this.m_timeStampTZFormat, true) != 0)
			{
				sqlCmd.AppendFormat(" NLS_TIMESTAMP_TZ_FORMAT=''{0}'' ", oraGlob.m_timeStampTZFormat);
			}
			if (oraGlob.m_timeZone != null && string.Compare(oraGlob.m_timeZone, this.m_timeZone, true) != 0)
			{
				if (oraGlob.m_timeZone.ToLowerInvariant() == "local")
				{
					sqlCmd.AppendFormat(" TIME_ZONE=local ", new object[0]);
					return;
				}
				if (oraGlob.m_timeZone.ToLowerInvariant() == "dbtimezone")
				{
					sqlCmd.AppendFormat(" TIME_ZONE=dbtimezone ", new object[0]);
					return;
				}
				if (oraGlob.m_timeZone.Length > 0)
				{
					sqlCmd.AppendFormat(" TIME_ZONE=''{0}'' ", oraGlob.m_timeZone);
				}
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000B3F50 File Offset: 0x000B2150
		internal void CreateDerivedSelectBlock(OracleGlobalizationImpl oraGlob, StringBuilder sqlCmd, bool onConnect, bool nls_lang, bool nls_territory)
		{
			if (onConnect || (oraGlob.m_calendar != null && string.Compare(oraGlob.m_calendar, this.m_calendar, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_calendar from nls_session_parameters where PARAMETER='NLS_CALENDAR';", new object[0]);
			}
			if (onConnect || (oraGlob.m_comparison != null && string.Compare(oraGlob.m_comparison, this.m_comparison, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_comp from nls_session_parameters where PARAMETER='NLS_COMP';", new object[0]);
			}
			if (onConnect || (oraGlob.m_lengthSemantics != null && string.Compare(oraGlob.m_lengthSemantics, this.m_lengthSemantics, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_length_semantics from nls_session_parameters where PARAMETER='NLS_LENGTH_SEMANTICS';", new object[0]);
			}
			if (onConnect || !oraGlob.m_nCharConvException == this.m_nCharConvException)
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_nchar_conv_excep from nls_session_parameters where PARAMETER='NLS_NCHAR_CONV_EXCP';", new object[0]);
			}
			if (nls_lang || (oraGlob.m_dateLanguage != null && string.Compare(oraGlob.m_dateLanguage, this.m_dateLanguage, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_date_lang from nls_session_parameters where PARAMETER='NLS_DATE_LANGUAGE';", new object[0]);
			}
			if (nls_lang || (oraGlob.m_sort != null && string.Compare(oraGlob.m_sort, this.m_sort, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_sort from nls_session_parameters where PARAMETER='NLS_SORT';", new object[0]);
			}
			if (nls_territory || (oraGlob.m_currency != null && string.Compare(oraGlob.m_currency, this.m_currency, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_currency from nls_session_parameters where PARAMETER='NLS_CURRENCY';", new object[0]);
			}
			if (nls_territory || (oraGlob.m_dateFormat != null && string.Compare(oraGlob.m_dateFormat, this.m_dateFormat, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_date_format from nls_session_parameters where PARAMETER='NLS_DATE_FORMAT';", new object[0]);
			}
			if (nls_territory || (oraGlob.m_isoCurrency != null && string.Compare(oraGlob.m_isoCurrency, this.m_isoCurrency, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_iso_currency from nls_session_parameters where PARAMETER='NLS_ISO_CURRENCY';", new object[0]);
			}
			if (nls_territory || (oraGlob.m_numericCharacters != null && string.Compare(oraGlob.m_numericCharacters, this.m_numericCharacters, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_numeric_chars from nls_session_parameters where PARAMETER='NLS_NUMERIC_CHARACTERS';", new object[0]);
			}
			if (nls_territory || (oraGlob.m_dualCurrency != null && string.Compare(oraGlob.m_dualCurrency, this.m_dualCurrency, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_dual_currency from nls_session_parameters where PARAMETER='NLS_DUAL_CURRENCY';", new object[0]);
			}
			if (nls_territory || (oraGlob.m_timeStampFormat != null && string.Compare(oraGlob.m_timeStampFormat, this.m_timeStampFormat, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_timestamp from nls_session_parameters where PARAMETER='NLS_TIMESTAMP_FORMAT';", new object[0]);
			}
			if (nls_territory || (oraGlob.m_timeStampTZFormat != null && string.Compare(oraGlob.m_timeStampTZFormat, this.m_timeStampTZFormat, true) != 0))
			{
				sqlCmd.AppendFormat(" SELECT VALUE into :p_nls_timestamp_tz from nls_session_parameters where PARAMETER='NLS_TIMESTAMP_TZ_FORMAT';", new object[0]);
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000B41D8 File Offset: 0x000B23D8
		public bool Equals(OracleGlobalizationImpl oraGlob)
		{
			return oraGlob != null && (oraGlob.m_language != null && string.Compare(oraGlob.m_language, this.m_language, true) == 0 && oraGlob.m_territory != null && string.Compare(oraGlob.m_territory, this.m_territory, true) == 0 && oraGlob.m_calendar != null && string.Compare(oraGlob.m_calendar, this.m_calendar, true) == 0 && oraGlob.m_comparison != null && string.Compare(oraGlob.m_comparison, this.m_comparison, true) == 0 && oraGlob.m_currency != null && string.Compare(oraGlob.m_currency, this.m_currency, true) == 0 && oraGlob.m_dualCurrency != null && string.Compare(oraGlob.m_dualCurrency, this.m_dualCurrency, true) == 0 && oraGlob.m_isoCurrency != null && string.Compare(oraGlob.m_isoCurrency, this.m_isoCurrency, true) == 0 && oraGlob.m_dateFormat != null && string.Compare(oraGlob.m_dateFormat, this.m_dateFormat, true) == 0 && oraGlob.m_dateLanguage != null && string.Compare(oraGlob.m_dateLanguage, this.m_dateLanguage, true) == 0 && oraGlob.m_lengthSemantics != null && string.Compare(oraGlob.m_lengthSemantics, this.m_lengthSemantics, true) == 0 && oraGlob.m_nCharConvException == this.m_nCharConvException && oraGlob.m_numericCharacters != null && string.Compare(oraGlob.m_numericCharacters, this.m_numericCharacters, true) == 0 && oraGlob.m_timeStampFormat != null && string.Compare(oraGlob.m_timeStampFormat, this.m_timeStampFormat, true) == 0 && oraGlob.m_timeStampTZFormat != null && string.Compare(oraGlob.m_timeStampTZFormat, this.m_timeStampTZFormat, true) == 0 && oraGlob.m_timeZone != null && string.Compare(oraGlob.m_timeZone, this.m_timeZone, true) == 0) && oraGlob.m_sort != null && string.Compare(oraGlob.m_sort, this.m_sort, true) == 0;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000B43E4 File Offset: 0x000B25E4
		public object Clone()
		{
			return new OracleGlobalizationImpl
			{
				m_calendar = this.m_calendar,
				m_comparison = this.m_comparison,
				m_currency = this.m_currency,
				m_dateFormat = this.m_dateFormat,
				m_dateLanguage = this.m_dateLanguage,
				m_dualCurrency = this.m_dualCurrency,
				m_isoCurrency = this.m_isoCurrency,
				m_language = this.m_language,
				m_lengthSemantics = this.m_lengthSemantics,
				m_nCharConvException = this.m_nCharConvException,
				m_numericCharacters = this.m_numericCharacters,
				m_sort = this.m_sort,
				m_territory = this.m_territory,
				m_timeStampFormat = this.m_timeStampFormat,
				m_timeStampTZFormat = this.m_timeStampTZFormat,
				m_timeZone = this.m_timeZone
			};
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000B44B8 File Offset: 0x000B26B8
		public void Dispose()
		{
			if (this.m_cmd != null && !this.m_cmd.m_disposed)
			{
				this.m_cmd.Dispose();
			}
		}

		// Token: 0x0400132D RID: 4909
		private const string DEFAULT_LANGUAGE_TERRITORY = "AMERICAN_AMERICA";

		// Token: 0x0400132E RID: 4910
		private const string DEFAULT_LANGUAGE = "AMERICAN";

		// Token: 0x0400132F RID: 4911
		internal string m_calendar;

		// Token: 0x04001330 RID: 4912
		internal string m_comparison;

		// Token: 0x04001331 RID: 4913
		internal string m_currency;

		// Token: 0x04001332 RID: 4914
		internal string m_dateFormat;

		// Token: 0x04001333 RID: 4915
		internal string m_dateLanguage;

		// Token: 0x04001334 RID: 4916
		internal string m_dualCurrency;

		// Token: 0x04001335 RID: 4917
		internal string m_isoCurrency;

		// Token: 0x04001336 RID: 4918
		internal string m_language;

		// Token: 0x04001337 RID: 4919
		internal string m_lengthSemantics;

		// Token: 0x04001338 RID: 4920
		internal bool m_nCharConvException;

		// Token: 0x04001339 RID: 4921
		internal string m_numericCharacters;

		// Token: 0x0400133A RID: 4922
		internal string m_sort;

		// Token: 0x0400133B RID: 4923
		internal string m_territory;

		// Token: 0x0400133C RID: 4924
		internal string m_timeStampFormat;

		// Token: 0x0400133D RID: 4925
		internal string m_timeStampTZFormat;

		// Token: 0x0400133E RID: 4926
		internal string m_timeZone;

		// Token: 0x0400133F RID: 4927
		private string m_error_msg;

		// Token: 0x04001340 RID: 4928
		private int m_error_code;

		// Token: 0x04001341 RID: 4929
		private OracleCommand m_cmd;
	}
}

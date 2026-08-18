using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x02000531 RID: 1329
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CultureInfoConverter : TypeConverter
	{
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000E157F File Offset: 0x000DF77F
		private string DefaultCultureString
		{
			get
			{
				return SR.GetString("CultureInfoConverterDefaultCultureString");
			}
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000E158B File Offset: 0x000DF78B
		protected virtual string GetCultureName(CultureInfo culture)
		{
			return culture.Name;
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000E1593 File Offset: 0x000DF793
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000E15B1 File Offset: 0x000DF7B1
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000E15D0 File Offset: 0x000DF7D0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (this.GetCultureName(CultureInfo.InvariantCulture).Equals(""))
			{
				text = CultureInfoConverter.CultureInfoMapper.GetCultureInfoName((string)value);
			}
			CultureInfo cultureInfo = null;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			if (culture != null && culture.Equals(CultureInfo.InvariantCulture))
			{
				Thread.CurrentThread.CurrentUICulture = culture;
			}
			try
			{
				if (text == null || text.Length == 0 || string.Compare(text, this.DefaultCultureString, StringComparison.Ordinal) == 0)
				{
					cultureInfo = CultureInfo.InvariantCulture;
				}
				if (cultureInfo == null)
				{
					ICollection standardValues = this.GetStandardValues(context);
					foreach (object obj in standardValues)
					{
						CultureInfo cultureInfo2 = (CultureInfo)obj;
						if (cultureInfo2 != null && string.Compare(this.GetCultureName(cultureInfo2), text, StringComparison.Ordinal) == 0)
						{
							cultureInfo = cultureInfo2;
							break;
						}
					}
				}
				if (cultureInfo == null)
				{
					try
					{
						cultureInfo = new CultureInfo(text);
					}
					catch
					{
					}
				}
				if (cultureInfo == null)
				{
					text = text.ToLower(CultureInfo.CurrentCulture);
					foreach (object obj2 in this.values)
					{
						CultureInfo cultureInfo3 = (CultureInfo)obj2;
						if (cultureInfo3 != null && this.GetCultureName(cultureInfo3).ToLower(CultureInfo.CurrentCulture).StartsWith(text))
						{
							cultureInfo = cultureInfo3;
							break;
						}
					}
				}
			}
			finally
			{
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
			}
			if (cultureInfo == null)
			{
				throw new ArgumentException(SR.GetString("CultureInfoConverterInvalidCulture", new object[]
				{
					(string)value
				}));
			}
			return cultureInfo;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000E1760 File Offset: 0x000DF960
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				if (culture != null && culture.Equals(CultureInfo.InvariantCulture))
				{
					Thread.CurrentThread.CurrentUICulture = culture;
				}
				string result;
				try
				{
					if (value == null || value == CultureInfo.InvariantCulture)
					{
						result = this.DefaultCultureString;
					}
					else
					{
						result = this.GetCultureName((CultureInfo)value);
					}
				}
				finally
				{
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
				}
				return result;
			}
			if (destinationType == typeof(InstanceDescriptor) && value is CultureInfo)
			{
				CultureInfo cultureInfo = (CultureInfo)value;
				ConstructorInfo constructor = typeof(CultureInfo).GetConstructor(new Type[]
				{
					typeof(string)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						cultureInfo.Name
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000E186C File Offset: 0x000DFA6C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures);
				int num = Array.IndexOf<CultureInfo>(cultures, CultureInfo.InvariantCulture);
				CultureInfo[] array;
				if (num != -1)
				{
					cultures[num] = null;
					array = new CultureInfo[cultures.Length];
				}
				else
				{
					array = new CultureInfo[cultures.Length + 1];
				}
				Array.Copy(cultures, array, cultures.Length);
				Array.Sort(array, new CultureInfoConverter.CultureComparer(this));
				if (array[0] == null)
				{
					array[0] = CultureInfo.InvariantCulture;
				}
				this.values = new TypeConverter.StandardValuesCollection(array);
			}
			return this.values;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000E18E7 File Offset: 0x000DFAE7
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000E18EA File Offset: 0x000DFAEA
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400296C RID: 10604
		private TypeConverter.StandardValuesCollection values;

		// Token: 0x02000892 RID: 2194
		private class CultureComparer : IComparer
		{
			// Token: 0x0600459A RID: 17818 RVA: 0x00122222 File Offset: 0x00120422
			public CultureComparer(CultureInfoConverter cultureConverter)
			{
				this.converter = cultureConverter;
			}

			// Token: 0x0600459B RID: 17819 RVA: 0x00122234 File Offset: 0x00120434
			public int Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					string cultureName = this.converter.GetCultureName((CultureInfo)item1);
					string cultureName2 = this.converter.GetCultureName((CultureInfo)item2);
					CompareInfo compareInfo = CultureInfo.CurrentCulture.CompareInfo;
					return compareInfo.Compare(cultureName, cultureName2, CompareOptions.StringSort);
				}
			}

			// Token: 0x040037CB RID: 14283
			private CultureInfoConverter converter;
		}

		// Token: 0x02000893 RID: 2195
		private static class CultureInfoMapper
		{
			// Token: 0x0600459C RID: 17820 RVA: 0x0012228C File Offset: 0x0012048C
			public static string GetCultureInfoName(string cultureInfoDisplayName)
			{
				if (CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap == null)
				{
					CultureInfoConverter.CultureInfoMapper.InitializeCultureInfoMap();
				}
				if (CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap.ContainsKey(cultureInfoDisplayName))
				{
					return CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap[cultureInfoDisplayName];
				}
				return cultureInfoDisplayName;
			}

			// Token: 0x0600459D RID: 17821 RVA: 0x001222BC File Offset: 0x001204BC
			private static void InitializeCultureInfoMap()
			{
				CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap = new Dictionary<string, string>
				{
					{
						"Afrikaans",
						"af"
					},
					{
						"Afrikaans (South Africa)",
						"af-ZA"
					},
					{
						"Albanian",
						"sq"
					},
					{
						"Albanian (Albania)",
						"sq-AL"
					},
					{
						"Alsatian (France)",
						"gsw-FR"
					},
					{
						"Amharic (Ethiopia)",
						"am-ET"
					},
					{
						"Arabic",
						"ar"
					},
					{
						"Arabic (Algeria)",
						"ar-DZ"
					},
					{
						"Arabic (Bahrain)",
						"ar-BH"
					},
					{
						"Arabic (Egypt)",
						"ar-EG"
					},
					{
						"Arabic (Iraq)",
						"ar-IQ"
					},
					{
						"Arabic (Jordan)",
						"ar-JO"
					},
					{
						"Arabic (Kuwait)",
						"ar-KW"
					},
					{
						"Arabic (Lebanon)",
						"ar-LB"
					},
					{
						"Arabic (Libya)",
						"ar-LY"
					},
					{
						"Arabic (Morocco)",
						"ar-MA"
					},
					{
						"Arabic (Oman)",
						"ar-OM"
					},
					{
						"Arabic (Qatar)",
						"ar-QA"
					},
					{
						"Arabic (Saudi Arabia)",
						"ar-SA"
					},
					{
						"Arabic (Syria)",
						"ar-SY"
					},
					{
						"Arabic (Tunisia)",
						"ar-TN"
					},
					{
						"Arabic (U.A.E.)",
						"ar-AE"
					},
					{
						"Arabic (Yemen)",
						"ar-YE"
					},
					{
						"Armenian",
						"hy"
					},
					{
						"Armenian (Armenia)",
						"hy-AM"
					},
					{
						"Assamese (India)",
						"as-IN"
					},
					{
						"Azeri",
						"az"
					},
					{
						"Azeri (Cyrillic, Azerbaijan)",
						"az-Cyrl-AZ"
					},
					{
						"Azeri (Latin, Azerbaijan)",
						"az-Latn-AZ"
					},
					{
						"Bashkir (Russia)",
						"ba-RU"
					},
					{
						"Basque",
						"eu"
					},
					{
						"Basque (Basque)",
						"eu-ES"
					},
					{
						"Belarusian",
						"be"
					},
					{
						"Belarusian (Belarus)",
						"be-BY"
					},
					{
						"Bengali (Bangladesh)",
						"bn-BD"
					},
					{
						"Bengali (India)",
						"bn-IN"
					},
					{
						"Bosnian (Cyrillic, Bosnia and Herzegovina)",
						"bs-Cyrl-BA"
					},
					{
						"Bosnian (Latin, Bosnia and Herzegovina)",
						"bs-Latn-BA"
					},
					{
						"Breton (France)",
						"br-FR"
					},
					{
						"Bulgarian",
						"bg"
					},
					{
						"Bulgarian (Bulgaria)",
						"bg-BG"
					},
					{
						"Catalan",
						"ca"
					},
					{
						"Catalan (Catalan)",
						"ca-ES"
					},
					{
						"Chinese (Hong Kong S.A.R.)",
						"zh-HK"
					},
					{
						"Chinese (Macao S.A.R.)",
						"zh-MO"
					},
					{
						"Chinese (People's Republic of China)",
						"zh-CN"
					},
					{
						"Chinese (Simplified)",
						"zh-CHS"
					},
					{
						"Chinese (Singapore)",
						"zh-SG"
					},
					{
						"Chinese (Taiwan)",
						"zh-TW"
					},
					{
						"Chinese (Traditional)",
						"zh-CHT"
					},
					{
						"Corsican (France)",
						"co-FR"
					},
					{
						"Croatian",
						"hr"
					},
					{
						"Croatian (Croatia)",
						"hr-HR"
					},
					{
						"Croatian (Latin, Bosnia and Herzegovina)",
						"hr-BA"
					},
					{
						"Czech",
						"cs"
					},
					{
						"Czech (Czech Republic)",
						"cs-CZ"
					},
					{
						"Danish",
						"da"
					},
					{
						"Danish (Denmark)",
						"da-DK"
					},
					{
						"Dari (Afghanistan)",
						"prs-AF"
					},
					{
						"Divehi",
						"dv"
					},
					{
						"Divehi (Maldives)",
						"dv-MV"
					},
					{
						"Dutch",
						"nl"
					},
					{
						"Dutch (Belgium)",
						"nl-BE"
					},
					{
						"Dutch (Netherlands)",
						"nl-NL"
					},
					{
						"English",
						"en"
					},
					{
						"English (Australia)",
						"en-AU"
					},
					{
						"English (Belize)",
						"en-BZ"
					},
					{
						"English (Canada)",
						"en-CA"
					},
					{
						"English (Caribbean)",
						"en-029"
					},
					{
						"English (India)",
						"en-IN"
					},
					{
						"English (Ireland)",
						"en-IE"
					},
					{
						"English (Jamaica)",
						"en-JM"
					},
					{
						"English (Malaysia)",
						"en-MY"
					},
					{
						"English (New Zealand)",
						"en-NZ"
					},
					{
						"English (Republic of the Philippines)",
						"en-PH"
					},
					{
						"English (Singapore)",
						"en-SG"
					},
					{
						"English (South Africa)",
						"en-ZA"
					},
					{
						"English (Trinidad and Tobago)",
						"en-TT"
					},
					{
						"English (United Kingdom)",
						"en-GB"
					},
					{
						"English (United States)",
						"en-US"
					},
					{
						"English (Zimbabwe)",
						"en-ZW"
					},
					{
						"Estonian",
						"et"
					},
					{
						"Estonian (Estonia)",
						"et-EE"
					},
					{
						"Faroese",
						"fo"
					},
					{
						"Faroese (Faroe Islands)",
						"fo-FO"
					},
					{
						"Filipino (Philippines)",
						"fil-PH"
					},
					{
						"Finnish",
						"fi"
					},
					{
						"Finnish (Finland)",
						"fi-FI"
					},
					{
						"French",
						"fr"
					},
					{
						"French (Belgium)",
						"fr-BE"
					},
					{
						"French (Canada)",
						"fr-CA"
					},
					{
						"French (France)",
						"fr-FR"
					},
					{
						"French (Luxembourg)",
						"fr-LU"
					},
					{
						"French (Principality of Monaco)",
						"fr-MC"
					},
					{
						"French (Switzerland)",
						"fr-CH"
					},
					{
						"Frisian (Netherlands)",
						"fy-NL"
					},
					{
						"Galician",
						"gl"
					},
					{
						"Galician (Galician)",
						"gl-ES"
					},
					{
						"Georgian",
						"ka"
					},
					{
						"Georgian (Georgia)",
						"ka-GE"
					},
					{
						"German",
						"de"
					},
					{
						"German (Austria)",
						"de-AT"
					},
					{
						"German (Germany)",
						"de-DE"
					},
					{
						"German (Liechtenstein)",
						"de-LI"
					},
					{
						"German (Luxembourg)",
						"de-LU"
					},
					{
						"German (Switzerland)",
						"de-CH"
					},
					{
						"Greek",
						"el"
					},
					{
						"Greek (Greece)",
						"el-GR"
					},
					{
						"Greenlandic (Greenland)",
						"kl-GL"
					},
					{
						"Gujarati",
						"gu"
					},
					{
						"Gujarati (India)",
						"gu-IN"
					},
					{
						"Hausa (Latin, Nigeria)",
						"ha-Latn-NG"
					},
					{
						"Hebrew",
						"he"
					},
					{
						"Hebrew (Israel)",
						"he-IL"
					},
					{
						"Hindi",
						"hi"
					},
					{
						"Hindi (India)",
						"hi-IN"
					},
					{
						"Hungarian",
						"hu"
					},
					{
						"Hungarian (Hungary)",
						"hu-HU"
					},
					{
						"Icelandic",
						"is"
					},
					{
						"Icelandic (Iceland)",
						"is-IS"
					},
					{
						"Igbo (Nigeria)",
						"ig-NG"
					},
					{
						"Indonesian",
						"id"
					},
					{
						"Indonesian (Indonesia)",
						"id-ID"
					},
					{
						"Inuktitut (Latin, Canada)",
						"iu-Latn-CA"
					},
					{
						"Inuktitut (Syllabics, Canada)",
						"iu-Cans-CA"
					},
					{
						"Invariant Language (Invariant Country)",
						""
					},
					{
						"Irish (Ireland)",
						"ga-IE"
					},
					{
						"isiXhosa (South Africa)",
						"xh-ZA"
					},
					{
						"isiZulu (South Africa)",
						"zu-ZA"
					},
					{
						"Italian",
						"it"
					},
					{
						"Italian (Italy)",
						"it-IT"
					},
					{
						"Italian (Switzerland)",
						"it-CH"
					},
					{
						"Japanese",
						"ja"
					},
					{
						"Japanese (Japan)",
						"ja-JP"
					},
					{
						"K'iche (Guatemala)",
						"qut-GT"
					},
					{
						"Kannada",
						"kn"
					},
					{
						"Kannada (India)",
						"kn-IN"
					},
					{
						"Kazakh",
						"kk"
					},
					{
						"Kazakh (Kazakhstan)",
						"kk-KZ"
					},
					{
						"Khmer (Cambodia)",
						"km-KH"
					},
					{
						"Kinyarwanda (Rwanda)",
						"rw-RW"
					},
					{
						"Kiswahili",
						"sw"
					},
					{
						"Kiswahili (Kenya)",
						"sw-KE"
					},
					{
						"Konkani",
						"kok"
					},
					{
						"Konkani (India)",
						"kok-IN"
					},
					{
						"Korean",
						"ko"
					},
					{
						"Korean (Korea)",
						"ko-KR"
					},
					{
						"Kyrgyz",
						"ky"
					},
					{
						"Kyrgyz (Kyrgyzstan)",
						"ky-KG"
					},
					{
						"Lao (Lao P.D.R.)",
						"lo-LA"
					},
					{
						"Latvian",
						"lv"
					},
					{
						"Latvian (Latvia)",
						"lv-LV"
					},
					{
						"Lithuanian",
						"lt"
					},
					{
						"Lithuanian (Lithuania)",
						"lt-LT"
					},
					{
						"Lower Sorbian (Germany)",
						"dsb-DE"
					},
					{
						"Luxembourgish (Luxembourg)",
						"lb-LU"
					},
					{
						"Macedonian",
						"mk"
					},
					{
						"Macedonian (Former Yugoslav Republic of Macedonia)",
						"mk-MK"
					},
					{
						"Malay",
						"ms"
					},
					{
						"Malay (Brunei Darussalam)",
						"ms-BN"
					},
					{
						"Malay (Malaysia)",
						"ms-MY"
					},
					{
						"Malayalam (India)",
						"ml-IN"
					},
					{
						"Maltese (Malta)",
						"mt-MT"
					},
					{
						"Maori (New Zealand)",
						"mi-NZ"
					},
					{
						"Mapudungun (Chile)",
						"arn-CL"
					},
					{
						"Marathi",
						"mr"
					},
					{
						"Marathi (India)",
						"mr-IN"
					},
					{
						"Mohawk (Mohawk)",
						"moh-CA"
					},
					{
						"Mongolian",
						"mn"
					},
					{
						"Mongolian (Cyrillic, Mongolia)",
						"mn-MN"
					},
					{
						"Mongolian (Traditional Mongolian, PRC)",
						"mn-Mong-CN"
					},
					{
						"Nepali (Nepal)",
						"ne-NP"
					},
					{
						"Norwegian",
						"no"
					},
					{
						"Norwegian, Bokmål (Norway)",
						"nb-NO"
					},
					{
						"Norwegian, Nynorsk (Norway)",
						"nn-NO"
					},
					{
						"Occitan (France)",
						"oc-FR"
					},
					{
						"Oriya (India)",
						"or-IN"
					},
					{
						"Pashto (Afghanistan)",
						"ps-AF"
					},
					{
						"Persian",
						"fa"
					},
					{
						"Persian (Iran)",
						"fa-IR"
					},
					{
						"Polish",
						"pl"
					},
					{
						"Polish (Poland)",
						"pl-PL"
					},
					{
						"Portuguese",
						"pt"
					},
					{
						"Portuguese (Brazil)",
						"pt-BR"
					},
					{
						"Portuguese (Portugal)",
						"pt-PT"
					},
					{
						"Punjabi",
						"pa"
					},
					{
						"Punjabi (India)",
						"pa-IN"
					},
					{
						"Quechua (Bolivia)",
						"quz-BO"
					},
					{
						"Quechua (Ecuador)",
						"quz-EC"
					},
					{
						"Quechua (Peru)",
						"quz-PE"
					},
					{
						"Romanian",
						"ro"
					},
					{
						"Romanian (Romania)",
						"ro-RO"
					},
					{
						"Romansh (Switzerland)",
						"rm-CH"
					},
					{
						"Russian",
						"ru"
					},
					{
						"Russian (Russia)",
						"ru-RU"
					},
					{
						"Sami, Inari (Finland)",
						"smn-FI"
					},
					{
						"Sami, Lule (Norway)",
						"smj-NO"
					},
					{
						"Sami, Lule (Sweden)",
						"smj-SE"
					},
					{
						"Sami, Northern (Finland)",
						"se-FI"
					},
					{
						"Sami, Northern (Norway)",
						"se-NO"
					},
					{
						"Sami, Northern (Sweden)",
						"se-SE"
					},
					{
						"Sami, Skolt (Finland)",
						"sms-FI"
					},
					{
						"Sami, Southern (Norway)",
						"sma-NO"
					},
					{
						"Sami, Southern (Sweden)",
						"sma-SE"
					},
					{
						"Sanskrit",
						"sa"
					},
					{
						"Sanskrit (India)",
						"sa-IN"
					},
					{
						"Serbian",
						"sr"
					},
					{
						"Serbian (Cyrillic, Bosnia and Herzegovina)",
						"sr-Cyrl-BA"
					},
					{
						"Serbian (Cyrillic, Serbia)",
						"sr-Cyrl-CS"
					},
					{
						"Serbian (Latin, Bosnia and Herzegovina)",
						"sr-Latn-BA"
					},
					{
						"Serbian (Latin, Serbia)",
						"sr-Latn-CS"
					},
					{
						"Sesotho sa Leboa (South Africa)",
						"nso-ZA"
					},
					{
						"Setswana (South Africa)",
						"tn-ZA"
					},
					{
						"Sinhala (Sri Lanka)",
						"si-LK"
					},
					{
						"Slovak",
						"sk"
					},
					{
						"Slovak (Slovakia)",
						"sk-SK"
					},
					{
						"Slovenian",
						"sl"
					},
					{
						"Slovenian (Slovenia)",
						"sl-SI"
					},
					{
						"Spanish",
						"es"
					},
					{
						"Spanish (Argentina)",
						"es-AR"
					},
					{
						"Spanish (Bolivia)",
						"es-BO"
					},
					{
						"Spanish (Chile)",
						"es-CL"
					},
					{
						"Spanish (Colombia)",
						"es-CO"
					},
					{
						"Spanish (Costa Rica)",
						"es-CR"
					},
					{
						"Spanish (Dominican Republic)",
						"es-DO"
					},
					{
						"Spanish (Ecuador)",
						"es-EC"
					},
					{
						"Spanish (El Salvador)",
						"es-SV"
					},
					{
						"Spanish (Guatemala)",
						"es-GT"
					},
					{
						"Spanish (Honduras)",
						"es-HN"
					},
					{
						"Spanish (Mexico)",
						"es-MX"
					},
					{
						"Spanish (Nicaragua)",
						"es-NI"
					},
					{
						"Spanish (Panama)",
						"es-PA"
					},
					{
						"Spanish (Paraguay)",
						"es-PY"
					},
					{
						"Spanish (Peru)",
						"es-PE"
					},
					{
						"Spanish (Puerto Rico)",
						"es-PR"
					},
					{
						"Spanish (Spain)",
						"es-ES"
					},
					{
						"Spanish (United States)",
						"es-US"
					},
					{
						"Spanish (Uruguay)",
						"es-UY"
					},
					{
						"Spanish (Venezuela)",
						"es-VE"
					},
					{
						"Swedish",
						"sv"
					},
					{
						"Swedish (Finland)",
						"sv-FI"
					},
					{
						"Swedish (Sweden)",
						"sv-SE"
					},
					{
						"Syriac",
						"syr"
					},
					{
						"Syriac (Syria)",
						"syr-SY"
					},
					{
						"Tajik (Cyrillic, Tajikistan)",
						"tg-Cyrl-TJ"
					},
					{
						"Tamazight (Latin, Algeria)",
						"tzm-Latn-DZ"
					},
					{
						"Tamil",
						"ta"
					},
					{
						"Tamil (India)",
						"ta-IN"
					},
					{
						"Tatar",
						"tt"
					},
					{
						"Tatar (Russia)",
						"tt-RU"
					},
					{
						"Telugu",
						"te"
					},
					{
						"Telugu (India)",
						"te-IN"
					},
					{
						"Thai",
						"th"
					},
					{
						"Thai (Thailand)",
						"th-TH"
					},
					{
						"Tibetan (PRC)",
						"bo-CN"
					},
					{
						"Turkish",
						"tr"
					},
					{
						"Turkish (Turkey)",
						"tr-TR"
					},
					{
						"Turkmen (Turkmenistan)",
						"tk-TM"
					},
					{
						"Uighur (PRC)",
						"ug-CN"
					},
					{
						"Ukrainian",
						"uk"
					},
					{
						"Ukrainian (Ukraine)",
						"uk-UA"
					},
					{
						"Upper Sorbian (Germany)",
						"hsb-DE"
					},
					{
						"Urdu",
						"ur"
					},
					{
						"Urdu (Islamic Republic of Pakistan)",
						"ur-PK"
					},
					{
						"Uzbek",
						"uz"
					},
					{
						"Uzbek (Cyrillic, Uzbekistan)",
						"uz-Cyrl-UZ"
					},
					{
						"Uzbek (Latin, Uzbekistan)",
						"uz-Latn-UZ"
					},
					{
						"Vietnamese",
						"vi"
					},
					{
						"Vietnamese (Vietnam)",
						"vi-VN"
					},
					{
						"Welsh (United Kingdom)",
						"cy-GB"
					},
					{
						"Wolof (Senegal)",
						"wo-SN"
					},
					{
						"Yakut (Russia)",
						"sah-RU"
					},
					{
						"Yi (PRC)",
						"ii-CN"
					},
					{
						"Yoruba (Nigeria)",
						"yo-NG"
					}
				};
			}

			// Token: 0x040037CC RID: 14284
			private static volatile Dictionary<string, string> cultureInfoNameMap;
		}
	}
}

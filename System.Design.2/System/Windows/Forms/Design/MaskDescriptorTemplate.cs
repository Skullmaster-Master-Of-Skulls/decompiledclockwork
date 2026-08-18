using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000311 RID: 785
	internal class MaskDescriptorTemplate : MaskDescriptor
	{
		// Token: 0x06001EF8 RID: 7928 RVA: 0x000B9036 File Offset: 0x000B7236
		public MaskDescriptorTemplate(string mask, string name, string sample, Type validatingType, CultureInfo culture) : this(mask, name, sample, validatingType, culture, false)
		{
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000B9048 File Offset: 0x000B7248
		public MaskDescriptorTemplate(string mask, string name, string sample, Type validatingType, CultureInfo culture, bool skipValidation)
		{
			this.mask = mask;
			this.name = name;
			this.sample = sample;
			this.type = validatingType;
			this.culture = culture;
			if (skipValidation)
			{
				return;
			}
			string text;
			if (!MaskDescriptor.IsValidMaskDescriptor(this, out text))
			{
				this.mask = null;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x000B9096 File Offset: 0x000B7296
		public override string Mask
		{
			get
			{
				return this.mask;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x000B909E File Offset: 0x000B729E
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x000B90A6 File Offset: 0x000B72A6
		public override string Sample
		{
			get
			{
				return this.sample;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x000B90AE File Offset: 0x000B72AE
		public override Type ValidatingType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x000B90B6 File Offset: 0x000B72B6
		public override CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000B90C0 File Offset: 0x000B72C0
		public static List<MaskDescriptor> GetLocalizedMaskDescriptors(CultureInfo culture)
		{
			MaskDescriptorTemplate.ValidMaskDescriptorList validMaskDescriptorList = new MaskDescriptorTemplate.ValidMaskDescriptorList();
			string text = culture.Parent.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1153205826U)
			{
				if (num <= 1069317731U)
				{
					if (num != 5974475U)
					{
						if (num != 89862570U)
						{
							if (num != 1069317731U)
							{
								goto IL_199;
							}
							if (!(text == "zh-CHT"))
							{
								goto IL_199;
							}
						}
						else
						{
							if (!(text == "zh-Hans"))
							{
								goto IL_199;
							}
							goto IL_95B;
						}
					}
					else if (!(text == "zh-Hant"))
					{
						goto IL_199;
					}
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("(00)9000-0000", "電話號碼", "01 2345678", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000-000-000", "行動電話號碼", "1234567890", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000/00/00", "西曆簡短日期", "20050620", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000年90月90日", "西曆完整日期", "2005年10月 2日", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000/00/00 00:00:00", "西曆簡短日期時間", "20050611043322", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000年90月90日 90時90分", "西曆完整日期時間", "2005年 6月 2日  6時22分", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("L000000000", "身分證字號", "A123456789", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("90:00", "時間格式", " 633", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("90時90分", "中文時間格式", " 6時 3分", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("99000", "3+2郵遞區號", "80407", null, culture));
					goto IL_DED;
				}
				if (num != 1092248970U)
				{
					if (num != 1111292255U)
					{
						if (num != 1153205826U)
						{
							goto IL_199;
						}
						if (!(text == "zh-CHS"))
						{
							goto IL_199;
						}
					}
					else
					{
						if (!(text == "ko"))
						{
							goto IL_199;
						}
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("99999", "숫자(5자리)", "12345", typeof(int), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("(999)9000-0000", "전화 번호", "01234567890", null, culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-9000-0000", "휴대폰 번호", "01012345678", null, culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("9000-0000", "지역 번호를 제외한 전화 번호", "12345678", null, culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000-00-00", "간단한 날짜", "20050620", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000-00-00 90:00", "간단한 날짜 및 시간", "2005-06-20  9:20", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000년90월90일 90시90분", "자세한 날짜 및 시간", "2005년 6월20일  6시33분", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("000000-0000000", "주민 등록 번호", "1234561234567", null, culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("90:00", "시간", " 633", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-000", "우편 번호", "182021", null, culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("90시90분", "자세한 시간", " 6시33분", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000년 90월 90일", "자세한 날짜", "20050620", typeof(DateTime), culture));
						goto IL_DED;
					}
				}
				else
				{
					if (!(text == "en"))
					{
						goto IL_199;
					}
					goto IL_1A0;
				}
				IL_95B:
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("99999", "数字(最长5位)", "12345", typeof(int), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("(900)9000-0000", "（区号）电话号码", " 1234567890", null, culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("9000-0000", "电话号码", "12345678", null, culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-0000-0000", "移动电话号码", "12345678901", null, culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000-00-00", "短日期格式", "20050611", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000年90月90日", "长日期格式", "20051211", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000-00-00 90:00:00", "短日期时间", "2005-06-11  6:30:22", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000年90月90日 90时00分", "长日期时间", "2005年 6月11日  6时33分", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("000000-000000-000", "15位身份证号码", "123456789012345", null, culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("000000-00000000-000A", "18位身份证号码", "123456789012345678", null, culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("90:00", "时间格式", " 633", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("90时90分", "中文时间格式", " 6时33分", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("000000", "邮政编码", "100080", null, culture));
				goto IL_DED;
			}
			if (num <= 1461901041U)
			{
				if (num != 1176137065U)
				{
					if (num != 1194886160U)
					{
						if (num == 1461901041U)
						{
							if (text == "fr")
							{
								string str = (culture.Name == "fr-CA") ? "11282005" : "28112005";
								validMaskDescriptorList.Add(new MaskDescriptorTemplate("99999", "Numérique (5 chiffres)", "12345", typeof(int), culture));
								validMaskDescriptorList.Add(new MaskDescriptorTemplate("00 00 00 00 00 00", "Numéro de téléphone (France)", "0123456789", null, culture));
								validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000", "Date (format court)", str, typeof(DateTime), culture));
								validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000 00:00", "Date et heure (format long)", str + "1430", typeof(DateTime), culture));
								validMaskDescriptorList.Add(new MaskDescriptorTemplate("0 00 00 00 000 000 00", "Numéro de Sécurité Sociale (France)", "163117801234548", null, culture));
								validMaskDescriptorList.Add(new MaskDescriptorTemplate("00:00", "Heure", "1430", typeof(DateTime), culture));
								validMaskDescriptorList.Add(new MaskDescriptorTemplate("00000", "Code postal (France)", "91450", null, culture));
								goto IL_DED;
							}
						}
					}
					else if (text == "it")
					{
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("99999", "Numerico (5 Cifre)", "12345", typeof(int), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000 00000", "Numero di telefono", "012345678", null, culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("000 0000000", "Numero di cellulare", "1234567890", null, culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000", "Data breve", "26102005", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000 00:00", "Data e ora", "261020051430", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("00:00", "Ora", "1430", typeof(DateTime), culture));
						validMaskDescriptorList.Add(new MaskDescriptorTemplate("00000", "Codice postale", "12345", null, culture));
						goto IL_DED;
					}
				}
				else if (text == "es")
				{
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("99999", "Numérico", "12345", typeof(int), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("(999)000-0000", "Número de teléfono", "0123456789", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-000-0000", "Número de teléfono móvil", "0001234567", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-0000", "Número de teléfono sin código de área", "1234567", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000", "Fecha", "26102005", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000 00:00", "Fecha y hora", "261020051430", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-00-0000", "Número del seguro social", "123456789", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("00:00", "Hora", "0830", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("00000", "Código postal", "12345", null, culture));
					goto IL_DED;
				}
			}
			else if (num != 1545391778U)
			{
				if (num != 1562713850U)
				{
					if (num == 1816099348U)
					{
						if (text == "ja")
						{
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("99999", "数値（５桁）", "12345", typeof(int), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("99900-9990-0000", "電話番号", "  012- 345-6789", null, culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-0000-0000", "携帯電話番号", "00001234567", null, culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000/00/00", "日付（西暦）", "20050620", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000/00/00 00:00:00", "日付と時間（西暦）", "2005/06/11 04:33:22", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("90:00", "時間", " 633", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-0000", "郵便番号", "1820021", null, culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000年90月90日", "日付（西暦、日本語）", "2005年 6月11日", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/00", "日付（和暦）", "170611", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("AA90年90月90日", "日付（和暦、日本語）", "平成17年 6月11日", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("0000年90月90日 90時90分", "日付と時間（日本語）", "2005年 6月11日  3時33分", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/00 00:00:00", "日付と時間（和暦）", "170611043322", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("AA00年90月90日 90時90分", "日付と時間（和暦、日本語）", "平成17年 6月11日  3時33分", typeof(DateTime), culture));
							validMaskDescriptorList.Add(new MaskDescriptorTemplate("90時90分", "時間（日本語）", " 633", typeof(DateTime), culture));
							goto IL_DED;
						}
					}
				}
				else if (text == "ar")
				{
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("(999)000-0000", "Phone Number", "0123456789", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-0000", "Phone Number no Area Code", "1234567", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("00 /00 /0000", "Short Date", "26102005", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("00 /00 /0000 00:00", "Short Date/Time", "261020051430", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-00-0000", "Social Security Number", "123456789", null, culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("90:00", "Time", " 230", typeof(DateTime), culture));
					validMaskDescriptorList.Add(new MaskDescriptorTemplate("00:00", "Time (24 Hour)", "1430", typeof(DateTime), culture));
					goto IL_DED;
				}
			}
			else if (text == "de")
			{
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000", "Datum kurz", "28112005", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000 00:00", "Datum lang", "281120051430", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("90:00", "Zeit", "1430", typeof(DateTime), culture));
				validMaskDescriptorList.Add(new MaskDescriptorTemplate("00000", "Postleitzahl", "91450", null, culture));
				goto IL_DED;
			}
			IL_199:
			culture = CultureInfo.InvariantCulture;
			IL_1A0:
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("00000", "Numeric (5-digits)", "12345", typeof(int), culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("(999) 000-0000", "Phone number", "5745550123", null, culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-0000", "Phone number no area code", "5550123", null, culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000", "Short date", "12112003", typeof(DateTime), culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("00/00/0000 90:00", "Short date and time (US)", "121120031120", typeof(DateTime), culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("000-00-0000", "Social security number", "000001234", null, culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("90:00", "Time (US)", "1120", typeof(DateTime), culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("00:00", "Time (European/Military)", "2320", typeof(DateTime), culture));
			validMaskDescriptorList.Add(new MaskDescriptorTemplate("00000-9999", "Zip Code", "980526399", null, culture));
			IL_DED:
			return validMaskDescriptorList.List;
		}

		// Token: 0x040017E4 RID: 6116
		private string mask;

		// Token: 0x040017E5 RID: 6117
		private string name;

		// Token: 0x040017E6 RID: 6118
		private string sample;

		// Token: 0x040017E7 RID: 6119
		private Type type;

		// Token: 0x040017E8 RID: 6120
		private CultureInfo culture;

		// Token: 0x02000582 RID: 1410
		private class ValidMaskDescriptorList
		{
			// Token: 0x0600327A RID: 12922 RVA: 0x001112F3 File Offset: 0x0010F4F3
			public void Add(MaskDescriptorTemplate mdt)
			{
				if (mdt.Mask != null)
				{
					this.dx.Add(mdt);
				}
			}

			// Token: 0x170009EE RID: 2542
			// (get) Token: 0x0600327B RID: 12923 RVA: 0x00111309 File Offset: 0x0010F509
			public List<MaskDescriptor> List
			{
				get
				{
					return this.dx;
				}
			}

			// Token: 0x040021A1 RID: 8609
			private List<MaskDescriptor> dx = new List<MaskDescriptor>();
		}
	}
}

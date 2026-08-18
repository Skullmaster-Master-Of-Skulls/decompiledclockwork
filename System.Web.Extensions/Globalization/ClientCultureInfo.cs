using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Script.Serialization;

namespace System.Web.Globalization
{
	// Token: 0x02000009 RID: 9
	internal class ClientCultureInfo
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002740 File Offset: 0x00000940
		private ClientCultureInfo(CultureInfo cultureInfo)
		{
			this.name = cultureInfo.Name;
			this.numberFormat = cultureInfo.NumberFormat;
			this.dateTimeFormat = cultureInfo.DateTimeFormat;
			Calendar calendar = (this.dateTimeFormat == null) ? null : this.dateTimeFormat.Calendar;
			if (calendar != null)
			{
				this.eras = new object[calendar.Eras.Length * 4];
				int num = 0;
				foreach (int num2 in calendar.Eras)
				{
					this.eras[num + ClientCultureInfo.eraNumber] = num2;
					this.eras[num + ClientCultureInfo.eraName] = this.dateTimeFormat.GetEraName(num2);
					this.eras[num + ClientCultureInfo.eraYearOffset] = 0;
					num += 4;
				}
				Type type = calendar.GetType();
				if (type != typeof(GregorianCalendar))
				{
					if (type == typeof(TaiwanCalendar))
					{
						this.eras[ClientCultureInfo.eraYearOffset] = 1911;
						return;
					}
					if (type == typeof(KoreanCalendar))
					{
						this.eras[ClientCultureInfo.eraYearOffset] = -2333;
						return;
					}
					if (type == typeof(ThaiBuddhistCalendar))
					{
						this.eras[ClientCultureInfo.eraYearOffset] = -543;
						return;
					}
					if (type == typeof(JapaneseCalendar))
					{
						this.eras[ClientCultureInfo.eraStart] = 60022080000L;
						this.eras[ClientCultureInfo.eraYearOffset] = 1988;
						this.eras[4 + ClientCultureInfo.eraStart] = -1357603200000L;
						this.eras[4 + ClientCultureInfo.eraYearOffset] = 1925;
						this.eras[8 + ClientCultureInfo.eraStart] = -1812153600000L;
						this.eras[8 + ClientCultureInfo.eraYearOffset] = 1911;
						this.eras[12 + ClientCultureInfo.eraYearOffset] = 1867;
						return;
					}
					if (type == typeof(HijriCalendar))
					{
						this._convertScript = "Date.HijriCalendar.js";
						this._adjustment = ((HijriCalendar)calendar).HijriAdjustment;
						return;
					}
					if (type == typeof(UmAlQuraCalendar))
					{
						this._convertScript = "Date.UmAlQuraCalendar.js";
					}
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000029BC File Offset: 0x00000BBC
		internal Tuple<string, string> GetClientCultureScriptBlock()
		{
			return ClientCultureInfo.GetClientCultureScriptBlock(CultureInfo.CurrentCulture);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000029C8 File Offset: 0x00000BC8
		internal static Tuple<string, string> GetClientCultureScriptBlock(CultureInfo cultureInfo)
		{
			if (cultureInfo == null)
			{
				return null;
			}
			Type type = (cultureInfo.DateTimeFormat == null) ? null : cultureInfo.DateTimeFormat.Calendar.GetType();
			if (cultureInfo.Equals(ClientCultureInfo.enUS) && type == typeof(GregorianCalendar))
			{
				return null;
			}
			Tuple<CultureInfo, Type> key = new Tuple<CultureInfo, Type>(cultureInfo, type);
			Tuple<string, string> tuple = ClientCultureInfo.cultureScriptBlockCache[key] as Tuple<string, string>;
			if (tuple == null)
			{
				ClientCultureInfo clientCultureInfo = new ClientCultureInfo(cultureInfo);
				string text = JavaScriptSerializer.SerializeInternal(ClientCultureInfo.BuildSerializeableCultureInfo(clientCultureInfo));
				if (text.Length > 0)
				{
					string text2 = "var __cultureInfo = " + text + ";";
					if (clientCultureInfo._adjustment != 0)
					{
						text2 = text2 + "\r\n__cultureInfo.dateTimeFormat.Calendar._adjustment = " + clientCultureInfo._adjustment.ToString(CultureInfo.InvariantCulture) + ";";
					}
					tuple = new Tuple<string, string>(text2, clientCultureInfo._convertScript);
				}
				ClientCultureInfo.cultureScriptBlockCache[key] = tuple;
			}
			return tuple;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002AAC File Offset: 0x00000CAC
		private static OrderedDictionary BuildSerializeableCultureInfo(ClientCultureInfo clientCultureInfo)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			orderedDictionary["name"] = clientCultureInfo.name;
			orderedDictionary["numberFormat"] = clientCultureInfo.numberFormat;
			orderedDictionary["dateTimeFormat"] = clientCultureInfo.dateTimeFormat;
			orderedDictionary["eras"] = clientCultureInfo.eras;
			return orderedDictionary;
		}

		// Token: 0x0400000E RID: 14
		private static Hashtable cultureScriptBlockCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400000F RID: 15
		private static readonly CultureInfo enUS = CultureInfo.GetCultureInfo(1033);

		// Token: 0x04000010 RID: 16
		private static int eraNumber = 0;

		// Token: 0x04000011 RID: 17
		private static int eraName = 1;

		// Token: 0x04000012 RID: 18
		private static int eraStart = 2;

		// Token: 0x04000013 RID: 19
		private static int eraYearOffset = 3;

		// Token: 0x04000014 RID: 20
		public string name;

		// Token: 0x04000015 RID: 21
		public NumberFormatInfo numberFormat;

		// Token: 0x04000016 RID: 22
		public DateTimeFormatInfo dateTimeFormat;

		// Token: 0x04000017 RID: 23
		public object[] eras;

		// Token: 0x04000018 RID: 24
		private string _convertScript;

		// Token: 0x04000019 RID: 25
		private int _adjustment;
	}
}

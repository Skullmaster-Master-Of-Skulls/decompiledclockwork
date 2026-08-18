using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x0200030A RID: 778
	internal class XmlCustomFormatter
	{
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000AD508 File Offset: 0x000AC508
		private static DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				if (XmlCustomFormatter.mode == DateTimeSerializationSection.DateTimeSerializationMode.Default)
				{
					DateTimeSerializationSection dateTimeSerializationSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.DateTimeSerializationSectionPath) as DateTimeSerializationSection;
					if (dateTimeSerializationSection != null)
					{
						XmlCustomFormatter.mode = dateTimeSerializationSection.Mode;
					}
					else
					{
						XmlCustomFormatter.mode = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip;
					}
				}
				return XmlCustomFormatter.mode;
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000AD547 File Offset: 0x000AC547
		private XmlCustomFormatter()
		{
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000AD550 File Offset: 0x000AC550
		internal static string FromDefaultValue(object value, string formatter)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == typeof(DateTime))
			{
				if (formatter == "DateTime")
				{
					return XmlCustomFormatter.FromDateTime((DateTime)value);
				}
				if (formatter == "Date")
				{
					return XmlCustomFormatter.FromDate((DateTime)value);
				}
				if (formatter == "Time")
				{
					return XmlCustomFormatter.FromTime((DateTime)value);
				}
			}
			else if (type == typeof(string))
			{
				if (formatter == "XmlName")
				{
					return XmlCustomFormatter.FromXmlName((string)value);
				}
				if (formatter == "XmlNCName")
				{
					return XmlCustomFormatter.FromXmlNCName((string)value);
				}
				if (formatter == "XmlNmToken")
				{
					return XmlCustomFormatter.FromXmlNmToken((string)value);
				}
				if (formatter == "XmlNmTokens")
				{
					return XmlCustomFormatter.FromXmlNmTokens((string)value);
				}
			}
			throw new Exception(Res.GetString("XmlUnsupportedDefaultType", new object[]
			{
				type.FullName
			}));
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000AD652 File Offset: 0x000AC652
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000AD65F File Offset: 0x000AC65F
		internal static string FromTime(DateTime value)
		{
			return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000AD67C File Offset: 0x000AC67C
		internal static string FromDateTime(DateTime value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
			}
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000AD699 File Offset: 0x000AC699
		internal static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000AD6A1 File Offset: 0x000AC6A1
		internal static string FromXmlName(string name)
		{
			return XmlConvert.EncodeName(name);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000AD6A9 File Offset: 0x000AC6A9
		internal static string FromXmlNCName(string ncName)
		{
			return XmlConvert.EncodeLocalName(ncName);
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000AD6B1 File Offset: 0x000AC6B1
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.EncodeNmToken(nmToken);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000AD6BC File Offset: 0x000AC6BC
		internal static string FromXmlNmTokens(string nmTokens)
		{
			if (nmTokens == null)
			{
				return null;
			}
			if (nmTokens.IndexOf(' ') < 0)
			{
				return XmlCustomFormatter.FromXmlNmToken(nmTokens);
			}
			string[] array = nmTokens.Split(new char[]
			{
				' '
			});
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(XmlCustomFormatter.FromXmlNmToken(array[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000AD72A File Offset: 0x000AC72A
		internal static void WriteArrayBase64(XmlWriter writer, byte[] inData, int start, int count)
		{
			if (inData == null || count == 0)
			{
				return;
			}
			writer.WriteBase64(inData, start, count);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000AD73C File Offset: 0x000AC73C
		internal static string FromByteArrayHex(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return "";
			}
			return XmlConvert.ToBinHexString(value);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000AD754 File Offset: 0x000AC754
		internal static string FromEnum(long val, string[] vals, long[] ids, string typeName)
		{
			long num = val;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = -1;
			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i] == 0L)
				{
					num2 = i;
				}
				else
				{
					if (val == 0L)
					{
						break;
					}
					if ((ids[i] & num) == ids[i])
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(vals[i]);
						val &= ~ids[i];
					}
				}
			}
			if (val != 0L)
			{
				throw new InvalidOperationException(Res.GetString("XmlUnknownConstant", new object[]
				{
					num,
					(typeName == null) ? "enum" : typeName
				}));
			}
			if (stringBuilder.Length == 0 && num2 >= 0)
			{
				stringBuilder.Append(vals[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000AD810 File Offset: 0x000AC810
		internal static object ToDefaultValue(string value, string formatter)
		{
			if (formatter == "DateTime")
			{
				return XmlCustomFormatter.ToDateTime(value);
			}
			if (formatter == "Date")
			{
				return XmlCustomFormatter.ToDate(value);
			}
			if (formatter == "Time")
			{
				return XmlCustomFormatter.ToTime(value);
			}
			if (formatter == "XmlName")
			{
				return XmlCustomFormatter.ToXmlName(value);
			}
			if (formatter == "XmlNCName")
			{
				return XmlCustomFormatter.ToXmlNCName(value);
			}
			if (formatter == "XmlNmToken")
			{
				return XmlCustomFormatter.ToXmlNmToken(value);
			}
			if (formatter == "XmlNmTokens")
			{
				return XmlCustomFormatter.ToXmlNmTokens(value);
			}
			throw new Exception(Res.GetString("XmlUnsupportedDefaultValue", new object[]
			{
				formatter
			}));
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000AD8D3 File Offset: 0x000AC8D3
		internal static DateTime ToDateTime(string value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
			}
			return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000AD8F0 File Offset: 0x000AC8F0
		internal static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000AD8F9 File Offset: 0x000AC8F9
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000AD906 File Offset: 0x000AC906
		internal static DateTime ToTime(string value)
		{
			return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000AD91A File Offset: 0x000AC91A
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000AD922 File Offset: 0x000AC922
		internal static string ToXmlName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000AD92F File Offset: 0x000AC92F
		internal static string ToXmlNCName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000AD93C File Offset: 0x000AC93C
		internal static string ToXmlNmToken(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000AD949 File Offset: 0x000AC949
		internal static string ToXmlNmTokens(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000AD956 File Offset: 0x000AC956
		internal static byte[] ToByteArrayBase64(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return new byte[0];
			}
			return Convert.FromBase64String(value);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000AD97A File Offset: 0x000AC97A
		internal static byte[] ToByteArrayHex(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			return XmlConvert.FromBinHexString(value);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000AD990 File Offset: 0x000AC990
		internal static long ToEnum(string val, Hashtable vals, string typeName, bool validate)
		{
			long num = 0L;
			string[] array = val.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = vals[array[i]];
				if (obj != null)
				{
					num |= (long)obj;
				}
				else if (validate && array[i].Length > 0)
				{
					throw new InvalidOperationException(Res.GetString("XmlUnknownConstant", new object[]
					{
						array[i],
						typeName
					}));
				}
			}
			return num;
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000ADA03 File Offset: 0x000ACA03
		private static string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		// Token: 0x04001573 RID: 5491
		private static DateTimeSerializationSection.DateTimeSerializationMode mode;

		// Token: 0x04001574 RID: 5492
		private static string[] allDateTimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz",
			"yyyy",
			"---dd",
			"---ddZ",
			"---ddzzzzzz",
			"--MM-dd",
			"--MM-ddZ",
			"--MM-ddzzzzzz",
			"--MM--",
			"--MM--Z",
			"--MM--zzzzzz",
			"yyyy-MM",
			"yyyy-MMZ",
			"yyyy-MMzzzzzz",
			"yyyyzzzzzz",
			"yyyy-MM-dd",
			"yyyy-MM-ddZ",
			"yyyy-MM-ddzzzzzz",
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:ssZ",
			"HH:mm:ss.fZ",
			"HH:mm:ss.ffZ",
			"HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ",
			"HH:mm:ss.fffffZ",
			"HH:mm:ss.ffffffZ",
			"HH:mm:ss.fffffffZ",
			"HH:mm:sszzzzzz",
			"HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz",
			"HH:mm:ss.ffffzzzzzz",
			"HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz",
			"HH:mm:ss.fffffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss",
			"yyyy-MM-ddTHH:mm:ss.f",
			"yyyy-MM-ddTHH:mm:ss.ff",
			"yyyy-MM-ddTHH:mm:ss.fff",
			"yyyy-MM-ddTHH:mm:ss.ffff",
			"yyyy-MM-ddTHH:mm:ss.fffff",
			"yyyy-MM-ddTHH:mm:ss.ffffff",
			"yyyy-MM-ddTHH:mm:ss.fffffff",
			"yyyy-MM-ddTHH:mm:ssZ",
			"yyyy-MM-ddTHH:mm:ss.fZ",
			"yyyy-MM-ddTHH:mm:ss.ffZ",
			"yyyy-MM-ddTHH:mm:ss.fffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffffffZ",
			"yyyy-MM-ddTHH:mm:sszzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fffffzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz"
		};

		// Token: 0x04001575 RID: 5493
		private static string[] allDateFormats = new string[]
		{
			"yyyy-MM-ddzzzzzz",
			"yyyy-MM-dd",
			"yyyy-MM-ddZ",
			"yyyy",
			"---dd",
			"---ddZ",
			"---ddzzzzzz",
			"--MM-dd",
			"--MM-ddZ",
			"--MM-ddzzzzzz",
			"--MM--",
			"--MM--Z",
			"--MM--zzzzzz",
			"yyyy-MM",
			"yyyy-MMZ",
			"yyyy-MMzzzzzz",
			"yyyyzzzzzz"
		};

		// Token: 0x04001576 RID: 5494
		private static string[] allTimeFormats = new string[]
		{
			"HH:mm:ss.fffffffzzzzzz",
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff",
			"HH:mm:ssZ",
			"HH:mm:ss.fZ",
			"HH:mm:ss.ffZ",
			"HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ",
			"HH:mm:ss.fffffZ",
			"HH:mm:ss.ffffffZ",
			"HH:mm:ss.fffffffZ",
			"HH:mm:sszzzzzz",
			"HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz",
			"HH:mm:ss.ffffzzzzzz",
			"HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz"
		};
	}
}

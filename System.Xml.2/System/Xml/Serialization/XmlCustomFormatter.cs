using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000191 RID: 401
	internal class XmlCustomFormatter
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0007634C File Offset: 0x0007454C
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

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0007638B File Offset: 0x0007458B
		private XmlCustomFormatter()
		{
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00076394 File Offset: 0x00074594
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

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000764A1 File Offset: 0x000746A1
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000764B0 File Offset: 0x000746B0
		internal static string FromTime(DateTime value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization && value.Kind == DateTimeKind.Utc)
			{
				return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffZ");
			}
			return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00076505 File Offset: 0x00074705
		internal static string FromDateTime(DateTime value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
			}
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00076522 File Offset: 0x00074722
		internal static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0007652A File Offset: 0x0007472A
		internal static string FromXmlName(string name)
		{
			return XmlConvert.EncodeName(name);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00076532 File Offset: 0x00074732
		internal static string FromXmlNCName(string ncName)
		{
			return XmlConvert.EncodeLocalName(ncName);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0007653A File Offset: 0x0007473A
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.EncodeNmToken(nmToken);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00076544 File Offset: 0x00074744
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

		// Token: 0x06001AAA RID: 6826 RVA: 0x000765B0 File Offset: 0x000747B0
		internal static void WriteArrayBase64(XmlWriter writer, byte[] inData, int start, int count)
		{
			if (inData == null || count == 0)
			{
				return;
			}
			writer.WriteBase64(inData, start, count);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000765C2 File Offset: 0x000747C2
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

		// Token: 0x06001AAC RID: 6828 RVA: 0x000765DC File Offset: 0x000747DC
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

		// Token: 0x06001AAD RID: 6829 RVA: 0x0007668C File Offset: 0x0007488C
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

		// Token: 0x06001AAE RID: 6830 RVA: 0x0007674D File Offset: 0x0007494D
		internal static DateTime ToDateTime(string value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
			}
			return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0007676A File Offset: 0x0007496A
		internal static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00076773 File Offset: 0x00074973
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00076780 File Offset: 0x00074980
		internal static DateTime ToTime(string value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization)
			{
				return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.RoundtripKind);
			}
			return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000767B1 File Offset: 0x000749B1
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000767B9 File Offset: 0x000749B9
		internal static string ToXmlName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000767C6 File Offset: 0x000749C6
		internal static string ToXmlNCName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000767D3 File Offset: 0x000749D3
		internal static string ToXmlNmToken(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000767E0 File Offset: 0x000749E0
		internal static string ToXmlNmTokens(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000767ED File Offset: 0x000749ED
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

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00076811 File Offset: 0x00074A11
		internal static byte[] ToByteArrayHex(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			return XmlConvert.FromBinHexString(value);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00076828 File Offset: 0x00074A28
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

		// Token: 0x06001ABA RID: 6842 RVA: 0x00076895 File Offset: 0x00074A95
		private static string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		// Token: 0x04000BE8 RID: 3048
		private static DateTimeSerializationSection.DateTimeSerializationMode mode;

		// Token: 0x04000BE9 RID: 3049
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

		// Token: 0x04000BEA RID: 3050
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

		// Token: 0x04000BEB RID: 3051
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

using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200007D RID: 125
	[__DynamicallyInvokable]
	public class XmlConvert
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x00010286 File Offset: 0x0000E486
		[__DynamicallyInvokable]
		public static string EncodeName(string name)
		{
			return XmlConvert.EncodeName(name, true, false);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00010290 File Offset: 0x0000E490
		[__DynamicallyInvokable]
		public static string EncodeNmToken(string name)
		{
			return XmlConvert.EncodeName(name, false, false);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001029A File Offset: 0x0000E49A
		[__DynamicallyInvokable]
		public static string EncodeLocalName(string name)
		{
			return XmlConvert.EncodeName(name, true, true);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000102A4 File Offset: 0x0000E4A4
		[__DynamicallyInvokable]
		public static string DecodeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int num2 = name.IndexOf('_');
			if (num2 < 0)
			{
				return name;
			}
			if (XmlConvert.c_DecodeCharPattern == null)
			{
				XmlConvert.c_DecodeCharPattern = new Regex("_[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
			}
			MatchCollection matchCollection = XmlConvert.c_DecodeCharPattern.Matches(name, num2);
			IEnumerator enumerator = matchCollection.GetEnumerator();
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				Match match = (Match)enumerator.Current;
				num3 = match.Index;
			}
			for (int i = 0; i < length - XmlConvert.c_EncodedCharLength + 1; i++)
			{
				if (i == num3)
				{
					if (enumerator.MoveNext())
					{
						Match match2 = (Match)enumerator.Current;
						num3 = match2.Index;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					stringBuilder.Append(name, num, i - num);
					if (name[i + 6] != '_')
					{
						int num4 = XmlConvert.FromHex(name[i + 2]) * 268435456 + XmlConvert.FromHex(name[i + 3]) * 16777216 + XmlConvert.FromHex(name[i + 4]) * 1048576 + XmlConvert.FromHex(name[i + 5]) * 65536 + XmlConvert.FromHex(name[i + 6]) * 4096 + XmlConvert.FromHex(name[i + 7]) * 256 + XmlConvert.FromHex(name[i + 8]) * 16 + XmlConvert.FromHex(name[i + 9]);
						if (num4 >= 65536)
						{
							if (num4 <= 1114111)
							{
								num = i + XmlConvert.c_EncodedCharLength + 4;
								char value;
								char value2;
								XmlCharType.SplitSurrogateChar(num4, out value, out value2);
								stringBuilder.Append(value2);
								stringBuilder.Append(value);
							}
						}
						else
						{
							num = i + XmlConvert.c_EncodedCharLength + 4;
							stringBuilder.Append((char)num4);
						}
						i += XmlConvert.c_EncodedCharLength - 1 + 4;
					}
					else
					{
						num = i + XmlConvert.c_EncodedCharLength;
						stringBuilder.Append((char)(XmlConvert.FromHex(name[i + 2]) * 4096 + XmlConvert.FromHex(name[i + 3]) * 256 + XmlConvert.FromHex(name[i + 4]) * 16 + XmlConvert.FromHex(name[i + 5])));
						i += XmlConvert.c_EncodedCharLength - 1;
					}
				}
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001053C File Offset: 0x0000E73C
		private static string EncodeName(string name, bool first, bool local)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int i = 0;
			int num2 = name.IndexOf('_');
			IEnumerator enumerator = null;
			if (num2 >= 0)
			{
				if (XmlConvert.c_EncodeCharPattern == null)
				{
					XmlConvert.c_EncodeCharPattern = new Regex("(?<=_)[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
				}
				MatchCollection matchCollection = XmlConvert.c_EncodeCharPattern.Matches(name, num2);
				enumerator = matchCollection.GetEnumerator();
			}
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				Match match = (Match)enumerator.Current;
				num3 = match.Index - 1;
			}
			if (first && ((!XmlConvert.xmlCharType.IsStartNCNameCharXml4e(name[0]) && (local || (!local && name[0] != ':'))) || num3 == 0))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length + 20);
				}
				stringBuilder.Append("_x");
				if (length > 1 && XmlCharType.IsHighSurrogate((int)name[0]) && XmlCharType.IsLowSurrogate((int)name[1]))
				{
					int highChar = (int)name[0];
					int lowChar = (int)name[1];
					stringBuilder.Append(XmlCharType.CombineSurrogateChar(lowChar, highChar).ToString("X8", CultureInfo.InvariantCulture));
					i++;
					num = 2;
				}
				else
				{
					stringBuilder.Append(((int)name[0]).ToString("X4", CultureInfo.InvariantCulture));
					num = 1;
				}
				stringBuilder.Append("_");
				i++;
				if (num3 == 0 && enumerator.MoveNext())
				{
					Match match2 = (Match)enumerator.Current;
					num3 = match2.Index - 1;
				}
			}
			while (i < length)
			{
				if ((local && !XmlConvert.xmlCharType.IsNCNameCharXml4e(name[i])) || (!local && !XmlConvert.xmlCharType.IsNameCharXml4e(name[i])) || num3 == i)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					if (num3 == i && enumerator.MoveNext())
					{
						Match match3 = (Match)enumerator.Current;
						num3 = match3.Index - 1;
					}
					stringBuilder.Append(name, num, i - num);
					stringBuilder.Append("_x");
					if (length > i + 1 && XmlCharType.IsHighSurrogate((int)name[i]) && XmlCharType.IsLowSurrogate((int)name[i + 1]))
					{
						int highChar2 = (int)name[i];
						int lowChar2 = (int)name[i + 1];
						stringBuilder.Append(XmlCharType.CombineSurrogateChar(lowChar2, highChar2).ToString("X8", CultureInfo.InvariantCulture));
						num = i + 2;
						i++;
					}
					else
					{
						stringBuilder.Append(((int)name[i]).ToString("X4", CultureInfo.InvariantCulture));
						num = i + 1;
					}
					stringBuilder.Append("_");
				}
				i++;
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00010819 File Offset: 0x0000EA19
		private static int FromHex(char digit)
		{
			if (digit > '9')
			{
				return (int)(((digit <= 'F') ? (digit - 'A') : (digit - 'a')) + '\n');
			}
			return (int)(digit - '0');
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00010837 File Offset: 0x0000EA37
		internal static byte[] FromBinHexString(string s)
		{
			return XmlConvert.FromBinHexString(s, true);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00010840 File Offset: 0x0000EA40
		internal static byte[] FromBinHexString(string s, bool allowOddCount)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return BinHexDecoder.Decode(s.ToCharArray(), allowOddCount);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001085C File Offset: 0x0000EA5C
		internal static string ToBinHexString(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return BinHexEncoder.Encode(inArray, 0, inArray.Length);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00010878 File Offset: 0x0000EA78
		[__DynamicallyInvokable]
		public static string VerifyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("Xml_EmptyName"));
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, ExceptionType.XmlException);
			}
			return name;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000108CC File Offset: 0x0000EACC
		internal static Exception TryVerifyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("Xml_EmptyName", string.Empty);
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException((num == 0) ? "Xml_BadStartNameChar" : "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00010922 File Offset: 0x0000EB22
		internal static string VerifyQName(string name)
		{
			return XmlConvert.VerifyQName(name, ExceptionType.XmlException);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001092C File Offset: 0x0000EB2C
		internal static string VerifyQName(string name, ExceptionType exceptionType)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentNullException("name");
			}
			int num = -1;
			int num2 = ValidateNames.ParseQName(name, 0, out num);
			if (num2 != name.Length)
			{
				throw XmlConvert.CreateException("Xml_BadNameChar", XmlException.BuildCharExceptionArgs(name, num2), exceptionType, 0, num2 + 1);
			}
			return name;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001097C File Offset: 0x0000EB7C
		[__DynamicallyInvokable]
		public static string VerifyNCName(string name)
		{
			return XmlConvert.VerifyNCName(name, ExceptionType.XmlException);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00010988 File Offset: 0x0000EB88
		internal static string VerifyNCName(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("Xml_EmptyLocalName"));
			}
			int num = ValidateNames.ParseNCName(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, exceptionType);
			}
			return name;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000109DC File Offset: 0x0000EBDC
		internal static Exception TryVerifyNCName(string name)
		{
			int num = ValidateNames.ParseNCName(name);
			if (num == 0 || num != name.Length)
			{
				return ValidateNames.GetInvalidNameException(name, 0, num);
			}
			return null;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00010A08 File Offset: 0x0000EC08
		public static string VerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return token;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				throw new XmlException("Sch_NotTokenString", token);
			}
			return token;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00010A6C File Offset: 0x0000EC6C
		internal static Exception TryVerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return null;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				return new XmlException("Sch_NotTokenString", token);
			}
			return null;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00010ACD File Offset: 0x0000ECCD
		[__DynamicallyInvokable]
		public static string VerifyNMTOKEN(string name)
		{
			return XmlConvert.VerifyNMTOKEN(name, ExceptionType.XmlException);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		internal static string VerifyNMTOKEN(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw XmlConvert.CreateException("Xml_InvalidNmToken", name, exceptionType);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateException("Xml_BadNameChar", XmlException.BuildCharExceptionArgs(name, num), exceptionType, 0, num + 1);
			}
			return name;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00010B34 File Offset: 0x0000ED34
		internal static Exception TryVerifyNMTOKEN(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("Xml_EmptyName", string.Empty);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException("Xml_BadNameChar", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00010B80 File Offset: 0x0000ED80
		internal static string VerifyNormalizedString(string str)
		{
			if (str.IndexOfAny(XmlConvert.crt) != -1)
			{
				throw new XmlSchemaException("Sch_NotNormalizedString", str);
			}
			return str;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00010B9D File Offset: 0x0000ED9D
		internal static Exception TryVerifyNormalizedString(string str)
		{
			if (str.IndexOfAny(XmlConvert.crt) != -1)
			{
				return new XmlSchemaException("Sch_NotNormalizedString", str);
			}
			return null;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00010BBA File Offset: 0x0000EDBA
		[__DynamicallyInvokable]
		public static string VerifyXmlChars(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			XmlConvert.VerifyCharData(content, ExceptionType.XmlException);
			return content;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		[__DynamicallyInvokable]
		public static string VerifyPublicId(string publicId)
		{
			if (publicId == null)
			{
				throw new ArgumentNullException("publicId");
			}
			int num = XmlConvert.xmlCharType.IsPublicId(publicId);
			if (num != -1)
			{
				throw XmlConvert.CreateInvalidCharException(publicId, num, ExceptionType.XmlException);
			}
			return publicId;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00010C0C File Offset: 0x0000EE0C
		[__DynamicallyInvokable]
		public static string VerifyWhitespace(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			int num = XmlConvert.xmlCharType.IsOnlyWhitespaceWithPos(content);
			if (num != -1)
			{
				throw new XmlException("Xml_InvalidWhitespaceCharacter", XmlException.BuildCharExceptionArgs(content, num), 0, num + 1);
			}
			return content;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00010C4E File Offset: 0x0000EE4E
		public unsafe static bool IsStartNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[ch] & 4) > 0;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00010C62 File Offset: 0x0000EE62
		public unsafe static bool IsNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[ch] & 8) > 0;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00010C76 File Offset: 0x0000EE76
		public unsafe static bool IsXmlChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[ch] & 16) > 0;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00010C8B File Offset: 0x0000EE8B
		public static bool IsXmlSurrogatePair(char lowChar, char highChar)
		{
			return XmlCharType.IsHighSurrogate((int)highChar) && XmlCharType.IsLowSurrogate((int)lowChar);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00010C9D File Offset: 0x0000EE9D
		public static bool IsPublicIdChar(char ch)
		{
			return XmlConvert.xmlCharType.IsPubidChar(ch);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00010CAA File Offset: 0x0000EEAA
		public unsafe static bool IsWhitespaceChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[ch] & 1) > 0;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00010CBE File Offset: 0x0000EEBE
		[__DynamicallyInvokable]
		public static string ToString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00010CCE File Offset: 0x0000EECE
		[__DynamicallyInvokable]
		public static string ToString(char value)
		{
			return value.ToString(null);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00010CD8 File Offset: 0x0000EED8
		[__DynamicallyInvokable]
		public static string ToString(decimal value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00010CE7 File Offset: 0x0000EEE7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00010CF6 File Offset: 0x0000EEF6
		[__DynamicallyInvokable]
		public static string ToString(short value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010D05 File Offset: 0x0000EF05
		[__DynamicallyInvokable]
		public static string ToString(int value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00010D14 File Offset: 0x0000EF14
		[__DynamicallyInvokable]
		public static string ToString(long value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00010D23 File Offset: 0x0000EF23
		[__DynamicallyInvokable]
		public static string ToString(byte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00010D32 File Offset: 0x0000EF32
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ushort value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00010D41 File Offset: 0x0000EF41
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(uint value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010D50 File Offset: 0x0000EF50
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static string ToString(ulong value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00010D5F File Offset: 0x0000EF5F
		[__DynamicallyInvokable]
		public static string ToString(float value)
		{
			if (float.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (float.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero((double)value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010D9D File Offset: 0x0000EF9D
		[__DynamicallyInvokable]
		public static string ToString(double value)
		{
			if (double.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (double.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero(value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00010DDC File Offset: 0x0000EFDC
		[__DynamicallyInvokable]
		public static string ToString(TimeSpan value)
		{
			return new XsdDuration(value).ToString();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010DFD File Offset: 0x0000EFFD
		[Obsolete("Use XmlConvert.ToString() that takes in XmlDateTimeSerializationMode")]
		public static string ToString(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010E0A File Offset: 0x0000F00A
		public static string ToString(DateTime value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010E1C File Offset: 0x0000F01C
		[__DynamicallyInvokable]
		public static string ToString(DateTime value, XmlDateTimeSerializationMode dateTimeOption)
		{
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				value = XmlConvert.SwitchToLocalTime(value);
				break;
			case XmlDateTimeSerializationMode.Utc:
				value = XmlConvert.SwitchToUtcTime(value);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				value = new DateTime(value.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("Sch_InvalidDateTimeOption", new object[]
				{
					dateTimeOption,
					"dateTimeOption"
				}));
			}
			XsdDateTime xsdDateTime = new XsdDateTime(value, XsdDateTimeFlags.DateTime);
			return xsdDateTime.ToString();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		[__DynamicallyInvokable]
		public static string ToString(DateTimeOffset value)
		{
			XsdDateTime xsdDateTime = new XsdDateTime(value);
			return xsdDateTime.ToString();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00010EC6 File Offset: 0x0000F0C6
		[__DynamicallyInvokable]
		public static string ToString(DateTimeOffset value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00010ED5 File Offset: 0x0000F0D5
		[__DynamicallyInvokable]
		public static string ToString(Guid value)
		{
			return value.ToString();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		[__DynamicallyInvokable]
		public static bool ToBoolean(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "1" || s == "true")
			{
				return true;
			}
			if (s == "0" || s == "false")
			{
				return false;
			}
			throw new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
			{
				s,
				"Boolean"
			}));
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00010F54 File Offset: 0x0000F154
		internal static Exception TryToBoolean(string s, out bool result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "0" || s == "false")
			{
				result = false;
				return null;
			}
			if (s == "1" || s == "true")
			{
				result = true;
				return null;
			}
			result = false;
			return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
			{
				s,
				"Boolean"
			}));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00010FCB File Offset: 0x0000F1CB
		[__DynamicallyInvokable]
		public static char ToChar(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Res.GetString("XmlConvert_NotOneCharString"));
			}
			return s[0];
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00010FFB File Offset: 0x0000F1FB
		internal static Exception TryToChar(string s, out char result)
		{
			if (!char.TryParse(s, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Char"
				}));
			}
			return null;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00011029 File Offset: 0x0000F229
		[__DynamicallyInvokable]
		public static decimal ToDecimal(string s)
		{
			return decimal.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00011038 File Offset: 0x0000F238
		internal static Exception TryToDecimal(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Decimal"
				}));
			}
			return null;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001106D File Offset: 0x0000F26D
		internal static decimal ToInteger(string s)
		{
			return decimal.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001107B File Offset: 0x0000F27B
		internal static Exception TryToInteger(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Integer"
				}));
			}
			return null;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000110AF File Offset: 0x0000F2AF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(string s)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000110BD File Offset: 0x0000F2BD
		internal static Exception TryToSByte(string s, out sbyte result)
		{
			if (!sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"SByte"
				}));
			}
			return null;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000110F1 File Offset: 0x0000F2F1
		[__DynamicallyInvokable]
		public static short ToInt16(string s)
		{
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000110FF File Offset: 0x0000F2FF
		internal static Exception TryToInt16(string s, out short result)
		{
			if (!short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Int16"
				}));
			}
			return null;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00011133 File Offset: 0x0000F333
		[__DynamicallyInvokable]
		public static int ToInt32(string s)
		{
			return int.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00011141 File Offset: 0x0000F341
		internal static Exception TryToInt32(string s, out int result)
		{
			if (!int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Int32"
				}));
			}
			return null;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00011175 File Offset: 0x0000F375
		[__DynamicallyInvokable]
		public static long ToInt64(string s)
		{
			return long.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00011183 File Offset: 0x0000F383
		internal static Exception TryToInt64(string s, out long result)
		{
			if (!long.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Int64"
				}));
			}
			return null;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000111B7 File Offset: 0x0000F3B7
		[__DynamicallyInvokable]
		public static byte ToByte(string s)
		{
			return byte.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000111C5 File Offset: 0x0000F3C5
		internal static Exception TryToByte(string s, out byte result)
		{
			if (!byte.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Byte"
				}));
			}
			return null;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000111F9 File Offset: 0x0000F3F9
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(string s)
		{
			return ushort.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00011207 File Offset: 0x0000F407
		internal static Exception TryToUInt16(string s, out ushort result)
		{
			if (!ushort.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"UInt16"
				}));
			}
			return null;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001123B File Offset: 0x0000F43B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(string s)
		{
			return uint.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011249 File Offset: 0x0000F449
		internal static Exception TryToUInt32(string s, out uint result)
		{
			if (!uint.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"UInt32"
				}));
			}
			return null;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001127D File Offset: 0x0000F47D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(string s)
		{
			return ulong.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001128B File Offset: 0x0000F48B
		internal static Exception TryToUInt64(string s, out ulong result)
		{
			if (!ulong.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"UInt64"
				}));
			}
			return null;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000112C0 File Offset: 0x0000F4C0
		[__DynamicallyInvokable]
		public static float ToSingle(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return float.NegativeInfinity;
			}
			if (s == "INF")
			{
				return float.PositiveInfinity;
			}
			float num = float.Parse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
			if (num == 0f && s[0] == '-')
			{
				return --0f;
			}
			return num;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00011328 File Offset: 0x0000F528
		internal static Exception TryToSingle(string s, out float result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = float.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = float.PositiveInfinity;
				return null;
			}
			if (!float.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Single"
				}));
			}
			if (result == 0f && s[0] == '-')
			{
				result = --0f;
			}
			return null;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000113BC File Offset: 0x0000F5BC
		[__DynamicallyInvokable]
		public static double ToDouble(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return double.NegativeInfinity;
			}
			if (s == "INF")
			{
				return double.PositiveInfinity;
			}
			double num = double.Parse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			if (num == 0.0 && s[0] == '-')
			{
				return --0.0;
			}
			return num;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00011434 File Offset: 0x0000F634
		internal static Exception TryToDouble(string s, out double result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = double.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = double.PositiveInfinity;
				return null;
			}
			if (!double.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Double"
				}));
			}
			if (result == 0.0 && s[0] == '-')
			{
				result = --0.0;
			}
			return null;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000114D8 File Offset: 0x0000F6D8
		internal static double ToXPathDouble(object o)
		{
			string text = o as string;
			if (text != null)
			{
				text = XmlConvert.TrimString(text);
				double result;
				if (text.Length != 0 && text[0] != '+' && double.TryParse(text, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result))
				{
					return result;
				}
				return double.NaN;
			}
			else
			{
				if (o is double)
				{
					return (double)o;
				}
				if (!(o is bool))
				{
					try
					{
						return Convert.ToDouble(o, NumberFormatInfo.InvariantInfo);
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					catch (ArgumentNullException)
					{
					}
					return double.NaN;
				}
				if (!(bool)o)
				{
					return 0.0;
				}
				return 1.0;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000115A4 File Offset: 0x0000F7A4
		internal static string ToXPathString(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			if (value is double)
			{
				return ((double)value).ToString("R", NumberFormatInfo.InvariantInfo);
			}
			if (!(value is bool))
			{
				return Convert.ToString(value, NumberFormatInfo.InvariantInfo);
			}
			if (!(bool)value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011608 File Offset: 0x0000F808
		internal static double XPathRound(double value)
		{
			double num = Math.Round(value);
			if (value - num != 0.5)
			{
				return num;
			}
			return num + 1.0;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011638 File Offset: 0x0000F838
		[__DynamicallyInvokable]
		public static TimeSpan ToTimeSpan(string s)
		{
			XsdDuration xsdDuration;
			try
			{
				xsdDuration = new XsdDuration(s);
			}
			catch (Exception)
			{
				throw new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"TimeSpan"
				}));
			}
			return xsdDuration.ToTimeSpan();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001168C File Offset: 0x0000F88C
		internal static Exception TryToTimeSpan(string s, out TimeSpan result)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, out xsdDuration);
			if (ex != null)
			{
				result = TimeSpan.MinValue;
				return ex;
			}
			return xsdDuration.TryToTimeSpan(out result);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000116BA File Offset: 0x0000F8BA
		private static string[] AllDateTimeFormats
		{
			get
			{
				if (XmlConvert.s_allDateTimeFormats == null)
				{
					XmlConvert.CreateAllDateTimeFormats();
				}
				return XmlConvert.s_allDateTimeFormats;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000116D4 File Offset: 0x0000F8D4
		private static void CreateAllDateTimeFormats()
		{
			if (XmlConvert.s_allDateTimeFormats == null)
			{
				XmlConvert.s_allDateTimeFormats = new string[]
				{
					"yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzz",
					"yyyy-MM-ddTHH:mm:ss.FFFFFFF",
					"yyyy-MM-ddTHH:mm:ss.FFFFFFFZ",
					"HH:mm:ss.FFFFFFF",
					"HH:mm:ss.FFFFFFFZ",
					"HH:mm:ss.FFFFFFFzzzzzz",
					"yyyy-MM-dd",
					"yyyy-MM-ddZ",
					"yyyy-MM-ddzzzzzz",
					"yyyy-MM",
					"yyyy-MMZ",
					"yyyy-MMzzzzzz",
					"yyyy",
					"yyyyZ",
					"yyyyzzzzzz",
					"--MM-dd",
					"--MM-ddZ",
					"--MM-ddzzzzzz",
					"---dd",
					"---ddZ",
					"---ddzzzzzz",
					"--MM--",
					"--MM--Z",
					"--MM--zzzzzz"
				};
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000117CA File Offset: 0x0000F9CA
		[Obsolete("Use XmlConvert.ToDateTime() that takes in XmlDateTimeSerializationMode")]
		public static DateTime ToDateTime(string s)
		{
			return XmlConvert.ToDateTime(s, XmlConvert.AllDateTimeFormats);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000117D7 File Offset: 0x0000F9D7
		public static DateTime ToDateTime(string s, string format)
		{
			return DateTime.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000117E6 File Offset: 0x0000F9E6
		public static DateTime ToDateTime(string s, string[] formats)
		{
			return DateTime.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000117F8 File Offset: 0x0000F9F8
		[__DynamicallyInvokable]
		public static DateTime ToDateTime(string s, XmlDateTimeSerializationMode dateTimeOption)
		{
			XsdDateTime xdt = new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
			DateTime dateTime = xdt;
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				dateTime = XmlConvert.SwitchToLocalTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Utc:
				dateTime = XmlConvert.SwitchToUtcTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("Sch_InvalidDateTimeOption", new object[]
				{
					dateTimeOption,
					"dateTimeOption"
				}));
			}
			return dateTime;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001187C File Offset: 0x0000FA7C
		[__DynamicallyInvokable]
		public static DateTimeOffset ToDateTimeOffset(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			XsdDateTime xdt = new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
			return xdt;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000118AC File Offset: 0x0000FAAC
		[__DynamicallyInvokable]
		public static DateTimeOffset ToDateTimeOffset(string s, string format)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000118C9 File Offset: 0x0000FAC9
		[__DynamicallyInvokable]
		public static DateTimeOffset ToDateTimeOffset(string s, string[] formats)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000118E6 File Offset: 0x0000FAE6
		[__DynamicallyInvokable]
		public static Guid ToGuid(string s)
		{
			return new Guid(s);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000118F0 File Offset: 0x0000FAF0
		internal static Exception TryToGuid(string s, out Guid result)
		{
			Exception result2 = null;
			result = Guid.Empty;
			try
			{
				result = new Guid(s);
			}
			catch (ArgumentException)
			{
				result2 = new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Guid"
				}));
			}
			catch (FormatException)
			{
				result2 = new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Guid"
				}));
			}
			return result2;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011980 File Offset: 0x0000FB80
		private static DateTime SwitchToLocalTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Local);
			case DateTimeKind.Utc:
				return value.ToLocalTime();
			case DateTimeKind.Local:
				return value;
			default:
				return value;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000119C4 File Offset: 0x0000FBC4
		private static DateTime SwitchToUtcTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Utc);
			case DateTimeKind.Utc:
				return value;
			case DateTimeKind.Local:
				return value.ToUniversalTime();
			default:
				return value;
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011A08 File Offset: 0x0000FC08
		internal static Uri ToUri(string s)
		{
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					throw new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
					{
						s,
						"Uri"
					}));
				}
			}
			Uri result;
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out result))
			{
				throw new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Uri"
				}));
			}
			return result;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011A90 File Offset: 0x0000FC90
		internal static Exception TryToUri(string s, out Uri result)
		{
			result = null;
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
					{
						s,
						"Uri"
					}));
				}
			}
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out result))
			{
				return new FormatException(Res.GetString("XmlConvert_BadFormat", new object[]
				{
					s,
					"Uri"
				}));
			}
			return null;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011B1C File Offset: 0x0000FD1C
		internal static bool StrEqual(char[] chars, int strPos1, int strLen1, string str2)
		{
			if (strLen1 != str2.Length)
			{
				return false;
			}
			int num = 0;
			while (num < strLen1 && chars[strPos1 + num] == str2[num])
			{
				num++;
			}
			return num == strLen1;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011B52 File Offset: 0x0000FD52
		internal static string TrimString(string value)
		{
			return value.Trim(XmlConvert.WhitespaceChars);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00011B5F File Offset: 0x0000FD5F
		internal static string TrimStringStart(string value)
		{
			return value.TrimStart(XmlConvert.WhitespaceChars);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00011B6C File Offset: 0x0000FD6C
		internal static string TrimStringEnd(string value)
		{
			return value.TrimEnd(XmlConvert.WhitespaceChars);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00011B79 File Offset: 0x0000FD79
		internal static string[] SplitString(string value)
		{
			return value.Split(XmlConvert.WhitespaceChars, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011B87 File Offset: 0x0000FD87
		internal static string[] SplitString(string value, StringSplitOptions splitStringOptions)
		{
			return value.Split(XmlConvert.WhitespaceChars, splitStringOptions);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00011B95 File Offset: 0x0000FD95
		internal static bool IsNegativeZero(double value)
		{
			return value == 0.0 && XmlConvert.DoubleToInt64Bits(value) == XmlConvert.DoubleToInt64Bits(--0.0);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00011BBC File Offset: 0x0000FDBC
		private unsafe static long DoubleToInt64Bits(double value)
		{
			return *(long*)(&value);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00011BC2 File Offset: 0x0000FDC2
		internal static void VerifyCharData(string data, ExceptionType exceptionType)
		{
			XmlConvert.VerifyCharData(data, exceptionType, exceptionType);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00011BCC File Offset: 0x0000FDCC
		internal unsafe static void VerifyCharData(string data, ExceptionType invCharExceptionType, ExceptionType invSurrogateExceptionType)
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			int num = 0;
			int length = data.Length;
			for (;;)
			{
				if (num >= length || (XmlConvert.xmlCharType.charProperties[data[num]] & 16) == 0)
				{
					if (num == length)
					{
						break;
					}
					char ch = data[num];
					if (!XmlCharType.IsHighSurrogate((int)ch))
					{
						goto IL_95;
					}
					if (num + 1 == length)
					{
						goto Block_5;
					}
					ch = data[num + 1];
					if (!XmlCharType.IsLowSurrogate((int)ch))
					{
						goto IL_7A;
					}
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return;
			Block_5:
			throw XmlConvert.CreateException("Xml_InvalidSurrogateMissingLowChar", invSurrogateExceptionType, 0, num + 1);
			IL_7A:
			throw XmlConvert.CreateInvalidSurrogatePairException(data[num + 1], data[num], invSurrogateExceptionType, 0, num + 1);
			IL_95:
			throw XmlConvert.CreateInvalidCharException(data, num, invCharExceptionType);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00011C78 File Offset: 0x0000FE78
		internal unsafe static void VerifyCharData(char[] data, int offset, int len, ExceptionType exceptionType)
		{
			if (data == null || len == 0)
			{
				return;
			}
			int num = offset;
			int num2 = offset + len;
			for (;;)
			{
				if (num >= num2 || (XmlConvert.xmlCharType.charProperties[data[num]] & 16) == 0)
				{
					if (num == num2)
					{
						break;
					}
					char ch = data[num];
					if (!XmlCharType.IsHighSurrogate((int)ch))
					{
						goto IL_7D;
					}
					if (num + 1 == num2)
					{
						goto Block_5;
					}
					ch = data[num + 1];
					if (!XmlCharType.IsLowSurrogate((int)ch))
					{
						goto IL_68;
					}
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return;
			Block_5:
			throw XmlConvert.CreateException("Xml_InvalidSurrogateMissingLowChar", exceptionType, 0, offset - num + 1);
			IL_68:
			throw XmlConvert.CreateInvalidSurrogatePairException(data[num + 1], data[num], exceptionType, 0, offset - num + 1);
			IL_7D:
			throw XmlConvert.CreateInvalidCharException(data, len, num, exceptionType);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00011D0C File Offset: 0x0000FF0C
		internal static string EscapeValueForDebuggerDisplay(string value)
		{
			StringBuilder stringBuilder = null;
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c < ' ' || c == '"')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(value.Length + 4);
					}
					if (i - num > 0)
					{
						stringBuilder.Append(value, num, i - num);
					}
					num = i + 1;
					switch (c)
					{
					case '\t':
						stringBuilder.Append("\\t");
						goto IL_A9;
					case '\n':
						stringBuilder.Append("\\n");
						goto IL_A9;
					case '\v':
					case '\f':
						break;
					case '\r':
						stringBuilder.Append("\\r");
						goto IL_A9;
					default:
						if (c == '"')
						{
							stringBuilder.Append("\\\"");
							goto IL_A9;
						}
						break;
					}
					stringBuilder.Append(c);
				}
				IL_A9:
				i++;
			}
			if (stringBuilder == null)
			{
				return value;
			}
			if (i - num > 0)
			{
				stringBuilder.Append(value, num, i - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00011DEF File Offset: 0x0000FFEF
		internal static Exception CreateException(string res, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, exceptionType, 0, 0);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00011DFA File Offset: 0x0000FFFA
		internal static Exception CreateException(string res, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, string.Empty, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res));
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00011E1E File Offset: 0x0001001E
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, arg, exceptionType, 0, 0);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00011E2A File Offset: 0x0001002A
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, arg, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, new object[]
			{
				arg
			}));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00011E55 File Offset: 0x00010055
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, args, exceptionType, 0, 0);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00011E64 File Offset: 0x00010064
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, args, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, args));
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00011E93 File Offset: 0x00010093
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, ExceptionType.ArgumentException);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00011E9D File Offset: 0x0001009D
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, exceptionType, 0, 0);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00011EAC File Offset: 0x000100AC
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string[] array = new string[2];
			int num = 0;
			uint num2 = (uint)hi;
			array[num] = num2.ToString("X", CultureInfo.InvariantCulture);
			int num3 = 1;
			num2 = (uint)low;
			array[num3] = num2.ToString("X", CultureInfo.InvariantCulture);
			string[] args = array;
			return XmlConvert.CreateException("Xml_InvalidSurrogatePairWithArgs", args, exceptionType, lineNo, linePos);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00011EFB File Offset: 0x000100FB
		internal static Exception CreateInvalidHighSurrogateCharException(char hi)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, ExceptionType.ArgumentException);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00011F04 File Offset: 0x00010104
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, exceptionType, 0, 0);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00011F10 File Offset: 0x00010110
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string res = "Xml_InvalidSurrogateHighChar";
			uint num = (uint)hi;
			return XmlConvert.CreateException(res, num.ToString("X", CultureInfo.InvariantCulture), exceptionType, lineNo, linePos);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011F3D File Offset: 0x0001013D
		internal static Exception CreateInvalidCharException(char[] data, int length, int invCharPos)
		{
			return XmlConvert.CreateInvalidCharException(data, length, invCharPos, ExceptionType.ArgumentException);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00011F48 File Offset: 0x00010148
		internal static Exception CreateInvalidCharException(char[] data, int length, int invCharPos, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(data, length, invCharPos), exceptionType, 0, invCharPos + 1);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00011F61 File Offset: 0x00010161
		internal static Exception CreateInvalidCharException(string data, int invCharPos)
		{
			return XmlConvert.CreateInvalidCharException(data, invCharPos, ExceptionType.ArgumentException);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00011F6B File Offset: 0x0001016B
		internal static Exception CreateInvalidCharException(string data, int invCharPos, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(data, invCharPos), exceptionType, 0, invCharPos + 1);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00011F83 File Offset: 0x00010183
		internal static Exception CreateInvalidCharException(char invChar, char nextChar)
		{
			return XmlConvert.CreateInvalidCharException(invChar, nextChar, ExceptionType.ArgumentException);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00011F8D File Offset: 0x0001018D
		internal static Exception CreateInvalidCharException(char invChar, char nextChar, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("Xml_InvalidCharacter", XmlException.BuildCharExceptionArgs(invChar, nextChar), exceptionType);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00011FA1 File Offset: 0x000101A1
		internal static Exception CreateInvalidNameCharException(string name, int index, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException((index == 0) ? "Xml_BadStartNameChar" : "Xml_BadNameChar", XmlException.BuildCharExceptionArgs(name, index), exceptionType, 0, index + 1);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00011FC3 File Offset: 0x000101C3
		internal static ArgumentException CreateInvalidNameArgumentException(string name, string argumentName)
		{
			if (name != null)
			{
				return new ArgumentException(Res.GetString("Xml_EmptyName"), argumentName);
			}
			return new ArgumentNullException(argumentName);
		}

		// Token: 0x040001E6 RID: 486
		private static XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040001E7 RID: 487
		internal static char[] crt = new char[]
		{
			'\n',
			'\r',
			'\t'
		};

		// Token: 0x040001E8 RID: 488
		private static readonly int c_EncodedCharLength = 7;

		// Token: 0x040001E9 RID: 489
		private static volatile Regex c_EncodeCharPattern;

		// Token: 0x040001EA RID: 490
		private static volatile Regex c_DecodeCharPattern;

		// Token: 0x040001EB RID: 491
		private static volatile string[] s_allDateTimeFormats;

		// Token: 0x040001EC RID: 492
		internal static readonly char[] WhitespaceChars = new char[]
		{
			' ',
			'\t',
			'\n',
			'\r'
		};
	}
}

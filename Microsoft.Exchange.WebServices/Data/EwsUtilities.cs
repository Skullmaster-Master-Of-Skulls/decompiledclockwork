using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D2 RID: 210
	internal static class EwsUtilities
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0001EC5B File Offset: 0x0001DC5B
		internal static void Assert(bool condition, string caller, string message)
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001EC60 File Offset: 0x0001DC60
		internal static string GetNamespacePrefix(XmlNamespace xmlNamespace)
		{
			switch (xmlNamespace)
			{
			case XmlNamespace.Messages:
				return "m";
			case XmlNamespace.Types:
				return "t";
			case XmlNamespace.Errors:
				return "e";
			case XmlNamespace.Soap:
			case XmlNamespace.Soap12:
				return "soap";
			case XmlNamespace.XmlSchemaInstance:
				return "xsi";
			case XmlNamespace.PassportSoapFault:
				return "psf";
			case XmlNamespace.WSTrustFebruary2005:
				return "wst";
			case XmlNamespace.WSAddressing:
				return "wsa";
			case XmlNamespace.Autodiscover:
				return "a";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001ECDC File Offset: 0x0001DCDC
		internal static string GetNamespaceUri(XmlNamespace xmlNamespace)
		{
			switch (xmlNamespace)
			{
			case XmlNamespace.Messages:
				return "http://schemas.microsoft.com/exchange/services/2006/messages";
			case XmlNamespace.Types:
				return "http://schemas.microsoft.com/exchange/services/2006/types";
			case XmlNamespace.Errors:
				return "http://schemas.microsoft.com/exchange/services/2006/errors";
			case XmlNamespace.Soap:
				return "http://schemas.xmlsoap.org/soap/envelope/";
			case XmlNamespace.Soap12:
				return "http://www.w3.org/2003/05/soap-envelope";
			case XmlNamespace.XmlSchemaInstance:
				return "http://www.w3.org/2001/XMLSchema-instance";
			case XmlNamespace.PassportSoapFault:
				return "http://schemas.microsoft.com/Passport/SoapServices/SOAPFault";
			case XmlNamespace.WSTrustFebruary2005:
				return "http://schemas.xmlsoap.org/ws/2005/02/trust";
			case XmlNamespace.WSAddressing:
				return "http://www.w3.org/2005/08/addressing";
			case XmlNamespace.Autodiscover:
				return "http://schemas.microsoft.com/exchange/2010/Autodiscover";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001ED60 File Offset: 0x0001DD60
		internal static XmlNamespace GetNamespaceFromUri(string namespaceUri)
		{
			switch (namespaceUri)
			{
			case "http://schemas.microsoft.com/exchange/services/2006/errors":
				return XmlNamespace.Errors;
			case "http://schemas.microsoft.com/exchange/services/2006/types":
				return XmlNamespace.Types;
			case "http://schemas.microsoft.com/exchange/services/2006/messages":
				return XmlNamespace.Messages;
			case "http://schemas.xmlsoap.org/soap/envelope/":
				return XmlNamespace.Soap;
			case "http://www.w3.org/2003/05/soap-envelope":
				return XmlNamespace.Soap12;
			case "http://www.w3.org/2001/XMLSchema-instance":
				return XmlNamespace.XmlSchemaInstance;
			case "http://schemas.microsoft.com/Passport/SoapServices/SOAPFault":
				return XmlNamespace.PassportSoapFault;
			case "http://schemas.xmlsoap.org/ws/2005/02/trust":
				return XmlNamespace.WSTrustFebruary2005;
			case "http://www.w3.org/2005/08/addressing":
				return XmlNamespace.WSAddressing;
			}
			return XmlNamespace.NotSpecified;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001EE4C File Offset: 0x0001DE4C
		internal static TServiceObject CreateEwsObjectFromXmlElementName<TServiceObject>(ExchangeService service, string xmlElementName) where TServiceObject : ServiceObject
		{
			Type key;
			if (!EwsUtilities.serviceObjectInfo.Member.XmlElementNameToServiceObjectClassMap.TryGetValue(xmlElementName, out key))
			{
				return default(TServiceObject);
			}
			CreateServiceObjectWithServiceParam createServiceObjectWithServiceParam;
			if (EwsUtilities.serviceObjectInfo.Member.ServiceObjectConstructorsWithServiceParam.TryGetValue(key, out createServiceObjectWithServiceParam))
			{
				return (TServiceObject)((object)createServiceObjectWithServiceParam(service));
			}
			throw new ArgumentException(Strings.NoAppropriateConstructorForItemClass);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001EEB4 File Offset: 0x0001DEB4
		internal static Item CreateItemFromItemClass(ItemAttachment itemAttachment, Type itemClass, bool isNew)
		{
			CreateServiceObjectWithAttachmentParam createServiceObjectWithAttachmentParam;
			if (EwsUtilities.serviceObjectInfo.Member.ServiceObjectConstructorsWithAttachmentParam.TryGetValue(itemClass, out createServiceObjectWithAttachmentParam))
			{
				return (Item)createServiceObjectWithAttachmentParam(itemAttachment, isNew);
			}
			throw new ArgumentException(Strings.NoAppropriateConstructorForItemClass);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001EEF8 File Offset: 0x0001DEF8
		internal static Item CreateItemFromXmlElementName(ItemAttachment itemAttachment, string xmlElementName)
		{
			Type itemClass;
			if (EwsUtilities.serviceObjectInfo.Member.XmlElementNameToServiceObjectClassMap.TryGetValue(xmlElementName, out itemClass))
			{
				return EwsUtilities.CreateItemFromItemClass(itemAttachment, itemClass, false);
			}
			return null;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001EF28 File Offset: 0x0001DF28
		internal static Type GetItemTypeFromXmlElementName(string xmlElementName)
		{
			Type result = null;
			EwsUtilities.serviceObjectInfo.Member.XmlElementNameToServiceObjectClassMap.TryGetValue(xmlElementName, out result);
			return result;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001EF50 File Offset: 0x0001DF50
		internal static TItem FindFirstItemOfType<TItem>(IEnumerable<Item> items) where TItem : Item
		{
			Type typeFromHandle = typeof(TItem);
			foreach (Item item in items)
			{
				if (item.GetType() == typeFromHandle)
				{
					return (TItem)((object)item);
				}
			}
			return default(TItem);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001EFBC File Offset: 0x0001DFBC
		private static void WriteTraceStartElement(XmlWriter writer, string traceTag, bool includeVersion)
		{
			writer.WriteStartElement("Trace");
			writer.WriteAttributeString("Tag", traceTag);
			writer.WriteAttributeString("Tid", Thread.CurrentThread.ManagedThreadId.ToString());
			writer.WriteAttributeString("Time", DateTime.UtcNow.ToString("u", DateTimeFormatInfo.InvariantInfo));
			if (includeVersion)
			{
				writer.WriteAttributeString("Version", EwsUtilities.BuildVersion);
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001F034 File Offset: 0x0001E034
		internal static string FormatLogMessage(string entryKind, string logEntry)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					EwsUtilities.WriteTraceStartElement(xmlTextWriter, entryKind, false);
					xmlTextWriter.WriteWhitespace(Environment.NewLine);
					xmlTextWriter.WriteValue(logEntry);
					xmlTextWriter.WriteWhitespace(Environment.NewLine);
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteWhitespace(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001F0CC File Offset: 0x0001E0CC
		private static void FormatHttpHeaders(StringBuilder sb, WebHeaderCollection headers)
		{
			foreach (object obj in headers.Keys)
			{
				string text = (string)obj;
				sb.Append(string.Format("{0}: {1}\n", text, headers[text]));
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001F138 File Offset: 0x0001E138
		internal static string FormatHttpRequestHeaders(IEwsHttpWebRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("{0} {1} HTTP/1.1\n", request.Method, request.RequestUri.AbsolutePath));
			EwsUtilities.FormatHttpHeaders(stringBuilder, request.Headers);
			stringBuilder.Append("\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001F18C File Offset: 0x0001E18C
		internal static string FormatHttpResponseHeaders(IEwsHttpWebResponse response)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("HTTP/{0} {1} {2}\n", response.ProtocolVersion, (int)response.StatusCode, response.StatusDescription));
			stringBuilder.Append(EwsUtilities.FormatHttpHeaders(response.Headers));
			stringBuilder.Append("\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001F1EC File Offset: 0x0001E1EC
		internal static string FormatHttpRequestHeaders(HttpWebRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("{0} {1} HTTP/{2}\n", request.Method.ToUpperInvariant(), request.RequestUri.AbsolutePath, request.ProtocolVersion));
			stringBuilder.Append(EwsUtilities.FormatHttpHeaders(request.Headers));
			stringBuilder.Append("\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001F250 File Offset: 0x0001E250
		private static string FormatHttpHeaders(WebHeaderCollection headers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in headers.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append(string.Format("{0}: {1}\n", text, headers[text]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001F2C8 File Offset: 0x0001E2C8
		internal static string FormatLogMessageWithXmlContent(string entryKind, MemoryStream memoryStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			xmlReaderSettings.IgnoreComments = true;
			xmlReaderSettings.IgnoreWhitespace = true;
			xmlReaderSettings.CloseInput = false;
			long position = memoryStream.Position;
			memoryStream.Position = 0L;
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(memoryStream, xmlReaderSettings))
				{
					using (StringWriter stringWriter = new StringWriter(stringBuilder))
					{
						using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
						{
							xmlTextWriter.Formatting = Formatting.Indented;
							EwsUtilities.WriteTraceStartElement(xmlTextWriter, entryKind, true);
							while (!xmlReader.EOF)
							{
								xmlTextWriter.WriteNode(xmlReader, true);
							}
							xmlTextWriter.WriteEndElement();
							xmlTextWriter.WriteWhitespace(Environment.NewLine);
						}
					}
				}
			}
			catch (XmlException)
			{
				stringBuilder.Length = 0;
				memoryStream.Position = 0L;
				stringBuilder.Append(Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
			}
			memoryStream.Position = position;
			return stringBuilder.ToString();
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001F3F8 File Offset: 0x0001E3F8
		internal static void CopyStream(Stream source, Stream target)
		{
			MemoryStream memoryStream = source as MemoryStream;
			if (memoryStream != null)
			{
				memoryStream.WriteTo(target);
				return;
			}
			byte[] array = new byte[4096];
			int count = array.Length;
			for (int i = source.Read(array, 0, count); i > 0; i = source.Read(array, 0, count))
			{
				target.Write(array, 0, i);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001F449 File Offset: 0x0001E449
		internal static string BuildVersion
		{
			get
			{
				return EwsUtilities.buildVersion.Member;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001F455 File Offset: 0x0001E455
		internal static string BoolToXSBool(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001F468 File Offset: 0x0001E468
		internal static void ParseEnumValueList<T>(IList<T> list, string value, params char[] separators) where T : struct
		{
			EwsUtilities.Assert(typeof(T).IsEnum, "EwsUtilities.ParseEnumValueList", "T is not an enum type.");
			string[] array = value.Split(separators);
			foreach (string value2 in array)
			{
				list.Add((T)((object)Enum.Parse(typeof(T), value2, false)));
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001F4CC File Offset: 0x0001E4CC
		internal static string SerializeEnum(Enum value)
		{
			Dictionary<Enum, string> dictionary;
			string result;
			if (EwsUtilities.enumToSchemaDictionaries.Member.TryGetValue(value.GetType(), out dictionary) && dictionary.TryGetValue(value, out result))
			{
				return result;
			}
			return value.ToString();
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001F508 File Offset: 0x0001E508
		internal static T Parse<T>(string value)
		{
			if (!typeof(T).IsEnum)
			{
				return (T)((object)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
			}
			Dictionary<string, Enum> dictionary;
			Enum @enum;
			if (EwsUtilities.schemaToEnumDictionaries.Member.TryGetValue(typeof(T), out dictionary) && dictionary.TryGetValue(value, out @enum))
			{
				return (T)((object)@enum);
			}
			return (T)((object)Enum.Parse(typeof(T), value, false));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001F588 File Offset: 0x0001E588
		internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			DateTime result;
			try
			{
				result = TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone);
			}
			catch (ArgumentException innerException)
			{
				throw new TimeZoneConversionException(string.Format(Strings.CannotConvertBetweenTimeZones, EwsUtilities.DateTimeToXSDateTime(dateTime), sourceTimeZone.DisplayName, destinationTimeZone.DisplayName), innerException);
			}
			return result;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001F5DC File Offset: 0x0001E5DC
		internal static DateTime ParseAsUnbiasedDatetimescopedToServicetimeZone(string dateString, ExchangeService service)
		{
			DateTime dateTime = DateTime.Parse(dateString, CultureInfo.InvariantCulture);
			if (service.TimeZone == TimeZoneInfo.Utc)
			{
				return new DateTime(dateTime.Ticks, DateTimeKind.Utc);
			}
			if (EwsUtilities.IsLocalTimeZone(service.TimeZone))
			{
				return new DateTime(dateTime.Ticks, DateTimeKind.Local);
			}
			return new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001F638 File Offset: 0x0001E638
		internal static bool IsLocalTimeZone(TimeZoneInfo timeZone)
		{
			return TimeZoneInfo.Local == timeZone || (TimeZoneInfo.Local.Id == timeZone.Id && TimeZoneInfo.Local.HasSameRules(timeZone));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001F668 File Offset: 0x0001E668
		internal static string DateTimeToXSDate(DateTime date)
		{
			string format;
			switch (date.Kind)
			{
			case DateTimeKind.Unspecified:
				format = "yyyy-MM-dd";
				break;
			case DateTimeKind.Utc:
				format = "yyyy-MM-ddZ";
				break;
			default:
				format = "yyyy-MM-ddzzz";
				break;
			}
			return date.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001F6B0 File Offset: 0x0001E6B0
		internal static string DateTimeToXSDateTime(DateTime dateTime)
		{
			string text = "yyyy-MM-ddTHH:mm:ss.fff";
			switch (dateTime.Kind)
			{
			case DateTimeKind.Utc:
				text += "Z";
				break;
			case DateTimeKind.Local:
				text += "zzz";
				break;
			}
			return dateTime.ToString(text, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001F704 File Offset: 0x0001E704
		internal static DayOfWeek EwsToSystemDayOfWeek(DayOfTheWeek dayOfTheWeek)
		{
			if (dayOfTheWeek == DayOfTheWeek.Day || dayOfTheWeek == DayOfTheWeek.Weekday || dayOfTheWeek == DayOfTheWeek.WeekendDay)
			{
				throw new ArgumentException(string.Format("Cannot convert {0} to System.DayOfWeek enum value", dayOfTheWeek), "dayOfTheWeek");
			}
			return (DayOfWeek)dayOfTheWeek;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001F72F File Offset: 0x0001E72F
		internal static DayOfTheWeek SystemToEwsDayOfTheWeek(DayOfWeek dayOfWeek)
		{
			return (DayOfTheWeek)dayOfWeek;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001F734 File Offset: 0x0001E734
		internal static string TimeSpanToXSDuration(TimeSpan timeSpan)
		{
			string text = (timeSpan.TotalSeconds < 0.0) ? "-" : string.Empty;
			return string.Format("{0}P{1}DT{2}H{3}M{4}S", new object[]
			{
				text,
				Math.Abs(timeSpan.Days),
				Math.Abs(timeSpan.Hours),
				Math.Abs(timeSpan.Minutes),
				Math.Abs(timeSpan.Seconds) + "." + Math.Abs(timeSpan.Milliseconds)
			});
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001F7E4 File Offset: 0x0001E7E4
		internal static TimeSpan XSDurationToTimeSpan(string xsDuration)
		{
			Regex regex = new Regex("(?<pos>-)?P((?<year>[0-9]+)Y)?((?<month>[0-9]+)M)?((?<day>[0-9]+)D)?(T((?<hour>[0-9]+)H)?((?<minute>[0-9]+)M)?((?<seconds>[0-9]+)(\\.(?<precision>[0-9]+))?S)?)?");
			Match match = regex.Match(xsDuration);
			if (!match.Success)
			{
				throw new ArgumentException(Strings.XsDurationCouldNotBeParsed);
			}
			string text = match.Result("${pos}");
			bool flag = false;
			if (!string.IsNullOrEmpty(text))
			{
				flag = true;
			}
			text = match.Result("${year}");
			int num = 0;
			if (!string.IsNullOrEmpty(text))
			{
				num = int.Parse(text);
			}
			text = match.Result("${month}");
			int num2 = 0;
			if (!string.IsNullOrEmpty(text))
			{
				num2 = int.Parse(text);
			}
			text = match.Result("${day}");
			int num3 = 0;
			if (!string.IsNullOrEmpty(text))
			{
				num3 = int.Parse(text);
			}
			text = match.Result("${hour}");
			int hours = 0;
			if (!string.IsNullOrEmpty(text))
			{
				hours = int.Parse(text);
			}
			text = match.Result("${minute}");
			int minutes = 0;
			if (!string.IsNullOrEmpty(text))
			{
				minutes = int.Parse(text);
			}
			text = match.Result("${seconds}");
			int seconds = 0;
			if (!string.IsNullOrEmpty(text))
			{
				seconds = int.Parse(text);
			}
			int milliseconds = 0;
			text = match.Result("${precision}");
			if (text.Length > 4)
			{
				text = text.Substring(0, 4);
			}
			if (!string.IsNullOrEmpty(text))
			{
				milliseconds = int.Parse(text);
			}
			num3 = num3 + num * 365 + num2 * 30;
			TimeSpan timeSpan = new TimeSpan(num3, hours, minutes, seconds, milliseconds);
			if (flag)
			{
				timeSpan = -timeSpan;
			}
			return timeSpan;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001F951 File Offset: 0x0001E951
		public static string TimeSpanToXSTime(TimeSpan timeSpan)
		{
			return string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001F98C File Offset: 0x0001E98C
		public static string GetPrintableTypeName(Type type)
		{
			if (type.IsGenericType)
			{
				string value = type.Name.Substring(0, type.Name.IndexOf('`'));
				StringBuilder stringBuilder = new StringBuilder(value);
				string[] value2 = type.GetGenericArguments().ToList<Type>().ConvertAll<string>((Type t) => EwsUtilities.GetPrintableTypeName(t)).ToArray<string>();
				stringBuilder.Append("<");
				stringBuilder.Append(string.Join(",", value2));
				stringBuilder.Append(">");
				return stringBuilder.ToString();
			}
			if (type.IsArray)
			{
				string typeName = type.Name.Substring(0, type.Name.IndexOf('['));
				StringBuilder stringBuilder2 = new StringBuilder(EwsUtilities.GetSimplifiedTypeName(typeName));
				for (int i = 0; i < type.GetArrayRank(); i++)
				{
					stringBuilder2.Append("[]");
				}
				return stringBuilder2.ToString();
			}
			return EwsUtilities.GetSimplifiedTypeName(type.Name);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001FA90 File Offset: 0x0001EA90
		private static string GetSimplifiedTypeName(string typeName)
		{
			string result;
			if (!EwsUtilities.typeNameToShortNameMap.Member.TryGetValue(typeName, out result))
			{
				return typeName;
			}
			return result;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001FAB4 File Offset: 0x0001EAB4
		internal static string DomainFromEmailAddress(string emailAddress)
		{
			string[] array = emailAddress.Split(new char[]
			{
				'@'
			});
			if (array.Length != 2 || string.IsNullOrEmpty(array[1]))
			{
				throw new FormatException(Strings.InvalidEmailAddress);
			}
			return array[1];
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001FAF8 File Offset: 0x0001EAF8
		internal static void ValidateParamAllowNull(object param, string paramName)
		{
			ISelfValidate selfValidate = param as ISelfValidate;
			if (selfValidate != null)
			{
				try
				{
					selfValidate.Validate();
				}
				catch (ServiceValidationException innerException)
				{
					throw new ArgumentException(Strings.ValidationFailed, paramName, innerException);
				}
			}
			ServiceObject serviceObject = param as ServiceObject;
			if (serviceObject != null && serviceObject.IsNew)
			{
				throw new ArgumentException(Strings.ObjectDoesNotHaveId, paramName);
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001FB60 File Offset: 0x0001EB60
		internal static void ValidateParam(object param, string paramName)
		{
			string text = param as string;
			bool flag;
			if (text != null)
			{
				flag = !string.IsNullOrEmpty(text);
			}
			else
			{
				flag = (param != null);
			}
			if (!flag)
			{
				throw new ArgumentNullException(paramName);
			}
			EwsUtilities.ValidateParamAllowNull(param, paramName);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001FB9C File Offset: 0x0001EB9C
		internal static void ValidateParamCollection(IEnumerable collection, string paramName)
		{
			EwsUtilities.ValidateParam(collection, paramName);
			int num = 0;
			foreach (object param in collection)
			{
				try
				{
					EwsUtilities.ValidateParam(param, string.Format("collection[{0}]", num));
				}
				catch (ArgumentException innerException)
				{
					throw new ArgumentException(string.Format("The element at position {0} is invalid", num), paramName, innerException);
				}
				num++;
			}
			if (num == 0)
			{
				throw new ArgumentException(Strings.CollectionIsEmpty, paramName);
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001FC50 File Offset: 0x0001EC50
		internal static void ValidateNonBlankStringParamAllowNull(string param, string paramName)
		{
			if (param != null)
			{
				if (param.Length == param.CountMatchingChars((char c) => char.IsWhiteSpace(c)))
				{
					throw new ArgumentException(Strings.ArgumentIsBlankString, paramName);
				}
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001FC9C File Offset: 0x0001EC9C
		internal static void ValidateNonBlankStringParam(string param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
			EwsUtilities.ValidateNonBlankStringParamAllowNull(param, paramName);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001FCB0 File Offset: 0x0001ECB0
		internal static void ValidateEnumVersionValue(Enum enumValue, ExchangeVersion requestVersion)
		{
			Type type = enumValue.GetType();
			Dictionary<Enum, ExchangeVersion> dictionary = EwsUtilities.enumVersionDictionaries.Member[type];
			ExchangeVersion exchangeVersion = dictionary[enumValue];
			if (requestVersion < exchangeVersion)
			{
				throw new ServiceVersionException(string.Format(Strings.EnumValueIncompatibleWithRequestVersion, enumValue.ToString(), type.Name, exchangeVersion));
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001FD08 File Offset: 0x0001ED08
		internal static void ValidateServiceObjectVersion(ServiceObject serviceObject, ExchangeVersion requestVersion)
		{
			ExchangeVersion minimumRequiredServerVersion = serviceObject.GetMinimumRequiredServerVersion();
			if (requestVersion < minimumRequiredServerVersion)
			{
				throw new ServiceVersionException(string.Format(Strings.ObjectTypeIncompatibleWithRequestVersion, serviceObject.GetType().Name, minimumRequiredServerVersion));
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001FD46 File Offset: 0x0001ED46
		internal static void ValidatePropertyVersion(ExchangeService service, ExchangeVersion minimumServerVersion, string propertyName)
		{
			if (service.RequestedServerVersion < minimumServerVersion)
			{
				throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, propertyName, minimumServerVersion));
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001FD6D File Offset: 0x0001ED6D
		internal static void ValidateMethodVersion(ExchangeService service, ExchangeVersion minimumServerVersion, string methodName)
		{
			if (service.RequestedServerVersion < minimumServerVersion)
			{
				throw new ServiceVersionException(string.Format(Strings.MethodIncompatibleWithRequestVersion, methodName, minimumServerVersion));
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001FD94 File Offset: 0x0001ED94
		internal static void ValidateClassVersion(ExchangeService service, ExchangeVersion minimumServerVersion, string className)
		{
			if (service.RequestedServerVersion < minimumServerVersion)
			{
				throw new ServiceVersionException(string.Format(Strings.ClassIncompatibleWithRequestVersion, className, minimumServerVersion));
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001FDBC File Offset: 0x0001EDBC
		internal static void ValidateDomainNameAllowNull(string domainName, string paramName)
		{
			if (domainName != null)
			{
				Regex regex = new Regex("^[-a-zA-Z0-9_.]+$");
				if (!regex.IsMatch(domainName))
				{
					throw new ArgumentException(string.Format(Strings.InvalidDomainName, domainName), paramName);
				}
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001FDF8 File Offset: 0x0001EDF8
		private static ExchangeVersion GetEnumVersion(Type enumType, string enumName)
		{
			MemberInfo[] member = enumType.GetMember(enumName);
			EwsUtilities.Assert(member != null && member.Length > 0, "EwsUtilities.GetEnumVersion", string.Concat(new object[]
			{
				"Enum member ",
				enumName,
				" not found in ",
				enumType
			}));
			object[] customAttributes = member[0].GetCustomAttributes(typeof(RequiredServerVersionAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				return ((RequiredServerVersionAttribute)customAttributes[0]).Version;
			}
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001FE74 File Offset: 0x0001EE74
		private static Dictionary<Enum, ExchangeVersion> BuildEnumDict(Type enumType)
		{
			Dictionary<Enum, ExchangeVersion> dictionary = new Dictionary<Enum, ExchangeVersion>();
			string[] names = Enum.GetNames(enumType);
			foreach (string text in names)
			{
				Enum key = (Enum)Enum.Parse(enumType, text, false);
				ExchangeVersion enumVersion = EwsUtilities.GetEnumVersion(enumType, text);
				dictionary.Add(key, enumVersion);
			}
			return dictionary;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001FECC File Offset: 0x0001EECC
		private static string GetEnumSchemaName(Type enumType, string enumName)
		{
			MemberInfo[] member = enumType.GetMember(enumName);
			EwsUtilities.Assert(member != null && member.Length > 0, "EwsUtilities.GetEnumSchemaName", string.Concat(new object[]
			{
				"Enum member ",
				enumName,
				" not found in ",
				enumType
			}));
			object[] customAttributes = member[0].GetCustomAttributes(typeof(EwsEnumAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				return ((EwsEnumAttribute)customAttributes[0]).SchemaName;
			}
			return null;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001FF48 File Offset: 0x0001EF48
		private static Dictionary<string, Enum> BuildSchemaToEnumDict(Type enumType)
		{
			Dictionary<string, Enum> dictionary = new Dictionary<string, Enum>();
			string[] names = Enum.GetNames(enumType);
			foreach (string text in names)
			{
				Enum value = (Enum)Enum.Parse(enumType, text, false);
				string enumSchemaName = EwsUtilities.GetEnumSchemaName(enumType, text);
				if (!string.IsNullOrEmpty(enumSchemaName))
				{
					dictionary.Add(enumSchemaName, value);
				}
			}
			return dictionary;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001FFA8 File Offset: 0x0001EFA8
		private static Dictionary<Enum, string> BuildEnumToSchemaDict(Type enumType)
		{
			Dictionary<Enum, string> dictionary = new Dictionary<Enum, string>();
			string[] names = Enum.GetNames(enumType);
			foreach (string text in names)
			{
				Enum key = (Enum)Enum.Parse(enumType, text, false);
				string enumSchemaName = EwsUtilities.GetEnumSchemaName(enumType, text);
				if (!string.IsNullOrEmpty(enumSchemaName))
				{
					dictionary.Add(key, enumSchemaName);
				}
			}
			return dictionary;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00020008 File Offset: 0x0001F008
		internal static int GetEnumeratedObjectCount(IEnumerable objects)
		{
			int num = 0;
			foreach (object obj in objects)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00020058 File Offset: 0x0001F058
		internal static object GetEnumeratedObjectAt(IEnumerable objects, int index)
		{
			int num = 0;
			foreach (object result in objects)
			{
				if (num == index)
				{
					return result;
				}
				num++;
			}
			throw new ArgumentOutOfRangeException("index", Strings.IEnumerableDoesNotContainThatManyObject);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000200C8 File Offset: 0x0001F0C8
		internal static int CountMatchingChars(this string str, Predicate<char> charPredicate)
		{
			int num = 0;
			foreach (char obj in str)
			{
				if (charPredicate(obj))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00020100 File Offset: 0x0001F100
		internal static bool TrueForAll<T>(this IEnumerable<T> collection, Predicate<T> predicate)
		{
			foreach (T obj in collection)
			{
				if (!predicate(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00020154 File Offset: 0x0001F154
		internal static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (T obj in collection)
			{
				action(obj);
			}
		}

		// Token: 0x040002D1 RID: 721
		internal const string XSFalse = "false";

		// Token: 0x040002D2 RID: 722
		internal const string XSTrue = "true";

		// Token: 0x040002D3 RID: 723
		internal const string EwsTypesNamespacePrefix = "t";

		// Token: 0x040002D4 RID: 724
		internal const string EwsMessagesNamespacePrefix = "m";

		// Token: 0x040002D5 RID: 725
		internal const string EwsErrorsNamespacePrefix = "e";

		// Token: 0x040002D6 RID: 726
		internal const string EwsSoapNamespacePrefix = "soap";

		// Token: 0x040002D7 RID: 727
		internal const string EwsXmlSchemaInstanceNamespacePrefix = "xsi";

		// Token: 0x040002D8 RID: 728
		internal const string PassportSoapFaultNamespacePrefix = "psf";

		// Token: 0x040002D9 RID: 729
		internal const string WSTrustFebruary2005NamespacePrefix = "wst";

		// Token: 0x040002DA RID: 730
		internal const string WSAddressingNamespacePrefix = "wsa";

		// Token: 0x040002DB RID: 731
		internal const string AutodiscoverSoapNamespacePrefix = "a";

		// Token: 0x040002DC RID: 732
		internal const string WSSecurityUtilityNamespacePrefix = "wsu";

		// Token: 0x040002DD RID: 733
		internal const string WSSecuritySecExtNamespacePrefix = "wsse";

		// Token: 0x040002DE RID: 734
		internal const string EwsTypesNamespace = "http://schemas.microsoft.com/exchange/services/2006/types";

		// Token: 0x040002DF RID: 735
		internal const string EwsMessagesNamespace = "http://schemas.microsoft.com/exchange/services/2006/messages";

		// Token: 0x040002E0 RID: 736
		internal const string EwsErrorsNamespace = "http://schemas.microsoft.com/exchange/services/2006/errors";

		// Token: 0x040002E1 RID: 737
		internal const string EwsSoapNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x040002E2 RID: 738
		internal const string EwsSoap12Namespace = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x040002E3 RID: 739
		internal const string EwsXmlSchemaInstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040002E4 RID: 740
		internal const string PassportSoapFaultNamespace = "http://schemas.microsoft.com/Passport/SoapServices/SOAPFault";

		// Token: 0x040002E5 RID: 741
		internal const string WSTrustFebruary2005Namespace = "http://schemas.xmlsoap.org/ws/2005/02/trust";

		// Token: 0x040002E6 RID: 742
		internal const string WSAddressingNamespace = "http://www.w3.org/2005/08/addressing";

		// Token: 0x040002E7 RID: 743
		internal const string AutodiscoverSoapNamespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover";

		// Token: 0x040002E8 RID: 744
		internal const string WSSecurityUtilityNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		// Token: 0x040002E9 RID: 745
		internal const string WSSecuritySecExtNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x040002EA RID: 746
		internal const string DomainRegex = "^[-a-zA-Z0-9_.]+$";

		// Token: 0x040002EB RID: 747
		private static LazyMember<ServiceObjectInfo> serviceObjectInfo = new LazyMember<ServiceObjectInfo>(() => new ServiceObjectInfo());

		// Token: 0x040002EC RID: 748
		private static LazyMember<string> buildVersion = new LazyMember<string>(delegate()
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			return versionInfo.FileVersion;
		});

		// Token: 0x040002ED RID: 749
		private static LazyMember<Dictionary<Type, Dictionary<Enum, ExchangeVersion>>> enumVersionDictionaries = new LazyMember<Dictionary<Type, Dictionary<Enum, ExchangeVersion>>>(() => new Dictionary<Type, Dictionary<Enum, ExchangeVersion>>
		{
			{
				typeof(WellKnownFolderName),
				EwsUtilities.BuildEnumDict(typeof(WellKnownFolderName))
			},
			{
				typeof(ItemTraversal),
				EwsUtilities.BuildEnumDict(typeof(ItemTraversal))
			},
			{
				typeof(ConversationQueryTraversal),
				EwsUtilities.BuildEnumDict(typeof(ConversationQueryTraversal))
			},
			{
				typeof(FileAsMapping),
				EwsUtilities.BuildEnumDict(typeof(FileAsMapping))
			},
			{
				typeof(EventType),
				EwsUtilities.BuildEnumDict(typeof(EventType))
			},
			{
				typeof(MeetingRequestsDeliveryScope),
				EwsUtilities.BuildEnumDict(typeof(MeetingRequestsDeliveryScope))
			},
			{
				typeof(ViewFilter),
				EwsUtilities.BuildEnumDict(typeof(ViewFilter))
			}
		});

		// Token: 0x040002EE RID: 750
		private static LazyMember<Dictionary<Type, Dictionary<string, Enum>>> schemaToEnumDictionaries = new LazyMember<Dictionary<Type, Dictionary<string, Enum>>>(() => new Dictionary<Type, Dictionary<string, Enum>>
		{
			{
				typeof(EventType),
				EwsUtilities.BuildSchemaToEnumDict(typeof(EventType))
			},
			{
				typeof(MailboxType),
				EwsUtilities.BuildSchemaToEnumDict(typeof(MailboxType))
			},
			{
				typeof(FileAsMapping),
				EwsUtilities.BuildSchemaToEnumDict(typeof(FileAsMapping))
			},
			{
				typeof(RuleProperty),
				EwsUtilities.BuildSchemaToEnumDict(typeof(RuleProperty))
			},
			{
				typeof(WellKnownFolderName),
				EwsUtilities.BuildSchemaToEnumDict(typeof(WellKnownFolderName))
			}
		});

		// Token: 0x040002EF RID: 751
		private static LazyMember<Dictionary<Type, Dictionary<Enum, string>>> enumToSchemaDictionaries = new LazyMember<Dictionary<Type, Dictionary<Enum, string>>>(() => new Dictionary<Type, Dictionary<Enum, string>>
		{
			{
				typeof(EventType),
				EwsUtilities.BuildEnumToSchemaDict(typeof(EventType))
			},
			{
				typeof(MailboxType),
				EwsUtilities.BuildEnumToSchemaDict(typeof(MailboxType))
			},
			{
				typeof(FileAsMapping),
				EwsUtilities.BuildEnumToSchemaDict(typeof(FileAsMapping))
			},
			{
				typeof(RuleProperty),
				EwsUtilities.BuildEnumToSchemaDict(typeof(RuleProperty))
			},
			{
				typeof(WellKnownFolderName),
				EwsUtilities.BuildEnumToSchemaDict(typeof(WellKnownFolderName))
			}
		});

		// Token: 0x040002F0 RID: 752
		private static LazyMember<Dictionary<string, string>> typeNameToShortNameMap = new LazyMember<Dictionary<string, string>>(() => new Dictionary<string, string>
		{
			{
				"Boolean",
				"bool"
			},
			{
				"Int16",
				"short"
			},
			{
				"Int32",
				"int"
			},
			{
				"String",
				"string"
			}
		});
	}
}

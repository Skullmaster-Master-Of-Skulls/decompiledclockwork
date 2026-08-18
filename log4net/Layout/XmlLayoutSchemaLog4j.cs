using System;
using System.Collections;
using System.Xml;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout
{
	// Token: 0x020000B5 RID: 181
	public class XmlLayoutSchemaLog4j : XmlLayoutBase
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x00010544 File Offset: 0x0000E744
		public XmlLayoutSchemaLog4j()
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001054C File Offset: 0x0000E74C
		public XmlLayoutSchemaLog4j(bool locationInfo) : base(locationInfo)
		{
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00010555 File Offset: 0x0000E755
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001055C File Offset: 0x0000E75C
		public string Version
		{
			get
			{
				return "1.2";
			}
			set
			{
				if (value != "1.2")
				{
					throw new ArgumentException("Only version 1.2 of the log4j schema is currently supported");
				}
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00010578 File Offset: 0x0000E778
		protected override void FormatXml(XmlWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent.LookupProperty("log4net:HostName") != null && loggingEvent.LookupProperty("log4jmachinename") == null)
			{
				loggingEvent.GetProperties()["log4jmachinename"] = loggingEvent.LookupProperty("log4net:HostName");
			}
			if (loggingEvent.LookupProperty("log4japp") == null && loggingEvent.Domain != null && loggingEvent.Domain.Length > 0)
			{
				loggingEvent.GetProperties()["log4japp"] = loggingEvent.Domain;
			}
			if (loggingEvent.Identity != null && loggingEvent.Identity.Length > 0 && loggingEvent.LookupProperty("log4net:Identity") == null)
			{
				loggingEvent.GetProperties()["log4net:Identity"] = loggingEvent.Identity;
			}
			if (loggingEvent.UserName != null && loggingEvent.UserName.Length > 0 && loggingEvent.LookupProperty("log4net:UserName") == null)
			{
				loggingEvent.GetProperties()["log4net:UserName"] = loggingEvent.UserName;
			}
			writer.WriteStartElement("log4j:event");
			writer.WriteAttributeString("logger", loggingEvent.LoggerName);
			writer.WriteAttributeString("timestamp", XmlConvert.ToString((long)(loggingEvent.TimeStampUtc - XmlLayoutSchemaLog4j.s_date1970).TotalMilliseconds));
			writer.WriteAttributeString("level", loggingEvent.Level.DisplayName);
			writer.WriteAttributeString("thread", loggingEvent.ThreadName);
			writer.WriteStartElement("log4j:message");
			Transform.WriteEscapedXmlString(writer, loggingEvent.RenderedMessage, base.InvalidCharReplacement);
			writer.WriteEndElement();
			object obj = loggingEvent.LookupProperty("NDC");
			if (obj != null)
			{
				string text = loggingEvent.Repository.RendererMap.FindAndRender(obj);
				if (text != null && text.Length > 0)
				{
					writer.WriteStartElement("log4j:NDC");
					Transform.WriteEscapedXmlString(writer, text, base.InvalidCharReplacement);
					writer.WriteEndElement();
				}
			}
			PropertiesDictionary properties = loggingEvent.GetProperties();
			if (properties.Count > 0)
			{
				writer.WriteStartElement("log4j:properties");
				foreach (object obj2 in ((IEnumerable)properties))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					writer.WriteStartElement("log4j:data");
					writer.WriteAttributeString("name", (string)dictionaryEntry.Key);
					string value = loggingEvent.Repository.RendererMap.FindAndRender(dictionaryEntry.Value);
					writer.WriteAttributeString("value", value);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			string exceptionString = loggingEvent.GetExceptionString();
			if (exceptionString != null && exceptionString.Length > 0)
			{
				writer.WriteStartElement("log4j:throwable");
				Transform.WriteEscapedXmlString(writer, exceptionString, base.InvalidCharReplacement);
				writer.WriteEndElement();
			}
			if (base.LocationInfo)
			{
				LocationInfo locationInformation = loggingEvent.LocationInformation;
				writer.WriteStartElement("log4j:locationInfo");
				writer.WriteAttributeString("class", locationInformation.ClassName);
				writer.WriteAttributeString("method", locationInformation.MethodName);
				writer.WriteAttributeString("file", locationInformation.FileName);
				writer.WriteAttributeString("line", locationInformation.LineNumber);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000234 RID: 564
		private static readonly DateTime s_date1970 = new DateTime(1970, 1, 1);
	}
}

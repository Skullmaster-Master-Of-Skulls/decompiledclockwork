using System;
using System.Collections;
using System.Text;
using System.Xml;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout
{
	// Token: 0x020000B4 RID: 180
	public class XmlLayout : XmlLayoutBase
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000100AC File Offset: 0x0000E2AC
		public XmlLayout()
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001010C File Offset: 0x0000E30C
		public XmlLayout(bool locationInfo) : base(locationInfo)
		{
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001016D File Offset: 0x0000E36D
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00010175 File Offset: 0x0000E375
		public string Prefix
		{
			get
			{
				return this.m_prefix;
			}
			set
			{
				this.m_prefix = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001017E File Offset: 0x0000E37E
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00010186 File Offset: 0x0000E386
		public bool Base64EncodeMessage
		{
			get
			{
				return this.m_base64Message;
			}
			set
			{
				this.m_base64Message = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001018F File Offset: 0x0000E38F
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00010197 File Offset: 0x0000E397
		public bool Base64EncodeProperties
		{
			get
			{
				return this.m_base64Properties;
			}
			set
			{
				this.m_base64Properties = value;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000101A0 File Offset: 0x0000E3A0
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.m_prefix != null && this.m_prefix.Length > 0)
			{
				this.m_elmEvent = this.m_prefix + ":event";
				this.m_elmMessage = this.m_prefix + ":message";
				this.m_elmProperties = this.m_prefix + ":properties";
				this.m_elmData = this.m_prefix + ":data";
				this.m_elmException = this.m_prefix + ":exception";
				this.m_elmLocation = this.m_prefix + ":locationInfo";
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00010254 File Offset: 0x0000E454
		protected override void FormatXml(XmlWriter writer, LoggingEvent loggingEvent)
		{
			writer.WriteStartElement(this.m_elmEvent);
			writer.WriteAttributeString("logger", loggingEvent.LoggerName);
			writer.WriteAttributeString("timestamp", XmlConvert.ToString(loggingEvent.TimeStamp, XmlDateTimeSerializationMode.Local));
			writer.WriteAttributeString("level", loggingEvent.Level.DisplayName);
			writer.WriteAttributeString("thread", loggingEvent.ThreadName);
			if (loggingEvent.Domain != null && loggingEvent.Domain.Length > 0)
			{
				writer.WriteAttributeString("domain", loggingEvent.Domain);
			}
			if (loggingEvent.Identity != null && loggingEvent.Identity.Length > 0)
			{
				writer.WriteAttributeString("identity", loggingEvent.Identity);
			}
			if (loggingEvent.UserName != null && loggingEvent.UserName.Length > 0)
			{
				writer.WriteAttributeString("username", loggingEvent.UserName);
			}
			writer.WriteStartElement(this.m_elmMessage);
			if (!this.Base64EncodeMessage)
			{
				Transform.WriteEscapedXmlString(writer, loggingEvent.RenderedMessage, base.InvalidCharReplacement);
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes(loggingEvent.RenderedMessage);
				string textData = Convert.ToBase64String(bytes, 0, bytes.Length);
				Transform.WriteEscapedXmlString(writer, textData, base.InvalidCharReplacement);
			}
			writer.WriteEndElement();
			PropertiesDictionary properties = loggingEvent.GetProperties();
			if (properties.Count > 0)
			{
				writer.WriteStartElement(this.m_elmProperties);
				foreach (object obj in ((IEnumerable)properties))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					writer.WriteStartElement(this.m_elmData);
					writer.WriteAttributeString("name", Transform.MaskXmlInvalidCharacters((string)dictionaryEntry.Key, base.InvalidCharReplacement));
					string value;
					if (!this.Base64EncodeProperties)
					{
						value = Transform.MaskXmlInvalidCharacters(loggingEvent.Repository.RendererMap.FindAndRender(dictionaryEntry.Value), base.InvalidCharReplacement);
					}
					else
					{
						byte[] bytes2 = Encoding.UTF8.GetBytes(loggingEvent.Repository.RendererMap.FindAndRender(dictionaryEntry.Value));
						value = Convert.ToBase64String(bytes2, 0, bytes2.Length);
					}
					writer.WriteAttributeString("value", value);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			string exceptionString = loggingEvent.GetExceptionString();
			if (exceptionString != null && exceptionString.Length > 0)
			{
				writer.WriteStartElement(this.m_elmException);
				Transform.WriteEscapedXmlString(writer, exceptionString, base.InvalidCharReplacement);
				writer.WriteEndElement();
			}
			if (base.LocationInfo)
			{
				LocationInfo locationInformation = loggingEvent.LocationInformation;
				writer.WriteStartElement(this.m_elmLocation);
				writer.WriteAttributeString("class", locationInformation.ClassName);
				writer.WriteAttributeString("method", locationInformation.MethodName);
				writer.WriteAttributeString("file", locationInformation.FileName);
				writer.WriteAttributeString("line", locationInformation.LineNumber);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000216 RID: 534
		private const string PREFIX = "log4net";

		// Token: 0x04000217 RID: 535
		private const string ELM_EVENT = "event";

		// Token: 0x04000218 RID: 536
		private const string ELM_MESSAGE = "message";

		// Token: 0x04000219 RID: 537
		private const string ELM_PROPERTIES = "properties";

		// Token: 0x0400021A RID: 538
		private const string ELM_GLOBAL_PROPERTIES = "global-properties";

		// Token: 0x0400021B RID: 539
		private const string ELM_DATA = "data";

		// Token: 0x0400021C RID: 540
		private const string ELM_EXCEPTION = "exception";

		// Token: 0x0400021D RID: 541
		private const string ELM_LOCATION = "locationInfo";

		// Token: 0x0400021E RID: 542
		private const string ATTR_LOGGER = "logger";

		// Token: 0x0400021F RID: 543
		private const string ATTR_TIMESTAMP = "timestamp";

		// Token: 0x04000220 RID: 544
		private const string ATTR_LEVEL = "level";

		// Token: 0x04000221 RID: 545
		private const string ATTR_THREAD = "thread";

		// Token: 0x04000222 RID: 546
		private const string ATTR_DOMAIN = "domain";

		// Token: 0x04000223 RID: 547
		private const string ATTR_IDENTITY = "identity";

		// Token: 0x04000224 RID: 548
		private const string ATTR_USERNAME = "username";

		// Token: 0x04000225 RID: 549
		private const string ATTR_CLASS = "class";

		// Token: 0x04000226 RID: 550
		private const string ATTR_METHOD = "method";

		// Token: 0x04000227 RID: 551
		private const string ATTR_FILE = "file";

		// Token: 0x04000228 RID: 552
		private const string ATTR_LINE = "line";

		// Token: 0x04000229 RID: 553
		private const string ATTR_NAME = "name";

		// Token: 0x0400022A RID: 554
		private const string ATTR_VALUE = "value";

		// Token: 0x0400022B RID: 555
		private string m_prefix = "log4net";

		// Token: 0x0400022C RID: 556
		private string m_elmEvent = "event";

		// Token: 0x0400022D RID: 557
		private string m_elmMessage = "message";

		// Token: 0x0400022E RID: 558
		private string m_elmData = "data";

		// Token: 0x0400022F RID: 559
		private string m_elmProperties = "properties";

		// Token: 0x04000230 RID: 560
		private string m_elmException = "exception";

		// Token: 0x04000231 RID: 561
		private string m_elmLocation = "locationInfo";

		// Token: 0x04000232 RID: 562
		private bool m_base64Message;

		// Token: 0x04000233 RID: 563
		private bool m_base64Properties;
	}
}

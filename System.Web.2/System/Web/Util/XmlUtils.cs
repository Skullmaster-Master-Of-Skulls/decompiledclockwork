using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Web.Util
{
	// Token: 0x0200020B RID: 523
	internal static class XmlUtils
	{
		// Token: 0x06001995 RID: 6549 RVA: 0x0004FEF4 File Offset: 0x0004E0F4
		public static XmlDocument CreateXmlDocumentFromContent(string content)
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (AppSettings.RestrictXmlControls)
			{
				using (StringReader stringReader = new StringReader(content))
				{
					xmlDocument.Load(XmlUtils.CreateXmlReader(stringReader));
					return xmlDocument;
				}
			}
			xmlDocument.LoadXml(content);
			return xmlDocument;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0004FF48 File Offset: 0x0004E148
		public static XPathDocument CreateXPathDocumentFromContent(string content)
		{
			StringReader stringReader = new StringReader(content);
			if (AppSettings.RestrictXmlControls)
			{
				return new XPathDocument(XmlUtils.CreateXmlReader(stringReader));
			}
			return new XPathDocument(stringReader);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0004FF78 File Offset: 0x0004E178
		public static XmlReaderSettings CreateXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			if (AppSettings.RestrictXmlControls)
			{
				xmlReaderSettings.MaxCharactersFromEntities = XmlUtils.MaxEntityExpansion;
				xmlReaderSettings.XmlResolver = null;
				xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
			}
			return xmlReaderSettings;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0004FFAC File Offset: 0x0004E1AC
		public static XmlReader CreateXmlReader(string filepath)
		{
			if (AppSettings.RestrictXmlControls)
			{
				XmlUtils.NoEntitiesXmlTextReader reader = new XmlUtils.NoEntitiesXmlTextReader(filepath);
				return XmlReader.Create(reader, XmlUtils.CreateXmlReaderSettings());
			}
			return new XmlTextReader(filepath);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0004FFDC File Offset: 0x0004E1DC
		public static XmlReader CreateXmlReader(Stream datastream)
		{
			if (AppSettings.RestrictXmlControls)
			{
				XmlUtils.NoEntitiesXmlTextReader reader = new XmlUtils.NoEntitiesXmlTextReader(datastream);
				return XmlReader.Create(reader, XmlUtils.CreateXmlReaderSettings());
			}
			return new XmlTextReader(datastream);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0005000C File Offset: 0x0004E20C
		public static XmlReader CreateXmlReader(TextReader reader)
		{
			if (AppSettings.RestrictXmlControls)
			{
				XmlUtils.NoEntitiesXmlTextReader reader2 = new XmlUtils.NoEntitiesXmlTextReader(reader);
				return XmlReader.Create(reader2, XmlUtils.CreateXmlReaderSettings());
			}
			return new XmlTextReader(reader);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0005003C File Offset: 0x0004E23C
		public static XmlReader CreateXmlReader(Stream contentStream, string baseURI)
		{
			if (AppSettings.RestrictXmlControls)
			{
				XmlUtils.NoEntitiesXmlTextReader reader = new XmlUtils.NoEntitiesXmlTextReader(baseURI, contentStream);
				return XmlReader.Create(reader, XmlUtils.CreateXmlReaderSettings());
			}
			return new XmlTextReader(baseURI, contentStream);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005006D File Offset: 0x0004E26D
		public static XmlReader CreateXmlReader(TextReader reader, string baseURI, XmlReaderSettings settings)
		{
			if (settings == null)
			{
				settings = XmlUtils.CreateXmlReaderSettings();
			}
			return XmlReader.Create(reader, settings, baseURI);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00050084 File Offset: 0x0004E284
		public static XslCompiledTransform CreateXslCompiledTransform(XmlReader xmlReader)
		{
			XmlReader stylesheet = xmlReader;
			if (AppSettings.RestrictXmlControls)
			{
				XmlTextReader xmlTextReader = xmlReader as XmlTextReader;
				if (xmlTextReader != null)
				{
					xmlTextReader.DtdProcessing = DtdProcessing.Ignore;
				}
				else
				{
					XmlReaderSettings xmlReaderSettings = xmlReader.Settings;
					if (xmlReaderSettings == null)
					{
						xmlReaderSettings = XmlUtils.CreateXmlReaderSettings();
					}
					xmlReaderSettings.DtdProcessing = DtdProcessing.Ignore;
					stylesheet = XmlReader.Create(xmlReader, xmlReaderSettings);
				}
			}
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(stylesheet, null, null);
			return xslCompiledTransform;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000500DC File Offset: 0x0004E2DC
		public static XslTransform CreateXslTransform(XmlReader reader)
		{
			if (!AppSettings.RestrictXmlControls)
			{
				XslTransform xslTransform = new XslTransform();
				xslTransform.Load(reader);
				return xslTransform;
			}
			return null;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00050100 File Offset: 0x0004E300
		public static XslTransform CreateXslTransform(XmlReader reader, XmlResolver resolver)
		{
			if (!AppSettings.RestrictXmlControls)
			{
				XslTransform xslTransform = new XslTransform();
				xslTransform.Load(reader, resolver, null);
				return xslTransform;
			}
			return null;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00050126 File Offset: 0x0004E326
		public static XslTransform GetXslTransform(XslTransform xform)
		{
			if (!AppSettings.RestrictXmlControls)
			{
				return xform;
			}
			return null;
		}

		// Token: 0x040017E0 RID: 6112
		public static readonly long MaxEntityExpansion = 1048576L;

		// Token: 0x02000949 RID: 2377
		private sealed class NoEntitiesXmlTextReader : XmlTextReader
		{
			// Token: 0x0600698C RID: 27020 RVA: 0x00177871 File Offset: 0x00175A71
			public NoEntitiesXmlTextReader()
			{
				this.Restrict();
			}

			// Token: 0x0600698D RID: 27021 RVA: 0x0017787F File Offset: 0x00175A7F
			public NoEntitiesXmlTextReader(string filepath) : base(filepath)
			{
				this.Restrict();
			}

			// Token: 0x0600698E RID: 27022 RVA: 0x0017788E File Offset: 0x00175A8E
			public NoEntitiesXmlTextReader(TextReader reader) : base(reader)
			{
				this.Restrict();
			}

			// Token: 0x0600698F RID: 27023 RVA: 0x0017789D File Offset: 0x00175A9D
			public NoEntitiesXmlTextReader(Stream datastream) : base(datastream)
			{
				this.Restrict();
			}

			// Token: 0x06006990 RID: 27024 RVA: 0x001778AC File Offset: 0x00175AAC
			public NoEntitiesXmlTextReader(string baseURI, Stream contentStream) : base(baseURI, contentStream)
			{
				this.Restrict();
			}

			// Token: 0x06006991 RID: 27025 RVA: 0x00006164 File Offset: 0x00004364
			public override void ResolveEntity()
			{
			}

			// Token: 0x06006992 RID: 27026 RVA: 0x001778BC File Offset: 0x00175ABC
			private void Restrict()
			{
				base.EntityHandling = EntityHandling.ExpandCharEntities;
				base.XmlResolver = null;
			}
		}
	}
}

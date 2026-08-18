using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A4 RID: 1700
	public sealed class XPathMessageFilterElement : ConfigurationElement
	{
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x000F9764 File Offset: 0x000F7964
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("filter", typeof(XPathMessageFilter), null, null, null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x000F97AA File Offset: 0x000F79AA
		// (set) Token: 0x060041DE RID: 16862 RVA: 0x000F97BC File Offset: 0x000F79BC
		[ConfigurationProperty("filter", DefaultValue = null, Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public XPathMessageFilter Filter
		{
			get
			{
				return (XPathMessageFilter)base["filter"];
			}
			set
			{
				base["filter"] = value;
			}
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000F97CC File Offset: 0x000F79CC
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				ConformanceLevel = ConformanceLevel.Fragment,
				OmitXmlDeclaration = false
			}))
			{
				xmlWriter.WriteStartElement(reader.Name);
				if (0 < reader.AttributeCount)
				{
					for (int i = 0; i < reader.AttributeCount; i++)
					{
						reader.MoveToAttribute(i);
						if (reader.Name.Equals("nodeQuota", StringComparison.Ordinal))
						{
							text = reader.Value;
						}
						else if (reader.Name.Contains(":"))
						{
							string[] array = reader.Name.Split(new char[]
							{
								':'
							}, StringSplitOptions.RemoveEmptyEntries);
							xmlWriter.WriteAttributeString(array[0], array[1], null, reader.Value);
						}
						else
						{
							xmlWriter.WriteAttributeString(reader.Name, reader.Value);
						}
					}
					reader.MoveToElement();
				}
				string text2 = reader.ReadString();
				text2 = text2.Trim();
				if (string.IsNullOrEmpty(text2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ConfigXPathFilterMustNotBeEmpty")));
				}
				xmlWriter.WriteString(text2);
				xmlWriter.WriteEndElement();
			}
			XPathMessageFilter xpathMessageFilter = null;
			using (StringReader stringReader = new StringReader(stringBuilder.ToString()))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader))
				{
					xpathMessageFilter = new XPathMessageFilter(xmlReader);
				}
			}
			if (xpathMessageFilter != null)
			{
				if (!string.IsNullOrEmpty(text))
				{
					xpathMessageFilter.NodeQuota = int.Parse(text, CultureInfo.CurrentCulture);
				}
				else
				{
					xpathMessageFilter.NodeQuota = 1000;
				}
			}
			this.Filter = xpathMessageFilter;
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000F999C File Offset: 0x000F7B9C
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			bool flag = this.Filter != null;
			if (flag && writer != null)
			{
				writer.WriteStartElement(elementName);
				writer.WriteAttributeString("nodeQuota", this.Filter.NodeQuota.ToString(NumberFormatInfo.CurrentInfo));
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
				{
					ConformanceLevel = ConformanceLevel.Fragment,
					OmitXmlDeclaration = false
				}))
				{
					this.Filter.WriteXPathTo(xmlWriter, null, elementName, null, true);
				}
				using (StringReader stringReader = new StringReader(stringBuilder.ToString()))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader))
					{
						if (xmlReader.Read())
						{
							if (0 < xmlReader.AttributeCount)
							{
								for (int i = 0; i < xmlReader.AttributeCount; i++)
								{
									xmlReader.MoveToAttribute(i);
									writer.WriteAttributeString(xmlReader.Name, xmlReader.Value);
								}
								xmlReader.MoveToElement();
							}
							writer.WriteString(xmlReader.ReadString());
						}
					}
				}
				writer.WriteEndElement();
			}
			return flag;
		}

		// Token: 0x04002CF2 RID: 11506
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CF3 RID: 11507
		private const int DefaultNodeQuota = 1000;
	}
}

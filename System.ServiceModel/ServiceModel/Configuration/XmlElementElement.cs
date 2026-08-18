using System;
using System.Configuration;
using System.Security;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A2 RID: 1698
	public sealed class XmlElementElement : ConfigurationElement
	{
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x000F9458 File Offset: 0x000F7658
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("xmlElement", typeof(XmlElement), null, null, null, ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x000F949E File Offset: 0x000F769E
		public XmlElementElement()
		{
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x000F94A6 File Offset: 0x000F76A6
		public XmlElementElement(XmlElement element) : this()
		{
			this.XmlElement = element;
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x000F94B8 File Offset: 0x000F76B8
		public void Copy(XmlElementElement source)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			if (source.XmlElement != null)
			{
				this.XmlElement = (XmlElement)source.XmlElement.Clone();
			}
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000F9518 File Offset: 0x000F7718
		[SecuritySafeCritical]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.SetIsPresent();
			this.DeserializeElementCore(reader);
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x000F9528 File Offset: 0x000F7728
		private void DeserializeElementCore(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			this.XmlElement = (XmlElement)xmlDocument.ReadNode(reader);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x000F954D File Offset: 0x000F774D
		internal void ResetInternal(XmlElementElement element)
		{
			this.Reset(element);
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x000F9556 File Offset: 0x000F7756
		[SecurityCritical]
		private void SetIsPresent()
		{
			ConfigurationHelpers.SetIsPresent(this);
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x000F9560 File Offset: 0x000F7760
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			bool flag = this.XmlElement != null;
			if (flag && writer != null)
			{
				if (!string.Equals(elementName, "xmlElement", StringComparison.Ordinal))
				{
					writer.WriteStartElement(elementName);
				}
				using (XmlNodeReader xmlNodeReader = new XmlNodeReader(this.XmlElement))
				{
					writer.WriteNode(xmlNodeReader, false);
				}
				if (!string.Equals(elementName, "xmlElement", StringComparison.Ordinal))
				{
					writer.WriteEndElement();
				}
			}
			return flag;
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x000F95D8 File Offset: 0x000F77D8
		protected override void PostDeserialize()
		{
			this.Validate();
			base.PostDeserialize();
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000F95E6 File Offset: 0x000F77E6
		private void Validate()
		{
			if (this.XmlElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigXmlElementMustBeSet"), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x000F9620 File Offset: 0x000F7820
		// (set) Token: 0x060041D2 RID: 16850 RVA: 0x000F9632 File Offset: 0x000F7832
		[ConfigurationProperty("xmlElement", DefaultValue = null, Options = ConfigurationPropertyOptions.IsKey)]
		public XmlElement XmlElement
		{
			get
			{
				return (XmlElement)base["xmlElement"];
			}
			set
			{
				base["xmlElement"] = value;
			}
		}

		// Token: 0x04002CF0 RID: 11504
		private ConfigurationPropertyCollection properties;
	}
}

using System;
using System.Configuration;
using System.Security;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E2 RID: 1506
	public sealed class AddressHeaderCollectionElement : ServiceModelConfigurationElement
	{
		// Token: 0x06003A4F RID: 14927 RVA: 0x000E0874 File Offset: 0x000DEA74
		internal void Copy(AddressHeaderCollectionElement source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			PropertyInformationCollection propertyInformationCollection = source.ElementInformation.Properties;
			if (propertyInformationCollection["headers"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.Headers = source.Headers;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003A50 RID: 14928 RVA: 0x000E08C0 File Offset: 0x000DEAC0
		// (set) Token: 0x06003A51 RID: 14929 RVA: 0x000E08E8 File Offset: 0x000DEAE8
		[ConfigurationProperty("headers", DefaultValue = null)]
		public AddressHeaderCollection Headers
		{
			get
			{
				AddressHeaderCollection addressHeaderCollection = (AddressHeaderCollection)base["headers"];
				if (addressHeaderCollection == null)
				{
					addressHeaderCollection = AddressHeaderCollection.EmptyHeaderCollection;
				}
				return addressHeaderCollection;
			}
			set
			{
				if (value == null)
				{
					value = AddressHeaderCollection.EmptyHeaderCollection;
				}
				base["headers"] = value;
			}
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000E0900 File Offset: 0x000DEB00
		[SecuritySafeCritical]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.SetIsPresent();
			this.DeserializeElementCore(reader);
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000E090F File Offset: 0x000DEB0F
		private void DeserializeElementCore(XmlReader reader)
		{
			this.Headers = AddressHeaderCollection.ReadServiceParameters(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000E0922 File Offset: 0x000DEB22
		[SecurityCritical]
		private void SetIsPresent()
		{
			ConfigurationHelpers.SetIsPresent(this);
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000E092C File Offset: 0x000DEB2C
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			bool flag = this.Headers.Count != 0;
			if (flag && writer != null)
			{
				writer.WriteStartElement(elementName);
				this.Headers.WriteContentsTo(XmlDictionaryWriter.CreateDictionaryWriter(writer));
				writer.WriteEndElement();
			}
			return flag;
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000E096D File Offset: 0x000DEB6D
		internal void InitializeFrom(AddressHeaderCollection headers)
		{
			base.SetPropertyValueIfNotDefaultValue<AddressHeaderCollection>("headers", headers);
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000E097C File Offset: 0x000DEB7C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("headers", typeof(AddressHeaderCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A58 RID: 10840
		private ConfigurationPropertyCollection properties;
	}
}

using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000695 RID: 1685
	public sealed class TextMessageEncodingElement : BindingElementExtensionElement
	{
		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x000F7A34 File Offset: 0x000F5C34
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("maxReadPoolSize", typeof(int), 64, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxWritePoolSize", typeof(int), 16, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("messageVersion", typeof(MessageVersion), "Soap12WSAddressing10", new MessageVersionConverter(), null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("writeEncoding", typeof(Encoding), "utf-8", new EncodingConverter(), null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000F7B30 File Offset: 0x000F5D30
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = (TextMessageEncodingBindingElement)bindingElement;
			textMessageEncodingBindingElement.MessageVersion = this.MessageVersion;
			textMessageEncodingBindingElement.WriteEncoding = this.WriteEncoding;
			textMessageEncodingBindingElement.MaxReadPoolSize = this.MaxReadPoolSize;
			textMessageEncodingBindingElement.MaxWritePoolSize = this.MaxWritePoolSize;
			this.ReaderQuotas.ApplyConfiguration(textMessageEncodingBindingElement.ReaderQuotas);
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x000F7B8C File Offset: 0x000F5D8C
		public override Type BindingElementType
		{
			get
			{
				return typeof(TextMessageEncodingBindingElement);
			}
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000F7B98 File Offset: 0x000F5D98
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			TextMessageEncodingElement textMessageEncodingElement = (TextMessageEncodingElement)from;
			this.MessageVersion = textMessageEncodingElement.MessageVersion;
			this.WriteEncoding = textMessageEncodingElement.WriteEncoding;
			this.MaxReadPoolSize = textMessageEncodingElement.MaxReadPoolSize;
			this.MaxWritePoolSize = textMessageEncodingElement.MaxWritePoolSize;
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000F7BE4 File Offset: 0x000F5DE4
		protected internal override BindingElement CreateBindingElement()
		{
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
			this.ApplyConfiguration(textMessageEncodingBindingElement);
			return textMessageEncodingBindingElement;
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000F7C00 File Offset: 0x000F5E00
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = (TextMessageEncodingBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<MessageVersion>("messageVersion", textMessageEncodingBindingElement.MessageVersion);
			base.SetPropertyValueIfNotDefaultValue<Encoding>("writeEncoding", textMessageEncodingBindingElement.WriteEncoding);
			base.SetPropertyValueIfNotDefaultValue<int>("maxReadPoolSize", textMessageEncodingBindingElement.MaxReadPoolSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxWritePoolSize", textMessageEncodingBindingElement.MaxWritePoolSize);
			this.ReaderQuotas.InitializeFrom(textMessageEncodingBindingElement.ReaderQuotas);
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x000F7C70 File Offset: 0x000F5E70
		// (set) Token: 0x06004140 RID: 16704 RVA: 0x000F7C82 File Offset: 0x000F5E82
		[ConfigurationProperty("maxReadPoolSize", DefaultValue = 64)]
		[IntegerValidator(MinValue = 1)]
		public int MaxReadPoolSize
		{
			get
			{
				return (int)base["maxReadPoolSize"];
			}
			set
			{
				base["maxReadPoolSize"] = value;
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x000F7C95 File Offset: 0x000F5E95
		// (set) Token: 0x06004142 RID: 16706 RVA: 0x000F7CA7 File Offset: 0x000F5EA7
		[ConfigurationProperty("maxWritePoolSize", DefaultValue = 16)]
		[IntegerValidator(MinValue = 1)]
		public int MaxWritePoolSize
		{
			get
			{
				return (int)base["maxWritePoolSize"];
			}
			set
			{
				base["maxWritePoolSize"] = value;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06004143 RID: 16707 RVA: 0x000F7CBA File Offset: 0x000F5EBA
		// (set) Token: 0x06004144 RID: 16708 RVA: 0x000F7CCC File Offset: 0x000F5ECC
		[ConfigurationProperty("messageVersion", DefaultValue = "Soap12WSAddressing10")]
		[TypeConverter(typeof(MessageVersionConverter))]
		public MessageVersion MessageVersion
		{
			get
			{
				return (MessageVersion)base["messageVersion"];
			}
			set
			{
				base["messageVersion"] = value;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06004145 RID: 16709 RVA: 0x000F7CDA File Offset: 0x000F5EDA
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x000F7CEC File Offset: 0x000F5EEC
		// (set) Token: 0x06004147 RID: 16711 RVA: 0x000F7CFE File Offset: 0x000F5EFE
		[ConfigurationProperty("writeEncoding", DefaultValue = "utf-8")]
		[TypeConverter(typeof(EncodingConverter))]
		public Encoding WriteEncoding
		{
			get
			{
				return (Encoding)base["writeEncoding"];
			}
			set
			{
				base["writeEncoding"] = value;
			}
		}

		// Token: 0x04002CE3 RID: 11491
		private ConfigurationPropertyCollection properties;
	}
}

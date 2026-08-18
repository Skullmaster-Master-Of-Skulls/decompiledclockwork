using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000649 RID: 1609
	public sealed class MtomMessageEncodingElement : BindingElementExtensionElement
	{
		// Token: 0x06003E0D RID: 15885 RVA: 0x000ECC48 File Offset: 0x000EAE48
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			MtomMessageEncodingBindingElement mtomMessageEncodingBindingElement = (MtomMessageEncodingBindingElement)bindingElement;
			mtomMessageEncodingBindingElement.MessageVersion = this.MessageVersion;
			mtomMessageEncodingBindingElement.WriteEncoding = this.WriteEncoding;
			mtomMessageEncodingBindingElement.MaxReadPoolSize = this.MaxReadPoolSize;
			mtomMessageEncodingBindingElement.MaxWritePoolSize = this.MaxWritePoolSize;
			this.ReaderQuotas.ApplyConfiguration(mtomMessageEncodingBindingElement.ReaderQuotas);
			mtomMessageEncodingBindingElement.MaxBufferSize = this.MaxBufferSize;
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003E0E RID: 15886 RVA: 0x000ECCB0 File Offset: 0x000EAEB0
		public override Type BindingElementType
		{
			get
			{
				return typeof(MtomMessageEncodingBindingElement);
			}
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000ECCBC File Offset: 0x000EAEBC
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			MtomMessageEncodingElement mtomMessageEncodingElement = (MtomMessageEncodingElement)from;
			this.MessageVersion = mtomMessageEncodingElement.MessageVersion;
			this.WriteEncoding = mtomMessageEncodingElement.WriteEncoding;
			this.MaxReadPoolSize = mtomMessageEncodingElement.MaxReadPoolSize;
			this.MaxWritePoolSize = mtomMessageEncodingElement.MaxWritePoolSize;
			this.MaxBufferSize = mtomMessageEncodingElement.MaxBufferSize;
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000ECD14 File Offset: 0x000EAF14
		protected internal override BindingElement CreateBindingElement()
		{
			MtomMessageEncodingBindingElement mtomMessageEncodingBindingElement = new MtomMessageEncodingBindingElement();
			this.ApplyConfiguration(mtomMessageEncodingBindingElement);
			return mtomMessageEncodingBindingElement;
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000ECD30 File Offset: 0x000EAF30
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			MtomMessageEncodingBindingElement mtomMessageEncodingBindingElement = (MtomMessageEncodingBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<MessageVersion>("messageVersion", mtomMessageEncodingBindingElement.MessageVersion);
			base.SetPropertyValueIfNotDefaultValue<Encoding>("writeEncoding", mtomMessageEncodingBindingElement.WriteEncoding);
			base.SetPropertyValueIfNotDefaultValue<int>("maxReadPoolSize", mtomMessageEncodingBindingElement.MaxReadPoolSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxWritePoolSize", mtomMessageEncodingBindingElement.MaxWritePoolSize);
			this.ReaderQuotas.InitializeFrom(mtomMessageEncodingBindingElement.ReaderQuotas);
			base.SetPropertyValueIfNotDefaultValue<int>("maxBufferSize", mtomMessageEncodingBindingElement.MaxBufferSize);
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000ECDB1 File Offset: 0x000EAFB1
		// (set) Token: 0x06003E13 RID: 15891 RVA: 0x000ECDC3 File Offset: 0x000EAFC3
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

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000ECDD6 File Offset: 0x000EAFD6
		// (set) Token: 0x06003E15 RID: 15893 RVA: 0x000ECDE8 File Offset: 0x000EAFE8
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

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06003E16 RID: 15894 RVA: 0x000ECDFB File Offset: 0x000EAFFB
		// (set) Token: 0x06003E17 RID: 15895 RVA: 0x000ECE0D File Offset: 0x000EB00D
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

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000ECE1B File Offset: 0x000EB01B
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003E19 RID: 15897 RVA: 0x000ECE2D File Offset: 0x000EB02D
		// (set) Token: 0x06003E1A RID: 15898 RVA: 0x000ECE3F File Offset: 0x000EB03F
		[ConfigurationProperty("maxBufferSize", DefaultValue = 65536)]
		[IntegerValidator(MinValue = 1)]
		public int MaxBufferSize
		{
			get
			{
				return (int)base["maxBufferSize"];
			}
			set
			{
				base["maxBufferSize"] = value;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x000ECE52 File Offset: 0x000EB052
		// (set) Token: 0x06003E1C RID: 15900 RVA: 0x000ECE64 File Offset: 0x000EB064
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

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x000ECE74 File Offset: 0x000EB074
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
						new ConfigurationProperty("maxBufferSize", typeof(int), 65536, null, new IntegerValidator(1, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("writeEncoding", typeof(Encoding), "utf-8", new EncodingConverter(), null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C9B RID: 11419
		private ConfigurationPropertyCollection properties;
	}
}

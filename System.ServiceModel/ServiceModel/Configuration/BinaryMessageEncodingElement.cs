using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F5 RID: 1525
	public sealed class BinaryMessageEncodingElement : BindingElementExtensionElement
	{
		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x000E17A4 File Offset: 0x000DF9A4
		public override Type BindingElementType
		{
			get
			{
				return typeof(BinaryMessageEncodingBindingElement);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000E17B0 File Offset: 0x000DF9B0
		// (set) Token: 0x06003AB6 RID: 15030 RVA: 0x000E17C2 File Offset: 0x000DF9C2
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

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000E17D5 File Offset: 0x000DF9D5
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x000E17E7 File Offset: 0x000DF9E7
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

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000E17FA File Offset: 0x000DF9FA
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x000E180C File Offset: 0x000DFA0C
		[ConfigurationProperty("maxSessionSize", DefaultValue = 2048)]
		[IntegerValidator(MinValue = 0)]
		public int MaxSessionSize
		{
			get
			{
				return (int)base["maxSessionSize"];
			}
			set
			{
				base["maxSessionSize"] = value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000E181F File Offset: 0x000DFA1F
		[ConfigurationProperty("readerQuotas")]
		public XmlDictionaryReaderQuotasElement ReaderQuotas
		{
			get
			{
				return (XmlDictionaryReaderQuotasElement)base["readerQuotas"];
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000E1831 File Offset: 0x000DFA31
		// (set) Token: 0x06003ABD RID: 15037 RVA: 0x000E1843 File Offset: 0x000DFA43
		[ConfigurationProperty("compressionFormat", DefaultValue = CompressionFormat.None)]
		[ServiceModelEnumValidator(typeof(CompressionFormatHelper))]
		public CompressionFormat CompressionFormat
		{
			get
			{
				return (CompressionFormat)base["compressionFormat"];
			}
			set
			{
				base["compressionFormat"] = value;
			}
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000E1858 File Offset: 0x000DFA58
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = (BinaryMessageEncodingBindingElement)bindingElement;
			binaryMessageEncodingBindingElement.MaxSessionSize = this.MaxSessionSize;
			binaryMessageEncodingBindingElement.MaxReadPoolSize = this.MaxReadPoolSize;
			binaryMessageEncodingBindingElement.MaxWritePoolSize = this.MaxWritePoolSize;
			this.ReaderQuotas.ApplyConfiguration(binaryMessageEncodingBindingElement.ReaderQuotas);
			binaryMessageEncodingBindingElement.CompressionFormat = this.CompressionFormat;
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000E18B4 File Offset: 0x000DFAB4
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			BinaryMessageEncodingElement binaryMessageEncodingElement = (BinaryMessageEncodingElement)from;
			this.MaxSessionSize = binaryMessageEncodingElement.MaxSessionSize;
			this.MaxReadPoolSize = binaryMessageEncodingElement.MaxReadPoolSize;
			this.MaxWritePoolSize = binaryMessageEncodingElement.MaxWritePoolSize;
			this.CompressionFormat = binaryMessageEncodingElement.CompressionFormat;
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000E1900 File Offset: 0x000DFB00
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = (BinaryMessageEncodingBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<int>("maxSessionSize", binaryMessageEncodingBindingElement.MaxSessionSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxReadPoolSize", binaryMessageEncodingBindingElement.MaxReadPoolSize);
			base.SetPropertyValueIfNotDefaultValue<int>("maxWritePoolSize", binaryMessageEncodingBindingElement.MaxWritePoolSize);
			this.ReaderQuotas.InitializeFrom(binaryMessageEncodingBindingElement.ReaderQuotas);
			base.SetPropertyValueIfNotDefaultValue<CompressionFormat>("compressionFormat", binaryMessageEncodingBindingElement.CompressionFormat);
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000E1970 File Offset: 0x000DFB70
		protected internal override BindingElement CreateBindingElement()
		{
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
			this.ApplyConfiguration(binaryMessageEncodingBindingElement);
			return binaryMessageEncodingBindingElement;
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000E198C File Offset: 0x000DFB8C
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
						new ConfigurationProperty("maxSessionSize", typeof(int), 2048, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("readerQuotas", typeof(XmlDictionaryReaderQuotasElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("compressionFormat", typeof(CompressionFormat), CompressionFormat.None, null, new ServiceModelEnumValidator(typeof(CompressionFormatHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A76 RID: 10870
		private ConfigurationPropertyCollection properties;
	}
}

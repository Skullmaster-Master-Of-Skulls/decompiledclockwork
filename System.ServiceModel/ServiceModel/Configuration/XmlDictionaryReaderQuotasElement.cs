using System;
using System.Configuration;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006A1 RID: 1697
	public sealed class XmlDictionaryReaderQuotasElement : ServiceModelConfigurationElement
	{
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x000F914C File Offset: 0x000F734C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("maxDepth", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxStringContentLength", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxArrayLength", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxBytesPerRead", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxNameTableCharCount", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x000F925D File Offset: 0x000F745D
		// (set) Token: 0x060041BA RID: 16826 RVA: 0x000F926F File Offset: 0x000F746F
		[ConfigurationProperty("maxDepth", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxDepth
		{
			get
			{
				return (int)base["maxDepth"];
			}
			set
			{
				base["maxDepth"] = value;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x000F9282 File Offset: 0x000F7482
		// (set) Token: 0x060041BC RID: 16828 RVA: 0x000F9294 File Offset: 0x000F7494
		[ConfigurationProperty("maxStringContentLength", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxStringContentLength
		{
			get
			{
				return (int)base["maxStringContentLength"];
			}
			set
			{
				base["maxStringContentLength"] = value;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x000F92A7 File Offset: 0x000F74A7
		// (set) Token: 0x060041BE RID: 16830 RVA: 0x000F92B9 File Offset: 0x000F74B9
		[ConfigurationProperty("maxArrayLength", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxArrayLength
		{
			get
			{
				return (int)base["maxArrayLength"];
			}
			set
			{
				base["maxArrayLength"] = value;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060041BF RID: 16831 RVA: 0x000F92CC File Offset: 0x000F74CC
		// (set) Token: 0x060041C0 RID: 16832 RVA: 0x000F92DE File Offset: 0x000F74DE
		[ConfigurationProperty("maxBytesPerRead", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxBytesPerRead
		{
			get
			{
				return (int)base["maxBytesPerRead"];
			}
			set
			{
				base["maxBytesPerRead"] = value;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x000F92F1 File Offset: 0x000F74F1
		// (set) Token: 0x060041C2 RID: 16834 RVA: 0x000F9303 File Offset: 0x000F7503
		[ConfigurationProperty("maxNameTableCharCount", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int MaxNameTableCharCount
		{
			get
			{
				return (int)base["maxNameTableCharCount"];
			}
			set
			{
				base["maxNameTableCharCount"] = value;
			}
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x000F9318 File Offset: 0x000F7518
		internal void ApplyConfiguration(XmlDictionaryReaderQuotas readerQuotas)
		{
			if (readerQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("readerQuotas");
			}
			if (this.MaxDepth != 0)
			{
				readerQuotas.MaxDepth = this.MaxDepth;
			}
			if (this.MaxStringContentLength != 0)
			{
				readerQuotas.MaxStringContentLength = this.MaxStringContentLength;
			}
			if (this.MaxArrayLength != 0)
			{
				readerQuotas.MaxArrayLength = this.MaxArrayLength;
			}
			if (this.MaxBytesPerRead != 0)
			{
				readerQuotas.MaxBytesPerRead = this.MaxBytesPerRead;
			}
			if (this.MaxNameTableCharCount != 0)
			{
				readerQuotas.MaxNameTableCharCount = this.MaxNameTableCharCount;
			}
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000F939C File Offset: 0x000F759C
		internal void InitializeFrom(XmlDictionaryReaderQuotas readerQuotas)
		{
			if (readerQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("readerQuotas");
			}
			if (readerQuotas.MaxDepth != 32)
			{
				base.SetPropertyValueIfNotDefaultValue<int>("maxDepth", readerQuotas.MaxDepth);
			}
			if (readerQuotas.MaxStringContentLength != 8192)
			{
				base.SetPropertyValueIfNotDefaultValue<int>("maxStringContentLength", readerQuotas.MaxStringContentLength);
			}
			if (readerQuotas.MaxArrayLength != 16384)
			{
				base.SetPropertyValueIfNotDefaultValue<int>("maxArrayLength", readerQuotas.MaxArrayLength);
			}
			if (readerQuotas.MaxBytesPerRead != 4096)
			{
				base.SetPropertyValueIfNotDefaultValue<int>("maxBytesPerRead", readerQuotas.MaxBytesPerRead);
			}
			if (readerQuotas.MaxNameTableCharCount != 16384)
			{
				base.SetPropertyValueIfNotDefaultValue<int>("maxNameTableCharCount", readerQuotas.MaxNameTableCharCount);
			}
		}

		// Token: 0x04002CEF RID: 11503
		private ConfigurationPropertyCollection properties;
	}
}

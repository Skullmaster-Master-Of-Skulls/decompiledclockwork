using System;

namespace System.Configuration
{
	// Token: 0x02000069 RID: 105
	public class KeyValueConfigurationElement : ConfigurationElement
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x000142F4 File Offset: 0x000124F4
		static KeyValueConfigurationElement()
		{
			KeyValueConfigurationElement._properties = new ConfigurationPropertyCollection();
			KeyValueConfigurationElement._properties.Add(KeyValueConfigurationElement._propKey);
			KeyValueConfigurationElement._properties.Add(KeyValueConfigurationElement._propValue);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00014367 File Offset: 0x00012567
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return KeyValueConfigurationElement._properties;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000127BF File Offset: 0x000109BF
		internal KeyValueConfigurationElement()
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001436E File Offset: 0x0001256E
		public KeyValueConfigurationElement(string key, string value)
		{
			this._needsInit = true;
			this._initKey = key;
			this._initValue = value;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001438B File Offset: 0x0001258B
		protected internal override void Init()
		{
			base.Init();
			if (this._needsInit)
			{
				this._needsInit = false;
				base[KeyValueConfigurationElement._propKey] = this._initKey;
				this.Value = this._initValue;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000143BF File Offset: 0x000125BF
		[ConfigurationProperty("key", Options = ConfigurationPropertyOptions.IsKey, DefaultValue = "")]
		public string Key
		{
			get
			{
				return (string)base[KeyValueConfigurationElement._propKey];
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000143D1 File Offset: 0x000125D1
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x000143E3 File Offset: 0x000125E3
		[ConfigurationProperty("value", DefaultValue = "")]
		public string Value
		{
			get
			{
				return (string)base[KeyValueConfigurationElement._propValue];
			}
			set
			{
				base[KeyValueConfigurationElement._propValue] = value;
			}
		}

		// Token: 0x04000290 RID: 656
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04000291 RID: 657
		private static readonly ConfigurationProperty _propKey = new ConfigurationProperty("key", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000292 RID: 658
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04000293 RID: 659
		private bool _needsInit;

		// Token: 0x04000294 RID: 660
		private string _initKey;

		// Token: 0x04000295 RID: 661
		private string _initValue;
	}
}

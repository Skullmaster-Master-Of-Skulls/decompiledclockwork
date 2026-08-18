using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006AF RID: 1711
	public sealed class ComContractElement : ConfigurationElement
	{
		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x000FB45C File Offset: 0x000F965C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("contract", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("exposedMethods", typeof(ComMethodElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("namespace", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("persistableTypes", typeof(ComPersistableTypeElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("requiresSession", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("userDefinedTypes", typeof(ComUdtElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000FB587 File Offset: 0x000F9787
		public ComContractElement()
		{
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000FB58F File Offset: 0x000F978F
		public ComContractElement(string contractType) : this()
		{
			this.Contract = contractType;
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06004258 RID: 16984 RVA: 0x000FB59E File Offset: 0x000F979E
		// (set) Token: 0x06004259 RID: 16985 RVA: 0x000FB5B0 File Offset: 0x000F97B0
		[ConfigurationProperty("contract", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string Contract
		{
			get
			{
				return (string)base["contract"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["contract"] = value;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x0600425A RID: 16986 RVA: 0x000FB5CD File Offset: 0x000F97CD
		[ConfigurationProperty("exposedMethods", Options = ConfigurationPropertyOptions.None)]
		public ComMethodElementCollection ExposedMethods
		{
			get
			{
				return (ComMethodElementCollection)base["exposedMethods"];
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x000FB5DF File Offset: 0x000F97DF
		// (set) Token: 0x0600425C RID: 16988 RVA: 0x000FB5F1 File Offset: 0x000F97F1
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.None)]
		[StringValidator(MinLength = 0)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["name"] = value;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x0600425D RID: 16989 RVA: 0x000FB60E File Offset: 0x000F980E
		// (set) Token: 0x0600425E RID: 16990 RVA: 0x000FB620 File Offset: 0x000F9820
		[ConfigurationProperty("namespace", DefaultValue = "", Options = ConfigurationPropertyOptions.None)]
		[StringValidator(MinLength = 0)]
		public string Namespace
		{
			get
			{
				return (string)base["namespace"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["namespace"] = value;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x000FB63D File Offset: 0x000F983D
		[ConfigurationProperty("persistableTypes")]
		public ComPersistableTypeElementCollection PersistableTypes
		{
			get
			{
				return (ComPersistableTypeElementCollection)base["persistableTypes"];
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x000FB64F File Offset: 0x000F984F
		// (set) Token: 0x06004261 RID: 16993 RVA: 0x000FB661 File Offset: 0x000F9861
		[ConfigurationProperty("requiresSession", DefaultValue = true)]
		public bool RequiresSession
		{
			get
			{
				return (bool)base["requiresSession"];
			}
			set
			{
				base["requiresSession"] = value;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x000FB674 File Offset: 0x000F9874
		[ConfigurationProperty("userDefinedTypes")]
		public ComUdtElementCollection UserDefinedTypes
		{
			get
			{
				return (ComUdtElementCollection)base["userDefinedTypes"];
			}
		}

		// Token: 0x04002CFE RID: 11518
		private ConfigurationPropertyCollection properties;
	}
}

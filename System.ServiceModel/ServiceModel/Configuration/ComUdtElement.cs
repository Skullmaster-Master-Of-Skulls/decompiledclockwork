using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B3 RID: 1715
	public sealed class ComUdtElement : ConfigurationElement
	{
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x000FB888 File Offset: 0x000F9A88
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("typeLibID", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired),
						new ConfigurationProperty("typeLibVersion", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired),
						new ConfigurationProperty("typeDefID", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x000FB95B File Offset: 0x000F9B5B
		public ComUdtElement()
		{
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x000FB963 File Offset: 0x000F9B63
		public ComUdtElement(string typeDefID) : this()
		{
			this.TypeDefID = typeDefID;
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x000FB972 File Offset: 0x000F9B72
		// (set) Token: 0x06004277 RID: 17015 RVA: 0x000FB984 File Offset: 0x000F9B84
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

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x000FB9A1 File Offset: 0x000F9BA1
		// (set) Token: 0x06004279 RID: 17017 RVA: 0x000FB9B3 File Offset: 0x000F9BB3
		[ConfigurationProperty("typeLibID", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string TypeLibID
		{
			get
			{
				return (string)base["typeLibID"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["typeLibID"] = value;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000FB9D0 File Offset: 0x000F9BD0
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x000FB9E2 File Offset: 0x000F9BE2
		[ConfigurationProperty("typeLibVersion", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string TypeLibVersion
		{
			get
			{
				return (string)base["typeLibVersion"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["typeLibVersion"] = value;
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x000FB9FF File Offset: 0x000F9BFF
		// (set) Token: 0x0600427D RID: 17021 RVA: 0x000FBA11 File Offset: 0x000F9C11
		[ConfigurationProperty("typeDefID", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string TypeDefID
		{
			get
			{
				return (string)base["typeDefID"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["typeDefID"] = value;
			}
		}

		// Token: 0x04002D02 RID: 11522
		private ConfigurationPropertyCollection properties;
	}
}

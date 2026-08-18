using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B2 RID: 1714
	public sealed class ComPersistableTypeElement : ConfigurationElement
	{
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x000FB794 File Offset: 0x000F9994
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("name", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("ID", typeof(string), null, null, new StringValidator(1, int.MaxValue, null), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000FB812 File Offset: 0x000F9A12
		public ComPersistableTypeElement()
		{
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000FB81A File Offset: 0x000F9A1A
		public ComPersistableTypeElement(string ID) : this()
		{
			this.ID = ID;
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x000FB829 File Offset: 0x000F9A29
		// (set) Token: 0x06004270 RID: 17008 RVA: 0x000FB83B File Offset: 0x000F9A3B
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

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x000FB858 File Offset: 0x000F9A58
		// (set) Token: 0x06004272 RID: 17010 RVA: 0x000FB86A File Offset: 0x000F9A6A
		[ConfigurationProperty("ID", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[StringValidator(MinLength = 1)]
		public string ID
		{
			get
			{
				return (string)base["ID"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["ID"] = value;
			}
		}

		// Token: 0x04002D01 RID: 11521
		private ConfigurationPropertyCollection properties;
	}
}

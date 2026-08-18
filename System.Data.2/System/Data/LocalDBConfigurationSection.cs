using System;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200012D RID: 301
	internal sealed class LocalDBConfigurationSection : ConfigurationSection
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00089DF8 File Offset: 0x000891F8
		[ConfigurationProperty("localdbinstances", IsRequired = true)]
		public LocalDBInstancesCollection LocalDbInstances
		{
			get
			{
				return ((LocalDBInstancesCollection)base["localdbinstances"]) ?? new LocalDBInstancesCollection();
			}
		}
	}
}

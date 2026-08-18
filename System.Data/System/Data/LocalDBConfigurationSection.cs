using System;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200033F RID: 831
	internal sealed class LocalDBConfigurationSection : ConfigurationSection
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x002C3A08 File Offset: 0x002C2E08
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

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002AF RID: 687
	internal class ConfigurationTypesFinder
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x000799B1 File Offset: 0x00077BB1
		public ConfigurationTypesFinder() : this(new ConfigurationTypeActivator(), new ConfigurationTypeFilter())
		{
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000799C3 File Offset: 0x00077BC3
		public ConfigurationTypesFinder(ConfigurationTypeActivator activator, ConfigurationTypeFilter filter)
		{
			this._activator = activator;
			this._filter = filter;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000799DC File Offset: 0x00077BDC
		public virtual void AddConfigurationTypesToModel(IEnumerable<Type> types, ModelConfiguration modelConfiguration)
		{
			foreach (Type type in types)
			{
				if (this._filter.IsEntityTypeConfiguration(type))
				{
					modelConfiguration.Add(this._activator.Activate<EntityTypeConfiguration>(type));
				}
				else if (this._filter.IsComplexTypeConfiguration(type))
				{
					modelConfiguration.Add(this._activator.Activate<ComplexTypeConfiguration>(type));
				}
			}
		}

		// Token: 0x04000870 RID: 2160
		private readonly ConfigurationTypeActivator _activator;

		// Token: 0x04000871 RID: 2161
		private readonly ConfigurationTypeFilter _filter;
	}
}

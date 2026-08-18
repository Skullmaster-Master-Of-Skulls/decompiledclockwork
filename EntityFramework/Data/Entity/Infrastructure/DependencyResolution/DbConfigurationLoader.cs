using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000158 RID: 344
	internal class DbConfigurationLoader
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x00038278 File Offset: 0x00036478
		public virtual Type TryLoadFromConfig(AppConfig config)
		{
			string configurationTypeName = config.ConfigurationTypeName;
			if (string.IsNullOrWhiteSpace(configurationTypeName))
			{
				return null;
			}
			Type type;
			try
			{
				type = Type.GetType(configurationTypeName, true);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Strings.DbConfigurationTypeNotFound(configurationTypeName), innerException);
			}
			if (!typeof(DbConfiguration).IsAssignableFrom(type))
			{
				throw new InvalidOperationException(Strings.CreateInstance_BadDbConfigurationType(type.ToString(), typeof(DbConfiguration).ToString()));
			}
			return type;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000382F4 File Offset: 0x000364F4
		public virtual bool AppConfigContainsDbConfigurationType(AppConfig config)
		{
			return !string.IsNullOrWhiteSpace(config.ConfigurationTypeName);
		}
	}
}

using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	// Token: 0x0200001F RID: 31
	public class ConfigurationBuilderCollection : ProviderCollection
	{
		// Token: 0x06000133 RID: 307 RVA: 0x000094C4 File Offset: 0x000076C4
		public override void Add(ProviderBase builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (!(builder is ConfigurationBuilder))
			{
				throw new ArgumentException(SR.GetString("Config_provider_must_implement_type", new object[]
				{
					typeof(ConfigurationBuilder).ToString()
				}), "builder");
			}
			base.Add(builder);
		}

		// Token: 0x17000054 RID: 84
		public ConfigurationBuilder this[string name]
		{
			get
			{
				return (ConfigurationBuilder)base[name];
			}
		}
	}
}

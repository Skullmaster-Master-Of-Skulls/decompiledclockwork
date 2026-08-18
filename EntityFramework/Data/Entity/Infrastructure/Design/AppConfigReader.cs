using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Internal.ConfigFile;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x02000147 RID: 327
	public class AppConfigReader
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x00036DE4 File Offset: 0x00034FE4
		public AppConfigReader(Configuration configuration)
		{
			Check.NotNull<Configuration>(configuration, "configuration");
			this._configuration = configuration;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00036E24 File Offset: 0x00035024
		public string GetProviderServices(string invariantName)
		{
			IEnumerable<ProviderElement> source = ((EntityFrameworkSection)this._configuration.GetSection("entityFramework")).Providers.Cast<ProviderElement>();
			return (from p in source
			where p.InvariantName == invariantName
			select p.ProviderTypeName).FirstOrDefault<string>();
		}

		// Token: 0x040002E2 RID: 738
		private readonly Configuration _configuration;
	}
}

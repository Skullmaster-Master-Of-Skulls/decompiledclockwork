using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Public.IoC
{
	// Token: 0x020000C2 RID: 194
	public class CommonPublicConventionInjection : ConventionInjection
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000E174 File Offset: 0x0000C374
		public CommonPublicConventionInjection()
		{
			this.DefaultObjectMap = new Dictionary<Type, IcwObject>
			{
				{
					typeof(ICacheStorageManager),
					this.RetrieveIcwObject<CacheStorageManager>(DefaultLifetime.Singleton.ToString())
				},
				{
					typeof(ApplicationContext),
					this.RetrieveIcwObject<ApplicationContext>(DefaultLifetime.Singleton.ToString())
				}
			};
		}
	}
}

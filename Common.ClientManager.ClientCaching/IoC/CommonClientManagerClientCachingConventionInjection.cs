using System;
using System.Collections.Generic;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.ClientCaching.IoC
{
	// Token: 0x02000003 RID: 3
	public class CommonClientManagerClientCachingConventionInjection : ConventionInjection
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00003538 File Offset: 0x00001738
		public CommonClientManagerClientCachingConventionInjection()
		{
			this.DefaultObjectMap = new Dictionary<Type, IcwObject>
			{
				{
					typeof(ClientCache),
					this.RetrieveIcwObject<ClientCache>(DefaultLifetime.Singleton.ToString())
				}
			};
		}
	}
}

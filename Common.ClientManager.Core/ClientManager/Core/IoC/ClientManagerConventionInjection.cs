using System;
using System.Collections.Generic;
using TechnoPro.Common.ClientManager.Core.Azure.Storage;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Azure.Storage;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.IoC
{
	// Token: 0x0200004D RID: 77
	public class ClientManagerConventionInjection : ConventionInjection
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000C284 File Offset: 0x0000A484
		public ClientManagerConventionInjection()
		{
			this.DefaultObjectMap = new Dictionary<Type, IcwObject>
			{
				{
					typeof(IRequestBuilderClientManager),
					this.RetrieveIcwObject<RequestBuilderClientManager>(DefaultLifetime.Singleton.ToString())
				},
				{
					typeof(IClockWorkSasTokenProviderClientManager),
					this.RetrieveIcwObject<ClockWorkSasTokenProviderClientManager>(DefaultLifetime.Transient.ToString())
				}
			};
		}
	}
}

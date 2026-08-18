using System;
using System.Collections.Generic;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Core.Impl.IoC
{
	// Token: 0x02000005 RID: 5
	public class ClockWorkServerCoreImplConventionInjection : ConventionInjection
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002C60 File Offset: 0x00000E60
		public ClockWorkServerCoreImplConventionInjection()
		{
			this.DefaultObjectMap = new Dictionary<Type, IcwObject>
			{
				{
					typeof(ServerExecutingContext),
					this.RetrieveIcwObject<ServerExecutingContext>(DefaultLifetime.Singleton.ToString())
				}
			};
		}
	}
}

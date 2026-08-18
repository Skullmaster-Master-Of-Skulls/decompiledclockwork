using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Public.Entities.OperationContexts
{
	// Token: 0x02000271 RID: 625
	public class ServiceProvidersOperationContext : OperationContext
	{
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00018FC6 File Offset: 0x000171C6
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x00018FCE File Offset: 0x000171CE
		public IList<ServiceProviderType> ServiceProviderTypes { get; set; }
	}
}

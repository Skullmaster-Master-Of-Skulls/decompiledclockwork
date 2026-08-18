using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200044C RID: 1100
	[KnownType(typeof(List<ServiceInfo>))]
	internal sealed class ServiceInfoCollection : Collection<ServiceInfo>
	{
		// Token: 0x06002AD8 RID: 10968 RVA: 0x000A74C0 File Offset: 0x000A56C0
		internal ServiceInfoCollection(IEnumerable<ServiceHostBase> services)
		{
			foreach (ServiceHostBase service in services)
			{
				base.Add(new ServiceInfo(service));
			}
		}
	}
}

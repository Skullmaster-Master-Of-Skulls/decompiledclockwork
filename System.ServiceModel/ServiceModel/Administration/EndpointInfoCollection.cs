using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Description;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000448 RID: 1096
	internal sealed class EndpointInfoCollection : Collection<EndpointInfo>
	{
		// Token: 0x06002AAB RID: 10923 RVA: 0x000A53C8 File Offset: 0x000A35C8
		internal EndpointInfoCollection(ServiceEndpointCollection endpoints, string serviceName)
		{
			for (int i = 0; i < endpoints.Count; i++)
			{
				base.Add(new EndpointInfo(endpoints[i], serviceName));
			}
		}
	}
}

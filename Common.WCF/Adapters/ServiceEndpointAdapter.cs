using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace TechnoPro.Common.WCF.Adapters
{
	// Token: 0x0200001F RID: 31
	public static class ServiceEndpointAdapter
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003EA8 File Offset: 0x000020A8
		public static void VerifyQueue(this ServiceEndpoint endpoint, bool transactional = false)
		{
			bool flag = endpoint.Binding is NetMsmqBinding;
			if (flag)
			{
				endpoint.Address.VerifyQueue(transactional);
			}
		}
	}
}

using System;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200021B RID: 539
	internal class DllHostedComPlusServiceHost : ComPlusServiceHost
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x0003C7AE File Offset: 0x0003A9AE
		public DllHostedComPlusServiceHost(Guid clsid, ServiceElement service, ComCatalogObject applicationObject, ComCatalogObject classObject)
		{
			base.Initialize(clsid, service, applicationObject, classObject, HostingMode.ComPlus);
		}
	}
}

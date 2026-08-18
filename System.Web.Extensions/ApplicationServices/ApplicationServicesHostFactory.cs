using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace System.Web.ApplicationServices
{
	// Token: 0x0200011B RID: 283
	public class ApplicationServicesHostFactory : ServiceHostFactory
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x00035D74 File Offset: 0x00033F74
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			ServiceHost result;
			if (typeof(ProfileService).Equals(serviceType))
			{
				result = new ServiceHost(new ProfileService(), baseAddresses);
			}
			else if (typeof(RoleService).Equals(serviceType))
			{
				result = new ServiceHost(new RoleService(), baseAddresses);
			}
			else if (typeof(AuthenticationService).Equals(serviceType))
			{
				result = new ServiceHost(new AuthenticationService(), baseAddresses);
			}
			else
			{
				result = base.CreateServiceHost(serviceType, baseAddresses);
			}
			return result;
		}
	}
}

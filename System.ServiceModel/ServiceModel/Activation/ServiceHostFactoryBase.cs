using System;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C1 RID: 1473
	public abstract class ServiceHostFactoryBase
	{
		// Token: 0x06003980 RID: 14720
		public abstract ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses);
	}
}

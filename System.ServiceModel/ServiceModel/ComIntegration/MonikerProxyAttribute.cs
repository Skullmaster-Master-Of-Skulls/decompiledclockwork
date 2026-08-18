using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Proxies;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200023D RID: 573
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class MonikerProxyAttribute : ProxyAttribute, ICustomFactory
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x0003E839 File Offset: 0x0003CA39
		public override MarshalByRefObject CreateInstance(Type serverType)
		{
			if (serverType != typeof(ServiceMoniker))
			{
				throw Fx.AssertAndThrow("MonikerProxyAttribute can only be used for the service Moniker");
			}
			return MonikerBuilder.CreateMonikerInstance();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0003E85D File Offset: 0x0003CA5D
		MarshalByRefObject ICustomFactory.CreateInstance(Type serverType)
		{
			if (serverType != typeof(ServiceMoniker))
			{
				throw Fx.AssertAndThrow("MonikerProxyAttribute can only be used for the service Moniker");
			}
			return MonikerBuilder.CreateMonikerInstance();
		}
	}
}

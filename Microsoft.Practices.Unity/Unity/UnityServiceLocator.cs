using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ServiceLocation;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200009F RID: 159
	public sealed class UnityServiceLocator : ServiceLocatorImplBase, IDisposable
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x000093BF File Offset: 0x000075BF
		public UnityServiceLocator(IUnityContainer container)
		{
			this.container = container;
			container.RegisterInstance(this, new ExternallyControlledLifetimeManager());
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000093DB File Offset: 0x000075DB
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "Object is not finalizable, no reason to call SuppressFinalize")]
		public void Dispose()
		{
			if (this.container != null)
			{
				this.container.Dispose();
				this.container = null;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000093F7 File Offset: 0x000075F7
		protected override object DoGetInstance(Type serviceType, string key)
		{
			if (this.container == null)
			{
				throw new ObjectDisposedException("container");
			}
			return this.container.Resolve(serviceType, key, new ResolverOverride[0]);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000941F File Offset: 0x0000761F
		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			if (this.container == null)
			{
				throw new ObjectDisposedException("container");
			}
			return this.container.ResolveAll(serviceType, new ResolverOverride[0]);
		}

		// Token: 0x040000A0 RID: 160
		private IUnityContainer container;
	}
}

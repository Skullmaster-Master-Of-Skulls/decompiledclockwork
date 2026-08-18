using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200008C RID: 140
	public class LifetimeManagerFactory : ILifetimeFactoryPolicy, IBuilderPolicy
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000854D File Offset: 0x0000674D
		public LifetimeManagerFactory(ExtensionContext containerContext, Type lifetimeType)
		{
			this.containerContext = containerContext;
			this.LifetimeType = lifetimeType;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00008564 File Offset: 0x00006764
		public ILifetimePolicy CreateLifetimePolicy()
		{
			LifetimeManager lifetimeManager = (LifetimeManager)this.containerContext.Container.Resolve(this.LifetimeType, new ResolverOverride[0]);
			if (lifetimeManager is IDisposable)
			{
				this.containerContext.Lifetime.Add(lifetimeManager);
			}
			lifetimeManager.InUse = true;
			return lifetimeManager;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000085B4 File Offset: 0x000067B4
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000085BC File Offset: 0x000067BC
		public Type LifetimeType { get; private set; }

		// Token: 0x0400008D RID: 141
		private readonly ExtensionContext containerContext;
	}
}

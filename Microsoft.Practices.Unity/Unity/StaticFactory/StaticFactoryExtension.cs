using System;

namespace Microsoft.Practices.Unity.StaticFactory
{
	// Token: 0x02000078 RID: 120
	[Obsolete("Use RegisterType<TInterface, TImpl>(new InjectionFactory(...)) instead of the extension's methods.")]
	public class StaticFactoryExtension : UnityContainerExtension, IStaticFactoryConfiguration, IUnityContainerExtensionConfigurator
	{
		// Token: 0x060001FE RID: 510 RVA: 0x000073EF File Offset: 0x000055EF
		protected override void Initialize()
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000073F4 File Offset: 0x000055F4
		public IStaticFactoryConfiguration RegisterFactory<TTypeToBuild>(string name, Func<IUnityContainer, object> factoryMethod)
		{
			base.Container.RegisterType(name, new InjectionMember[]
			{
				new InjectionFactory(factoryMethod)
			});
			return this;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007420 File Offset: 0x00005620
		public IStaticFactoryConfiguration RegisterFactory<TTypeToBuild>(Func<IUnityContainer, object> factoryMethod)
		{
			return this.RegisterFactory<TTypeToBuild>(null, factoryMethod);
		}
	}
}

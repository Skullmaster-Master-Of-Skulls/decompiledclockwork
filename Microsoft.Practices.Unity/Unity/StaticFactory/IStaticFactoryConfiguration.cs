using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity.StaticFactory
{
	// Token: 0x02000036 RID: 54
	public interface IStaticFactoryConfiguration : IUnityContainerExtensionConfigurator
	{
		// Token: 0x060000BE RID: 190
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		IStaticFactoryConfiguration RegisterFactory<TTypeToBuild>(Func<IUnityContainer, object> factoryMethod);

		// Token: 0x060000BF RID: 191
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		IStaticFactoryConfiguration RegisterFactory<TTypeToBuild>(string name, Func<IUnityContainer, object> factoryMethod);
	}
}

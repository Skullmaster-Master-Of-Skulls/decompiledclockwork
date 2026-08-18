using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000035 RID: 53
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Configurator", Justification = "Configurator IS spelled correctly")]
	public interface IUnityContainerExtensionConfigurator
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000BD RID: 189
		IUnityContainer Container { get; }
	}
}

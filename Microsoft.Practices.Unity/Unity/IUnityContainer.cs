using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200008A RID: 138
	public interface IUnityContainer : IDisposable
	{
		// Token: 0x06000281 RID: 641
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "To", Justification = "Identifier name 'to' makes sense. Avoid changing public API names.")]
		IUnityContainer RegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers);

		// Token: 0x06000282 RID: 642
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		IUnityContainer RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime);

		// Token: 0x06000283 RID: 643
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides);

		// Token: 0x06000284 RID: 644
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides);

		// Token: 0x06000285 RID: 645
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides);

		// Token: 0x06000286 RID: 646
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o", Justification = "Parameter name is valid in context")]
		void Teardown(object o);

		// Token: 0x06000287 RID: 647
		IUnityContainer AddExtension(UnityContainerExtension extension);

		// Token: 0x06000288 RID: 648
		object Configure(Type configurationInterface);

		// Token: 0x06000289 RID: 649
		IUnityContainer RemoveAllExtensions();

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600028A RID: 650
		IUnityContainer Parent { get; }

		// Token: 0x0600028B RID: 651
		IUnityContainer CreateChildContainer();

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600028C RID: 652
		IEnumerable<ContainerRegistration> Registrations { get; }
	}
}

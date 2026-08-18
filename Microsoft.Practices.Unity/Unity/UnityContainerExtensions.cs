using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200007B RID: 123
	public static class UnityContainerExtensions
	{
		// Token: 0x06000205 RID: 517 RVA: 0x000074B7 File Offset: 0x000056B7
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<T>(this IUnityContainer container, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, typeof(T), null, null, injectionMembers);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000074D8 File Offset: 0x000056D8
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<TFrom, TTo>(this IUnityContainer container, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(typeof(TFrom), typeof(TTo), null, null, injectionMembers);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007502 File Offset: 0x00005702
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<TFrom, TTo>(this IUnityContainer container, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(typeof(TFrom), typeof(TTo), null, lifetimeManager, injectionMembers);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000752C File Offset: 0x0000572C
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<TFrom, TTo>(this IUnityContainer container, string name, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(typeof(TFrom), typeof(TTo), name, null, injectionMembers);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007556 File Offset: 0x00005756
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<TFrom, TTo>(this IUnityContainer container, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(typeof(TFrom), typeof(TTo), name, lifetimeManager, injectionMembers);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007580 File Offset: 0x00005780
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<T>(this IUnityContainer container, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, typeof(T), null, lifetimeManager, injectionMembers);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000075A1 File Offset: 0x000057A1
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<T>(this IUnityContainer container, string name, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, typeof(T), name, null, injectionMembers);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000075C2 File Offset: 0x000057C2
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer RegisterType<T>(this IUnityContainer container, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, typeof(T), name, lifetimeManager, injectionMembers);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000075E3 File Offset: 0x000057E3
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static IUnityContainer RegisterType(this IUnityContainer container, Type t, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, t, null, null, injectionMembers);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000075FB File Offset: 0x000057FB
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "To", Justification = "Identifier name 'to' makes sense. Avoid changing public API names.")]
		public static IUnityContainer RegisterType(this IUnityContainer container, Type from, Type to, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(from, to, null, null, injectionMembers);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007613 File Offset: 0x00005813
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "To", Justification = "Identifier name 'to' makes sense. Avoid changing public API names.")]
		public static IUnityContainer RegisterType(this IUnityContainer container, Type from, Type to, string name, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(from, to, name, null, injectionMembers);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000762C File Offset: 0x0000582C
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "To", Justification = "Identifier name 'to' makes sense. Avoid changing public API names.")]
		public static IUnityContainer RegisterType(this IUnityContainer container, Type from, Type to, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(from, to, null, lifetimeManager, injectionMembers);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007645 File Offset: 0x00005845
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static IUnityContainer RegisterType(this IUnityContainer container, Type t, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, t, null, lifetimeManager, injectionMembers);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000765D File Offset: 0x0000585D
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static IUnityContainer RegisterType(this IUnityContainer container, Type t, string name, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, t, name, null, injectionMembers);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007675 File Offset: 0x00005875
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static IUnityContainer RegisterType(this IUnityContainer container, Type t, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterType(null, t, name, lifetimeManager, injectionMembers);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000768E File Offset: 0x0000588E
		public static IUnityContainer RegisterInstance<TInterface>(this IUnityContainer container, TInterface instance)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterInstance(typeof(TInterface), null, instance, UnityContainerExtensions.CreateDefaultInstanceLifetimeManager());
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000076B7 File Offset: 0x000058B7
		public static IUnityContainer RegisterInstance<TInterface>(this IUnityContainer container, TInterface instance, LifetimeManager lifetimeManager)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterInstance(typeof(TInterface), null, instance, lifetimeManager);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000076DC File Offset: 0x000058DC
		public static IUnityContainer RegisterInstance<TInterface>(this IUnityContainer container, string name, TInterface instance)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterInstance(typeof(TInterface), name, instance, UnityContainerExtensions.CreateDefaultInstanceLifetimeManager());
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007705 File Offset: 0x00005905
		public static IUnityContainer RegisterInstance<TInterface>(this IUnityContainer container, string name, TInterface instance, LifetimeManager lifetimeManager)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterInstance(typeof(TInterface), name, instance, lifetimeManager);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000772A File Offset: 0x0000592A
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static IUnityContainer RegisterInstance(this IUnityContainer container, Type t, object instance)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterInstance(t, null, instance, UnityContainerExtensions.CreateDefaultInstanceLifetimeManager());
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007745 File Offset: 0x00005945
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static IUnityContainer RegisterInstance(this IUnityContainer container, Type t, object instance, LifetimeManager lifetimeManager)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterInstance(t, null, instance, lifetimeManager);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000775C File Offset: 0x0000595C
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static IUnityContainer RegisterInstance(this IUnityContainer container, Type t, string name, object instance)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.RegisterInstance(t, name, instance, UnityContainerExtensions.CreateDefaultInstanceLifetimeManager());
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007777 File Offset: 0x00005977
		public static T Resolve<T>(this IUnityContainer container, params ResolverOverride[] overrides)
		{
			Guard.ArgumentNotNull(container, "container");
			return (T)((object)container.Resolve(typeof(T), null, overrides));
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000779B File Offset: 0x0000599B
		public static T Resolve<T>(this IUnityContainer container, string name, params ResolverOverride[] overrides)
		{
			Guard.ArgumentNotNull(container, "container");
			return (T)((object)container.Resolve(typeof(T), name, overrides));
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000077BF File Offset: 0x000059BF
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static object Resolve(this IUnityContainer container, Type t, params ResolverOverride[] overrides)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.Resolve(t, null, overrides);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000077D5 File Offset: 0x000059D5
		public static IEnumerable<T> ResolveAll<T>(this IUnityContainer container, params ResolverOverride[] resolverOverrides)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.ResolveAll(typeof(T), resolverOverrides).Cast<T>();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000077F8 File Offset: 0x000059F8
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
		public static T BuildUp<T>(this IUnityContainer container, T existing, params ResolverOverride[] resolverOverrides)
		{
			Guard.ArgumentNotNull(container, "container");
			return (T)((object)container.BuildUp(typeof(T), existing, null, resolverOverrides));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007822 File Offset: 0x00005A22
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "BuildUp is correct.")]
		public static T BuildUp<T>(this IUnityContainer container, T existing, string name, params ResolverOverride[] resolverOverrides)
		{
			Guard.ArgumentNotNull(container, "container");
			return (T)((object)container.BuildUp(typeof(T), existing, name, resolverOverrides));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000784C File Offset: 0x00005A4C
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BuildUp", Justification = "Backwards compatibility with ObjectBuilder")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t", Justification = "Parameter name is meaningful enough in context")]
		public static object BuildUp(this IUnityContainer container, Type t, object existing, params ResolverOverride[] resolverOverrides)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.BuildUp(t, existing, null, resolverOverrides);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007864 File Offset: 0x00005A64
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static IUnityContainer AddNewExtension<TExtension>(this IUnityContainer container) where TExtension : UnityContainerExtension
		{
			Guard.ArgumentNotNull(container, "container");
			TExtension textension = container.Resolve(new ResolverOverride[0]);
			return container.AddExtension(textension);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007895 File Offset: 0x00005A95
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Configurator", Justification = "Configurator IS spelled correctly")]
		public static TConfigurator Configure<TConfigurator>(this IUnityContainer container) where TConfigurator : IUnityContainerExtensionConfigurator
		{
			Guard.ArgumentNotNull(container, "container");
			return (TConfigurator)((object)container.Configure(typeof(TConfigurator)));
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000078B7 File Offset: 0x00005AB7
		public static bool IsRegistered(this IUnityContainer container, Type typeToCheck)
		{
			Guard.ArgumentNotNull(container, "container");
			Guard.ArgumentNotNull(typeToCheck, "typeToCheck");
			return container.IsRegistered(typeToCheck, null);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007904 File Offset: 0x00005B04
		public static bool IsRegistered(this IUnityContainer container, Type typeToCheck, string nameToCheck)
		{
			Guard.ArgumentNotNull(container, "container");
			Guard.ArgumentNotNull(typeToCheck, "typeToCheck");
			IEnumerable<ContainerRegistration> source = from r in container.Registrations
			where r.RegisteredType == typeToCheck && r.Name == nameToCheck
			select r;
			return source.FirstOrDefault<ContainerRegistration>() != null;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007964 File Offset: 0x00005B64
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static bool IsRegistered<T>(this IUnityContainer container)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.IsRegistered(typeof(T));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007981 File Offset: 0x00005B81
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static bool IsRegistered<T>(this IUnityContainer container, string nameToCheck)
		{
			Guard.ArgumentNotNull(container, "container");
			return container.IsRegistered(typeof(T), nameToCheck);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000799F File Offset: 0x00005B9F
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Factory method to create a disposable but is not expected to owns its lifetime.")]
		private static LifetimeManager CreateDefaultInstanceLifetimeManager()
		{
			return new ContainerControlledLifetimeManager();
		}
	}
}

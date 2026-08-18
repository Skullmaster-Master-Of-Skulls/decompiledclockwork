using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000082 RID: 130
	[Obsolete("Use the IUnityContainer.RegisterType method instead of this interface")]
	public class InjectedMembers : UnityContainerExtension
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00007C9A File Offset: 0x00005E9A
		protected override void Initialize()
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007C9C File Offset: 0x00005E9C
		public InjectedMembers ConfigureInjectionFor<TTypeToInject>(params InjectionMember[] injectionMembers)
		{
			return this.ConfigureInjectionFor(typeof(TTypeToInject), null, injectionMembers);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007CB0 File Offset: 0x00005EB0
		public InjectedMembers ConfigureInjectionFor<TTypeToInject>(string name, params InjectionMember[] injectionMembers)
		{
			return this.ConfigureInjectionFor(typeof(TTypeToInject), name, injectionMembers);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public InjectedMembers ConfigureInjectionFor(Type typeToInject, params InjectionMember[] injectionMembers)
		{
			return this.ConfigureInjectionFor(null, typeToInject, null, injectionMembers);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public InjectedMembers ConfigureInjectionFor(Type typeToInject, string name, params InjectionMember[] injectionMembers)
		{
			return this.ConfigureInjectionFor(null, typeToInject, name, injectionMembers);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007CDC File Offset: 0x00005EDC
		public InjectedMembers ConfigureInjectionFor(Type serviceType, Type implementationType, string name, params InjectionMember[] injectionMembers)
		{
			base.Container.RegisterType(serviceType, implementationType, name, injectionMembers);
			return this;
		}
	}
}

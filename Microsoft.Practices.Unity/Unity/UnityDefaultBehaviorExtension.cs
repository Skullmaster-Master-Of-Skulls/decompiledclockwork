using System;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.Unity
{
	// Token: 0x020000A6 RID: 166
	public class UnityDefaultBehaviorExtension : UnityContainerExtension
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000A560 File Offset: 0x00008760
		protected override void Initialize()
		{
			base.Context.Registering += this.OnRegister;
			base.Context.RegisteringInstance += this.OnRegisterInstance;
			base.Container.RegisterInstance(base.Container, new UnityDefaultBehaviorExtension.ContainerLifetimeManager());
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A5B2 File Offset: 0x000087B2
		public override void Remove()
		{
			base.Context.Registering -= this.OnRegister;
			base.Context.RegisteringInstance -= this.OnRegisterInstance;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A5E4 File Offset: 0x000087E4
		private void OnRegister(object sender, RegisterEventArgs e)
		{
			base.Context.RegisterNamedType(e.TypeFrom ?? e.TypeTo, e.Name);
			if (e.TypeFrom != null)
			{
				if (e.TypeFrom.GetTypeInfo().IsGenericTypeDefinition && e.TypeTo.GetTypeInfo().IsGenericTypeDefinition)
				{
					base.Context.Policies.Set(new GenericTypeBuildKeyMappingPolicy(new NamedTypeBuildKey(e.TypeTo, e.Name)), new NamedTypeBuildKey(e.TypeFrom, e.Name));
				}
				else
				{
					base.Context.Policies.Set(new BuildKeyMappingPolicy(new NamedTypeBuildKey(e.TypeTo, e.Name)), new NamedTypeBuildKey(e.TypeFrom, e.Name));
				}
			}
			if (e.LifetimeManager != null)
			{
				this.SetLifetimeManager(e.TypeTo, e.Name, e.LifetimeManager);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000A6D4 File Offset: 0x000088D4
		private void OnRegisterInstance(object sender, RegisterInstanceEventArgs e)
		{
			base.Context.RegisterNamedType(e.RegisteredType, e.Name);
			this.SetLifetimeManager(e.RegisteredType, e.Name, e.LifetimeManager);
			NamedTypeBuildKey namedTypeBuildKey = new NamedTypeBuildKey(e.RegisteredType, e.Name);
			base.Context.Policies.Set(new BuildKeyMappingPolicy(namedTypeBuildKey), namedTypeBuildKey);
			e.LifetimeManager.SetValue(e.Instance);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A74C File Offset: 0x0000894C
		private void SetLifetimeManager(Type lifetimeType, string name, LifetimeManager lifetimeManager)
		{
			if (lifetimeManager.InUse)
			{
				throw new InvalidOperationException(Resources.LifetimeManagerInUse);
			}
			if (lifetimeType.GetTypeInfo().IsGenericTypeDefinition)
			{
				LifetimeManagerFactory policy = new LifetimeManagerFactory(base.Context, lifetimeManager.GetType());
				base.Context.Policies.Set(policy, new NamedTypeBuildKey(lifetimeType, name));
				return;
			}
			lifetimeManager.InUse = true;
			base.Context.Policies.Set(lifetimeManager, new NamedTypeBuildKey(lifetimeType, name));
			if (lifetimeManager is IDisposable)
			{
				base.Context.Lifetime.Add(lifetimeManager);
			}
		}

		// Token: 0x020000A7 RID: 167
		private class ContainerLifetimeManager : LifetimeManager
		{
			// Token: 0x06000351 RID: 849 RVA: 0x0000A7E4 File Offset: 0x000089E4
			public override object GetValue()
			{
				return this.value;
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0000A7EC File Offset: 0x000089EC
			public override void SetValue(object newValue)
			{
				this.value = newValue;
			}

			// Token: 0x06000353 RID: 851 RVA: 0x0000A7F5 File Offset: 0x000089F5
			public override void RemoveValue()
			{
			}

			// Token: 0x040000BE RID: 190
			private object value;
		}
	}
}

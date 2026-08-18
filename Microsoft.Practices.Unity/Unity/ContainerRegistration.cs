using System;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200000F RID: 15
	public class ContainerRegistration
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002682 File Offset: 0x00000882
		internal ContainerRegistration(Type registeredType, string name, IPolicyList policies)
		{
			this.buildKey = new NamedTypeBuildKey(registeredType, name);
			this.MappedToType = this.GetMappedType(policies);
			this.LifetimeManagerType = this.GetLifetimeManagerType(policies);
			this.LifetimeManager = this.GetLifetimeManager(policies);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000026BE File Offset: 0x000008BE
		public Type RegisteredType
		{
			get
			{
				return this.buildKey.Type;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026CB File Offset: 0x000008CB
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026D3 File Offset: 0x000008D3
		public Type MappedToType { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026DC File Offset: 0x000008DC
		public string Name
		{
			get
			{
				return this.buildKey.Name;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026E9 File Offset: 0x000008E9
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026F1 File Offset: 0x000008F1
		public Type LifetimeManagerType { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026FA File Offset: 0x000008FA
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002702 File Offset: 0x00000902
		public LifetimeManager LifetimeManager { get; private set; }

		// Token: 0x06000032 RID: 50 RVA: 0x0000270C File Offset: 0x0000090C
		private Type GetMappedType(IPolicyList policies)
		{
			IBuildKeyMappingPolicy buildKeyMappingPolicy = policies.Get(this.buildKey);
			if (buildKeyMappingPolicy != null)
			{
				return buildKeyMappingPolicy.Map(this.buildKey, null).Type;
			}
			return this.buildKey.Type;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002748 File Offset: 0x00000948
		private Type GetLifetimeManagerType(IPolicyList policies)
		{
			NamedTypeBuildKey namedTypeBuildKey = new NamedTypeBuildKey(this.MappedToType, this.Name);
			ILifetimePolicy lifetimePolicy = policies.Get(namedTypeBuildKey);
			if (lifetimePolicy != null)
			{
				return lifetimePolicy.GetType();
			}
			if (this.MappedToType.GetTypeInfo().IsGenericType)
			{
				NamedTypeBuildKey namedTypeBuildKey2 = new NamedTypeBuildKey(this.MappedToType.GetGenericTypeDefinition(), this.Name);
				ILifetimeFactoryPolicy lifetimeFactoryPolicy = policies.Get(namedTypeBuildKey2);
				if (lifetimeFactoryPolicy != null)
				{
					return lifetimeFactoryPolicy.LifetimeType;
				}
			}
			return typeof(TransientLifetimeManager);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027C0 File Offset: 0x000009C0
		private LifetimeManager GetLifetimeManager(IPolicyList policies)
		{
			NamedTypeBuildKey namedTypeBuildKey = new NamedTypeBuildKey(this.MappedToType, this.Name);
			return (LifetimeManager)policies.Get(namedTypeBuildKey);
		}

		// Token: 0x0400000A RID: 10
		private readonly NamedTypeBuildKey buildKey;
	}
}

using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000019 RID: 25
	public class InjectionFactory : InjectionMember
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002E64 File Offset: 0x00001064
		public InjectionFactory(Func<IUnityContainer, object> factoryFunc) : this((IUnityContainer c, Type t, string s) => factoryFunc(c))
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E97 File Offset: 0x00001097
		public InjectionFactory(Func<IUnityContainer, Type, string, object> factoryFunc)
		{
			Guard.ArgumentNotNull(factoryFunc, "factoryFunc");
			this.factoryFunc = factoryFunc;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EB4 File Offset: 0x000010B4
		public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
		{
			Guard.ArgumentNotNull(implementationType, "implementationType");
			Guard.ArgumentNotNull(policies, "policies");
			FactoryDelegateBuildPlanPolicy policy = new FactoryDelegateBuildPlanPolicy(this.factoryFunc);
			policies.Set(policy, new NamedTypeBuildKey(implementationType, name));
		}

		// Token: 0x04000015 RID: 21
		private readonly Func<IUnityContainer, Type, string, object> factoryFunc;
	}
}

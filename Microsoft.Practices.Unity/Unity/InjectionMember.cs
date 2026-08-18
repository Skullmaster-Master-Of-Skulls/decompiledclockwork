using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000018 RID: 24
	public abstract class InjectionMember
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002E3A File Offset: 0x0000103A
		public void AddPolicies(Type typeToCreate, IPolicyList policies)
		{
			this.AddPolicies(null, typeToCreate, null, policies);
		}

		// Token: 0x06000056 RID: 86
		public abstract void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies);
	}
}

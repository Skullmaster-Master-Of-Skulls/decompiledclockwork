using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x02000094 RID: 148
	public class LiteralValueDependencyResolverPolicy : IDependencyResolverPolicy, IBuilderPolicy
	{
		// Token: 0x060002AD RID: 685 RVA: 0x000088E2 File Offset: 0x00006AE2
		public LiteralValueDependencyResolverPolicy(object dependencyValue)
		{
			this.dependencyValue = dependencyValue;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000088F1 File Offset: 0x00006AF1
		public object Resolve(IBuilderContext context)
		{
			return this.dependencyValue;
		}

		// Token: 0x04000093 RID: 147
		private object dependencyValue;
	}
}

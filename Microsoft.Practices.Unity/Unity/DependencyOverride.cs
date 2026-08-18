using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000010 RID: 16
	public class DependencyOverride : ResolverOverride
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000027EB File Offset: 0x000009EB
		public DependencyOverride(Type typeToConstruct, object dependencyValue)
		{
			this.typeToConstruct = typeToConstruct;
			this.dependencyValue = InjectionParameterValue.ToParameter(dependencyValue);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002808 File Offset: 0x00000A08
		public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
		{
			IDependencyResolverPolicy result = null;
			if (dependencyType == this.typeToConstruct)
			{
				result = this.dependencyValue.GetResolverPolicy(dependencyType);
			}
			return result;
		}

		// Token: 0x0400000E RID: 14
		private readonly InjectionParameterValue dependencyValue;

		// Token: 0x0400000F RID: 15
		private readonly Type typeToConstruct;
	}
}

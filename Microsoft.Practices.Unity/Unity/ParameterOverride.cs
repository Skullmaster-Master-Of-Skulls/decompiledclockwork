using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000031 RID: 49
	public class ParameterOverride : ResolverOverride
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000377D File Offset: 0x0000197D
		public ParameterOverride(string parameterName, object parameterValue)
		{
			this.parameterName = parameterName;
			this.parameterValue = InjectionParameterValue.ToParameter(parameterValue);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003798 File Offset: 0x00001998
		public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
		{
			Guard.ArgumentNotNull(context, "context");
			ConstructorArgumentResolveOperation constructorArgumentResolveOperation = context.CurrentOperation as ConstructorArgumentResolveOperation;
			if (constructorArgumentResolveOperation != null && constructorArgumentResolveOperation.ParameterName == this.parameterName)
			{
				return this.parameterValue.GetResolverPolicy(dependencyType);
			}
			return null;
		}

		// Token: 0x04000022 RID: 34
		private readonly string parameterName;

		// Token: 0x04000023 RID: 35
		private readonly InjectionParameterValue parameterValue;
	}
}

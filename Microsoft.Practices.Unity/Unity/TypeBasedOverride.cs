using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000079 RID: 121
	public class TypeBasedOverride : ResolverOverride
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00007432 File Offset: 0x00005632
		public TypeBasedOverride(Type targetType, ResolverOverride innerOverride)
		{
			Guard.ArgumentNotNull(targetType, "targetType");
			Guard.ArgumentNotNull(innerOverride, "innerOverride");
			this.targetType = targetType;
			this.innerOverride = innerOverride;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007460 File Offset: 0x00005660
		public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
		{
			Guard.ArgumentNotNull(context, "context");
			BuildOperation buildOperation = context.CurrentOperation as BuildOperation;
			if (buildOperation != null && buildOperation.TypeBeingConstructed == this.targetType)
			{
				return this.innerOverride.GetResolver(context, dependencyType);
			}
			return null;
		}

		// Token: 0x04000075 RID: 117
		private readonly Type targetType;

		// Token: 0x04000076 RID: 118
		private readonly ResolverOverride innerOverride;
	}
}

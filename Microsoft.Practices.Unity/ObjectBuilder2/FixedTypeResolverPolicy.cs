using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200006D RID: 109
	public class FixedTypeResolverPolicy : IDependencyResolverPolicy, IBuilderPolicy
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00006A8A File Offset: 0x00004C8A
		public FixedTypeResolverPolicy(Type typeToBuild)
		{
			this.keyToBuild = new NamedTypeBuildKey(typeToBuild);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006A9E File Offset: 0x00004C9E
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done via Guard class")]
		public object Resolve(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			return context.NewBuildUp(this.keyToBuild);
		}

		// Token: 0x0400006A RID: 106
		private readonly NamedTypeBuildKey keyToBuild;
	}
}

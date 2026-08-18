using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000048 RID: 72
	public interface IBuildKeyMappingPolicy : IBuilderPolicy
	{
		// Token: 0x06000139 RID: 313
		NamedTypeBuildKey Map(NamedTypeBuildKey buildKey, IBuilderContext context);
	}
}

using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000027 RID: 39
	public interface IDependencyResolverPolicy : IBuilderPolicy
	{
		// Token: 0x0600008D RID: 141
		object Resolve(IBuilderContext context);
	}
}

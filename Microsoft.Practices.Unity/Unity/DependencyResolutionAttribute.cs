using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000008 RID: 8
	public abstract class DependencyResolutionAttribute : Attribute
	{
		// Token: 0x06000013 RID: 19
		public abstract IDependencyResolverPolicy CreateResolver(Type typeToResolve);
	}
}

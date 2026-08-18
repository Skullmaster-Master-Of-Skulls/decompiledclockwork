using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200000D RID: 13
	public abstract class ResolverOverride
	{
		// Token: 0x0600001F RID: 31
		public abstract IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType);

		// Token: 0x06000020 RID: 32 RVA: 0x000025E0 File Offset: 0x000007E0
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public ResolverOverride OnType<T>()
		{
			return new TypeBasedOverride<T>(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025E8 File Offset: 0x000007E8
		public ResolverOverride OnType(Type typeToOverride)
		{
			return new TypeBasedOverride(typeToOverride, this);
		}
	}
}

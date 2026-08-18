using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class OptionalDependencyAttribute : DependencyResolutionAttribute
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000025B2 File Offset: 0x000007B2
		public OptionalDependencyAttribute() : this(null)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025BB File Offset: 0x000007BB
		public OptionalDependencyAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025CA File Offset: 0x000007CA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025D2 File Offset: 0x000007D2
		public override IDependencyResolverPolicy CreateResolver(Type typeToResolve)
		{
			return new OptionalDependencyResolverPolicy(typeToResolve, this.name);
		}

		// Token: 0x04000008 RID: 8
		private readonly string name;
	}
}

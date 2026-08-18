using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class DependencyAttribute : DependencyResolutionAttribute
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002584 File Offset: 0x00000784
		public DependencyAttribute() : this(null)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000258D File Offset: 0x0000078D
		public DependencyAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000259C File Offset: 0x0000079C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025A4 File Offset: 0x000007A4
		public override IDependencyResolverPolicy CreateResolver(Type typeToResolve)
		{
			return new NamedTypeDependencyResolverPolicy(typeToResolve, this.name);
		}

		// Token: 0x04000007 RID: 7
		private readonly string name;
	}
}

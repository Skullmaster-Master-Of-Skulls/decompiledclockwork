using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200000E RID: 14
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not really a collection, only implement IEnumerable to get convenient initialization syntax.")]
	public class CompositeResolverOverride : ResolverOverride, IEnumerable<ResolverOverride>, IEnumerable
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000025F9 File Offset: 0x000007F9
		public void Add(ResolverOverride newOverride)
		{
			this.overrides.Add(newOverride);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002607 File Offset: 0x00000807
		public void AddRange(IEnumerable<ResolverOverride> newOverrides)
		{
			this.overrides.AddRange(newOverrides);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002615 File Offset: 0x00000815
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000261D File Offset: 0x0000081D
		public IEnumerator<ResolverOverride> GetEnumerator()
		{
			return this.overrides.GetEnumerator();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002630 File Offset: 0x00000830
		public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
		{
			for (int i = this.overrides.Count<ResolverOverride>() - 1; i >= 0; i--)
			{
				IDependencyResolverPolicy resolver = this.overrides[i].GetResolver(context, dependencyType);
				if (resolver != null)
				{
					return resolver;
				}
			}
			return null;
		}

		// Token: 0x04000009 RID: 9
		private readonly List<ResolverOverride> overrides = new List<ResolverOverride>();
	}
}

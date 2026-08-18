using System;
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000051 RID: 81
	public class SelectedMemberWithParameters
	{
		// Token: 0x06000150 RID: 336 RVA: 0x000047C5 File Offset: 0x000029C5
		public void AddParameterResolver(IDependencyResolverPolicy newResolver)
		{
			this.parameterResolvers.Add(newResolver);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000047D3 File Offset: 0x000029D3
		public IDependencyResolverPolicy[] GetParameterResolvers()
		{
			return this.parameterResolvers.ToArray();
		}

		// Token: 0x04000044 RID: 68
		private readonly List<IDependencyResolverPolicy> parameterResolvers = new List<IDependencyResolverPolicy>();
	}
}

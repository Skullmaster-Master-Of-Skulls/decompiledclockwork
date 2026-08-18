using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000080 RID: 128
	public class GenericParameter : GenericParameterBase
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00007A82 File Offset: 0x00005C82
		public GenericParameter(string genericParameterName) : base(genericParameterName)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007A8B File Offset: 0x00005C8B
		public GenericParameter(string genericParameterName, string resolutionKey) : base(genericParameterName, resolutionKey)
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007A95 File Offset: 0x00005C95
		protected override IDependencyResolverPolicy DoGetResolverPolicy(Type typeToResolve, string resolutionKey)
		{
			return new NamedTypeDependencyResolverPolicy(typeToResolve, resolutionKey);
		}
	}
}

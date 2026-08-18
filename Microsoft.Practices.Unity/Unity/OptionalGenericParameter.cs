using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000017 RID: 23
	public class OptionalGenericParameter : GenericParameterBase
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002E1E File Offset: 0x0000101E
		public OptionalGenericParameter(string genericParameterName) : base(genericParameterName)
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E27 File Offset: 0x00001027
		public OptionalGenericParameter(string genericParameterName, string resolutionKey) : base(genericParameterName, resolutionKey)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E31 File Offset: 0x00001031
		protected override IDependencyResolverPolicy DoGetResolverPolicy(Type typeToResolve, string resolutionKey)
		{
			return new OptionalDependencyResolverPolicy(typeToResolve, resolutionKey);
		}
	}
}

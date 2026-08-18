using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200007F RID: 127
	internal sealed class TypeUsageEqualityComparer : IEqualityComparer<TypeUsage>
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x00002050 File Offset: 0x00000250
		private TypeUsageEqualityComparer()
		{
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0003345D File Offset: 0x0003165D
		public bool Equals(TypeUsage x, TypeUsage y)
		{
			return x != null && y != null && TypeUsageEqualityComparer.Equals(x.EdmType, y.EdmType);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00033478 File Offset: 0x00031678
		public int GetHashCode(TypeUsage obj)
		{
			return obj.EdmType.Identity.GetHashCode();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0003348A File Offset: 0x0003168A
		internal static bool Equals(EdmType x, EdmType y)
		{
			return x.Identity.Equals(y.Identity);
		}

		// Token: 0x0400087F RID: 2175
		internal static readonly TypeUsageEqualityComparer Instance = new TypeUsageEqualityComparer();
	}
}

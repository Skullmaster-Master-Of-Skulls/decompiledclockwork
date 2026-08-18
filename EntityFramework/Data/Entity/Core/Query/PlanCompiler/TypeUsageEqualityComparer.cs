using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A8 RID: 1704
	internal sealed class TypeUsageEqualityComparer : IEqualityComparer<TypeUsage>
	{
		// Token: 0x06004386 RID: 17286 RVA: 0x00140813 File Offset: 0x0013EA13
		private TypeUsageEqualityComparer()
		{
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x0014081B File Offset: 0x0013EA1B
		public bool Equals(TypeUsage x, TypeUsage y)
		{
			return x != null && y != null && TypeUsageEqualityComparer.Equals(x.EdmType, y.EdmType);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x00140836 File Offset: 0x0013EA36
		public int GetHashCode(TypeUsage obj)
		{
			return obj.EdmType.Identity.GetHashCode();
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x00140848 File Offset: 0x0013EA48
		internal static bool Equals(EdmType x, EdmType y)
		{
			return x.Identity.Equals(y.Identity);
		}

		// Token: 0x04001909 RID: 6409
		internal static readonly TypeUsageEqualityComparer Instance = new TypeUsageEqualityComparer();
	}
}

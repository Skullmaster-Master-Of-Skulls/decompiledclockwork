using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000650 RID: 1616
	internal class AllPropertyRef : PropertyRef
	{
		// Token: 0x06003F2E RID: 16174 RVA: 0x00120F6A File Offset: 0x0011F16A
		private AllPropertyRef()
		{
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x00120F72 File Offset: 0x0011F172
		internal override PropertyRef CreateNestedPropertyRef(PropertyRef p)
		{
			return p;
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x00120F75 File Offset: 0x0011F175
		public override string ToString()
		{
			return "ALL";
		}

		// Token: 0x04001795 RID: 6037
		internal static AllPropertyRef Instance = new AllPropertyRef();
	}
}

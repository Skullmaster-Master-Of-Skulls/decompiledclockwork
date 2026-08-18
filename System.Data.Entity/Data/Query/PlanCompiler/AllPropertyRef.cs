using System;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000069 RID: 105
	internal class AllPropertyRef : PropertyRef
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		private AllPropertyRef()
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00002391 File Offset: 0x00000591
		internal override PropertyRef CreateNestedPropertyRef(PropertyRef p)
		{
			return p;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002CD02 File Offset: 0x0002AF02
		public override string ToString()
		{
			return "ALL";
		}

		// Token: 0x040007FD RID: 2045
		internal static AllPropertyRef Instance = new AllPropertyRef();
	}
}

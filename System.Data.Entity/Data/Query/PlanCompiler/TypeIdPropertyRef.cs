using System;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000065 RID: 101
	internal class TypeIdPropertyRef : PropertyRef
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		private TypeIdPropertyRef()
		{
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002CC00 File Offset: 0x0002AE00
		public override string ToString()
		{
			return "TYPEID";
		}

		// Token: 0x040007F8 RID: 2040
		internal static TypeIdPropertyRef Instance = new TypeIdPropertyRef();
	}
}

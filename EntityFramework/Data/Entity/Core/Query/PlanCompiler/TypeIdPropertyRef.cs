using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A7 RID: 1703
	internal class TypeIdPropertyRef : PropertyRef
	{
		// Token: 0x06004383 RID: 17283 RVA: 0x001407F8 File Offset: 0x0013E9F8
		private TypeIdPropertyRef()
		{
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00140800 File Offset: 0x0013EA00
		public override string ToString()
		{
			return "TYPEID";
		}

		// Token: 0x04001908 RID: 6408
		internal static TypeIdPropertyRef Instance = new TypeIdPropertyRef();
	}
}

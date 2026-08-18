using System;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000067 RID: 103
	internal class EntitySetIdPropertyRef : PropertyRef
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		private EntitySetIdPropertyRef()
		{
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002CC2D File Offset: 0x0002AE2D
		public override string ToString()
		{
			return "ENTITYSETID";
		}

		// Token: 0x040007FA RID: 2042
		internal static EntitySetIdPropertyRef Instance = new EntitySetIdPropertyRef();
	}
}

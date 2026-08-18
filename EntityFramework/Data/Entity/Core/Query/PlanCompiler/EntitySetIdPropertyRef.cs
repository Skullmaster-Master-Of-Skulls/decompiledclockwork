using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200066B RID: 1643
	internal class EntitySetIdPropertyRef : PropertyRef
	{
		// Token: 0x06004034 RID: 16436 RVA: 0x00125E82 File Offset: 0x00124082
		private EntitySetIdPropertyRef()
		{
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x00125E8A File Offset: 0x0012408A
		public override string ToString()
		{
			return "ENTITYSETID";
		}

		// Token: 0x040017E4 RID: 6116
		internal static EntitySetIdPropertyRef Instance = new EntitySetIdPropertyRef();
	}
}

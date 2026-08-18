using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200043A RID: 1082
	internal abstract class TileQueryProcessor<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x060027A6 RID: 10150
		internal abstract T_Query Intersect(T_Query arg1, T_Query arg2);

		// Token: 0x060027A7 RID: 10151
		internal abstract T_Query Difference(T_Query arg1, T_Query arg2);

		// Token: 0x060027A8 RID: 10152
		internal abstract T_Query Union(T_Query arg1, T_Query arg2);

		// Token: 0x060027A9 RID: 10153
		internal abstract bool IsSatisfiable(T_Query query);

		// Token: 0x060027AA RID: 10154
		internal abstract T_Query CreateDerivedViewBySelectingConstantAttributes(T_Query query);
	}
}

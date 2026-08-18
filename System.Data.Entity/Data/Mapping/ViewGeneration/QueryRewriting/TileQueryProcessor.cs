using System;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000293 RID: 659
	internal abstract class TileQueryProcessor<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x0600275E RID: 10078
		internal abstract T_Query Intersect(T_Query arg1, T_Query arg2);

		// Token: 0x0600275F RID: 10079
		internal abstract T_Query Difference(T_Query arg1, T_Query arg2);

		// Token: 0x06002760 RID: 10080
		internal abstract T_Query Union(T_Query arg1, T_Query arg2);

		// Token: 0x06002761 RID: 10081
		internal abstract bool IsSatisfiable(T_Query query);

		// Token: 0x06002762 RID: 10082
		internal abstract T_Query CreateDerivedViewBySelectingConstantAttributes(T_Query query);
	}
}

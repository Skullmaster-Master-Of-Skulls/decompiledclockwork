using System;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200028E RID: 654
	internal abstract class TileProcessor<T_Tile>
	{
		// Token: 0x06002732 RID: 10034
		internal abstract bool IsEmpty(T_Tile tile);

		// Token: 0x06002733 RID: 10035
		internal abstract T_Tile Union(T_Tile a, T_Tile b);

		// Token: 0x06002734 RID: 10036
		internal abstract T_Tile Join(T_Tile a, T_Tile b);

		// Token: 0x06002735 RID: 10037
		internal abstract T_Tile AntiSemiJoin(T_Tile a, T_Tile b);

		// Token: 0x06002736 RID: 10038
		internal abstract T_Tile GetArg1(T_Tile tile);

		// Token: 0x06002737 RID: 10039
		internal abstract T_Tile GetArg2(T_Tile tile);

		// Token: 0x06002738 RID: 10040
		internal abstract TileOpKind GetOpKind(T_Tile tile);
	}
}

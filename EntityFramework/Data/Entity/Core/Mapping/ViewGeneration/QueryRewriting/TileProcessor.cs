using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000435 RID: 1077
	internal abstract class TileProcessor<T_Tile>
	{
		// Token: 0x06002783 RID: 10115
		internal abstract bool IsEmpty(T_Tile tile);

		// Token: 0x06002784 RID: 10116
		internal abstract T_Tile Union(T_Tile a, T_Tile b);

		// Token: 0x06002785 RID: 10117
		internal abstract T_Tile Join(T_Tile a, T_Tile b);

		// Token: 0x06002786 RID: 10118
		internal abstract T_Tile AntiSemiJoin(T_Tile a, T_Tile b);

		// Token: 0x06002787 RID: 10119
		internal abstract T_Tile GetArg1(T_Tile tile);

		// Token: 0x06002788 RID: 10120
		internal abstract T_Tile GetArg2(T_Tile tile);

		// Token: 0x06002789 RID: 10121
		internal abstract TileOpKind GetOpKind(T_Tile tile);
	}
}

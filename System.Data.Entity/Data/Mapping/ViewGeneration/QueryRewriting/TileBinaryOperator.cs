using System;
using System.Globalization;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000297 RID: 663
	internal class TileBinaryOperator<T_Query> : Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x06002780 RID: 10112 RVA: 0x00099AA2 File Offset: 0x00097CA2
		public TileBinaryOperator(Tile<T_Query> arg1, Tile<T_Query> arg2, TileOpKind opKind, T_Query query) : base(opKind, query)
		{
			this.m_arg1 = arg1;
			this.m_arg2 = arg2;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x00099ABB File Offset: 0x00097CBB
		public override Tile<T_Query> Arg1
		{
			get
			{
				return this.m_arg1;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x00099AC3 File Offset: 0x00097CC3
		public override Tile<T_Query> Arg2
		{
			get
			{
				return this.m_arg2;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x00099ACC File Offset: 0x00097CCC
		public override string Description
		{
			get
			{
				string format = null;
				switch (base.OpKind)
				{
				case TileOpKind.Union:
					format = "({0} | {1})";
					break;
				case TileOpKind.Join:
					format = "({0} & {1})";
					break;
				case TileOpKind.AntiSemiJoin:
					format = "({0} - {1})";
					break;
				}
				return string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					this.Arg1.Description,
					this.Arg2.Description
				});
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00099B3C File Offset: 0x00097D3C
		internal override Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile)
		{
			Tile<T_Query> tile = this.Arg1.Replace(oldTile, newTile);
			Tile<T_Query> tile2 = this.Arg2.Replace(oldTile, newTile);
			if (tile != this.Arg1 || tile2 != this.Arg2)
			{
				return new TileBinaryOperator<T_Query>(tile, tile2, base.OpKind, base.Query);
			}
			return this;
		}

		// Token: 0x0400121D RID: 4637
		private readonly Tile<T_Query> m_arg1;

		// Token: 0x0400121E RID: 4638
		private readonly Tile<T_Query> m_arg2;
	}
}

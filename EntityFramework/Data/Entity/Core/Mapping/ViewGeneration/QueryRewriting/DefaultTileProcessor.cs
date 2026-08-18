using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000436 RID: 1078
	internal class DefaultTileProcessor<T_Query> : TileProcessor<Tile<T_Query>> where T_Query : ITileQuery
	{
		// Token: 0x0600278B RID: 10123 RVA: 0x000BFF8D File Offset: 0x000BE18D
		internal DefaultTileProcessor(TileQueryProcessor<T_Query> tileQueryProcessor)
		{
			this._tileQueryProcessor = tileQueryProcessor;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x000BFF9C File Offset: 0x000BE19C
		internal TileQueryProcessor<T_Query> QueryProcessor
		{
			get
			{
				return this._tileQueryProcessor;
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000BFFA4 File Offset: 0x000BE1A4
		internal override bool IsEmpty(Tile<T_Query> tile)
		{
			return !this._tileQueryProcessor.IsSatisfiable(tile.Query);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000BFFBA File Offset: 0x000BE1BA
		internal override Tile<T_Query> Union(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.Union, this._tileQueryProcessor.Union(arg1.Query, arg2.Query));
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000BFFDB File Offset: 0x000BE1DB
		internal override Tile<T_Query> Join(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.Join, this._tileQueryProcessor.Intersect(arg1.Query, arg2.Query));
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000BFFFC File Offset: 0x000BE1FC
		internal override Tile<T_Query> AntiSemiJoin(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.AntiSemiJoin, this._tileQueryProcessor.Difference(arg1.Query, arg2.Query));
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000C001D File Offset: 0x000BE21D
		internal override Tile<T_Query> GetArg1(Tile<T_Query> tile)
		{
			return tile.Arg1;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000C0025 File Offset: 0x000BE225
		internal override Tile<T_Query> GetArg2(Tile<T_Query> tile)
		{
			return tile.Arg2;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000C002D File Offset: 0x000BE22D
		internal override TileOpKind GetOpKind(Tile<T_Query> tile)
		{
			return tile.OpKind;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000C0035 File Offset: 0x000BE235
		internal bool IsContainedIn(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return this.IsEmpty(this.AntiSemiJoin(arg1, arg2));
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000C0045 File Offset: 0x000BE245
		internal bool IsEquivalentTo(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return this.IsContainedIn(arg1, arg2) && this.IsContainedIn(arg2, arg1);
		}

		// Token: 0x04000EF1 RID: 3825
		private readonly TileQueryProcessor<T_Query> _tileQueryProcessor;
	}
}

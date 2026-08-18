using System;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000294 RID: 660
	internal class DefaultTileProcessor<T_Query> : TileProcessor<Tile<T_Query>> where T_Query : ITileQuery
	{
		// Token: 0x06002764 RID: 10084 RVA: 0x000998CB File Offset: 0x00097ACB
		internal DefaultTileProcessor(TileQueryProcessor<T_Query> tileQueryProcessor)
		{
			this._tileQueryProcessor = tileQueryProcessor;
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x000998DA File Offset: 0x00097ADA
		internal TileQueryProcessor<T_Query> QueryProcessor
		{
			get
			{
				return this._tileQueryProcessor;
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000998E2 File Offset: 0x00097AE2
		internal override bool IsEmpty(Tile<T_Query> tile)
		{
			return !this._tileQueryProcessor.IsSatisfiable(tile.Query);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000998F8 File Offset: 0x00097AF8
		internal override Tile<T_Query> Union(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.Union, this._tileQueryProcessor.Union(arg1.Query, arg2.Query));
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00099919 File Offset: 0x00097B19
		internal override Tile<T_Query> Join(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.Join, this._tileQueryProcessor.Intersect(arg1.Query, arg2.Query));
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0009993A File Offset: 0x00097B3A
		internal override Tile<T_Query> AntiSemiJoin(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return new TileBinaryOperator<T_Query>(arg1, arg2, TileOpKind.AntiSemiJoin, this._tileQueryProcessor.Difference(arg1.Query, arg2.Query));
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0009995B File Offset: 0x00097B5B
		internal override Tile<T_Query> GetArg1(Tile<T_Query> tile)
		{
			return tile.Arg1;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00099963 File Offset: 0x00097B63
		internal override Tile<T_Query> GetArg2(Tile<T_Query> tile)
		{
			return tile.Arg2;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x0009996B File Offset: 0x00097B6B
		internal override TileOpKind GetOpKind(Tile<T_Query> tile)
		{
			return tile.OpKind;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00099973 File Offset: 0x00097B73
		internal bool IsContainedIn(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return this.IsEmpty(this.AntiSemiJoin(arg1, arg2));
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00099983 File Offset: 0x00097B83
		internal bool IsEquivalentTo(Tile<T_Query> arg1, Tile<T_Query> arg2)
		{
			return this.IsContainedIn(arg1, arg2) && this.IsContainedIn(arg2, arg1);
		}

		// Token: 0x0400121A RID: 4634
		private readonly TileQueryProcessor<T_Query> _tileQueryProcessor;
	}
}

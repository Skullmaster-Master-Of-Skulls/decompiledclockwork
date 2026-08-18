using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000453 RID: 1107
	internal class TileNamed<T_Query> : Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x000C5C10 File Offset: 0x000C3E10
		public TileNamed(T_Query namedQuery) : base(TileOpKind.Named, namedQuery)
		{
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000C5C1A File Offset: 0x000C3E1A
		public T_Query NamedQuery
		{
			get
			{
				return base.Query;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x000C5C22 File Offset: 0x000C3E22
		public override Tile<T_Query> Arg1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000C5C25 File Offset: 0x000C3E25
		public override Tile<T_Query> Arg2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000C5C28 File Offset: 0x000C3E28
		public override string Description
		{
			get
			{
				T_Query query = base.Query;
				return query.Description;
			}
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000C5C4C File Offset: 0x000C3E4C
		public override string ToString()
		{
			T_Query query = base.Query;
			return query.ToString();
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000C5C6D File Offset: 0x000C3E6D
		internal override Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile)
		{
			if (this != oldTile)
			{
				return this;
			}
			return newTile;
		}
	}
}

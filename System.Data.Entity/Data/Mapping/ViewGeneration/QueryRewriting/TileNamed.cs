using System;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000296 RID: 662
	internal class TileNamed<T_Query> : Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x06002779 RID: 10105 RVA: 0x00099A3F File Offset: 0x00097C3F
		public TileNamed(T_Query namedQuery) : base(TileOpKind.Named, namedQuery)
		{
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x00099A49 File Offset: 0x00097C49
		public T_Query NamedQuery
		{
			get
			{
				return base.Query;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x00006174 File Offset: 0x00004374
		public override Tile<T_Query> Arg1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x00006174 File Offset: 0x00004374
		public override Tile<T_Query> Arg2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x00099A54 File Offset: 0x00097C54
		public override string Description
		{
			get
			{
				T_Query query = base.Query;
				return query.Description;
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00099A78 File Offset: 0x00097C78
		public override string ToString()
		{
			T_Query query = base.Query;
			return query.ToString();
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00099A99 File Offset: 0x00097C99
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

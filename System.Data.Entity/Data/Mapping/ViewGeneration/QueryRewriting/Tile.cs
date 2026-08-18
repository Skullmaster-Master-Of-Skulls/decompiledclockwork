using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000295 RID: 661
	internal abstract class Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x00099999 File Offset: 0x00097B99
		protected Tile(TileOpKind opKind, T_Query query)
		{
			this.m_opKind = opKind;
			this.m_query = query;
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000999AF File Offset: 0x00097BAF
		public T_Query Query
		{
			get
			{
				return this.m_query;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002771 RID: 10097
		public abstract string Description { get; }

		// Token: 0x06002772 RID: 10098 RVA: 0x000999B7 File Offset: 0x00097BB7
		public IEnumerable<T_Query> GetNamedQueries()
		{
			return Tile<T_Query>.GetNamedQueries(this);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000999BF File Offset: 0x00097BBF
		private static IEnumerable<T_Query> GetNamedQueries(Tile<T_Query> rewriting)
		{
			if (rewriting != null)
			{
				if (rewriting.OpKind == TileOpKind.Named)
				{
					yield return ((TileNamed<T_Query>)rewriting).NamedQuery;
				}
				else
				{
					foreach (T_Query t_Query in Tile<T_Query>.GetNamedQueries(rewriting.Arg1))
					{
						yield return t_Query;
					}
					IEnumerator<T_Query> enumerator = null;
					foreach (T_Query t_Query2 in Tile<T_Query>.GetNamedQueries(rewriting.Arg2))
					{
						yield return t_Query2;
					}
					enumerator = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000999D0 File Offset: 0x00097BD0
		public override string ToString()
		{
			string description = this.Description;
			if (description != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}: [{1}]", new object[]
				{
					this.Description,
					this.Query
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
			{
				this.Query
			});
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002775 RID: 10101
		public abstract Tile<T_Query> Arg1 { get; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002776 RID: 10102
		public abstract Tile<T_Query> Arg2 { get; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x00099A37 File Offset: 0x00097C37
		public TileOpKind OpKind
		{
			get
			{
				return this.m_opKind;
			}
		}

		// Token: 0x06002778 RID: 10104
		internal abstract Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile);

		// Token: 0x0400121B RID: 4635
		private readonly T_Query m_query;

		// Token: 0x0400121C RID: 4636
		private readonly TileOpKind m_opKind;
	}
}

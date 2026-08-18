using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000451 RID: 1105
	internal abstract class Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x0600289E RID: 10398 RVA: 0x000C57C6 File Offset: 0x000C39C6
		protected Tile(TileOpKind opKind, T_Query query)
		{
			this.m_opKind = opKind;
			this.m_query = query;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x000C57DC File Offset: 0x000C39DC
		public T_Query Query
		{
			get
			{
				return this.m_query;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060028A0 RID: 10400
		public abstract string Description { get; }

		// Token: 0x060028A1 RID: 10401 RVA: 0x000C57E4 File Offset: 0x000C39E4
		public IEnumerable<T_Query> GetNamedQueries()
		{
			return Tile<T_Query>.GetNamedQueries(this);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000C5A94 File Offset: 0x000C3C94
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
					foreach (T_Query query in Tile<T_Query>.GetNamedQueries(rewriting.Arg1))
					{
						yield return query;
					}
					foreach (T_Query query2 in Tile<T_Query>.GetNamedQueries(rewriting.Arg2))
					{
						yield return query2;
					}
				}
			}
			yield break;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000C5AB4 File Offset: 0x000C3CB4
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

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060028A4 RID: 10404
		public abstract Tile<T_Query> Arg1 { get; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060028A5 RID: 10405
		public abstract Tile<T_Query> Arg2 { get; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x000C5B1F File Offset: 0x000C3D1F
		public TileOpKind OpKind
		{
			get
			{
				return this.m_opKind;
			}
		}

		// Token: 0x060028A7 RID: 10407
		internal abstract Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile);

		// Token: 0x04000F37 RID: 3895
		private readonly T_Query m_query;

		// Token: 0x04000F38 RID: 3896
		private readonly TileOpKind m_opKind;
	}
}

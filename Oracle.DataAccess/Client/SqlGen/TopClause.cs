using System;
using System.Globalization;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x0200004A RID: 74
	internal class TopClause : ISqlFragment
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00028562 File Offset: 0x00027562
		internal bool WithTies
		{
			get
			{
				return this.withTies;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0002856A File Offset: 0x0002756A
		internal ISqlFragment TopCount
		{
			get
			{
				return this.topCount;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00028572 File Offset: 0x00027572
		internal TopClause(ISqlFragment topCount, bool withTies)
		{
			this.topCount = topCount;
			this.withTies = withTies;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00028588 File Offset: 0x00027588
		internal TopClause(int topCount, bool withTies)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(topCount.ToString(CultureInfo.InvariantCulture));
			this.topCount = sqlBuilder;
			this.withTies = withTies;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000285C4 File Offset: 0x000275C4
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("ROWNUM <= (");
			this.TopCount.WriteSql(writer, sqlGenerator);
			writer.Write(")");
			writer.Write(" ");
			if (this.WithTies)
			{
				writer.Write("WITH TIES ");
			}
		}

		// Token: 0x0400025B RID: 603
		private ISqlFragment topCount;

		// Token: 0x0400025C RID: 604
		private bool withTies;
	}
}

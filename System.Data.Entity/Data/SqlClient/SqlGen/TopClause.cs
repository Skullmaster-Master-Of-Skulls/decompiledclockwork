using System;
using System.Globalization;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000038 RID: 56
	internal class TopClause : ISqlFragment
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001715C File Offset: 0x0001535C
		internal bool WithTies
		{
			get
			{
				return this.withTies;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00017164 File Offset: 0x00015364
		internal ISqlFragment TopCount
		{
			get
			{
				return this.topCount;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001716C File Offset: 0x0001536C
		internal TopClause(ISqlFragment topCount, bool withTies)
		{
			this.topCount = topCount;
			this.withTies = withTies;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017184 File Offset: 0x00015384
		internal TopClause(int topCount, bool withTies)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(topCount.ToString(CultureInfo.InvariantCulture));
			this.topCount = sqlBuilder;
			this.withTies = withTies;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000171C0 File Offset: 0x000153C0
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("TOP ");
			if (sqlGenerator.SqlVersion != SqlVersion.Sql8)
			{
				writer.Write("(");
			}
			this.TopCount.WriteSql(writer, sqlGenerator);
			if (sqlGenerator.SqlVersion != SqlVersion.Sql8)
			{
				writer.Write(")");
			}
			writer.Write(" ");
			if (this.WithTies)
			{
				writer.Write("WITH TIES ");
			}
		}

		// Token: 0x0400073A RID: 1850
		private ISqlFragment topCount;

		// Token: 0x0400073B RID: 1851
		private bool withTies;
	}
}

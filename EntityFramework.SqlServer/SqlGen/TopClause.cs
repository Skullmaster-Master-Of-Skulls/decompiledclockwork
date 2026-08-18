using System;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000041 RID: 65
	internal class TopClause : ISqlFragment
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001466F File Offset: 0x0001286F
		internal bool WithTies
		{
			get
			{
				return this.withTies;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00014677 File Offset: 0x00012877
		internal ISqlFragment TopCount
		{
			get
			{
				return this.topCount;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001467F File Offset: 0x0001287F
		internal TopClause(ISqlFragment topCount, bool withTies)
		{
			this.topCount = topCount;
			this.withTies = withTies;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014698 File Offset: 0x00012898
		internal TopClause(int topCount, bool withTies)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(topCount.ToString(CultureInfo.InvariantCulture));
			this.topCount = sqlBuilder;
			this.withTies = withTies;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000146D4 File Offset: 0x000128D4
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

		// Token: 0x040000F7 RID: 247
		private readonly ISqlFragment topCount;

		// Token: 0x040000F8 RID: 248
		private readonly bool withTies;
	}
}

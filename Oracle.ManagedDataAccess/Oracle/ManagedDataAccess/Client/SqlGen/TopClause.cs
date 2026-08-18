using System;
using System.Globalization;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F8 RID: 248
	internal class TopClause : ISqlFragment
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x000754C0 File Offset: 0x000736C0
		internal bool WithTies
		{
			get
			{
				return this.withTies;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x000754C8 File Offset: 0x000736C8
		internal ISqlFragment TopCount
		{
			get
			{
				return this.topCount;
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000754D0 File Offset: 0x000736D0
		internal TopClause(ISqlFragment topCount, bool withTies)
		{
			this.topCount = topCount;
			this.withTies = withTies;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000754E8 File Offset: 0x000736E8
		internal TopClause(int topCount, bool withTies)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(topCount.ToString(CultureInfo.InvariantCulture));
			this.topCount = sqlBuilder;
			this.withTies = withTies;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00075524 File Offset: 0x00073724
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

		// Token: 0x04000C84 RID: 3204
		private ISqlFragment topCount;

		// Token: 0x04000C85 RID: 3205
		private bool withTies;
	}
}

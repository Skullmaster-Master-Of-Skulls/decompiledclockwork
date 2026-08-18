using System;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000023 RID: 35
	internal class SkipClause : ISqlFragment
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007B3E File Offset: 0x00005D3E
		internal ISqlFragment SkipCount
		{
			get
			{
				return this.skipCount;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007B46 File Offset: 0x00005D46
		internal SkipClause(ISqlFragment skipCount)
		{
			this.skipCount = skipCount;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007B58 File Offset: 0x00005D58
		internal SkipClause(int skipCount)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(skipCount.ToString(CultureInfo.InvariantCulture));
			this.skipCount = sqlBuilder;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007B8A File Offset: 0x00005D8A
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("OFFSET ");
			this.SkipCount.WriteSql(writer, sqlGenerator);
			writer.Write(" ROWS ");
		}

		// Token: 0x04000070 RID: 112
		private readonly ISqlFragment skipCount;
	}
}

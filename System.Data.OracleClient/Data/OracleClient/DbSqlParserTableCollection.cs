using System;
using System.Collections;

namespace System.Data.OracleClient
{
	// Token: 0x02000017 RID: 23
	internal sealed class DbSqlParserTableCollection : CollectionBase
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00057AB4 File Offset: 0x00056EB4
		private Type ItemType
		{
			get
			{
				return typeof(DbSqlParserTable);
			}
		}

		// Token: 0x17000023 RID: 35
		internal DbSqlParserTable this[int i]
		{
			get
			{
				return (DbSqlParserTable)base.InnerList[i];
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00057AF4 File Offset: 0x00056EF4
		internal DbSqlParserTable Add(DbSqlParserTable value)
		{
			this.OnValidate(value);
			base.InnerList.Add(value);
			return value;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00057B24 File Offset: 0x00056F24
		internal DbSqlParserTable Add(string databaseName, string schemaName, string tableName, string correlationName)
		{
			DbSqlParserTable value = new DbSqlParserTable(databaseName, schemaName, tableName, correlationName);
			return this.Add(value);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00057B44 File Offset: 0x00056F44
		protected override void OnValidate(object value)
		{
		}
	}
}

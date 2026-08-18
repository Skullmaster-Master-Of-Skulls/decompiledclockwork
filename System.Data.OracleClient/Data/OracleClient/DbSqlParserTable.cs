using System;

namespace System.Data.OracleClient
{
	// Token: 0x02000016 RID: 22
	internal sealed class DbSqlParserTable
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00057954 File Offset: 0x00056D54
		internal DbSqlParserTable(string databaseName, string schemaName, string tableName, string correlationName)
		{
			this._databaseName = databaseName;
			this._schemaName = schemaName;
			this._tableName = tableName;
			this._correlationName = correlationName;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00057984 File Offset: 0x00056D84
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000579B4 File Offset: 0x00056DB4
		internal DbSqlParserColumnCollection Columns
		{
			get
			{
				if (this._columns == null)
				{
					this._columns = new DbSqlParserColumnCollection();
				}
				return this._columns;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!typeof(DbSqlParserColumnCollection).IsInstanceOfType(value))
				{
					throw new InvalidCastException("value");
				}
				this._columns = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000579F4 File Offset: 0x00056DF4
		internal string CorrelationName
		{
			get
			{
				if (this._correlationName != null)
				{
					return this._correlationName;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00057A24 File Offset: 0x00056E24
		internal string DatabaseName
		{
			get
			{
				if (this._databaseName != null)
				{
					return this._databaseName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00057A54 File Offset: 0x00056E54
		internal string SchemaName
		{
			get
			{
				if (this._schemaName != null)
				{
					return this._schemaName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00057A84 File Offset: 0x00056E84
		internal string TableName
		{
			get
			{
				if (this._tableName != null)
				{
					return this._tableName;
				}
				return string.Empty;
			}
		}

		// Token: 0x0400014F RID: 335
		private string _databaseName;

		// Token: 0x04000150 RID: 336
		private string _schemaName;

		// Token: 0x04000151 RID: 337
		private string _tableName;

		// Token: 0x04000152 RID: 338
		private string _correlationName;

		// Token: 0x04000153 RID: 339
		private DbSqlParserColumnCollection _columns;
	}
}

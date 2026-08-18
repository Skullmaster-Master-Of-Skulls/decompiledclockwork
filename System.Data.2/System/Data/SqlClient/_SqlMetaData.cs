using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021F RID: 543
	internal sealed class _SqlMetaData : SqlMetaDataPriv, ICloneable
	{
		// Token: 0x06002201 RID: 8705 RVA: 0x000EC6E0 File Offset: 0x000EBAE0
		internal _SqlMetaData(int ordinal)
		{
			this.ordinal = ordinal;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000EC6FC File Offset: 0x000EBAFC
		internal string serverName
		{
			get
			{
				return this.multiPartTableName.ServerName;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x000EC714 File Offset: 0x000EBB14
		internal string catalogName
		{
			get
			{
				return this.multiPartTableName.CatalogName;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x000EC72C File Offset: 0x000EBB2C
		internal string schemaName
		{
			get
			{
				return this.multiPartTableName.SchemaName;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x000EC744 File Offset: 0x000EBB44
		internal string tableName
		{
			get
			{
				return this.multiPartTableName.TableName;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000EC75C File Offset: 0x000EBB5C
		internal bool IsNewKatmaiDateTimeType
		{
			get
			{
				return SqlDbType.Date == this.type || SqlDbType.Time == this.type || SqlDbType.DateTime2 == this.type || SqlDbType.DateTimeOffset == this.type;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x000EC794 File Offset: 0x000EBB94
		internal bool IsLargeUdt
		{
			get
			{
				return this.type == SqlDbType.Udt && this.length == int.MaxValue;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000EC7BC File Offset: 0x000EBBBC
		public object Clone()
		{
			_SqlMetaData sqlMetaData = new _SqlMetaData(this.ordinal);
			sqlMetaData.CopyFrom(this);
			sqlMetaData.column = this.column;
			sqlMetaData.baseColumn = this.baseColumn;
			sqlMetaData.multiPartTableName = this.multiPartTableName;
			sqlMetaData.updatability = this.updatability;
			sqlMetaData.tableNum = this.tableNum;
			sqlMetaData.isDifferentName = this.isDifferentName;
			sqlMetaData.isKey = this.isKey;
			sqlMetaData.isHidden = this.isHidden;
			sqlMetaData.isExpression = this.isExpression;
			sqlMetaData.isIdentity = this.isIdentity;
			sqlMetaData.isColumnSet = this.isColumnSet;
			sqlMetaData.op = this.op;
			sqlMetaData.operand = this.operand;
			return sqlMetaData;
		}

		// Token: 0x0400145B RID: 5211
		internal string column;

		// Token: 0x0400145C RID: 5212
		internal string baseColumn;

		// Token: 0x0400145D RID: 5213
		internal MultiPartTableName multiPartTableName;

		// Token: 0x0400145E RID: 5214
		internal readonly int ordinal;

		// Token: 0x0400145F RID: 5215
		internal byte updatability;

		// Token: 0x04001460 RID: 5216
		internal byte tableNum;

		// Token: 0x04001461 RID: 5217
		internal bool isDifferentName;

		// Token: 0x04001462 RID: 5218
		internal bool isKey;

		// Token: 0x04001463 RID: 5219
		internal bool isHidden;

		// Token: 0x04001464 RID: 5220
		internal bool isExpression;

		// Token: 0x04001465 RID: 5221
		internal bool isIdentity;

		// Token: 0x04001466 RID: 5222
		internal bool isColumnSet;

		// Token: 0x04001467 RID: 5223
		internal byte op;

		// Token: 0x04001468 RID: 5224
		internal ushort operand;
	}
}

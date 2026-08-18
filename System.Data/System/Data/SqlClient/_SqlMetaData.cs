using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000329 RID: 809
	internal sealed class _SqlMetaData : SqlMetaDataPriv
	{
		// Token: 0x06002A5E RID: 10846 RVA: 0x002BE7C8 File Offset: 0x002BDBC8
		internal _SqlMetaData(int ordinal)
		{
			this.ordinal = ordinal;
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x002BE7E8 File Offset: 0x002BDBE8
		internal string serverName
		{
			get
			{
				return this.multiPartTableName.ServerName;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x002BE808 File Offset: 0x002BDC08
		internal string catalogName
		{
			get
			{
				return this.multiPartTableName.CatalogName;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x002BE828 File Offset: 0x002BDC28
		internal string schemaName
		{
			get
			{
				return this.multiPartTableName.SchemaName;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x002BE848 File Offset: 0x002BDC48
		internal string tableName
		{
			get
			{
				return this.multiPartTableName.TableName;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x002BE868 File Offset: 0x002BDC68
		internal bool IsNewKatmaiDateTimeType
		{
			get
			{
				return SqlDbType.Date == this.type || SqlDbType.Time == this.type || SqlDbType.DateTime2 == this.type || SqlDbType.DateTimeOffset == this.type;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x002BE8A8 File Offset: 0x002BDCA8
		internal bool IsLargeUdt
		{
			get
			{
				return this.type == SqlDbType.Udt && this.length == int.MaxValue;
			}
		}

		// Token: 0x04001BD5 RID: 7125
		internal string column;

		// Token: 0x04001BD6 RID: 7126
		internal string baseColumn;

		// Token: 0x04001BD7 RID: 7127
		internal MultiPartTableName multiPartTableName;

		// Token: 0x04001BD8 RID: 7128
		internal readonly int ordinal;

		// Token: 0x04001BD9 RID: 7129
		internal byte updatability;

		// Token: 0x04001BDA RID: 7130
		internal byte tableNum;

		// Token: 0x04001BDB RID: 7131
		internal bool isDifferentName;

		// Token: 0x04001BDC RID: 7132
		internal bool isKey;

		// Token: 0x04001BDD RID: 7133
		internal bool isHidden;

		// Token: 0x04001BDE RID: 7134
		internal bool isExpression;

		// Token: 0x04001BDF RID: 7135
		internal bool isIdentity;

		// Token: 0x04001BE0 RID: 7136
		internal bool isColumnSet;

		// Token: 0x04001BE1 RID: 7137
		internal byte op;

		// Token: 0x04001BE2 RID: 7138
		internal ushort operand;
	}
}

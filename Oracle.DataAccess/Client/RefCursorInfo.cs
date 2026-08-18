using System;
using System.Data;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000098 RID: 152
	internal class RefCursorInfo
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x000488FC File Offset: 0x000478FC
		internal RefCursorInfo()
		{
			this.columnInfo.Columns.Add("ColumnName", typeof(string));
			this.columnInfo.Columns.Add("ColumnOrdinal", typeof(int));
			this.columnInfo.Columns.Add("ColumnSize", typeof(int));
			this.columnInfo.Columns.Add("NumericPrecision", typeof(short));
			this.columnInfo.Columns.Add("NumericScale", typeof(short));
			this.columnInfo.Columns.Add("IsUnique", typeof(bool));
			this.columnInfo.Columns.Add("IsKey", typeof(bool));
			this.columnInfo.Columns.Add("IsRowID", typeof(bool));
			this.columnInfo.Columns.Add("BaseColumnName", typeof(string));
			this.columnInfo.Columns.Add("BaseSchemaName", typeof(string));
			this.columnInfo.Columns.Add("BaseTableName", typeof(string));
			this.columnInfo.Columns.Add("DataType", typeof(Type));
			this.columnInfo.Columns.Add("ProviderType", typeof(OracleDbType));
			this.columnInfo.Columns.Add("AllowDBNull", typeof(bool));
			this.columnInfo.Columns.Add("IsAliased", typeof(bool));
			this.columnInfo.Columns.Add("IsByteSemantic", typeof(bool));
			this.columnInfo.Columns.Add("IsExpression", typeof(bool));
			this.columnInfo.Columns.Add("IsHidden", typeof(bool));
			this.columnInfo.Columns.Add("IsReadOnly", typeof(bool));
			this.columnInfo.Columns.Add("IsLong", typeof(bool));
			this.columnInfo.Columns.Add("UdtTypeName", typeof(string));
			this.columnInfo.Columns.Add("NativeDataType", typeof(string));
			this.columnInfo.Columns.Add("ProviderDBType", typeof(DbType));
			this.columnInfo.Columns.Add("ObjectName", typeof(string));
		}

		// Token: 0x04000433 RID: 1075
		internal string name = string.Empty;

		// Token: 0x04000434 RID: 1076
		internal int position;

		// Token: 0x04000435 RID: 1077
		internal ParameterDirection mode;

		// Token: 0x04000436 RID: 1078
		internal bool isPrimaryKeyPresent;

		// Token: 0x04000437 RID: 1079
		internal DataTable columnInfo = new DataTable("SchemaTable");
	}
}

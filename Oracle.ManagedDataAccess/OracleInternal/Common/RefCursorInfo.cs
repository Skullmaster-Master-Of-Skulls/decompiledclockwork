using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.Common
{
	// Token: 0x02000039 RID: 57
	internal class RefCursorInfo
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000F52C File Offset: 0x0000D72C
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

		// Token: 0x060002B9 RID: 697 RVA: 0x0000F85C File Offset: 0x0000DA5C
		internal void FillMissingValuesFromMetadata(DataRow srcDataRow, DataRow targetDataRow)
		{
			for (int i = 0; i < targetDataRow.Table.Columns.Count; i++)
			{
				if (targetDataRow[i] == null || targetDataRow[i] == DBNull.Value)
				{
					string columnName = targetDataRow.Table.Columns[i].ToString();
					if (srcDataRow.Table.Columns.Contains(columnName))
					{
						targetDataRow[columnName] = srcDataRow[columnName];
					}
				}
			}
		}

		// Token: 0x040003A8 RID: 936
		internal string name = string.Empty;

		// Token: 0x040003A9 RID: 937
		internal int position;

		// Token: 0x040003AA RID: 938
		internal ParameterDirection mode;

		// Token: 0x040003AB RID: 939
		internal bool isPrimaryKeyPresent;

		// Token: 0x040003AC RID: 940
		internal DataTable columnInfo = new DataTable("SchemaTable");
	}
}

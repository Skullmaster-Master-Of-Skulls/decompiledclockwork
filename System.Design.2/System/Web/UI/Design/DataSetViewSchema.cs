using System;
using System.Collections.Generic;
using System.Data;

namespace System.Web.UI.Design
{
	// Token: 0x02000029 RID: 41
	public sealed class DataSetViewSchema : IDataSourceViewSchema
	{
		// Token: 0x0600014C RID: 332 RVA: 0x0000C294 File Offset: 0x0000A494
		public DataSetViewSchema(DataTable dataTable)
		{
			if (dataTable == null)
			{
				throw new ArgumentNullException("dataTable");
			}
			this._dataTable = dataTable;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000C2B1 File Offset: 0x0000A4B1
		public string Name
		{
			get
			{
				return this._dataTable.TableName;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003598 File Offset: 0x00001798
		public IDataSourceViewSchema[] GetChildren()
		{
			return null;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public IDataSourceFieldSchema[] GetFields()
		{
			List<DataSetFieldSchema> list = new List<DataSetFieldSchema>();
			foreach (object obj in this._dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					list.Add(new DataSetFieldSchema(dataColumn));
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000112 RID: 274
		private DataTable _dataTable;
	}
}

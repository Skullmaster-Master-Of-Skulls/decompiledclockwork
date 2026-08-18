using System;
using System.Data;

namespace System.Web.UI.Design
{
	// Token: 0x02000028 RID: 40
	public sealed class DataSetSchema : IDataSourceSchema
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000C22D File Offset: 0x0000A42D
		public DataSetSchema(DataSet dataSet)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			this._dataSet = dataSet;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000C24C File Offset: 0x0000A44C
		public IDataSourceViewSchema[] GetViews()
		{
			DataTableCollection tables = this._dataSet.Tables;
			DataSetViewSchema[] array = new DataSetViewSchema[tables.Count];
			for (int i = 0; i < tables.Count; i++)
			{
				array[i] = new DataSetViewSchema(tables[i]);
			}
			return array;
		}

		// Token: 0x04000111 RID: 273
		private DataSet _dataSet;
	}
}

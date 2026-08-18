using System;
using System.Data;

namespace Telerik.Charting
{
	// Token: 0x020016F6 RID: 5878
	public class DataSetClass : IDisposable
	{
		// Token: 0x0600E446 RID: 58438 RVA: 0x0032ACD8 File Offset: 0x00328ED8
		public DataSetClass()
		{
			if (this.ds.Tables.Count == 0)
			{
				DataTable dataTable = new DataTable("table1");
				dataTable.Columns.Add("Id", Type.GetType("System.Int32"));
				dataTable.Columns.Add("Name", Type.GetType("System.String"));
				dataTable.Columns.Add("Price", Type.GetType("System.Double"));
				dataTable.Rows.Add(new object[]
				{
					1,
					"Pen",
					5.45
				});
				dataTable.Rows.Add(new object[]
				{
					2,
					"Box",
					9.95
				});
				dataTable.Rows.Add(new object[]
				{
					3,
					"Pencil",
					1.99
				});
				dataTable.Rows.Add(new object[]
				{
					4,
					"Book",
					15.95
				});
				this.ds.Tables.Add(dataTable);
			}
		}

		// Token: 0x0600E447 RID: 58439 RVA: 0x0032AE5C File Offset: 0x0032905C
		~DataSetClass()
		{
			this.Dispose(false);
		}

		// Token: 0x0600E448 RID: 58440 RVA: 0x0032AE8C File Offset: 0x0032908C
		public DataSet GetData()
		{
			return this.ds;
		}

		// Token: 0x0600E449 RID: 58441 RVA: 0x0032AE94 File Offset: 0x00329094
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E44A RID: 58442 RVA: 0x0032AEA3 File Offset: 0x003290A3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ds.Dispose();
			}
		}

		// Token: 0x040041E9 RID: 16873
		private DataSet ds = new DataSet("data1");
	}
}

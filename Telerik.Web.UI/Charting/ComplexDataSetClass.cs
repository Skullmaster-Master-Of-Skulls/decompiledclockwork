using System;
using System.Data;

namespace Telerik.Charting
{
	// Token: 0x020016F7 RID: 5879
	public class ComplexDataSetClass : IDisposable
	{
		// Token: 0x0600E44B RID: 58443 RVA: 0x0032AEB4 File Offset: 0x003290B4
		public ComplexDataSetClass()
		{
			DataTable dataTable = new DataTable("table2");
			dataTable.Columns.Add("Month", Type.GetType("System.String"));
			dataTable.Columns.Add("Cars", Type.GetType("System.Int32"));
			dataTable.Columns.Add("Bikes", Type.GetType("System.Int32"));
			dataTable.Columns.Add("Trailers", Type.GetType("System.Int32"));
			dataTable.Rows.Add(new object[]
			{
				"October",
				3000,
				550,
				200
			});
			dataTable.Rows.Add(new object[]
			{
				"November",
				2000,
				750,
				600
			});
			dataTable.Rows.Add(new object[]
			{
				"December",
				6000,
				460,
				1000
			});
			dataTable.Rows.Add(new object[]
			{
				"January",
				8000,
				800,
				400
			});
			this.ds.Tables.Add(dataTable);
		}

		// Token: 0x0600E44C RID: 58444 RVA: 0x0032B074 File Offset: 0x00329274
		~ComplexDataSetClass()
		{
			this.Dispose(false);
		}

		// Token: 0x0600E44D RID: 58445 RVA: 0x0032B0A4 File Offset: 0x003292A4
		public DataSet GetData()
		{
			return this.ds;
		}

		// Token: 0x0600E44E RID: 58446 RVA: 0x0032B0AC File Offset: 0x003292AC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E44F RID: 58447 RVA: 0x0032B0BB File Offset: 0x003292BB
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ds.Dispose();
			}
		}

		// Token: 0x040041EA RID: 16874
		private DataSet ds = new DataSet("data2");
	}
}

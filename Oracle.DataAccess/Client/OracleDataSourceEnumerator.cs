using System;
using System.Data;
using System.Data.Common;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000074 RID: 116
	public sealed class OracleDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0003ACC1 File Offset: 0x00039CC1
		static OracleDataSourceEnumerator()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0003ACD0 File Offset: 0x00039CD0
		public override DataTable GetDataSources()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			int num = 0;
			DataTable dataTable = new DataTable("DataSource");
			dataTable.Columns.Add(new DataColumn("InstanceName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ServerName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ServiceName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Protocol", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Port", typeof(string)));
			try
			{
				num = OpsCom.ParseTnsnamesFile(out text, out text2, out text3, out text4, out text5);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0 || text == null)
			{
				return dataTable;
			}
			char[] separator = new char[]
			{
				' '
			};
			string[] array = text.Split(separator);
			if (array == null)
			{
				return dataTable;
			}
			string[] array2 = text2.Split(separator);
			string[] array3 = text3.Split(separator);
			string[] array4 = text4.Split(separator);
			string[] array5 = text5.Split(separator);
			int num2 = array.Length;
			for (int i = 0; i < num2; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["InstanceName"] = array[i];
				dataRow["ServerName"] = ((array3[i] != "*") ? array3[i] : string.Empty);
				dataRow["ServiceName"] = ((array4[i] != "*") ? array4[i] : string.Empty);
				dataRow["Protocol"] = ((array5[i] != "*") ? array5[i] : string.Empty);
				dataRow["Port"] = ((array2[i] != "*") ? array2[i] : string.Empty);
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
	}
}

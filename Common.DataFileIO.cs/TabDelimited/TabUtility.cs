using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace TechnoPro.Common.DataFileIO.cs.TabDelimited
{
	// Token: 0x02000004 RID: 4
	public static class TabUtility
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000311C File Offset: 0x0000131C
		public static DataTable ImportTableFromTabDelimited(string fileName, bool headersInFirstRow)
		{
			DataTable dataTable = new DataTable("t");
			using (TextReader textReader = new StreamReader(fileName))
			{
				TabStream tabStream = new TabStream(textReader);
				string[] nextRow = tabStream.GetNextRow();
				bool flag = nextRow == null;
				if (flag)
				{
					return dataTable;
				}
				List<string> list;
				if (headersInFirstRow)
				{
					list = nextRow.ToList<string>();
				}
				else
				{
					list = new List<string>();
					for (int i = 0; i < nextRow.Length; i++)
					{
						list.Add("col" + i.ToString());
					}
				}
				foreach (string columnName in list)
				{
					dataTable.Columns.Add(columnName);
				}
				for (string[] array = headersInFirstRow ? tabStream.GetNextRow() : nextRow; array != null; array = tabStream.GetNextRow())
				{
					int num = array.Length - dataTable.Columns.Count;
					bool flag2 = num > 0;
					if (flag2)
					{
						for (int j = 0; j < num; j++)
						{
							dataTable.Columns.Add("column" + j++.ToString());
						}
					}
					DataRowCollection rows = dataTable.Rows;
					object[] values = array;
					rows.Add(values);
				}
			}
			return dataTable;
		}
	}
}

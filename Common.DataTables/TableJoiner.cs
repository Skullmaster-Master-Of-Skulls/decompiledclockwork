using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.DataTables.Entities;

namespace TechnoPro.Common.DataTables
{
	// Token: 0x02000008 RID: 8
	public static class TableJoiner
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000026E4 File Offset: 0x000008E4
		public static IList<string> ApplyJoins(DataSet dataSet, TableJoiner.JoinType joinType = TableJoiner.JoinType.Left, params TableJoinSingle[] joinsOrdered)
		{
			List<string> list = new List<string>();
			if (joinsOrdered == null)
			{
				return list;
			}
			for (int i = 0; i < joinsOrdered.Length; i++)
			{
				TableJoinSingle join = joinsOrdered[i];
				if (dataSet.Tables.Contains(join.NewTableName))
				{
					list.Add("New table name already exists: '" + join.NewTableName + "' in join#" + i.ToString());
				}
				else if (!dataSet.Tables.Contains(join.Table1Name))
				{
					list.Add("Missing table '" + join.Table1Name + "' in join #" + i.ToString());
				}
				else if (!dataSet.Tables.Contains(join.Table2Name))
				{
					list.Add("Missing table '" + join.Table2Name + "' in join #" + i.ToString());
				}
				else
				{
					DataTable t1 = dataSet.Tables[join.Table1Name];
					DataTable dataTable = dataSet.Tables[join.Table2Name];
					if (!t1.Columns.Contains(join.JoinCol1Name))
					{
						list.Add("Missing column '" + join.JoinCol1Name + "' in join #" + i.ToString());
					}
					else if (!dataTable.Columns.Contains(join.JoinCol2Name))
					{
						list.Add("Missing column '" + join.JoinCol2Name + "' in join #" + i.ToString());
					}
					else
					{
						if (join.ColumnsToPull != null && join.ColumnsToPull.Any((string g) => t1.Columns.Contains(g)))
						{
							DataTable dataTable2 = t1.Copy();
							dataTable2.TableName = "t1b";
							foreach (string name in (from DataColumn dc in dataTable2.Columns
							select dc.ColumnName into g
							where !g.Equals(@join.JoinCol1Name, StringComparison.OrdinalIgnoreCase) && @join.ColumnsToPull.FirstOrDefault((string h) => h.Equals(g, StringComparison.OrdinalIgnoreCase)) == null
							select g).ToList<string>())
							{
								dataTable2.Columns.Remove(name);
							}
							t1 = dataTable2;
						}
						DataTable dataTable3 = TableJoiner.JoinTwoDataTablesOnOneColumn(t1, dataTable, join.JoinCol1Name, join.JoinCol2Name, joinType) ?? new DataTable();
						dataTable3.TableName = join.NewTableName;
						dataSet.Tables.Add(dataTable3);
					}
				}
			}
			return list;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029E4 File Offset: 0x00000BE4
		private static DataTable JoinTwoDataTablesOnOneColumn(DataTable dtblLeft, DataTable dtblRight, string colLeftToJoinOn, string colRightToJoinOn, TableJoiner.JoinType joinType)
		{
			string strTempColName = colRightToJoinOn + "_2";
			if (dtblRight.Columns.Contains(colRightToJoinOn))
			{
				dtblRight.Columns[colRightToJoinOn].ColumnName = strTempColName;
			}
			DataTable dtblResult = dtblLeft.Clone();
			IEnumerable<DataColumn> source = from dc in (from dc in dtblRight.Columns.OfType<DataColumn>()
			select new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping)).AsEnumerable<DataColumn>()
			where !dtblResult.Columns.Contains(dc.ColumnName)
			select dc;
			dtblResult.Columns.AddRange(source.ToArray<DataColumn>());
			if (!dtblLeft.Columns.Contains(colLeftToJoinOn) || (!dtblRight.Columns.Contains(colRightToJoinOn) && !dtblRight.Columns.Contains(strTempColName)))
			{
				if (!dtblResult.Columns.Contains(colLeftToJoinOn))
				{
					dtblResult.Columns.Add(colLeftToJoinOn);
				}
				return dtblResult;
			}
			if (joinType != TableJoiner.JoinType.Inner)
			{
				if (joinType != TableJoiner.JoinType.Left)
				{
				}
			}
			else
			{
				using (IEnumerator<object[]> enumerator = (from rowLeft in dtblLeft.AsEnumerable()
				join rowRight in dtblRight.AsEnumerable() on rowLeft[colLeftToJoinOn] equals rowRight[strTempColName]
				select rowLeft.ItemArray.Concat(rowRight.ItemArray).ToArray<object>()).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object[] values = enumerator.Current;
						dtblResult.Rows.Add(values);
					}
					goto IL_27F;
				}
			}
			foreach (object[] values2 in from rowLeft in dtblLeft.AsEnumerable()
			join rowRight in dtblRight.AsEnumerable() on rowLeft[colLeftToJoinOn] equals rowRight[strTempColName] into gj
			from subRight in gj.DefaultIfEmpty<DataRow>()
			select rowLeft.ItemArray.Concat((subRight == null) ? dtblRight.NewRow().ItemArray : subRight.ItemArray).ToArray<object>())
			{
				dtblResult.Rows.Add(values2);
			}
			IL_27F:
			dtblRight.Columns[strTempColName].ColumnName = colRightToJoinOn;
			dtblResult.Columns.Remove(strTempColName);
			return dtblResult;
		}

		// Token: 0x0200000D RID: 13
		public enum JoinType
		{
			// Token: 0x04000014 RID: 20
			Inner,
			// Token: 0x04000015 RID: 21
			Left
		}
	}
}

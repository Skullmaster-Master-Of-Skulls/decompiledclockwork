using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.DynamicQueries;
using TechnoPro.Common.DAO.Impl.DynamicQueries;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000061 RID: 97
	public class BreakdownNumbers : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00016F14 File Offset: 0x00015114
		public BreakdownNumbers()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00016F2F File Offset: 0x0001512F
		public BreakdownNumbers(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00016F4D File Offset: 0x0001514D
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00016F55 File Offset: 0x00015155
		public OperationContext OpContext { get; set; }

		// Token: 0x06000419 RID: 1049 RVA: 0x00016F60 File Offset: 0x00015160
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null;
			if (flag)
			{
				string defaultFunctionParameter = function.GetDefaultFunctionParameter();
				string[] array = BreakdownNumbers.SplitStringIntoNEWLINE_delimitered_parts(defaultFunctionParameter, true);
				DataView dataView = (array.Length <= 1) ? BreakdownNumbers.BreakdownNumbersLegacy(primaryDataTable.DefaultView, defaultFunctionParameter, "", this.OpContext) : BreakdownNumbers.BreakdownNumbersLegacy(primaryDataTable.DefaultView, array[0], array[1], this.OpContext);
				result.Data.Table = dataView.Table;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00016FDC File Offset: 0x000151DC
		private static string[] SplitStringIntoNEWLINE_delimitered_parts(string s, bool excludeEmptyStrings)
		{
			string[] array = s.Split(Environment.NewLine.ToCharArray());
			bool flag = !excludeEmptyStrings;
			string[] result;
			if (flag)
			{
				result = array;
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					bool flag2 = text.Trim().Length > 0;
					if (flag2)
					{
						arrayList.Add(text.Trim());
					}
				}
				array = new string[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array[j] = (string)arrayList[j];
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00017090 File Offset: 0x00015290
		private static void SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref DataView dv, string newSortString)
		{
			string sort = dv.Sort;
			string text = newSortString;
			bool flag = sort.Length > 0;
			if (flag)
			{
				string[] source = newSortString.Split(new char[]
				{
					','
				});
				string[] array = sort.Split(new char[]
				{
					','
				});
				string text2 = "";
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text3 = array2[i];
					bool flag2 = !dv.Table.Columns.Contains(text3);
					if (!flag2)
					{
						string s1 = text3.Trim().ToLower();
						bool flag3 = (from colName in source
						select colName.Trim().ToLower()).Any((string s2) => s1.CompareTo(s2) == 0);
						bool flag4 = flag3;
						if (!flag4)
						{
							bool flag5 = text2.Length > 0;
							if (flag5)
							{
								text2 += ",";
							}
							text2 += text3.Trim();
						}
					}
				}
				bool flag6 = text2.Length > 0;
				if (flag6)
				{
					text = text + "," + text2;
				}
			}
			try
			{
				dv.Sort = text;
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000171FC File Offset: 0x000153FC
		private static void AddDataColumn(ref DataTable t, string newColName)
		{
			BreakdownNumbers.AddDataColumn(ref t, newColName, Type.GetType("System.String"));
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00017214 File Offset: 0x00015414
		private static void AddDataColumn(ref DataTable t, string newColName, Type newColType)
		{
			string text = newColName;
			int num = 2;
			while (t.Columns.Contains(text))
			{
				text += num.ToString();
				num++;
			}
			t.Columns.Add(text, newColType);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001725C File Offset: 0x0001545C
		private static ArrayList GetEquivalentRows_ListIsSortedByUniqueColNames(DataView dv, int indexOfDataRowView, int[] uniqueColIndices, out int indexOfFirstNonMatchingRow)
		{
			ArrayList arrayList = new ArrayList(120);
			DataRowView dataRowView = dv[indexOfDataRowView];
			DataRow row = dataRowView.Row;
			int i;
			for (i = indexOfDataRowView + 1; i < dv.Count; i++)
			{
				DataRowView dataRowView2 = dv[i];
				DataRow row2 = dataRowView2.Row;
				bool flag = true;
				for (int j = 0; j < uniqueColIndices.Length; j++)
				{
					string text = row2[uniqueColIndices[j]].ToString().Trim();
					string strB = row[uniqueColIndices[j]].ToString().Trim();
					bool flag2 = text.CompareTo(strB) != 0;
					if (flag2)
					{
						flag = false;
						break;
					}
				}
				bool flag3 = flag;
				if (!flag3)
				{
					break;
				}
				arrayList.Add(row2);
			}
			indexOfFirstNonMatchingRow = i;
			return arrayList;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00017338 File Offset: 0x00015538
		private static DataView BreakdownNumbersLegacy(DataView dv, string uniqueColNames, string enforceRows, OperationContext opContext)
		{
			bool flag = dv == null || dv.Table.Rows.Count < 1 || uniqueColNames.Trim().Length < 1;
			DataView result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DataTable table = dv.Table;
				bool flag2 = string.IsNullOrEmpty(table.TableName);
				if (flag2)
				{
					table.TableName = "t";
				}
				BreakdownNumbers.SetNewSortButKeepOldSortValuesAtEndOfNewSort(ref dv, uniqueColNames);
				DataTable table2 = dv.Table;
				string[] array = uniqueColNames.Split(new char[]
				{
					','
				});
				bool flag3 = array.Length < 1;
				if (flag3)
				{
					result = null;
				}
				else
				{
					int[] array2 = new int[array.Length];
					DataTable dataTable = new DataTable("t");
					for (int i = 0; i < array.Length; i++)
					{
						int num = table2.Columns.IndexOf(array[i].Trim());
						bool flag4 = num >= 0;
						if (!flag4)
						{
							return null;
						}
						array2[i] = num;
						dataTable.Columns.Add(table2.Columns[num].ColumnName, table2.Columns[num].DataType);
					}
					BreakdownNumbers.AddDataColumn(ref dataTable, "NumRows", typeof(int));
					int columnIndex = dataTable.Columns.Count - 1;
					int num2;
					for (int j = 0; j < dv.Count; j = num2)
					{
						DataRowView dataRowView = dv[j];
						DataRow row = dataRowView.Row;
						ArrayList equivalentRows_ListIsSortedByUniqueColNames = BreakdownNumbers.GetEquivalentRows_ListIsSortedByUniqueColNames(dv, j, array2, out num2);
						int num3 = equivalentRows_ListIsSortedByUniqueColNames.Count + 1;
						DataRow dataRow = dataTable.NewRow();
						for (int k = 0; k < array2.Length; k++)
						{
							dataRow[k] = row[array2[k]];
						}
						dataRow[columnIndex] = num3;
						dataTable.Rows.Add(dataRow);
					}
					ArrayList arrayList = new ArrayList();
					string text = enforceRows;
					int num4 = 0;
					bool flag6;
					do
					{
						int num5 = text.IndexOf("{");
						bool flag5 = num5 >= 0;
						if (!flag5)
						{
							break;
						}
						int num6 = text.IndexOf("}", num5);
						string text2 = text.Substring(num5, num6 - num5 + 1);
						text = text.Remove(num5, num6 - num5 + 1);
						text2 = text2.Replace(',', '~');
						text2 = text2.Replace('{', '[');
						text2 = text2.Replace('}', ']');
						text = text.Insert(num5, text2);
						num4++;
						flag6 = (num4 > 100000);
					}
					while (!flag6);
					string[] array3 = text.Split(new char[]
					{
						','
					});
					IDynamicQueryDAO dynamicQueryDAO = new DynamicQueryDAO(opContext);
					string sql = "SELECT controlid,controlcaption FROM dynamiccontrols WHERE controlid in (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
					foreach (string text3 in array3)
					{
						bool flag7 = text3.Trim().Length > 0;
						if (flag7)
						{
							bool flag8 = text3[0] == '[';
							if (flag8)
							{
								string text4 = text3.Substring(1, text3.Length - 2);
								text4 = text4.Replace('~', ',');
								DataTable dataTable2 = dynamicQueryDAO.ExecuteQuery(new QueryRequest
								{
									Sql = sql,
									Parameters = new List<CommonParameter>
									{
										new CommonParameter
										{
											Name = "@cids",
											DbType = new DbType?(DbType.String),
											Value = text4
										}
									}
								}).DataTable;
								foreach (object obj in dataTable2.Rows)
								{
									DataRow dataRow2 = (DataRow)obj;
									string text5 = dataRow2["controlcaption"].ToString();
									int num7 = text5.IndexOf("~~");
									bool flag9 = num7 > 0;
									if (flag9)
									{
										text5 = text5.Substring(0, num7);
									}
									arrayList.Add(text5);
								}
							}
							else
							{
								arrayList.Add(text3);
							}
						}
					}
					foreach (object obj2 in arrayList)
					{
						string text6 = (string)obj2;
						string erc = text6.Trim().ToLower();
						bool flag10 = (from DataRow dr in dataTable.Rows
						select dr[0].ToString().Trim().ToLower()).Any((string ccaption) => ccaption.CompareTo(erc) == 0);
						bool flag11 = !flag10;
						if (flag11)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3[0] = text6;
							for (int m = 1; m < dataTable.Columns.Count; m++)
							{
								bool flag12 = dataTable.Columns[m].DataType == typeof(int);
								if (flag12)
								{
									dataRow3[m] = 0;
								}
							}
							dataTable.Rows.Add(dataRow3);
						}
					}
					bool flag13 = string.IsNullOrEmpty(dataTable.TableName);
					if (flag13)
					{
						dataTable.TableName = "t";
					}
					DataView dataView = new DataView(dataTable);
					dv.Sort = uniqueColNames;
					result = dataView;
				}
			}
			return result;
		}

		// Token: 0x040000BB RID: 187
		private ReportDAO dao;
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Reports.Impl
{
	// Token: 0x02000009 RID: 9
	public static class ReportFunctionsLegacy
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000785C File Offset: 0x00005A5C
		public static DataView DecodeDynamicData(DataView dvOriginal, OperationContext opContext, params string[] uniqueColNames)
		{
			string[] array = new string[]
			{
				"controlid",
				"controlcode",
				"setting1",
				"setting2",
				"setting3",
				"setting4",
				"setting4string",
				"defaultvalue",
				"defaultvaluestring",
				"screennum",
				"valint",
				"valbytes",
				"valdate",
				"controlcaption",
				"controlvalue"
			};
			DataTable table = dvOriginal.Table;
			DataTable dataTable = table.Clone();
			foreach (string name in array)
			{
				bool flag = dataTable.Columns.Contains(name);
				if (flag)
				{
					dataTable.Columns.Remove(name);
				}
			}
			int count = dataTable.Columns.Count;
			int l;
			for (int j = 0; j < dvOriginal.Count; j = l)
			{
				DataRow row = dvOriginal[j].Row;
				string[] array3 = new string[uniqueColNames.Length];
				for (int k = 0; k < uniqueColNames.Length; k++)
				{
					array3[k] = row[uniqueColNames[k]].ToString();
				}
				for (l = j + 1; l < dvOriginal.Count; l++)
				{
					DataRow row2 = dvOriginal[l].Row;
					string[] array4 = new string[uniqueColNames.Length];
					for (int m = 0; m < uniqueColNames.Length; m++)
					{
						array4[m] = row2[uniqueColNames[m]].ToString();
					}
					bool flag2 = true;
					for (int n = 0; n < array4.Length; n++)
					{
						string text = array3[n];
						string strB = array4[n];
						bool flag3 = text.CompareTo(strB) != 0;
						if (flag3)
						{
							flag2 = false;
							break;
						}
					}
					bool flag4 = !flag2;
					if (flag4)
					{
						break;
					}
				}
				DataRow dataRow = dataTable.NewRow();
				for (int num = 0; num < count; num++)
				{
					string columnName = dataTable.Columns[num].ColumnName;
					dataRow[columnName] = row[columnName];
				}
				for (int num2 = j; num2 < l; num2++)
				{
					DataRow row3 = dvOriginal[num2].Row;
					DynamicControl dynamicControl = new DynamicControl(row3);
					bool flag5 = dynamicControl.ControlId > 0;
					if (flag5)
					{
						string text2;
						object value = ReportFunction.DynamicDataToObjectAndString(row3, dynamicControl, "valint", "valbytes", "valdate", "", out text2, opContext);
						string text3 = dynamicControl.ControlCaptionForDisplay.Replace(' ', '_');
						bool flag6 = !dataTable.Columns.Contains(text3);
						if (flag6)
						{
							object[] itemArray = dataRow.ItemArray;
							dataTable.Columns.Add(text3);
							dataRow = dataTable.NewRow();
							for (int num3 = 0; num3 < itemArray.Length; num3++)
							{
								dataRow[num3] = itemArray[num3];
							}
						}
						dataRow[text3] = value;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable.DefaultView;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00007BC4 File Offset: 0x00005DC4
		public static DataTable PullInData(DataTable t, string sql, OperationContext opContext)
		{
			bool flag = t == null;
			DataTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DataView dataView = ReportFunctionsLegacy.PullInData(t.DefaultView, sql, opContext);
				result = ((dataView != null) ? dataView.Table : null);
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00007BFC File Offset: 0x00005DFC
		private static DataView PullInData(DataView dv, string sql, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			bool flag = dv == null || dv.Table.Rows.Count < 1;
			DataView result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = sql.Length < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Regex regex = new Regex("@\\b\\w+");
					DataTable table = dv.Table.Copy();
					DataView dataView = new DataView
					{
						Table = table
					};
					MatchCollection matchCollection = regex.Matches(sql);
					ColumnIndexCollection columnIndexCollection = new ColumnIndexCollection();
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						bool encrypted = false;
						string text = match.Value.Trim().ToLower();
						string text2 = text.Substring(1);
						bool flag3 = columnIndexCollection.Contains(text2);
						if (!flag3)
						{
							int num = dataView.Table.Columns.IndexOf(text.Substring(1));
							bool flag4 = num < 0 && text2 == "personid";
							if (flag4)
							{
								num = dataView.Table.Columns.IndexOf("student_no");
								bool flag5 = num >= 0;
								if (flag5)
								{
									encrypted = true;
									text2 = "student_no";
								}
							}
							else
							{
								string text3 = text2;
								string a = text3;
								if (!(a == "firstname"))
								{
									if (!(a == "lastname"))
									{
										if (!(a == "student_no"))
										{
											if (a == "middlename")
											{
												encrypted = true;
											}
										}
										else
										{
											encrypted = true;
										}
									}
									else
									{
										encrypted = true;
									}
								}
								else
								{
									encrypted = true;
								}
							}
							bool flag6 = num < 0;
							if (!flag6)
							{
								bool flag7 = text2.Length > 1 && text2[0] == '*';
								if (flag7)
								{
									encrypted = true;
									text2 = text2.Substring(1);
								}
								ColumnIndexClass newColumnIndexClass = new ColumnIndexClass(num, text2, text, encrypted);
								columnIndexCollection.Add(newColumnIndexClass);
							}
						}
					}
					byte[] array = new byte[1];
					Type type = array.GetType();
					try
					{
						int num2 = 0;
						string text4 = null;
						for (;;)
						{
							bool flag8 = num2 >= dataView.Table.Rows.Count;
							if (flag8)
							{
								break;
							}
							List<DbParameter> list = new List<DbParameter>();
							foreach (object obj2 in columnIndexCollection)
							{
								ColumnIndexClass columnIndexClass = (ColumnIndexClass)obj2;
								string pName = columnIndexClass.ParamName;
								string colName = columnIndexClass.ColName;
								int index = columnIndexClass.Index;
								bool flag9 = index < 0;
								if (!flag9)
								{
									DataRow dataRow = dataView.Table.Rows[num2];
									object obj3 = dataRow[index];
									bool encrypted2 = columnIndexClass.Encrypted;
									if (encrypted2)
									{
										string plainText = obj3.ToString();
										obj3 = encryption.Encrypt(plainText);
									}
									bool flag10 = pName.CompareTo("@" + colName) != 0;
									if (flag10)
									{
										string specialPName = "@___x";
										sql = "SELECT personid FROM people WHERE " + colName + "=" + specialPName;
										DbParameter dbParameter = list.FirstOrDefault((DbParameter g) => g.ParameterName.Equals(specialPName, StringComparison.OrdinalIgnoreCase));
										bool flag11 = dbParameter != null;
										if (flag11)
										{
											dbParameter.Value = obj3;
										}
										else
										{
											list.Add(databaseLayer.GetParameter(specialPName, ReportFunctionsLegacy.GetDbType(obj3), obj3));
										}
										DataTable dataTable = databaseLayer.ExecuteQuery(sql, list.ToArray());
										obj3 = ((dataTable.Rows.Count > 0) ? ((int)dataTable.Rows[0][0]) : -1);
										pName = "@personid";
									}
									DbParameter dbParameter2 = list.FirstOrDefault((DbParameter g) => g.ParameterName.Equals(pName, StringComparison.OrdinalIgnoreCase));
									bool flag12 = dbParameter2 != null;
									if (flag12)
									{
										dbParameter2.Value = obj3;
									}
									else
									{
										DbType dbType = ReportFunctionsLegacy.GetDbType(obj3);
										list.Add(databaseLayer.GetParameter(pName, dbType, obj3));
									}
								}
							}
							DataTable dataTable2 = databaseLayer.ExecuteQuery(sql, list.ToArray());
							bool flag13 = !string.IsNullOrEmpty(text4);
							if (flag13)
							{
								break;
							}
							foreach (object obj4 in dataTable2.Rows)
							{
								DataRow dataRow2 = (DataRow)obj4;
								for (int i = 0; i < dataTable2.Columns.Count; i++)
								{
									string columnName = "_" + dataTable2.Columns[i].ColumnName;
									int num3 = dataView.Table.Columns.IndexOf(columnName);
									bool flag14 = num3 < 0;
									if (flag14)
									{
										Type type2 = dataTable2.Columns[i].DataType;
										bool flag15 = type2 == type;
										if (flag15)
										{
											type2 = Type.GetType("System.String");
										}
										dataView.Table.Columns.Add(columnName, type2);
										num3 = dataView.Table.Columns.IndexOf(columnName);
									}
									bool flag16 = num3 < 0;
									if (!flag16)
									{
										bool flag17 = dataTable2.Columns[i].DataType == type;
										object value;
										if (flag17)
										{
											value = ((dataRow2[i] == DBNull.Value) ? null : encryption.Decrypt((byte[])dataRow2[i]));
										}
										else
										{
											value = dataRow2[i];
										}
										dataView.Table.Rows[num2][num3] = value;
									}
								}
							}
							num2++;
						}
						bool flag18 = !string.IsNullOrEmpty(text4);
						if (flag18)
						{
							ReportFunctionsLegacy.MessageBoxShow(text4);
						}
					}
					catch (Exception ex)
					{
						ReportFunctionsLegacy.MessageBoxShow(ex.ToString());
					}
					result = dataView;
				}
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000082C0 File Offset: 0x000064C0
		private static DbType GetDbType(object o)
		{
			bool flag = o is DateTime;
			DbType result;
			if (flag)
			{
				result = DbType.DateTime;
			}
			else
			{
				bool flag2 = o is byte[];
				if (flag2)
				{
					result = DbType.Binary;
				}
				else
				{
					bool flag3 = o is bool;
					if (flag3)
					{
						result = DbType.Boolean;
					}
					else
					{
						bool flag4 = o is int;
						if (flag4)
						{
							result = DbType.Int32;
						}
						else
						{
							bool flag5 = o is double;
							if (flag5)
							{
								result = DbType.Double;
							}
							else
							{
								result = DbType.String;
								o = (((o != null) ? o.ToString() : null) ?? "");
							}
						}
					}
				}
			}
			return result;
		}
	}
}

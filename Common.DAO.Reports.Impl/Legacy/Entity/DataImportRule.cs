using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000015 RID: 21
	public class DataImportRule
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000243C9 File Offset: 0x000225C9
		public int[] ExternalColIndices
		{
			get
			{
				return this._externalColIndices;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000243D1 File Offset: 0x000225D1
		public int Controlid
		{
			get
			{
				return this._controlid;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000243D9 File Offset: 0x000225D9
		public string MappingString
		{
			get
			{
				return this.mappingString;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000243E4 File Offset: 0x000225E4
		private static string OnlyKeepCharsDigitsSpace(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				IEnumerable<char> source = from g in s.ToCharArray().ToList<char>()
				where char.IsLetterOrDigit(g) || g == ' '
				select g;
				result = string.Join("", source.ToList<char>().ConvertAll<string>((char g) => g.ToString()).ToArray());
			}
			return result;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00024474 File Offset: 0x00022674
		public DataImportRule(DataTable externalTable, string str, OperationContext opContext, string mappingString = null)
		{
			this.mappingString = mappingString;
			int num = str.IndexOf("=");
			bool flag = num > 0;
			if (flag)
			{
				string s = str.Substring(0, num);
				string text = str.Substring(num + 1);
				try
				{
					this._controlid = int.Parse(s);
				}
				catch
				{
					this._controlid = -1;
				}
				string[] array = text.Split(new char[]
				{
					','
				});
				this._externalColIndices = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = externalTable.Columns.IndexOf(array[i]);
					this._externalColIndices[i] = num2;
					bool flag2 = num2 < 0;
					if (flag2)
					{
						string text2 = DataImportRule.OnlyKeepCharsDigitsSpace(array[i]);
						bool flag3 = text2.Length > 0;
						if (flag3)
						{
							foreach (object obj in externalTable.Columns)
							{
								DataColumn dataColumn = (DataColumn)obj;
								string value = DataImportRule.OnlyKeepCharsDigitsSpace(dataColumn.ColumnName);
								bool flag4 = text2.Equals(value, StringComparison.OrdinalIgnoreCase);
								if (flag4)
								{
									num2 = externalTable.Columns.IndexOf(dataColumn);
								}
							}
						}
					}
					bool flag5 = num2 < 0;
					if (flag5)
					{
						throw new Exception("Can't find a column name: " + array[i] + "; " + text);
					}
				}
				string text3 = "SELECT dc.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,dc.p,dc.statsholding";
				text3 += ",dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers,dc.defaultvaluestring,dc.setting4string,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline";
				text3 = text3 + " FROM dynamiccontrols dc WHERE dc.controlid=" + this._controlid.ToString();
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				DataTable dataTable = databaseLayer.ExecuteQuery(text3);
				bool flag6 = dataTable.Rows.Count > 0;
				if (flag6)
				{
					this._dynamicControlRow = dataTable.Rows[0];
					this._controlCode = (int)this._dynamicControlRow[1];
				}
				else
				{
					this._dynamicControlRow = null;
					this._controlCode = -1;
				}
				return;
			}
			throw new Exception("Missing equals operator: " + str);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000246C8 File Offset: 0x000228C8
		private static DataRow FindRowToImport(DataTable rowsToImport, int personid, int controlid)
		{
			return (from DataRow dr in rowsToImport.Rows
			let pid = (int)dr[2]
			where pid == personid
			let cid = (int)dr[3]
			where cid == controlid
			select dr).FirstOrDefault<DataRow>();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0002478C File Offset: 0x0002298C
		private static bool IntToBool(int i)
		{
			return i != 0;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000247A4 File Offset: 0x000229A4
		private static int BoolToInt(bool b)
		{
			int result;
			if (b)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000247C0 File Offset: 0x000229C0
		private static void Log(string s)
		{
			ReportFunctionsLegacy.MessageBoxShow(s);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000247CC File Offset: 0x000229CC
		private static string DataTableToString(DataTable t)
		{
			string text = "";
			foreach (object obj in t.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				bool flag = text.Length > 0;
				if (flag)
				{
					text += ",";
				}
				text += dataColumn.ColumnName;
			}
			text += Environment.NewLine;
			text += "============";
			text += Environment.NewLine;
			for (int i = 0; i < t.Rows.Count; i++)
			{
				DataRow dataRow = t.Rows[i];
				bool flag2 = dataRow.RowState != DataRowState.Deleted;
				if (flag2)
				{
					for (int j = 0; j < t.Columns.Count; j++)
					{
						bool flag3 = j > 0;
						if (flag3)
						{
							text += ",";
						}
						text += dataRow[j].ToString().Replace(',', '`').Replace(Environment.NewLine, "\\n");
					}
					text += Environment.NewLine;
				}
				else
				{
					text = text + "DELETED ROW" + Environment.NewLine;
				}
			}
			return text;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00024950 File Offset: 0x00022B50
		private static void MapFilePicture(ref DataTable rowsToImport, DataRow externalRow, int ind, int personid, int controlid, DatabaseLayer databaseManager)
		{
			try
			{
				bool flag = externalRow[ind] == DBNull.Value || !(externalRow[ind] is byte[]);
				if (!flag)
				{
					byte[] array = (byte[])externalRow[ind];
					bool flag2 = array.Length == 0;
					if (!flag2)
					{
						string query = string.Format("SELECT DATALENGTH(controlvalue) AS piclen FROM imageinfops WHERE personid={0} AND controlid={1}", personid, controlid);
						DataTable dataTable = databaseManager.ExecuteQuery(query);
						string text = "";
						bool flag3 = dataTable.Rows.Count > 0 && !(dataTable.Rows[0][0] is DBNull);
						if (flag3)
						{
							int num = (int)dataTable.Rows[0][0];
							bool flag4 = num != array.Length;
							if (flag4)
							{
								text = "modify";
							}
						}
						else
						{
							text = "add";
						}
						bool flag5 = text.Length < 1;
						if (!flag5)
						{
							DataRow dataRow = rowsToImport.NewRow();
							dataRow[2] = personid;
							dataRow[3] = controlid;
							dataRow[10] = array;
							dataRow[4] = text;
							rowsToImport.Rows.Add(dataRow);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00024AC0 File Offset: 0x00022CC0
		private static void MapCheckboxAndLegacySingleRadioButton(ref DataTable rowsToImport, string firstExternalStringValue, int personid, int controlid, DatabaseLayer databaseManager)
		{
			bool flag = firstExternalStringValue.Length >= 1 && firstExternalStringValue.ToLower()[0] != 'f' && firstExternalStringValue[0] != '0' && firstExternalStringValue[0] != '-';
			string query = "SELECT controlid FROM maininfops WHERE personid=" + personid.ToString() + " AND controlid=" + controlid.ToString();
			DataTable dataTable = databaseManager.ExecuteQuery(query);
			bool flag2 = dataTable.Rows.Count >= 1;
			bool flag3 = flag == flag2;
			if (!flag3)
			{
				DataRow dataRow = rowsToImport.NewRow();
				dataRow[2] = personid;
				dataRow[3] = controlid;
				dataRow[5] = DataImportRule.BoolToInt(true);
				dataRow[4] = (flag ? "add" : "deletechk");
				rowsToImport.Rows.Add(dataRow);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00024BAC File Offset: 0x00022DAC
		private static void MapTextbox(ref DataTable rowsToImport, DataRow dynamicControlRow, string firstExternalStringValue, int personid, int controlid, DatabaseLayer databaseManager, IEncryption tripleDes)
		{
			string query = "SELECT controlvalue FROM otherinfops WHERE personid=" + personid.ToString() + " AND controlid=" + controlid.ToString();
			DataTable dataTable = databaseManager.ExecuteQuery(query);
			int num = (int)dynamicControlRow["setting3"];
			string text = (dataTable.Rows.Count > 0) ? ((dataTable.Rows[0][0] == DBNull.Value) ? "" : DataImportRule.BytesToString((byte[])dataTable.Rows[0][0], num == 1, tripleDes)).ToLower().Trim() : "";
			string text2 = firstExternalStringValue.ToLower();
			bool flag = text == text2;
			if (!flag)
			{
				DataRow dataRow = rowsToImport.NewRow();
				dataRow[2] = personid;
				dataRow[3] = controlid;
				dataRow[6] = DataImportRule.StringToBytes(firstExternalStringValue, num == 1, tripleDes);
				dataRow[8] = text2;
				bool flag2 = text.Length < 1;
				if (flag2)
				{
					dataRow[4] = "add";
				}
				else
				{
					bool flag3 = text2.Length < 1;
					if (flag3)
					{
						dataRow[4] = "delete";
					}
					else
					{
						dataRow[4] = "modify";
					}
				}
				rowsToImport.Rows.Add(dataRow);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00024D14 File Offset: 0x00022F14
		private static void MapDate(ref DataTable rowsToImport, string firstExternalStringValue, int personid, int controlid, DatabaseLayer databaseManager)
		{
			bool flag = firstExternalStringValue.Length > 0;
			DateTime dateTime;
			if (flag)
			{
				try
				{
					dateTime = DateTime.Parse(firstExternalStringValue);
				}
				catch
				{
					dateTime = DateTime.MinValue;
				}
			}
			else
			{
				dateTime = DateTime.MinValue;
			}
			string query = "SELECT controlvalue FROM datetimeinfops WHERE personid=" + personid.ToString() + " AND controlid=" + controlid.ToString();
			DataTable dataTable = databaseManager.ExecuteQuery(query);
			DateTime d = (dataTable.Rows.Count > 0) ? ((dataTable.Rows[0][0] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataTable.Rows[0][0])) : DateTime.MinValue;
			bool flag2 = d == dateTime;
			if (!flag2)
			{
				DataRow dataRow = rowsToImport.NewRow();
				dataRow[2] = personid;
				dataRow[3] = controlid;
				dataRow[7] = dateTime;
				bool flag3 = d == DateTime.MinValue;
				if (flag3)
				{
					dataRow[4] = "add";
				}
				else
				{
					bool flag4 = dateTime == DateTime.MinValue;
					if (flag4)
					{
						dataRow[4] = "delete";
					}
					else
					{
						dataRow[4] = "modify";
					}
				}
				rowsToImport.Rows.Add(dataRow);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00024E7C File Offset: 0x0002307C
		private static void MapRadioGroup(ref DataTable rowsToImport, DataRow dynamicControlRow, string firstExternalStringValue, int personid, int controlid, DatabaseLayer databaseManager)
		{
			int num = (int)dynamicControlRow["setting1"];
			string query = string.Concat(new string[]
			{
				"SELECT mi.controlvalue,ll.lookuptext FROM maininfops mi LEFT JOIN lookuplists ll ON ll.lookuplistid=mi.controlvalue WHERE mi.personid=",
				personid.ToString(),
				" AND mi.controlid=",
				controlid.ToString(),
				" AND ll.lookupgroupid=",
				num.ToString()
			});
			DataTable dataTable = databaseManager.ExecuteQuery(query);
			string text = (dataTable.Rows.Count > 0) ? dataTable.Rows[0][1].ToString().ToLower().Trim() : "";
			string text2 = firstExternalStringValue.ToLower().Trim();
			bool flag = text2.Length > 0;
			int num2;
			if (flag)
			{
				query = "SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgid AND lookuptext=@txt";
				DbParameter[] parameters = new DbParameter[]
				{
					databaseManager.GetParameter("@txt", DbType.String, text2),
					databaseManager.GetParameter("@lgid", DbType.Int32, num)
				};
				dataTable = databaseManager.ExecuteQuery(query, parameters);
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					num2 = (int)dataTable.Rows[0][0];
				}
				else
				{
					num2 = -1;
					text2 = "";
				}
			}
			else
			{
				num2 = -1;
			}
			bool flag3 = text.CompareTo(text2) == 0;
			if (!flag3)
			{
				DataRow dataRow = rowsToImport.NewRow();
				dataRow[2] = personid;
				dataRow[3] = controlid;
				dataRow[8] = text2;
				bool flag4 = text2.Length < 1;
				if (flag4)
				{
					dataRow[5] = -1;
					dataRow[4] = "delete";
				}
				else
				{
					bool flag5 = text.Length < 1;
					if (flag5)
					{
						dataRow[5] = num2;
						dataRow[4] = "add";
					}
					else
					{
						dataRow[4] = "modify";
						dataRow[5] = num2;
					}
				}
				rowsToImport.Rows.Add(dataRow);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0002509C File Offset: 0x0002329C
		private static void MapDropList(ref DataTable rowsToImport, DataRow dynamicControlRow, string firstExternalStringValue, int personid, int controlid, DatabaseLayer databaseManager, IEncryption tripleDes)
		{
			int num = (int)dynamicControlRow["setting3"];
			int num2 = (int)dynamicControlRow["setting1"];
			bool flag = num == 0;
			if (flag)
			{
				string query = string.Concat(new string[]
				{
					"SELECT mi.controlvalue,ll.lookuptext FROM maininfops mi LEFT JOIN lookuplists ll ON ll.lookuplistid=mi.controlvalue WHERE mi.personid=",
					personid.ToString(),
					" AND mi.controlid=",
					controlid.ToString(),
					" AND ll.lookupgroupid=",
					num2.ToString()
				});
				DataTable dataTable = databaseManager.ExecuteQuery(query);
				string text = (dataTable.Rows.Count > 0) ? dataTable.Rows[0][1].ToString() : "";
				string text2 = firstExternalStringValue.ToLower().Trim();
				text = text.ToLower().Trim();
				bool flag2 = text2.Length > 0;
				int num3;
				if (flag2)
				{
					query = "SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgid AND lookuptext=@txt";
					DbParameter[] parameters = new DbParameter[]
					{
						databaseManager.GetParameter("@txt", DbType.String, text2),
						databaseManager.GetParameter("@lgid", DbType.Int32, num2)
					};
					dataTable = databaseManager.ExecuteQuery(query, parameters);
					bool flag3 = dataTable.Rows.Count > 0;
					if (flag3)
					{
						num3 = (int)dataTable.Rows[0][0];
					}
					else
					{
						num3 = -1;
						text2 = "";
					}
				}
				else
				{
					num3 = -1;
				}
				bool flag4 = text.CompareTo(text2) == 0;
				if (!flag4)
				{
					DataRow dataRow = rowsToImport.NewRow();
					dataRow[2] = personid;
					dataRow[3] = controlid;
					dataRow[8] = text2;
					bool flag5 = text2.Length < 1;
					if (flag5)
					{
						dataRow[5] = -1;
						dataRow[4] = "delete";
					}
					else
					{
						bool flag6 = text.Length < 1;
						if (flag6)
						{
							dataRow[5] = num3;
							dataRow[4] = "add";
						}
						else
						{
							dataRow[4] = "modify";
							dataRow[5] = num3;
						}
					}
					rowsToImport.Rows.Add(dataRow);
				}
			}
			else
			{
				int num4 = (int)dynamicControlRow["setting2"];
				string query2 = "SELECT controlvalue FROM otherinfops WHERE personid=" + personid.ToString() + " AND controlid=" + controlid.ToString();
				DataTable dataTable2 = databaseManager.ExecuteQuery(query2);
				bool flag7 = dataTable2.Rows.Count > 0;
				string text3;
				if (flag7)
				{
					text3 = ((dataTable2.Rows[0][0] == DBNull.Value) ? "" : DataImportRule.BytesToString((byte[])dataTable2.Rows[0][0], num == -1, tripleDes));
				}
				else
				{
					text3 = "";
				}
				text3 = text3.ToLower().Trim();
				string text4 = firstExternalStringValue.ToLower().Trim();
				bool flag8 = text3.CompareTo(text4) == 0;
				if (!flag8)
				{
					DataRow dataRow2 = rowsToImport.NewRow();
					dataRow2[2] = personid;
					dataRow2[3] = controlid;
					dataRow2[6] = DataImportRule.StringToBytes(text4, num == -1, tripleDes);
					dataRow2[8] = text4;
					bool flag9 = text3.Length < 1;
					if (flag9)
					{
						dataRow2[4] = "add";
					}
					else
					{
						bool flag10 = text4.Length < 1;
						if (flag10)
						{
							dataRow2[4] = "delete";
						}
						else
						{
							dataRow2[4] = "modify";
						}
					}
					rowsToImport.Rows.Add(dataRow2);
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00025468 File Offset: 0x00023668
		private static void MapListView(ref DataTable rowsToImport, DataTable originalExternalRowsTable, int[] externalColIndices, int personid, int controlid, DatabaseLayer databaseManager, IEncryption tripleDes)
		{
			DataRow dataRow = DataImportRule.FindRowToImport(rowsToImport, personid, controlid);
			bool flag = dataRow != null;
			if (!flag)
			{
				string text = "";
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < originalExternalRowsTable.Rows.Count; i++)
				{
					DataRow dataRow2 = originalExternalRowsTable.Rows[i];
					bool flag2 = dataRow2.RowState == DataRowState.Deleted;
					if (!flag2)
					{
						int num = (int)dataRow2["pid"];
						bool flag3 = num != personid;
						if (!flag3)
						{
							bool flag4 = true;
							string text2 = "";
							string text3 = "";
							for (int j = 0; j < externalColIndices.Length; j++)
							{
								bool flag5 = j > 0;
								if (flag5)
								{
									text2 += "\0";
								}
								string text4 = dataRow2[externalColIndices[j]].ToString();
								text2 += text4;
								bool flag6 = flag4 && text4.Trim().Length > 0;
								if (flag6)
								{
									flag4 = false;
								}
								bool flag7 = j == externalColIndices.Length - 1;
								if (flag7)
								{
									text3 = text4.Trim().ToLower();
								}
							}
							bool flag8 = flag4 || arrayList.Contains(text3);
							if (!flag8)
							{
								arrayList.Add(text3);
								bool flag9 = text.Length > 0;
								if (flag9)
								{
									text += "\t";
								}
								text += text2;
							}
						}
					}
				}
				string query = "SELECT controlvalue FROM otherinfops WHERE personid=" + personid.ToString() + " AND controlid=" + controlid.ToString();
				string text5 = "";
				DataTable dataTable;
				try
				{
					dataTable = databaseManager.ExecuteQuery(query);
				}
				catch (Exception ex)
				{
					text5 = text5.ToString();
					dataTable = new DataTable();
				}
				string text6 = (dataTable.Rows.Count > 0) ? ((dataTable.Rows[0][0] == DBNull.Value) ? "" : DataImportRule.BytesToString((byte[])dataTable.Rows[0][0], false, tripleDes)) : "";
				bool flag10 = text6.Length > 0;
				if (flag10)
				{
					string[] array = text6.Split(new char[]
					{
						'\t'
					});
					string[] array2 = array;
					for (int k = 0; k < array2.Length; k++)
					{
						string text7 = array2[k];
						string[] array3 = text7.Split(new char[1]);
						bool flag11 = array3.Length == 0;
						if (!flag11)
						{
							string ds = array3[array3.Length - 1].Trim().ToLower();
							bool flag12 = arrayList.Cast<string>().Any((string ds2) => ds2.CompareTo(ds) == 0);
							bool flag13 = flag12;
							if (!flag13)
							{
								bool flag14 = text.Length > 0;
								if (flag14)
								{
									text += "\t";
								}
								text += text7;
							}
						}
					}
				}
				bool flag15 = text.ToLower().Trim().CompareTo(text6.ToLower().Trim()) == 0;
				if (!flag15)
				{
					DataRow dataRow3 = rowsToImport.NewRow();
					dataRow3[2] = personid;
					dataRow3[3] = controlid;
					bool flag16 = text6.Length < 1;
					if (flag16)
					{
						dataRow3[6] = DataImportRule.StringToBytes(text, false, tripleDes);
						dataRow3[4] = "add";
					}
					else
					{
						dataRow3[6] = DataImportRule.StringToBytes(text, false, tripleDes);
						dataRow3[4] = "modify";
					}
					rowsToImport.Rows.Add(dataRow3);
				}
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00025834 File Offset: 0x00023A34
		public void Map(DataRow externalRow, int personid, ref DataTable rowsToImport, DataTable originalExternalRowsTable, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			bool flag = this._externalColIndices == null || this._externalColIndices.Length < 1;
			if (flag)
			{
				throw new Exception("Something went wrong with _controlid=" + this._controlid.ToString());
			}
			int num = this._externalColIndices[0];
			bool flag2 = num < 0;
			if (flag2)
			{
			}
			string firstExternalStringValue = externalRow[num].ToString().Trim();
			int controlCode = this._controlCode;
			int num2 = controlCode;
			switch (num2)
			{
			case 1:
			case 11:
				DataImportRule.MapTextbox(ref rowsToImport, this._dynamicControlRow, firstExternalStringValue, personid, this._controlid, databaseLayer, encryption);
				break;
			case 2:
			case 4:
			case 12:
				DataImportRule.MapCheckboxAndLegacySingleRadioButton(ref rowsToImport, firstExternalStringValue, personid, this._controlid, databaseLayer);
				break;
			case 3:
				DataImportRule.MapDropList(ref rowsToImport, this._dynamicControlRow, firstExternalStringValue, personid, this._controlid, databaseLayer, encryption);
				break;
			case 5:
			case 7:
			case 8:
			case 9:
			case 13:
				break;
			case 6:
				DataImportRule.MapDate(ref rowsToImport, firstExternalStringValue, personid, this._controlid, databaseLayer);
				break;
			case 10:
				DataImportRule.MapListView(ref rowsToImport, originalExternalRowsTable, this._externalColIndices, personid, this._controlid, databaseLayer, encryption);
				break;
			case 14:
				DataImportRule.MapRadioGroup(ref rowsToImport, this._dynamicControlRow, firstExternalStringValue, personid, this._controlid, databaseLayer);
				break;
			default:
				if (num2 == 21 || num2 == 400)
				{
					DataImportRule.MapFilePicture(ref rowsToImport, externalRow, num, personid, this._controlid, databaseLayer);
				}
				break;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000259C4 File Offset: 0x00023BC4
		private static byte[] StringToBytes(string txt, bool encrypt, IEncryption tripleDES)
		{
			bool flag = txt == null;
			if (flag)
			{
				txt = "";
			}
			byte[] result;
			if (encrypt)
			{
				result = tripleDES.Encrypt(txt);
			}
			else
			{
				Encoding encoding = (tripleDES != null) ? tripleDES.Encoder : new UTF8Encoding();
				result = encoding.GetBytes(txt);
			}
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00025A0C File Offset: 0x00023C0C
		public static string BytesToString(byte[] bytes, bool decrypt, IEncryption tripleDES)
		{
			bool flag = bytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else if (decrypt)
			{
				result = tripleDES.Decrypt(bytes);
			}
			else
			{
				Encoding encoding = (tripleDES != null) ? tripleDES.Encoder : new UTF8Encoding();
				result = encoding.GetString(bytes);
			}
			return result;
		}

		// Token: 0x0400005B RID: 91
		private readonly int _controlid;

		// Token: 0x0400005C RID: 92
		private readonly int[] _externalColIndices;

		// Token: 0x0400005D RID: 93
		private readonly string mappingString;

		// Token: 0x0400005E RID: 94
		private readonly DataRow _dynamicControlRow;

		// Token: 0x0400005F RID: 95
		private readonly int _controlCode;

		// Token: 0x04000060 RID: 96
		private const int ColPersonId = 2;

		// Token: 0x04000061 RID: 97
		private const int ColControlId = 3;

		// Token: 0x04000062 RID: 98
		private const int ColAction = 4;

		// Token: 0x04000063 RID: 99
		private const int ColControlValueInt = 5;

		// Token: 0x04000064 RID: 100
		private const int ColControlValueBytes = 6;

		// Token: 0x04000065 RID: 101
		private const int ColControlValueDateTime = 7;

		// Token: 0x04000066 RID: 102
		private const int ColNote = 8;

		// Token: 0x04000067 RID: 103
		private const int ColControlValueImage = 10;
	}
}

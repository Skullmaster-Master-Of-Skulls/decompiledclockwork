using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000079 RID: 121
	public class DynamicData
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00021200 File Offset: 0x00020200
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00021218 File Offset: 0x00020218
		public int ScreenNum
		{
			get
			{
				return this.screenNum;
			}
			set
			{
				this.screenNum = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00021224 File Offset: 0x00020224
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0002123C File Offset: 0x0002023C
		public int PersonId
		{
			get
			{
				return this.personId;
			}
			set
			{
				this.personId = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00021248 File Offset: 0x00020248
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00021260 File Offset: 0x00020260
		public int AppointmentId
		{
			get
			{
				return this.appointmentId;
			}
			set
			{
				this.appointmentId = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0002126C File Offset: 0x0002026C
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00021284 File Offset: 0x00020284
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
			set
			{
				this.dataSet = value;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002128E File Offset: 0x0002028E
		public DynamicData(int personId, int appointmentId, int screenNum)
		{
			this.Init(personId, appointmentId, screenNum);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000212A3 File Offset: 0x000202A3
		private void Init(int personId, int appointmentId, int screenNum)
		{
			this.personId = personId;
			this.appointmentId = appointmentId;
			this.screenNum = screenNum;
			this.dataSet = new DataSet();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000212C8 File Offset: 0x000202C8
		public object GetControlValue(int controlid)
		{
			object result;
			if (this.dataSet == null)
			{
				result = null;
			}
			else
			{
				foreach (object obj in this.dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						if (dataRow.RowState != DataRowState.Deleted)
						{
							int num = (int)dataRow["controlid"];
							if (num == controlid)
							{
								return dataRow["controlvalue"];
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000213F4 File Offset: 0x000203F4
		public static bool AnyChangesInDataSet(DataSet data, int newAppId)
		{
			bool result = false;
			foreach (object obj in data.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (dataRow.RowState == DataRowState.Added || dataRow.RowState == DataRowState.Modified)
					{
						dataRow["appointmentid"] = newAppId;
						result = true;
					}
					else if (dataRow.RowState == DataRowState.Deleted)
					{
						dataRow.RejectChanges();
						dataRow["appointmentid"] = newAppId;
						dataRow.Delete();
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00021534 File Offset: 0x00020534
		public static int SaveDataPS(UnivDataAdapter da, DataTable t, string tableName, int screenNum, int studentPid, int whoModifiedPid, out Exception exception)
		{
			return DynamicData.SaveDataPS(da, t, tableName, screenNum, studentPid, whoModifiedPid, out exception, true);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00021558 File Offset: 0x00020558
		public static int SaveDataPS(UnivDataAdapter da, DataTable t, string tableName, int studentPid, int whoModifiedPid, out Exception exception)
		{
			return DynamicData.SaveDataPS(da, t, tableName, -1, studentPid, whoModifiedPid, out exception, false);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0002157C File Offset: 0x0002057C
		public static int SaveDataPS(UnivDataAdapter da, DataTable t, string tableName, int screenNum, int studentPid, int whoModifiedPid, out Exception exception, bool tablesStoreScreenNum)
		{
			int num = 0;
			string commandText;
			if (tablesStoreScreenNum)
			{
				commandText = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE personid=@personid AND controlid=@controlid)\r\nBEGIN\r\n    INSERT INTO {0}{1} (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) SELECT getdate(),@whoami,1,personid,controlid,controlvalue FROM {0} WHERE personid=@personid AND controlid=@controlid\r\n    UPDATE {0} SET controlvalue=@controlvalue WHERE personid=@personid AND controlid=@controlid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO {0}{1} (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) VALUES (getdate(),@whoami,0,@personid,@controlid,NULL)\r\n    INSERT INTO {0} (screennum,personid,controlid,controlvalue) VALUES (@screennum,@personid,@controlid,@controlvalue)\r\nEND", tableName, "archive");
			}
			else
			{
				commandText = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE personid=@personid AND controlid=@controlid)\r\nBEGIN\r\n    INSERT INTO {0}{1} (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) SELECT getdate(),@whoami,1,personid,controlid,controlvalue FROM {0} WHERE personid=@personid AND controlid=@controlid\r\n    UPDATE {0} SET controlvalue=@controlvalue WHERE personid=@personid AND controlid=@controlid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO {0}{1} (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) VALUES (getdate(),@whoami,0,@personid,@controlid,NULL)\r\n    INSERT INTO {0} (personid,controlid,controlvalue) VALUES (@personid,@controlid,@controlvalue)\r\nEND", tableName, "archive");
			}
			string commandText2 = string.Format("INSERT INTO {0}{1} (dateentered,whoentered,wasdeleted,personid,controlid,oldcontrolvalue) SELECT getdate(),@whoami,1,personid,controlid,controlvalue FROM {0} WHERE personid=@personid AND controlid=@controlid\r\nDELETE FROM {0} WHERE personid=@personid AND controlid=@controlid", tableName, "archive");
			try
			{
				try
				{
					da.Connection.Open();
				}
				catch
				{
					try
					{
						da.Connection.Close();
						da.Connection.Open();
					}
					catch (Exception ex)
					{
						exception = ex;
						return 0;
					}
				}
				if (t != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in t.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						if (dataRow.RowState == DataRowState.Added)
						{
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							if (tablesStoreScreenNum)
							{
								da.SelectCommand.Parameters.Add("@screennum", screenNum);
							}
							da.SelectCommand.Parameters.Add("@personid", studentPid);
							da.SelectCommand.Parameters.Add("@controlid", dataRow["controlid"]);
							da.SelectCommand.Parameters.Add("@controlvalue", dataRow["controlvalue"]);
							da.SelectCommand.Parameters.Add("@whoami", whoModifiedPid);
							string text;
							da.SelectCommand.ExecuteNonQuery2(out text);
							if (string.IsNullOrEmpty(text))
							{
								num++;
							}
							else
							{
								stringBuilder.AppendFormat("{0}\r\n", text);
							}
						}
						else if (dataRow.RowState == DataRowState.Deleted)
						{
							dataRow.RejectChanges();
							da.SelectCommand.CommandText = commandText2;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@screennum", screenNum);
							da.SelectCommand.Parameters.Add("@personid", studentPid);
							da.SelectCommand.Parameters.Add("@controlid", dataRow["controlid"]);
							da.SelectCommand.Parameters.Add("@whoami", whoModifiedPid);
							string text;
							da.SelectCommand.ExecuteNonQuery2(out text);
							dataRow.Delete();
							if (string.IsNullOrEmpty(text))
							{
								num++;
							}
							else
							{
								stringBuilder.AppendFormat("{0}\r\n", text);
							}
						}
						else if (dataRow.RowState == DataRowState.Modified)
						{
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@controlvalue", dataRow["controlvalue"]);
							da.SelectCommand.Parameters.Add("@personid", studentPid);
							da.SelectCommand.Parameters.Add("@controlid", dataRow["controlid"]);
							da.SelectCommand.Parameters.Add("@whoami", whoModifiedPid);
							if (tablesStoreScreenNum)
							{
								da.SelectCommand.Parameters.Add("@screennum", screenNum);
							}
							string text;
							da.SelectCommand.ExecuteNonQuery2(out text);
							if (string.IsNullOrEmpty(text))
							{
								num++;
							}
							else
							{
								stringBuilder.AppendFormat("{0}\r\n", text);
							}
						}
					}
					string text2 = stringBuilder.ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						MessageBox.Show("Something went wrong, your data may not have been saved correctly (" + text2 + ")");
						exception = new Exception(text2);
						return num;
					}
					t.AcceptChanges();
				}
				exception = null;
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			finally
			{
				da.Connection.Close();
			}
			return num;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00021A88 File Offset: 0x00020A88
		public static void LogDataChange(UnivDataAdapter da, bool deleteOldLogData, int screenNum, int studentPid, int whoModifiedPid)
		{
			da.SelectCommand.CommandText = "INSERT INTO screendata (screennum,personid,datemodified,whomodified) VALUES (@screennum,@personid,@datemodified,@whomodified)";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			da.SelectCommand.Parameters.Add("@personid", studentPid);
			da.SelectCommand.Parameters.Add("@datemodified", DateTime.Now);
			da.SelectCommand.Parameters.Add("@whomodified", whoModifiedPid);
			da.Fill(new DataTable());
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.NewPerStudentDataScreenRememberSchoolYearSnapshots);
			if (flag)
			{
				object[] yearStartEnd = ClockWorkCore.GetYearStartEnd(da);
				DateTime dateTime;
				if (yearStartEnd != null)
				{
					dateTime = (DateTime)yearStartEnd[0];
				}
				else
				{
					dateTime = DateTime.Now.Date;
				}
				DateTime dateTime2 = new DateTime(dateTime.Year, 1, 1);
				string text = "DELETE FROM @archive WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum) AND dateentered=@dateentered";
				da.SelectCommand.CommandText = text.Replace("@archive", "archive_otherinfops");
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@pid", studentPid);
				da.SelectCommand.Parameters.Add("@screennum", screenNum);
				da.SelectCommand.Parameters.Add("@dateentered", dateTime2);
				string text2;
				da.Fill(new DataTable(), out text2);
				da.SelectCommand.CommandText = text.Replace("@archive", "archive_maininfops");
				da.Fill(new DataTable(), out text2);
				da.SelectCommand.CommandText = text.Replace("@archive", "archive_datetimeinfops");
				da.Fill(new DataTable(), out text2);
				text = "INSERT INTO @archive (personid,controlid,controlvalue,dateentered,whoentered) SELECT personid,controlid,controlvalue,@dateentered,@whoentered FROM @archive2 WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)";
				da.SelectCommand.CommandText = text.Replace("@archive2", "otherinfops").Replace("@archive", "archive_otherinfops");
				da.SelectCommand.Parameters.Add("@whoentered", whoModifiedPid);
				da.Fill(new DataTable());
				da.SelectCommand.CommandText = text.Replace("@archive2", "maininfops").Replace("@archive", "archive_maininfops");
				da.Fill(new DataTable());
				da.SelectCommand.CommandText = text.Replace("@archive2", "datetimeinfops").Replace("@archive", "archive_datetimeinfops");
				da.Fill(new DataTable());
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00021D46 File Offset: 0x00020D46
		public static void SaveDataPAPM(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int personid, DataTable t, string tableName, ref ArrayList changedAppIDs)
		{
			DynamicData.SaveDataPAPM(da, tripleDES, screenNum, personid, t, tableName, ref changedAppIDs, true);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00021D5A File Offset: 0x00020D5A
		public static void SaveDataPAPM(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int personid, DataTable t, string tableName, ref ArrayList changedAppIDs)
		{
			DynamicData.SaveDataPAPM(da, tripleDES, -1, personid, t, tableName, ref changedAppIDs, false);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00021D70 File Offset: 0x00020D70
		public static void SaveDataPAPM(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int personid, DataTable t, string tableName, ref ArrayList changedAppIDs, bool tableUsesScreenNum)
		{
			DynamicData.SaveDataPAPM(da, tripleDES, -1, personid, t, tableName, ref changedAppIDs, tableUsesScreenNum, false);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00021D94 File Offset: 0x00020D94
		public static void SaveDataPAPM(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int personid, DataTable t, string tableName, ref ArrayList changedAppIDs, bool tableUsesScreenNum, bool ignoreZeroPersonId)
		{
			if (t != null)
			{
				try
				{
					if (!ignoreZeroPersonId && personid < 1)
					{
						MessageBox.Show("Warning: Your notes may not have saved correctly (personid<1)");
					}
					string commandText;
					if (tableUsesScreenNum)
					{
						commandText = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE controlid=@controlid AND personid=@personid AND appointmentid=@appid)\r\n    UPDATE {0} SET controlvalue=@controlvalue WHERE controlid=@controlid AND personid=@personid AND appointmentid=@appid\r\nELSE\r\n    INSERT INTO {0} (screennum,personid,controlid,controlvalue,appointmentid) VALUES (@screennum,@personid,@controlid,@controlvalue,@appid)", tableName);
					}
					else
					{
						commandText = string.Format("IF EXISTS(SELECT dataid FROM {0} WHERE controlid=@controlid AND personid=@personid AND appointmentid=@appid)\r\n    UPDATE {0} SET controlvalue=@controlvalue WHERE controlid=@controlid AND personid=@personid AND appointmentid=@appid\r\nELSE\r\n    INSERT INTO {0} (personid,controlid,controlvalue,appointmentid) VALUES (@personid,@controlid,@controlvalue,@appid)", tableName);
					}
					da.Connection.Open();
					foreach (object obj in t.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string text = null;
						if (dataRow.RowState != DataRowState.Deleted)
						{
							int num = (dataRow["personid"] == DBNull.Value) ? 0 : ((int)dataRow["personid"]);
							int num2 = (dataRow["appointmentid"] == DBNull.Value) ? 0 : ((int)dataRow["appointmentid"]);
							if (!ignoreZeroPersonId && num < 1)
							{
								MessageBox.Show("Missing personid");
							}
							else if (num2 < 1)
							{
								MessageBox.Show("Missing appointmentid");
							}
						}
						if (dataRow.RowState == DataRowState.Added)
						{
							int num3 = (int)dataRow["controlid"];
							int num4 = (int)dataRow["appointmentid"];
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@personid", personid);
							da.SelectCommand.Parameters.Add("@controlid", num3);
							da.SelectCommand.Parameters.Add("@controlvalue", dataRow["controlvalue"]);
							da.SelectCommand.Parameters.Add("@appid", num4);
							if (tableUsesScreenNum)
							{
								da.SelectCommand.Parameters.Add("@screennum", screenNum);
							}
							da.SelectCommand.ExecuteNonQuery(out text);
							changedAppIDs.Add(new Point((int)dataRow["appointmentid"], 1));
						}
						else if (dataRow.RowState == DataRowState.Deleted)
						{
							dataRow.RejectChanges();
							da.SelectCommand.CommandText = "DELETE FROM " + tableName + " WHERE controlid=@controlid AND personid=@personid AND appointmentid=@appid";
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@personid", personid);
							da.SelectCommand.Parameters.Add("@controlid", dataRow["controlid"]);
							da.SelectCommand.Parameters.Add("@appid", dataRow["appointmentid"]);
							da.SelectCommand.ExecuteNonQuery(out text);
							changedAppIDs.Add(new Point((int)dataRow["appointmentid"], 3));
							dataRow.Delete();
						}
						else if (dataRow.RowState == DataRowState.Modified)
						{
							dataRow.AcceptChanges();
							da.SelectCommand.CommandText = commandText;
							int num3 = (int)dataRow["controlid"];
							int num4 = (int)dataRow["appointmentid"];
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@controlvalue", dataRow["controlvalue"]);
							da.SelectCommand.Parameters.Add("@dataid", dataRow["dataid"]);
							da.SelectCommand.Parameters.Add("@personid", personid);
							da.SelectCommand.Parameters.Add("@controlid", num3);
							da.SelectCommand.Parameters.Add("@appid", num4);
							if (tableUsesScreenNum)
							{
								da.SelectCommand.Parameters.Add("@screennum", screenNum);
							}
							int num5 = da.SelectCommand.ExecuteNonQuery(out text);
							changedAppIDs.Add(new Point((int)dataRow["appointmentid"], 2));
						}
						else
						{
							changedAppIDs.Add(new Point((int)dataRow["appointmentid"], 4));
						}
						if (text != null && text.Length > 0)
						{
							MessageBox.Show("Something went wrong - your data may not have been saved correctly: " + text);
						}
					}
					t.AcceptChanges();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
				finally
				{
					da.Connection.Close();
				}
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00022340 File Offset: 0x00021340
		public static void AddDynamicDataColumnsToTable(ref DataTable t)
		{
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow["controlcaption"].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					string newColumnName = DynamicData.GetNewColumnName(text);
					if (!t.Columns.Contains(newColumnName))
					{
						int num = (dataRow["controlcode"] == DBNull.Value) ? 0 : ((int)dataRow["controlcode"]);
						if (num == 2 || num == 700)
						{
							t.Columns.Add(newColumnName, typeof(bool));
						}
						else if (num == 6)
						{
							t.Columns.Add(newColumnName, typeof(DateTime));
						}
						else
						{
							t.Columns.Add(newColumnName);
						}
					}
				}
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00022484 File Offset: 0x00021484
		private static string GetNewColumnName(string controlCaption)
		{
			int num = controlCaption.IndexOf("~~");
			if (num > 0)
			{
				controlCaption = controlCaption.Substring(0, num);
			}
			return Regex.Replace(controlCaption, "[^\\w_]", "");
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000224CC File Offset: 0x000214CC
		private static string GetNewColumnNameCheckExistsInTable(string controlCaption, DataTable t)
		{
			string newColumnName = DynamicData.GetNewColumnName(controlCaption);
			for (int i = 0; i < 1000; i++)
			{
				if (i == 0)
				{
					if (!t.Columns.Contains(newColumnName))
					{
						return newColumnName;
					}
				}
				else
				{
					string text = string.Format("{0}_{1}", newColumnName, i.ToString());
					if (!t.Columns.Contains(text))
					{
						return text;
					}
				}
			}
			return newColumnName;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00022550 File Offset: 0x00021550
		public static DataTable PivotTableForDynamicDataByPersonAndAppointmentId(DataTable table, TripleDESEncryptionClass tripleDES, bool removeDynamicDataColumns_valbytes_valtext_controlid_etc)
		{
			DynamicData.AddDynamicDataColumnsToTable(ref table);
			DataTable dataTable = table.Clone();
			RichTextBox richTextBox = null;
			int i = 0;
			DataView dataView = new DataView(table);
			dataView.Sort = "personid,appointmentid";
			while (i < dataView.Count)
			{
				DataRow row = dataView[i].Row;
				int num = (row["personid"] == DBNull.Value) ? 0 : ((int)row["personid"]);
				int num2 = (row["appointmentid"] == DBNull.Value) ? 0 : ((int)row["appointmentid"]);
				int j;
				for (j = i; j < dataView.Count; j++)
				{
					try
					{
						DataRow row2 = dataView[j].Row;
						int num3 = (row2["personid"] == DBNull.Value) ? 0 : ((int)row2["personid"]);
						int num4 = (row2["appointmentid"] == DBNull.Value) ? 0 : ((int)row2["appointmentid"]);
						if (num3 != num || num2 != num4)
						{
							break;
						}
						if (row2.RowState != DataRowState.Deleted && row2["controlid"] != DBNull.Value)
						{
							bool flag = row2["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(row2["valbytesisencrypted"]);
							byte[] array = (row2["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])row2["valbytes"]);
							byte[] array2 = (row2["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])row2["valimage"]);
							string controlCaption = row2["controlcaption"].ToString();
							string newColumnName = DynamicData.GetNewColumnName(controlCaption);
							int num5 = (row2["controlcode"] == DBNull.Value) ? 0 : ((int)row2["controlcode"]);
							if (num5 == 1)
							{
								if (flag && array.Length > 0)
								{
									row[newColumnName] = ClockWorkCore.BytesToString(array, true, tripleDES);
								}
								else
								{
									row[newColumnName] = row2["valtext"].ToString();
								}
							}
							else if (array2.Length > 0)
							{
								string text = tripleDES.Decrypt(array2);
								if (text.StartsWith("{\\rtf"))
								{
									if (richTextBox == null)
									{
										richTextBox = new RichTextBox();
									}
									richTextBox.Rtf = text;
									text = richTextBox.Text;
								}
								if (row.Table.Columns[newColumnName].DataType == typeof(bool))
								{
									row[newColumnName] = !string.IsNullOrEmpty(text);
								}
								else
								{
									row[newColumnName] = text;
								}
							}
							else if (flag && array.Length > 0)
							{
								string text = tripleDES.Decrypt(array);
								if (row.Table.Columns[newColumnName].DataType == typeof(bool))
								{
									row[newColumnName] = text;
								}
							}
							else if (dataTable.Columns[newColumnName].DataType == typeof(bool))
							{
								row[newColumnName] = (row2["valint"] != DBNull.Value && (int)row2["valint"] != 0);
							}
							else if (dataTable.Columns[newColumnName].DataType == typeof(DateTime))
							{
								DateTime? dateTime = (row2[newColumnName] == DBNull.Value) ? null : ((DateTime?)row2[newColumnName]);
								if (dateTime != null)
								{
									row[newColumnName] = dateTime.Value;
								}
							}
							else if (row.Table.Columns[newColumnName].DataType == typeof(bool))
							{
								row[newColumnName] = (row2["valint"] != DBNull.Value && (int)row2["valint"] != 0);
							}
							else
							{
								row[newColumnName] = row2["valtext"].ToString();
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
				dataTable.ImportRow(row);
				i = j;
			}
			if (richTextBox != null)
			{
				richTextBox.Dispose();
				richTextBox = null;
			}
			if (removeDynamicDataColumns_valbytes_valtext_controlid_etc)
			{
				string[] array3 = new string[]
				{
					"controlid",
					"controlcode",
					"controlcaption",
					"valtext",
					"valint",
					"valdate",
					"valimage",
					"valbytes",
					"valbytesisencrypted",
					"dataid",
					"setting1",
					"setting2",
					"setting3",
					"setting4",
					"setting4string",
					"defaultvalue"
				};
				foreach (string name in array3)
				{
					if (dataTable.Columns.Contains(name))
					{
						dataTable.Columns.Remove(name);
					}
				}
			}
			return dataTable;
		}

		// Token: 0x04000327 RID: 807
		private int personId;

		// Token: 0x04000328 RID: 808
		private int appointmentId;

		// Token: 0x04000329 RID: 809
		private int screenNum;

		// Token: 0x0400032A RID: 810
		private DataSet dataSet;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DynamicScreens;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.ClientManager.OldUserSettings;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000019 RID: 25
	public class DataImportRule
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00036E30 File Offset: 0x00035E30
		public int[] ExternalColIndices
		{
			get
			{
				return this.externalColIndices;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00036E48 File Offset: 0x00035E48
		public int Controlid
		{
			get
			{
				return this.controlid;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00036E60 File Offset: 0x00035E60
		public string MappingString
		{
			get
			{
				return this.mappingString;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00036EB4 File Offset: 0x00035EB4
		private string OnlyKeepCharsDigitsSpace(string s)
		{
			string result;
			if (string.IsNullOrEmpty(s))
			{
				result = "";
			}
			else
			{
				IEnumerable<char> source = Enumerable.Where<char>(s.ToCharArray().ToList<char>(), (char g) => char.IsLetterOrDigit(g) || g == ' ');
				result = string.Join("", source.ToList<char>().ConvertAll<string>((char g) => g.ToString()).ToArray());
			}
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00036F44 File Offset: 0x00035F44
		public DataImportRule(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DataTable externalTable, string str)
		{
			this.da = da;
			this.tripleDES = tripleDES;
			int num = str.IndexOf("=");
			if (num > 0)
			{
				string s = str.Substring(0, num);
				string text = str.Substring(num + 1);
				try
				{
					this.controlid = int.Parse(s);
				}
				catch
				{
					this.controlid = -1;
				}
				string[] array = text.Split(new char[]
				{
					','
				});
				this.externalColIndices = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = externalTable.Columns.IndexOf(array[i]);
					this.externalColIndices[i] = num2;
					if (num2 < 0)
					{
						string text2 = this.OnlyKeepCharsDigitsSpace(array[i]);
						if (text2.Length > 0)
						{
							foreach (object obj in externalTable.Columns)
							{
								DataColumn dataColumn = (DataColumn)obj;
								string value = this.OnlyKeepCharsDigitsSpace(dataColumn.ColumnName);
								if (text2.Equals(value, StringComparison.OrdinalIgnoreCase))
								{
									num2 = externalTable.Columns.IndexOf(dataColumn);
								}
							}
						}
					}
					if (num2 < 0)
					{
						throw new Exception("Can't find a column name: " + array[i] + "; " + text);
					}
					this.Log("Resolved '" + array[i] + "' to index=" + this.externalColIndices[i].ToString());
				}
				bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, da.availableFeatures, da.unavailableFeatures, DatabaseVersionManager.ClockWorkFeature.DynamicScreenControlExtendedDescriptionFields_Mar_07);
				da.SelectCommand.CommandText = "SELECT dc.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,dc.p,dc.statsholding";
				if (flag)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += ",dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers,dc.defaultvaluestring,dc.setting4string,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline";
				}
				else
				{
					UnivCommand selectCommand2 = da.SelectCommand;
					selectCommand2.CommandText += ",'' AS controlname,'' AS controlgroup,'' AS helptext,1 AS helptextdisplaymethod,'' AS mask,0 AS enforce,'' AS actionhandlers,'' AS defaultvaluestring,'' AS setting4string,1 AS enabled,0 AS readonly,0 AS hidecaption,0 AS setting4,0 AS fontsize,0 AS dontwraptonextline";
				}
				UnivCommand selectCommand3 = da.SelectCommand;
				selectCommand3.CommandText = selectCommand3.CommandText + " FROM dynamiccontrols dc WHERE dc.controlid=" + this.controlid.ToString();
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					this.dynamicControlRow = dataTable.Rows[0];
					this.controlCode = (int)this.dynamicControlRow[1];
				}
				else
				{
					this.dynamicControlRow = null;
					this.controlCode = -1;
				}
				return;
			}
			throw new Exception("Missing equals operator: " + str);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0003724C File Offset: 0x0003624C
		private DataRow FindRowToImport(DataTable rowsToImport, int personid, int controlid)
		{
			foreach (object obj in rowsToImport.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[2];
				if (num == personid)
				{
					int num2 = (int)dataRow[3];
					if (num2 == controlid)
					{
						return dataRow;
					}
				}
			}
			return null;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000372F8 File Offset: 0x000362F8
		private void Log(string s)
		{
			ReportFunction.Log(s);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00037304 File Offset: 0x00036304
		private string DataTableToString(DataTable t)
		{
			return ReportFunction.DataTableToString(t);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0003731C File Offset: 0x0003631C
		public void Map(DataRow externalRow, int personid, ref DataTable rowsToImport, DataTable originalExternalRowsTable)
		{
			this.Log(string.Concat(new string[]
			{
				"Map begin: pid=",
				personid.ToString(),
				Environment.NewLine,
				"rowstoimport:",
				Environment.NewLine,
				this.DataTableToString(rowsToImport),
				Environment.NewLine,
				Environment.NewLine,
				"originalExternalRowsTable:",
				Environment.NewLine,
				this.DataTableToString(originalExternalRowsTable)
			}));
			if (this.externalColIndices == null || this.externalColIndices.Length < 1)
			{
				this.Log("return; externalcolindices.length < 1");
				throw new Exception("Something went wrong with controlid=" + this.controlid.ToString());
			}
			int num = this.externalColIndices[0];
			if (num < 0)
			{
				this.Log("externalColIndices[ 0 ] < 0!");
			}
			string text = externalRow[num].ToString().Trim();
			this.Log("Checking for controlcode=" + this.controlCode.ToString());
			int num2 = this.controlCode;
			switch (num2)
			{
			case 1:
			case 11:
			{
				this.Log("_textBox / _myTextBox");
				this.da.SelectCommand.CommandText = "SELECT controlvalue FROM otherinfops WHERE personid=" + personid.ToString() + " AND controlid=" + this.controlid.ToString();
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				int num3 = (int)this.dynamicControlRow["setting3"];
				string text2;
				if (dataTable.Rows.Count > 0)
				{
					text2 = ((dataTable.Rows[0][0] == DBNull.Value) ? "" : DynamicScreen.BytesToString((byte[])dataTable.Rows[0][0], num3 == 1, this.tripleDES));
				}
				else
				{
					text2 = "";
				}
				text2 = text2.ToLower().Trim();
				string text3 = text.ToLower();
				this.Log("oldtext=" + text2);
				this.Log(string.Concat(new string[]
				{
					"oldtext=",
					text2,
					"; t:",
					Environment.NewLine,
					this.DataTableToString(dataTable)
				}));
				this.Log("newtext=" + text3);
				if (text2.CompareTo(text3) != 0)
				{
					DataRow dataRow = rowsToImport.NewRow();
					dataRow[2] = personid;
					dataRow[3] = this.controlid;
					dataRow[6] = DynamicScreen.StringToBytes(text, num3 == 1, this.tripleDES);
					dataRow[8] = text3;
					if (text2.Length < 1)
					{
						dataRow[4] = "add";
					}
					else if (text3.Length < 1)
					{
						dataRow[4] = "delete";
					}
					else
					{
						dataRow[4] = "modify";
					}
					rowsToImport.Rows.Add(dataRow);
				}
				break;
			}
			case 2:
			case 4:
			case 12:
			{
				this.Log("_checkbox / _myCheckbox / _radioButton");
				bool flag = text.Length >= 1 && text.ToLower()[0] != 'f' && text[0] != '0' && text[0] != '-';
				this.da.SelectCommand.CommandText = "SELECT controlid FROM maininfops WHERE personid=" + personid.ToString() + " AND controlid=" + this.controlid.ToString();
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				this.Log("isChecked=" + flag.ToString());
				this.Log("DataTable t: " + Environment.NewLine + this.DataTableToString(dataTable));
				bool flag2 = dataTable.Rows.Count >= 1;
				if (flag != flag2)
				{
					DataRow dataRow = rowsToImport.NewRow();
					dataRow[2] = personid;
					dataRow[3] = this.controlid;
					dataRow[5] = OldUserSettingClientManager.CurrentInstance.BoolToInt(true);
					if (flag)
					{
						dataRow[4] = "add";
					}
					else
					{
						dataRow[4] = "deletechk";
					}
					rowsToImport.Rows.Add(dataRow);
				}
				break;
			}
			case 3:
			{
				this.Log("_comboBox");
				int num3 = (int)this.dynamicControlRow["setting3"];
				int num4 = (int)this.dynamicControlRow["setting1"];
				if (num3 == 0)
				{
					this.da.SelectCommand.CommandText = string.Concat(new string[]
					{
						"SELECT mi.controlvalue,ll.lookuptext FROM maininfops mi LEFT JOIN lookuplists ll ON ll.lookuplistid=mi.controlvalue WHERE mi.personid=",
						personid.ToString(),
						" AND mi.controlid=",
						this.controlid.ToString(),
						" AND ll.lookupgroupid=",
						num4.ToString()
					});
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					string text2;
					if (dataTable.Rows.Count > 0)
					{
						text2 = dataTable.Rows[0][1].ToString();
					}
					else
					{
						text2 = "";
					}
					string text3 = text.ToLower().Trim();
					text2 = text2.ToLower().Trim();
					int num5;
					if (text3.Length > 0)
					{
						this.da.SelectCommand.CommandText = "SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgid AND lookuptext=@txt";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@txt", text3);
						this.da.SelectCommand.Parameters.Add("@lgid", num4);
						dataTable = new DataTable();
						this.da.Fill(dataTable);
						if (dataTable.Rows.Count > 0)
						{
							num5 = (int)dataTable.Rows[0][0];
						}
						else
						{
							num5 = -1;
							text3 = "";
						}
					}
					else
					{
						num5 = -1;
					}
					if (text2.CompareTo(text3) != 0)
					{
						DataRow dataRow = rowsToImport.NewRow();
						dataRow[2] = personid;
						dataRow[3] = this.controlid;
						dataRow[8] = text3;
						if (text3.Length < 1)
						{
							dataRow[5] = -1;
							dataRow[4] = "delete";
						}
						else if (text2.Length < 1)
						{
							dataRow[5] = num5;
							dataRow[4] = "add";
						}
						else
						{
							dataRow[4] = "modify";
							dataRow[5] = num5;
						}
						rowsToImport.Rows.Add(dataRow);
					}
				}
				else
				{
					int num6 = (int)this.dynamicControlRow["setting2"];
					this.da.SelectCommand.CommandText = "SELECT controlvalue FROM otherinfops WHERE personid=" + personid.ToString() + " AND controlid=" + this.controlid.ToString();
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					string text2;
					if (dataTable.Rows.Count > 0)
					{
						text2 = ((dataTable.Rows[0][0] == DBNull.Value) ? "" : DynamicScreen.BytesToString((byte[])dataTable.Rows[0][0], num3 == -1, this.tripleDES));
					}
					else
					{
						text2 = "";
					}
					text2 = text2.ToLower().Trim();
					string text3 = text.ToLower().Trim();
					if (text2.CompareTo(text3) != 0)
					{
						DataRow dataRow = rowsToImport.NewRow();
						dataRow[2] = personid;
						dataRow[3] = this.controlid;
						dataRow[6] = DynamicScreen.StringToBytes(text3, num3 == -1, this.tripleDES);
						dataRow[8] = text3;
						if (text2.Length < 1)
						{
							dataRow[4] = "add";
						}
						else if (text3.Length < 1)
						{
							dataRow[4] = "delete";
						}
						else
						{
							dataRow[4] = "modify";
						}
						rowsToImport.Rows.Add(dataRow);
					}
				}
				break;
			}
			case 5:
			case 7:
			case 8:
			case 9:
			case 13:
				break;
			case 6:
			{
				this.Log("_date");
				DateTime dateTime;
				if (text.Length > 0)
				{
					try
					{
						dateTime = DateTime.Parse(text);
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
				this.da.SelectCommand.CommandText = "SELECT controlvalue FROM datetimeinfops WHERE personid=" + personid.ToString() + " AND controlid=" + this.controlid.ToString();
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				DateTime d;
				if (dataTable.Rows.Count > 0)
				{
					d = ((dataTable.Rows[0][0] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataTable.Rows[0][0]));
				}
				else
				{
					d = DateTime.MinValue;
				}
				if (d != dateTime)
				{
					DataRow dataRow = rowsToImport.NewRow();
					dataRow[2] = personid;
					dataRow[3] = this.controlid;
					dataRow[7] = dateTime;
					if (d == DateTime.MinValue)
					{
						dataRow[4] = "add";
					}
					else if (dateTime == DateTime.MinValue)
					{
						dataRow[4] = "delete";
					}
					else
					{
						dataRow[4] = "modify";
					}
					rowsToImport.Rows.Add(dataRow);
				}
				break;
			}
			case 10:
			{
				this.Log("BEGIN _listView");
				this.Log(string.Concat(new string[]
				{
					"Looking for alreadyImportedRow: pid: ",
					personid.ToString(),
					"; cid: ",
					this.controlid.ToString(),
					" ..."
				}));
				DataRow dataRow2 = this.FindRowToImport(rowsToImport, personid, this.controlid);
				this.Log(("RESULT: " + dataRow2 == null) ? "NULL" : "Not null");
				if (dataRow2 == null)
				{
					string text4 = "";
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < originalExternalRowsTable.Rows.Count; i++)
					{
						DataRow dataRow3 = originalExternalRowsTable.Rows[i];
						if (dataRow3.RowState != DataRowState.Deleted)
						{
							int num7 = (int)dataRow3["pid"];
							if (num7 == personid)
							{
								bool flag3 = true;
								string text5 = "";
								string text6 = "";
								for (int j = 0; j < this.externalColIndices.Length; j++)
								{
									if (j > 0)
									{
										text5 += '\0';
									}
									string text7 = dataRow3[this.externalColIndices[j]].ToString();
									text5 += text7;
									if (flag3 && text7.Trim().Length > 0)
									{
										flag3 = false;
									}
									if (j == this.externalColIndices.Length - 1)
									{
										text6 = text7.Trim().ToLower();
									}
								}
								if (!flag3 && !arrayList.Contains(text6))
								{
									arrayList.Add(text6);
									if (text4.Length > 0)
									{
										text4 += '\t';
									}
									text4 += text5;
								}
							}
						}
					}
					this.Log("newLvRow: " + text4);
					this.da.SelectCommand.CommandText = "SELECT controlvalue FROM otherinfops WHERE personid=" + personid.ToString() + " AND controlid=" + this.controlid.ToString();
					DataTable dataTable = new DataTable();
					string text8;
					this.da.Fill(dataTable, out text8);
					this.Log("Merge in rows: " + dataTable.Rows.Count.ToString() + ((text8 != null && text8.Length > 0) ? ("; errmsg=" + text8) : ""));
					string text9;
					if (dataTable.Rows.Count > 0)
					{
						text9 = ((dataTable.Rows[0][0] == DBNull.Value) ? "" : DynamicScreen.BytesToString((byte[])dataTable.Rows[0][0], false, this.tripleDES));
					}
					else
					{
						text9 = "";
					}
					this.Log("oldLvAll: " + text9);
					if (text9.Length > 0)
					{
						string[] array = text9.Split(new char[]
						{
							'\t'
						});
						foreach (string text5 in array)
						{
							string text10 = text5;
							char[] separator = new char[1];
							string[] array3 = text10.Split(separator);
							if (array3.Length > 0)
							{
								string strB = array3[array3.Length - 1].Trim().ToLower();
								bool flag4 = false;
								foreach (object obj in arrayList)
								{
									string text11 = (string)obj;
									if (text11.CompareTo(strB) == 0)
									{
										flag4 = true;
										break;
									}
								}
								if (!flag4)
								{
									if (text4.Length > 0)
									{
										text4 += '\t';
									}
									text4 += text5;
								}
							}
						}
					}
					this.Log("newLvRow2: " + text4);
					if (text4.ToLower().Trim().CompareTo(text9.ToLower().Trim()) != 0)
					{
						DataRow dataRow = rowsToImport.NewRow();
						dataRow[2] = personid;
						dataRow[3] = this.controlid;
						if (text9.Length < 1)
						{
							dataRow[6] = DynamicScreen.StringToBytes(text4, false, this.tripleDES);
							dataRow[4] = "add";
						}
						else
						{
							dataRow[6] = DynamicScreen.StringToBytes(text4, false, this.tripleDES);
							dataRow[4] = "modify";
						}
						rowsToImport.Rows.Add(dataRow);
					}
				}
				this.Log("END _listView");
				break;
			}
			case 14:
			{
				this.Log("_radioGroup");
				int num8 = (int)this.dynamicControlRow["setting1"];
				this.da.SelectCommand.CommandText = string.Concat(new string[]
				{
					"SELECT mi.controlvalue,ll.lookuptext FROM maininfops mi LEFT JOIN lookuplists ll ON ll.lookuplistid=mi.controlvalue WHERE mi.personid=",
					personid.ToString(),
					" AND mi.controlid=",
					this.controlid.ToString(),
					" AND ll.lookupgroupid=",
					num8.ToString()
				});
				DataTable dataTable = new DataTable();
				this.da.Fill(dataTable);
				string text2;
				if (dataTable.Rows.Count > 0)
				{
					text2 = dataTable.Rows[0][1].ToString();
				}
				else
				{
					text2 = "";
				}
				string text3 = text.ToLower().Trim();
				text2 = text2.ToLower().Trim();
				int num5;
				if (text3.Length > 0)
				{
					this.da.SelectCommand.CommandText = "SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgid AND lookuptext=@txt";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@txt", text3);
					this.da.SelectCommand.Parameters.Add("@lgid", num8);
					dataTable = new DataTable();
					this.da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						num5 = (int)dataTable.Rows[0][0];
					}
					else
					{
						num5 = -1;
						text3 = "";
					}
				}
				else
				{
					num5 = -1;
				}
				if (text2.CompareTo(text3) != 0)
				{
					DataRow dataRow = rowsToImport.NewRow();
					dataRow[2] = personid;
					dataRow[3] = this.controlid;
					dataRow[8] = text3;
					if (text3.Length < 1)
					{
						dataRow[5] = -1;
						dataRow[4] = "delete";
					}
					else if (text2.Length < 1)
					{
						dataRow[5] = num5;
						dataRow[4] = "add";
					}
					else
					{
						dataRow[4] = "modify";
						dataRow[5] = num5;
					}
					rowsToImport.Rows.Add(dataRow);
				}
				break;
			}
			default:
				if (num2 == 21 || num2 == 400)
				{
					this.Log("File / Picture");
					try
					{
						if (externalRow[num] != DBNull.Value && externalRow[num] is byte[])
						{
							byte[] array4 = (byte[])externalRow[num];
							if (array4.Length > 0)
							{
								this.da.SelectCommand.CommandText = "SELECT controlvalue FROM imageinfops WHERE personid=" + personid.ToString() + " AND controlid=" + this.controlid.ToString();
								DataTable dataTable = new DataTable();
								this.da.Fill(dataTable);
								string text12 = "";
								if (dataTable.Rows.Count > 0)
								{
									byte[] second = (dataTable.Rows[0][0] is DBNull) ? new byte[0] : ((byte[])dataTable.Rows[0][0]);
									if (array4.SequenceEqual(second))
									{
										text12 = "modify";
									}
								}
								else
								{
									text12 = "add";
								}
								if (text12.Length > 0)
								{
									DataRow dataRow = rowsToImport.NewRow();
									dataRow[2] = personid;
									dataRow[3] = this.controlid;
									dataRow[10] = array4;
									dataRow[4] = text12;
									rowsToImport.Rows.Add(dataRow);
								}
							}
						}
					}
					catch (Exception ex)
					{
						this.Log(ex.ToString());
					}
				}
				break;
			}
		}

		// Token: 0x04000102 RID: 258
		private int controlid;

		// Token: 0x04000103 RID: 259
		private int[] externalColIndices;

		// Token: 0x04000104 RID: 260
		private string mappingString;

		// Token: 0x04000105 RID: 261
		private DataRow dynamicControlRow;

		// Token: 0x04000106 RID: 262
		private UnivDataAdapter da;

		// Token: 0x04000107 RID: 263
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000108 RID: 264
		private int controlCode;
	}
}

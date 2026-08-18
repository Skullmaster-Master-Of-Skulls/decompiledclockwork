using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.ClockWorkSnapshots;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkSnapshot;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.DAO.Impl.ClockWorkSnapshots
{
	// Token: 0x0200010D RID: 269
	public class ClockWorkSnapshotDAO : IClockWorkSnapshotDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0004E8F7 File Offset: 0x0004CAF7
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0004E8FF File Offset: 0x0004CAFF
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060007B2 RID: 1970 RVA: 0x0004E908 File Offset: 0x0004CB08
		public ClockWorkSnapshotDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0004E939 File Offset: 0x0004CB39
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0004E941 File Offset: 0x0004CB41
		public OperationContext OpContext { get; set; }

		// Token: 0x060007B5 RID: 1973 RVA: 0x0004E94C File Offset: 0x0004CB4C
		private List<ClockWorkSnapshotTable> GetTables(eSnapshotDataGroup dataGroups)
		{
			List<ClockWorkSnapshotTable> list = new List<ClockWorkSnapshotTable>();
			foreach (object obj in Enum.GetValues(typeof(eSnapshotDataGroup)))
			{
				eSnapshotDataGroup eSnapshotDataGroup = (eSnapshotDataGroup)obj;
				bool flag = (dataGroups & eSnapshotDataGroup) == eSnapshotDataGroup;
				if (flag)
				{
					eSnapshotDataGroup eSnapshotDataGroup2 = eSnapshotDataGroup;
					eSnapshotDataGroup eSnapshotDataGroup3 = eSnapshotDataGroup2;
					if (eSnapshotDataGroup3 <= eSnapshotDataGroup.Misc)
					{
						if (eSnapshotDataGroup3 <= eSnapshotDataGroup.Availability)
						{
							if (eSnapshotDataGroup3 <= eSnapshotDataGroup.AppointmentShowTimeAs)
							{
								switch (eSnapshotDataGroup3)
								{
								case eSnapshotDataGroup.DynamicForms:
									list.Add(this.GetSnapshotTable("accommodations", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("accommodationsrooms", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("dynamiccontrols", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("dynamicscreencontrols", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("dynamicscreennondatacontrols", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("lookupgroups", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("lookuplists", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("screens", Array.Empty<string>()));
									break;
								case eSnapshotDataGroup.AppointmentCancelled:
									list.Add(this.GetSnapshotTable("appointmentcancelledreason", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("cancelreason", Array.Empty<string>()));
									break;
								case eSnapshotDataGroup.DynamicForms | eSnapshotDataGroup.AppointmentCancelled:
									break;
								case eSnapshotDataGroup.AppointmentIcon:
									list.Add(this.GetSnapshotTable("appointmenticoninfo", Array.Empty<string>()));
									break;
								default:
									if (eSnapshotDataGroup3 == eSnapshotDataGroup.AppointmentShowTimeAs)
									{
										list.Add(this.GetSnapshotTable("appointmentshowtimeas", Array.Empty<string>()));
									}
									break;
								}
							}
							else if (eSnapshotDataGroup3 != eSnapshotDataGroup.AppointmentTypes)
							{
								if (eSnapshotDataGroup3 == eSnapshotDataGroup.Availability)
								{
									list.Add(this.GetSnapshotTable("availabilitygroup", Array.Empty<string>()));
								}
							}
							else
							{
								list.Add(this.GetSnapshotTable("appointmenttypegroups", Array.Empty<string>()));
								list.Add(this.GetSnapshotTable("appointmenttypes", Array.Empty<string>()));
							}
						}
						else if (eSnapshotDataGroup3 <= eSnapshotDataGroup.Templates)
						{
							if (eSnapshotDataGroup3 != eSnapshotDataGroup.Courses)
							{
								if (eSnapshotDataGroup3 == eSnapshotDataGroup.Templates)
								{
									list.Add(this.GetSnapshotTable("emailtemplategroups", Array.Empty<string>()));
									list.Add(this.GetSnapshotTable("emailtemplates", Array.Empty<string>()));
								}
							}
							else
							{
								list.Add(this.GetSnapshotTable("lucoursesessiondate", Array.Empty<string>()));
								list.Add(this.GetSnapshotTable("lutermdurationdates", Array.Empty<string>()));
								list.Add(this.GetSnapshotTable("dateranges", Array.Empty<string>()));
							}
						}
						else if (eSnapshotDataGroup3 != eSnapshotDataGroup.Groups)
						{
							if (eSnapshotDataGroup3 == eSnapshotDataGroup.Misc)
							{
								list.Add(this.GetSnapshotTable("misc", Array.Empty<string>()));
								list.Add(this.GetSnapshotTable("miscsafe", Array.Empty<string>()));
							}
						}
						else
						{
							list.Add(this.GetSnapshotTable("groups", Array.Empty<string>()));
							list.Add(this.GetSnapshotTable("groupscustom", Array.Empty<string>()));
						}
					}
					else if (eSnapshotDataGroup3 <= eSnapshotDataGroup.Permissions)
					{
						if (eSnapshotDataGroup3 <= eSnapshotDataGroup.SettingsGroups)
						{
							if (eSnapshotDataGroup3 != eSnapshotDataGroup.Settings)
							{
								if (eSnapshotDataGroup3 == eSnapshotDataGroup.SettingsGroups)
								{
									list.Add(this.GetSnapshotTable("settingsgroups", Array.Empty<string>()));
								}
							}
							else
							{
								list.Add(this.GetSnapshotTable("settings", Array.Empty<string>()));
							}
						}
						else if (eSnapshotDataGroup3 != eSnapshotDataGroup.WebSettings)
						{
							if (eSnapshotDataGroup3 != eSnapshotDataGroup.Permissions)
							{
							}
						}
						else
						{
							list.Add(this.GetSnapshotTable("websettings2", new string[]
							{
								"settingstringvalue"
							}));
						}
					}
					else if (eSnapshotDataGroup3 <= eSnapshotDataGroup.TestsExams)
					{
						if (eSnapshotDataGroup3 != eSnapshotDataGroup.PermissionsGroups)
						{
							if (eSnapshotDataGroup3 == eSnapshotDataGroup.TestsExams)
							{
								list.Add(this.GetSnapshotTable("examstatuslookup", Array.Empty<string>()));
							}
						}
						else
						{
							list.Add(this.GetSnapshotTable("permissionsgroups", Array.Empty<string>()));
						}
					}
					else if (eSnapshotDataGroup3 != eSnapshotDataGroup.Departments)
					{
						if (eSnapshotDataGroup3 == eSnapshotDataGroup.Surveys)
						{
							list.Add(this.GetSnapshotTable("survey", Array.Empty<string>()));
						}
					}
					else
					{
						list.Add(this.GetSnapshotTable("departmentgroups", Array.Empty<string>()));
						list.Add(this.GetSnapshotTable("departments", Array.Empty<string>()));
					}
				}
			}
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("with Fkeys as (\r\n    select distinct\r\n         OnTable       = OnTable.name\r\n        ,AgainstTable  = AgainstTable.name \r\n    from \r\n        sysforeignkeys fk\r\n        inner join sysobjects onTable \r\n            on fk.fkeyid = onTable.id\r\n        inner join sysobjects againstTable  \r\n            on fk.rkeyid = againstTable.id\r\n    where 1=1\r\n        AND AgainstTable.TYPE = 'U'\r\n        AND OnTable.TYPE = 'U'\r\n        -- ignore self joins; they cause an infinite recursion\r\n        and OnTable.Name <> AgainstTable.Name\r\n    )\r\n,MyData as (\r\n    select \r\n         OnTable = o.name\r\n        ,AgainstTable = FKeys.againstTable\r\n    from \r\n        sys.objects o\r\n        left join FKeys\r\n            on  o.name = FKeys.onTable\r\n    where 1=1\r\n        and o.type = 'U'\r\n        and o.name not like 'sys%'\r\n    )\r\n,MyRecursion as (\r\n    -- base case\r\n    select  \r\n         TableName    = OnTable\r\n        ,Lvl        = 1\r\n    from\r\n        MyData\r\n    where 1=1\r\n        and AgainstTable is null\r\n\r\n    -- recursive case\r\n    union all select\r\n         TableName    = OnTable\r\n        ,Lvl        = r.Lvl + 1\r\n    from \r\n        MyData d\r\n        inner join MyRecursion r\r\n            on d.AgainstTable = r.TableName\r\n)\r\nselect\r\n    Lvl = max(Lvl)\r\n    ,TableName\r\n    ,strSql = 'delete from [' + tablename + ']'\r\nfrom \r\n    MyRecursion\r\ngroup by\r\n    TableName\r\norder by \r\n     1 \r\n    ,2 ");
			foreach (ClockWorkSnapshotTable clockWorkSnapshotTable in list)
			{
				DataRow[] array = dataTable.Select("TableName='" + clockWorkSnapshotTable.TableName + "'");
				clockWorkSnapshotTable.OrderNum = (int)array[0]["lvl"];
			}
			list.Sort((ClockWorkSnapshotTable t1, ClockWorkSnapshotTable t2) => t1.OrderNum.CompareTo(t2.OrderNum));
			return list;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0004EF04 File Offset: 0x0004D104
		private ClockWorkSnapshotTable GetSnapshotTable(string tableName, params string[] encryptedColumnNames)
		{
			List<string> encryptedColumns = new List<string>(encryptedColumnNames);
			return new ClockWorkSnapshotTable
			{
				TableName = tableName,
				EncryptedColumns = encryptedColumns
			};
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0004EF34 File Offset: 0x0004D134
		public BinaryFile GetClockWorkSnapshot(eSnapshotDataGroup DataGroups)
		{
			List<ClockWorkSnapshotTable> tables = this.GetTables(DataGroups);
			DataSet dataSet = new DataSet("snapshot");
			foreach (ClockWorkSnapshotTable clockWorkSnapshotTable in tables)
			{
				string tableName = clockWorkSnapshotTable.TableName;
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@tablename", DbType.String, tableName)
				};
				DataTable dataTable = this.DatabaseManager.ExecuteQuery("DECLARE @q varchar(256)\r\nSET @q = \r\n'IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N''[' + @tablename + ']'') AND OBJECTPROPERTY(id, N''IsUserTable'') = 1)\r\nSELECT * FROM ' + @tablename + '\r\nELSE\r\nSELECT ''notexists'' AS errmsg WHERE 1=0'\r\n\r\nEXEC (@q)", parameters);
				bool flag = dataTable != null && dataTable.Rows.Count > 0;
				if (flag)
				{
					bool flag2 = clockWorkSnapshotTable.EncryptedColumns != null && clockWorkSnapshotTable.EncryptedColumns.Count > 0;
					if (flag2)
					{
						foreach (string text in clockWorkSnapshotTable.EncryptedColumns)
						{
							DataColumn dataColumn = dataTable.Columns[text];
							dataColumn.ColumnName += "_encrypted";
							dataTable.Columns.Add(text);
						}
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							foreach (string text2 in clockWorkSnapshotTable.EncryptedColumns)
							{
								string columnName = text2 + "_encrypted";
								byte[] array = (dataRow[columnName] == DBNull.Value) ? new byte[0] : ((byte[])dataRow[columnName]);
								dataRow[text2] = ((array.Length != 0) ? this.DatabaseManager.Encryption.Decrypt(array) : "");
							}
						}
						foreach (string str in clockWorkSnapshotTable.EncryptedColumns)
						{
							dataTable.Columns.Remove(str + "_encrypted");
						}
					}
					dataTable.TableName = tableName;
					dataSet.Tables.Add(dataTable);
				}
			}
			byte[] array2 = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataSet.WriteXml(memoryStream, XmlWriteMode.WriteSchema);
				array2 = memoryStream.ToArray();
			}
			bool flag3 = array2 != null;
			BinaryFile result;
			if (flag3)
			{
				BinaryFile binaryFile = new BinaryFile
				{
					FileName = "Snapshot.txt",
					ByteArray = array2
				};
				result = binaryFile;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0004F2AC File Offset: 0x0004D4AC
		public ClockWorkSnapshotRestoreResult RestoreClockWorkSnapshot(BinaryFile Snapshot, eSnapshotDataGroup DataGroups, bool AllowRestoreToDatabaseWithMoreThanOneUser = false)
		{
			ClockWorkSnapshotRestoreResult clockWorkSnapshotRestoreResult = new ClockWorkSnapshotRestoreResult
			{
				TableResults = new List<ClockWorkSnapshotTableRestoreResult>()
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("select TABLE_NAME, COLUMN_NAME\r\nfrom INFORMATION_SCHEMA.COLUMNS\r\norder by TABLE_NAME, ORDINAL_POSITION");
			DataSet dataSet = new DataSet("snapshot");
			using (MemoryStream memoryStream = new MemoryStream(Snapshot.ByteArray))
			{
				dataSet.ReadXml(memoryStream, XmlReadMode.ReadSchema);
			}
			bool flag = dataSet.Tables.Count > 0;
			ClockWorkSnapshotRestoreResult result;
			if (flag)
			{
				DataTable dataTable2 = this.DatabaseManager.ExecuteQuery("SELECT COUNT(*) FROM people");
				bool flag2 = !AllowRestoreToDatabaseWithMoreThanOneUser && (dataTable2.Rows.Count < 1 || (int)dataTable2.Rows[0][0] > 1);
				if (flag2)
				{
					clockWorkSnapshotRestoreResult.ErrorMessage = "There are already users in this database!  There should only be one admin user in this database for the restore to proceed.  Nothing was done.";
					result = clockWorkSnapshotRestoreResult;
				}
				else
				{
					List<ClockWorkSnapshotTable> tables = this.GetTables(DataGroups);
					foreach (ClockWorkSnapshotTable snapshotTable in tables)
					{
						clockWorkSnapshotRestoreResult.TableResults.Add(new ClockWorkSnapshotTableRestoreResult
						{
							SnapshotTable = snapshotTable,
							ErrorMessages = new List<string>()
						});
					}
					DataSet dataSet2 = new DataSet();
					foreach (ClockWorkSnapshotTableRestoreResult clockWorkSnapshotTableRestoreResult in clockWorkSnapshotRestoreResult.TableResults)
					{
						clockWorkSnapshotTableRestoreResult.StartedProcessing = true;
						ClockWorkSnapshotTable snapshotTable2 = clockWorkSnapshotTableRestoreResult.SnapshotTable;
						bool flag3 = dataSet.Tables.Contains(snapshotTable2.TableName);
						if (flag3)
						{
							DataTable dataTable3 = dataSet.Tables[snapshotTable2.TableName];
							clockWorkSnapshotTableRestoreResult.RowCountFromSnapshot = new int?(dataTable3.Rows.Count);
							bool flag4 = dataTable3.Rows.Count > 0;
							if (flag4)
							{
								bool flag5 = snapshotTable2.EncryptedColumns != null && snapshotTable2.EncryptedColumns.Count > 0;
								if (flag5)
								{
									foreach (string text in snapshotTable2.EncryptedColumns)
									{
										DataColumn dataColumn = dataTable3.Columns[text];
										dataColumn.ColumnName += "_plaintext";
										dataTable3.Columns.Add(text, typeof(byte[]));
									}
									foreach (object obj in dataTable3.Rows)
									{
										DataRow dataRow = (DataRow)obj;
										foreach (string text2 in snapshotTable2.EncryptedColumns)
										{
											string columnName = text2 + "_plaintext";
											string text3 = (dataRow[columnName] == DBNull.Value) ? "" : dataRow[columnName].ToString().Trim();
											bool flag6 = text3.Length > 0;
											if (flag6)
											{
												dataRow[text2] = this.DatabaseManager.Encryption.Encrypt((string)dataRow[columnName]);
											}
										}
									}
									foreach (string str in snapshotTable2.EncryptedColumns)
									{
										dataTable3.Columns.Remove(str + "_plaintext");
									}
								}
								dataSet.Tables.Remove(dataTable3);
								dataSet2.Tables.Add(dataTable3);
							}
						}
					}
					List<ClockWorkSnapshotTableRestoreResult> list = clockWorkSnapshotRestoreResult.TableResults.ToList<ClockWorkSnapshotTableRestoreResult>();
					for (int i = dataSet2.Tables.Count - 1; i >= 0; i--)
					{
						string tableName = dataSet2.Tables[i].TableName;
						try
						{
							this.DatabaseManager.ExecuteNonQuery("DECLARE @s varchar(256)\r\nSET @s = 'IF EXISTS(SELECT TOP 1 * FROM ' + @tablename + ') TRUNCATE TABLE ' + @tablename \r\nEXEC (@s)", new DbParameter[]
							{
								this.DatabaseManager.GetParameter("@tablename", DbType.String, tableName)
							});
						}
						catch
						{
							try
							{
								this.DatabaseManager.ExecuteNonQuery("DECLARE @s varchar(256)\r\nSET @s = 'IF EXISTS(SELECT TOP 1 * FROM ' + @tablename + ') DELETE FROM ' + @tablename\r\nEXEC (@s)", new DbParameter[]
								{
									this.DatabaseManager.GetParameter("@tablename", DbType.String, tableName)
								});
							}
							catch
							{
							}
						}
						ClockWorkSnapshotTableRestoreResult clockWorkSnapshotTableRestoreResult2 = list.Find((ClockWorkSnapshotTableRestoreResult f) => f.SnapshotTable != null && f.SnapshotTable.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
						bool flag7 = clockWorkSnapshotTableRestoreResult2 == null;
						if (flag7)
						{
							clockWorkSnapshotRestoreResult.ErrorMessage = "Can't find table: " + tableName;
							return clockWorkSnapshotRestoreResult;
						}
						int value = (int)this.DatabaseManager.ExecuteScalar("SELECT COUNT(*) FROM " + tableName);
						clockWorkSnapshotTableRestoreResult2.RowCountFromExistingDatabase = new int?(value);
					}
					foreach (object obj2 in dataSet2.Tables)
					{
						DataTable dataTable4 = (DataTable)obj2;
						string tableName = dataTable4.TableName;
						ClockWorkSnapshotTableRestoreResult clockWorkSnapshotTableRestoreResult3 = list.Find((ClockWorkSnapshotTableRestoreResult f) => f.SnapshotTable != null && f.SnapshotTable.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
						bool flag8 = clockWorkSnapshotTableRestoreResult3 == null;
						if (flag8)
						{
							clockWorkSnapshotRestoreResult.ErrorMessage = "Can't find table2: " + tableName;
							return clockWorkSnapshotRestoreResult;
						}
						foreach (object obj3 in dataTable4.Rows)
						{
							DataRow dataRow2 = (DataRow)obj3;
							List<string> list2 = new List<string>();
							foreach (object obj4 in dataTable4.Columns)
							{
								DataColumn dataColumn2 = (DataColumn)obj4;
								DataRow[] array = dataTable.Select(string.Format("table_name='{0}' AND column_name='{1}'", tableName, dataColumn2.ColumnName));
								bool flag9 = array.Length != 0;
								if (flag9)
								{
									list2.Add(dataColumn2.ColumnName);
								}
							}
							string text4 = "";
							string text5 = "";
							DbParameter[] array2 = new DbParameter[list2.Count];
							int num = 0;
							foreach (string text6 in list2)
							{
								bool flag10 = text4.Length > 0;
								if (flag10)
								{
									text4 += ",";
									text5 += ",";
								}
								string text7 = "@" + text6;
								text4 += text6;
								text5 += text7;
								Type dataType = dataTable4.Columns[text6].DataType;
								bool flag11 = dataType == typeof(int);
								DbType pType;
								if (flag11)
								{
									pType = DbType.Int32;
								}
								else
								{
									bool flag12 = dataType == typeof(string);
									if (flag12)
									{
										pType = DbType.String;
									}
									else
									{
										bool flag13 = dataType == typeof(byte[]);
										if (flag13)
										{
											pType = DbType.Binary;
										}
										else
										{
											bool flag14 = dataType == typeof(double);
											if (flag14)
											{
												pType = DbType.Double;
											}
											else
											{
												bool flag15 = dataType == typeof(DateTime);
												if (flag15)
												{
													pType = DbType.DateTime;
												}
												else
												{
													pType = DbType.String;
												}
											}
										}
									}
								}
								array2[num++] = this.DatabaseManager.GetParameter(text7, pType, dataRow2[text6]);
							}
							string query = string.Format("SET IDENTITY_INSERT {0} ON\r\nINSERT INTO {0} ({1}) VALUES ({2})\r\nSET IDENTITY_INSERT {0} OFF", tableName, text4, text5);
							try
							{
								this.DatabaseManager.ExecuteNonQuery(query, array2);
							}
							catch
							{
								query = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, text4, text5);
								DbParameter[] array3 = new DbParameter[array2.Length];
								for (int j = 0; j < array2.Length; j++)
								{
									DbParameter dbParameter = array2[j];
									array3[j] = this.DatabaseManager.GetParameter(dbParameter.ParameterName, dbParameter.DbType, dbParameter.Value);
								}
								try
								{
									this.DatabaseManager.ExecuteNonQuery(query, array3);
								}
								catch (Exception ex)
								{
									clockWorkSnapshotTableRestoreResult3.ErrorMessages.Add("Failed inserting row: " + ex.ToString());
								}
							}
						}
					}
					result = clockWorkSnapshotRestoreResult;
				}
			}
			else
			{
				clockWorkSnapshotRestoreResult.ErrorMessage = "Couldn't parse file to create dataset.";
				result = clockWorkSnapshotRestoreResult;
			}
			return result;
		}
	}
}

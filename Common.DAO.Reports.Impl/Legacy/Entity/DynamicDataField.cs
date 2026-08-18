using System;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200001B RID: 27
	public class DynamicDataField
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00027D0C File Offset: 0x00025F0C
		public DynamicDataField(DataRow dr)
		{
			this.controlCaption = (string)dr["controlcaption"];
			this.controlId = (int)dr["controlid"];
			this.controlCode = (int)dr["controlcode"];
			this.setting1 = (int)dr["setting1"];
			this.setting2 = (int)dr["setting2"];
			this.setting3 = (int)dr["setting3"];
			this.defaultValue = (int)dr["defaultvalue"];
			bool flag = dr.Table != null && dr.Table.Columns.Contains("setting4");
			if (flag)
			{
				this.setting4 = ((dr["setting4"] == DBNull.Value) ? 0 : ((int)dr["setting4"]));
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00027E1C File Offset: 0x0002601C
		public string ControlCaption
		{
			get
			{
				return this.controlCaption;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00027E34 File Offset: 0x00026034
		public int ControlCode
		{
			get
			{
				return this.controlCode;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00027E4C File Offset: 0x0002604C
		public int Setting1
		{
			get
			{
				return this.setting1;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00027E64 File Offset: 0x00026064
		public int Setting2
		{
			get
			{
				return this.setting2;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00027E7C File Offset: 0x0002607C
		public int Setting3
		{
			get
			{
				return this.setting3;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00027E94 File Offset: 0x00026094
		public int Setting4
		{
			get
			{
				return this.setting4;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00027EAC File Offset: 0x000260AC
		public int DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00027EC4 File Offset: 0x000260C4
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00027EDC File Offset: 0x000260DC
		public int MappedColIndex
		{
			get
			{
				return this.mappedColIndex;
			}
			set
			{
				this.mappedColIndex = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00027EE8 File Offset: 0x000260E8
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00027F00 File Offset: 0x00026100
		public string[] MappedAdditionalColNames
		{
			get
			{
				return this.mappedAdditionalColNames;
			}
			set
			{
				this.mappedAdditionalColNames = value;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00027F0C File Offset: 0x0002610C
		public Type GetDataType()
		{
			switch (this.controlCode)
			{
			case 2:
				return typeof(bool);
			case 4:
				return typeof(bool);
			case 6:
				return typeof(DateTime);
			}
			return typeof(string);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00027F78 File Offset: 0x00026178
		public static string BytesToString(byte[] bytes, bool decrypt, IEncryption encryption)
		{
			bool flag = bytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else if (decrypt)
			{
				result = encryption.Decrypt(bytes);
			}
			else
			{
				result = encryption.Encoder.GetString(bytes);
			}
			return result;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00027FB8 File Offset: 0x000261B8
		[Obsolete("Use the one with IEncryption instead")]
		public static string BytesToString(byte[] bytes, bool decrypt, TripleDESEncryptionClass tripleDES)
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
				result = tripleDES.Encoder.GetString(bytes);
			}
			return result;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00027FF8 File Offset: 0x000261F8
		public string GetDataObjectLatestListView(DataRow dr, ref DataSet comboBoxData, ref DataSet peopleGroups, OperationContext opContext)
		{
			byte[] bytes = (dr["valbytes"] == DBNull.Value) ? null : ((byte[])dr["valbytes"]);
			string result = "";
			bool flag = this.controlCode == 10;
			if (flag)
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
				IEncryption encryption = databaseLayer.Encryption;
				string text = DynamicDataField.BytesToString(bytes, false, encryption);
				string[] array = text.Split(new char[]
				{
					'\t'
				});
				bool flag2 = array.Length != 0;
				if (flag2)
				{
					string text2 = "";
					for (int i = 0; i < array.Length; i++)
					{
						string text3 = array[i];
						string str = text3.Replace("\0", " | ");
						bool flag3 = i > 0;
						if (flag3)
						{
							text2 += ",";
						}
						text2 += str;
					}
					result = text2;
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00028104 File Offset: 0x00026304
		public object GetDataObject(DataRow dr, ref DataSet comboBoxData, ref DataSet peopleGroups, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			IEncryption encryption = databaseLayer.Encryption;
			int num = (dr["valint"] == DBNull.Value) ? -1 : ((int)dr["valint"]);
			byte[] bytes = (dr["valbytes"] == DBNull.Value) ? null : ((byte[])dr["valbytes"]);
			DateTime dateTime = (dr["valdate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["valdate"]);
			bool flag = this.controlCode == 2 || this.controlCode == 4;
			object result;
			if (flag)
			{
				result = ((num > 0) ? true : null);
			}
			else
			{
				bool flag2 = this.controlCode == 1 || this.controlCode == 11;
				if (flag2)
				{
					result = DynamicDataField.BytesToString(bytes, this.setting3 != 0, encryption);
				}
				else
				{
					bool flag3 = this.controlCode == 14;
					if (flag3)
					{
						bool flag4 = num > 0;
						if (flag4)
						{
							bool flag5 = this.setting4 == 0;
							if (flag5)
							{
								DataTable lookupList = ReportFunctionsLegacy.GetLookupList(this.setting1, false, -1, ref comboBoxData, false, opContext);
								result = ((lookupList == null) ? "" : ReportFunctionsLegacy.GetLookupListValue(lookupList, num));
							}
							else
							{
								string query = "SELECT controlcaption FROM dynamiccontrols WHERE controlid=@cid";
								DbParameter[] parameters = new DbParameter[]
								{
									databaseLayer.GetParameter("@cid", DbType.Int32, num)
								};
								DataTable dataTable = databaseLayer.ExecuteQuery(query, parameters);
								bool flag6 = dataTable.Rows.Count > 0;
								if (flag6)
								{
									string text = dataTable.Rows[0][0].ToString();
									int num2 = text.IndexOf("__");
									bool flag7 = num2 > 0;
									if (flag7)
									{
										text = text.Substring(0, num2);
									}
									result = text;
								}
								else
								{
									result = "?";
								}
							}
						}
						else
						{
							result = "";
						}
					}
					else
					{
						bool flag8 = this.controlCode == 3;
						if (flag8)
						{
							bool flag9 = this.setting3 == 0 || this.setting3 == 2;
							if (flag9)
							{
								bool flag10 = num >= 0;
								if (flag10)
								{
									DataTable lookupList2 = ReportFunctionsLegacy.GetLookupList(this.setting1, false, -1, ref comboBoxData, false, opContext);
									result = ((lookupList2 == null) ? "" : ReportFunctionsLegacy.GetLookupListValue(lookupList2, num));
								}
								else
								{
									result = "";
								}
							}
							else
							{
								result = DynamicDataField.BytesToString(bytes, this.setting3 == -1, encryption);
							}
						}
						else
						{
							bool flag11 = this.controlCode == 100;
							if (flag11)
							{
								bool flag12 = num >= 0;
								if (flag12)
								{
									string text2 = "pg" + this.setting1.ToString();
									DataTable dataTable2 = peopleGroups.Tables[text2];
									bool flag13 = dataTable2 == null;
									if (flag13)
									{
										string query2 = "SELECT p.personid,p.firstname,p.lastname,p.student_no FROM peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid WHERE pg.groupid=" + this.setting1.ToString();
										dataTable2 = databaseLayer.ExecuteQuery(query2);
										bool flag14 = dataTable2 != null;
										if (flag14)
										{
											dataTable2.TableName = text2;
										}
										peopleGroups.Tables.Add(dataTable2);
									}
									result = "";
									for (int i = 0; i < dataTable2.Rows.Count; i++)
									{
										bool flag15 = (int)dataTable2.Rows[i][0] == num;
										if (flag15)
										{
											byte[] bytes2 = (dataTable2.Rows[i][1] == DBNull.Value) ? null : ((byte[])dataTable2.Rows[i][1]);
											byte[] bytes3 = (dataTable2.Rows[i][2] == DBNull.Value) ? null : ((byte[])dataTable2.Rows[i][2]);
											byte[] bytes4 = (dataTable2.Rows[i][3] == DBNull.Value) ? null : ((byte[])dataTable2.Rows[i][3]);
											string text3 = DynamicDataField.BytesToString(bytes2, true, encryption);
											string str = DynamicDataField.BytesToString(bytes3, true, encryption);
											string text4 = DynamicDataField.BytesToString(bytes4, true, encryption);
											result = str + ((text3.Length > 0) ? ", " : "") + text3;
											break;
										}
									}
								}
								else
								{
									result = "";
								}
							}
							else
							{
								bool flag16 = this.controlCode == 10;
								if (flag16)
								{
									string text5 = DynamicDataField.BytesToString(bytes, false, encryption);
									string[] array = text5.Split(new char[]
									{
										'\t'
									});
									bool flag17 = array.Length != 0;
									if (flag17)
									{
										string text6 = "";
										for (int j = 0; j < array.Length; j++)
										{
											string text7 = array[j].Replace(",", "`").Replace(" | ", " ~ ");
											string str2 = text7.Replace("\0", " | ");
											bool flag18 = j > 0;
											if (flag18)
											{
												text6 += ",";
											}
											text6 += str2;
										}
										result = text6;
									}
									else
									{
										result = "";
									}
								}
								else
								{
									bool flag19 = this.controlCode == 6;
									if (flag19)
									{
										result = ((dateTime == DateTime.MinValue) ? null : dateTime);
									}
									else
									{
										result = DynamicDataField.BytesToString(bytes, false, encryption);
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040000C5 RID: 197
		private string controlCaption;

		// Token: 0x040000C6 RID: 198
		private int controlId;

		// Token: 0x040000C7 RID: 199
		private int controlCode;

		// Token: 0x040000C8 RID: 200
		private int setting1;

		// Token: 0x040000C9 RID: 201
		private int setting2;

		// Token: 0x040000CA RID: 202
		private int setting3;

		// Token: 0x040000CB RID: 203
		private int setting4;

		// Token: 0x040000CC RID: 204
		private int defaultValue;

		// Token: 0x040000CD RID: 205
		private int mappedColIndex = -1;

		// Token: 0x040000CE RID: 206
		private string[] mappedAdditionalColNames = null;
	}
}

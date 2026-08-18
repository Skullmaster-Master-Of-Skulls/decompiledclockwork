using System;
using System.Data;
using EncryptionClassLibrary;
using UnivOleDb;

namespace DynamicScreens
{
	// Token: 0x0200005A RID: 90
	public class DynamicDataField
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0003FEA8 File Offset: 0x0003EEA8
		public DynamicDataField(DataRow dr)
		{
			this.controlCaption = (string)dr["controlcaption"];
			this.controlId = (int)dr["controlid"];
			this.controlCode = (int)dr["controlcode"];
			this.setting1 = (int)dr["setting1"];
			this.setting2 = (int)dr["setting2"];
			this.setting3 = (int)dr["setting3"];
			this.defaultValue = (int)dr["defaultvalue"];
			if (dr.Table != null && dr.Table.Columns.Contains("setting4"))
			{
				this.setting4 = ((dr["setting4"] == DBNull.Value) ? 0 : ((int)dr["setting4"]));
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0003FFBC File Offset: 0x0003EFBC
		public string ControlCaption
		{
			get
			{
				return this.controlCaption;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0003FFD4 File Offset: 0x0003EFD4
		public int ControlCode
		{
			get
			{
				return this.controlCode;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0003FFEC File Offset: 0x0003EFEC
		public int Setting1
		{
			get
			{
				return this.setting1;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00040004 File Offset: 0x0003F004
		public int Setting2
		{
			get
			{
				return this.setting2;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0004001C File Offset: 0x0003F01C
		public int Setting3
		{
			get
			{
				return this.setting3;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00040034 File Offset: 0x0003F034
		public int Setting4
		{
			get
			{
				return this.setting4;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0004004C File Offset: 0x0003F04C
		public int DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00040064 File Offset: 0x0003F064
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0004007C File Offset: 0x0003F07C
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00040088 File Offset: 0x0003F088
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x000400A0 File Offset: 0x0003F0A0
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

		// Token: 0x060004D6 RID: 1238 RVA: 0x000400AC File Offset: 0x0003F0AC
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

		// Token: 0x060004D7 RID: 1239 RVA: 0x00040114 File Offset: 0x0003F114
		public string GetDataObjectLatestListView(DataRow dr, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref DataSet comboBoxData, ref DataSet peopleGroups)
		{
			byte[] bytes = (dr["valbytes"] == DBNull.Value) ? null : ((byte[])dr["valbytes"]);
			string result = "";
			if (this.controlCode == 10)
			{
				string text = DynamicScreen.BytesToString(bytes, false, null);
				string[] array = text.Split(new char[]
				{
					'\t'
				});
				if (array.Length > 0)
				{
					string text2 = "";
					for (int i = 0; i < array.Length; i++)
					{
						string text3 = array[i];
						string str = text3.Replace(string.Concat('\0'), " | ");
						if (i > 0)
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

		// Token: 0x060004D8 RID: 1240 RVA: 0x00040214 File Offset: 0x0003F214
		public object GetDataObject(DataRow dr, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, ref DataSet comboBoxData, ref DataSet peopleGroups)
		{
			int num = (dr["valint"] == DBNull.Value) ? -1 : ((int)dr["valint"]);
			byte[] bytes = (dr["valbytes"] == DBNull.Value) ? null : ((byte[])dr["valbytes"]);
			DateTime dateTime = (dr["valdate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["valdate"]);
			object result;
			if (this.controlCode == 2 || this.controlCode == 4)
			{
				result = ((num > 0) ? true : null);
			}
			else if (this.controlCode == 1 || this.controlCode == 11)
			{
				result = DynamicScreen.BytesToString(bytes, this.setting3 != 0, tripleDES);
			}
			else if (this.controlCode == 14)
			{
				if (num > 0)
				{
					if (this.setting4 == 0)
					{
						DataTable lookupList = DynamicScreen.GetLookupList(this.setting1, false, -1, ref comboBoxData, da, false);
						if (lookupList == null)
						{
							result = "";
						}
						else
						{
							result = DynamicScreen.GetLookupListValue(lookupList, num);
						}
					}
					else
					{
						da.SelectCommand.CommandText = "SELECT controlcaption FROM dynamiccontrols WHERE controlid=@cid";
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@cid", num);
						DataTable dataTable = new DataTable();
						da.Fill(dataTable);
						if (dataTable.Rows.Count > 0)
						{
							string text = dataTable.Rows[0][0].ToString();
							int num2 = text.IndexOf("__");
							if (num2 > 0)
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
			else if (this.controlCode == 3)
			{
				if (this.setting3 == 0 || this.setting3 == 2)
				{
					if (num >= 0)
					{
						DataTable lookupList = DynamicScreen.GetLookupList(this.setting1, false, -1, ref comboBoxData, da, false);
						if (lookupList == null)
						{
							result = "";
						}
						else
						{
							result = DynamicScreen.GetLookupListValue(lookupList, num);
						}
					}
					else
					{
						result = "";
					}
				}
				else
				{
					result = DynamicScreen.BytesToString(bytes, this.setting3 == -1, tripleDES);
				}
			}
			else if (this.controlCode == 100)
			{
				if (num >= 0)
				{
					string text2 = "pg" + this.setting1.ToString();
					DataTable dataTable = peopleGroups.Tables[text2];
					if (dataTable == null)
					{
						da.SelectCommand.CommandText = "SELECT p.personid,p.firstname,p.lastname,p.student_no FROM peoplegroups pg LEFT JOIN people p ON p.personid=pg.personid WHERE pg.groupid=" + this.setting1.ToString();
						dataTable = new DataTable(text2);
						da.Fill(dataTable);
						peopleGroups.Tables.Add(dataTable);
					}
					result = "";
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						if ((int)dataTable.Rows[i][0] == num)
						{
							byte[] bytes2 = (dataTable.Rows[i][1] == DBNull.Value) ? null : ((byte[])dataTable.Rows[i][1]);
							byte[] bytes3 = (dataTable.Rows[i][2] == DBNull.Value) ? null : ((byte[])dataTable.Rows[i][2]);
							byte[] bytes4 = (dataTable.Rows[i][3] == DBNull.Value) ? null : ((byte[])dataTable.Rows[i][3]);
							string text3 = DynamicScreen.BytesToString(bytes2, true, tripleDES);
							string str = DynamicScreen.BytesToString(bytes3, true, tripleDES);
							string text4 = DynamicScreen.BytesToString(bytes4, true, tripleDES);
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
			else if (this.controlCode == 10)
			{
				string text5 = DynamicScreen.BytesToString(bytes, false, null);
				string[] array = text5.Split(new char[]
				{
					'\t'
				});
				if (array.Length > 0)
				{
					string text6 = "";
					for (int i = 0; i < array.Length; i++)
					{
						string text7 = array[i].Replace(",", "`").Replace(" | ", " ~ ");
						string str2 = text7.Replace(string.Concat('\0'), " | ");
						if (i > 0)
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
			else if (this.controlCode == 6)
			{
				result = ((dateTime == DateTime.MinValue) ? null : dateTime);
			}
			else
			{
				result = DynamicScreen.BytesToString(bytes, false, null);
			}
			return result;
		}

		// Token: 0x04000366 RID: 870
		private string controlCaption;

		// Token: 0x04000367 RID: 871
		private int controlId;

		// Token: 0x04000368 RID: 872
		private int controlCode;

		// Token: 0x04000369 RID: 873
		private int setting1;

		// Token: 0x0400036A RID: 874
		private int setting2;

		// Token: 0x0400036B RID: 875
		private int setting3;

		// Token: 0x0400036C RID: 876
		private int setting4;

		// Token: 0x0400036D RID: 877
		private int defaultValue;

		// Token: 0x0400036E RID: 878
		private int mappedColIndex = -1;

		// Token: 0x0400036F RID: 879
		private string[] mappedAdditionalColNames = null;
	}
}

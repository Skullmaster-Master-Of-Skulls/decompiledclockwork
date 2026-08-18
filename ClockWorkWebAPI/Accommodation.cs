using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Web.Caching;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPI
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class Accommodation
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027BC File Offset: 0x000009BC
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000027D4 File Offset: 0x000009D4
		public int Lucid
		{
			get
			{
				return this.lucid;
			}
			set
			{
				this.lucid = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000027F8 File Offset: 0x000009F8
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
			set
			{
				this.controlId = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002804 File Offset: 0x00000A04
		public string OriginalControlCaption
		{
			get
			{
				return this.originalControlCaption;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000281C File Offset: 0x00000A1C
		public string ValueText
		{
			get
			{
				bool flag = this.value == null;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = this.value is string;
					if (flag2)
					{
						result = (string)this.value;
					}
					else
					{
						bool flag3 = this.value is bool;
						if (flag3)
						{
							result = "";
						}
						else
						{
							bool flag4 = this.value is DateTime;
							if (flag4)
							{
								result = ((DateTime)this.value).ToString("MMMM d, yyyy");
							}
							else
							{
								result = this.value.ToString();
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000028B8 File Offset: 0x00000AB8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000028D0 File Offset: 0x00000AD0
		public string ControlCaption
		{
			get
			{
				return this.controlCaption;
			}
			set
			{
				this.controlCaption = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000028DC File Offset: 0x00000ADC
		public string ControlCaptionForDisplay
		{
			get
			{
				bool flag = !string.IsNullOrEmpty(this.longDescription);
				string result;
				if (flag)
				{
					result = this.longDescription;
				}
				else
				{
					int num = this.controlCaption.IndexOf("~~");
					bool flag2 = num > 0;
					if (flag2)
					{
						result = this.controlCaption.Substring(0, num);
					}
					else
					{
						result = this.controlCaption;
					}
				}
				return result;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002938 File Offset: 0x00000B38
		public string CaptionWithValue
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.ControlCaptionForDisplay);
				string valueText = this.ValueText;
				bool flag = !string.IsNullOrEmpty(valueText);
				if (flag)
				{
					stringBuilder.AppendFormat(" ({0})", valueText);
				}
				bool flag2 = !string.IsNullOrEmpty(this.altLongDescription);
				if (flag2)
				{
					stringBuilder.AppendFormat(" [{0}]", this.altLongDescription);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000029A4 File Offset: 0x00000BA4
		public int Setting1
		{
			get
			{
				return this.setting1;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000029BC File Offset: 0x00000BBC
		public int Setting2
		{
			get
			{
				return this.setting2;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000029D4 File Offset: 0x00000BD4
		public int Setting3
		{
			get
			{
				return this.setting3;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000029EC File Offset: 0x00000BEC
		public int Setting4
		{
			get
			{
				return this.setting4;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002A04 File Offset: 0x00000C04
		public int DataId
		{
			get
			{
				return this.dataId;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002A1C File Offset: 0x00000C1C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002A34 File Offset: 0x00000C34
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A40 File Offset: 0x00000C40
		public int ControlCode
		{
			get
			{
				return this.controlCode;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A58 File Offset: 0x00000C58
		public string AltLongDescription
		{
			get
			{
				return this.altLongDescription;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A70 File Offset: 0x00000C70
		public string LongDescription
		{
			get
			{
				return this.longDescription;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A88 File Offset: 0x00000C88
		public Accommodation()
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A94 File Offset: 0x00000C94
		public Accommodation(DataRow dr, string languageCode)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool flag = dr.Table.Columns.Contains("lucourseid");
			if (flag)
			{
				this.lucid = ((dr["lucourseid"] == DBNull.Value) ? 0 : ((int)dr["lucourseid"]));
			}
			else
			{
				this.lucid = 0;
			}
			this.controlId = (int)dr["controlid"];
			string text = languageCode.ToLower();
			this.originalControlCaption = (string)dr["controlcaption"];
			bool flag2 = text.Equals("fr-ca");
			if (flag2)
			{
				this.controlCaption = dr["setting4string"].ToString();
				this.longDescription = "";
			}
			else
			{
				this.controlCaption = this.originalControlCaption;
				this.longDescription = (string)dr["longdescription"];
			}
			this.dataId = (int)dr["dataid"];
			byte[] array = (dr["valimage"] == DBNull.Value) ? new byte[0] : ((byte[])dr["valimage"]);
			byte[] array2 = (dr["valbytes"] == DBNull.Value) ? new byte[0] : ((byte[])dr["valbytes"]);
			bool flag3 = dr["valbytesisencrypted"] != DBNull.Value && Convert.ToBoolean(dr["valbytesisencrypted"]);
			this.setting1 = (int)dr["setting1"];
			this.setting2 = (int)dr["setting2"];
			this.setting3 = (int)dr["setting3"];
			this.setting4 = (int)dr["setting4"];
			this.controlCode = ((dr["controlcode"] == DBNull.Value) ? 0 : ((int)dr["controlcode"]));
			bool flag4 = this.controlCode == 2 || this.controlCode == 700;
			if (flag4)
			{
				this.value = Utility.IntToBool((dr["valint"] == DBNull.Value) ? 0 : ((int)dr["valint"]));
			}
			else
			{
				bool flag5 = array.Length != 0;
				if (flag5)
				{
					this.value = Utility.BytesToPlainText(array, encryption);
				}
				else
				{
					bool flag6 = array2.Length != 0 && flag3;
					if (flag6)
					{
						this.value = Utility.BytesToPlainText(array2, encryption);
					}
					else
					{
						bool flag7 = dr["valdate"] != DBNull.Value && (DateTime)dr["valdate"] != DateTime.MinValue;
						if (flag7)
						{
							this.value = (DateTime)dr["valdate"];
						}
						else
						{
							this.value = dr["valtext"].ToString();
						}
					}
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public Accommodation(Accommodation acc)
		{
			this.controlId = acc.ControlId;
			this.dataId = acc.DataId;
			this.value = acc.Value;
			this.controlCode = acc.ControlCode;
			this.setting1 = acc.Setting1;
			this.setting2 = acc.Setting2;
			this.setting3 = acc.Setting3;
			this.setting4 = acc.Setting4;
			this.controlCaption = acc.ControlCaption;
			this.originalControlCaption = acc.OriginalControlCaption;
			this.altLongDescription = acc.AltLongDescription;
			this.longDescription = acc.LongDescription;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E5C File Offset: 0x0000105C
		public static AccommodationCollection LoadAccommodations(db conn, int pid, Course course, string languageCode)
		{
			return Accommodation.LoadAccommodations(conn, pid, 0, languageCode);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E78 File Offset: 0x00001078
		public static AccommodationCollection LoadAccommodations(db conn, int pid, int lucid, string languageCode)
		{
			SqlDataAdapter da = conn.Da;
			da.SelectCommand.CommandText = "SELECT a.dataID,a.screenNum,a.personID,a.controlID,a.intval,a.bytesval,a.datetimeval,a.dtype,a.controlcode,a.setting3,a.controlcaption,a.lookuptext,a2.longdescription,a.setting4string,a.expirydate,a.altlongdescription,a.showonletter,a.approved,a.offline FROM (\t SELECT \tmps.dataID,mps.screenNum,mps.personID,mps.controlID,\t\tmps.controlValue AS intval,NULL AS bytesval,NULL AS datetimeval,\t\t1 AS dtype,dc.controlcode,0 AS setting3,dc.controlcaption,\t\tll.lookuptext,dc.setting4string,mps.expirydate,mps.altlongdescription       ,mps.showonletter,mps.approved,mps.offline FROM \tmaininfoaccommodationps mps LEFT JOIN dynamiccontrols dc ON dc.controlid=mps.controlid LEFT JOIN lookuplists ll ON dc.controlcode=3 AND dc.setting3=0 AND ll.lookuplistid=mps.controlvalue WHERE\tmps.personid=@personid \tAND mps.courseid=@lucourseid \tAND mps.controlid IN (SELECT controlid FROM accommodations) UNION SELECT \tops.dataID,ops.screenNum,ops.personID,ops.controlID,\tNULL AS intval,ops.controlvalue AS bytesval,NULL AS datetimeval,\t3 as dtype,dc.controlcode,dc.setting3,dc.controlcaption, \tNULL AS lookuptext,dc.setting4string,ops.expirydate,ops.altlongdescription       ,ops.showonletter,ops.approved,ops.offline FROM \totherinfoaccommodationps ops LEFT JOIN dynamiccontrols dc ON dc.controlid=ops.controlid WHERE\tpersonid=@personid \tAND ops.courseid=@lucourseid \tAND ops.controlid IN (SELECT controlid FROM accommodations) UNION SELECT \tdps.dataID,dps.screenNum,dps.personID,dps.controlID,\tNULL AS intval,NULL AS bytesval,dps.controlvalue AS datetimeval,\t\t2 AS dtype,0 AS controlcode,0 AS setting3,dc.controlcaption,\t\tNULL AS lookuptext,dc.setting4string,dps.expirydate,dps.altlongdescription       ,dps.showonletter,dps.approved,dps.offline FROM \tdatetimeinfoaccommodationps dps LEFT JOIN dynamiccontrols dc ON dc.controlid=dps.controlid WHERE\tdps.personid=@personid \t\tAND dps.courseid=@lucourseid \t\tAND dps.controlid IN (SELECT controlid FROM accommodations) ) a LEFT JOIN accommodations a2 ON a2.controlid=a.controlid WHERE a.controlid IN (SELECT controlid FROM accommodations WHERE (showonletter & 2) = 2) AND NOT a.offline=1 AND (a.expirydate IS NULL OR a.expirydate>getdate())";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.AddWithValue("@personid", pid);
			da.SelectCommand.Parameters.AddWithValue("@lucourseid", lucid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			bool flag = dataTable.Rows.Count < 1;
			if (flag)
			{
				da.SelectCommand.Parameters["@lucourseid"].Value = 0;
				dataTable = new DataTable();
				da.Fill(dataTable);
			}
			AccommodationCollection accommodationCollection = new AccommodationCollection();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Accommodation accommodation = new Accommodation(dr, languageCode);
				accommodationCollection.Add(accommodation);
			}
			dataTable.Rows.Clear();
			dataTable.Dispose();
			dataTable = null;
			return accommodationCollection;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002FC0 File Offset: 0x000011C0
		private static bool AreAllCharactersDigits(string s)
		{
			foreach (char c in s)
			{
				bool flag = !char.IsDigit(c);
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003008 File Offset: 0x00001208
		private static string GetAccommodationDisplayString(bool inFrench, DataRow dr)
		{
			string text2;
			if (inFrench)
			{
				string text = (dr["setting4string"] == DBNull.Value) ? "" : ((string)dr["setting4string"]);
				bool flag = text.Trim().Length > 0;
				if (flag)
				{
					text2 = text;
				}
				else
				{
					text2 = "";
				}
			}
			else
			{
				text2 = "";
			}
			bool flag2 = text2.Length < 1;
			if (flag2)
			{
				text2 = ((dr["controlcaption"] == DBNull.Value) ? "?" : ((string)dr["controlcaption"]));
			}
			int num = text2.IndexOf("~~");
			bool flag3 = num > 0;
			if (flag3)
			{
				text2 = text2.Substring(0, num);
			}
			bool flag4 = dr["valdate"] != DBNull.Value;
			if (flag4)
			{
				text2 = string.Format("{0}: {1}", text2, ((DateTime)dr["valdate"]).ToString("yyyy-MM-dd"));
			}
			else
			{
				string text3 = (dr["valtext"] == DBNull.Value) ? "" : ((string)dr["valtext"]);
				bool flag5 = text3.Length > 0 && !text3.ToLower().Equals("true");
				if (flag5)
				{
					text2 = text2 + ": " + text3;
				}
			}
			return text2;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000317C File Offset: 0x0000137C
		public static List<string> GetAccommodationsDisplayString(bool inFrench, DataTable accommodationsTable)
		{
			List<string> list = new List<string>();
			foreach (object obj in accommodationsTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				string accommodationDisplayString = Accommodation.GetAccommodationDisplayString(inFrench, dr);
				list.Add(accommodationDisplayString);
			}
			return list;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000031F4 File Offset: 0x000013F4
		private static int GetAccommodationsCount(DataTable accommodationsTable, bool showProf, bool showExam, bool showOther, bool inFrench)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (object obj in accommodationsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = (dataRow["showonletter"] != DBNull.Value) ? ((int)dataRow["showonletter"]) : 0;
				bool flag = (showProf && (num2 & 1) == 1) || (showExam && (num2 & 2) == 2) || (showOther && (num2 & 4) == 4) || (showProf && showExam && showOther);
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000032B8 File Offset: 0x000014B8
		public static DataTable LoadStudentsAccommodations(int pid, int lucid, bool inFrench, bool showAllAccommodations_ignoreShowOnLetter)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@lucid", DbType.Int32, lucid),
				clockWork.GetParameter("@showallaccommodations", DbType.Boolean, showAllAccommodations_ignoreShowOnLetter)
			};
			string query = "DECLARE @usecourse int\r\nSELECT @usecourse = MAX(dataid) FROM accommodationdata WHERE personid=@pid AND courseid=@lucid\r\n\r\nSELECT    ad.personid,ad.courseid,ad.controlid,ad.controlcaption,dc.setting4string,ad.valtext,ad.valint,ad.valbytes,valbytesisencrypted \r\n            ,a.longdescription,a.showonletter\r\n            ,ad.valdate\r\n  FROM      accommodationdata ad LEFT JOIN dynamiccontrols dc ON dc.controlid=ad.controlid\r\n            LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\n  WHERE     personid=@pid AND\r\n           (  ( NOT @usecourse IS NULL AND courseid=@lucid )\r\n                OR\r\n               ( @usecourse IS NULL AND courseid=0 )\r\n            )\r\n            AND (ad.offline IS NULL OR ad.offline=0)\r\n            AND (ad.expirydate IS NULL OR ad.expirydate > getdate() )\r\n            AND (@showallaccommodations=1 OR a.showonletter>0)\r\n            AND (ad.showonletter=1)";
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			for (int i = 0; i < dataTable.Columns.Count; i++)
			{
				dataTable.Columns[i].ReadOnly = false;
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				object obj2 = dataRow["valbytesisencrypted"];
				bool flag = obj2 != DBNull.Value && Convert.ToBoolean(obj2) && dataRow["valbytes"] != DBNull.Value;
				if (flag)
				{
					dataRow["valtext"] = encryption.Decrypt((byte[])dataRow["valbytes"]);
				}
				if (!inFrench)
				{
					obj2 = dataRow["longdescription"];
					bool flag2 = obj2 != DBNull.Value;
					if (flag2)
					{
						string text = (string)obj2;
						bool flag3 = text.Length > 0;
						if (flag3)
						{
							dataRow["controlcaption"] = text;
						}
					}
				}
			}
			dataTable.Columns.Remove("valbytesisencrypted");
			dataTable.Columns.Remove("valbytes");
			return dataTable;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034A4 File Offset: 0x000016A4
		public Accommodation Copy()
		{
			return new Accommodation(this);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000034BC File Offset: 0x000016BC
		[Obsolete("Use AccommodationsWebClientManager.GetStudentAccommodationsExpiryDate(...) instead")]
		public static DateTime GetStudentsAccommodationsExpiryDate(int pid)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			bool settingValue2 = webSettingsClientManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired);
			bool flag = settingValue > 0;
			DateTime result;
			if (flag)
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				string query = "SELECT MAX(controlvalue) AS expirydate FROM datetimeinfoaccommodationps WHERE personid=@pid AND controlid=@cid AND courseid=0";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@pid", DbType.Int32, pid),
					clockWork.GetParameter("@cid", DbType.Int32, settingValue)
				});
				bool flag2 = dataTable.Rows.Count > 0;
				DateTime dateTime;
				if (flag2)
				{
					dateTime = ((dataTable.Rows[0][0] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataTable.Rows[0][0]));
				}
				else
				{
					dateTime = DateTime.MinValue;
				}
				bool flag3 = dateTime == DateTime.MinValue && settingValue2;
				if (flag3)
				{
					dateTime = DateTime.Now.AddYears(-1);
				}
				result = dateTime;
			}
			else
			{
				result = DateTime.MinValue;
			}
			return result;
		}

		// Token: 0x0400000D RID: 13
		private int lucid;

		// Token: 0x0400000E RID: 14
		private int controlId;

		// Token: 0x0400000F RID: 15
		private int dataId;

		// Token: 0x04000010 RID: 16
		private object value;

		// Token: 0x04000011 RID: 17
		private int controlCode;

		// Token: 0x04000012 RID: 18
		private int setting1;

		// Token: 0x04000013 RID: 19
		private int setting2;

		// Token: 0x04000014 RID: 20
		private int setting3;

		// Token: 0x04000015 RID: 21
		private int setting4;

		// Token: 0x04000016 RID: 22
		private string controlCaption;

		// Token: 0x04000017 RID: 23
		private string originalControlCaption;

		// Token: 0x04000018 RID: 24
		private string altLongDescription;

		// Token: 0x04000019 RID: 25
		private string longDescription;

		// Token: 0x0400001A RID: 26
		public static string RTF_NEWLINE = "\r\n";

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x06000664 RID: 1636
		private delegate DataTable LoadCachedTableDelegate(db conn, int pid, int lucid, Cache Cache);

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x06000668 RID: 1640
		private delegate DataTable LoadCachedTableDelegate2(int pid, int lucid, Cache Cache);
	}
}

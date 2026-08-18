using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.Caching;
using System.Web.SessionState;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000009 RID: 9
	public class Club
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000047F8 File Offset: 0x000029F8
		public static bool VerifyAllowedToAccessClubPid(HttpSessionState Session, string userName, int pid, int clubPid, Cache cache)
		{
			bool flag = userName.Trim().Length < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = pid <= 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					int[] authorizedClubPids = Club.GetAuthorizedClubPids(Session, userName, pid, false, cache);
					bool flag3 = Array.IndexOf<int>(authorizedClubPids, clubPid) >= 0;
					if (flag3)
					{
						result = true;
					}
					else
					{
						authorizedClubPids = Club.GetAuthorizedClubPids(Session, userName, pid, true, cache);
						result = (Array.IndexOf<int>(authorizedClubPids, clubPid) >= 0);
					}
				}
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000486C File Offset: 0x00002A6C
		public static DataTable LoadMyApps(Cache cache, HttpSessionState Session, string userName, int userPid, db conn)
		{
			string key = "myapps" + userPid.ToString();
			bool flag = cache[key] != null;
			DataTable result;
			if (flag)
			{
				result = (DataTable)cache[key];
			}
			else
			{
				int[] authorizedClubPids = Club.GetAuthorizedClubPids(Session, userName, userPid, true, cache);
				string text = "";
				for (int i = 0; i < authorizedClubPids.Length; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						text += ",";
					}
					text += authorizedClubPids[i].ToString();
				}
				int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventApprovedCid, conn, cache);
				int settingValueInt2 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_groupNameCid, conn, cache);
				int settingValueInt3 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventAppAppTypeId, conn, cache);
				int settingValueInt4 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventNameCid, conn, cache);
				conn.Da.SelectCommand.CommandText = "SELECT \tatt.personid,att.appointmentid,app.startdate,app.enddate,oi.controlvalue AS groupNameBytes,oi2.controlvalue AS eventNameBytes,mi.controlvalue AS approved,oi3.controlvalue AS approvedlocationbytes,d1.controlvalue AS eventstartdate,mi2.controlvalue AS sdenied,mi3.controlvalue AS sprocessing,p.student_no\r\nFROM\tattendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid\r\n\tLEFT JOIN otherinfops oi ON oi.personid=att.personid AND oi.controlid=@gncid\r\n    LEFT JOIN maininfopa mi ON mi.personid=att.personid AND mi.appointmentid=att.appointmentid AND mi.controlid=@acid\r\n    LEFT JOIN otherinfopa oi2 ON oi2.personid=att.personid AND oi2.appointmentid=att.appointmentid AND oi2.controlid=@encid\r\n    LEFT JOIN otherinfopa oi3 ON oi3.personid=att.personid AND oi3.appointmentid=att.appointmentid AND oi3.controlid=@approvedlocationcid\r\n    LEFT JOIN datetimeinfopa d1 ON d1.personid=att.personid AND d1.appointmentid=att.appointmentid AND d1.controlid=@startdatecid\r\n    LEFT JOIN maininfopa mi2 ON mi2.personid=att.personid AND mi2.appointmentid=att.appointmentid AND mi2.controlid=@deniedcid\r\n    LEFT JOIN maininfopa mi3 ON mi3.personid=att.personid AND mi3.appointmentid=att.appointmentid AND mi3.controlid=@processingcid\r\n    LEFT JOIN people p ON p.personid=att.personid\r\nWHERE\tatt.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n\tAND app.apptypeid=@apptypeid AND app.cancelled=0";
				conn.Da.SelectCommand.Parameters.Clear();
				conn.Da.SelectCommand.Parameters.AddWithValue("@pids", text);
				conn.Da.SelectCommand.Parameters.AddWithValue("@gncid", settingValueInt2);
				conn.Da.SelectCommand.Parameters.AddWithValue("@encid", settingValueInt4);
				conn.Da.SelectCommand.Parameters.AddWithValue("@acid", settingValueInt);
				conn.Da.SelectCommand.Parameters.AddWithValue("@apptypeid", settingValueInt3);
				conn.Da.SelectCommand.Parameters.AddWithValue("@approvedlocationcid", 1179);
				conn.Da.SelectCommand.Parameters.AddWithValue("@startdatecid", 624);
				conn.Da.SelectCommand.Parameters.AddWithValue("@deniedcid", 1471);
				conn.Da.SelectCommand.Parameters.AddWithValue("@processingcid", 1472);
				DataTable dataTable = new DataTable();
				conn.Da.Fill(dataTable);
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("personid", typeof(int));
				dataTable2.Columns.Add("groupname");
				dataTable2.Columns.Add("eventname");
				dataTable2.Columns.Add("date");
				dataTable2.Columns.Add("status");
				dataTable2.Columns.Add("approvedlocation");
				dataTable2.Columns.Add("eventstartdate");
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string str = (dataRow["student_no"] != DBNull.Value) ? Core.BytesToString((byte[])dataRow["student_no"], true, conn.TripleDES) : "";
					string text2 = (dataRow["groupNameBytes"] == DBNull.Value) ? "" : Core.BytesToString((byte[])dataRow["groupNameBytes"], true, conn.TripleDES);
					text2 = text2 + " (" + str + ")";
					string text3 = (dataRow["eventNameBytes"] == DBNull.Value) ? "" : Core.BytesToString((byte[])dataRow["eventNameBytes"], true, conn.TripleDES);
					text3 = text3 + " (" + dataRow["appointmentid"].ToString() + ")";
					string value = (dataRow["approvedlocationbytes"] == DBNull.Value) ? "" : Core.BytesToString((byte[])dataRow["approvedlocationbytes"], true, conn.TripleDES);
					DateTime dateTime = (DateTime)dataRow["startdate"];
					DateTime d = (dataRow["eventstartdate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["eventstartdate"]);
					bool flag3 = dataRow["approved"] != DBNull.Value && (int)dataRow["approved"] > 0;
					bool flag4 = dataRow["sdenied"] != DBNull.Value && (int)dataRow["sdenied"] > 0;
					bool flag5 = dataRow["sprocessing"] != DBNull.Value && (int)dataRow["sprocessing"] > 0;
					bool flag6 = flag4;
					string value2;
					if (flag6)
					{
						value2 = "Denied";
					}
					else
					{
						bool flag7 = flag3;
						if (flag7)
						{
							value2 = "Approved";
						}
						else
						{
							bool flag8 = flag5;
							if (flag8)
							{
								value2 = "Processing";
							}
							else
							{
								value2 = "Pending";
							}
						}
					}
					DataRow dataRow2 = dataTable2.NewRow();
					dataRow2["personid"] = dataRow["personid"];
					dataRow2["groupname"] = text2;
					dataRow2["eventname"] = text3;
					dataRow2["date"] = dateTime.ToString("yyyy-MM-dd");
					dataRow2["status"] = value2;
					dataRow2["approvedlocation"] = value;
					dataRow2["eventstartdate"] = ((d == DateTime.MinValue) ? "" : d.ToString("yyyy-MM-dd"));
					dataTable2.Rows.Add(dataRow2);
				}
				int settingValueInt5 = AppSettingsV2.GetSettingValueInt(Setting.GENERAL_Caching_MinutesToCacheUserData, conn, cache);
				cache.Insert(key, dataTable2, null, DateTime.Now.AddMinutes((double)settingValueInt5), TimeSpan.Zero);
				result = dataTable2;
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004EC8 File Offset: 0x000030C8
		public static DataTable LoadPublishedEvents(Cache cache, HttpSessionState Session, db conn)
		{
			string key = "publishedevents";
			bool flag = cache[key] != null;
			DataTable result;
			if (flag)
			{
				result = (DataTable)cache[key];
			}
			else
			{
				int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_groupNameCid, conn, cache);
				int settingValueInt2 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_group_emailCid, conn, cache);
				int settingValueInt3 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventAppAppTypeId, conn, cache);
				int settingValueInt4 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventNameCid, conn, cache);
				string settingValueString = AppSettingsV2.GetSettingValueString(Setting.CLUBS_eventAppStartDateCids, conn, cache);
				string[] array = settingValueString.Split(new char[]
				{
					','
				});
				int num = (array.Length != 0) ? int.Parse(array[0]) : 0;
				int num2 = (array.Length > 1) ? int.Parse(array[1]) : 0;
				int settingValueInt5 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventAppEndDateCids, conn, cache);
				int settingValueInt6 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventAppStartTimeCid, conn, cache);
				int settingValueInt7 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_eventAppEndTimeCid, conn, cache);
				int settingValueInt8 = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_publishedEventsCheckboxCid, conn, cache);
				List<int> settingValueIntArray = AppSettingsV2.GetSettingValueIntArray(Setting.CLUBS_publishedCids, conn, cache);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < settingValueIntArray.Count; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(settingValueIntArray[i].ToString());
				}
				List<int> settingValueIntArray2 = AppSettingsV2.GetSettingValueIntArray(Setting.CLUBS_publishedDetailCids, conn, cache);
				foreach (int item in settingValueIntArray2)
				{
					bool flag3 = !settingValueIntArray.Contains(item);
					if (flag3)
					{
						settingValueIntArray.Add(item);
						stringBuilder.Append(",");
						stringBuilder.Append(item.ToString());
					}
				}
				conn.Da.SelectCommand.CommandText = "SELECT \tatt.personid,p.student_no,att.appointmentid,psd.valbytes AS groupnamebytes,psd2.valbytes AS groupemailbytes,pad2.valbytes AS eventnamebytes,app.personid AS whobooked,p2.firstname AS whobookedfirstname,p2.lastname AS whobookedlastname,\r\n        pad.*,dc.*,CAST(att.personid AS varchar(1000)) AS personid2,CAST(att.appointmentid AS varchar(1000)) AS appointmentid2\r\n,pad3.valdate AS startdate,pad4.valdate AS startdate2,pad7.valdate AS enddate\r\n,pad5.valbytes AS starttime,dc5.setting3 AS starttimesetting3,pad6.valbytes AS endtime,dc6.setting3 AS endtimesetting3\r\nFROM\tattendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid\r\n    LEFT JOIN perstudentdata psd ON psd.personid=att.personid AND psd.controlid=@groupcid\r\n    LEFT JOIN perstudentdata psd2 ON psd2.personid=att.personid AND psd2.controlid=@groupemailcid\r\n    LEFT JOIN perappdata pad2 ON pad2.personid=att.personid AND pad2.appointmentid=att.appointmentid AND pad2.controlid=@eventnamecid\r\n    LEFT JOIN perappdata pad3 ON pad3.personid=att.personid AND pad3.appointmentid=att.appointmentid AND pad3.controlid=@startdatecid\r\n    LEFT JOIN perappdata pad4 ON pad4.personid=att.personid AND pad4.appointmentid=att.appointmentid AND pad4.controlid=@startdatecid2\r\n    LEFT JOIN perappdata pad5 ON pad5.personid=att.personid AND pad5.appointmentid=att.appointmentid AND pad5.controlid=@starttimecid\r\n    LEFT JOIN dynamiccontrols dc5 ON dc5.controlid=pad5.controlid\r\n    LEFT JOIN perappdata pad6 ON pad6.personid=att.personid AND pad6.appointmentid=att.appointmentid AND pad6.controlid=@endtimecid\r\n    LEFT JOIN dynamiccontrols dc6 ON dc6.controlid=pad6.controlid\r\n    LEFT JOIN perappdata pad7 ON pad7.personid=att.personid AND pad7.appointmentid=att.appointmentid AND pad7.controlid=@enddatecid\r\n\tLEFT JOIN perappdata pad ON pad.personid=att.personid AND pad.appointmentid=att.appointmentid AND pad.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\n\tLEFT JOIN dynamiccontrols dc ON dc.controlid=pad.controlid\r\n\tLEFT JOIN people p ON p.personid=att.personid\r\n    LEFT JOIN people p2 ON p2.personid=app.personid\r\nWHERE\tapp.apptypeid=@apptypeid AND app.cancelled=0 \r\n\tAND att.appointmentid IN (SELECT appointmentid FROM maininfopa WHERE controlid=@publishedcid)\r\n\tAND \r\n\t( att.appointmentid IN (SELECT appointmentid FROM datetimeinfopa WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@startdatecids,',')) AND controlvalue>=getdate())\r\n\t OR att.appointmentid IN (SELECT appointmentid FROM datetimeinfopa WHERE controlid=@enddatecid AND controlvalue>=getdate())\r\n\t)";
				conn.Da.SelectCommand.Parameters.Clear();
				conn.Da.SelectCommand.Parameters.AddWithValue("@apptypeid", settingValueInt3);
				conn.Da.SelectCommand.Parameters.AddWithValue("@startdatecids", settingValueString);
				conn.Da.SelectCommand.Parameters.AddWithValue("@enddatecid", settingValueInt5);
				conn.Da.SelectCommand.Parameters.AddWithValue("@publishedcid", settingValueInt8);
				conn.Da.SelectCommand.Parameters.AddWithValue("@cids", stringBuilder.ToString());
				conn.Da.SelectCommand.Parameters.AddWithValue("@groupcid", settingValueInt);
				conn.Da.SelectCommand.Parameters.AddWithValue("@eventnamecid", settingValueInt4);
				conn.Da.SelectCommand.Parameters.AddWithValue("@startdatecid", num);
				conn.Da.SelectCommand.Parameters.AddWithValue("@startdatecid2", num2);
				conn.Da.SelectCommand.Parameters.AddWithValue("@starttimecid", settingValueInt6);
				conn.Da.SelectCommand.Parameters.AddWithValue("@endtimecid", settingValueInt7);
				conn.Da.SelectCommand.Parameters.AddWithValue("@groupemailcid", settingValueInt2);
				DataTable dataTable = new DataTable();
				conn.Da.Fill(dataTable);
				dataTable.Columns.Add("cval");
				DynamicScreenLayout.DecipherDynamicData(ref dataTable, "cval", conn);
				dataTable = conn.TripleDES.EncryptOrDecryptNameDataTableBatch(true, dataTable, new string[]
				{
					"personid2",
					"appointmentid2"
				});
				byte[] array2 = new byte[0];
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("appointmentid", typeof(int));
				dataTable2.Columns.Add("personid", typeof(int));
				dataTable2.Columns.Add("personid2", array2.GetType());
				dataTable2.Columns.Add("appointmentid2", array2.GetType());
				dataTable2.Columns.Add("groupname");
				dataTable2.Columns.Add("groupemail");
				dataTable2.Columns.Add("booker");
				dataTable2.Columns.Add("eventname");
				dataTable2.Columns.Add("eventdate");
				dataTable2.Columns.Add("eventtime");
				foreach (int num3 in settingValueIntArray)
				{
					string columnName = "c" + num3.ToString();
					dataTable2.Columns.Add(columnName);
				}
				int k;
				for (int j = 0; j < dataTable.Rows.Count; j = k)
				{
					DataRow dataRow = dataTable.Rows[j];
					int num4 = (int)dataRow["appointmentid"];
					byte[] dataRowBytesData = DynamicScreenLayout.GetDataRowBytesData(dataRow, "groupnamebytes");
					string value = conn.TripleDES.Decrypt(dataRowBytesData);
					DataRow dataRow2 = dataTable2.NewRow();
					byte[] dataRowBytesData2 = DynamicScreenLayout.GetDataRowBytesData(dataRow, "groupemailbytes");
					dataRow2["groupemail"] = conn.TripleDES.Decrypt(dataRowBytesData2);
					byte[] dataRowBytesData3 = DynamicScreenLayout.GetDataRowBytesData(dataRow, "eventnamebytes");
					string value2 = conn.TripleDES.Decrypt(dataRowBytesData3);
					dataRow2["personid"] = dataRow["personid"];
					dataRow2["appointmentid"] = num4;
					dataRow2["groupname"] = value;
					dataRow2["eventname"] = value2;
					dataRow2["personid2"] = dataRow["personid2"];
					dataRow2["appointmentid2"] = dataRow["appointmentid2"];
					DateTime dataRowDateTimeData = DynamicScreenLayout.GetDataRowDateTimeData(dataRow, "startdate");
					DateTime dataRowDateTimeData2 = DynamicScreenLayout.GetDataRowDateTimeData(dataRow, "startdate2");
					DateTime dataRowDateTimeData3 = DynamicScreenLayout.GetDataRowDateTimeData(dataRow, "startdate");
					byte[] dataRowBytesData4 = DynamicScreenLayout.GetDataRowBytesData(dataRow, "starttime");
					byte[] dataRowBytesData5 = DynamicScreenLayout.GetDataRowBytesData(dataRow, "endtime");
					string str = Core.BytesToString(dataRowBytesData4, DynamicScreenLayout.GetDataRowIntData(dataRow, "starttimesetting3", 0) == 1, conn.TripleDES);
					string str2 = Core.BytesToString(dataRowBytesData5, DynamicScreenLayout.GetDataRowIntData(dataRow, "endtimesetting3", 0) == 1, conn.TripleDES);
					string text = (dataRowDateTimeData == DateTime.MinValue) ? "" : dataRowDateTimeData.ToString("MMM d, yyyy");
					bool flag4 = dataRowDateTimeData2 != DateTime.MinValue;
					if (flag4)
					{
						bool flag5 = text.Length > 0;
						if (flag5)
						{
							text += ": ";
						}
						text += dataRowDateTimeData2.ToString("MMM d, yyyy");
					}
					dataRow2["eventdate"] = text;
					dataRow2["eventtime"] = str + " to " + str2;
					byte[] dataRowBytesData6 = DynamicScreenLayout.GetDataRowBytesData(dataRow, "whobookedfirstname");
					byte[] dataRowBytesData7 = DynamicScreenLayout.GetDataRowBytesData(dataRow, "whobookedlastname");
					dataRow2["booker"] = conn.TripleDES.Decrypt(dataRowBytesData6) + " " + conn.TripleDES.Decrypt(dataRowBytesData7);
					for (k = j; k < dataTable.Rows.Count; k++)
					{
						DataRow dataRow3 = dataTable.Rows[k];
						int num5 = (int)dataRow["appointmentid"];
						bool flag6 = num5 != num4;
						if (flag6)
						{
							break;
						}
						bool flag7 = dataRow3["cval"] != DBNull.Value;
						if (flag7)
						{
							int dataRowIntData = DynamicScreenLayout.GetDataRowIntData(dataRow3, "controlid", -100);
							string value3 = dataRow3["cval"].ToString();
							string columnName2 = "c" + dataRowIntData.ToString();
							dataRow2[columnName2] = value3;
						}
					}
					dataTable2.Rows.Add(dataRow2);
				}
				int settingValueInt9 = AppSettingsV2.GetSettingValueInt(Setting.GENERAL_Caching_MinutesToCachePublicData, conn, cache);
				cache.Insert(key, dataTable2, null, DateTime.Now.AddMinutes((double)settingValueInt9), TimeSpan.Zero);
				result = dataTable2;
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000057A0 File Offset: 0x000039A0
		public static DataTable LoadMyClubs(int expiryDateCid, Cache cache, HttpSessionState Session, string userName, int userPid, int approvedLocationCid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string text = "myclubs" + userPid.ToString();
			bool flag = cache[text] != null;
			DataTable result;
			if (flag)
			{
				result = (DataTable)cache[text];
			}
			else
			{
				int[] authorizedClubPids = Club.GetAuthorizedClubPids(Session, userName, userPid, true, cache);
				string text2 = "";
				for (int i = 0; i < authorizedClubPids.Length; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						text2 += ",";
					}
					text2 += authorizedClubPids[i].ToString();
				}
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.CLUBS_profileScreenNumTemp);
				int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.CLUBS_groupNameCid);
				string str = "SELECT DISTINCT personid FROM (\r\nSELECT personid FROM maininfops WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) \r\n    UNION SELECT personid FROM otherinfops WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) \r\n    UNION SELECT personid FROM datetimeinfops WHERE personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@sn) \r\n   ) x ";
				DataTable dataTable = new DataTable(text);
				string query = "SELECT p.personid,p.firstname,p.lastname,p.student_no,a2.personid AS changespending,di.controlvalue AS controlvaluedatetime,oi.controlvalue AS groupnamebytes \r\nFROM people p LEFT JOIN (" + str + ") a2 ON a2.personid=p.personid \r\n LEFT JOIN datetimeinfops di ON di.personid=p.personid AND di.controlid=@cid1 \r\n LEFT JOIN otherinfops oi ON oi.personid=p.personid AND oi.controlid=@gncid \r\n WHERE p.isactive=1 AND p.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) ORDER BY p.personid";
				dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@pids", DbType.String, text2),
					clockWork.GetParameter("@cid1", DbType.Int32, expiryDateCid),
					clockWork.GetParameter("@sn", DbType.Int32, settingValue),
					clockWork.GetParameter("@gncid", DbType.Int32, settingValue2),
					clockWork.GetParameter("@loccid", DbType.Int32, approvedLocationCid)
				});
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("personid", typeof(int));
				dataTable2.Columns.Add("groupname");
				dataTable2.Columns.Add("status");
				dataTable2.Columns.Add("approvedlocation");
				int j = 0;
				while (j < dataTable.Rows.Count)
				{
					DataRow dataRow = dataTable.Rows[j];
					int num = (int)dataTable.Rows[j][0];
					int k = j;
					DataRow dataRow2 = dataTable2.NewRow();
					dataRow2["personid"] = num;
					string str2 = Core.BytesToString((byte[])dataRow["student_no"], true, encryption);
					dataRow2["groupname"] = Core.BytesToString((byte[])dataRow["groupnamebytes"], true, encryption) + " (" + str2 + ")";
					DateTime dateTime = DateTime.MinValue;
					while (k < dataTable.Rows.Count)
					{
						DataRow dataRow3 = dataTable.Rows[k];
						int num2 = (int)dataRow3[0];
						bool flag3 = num2 != num;
						if (flag3)
						{
							break;
						}
						bool flag4 = dataRow3["controlvaluedatetime"] != DBNull.Value;
						if (flag4)
						{
							dateTime = (DateTime)dataRow3["controlvaluedatetime"];
						}
						k++;
					}
					j = k;
					bool flag5 = dateTime == DateTime.MinValue;
					if (flag5)
					{
						dataRow2["status"] = "Application pending";
					}
					else
					{
						bool flag6 = dateTime <= DateTime.Now;
						if (flag6)
						{
							bool flag7 = dataRow["changespending"] == DBNull.Value;
							if (flag7)
							{
								dataRow2["status"] = "Expired";
							}
							else
							{
								dataRow2["status"] = "Awaiting renewal request";
							}
						}
						else
						{
							bool flag8 = dataRow["changespending"] == DBNull.Value;
							if (flag8)
							{
								dataRow2["status"] = "Active";
							}
							else
							{
								dataRow2["status"] = "Active; awaiting change approval";
							}
						}
					}
					dataTable2.Rows.Add(dataRow2);
				}
				int settingValue3 = webSettingsClientManager.GetSettingValue<int>(Setting.GENERAL_Caching_MinutesToCacheUserData);
				cache.Insert(text, dataTable2, null, DateTime.Now.AddMinutes((double)settingValue3), TimeSpan.Zero);
				result = dataTable2;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public static int[] GetAuthorizedClubPids(HttpSessionState Session, string userName, int userPid, bool forceReloadInfoFromDb, Cache cache)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool flag = userName.Trim().Length < 1;
			int[] result;
			if (flag)
			{
				result = new int[0];
			}
			else
			{
				bool flag2 = userPid <= 0;
				if (flag2)
				{
					result = new int[0];
				}
				else
				{
					object obj = forceReloadInfoFromDb ? null : Session["clubAuthorizedPids"];
					bool flag3 = obj != null;
					if (flag3)
					{
						result = (int[])obj;
					}
					else
					{
						IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
						string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.CLUBS_authorizedUserNameOrPidCids);
						string query = "SELECT DISTINCT personid FROM otherinfops WHERE controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')) AND (controlvalue=@un OR controlvalue=@une)";
						DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
						{
							clockWork.GetParameter("@cids", DbType.String, settingValue),
							clockWork.GetParameter("@un", DbType.Binary, Core.StringToBytes(userName, false, encryption)),
							clockWork.GetParameter("@une", DbType.Binary, Core.StringToBytes(userName, true, encryption))
						});
						int[] array = new int[dataTable.Rows.Count];
						for (int i = 0; i < dataTable.Rows.Count; i++)
						{
							DataRow dataRow = dataTable.Rows[i];
							array[i] = (int)dataRow[0];
						}
						Session["clubAuthorizedPids"] = array;
						result = array;
					}
				}
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005D34 File Offset: 0x00003F34
		public static DataView LoadClubs(Cache cache)
		{
			string key = "clubs";
			object obj = cache.Get(key);
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool flag = obj == null;
			DataView result;
			if (flag)
			{
				DataTable dataTable = new DataTable();
				string value = "485,1147,480,481,492,493,510";
				string query = "SELECT p.personid,CAST(p.personid AS varchar(1000)) AS personid2,pd.controlid,pd.valint,pd.valbytes,pd.valdate,pd.valimage,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue,dc.controlcaption,dc.setting4string\r\nFROM people p LEFT JOIN perstudentdata pd ON pd.personid=p.personid\r\nLEFT JOIN dynamiccontrols dc ON dc.controlid=pd.controlid\r\nWHERE p.isactive=1 AND p.personid IN (SELECT personid FROM datetimeinfops WHERE controlid=388 AND controlvalue>getdate())\r\n        AND p.isactive=1 AND pd.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
				dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@cids", DbType.String, value)
				});
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(true, dataTable, new string[]
				{
					"personid2"
				});
				DataTable table = DynamicScreenLayout.ConvertDynamicDataToRegularTableData(dataTable, new string[]
				{
					"personid2"
				});
				DataView dataView = new DataView(table);
				dataView.Sort = "groupname";
				cache.Insert(key, dataView, null, DateTime.Now.AddMinutes(20.0), TimeSpan.Zero);
				result = dataView;
			}
			else
			{
				result = (DataView)obj;
			}
			return result;
		}
	}
}

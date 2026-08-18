using System;
using System.Collections;
using System.Data;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x0200005B RID: 91
	public class Student
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x000191D0 File Offset: 0x000181D0
		public static object[] DecryptStudentNamesNumber(DataRow dr, TripleDESEncryptionClass tripleDES)
		{
			byte[] inputInBytes = (byte[])dr["firstname"];
			byte[] inputInBytes2 = (byte[])dr["lastname"];
			byte[] inputInBytes3 = (byte[])dr["student_no"];
			byte[] inputInBytes4 = (dr["middlename"] == DBNull.Value) ? null : ((byte[])dr["middlename"]);
			return new object[]
			{
				(int)dr["personid"],
				tripleDES.Decrypt(inputInBytes2),
				tripleDES.Decrypt(inputInBytes4),
				tripleDES.Decrypt(inputInBytes),
				tripleDES.Decrypt(inputInBytes3)
			};
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001928C File Offset: 0x0001828C
		public static DataRow DecryptStudentNamesNumber2(DataTable tableToAddRowTo, DataRow dr, TripleDESEncryptionClass tripleDES)
		{
			byte[] inputInBytes = (byte[])dr["firstname"];
			byte[] inputInBytes2 = (byte[])dr["lastname"];
			byte[] inputInBytes3 = (byte[])dr["student_no"];
			byte[] inputInBytes4 = (dr["middlename"] == DBNull.Value) ? null : ((byte[])dr["middlename"]);
			DataRow dataRow = tableToAddRowTo.NewRow();
			dataRow["firstname"] = tripleDES.Decrypt(inputInBytes);
			dataRow["lastname"] = tripleDES.Decrypt(inputInBytes2);
			dataRow["middlename"] = tripleDES.Decrypt(inputInBytes4);
			dataRow["student_no"] = tripleDES.Decrypt(inputInBytes3);
			dataRow["personid"] = dr["personid"];
			string[] array = new string[]
			{
				"firstname",
				"lastname",
				"middlename",
				"student_no",
				"personid"
			};
			for (int i = 0; i < tableToAddRowTo.Columns.Count; i++)
			{
				string columnName = tableToAddRowTo.Columns[i].ColumnName;
				if (Array.IndexOf<string>(array, columnName.ToLower()) < 0 && dr.Table.Columns.Contains(columnName))
				{
					dataRow[columnName] = dr[columnName];
				}
			}
			return dataRow;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00019420 File Offset: 0x00018420
		public static void FixPerAppScreenIcons(UnivDataAdapter da, int screenNum, int personid, int screenIconId, int controlIdToActivate)
		{
			bool flag = DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, DatabaseVersionManager.ClockWorkFeature.DynamicImageData);
			da.SelectCommand.CommandText = "DELETE FROM appointmenticons WHERE screennum=@screennum AND appointmentid IN (SELECT app.appointmentid FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid WHERE att.personid=@personid)";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			da.SelectCommand.Parameters.Add("@personid", personid);
			da.Fill(new DataTable());
			if (controlIdToActivate > 0)
			{
				da.SelectCommand.CommandText = "SELECT a1.appointmentid FROM (SELECT DISTINCT appointmentid FROM maininfopa WHERE controlid=@cid AND personid=@personid UNION SELECT DISTINCT appointmentid FROM otherinfopa WHERE controlid=@cid AND personid=@personid UNION SELECT DISTINCT appointmentid FROM datetimeinfopa WHERE controlid=@cid AND personid=@personid";
				if (flag)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.CommandText += " UNION SELECT DISTINCT appointmentid FROM imageinfopa WHERE controlid=@cid AND personid=@personid";
				}
				UnivCommand selectCommand2 = da.SelectCommand;
				selectCommand2.CommandText += ") a1 WHERE NOT a1.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE screennum=@screennum)";
			}
			else
			{
				da.SelectCommand.CommandText = "SELECT a1.appointmentid FROM (SELECT DISTINCT appointmentid FROM maininfopa WHERE personid=@personid UNION SELECT DISTINCT appointmentid FROM otherinfopa WHERE personid=@personid UNION SELECT DISTINCT appointmentid FROM datetimeinfopa WHERE personid=@personid";
				if (flag)
				{
					UnivCommand selectCommand3 = da.SelectCommand;
					selectCommand3.CommandText += " UNION SELECT DISTINCT appointmentid FROM imageinfopa WHERE personid=@personid";
				}
				UnivCommand selectCommand4 = da.SelectCommand;
				selectCommand4.CommandText += ") a1 WHERE NOT a1.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE screennum=@screennum)";
			}
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@personid", personid);
			da.SelectCommand.Parameters.Add("@screennum", screenNum);
			if (controlIdToActivate > 0)
			{
				da.SelectCommand.Parameters.Add("@cid", controlIdToActivate);
			}
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (screenIconId >= 0)
				{
					da.SelectCommand.CommandText = "INSERT INTO appointmenticons (appointmentid,screennum,iconnum) VALUES (@appointmentid,@screennum,@iconnum)";
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@appointmentid", (int)dataRow[0]);
					da.SelectCommand.Parameters.Add("@screennum", screenNum);
					da.SelectCommand.Parameters.Add("@iconnum", screenIconId);
					da.Fill(new DataTable());
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000196CC File Offset: 0x000186CC
		public static void SaveData(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum, int personid, DataTable t, string tableName, ref ArrayList changedAppIDs)
		{
			DynamicData.SaveDataPAPM(da, tripleDES, screenNum, personid, t, tableName, ref changedAppIDs);
		}

		// Token: 0x040001DE RID: 478
		public const int APPDATAADDED = 1;

		// Token: 0x040001DF RID: 479
		public const int APPDATAMODIFIED = 2;

		// Token: 0x040001E0 RID: 480
		public const int APPDATADELETED = 3;

		// Token: 0x040001E1 RID: 481
		public const int APPDATAUNCHANGED = 4;
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkWebAPI;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using Databases;

namespace ClockWorkController
{
	// Token: 0x02000002 RID: 2
	public class Accommodation
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static AccommodationCollection LoadAccommodations(int pid, int lucid, string languageCode)
		{
			List<int> lucids = new List<int>(1)
			{
				lucid
			};
			return ClockWorkController.Accommodation.LoadAccommodations(pid, lucids, languageCode);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207C File Offset: 0x0000027C
		public static AccommodationCollection LoadAccommodations(int pid, List<int> lucids, string languageCode)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[2];
			bool flag = lucids.Count > 1;
			AccommodationCollection result;
			if (flag)
			{
				array[0] = clockWork.GetParameter("@personid", DbType.Int32, pid);
				array[1] = clockWork.GetParameter("@lucourseids", DbType.String, Utility.ListToString(lucids));
				string query = "SELECT\ta.courseid AS lucourseid,a.dataid,a.personid,a.controlid,a.controlcode,a.controlcaption\r\n\t\t,a.valtext,a.valint,a.valdate,a.valimage\r\n\t\t,a.altlongdescription \r\n\t\t,a.valbytes,a.valbytesisencrypted,a.setting1,a.setting2,a.setting3,a.setting4\r\n\t\t,acc.longDescription,dc.setting4string\r\nFROM\taccommodationdataactive a \r\n\t\tLEFT JOIN Accommodations acc ON acc.ControlID=a.ControlID\r\n\t\tLEFT JOIN DynamicControls dc ON dc.ControlID=a.ControlID \r\nWHERE\ta.PersonID=@personid\r\n\t\tAND (a.courseid=0 OR a.courseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucourseids,',')))\r\n\t\tAND (acc.showonletter & 2) = 2\r\nORDER BY a.personid,a.courseid,a.controlid";
				DataTable dataTable = clockWork.ExecuteQuery(query, array);
				DataTable dataTable2 = dataTable.Clone();
				AccommodationCollection accommodationCollection = new AccommodationCollection();
				foreach (int num in lucids)
				{
					DataRow[] array2 = dataTable.Select("lucourseid=" + num.ToString());
					bool flag2 = array2.Length < 1;
					if (flag2)
					{
						array2 = dataTable.Select("lucourseid=0");
					}
					foreach (DataRow row in array2)
					{
						dataTable2.ImportRow(row);
						DataRow dataRow = dataTable2.Rows[dataTable2.Rows.Count - 1];
						dataRow["lucourseid"] = num;
						ClockWorkWebAPI.Accommodation accommodation = new ClockWorkWebAPI.Accommodation(dataRow, languageCode);
						accommodationCollection.Add(accommodation);
					}
				}
				result = accommodationCollection;
			}
			else
			{
				bool flag3 = lucids.Count == 1;
				DataTable dataTable;
				if (flag3)
				{
					array[0] = clockWork.GetParameter("@personid", DbType.Int32, pid);
					array[1] = clockWork.GetParameter("@lucourseid", DbType.Int32, lucids[0]);
					DataTable dataTable3 = clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentAccommodations, array);
					dataTable = dataTable3.Clone();
					List<int> list = new List<int>();
					foreach (object obj in dataTable3.Rows)
					{
						DataRow dataRow2 = (DataRow)obj;
						int item = (dataRow2["controlid"] is DBNull) ? 0 : ((int)dataRow2["controlid"]);
						bool flag4 = !list.Contains(item);
						if (flag4)
						{
							dataTable.ImportRow(dataRow2);
							list.Add(item);
						}
					}
					dataTable3.Dispose();
				}
				else
				{
					dataTable = new DataTable();
				}
				AccommodationCollection accommodationCollection2 = new AccommodationCollection();
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dr = (DataRow)obj2;
					ClockWorkWebAPI.Accommodation accommodation2 = new ClockWorkWebAPI.Accommodation(dr, languageCode);
					accommodationCollection2.Add(accommodation2);
				}
				dataTable.Rows.Clear();
				dataTable.Dispose();
				dataTable = null;
				result = accommodationCollection2;
			}
			return result;
		}
	}
}

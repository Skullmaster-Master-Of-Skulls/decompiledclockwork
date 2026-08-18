using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x020003FB RID: 1019
	public class MigrationAppointment : IMigrationDataItems
	{
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00022F00 File Offset: 0x00021100
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x00022F08 File Offset: 0x00021108
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00022F11 File Offset: 0x00021111
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x00022F19 File Offset: 0x00021119
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00022F22 File Offset: 0x00021122
		// (set) Token: 0x06001F2D RID: 7981 RVA: 0x00022F2A File Offset: 0x0002112A
		public string Subject { get; set; }

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x00022F33 File Offset: 0x00021133
		// (set) Token: 0x06001F2F RID: 7983 RVA: 0x00022F3B File Offset: 0x0002113B
		public string Memo { get; set; }

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00022F44 File Offset: 0x00021144
		// (set) Token: 0x06001F31 RID: 7985 RVA: 0x00022F4C File Offset: 0x0002114C
		public IList<MigrationDataItem> DataItems { get; set; }

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x00022F55 File Offset: 0x00021155
		// (set) Token: 0x06001F33 RID: 7987 RVA: 0x00022F5D File Offset: 0x0002115D
		public string StudentId { get; set; }

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00022F66 File Offset: 0x00021166
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x00022F6E File Offset: 0x0002116E
		public string StaffId { get; set; }

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x00022F77 File Offset: 0x00021177
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x00022F7F File Offset: 0x0002117F
		public string Location { get; set; }

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x00022F88 File Offset: 0x00021188
		// (set) Token: 0x06001F39 RID: 7993 RVA: 0x00022F90 File Offset: 0x00021190
		public bool IsCancelled { get; set; }

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x00022F99 File Offset: 0x00021199
		// (set) Token: 0x06001F3B RID: 7995 RVA: 0x00022FA1 File Offset: 0x000211A1
		public bool IsTentative { get; set; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00022FAA File Offset: 0x000211AA
		// (set) Token: 0x06001F3D RID: 7997 RVA: 0x00022FB2 File Offset: 0x000211B2
		public bool IsNoShow { get; set; }

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x00022FBB File Offset: 0x000211BB
		// (set) Token: 0x06001F3F RID: 7999 RVA: 0x00022FC3 File Offset: 0x000211C3
		public bool IsPrivate { get; set; }

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00022FCC File Offset: 0x000211CC
		// (set) Token: 0x06001F41 RID: 8001 RVA: 0x00022FD4 File Offset: 0x000211D4
		public string ExternalAppId { get; set; }

		// Token: 0x06001F42 RID: 8002 RVA: 0x00022FE0 File Offset: 0x000211E0
		public static MigrationAppointment GetMigrationAppointmentFromDataRow(DataRow dr, IList<MigrationMapperDataItem> mapperItems)
		{
			DataTable table = dr.Table;
			return new MigrationAppointment
			{
				StartDateTime = MigrationAppointment.GetDateTimeFromDataRow(dr, "startdate"),
				EndDateTime = MigrationAppointment.GetDateTimeFromDataRow(dr, "enddate"),
				Subject = (table.Columns.Contains("apptype") ? dr["apptype"].ToString().Trim() : (table.Columns.Contains("subject") ? dr["subject"].ToString().Trim() : (table.Columns.Contains("title") ? dr["title"].ToString().Trim() : ""))),
				Memo = (table.Columns.Contains("memo") ? dr["memo"].ToString().Trim() : ""),
				StudentId = (table.Columns.Contains("student_no") ? dr["student_no"].ToString().Trim().ToUpper() : ""),
				StaffId = (table.Columns.Contains("staff_student_no") ? dr["staff_student_no"].ToString().Trim().ToUpper() : ""),
				Location = (table.Columns.Contains("location") ? dr["location"].ToString().Trim() : ""),
				IsCancelled = MigrationAppointment.GetBoolFromDataRow(table, dr, "cancelled"),
				IsTentative = MigrationAppointment.GetBoolFromDataRow(table, dr, "tentative"),
				IsNoShow = MigrationAppointment.GetBoolFromDataRow(table, dr, "noshow"),
				IsPrivate = MigrationAppointment.GetBoolFromDataRow(table, dr, "private"),
				ExternalAppId = (table.Columns.Contains("externalappid") ? dr["externalappid"].ToString().Trim() : ""),
				DataItems = MigrationDataItem.GetMigrationDataFromDataRow(dr, mapperItems)
			};
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00023218 File Offset: 0x00021418
		private static bool GetBoolFromDataRow(DataTable t, DataRow dr, string colName)
		{
			return t.Columns.Contains(colName) && dr[colName] != DBNull.Value && ((t.Columns[colName].DataType == typeof(bool) && (bool)dr[colName]) || "1trueyes".IndexOf(dr[colName].ToString()) >= 0);
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00023298 File Offset: 0x00021498
		private static DateTime GetDateTimeFromDataRow(DataRow dr, string colName)
		{
			DateTime minValue = DateTime.MinValue;
			bool flag = dr[colName] is DBNull;
			DateTime result;
			if (flag)
			{
				result = minValue;
			}
			else
			{
				bool flag2 = dr.Table.Columns[colName].DataType == typeof(DateTime);
				if (flag2)
				{
					result = (DateTime)dr[colName];
				}
				else
				{
					string s = dr[colName].ToString();
					DateTime dateTime;
					result = ((!DateTime.TryParse(s, out dateTime)) ? minValue : dateTime);
				}
			}
			return result;
		}
	}
}

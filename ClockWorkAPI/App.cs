using System;
using System.Data;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000028 RID: 40
	public class App
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000C928 File Offset: 0x0000B928
		private static int GetBinaryMask(params int[] powers)
		{
			int num = 0;
			foreach (int num2 in powers)
			{
				int num3 = Convert.ToInt32(Math.Pow(2.0, (double)num2));
				num += num3;
			}
			return num;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C97C File Offset: 0x0000B97C
		public static string GetAccessLevelString(int access_level)
		{
			return access_level.ToString();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000C998 File Offset: 0x0000B998
		public static int GetPersonPermissionsForUser(int personid_whosSchedule, int personid_whosAsking, UnivDataAdapter da, bool appointmentPermissionsEnabled)
		{
			int result;
			if (personid_whosSchedule == personid_whosAsking || !appointmentPermissionsEnabled)
			{
				result = App.AppointmentPermissions.FULL_ACCESS;
			}
			else
			{
				da.SelectCommand.CommandText = "EXECUTE procAppointmentPermissionsSelect_getPermissionsForSchedule @personid_whosSchedule,@personid_whosAsking";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@personid_whosSchedule", personid_whosSchedule);
				da.SelectCommand.Parameters.Add("@personid_whosAsking", personid_whosAsking);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					int num = (int)dataTable.Rows[0][0];
					result = num;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000CA5C File Offset: 0x0000BA5C
		public static int GetPersonPermissionsForUser(int personid_whosSchedule, int personid_whosAsking, bool appointmentPermissionsEnabled)
		{
			int result;
			if (personid_whosSchedule == personid_whosAsking || !appointmentPermissionsEnabled)
			{
				result = App.AppointmentPermissions.FULL_ACCESS;
			}
			else
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText = "EXECUTE procAppointmentPermissionsSelect_getPermissionsForSchedule @personid_whosSchedule,@personid_whosAsking";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@personid_whosSchedule", personid_whosSchedule);
				da.SelectCommand.Parameters.Add("@personid_whosAsking", personid_whosAsking);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					int num = (int)dataTable.Rows[0][0];
					result = num;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x02000029 RID: 41
		public struct AppointmentPermissions
		{
			// Token: 0x04000108 RID: 264
			public static int NONE = 1;

			// Token: 0x04000109 RID: 265
			public static int view_unused1 = 2;

			// Token: 0x0400010A RID: 266
			public static int view_unused2 = 4;

			// Token: 0x0400010B RID: 267
			public static int view_unused3 = 8;

			// Token: 0x0400010C RID: 268
			public static int view_unused4 = 16;

			// Token: 0x0400010D RID: 269
			public static int view_time = 32;

			// Token: 0x0400010E RID: 270
			public static int view_description = 64;

			// Token: 0x0400010F RID: 271
			public static int view_memo = 128;

			// Token: 0x04000110 RID: 272
			public static int view_attendees = 256;

			// Token: 0x04000111 RID: 273
			public static int view_room = 512;

			// Token: 0x04000112 RID: 274
			public static int view_courseInfo = 1024;

			// Token: 0x04000113 RID: 275
			public static int view_accommodations = 2048;

			// Token: 0x04000114 RID: 276
			public static int view_icons = 4096;

			// Token: 0x04000115 RID: 277
			public static int view_studentAttendees = 8192;

			// Token: 0x04000116 RID: 278
			public static int view_unused6 = 16384;

			// Token: 0x04000117 RID: 279
			public static int VIEW_ALL = App.GetBinaryMask(new int[]
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14
			});

			// Token: 0x04000118 RID: 280
			public static int modify_date = 32768;

			// Token: 0x04000119 RID: 281
			public static int modify_time = 65536;

			// Token: 0x0400011A RID: 282
			public static int modify_description = 131072;

			// Token: 0x0400011B RID: 283
			public static int modify_memo = 262144;

			// Token: 0x0400011C RID: 284
			public static int modify_attendees = 524288;

			// Token: 0x0400011D RID: 285
			public static int modify_courseInfo = 1048576;

			// Token: 0x0400011E RID: 286
			public static int modify_room = 2097152;

			// Token: 0x0400011F RID: 287
			public static int modify_unused1 = 4194304;

			// Token: 0x04000120 RID: 288
			public static int modify_unused2 = 8388608;

			// Token: 0x04000121 RID: 289
			public static int modify_unused3 = 16777216;

			// Token: 0x04000122 RID: 290
			public static int modify_unused4 = 33554432;

			// Token: 0x04000123 RID: 291
			public static int MODIFY_ALL = App.GetBinaryMask(new int[]
			{
				15,
				16,
				17,
				18,
				19,
				20,
				21,
				22,
				23,
				24,
				25
			});

			// Token: 0x04000124 RID: 292
			public static int unused1 = 67108864;

			// Token: 0x04000125 RID: 293
			public static int delete = 134217728;

			// Token: 0x04000126 RID: 294
			public static int cancel = 268435456;

			// Token: 0x04000127 RID: 295
			public static int create = 536870912;

			// Token: 0x04000128 RID: 296
			public static int unused2 = 1073741824;

			// Token: 0x04000129 RID: 297
			public static int FULL_ACCESS = App.GetBinaryMask(new int[]
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15,
				16,
				17,
				18,
				19,
				20,
				21,
				22,
				23,
				24,
				25,
				26,
				27,
				28,
				29,
				30
			});
		}
	}
}

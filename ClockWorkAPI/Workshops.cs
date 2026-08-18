using System;
using System.Data;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x0200002A RID: 42
	public class Workshops
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000CDD0 File Offset: 0x0000BDD0
		public static int[] GetAllAppTypeIdsWithUnmarkedWorkshops(UnivDataAdapter da)
		{
			da.SelectCommand.CommandText = "SELECT DISTINCT at.apptypeid FROM appointmenttypes at LEFT JOIN appointments app ON app.apptypeid=at.apptypeid LEFT JOIN appointmentworkshops aw ON aw.appointmentid=app.appointmentid WHERE at.isworkshop=1 AND (aw.workshopid IS NULL OR aw.workshopid < 0)";
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			int[] array;
			if (dataTable.Rows.Count > 0)
			{
				array = new int[dataTable.Rows.Count];
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.Rows[i];
					array[i] = (int)dataRow[0];
				}
			}
			else
			{
				array = null;
			}
			dataTable.Dispose();
			return array;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using ReportFunctions.ClockWorkDataSync.Courses;
using ReportFunctions.ClockWorkDataSync.ServiceProviders.ServiceProviderData;

namespace ReportFunctions.ClockWorkDataSync
{
	// Token: 0x02000054 RID: 84
	public class Utility
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x000501D0 File Offset: 0x0004F1D0
		private static DataTable GetEmptyBaseActionsTable()
		{
			return new DataTable("actions")
			{
				Columns = 
				{
					"ActionResult",
					"ActionType",
					{
						"pid",
						typeof(int)
					}
				}
			};
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0005022C File Offset: 0x0004F22C
		private static DataRow GetNewBaseActionRow(DataTable t, DataSyncAction action)
		{
			DataRow dataRow = t.NewRow();
			dataRow["pid"] = action.Pid;
			dataRow["ActionType"] = action.ActionType.ToString();
			dataRow["ActionResult"] = action.ActionResult.ToString();
			return dataRow;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00050298 File Offset: 0x0004F298
		public static DataTable TableFromActions(List<DataSyncAction> actions)
		{
			DataTable emptyBaseActionsTable = Utility.GetEmptyBaseActionsTable();
			foreach (DataSyncAction action in actions)
			{
				DataRow newBaseActionRow = Utility.GetNewBaseActionRow(emptyBaseActionsTable, action);
				emptyBaseActionsTable.Rows.Add(newBaseActionRow);
			}
			return emptyBaseActionsTable;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0005030C File Offset: 0x0004F30C
		public static DataTable TableFromActions(List<ServiceProviderDataSyncDataItemAction> actions)
		{
			DataTable emptyBaseActionsTable = Utility.GetEmptyBaseActionsTable();
			foreach (ServiceProviderDataSyncDataItemAction action in actions)
			{
				DataRow newBaseActionRow = Utility.GetNewBaseActionRow(emptyBaseActionsTable, action);
				emptyBaseActionsTable.Rows.Add(newBaseActionRow);
			}
			return emptyBaseActionsTable;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00050380 File Offset: 0x0004F380
		public static DataTable TableFromActions(List<DataSyncCourseAction> actions)
		{
			DataTable emptyBaseActionsTable = Utility.GetEmptyBaseActionsTable();
			foreach (DataSyncCourseAction action in actions)
			{
				DataRow newBaseActionRow = Utility.GetNewBaseActionRow(emptyBaseActionsTable, action);
				emptyBaseActionsTable.Rows.Add(newBaseActionRow);
			}
			return emptyBaseActionsTable;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x02000409 RID: 1033
	public class MigrationAppointmentItemResult
	{
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x00023D84 File Offset: 0x00021F84
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x00023D8C File Offset: 0x00021F8C
		public eMigrationAppointmentItemStatus Status { get; set; }

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x00023D95 File Offset: 0x00021F95
		// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x00023D9D File Offset: 0x00021F9D
		public string ErrorMessage { get; set; }

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x00023DA6 File Offset: 0x00021FA6
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x00023DAE File Offset: 0x00021FAE
		public MigrationAppointment ExternalAppointment { get; set; }

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00023DB7 File Offset: 0x00021FB7
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x00023DBF File Offset: 0x00021FBF
		public IList<MigrationDataItemResult> DataItemResults { get; set; }

		// Token: 0x06001FAB RID: 8107 RVA: 0x00023DC8 File Offset: 0x00021FC8
		public static DataTable ListToDataTable(IList<MigrationAppointmentItemResult> items)
		{
			bool flag = items == null;
			if (flag)
			{
				items = new List<MigrationAppointmentItemResult>();
			}
			bool flag2 = items.Count < 1;
			bool flag3 = flag2;
			if (flag3)
			{
				items.Add(new MigrationAppointmentItemResult());
			}
			var list = (from item in items
			select new
			{
				item = item,
				app = item.ExternalAppointment
			}).Select(delegate(<>h__TransparentIdentifier0)
			{
				string status = <>h__TransparentIdentifier0.item.Status.ToString();
				string errorMessage = <>h__TransparentIdentifier0.item.ErrorMessage;
				MigrationAppointment app = <>h__TransparentIdentifier0.app;
				string externalAppId = (app != null) ? app.ExternalAppId : null;
				MigrationAppointment app2 = <>h__TransparentIdentifier0.app;
				string startDateTime = (app2 != null) ? app2.StartDateTime.ToString("yyyy-MM-dd") : null;
				MigrationAppointment app3 = <>h__TransparentIdentifier0.app;
				string endDateTime = (app3 != null) ? app3.EndDateTime.ToString("yyyy-MM-dd") : null;
				MigrationAppointment app4 = <>h__TransparentIdentifier0.app;
				string studentId = (app4 != null) ? app4.StudentId : null;
				MigrationAppointment app5 = <>h__TransparentIdentifier0.app;
				string staffId = (app5 != null) ? app5.StaffId : null;
				MigrationAppointment app6 = <>h__TransparentIdentifier0.app;
				string subject = (app6 != null) ? app6.Subject : null;
				MigrationAppointment app7 = <>h__TransparentIdentifier0.app;
				string isCancelled = (app7 != null) ? app7.IsCancelled.ToString() : null;
				MigrationAppointment app8 = <>h__TransparentIdentifier0.app;
				string isNoShow = (app8 != null) ? app8.IsNoShow.ToString() : null;
				MigrationAppointment app9 = <>h__TransparentIdentifier0.app;
				string isTentative = (app9 != null) ? app9.IsTentative.ToString() : null;
				MigrationAppointment app10 = <>h__TransparentIdentifier0.app;
				string isPrivate = (app10 != null) ? app10.IsPrivate.ToString() : null;
				MigrationAppointment app11 = <>h__TransparentIdentifier0.app;
				string location = (app11 != null) ? app11.Location : null;
				MigrationAppointment app12 = <>h__TransparentIdentifier0.app;
				string memo = (app12 != null) ? app12.Memo : null;
				MigrationAppointment app13 = <>h__TransparentIdentifier0.app;
				string dataItems;
				if (((app13 != null) ? app13.DataItems : null) != null)
				{
					dataItems = string.Join<char[]>("\r\n", from g in <>h__TransparentIdentifier0.app.DataItems
					select ((g.DataName ?? "") + "=" + (g.DataValue ?? "")).ToArray<char>());
				}
				else
				{
					dataItems = "";
				}
				return new
				{
					Status = status,
					ErrorMessage = errorMessage,
					ExternalAppId = externalAppId,
					StartDateTime = startDateTime,
					EndDateTime = endDateTime,
					StudentId = studentId,
					StaffId = staffId,
					Subject = subject,
					IsCancelled = isCancelled,
					IsNoShow = isNoShow,
					IsTentative = isTentative,
					IsPrivate = isPrivate,
					Location = location,
					Memo = memo,
					DataItems = dataItems
				};
			}).ToList();
			var <>f__AnonymousType = list[0];
			bool flag4 = flag2;
			if (flag4)
			{
				items.Clear();
			}
			Type type = <>f__AnonymousType.GetType();
			PropertyInfo[] properties = type.GetProperties();
			DataTable dataTable = new DataTable("t3");
			foreach (PropertyInfo propertyInfo in properties)
			{
				dataTable.Columns.Add(propertyInfo.Name);
			}
			foreach (var obj in list)
			{
				object[] array2 = new object[properties.Length];
				for (int j = 0; j < properties.Length; j++)
				{
					string text = properties[j].GetValue(obj, null) as string;
					array2[j] = (text ?? "");
				}
				dataTable.Rows.Add(array2);
			}
			return dataTable;
		}
	}
}

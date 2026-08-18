using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x0200040B RID: 1035
	public class MigrationCreateStudentResult
	{
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00023F58 File Offset: 0x00022158
		// (set) Token: 0x06001FAE RID: 8110 RVA: 0x00023F60 File Offset: 0x00022160
		public string StudentNumber { get; set; }

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x00023F69 File Offset: 0x00022169
		// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x00023F71 File Offset: 0x00022171
		public eMigrationCreateStudentStatus Status { get; set; }

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x00023F7A File Offset: 0x0002217A
		// (set) Token: 0x06001FB2 RID: 8114 RVA: 0x00023F82 File Offset: 0x00022182
		public string ErrorMessage { get; set; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x00023F8B File Offset: 0x0002218B
		// (set) Token: 0x06001FB4 RID: 8116 RVA: 0x00023F93 File Offset: 0x00022193
		public int PersonId { get; set; }

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00023F9C File Offset: 0x0002219C
		public static DataTable ConvertToDataTable(IList<MigrationCreateStudentResult> items)
		{
			bool flag = items == null;
			if (flag)
			{
				items = new List<MigrationCreateStudentResult>();
			}
			bool flag2 = items.Count < 1;
			bool flag3 = flag2;
			if (flag3)
			{
				items.Add(new MigrationCreateStudentResult());
			}
			var list = (from item in items
			select new
			{
				Status = item.Status.ToString(),
				ErrorMessage = item.ErrorMessage,
				PersonId = item.PersonId.ToString(),
				StudentNumber = item.StudentNumber
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

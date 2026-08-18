using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using TechnoPro.Common.Public.Entities.DataMigration.Internal;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x02000411 RID: 1041
	public class MigrationFileItemResult
	{
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x000242B7 File Offset: 0x000224B7
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x000242BF File Offset: 0x000224BF
		public eMigrationAppointmentItemStatus Status { get; set; }

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x000242C8 File Offset: 0x000224C8
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x000242D0 File Offset: 0x000224D0
		public string ErrorMessage { get; set; }

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000242D9 File Offset: 0x000224D9
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x000242E1 File Offset: 0x000224E1
		public string StudentNumber { get; set; }

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x000242EA File Offset: 0x000224EA
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x000242F2 File Offset: 0x000224F2
		public int PersonId { get; set; }

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x000242FB File Offset: 0x000224FB
		// (set) Token: 0x06001FD3 RID: 8147 RVA: 0x00024303 File Offset: 0x00022503
		public IList<MigrationFileInfo> ExternalFiles { get; set; }

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0002430C File Offset: 0x0002250C
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x00024314 File Offset: 0x00022514
		public string FileListContents { get; set; }

		// Token: 0x06001FD6 RID: 8150 RVA: 0x00024320 File Offset: 0x00022520
		public static DataTable ListToDataTable(IList<MigrationFileItemResult> items)
		{
			bool flag = items == null;
			if (flag)
			{
				items = new List<MigrationFileItemResult>();
			}
			bool flag2 = items.Count < 1;
			bool flag3 = flag2;
			if (flag3)
			{
				items.Add(new MigrationFileItemResult());
			}
			var list = (from item in items
			let extFiles = item.ExternalFiles ?? new List<MigrationFileInfo>()
			select new
			{
				Status = item.Status.ToString(),
				ErrorMessage = item.ErrorMessage,
				StudentNumber = item.StudentNumber,
				Pid = item.PersonId,
				FileListContents = item.FileListContents,
				ExternalFiles = string.Join(", ", from g in extFiles
				select g.UniqueFilenameWithoutPath)
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

using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.Public.Entities.DataMigration.Results
{
	// Token: 0x0200040D RID: 1037
	public class MigrationDataItemResult
	{
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x00024108 File Offset: 0x00022308
		// (set) Token: 0x06001FB8 RID: 8120 RVA: 0x00024110 File Offset: 0x00022310
		public string StudentNumber { get; set; }

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x00024119 File Offset: 0x00022319
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x00024121 File Offset: 0x00022321
		public string DataItemName { get; set; }

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0002412A File Offset: 0x0002232A
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x00024132 File Offset: 0x00022332
		public string DataItemValue { get; set; }

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0002413B File Offset: 0x0002233B
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x00024143 File Offset: 0x00022343
		public eMigrationDataItemStatus Status { get; set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0002414C File Offset: 0x0002234C
		// (set) Token: 0x06001FC0 RID: 8128 RVA: 0x00024154 File Offset: 0x00022354
		public string ErrorMessage { get; set; }

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00024160 File Offset: 0x00022360
		public static DataTable ListToDataTable(IList<MigrationDataItemResult> items)
		{
			DataTable dataTable = new DataTable("t3");
			dataTable.Columns.Add("Status");
			dataTable.Columns.Add("student_no");
			dataTable.Columns.Add("DataItemName");
			dataTable.Columns.Add("DataItemValue");
			dataTable.Columns.Add("ErrorMessage");
			foreach (MigrationDataItemResult migrationDataItemResult in items)
			{
				dataTable.Rows.Add(new object[]
				{
					migrationDataItemResult.Status.ToString(),
					migrationDataItemResult.StudentNumber ?? "NULL",
					migrationDataItemResult.DataItemName ?? "NULL",
					migrationDataItemResult.DataItemValue ?? "",
					migrationDataItemResult.ErrorMessage ?? ""
				});
			}
			return dataTable;
		}
	}
}

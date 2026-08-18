using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x02000402 RID: 1026
	public class MigrationStudentWithData : IMigrationDataItems
	{
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x00023672 File Offset: 0x00021872
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x0002367A File Offset: 0x0002187A
		public MigrationStudent Student { get; set; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00023683 File Offset: 0x00021883
		// (set) Token: 0x06001F74 RID: 8052 RVA: 0x0002368B File Offset: 0x0002188B
		public IList<MigrationDataItem> DataItems { get; set; }

		// Token: 0x06001F75 RID: 8053 RVA: 0x00023694 File Offset: 0x00021894
		public static DataTable ListToDataTable(IList<MigrationStudentWithData> studentsWithData)
		{
			DataTable dataTable = new DataTable("q");
			dataTable.Columns.Add("student_no");
			dataTable.Columns.Add("dataitem");
			dataTable.Columns.Add("datavalue");
			foreach (MigrationStudentWithData migrationStudentWithData in studentsWithData)
			{
				string text = (migrationStudentWithData.Student == null) ? "NULL" : (migrationStudentWithData.Student.StudentNumber ?? "NULL2");
				bool flag = migrationStudentWithData.DataItems == null || migrationStudentWithData.DataItems.Count < 1;
				if (flag)
				{
					dataTable.Rows.Add(new object[]
					{
						text,
						"No data items",
						""
					});
				}
				else
				{
					foreach (MigrationDataItem migrationDataItem in migrationStudentWithData.DataItems)
					{
						dataTable.Rows.Add(new object[]
						{
							text,
							migrationDataItem.DataName,
							(migrationDataItem.DataValue == null) ? "NULL" : migrationDataItem.DataValue.ToString()
						});
					}
				}
			}
			return dataTable;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00023810 File Offset: 0x00021A10
		public static MigrationStudentWithData GetMigrationStudentWithDataFromDataRowUsingMapperItems(DataRow dr, MigrationStudent student, IList<MigrationMapperDataItem> mapperItems)
		{
			return new MigrationStudentWithData
			{
				Student = student,
				DataItems = MigrationDataItem.GetMigrationDataFromDataRow(dr, mapperItems)
			};
		}
	}
}

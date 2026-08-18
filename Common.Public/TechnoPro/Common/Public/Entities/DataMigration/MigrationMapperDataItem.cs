using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x02000404 RID: 1028
	public class MigrationMapperDataItem
	{
		// Token: 0x06001F7F RID: 8063 RVA: 0x0000D55A File Offset: 0x0000B75A
		public MigrationMapperDataItem()
		{
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00023879 File Offset: 0x00021A79
		public MigrationMapperDataItem(string DataName, int ClockWorkCid)
		{
			this.Add(DataName, ClockWorkCid);
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0002388C File Offset: 0x00021A8C
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x00023894 File Offset: 0x00021A94
		public IList<string> DataNamesOrdered { get; set; }

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x0002389D File Offset: 0x00021A9D
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x000238A5 File Offset: 0x00021AA5
		public int ClockWorkCid { get; set; }

		// Token: 0x06001F85 RID: 8069 RVA: 0x000238AE File Offset: 0x00021AAE
		public void Add(string DataName, int ClockWorkCid)
		{
			this.Add(new List<string>
			{
				DataName
			}, ClockWorkCid);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000238C8 File Offset: 0x00021AC8
		public void Add(IList<string> DataNames, int ClockWorkCid)
		{
			bool flag = this.DataNamesOrdered == null;
			if (flag)
			{
				this.DataNamesOrdered = new List<string>();
			}
			foreach (string item in DataNames)
			{
				this.DataNamesOrdered.Add(item);
			}
			this.ClockWorkCid = ClockWorkCid;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0002393C File Offset: 0x00021B3C
		public static DataTable ToDataTable(IList<MigrationMapperDataItem> items)
		{
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("ClockWorkCid", typeof(int));
			dataTable.Columns.Add("ExtraNames");
			bool flag = items == null;
			DataTable result;
			if (flag)
			{
				result = dataTable;
			}
			else
			{
				foreach (MigrationMapperDataItem migrationMapperDataItem in items)
				{
					string text = (migrationMapperDataItem.DataNamesOrdered == null || migrationMapperDataItem.DataNamesOrdered.Count < 1) ? "" : (migrationMapperDataItem.DataNamesOrdered[0] ?? "");
					string text2 = (migrationMapperDataItem.DataNamesOrdered != null && migrationMapperDataItem.DataNamesOrdered.Count > 0) ? string.Join(", ", migrationMapperDataItem.DataNamesOrdered.ToList<string>().GetRange(1, migrationMapperDataItem.DataNamesOrdered.Count - 1).ToArray()) : "";
					dataTable.Rows.Add(new object[]
					{
						text,
						migrationMapperDataItem.ClockWorkCid,
						text2
					});
				}
				result = dataTable;
			}
			return result;
		}
	}
}

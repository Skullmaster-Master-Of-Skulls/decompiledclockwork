using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x020003FD RID: 1021
	public class MigrationDataItem : BusinessBase<string>
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x000233DC File Offset: 0x000215DC
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string DataName
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x000233F4 File Offset: 0x000215F4
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x000233FC File Offset: 0x000215FC
		public object DataValue { get; set; }

		// Token: 0x06001F61 RID: 8033 RVA: 0x00023408 File Offset: 0x00021608
		public static IList<MigrationDataItem> GetMigrationDataFromDataRow(DataRow dr, IList<MigrationMapperDataItem> mapperItems)
		{
			List<MigrationDataItem> list = new List<MigrationDataItem>();
			DataTable table = dr.Table;
			foreach (MigrationMapperDataItem migrationMapperDataItem in mapperItems)
			{
				IList<string> dataNamesOrdered = migrationMapperDataItem.DataNamesOrdered;
				bool flag = dataNamesOrdered.Count <= 1;
				if (flag)
				{
					string text = (dataNamesOrdered.Count == 0) ? dataNamesOrdered[0] : "";
					bool flag2 = table.Columns.Contains(text);
					if (flag2)
					{
						list.Add(new MigrationDataItem
						{
							DataName = text,
							DataValue = ((dr[text] is DBNull) ? null : dr[text])
						});
					}
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					string text2 = dataNamesOrdered[0] + "_" + migrationMapperDataItem.ClockWorkCid.ToString();
					bool flag3 = !table.Columns.Contains(text2);
					if (flag3)
					{
						table.Columns.Add(text2);
					}
					foreach (string columnName in dataNamesOrdered)
					{
						object value = (dr[columnName] is DBNull) ? "" : dr[columnName];
						bool flag4 = stringBuilder.Length > 0;
						if (flag4)
						{
							stringBuilder.Append('\0');
						}
						stringBuilder.Append(value);
					}
					list.Add(new MigrationDataItem
					{
						DataName = text2,
						DataValue = stringBuilder.ToString()
					});
					migrationMapperDataItem.DataNamesOrdered = new List<string>
					{
						text2
					};
				}
			}
			return list;
		}
	}
}

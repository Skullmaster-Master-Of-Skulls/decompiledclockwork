using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003EF RID: 1007
	public class DataSyncDataItemListData : DataSyncDataItemBase
	{
		// Token: 0x06001EE8 RID: 7912 RVA: 0x00022403 File Offset: 0x00020603
		public DataSyncDataItemListData()
		{
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0002240D File Offset: 0x0002060D
		public DataSyncDataItemListData(byte[] data)
		{
			base.DataValue = data;
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00022AC4 File Offset: 0x00020CC4
		public IList<DynamicDataItemListRow> Rows
		{
			get
			{
				bool flag = base.DataValue is List<DynamicDataItemListRow>;
				IList<DynamicDataItemListRow> result;
				if (flag)
				{
					result = (IList<DynamicDataItemListRow>)base.DataValue;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x00022AF7 File Offset: 0x00020CF7
		public override bool HasValue
		{
			get
			{
				IList<DynamicDataItemListRow> rows = this.Rows;
				return rows != null && rows.Count > 0;
			}
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00022B10 File Offset: 0x00020D10
		public override bool Equals(DataSyncDataItemBase item)
		{
			bool flag = !base.CheckEqualsShallow(item);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DataSyncDataItemListData dataSyncDataItemListData = item.ConvertTo<DataSyncDataItemListData>();
				IList<DynamicDataItemListRow> list = this.Rows ?? new List<DynamicDataItemListRow>();
				IList<DynamicDataItemListRow> list2 = dataSyncDataItemListData.Rows ?? new List<DynamicDataItemListRow>();
				bool flag2 = list.Count != list2.Count;
				if (flag2)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < list.Count; i++)
					{
					}
					result = true;
				}
			}
			return result;
		}
	}
}

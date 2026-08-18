using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x02000396 RID: 918
	public class DynamicDataItemListView : DynamicDataItemBase<IList<DynamicDataItemListRow>>
	{
		// Token: 0x06001C2A RID: 7210 RVA: 0x000202C0 File Offset: 0x0001E4C0
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			base.Value = ((item == null || string.IsNullOrEmpty(item.OtherValue)) ? null : DynamicDataItemListRow.DeSerializeRows(item.OtherValue));
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000202E8 File Offset: 0x0001E4E8
		public override DynamicDataStorageItem WriteToStorage()
		{
			bool flag = base.Value == null || base.Value.Count < 1;
			DynamicDataStorageItem result;
			if (flag)
			{
				result = new DynamicDataStorageItem(base.Field);
			}
			else
			{
				result = new DynamicDataStorageItem(base.Field)
				{
					OtherValue = DynamicDataItemListRow.SerializeRows(base.Value)
				};
			}
			return result;
		}
	}
}

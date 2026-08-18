using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x02000398 RID: 920
	public class DynamicDataItemMultiCheckbox : DynamicDataItemBase<IList<bool>>
	{
		// Token: 0x06001C30 RID: 7216 RVA: 0x00020378 File Offset: 0x0001E578
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null || item.IntValue == null;
			if (flag)
			{
				base.Clear();
			}
			else
			{
				base.Value = new List<bool>();
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000203B8 File Offset: 0x0001E5B8
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
				result = new DynamicDataStorageItem(base.Field);
			}
			return result;
		}
	}
}

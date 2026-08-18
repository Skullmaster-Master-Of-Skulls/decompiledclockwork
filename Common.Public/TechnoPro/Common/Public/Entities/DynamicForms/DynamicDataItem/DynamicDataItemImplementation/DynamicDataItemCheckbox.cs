using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200038C RID: 908
	public class DynamicDataItemCheckbox : DynamicDataItemBase<DynamicDataItemBool>
	{
		// Token: 0x06001C03 RID: 7171 RVA: 0x0001FC3C File Offset: 0x0001DE3C
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			base.Value = ((item == null || item.IntValue == null) ? null : new DynamicDataItemBool(item.IntValue.Value != 0));
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0001FC80 File Offset: 0x0001DE80
		public override DynamicDataStorageItem WriteToStorage()
		{
			return new DynamicDataStorageItem(base.Field)
			{
				IntValue = new int?(base.Value.IsChecked ? 1 : 0)
			};
		}
	}
}

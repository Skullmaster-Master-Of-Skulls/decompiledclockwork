using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200039D RID: 925
	public class DynamicDataItemRadioButtonGroup : DynamicDataItemBase<DynamicDataItemListItem>
	{
		// Token: 0x06001C3E RID: 7230 RVA: 0x000204AC File Offset: 0x0001E6AC
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null;
			if (flag)
			{
				base.Clear();
			}
			else
			{
				base.Value = new DynamicDataItemListItem
				{
					Id = ((item.IntValue != null) ? item.IntValue.Value : 0),
					Title = (item.OtherValue ?? "")
				};
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00020518 File Offset: 0x0001E718
		public override DynamicDataStorageItem WriteToStorage()
		{
			DynamicDataItemListItem value = base.Value;
			return new DynamicDataStorageItem(base.Field)
			{
				IntValue = ((value != null) ? new int?(value.Id) : null),
				OtherValue = ((value != null) ? (value.Title ?? "") : null)
			};
		}
	}
}

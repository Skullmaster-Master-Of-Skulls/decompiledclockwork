using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x02000390 RID: 912
	public class DynamicDataItemDropListGeneral : DynamicDataItemBase<DynamicDataItemListItem>
	{
		// Token: 0x06001C10 RID: 7184 RVA: 0x0001FDB7 File Offset: 0x0001DFB7
		public new void Clear()
		{
			base.Value = null;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0001FDC4 File Offset: 0x0001DFC4
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null || item.IntValue == null;
			if (flag)
			{
				this.Clear();
			}
			else
			{
				base.Value = new DynamicDataItemListItem
				{
					Id = item.IntValue.Value,
					Title = (item.OtherValue ?? "")
				};
			}
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0001FE30 File Offset: 0x0001E030
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

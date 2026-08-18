using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200039C RID: 924
	public class DynamicDataItemPicture : DynamicDataItemBase<byte[]>
	{
		// Token: 0x06001C3C RID: 7228 RVA: 0x00020414 File Offset: 0x0001E614
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null || item.ImageValue == null || item.ImageValue.Length < 1;
			if (flag)
			{
				base.Clear();
			}
			else
			{
				base.Value = item.ImageValue;
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00020458 File Offset: 0x0001E658
		public override DynamicDataStorageItem WriteToStorage()
		{
			bool flag = base.Value == null || base.Value.Length < 1;
			DynamicDataStorageItem result;
			if (flag)
			{
				result = new DynamicDataStorageItem(base.Field);
			}
			else
			{
				result = new DynamicDataStorageItem(base.Field)
				{
					ImageValue = base.Value
				};
			}
			return result;
		}
	}
}

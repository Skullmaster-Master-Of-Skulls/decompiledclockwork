using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x02000392 RID: 914
	public class DynamicDataItemFile : DynamicDataItemBase<DynamicDataBinaryFile>
	{
		// Token: 0x06001C1A RID: 7194 RVA: 0x0001FEC8 File Offset: 0x0001E0C8
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null || item.ImageValue == null || item.ImageValue.Length < 1;
			if (flag)
			{
				base.Clear();
			}
			else
			{
				base.Value = new DynamicDataBinaryFile();
				base.Value.Deserialize(item.ImageValue);
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0001FF1C File Offset: 0x0001E11C
		public override DynamicDataStorageItem WriteToStorage()
		{
			bool flag = base.Value == null || base.Value.Data.Length < 1;
			DynamicDataStorageItem result;
			if (flag)
			{
				result = new DynamicDataStorageItem(base.Field);
			}
			else
			{
				result = new DynamicDataStorageItem(base.Field)
				{
					ImageValue = base.Value.Serialize()
				};
			}
			return result;
		}
	}
}

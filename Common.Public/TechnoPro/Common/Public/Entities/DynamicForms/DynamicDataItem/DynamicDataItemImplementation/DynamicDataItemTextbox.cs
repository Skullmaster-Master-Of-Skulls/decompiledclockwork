using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x020003A0 RID: 928
	public class DynamicDataItemTextbox : DynamicDataItemBase<string>
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x00020804 File Offset: 0x0001EA04
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			base.Value = ((item == null) ? null : item.OtherValue);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0002081C File Offset: 0x0001EA1C
		public override DynamicDataStorageItem WriteToStorage()
		{
			return new DynamicDataStorageItem(base.Field)
			{
				OtherValue = base.Value
			};
		}
	}
}

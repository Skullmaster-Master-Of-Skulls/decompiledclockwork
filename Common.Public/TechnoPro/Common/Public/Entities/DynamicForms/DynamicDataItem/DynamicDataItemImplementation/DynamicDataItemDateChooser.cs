using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200038E RID: 910
	public class DynamicDataItemDateChooser : DynamicDataItemBase<DynamicDataItemDateValue>
	{
		// Token: 0x06001C0A RID: 7178 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null || item.DateTimeValue == null;
			if (flag)
			{
				base.Clear();
			}
			else
			{
				base.Value = new DynamicDataItemDateValue(item.DateTimeValue);
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0001FD30 File Offset: 0x0001DF30
		public override DynamicDataStorageItem WriteToStorage()
		{
			return new DynamicDataStorageItem(base.Field)
			{
				DateTimeValue = ((base.Value == null || base.Value.Value == null) ? null : new DateTime?(base.Value.Value.Value))
			};
		}
	}
}

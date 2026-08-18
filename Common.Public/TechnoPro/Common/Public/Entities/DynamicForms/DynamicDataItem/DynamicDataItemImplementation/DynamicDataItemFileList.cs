using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x02000394 RID: 916
	public class DynamicDataItemFileList : DynamicDataItemBase<IList<DynamicDataItemFileListRow>>
	{
		// Token: 0x06001C25 RID: 7205 RVA: 0x0001B2CC File Offset: 0x000194CC
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00020290 File Offset: 0x0001E490
		public override DynamicDataStorageItem WriteToStorage()
		{
			return new DynamicDataStorageItem(base.Field);
		}
	}
}

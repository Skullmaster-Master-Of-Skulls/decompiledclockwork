using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200038D RID: 909
	public class DynamicDataItemBool
	{
		// Token: 0x06001C05 RID: 7173 RVA: 0x0000D55A File Offset: 0x0000B75A
		public DynamicDataItemBool()
		{
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0001FCBA File Offset: 0x0001DEBA
		public DynamicDataItemBool(bool isChecked)
		{
			this.IsChecked = isChecked;
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0001FCCC File Offset: 0x0001DECC
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0001FCD4 File Offset: 0x0001DED4
		public bool IsChecked { get; set; }
	}
}

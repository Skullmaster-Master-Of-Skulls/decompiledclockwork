using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200038F RID: 911
	public class DynamicDataItemDateValue
	{
		// Token: 0x06001C0C RID: 7180 RVA: 0x0000D55A File Offset: 0x0000B75A
		public DynamicDataItemDateValue()
		{
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0001FD94 File Offset: 0x0001DF94
		public DynamicDataItemDateValue(DateTime? val)
		{
			this.Value = val;
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0001FDA6 File Offset: 0x0001DFA6
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x0001FDAE File Offset: 0x0001DFAE
		public DateTime? Value { get; set; }
	}
}

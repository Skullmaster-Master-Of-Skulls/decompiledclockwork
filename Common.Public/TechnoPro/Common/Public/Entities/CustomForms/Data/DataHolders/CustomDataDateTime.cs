using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x02000429 RID: 1065
	public class CustomDataDateTime : CustomDataHolder
	{
		// Token: 0x0600204F RID: 8271 RVA: 0x0002489A File Offset: 0x00022A9A
		public CustomDataDateTime()
		{
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000248A4 File Offset: 0x00022AA4
		public CustomDataDateTime(CustomDataHolder dataObj) : base(dataObj)
		{
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000248AF File Offset: 0x00022AAF
		public CustomDataDateTime(Guid dataInstanceId, eCustomDataPrimitiveType dataType) : base(dataInstanceId, dataType)
		{
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x000248CC File Offset: 0x00022ACC
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x000248D4 File Offset: 0x00022AD4
		public DateTime Value { get; set; }
	}
}

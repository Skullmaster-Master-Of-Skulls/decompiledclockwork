using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x02000427 RID: 1063
	public class CustomDataBoolean : CustomDataHolder
	{
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00024889 File Offset: 0x00022A89
		// (set) Token: 0x06002046 RID: 8262 RVA: 0x00024891 File Offset: 0x00022A91
		public bool Value { get; set; }

		// Token: 0x06002047 RID: 8263 RVA: 0x0002489A File Offset: 0x00022A9A
		public CustomDataBoolean()
		{
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000248A4 File Offset: 0x00022AA4
		public CustomDataBoolean(CustomDataHolder dataObj) : base(dataObj)
		{
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000248AF File Offset: 0x00022AAF
		public CustomDataBoolean(Guid dataInstanceId, eCustomDataPrimitiveType dataType) : base(dataInstanceId, dataType)
		{
		}
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x02000428 RID: 1064
	public class CustomDataBooleanNullable : CustomDataHolder
	{
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x000248BB File Offset: 0x00022ABB
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x000248C3 File Offset: 0x00022AC3
		public bool? Value { get; set; }

		// Token: 0x0600204C RID: 8268 RVA: 0x0002489A File Offset: 0x00022A9A
		public CustomDataBooleanNullable()
		{
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000248A4 File Offset: 0x00022AA4
		public CustomDataBooleanNullable(CustomDataHolder dataObj) : base(dataObj)
		{
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000248AF File Offset: 0x00022AAF
		public CustomDataBooleanNullable(Guid dataInstanceId, eCustomDataPrimitiveType dataType) : base(dataInstanceId, dataType)
		{
		}
	}
}

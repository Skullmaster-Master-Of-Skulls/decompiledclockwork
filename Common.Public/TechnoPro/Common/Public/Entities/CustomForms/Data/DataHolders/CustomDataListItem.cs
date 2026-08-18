using System;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x0200042E RID: 1070
	public class CustomDataListItem : CustomDataHolder
	{
		// Token: 0x0600206F RID: 8303 RVA: 0x0002489A File Offset: 0x00022A9A
		public CustomDataListItem()
		{
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000248A4 File Offset: 0x00022AA4
		public CustomDataListItem(CustomDataHolder dataObj) : base(dataObj)
		{
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000248AF File Offset: 0x00022AAF
		public CustomDataListItem(Guid dataInstanceId, eCustomDataPrimitiveType dataType) : base(dataInstanceId, dataType)
		{
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x00024B3D File Offset: 0x00022D3D
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x00024B45 File Offset: 0x00022D45
		public CustomListItem ListItem { get; set; }
	}
}

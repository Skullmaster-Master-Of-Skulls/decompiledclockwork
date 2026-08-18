using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x0200042F RID: 1071
	public class CustomDataString : CustomDataHolder
	{
		// Token: 0x06002074 RID: 8308 RVA: 0x0002489A File Offset: 0x00022A9A
		public CustomDataString()
		{
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000248A4 File Offset: 0x00022AA4
		public CustomDataString(CustomDataHolder dataObj) : base(dataObj)
		{
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000248AF File Offset: 0x00022AAF
		public CustomDataString(Guid dataInstanceId, eCustomDataPrimitiveType dataType) : base(dataInstanceId, dataType)
		{
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x00024B4E File Offset: 0x00022D4E
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x00024B56 File Offset: 0x00022D56
		public string Value { get; set; }

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x00024B5F File Offset: 0x00022D5F
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x00024B67 File Offset: 0x00022D67
		public eCustomDataStringTextType TextType { get; set; }
	}
}

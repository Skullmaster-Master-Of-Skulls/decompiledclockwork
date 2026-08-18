using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x0200042D RID: 1069
	public class CustomDataInt : CustomDataHolder
	{
		// Token: 0x06002069 RID: 8297 RVA: 0x00024AF6 File Offset: 0x00022CF6
		public CustomDataInt()
		{
			this.SetDefaults();
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00024B07 File Offset: 0x00022D07
		public CustomDataInt(CustomDataHolder dataObj) : base(dataObj)
		{
			this.SetDefaults();
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00024B19 File Offset: 0x00022D19
		public CustomDataInt(Guid dataInstanceId, eCustomDataPrimitiveType dataType) : base(dataInstanceId, dataType)
		{
			this.SetDefaults();
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x00024B2C File Offset: 0x00022D2C
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x00024B34 File Offset: 0x00022D34
		public int Value { get; set; }

		// Token: 0x0600206E RID: 8302 RVA: 0x0001B2CC File Offset: 0x000194CC
		private void SetDefaults()
		{
		}
	}
}

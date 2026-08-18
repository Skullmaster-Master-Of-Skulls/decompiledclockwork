using System;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.Common.Public.Entities.CustomForms.Field
{
	// Token: 0x0200041C RID: 1052
	public class CustomDataInstance : BusinessBase<Guid>
	{
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x00024610 File Offset: 0x00022810
		// (set) Token: 0x06002007 RID: 8199 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid DataInstanceId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00024628 File Offset: 0x00022828
		// (set) Token: 0x06002009 RID: 8201 RVA: 0x00024630 File Offset: 0x00022830
		public eCustomDataPrimitiveType DataType { get; set; }

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00024639 File Offset: 0x00022839
		// (set) Token: 0x0600200B RID: 8203 RVA: 0x00024641 File Offset: 0x00022841
		public string Title { get; set; }

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x0002464A File Offset: 0x0002284A
		// (set) Token: 0x0600200D RID: 8205 RVA: 0x00024652 File Offset: 0x00022852
		public eCustomDataPurposeCode Purpose { get; set; }

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x0002465B File Offset: 0x0002285B
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x00024663 File Offset: 0x00022863
		public bool IsHidden { get; set; }
	}
}

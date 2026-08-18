using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Legacy
{
	// Token: 0x02000375 RID: 885
	public class LegacyDynamicDataRowData
	{
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0001F4D9 File Offset: 0x0001D6D9
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x0001F4E1 File Offset: 0x0001D6E1
		public eLegacyDynamicDataRowState RowState { get; set; }

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0001F4EA File Offset: 0x0001D6EA
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0001F4F2 File Offset: 0x0001D6F2
		public int ControlId { get; set; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0001F4FB File Offset: 0x0001D6FB
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x0001F503 File Offset: 0x0001D703
		public int? ControlValueInt { get; set; }

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0001F50C File Offset: 0x0001D70C
		// (set) Token: 0x06001B6B RID: 7019 RVA: 0x0001F514 File Offset: 0x0001D714
		public byte[] ControlValueBytes { get; set; }

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0001F51D File Offset: 0x0001D71D
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0001F525 File Offset: 0x0001D725
		public DateTime? ControlValueDateTime { get; set; }
	}
}

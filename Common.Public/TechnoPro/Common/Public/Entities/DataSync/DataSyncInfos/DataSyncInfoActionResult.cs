using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos
{
	// Token: 0x020003DE RID: 990
	public class DataSyncInfoActionResult
	{
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0002206A File Offset: 0x0002026A
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x00022072 File Offset: 0x00020272
		public DataSyncInfoAction Action { get; set; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0002207B File Offset: 0x0002027B
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x00022083 File Offset: 0x00020283
		public object PreviousValue { get; set; }

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0002208C File Offset: 0x0002028C
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x00022094 File Offset: 0x00020294
		public eDataSyncActionResultType ResultType { get; set; }

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0002209D File Offset: 0x0002029D
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x000220A5 File Offset: 0x000202A5
		public bool WasSuccessful { get; set; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x000220AE File Offset: 0x000202AE
		// (set) Token: 0x06001E98 RID: 7832 RVA: 0x000220B6 File Offset: 0x000202B6
		public string ErrorMessage { get; set; }
	}
}

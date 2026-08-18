using System;

namespace TechnoPro.Common.Public.Entities.Azure.Storage
{
	// Token: 0x02000474 RID: 1140
	public class AzureStorageConnectionInfo
	{
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00026644 File Offset: 0x00024844
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x0002664C File Offset: 0x0002484C
		public UsernameClientCredentials ClientCredentials { get; set; }

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00026655 File Offset: 0x00024855
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x0002665D File Offset: 0x0002485D
		public string BlobsServiceEndpoint { get; set; }
	}
}

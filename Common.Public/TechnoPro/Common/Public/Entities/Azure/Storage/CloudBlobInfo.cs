using System;

namespace TechnoPro.Common.Public.Entities.Azure.Storage
{
	// Token: 0x02000475 RID: 1141
	public class CloudBlobInfo
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x00026666 File Offset: 0x00024866
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x0002666E File Offset: 0x0002486E
		public string BlobName { get; set; }

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x00026677 File Offset: 0x00024877
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x0002667F File Offset: 0x0002487F
		public Uri BlobUri { get; set; }

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x00026688 File Offset: 0x00024888
		// (set) Token: 0x06002287 RID: 8839 RVA: 0x00026690 File Offset: 0x00024890
		public long SizeinBytes { get; set; }

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x00026699 File Offset: 0x00024899
		// (set) Token: 0x06002289 RID: 8841 RVA: 0x000266A1 File Offset: 0x000248A1
		public DateTimeOffset? LastModifiedTime { get; set; }

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000266AA File Offset: 0x000248AA
		// (set) Token: 0x0600228B RID: 8843 RVA: 0x000266B2 File Offset: 0x000248B2
		public string ContainerName { get; set; }

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x000266BB File Offset: 0x000248BB
		// (set) Token: 0x0600228D RID: 8845 RVA: 0x000266C3 File Offset: 0x000248C3
		public Uri ContainerUri { get; set; }
	}
}

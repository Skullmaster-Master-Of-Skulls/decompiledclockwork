using System;

namespace TechnoPro.Common.Public.Entities.Azure.Storage
{
	// Token: 0x02000477 RID: 1143
	public class InMemoryCloudBlob : CloudBlobInfo
	{
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x000266E6 File Offset: 0x000248E6
		// (set) Token: 0x06002293 RID: 8851 RVA: 0x000266EE File Offset: 0x000248EE
		public byte[] FileBytes { get; set; }
	}
}

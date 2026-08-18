using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001AE RID: 430
	internal class CloudUploadConfiguration : ICloudUploadConfiguration
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00039EE9 File Offset: 0x000380E9
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x00039EF1 File Offset: 0x000380F1
		public long MaxFileSize { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00039EFA File Offset: 0x000380FA
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00039F02 File Offset: 0x00038102
		public string[] AllowedFileExtensions { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00039F0B File Offset: 0x0003810B
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00039F13 File Offset: 0x00038113
		public ProviderType? ProviderType { get; set; }
	}
}

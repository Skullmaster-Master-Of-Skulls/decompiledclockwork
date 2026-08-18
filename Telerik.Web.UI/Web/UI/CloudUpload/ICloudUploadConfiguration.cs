using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001A6 RID: 422
	internal interface ICloudUploadConfiguration
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000F65 RID: 3941
		// (set) Token: 0x06000F66 RID: 3942
		long MaxFileSize { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000F67 RID: 3943
		// (set) Token: 0x06000F68 RID: 3944
		string[] AllowedFileExtensions { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000F69 RID: 3945
		// (set) Token: 0x06000F6A RID: 3946
		ProviderType? ProviderType { get; set; }
	}
}

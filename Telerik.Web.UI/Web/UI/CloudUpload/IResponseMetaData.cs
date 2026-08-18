using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001B1 RID: 433
	internal interface IResponseMetaData
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000FB3 RID: 4019
		// (set) Token: 0x06000FB4 RID: 4020
		string KeyName { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000FB5 RID: 4021
		// (set) Token: 0x06000FB6 RID: 4022
		string UploadId { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000FB7 RID: 4023
		// (set) Token: 0x06000FB8 RID: 4024
		string PartETag { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000FB9 RID: 4025
		// (set) Token: 0x06000FBA RID: 4026
		string ContentType { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000FBB RID: 4027
		// (set) Token: 0x06000FBC RID: 4028
		long ContentLength { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000FBD RID: 4029
		// (set) Token: 0x06000FBE RID: 4030
		ResponseStatus Status { get; set; }
	}
}

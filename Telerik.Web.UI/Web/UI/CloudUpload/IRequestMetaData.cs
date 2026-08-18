using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001B0 RID: 432
	internal interface IRequestMetaData
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000FA5 RID: 4005
		// (set) Token: 0x06000FA6 RID: 4006
		string KeyName { get; set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000FA7 RID: 4007
		// (set) Token: 0x06000FA8 RID: 4008
		string OriginalName { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000FA9 RID: 4009
		// (set) Token: 0x06000FAA RID: 4010
		string UploadId { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000FAB RID: 4011
		// (set) Token: 0x06000FAC RID: 4012
		bool IsSingleUpload { get; set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000FAD RID: 4013
		// (set) Token: 0x06000FAE RID: 4014
		bool IsLastChunk { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000FAF RID: 4015
		// (set) Token: 0x06000FB0 RID: 4016
		int ChunkNumber { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000FB1 RID: 4017
		// (set) Token: 0x06000FB2 RID: 4018
		string[] PartEtags { get; set; }
	}
}

using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001B2 RID: 434
	internal class RequestMetaData : IRequestMetaData
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003A388 File Offset: 0x00038588
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0003A390 File Offset: 0x00038590
		public string KeyName { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0003A399 File Offset: 0x00038599
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x0003A3A1 File Offset: 0x000385A1
		public string OriginalName { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0003A3AA File Offset: 0x000385AA
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0003A3B2 File Offset: 0x000385B2
		public string UploadId { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0003A3BB File Offset: 0x000385BB
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x0003A3C3 File Offset: 0x000385C3
		public bool IsSingleUpload { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003A3CC File Offset: 0x000385CC
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0003A3D4 File Offset: 0x000385D4
		public bool IsLastChunk { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0003A3DD File Offset: 0x000385DD
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0003A3E5 File Offset: 0x000385E5
		public int ChunkNumber { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0003A3EE File Offset: 0x000385EE
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0003A3F6 File Offset: 0x000385F6
		public string[] PartEtags { get; set; }
	}
}

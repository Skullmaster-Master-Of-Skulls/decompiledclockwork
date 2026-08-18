using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001B3 RID: 435
	internal class ResponseMetaData : IResponseMetaData
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0003A407 File Offset: 0x00038607
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x0003A40F File Offset: 0x0003860F
		public string KeyName { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0003A418 File Offset: 0x00038618
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x0003A420 File Offset: 0x00038620
		public string UploadId { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0003A429 File Offset: 0x00038629
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x0003A431 File Offset: 0x00038631
		public string PartETag { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0003A43A File Offset: 0x0003863A
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0003A442 File Offset: 0x00038642
		public string ContentType { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003A44B File Offset: 0x0003864B
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0003A453 File Offset: 0x00038653
		public long ContentLength { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0003A45C File Offset: 0x0003865C
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0003A464 File Offset: 0x00038664
		public ResponseStatus Status { get; set; }
	}
}

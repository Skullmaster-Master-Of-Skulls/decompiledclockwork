using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000028 RID: 40
	public class FileHeaderInfo
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000060FC File Offset: 0x000042FC
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00006104 File Offset: 0x00004304
		public string FileName { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000610D File Offset: 0x0000430D
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00006115 File Offset: 0x00004315
		public string ContentType { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000611E File Offset: 0x0000431E
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00006126 File Offset: 0x00004326
		public int StartIndex { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000612F File Offset: 0x0000432F
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00006137 File Offset: 0x00004337
		public int BoundaryDelimiterLength { get; set; }
	}
}

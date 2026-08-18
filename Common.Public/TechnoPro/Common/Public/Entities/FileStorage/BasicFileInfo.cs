using System;

namespace TechnoPro.Common.Public.Entities.FileStorage
{
	// Token: 0x02000341 RID: 833
	[Serializable]
	public class BasicFileInfo
	{
		// Token: 0x060019DC RID: 6620 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		public BasicFileInfo()
		{
			this.FileIdentifier = new FileIdentifier();
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0001E2EA File Offset: 0x0001C4EA
		public BasicFileInfo(BasicFileInfo fileInfo)
		{
			this.FileIdentifier = fileInfo.FileIdentifier;
			this.FileName = fileInfo.FileName;
			this.FileUri = fileInfo.FileUri;
			this.Length = fileInfo.Length;
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x0001E328 File Offset: 0x0001C528
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x0001E330 File Offset: 0x0001C530
		public FileIdentifier FileIdentifier { get; set; }

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0001E339 File Offset: 0x0001C539
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x0001E341 File Offset: 0x0001C541
		public string FileName { get; set; }

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x0001E34A File Offset: 0x0001C54A
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x0001E352 File Offset: 0x0001C552
		public long Length { get; set; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0001E35B File Offset: 0x0001C55B
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x0001E363 File Offset: 0x0001C563
		public Uri FileUri { get; set; }
	}
}

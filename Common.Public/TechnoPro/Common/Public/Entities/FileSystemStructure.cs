using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public class FileSystemStructure : FileStructure
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000E9C1 File Offset: 0x0000CBC1
		public virtual string Filename { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000E9CA File Offset: 0x0000CBCA
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000E9D2 File Offset: 0x0000CBD2
		public virtual string Extension { get; set; }
	}
}

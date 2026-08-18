using System;

namespace TechnoPro.Common.Public.Entities.FileStorage
{
	// Token: 0x02000342 RID: 834
	[Serializable]
	public class InMemoryFile : BasicFileInfo
	{
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x0001E36C File Offset: 0x0001C56C
		// (set) Token: 0x060019E7 RID: 6631 RVA: 0x0001E374 File Offset: 0x0001C574
		public byte[] FileData { get; set; }

		// Token: 0x060019E8 RID: 6632 RVA: 0x0001E37D File Offset: 0x0001C57D
		public InMemoryFile()
		{
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0001E387 File Offset: 0x0001C587
		public InMemoryFile(BasicFileInfo fileInfo) : base(fileInfo)
		{
		}
	}
}

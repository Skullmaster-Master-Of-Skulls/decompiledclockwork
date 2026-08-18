using System;
using System.IO;

namespace TechnoPro.Common.Public.Entities.FileStorage
{
	// Token: 0x02000343 RID: 835
	[Serializable]
	public class StreamingFile : BasicFileInfo
	{
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x0001E392 File Offset: 0x0001C592
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x0001E39A File Offset: 0x0001C59A
		public Stream FileByteStream { get; set; }

		// Token: 0x060019EC RID: 6636 RVA: 0x0001E37D File Offset: 0x0001C57D
		public StreamingFile()
		{
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0001E387 File Offset: 0x0001C587
		public StreamingFile(BasicFileInfo fileInfo) : base(fileInfo)
		{
		}
	}
}

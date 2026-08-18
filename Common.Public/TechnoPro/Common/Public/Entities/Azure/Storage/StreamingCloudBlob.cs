using System;
using System.IO;

namespace TechnoPro.Common.Public.Entities.Azure.Storage
{
	// Token: 0x02000476 RID: 1142
	public class StreamingCloudBlob : CloudBlobInfo
	{
		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000266CC File Offset: 0x000248CC
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x000266D4 File Offset: 0x000248D4
		public Stream FileByteStream { get; set; }
	}
}

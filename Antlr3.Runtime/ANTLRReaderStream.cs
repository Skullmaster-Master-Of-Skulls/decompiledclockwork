using System;
using System.IO;

namespace Antlr.Runtime
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class ANTLRReaderStream : ANTLRStringStream
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002473 File Offset: 0x00000673
		public ANTLRReaderStream(TextReader r) : this(r, 1024, 1024)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002486 File Offset: 0x00000686
		public ANTLRReaderStream(TextReader r, int size) : this(r, size, 1024)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002495 File Offset: 0x00000695
		public ANTLRReaderStream(TextReader r, int size, int readChunkSize)
		{
			this.Load(r, size, readChunkSize);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024A8 File Offset: 0x000006A8
		public virtual void Load(TextReader r, int size, int readChunkSize)
		{
			if (r == null)
			{
				return;
			}
			if (size <= 0)
			{
				size = 1024;
			}
			if (readChunkSize <= 0)
			{
				readChunkSize = 1024;
			}
			try
			{
				this.data = r.ReadToEnd().ToCharArray();
				this.n = this.data.Length;
			}
			finally
			{
				r.Close();
			}
		}

		// Token: 0x0400000B RID: 11
		public const int ReadBufferSize = 1024;

		// Token: 0x0400000C RID: 12
		public const int InitialBufferSize = 1024;
	}
}

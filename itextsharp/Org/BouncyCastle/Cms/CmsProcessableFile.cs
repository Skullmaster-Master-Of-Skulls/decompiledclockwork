using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020004CC RID: 1228
	public class CmsProcessableFile : CmsProcessable
	{
		// Token: 0x060029E7 RID: 10727 RVA: 0x000FFA64 File Offset: 0x000FEA64
		public CmsProcessableFile(FileInfo file) : this(file, 32768)
		{
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000FFA72 File Offset: 0x000FEA72
		public CmsProcessableFile(FileInfo file, int bufSize)
		{
			this._file = file;
			this._bufSize = bufSize;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000FFA88 File Offset: 0x000FEA88
		public virtual Stream Read()
		{
			return new FileStream(this._file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, this._bufSize);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000FFAA4 File Offset: 0x000FEAA4
		public virtual void Write(Stream zOut)
		{
			Stream stream = this.Read();
			Streams.PipeAll(stream, zOut);
			stream.Close();
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000FFAC5 File Offset: 0x000FEAC5
		public virtual object GetContent()
		{
			return this._file;
		}

		// Token: 0x04001D29 RID: 7465
		private const int DefaultBufSize = 32768;

		// Token: 0x04001D2A RID: 7466
		private readonly FileInfo _file;

		// Token: 0x04001D2B RID: 7467
		private readonly int _bufSize;
	}
}

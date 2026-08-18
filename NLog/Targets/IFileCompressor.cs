using System;

namespace NLog.Targets
{
	// Token: 0x0200015E RID: 350
	public interface IFileCompressor
	{
		// Token: 0x06000D27 RID: 3367
		void CompressFile(string fileName, string archiveFileName);
	}
}

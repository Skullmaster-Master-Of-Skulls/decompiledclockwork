using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200002B RID: 43
	public interface IArchiveStorage
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000199 RID: 409
		FileUpdateMode UpdateMode { get; }

		// Token: 0x0600019A RID: 410
		Stream GetTemporaryOutput();

		// Token: 0x0600019B RID: 411
		Stream ConvertTemporaryToFinal();

		// Token: 0x0600019C RID: 412
		Stream MakeTemporaryCopy(Stream stream);

		// Token: 0x0600019D RID: 413
		Stream OpenForDirectUpdate(Stream stream);

		// Token: 0x0600019E RID: 414
		void Dispose();
	}
}

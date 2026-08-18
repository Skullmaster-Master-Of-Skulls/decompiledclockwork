using System;

namespace System.IO.Compression
{
	// Token: 0x0200042B RID: 1067
	internal interface IFileFormatWriter
	{
		// Token: 0x06002815 RID: 10261
		byte[] GetHeader();

		// Token: 0x06002816 RID: 10262
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x06002817 RID: 10263
		byte[] GetFooter();
	}
}

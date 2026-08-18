using System;

namespace System.IO.Compression
{
	// Token: 0x0200042C RID: 1068
	internal interface IFileFormatReader
	{
		// Token: 0x06002818 RID: 10264
		bool ReadHeader(InputBuffer input);

		// Token: 0x06002819 RID: 10265
		bool ReadFooter(InputBuffer input);

		// Token: 0x0600281A RID: 10266
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x0600281B RID: 10267
		void Validate();
	}
}

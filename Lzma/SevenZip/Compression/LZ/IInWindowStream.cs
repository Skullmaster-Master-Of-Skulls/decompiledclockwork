using System;
using System.IO;

namespace SevenZip.Compression.LZ
{
	// Token: 0x0200000F RID: 15
	internal interface IInWindowStream
	{
		// Token: 0x06000054 RID: 84
		void SetStream(Stream inStream);

		// Token: 0x06000055 RID: 85
		void Init();

		// Token: 0x06000056 RID: 86
		void ReleaseStream();

		// Token: 0x06000057 RID: 87
		byte GetIndexByte(int index);

		// Token: 0x06000058 RID: 88
		uint GetMatchLen(int index, uint distance, uint limit);

		// Token: 0x06000059 RID: 89
		uint GetNumAvailableBytes();
	}
}

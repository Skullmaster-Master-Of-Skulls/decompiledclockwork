using System;

namespace System.IO.Compression
{
	// Token: 0x02000420 RID: 1056
	internal interface IInflater : IDisposable
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002782 RID: 10114
		int AvailableOutput { get; }

		// Token: 0x06002783 RID: 10115
		int Inflate(byte[] bytes, int offset, int length);

		// Token: 0x06002784 RID: 10116
		bool Finished();

		// Token: 0x06002785 RID: 10117
		bool NeedsInput();

		// Token: 0x06002786 RID: 10118
		void SetInput(byte[] inputBytes, int offset, int length);
	}
}

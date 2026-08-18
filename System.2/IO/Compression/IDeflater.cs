using System;

namespace System.IO.Compression
{
	// Token: 0x0200041F RID: 1055
	internal interface IDeflater : IDisposable
	{
		// Token: 0x0600277E RID: 10110
		bool NeedsInput();

		// Token: 0x0600277F RID: 10111
		void SetInput(byte[] inputBuffer, int startIndex, int count);

		// Token: 0x06002780 RID: 10112
		int GetDeflateOutput(byte[] outputBuffer);

		// Token: 0x06002781 RID: 10113
		bool Finish(byte[] outputBuffer, out int bytesRead);
	}
}

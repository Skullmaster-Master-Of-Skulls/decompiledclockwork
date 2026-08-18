using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000865 RID: 2149
	[ComVisible(true)]
	public interface ICryptoTransform : IDisposable
	{
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06004E76 RID: 20086
		int InputBlockSize { get; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06004E77 RID: 20087
		int OutputBlockSize { get; }

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06004E78 RID: 20088
		bool CanTransformMultipleBlocks { get; }

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06004E79 RID: 20089
		bool CanReuseTransform { get; }

		// Token: 0x06004E7A RID: 20090
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		// Token: 0x06004E7B RID: 20091
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}

using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020003C7 RID: 967
	public interface ITranslator
	{
		// Token: 0x060021AE RID: 8622
		int GetEncodedBlockSize();

		// Token: 0x060021AF RID: 8623
		int Encode(byte[] input, int inOff, int length, byte[] outBytes, int outOff);

		// Token: 0x060021B0 RID: 8624
		int GetDecodedBlockSize();

		// Token: 0x060021B1 RID: 8625
		int Decode(byte[] input, int inOff, int length, byte[] outBytes, int outOff);
	}
}

using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000100 RID: 256
	public interface IEncoder
	{
		// Token: 0x06000A28 RID: 2600
		int Encode(byte[] data, int off, int length, Stream outStream);

		// Token: 0x06000A29 RID: 2601
		int Decode(byte[] data, int off, int length, Stream outStream);

		// Token: 0x06000A2A RID: 2602
		int DecodeString(string data, Stream outStream);
	}
}

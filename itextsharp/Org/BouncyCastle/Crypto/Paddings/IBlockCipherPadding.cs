using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x02000086 RID: 134
	public interface IBlockCipherPadding
	{
		// Token: 0x0600042A RID: 1066
		void Init(SecureRandom random);

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600042B RID: 1067
		string PaddingName { get; }

		// Token: 0x0600042C RID: 1068
		int AddPadding(byte[] input, int inOff);

		// Token: 0x0600042D RID: 1069
		int PadCount(byte[] input);
	}
}

using System;

namespace EncryptionClassLibrary
{
	// Token: 0x02000013 RID: 19
	public interface IBatchDecryptor : IDisposable
	{
		// Token: 0x060000AC RID: 172
		string Decrypt(byte[] data);
	}
}

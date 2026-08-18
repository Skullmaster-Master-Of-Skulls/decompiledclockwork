using System;

namespace EncryptionClassLibrary
{
	// Token: 0x02000014 RID: 20
	public interface IBatchEncryptor : IDisposable
	{
		// Token: 0x060000AD RID: 173
		byte[] Encrypt(string data);
	}
}

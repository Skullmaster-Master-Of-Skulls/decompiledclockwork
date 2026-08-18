using System;
using System.IO;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000B9 RID: 185
	public interface IRelDecryptor
	{
		// Token: 0x06000443 RID: 1091
		Stream Decrypt(EncryptionMethod encryptionMethod, KeyInfo keyInfo, Stream toDecrypt);
	}
}

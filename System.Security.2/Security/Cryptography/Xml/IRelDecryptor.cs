using System;
using System.IO;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000062 RID: 98
	public interface IRelDecryptor
	{
		// Token: 0x060003A6 RID: 934
		Stream Decrypt(EncryptionMethod encryptionMethod, KeyInfo keyInfo, Stream toDecrypt);
	}
}

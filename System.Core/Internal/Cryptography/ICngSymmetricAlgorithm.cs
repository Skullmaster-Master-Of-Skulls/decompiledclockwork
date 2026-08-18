using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace Internal.Cryptography
{
	// Token: 0x02000007 RID: 7
	internal interface ICngSymmetricAlgorithm
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8
		int BlockSize { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9
		CipherMode Mode { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10
		PaddingMode Padding { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		byte[] IV { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13
		KeySizes[] LegalKeySizes { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000F RID: 15
		byte[] BaseKey { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		int BaseKeySize { get; set; }

		// Token: 0x06000012 RID: 18
		bool IsWeakKey(byte[] key);

		// Token: 0x06000013 RID: 19
		string GetNCryptAlgorithmIdentifier();

		// Token: 0x06000014 RID: 20
		[SecurityCritical]
		SafeBCryptAlgorithmHandle GetEphemeralModeHandle();
	}
}

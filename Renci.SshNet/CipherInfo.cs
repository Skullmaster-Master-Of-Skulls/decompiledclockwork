using System;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet
{
	// Token: 0x02000008 RID: 8
	public class CipherInfo
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000287C File Offset: 0x00000A7C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002884 File Offset: 0x00000A84
		public int KeySize { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000288D File Offset: 0x00000A8D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002895 File Offset: 0x00000A95
		public Func<byte[], byte[], Cipher> Cipher { get; private set; }

		// Token: 0x06000047 RID: 71 RVA: 0x000028A0 File Offset: 0x00000AA0
		public CipherInfo(int keySize, Func<byte[], byte[], Cipher> cipher)
		{
			CipherInfo <>4__this = this;
			this.KeySize = keySize;
			this.Cipher = ((byte[] key, byte[] iv) => cipher(key.Take(<>4__this.KeySize / 8), iv));
		}
	}
}

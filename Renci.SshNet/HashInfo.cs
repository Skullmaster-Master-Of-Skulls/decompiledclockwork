using System;
using System.Security.Cryptography;

namespace Renci.SshNet
{
	// Token: 0x0200000D RID: 13
	public class HashInfo
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003D25 File Offset: 0x00001F25
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003D2D File Offset: 0x00001F2D
		public int KeySize { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003D36 File Offset: 0x00001F36
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003D3E File Offset: 0x00001F3E
		public Func<byte[], HashAlgorithm> HashAlgorithm { get; private set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00003D48 File Offset: 0x00001F48
		public HashInfo(int keySize, Func<byte[], HashAlgorithm> hash)
		{
			HashInfo <>4__this = this;
			this.KeySize = keySize;
			this.HashAlgorithm = ((byte[] key) => hash(key.Take(<>4__this.KeySize / 8)));
		}
	}
}

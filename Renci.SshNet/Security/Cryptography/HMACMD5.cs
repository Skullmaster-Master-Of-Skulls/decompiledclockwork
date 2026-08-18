using System;
using System.Security.Cryptography;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000076 RID: 118
	public class HMACMD5 : HMACMD5
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00014F0C File Offset: 0x0001310C
		public HMACMD5(byte[] key) : base(key)
		{
			this._hashSize = base.HashSize;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00014F21 File Offset: 0x00013121
		public HMACMD5(byte[] key, int hashSize) : base(key)
		{
			this._hashSize = hashSize;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00014F31 File Offset: 0x00013131
		public override int HashSize
		{
			get
			{
				return this._hashSize;
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00014F39 File Offset: 0x00013139
		protected override byte[] HashFinal()
		{
			return base.HashFinal().Take(this.HashSize / 8);
		}

		// Token: 0x0400025D RID: 605
		private readonly int _hashSize;
	}
}

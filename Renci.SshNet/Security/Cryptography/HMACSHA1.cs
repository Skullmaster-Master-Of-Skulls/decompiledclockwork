using System;
using System.Security.Cryptography;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000077 RID: 119
	public class HMACSHA1 : HMACSHA1
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x00014F4E File Offset: 0x0001314E
		public HMACSHA1(byte[] key) : base(key)
		{
			this._hashSize = base.HashSize;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00014F63 File Offset: 0x00013163
		public HMACSHA1(byte[] key, int hashSize) : base(key)
		{
			this._hashSize = hashSize;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00014F73 File Offset: 0x00013173
		public override int HashSize
		{
			get
			{
				return this._hashSize;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00014F39 File Offset: 0x00013139
		protected override byte[] HashFinal()
		{
			return base.HashFinal().Take(this.HashSize / 8);
		}

		// Token: 0x0400025E RID: 606
		private readonly int _hashSize;
	}
}

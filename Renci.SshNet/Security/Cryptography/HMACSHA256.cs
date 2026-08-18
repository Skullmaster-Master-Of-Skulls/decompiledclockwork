using System;
using System.Security.Cryptography;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000078 RID: 120
	public class HMACSHA256 : HMACSHA256
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x00014F7B File Offset: 0x0001317B
		public HMACSHA256(byte[] key) : base(key)
		{
			this._hashSize = base.HashSize;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00014F90 File Offset: 0x00013190
		public HMACSHA256(byte[] key, int hashSize) : base(key)
		{
			this._hashSize = hashSize;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00014FA0 File Offset: 0x000131A0
		public override int HashSize
		{
			get
			{
				return this._hashSize;
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00014F39 File Offset: 0x00013139
		protected override byte[] HashFinal()
		{
			return base.HashFinal().Take(this.HashSize / 8);
		}

		// Token: 0x0400025F RID: 607
		private readonly int _hashSize;
	}
}

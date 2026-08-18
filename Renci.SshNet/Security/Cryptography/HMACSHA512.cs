using System;
using System.Security.Cryptography;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x0200007A RID: 122
	public class HMACSHA512 : HMACSHA512
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00014FD5 File Offset: 0x000131D5
		public HMACSHA512(byte[] key) : base(key)
		{
			this._hashSize = base.HashSize;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00014FEA File Offset: 0x000131EA
		public HMACSHA512(byte[] key, int hashSize) : base(key)
		{
			this._hashSize = hashSize;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00014FFA File Offset: 0x000131FA
		public override int HashSize
		{
			get
			{
				return this._hashSize;
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00014F39 File Offset: 0x00013139
		protected override byte[] HashFinal()
		{
			return base.HashFinal().Take(this.HashSize / 8);
		}

		// Token: 0x04000261 RID: 609
		private readonly int _hashSize;
	}
}

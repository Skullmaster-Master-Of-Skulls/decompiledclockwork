using System;
using System.Security.Cryptography;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000079 RID: 121
	public class HMACSHA384 : HMACSHA384
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x00014FA8 File Offset: 0x000131A8
		public HMACSHA384(byte[] key) : base(key)
		{
			this._hashSize = base.HashSize;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00014FBD File Offset: 0x000131BD
		public HMACSHA384(byte[] key, int hashSize) : base(key)
		{
			this._hashSize = hashSize;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00014FCD File Offset: 0x000131CD
		public override int HashSize
		{
			get
			{
				return this._hashSize;
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00014F39 File Offset: 0x00013139
		protected override byte[] HashFinal()
		{
			return base.HashFinal().Take(this.HashSize / 8);
		}

		// Token: 0x04000260 RID: 608
		private readonly int _hashSize;
	}
}

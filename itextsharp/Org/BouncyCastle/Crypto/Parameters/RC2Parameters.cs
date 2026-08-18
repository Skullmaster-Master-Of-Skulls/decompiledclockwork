using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020001EE RID: 494
	public class RC2Parameters : KeyParameter
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x0006E8C6 File Offset: 0x0006D8C6
		public RC2Parameters(byte[] key) : this(key, (key.Length > 128) ? 1024 : (key.Length * 8))
		{
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0006E8E5 File Offset: 0x0006D8E5
		public RC2Parameters(byte[] key, int keyOff, int keyLen) : this(key, keyOff, keyLen, (keyLen > 128) ? 1024 : (keyLen * 8))
		{
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0006E902 File Offset: 0x0006D902
		public RC2Parameters(byte[] key, int bits) : base(key)
		{
			this.bits = bits;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0006E912 File Offset: 0x0006D912
		public RC2Parameters(byte[] key, int keyOff, int keyLen, int bits) : base(key, keyOff, keyLen)
		{
			this.bits = bits;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0006E925 File Offset: 0x0006D925
		public int EffectiveKeyBits
		{
			get
			{
				return this.bits;
			}
		}

		// Token: 0x04000D7E RID: 3454
		private readonly int bits;
	}
}

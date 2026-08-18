using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020001ED RID: 493
	public class KeyParameter : ICipherParameters
	{
		// Token: 0x06001340 RID: 4928 RVA: 0x0006E823 File Offset: 0x0006D823
		public KeyParameter(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = (byte[])key.Clone();
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0006E84C File Offset: 0x0006D84C
		public KeyParameter(byte[] key, int keyOff, int keyLen)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (keyOff < 0 || keyOff > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyOff");
			}
			if (keyLen < 0 || keyOff + keyLen > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyLen");
			}
			this.key = new byte[keyLen];
			Array.Copy(key, keyOff, this.key, 0, keyLen);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0006E8B4 File Offset: 0x0006D8B4
		public byte[] GetKey()
		{
			return (byte[])this.key.Clone();
		}

		// Token: 0x04000D7D RID: 3453
		private readonly byte[] key;
	}
}

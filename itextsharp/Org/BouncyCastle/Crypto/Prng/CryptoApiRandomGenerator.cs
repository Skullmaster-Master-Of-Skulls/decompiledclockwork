using System;
using System.Security.Cryptography;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200023C RID: 572
	public class CryptoApiRandomGenerator : IRandomGenerator
	{
		// Token: 0x06001638 RID: 5688 RVA: 0x00081FD1 File Offset: 0x00080FD1
		public CryptoApiRandomGenerator()
		{
			this.rndProv = new RNGCryptoServiceProvider();
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00081FE4 File Offset: 0x00080FE4
		public virtual void AddSeedMaterial(byte[] seed)
		{
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00081FE6 File Offset: 0x00080FE6
		public virtual void AddSeedMaterial(long seed)
		{
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00081FE8 File Offset: 0x00080FE8
		public virtual void NextBytes(byte[] bytes)
		{
			this.rndProv.GetBytes(bytes);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00081FF8 File Offset: 0x00080FF8
		public virtual void NextBytes(byte[] bytes, int start, int len)
		{
			if (start < 0)
			{
				throw new ArgumentException("Start offset cannot be negative", "start");
			}
			if (bytes.Length < start + len)
			{
				throw new ArgumentException("Byte array too small for requested offset and length");
			}
			if (bytes.Length == len && start == 0)
			{
				this.NextBytes(bytes);
				return;
			}
			byte[] array = new byte[len];
			this.rndProv.GetBytes(array);
			Array.Copy(array, 0, bytes, start, len);
		}

		// Token: 0x04000F46 RID: 3910
		private readonly RNGCryptoServiceProvider rndProv;
	}
}

using System;

namespace System.Web.Security.Cryptography
{
	// Token: 0x0200060A RID: 1546
	internal sealed class CryptographicKey
	{
		// Token: 0x06004DB5 RID: 19893 RVA: 0x0010DD04 File Offset: 0x0010BF04
		public CryptographicKey(byte[] keyMaterial)
		{
			this._keyMaterial = keyMaterial;
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x0010DD13 File Offset: 0x0010BF13
		public int KeyLength
		{
			get
			{
				return checked(this._keyMaterial.Length * 8);
			}
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x0010DD20 File Offset: 0x0010BF20
		public CryptographicKey ExtractBits(int offset, int count)
		{
			int srcOffset = offset / 8;
			int num = count / 8;
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._keyMaterial, srcOffset, array, 0, num);
			return new CryptographicKey(array);
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x0010DD51 File Offset: 0x0010BF51
		public byte[] GetKeyMaterial()
		{
			return this._keyMaterial;
		}

		// Token: 0x0400296F RID: 10607
		private readonly byte[] _keyMaterial;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000883 RID: 2179
	[ComVisible(true)]
	public abstract class KeyedHashAlgorithm : HashAlgorithm
	{
		// Token: 0x06004F75 RID: 20341 RVA: 0x0011477A File Offset: 0x0011377A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
				}
				this.KeyValue = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x001147A9 File Offset: 0x001137A9
		// (set) Token: 0x06004F77 RID: 20343 RVA: 0x001147BB File Offset: 0x001137BB
		public virtual byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.State != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
				}
				this.KeyValue = (byte[])value.Clone();
			}
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x001147E6 File Offset: 0x001137E6
		public new static KeyedHashAlgorithm Create()
		{
			return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x001147F2 File Offset: 0x001137F2
		public new static KeyedHashAlgorithm Create(string algName)
		{
			return (KeyedHashAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x040028FF RID: 10495
		protected byte[] KeyValue;
	}
}

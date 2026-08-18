using System;

namespace System.Security.Cryptography
{
	// Token: 0x020000DE RID: 222
	public sealed class AesManaged : Aes
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x000166E0 File Offset: 0x000148E0
		public AesManaged()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && LocalAppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(SR.GetString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this.m_impl = new AesCng();
			this.m_impl.BlockSize = this.BlockSize;
			this.m_impl.KeySize = this.KeySize;
			this.m_impl.FeedbackSize = 128;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001674E File Offset: 0x0001494E
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001675B File Offset: 0x0001495B
		public override int FeedbackSize
		{
			get
			{
				return this.m_impl.FeedbackSize;
			}
			set
			{
				this.m_impl.FeedbackSize = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00016769 File Offset: 0x00014969
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00016776 File Offset: 0x00014976
		public override byte[] IV
		{
			get
			{
				return this.m_impl.IV;
			}
			set
			{
				this.m_impl.IV = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00016784 File Offset: 0x00014984
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00016791 File Offset: 0x00014991
		public override byte[] Key
		{
			get
			{
				return this.m_impl.Key;
			}
			set
			{
				this.m_impl.Key = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001679F File Offset: 0x0001499F
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x000167AC File Offset: 0x000149AC
		public override int KeySize
		{
			get
			{
				return this.m_impl.KeySize;
			}
			set
			{
				this.m_impl.KeySize = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x000167BA File Offset: 0x000149BA
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x000167C7 File Offset: 0x000149C7
		public override CipherMode Mode
		{
			get
			{
				return this.m_impl.Mode;
			}
			set
			{
				if (value == CipherMode.CFB || value == CipherMode.OFB)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidCipherMode"));
				}
				this.m_impl.Mode = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x000167ED File Offset: 0x000149ED
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x000167FA File Offset: 0x000149FA
		public override PaddingMode Padding
		{
			get
			{
				return this.m_impl.Padding;
			}
			set
			{
				this.m_impl.Padding = value;
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00016808 File Offset: 0x00014A08
		public override ICryptoTransform CreateDecryptor()
		{
			return this.m_impl.CreateDecryptor();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00016818 File Offset: 0x00014A18
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidKeySize"), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidIVSize"), "iv");
			}
			return this.m_impl.CreateDecryptor(key, iv);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00016887 File Offset: 0x00014A87
		public override ICryptoTransform CreateEncryptor()
		{
			return this.m_impl.CreateEncryptor();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00016894 File Offset: 0x00014A94
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidKeySize"), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidIVSize"), "iv");
			}
			return this.m_impl.CreateEncryptor(key, iv);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00016904 File Offset: 0x00014B04
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((IDisposable)this.m_impl).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001693C File Offset: 0x00014B3C
		public override void GenerateIV()
		{
			this.m_impl.GenerateIV();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00016949 File Offset: 0x00014B49
		public override void GenerateKey()
		{
			this.m_impl.GenerateKey();
		}

		// Token: 0x040005D7 RID: 1495
		private SymmetricAlgorithm m_impl;
	}
}

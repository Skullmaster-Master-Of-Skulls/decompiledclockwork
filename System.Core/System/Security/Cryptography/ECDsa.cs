using System;
using System.IO;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000FE RID: 254
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ECDsa : AsymmetricAlgorithm
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001C230 File Offset: 0x0001A430
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001C233 File Offset: 0x0001A433
		public override string SignatureAlgorithm
		{
			get
			{
				return "ECDsa";
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001C23A File Offset: 0x0001A43A
		public new static ECDsa Create()
		{
			return ECDsa.Create(typeof(ECDsaCng).FullName);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001C250 File Offset: 0x0001A450
		public new static ECDsa Create(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			return CryptoConfig.CreateFromName(algorithm) as ECDsa;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001C26C File Offset: 0x0001A46C
		public static ECDsa Create(ECCurve curve)
		{
			ECDsa ecdsa = ECDsa.Create();
			if (ecdsa != null)
			{
				try
				{
					ecdsa.GenerateKey(curve);
				}
				catch
				{
					ecdsa.Dispose();
					throw;
				}
			}
			return ecdsa;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		public static ECDsa Create(ECParameters parameters)
		{
			ECDsa ecdsa = ECDsa.Create();
			if (ecdsa != null)
			{
				try
				{
					ecdsa.ImportParameters(parameters);
				}
				catch
				{
					ecdsa.Dispose();
					throw;
				}
			}
			return ecdsa;
		}

		// Token: 0x06000851 RID: 2129
		public abstract byte[] SignHash(byte[] hash);

		// Token: 0x06000852 RID: 2130
		public abstract bool VerifyHash(byte[] hash, byte[] signature);

		// Token: 0x06000853 RID: 2131 RVA: 0x0001C2E4 File Offset: 0x0001A4E4
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw ECDsa.DerivedClassMustOverride();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001C2EB File Offset: 0x0001A4EB
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw ECDsa.DerivedClassMustOverride();
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001C2F2 File Offset: 0x0001A4F2
		public virtual byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001C310 File Offset: 0x0001A510
		public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] hash = this.HashData(data, offset, count, hashAlgorithm);
			return this.SignHash(hash);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001C380 File Offset: 0x0001A580
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] hash = this.HashData(data, hashAlgorithm);
			return this.SignHash(hash);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001C3BF File Offset: 0x0001A5BF
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] hash = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifyHash(hash, signature);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001C45C File Offset: 0x0001A65C
		public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] hash = this.HashData(data, hashAlgorithm);
			return this.VerifyHash(hash, signature);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001C4AA File Offset: 0x0001A6AA
		public virtual ECParameters ExportParameters(bool includePrivateParameters)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001C4BB File Offset: 0x0001A6BB
		public virtual ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001C4CC File Offset: 0x0001A6CC
		public virtual void ImportParameters(ECParameters parameters)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001C4DD File Offset: 0x0001A6DD
		public virtual void GenerateKey(ECCurve curve)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001C4EE File Offset: 0x0001A6EE
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001C4FF File Offset: 0x0001A6FF
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
		}
	}
}

using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000F9 RID: 249
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ECDiffieHellman : AsymmetricAlgorithm
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001B4F0 File Offset: 0x000196F0
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "ECDiffieHellman";
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0001B4F7 File Offset: 0x000196F7
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001B4FA File Offset: 0x000196FA
		public new static ECDiffieHellman Create()
		{
			return ECDiffieHellman.Create(typeof(ECDiffieHellmanCng).FullName);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001B510 File Offset: 0x00019710
		public new static ECDiffieHellman Create(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			return CryptoConfig.CreateFromName(algorithm) as ECDiffieHellman;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001B52C File Offset: 0x0001972C
		public static ECDiffieHellman Create(ECCurve curve)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.GenerateKey(curve);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001B568 File Offset: 0x00019768
		public static ECDiffieHellman Create(ECParameters parameters)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.ImportParameters(parameters);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000805 RID: 2053
		public abstract ECDiffieHellmanPublicKey PublicKey { get; }

		// Token: 0x06000806 RID: 2054 RVA: 0x0001B5A4 File Offset: 0x000197A4
		public virtual byte[] DeriveKeyMaterial(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001B5AB File Offset: 0x000197AB
		public byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm)
		{
			return this.DeriveKeyFromHash(otherPartyPublicKey, hashAlgorithm, null, null);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001B5B7 File Offset: 0x000197B7
		public virtual byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001B5BE File Offset: 0x000197BE
		public byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey)
		{
			return this.DeriveKeyFromHmac(otherPartyPublicKey, hashAlgorithm, hmacKey, null, null);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001B5CB File Offset: 0x000197CB
		public virtual byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001B5D2 File Offset: 0x000197D2
		public virtual byte[] DeriveKeyTls(ECDiffieHellmanPublicKey otherPartyPublicKey, byte[] prfLabel, byte[] prfSeed)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001B5D9 File Offset: 0x000197D9
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001B5EA File Offset: 0x000197EA
		public virtual ECParameters ExportParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001B5F1 File Offset: 0x000197F1
		public virtual ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001B5F8 File Offset: 0x000197F8
		public virtual void ImportParameters(ECParameters parameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001B5FF File Offset: 0x000197FF
		public virtual void GenerateKey(ECCurve curve)
		{
			throw new NotSupportedException(SR.GetString("NotSupported_SubclassOverride"));
		}
	}
}

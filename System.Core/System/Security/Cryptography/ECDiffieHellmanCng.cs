using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000FC RID: 252
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ECDiffieHellmanCng : ECDiffieHellman
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x0001B6A3 File Offset: 0x000198A3
		public ECDiffieHellmanCng() : this(521)
		{
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001B6B0 File Offset: 0x000198B0
		public ECDiffieHellmanCng(int keySize)
		{
			this.m_hashAlgorithm = CngAlgorithm.Sha256;
			base..ctor();
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			this.LegalKeySizesValue = ECDiffieHellmanCng.s_legalKeySizes;
			this.KeySize = keySize;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001B6EC File Offset: 0x000198EC
		public ECDiffieHellmanCng(ECCurve curve)
		{
			this.m_hashAlgorithm = CngAlgorithm.Sha256;
			base..ctor();
			this.GenerateKey(curve);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001B708 File Offset: 0x00019908
		[SecuritySafeCritical]
		public ECDiffieHellmanCng(CngKey key)
		{
			this.m_hashAlgorithm = CngAlgorithm.Sha256;
			base..ctor();
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.AlgorithmGroup != CngAlgorithmGroup.ECDiffieHellman)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDHRequiresECDHKey"), "key");
			}
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			this.LegalKeySizesValue = ECDiffieHellmanCng.s_legalKeySizes;
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			using (SafeNCryptKeyHandle handle = key.Handle)
			{
				this.Key = CngKey.Open(handle, key.IsEphemeral ? CngKeyHandleOpenOptions.EphemeralKey : CngKeyHandleOpenOptions.None);
			}
			CodeAccessPermission.RevertAssert();
			this.KeySizeValue = this.m_key.KeySize;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001B7DC File Offset: 0x000199DC
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public CngAlgorithm HashAlgorithm
		{
			get
			{
				return this.m_hashAlgorithm;
			}
			set
			{
				if (this.m_hashAlgorithm == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_hashAlgorithm = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001B806 File Offset: 0x00019A06
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0001B80E File Offset: 0x00019A0E
		public byte[] HmacKey
		{
			get
			{
				return this.m_hmacKey;
			}
			set
			{
				this.m_hmacKey = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001B817 File Offset: 0x00019A17
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001B81F File Offset: 0x00019A1F
		public ECDiffieHellmanKeyDerivationFunction KeyDerivationFunction
		{
			get
			{
				return this.m_kdf;
			}
			set
			{
				if (value < ECDiffieHellmanKeyDerivationFunction.Hash || value > ECDiffieHellmanKeyDerivationFunction.Tls)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_kdf = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001B83B File Offset: 0x00019A3B
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001B843 File Offset: 0x00019A43
		public byte[] Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001B84C File Offset: 0x00019A4C
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0001B854 File Offset: 0x00019A54
		public byte[] SecretAppend
		{
			get
			{
				return this.m_secretAppend;
			}
			set
			{
				this.m_secretAppend = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001B85D File Offset: 0x00019A5D
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001B865 File Offset: 0x00019A65
		public byte[] SecretPrepend
		{
			get
			{
				return this.m_secretPrepend;
			}
			set
			{
				this.m_secretPrepend = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001B86E File Offset: 0x00019A6E
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001B876 File Offset: 0x00019A76
		public byte[] Seed
		{
			get
			{
				return this.m_seed;
			}
			set
			{
				this.m_seed = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001B880 File Offset: 0x00019A80
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0001B924 File Offset: 0x00019B24
		public CngKey Key
		{
			get
			{
				if (this.m_key != null && this.m_key.KeySize != this.KeySize)
				{
					this.m_key.Dispose();
					this.m_key = null;
				}
				if (this.m_key == null)
				{
					CngAlgorithm algorithm = null;
					int keySize = this.KeySize;
					if (keySize != 256)
					{
						if (keySize != 384)
						{
							if (keySize == 521)
							{
								algorithm = CngAlgorithm.ECDiffieHellmanP521;
							}
						}
						else
						{
							algorithm = CngAlgorithm.ECDiffieHellmanP384;
						}
					}
					else
					{
						algorithm = CngAlgorithm.ECDiffieHellmanP256;
					}
					CngKeyCreationParameters creationParameters = new CngKeyCreationParameters
					{
						ExportPolicy = new CngExportPolicies?(CngExportPolicies.AllowPlaintextExport)
					};
					this.m_key = CngKey.Create(algorithm, null, creationParameters);
				}
				return this.m_key;
			}
			private set
			{
				if (value.AlgorithmGroup != CngAlgorithmGroup.ECDiffieHellman)
				{
					throw new ArgumentException(SR.GetString("Cryptography_ArgECDHRequiresECDHKey"));
				}
				if (this.m_key != null)
				{
					this.m_key.Dispose();
				}
				this.m_key = value;
				this.KeySizeValue = this.m_key.KeySize;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001B97E File Offset: 0x00019B7E
		public override ECDiffieHellmanPublicKey PublicKey
		{
			get
			{
				return ECDiffieHellmanCngPublicKey.FromKey(this.Key);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001B98B File Offset: 0x00019B8B
		public bool UseSecretAgreementAsHmacKey
		{
			get
			{
				return this.HmacKey == null;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001B998 File Offset: 0x00019B98
		public override byte[] DeriveKeyMaterial(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentNullException("otherPartyPublicKey");
			}
			ECDiffieHellmanCngPublicKey ecdiffieHellmanCngPublicKey = otherPartyPublicKey as ECDiffieHellmanCngPublicKey;
			if (ecdiffieHellmanCngPublicKey == null)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgExpectedECDiffieHellmanCngPublicKey"));
			}
			byte[] result;
			using (CngKey cngKey = ecdiffieHellmanCngPublicKey.Import())
			{
				result = this.DeriveKeyMaterial(cngKey);
			}
			return result;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001B9FC File Offset: 0x00019BFC
		[SecuritySafeCritical]
		public byte[] DeriveKeyMaterial(CngKey otherPartyPublicKey)
		{
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentNullException("otherPartyPublicKey");
			}
			if (otherPartyPublicKey.AlgorithmGroup != CngAlgorithmGroup.ECDiffieHellman)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDHRequiresECDHKey"), "otherPartyPublicKey");
			}
			if (otherPartyPublicKey.KeySize != this.KeySize)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDHKeySizeMismatch"), "otherPartyPublicKey");
			}
			NCryptNative.SecretAgreementFlags flags = this.UseSecretAgreementAsHmacKey ? NCryptNative.SecretAgreementFlags.UseSecretAsHmacKey : NCryptNative.SecretAgreementFlags.None;
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			byte[] result;
			using (SafeNCryptKeyHandle handle = this.Key.Handle)
			{
				using (SafeNCryptKeyHandle handle2 = otherPartyPublicKey.Handle)
				{
					CodeAccessPermission.RevertAssert();
					using (SafeNCryptSecretHandle safeNCryptSecretHandle = NCryptNative.DeriveSecretAgreement(handle, handle2))
					{
						if (this.KeyDerivationFunction == ECDiffieHellmanKeyDerivationFunction.Hash)
						{
							byte[] secretAppend = (this.SecretAppend == null) ? null : (this.SecretAppend.Clone() as byte[]);
							byte[] secretPrepend = (this.SecretPrepend == null) ? null : (this.SecretPrepend.Clone() as byte[]);
							result = NCryptNative.DeriveKeyMaterialHash(safeNCryptSecretHandle, this.HashAlgorithm.Algorithm, secretPrepend, secretAppend, flags);
						}
						else if (this.KeyDerivationFunction == ECDiffieHellmanKeyDerivationFunction.Hmac)
						{
							byte[] hmacKey = (this.HmacKey == null) ? null : (this.HmacKey.Clone() as byte[]);
							byte[] secretAppend2 = (this.SecretAppend == null) ? null : (this.SecretAppend.Clone() as byte[]);
							byte[] secretPrepend2 = (this.SecretPrepend == null) ? null : (this.SecretPrepend.Clone() as byte[]);
							result = NCryptNative.DeriveKeyMaterialHmac(safeNCryptSecretHandle, this.HashAlgorithm.Algorithm, hmacKey, secretPrepend2, secretAppend2, flags);
						}
						else
						{
							byte[] array = (this.Label == null) ? null : (this.Label.Clone() as byte[]);
							byte[] array2 = (this.Seed == null) ? null : (this.Seed.Clone() as byte[]);
							if (array == null || array2 == null)
							{
								throw new InvalidOperationException(SR.GetString("Cryptography_TlsRequiresLabelAndSeed"));
							}
							result = NCryptNative.DeriveKeyMaterialTls(safeNCryptSecretHandle, array, array2, flags);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001BC44 File Offset: 0x00019E44
		[SecuritySafeCritical]
		public override byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] secretPrepend, byte[] secretAppend)
		{
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentNullException("otherPartyPublicKey");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			byte[] result;
			using (SafeNCryptSecretHandle safeNCryptSecretHandle = this.DeriveSecretAgreementHandle(otherPartyPublicKey))
			{
				result = NCryptNative.DeriveKeyMaterialHash(safeNCryptSecretHandle, hashAlgorithm.Name, secretPrepend, secretAppend, NCryptNative.SecretAgreementFlags.None);
			}
			return result;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001BCBC File Offset: 0x00019EBC
		[SecuritySafeCritical]
		public override byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey, byte[] secretPrepend, byte[] secretAppend)
		{
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentNullException("otherPartyPublicKey");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			byte[] result;
			using (SafeNCryptSecretHandle safeNCryptSecretHandle = this.DeriveSecretAgreementHandle(otherPartyPublicKey))
			{
				NCryptNative.SecretAgreementFlags flags = (hmacKey == null) ? NCryptNative.SecretAgreementFlags.UseSecretAsHmacKey : NCryptNative.SecretAgreementFlags.None;
				result = NCryptNative.DeriveKeyMaterialHmac(safeNCryptSecretHandle, hashAlgorithm.Name, hmacKey, secretPrepend, secretAppend, flags);
			}
			return result;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001BD3C File Offset: 0x00019F3C
		[SecuritySafeCritical]
		public override byte[] DeriveKeyTls(ECDiffieHellmanPublicKey otherPartyPublicKey, byte[] prfLabel, byte[] prfSeed)
		{
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentNullException("otherPartyPublicKey");
			}
			if (prfLabel == null)
			{
				throw new ArgumentNullException("prfLabel");
			}
			if (prfSeed == null)
			{
				throw new ArgumentNullException("prfSeed");
			}
			byte[] result;
			using (SafeNCryptSecretHandle safeNCryptSecretHandle = this.DeriveSecretAgreementHandle(otherPartyPublicKey))
			{
				result = NCryptNative.DeriveKeyMaterialTls(safeNCryptSecretHandle, prfLabel, prfSeed, NCryptNative.SecretAgreementFlags.None);
			}
			return result;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public SafeNCryptSecretHandle DeriveSecretAgreementHandle(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentNullException("otherPartyPublicKey");
			}
			ECDiffieHellmanCngPublicKey ecdiffieHellmanCngPublicKey = otherPartyPublicKey as ECDiffieHellmanCngPublicKey;
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgExpectedECDiffieHellmanCngPublicKey"));
			}
			SafeNCryptSecretHandle result;
			using (CngKey cngKey = ecdiffieHellmanCngPublicKey.Import())
			{
				result = this.DeriveSecretAgreementHandle(cngKey);
			}
			return result;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001BE08 File Offset: 0x0001A008
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public SafeNCryptSecretHandle DeriveSecretAgreementHandle(CngKey otherPartyPublicKey)
		{
			if (otherPartyPublicKey == null)
			{
				throw new ArgumentNullException("otherPartyPublicKey");
			}
			if (otherPartyPublicKey.AlgorithmGroup != CngAlgorithmGroup.ECDiffieHellman)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDHRequiresECDHKey"), "otherPartyPublicKey");
			}
			if (otherPartyPublicKey.KeySize != this.KeySize)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDHKeySizeMismatch"), "otherPartyPublicKey");
			}
			SafeNCryptSecretHandle result;
			using (SafeNCryptKeyHandle handle = this.Key.Handle)
			{
				using (SafeNCryptKeyHandle handle2 = otherPartyPublicKey.Handle)
				{
					result = NCryptNative.DeriveSecretAgreement(handle, handle2);
				}
			}
			return result;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_key != null)
				{
					this.m_key.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001BEFC File Offset: 0x0001A0FC
		public override void GenerateKey(ECCurve curve)
		{
			curve.Validate();
			if (this.m_key != null)
			{
				this.m_key.Dispose();
				this.m_key = null;
			}
			CngKey cngKey = CngKey.Create(curve, (string name) => CngKey.EcdhCurveNameToAlgorithm(name));
			this.m_key = cngKey;
			this.KeySizeValue = cngKey.KeySize;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001BF63 File Offset: 0x0001A163
		public override void FromXmlString(string xmlString)
		{
			throw new NotImplementedException(SR.GetString("Cryptography_ECXmlSerializationFormatRequired"));
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001BF74 File Offset: 0x0001A174
		public void FromXmlString(string xml, ECKeyXmlFormat format)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (format != ECKeyXmlFormat.Rfc4050)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			bool flag;
			ECParameters parameters = Rfc4050KeyFormatter.FromXml(xml, out flag);
			if (!flag)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgECDHRequiresECDHKey"), "xml");
			}
			this.ImportParameters(parameters);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001BFC5 File Offset: 0x0001A1C5
		public override string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException(SR.GetString("Cryptography_ECXmlSerializationFormatRequired"));
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
		public string ToXmlString(ECKeyXmlFormat format)
		{
			if (format != ECKeyXmlFormat.Rfc4050)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			ECParameters parameters = this.ExportParameters(false);
			return Rfc4050KeyFormatter.ToXml(parameters, true);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001C002 File Offset: 0x0001A202
		public override void ImportParameters(ECParameters parameters)
		{
			this.Key = ECCng.ImportEcdhParameters(ref parameters);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001C011 File Offset: 0x0001A211
		public override ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			return ECCng.ExportExplicitParameters(this.Key, includePrivateParameters);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001C01F File Offset: 0x0001A21F
		public override ECParameters ExportParameters(bool includePrivateParameters)
		{
			return ECCng.ExportParameters(this.Key, includePrivateParameters);
		}

		// Token: 0x0400066A RID: 1642
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(256, 384, 128),
			new KeySizes(521, 521, 0)
		};

		// Token: 0x0400066B RID: 1643
		private CngAlgorithm m_hashAlgorithm;

		// Token: 0x0400066C RID: 1644
		private byte[] m_hmacKey;

		// Token: 0x0400066D RID: 1645
		private CngKey m_key;

		// Token: 0x0400066E RID: 1646
		private ECDiffieHellmanKeyDerivationFunction m_kdf;

		// Token: 0x0400066F RID: 1647
		private byte[] m_label;

		// Token: 0x04000670 RID: 1648
		private byte[] m_secretAppend;

		// Token: 0x04000671 RID: 1649
		private byte[] m_secretPrepend;

		// Token: 0x04000672 RID: 1650
		private byte[] m_seed;
	}
}

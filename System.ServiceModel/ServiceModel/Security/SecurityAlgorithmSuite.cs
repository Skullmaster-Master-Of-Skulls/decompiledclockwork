using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Tokens;
using System.ServiceModel.Configuration;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002DD RID: 733
	[TypeConverter(typeof(SecurityAlgorithmSuiteConverter))]
	public abstract class SecurityAlgorithmSuite
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0005B06A File Offset: 0x0005926A
		internal static SecurityAlgorithmSuite KerberosDefault
		{
			get
			{
				return SecurityAlgorithmSuite.Basic128;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0005B071 File Offset: 0x00059271
		public static SecurityAlgorithmSuite Default
		{
			get
			{
				return SecurityAlgorithmSuite.Basic256;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0005B078 File Offset: 0x00059278
		public static SecurityAlgorithmSuite Basic256
		{
			get
			{
				if (SecurityAlgorithmSuite.basic256 == null)
				{
					SecurityAlgorithmSuite.basic256 = new Basic256SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic256;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0005B090 File Offset: 0x00059290
		public static SecurityAlgorithmSuite Basic192
		{
			get
			{
				if (SecurityAlgorithmSuite.basic192 == null)
				{
					SecurityAlgorithmSuite.basic192 = new Basic192SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic192;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0005B0A8 File Offset: 0x000592A8
		public static SecurityAlgorithmSuite Basic128
		{
			get
			{
				if (SecurityAlgorithmSuite.basic128 == null)
				{
					SecurityAlgorithmSuite.basic128 = new Basic128SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic128;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0005B0C0 File Offset: 0x000592C0
		public static SecurityAlgorithmSuite TripleDes
		{
			get
			{
				if (SecurityAlgorithmSuite.tripleDes == null)
				{
					SecurityAlgorithmSuite.tripleDes = new TripleDesSecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.tripleDes;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0005B0D8 File Offset: 0x000592D8
		public static SecurityAlgorithmSuite Basic256Rsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.basic256Rsa15 == null)
				{
					SecurityAlgorithmSuite.basic256Rsa15 = new Basic256Rsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic256Rsa15;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0005B0F0 File Offset: 0x000592F0
		public static SecurityAlgorithmSuite Basic192Rsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.basic192Rsa15 == null)
				{
					SecurityAlgorithmSuite.basic192Rsa15 = new Basic192Rsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic192Rsa15;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0005B108 File Offset: 0x00059308
		public static SecurityAlgorithmSuite Basic128Rsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.basic128Rsa15 == null)
				{
					SecurityAlgorithmSuite.basic128Rsa15 = new Basic128Rsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic128Rsa15;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0005B120 File Offset: 0x00059320
		public static SecurityAlgorithmSuite TripleDesRsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.tripleDesRsa15 == null)
				{
					SecurityAlgorithmSuite.tripleDesRsa15 = new TripleDesRsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.tripleDesRsa15;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0005B138 File Offset: 0x00059338
		public static SecurityAlgorithmSuite Basic256Sha256
		{
			get
			{
				if (SecurityAlgorithmSuite.basic256Sha256 == null)
				{
					SecurityAlgorithmSuite.basic256Sha256 = new Basic256Sha256SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic256Sha256;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0005B150 File Offset: 0x00059350
		public static SecurityAlgorithmSuite Basic192Sha256
		{
			get
			{
				if (SecurityAlgorithmSuite.basic192Sha256 == null)
				{
					SecurityAlgorithmSuite.basic192Sha256 = new Basic192Sha256SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic192Sha256;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0005B168 File Offset: 0x00059368
		public static SecurityAlgorithmSuite Basic128Sha256
		{
			get
			{
				if (SecurityAlgorithmSuite.basic128Sha256 == null)
				{
					SecurityAlgorithmSuite.basic128Sha256 = new Basic128Sha256SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic128Sha256;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0005B180 File Offset: 0x00059380
		public static SecurityAlgorithmSuite TripleDesSha256
		{
			get
			{
				if (SecurityAlgorithmSuite.tripleDesSha256 == null)
				{
					SecurityAlgorithmSuite.tripleDesSha256 = new TripleDesSha256SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.tripleDesSha256;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x0005B198 File Offset: 0x00059398
		public static SecurityAlgorithmSuite Basic256Sha256Rsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.basic256Sha256Rsa15 == null)
				{
					SecurityAlgorithmSuite.basic256Sha256Rsa15 = new Basic256Sha256Rsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic256Sha256Rsa15;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0005B1B0 File Offset: 0x000593B0
		public static SecurityAlgorithmSuite Basic192Sha256Rsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.basic192Sha256Rsa15 == null)
				{
					SecurityAlgorithmSuite.basic192Sha256Rsa15 = new Basic192Sha256Rsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic192Sha256Rsa15;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0005B1C8 File Offset: 0x000593C8
		public static SecurityAlgorithmSuite Basic128Sha256Rsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.basic128Sha256Rsa15 == null)
				{
					SecurityAlgorithmSuite.basic128Sha256Rsa15 = new Basic128Sha256Rsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.basic128Sha256Rsa15;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0005B1E0 File Offset: 0x000593E0
		public static SecurityAlgorithmSuite TripleDesSha256Rsa15
		{
			get
			{
				if (SecurityAlgorithmSuite.tripleDesSha256Rsa15 == null)
				{
					SecurityAlgorithmSuite.tripleDesSha256Rsa15 = new TripleDesSha256Rsa15SecurityAlgorithmSuite();
				}
				return SecurityAlgorithmSuite.tripleDesSha256Rsa15;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060017FE RID: 6142
		public abstract string DefaultCanonicalizationAlgorithm { get; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060017FF RID: 6143
		public abstract string DefaultDigestAlgorithm { get; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001800 RID: 6144
		public abstract string DefaultEncryptionAlgorithm { get; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001801 RID: 6145
		public abstract int DefaultEncryptionKeyDerivationLength { get; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001802 RID: 6146
		public abstract string DefaultSymmetricKeyWrapAlgorithm { get; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001803 RID: 6147
		public abstract string DefaultAsymmetricKeyWrapAlgorithm { get; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001804 RID: 6148
		public abstract string DefaultSymmetricSignatureAlgorithm { get; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001805 RID: 6149
		public abstract string DefaultAsymmetricSignatureAlgorithm { get; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001806 RID: 6150
		public abstract int DefaultSignatureKeyDerivationLength { get; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001807 RID: 6151
		public abstract int DefaultSymmetricKeyLength { get; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0005B1F8 File Offset: 0x000593F8
		internal virtual XmlDictionaryString DefaultCanonicalizationAlgorithmDictionaryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0005B1FB File Offset: 0x000593FB
		internal virtual XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x0005B1FE File Offset: 0x000593FE
		internal virtual XmlDictionaryString DefaultEncryptionAlgorithmDictionaryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0005B201 File Offset: 0x00059401
		internal virtual XmlDictionaryString DefaultSymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x0005B204 File Offset: 0x00059404
		internal virtual XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0005B207 File Offset: 0x00059407
		internal virtual XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0005B20A File Offset: 0x0005940A
		internal virtual XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0005B215 File Offset: 0x00059415
		public virtual bool IsCanonicalizationAlgorithmSupported(string algorithm)
		{
			return algorithm == this.DefaultCanonicalizationAlgorithm;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0005B223 File Offset: 0x00059423
		public virtual bool IsDigestAlgorithmSupported(string algorithm)
		{
			return algorithm == this.DefaultDigestAlgorithm;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0005B231 File Offset: 0x00059431
		public virtual bool IsEncryptionAlgorithmSupported(string algorithm)
		{
			return algorithm == this.DefaultEncryptionAlgorithm;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0005B23F File Offset: 0x0005943F
		public virtual bool IsEncryptionKeyDerivationAlgorithmSupported(string algorithm)
		{
			return algorithm == "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1" || algorithm == "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0005B25B File Offset: 0x0005945B
		public virtual bool IsSymmetricKeyWrapAlgorithmSupported(string algorithm)
		{
			return algorithm == this.DefaultSymmetricKeyWrapAlgorithm;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0005B269 File Offset: 0x00059469
		public virtual bool IsAsymmetricKeyWrapAlgorithmSupported(string algorithm)
		{
			return algorithm == this.DefaultAsymmetricKeyWrapAlgorithm;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0005B277 File Offset: 0x00059477
		public virtual bool IsSymmetricSignatureAlgorithmSupported(string algorithm)
		{
			return algorithm == this.DefaultSymmetricSignatureAlgorithm;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0005B285 File Offset: 0x00059485
		public virtual bool IsAsymmetricSignatureAlgorithmSupported(string algorithm)
		{
			return algorithm == this.DefaultAsymmetricSignatureAlgorithm;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0005B293 File Offset: 0x00059493
		public virtual bool IsSignatureKeyDerivationAlgorithmSupported(string algorithm)
		{
			return algorithm == "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1" || algorithm == "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";
		}

		// Token: 0x06001819 RID: 6169
		public abstract bool IsSymmetricKeyLengthSupported(int length);

		// Token: 0x0600181A RID: 6170
		public abstract bool IsAsymmetricKeyLengthSupported(int length);

		// Token: 0x0600181B RID: 6171 RVA: 0x0005B2B0 File Offset: 0x000594B0
		internal static bool IsRsaSHA256(SecurityAlgorithmSuite suite)
		{
			return suite != null && (suite == SecurityAlgorithmSuite.Basic128Sha256 || suite == SecurityAlgorithmSuite.Basic128Sha256Rsa15 || suite == SecurityAlgorithmSuite.Basic192Sha256 || suite == SecurityAlgorithmSuite.Basic192Sha256Rsa15 || suite == SecurityAlgorithmSuite.Basic256Sha256 || suite == SecurityAlgorithmSuite.Basic256Sha256Rsa15 || suite == SecurityAlgorithmSuite.TripleDesSha256 || suite == SecurityAlgorithmSuite.TripleDesSha256Rsa15);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0005B304 File Offset: 0x00059504
		internal string GetEncryptionKeyDerivationAlgorithm(SecurityToken token, SecureConversationVersion version)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(version);
			if (SecurityUtils.IsSupportedAlgorithm(keyDerivationAlgorithm, token))
			{
				return keyDerivationAlgorithm;
			}
			return null;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0005B338 File Offset: 0x00059538
		internal int GetEncryptionKeyDerivationLength(SecurityToken token, SecureConversationVersion version)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(version);
			if (!SecurityUtils.IsSupportedAlgorithm(keyDerivationAlgorithm, token))
			{
				return 0;
			}
			if (this.DefaultEncryptionKeyDerivationLength % 8 != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("Psha1KeyLengthInvalid", new object[]
				{
					this.DefaultEncryptionKeyDerivationLength
				})));
			}
			return this.DefaultEncryptionKeyDerivationLength / 8;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0005B3AC File Offset: 0x000595AC
		internal void GetKeyWrapAlgorithm(SecurityToken token, out string keyWrapAlgorithm, out XmlDictionaryString keyWrapAlgorithmDictionaryString)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (SecurityUtils.IsSupportedAlgorithm(this.DefaultSymmetricKeyWrapAlgorithm, token))
			{
				keyWrapAlgorithm = this.DefaultSymmetricKeyWrapAlgorithm;
				keyWrapAlgorithmDictionaryString = this.DefaultSymmetricKeyWrapAlgorithmDictionaryString;
				return;
			}
			keyWrapAlgorithm = this.DefaultAsymmetricKeyWrapAlgorithm;
			keyWrapAlgorithmDictionaryString = this.DefaultAsymmetricKeyWrapAlgorithmDictionaryString;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0005B3FC File Offset: 0x000595FC
		internal void GetSignatureAlgorithmAndKey(SecurityToken token, out string signatureAlgorithm, out SecurityKey key, out XmlDictionaryString signatureAlgorithmDictionaryString)
		{
			ReadOnlyCollection<SecurityKey> securityKeys = token.SecurityKeys;
			if (securityKeys == null || securityKeys.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SigningTokenHasNoKeys", new object[]
				{
					token
				})));
			}
			for (int i = 0; i < securityKeys.Count; i++)
			{
				if (securityKeys[i].IsSupportedAlgorithm(this.DefaultSymmetricSignatureAlgorithm))
				{
					signatureAlgorithm = this.DefaultSymmetricSignatureAlgorithm;
					signatureAlgorithmDictionaryString = this.DefaultSymmetricSignatureAlgorithmDictionaryString;
					key = securityKeys[i];
					return;
				}
				if (securityKeys[i].IsSupportedAlgorithm(this.DefaultAsymmetricSignatureAlgorithm))
				{
					signatureAlgorithm = this.DefaultAsymmetricSignatureAlgorithm;
					signatureAlgorithmDictionaryString = this.DefaultAsymmetricSignatureAlgorithmDictionaryString;
					key = securityKeys[i];
					return;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SigningTokenHasNoKeysSupportingTheAlgorithmSuite", new object[]
			{
				token,
				this
			})));
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0005B4D8 File Offset: 0x000596D8
		internal string GetSignatureKeyDerivationAlgorithm(SecurityToken token, SecureConversationVersion version)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(version);
			if (SecurityUtils.IsSupportedAlgorithm(keyDerivationAlgorithm, token))
			{
				return keyDerivationAlgorithm;
			}
			return null;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0005B50C File Offset: 0x0005970C
		internal int GetSignatureKeyDerivationLength(SecurityToken token, SecureConversationVersion version)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(version);
			if (!SecurityUtils.IsSupportedAlgorithm(keyDerivationAlgorithm, token))
			{
				return 0;
			}
			if (this.DefaultSignatureKeyDerivationLength % 8 != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("Psha1KeyLengthInvalid", new object[]
				{
					this.DefaultSignatureKeyDerivationLength
				})));
			}
			return this.DefaultSignatureKeyDerivationLength / 8;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0005B57E File Offset: 0x0005977E
		internal void EnsureAcceptableSymmetricSignatureAlgorithm(string algorithm)
		{
			if (!this.IsSymmetricSignatureAlgorithmSupported(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
				{
					algorithm,
					"SymmetricSignature",
					this
				})));
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0005B5BC File Offset: 0x000597BC
		internal void EnsureAcceptableSignatureKeySize(SecurityKey securityKey, SecurityToken token)
		{
			AsymmetricSecurityKey asymmetricSecurityKey = securityKey as AsymmetricSecurityKey;
			if (asymmetricSecurityKey != null)
			{
				if (!this.IsAsymmetricKeyLengthSupported(asymmetricSecurityKey.KeySize))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenDoesNotMeetKeySizeRequirements", new object[]
					{
						this,
						token,
						asymmetricSecurityKey.KeySize
					})));
				}
			}
			else
			{
				SymmetricSecurityKey symmetricSecurityKey = securityKey as SymmetricSecurityKey;
				if (symmetricSecurityKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnknownICryptoType", new object[]
					{
						symmetricSecurityKey
					})));
				}
				this.EnsureAcceptableSignatureSymmetricKeySize(symmetricSecurityKey, token);
			}
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0005B650 File Offset: 0x00059850
		internal void EnsureAcceptableSignatureSymmetricKeySize(SymmetricSecurityKey securityKey, SecurityToken token)
		{
			DerivedKeySecurityToken derivedKeySecurityToken = token as DerivedKeySecurityToken;
			int keySize;
			if (derivedKeySecurityToken != null)
			{
				token = derivedKeySecurityToken.TokenToDerive;
				keySize = ((SymmetricSecurityKey)token.SecurityKeys[0]).KeySize;
				if (derivedKeySecurityToken.SecurityKeys[0].KeySize < this.DefaultSignatureKeyDerivationLength)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenDoesNotMeetKeySizeRequirements", new object[]
					{
						this,
						derivedKeySecurityToken,
						derivedKeySecurityToken.SecurityKeys[0].KeySize
					})));
				}
			}
			else
			{
				keySize = securityKey.KeySize;
			}
			if (!this.IsSymmetricKeyLengthSupported(keySize))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenDoesNotMeetKeySizeRequirements", new object[]
				{
					this,
					token,
					keySize
				})));
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0005B724 File Offset: 0x00059924
		internal void EnsureAcceptableDecryptionSymmetricKeySize(SymmetricSecurityKey securityKey, SecurityToken token)
		{
			DerivedKeySecurityToken derivedKeySecurityToken = token as DerivedKeySecurityToken;
			int keySize;
			if (derivedKeySecurityToken != null)
			{
				token = derivedKeySecurityToken.TokenToDerive;
				keySize = ((SymmetricSecurityKey)token.SecurityKeys[0]).KeySize;
				if (derivedKeySecurityToken.SecurityKeys[0].KeySize < this.DefaultEncryptionKeyDerivationLength)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenDoesNotMeetKeySizeRequirements", new object[]
					{
						this,
						derivedKeySecurityToken,
						derivedKeySecurityToken.SecurityKeys[0].KeySize
					})));
				}
			}
			else
			{
				keySize = securityKey.KeySize;
			}
			if (!this.IsSymmetricKeyLengthSupported(keySize))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenDoesNotMeetKeySizeRequirements", new object[]
				{
					this,
					token,
					keySize
				})));
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0005B7F8 File Offset: 0x000599F8
		internal void EnsureAcceptableSignatureAlgorithm(SecurityKey verificationKey, string algorithm)
		{
			InMemorySymmetricSecurityKey inMemorySymmetricSecurityKey = verificationKey as InMemorySymmetricSecurityKey;
			if (inMemorySymmetricSecurityKey != null)
			{
				this.EnsureAcceptableSymmetricSignatureAlgorithm(algorithm);
				return;
			}
			if (!(verificationKey is AsymmetricSecurityKey))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnknownICryptoType", new object[]
				{
					verificationKey
				})));
			}
			this.EnsureAcceptableAsymmetricSignatureAlgorithm(algorithm);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0005B84C File Offset: 0x00059A4C
		internal void EnsureAcceptableAsymmetricSignatureAlgorithm(string algorithm)
		{
			if (!this.IsAsymmetricSignatureAlgorithmSupported(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
				{
					algorithm,
					"AsymmetricSignature",
					this
				})));
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0005B888 File Offset: 0x00059A88
		internal void EnsureAcceptableKeyWrapAlgorithm(string algorithm, bool isAsymmetric)
		{
			if (isAsymmetric)
			{
				if (!this.IsAsymmetricKeyWrapAlgorithmSupported(algorithm))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
					{
						algorithm,
						"AsymmetricKeyWrap",
						this
					})));
				}
			}
			else if (!this.IsSymmetricKeyWrapAlgorithmSupported(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
				{
					algorithm,
					"SymmetricKeyWrap",
					this
				})));
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0005B90A File Offset: 0x00059B0A
		internal void EnsureAcceptableEncryptionAlgorithm(string algorithm)
		{
			if (!this.IsEncryptionAlgorithmSupported(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
				{
					algorithm,
					"Encryption",
					this
				})));
			}
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0005B945 File Offset: 0x00059B45
		internal void EnsureAcceptableSignatureKeyDerivationAlgorithm(string algorithm)
		{
			if (!this.IsSignatureKeyDerivationAlgorithmSupported(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
				{
					algorithm,
					"SignatureKeyDerivation",
					this
				})));
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0005B980 File Offset: 0x00059B80
		internal void EnsureAcceptableEncryptionKeyDerivationAlgorithm(string algorithm)
		{
			if (!this.IsEncryptionKeyDerivationAlgorithmSupported(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
				{
					algorithm,
					"EncryptionKeyDerivation",
					this
				})));
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0005B9BB File Offset: 0x00059BBB
		internal void EnsureAcceptableDigestAlgorithm(string algorithm)
		{
			if (!this.IsDigestAlgorithmSupported(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SuiteDoesNotAcceptAlgorithm", new object[]
				{
					algorithm,
					"Digest",
					this
				})));
			}
		}

		// Token: 0x04001C44 RID: 7236
		private static SecurityAlgorithmSuite basic256;

		// Token: 0x04001C45 RID: 7237
		private static SecurityAlgorithmSuite basic192;

		// Token: 0x04001C46 RID: 7238
		private static SecurityAlgorithmSuite basic128;

		// Token: 0x04001C47 RID: 7239
		private static SecurityAlgorithmSuite tripleDes;

		// Token: 0x04001C48 RID: 7240
		private static SecurityAlgorithmSuite basic256Rsa15;

		// Token: 0x04001C49 RID: 7241
		private static SecurityAlgorithmSuite basic192Rsa15;

		// Token: 0x04001C4A RID: 7242
		private static SecurityAlgorithmSuite basic128Rsa15;

		// Token: 0x04001C4B RID: 7243
		private static SecurityAlgorithmSuite tripleDesRsa15;

		// Token: 0x04001C4C RID: 7244
		private static SecurityAlgorithmSuite basic256Sha256;

		// Token: 0x04001C4D RID: 7245
		private static SecurityAlgorithmSuite basic192Sha256;

		// Token: 0x04001C4E RID: 7246
		private static SecurityAlgorithmSuite basic128Sha256;

		// Token: 0x04001C4F RID: 7247
		private static SecurityAlgorithmSuite tripleDesSha256;

		// Token: 0x04001C50 RID: 7248
		private static SecurityAlgorithmSuite basic256Sha256Rsa15;

		// Token: 0x04001C51 RID: 7249
		private static SecurityAlgorithmSuite basic192Sha256Rsa15;

		// Token: 0x04001C52 RID: 7250
		private static SecurityAlgorithmSuite basic128Sha256Rsa15;

		// Token: 0x04001C53 RID: 7251
		private static SecurityAlgorithmSuite tripleDesSha256Rsa15;
	}
}

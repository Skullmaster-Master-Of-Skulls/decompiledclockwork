using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000383 RID: 899
	internal sealed class DerivedKeySecurityToken : SecurityToken
	{
		// Token: 0x06002134 RID: 8500 RVA: 0x0007B1AE File Offset: 0x000793AE
		public DerivedKeySecurityToken(SecurityToken tokenToDerive, SecurityKeyIdentifierClause tokenToDeriveIdentifier, int length) : this(tokenToDerive, tokenToDeriveIdentifier, length, SecurityUtils.GenerateId())
		{
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0007B1C0 File Offset: 0x000793C0
		internal DerivedKeySecurityToken(SecurityToken tokenToDerive, SecurityKeyIdentifierClause tokenToDeriveIdentifier, int length, string id)
		{
			this.length = -1;
			this.offset = -1;
			this.generation = -1;
			base..ctor();
			if (length != 16 && length != 24 && length != 32)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("Psha1KeyLengthInvalid", new object[]
				{
					length * 8
				})));
			}
			byte[] data = new byte[16];
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetBytes(data);
			this.Initialize(id, -1, 0, length, null, data, tokenToDerive, tokenToDeriveIdentifier, "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1");
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0007B24C File Offset: 0x0007944C
		internal DerivedKeySecurityToken(int generation, int offset, int length, string label, int minNonceLength, SecurityToken tokenToDerive, SecurityKeyIdentifierClause tokenToDeriveIdentifier, string derivationAlgorithm, string id)
		{
			this.length = -1;
			this.offset = -1;
			this.generation = -1;
			base..ctor();
			byte[] data = new byte[minNonceLength];
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetBytes(data);
			this.Initialize(id, generation, offset, length, label, data, tokenToDerive, tokenToDeriveIdentifier, derivationAlgorithm);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0007B2A0 File Offset: 0x000794A0
		internal DerivedKeySecurityToken(int generation, int offset, int length, string label, byte[] nonce, SecurityToken tokenToDerive, SecurityKeyIdentifierClause tokenToDeriveIdentifier, string derivationAlgorithm, string id)
		{
			this.length = -1;
			this.offset = -1;
			this.generation = -1;
			base..ctor();
			this.Initialize(id, generation, offset, length, label, nonce, tokenToDerive, tokenToDeriveIdentifier, derivationAlgorithm, false);
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x0007B2DE File Offset: 0x000794DE
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x0007B2E6 File Offset: 0x000794E6
		public override DateTime ValidFrom
		{
			get
			{
				return this.tokenToDerive.ValidFrom;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x0007B2F3 File Offset: 0x000794F3
		public override DateTime ValidTo
		{
			get
			{
				return this.tokenToDerive.ValidTo;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x0007B300 File Offset: 0x00079500
		public string KeyDerivationAlgorithm
		{
			get
			{
				return this.keyDerivationAlgorithm;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0007B308 File Offset: 0x00079508
		public int Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0007B310 File Offset: 0x00079510
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0007B318 File Offset: 0x00079518
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0007B320 File Offset: 0x00079520
		internal byte[] Nonce
		{
			get
			{
				return this.nonce;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0007B328 File Offset: 0x00079528
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0007B330 File Offset: 0x00079530
		internal SecurityToken TokenToDerive
		{
			get
			{
				return this.tokenToDerive;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0007B338 File Offset: 0x00079538
		internal SecurityKeyIdentifierClause TokenToDeriveIdentifier
		{
			get
			{
				return this.tokenToDeriveIdentifier;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0007B340 File Offset: 0x00079540
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				if (this.securityKeys == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DerivedKeyNotInitialized")));
				}
				return this.securityKeys;
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0007B36A File Offset: 0x0007956A
		public byte[] GetKeyBytes()
		{
			return SecurityUtils.CloneBuffer(this.key);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0007B377 File Offset: 0x00079577
		public byte[] GetNonce()
		{
			return SecurityUtils.CloneBuffer(this.nonce);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0007B384 File Offset: 0x00079584
		internal bool TryGetSecurityKeys(out ReadOnlyCollection<SecurityKey> keys)
		{
			keys = this.securityKeys;
			return keys != null;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0007B394 File Offset: 0x00079594
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			stringWriter.WriteLine("DerivedKeySecurityToken:");
			stringWriter.WriteLine("   Generation: {0}", this.Generation);
			stringWriter.WriteLine("   Offset: {0}", this.Offset);
			stringWriter.WriteLine("   Length: {0}", this.Length);
			stringWriter.WriteLine("   Label: {0}", this.Label);
			stringWriter.WriteLine("   Nonce: {0}", Convert.ToBase64String(this.Nonce));
			stringWriter.WriteLine("   TokenToDeriveFrom:");
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				SecurityStandardsManager.DefaultInstance.SecurityTokenSerializer.WriteKeyIdentifierClause(XmlDictionaryWriter.CreateDictionaryWriter(xmlTextWriter), this.TokenToDeriveIdentifier);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0007B478 File Offset: 0x00079678
		private void Initialize(string id, int generation, int offset, int length, string label, byte[] nonce, SecurityToken tokenToDerive, SecurityKeyIdentifierClause tokenToDeriveIdentifier, string derivationAlgorithm)
		{
			this.Initialize(id, generation, offset, length, label, nonce, tokenToDerive, tokenToDeriveIdentifier, derivationAlgorithm, true);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0007B49C File Offset: 0x0007969C
		private void Initialize(string id, int generation, int offset, int length, string label, byte[] nonce, SecurityToken tokenToDerive, SecurityKeyIdentifierClause tokenToDeriveIdentifier, string derivationAlgorithm, bool initializeDerivedKey)
		{
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			if (tokenToDerive == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenToDerive");
			}
			if (tokenToDeriveIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokentoDeriveIdentifier");
			}
			if (!SecurityUtils.IsSupportedAlgorithm(derivationAlgorithm, tokenToDerive))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("DerivedKeyCannotDeriveFromSecret")));
			}
			if (nonce == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("nonce");
			}
			if (length == -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("length"));
			}
			if (offset == -1 && generation == -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("DerivedKeyPosAndGenNotSpecified"));
			}
			if (offset >= 0 && generation >= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("DerivedKeyPosAndGenBothSpecified"));
			}
			this.id = id;
			this.label = label;
			this.nonce = nonce;
			this.length = length;
			this.offset = offset;
			this.generation = generation;
			this.tokenToDerive = tokenToDerive;
			this.tokenToDeriveIdentifier = tokenToDeriveIdentifier;
			this.keyDerivationAlgorithm = derivationAlgorithm;
			if (initializeDerivedKey)
			{
				this.InitializeDerivedKey(this.length);
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0007B5C8 File Offset: 0x000797C8
		internal void InitializeDerivedKey(int maxKeyLength)
		{
			if (this.key != null)
			{
				return;
			}
			if (this.length > maxKeyLength)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("DerivedKeyLengthTooLong", new object[]
				{
					this.length,
					maxKeyLength
				}));
			}
			this.key = SecurityUtils.GenerateDerivedKey(this.tokenToDerive, this.keyDerivationAlgorithm, (this.label != null) ? Encoding.UTF8.GetBytes(this.label) : DerivedKeySecurityToken.DefaultLabel, this.nonce, this.length * 8, (this.offset >= 0) ? this.offset : (this.generation * this.length));
			if (this.key == null || this.key.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("DerivedKeyCannotDeriveFromSecret"));
			}
			this.securityKeys = new List<SecurityKey>(1)
			{
				new InMemorySymmetricSecurityKey(this.key, false)
			}.AsReadOnly();
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0007B6C6 File Offset: 0x000798C6
		internal void InitializeDerivedKey(ReadOnlyCollection<SecurityKey> securityKeys)
		{
			this.key = ((SymmetricSecurityKey)securityKeys[0]).GetSymmetricKey();
			this.securityKeys = securityKeys;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0007B6E8 File Offset: 0x000798E8
		internal static void EnsureAcceptableOffset(int offset, int generation, int length, int maxOffset)
		{
			if (offset != -1)
			{
				if (offset > maxOffset)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("DerivedKeyTokenOffsetTooHigh", new object[]
					{
						offset,
						maxOffset
					})));
				}
			}
			else
			{
				int num = generation * length;
				if ((num < generation && num < length) || num > maxOffset)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("DerivedKeyTokenGenerationAndLengthTooHigh", new object[]
					{
						generation,
						length,
						maxOffset
					})));
				}
			}
		}

		// Token: 0x04001F3C RID: 7996
		private static readonly byte[] DefaultLabel = new byte[]
		{
			87,
			83,
			45,
			83,
			101,
			99,
			117,
			114,
			101,
			67,
			111,
			110,
			118,
			101,
			114,
			115,
			97,
			116,
			105,
			111,
			110,
			87,
			83,
			45,
			83,
			101,
			99,
			117,
			114,
			101,
			67,
			111,
			110,
			118,
			101,
			114,
			115,
			97,
			116,
			105,
			111,
			110
		};

		// Token: 0x04001F3D RID: 7997
		public const int DefaultNonceLength = 16;

		// Token: 0x04001F3E RID: 7998
		public const int DefaultDerivedKeyLength = 32;

		// Token: 0x04001F3F RID: 7999
		private string id;

		// Token: 0x04001F40 RID: 8000
		private byte[] key;

		// Token: 0x04001F41 RID: 8001
		private string keyDerivationAlgorithm;

		// Token: 0x04001F42 RID: 8002
		private string label;

		// Token: 0x04001F43 RID: 8003
		private int length;

		// Token: 0x04001F44 RID: 8004
		private byte[] nonce;

		// Token: 0x04001F45 RID: 8005
		private int offset;

		// Token: 0x04001F46 RID: 8006
		private int generation;

		// Token: 0x04001F47 RID: 8007
		private SecurityToken tokenToDerive;

		// Token: 0x04001F48 RID: 8008
		private SecurityKeyIdentifierClause tokenToDeriveIdentifier;

		// Token: 0x04001F49 RID: 8009
		private ReadOnlyCollection<SecurityKey> securityKeys;
	}
}

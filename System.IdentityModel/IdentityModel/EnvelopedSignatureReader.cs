using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200003C RID: 60
	public sealed class EnvelopedSignatureReader : DelegatingXmlDictionaryReader
	{
		// Token: 0x0600022F RID: 559 RVA: 0x000092AB File Offset: 0x000074AB
		public EnvelopedSignatureReader(XmlReader reader, SecurityTokenSerializer securityTokenSerializer) : this(reader, securityTokenSerializer, null)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000092B6 File Offset: 0x000074B6
		public EnvelopedSignatureReader(XmlReader reader, SecurityTokenSerializer securityTokenSerializer, SecurityTokenResolver signingTokenResolver) : this(reader, securityTokenSerializer, signingTokenResolver, true, true, true)
		{
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000092C4 File Offset: 0x000074C4
		public EnvelopedSignatureReader(XmlReader reader, SecurityTokenSerializer securityTokenSerializer, SecurityTokenResolver signingTokenResolver, bool requireSignature, bool automaticallyReadSignature, bool resolveIntrinsicSigningKeys)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (securityTokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenSerializer");
			}
			this._automaticallyReadSignature = automaticallyReadSignature;
			this._dictionaryManager = new DictionaryManager();
			this._tokenSerializer = securityTokenSerializer;
			this._requireSignature = requireSignature;
			this._signingTokenResolver = (signingTokenResolver ?? EmptySecurityTokenResolver.Instance);
			this._resolveIntrinsicSigningKeys = resolveIntrinsicSigningKeys;
			XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
			this._wrappedReader = new WrappedReader(reader2);
			base.InitializeInnerReader(this._wrappedReader);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009358 File Offset: 0x00007558
		private void OnEndOfRootElement()
		{
			if (this._signedXml == null)
			{
				if (this._requireSignature)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID3089")));
				}
			}
			else
			{
				this.ResolveSigningCredentials();
				this._signedXml.StartSignatureVerification(this._signingCredentials.SigningKey);
				this._wrappedReader.XmlTokens.SetElementExclusion(XD.XmlSignatureDictionary.Signature.Value, XD.XmlSignatureDictionary.Namespace.Value);
				WifSignedInfo wifSignedInfo = this._signedXml.Signature.SignedInfo as WifSignedInfo;
				this._signedXml.EnsureDigestValidity(wifSignedInfo[0].ExtractReferredId(), this._wrappedReader);
				this._signedXml.CompleteSignatureVerification();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000941A File Offset: 0x0000761A
		public SigningCredentials SigningCredentials
		{
			get
			{
				return this._signingCredentials;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00009422 File Offset: 0x00007622
		internal XmlTokenStream XmlTokens
		{
			get
			{
				return this._wrappedReader.XmlTokens.Trim();
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009434 File Offset: 0x00007634
		public override bool Read()
		{
			if (base.NodeType == XmlNodeType.Element && !base.IsEmptyElement)
			{
				this._elementCount++;
			}
			if (base.NodeType == XmlNodeType.EndElement)
			{
				this._elementCount--;
				if (this._elementCount == 0)
				{
					this.OnEndOfRootElement();
				}
			}
			bool flag = base.Read();
			if (this._automaticallyReadSignature && this._signedXml == null && flag && base.InnerReader.IsLocalName(XD.XmlSignatureDictionary.Signature) && base.InnerReader.IsNamespaceUri(XD.XmlSignatureDictionary.Namespace))
			{
				this.ReadSignature();
			}
			return flag;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000094DC File Offset: 0x000076DC
		private void ReadSignature()
		{
			this._signedXml = new SignedXml(new WifSignedInfo(this._dictionaryManager), this._dictionaryManager, this._tokenSerializer);
			this._signedXml.TransformFactory = ExtendedTransformFactory.Instance;
			this._signedXml.ReadFrom(this._wrappedReader);
			if (this._signedXml.Signature.SignedInfo.ReferenceCount != 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID3057")));
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009560 File Offset: 0x00007760
		private void ResolveSigningCredentials()
		{
			if (this._signedXml.Signature == null || this._signedXml.Signature.KeyIdentifier == null || this._signedXml.Signature.KeyIdentifier.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3276")));
			}
			SecurityKey signingKey = null;
			WifSignedInfo wifSignedInfo = this._signedXml.Signature.SignedInfo as WifSignedInfo;
			if (!this._signingTokenResolver.TryResolveSecurityKey(this._signedXml.Signature.KeyIdentifier[0], out signingKey))
			{
				if (this._resolveIntrinsicSigningKeys && this._signedXml.Signature.KeyIdentifier.CanCreateKey)
				{
					if (this._signedXml.Signature.KeyIdentifier.Count < 2 || LocalAppContextSwitches.ReturnMultipleSecurityKeyIdentifierClauses)
					{
						signingKey = this._signedXml.Signature.KeyIdentifier.CreateKey();
						this._signingCredentials = new SigningCredentials(signingKey, this._signedXml.Signature.SignedInfo.SignatureMethod, wifSignedInfo[0].DigestMethod, this._signedXml.Signature.KeyIdentifier);
						return;
					}
					foreach (SecurityKeyIdentifierClause securityKeyIdentifierClause in this._signedXml.Signature.KeyIdentifier)
					{
						if (securityKeyIdentifierClause.CanCreateKey)
						{
							this._signingCredentials = new SigningCredentials(securityKeyIdentifierClause.CreateKey(), this._signedXml.Signature.SignedInfo.SignatureMethod, wifSignedInfo[0].DigestMethod, new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
							{
								securityKeyIdentifierClause
							}));
							return;
						}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("KeyIdentifierCannotCreateKey")));
				}
				else
				{
					EncryptedKeyIdentifierClause encryptedKeyIdentifierClause;
					if (this._signedXml.Signature.KeyIdentifier.TryFind<EncryptedKeyIdentifierClause>(out encryptedKeyIdentifierClause))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SignatureVerificationFailedException(SR.GetString("ID4036", new object[]
						{
							XmlUtil.SerializeSecurityKeyIdentifier(this._signedXml.Signature.KeyIdentifier, this._tokenSerializer)
						})));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SignatureVerificationFailedException(SR.GetString("ID4037", new object[]
					{
						this._signedXml.Signature.KeyIdentifier.ToString()
					})));
				}
			}
			else
			{
				if (this._signedXml.Signature.KeyIdentifier.Count < 2 || LocalAppContextSwitches.ReturnMultipleSecurityKeyIdentifierClauses)
				{
					this._signingCredentials = new SigningCredentials(signingKey, this._signedXml.Signature.SignedInfo.SignatureMethod, wifSignedInfo[0].DigestMethod, this._signedXml.Signature.KeyIdentifier);
					return;
				}
				this._signingCredentials = new SigningCredentials(signingKey, this._signedXml.Signature.SignedInfo.SignatureMethod, wifSignedInfo[0].DigestMethod, new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					this._signedXml.Signature.KeyIdentifier[0]
				}));
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009880 File Offset: 0x00007A80
		public bool TryReadSignature()
		{
			if (this.IsStartElement(XD.XmlSignatureDictionary.Signature, XD.XmlSignatureDictionary.Namespace))
			{
				this.ReadSignature();
				return true;
			}
			return false;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000098A7 File Offset: 0x00007AA7
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._disposed)
			{
				return;
			}
			if (disposing && this._wrappedReader != null)
			{
				this._wrappedReader.Close();
				this._wrappedReader = null;
			}
			this._disposed = true;
		}

		// Token: 0x0400014F RID: 335
		private bool _automaticallyReadSignature;

		// Token: 0x04000150 RID: 336
		private DictionaryManager _dictionaryManager;

		// Token: 0x04000151 RID: 337
		private int _elementCount;

		// Token: 0x04000152 RID: 338
		private bool _resolveIntrinsicSigningKeys;

		// Token: 0x04000153 RID: 339
		private bool _requireSignature;

		// Token: 0x04000154 RID: 340
		private SigningCredentials _signingCredentials;

		// Token: 0x04000155 RID: 341
		private SecurityTokenResolver _signingTokenResolver;

		// Token: 0x04000156 RID: 342
		private SignedXml _signedXml;

		// Token: 0x04000157 RID: 343
		private SecurityTokenSerializer _tokenSerializer;

		// Token: 0x04000158 RID: 344
		private WrappedReader _wrappedReader;

		// Token: 0x04000159 RID: 345
		private bool _disposed;
	}
}

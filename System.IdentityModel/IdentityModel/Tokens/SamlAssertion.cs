using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IO;
using System.Runtime;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200014C RID: 332
	public class SamlAssertion : ICanonicalWriterEndRootElementCallback
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0002BFB5 File Offset: 0x0002A1B5
		internal static void ResetAssertionDepth()
		{
			SamlAssertion.t_assertionDepth = 0;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002BFC0 File Offset: 0x0002A1C0
		public SamlAssertion()
		{
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002C014 File Offset: 0x0002A214
		public SamlAssertion(string assertionId, string issuer, DateTime issueInstant, SamlConditions samlConditions, SamlAdvice samlAdvice, IEnumerable<SamlStatement> samlStatements)
		{
			if (string.IsNullOrEmpty(assertionId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAssertionIdRequired"));
			}
			if (!this.IsAssertionIdValid(assertionId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAssertionIDIsInvalid", new object[]
				{
					assertionId
				}));
			}
			if (string.IsNullOrEmpty(issuer))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAssertionIssuerRequired"));
			}
			if (samlStatements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlStatements");
			}
			this.assertionId = assertionId;
			this.issuer = issuer;
			this.issueInstant = issueInstant.ToUniversalTime();
			this.conditions = samlConditions;
			this.advice = samlAdvice;
			foreach (SamlStatement samlStatement in samlStatements)
			{
				if (samlStatement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
					{
						XD.SamlDictionary.Statement.Value
					}));
				}
				this.statements.Add(samlStatement);
			}
			if (this.statements.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAssertionRequireOneStatement"));
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002C19C File Offset: 0x0002A39C
		public int MinorVersion
		{
			get
			{
				return SamlConstants.MinorVersionValue;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002C1A3 File Offset: 0x0002A3A3
		public int MajorVersion
		{
			get
			{
				return SamlConstants.MajorVersionValue;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002C1AA File Offset: 0x0002A3AA
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0002C1B4 File Offset: 0x0002A3B4
		public string AssertionId
		{
			get
			{
				return this.assertionId;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAssertionIdRequired"));
				}
				this.assertionId = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0002C207 File Offset: 0x0002A407
		public virtual bool CanWriteSourceData
		{
			get
			{
				return this.sourceData != null;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002C212 File Offset: 0x0002A412
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0002C21C File Offset: 0x0002A41C
		public string Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAssertionIssuerRequired"));
				}
				this.issuer = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0002C26F File Offset: 0x0002A46F
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x0002C277 File Offset: 0x0002A477
		public DateTime IssueInstant
		{
			get
			{
				return this.issueInstant;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.issueInstant = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002C2A2 File Offset: 0x0002A4A2
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0002C2AA File Offset: 0x0002A4AA
		public SamlConditions Conditions
		{
			get
			{
				return this.conditions;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.conditions = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002C2D5 File Offset: 0x0002A4D5
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0002C2DD File Offset: 0x0002A4DD
		public SamlAdvice Advice
		{
			get
			{
				return this.advice;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.advice = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002C308 File Offset: 0x0002A508
		public IList<SamlStatement> Statements
		{
			get
			{
				return this.statements;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002C310 File Offset: 0x0002A510
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0002C318 File Offset: 0x0002A518
		public SigningCredentials SigningCredentials
		{
			get
			{
				return this.signingCredentials;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.signingCredentials = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002C343 File Offset: 0x0002A543
		internal SignedXml Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002C34B File Offset: 0x0002A54B
		internal SecurityKey SignatureVerificationKey
		{
			get
			{
				return this.verificationKey;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002C353 File Offset: 0x0002A553
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002C35B File Offset: 0x0002A55B
		public SecurityToken SigningToken
		{
			get
			{
				return this.signingToken;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.signingToken = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002C386 File Offset: 0x0002A586
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002C38E File Offset: 0x0002A58E
		internal ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this.cryptoList;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002C398 File Offset: 0x0002A598
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				if (this.conditions != null)
				{
					this.conditions.MakeReadOnly();
				}
				if (this.advice != null)
				{
					this.advice.MakeReadOnly();
				}
				foreach (SamlStatement samlStatement in this.statements)
				{
					samlStatement.MakeReadOnly();
				}
				this.statements.MakeReadOnly();
				if (this.cryptoList == null)
				{
					this.cryptoList = this.BuildCryptoList();
				}
				this.isReadOnly = true;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002C438 File Offset: 0x0002A638
		internal virtual void CaptureSourceData(EnvelopedSignatureReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			this.sourceData = reader.XmlTokens;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002C45C File Offset: 0x0002A65C
		protected void ReadSignature(XmlDictionaryReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver, SamlSerializer samlSerializer)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSerializer");
			}
			if (this.signature != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SAMLSignatureAlreadyRead")));
			}
			XmlDictionaryReader xmlDictionaryReader = reader;
			if (!xmlDictionaryReader.CanCanonicalize)
			{
				MemoryStream memoryStream = new MemoryStream();
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream, samlSerializer.DictionaryManager.ParentDictionary);
				xmlDictionaryWriter.WriteNode(xmlDictionaryReader, false);
				xmlDictionaryWriter.Flush();
				memoryStream.Position = 0L;
				xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, samlSerializer.DictionaryManager.ParentDictionary, reader.Quotas);
				xmlDictionaryReader.MoveToContent();
				xmlDictionaryWriter.Close();
			}
			SignedXml signedXml = new SignedXml(new StandardSignedInfo(samlSerializer.DictionaryManager), samlSerializer.DictionaryManager, keyInfoSerializer);
			signedXml.TransformFactory = ExtendedTransformFactory.Instance;
			signedXml.ReadFrom(xmlDictionaryReader);
			SecurityKeyIdentifier keyIdentifier = signedXml.Signature.KeyIdentifier;
			SecurityKeyIdentifierClause securityKeyIdentifierClause = null;
			if (keyIdentifier.Count < 2 || LocalAppContextSwitches.ProcessMultipleSecurityKeyIdentifierClauses)
			{
				this.verificationKey = SamlSerializer.ResolveSecurityKey(keyIdentifier, outOfBandTokenResolver);
			}
			else
			{
				this.verificationKey = SamlAssertion.ResolveSecurityKey(keyIdentifier, outOfBandTokenResolver, out securityKeyIdentifierClause);
			}
			if (this.verificationKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToResolveSignatureKey", new object[]
				{
					this.issuer
				})));
			}
			this.signature = signedXml;
			if (keyIdentifier.Count < 2 || LocalAppContextSwitches.ProcessMultipleSecurityKeyIdentifierClauses)
			{
				this.signingToken = SamlSerializer.ResolveSecurityToken(keyIdentifier, outOfBandTokenResolver);
			}
			else
			{
				this.signingToken = SamlSerializer.ResolveSecurityToken(new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					securityKeyIdentifierClause
				}), outOfBandTokenResolver);
			}
			if (this.signingToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SamlSigningTokenNotFound")));
			}
			if (reader != xmlDictionaryReader)
			{
				xmlDictionaryReader.Close();
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002C630 File Offset: 0x0002A830
		private static SecurityKey ResolveSecurityKey(SecurityKeyIdentifier ski, SecurityTokenResolver tokenResolver, out SecurityKeyIdentifierClause clause)
		{
			if (ski == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ski");
			}
			clause = null;
			if (tokenResolver != null)
			{
				for (int i = 0; i < ski.Count; i++)
				{
					SecurityKey result = null;
					if (tokenResolver.TryResolveSecurityKey(ski[i], out result))
					{
						clause = ski[i];
						return result;
					}
				}
			}
			if (ski.CanCreateKey)
			{
				foreach (SecurityKeyIdentifierClause securityKeyIdentifierClause in ski)
				{
					if (securityKeyIdentifierClause.CanCreateKey)
					{
						clause = securityKeyIdentifierClause;
						return clause.CreateKey();
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("KeyIdentifierCannotCreateKey")));
			}
			return null;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002C6F8 File Offset: 0x0002A8F8
		private void CheckObjectValidity()
		{
			if (string.IsNullOrEmpty(this.assertionId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionIdRequired")));
			}
			if (!this.IsAssertionIdValid(this.assertionId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionIDIsInvalid", new object[]
				{
					this.assertionId
				})));
			}
			if (string.IsNullOrEmpty(this.issuer))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionIssuerRequired")));
			}
			if (this.statements.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionRequireOneStatement")));
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002C7B4 File Offset: 0x0002A9B4
		private bool IsAssertionIdValid(string assertionId)
		{
			return !string.IsNullOrEmpty(assertionId) && ((assertionId[0] >= 'A' && assertionId[0] <= 'Z') || (assertionId[0] >= 'a' && assertionId[0] <= 'z') || assertionId[0] == '_');
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002C804 File Offset: 0x0002AA04
		private ReadOnlyCollection<SecurityKey> BuildCryptoList()
		{
			List<SecurityKey> list = new List<SecurityKey>();
			for (int i = 0; i < this.statements.Count; i++)
			{
				SamlSubjectStatement samlSubjectStatement = this.statements[i] as SamlSubjectStatement;
				if (samlSubjectStatement != null)
				{
					bool flag = false;
					SecurityKey securityKey = null;
					if (samlSubjectStatement.SamlSubject != null)
					{
						securityKey = samlSubjectStatement.SamlSubject.Crypto;
					}
					InMemorySymmetricSecurityKey inMemorySymmetricSecurityKey = securityKey as InMemorySymmetricSecurityKey;
					if (inMemorySymmetricSecurityKey != null)
					{
						for (int j = 0; j < list.Count; j++)
						{
							if (list[j] is InMemorySymmetricSecurityKey && list[j].KeySize == inMemorySymmetricSecurityKey.KeySize)
							{
								byte[] symmetricKey = ((InMemorySymmetricSecurityKey)list[j]).GetSymmetricKey();
								byte[] symmetricKey2 = inMemorySymmetricSecurityKey.GetSymmetricKey();
								int num = 0;
								while (num < symmetricKey.Length && symmetricKey[num] == symmetricKey2[num])
								{
									num++;
								}
								flag = (num == symmetricKey.Length);
							}
							if (flag)
							{
								break;
							}
						}
					}
					if (!flag && securityKey != null)
					{
						list.Add(securityKey);
					}
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002C910 File Offset: 0x0002AB10
		private void VerifySignature(SignedXml signature, SecurityKey signatureVerificationKey)
		{
			if (signature == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signature");
			}
			if (signatureVerificationKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signatureVerificatonKey");
			}
			signature.StartSignatureVerification(signatureVerificationKey);
			signature.EnsureDigestValidity(this.assertionId, this.tokenStream);
			signature.CompleteSignatureVerification();
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002C964 File Offset: 0x0002AB64
		void ICanonicalWriterEndRootElementCallback.OnEndOfRootElement(XmlDictionaryWriter dictionaryWriter)
		{
			byte[] digest = this.hashStream.FlushHashAndGetValue();
			PreDigestedSignedInfo preDigestedSignedInfo = new PreDigestedSignedInfo(this.dictionaryManager);
			preDigestedSignedInfo.AddEnvelopedSignatureTransform = true;
			preDigestedSignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
			preDigestedSignedInfo.SignatureMethod = this.signingCredentials.SignatureAlgorithm;
			preDigestedSignedInfo.DigestMethod = this.signingCredentials.DigestAlgorithm;
			preDigestedSignedInfo.AddReference(this.assertionId, digest);
			SignedXml signedXml = new SignedXml(preDigestedSignedInfo, this.dictionaryManager, this.keyInfoSerializer);
			signedXml.ComputeSignature(this.signingCredentials.SigningKey);
			signedXml.Signature.KeyIdentifier = this.signingCredentials.SigningKeyIdentifier;
			signedXml.WriteTo(dictionaryWriter);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002CA0C File Offset: 0x0002AC0C
		public virtual void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ReadXml"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlAssertion.t_assertionDepth++;
			if (!LocalAppContextSwitches.AllowUnlimitedXmlRecursion && SamlAssertion.t_assertionDepth > 8)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToLoadAssertion"), new InvalidOperationException(SR.GetString("ID4194", new object[]
				{
					SamlAssertion.t_assertionDepth,
					8
				}))));
			}
			try
			{
				XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
				WrappedReader wrappedReader = new WrappedReader(reader2);
				SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
				if (!wrappedReader.IsStartElement(samlDictionary.Assertion, samlDictionary.Namespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLElementNotRecognized", new object[]
					{
						wrappedReader.LocalName
					})));
				}
				string attribute = wrappedReader.GetAttribute(samlDictionary.MajorVersion, null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionMissingMajorVersionAttributeOnRead")));
				}
				int num = int.Parse(attribute, CultureInfo.InvariantCulture);
				attribute = wrappedReader.GetAttribute(samlDictionary.MinorVersion, null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionMissingMinorVersionAttributeOnRead")));
				}
				int num2 = int.Parse(attribute, CultureInfo.InvariantCulture);
				if (num != SamlConstants.MajorVersionValue || num2 != SamlConstants.MinorVersionValue)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLTokenVersionNotSupported", new object[]
					{
						num,
						num2,
						SamlConstants.MajorVersionValue,
						SamlConstants.MinorVersionValue
					})));
				}
				attribute = wrappedReader.GetAttribute(samlDictionary.AssertionId, null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionIdRequired")));
				}
				if (!this.IsAssertionIdValid(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionIDIsInvalid", new object[]
					{
						attribute
					})));
				}
				this.assertionId = attribute;
				attribute = wrappedReader.GetAttribute(samlDictionary.Issuer, null);
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionMissingIssuerAttributeOnRead")));
				}
				this.issuer = attribute;
				attribute = wrappedReader.GetAttribute(samlDictionary.IssueInstant, null);
				if (!string.IsNullOrEmpty(attribute))
				{
					this.issueInstant = DateTime.ParseExact(attribute, SamlConstants.AcceptedDateTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
				}
				wrappedReader.MoveToContent();
				wrappedReader.Read();
				if (wrappedReader.IsStartElement(samlDictionary.Conditions, samlDictionary.Namespace))
				{
					this.conditions = samlSerializer.LoadConditions(wrappedReader, keyInfoSerializer, outOfBandTokenResolver);
					if (this.conditions == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToLoadCondtions")));
					}
				}
				if (wrappedReader.IsStartElement(samlDictionary.Advice, samlDictionary.Namespace))
				{
					this.advice = samlSerializer.LoadAdvice(wrappedReader, keyInfoSerializer, outOfBandTokenResolver);
					if (this.advice == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToLoadAdvice")));
					}
				}
				while (wrappedReader.IsStartElement() && !wrappedReader.IsStartElement(samlSerializer.DictionaryManager.XmlSignatureDictionary.Signature, samlSerializer.DictionaryManager.XmlSignatureDictionary.Namespace))
				{
					SamlStatement samlStatement = samlSerializer.LoadStatement(wrappedReader, keyInfoSerializer, outOfBandTokenResolver);
					if (samlStatement == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToLoadStatement")));
					}
					this.statements.Add(samlStatement);
				}
				if (this.statements.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAssertionRequireOneStatementOnRead")));
				}
				if (wrappedReader.IsStartElement(samlSerializer.DictionaryManager.XmlSignatureDictionary.Signature, samlSerializer.DictionaryManager.XmlSignatureDictionary.Namespace))
				{
					this.ReadSignature(wrappedReader, keyInfoSerializer, outOfBandTokenResolver, samlSerializer);
				}
				wrappedReader.MoveToContent();
				wrappedReader.ReadEndElement();
				this.tokenStream = wrappedReader.XmlTokens;
				if (this.signature != null)
				{
					this.VerifySignature(this.signature, this.verificationKey);
				}
				this.BuildCryptoList();
			}
			finally
			{
				SamlAssertion.t_assertionDepth--;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002CE70 File Offset: 0x0002B070
		internal void WriteTo(XmlWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (this.signingCredentials == null && this.signature == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SamlAssertionMissingSigningCredentials")));
			}
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			if (this.signingCredentials != null)
			{
				using (HashAlgorithm hashAlgorithm = CryptoHelper.CreateHashAlgorithm(this.signingCredentials.DigestAlgorithm))
				{
					this.hashStream = new HashStream(hashAlgorithm);
					this.keyInfoSerializer = keyInfoSerializer;
					this.dictionaryManager = samlSerializer.DictionaryManager;
					SamlDelegatingWriter writer2 = new SamlDelegatingWriter(xmlDictionaryWriter, this.hashStream, this, samlSerializer.DictionaryManager.ParentDictionary);
					this.WriteXml(writer2, samlSerializer, keyInfoSerializer);
					return;
				}
			}
			this.tokenStream.SetElementExclusion(null, null);
			this.tokenStream.WriteTo(xmlDictionaryWriter, samlSerializer.DictionaryManager);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002CF58 File Offset: 0x0002B158
		public virtual void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
		{
			this.CheckObjectValidity();
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			try
			{
				writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.Assertion, samlDictionary.Namespace);
				writer.WriteStartAttribute(samlDictionary.MajorVersion, null);
				writer.WriteValue(SamlConstants.MajorVersionValue);
				writer.WriteEndAttribute();
				writer.WriteStartAttribute(samlDictionary.MinorVersion, null);
				writer.WriteValue(SamlConstants.MinorVersionValue);
				writer.WriteEndAttribute();
				writer.WriteStartAttribute(samlDictionary.AssertionId, null);
				writer.WriteString(this.assertionId);
				writer.WriteEndAttribute();
				writer.WriteStartAttribute(samlDictionary.Issuer, null);
				writer.WriteString(this.issuer);
				writer.WriteEndAttribute();
				writer.WriteStartAttribute(samlDictionary.IssueInstant, null);
				writer.WriteString(this.issueInstant.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture));
				writer.WriteEndAttribute();
				if (this.conditions != null)
				{
					this.conditions.WriteXml(writer, samlSerializer, keyInfoSerializer);
				}
				if (this.advice != null)
				{
					this.advice.WriteXml(writer, samlSerializer, keyInfoSerializer);
				}
				for (int i = 0; i < this.statements.Count; i++)
				{
					this.statements[i].WriteXml(writer, samlSerializer, keyInfoSerializer);
				}
				writer.WriteEndElement();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SAMLTokenNotSerialized"), ex));
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002D108 File Offset: 0x0002B308
		public virtual void WriteSourceData(XmlWriter writer)
		{
			if (!this.CanWriteSourceData)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4140")));
			}
			XmlDictionaryWriter writer2 = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.sourceData.SetElementExclusion(null, null);
			this.sourceData.GetWriter().WriteTo(writer2, null);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002D160 File Offset: 0x0002B360
		internal static void AddSamlClaimTypes(ICollection<Type> knownClaimTypes)
		{
			if (knownClaimTypes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("knownClaimTypes");
			}
			knownClaimTypes.Add(typeof(SamlAuthorizationDecisionClaimResource));
			knownClaimTypes.Add(typeof(SamlAuthenticationClaimResource));
			knownClaimTypes.Add(typeof(SamlAccessDecision));
			knownClaimTypes.Add(typeof(SamlAuthorityBinding));
			knownClaimTypes.Add(typeof(SamlNameIdentifierClaimResource));
		}

		// Token: 0x04000B84 RID: 2948
		private string assertionId = "SamlSecurityToken-" + Guid.NewGuid().ToString();

		// Token: 0x04000B85 RID: 2949
		private string issuer;

		// Token: 0x04000B86 RID: 2950
		private DateTime issueInstant = DateTime.UtcNow.ToUniversalTime();

		// Token: 0x04000B87 RID: 2951
		private SamlConditions conditions;

		// Token: 0x04000B88 RID: 2952
		private SamlAdvice advice;

		// Token: 0x04000B89 RID: 2953
		private readonly ImmutableCollection<SamlStatement> statements = new ImmutableCollection<SamlStatement>();

		// Token: 0x04000B8A RID: 2954
		private ReadOnlyCollection<SecurityKey> cryptoList;

		// Token: 0x04000B8B RID: 2955
		private SignedXml signature;

		// Token: 0x04000B8C RID: 2956
		private SigningCredentials signingCredentials;

		// Token: 0x04000B8D RID: 2957
		private SecurityKey verificationKey;

		// Token: 0x04000B8E RID: 2958
		private SecurityToken signingToken;

		// Token: 0x04000B8F RID: 2959
		private HashStream hashStream;

		// Token: 0x04000B90 RID: 2960
		private XmlTokenStream tokenStream;

		// Token: 0x04000B91 RID: 2961
		private SecurityTokenSerializer keyInfoSerializer;

		// Token: 0x04000B92 RID: 2962
		private DictionaryManager dictionaryManager;

		// Token: 0x04000B93 RID: 2963
		private XmlTokenStream sourceData;

		// Token: 0x04000B94 RID: 2964
		private bool isReadOnly;

		// Token: 0x04000B95 RID: 2965
		[ThreadStatic]
		private static int t_assertionDepth;

		// Token: 0x04000B96 RID: 2966
		private const int MaxAssertionDepth = 8;
	}
}

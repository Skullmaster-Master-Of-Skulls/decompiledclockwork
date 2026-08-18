using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200028A RID: 650
	internal abstract class WSSecureConversation : WSSecurityTokenSerializer.SerializerEntries
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0004430F File Offset: 0x0004250F
		protected WSSecureConversation(WSSecurityTokenSerializer tokenSerializer, int maxKeyDerivationOffset, int maxKeyDerivationLabelLength, int maxKeyDerivationNonceLength)
		{
			if (tokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenSerializer");
			}
			this.tokenSerializer = tokenSerializer;
			this.derivedKeyEntry = new WSSecureConversation.DerivedKeyTokenEntry(this, maxKeyDerivationOffset, maxKeyDerivationLabelLength, maxKeyDerivationNonceLength);
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060012F2 RID: 4850
		public abstract SecureConversationDictionary SerializerDictionary { get; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00044341 File Offset: 0x00042541
		public WSSecurityTokenSerializer WSSecurityTokenSerializer
		{
			get
			{
				return this.tokenSerializer;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00044349 File Offset: 0x00042549
		public override void PopulateTokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			if (tokenEntryList == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenEntryList");
			}
			tokenEntryList.Add(this.derivedKeyEntry);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004436A File Offset: 0x0004256A
		public virtual bool IsAtDerivedKeyToken(XmlDictionaryReader reader)
		{
			return this.derivedKeyEntry.CanReadTokenCore(reader);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00044378 File Offset: 0x00042578
		public virtual void ReadDerivedKeyTokenParameters(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver, out string id, out string derivationAlgorithm, out string label, out int length, out byte[] nonce, out int offset, out int generation, out SecurityKeyIdentifierClause tokenToDeriveIdentifier, out SecurityToken tokenToDerive)
		{
			this.derivedKeyEntry.ReadDerivedKeyTokenParameters(reader, tokenResolver, out id, out derivationAlgorithm, out label, out length, out nonce, out offset, out generation, out tokenToDeriveIdentifier, out tokenToDerive);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000443A4 File Offset: 0x000425A4
		public virtual SecurityToken CreateDerivedKeyToken(string id, string derivationAlgorithm, string label, int length, byte[] nonce, int offset, int generation, SecurityKeyIdentifierClause tokenToDeriveIdentifier, SecurityToken tokenToDerive)
		{
			return this.derivedKeyEntry.CreateDerivedKeyToken(id, derivationAlgorithm, label, length, nonce, offset, generation, tokenToDeriveIdentifier, tokenToDerive);
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000443CB File Offset: 0x000425CB
		public virtual string DerivationAlgorithm
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";
			}
		}

		// Token: 0x04001A0B RID: 6667
		private WSSecurityTokenSerializer tokenSerializer;

		// Token: 0x04001A0C RID: 6668
		private WSSecureConversation.DerivedKeyTokenEntry derivedKeyEntry;

		// Token: 0x02000B24 RID: 2852
		protected class DerivedKeyTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x06006FC8 RID: 28616 RVA: 0x0019EA21 File Offset: 0x0019CC21
			public DerivedKeyTokenEntry(WSSecureConversation parent, int maxKeyDerivationOffset, int maxKeyDerivationLabelLength, int maxKeyDerivationNonceLength)
			{
				if (parent == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
				}
				this.parent = parent;
				this.maxKeyDerivationOffset = maxKeyDerivationOffset;
				this.maxKeyDerivationLabelLength = maxKeyDerivationLabelLength;
				this.maxKeyDerivationNonceLength = maxKeyDerivationNonceLength;
			}

			// Token: 0x17001A10 RID: 6672
			// (get) Token: 0x06006FC9 RID: 28617 RVA: 0x0019EA59 File Offset: 0x0019CC59
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return this.parent.SerializerDictionary.DerivedKeyToken;
				}
			}

			// Token: 0x17001A11 RID: 6673
			// (get) Token: 0x06006FCA RID: 28618 RVA: 0x0019EA6B File Offset: 0x0019CC6B
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return this.parent.SerializerDictionary.Namespace;
				}
			}

			// Token: 0x06006FCB RID: 28619 RVA: 0x0019EA7D File Offset: 0x0019CC7D
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(DerivedKeySecurityToken)
				};
			}

			// Token: 0x17001A12 RID: 6674
			// (get) Token: 0x06006FCC RID: 28620 RVA: 0x0019EA92 File Offset: 0x0019CC92
			public override string TokenTypeUri
			{
				get
				{
					return this.parent.SerializerDictionary.DerivedKeyTokenType.Value;
				}
			}

			// Token: 0x17001A13 RID: 6675
			// (get) Token: 0x06006FCD RID: 28621 RVA: 0x0019EAA9 File Offset: 0x0019CCA9
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006FCE RID: 28622 RVA: 0x0019EAAC File Offset: 0x0019CCAC
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				TokenReferenceStyleHelper.Validate(tokenReferenceStyle);
				if (tokenReferenceStyle == SecurityTokenReferenceStyle.Internal)
				{
					return WSSecurityTokenSerializer.TokenEntry.CreateDirectReference(issuedTokenXml, "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", typeof(DerivedKeySecurityToken));
				}
				if (tokenReferenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenReferenceStyle"));
				}
				return null;
			}

			// Token: 0x06006FCF RID: 28623 RVA: 0x0019EAFC File Offset: 0x0019CCFC
			public virtual void ReadDerivedKeyTokenParameters(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver, out string id, out string derivationAlgorithm, out string label, out int length, out byte[] nonce, out int offset, out int generation, out SecurityKeyIdentifierClause tokenToDeriveIdentifier, out SecurityToken tokenToDerive)
			{
				if (tokenResolver == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenResolver");
				}
				id = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				derivationAlgorithm = reader.GetAttribute(XD.XmlSignatureDictionary.Algorithm, null);
				if (derivationAlgorithm == null)
				{
					derivationAlgorithm = this.parent.DerivationAlgorithm;
				}
				reader.ReadStartElement();
				tokenToDeriveIdentifier = null;
				tokenToDerive = null;
				if (!reader.IsStartElement(XD.SecurityJan2004Dictionary.SecurityTokenReference, XD.SecurityJan2004Dictionary.Namespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("DerivedKeyTokenRequiresTokenReference")));
				}
				tokenToDeriveIdentifier = this.parent.WSSecurityTokenSerializer.ReadKeyIdentifierClause(reader);
				tokenResolver.TryResolveToken(tokenToDeriveIdentifier, out tokenToDerive);
				generation = -1;
				if (reader.IsStartElement(this.parent.SerializerDictionary.Generation, this.parent.SerializerDictionary.Namespace))
				{
					reader.ReadStartElement();
					generation = reader.ReadContentAsInt();
					reader.ReadEndElement();
					if (generation < 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("DerivedKeyInvalidGenerationSpecified", new object[]
						{
							generation
						})));
					}
				}
				offset = -1;
				if (reader.IsStartElement(this.parent.SerializerDictionary.Offset, this.parent.SerializerDictionary.Namespace))
				{
					reader.ReadStartElement();
					offset = reader.ReadContentAsInt();
					reader.ReadEndElement();
					if (offset < 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("DerivedKeyInvalidOffsetSpecified", new object[]
						{
							offset
						})));
					}
				}
				length = 32;
				if (reader.IsStartElement(this.parent.SerializerDictionary.Length, this.parent.SerializerDictionary.Namespace))
				{
					reader.ReadStartElement();
					length = reader.ReadContentAsInt();
					reader.ReadEndElement();
				}
				if (offset == -1 && generation == -1)
				{
					offset = 0;
				}
				DerivedKeySecurityToken.EnsureAcceptableOffset(offset, generation, length, this.maxKeyDerivationOffset);
				label = null;
				if (reader.IsStartElement(this.parent.SerializerDictionary.Label, this.parent.SerializerDictionary.Namespace))
				{
					reader.ReadStartElement();
					label = reader.ReadString();
					reader.ReadEndElement();
				}
				if (label != null && label.Length > this.maxKeyDerivationLabelLength)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("DerivedKeyTokenLabelTooLong", new object[]
					{
						label.Length,
						this.maxKeyDerivationLabelLength
					})));
				}
				nonce = null;
				reader.ReadStartElement(this.parent.SerializerDictionary.Nonce, this.parent.SerializerDictionary.Namespace);
				nonce = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				if (nonce != null && nonce.Length > this.maxKeyDerivationNonceLength)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("DerivedKeyTokenNonceTooLong", new object[]
					{
						nonce.Length,
						this.maxKeyDerivationNonceLength
					})));
				}
				reader.ReadEndElement();
			}

			// Token: 0x06006FD0 RID: 28624 RVA: 0x0019EE30 File Offset: 0x0019D030
			public virtual SecurityToken CreateDerivedKeyToken(string id, string derivationAlgorithm, string label, int length, byte[] nonce, int offset, int generation, SecurityKeyIdentifierClause tokenToDeriveIdentifier, SecurityToken tokenToDerive)
			{
				if (tokenToDerive == null)
				{
					return new DerivedKeySecurityTokenStub(generation, offset, length, label, nonce, tokenToDeriveIdentifier, derivationAlgorithm, id);
				}
				return new DerivedKeySecurityToken(generation, offset, length, label, nonce, tokenToDerive, tokenToDeriveIdentifier, derivationAlgorithm, id);
			}

			// Token: 0x06006FD1 RID: 28625 RVA: 0x0019EE68 File Offset: 0x0019D068
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				string id;
				string derivationAlgorithm;
				string label;
				int length;
				byte[] nonce;
				int offset;
				int generation;
				SecurityKeyIdentifierClause tokenToDeriveIdentifier;
				SecurityToken tokenToDerive;
				this.ReadDerivedKeyTokenParameters(reader, tokenResolver, out id, out derivationAlgorithm, out label, out length, out nonce, out offset, out generation, out tokenToDeriveIdentifier, out tokenToDerive);
				return this.CreateDerivedKeyToken(id, derivationAlgorithm, label, length, nonce, offset, generation, tokenToDeriveIdentifier, tokenToDerive);
			}

			// Token: 0x06006FD2 RID: 28626 RVA: 0x0019EEA4 File Offset: 0x0019D0A4
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				DerivedKeySecurityToken derivedKeySecurityToken = token as DerivedKeySecurityToken;
				string value = this.parent.SerializerDictionary.Prefix.Value;
				writer.WriteStartElement(value, this.parent.SerializerDictionary.DerivedKeyToken, this.parent.SerializerDictionary.Namespace);
				if (derivedKeySecurityToken.Id != null)
				{
					writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, derivedKeySecurityToken.Id);
				}
				if (derivedKeySecurityToken.KeyDerivationAlgorithm != this.parent.DerivationAlgorithm)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnsupportedKeyDerivationAlgorithm", new object[]
					{
						derivedKeySecurityToken.KeyDerivationAlgorithm
					})));
				}
				this.parent.WSSecurityTokenSerializer.WriteKeyIdentifierClause(writer, derivedKeySecurityToken.TokenToDeriveIdentifier);
				if (derivedKeySecurityToken.Generation > 0 || derivedKeySecurityToken.Offset > 0 || derivedKeySecurityToken.Length != 32)
				{
					if (derivedKeySecurityToken.Generation >= 0 && derivedKeySecurityToken.Offset >= 0)
					{
						writer.WriteStartElement(value, this.parent.SerializerDictionary.Generation, this.parent.SerializerDictionary.Namespace);
						writer.WriteValue(derivedKeySecurityToken.Generation);
						writer.WriteEndElement();
					}
					else if (derivedKeySecurityToken.Generation != -1)
					{
						writer.WriteStartElement(value, this.parent.SerializerDictionary.Generation, this.parent.SerializerDictionary.Namespace);
						writer.WriteValue(derivedKeySecurityToken.Generation);
						writer.WriteEndElement();
					}
					else if (derivedKeySecurityToken.Offset != -1)
					{
						writer.WriteStartElement(value, this.parent.SerializerDictionary.Offset, this.parent.SerializerDictionary.Namespace);
						writer.WriteValue(derivedKeySecurityToken.Offset);
						writer.WriteEndElement();
					}
					if (derivedKeySecurityToken.Length != 32)
					{
						writer.WriteStartElement(value, this.parent.SerializerDictionary.Length, this.parent.SerializerDictionary.Namespace);
						writer.WriteValue(derivedKeySecurityToken.Length);
						writer.WriteEndElement();
					}
				}
				if (derivedKeySecurityToken.Label != null)
				{
					writer.WriteStartElement(value, this.parent.SerializerDictionary.Generation, this.parent.SerializerDictionary.Namespace);
					writer.WriteString(derivedKeySecurityToken.Label);
					writer.WriteEndElement();
				}
				writer.WriteStartElement(value, this.parent.SerializerDictionary.Nonce, this.parent.SerializerDictionary.Namespace);
				writer.WriteBase64(derivedKeySecurityToken.Nonce, 0, derivedKeySecurityToken.Nonce.Length);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}

			// Token: 0x04003FEF RID: 16367
			public const string DefaultLabel = "WS-SecureConversation";

			// Token: 0x04003FF0 RID: 16368
			private WSSecureConversation parent;

			// Token: 0x04003FF1 RID: 16369
			private int maxKeyDerivationOffset;

			// Token: 0x04003FF2 RID: 16370
			private int maxKeyDerivationLabelLength;

			// Token: 0x04003FF3 RID: 16371
			private int maxKeyDerivationNonceLength;
		}

		// Token: 0x02000B25 RID: 2853
		protected abstract class SecurityContextTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x06006FD3 RID: 28627 RVA: 0x0019F145 File Offset: 0x0019D345
			public SecurityContextTokenEntry(WSSecureConversation parent, SecurityStateEncoder securityStateEncoder, IList<Type> knownClaimTypes)
			{
				this.parent = parent;
				this.cookieSerializer = new SecurityContextCookieSerializer(securityStateEncoder, knownClaimTypes);
			}

			// Token: 0x17001A14 RID: 6676
			// (get) Token: 0x06006FD4 RID: 28628 RVA: 0x0019F161 File Offset: 0x0019D361
			protected WSSecureConversation Parent
			{
				get
				{
					return this.parent;
				}
			}

			// Token: 0x17001A15 RID: 6677
			// (get) Token: 0x06006FD5 RID: 28629 RVA: 0x0019F169 File Offset: 0x0019D369
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return this.parent.SerializerDictionary.SecurityContextToken;
				}
			}

			// Token: 0x17001A16 RID: 6678
			// (get) Token: 0x06006FD6 RID: 28630 RVA: 0x0019F17B File Offset: 0x0019D37B
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return this.parent.SerializerDictionary.Namespace;
				}
			}

			// Token: 0x06006FD7 RID: 28631 RVA: 0x0019F18D File Offset: 0x0019D38D
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(SecurityContextSecurityToken)
				};
			}

			// Token: 0x17001A17 RID: 6679
			// (get) Token: 0x06006FD8 RID: 28632 RVA: 0x0019F1A2 File Offset: 0x0019D3A2
			public override string TokenTypeUri
			{
				get
				{
					return this.parent.SerializerDictionary.SecurityContextTokenType.Value;
				}
			}

			// Token: 0x17001A18 RID: 6680
			// (get) Token: 0x06006FD9 RID: 28633 RVA: 0x0019F1B9 File Offset: 0x0019D3B9
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006FDA RID: 28634 RVA: 0x0019F1BC File Offset: 0x0019D3BC
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				TokenReferenceStyleHelper.Validate(tokenReferenceStyle);
				if (tokenReferenceStyle == SecurityTokenReferenceStyle.Internal)
				{
					return WSSecurityTokenSerializer.TokenEntry.CreateDirectReference(issuedTokenXml, "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", typeof(SecurityContextSecurityToken));
				}
				if (tokenReferenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenReferenceStyle"));
				}
				UniqueId contextId = null;
				UniqueId generation = null;
				foreach (object obj in issuedTokenXml.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement != null)
					{
						if (xmlElement.LocalName == this.parent.SerializerDictionary.Identifier.Value && xmlElement.NamespaceURI == this.parent.SerializerDictionary.Namespace.Value)
						{
							contextId = XmlHelper.ReadTextElementAsUniqueId(xmlElement);
						}
						else if (this.CanReadGeneration(xmlElement))
						{
							generation = this.ReadGeneration(xmlElement);
						}
					}
				}
				return new SecurityContextKeyIdentifierClause(contextId, generation);
			}

			// Token: 0x06006FDB RID: 28635
			protected abstract bool CanReadGeneration(XmlDictionaryReader reader);

			// Token: 0x06006FDC RID: 28636
			protected abstract bool CanReadGeneration(XmlElement element);

			// Token: 0x06006FDD RID: 28637
			protected abstract UniqueId ReadGeneration(XmlDictionaryReader reader);

			// Token: 0x06006FDE RID: 28638
			protected abstract UniqueId ReadGeneration(XmlElement element);

			// Token: 0x06006FDF RID: 28639 RVA: 0x0019F2D0 File Offset: 0x0019D4D0
			private SecurityContextSecurityToken TryResolveSecurityContextToken(UniqueId contextId, UniqueId generation, string id, SecurityTokenResolver tokenResolver, out ISecurityContextSecurityTokenCache sctCache)
			{
				SecurityContextSecurityToken securityContextSecurityToken = null;
				sctCache = null;
				if (tokenResolver is ISecurityContextSecurityTokenCache)
				{
					sctCache = (ISecurityContextSecurityTokenCache)tokenResolver;
					securityContextSecurityToken = sctCache.GetContext(contextId, generation);
				}
				else if (tokenResolver is AggregateSecurityHeaderTokenResolver)
				{
					AggregateSecurityHeaderTokenResolver aggregateSecurityHeaderTokenResolver = tokenResolver as AggregateSecurityHeaderTokenResolver;
					for (int i = 0; i < aggregateSecurityHeaderTokenResolver.TokenResolvers.Count; i++)
					{
						ISecurityContextSecurityTokenCache securityContextSecurityTokenCache = aggregateSecurityHeaderTokenResolver.TokenResolvers[i] as ISecurityContextSecurityTokenCache;
						if (securityContextSecurityTokenCache != null)
						{
							if (sctCache == null)
							{
								sctCache = securityContextSecurityTokenCache;
							}
							securityContextSecurityToken = securityContextSecurityTokenCache.GetContext(contextId, generation);
							if (securityContextSecurityToken != null)
							{
								break;
							}
						}
					}
				}
				if (securityContextSecurityToken == null)
				{
					return null;
				}
				if (securityContextSecurityToken.Id == id)
				{
					return securityContextSecurityToken;
				}
				return new SecurityContextSecurityToken(securityContextSecurityToken, id);
			}

			// Token: 0x06006FE0 RID: 28640 RVA: 0x0019F370 File Offset: 0x0019D570
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				UniqueId uniqueId = null;
				bool flag = false;
				string attribute = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				SecurityContextSecurityToken securityContextSecurityToken = null;
				reader.ReadFullStartElement();
				reader.MoveToStartElement(this.parent.SerializerDictionary.Identifier, this.parent.SerializerDictionary.Namespace);
				UniqueId uniqueId2 = reader.ReadElementContentAsUniqueId();
				if (this.CanReadGeneration(reader))
				{
					uniqueId = this.ReadGeneration(reader);
				}
				if (reader.IsStartElement(this.parent.SerializerDictionary.Cookie, XD.DotNetSecurityDictionary.Namespace))
				{
					flag = true;
					ISecurityContextSecurityTokenCache securityContextSecurityTokenCache;
					securityContextSecurityToken = this.TryResolveSecurityContextToken(uniqueId2, uniqueId, attribute, tokenResolver, out securityContextSecurityTokenCache);
					if (securityContextSecurityToken == null)
					{
						byte[] array = reader.ReadElementContentAsBase64();
						if (array != null)
						{
							securityContextSecurityToken = this.cookieSerializer.CreateSecurityContextFromCookie(array, uniqueId2, uniqueId, attribute, reader.Quotas);
							if (securityContextSecurityTokenCache != null)
							{
								securityContextSecurityTokenCache.AddContext(securityContextSecurityToken);
							}
						}
					}
					else
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
				if (uniqueId2 == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoSecurityContextIdentifier")));
				}
				if (securityContextSecurityToken == null && !flag)
				{
					ISecurityContextSecurityTokenCache securityContextSecurityTokenCache2;
					securityContextSecurityToken = this.TryResolveSecurityContextToken(uniqueId2, uniqueId, attribute, tokenResolver, out securityContextSecurityTokenCache2);
				}
				if (securityContextSecurityToken == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityContextTokenValidationException(SR.GetString("SecurityContextNotRegistered", new object[]
					{
						uniqueId2,
						uniqueId
					})));
				}
				return securityContextSecurityToken;
			}

			// Token: 0x06006FE1 RID: 28641 RVA: 0x0019F4C3 File Offset: 0x0019D6C3
			protected virtual void WriteGeneration(XmlDictionaryWriter writer, SecurityContextSecurityToken sct)
			{
			}

			// Token: 0x06006FE2 RID: 28642 RVA: 0x0019F4C8 File Offset: 0x0019D6C8
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				SecurityContextSecurityToken securityContextSecurityToken = token as SecurityContextSecurityToken;
				writer.WriteStartElement(this.parent.SerializerDictionary.Prefix.Value, this.parent.SerializerDictionary.SecurityContextToken, this.parent.SerializerDictionary.Namespace);
				if (securityContextSecurityToken.Id != null)
				{
					writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, securityContextSecurityToken.Id);
				}
				writer.WriteStartElement(this.parent.SerializerDictionary.Prefix.Value, this.parent.SerializerDictionary.Identifier, this.parent.SerializerDictionary.Namespace);
				XmlHelper.WriteStringAsUniqueId(writer, securityContextSecurityToken.ContextId);
				writer.WriteEndElement();
				this.WriteGeneration(writer, securityContextSecurityToken);
				if (securityContextSecurityToken.IsCookieMode)
				{
					if (securityContextSecurityToken.CookieBlob == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoCookieInSct")));
					}
					writer.WriteStartElement(XD.DotNetSecurityDictionary.Prefix.Value, this.parent.SerializerDictionary.Cookie, XD.DotNetSecurityDictionary.Namespace);
					writer.WriteBase64(securityContextSecurityToken.CookieBlob, 0, securityContextSecurityToken.CookieBlob.Length);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}

			// Token: 0x04003FF4 RID: 16372
			private WSSecureConversation parent;

			// Token: 0x04003FF5 RID: 16373
			private SecurityContextCookieSerializer cookieSerializer;
		}

		// Token: 0x02000B26 RID: 2854
		public abstract class Driver : SecureConversationDriver
		{
			// Token: 0x06006FE3 RID: 28643 RVA: 0x0019F61D File Offset: 0x0019D81D
			public Driver()
			{
			}

			// Token: 0x17001A19 RID: 6681
			// (get) Token: 0x06006FE4 RID: 28644
			protected abstract SecureConversationDictionary DriverDictionary { get; }

			// Token: 0x17001A1A RID: 6682
			// (get) Token: 0x06006FE5 RID: 28645 RVA: 0x0019F625 File Offset: 0x0019D825
			public override XmlDictionaryString IssueAction
			{
				get
				{
					return this.DriverDictionary.RequestSecurityContextIssuance;
				}
			}

			// Token: 0x17001A1B RID: 6683
			// (get) Token: 0x06006FE6 RID: 28646 RVA: 0x0019F632 File Offset: 0x0019D832
			public override XmlDictionaryString IssueResponseAction
			{
				get
				{
					return this.DriverDictionary.RequestSecurityContextIssuanceResponse;
				}
			}

			// Token: 0x17001A1C RID: 6684
			// (get) Token: 0x06006FE7 RID: 28647 RVA: 0x0019F63F File Offset: 0x0019D83F
			public override XmlDictionaryString RenewNeededFaultCode
			{
				get
				{
					return this.DriverDictionary.RenewNeededFaultCode;
				}
			}

			// Token: 0x17001A1D RID: 6685
			// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x0019F64C File Offset: 0x0019D84C
			public override XmlDictionaryString BadContextTokenFaultCode
			{
				get
				{
					return this.DriverDictionary.BadContextTokenFaultCode;
				}
			}

			// Token: 0x06006FE9 RID: 28649 RVA: 0x0019F65C File Offset: 0x0019D85C
			public override UniqueId GetSecurityContextTokenId(XmlDictionaryReader reader)
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				reader.ReadStartElement(this.DriverDictionary.SecurityContextToken, this.DriverDictionary.Namespace);
				UniqueId result = XmlHelper.ReadElementStringAsUniqueId(reader, this.DriverDictionary.Identifier, this.DriverDictionary.Namespace);
				while (reader.IsStartElement())
				{
					reader.Skip();
				}
				reader.ReadEndElement();
				return result;
			}

			// Token: 0x06006FEA RID: 28650 RVA: 0x0019F6CC File Offset: 0x0019D8CC
			public override bool IsAtSecurityContextToken(XmlDictionaryReader reader)
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				return reader.IsStartElement(this.DriverDictionary.SecurityContextToken, this.DriverDictionary.Namespace);
			}
		}
	}
}

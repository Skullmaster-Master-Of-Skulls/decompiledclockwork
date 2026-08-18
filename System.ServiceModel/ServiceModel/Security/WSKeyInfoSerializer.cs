using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000368 RID: 872
	internal class WSKeyInfoSerializer : KeyInfoSerializer
	{
		// Token: 0x06001FF2 RID: 8178 RVA: 0x0007794C File Offset: 0x00075B4C
		private static Func<KeyInfoSerializer, IEnumerable<SecurityTokenSerializer.SerializerEntries>> CreateAdditionalEntries(SecurityVersion securityVersion, SecureConversationVersion secureConversationVersion)
		{
			return delegate(KeyInfoSerializer keyInfoSerializer)
			{
				List<SecurityTokenSerializer.SerializerEntries> list = new List<SecurityTokenSerializer.SerializerEntries>();
				if (securityVersion == SecurityVersion.WSSecurity10)
				{
					list.Add(new WSSecurityJan2004(keyInfoSerializer));
				}
				else
				{
					if (securityVersion != SecurityVersion.WSSecurity11)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("securityVersion", SR.GetString("MessageSecurityVersionOutOfRange")));
					}
					list.Add(new WSSecurityXXX2005(keyInfoSerializer));
				}
				if (secureConversationVersion == SecureConversationVersion.WSSecureConversationFeb2005)
				{
					list.Add(new WSKeyInfoSerializer.WSSecureConversationFeb2005(keyInfoSerializer));
				}
				else
				{
					if (secureConversationVersion != SecureConversationVersion.WSSecureConversation13)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
					list.Add(new WSKeyInfoSerializer.WSSecureConversationDec2005(keyInfoSerializer));
				}
				return list;
			};
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00077979 File Offset: 0x00075B79
		public WSKeyInfoSerializer(bool emitBspRequiredAttributes, DictionaryManager dictionaryManager, TrustDictionary trustDictionary, SecurityTokenSerializer innerSecurityTokenSerializer, SecurityVersion securityVersion, SecureConversationVersion secureConversationVersion) : base(emitBspRequiredAttributes, dictionaryManager, trustDictionary, innerSecurityTokenSerializer, WSKeyInfoSerializer.CreateAdditionalEntries(securityVersion, secureConversationVersion))
		{
		}

		// Token: 0x02000B85 RID: 2949
		private abstract class WSSecureConversation : SecurityTokenSerializer.SerializerEntries
		{
			// Token: 0x060072F1 RID: 29425 RVA: 0x001AD2CB File Offset: 0x001AB4CB
			protected WSSecureConversation(KeyInfoSerializer securityTokenSerializer)
			{
				this.securityTokenSerializer = securityTokenSerializer;
			}

			// Token: 0x17001A98 RID: 6808
			// (get) Token: 0x060072F2 RID: 29426 RVA: 0x001AD2DA File Offset: 0x001AB4DA
			public KeyInfoSerializer SecurityTokenSerializer
			{
				get
				{
					return this.securityTokenSerializer;
				}
			}

			// Token: 0x17001A99 RID: 6809
			// (get) Token: 0x060072F3 RID: 29427
			public abstract SecureConversationDictionary SerializerDictionary { get; }

			// Token: 0x17001A9A RID: 6810
			// (get) Token: 0x060072F4 RID: 29428 RVA: 0x001AD2E2 File Offset: 0x001AB4E2
			public virtual string DerivationAlgorithm
			{
				get
				{
					return "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";
				}
			}

			// Token: 0x060072F5 RID: 29429 RVA: 0x001AD2E9 File Offset: 0x001AB4E9
			public override void PopulateTokenEntries(IList<SecurityTokenSerializer.TokenEntry> tokenEntryList)
			{
				if (tokenEntryList == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenEntryList");
				}
				tokenEntryList.Add(new WSKeyInfoSerializer.WSSecureConversation.DerivedKeyTokenEntry(this));
				tokenEntryList.Add(new WSKeyInfoSerializer.WSSecureConversation.SecurityContextTokenEntry(this));
			}

			// Token: 0x04004111 RID: 16657
			private KeyInfoSerializer securityTokenSerializer;

			// Token: 0x02000EF8 RID: 3832
			protected abstract class SctStrEntry : SecurityTokenSerializer.StrEntry
			{
				// Token: 0x06008558 RID: 34136 RVA: 0x001ED9EF File Offset: 0x001EBBEF
				public SctStrEntry(WSKeyInfoSerializer.WSSecureConversation parent)
				{
					this.parent = parent;
				}

				// Token: 0x17001D47 RID: 7495
				// (get) Token: 0x06008559 RID: 34137 RVA: 0x001ED9FE File Offset: 0x001EBBFE
				protected WSKeyInfoSerializer.WSSecureConversation Parent
				{
					get
					{
						return this.parent;
					}
				}

				// Token: 0x0600855A RID: 34138 RVA: 0x001EDA06 File Offset: 0x001EBC06
				public override Type GetTokenType(SecurityKeyIdentifierClause clause)
				{
					return null;
				}

				// Token: 0x0600855B RID: 34139 RVA: 0x001EDA09 File Offset: 0x001EBC09
				public override string GetTokenTypeUri()
				{
					return null;
				}

				// Token: 0x0600855C RID: 34140 RVA: 0x001EDA0C File Offset: 0x001EBC0C
				public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
				{
					if (tokenType != null && tokenType != this.parent.SerializerDictionary.SecurityContextTokenType.Value)
					{
						return false;
					}
					if (reader.IsStartElement(this.parent.SecurityTokenSerializer.DictionaryManager.SecurityJan2004Dictionary.Reference, this.parent.SecurityTokenSerializer.DictionaryManager.SecurityJan2004Dictionary.Namespace))
					{
						string attribute = reader.GetAttribute(this.parent.SecurityTokenSerializer.DictionaryManager.SecurityJan2004Dictionary.ValueType, null);
						if (attribute != null && attribute != this.parent.SerializerDictionary.SecurityContextTokenReferenceValueType.Value)
						{
							return false;
						}
						string attribute2 = reader.GetAttribute(this.parent.SecurityTokenSerializer.DictionaryManager.SecurityJan2004Dictionary.URI, null);
						if (attribute2 != null && attribute2.Length > 0 && attribute2[0] != '#')
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x0600855D RID: 34141 RVA: 0x001EDAF8 File Offset: 0x001EBCF8
				public override SecurityKeyIdentifierClause ReadClause(XmlDictionaryReader reader, byte[] derivationNonce, int derivationLength, string tokenType)
				{
					UniqueId attributeAsUniqueId = XmlHelper.GetAttributeAsUniqueId(reader, XD.SecurityJan2004Dictionary.URI, null);
					UniqueId generation = this.ReadGeneration(reader);
					if (reader.IsEmptyElement)
					{
						reader.Read();
					}
					else
					{
						reader.ReadStartElement();
						while (reader.IsStartElement())
						{
							reader.Skip();
						}
						reader.ReadEndElement();
					}
					return new SecurityContextKeyIdentifierClause(attributeAsUniqueId, generation, derivationNonce, derivationLength);
				}

				// Token: 0x0600855E RID: 34142
				protected abstract UniqueId ReadGeneration(XmlDictionaryReader reader);

				// Token: 0x0600855F RID: 34143 RVA: 0x001EDB55 File Offset: 0x001EBD55
				public override bool SupportsCore(SecurityKeyIdentifierClause clause)
				{
					return clause is SecurityContextKeyIdentifierClause;
				}

				// Token: 0x06008560 RID: 34144 RVA: 0x001EDB60 File Offset: 0x001EBD60
				public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
				{
					SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = clause as SecurityContextKeyIdentifierClause;
					writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.Reference, XD.SecurityJan2004Dictionary.Namespace);
					XmlHelper.WriteAttributeStringAsUniqueId(writer, null, XD.SecurityJan2004Dictionary.URI, null, securityContextKeyIdentifierClause.ContextId);
					this.WriteGeneration(writer, securityContextKeyIdentifierClause);
					writer.WriteAttributeString(XD.SecurityJan2004Dictionary.ValueType, null, this.parent.SerializerDictionary.SecurityContextTokenReferenceValueType.Value);
					writer.WriteEndElement();
				}

				// Token: 0x06008561 RID: 34145
				protected abstract void WriteGeneration(XmlDictionaryWriter writer, SecurityContextKeyIdentifierClause clause);

				// Token: 0x04004D3D RID: 19773
				private WSKeyInfoSerializer.WSSecureConversation parent;
			}

			// Token: 0x02000EF9 RID: 3833
			protected class SecurityContextTokenEntry : SecurityTokenSerializer.TokenEntry
			{
				// Token: 0x06008562 RID: 34146 RVA: 0x001EDBE9 File Offset: 0x001EBDE9
				public SecurityContextTokenEntry(WSKeyInfoSerializer.WSSecureConversation parent)
				{
					this.parent = parent;
				}

				// Token: 0x17001D48 RID: 7496
				// (get) Token: 0x06008563 RID: 34147 RVA: 0x001EDBF8 File Offset: 0x001EBDF8
				protected WSKeyInfoSerializer.WSSecureConversation Parent
				{
					get
					{
						return this.parent;
					}
				}

				// Token: 0x17001D49 RID: 7497
				// (get) Token: 0x06008564 RID: 34148 RVA: 0x001EDC00 File Offset: 0x001EBE00
				protected override XmlDictionaryString LocalName
				{
					get
					{
						return this.parent.SerializerDictionary.SecurityContextToken;
					}
				}

				// Token: 0x17001D4A RID: 7498
				// (get) Token: 0x06008565 RID: 34149 RVA: 0x001EDC12 File Offset: 0x001EBE12
				protected override XmlDictionaryString NamespaceUri
				{
					get
					{
						return this.parent.SerializerDictionary.Namespace;
					}
				}

				// Token: 0x06008566 RID: 34150 RVA: 0x001EDC24 File Offset: 0x001EBE24
				protected override Type[] GetTokenTypesCore()
				{
					if (this.tokenTypes == null)
					{
						this.tokenTypes = new Type[]
						{
							typeof(SecurityContextSecurityToken)
						};
					}
					return this.tokenTypes;
				}

				// Token: 0x17001D4B RID: 7499
				// (get) Token: 0x06008567 RID: 34151 RVA: 0x001EDC4D File Offset: 0x001EBE4D
				public override string TokenTypeUri
				{
					get
					{
						return this.parent.SerializerDictionary.SecurityContextTokenType.Value;
					}
				}

				// Token: 0x17001D4C RID: 7500
				// (get) Token: 0x06008568 RID: 34152 RVA: 0x001EDC64 File Offset: 0x001EBE64
				protected override string ValueTypeUri
				{
					get
					{
						return null;
					}
				}

				// Token: 0x04004D3E RID: 19774
				private WSKeyInfoSerializer.WSSecureConversation parent;

				// Token: 0x04004D3F RID: 19775
				private Type[] tokenTypes;
			}

			// Token: 0x02000EFA RID: 3834
			protected class DerivedKeyTokenEntry : SecurityTokenSerializer.TokenEntry
			{
				// Token: 0x06008569 RID: 34153 RVA: 0x001EDC67 File Offset: 0x001EBE67
				public DerivedKeyTokenEntry(WSKeyInfoSerializer.WSSecureConversation parent)
				{
					if (parent == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
					}
					this.parent = parent;
				}

				// Token: 0x17001D4D RID: 7501
				// (get) Token: 0x0600856A RID: 34154 RVA: 0x001EDC89 File Offset: 0x001EBE89
				protected override XmlDictionaryString LocalName
				{
					get
					{
						return this.parent.SerializerDictionary.DerivedKeyToken;
					}
				}

				// Token: 0x17001D4E RID: 7502
				// (get) Token: 0x0600856B RID: 34155 RVA: 0x001EDC9B File Offset: 0x001EBE9B
				protected override XmlDictionaryString NamespaceUri
				{
					get
					{
						return this.parent.SerializerDictionary.Namespace;
					}
				}

				// Token: 0x0600856C RID: 34156 RVA: 0x001EDCAD File Offset: 0x001EBEAD
				protected override Type[] GetTokenTypesCore()
				{
					if (this.tokenTypes == null)
					{
						this.tokenTypes = new Type[]
						{
							typeof(DerivedKeySecurityToken)
						};
					}
					return this.tokenTypes;
				}

				// Token: 0x17001D4F RID: 7503
				// (get) Token: 0x0600856D RID: 34157 RVA: 0x001EDCD6 File Offset: 0x001EBED6
				public override string TokenTypeUri
				{
					get
					{
						return this.parent.SerializerDictionary.DerivedKeyTokenType.Value;
					}
				}

				// Token: 0x17001D50 RID: 7504
				// (get) Token: 0x0600856E RID: 34158 RVA: 0x001EDCED File Offset: 0x001EBEED
				protected override string ValueTypeUri
				{
					get
					{
						return null;
					}
				}

				// Token: 0x04004D40 RID: 19776
				public const string DefaultLabel = "WS-SecureConversation";

				// Token: 0x04004D41 RID: 19777
				private WSKeyInfoSerializer.WSSecureConversation parent;

				// Token: 0x04004D42 RID: 19778
				private Type[] tokenTypes;
			}
		}

		// Token: 0x02000B86 RID: 2950
		private class WSSecureConversationFeb2005 : WSKeyInfoSerializer.WSSecureConversation
		{
			// Token: 0x060072F6 RID: 29430 RVA: 0x001AD316 File Offset: 0x001AB516
			public WSSecureConversationFeb2005(KeyInfoSerializer securityTokenSerializer) : base(securityTokenSerializer)
			{
			}

			// Token: 0x17001A9B RID: 6811
			// (get) Token: 0x060072F7 RID: 29431 RVA: 0x001AD31F File Offset: 0x001AB51F
			public override SecureConversationDictionary SerializerDictionary
			{
				get
				{
					return base.SecurityTokenSerializer.DictionaryManager.SecureConversationFeb2005Dictionary;
				}
			}

			// Token: 0x060072F8 RID: 29432 RVA: 0x001AD331 File Offset: 0x001AB531
			public override void PopulateStrEntries(IList<SecurityTokenSerializer.StrEntry> strEntries)
			{
				strEntries.Add(new WSKeyInfoSerializer.WSSecureConversationFeb2005.SctStrEntryFeb2005(this));
			}

			// Token: 0x02000EFB RID: 3835
			private class SctStrEntryFeb2005 : WSKeyInfoSerializer.WSSecureConversation.SctStrEntry
			{
				// Token: 0x0600856F RID: 34159 RVA: 0x001EDCF0 File Offset: 0x001EBEF0
				public SctStrEntryFeb2005(WSKeyInfoSerializer.WSSecureConversationFeb2005 parent) : base(parent)
				{
				}

				// Token: 0x06008570 RID: 34160 RVA: 0x001EDCF9 File Offset: 0x001EBEF9
				protected override UniqueId ReadGeneration(XmlDictionaryReader reader)
				{
					return XmlHelper.GetAttributeAsUniqueId(reader, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary.Instance, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationFeb2005Dictionary.Namespace);
				}

				// Token: 0x06008571 RID: 34161 RVA: 0x001EDD38 File Offset: 0x001EBF38
				protected override void WriteGeneration(XmlDictionaryWriter writer, SecurityContextKeyIdentifierClause clause)
				{
					if (clause.Generation != null)
					{
						XmlHelper.WriteAttributeStringAsUniqueId(writer, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationFeb2005Dictionary.Prefix.Value, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary.Instance, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationFeb2005Dictionary.Namespace, clause.Generation);
					}
				}
			}
		}

		// Token: 0x02000B87 RID: 2951
		private class WSSecureConversationDec2005 : WSKeyInfoSerializer.WSSecureConversation
		{
			// Token: 0x060072F9 RID: 29433 RVA: 0x001AD33F File Offset: 0x001AB53F
			public WSSecureConversationDec2005(KeyInfoSerializer securityTokenSerializer) : base(securityTokenSerializer)
			{
			}

			// Token: 0x17001A9C RID: 6812
			// (get) Token: 0x060072FA RID: 29434 RVA: 0x001AD348 File Offset: 0x001AB548
			public override SecureConversationDictionary SerializerDictionary
			{
				get
				{
					return base.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary;
				}
			}

			// Token: 0x060072FB RID: 29435 RVA: 0x001AD35A File Offset: 0x001AB55A
			public override void PopulateStrEntries(IList<SecurityTokenSerializer.StrEntry> strEntries)
			{
				strEntries.Add(new WSKeyInfoSerializer.WSSecureConversationDec2005.SctStrEntryDec2005(this));
			}

			// Token: 0x17001A9D RID: 6813
			// (get) Token: 0x060072FC RID: 29436 RVA: 0x001AD368 File Offset: 0x001AB568
			public override string DerivationAlgorithm
			{
				get
				{
					return "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";
				}
			}

			// Token: 0x02000EFC RID: 3836
			private class SctStrEntryDec2005 : WSKeyInfoSerializer.WSSecureConversation.SctStrEntry
			{
				// Token: 0x06008572 RID: 34162 RVA: 0x001EDDB2 File Offset: 0x001EBFB2
				public SctStrEntryDec2005(WSKeyInfoSerializer.WSSecureConversationDec2005 parent) : base(parent)
				{
				}

				// Token: 0x06008573 RID: 34163 RVA: 0x001EDDBB File Offset: 0x001EBFBB
				protected override UniqueId ReadGeneration(XmlDictionaryReader reader)
				{
					return XmlHelper.GetAttributeAsUniqueId(reader, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary.Instance, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary.Namespace);
				}

				// Token: 0x06008574 RID: 34164 RVA: 0x001EDDF8 File Offset: 0x001EBFF8
				protected override void WriteGeneration(XmlDictionaryWriter writer, SecurityContextKeyIdentifierClause clause)
				{
					if (clause.Generation != null)
					{
						XmlHelper.WriteAttributeStringAsUniqueId(writer, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary.Prefix.Value, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary.Instance, base.Parent.SecurityTokenSerializer.DictionaryManager.SecureConversationDec2005Dictionary.Namespace, clause.Generation);
					}
				}
			}
		}
	}
}

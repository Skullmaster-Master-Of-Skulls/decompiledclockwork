using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200018C RID: 396
	internal class WSSecurityJan2004 : SecurityTokenSerializer.SerializerEntries
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x0003C0A5 File Offset: 0x0003A2A5
		public WSSecurityJan2004(KeyInfoSerializer securityTokenSerializer)
		{
			this.securityTokenSerializer = securityTokenSerializer;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0003C0B4 File Offset: 0x0003A2B4
		public KeyInfoSerializer SecurityTokenSerializer
		{
			get
			{
				return this.securityTokenSerializer;
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0003C0BC File Offset: 0x0003A2BC
		public override void PopulateKeyIdentifierClauseEntries(IList<SecurityTokenSerializer.KeyIdentifierClauseEntry> clauseEntries)
		{
			List<SecurityTokenSerializer.StrEntry> strEntries = new List<SecurityTokenSerializer.StrEntry>();
			this.securityTokenSerializer.PopulateStrEntries(strEntries);
			WSSecurityJan2004.SecurityTokenReferenceJan2004ClauseEntry item = new WSSecurityJan2004.SecurityTokenReferenceJan2004ClauseEntry(this.securityTokenSerializer.EmitBspRequiredAttributes, strEntries);
			clauseEntries.Add(item);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003C0F4 File Offset: 0x0003A2F4
		protected void PopulateJan2004StrEntries(IList<SecurityTokenSerializer.StrEntry> strEntries)
		{
			strEntries.Add(new WSSecurityJan2004.LocalReferenceStrEntry(this.securityTokenSerializer.EmitBspRequiredAttributes, this.securityTokenSerializer));
			strEntries.Add(new WSSecurityJan2004.KerberosHashStrEntry(this.securityTokenSerializer.EmitBspRequiredAttributes));
			strEntries.Add(new WSSecurityJan2004.X509SkiStrEntry(this.securityTokenSerializer.EmitBspRequiredAttributes));
			strEntries.Add(new WSSecurityJan2004.X509IssuerSerialStrEntry());
			strEntries.Add(new WSSecurityJan2004.RelDirectStrEntry());
			strEntries.Add(new WSSecurityJan2004.SamlJan2004KeyIdentifierStrEntry());
			strEntries.Add(new WSSecurityJan2004.Saml2Jan2004KeyIdentifierStrEntry());
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0003C175 File Offset: 0x0003A375
		public override void PopulateStrEntries(IList<SecurityTokenSerializer.StrEntry> strEntries)
		{
			this.PopulateJan2004StrEntries(strEntries);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003C17E File Offset: 0x0003A37E
		protected void PopulateJan2004TokenEntries(IList<SecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			tokenEntryList.Add(new WSSecurityJan2004.GenericXmlTokenEntry());
			tokenEntryList.Add(new WSSecurityJan2004.UserNamePasswordTokenEntry());
			tokenEntryList.Add(new WSSecurityJan2004.KerberosTokenEntry());
			tokenEntryList.Add(new WSSecurityJan2004.X509TokenEntry());
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		public override void PopulateTokenEntries(IList<SecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			this.PopulateJan2004TokenEntries(tokenEntryList);
			tokenEntryList.Add(new WSSecurityJan2004.SamlTokenEntry());
			tokenEntryList.Add(new WSSecurityJan2004.WrappedKeyTokenEntry());
		}

		// Token: 0x04000CA4 RID: 3236
		private KeyInfoSerializer securityTokenSerializer;

		// Token: 0x02000273 RID: 627
		internal abstract class BinaryTokenEntry : SecurityTokenSerializer.TokenEntry
		{
			// Token: 0x0600129D RID: 4765 RVA: 0x00050834 File Offset: 0x0004EA34
			protected BinaryTokenEntry(string valueTypeUri)
			{
				this.valueTypeUris = new string[1];
				this.valueTypeUris[0] = valueTypeUri;
			}

			// Token: 0x0600129E RID: 4766 RVA: 0x00050854 File Offset: 0x0004EA54
			protected BinaryTokenEntry(string[] valueTypeUris)
			{
				if (valueTypeUris == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("valueTypeUris");
				}
				this.valueTypeUris = new string[valueTypeUris.GetLength(0)];
				for (int i = 0; i < this.valueTypeUris.GetLength(0); i++)
				{
					this.valueTypeUris[i] = valueTypeUris[i];
				}
			}

			// Token: 0x1700052F RID: 1327
			// (get) Token: 0x0600129F RID: 4767 RVA: 0x000508AE File Offset: 0x0004EAAE
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return WSSecurityJan2004.BinaryTokenEntry.ElementName;
				}
			}

			// Token: 0x17000530 RID: 1328
			// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000508B5 File Offset: 0x0004EAB5
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.SecurityJan2004Dictionary.Namespace;
				}
			}

			// Token: 0x17000531 RID: 1329
			// (get) Token: 0x060012A1 RID: 4769 RVA: 0x000508C1 File Offset: 0x0004EAC1
			public override string TokenTypeUri
			{
				get
				{
					return this.valueTypeUris[0];
				}
			}

			// Token: 0x17000532 RID: 1330
			// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000508C1 File Offset: 0x0004EAC1
			protected override string ValueTypeUri
			{
				get
				{
					return this.valueTypeUris[0];
				}
			}

			// Token: 0x060012A3 RID: 4771 RVA: 0x000508CC File Offset: 0x0004EACC
			public override bool SupportsTokenTypeUri(string tokenTypeUri)
			{
				for (int i = 0; i < this.valueTypeUris.GetLength(0); i++)
				{
					if (this.valueTypeUris[i] == tokenTypeUri)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04001118 RID: 4376
			internal static readonly XmlDictionaryString ElementName = XD.SecurityJan2004Dictionary.BinarySecurityToken;

			// Token: 0x04001119 RID: 4377
			internal static readonly XmlDictionaryString EncodingTypeAttribute = XD.SecurityJan2004Dictionary.EncodingType;

			// Token: 0x0400111A RID: 4378
			internal const string EncodingTypeAttributeString = "EncodingType";

			// Token: 0x0400111B RID: 4379
			internal const string EncodingTypeValueBase64Binary = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

			// Token: 0x0400111C RID: 4380
			internal const string EncodingTypeValueHexBinary = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

			// Token: 0x0400111D RID: 4381
			internal static readonly XmlDictionaryString ValueTypeAttribute = XD.SecurityJan2004Dictionary.ValueType;

			// Token: 0x0400111E RID: 4382
			private string[] valueTypeUris;
		}

		// Token: 0x02000274 RID: 628
		private class GenericXmlTokenEntry : SecurityTokenSerializer.TokenEntry
		{
			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00003459 File Offset: 0x00001659
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00003459 File Offset: 0x00001659
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060012A7 RID: 4775 RVA: 0x00050932 File Offset: 0x0004EB32
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(GenericXmlSecurityToken)
				};
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00003459 File Offset: 0x00001659
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00003459 File Offset: 0x00001659
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}
		}

		// Token: 0x02000275 RID: 629
		private class KerberosTokenEntry : WSSecurityJan2004.BinaryTokenEntry
		{
			// Token: 0x060012AB RID: 4779 RVA: 0x0005094F File Offset: 0x0004EB4F
			public KerberosTokenEntry() : base(new string[]
			{
				"http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ",
				"http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510"
			})
			{
			}

			// Token: 0x060012AC RID: 4780 RVA: 0x0005096D File Offset: 0x0004EB6D
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(KerberosReceiverSecurityToken),
					typeof(KerberosRequestorSecurityToken)
				};
			}
		}

		// Token: 0x02000276 RID: 630
		protected class SamlTokenEntry : SecurityTokenSerializer.TokenEntry
		{
			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x060012AD RID: 4781 RVA: 0x0005098F File Offset: 0x0004EB8F
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.SecurityJan2004Dictionary.SamlAssertion;
				}
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x060012AE RID: 4782 RVA: 0x0005099B File Offset: 0x0004EB9B
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.SecurityJan2004Dictionary.SamlUri;
				}
			}

			// Token: 0x060012AF RID: 4783 RVA: 0x000509A7 File Offset: 0x0004EBA7
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(SamlSecurityToken)
				};
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00003459 File Offset: 0x00001659
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00003459 File Offset: 0x00001659
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}
		}

		// Token: 0x02000277 RID: 631
		private class UserNamePasswordTokenEntry : SecurityTokenSerializer.TokenEntry
		{
			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000509BC File Offset: 0x0004EBBC
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.SecurityJan2004Dictionary.UserNameTokenElement;
				}
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000508B5 File Offset: 0x0004EAB5
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.SecurityJan2004Dictionary.Namespace;
				}
			}

			// Token: 0x060012B5 RID: 4789 RVA: 0x000509C8 File Offset: 0x0004EBC8
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(UserNameSecurityToken)
				};
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000509DD File Offset: 0x0004EBDD
			public override string TokenTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#UsernameToken";
				}
			}

			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00003459 File Offset: 0x00001659
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}
		}

		// Token: 0x02000278 RID: 632
		protected class WrappedKeyTokenEntry : SecurityTokenSerializer.TokenEntry
		{
			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000024BA File Offset: 0x000006BA
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return EncryptedKey.ElementName;
				}
			}

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x060012BA RID: 4794 RVA: 0x000509E4 File Offset: 0x0004EBE4
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.XmlEncryptionDictionary.Namespace;
				}
			}

			// Token: 0x060012BB RID: 4795 RVA: 0x000509F0 File Offset: 0x0004EBF0
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(WrappedKeySecurityToken)
				};
			}

			// Token: 0x17000541 RID: 1345
			// (get) Token: 0x060012BC RID: 4796 RVA: 0x00003459 File Offset: 0x00001659
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000542 RID: 1346
			// (get) Token: 0x060012BD RID: 4797 RVA: 0x00003459 File Offset: 0x00001659
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}
		}

		// Token: 0x02000279 RID: 633
		protected class X509TokenEntry : WSSecurityJan2004.BinaryTokenEntry
		{
			// Token: 0x060012BF RID: 4799 RVA: 0x00050A05 File Offset: 0x0004EC05
			public X509TokenEntry() : base("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3")
			{
			}

			// Token: 0x060012C0 RID: 4800 RVA: 0x00050A12 File Offset: 0x0004EC12
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(X509SecurityToken),
					typeof(X509WindowsSecurityToken)
				};
			}

			// Token: 0x0400111F RID: 4383
			internal const string ValueTypeAbsoluteUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3";
		}

		// Token: 0x0200027A RID: 634
		protected class SecurityTokenReferenceJan2004ClauseEntry : SecurityTokenSerializer.KeyIdentifierClauseEntry
		{
			// Token: 0x060012C1 RID: 4801 RVA: 0x00050A34 File Offset: 0x0004EC34
			public SecurityTokenReferenceJan2004ClauseEntry(bool emitBspRequiredAttributes, IList<SecurityTokenSerializer.StrEntry> strEntries)
			{
				this.emitBspRequiredAttributes = emitBspRequiredAttributes;
				this.strEntries = strEntries;
			}

			// Token: 0x17000543 RID: 1347
			// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00050A4A File Offset: 0x0004EC4A
			protected bool EmitBspRequiredAttributes
			{
				get
				{
					return this.emitBspRequiredAttributes;
				}
			}

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00050A52 File Offset: 0x0004EC52
			protected IList<SecurityTokenSerializer.StrEntry> StrEntries
			{
				get
				{
					return this.strEntries;
				}
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00050A5A File Offset: 0x0004EC5A
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.SecurityJan2004Dictionary.SecurityTokenReference;
				}
			}

			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000508B5 File Offset: 0x0004EAB5
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.SecurityJan2004Dictionary.Namespace;
				}
			}

			// Token: 0x060012C6 RID: 4806 RVA: 0x00003459 File Offset: 0x00001659
			protected virtual string ReadTokenType(XmlDictionaryReader reader)
			{
				return null;
			}

			// Token: 0x060012C7 RID: 4807 RVA: 0x00050A68 File Offset: 0x0004EC68
			public override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				byte[] derivationNonce = null;
				int derivationLength = 0;
				if (reader.IsStartElement(XD.SecurityJan2004Dictionary.SecurityTokenReference, this.NamespaceUri))
				{
					string attribute = reader.GetAttribute(XD.SecureConversationFeb2005Dictionary.Nonce, XD.SecureConversationFeb2005Dictionary.Namespace);
					if (attribute != null)
					{
						derivationNonce = Convert.FromBase64String(attribute);
					}
					string attribute2 = reader.GetAttribute(XD.SecureConversationFeb2005Dictionary.Length, XD.SecureConversationFeb2005Dictionary.Namespace);
					if (attribute2 != null)
					{
						derivationLength = Convert.ToInt32(attribute2, CultureInfo.InvariantCulture);
					}
					else
					{
						derivationLength = 32;
					}
				}
				string tokenType = this.ReadTokenType(reader);
				string attribute3 = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				reader.ReadStartElement(XD.SecurityJan2004Dictionary.SecurityTokenReference, this.NamespaceUri);
				SecurityKeyIdentifierClause securityKeyIdentifierClause = null;
				for (int i = 0; i < this.strEntries.Count; i++)
				{
					if (this.strEntries[i].CanReadClause(reader, tokenType))
					{
						securityKeyIdentifierClause = this.strEntries[i].ReadClause(reader, derivationNonce, derivationLength, tokenType);
						break;
					}
				}
				if (securityKeyIdentifierClause == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CannotReadKeyIdentifierClause", new object[]
					{
						reader.LocalName,
						reader.NamespaceURI
					})));
				}
				if (!string.IsNullOrEmpty(attribute3))
				{
					securityKeyIdentifierClause.Id = attribute3;
				}
				reader.ReadEndElement();
				return securityKeyIdentifierClause;
			}

			// Token: 0x060012C8 RID: 4808 RVA: 0x00050BC0 File Offset: 0x0004EDC0
			public override bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				for (int i = 0; i < this.strEntries.Count; i++)
				{
					if (this.strEntries[i].SupportsCore(keyIdentifierClause))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060012C9 RID: 4809 RVA: 0x00050BFC File Offset: 0x0004EDFC
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				for (int i = 0; i < this.strEntries.Count; i++)
				{
					if (this.strEntries[i].SupportsCore(keyIdentifierClause))
					{
						writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.SecurityTokenReference, XD.SecurityJan2004Dictionary.Namespace);
						this.strEntries[i].WriteContent(writer, keyIdentifierClause);
						writer.WriteEndElement();
						return;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StandardsManagerCannotWriteObject", new object[]
				{
					keyIdentifierClause.GetType()
				})));
			}

			// Token: 0x04001120 RID: 4384
			private const int DefaultDerivedKeyLength = 32;

			// Token: 0x04001121 RID: 4385
			private bool emitBspRequiredAttributes;

			// Token: 0x04001122 RID: 4386
			private IList<SecurityTokenSerializer.StrEntry> strEntries;
		}

		// Token: 0x0200027B RID: 635
		protected abstract class KeyIdentifierStrEntry : SecurityTokenSerializer.StrEntry
		{
			// Token: 0x17000547 RID: 1351
			// (get) Token: 0x060012CA RID: 4810
			protected abstract Type ClauseType { get; }

			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x060012CB RID: 4811 RVA: 0x00050C9E File Offset: 0x0004EE9E
			protected virtual string DefaultEncodingType
			{
				get
				{
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";
				}
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x060012CC RID: 4812
			public abstract Type TokenType { get; }

			// Token: 0x1700054A RID: 1354
			// (get) Token: 0x060012CD RID: 4813
			protected abstract string ValueTypeUri { get; }

			// Token: 0x1700054B RID: 1355
			// (get) Token: 0x060012CE RID: 4814 RVA: 0x00050CA5 File Offset: 0x0004EEA5
			protected bool EmitBspRequiredAttributes
			{
				get
				{
					return this.emitBspRequiredAttributes;
				}
			}

			// Token: 0x060012CF RID: 4815 RVA: 0x00050CAD File Offset: 0x0004EEAD
			protected KeyIdentifierStrEntry(bool emitBspRequiredAttributes)
			{
				this.emitBspRequiredAttributes = emitBspRequiredAttributes;
			}

			// Token: 0x060012D0 RID: 4816 RVA: 0x00050CBC File Offset: 0x0004EEBC
			public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
			{
				if (reader.IsStartElement(XD.SecurityJan2004Dictionary.KeyIdentifier, XD.SecurityJan2004Dictionary.Namespace))
				{
					string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null);
					return this.ValueTypeUri == attribute;
				}
				return false;
			}

			// Token: 0x060012D1 RID: 4817
			protected abstract SecurityKeyIdentifierClause CreateClause(byte[] bytes, byte[] derivationNonce, int derivationLength);

			// Token: 0x060012D2 RID: 4818 RVA: 0x00050D05 File Offset: 0x0004EF05
			public override Type GetTokenType(SecurityKeyIdentifierClause clause)
			{
				return this.TokenType;
			}

			// Token: 0x060012D3 RID: 4819 RVA: 0x00050D10 File Offset: 0x0004EF10
			public override SecurityKeyIdentifierClause ReadClause(XmlDictionaryReader reader, byte[] derivationNonce, int derivationLength, string tokenType)
			{
				string text = reader.GetAttribute(XD.SecurityJan2004Dictionary.EncodingType, null);
				if (text == null)
				{
					text = this.DefaultEncodingType;
				}
				reader.ReadStartElement();
				byte[] bytes;
				if (text == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary")
				{
					bytes = reader.ReadContentAsBase64();
				}
				else if (text == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary")
				{
					bytes = SoapHexBinary.Parse(reader.ReadContentAsString()).Value;
				}
				else
				{
					if (!(text == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("UnknownEncodingInKeyIdentifier")));
					}
					bytes = new UTF8Encoding().GetBytes(reader.ReadContentAsString());
				}
				reader.ReadEndElement();
				return this.CreateClause(bytes, derivationNonce, derivationLength);
			}

			// Token: 0x060012D4 RID: 4820 RVA: 0x00050DBE File Offset: 0x0004EFBE
			public override bool SupportsCore(SecurityKeyIdentifierClause clause)
			{
				return this.ClauseType.IsAssignableFrom(clause.GetType());
			}

			// Token: 0x060012D5 RID: 4821 RVA: 0x00050DD4 File Offset: 0x0004EFD4
			public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
			{
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.KeyIdentifier, XD.SecurityJan2004Dictionary.Namespace);
				writer.WriteAttributeString(XD.SecurityJan2004Dictionary.ValueType, null, this.ValueTypeUri);
				if (this.emitBspRequiredAttributes)
				{
					writer.WriteAttributeString(XD.SecurityJan2004Dictionary.EncodingType, null, this.DefaultEncodingType);
				}
				string defaultEncodingType = this.DefaultEncodingType;
				BinaryKeyIdentifierClause binaryKeyIdentifierClause = clause as BinaryKeyIdentifierClause;
				byte[] buffer = binaryKeyIdentifierClause.GetBuffer();
				if (defaultEncodingType == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary")
				{
					writer.WriteBase64(buffer, 0, buffer.Length);
				}
				else if (defaultEncodingType == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary")
				{
					writer.WriteBinHex(buffer, 0, buffer.Length);
				}
				else if (defaultEncodingType == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text")
				{
					writer.WriteString(new UTF8Encoding().GetString(buffer, 0, buffer.Length));
				}
				writer.WriteEndElement();
			}

			// Token: 0x04001123 RID: 4387
			private bool emitBspRequiredAttributes;

			// Token: 0x04001124 RID: 4388
			protected const string EncodingTypeValueBase64Binary = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

			// Token: 0x04001125 RID: 4389
			protected const string EncodingTypeValueHexBinary = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

			// Token: 0x04001126 RID: 4390
			protected const string EncodingTypeValueText = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text";
		}

		// Token: 0x0200027C RID: 636
		protected class KerberosHashStrEntry : WSSecurityJan2004.KeyIdentifierStrEntry
		{
			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00050EB1 File Offset: 0x0004F0B1
			protected override Type ClauseType
			{
				get
				{
					return typeof(KerberosTicketHashKeyIdentifierClause);
				}
			}

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00050EBD File Offset: 0x0004F0BD
			public override Type TokenType
			{
				get
				{
					return typeof(KerberosRequestorSecurityToken);
				}
			}

			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00050EC9 File Offset: 0x0004F0C9
			protected override string ValueTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1";
				}
			}

			// Token: 0x060012D9 RID: 4825 RVA: 0x00050ED0 File Offset: 0x0004F0D0
			public KerberosHashStrEntry(bool emitBspRequiredAttributes) : base(emitBspRequiredAttributes)
			{
			}

			// Token: 0x060012DA RID: 4826 RVA: 0x00050ED9 File Offset: 0x0004F0D9
			protected override SecurityKeyIdentifierClause CreateClause(byte[] bytes, byte[] derivationNonce, int derivationLength)
			{
				return new KerberosTicketHashKeyIdentifierClause(bytes, derivationNonce, derivationLength);
			}

			// Token: 0x060012DB RID: 4827 RVA: 0x00050EE3 File Offset: 0x0004F0E3
			public override string GetTokenTypeUri()
			{
				return XD.SecurityJan2004Dictionary.KerberosTokenTypeGSS.Value;
			}

			// Token: 0x060012DC RID: 4828 RVA: 0x00050EF4 File Offset: 0x0004F0F4
			public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
			{
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.KeyIdentifier, XD.SecurityJan2004Dictionary.Namespace);
				writer.WriteAttributeString(XD.SecurityJan2004Dictionary.ValueType, null, this.ValueTypeUri);
				KerberosTicketHashKeyIdentifierClause kerberosTicketHashKeyIdentifierClause = clause as KerberosTicketHashKeyIdentifierClause;
				if (base.EmitBspRequiredAttributes)
				{
					writer.WriteAttributeString(XD.SecurityJan2004Dictionary.EncodingType, null, this.DefaultEncodingType);
				}
				string defaultEncodingType = this.DefaultEncodingType;
				byte[] buffer = kerberosTicketHashKeyIdentifierClause.GetBuffer();
				if (defaultEncodingType == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary")
				{
					writer.WriteBase64(buffer, 0, buffer.Length);
				}
				else if (defaultEncodingType == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary")
				{
					writer.WriteBinHex(buffer, 0, buffer.Length);
				}
				else if (defaultEncodingType == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text")
				{
					writer.WriteString(new UTF8Encoding().GetString(buffer, 0, buffer.Length));
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x0200027D RID: 637
		protected class X509SkiStrEntry : WSSecurityJan2004.KeyIdentifierStrEntry
		{
			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x060012DD RID: 4829 RVA: 0x00050FD1 File Offset: 0x0004F1D1
			protected override Type ClauseType
			{
				get
				{
					return typeof(X509SubjectKeyIdentifierClause);
				}
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x060012DE RID: 4830 RVA: 0x0003E334 File Offset: 0x0003C534
			public override Type TokenType
			{
				get
				{
					return typeof(X509SecurityToken);
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x060012DF RID: 4831 RVA: 0x00050FDD File Offset: 0x0004F1DD
			protected override string ValueTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier";
				}
			}

			// Token: 0x060012E0 RID: 4832 RVA: 0x00050ED0 File Offset: 0x0004F0D0
			public X509SkiStrEntry(bool emitBspRequiredAttributes) : base(emitBspRequiredAttributes)
			{
			}

			// Token: 0x060012E1 RID: 4833 RVA: 0x00050FE4 File Offset: 0x0004F1E4
			protected override SecurityKeyIdentifierClause CreateClause(byte[] bytes, byte[] derivationNonce, int derivationLength)
			{
				return new X509SubjectKeyIdentifierClause(bytes);
			}

			// Token: 0x060012E2 RID: 4834 RVA: 0x00050FEC File Offset: 0x0004F1EC
			public override string GetTokenTypeUri()
			{
				return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3";
			}
		}

		// Token: 0x0200027E RID: 638
		protected class LocalReferenceStrEntry : SecurityTokenSerializer.StrEntry
		{
			// Token: 0x060012E3 RID: 4835 RVA: 0x00050FF3 File Offset: 0x0004F1F3
			public LocalReferenceStrEntry(bool emitBspRequiredAttributes, KeyInfoSerializer tokenSerializer)
			{
				this.emitBspRequiredAttributes = emitBspRequiredAttributes;
				this.tokenSerializer = tokenSerializer;
			}

			// Token: 0x060012E4 RID: 4836 RVA: 0x0005100C File Offset: 0x0004F20C
			public override Type GetTokenType(SecurityKeyIdentifierClause clause)
			{
				LocalIdKeyIdentifierClause localIdKeyIdentifierClause = clause as LocalIdKeyIdentifierClause;
				return localIdKeyIdentifierClause.OwnerType;
			}

			// Token: 0x060012E5 RID: 4837 RVA: 0x00051028 File Offset: 0x0004F228
			public string GetLocalTokenTypeUri(SecurityKeyIdentifierClause clause)
			{
				Type tokenType = this.GetTokenType(clause);
				return this.tokenSerializer.GetTokenTypeUri(tokenType);
			}

			// Token: 0x060012E6 RID: 4838 RVA: 0x00003459 File Offset: 0x00001659
			public override string GetTokenTypeUri()
			{
				return null;
			}

			// Token: 0x060012E7 RID: 4839 RVA: 0x0005104C File Offset: 0x0004F24C
			public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
			{
				if (reader.IsStartElement(XD.SecurityJan2004Dictionary.Reference, XD.SecurityJan2004Dictionary.Namespace))
				{
					string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.URI, null);
					if (attribute != null && attribute.Length > 0 && attribute[0] == '#')
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060012E8 RID: 4840 RVA: 0x000510A4 File Offset: 0x0004F2A4
			public override SecurityKeyIdentifierClause ReadClause(XmlDictionaryReader reader, byte[] derivationNonce, int derivationLength, string tokenType)
			{
				string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.URI, null);
				string attribute2 = reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null);
				Type[] ownerTypes = null;
				if (attribute2 != null)
				{
					ownerTypes = this.tokenSerializer.GetTokenTypes(attribute2);
				}
				SecurityKeyIdentifierClause result = new LocalIdKeyIdentifierClause(attribute.Substring(1), derivationNonce, derivationLength, ownerTypes);
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadStartElement();
					reader.ReadEndElement();
				}
				return result;
			}

			// Token: 0x060012E9 RID: 4841 RVA: 0x00051115 File Offset: 0x0004F315
			public override bool SupportsCore(SecurityKeyIdentifierClause clause)
			{
				return clause is LocalIdKeyIdentifierClause;
			}

			// Token: 0x060012EA RID: 4842 RVA: 0x00051120 File Offset: 0x0004F320
			public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
			{
				LocalIdKeyIdentifierClause localIdKeyIdentifierClause = clause as LocalIdKeyIdentifierClause;
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.Reference, XD.SecurityJan2004Dictionary.Namespace);
				if (this.emitBspRequiredAttributes)
				{
					string localTokenTypeUri = this.GetLocalTokenTypeUri(localIdKeyIdentifierClause);
					if (localTokenTypeUri != null)
					{
						writer.WriteAttributeString(XD.SecurityJan2004Dictionary.ValueType, null, localTokenTypeUri);
					}
				}
				writer.WriteAttributeString(XD.SecurityJan2004Dictionary.URI, null, "#" + localIdKeyIdentifierClause.LocalId);
				writer.WriteEndElement();
			}

			// Token: 0x04001127 RID: 4391
			private bool emitBspRequiredAttributes;

			// Token: 0x04001128 RID: 4392
			private KeyInfoSerializer tokenSerializer;
		}

		// Token: 0x0200027F RID: 639
		protected class SamlJan2004KeyIdentifierStrEntry : SecurityTokenSerializer.StrEntry
		{
			// Token: 0x060012EB RID: 4843 RVA: 0x000511A9 File Offset: 0x0004F3A9
			protected virtual bool IsMatchingValueType(string valueType)
			{
				return valueType == "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID";
			}

			// Token: 0x060012EC RID: 4844 RVA: 0x000511B8 File Offset: 0x0004F3B8
			public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
			{
				if (reader.IsStartElement(XD.SamlDictionary.AuthorityBinding, XD.SecurityJan2004Dictionary.SamlUri))
				{
					return true;
				}
				if (reader.IsStartElement(XD.SecurityJan2004Dictionary.KeyIdentifier, XD.SecurityJan2004Dictionary.Namespace))
				{
					string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null);
					return this.IsMatchingValueType(attribute);
				}
				return false;
			}

			// Token: 0x060012ED RID: 4845 RVA: 0x00034A00 File Offset: 0x00032C00
			public override Type GetTokenType(SecurityKeyIdentifierClause clause)
			{
				return typeof(SamlSecurityToken);
			}

			// Token: 0x060012EE RID: 4846 RVA: 0x0005121A File Offset: 0x0004F41A
			public override string GetTokenTypeUri()
			{
				return XD.SecurityXXX2005Dictionary.SamlTokenType.Value;
			}

			// Token: 0x060012EF RID: 4847 RVA: 0x0005122C File Offset: 0x0004F42C
			public override SecurityKeyIdentifierClause ReadClause(XmlDictionaryReader reader, byte[] derivationNone, int derivationLength, string tokenType)
			{
				bool flag = false;
				bool flag2 = false;
				string assertionId = null;
				string valueType = null;
				string text = null;
				string text2 = null;
				string text3 = null;
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement(XD.SamlDictionary.AuthorityBinding, XD.SecurityJan2004Dictionary.SamlUri))
					{
						if (flag)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("MultipleSamlAuthorityBindingsInReference")));
						}
						flag = true;
						text = reader.GetAttribute(XD.SamlDictionary.Binding, null);
						if (string.IsNullOrEmpty(text))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("RequiredAttributeMissing", new object[]
							{
								XD.SamlDictionary.Binding.Value,
								XD.SamlDictionary.AuthorityBinding.Value
							})));
						}
						text2 = reader.GetAttribute(XD.SamlDictionary.Location, null);
						if (string.IsNullOrEmpty(text2))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("RequiredAttributeMissing", new object[]
							{
								XD.SamlDictionary.Location.Value,
								XD.SamlDictionary.AuthorityBinding.Value
							})));
						}
						text3 = reader.GetAttribute(XD.SamlDictionary.AuthorityKind, null);
						if (string.IsNullOrEmpty(text3))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("RequiredAttributeMissing", new object[]
							{
								XD.SamlDictionary.AuthorityKind.Value,
								XD.SamlDictionary.AuthorityBinding.Value
							})));
						}
						if (reader.IsEmptyElement)
						{
							reader.Read();
						}
						else
						{
							reader.ReadStartElement();
							reader.ReadEndElement();
						}
					}
					else
					{
						if (!reader.IsStartElement(XD.SecurityJan2004Dictionary.KeyIdentifier, XD.SecurityJan2004Dictionary.Namespace))
						{
							break;
						}
						if (flag2)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("MultipleKeyIdentifiersInReference")));
						}
						flag2 = true;
						valueType = reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null);
						assertionId = reader.ReadElementContentAsString();
					}
				}
				if (!flag2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("DidNotFindKeyIdentifierInReference")));
				}
				return new SamlAssertionKeyIdentifierClause(assertionId, derivationNone, derivationLength, valueType, tokenType, text, text2, text3);
			}

			// Token: 0x060012F0 RID: 4848 RVA: 0x0005145C File Offset: 0x0004F65C
			public override bool SupportsCore(SecurityKeyIdentifierClause clause)
			{
				if (typeof(SamlAssertionKeyIdentifierClause).IsAssignableFrom(clause.GetType()))
				{
					SamlAssertionKeyIdentifierClause samlAssertionKeyIdentifierClause = clause as SamlAssertionKeyIdentifierClause;
					if (samlAssertionKeyIdentifierClause.TokenTypeUri == null || samlAssertionKeyIdentifierClause.TokenTypeUri == this.GetTokenTypeUri())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060012F1 RID: 4849 RVA: 0x000514A8 File Offset: 0x0004F6A8
			public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
			{
				SamlAssertionKeyIdentifierClause samlAssertionKeyIdentifierClause = clause as SamlAssertionKeyIdentifierClause;
				if (!string.IsNullOrEmpty(samlAssertionKeyIdentifierClause.Binding) || !string.IsNullOrEmpty(samlAssertionKeyIdentifierClause.Location) || !string.IsNullOrEmpty(samlAssertionKeyIdentifierClause.AuthorityKind))
				{
					writer.WriteStartElement(XD.SamlDictionary.PreferredPrefix.Value, XD.SamlDictionary.AuthorityBinding, XD.SecurityJan2004Dictionary.SamlUri);
					if (!string.IsNullOrEmpty(samlAssertionKeyIdentifierClause.Binding))
					{
						writer.WriteAttributeString(XD.SamlDictionary.Binding, null, samlAssertionKeyIdentifierClause.Binding);
					}
					if (!string.IsNullOrEmpty(samlAssertionKeyIdentifierClause.Location))
					{
						writer.WriteAttributeString(XD.SamlDictionary.Location, null, samlAssertionKeyIdentifierClause.Location);
					}
					if (!string.IsNullOrEmpty(samlAssertionKeyIdentifierClause.AuthorityKind))
					{
						writer.WriteAttributeString(XD.SamlDictionary.AuthorityKind, null, samlAssertionKeyIdentifierClause.AuthorityKind);
					}
					writer.WriteEndElement();
				}
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.KeyIdentifier, XD.SecurityJan2004Dictionary.Namespace);
				string value = string.IsNullOrEmpty(samlAssertionKeyIdentifierClause.ValueType) ? XD.SecurityJan2004Dictionary.SamlAssertionIdValueType.Value : samlAssertionKeyIdentifierClause.ValueType;
				writer.WriteAttributeString(XD.SecurityJan2004Dictionary.ValueType, null, value);
				writer.WriteString(samlAssertionKeyIdentifierClause.AssertionId);
				writer.WriteEndElement();
			}
		}

		// Token: 0x02000280 RID: 640
		private class Saml2Jan2004KeyIdentifierStrEntry : WSSecurityJan2004.SamlJan2004KeyIdentifierStrEntry
		{
			// Token: 0x060012F3 RID: 4851 RVA: 0x000515FB File Offset: 0x0004F7FB
			protected override bool IsMatchingValueType(string valueType)
			{
				return valueType == XD.SecurityXXX2005Dictionary.Saml11AssertionValueType.Value;
			}

			// Token: 0x060012F4 RID: 4852 RVA: 0x00051612 File Offset: 0x0004F812
			public override string GetTokenTypeUri()
			{
				return XD.SecurityXXX2005Dictionary.Saml20TokenType.Value;
			}
		}

		// Token: 0x02000281 RID: 641
		protected class RelDirectStrEntry : SecurityTokenSerializer.StrEntry
		{
			// Token: 0x060012F6 RID: 4854 RVA: 0x0005162C File Offset: 0x0004F82C
			public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
			{
				if (reader.IsStartElement(XD.SecurityJan2004Dictionary.Reference, XD.SecurityJan2004Dictionary.Namespace))
				{
					string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null);
					return attribute == "http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license";
				}
				return false;
			}

			// Token: 0x060012F7 RID: 4855 RVA: 0x00003459 File Offset: 0x00001659
			public override Type GetTokenType(SecurityKeyIdentifierClause clause)
			{
				return null;
			}

			// Token: 0x060012F8 RID: 4856 RVA: 0x00051674 File Offset: 0x0004F874
			public override string GetTokenTypeUri()
			{
				return XD.SecurityJan2004Dictionary.RelAssertionValueType.Value;
			}

			// Token: 0x060012F9 RID: 4857 RVA: 0x00051688 File Offset: 0x0004F888
			public override SecurityKeyIdentifierClause ReadClause(XmlDictionaryReader reader, byte[] derivationNone, int derivationLength, string tokenType)
			{
				string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.URI, null);
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.ReadStartElement();
					reader.ReadEndElement();
				}
				return new RelAssertionDirectKeyIdentifierClause(attribute, derivationNone, derivationLength);
			}

			// Token: 0x060012FA RID: 4858 RVA: 0x000516CC File Offset: 0x0004F8CC
			public override bool SupportsCore(SecurityKeyIdentifierClause clause)
			{
				return typeof(RelAssertionDirectKeyIdentifierClause).IsAssignableFrom(clause.GetType());
			}

			// Token: 0x060012FB RID: 4859 RVA: 0x000516E4 File Offset: 0x0004F8E4
			public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
			{
				RelAssertionDirectKeyIdentifierClause relAssertionDirectKeyIdentifierClause = clause as RelAssertionDirectKeyIdentifierClause;
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.Reference, XD.SecurityJan2004Dictionary.Namespace);
				writer.WriteAttributeString(XD.SecurityJan2004Dictionary.ValueType, null, "http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license");
				writer.WriteAttributeString(XD.SecurityJan2004Dictionary.URI, null, relAssertionDirectKeyIdentifierClause.AssertionId);
				writer.WriteEndElement();
			}
		}

		// Token: 0x02000282 RID: 642
		protected class X509IssuerSerialStrEntry : SecurityTokenSerializer.StrEntry
		{
			// Token: 0x060012FD RID: 4861 RVA: 0x0003E334 File Offset: 0x0003C534
			public override Type GetTokenType(SecurityKeyIdentifierClause clause)
			{
				return typeof(X509SecurityToken);
			}

			// Token: 0x060012FE RID: 4862 RVA: 0x00051754 File Offset: 0x0004F954
			public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
			{
				return reader.IsStartElement(XD.XmlSignatureDictionary.X509Data, XD.XmlSignatureDictionary.Namespace);
			}

			// Token: 0x060012FF RID: 4863 RVA: 0x00050FEC File Offset: 0x0004F1EC
			public override string GetTokenTypeUri()
			{
				return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3";
			}

			// Token: 0x06001300 RID: 4864 RVA: 0x00051770 File Offset: 0x0004F970
			public override SecurityKeyIdentifierClause ReadClause(XmlDictionaryReader reader, byte[] derivationNonce, int derivationLength, string tokenType)
			{
				reader.ReadStartElement(XD.XmlSignatureDictionary.X509Data, XD.XmlSignatureDictionary.Namespace);
				reader.ReadStartElement(XD.XmlSignatureDictionary.X509IssuerSerial, XD.XmlSignatureDictionary.Namespace);
				reader.ReadStartElement(XD.XmlSignatureDictionary.X509IssuerName, XD.XmlSignatureDictionary.Namespace);
				string issuerName = reader.ReadContentAsString();
				reader.ReadEndElement();
				reader.ReadStartElement(XD.XmlSignatureDictionary.X509SerialNumber, XD.XmlSignatureDictionary.Namespace);
				string issuerSerialNumber = reader.ReadContentAsString();
				reader.ReadEndElement();
				reader.ReadEndElement();
				reader.ReadEndElement();
				return new X509IssuerSerialKeyIdentifierClause(issuerName, issuerSerialNumber);
			}

			// Token: 0x06001301 RID: 4865 RVA: 0x00051812 File Offset: 0x0004FA12
			public override bool SupportsCore(SecurityKeyIdentifierClause clause)
			{
				return clause is X509IssuerSerialKeyIdentifierClause;
			}

			// Token: 0x06001302 RID: 4866 RVA: 0x00051820 File Offset: 0x0004FA20
			public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
			{
				X509IssuerSerialKeyIdentifierClause x509IssuerSerialKeyIdentifierClause = clause as X509IssuerSerialKeyIdentifierClause;
				writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509Data, XD.XmlSignatureDictionary.Namespace);
				writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509IssuerSerial, XD.XmlSignatureDictionary.Namespace);
				writer.WriteElementString(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509IssuerName, XD.XmlSignatureDictionary.Namespace, x509IssuerSerialKeyIdentifierClause.IssuerName);
				writer.WriteElementString(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509SerialNumber, XD.XmlSignatureDictionary.Namespace, x509IssuerSerialKeyIdentifierClause.IssuerSerialNumber);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		// Token: 0x02000283 RID: 643
		public class IdManager : SignatureTargetIdManager
		{
			// Token: 0x06001304 RID: 4868 RVA: 0x000518F0 File Offset: 0x0004FAF0
			private IdManager()
			{
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x06001305 RID: 4869 RVA: 0x000518F8 File Offset: 0x0004FAF8
			public override string DefaultIdNamespacePrefix
			{
				get
				{
					return "u";
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x06001306 RID: 4870 RVA: 0x000518FF File Offset: 0x0004FAFF
			public override string DefaultIdNamespaceUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
				}
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x06001307 RID: 4871 RVA: 0x00051906 File Offset: 0x0004FB06
			internal static WSSecurityJan2004.IdManager Instance
			{
				get
				{
					return WSSecurityJan2004.IdManager.instance;
				}
			}

			// Token: 0x06001308 RID: 4872 RVA: 0x00051910 File Offset: 0x0004FB10
			public override string ExtractId(XmlDictionaryReader reader)
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				if (reader.IsStartElement(WSSecurityJan2004.IdManager.ElementName, XD.XmlEncryptionDictionary.Namespace))
				{
					return reader.GetAttribute(XD.XmlEncryptionDictionary.Id, null);
				}
				return reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
			}

			// Token: 0x06001309 RID: 4873 RVA: 0x00051973 File Offset: 0x0004FB73
			public override void WriteIdAttribute(XmlDictionaryWriter writer, string id)
			{
				if (writer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
				}
				writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, id);
			}

			// Token: 0x04001129 RID: 4393
			internal static readonly XmlDictionaryString ElementName = XD.XmlEncryptionDictionary.EncryptedData;

			// Token: 0x0400112A RID: 4394
			private static readonly WSSecurityJan2004.IdManager instance = new WSSecurityJan2004.IdManager();
		}
	}
}

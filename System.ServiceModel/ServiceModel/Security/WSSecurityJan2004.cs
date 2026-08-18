using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000369 RID: 873
	internal class WSSecurityJan2004 : WSSecurityTokenSerializer.SerializerEntries
	{
		// Token: 0x06001FF4 RID: 8180 RVA: 0x0007798F File Offset: 0x00075B8F
		public WSSecurityJan2004(WSSecurityTokenSerializer tokenSerializer, SamlSerializer samlSerializer)
		{
			this.tokenSerializer = tokenSerializer;
			this.samlSerializer = samlSerializer;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x000779A5 File Offset: 0x00075BA5
		public WSSecurityTokenSerializer WSSecurityTokenSerializer
		{
			get
			{
				return this.tokenSerializer;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x000779AD File Offset: 0x00075BAD
		public SamlSerializer SamlSerializer
		{
			get
			{
				return this.samlSerializer;
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000779B5 File Offset: 0x00075BB5
		protected void PopulateJan2004TokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			tokenEntryList.Add(new WSSecurityJan2004.GenericXmlTokenEntry());
			tokenEntryList.Add(new WSSecurityJan2004.UserNamePasswordTokenEntry(this.tokenSerializer));
			tokenEntryList.Add(new WSSecurityJan2004.KerberosTokenEntry(this.tokenSerializer));
			tokenEntryList.Add(new WSSecurityJan2004.X509TokenEntry(this.tokenSerializer));
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000779F5 File Offset: 0x00075BF5
		public override void PopulateTokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			this.PopulateJan2004TokenEntries(tokenEntryList);
			tokenEntryList.Add(new WSSecurityJan2004.SamlTokenEntry(this.tokenSerializer, this.samlSerializer));
			tokenEntryList.Add(new WSSecurityJan2004.WrappedKeyTokenEntry(this.tokenSerializer));
		}

		// Token: 0x04001F05 RID: 7941
		private WSSecurityTokenSerializer tokenSerializer;

		// Token: 0x04001F06 RID: 7942
		private SamlSerializer samlSerializer;

		// Token: 0x02000B89 RID: 2953
		internal abstract class BinaryTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x060072FF RID: 29439 RVA: 0x001AD427 File Offset: 0x001AB627
			protected BinaryTokenEntry(WSSecurityTokenSerializer tokenSerializer, string valueTypeUri)
			{
				this.tokenSerializer = tokenSerializer;
				this.valueTypeUris = new string[1];
				this.valueTypeUris[0] = valueTypeUri;
			}

			// Token: 0x06007300 RID: 29440 RVA: 0x001AD44C File Offset: 0x001AB64C
			protected BinaryTokenEntry(WSSecurityTokenSerializer tokenSerializer, string[] valueTypeUris)
			{
				if (valueTypeUris == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("valueTypeUris");
				}
				this.tokenSerializer = tokenSerializer;
				this.valueTypeUris = new string[valueTypeUris.GetLength(0)];
				for (int i = 0; i < this.valueTypeUris.GetLength(0); i++)
				{
					this.valueTypeUris[i] = valueTypeUris[i];
				}
			}

			// Token: 0x17001A9E RID: 6814
			// (get) Token: 0x06007301 RID: 29441 RVA: 0x001AD4AD File Offset: 0x001AB6AD
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return WSSecurityJan2004.BinaryTokenEntry.ElementName;
				}
			}

			// Token: 0x17001A9F RID: 6815
			// (get) Token: 0x06007302 RID: 29442 RVA: 0x001AD4B4 File Offset: 0x001AB6B4
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.SecurityJan2004Dictionary.Namespace;
				}
			}

			// Token: 0x17001AA0 RID: 6816
			// (get) Token: 0x06007303 RID: 29443 RVA: 0x001AD4C0 File Offset: 0x001AB6C0
			public override string TokenTypeUri
			{
				get
				{
					return this.valueTypeUris[0];
				}
			}

			// Token: 0x17001AA1 RID: 6817
			// (get) Token: 0x06007304 RID: 29444 RVA: 0x001AD4CA File Offset: 0x001AB6CA
			protected override string ValueTypeUri
			{
				get
				{
					return this.valueTypeUris[0];
				}
			}

			// Token: 0x06007305 RID: 29445 RVA: 0x001AD4D4 File Offset: 0x001AB6D4
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

			// Token: 0x06007306 RID: 29446
			public abstract SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromBinaryCore(byte[] rawData);

			// Token: 0x06007307 RID: 29447 RVA: 0x001AD50C File Offset: 0x001AB70C
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				TokenReferenceStyleHelper.Validate(tokenReferenceStyle);
				if (tokenReferenceStyle == SecurityTokenReferenceStyle.Internal)
				{
					return WSSecurityTokenSerializer.TokenEntry.CreateDirectReference(issuedTokenXml, "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", base.TokenType);
				}
				if (tokenReferenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenReferenceStyle"));
				}
				string attribute = issuedTokenXml.GetAttribute("EncodingType", null);
				string innerText = issuedTokenXml.InnerText;
				byte[] rawData;
				if (attribute == null || attribute == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary")
				{
					rawData = Convert.FromBase64String(innerText);
				}
				else
				{
					if (!(attribute == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnknownEncodingInBinarySecurityToken")));
					}
					rawData = SoapHexBinary.Parse(innerText).Value;
				}
				return this.CreateKeyIdentifierClauseFromBinaryCore(rawData);
			}

			// Token: 0x06007308 RID: 29448
			public abstract SecurityToken ReadBinaryCore(string id, string valueTypeUri, byte[] rawData);

			// Token: 0x06007309 RID: 29449 RVA: 0x001AD5C0 File Offset: 0x001AB7C0
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				string attribute = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				string attribute2 = reader.GetAttribute(WSSecurityJan2004.BinaryTokenEntry.ValueTypeAttribute, null);
				string attribute3 = reader.GetAttribute(WSSecurityJan2004.BinaryTokenEntry.EncodingTypeAttribute, null);
				byte[] rawData;
				if (attribute3 == null || attribute3 == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary")
				{
					rawData = reader.ReadElementContentAsBase64();
				}
				else
				{
					if (!(attribute3 == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnknownEncodingInBinarySecurityToken")));
					}
					rawData = SoapHexBinary.Parse(reader.ReadElementContentAsString()).Value;
				}
				return this.ReadBinaryCore(attribute, attribute2, rawData);
			}

			// Token: 0x0600730A RID: 29450
			public abstract void WriteBinaryCore(SecurityToken token, out string id, out byte[] rawData);

			// Token: 0x0600730B RID: 29451 RVA: 0x001AD660 File Offset: 0x001AB860
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				string text;
				byte[] array;
				this.WriteBinaryCore(token, out text, out array);
				if (array == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rawData");
				}
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, WSSecurityJan2004.BinaryTokenEntry.ElementName, XD.SecurityJan2004Dictionary.Namespace);
				if (text != null)
				{
					writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, text);
				}
				if (this.valueTypeUris != null)
				{
					writer.WriteAttributeString(WSSecurityJan2004.BinaryTokenEntry.ValueTypeAttribute, null, this.valueTypeUris[0]);
				}
				if (this.tokenSerializer.EmitBspRequiredAttributes)
				{
					writer.WriteAttributeString(WSSecurityJan2004.BinaryTokenEntry.EncodingTypeAttribute, null, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
				}
				writer.WriteBase64(array, 0, array.Length);
				writer.WriteEndElement();
			}

			// Token: 0x04004114 RID: 16660
			internal static readonly XmlDictionaryString ElementName = XD.SecurityJan2004Dictionary.BinarySecurityToken;

			// Token: 0x04004115 RID: 16661
			internal static readonly XmlDictionaryString EncodingTypeAttribute = XD.SecurityJan2004Dictionary.EncodingType;

			// Token: 0x04004116 RID: 16662
			internal const string EncodingTypeAttributeString = "EncodingType";

			// Token: 0x04004117 RID: 16663
			internal const string EncodingTypeValueBase64Binary = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

			// Token: 0x04004118 RID: 16664
			internal const string EncodingTypeValueHexBinary = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

			// Token: 0x04004119 RID: 16665
			internal static readonly XmlDictionaryString ValueTypeAttribute = XD.SecurityJan2004Dictionary.ValueType;

			// Token: 0x0400411A RID: 16666
			private WSSecurityTokenSerializer tokenSerializer;

			// Token: 0x0400411B RID: 16667
			private string[] valueTypeUris;
		}

		// Token: 0x02000B8A RID: 2954
		private class GenericXmlTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x17001AA2 RID: 6818
			// (get) Token: 0x0600730D RID: 29453 RVA: 0x001AD756 File Offset: 0x001AB956
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001AA3 RID: 6819
			// (get) Token: 0x0600730E RID: 29454 RVA: 0x001AD759 File Offset: 0x001AB959
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600730F RID: 29455 RVA: 0x001AD75C File Offset: 0x001AB95C
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(GenericXmlSecurityToken)
				};
			}

			// Token: 0x17001AA4 RID: 6820
			// (get) Token: 0x06007310 RID: 29456 RVA: 0x001AD771 File Offset: 0x001AB971
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001AA5 RID: 6821
			// (get) Token: 0x06007311 RID: 29457 RVA: 0x001AD774 File Offset: 0x001AB974
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007313 RID: 29459 RVA: 0x001AD77F File Offset: 0x001AB97F
			public override bool CanReadTokenCore(XmlElement element)
			{
				return false;
			}

			// Token: 0x06007314 RID: 29460 RVA: 0x001AD782 File Offset: 0x001AB982
			public override bool CanReadTokenCore(XmlDictionaryReader reader)
			{
				return false;
			}

			// Token: 0x06007315 RID: 29461 RVA: 0x001AD785 File Offset: 0x001AB985
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x06007316 RID: 29462 RVA: 0x001AD796 File Offset: 0x001AB996
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x06007317 RID: 29463 RVA: 0x001AD7A8 File Offset: 0x001AB9A8
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				BufferedGenericXmlSecurityToken bufferedGenericXmlSecurityToken = token as BufferedGenericXmlSecurityToken;
				if (bufferedGenericXmlSecurityToken != null && bufferedGenericXmlSecurityToken.TokenXmlBuffer != null)
				{
					using (XmlDictionaryReader reader = bufferedGenericXmlSecurityToken.TokenXmlBuffer.GetReader(0))
					{
						writer.WriteNode(reader, false);
						return;
					}
				}
				GenericXmlSecurityToken genericXmlSecurityToken = (GenericXmlSecurityToken)token;
				genericXmlSecurityToken.TokenXml.WriteTo(writer);
			}
		}

		// Token: 0x02000B8B RID: 2955
		private class KerberosTokenEntry : WSSecurityJan2004.BinaryTokenEntry
		{
			// Token: 0x06007318 RID: 29464 RVA: 0x001AD80C File Offset: 0x001ABA0C
			public KerberosTokenEntry(WSSecurityTokenSerializer tokenSerializer) : base(tokenSerializer, new string[]
			{
				"http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ",
				"http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510"
			})
			{
			}

			// Token: 0x06007319 RID: 29465 RVA: 0x001AD82B File Offset: 0x001ABA2B
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(KerberosReceiverSecurityToken),
					typeof(KerberosRequestorSecurityToken)
				};
			}

			// Token: 0x0600731A RID: 29466 RVA: 0x001AD850 File Offset: 0x001ABA50
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromBinaryCore(byte[] rawData)
			{
				byte[] ticketHash;
				using (HashAlgorithm hashAlgorithm = CryptoHelper.NewSha1HashAlgorithm())
				{
					ticketHash = hashAlgorithm.ComputeHash(rawData, 0, rawData.Length);
				}
				return new KerberosTicketHashKeyIdentifierClause(ticketHash);
			}

			// Token: 0x0600731B RID: 29467 RVA: 0x001AD894 File Offset: 0x001ABA94
			public override SecurityToken ReadBinaryCore(string id, string valueTypeUri, byte[] rawData)
			{
				return new KerberosReceiverSecurityToken(rawData, id, false, valueTypeUri);
			}

			// Token: 0x0600731C RID: 29468 RVA: 0x001AD8A0 File Offset: 0x001ABAA0
			public override void WriteBinaryCore(SecurityToken token, out string id, out byte[] rawData)
			{
				KerberosRequestorSecurityToken kerberosRequestorSecurityToken = (KerberosRequestorSecurityToken)token;
				id = token.Id;
				rawData = kerberosRequestorSecurityToken.GetRequest();
			}
		}

		// Token: 0x02000B8C RID: 2956
		protected class SamlTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x0600731D RID: 29469 RVA: 0x001AD8C4 File Offset: 0x001ABAC4
			public SamlTokenEntry(SecurityTokenSerializer tokenSerializer, SamlSerializer samlSerializer)
			{
				this.tokenSerializer = tokenSerializer;
				if (samlSerializer != null)
				{
					this.samlSerializer = samlSerializer;
				}
				else
				{
					this.samlSerializer = new SamlSerializer();
				}
				this.samlSerializer.PopulateDictionary(BinaryMessageEncoderFactory.XmlDictionary);
			}

			// Token: 0x17001AA6 RID: 6822
			// (get) Token: 0x0600731E RID: 29470 RVA: 0x001AD8FA File Offset: 0x001ABAFA
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.SecurityJan2004Dictionary.SamlAssertion;
				}
			}

			// Token: 0x17001AA7 RID: 6823
			// (get) Token: 0x0600731F RID: 29471 RVA: 0x001AD906 File Offset: 0x001ABB06
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.SecurityJan2004Dictionary.SamlUri;
				}
			}

			// Token: 0x06007320 RID: 29472 RVA: 0x001AD912 File Offset: 0x001ABB12
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(SamlSecurityToken)
				};
			}

			// Token: 0x17001AA8 RID: 6824
			// (get) Token: 0x06007321 RID: 29473 RVA: 0x001AD927 File Offset: 0x001ABB27
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001AA9 RID: 6825
			// (get) Token: 0x06007322 RID: 29474 RVA: 0x001AD92A File Offset: 0x001ABB2A
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007323 RID: 29475 RVA: 0x001AD930 File Offset: 0x001ABB30
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				TokenReferenceStyleHelper.Validate(tokenReferenceStyle);
				if (tokenReferenceStyle <= SecurityTokenReferenceStyle.External)
				{
					string attribute = issuedTokenXml.GetAttribute("AssertionID");
					return new SamlAssertionKeyIdentifierClause(attribute);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenReferenceStyle"));
			}

			// Token: 0x06007324 RID: 29476 RVA: 0x001AD970 File Offset: 0x001ABB70
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				return this.samlSerializer.ReadToken(reader, this.tokenSerializer, tokenResolver);
			}

			// Token: 0x06007325 RID: 29477 RVA: 0x001AD994 File Offset: 0x001ABB94
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				SamlSecurityToken token2 = token as SamlSecurityToken;
				this.samlSerializer.WriteToken(token2, writer, this.tokenSerializer);
			}

			// Token: 0x0400411C RID: 16668
			private const string samlAssertionId = "AssertionID";

			// Token: 0x0400411D RID: 16669
			private SamlSerializer samlSerializer;

			// Token: 0x0400411E RID: 16670
			private SecurityTokenSerializer tokenSerializer;
		}

		// Token: 0x02000B8D RID: 2957
		private class UserNamePasswordTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x06007326 RID: 29478 RVA: 0x001AD9BB File Offset: 0x001ABBBB
			public UserNamePasswordTokenEntry(WSSecurityTokenSerializer tokenSerializer)
			{
				this.tokenSerializer = tokenSerializer;
			}

			// Token: 0x17001AAA RID: 6826
			// (get) Token: 0x06007327 RID: 29479 RVA: 0x001AD9CA File Offset: 0x001ABBCA
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.SecurityJan2004Dictionary.UserNameTokenElement;
				}
			}

			// Token: 0x17001AAB RID: 6827
			// (get) Token: 0x06007328 RID: 29480 RVA: 0x001AD9D6 File Offset: 0x001ABBD6
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.SecurityJan2004Dictionary.Namespace;
				}
			}

			// Token: 0x06007329 RID: 29481 RVA: 0x001AD9E2 File Offset: 0x001ABBE2
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(UserNameSecurityToken)
				};
			}

			// Token: 0x17001AAC RID: 6828
			// (get) Token: 0x0600732A RID: 29482 RVA: 0x001AD9F7 File Offset: 0x001ABBF7
			public override string TokenTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#UsernameToken";
				}
			}

			// Token: 0x17001AAD RID: 6829
			// (get) Token: 0x0600732B RID: 29483 RVA: 0x001AD9FE File Offset: 0x001ABBFE
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600732C RID: 29484 RVA: 0x001ADA04 File Offset: 0x001ABC04
			public override IAsyncResult BeginReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver, AsyncCallback callback, object state)
			{
				string id;
				string userName;
				string password;
				WSSecurityJan2004.UserNamePasswordTokenEntry.ParseToken(reader, out id, out userName, out password);
				SecurityToken data = new UserNameSecurityToken(userName, password, id);
				return new CompletedAsyncResult<SecurityToken>(data, callback, state);
			}

			// Token: 0x0600732D RID: 29485 RVA: 0x001ADA30 File Offset: 0x001ABC30
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				TokenReferenceStyleHelper.Validate(tokenReferenceStyle);
				if (tokenReferenceStyle == SecurityTokenReferenceStyle.Internal)
				{
					return WSSecurityTokenSerializer.TokenEntry.CreateDirectReference(issuedTokenXml, "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", typeof(UserNameSecurityToken));
				}
				if (tokenReferenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenReferenceStyle"));
				}
				return null;
			}

			// Token: 0x0600732E RID: 29486 RVA: 0x001ADA7D File Offset: 0x001ABC7D
			public override SecurityToken EndReadTokenCore(IAsyncResult result)
			{
				return CompletedAsyncResult<SecurityToken>.End(result);
			}

			// Token: 0x0600732F RID: 29487 RVA: 0x001ADA88 File Offset: 0x001ABC88
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				string value;
				string userName;
				string password;
				WSSecurityJan2004.UserNamePasswordTokenEntry.ParseToken(reader, out value, out userName, out password);
				if (value == null)
				{
					value = SecurityUniqueId.Create().Value;
				}
				return new UserNameSecurityToken(userName, password, value);
			}

			// Token: 0x06007330 RID: 29488 RVA: 0x001ADABC File Offset: 0x001ABCBC
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				UserNameSecurityToken userNameSecurityToken = (UserNameSecurityToken)token;
				this.WriteUserNamePassword(writer, userNameSecurityToken.Id, userNameSecurityToken.UserName, userNameSecurityToken.Password);
			}

			// Token: 0x06007331 RID: 29489 RVA: 0x001ADAEC File Offset: 0x001ABCEC
			private void WriteUserNamePassword(XmlDictionaryWriter writer, string id, string userName, string password)
			{
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.UserNameTokenElement, XD.SecurityJan2004Dictionary.Namespace);
				writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, id);
				writer.WriteElementString(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.UserNameElement, XD.SecurityJan2004Dictionary.Namespace, userName);
				if (password != null)
				{
					writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.PasswordElement, XD.SecurityJan2004Dictionary.Namespace);
					if (this.tokenSerializer.EmitBspRequiredAttributes)
					{
						writer.WriteAttributeString(XD.SecurityJan2004Dictionary.TypeAttribute, null, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText");
					}
					writer.WriteString(password);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}

			// Token: 0x06007332 RID: 29490 RVA: 0x001ADBDC File Offset: 0x001ABDDC
			private static string ParsePassword(XmlDictionaryReader reader)
			{
				string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.TypeAttribute, null);
				if (attribute != null && attribute.Length > 0 && attribute != "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedPasswordType", new object[]
					{
						attribute
					})));
				}
				return reader.ReadElementString();
			}

			// Token: 0x06007333 RID: 29491 RVA: 0x001ADC40 File Offset: 0x001ABE40
			private static void ParseToken(XmlDictionaryReader reader, out string id, out string userName, out string password)
			{
				id = null;
				userName = null;
				password = null;
				reader.MoveToContent();
				id = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				reader.ReadStartElement(XD.SecurityJan2004Dictionary.UserNameTokenElement, XD.SecurityJan2004Dictionary.Namespace);
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement(XD.SecurityJan2004Dictionary.UserNameElement, XD.SecurityJan2004Dictionary.Namespace))
					{
						userName = reader.ReadElementString();
					}
					else if (reader.IsStartElement(XD.SecurityJan2004Dictionary.PasswordElement, XD.SecurityJan2004Dictionary.Namespace))
					{
						password = WSSecurityJan2004.UserNamePasswordTokenEntry.ParsePassword(reader);
					}
					else if (reader.IsStartElement(XD.SecurityJan2004Dictionary.NonceElement, XD.SecurityJan2004Dictionary.Namespace))
					{
						reader.Skip();
					}
					else if (reader.IsStartElement(XD.UtilityDictionary.CreatedElement, XD.UtilityDictionary.Namespace))
					{
						reader.Skip();
					}
					else
					{
						XmlHelper.OnUnexpectedChildNodeError("UsernameToken", reader);
					}
				}
				reader.ReadEndElement();
				if (userName == null)
				{
					XmlHelper.OnRequiredElementMissing("Username", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
				}
			}

			// Token: 0x0400411F RID: 16671
			private WSSecurityTokenSerializer tokenSerializer;
		}

		// Token: 0x02000B8E RID: 2958
		protected class WrappedKeyTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x06007334 RID: 29492 RVA: 0x001ADD5B File Offset: 0x001ABF5B
			public WrappedKeyTokenEntry(WSSecurityTokenSerializer tokenSerializer)
			{
				this.tokenSerializer = tokenSerializer;
			}

			// Token: 0x17001AAE RID: 6830
			// (get) Token: 0x06007335 RID: 29493 RVA: 0x001ADD6A File Offset: 0x001ABF6A
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return EncryptedKey.ElementName;
				}
			}

			// Token: 0x17001AAF RID: 6831
			// (get) Token: 0x06007336 RID: 29494 RVA: 0x001ADD71 File Offset: 0x001ABF71
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.XmlEncryptionDictionary.Namespace;
				}
			}

			// Token: 0x06007337 RID: 29495 RVA: 0x001ADD7D File Offset: 0x001ABF7D
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(WrappedKeySecurityToken)
				};
			}

			// Token: 0x17001AB0 RID: 6832
			// (get) Token: 0x06007338 RID: 29496 RVA: 0x001ADD92 File Offset: 0x001ABF92
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001AB1 RID: 6833
			// (get) Token: 0x06007339 RID: 29497 RVA: 0x001ADD95 File Offset: 0x001ABF95
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600733A RID: 29498 RVA: 0x001ADD98 File Offset: 0x001ABF98
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				TokenReferenceStyleHelper.Validate(tokenReferenceStyle);
				if (tokenReferenceStyle == SecurityTokenReferenceStyle.Internal)
				{
					return WSSecurityTokenSerializer.TokenEntry.CreateDirectReference(issuedTokenXml, "Id", null, null);
				}
				if (tokenReferenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenReferenceStyle"));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CantInferReferenceForToken", new object[]
				{
					EncryptedKey.ElementName.Value
				})));
			}

			// Token: 0x0600733B RID: 29499 RVA: 0x001ADE04 File Offset: 0x001AC004
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				EncryptedKey encryptedKey = new EncryptedKey();
				encryptedKey.SecurityTokenSerializer = this.tokenSerializer;
				encryptedKey.ReadFrom(reader);
				SecurityKeyIdentifier keyIdentifier = encryptedKey.KeyIdentifier;
				byte[] wrappedKey = encryptedKey.GetWrappedKey();
				WrappedKeySecurityToken wrappedKeySecurityToken = this.CreateWrappedKeyToken(encryptedKey.Id, encryptedKey.EncryptionMethod, encryptedKey.CarriedKeyName, keyIdentifier, wrappedKey, tokenResolver);
				wrappedKeySecurityToken.EncryptedKey = encryptedKey;
				return wrappedKeySecurityToken;
			}

			// Token: 0x0600733C RID: 29500 RVA: 0x001ADE5C File Offset: 0x001AC05C
			private WrappedKeySecurityToken CreateWrappedKeyToken(string id, string encryptionMethod, string carriedKeyName, SecurityKeyIdentifier unwrappingTokenIdentifier, byte[] wrappedKey, SecurityTokenResolver tokenResolver)
			{
				ISspiNegotiationInfo sspiNegotiationInfo = tokenResolver as ISspiNegotiationInfo;
				if (sspiNegotiationInfo != null)
				{
					ISspiNegotiation sspiNegotiation = sspiNegotiationInfo.SspiNegotiation;
					if (encryptionMethod != sspiNegotiation.KeyEncryptionAlgorithm)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("BadKeyEncryptionAlgorithm", new object[]
						{
							encryptionMethod
						})));
					}
					byte[] array = sspiNegotiation.Decrypt(wrappedKey);
					return new WrappedKeySecurityToken(id, array, encryptionMethod, sspiNegotiation, array);
				}
				else
				{
					if (tokenResolver == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("tokenResolver"));
					}
					if (unwrappingTokenIdentifier == null || unwrappingTokenIdentifier.Count == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MissingKeyInfoInEncryptedKey")));
					}
					SecurityHeaderTokenResolver securityHeaderTokenResolver = tokenResolver as SecurityHeaderTokenResolver;
					SecurityToken securityToken;
					if (securityHeaderTokenResolver != null)
					{
						securityToken = securityHeaderTokenResolver.ExpectedWrapper;
						if (securityToken == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveKeyInfoForUnwrappingToken", new object[]
							{
								unwrappingTokenIdentifier,
								securityHeaderTokenResolver
							})));
						}
						if (!securityHeaderTokenResolver.CheckExternalWrapperMatch(unwrappingTokenIdentifier))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedKeyWasNotEncryptedWithTheRequiredEncryptingToken", new object[]
							{
								securityToken
							})));
						}
					}
					else
					{
						try
						{
							securityToken = tokenResolver.ResolveToken(unwrappingTokenIdentifier);
						}
						catch (Exception ex)
						{
							if (ex is MessageSecurityException)
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveKeyInfoForUnwrappingToken", new object[]
							{
								unwrappingTokenIdentifier,
								tokenResolver
							}), ex));
						}
					}
					SecurityKey wrappingSecurityKey;
					byte[] keyToWrap = SecurityUtils.DecryptKey(securityToken, encryptionMethod, wrappedKey, out wrappingSecurityKey);
					return new WrappedKeySecurityToken(id, keyToWrap, encryptionMethod, securityToken, unwrappingTokenIdentifier, wrappedKey, wrappingSecurityKey);
				}
			}

			// Token: 0x0600733D RID: 29501 RVA: 0x001ADFE8 File Offset: 0x001AC1E8
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				WrappedKeySecurityToken wrappedKeySecurityToken = token as WrappedKeySecurityToken;
				wrappedKeySecurityToken.EnsureEncryptedKeySetUp();
				wrappedKeySecurityToken.EncryptedKey.SecurityTokenSerializer = this.tokenSerializer;
				wrappedKeySecurityToken.EncryptedKey.WriteTo(writer, ServiceModelDictionaryManager.Instance);
			}

			// Token: 0x04004120 RID: 16672
			private WSSecurityTokenSerializer tokenSerializer;
		}

		// Token: 0x02000B8F RID: 2959
		protected class X509TokenEntry : WSSecurityJan2004.BinaryTokenEntry
		{
			// Token: 0x0600733E RID: 29502 RVA: 0x001AE024 File Offset: 0x001AC224
			public X509TokenEntry(WSSecurityTokenSerializer tokenSerializer) : base(tokenSerializer, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3")
			{
			}

			// Token: 0x0600733F RID: 29503 RVA: 0x001AE032 File Offset: 0x001AC232
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(X509SecurityToken),
					typeof(X509WindowsSecurityToken)
				};
			}

			// Token: 0x06007340 RID: 29504 RVA: 0x001AE054 File Offset: 0x001AC254
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromBinaryCore(byte[] rawData)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CantInferReferenceForToken", new object[]
				{
					"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"
				})));
			}

			// Token: 0x06007341 RID: 29505 RVA: 0x001AE080 File Offset: 0x001AC280
			public override SecurityToken ReadBinaryCore(string id, string valueTypeUri, byte[] rawData)
			{
				X509Certificate2 certificate;
				if (!SecurityUtils.TryCreateX509CertificateFromRawData(rawData, out certificate))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("InvalidX509RawData")));
				}
				return new X509SecurityToken(certificate, id, false);
			}

			// Token: 0x06007342 RID: 29506 RVA: 0x001AE0BC File Offset: 0x001AC2BC
			public override void WriteBinaryCore(SecurityToken token, out string id, out byte[] rawData)
			{
				id = token.Id;
				X509SecurityToken x509SecurityToken = token as X509SecurityToken;
				if (x509SecurityToken != null)
				{
					rawData = x509SecurityToken.Certificate.GetRawCertData();
					return;
				}
				rawData = ((X509WindowsSecurityToken)token).Certificate.GetRawCertData();
			}

			// Token: 0x04004121 RID: 16673
			internal const string ValueTypeAbsoluteUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3";
		}

		// Token: 0x02000B90 RID: 2960
		public class IdManager : SignatureTargetIdManager
		{
			// Token: 0x06007343 RID: 29507 RVA: 0x001AE0FB File Offset: 0x001AC2FB
			private IdManager()
			{
			}

			// Token: 0x17001AB2 RID: 6834
			// (get) Token: 0x06007344 RID: 29508 RVA: 0x001AE103 File Offset: 0x001AC303
			public override string DefaultIdNamespacePrefix
			{
				get
				{
					return "u";
				}
			}

			// Token: 0x17001AB3 RID: 6835
			// (get) Token: 0x06007345 RID: 29509 RVA: 0x001AE10A File Offset: 0x001AC30A
			public override string DefaultIdNamespaceUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
				}
			}

			// Token: 0x17001AB4 RID: 6836
			// (get) Token: 0x06007346 RID: 29510 RVA: 0x001AE111 File Offset: 0x001AC311
			internal static WSSecurityJan2004.IdManager Instance
			{
				get
				{
					return WSSecurityJan2004.IdManager.instance;
				}
			}

			// Token: 0x06007347 RID: 29511 RVA: 0x001AE118 File Offset: 0x001AC318
			public override string ExtractId(XmlDictionaryReader reader)
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				if (reader.IsStartElement(EncryptedData.ElementName, XD.XmlEncryptionDictionary.Namespace))
				{
					return reader.GetAttribute(XD.XmlEncryptionDictionary.Id, null);
				}
				return reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
			}

			// Token: 0x06007348 RID: 29512 RVA: 0x001AE17B File Offset: 0x001AC37B
			public override void WriteIdAttribute(XmlDictionaryWriter writer, string id)
			{
				if (writer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
				}
				writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, id);
			}

			// Token: 0x04004122 RID: 16674
			private static readonly WSSecurityJan2004.IdManager instance = new WSSecurityJan2004.IdManager();
		}
	}
}

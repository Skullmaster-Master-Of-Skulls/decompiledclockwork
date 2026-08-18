using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200018D RID: 397
	internal class WSSecurityXXX2005 : WSSecurityJan2004
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x0003C1CB File Offset: 0x0003A3CB
		public WSSecurityXXX2005(KeyInfoSerializer securityTokenSerializer) : base(securityTokenSerializer)
		{
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003C1D4 File Offset: 0x0003A3D4
		public override void PopulateStrEntries(IList<SecurityTokenSerializer.StrEntry> strEntries)
		{
			base.PopulateJan2004StrEntries(strEntries);
			strEntries.Add(new WSSecurityXXX2005.SamlDirectStrEntry());
			strEntries.Add(new WSSecurityXXX2005.X509ThumbprintStrEntry(base.SecurityTokenSerializer.EmitBspRequiredAttributes));
			strEntries.Add(new WSSecurityXXX2005.EncryptedKeyHashStrEntry(base.SecurityTokenSerializer.EmitBspRequiredAttributes));
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0003C214 File Offset: 0x0003A414
		public override void PopulateTokenEntries(IList<SecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			base.PopulateJan2004TokenEntries(tokenEntryList);
			tokenEntryList.Add(new WSSecurityXXX2005.WrappedKeyTokenEntry());
			tokenEntryList.Add(new WSSecurityXXX2005.SamlTokenEntry());
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0003C234 File Offset: 0x0003A434
		public override void PopulateKeyIdentifierClauseEntries(IList<SecurityTokenSerializer.KeyIdentifierClauseEntry> clauseEntries)
		{
			List<SecurityTokenSerializer.StrEntry> strEntries = new List<SecurityTokenSerializer.StrEntry>();
			base.SecurityTokenSerializer.PopulateStrEntries(strEntries);
			WSSecurityXXX2005.SecurityTokenReferenceXXX2005ClauseEntry item = new WSSecurityXXX2005.SecurityTokenReferenceXXX2005ClauseEntry(base.SecurityTokenSerializer.EmitBspRequiredAttributes, strEntries);
			clauseEntries.Add(item);
		}

		// Token: 0x02000284 RID: 644
		private new class SamlTokenEntry : WSSecurityJan2004.SamlTokenEntry
		{
			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x0600130B RID: 4875 RVA: 0x000519CD File Offset: 0x0004FBCD
			public override string TokenTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";
				}
			}
		}

		// Token: 0x02000285 RID: 645
		private new class WrappedKeyTokenEntry : WSSecurityJan2004.WrappedKeyTokenEntry
		{
			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x0600130D RID: 4877 RVA: 0x000519DC File Offset: 0x0004FBDC
			public override string TokenTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey";
				}
			}
		}

		// Token: 0x02000286 RID: 646
		private class SecurityTokenReferenceXXX2005ClauseEntry : WSSecurityJan2004.SecurityTokenReferenceJan2004ClauseEntry
		{
			// Token: 0x0600130F RID: 4879 RVA: 0x000519EB File Offset: 0x0004FBEB
			public SecurityTokenReferenceXXX2005ClauseEntry(bool emitBspRequiredAttributes, IList<SecurityTokenSerializer.StrEntry> strEntries) : base(emitBspRequiredAttributes, strEntries)
			{
			}

			// Token: 0x06001310 RID: 4880 RVA: 0x000519F5 File Offset: 0x0004FBF5
			protected override string ReadTokenType(XmlDictionaryReader reader)
			{
				return reader.GetAttribute(XD.SecurityXXX2005Dictionary.TokenTypeAttribute, XD.SecurityXXX2005Dictionary.Namespace);
			}

			// Token: 0x06001311 RID: 4881 RVA: 0x00051A14 File Offset: 0x0004FC14
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				for (int i = 0; i < base.StrEntries.Count; i++)
				{
					if (base.StrEntries[i].SupportsCore(keyIdentifierClause))
					{
						writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.SecurityTokenReference, XD.SecurityJan2004Dictionary.Namespace);
						string tokenTypeUri = this.GetTokenTypeUri(base.StrEntries[i], keyIdentifierClause);
						if (tokenTypeUri != null)
						{
							writer.WriteAttributeString(XD.SecurityXXX2005Dictionary.Prefix.Value, XD.SecurityXXX2005Dictionary.TokenTypeAttribute, XD.SecurityXXX2005Dictionary.Namespace, tokenTypeUri);
						}
						base.StrEntries[i].WriteContent(writer, keyIdentifierClause);
						writer.WriteEndElement();
						return;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StandardsManagerCannotWriteObject", new object[]
				{
					keyIdentifierClause.GetType()
				})));
			}

			// Token: 0x06001312 RID: 4882 RVA: 0x00051B00 File Offset: 0x0004FD00
			private string GetTokenTypeUri(SecurityTokenSerializer.StrEntry str, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				bool flag = this.EmitTokenType(str);
				if (flag)
				{
					string text;
					if (str is WSSecurityJan2004.LocalReferenceStrEntry)
					{
						text = (str as WSSecurityJan2004.LocalReferenceStrEntry).GetLocalTokenTypeUri(keyIdentifierClause);
						if (!(text == "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0") && !(text == "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1") && !(text == "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey") && !(text == "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ"))
						{
							text = null;
						}
					}
					else
					{
						text = str.GetTokenTypeUri();
					}
					return text;
				}
				return null;
			}

			// Token: 0x06001313 RID: 4883 RVA: 0x00051B70 File Offset: 0x0004FD70
			private bool EmitTokenType(SecurityTokenSerializer.StrEntry str)
			{
				bool result = false;
				if (str is WSSecurityJan2004.SamlJan2004KeyIdentifierStrEntry || str is WSSecurityXXX2005.EncryptedKeyHashStrEntry || str is WSSecurityXXX2005.SamlDirectStrEntry)
				{
					result = true;
				}
				else if (base.EmitBspRequiredAttributes && (str is WSSecurityJan2004.KerberosHashStrEntry || str is WSSecurityJan2004.LocalReferenceStrEntry))
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x02000287 RID: 647
		private class EncryptedKeyHashStrEntry : WSSecurityJan2004.KeyIdentifierStrEntry
		{
			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x06001314 RID: 4884 RVA: 0x00051BB6 File Offset: 0x0004FDB6
			protected override Type ClauseType
			{
				get
				{
					return typeof(EncryptedKeyHashIdentifierClause);
				}
			}

			// Token: 0x17000558 RID: 1368
			// (get) Token: 0x06001315 RID: 4885 RVA: 0x00051BC2 File Offset: 0x0004FDC2
			public override Type TokenType
			{
				get
				{
					return typeof(WrappedKeySecurityToken);
				}
			}

			// Token: 0x17000559 RID: 1369
			// (get) Token: 0x06001316 RID: 4886 RVA: 0x00051BCE File Offset: 0x0004FDCE
			protected override string ValueTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1";
				}
			}

			// Token: 0x06001317 RID: 4887 RVA: 0x00050ED0 File Offset: 0x0004F0D0
			public EncryptedKeyHashStrEntry(bool emitBspRequiredAttributes) : base(emitBspRequiredAttributes)
			{
			}

			// Token: 0x06001318 RID: 4888 RVA: 0x00051BD5 File Offset: 0x0004FDD5
			public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
			{
				return (tokenType == null || !(tokenType != "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey")) && base.CanReadClause(reader, tokenType);
			}

			// Token: 0x06001319 RID: 4889 RVA: 0x00051BF1 File Offset: 0x0004FDF1
			protected override SecurityKeyIdentifierClause CreateClause(byte[] bytes, byte[] derivationNonce, int derivationLength)
			{
				return new EncryptedKeyHashIdentifierClause(bytes, true, derivationNonce, derivationLength);
			}

			// Token: 0x0600131A RID: 4890 RVA: 0x000519DC File Offset: 0x0004FBDC
			public override string GetTokenTypeUri()
			{
				return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey";
			}
		}

		// Token: 0x02000288 RID: 648
		private class X509ThumbprintStrEntry : WSSecurityJan2004.KeyIdentifierStrEntry
		{
			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x0600131B RID: 4891 RVA: 0x00051BFC File Offset: 0x0004FDFC
			protected override Type ClauseType
			{
				get
				{
					return typeof(X509ThumbprintKeyIdentifierClause);
				}
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x0600131C RID: 4892 RVA: 0x0003E334 File Offset: 0x0003C534
			public override Type TokenType
			{
				get
				{
					return typeof(X509SecurityToken);
				}
			}

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x0600131D RID: 4893 RVA: 0x00051C08 File Offset: 0x0004FE08
			protected override string ValueTypeUri
			{
				get
				{
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1";
				}
			}

			// Token: 0x0600131E RID: 4894 RVA: 0x00050ED0 File Offset: 0x0004F0D0
			public X509ThumbprintStrEntry(bool emitBspRequiredAttributes) : base(emitBspRequiredAttributes)
			{
			}

			// Token: 0x0600131F RID: 4895 RVA: 0x00051C0F File Offset: 0x0004FE0F
			protected override SecurityKeyIdentifierClause CreateClause(byte[] bytes, byte[] derivationNonce, int derivationLength)
			{
				return new X509ThumbprintKeyIdentifierClause(bytes);
			}

			// Token: 0x06001320 RID: 4896 RVA: 0x00051C17 File Offset: 0x0004FE17
			public override string GetTokenTypeUri()
			{
				return XD.SecurityXXX2005Dictionary.ThumbprintSha1ValueType.Value;
			}
		}

		// Token: 0x02000289 RID: 649
		private class SamlDirectStrEntry : SecurityTokenSerializer.StrEntry
		{
			// Token: 0x06001321 RID: 4897 RVA: 0x00051C28 File Offset: 0x0004FE28
			public override bool CanReadClause(XmlDictionaryReader reader, string tokenType)
			{
				return !(tokenType != XD.SecurityXXX2005Dictionary.Saml20TokenType.Value) && reader.IsStartElement(XD.SecurityJan2004Dictionary.Reference, XD.SecurityJan2004Dictionary.Namespace);
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x00003459 File Offset: 0x00001659
			public override Type GetTokenType(SecurityKeyIdentifierClause clause)
			{
				return null;
			}

			// Token: 0x06001323 RID: 4899 RVA: 0x00051612 File Offset: 0x0004F812
			public override string GetTokenTypeUri()
			{
				return XD.SecurityXXX2005Dictionary.Saml20TokenType.Value;
			}

			// Token: 0x06001324 RID: 4900 RVA: 0x00051C60 File Offset: 0x0004FE60
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
				return new SamlAssertionDirectKeyIdentifierClause(attribute, derivationNone, derivationLength);
			}

			// Token: 0x06001325 RID: 4901 RVA: 0x00051CA4 File Offset: 0x0004FEA4
			public override bool SupportsCore(SecurityKeyIdentifierClause clause)
			{
				return typeof(SamlAssertionDirectKeyIdentifierClause).IsAssignableFrom(clause.GetType());
			}

			// Token: 0x06001326 RID: 4902 RVA: 0x00051CBC File Offset: 0x0004FEBC
			public override void WriteContent(XmlDictionaryWriter writer, SecurityKeyIdentifierClause clause)
			{
				SamlAssertionDirectKeyIdentifierClause samlAssertionDirectKeyIdentifierClause = clause as SamlAssertionDirectKeyIdentifierClause;
				writer.WriteStartElement(XD.SecurityJan2004Dictionary.Prefix.Value, XD.SecurityJan2004Dictionary.Reference, XD.SecurityJan2004Dictionary.Namespace);
				writer.WriteAttributeString(XD.SecurityJan2004Dictionary.URI, null, samlAssertionDirectKeyIdentifierClause.SamlUri);
				writer.WriteEndElement();
			}
		}
	}
}

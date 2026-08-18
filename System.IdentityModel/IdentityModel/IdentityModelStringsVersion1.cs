using System;

namespace System.IdentityModel
{
	// Token: 0x02000044 RID: 68
	internal class IdentityModelStringsVersion1 : IdentityModelStrings
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A837 File Offset: 0x00008A37
		public override int Count
		{
			get
			{
				return 279;
			}
		}

		// Token: 0x1700009D RID: 157
		public override string this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return "Algorithm";
				case 1:
					return "URI";
				case 2:
					return "Reference";
				case 3:
					return "Id";
				case 4:
					return "Transforms";
				case 5:
					return "Transform";
				case 6:
					return "DigestMethod";
				case 7:
					return "DigestValue";
				case 8:
					return "http://www.w3.org/2000/09/xmldsig#";
				case 9:
					return "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
				case 10:
					return "KeyInfo";
				case 11:
					return "Signature";
				case 12:
					return "SignedInfo";
				case 13:
					return "CanonicalizationMethod";
				case 14:
					return "SignatureMethod";
				case 15:
					return "SignatureValue";
				case 16:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
				case 17:
					return "Timestamp";
				case 18:
					return "Created";
				case 19:
					return "Expires";
				case 20:
					return "http://www.w3.org/2001/10/xml-exc-c14n#";
				case 21:
					return "PrefixList";
				case 22:
					return "InclusiveNamespaces";
				case 23:
					return "ec";
				case 24:
					return "Access";
				case 25:
					return "AccessDecision";
				case 26:
					return "Action";
				case 27:
					return "Advice";
				case 28:
					return "Assertion";
				case 29:
					return "AssertionID";
				case 30:
					return "AssertionIDReference";
				case 31:
					return "Attribute";
				case 32:
					return "AttributeName";
				case 33:
					return "AttributeNamespace";
				case 34:
					return "AttributeStatement";
				case 35:
					return "AttributeValue";
				case 36:
					return "Audience";
				case 37:
					return "AudienceRestrictionCondition";
				case 38:
					return "AuthenticationInstant";
				case 39:
					return "AuthenticationMethod";
				case 40:
					return "AuthenticationStatement";
				case 41:
					return "AuthorityBinding";
				case 42:
					return "AuthorityKind";
				case 43:
					return "AuthorizationDecisionStatement";
				case 44:
					return "Binding";
				case 45:
					return "Condition";
				case 46:
					return "Conditions";
				case 47:
					return "Decision";
				case 48:
					return "DoNotCacheCondition";
				case 49:
					return "Evidence";
				case 50:
					return "IssueInstant";
				case 51:
					return "Issuer";
				case 52:
					return "Location";
				case 53:
					return "MajorVersion";
				case 54:
					return "MinorVersion";
				case 55:
					return "urn:oasis:names:tc:SAML:1.0:assertion";
				case 56:
					return "NameIdentifier";
				case 57:
					return "Format";
				case 58:
					return "NameQualifier";
				case 59:
					return "Namespace";
				case 60:
					return "NotBefore";
				case 61:
					return "NotOnOrAfter";
				case 62:
					return "saml";
				case 63:
					return "Statement";
				case 64:
					return "Subject";
				case 65:
					return "SubjectConfirmation";
				case 66:
					return "SubjectConfirmationData";
				case 67:
					return "ConfirmationMethod";
				case 68:
					return "urn:oasis:names:tc:SAML:1.0:cm:holder-of-key";
				case 69:
					return "urn:oasis:names:tc:SAML:1.0:cm:sender-vouches";
				case 70:
					return "SubjectLocality";
				case 71:
					return "DNSAddress";
				case 72:
					return "IPAddress";
				case 73:
					return "SubjectStatement";
				case 74:
					return "urn:oasis:names:tc:SAML:1.0:am:unspecified";
				case 75:
					return "xmlns";
				case 76:
					return "Resource";
				case 77:
					return "UserName";
				case 78:
					return "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName";
				case 79:
					return "EmailName";
				case 80:
					return "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress";
				case 81:
					return "u";
				case 82:
					return "KeyName";
				case 83:
					return "Type";
				case 84:
					return "MgmtData";
				case 85:
					return "";
				case 86:
					return "KeyValue";
				case 87:
					return "RSAKeyValue";
				case 88:
					return "Modulus";
				case 89:
					return "Exponent";
				case 90:
					return "X509Data";
				case 91:
					return "X509IssuerSerial";
				case 92:
					return "X509IssuerName";
				case 93:
					return "X509SerialNumber";
				case 94:
					return "X509Certificate";
				case 95:
					return "http://www.w3.org/2001/04/xmlenc#aes128-cbc";
				case 96:
					return "http://www.w3.org/2001/04/xmlenc#kw-aes128";
				case 97:
					return "http://www.w3.org/2001/04/xmlenc#aes192-cbc";
				case 98:
					return "http://www.w3.org/2001/04/xmlenc#kw-aes192";
				case 99:
					return "http://www.w3.org/2001/04/xmlenc#aes256-cbc";
				case 100:
					return "http://www.w3.org/2001/04/xmlenc#kw-aes256";
				case 101:
					return "http://www.w3.org/2001/04/xmlenc#des-cbc";
				case 102:
					return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
				case 103:
					return "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
				case 104:
					return "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
				case 105:
					return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
				case 106:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";
				case 107:
					return "http://www.w3.org/2001/04/xmlenc#ripemd160";
				case 108:
					return "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";
				case 109:
					return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
				case 110:
					return "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
				case 111:
					return "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
				case 112:
					return "http://www.w3.org/2000/09/xmldsig#sha1";
				case 113:
					return "http://www.w3.org/2001/04/xmlenc#sha256";
				case 114:
					return "http://www.w3.org/2001/04/xmlenc#sha512";
				case 115:
					return "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";
				case 116:
					return "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
				case 117:
					return "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap";
				case 118:
					return "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap";
				case 119:
					return "o";
				case 120:
					return "Nonce";
				case 121:
					return "Password";
				case 122:
					return "PasswordText";
				case 123:
					return "Username";
				case 124:
					return "UsernameToken";
				case 125:
					return "BinarySecurityToken";
				case 126:
					return "EncodingType";
				case 127:
					return "KeyIdentifier";
				case 128:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";
				case 129:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";
				case 130:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text";
				case 131:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier";
				case 132:
					return "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ";
				case 133:
					return "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510";
				case 134:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID";
				case 135:
					return "http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license";
				case 136:
					return "FailedAuthentication";
				case 137:
					return "InvalidSecurityToken";
				case 138:
					return "InvalidSecurity";
				case 139:
					return "SecurityTokenReference";
				case 140:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
				case 141:
					return "Security";
				case 142:
					return "ValueType";
				case 143:
					return "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1";
				case 144:
					return "k";
				case 145:
					return "SignatureConfirmation";
				case 146:
					return "Value";
				case 147:
					return "TokenType";
				case 148:
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1";
				case 149:
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey";
				case 150:
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1";
				case 151:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";
				case 152:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";
				case 153:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID";
				case 154:
					return "EncryptedHeader";
				case 155:
					return "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd";
				case 156:
					return "http://www.w3.org/2001/04/xmlenc#";
				case 157:
					return "DataReference";
				case 158:
					return "EncryptedData";
				case 159:
					return "EncryptionMethod";
				case 160:
					return "CipherData";
				case 161:
					return "CipherValue";
				case 162:
					return "ReferenceList";
				case 163:
					return "Encoding";
				case 164:
					return "MimeType";
				case 165:
					return "CarriedKeyName";
				case 166:
					return "Recipient";
				case 167:
					return "EncryptedKey";
				case 168:
					return "KeyReference";
				case 169:
					return "e";
				case 170:
					return "http://www.w3.org/2001/04/xmlenc#Element";
				case 171:
					return "http://www.w3.org/2001/04/xmlenc#Content";
				case 172:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc";
				case 173:
					return "DerivedKeyToken";
				case 174:
					return "Length";
				case 175:
					return "SecurityContextToken";
				case 176:
					return "Generation";
				case 177:
					return "Label";
				case 178:
					return "Offset";
				case 179:
					return "Properties";
				case 180:
					return "Identifier";
				case 181:
					return "Cookie";
				case 182:
					return "RenewNeeded";
				case 183:
					return "BadContextToken";
				case 184:
					return "c";
				case 185:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc/dk";
				case 186:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc/sct";
				case 187:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT";
				case 188:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT";
				case 189:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew";
				case 190:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew";
				case 191:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel";
				case 192:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel";
				case 193:
					return "RequestSecurityTokenResponseCollection";
				case 194:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust";
				case 195:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust#BinarySecret";
				case 196:
					return "AUTH-HASH";
				case 197:
					return "RequestSecurityTokenResponse";
				case 198:
					return "KeySize";
				case 199:
					return "RequestedTokenReference";
				case 200:
					return "AppliesTo";
				case 201:
					return "Authenticator";
				case 202:
					return "CombinedHash";
				case 203:
					return "BinaryExchange";
				case 204:
					return "Lifetime";
				case 205:
					return "RequestedSecurityToken";
				case 206:
					return "Entropy";
				case 207:
					return "RequestedProofToken";
				case 208:
					return "ComputedKey";
				case 209:
					return "RequestSecurityToken";
				case 210:
					return "RequestType";
				case 211:
					return "Context";
				case 212:
					return "BinarySecret";
				case 213:
					return "http://schemas.microsoft.com/net/2004/07/secext/WS-SPNego";
				case 214:
					return "http://schemas.microsoft.com/net/2004/07/secext/TLSNego";
				case 215:
					return "t";
				case 216:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";
				case 217:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue";
				case 218:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";
				case 219:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";
				case 220:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";
				case 221:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce";
				case 222:
					return "RenewTarget";
				case 223:
					return "CancelTarget";
				case 224:
					return "RequestedTokenCancelled";
				case 225:
					return "RequestedAttachedReference";
				case 226:
					return "RequestedUnattachedReference";
				case 227:
					return "IssuedTokens";
				case 228:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Renew";
				case 229:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel";
				case 230:
					return "KeyType";
				case 231:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey";
				case 232:
					return "Claims";
				case 233:
					return "InvalidRequest";
				case 234:
					return "UseKey";
				case 235:
					return "SignWith";
				case 236:
					return "EncryptWith";
				case 237:
					return "EncryptionAlgorithm";
				case 238:
					return "CanonicalizationAlgorithm";
				case 239:
					return "ComputedKeyAlgorithm";
				case 240:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/spnego";
				case 241:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego";
				case 242:
					return "trust";
				case 243:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue";
				case 244:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue";
				case 245:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue";
				case 246:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/AsymmetricKey";
				case 247:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey";
				case 248:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Nonce";
				case 249:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1";
				case 250:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey";
				case 251:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512";
				case 252:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512#BinarySecret";
				case 253:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal";
				case 254:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew";
				case 255:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew";
				case 256:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal";
				case 257:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel";
				case 258:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel";
				case 259:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal";
				case 260:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew";
				case 261:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Cancel";
				case 262:
					return "KeyWrapAlgorithm";
				case 263:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer";
				case 264:
					return "SecondaryParameters";
				case 265:
					return "Dialect";
				case 266:
					return "http://schemas.xmlsoap.org/ws/2005/05/identity";
				case 267:
					return "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";
				case 268:
					return "sc";
				case 269:
					return "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk";
				case 270:
					return "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct";
				case 271:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT";
				case 272:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT";
				case 273:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Renew";
				case 274:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Renew";
				case 275:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Cancel";
				case 276:
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Cancel";
				case 277:
					return "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512";
				case 278:
					return "Instance";
				default:
					return null;
				}
			}
		}

		// Token: 0x0400017C RID: 380
		public const string String0 = "Algorithm";

		// Token: 0x0400017D RID: 381
		public const string String1 = "URI";

		// Token: 0x0400017E RID: 382
		public const string String2 = "Reference";

		// Token: 0x0400017F RID: 383
		public const string String3 = "Id";

		// Token: 0x04000180 RID: 384
		public const string String4 = "Transforms";

		// Token: 0x04000181 RID: 385
		public const string String5 = "Transform";

		// Token: 0x04000182 RID: 386
		public const string String6 = "DigestMethod";

		// Token: 0x04000183 RID: 387
		public const string String7 = "DigestValue";

		// Token: 0x04000184 RID: 388
		public const string String8 = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04000185 RID: 389
		public const string String9 = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x04000186 RID: 390
		public const string String10 = "KeyInfo";

		// Token: 0x04000187 RID: 391
		public const string String11 = "Signature";

		// Token: 0x04000188 RID: 392
		public const string String12 = "SignedInfo";

		// Token: 0x04000189 RID: 393
		public const string String13 = "CanonicalizationMethod";

		// Token: 0x0400018A RID: 394
		public const string String14 = "SignatureMethod";

		// Token: 0x0400018B RID: 395
		public const string String15 = "SignatureValue";

		// Token: 0x0400018C RID: 396
		public const string String16 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		// Token: 0x0400018D RID: 397
		public const string String17 = "Timestamp";

		// Token: 0x0400018E RID: 398
		public const string String18 = "Created";

		// Token: 0x0400018F RID: 399
		public const string String19 = "Expires";

		// Token: 0x04000190 RID: 400
		public const string String20 = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04000191 RID: 401
		public const string String21 = "PrefixList";

		// Token: 0x04000192 RID: 402
		public const string String22 = "InclusiveNamespaces";

		// Token: 0x04000193 RID: 403
		public const string String23 = "ec";

		// Token: 0x04000194 RID: 404
		public const string String24 = "Access";

		// Token: 0x04000195 RID: 405
		public const string String25 = "AccessDecision";

		// Token: 0x04000196 RID: 406
		public const string String26 = "Action";

		// Token: 0x04000197 RID: 407
		public const string String27 = "Advice";

		// Token: 0x04000198 RID: 408
		public const string String28 = "Assertion";

		// Token: 0x04000199 RID: 409
		public const string String29 = "AssertionID";

		// Token: 0x0400019A RID: 410
		public const string String30 = "AssertionIDReference";

		// Token: 0x0400019B RID: 411
		public const string String31 = "Attribute";

		// Token: 0x0400019C RID: 412
		public const string String32 = "AttributeName";

		// Token: 0x0400019D RID: 413
		public const string String33 = "AttributeNamespace";

		// Token: 0x0400019E RID: 414
		public const string String34 = "AttributeStatement";

		// Token: 0x0400019F RID: 415
		public const string String35 = "AttributeValue";

		// Token: 0x040001A0 RID: 416
		public const string String36 = "Audience";

		// Token: 0x040001A1 RID: 417
		public const string String37 = "AudienceRestrictionCondition";

		// Token: 0x040001A2 RID: 418
		public const string String38 = "AuthenticationInstant";

		// Token: 0x040001A3 RID: 419
		public const string String39 = "AuthenticationMethod";

		// Token: 0x040001A4 RID: 420
		public const string String40 = "AuthenticationStatement";

		// Token: 0x040001A5 RID: 421
		public const string String41 = "AuthorityBinding";

		// Token: 0x040001A6 RID: 422
		public const string String42 = "AuthorityKind";

		// Token: 0x040001A7 RID: 423
		public const string String43 = "AuthorizationDecisionStatement";

		// Token: 0x040001A8 RID: 424
		public const string String44 = "Binding";

		// Token: 0x040001A9 RID: 425
		public const string String45 = "Condition";

		// Token: 0x040001AA RID: 426
		public const string String46 = "Conditions";

		// Token: 0x040001AB RID: 427
		public const string String47 = "Decision";

		// Token: 0x040001AC RID: 428
		public const string String48 = "DoNotCacheCondition";

		// Token: 0x040001AD RID: 429
		public const string String49 = "Evidence";

		// Token: 0x040001AE RID: 430
		public const string String50 = "IssueInstant";

		// Token: 0x040001AF RID: 431
		public const string String51 = "Issuer";

		// Token: 0x040001B0 RID: 432
		public const string String52 = "Location";

		// Token: 0x040001B1 RID: 433
		public const string String53 = "MajorVersion";

		// Token: 0x040001B2 RID: 434
		public const string String54 = "MinorVersion";

		// Token: 0x040001B3 RID: 435
		public const string String55 = "urn:oasis:names:tc:SAML:1.0:assertion";

		// Token: 0x040001B4 RID: 436
		public const string String56 = "NameIdentifier";

		// Token: 0x040001B5 RID: 437
		public const string String57 = "Format";

		// Token: 0x040001B6 RID: 438
		public const string String58 = "NameQualifier";

		// Token: 0x040001B7 RID: 439
		public const string String59 = "Namespace";

		// Token: 0x040001B8 RID: 440
		public const string String60 = "NotBefore";

		// Token: 0x040001B9 RID: 441
		public const string String61 = "NotOnOrAfter";

		// Token: 0x040001BA RID: 442
		public const string String62 = "saml";

		// Token: 0x040001BB RID: 443
		public const string String63 = "Statement";

		// Token: 0x040001BC RID: 444
		public const string String64 = "Subject";

		// Token: 0x040001BD RID: 445
		public const string String65 = "SubjectConfirmation";

		// Token: 0x040001BE RID: 446
		public const string String66 = "SubjectConfirmationData";

		// Token: 0x040001BF RID: 447
		public const string String67 = "ConfirmationMethod";

		// Token: 0x040001C0 RID: 448
		public const string String68 = "urn:oasis:names:tc:SAML:1.0:cm:holder-of-key";

		// Token: 0x040001C1 RID: 449
		public const string String69 = "urn:oasis:names:tc:SAML:1.0:cm:sender-vouches";

		// Token: 0x040001C2 RID: 450
		public const string String70 = "SubjectLocality";

		// Token: 0x040001C3 RID: 451
		public const string String71 = "DNSAddress";

		// Token: 0x040001C4 RID: 452
		public const string String72 = "IPAddress";

		// Token: 0x040001C5 RID: 453
		public const string String73 = "SubjectStatement";

		// Token: 0x040001C6 RID: 454
		public const string String74 = "urn:oasis:names:tc:SAML:1.0:am:unspecified";

		// Token: 0x040001C7 RID: 455
		public const string String75 = "xmlns";

		// Token: 0x040001C8 RID: 456
		public const string String76 = "Resource";

		// Token: 0x040001C9 RID: 457
		public const string String77 = "UserName";

		// Token: 0x040001CA RID: 458
		public const string String78 = "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName";

		// Token: 0x040001CB RID: 459
		public const string String79 = "EmailName";

		// Token: 0x040001CC RID: 460
		public const string String80 = "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress";

		// Token: 0x040001CD RID: 461
		public const string String81 = "u";

		// Token: 0x040001CE RID: 462
		public const string String82 = "KeyName";

		// Token: 0x040001CF RID: 463
		public const string String83 = "Type";

		// Token: 0x040001D0 RID: 464
		public const string String84 = "MgmtData";

		// Token: 0x040001D1 RID: 465
		public const string String85 = "";

		// Token: 0x040001D2 RID: 466
		public const string String86 = "KeyValue";

		// Token: 0x040001D3 RID: 467
		public const string String87 = "RSAKeyValue";

		// Token: 0x040001D4 RID: 468
		public const string String88 = "Modulus";

		// Token: 0x040001D5 RID: 469
		public const string String89 = "Exponent";

		// Token: 0x040001D6 RID: 470
		public const string String90 = "X509Data";

		// Token: 0x040001D7 RID: 471
		public const string String91 = "X509IssuerSerial";

		// Token: 0x040001D8 RID: 472
		public const string String92 = "X509IssuerName";

		// Token: 0x040001D9 RID: 473
		public const string String93 = "X509SerialNumber";

		// Token: 0x040001DA RID: 474
		public const string String94 = "X509Certificate";

		// Token: 0x040001DB RID: 475
		public const string String95 = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		// Token: 0x040001DC RID: 476
		public const string String96 = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		// Token: 0x040001DD RID: 477
		public const string String97 = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		// Token: 0x040001DE RID: 478
		public const string String98 = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		// Token: 0x040001DF RID: 479
		public const string String99 = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x040001E0 RID: 480
		public const string String100 = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		// Token: 0x040001E1 RID: 481
		public const string String101 = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		// Token: 0x040001E2 RID: 482
		public const string String102 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x040001E3 RID: 483
		public const string String103 = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x040001E4 RID: 484
		public const string String104 = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x040001E5 RID: 485
		public const string String105 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x040001E6 RID: 486
		public const string String106 = "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";

		// Token: 0x040001E7 RID: 487
		public const string String107 = "http://www.w3.org/2001/04/xmlenc#ripemd160";

		// Token: 0x040001E8 RID: 488
		public const string String108 = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		// Token: 0x040001E9 RID: 489
		public const string String109 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x040001EA RID: 490
		public const string String110 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x040001EB RID: 491
		public const string String111 = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		// Token: 0x040001EC RID: 492
		public const string String112 = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x040001ED RID: 493
		public const string String113 = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040001EE RID: 494
		public const string String114 = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x040001EF RID: 495
		public const string String115 = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		// Token: 0x040001F0 RID: 496
		public const string String116 = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		// Token: 0x040001F1 RID: 497
		public const string String117 = "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap";

		// Token: 0x040001F2 RID: 498
		public const string String118 = "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap";

		// Token: 0x040001F3 RID: 499
		public const string String119 = "o";

		// Token: 0x040001F4 RID: 500
		public const string String120 = "Nonce";

		// Token: 0x040001F5 RID: 501
		public const string String121 = "Password";

		// Token: 0x040001F6 RID: 502
		public const string String122 = "PasswordText";

		// Token: 0x040001F7 RID: 503
		public const string String123 = "Username";

		// Token: 0x040001F8 RID: 504
		public const string String124 = "UsernameToken";

		// Token: 0x040001F9 RID: 505
		public const string String125 = "BinarySecurityToken";

		// Token: 0x040001FA RID: 506
		public const string String126 = "EncodingType";

		// Token: 0x040001FB RID: 507
		public const string String127 = "KeyIdentifier";

		// Token: 0x040001FC RID: 508
		public const string String128 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

		// Token: 0x040001FD RID: 509
		public const string String129 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

		// Token: 0x040001FE RID: 510
		public const string String130 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text";

		// Token: 0x040001FF RID: 511
		public const string String131 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier";

		// Token: 0x04000200 RID: 512
		public const string String132 = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ";

		// Token: 0x04000201 RID: 513
		public const string String133 = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510";

		// Token: 0x04000202 RID: 514
		public const string String134 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID";

		// Token: 0x04000203 RID: 515
		public const string String135 = "http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license";

		// Token: 0x04000204 RID: 516
		public const string String136 = "FailedAuthentication";

		// Token: 0x04000205 RID: 517
		public const string String137 = "InvalidSecurityToken";

		// Token: 0x04000206 RID: 518
		public const string String138 = "InvalidSecurity";

		// Token: 0x04000207 RID: 519
		public const string String139 = "SecurityTokenReference";

		// Token: 0x04000208 RID: 520
		public const string String140 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x04000209 RID: 521
		public const string String141 = "Security";

		// Token: 0x0400020A RID: 522
		public const string String142 = "ValueType";

		// Token: 0x0400020B RID: 523
		public const string String143 = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1";

		// Token: 0x0400020C RID: 524
		public const string String144 = "k";

		// Token: 0x0400020D RID: 525
		public const string String145 = "SignatureConfirmation";

		// Token: 0x0400020E RID: 526
		public const string String146 = "Value";

		// Token: 0x0400020F RID: 527
		public const string String147 = "TokenType";

		// Token: 0x04000210 RID: 528
		public const string String148 = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1";

		// Token: 0x04000211 RID: 529
		public const string String149 = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey";

		// Token: 0x04000212 RID: 530
		public const string String150 = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1";

		// Token: 0x04000213 RID: 531
		public const string String151 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";

		// Token: 0x04000214 RID: 532
		public const string String152 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";

		// Token: 0x04000215 RID: 533
		public const string String153 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID";

		// Token: 0x04000216 RID: 534
		public const string String154 = "EncryptedHeader";

		// Token: 0x04000217 RID: 535
		public const string String155 = "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd";

		// Token: 0x04000218 RID: 536
		public const string String156 = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x04000219 RID: 537
		public const string String157 = "DataReference";

		// Token: 0x0400021A RID: 538
		public const string String158 = "EncryptedData";

		// Token: 0x0400021B RID: 539
		public const string String159 = "EncryptionMethod";

		// Token: 0x0400021C RID: 540
		public const string String160 = "CipherData";

		// Token: 0x0400021D RID: 541
		public const string String161 = "CipherValue";

		// Token: 0x0400021E RID: 542
		public const string String162 = "ReferenceList";

		// Token: 0x0400021F RID: 543
		public const string String163 = "Encoding";

		// Token: 0x04000220 RID: 544
		public const string String164 = "MimeType";

		// Token: 0x04000221 RID: 545
		public const string String165 = "CarriedKeyName";

		// Token: 0x04000222 RID: 546
		public const string String166 = "Recipient";

		// Token: 0x04000223 RID: 547
		public const string String167 = "EncryptedKey";

		// Token: 0x04000224 RID: 548
		public const string String168 = "KeyReference";

		// Token: 0x04000225 RID: 549
		public const string String169 = "e";

		// Token: 0x04000226 RID: 550
		public const string String170 = "http://www.w3.org/2001/04/xmlenc#Element";

		// Token: 0x04000227 RID: 551
		public const string String171 = "http://www.w3.org/2001/04/xmlenc#Content";

		// Token: 0x04000228 RID: 552
		public const string String172 = "http://schemas.xmlsoap.org/ws/2005/02/sc";

		// Token: 0x04000229 RID: 553
		public const string String173 = "DerivedKeyToken";

		// Token: 0x0400022A RID: 554
		public const string String174 = "Length";

		// Token: 0x0400022B RID: 555
		public const string String175 = "SecurityContextToken";

		// Token: 0x0400022C RID: 556
		public const string String176 = "Generation";

		// Token: 0x0400022D RID: 557
		public const string String177 = "Label";

		// Token: 0x0400022E RID: 558
		public const string String178 = "Offset";

		// Token: 0x0400022F RID: 559
		public const string String179 = "Properties";

		// Token: 0x04000230 RID: 560
		public const string String180 = "Identifier";

		// Token: 0x04000231 RID: 561
		public const string String181 = "Cookie";

		// Token: 0x04000232 RID: 562
		public const string String182 = "RenewNeeded";

		// Token: 0x04000233 RID: 563
		public const string String183 = "BadContextToken";

		// Token: 0x04000234 RID: 564
		public const string String184 = "c";

		// Token: 0x04000235 RID: 565
		public const string String185 = "http://schemas.xmlsoap.org/ws/2005/02/sc/dk";

		// Token: 0x04000236 RID: 566
		public const string String186 = "http://schemas.xmlsoap.org/ws/2005/02/sc/sct";

		// Token: 0x04000237 RID: 567
		public const string String187 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT";

		// Token: 0x04000238 RID: 568
		public const string String188 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT";

		// Token: 0x04000239 RID: 569
		public const string String189 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew";

		// Token: 0x0400023A RID: 570
		public const string String190 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew";

		// Token: 0x0400023B RID: 571
		public const string String191 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel";

		// Token: 0x0400023C RID: 572
		public const string String192 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel";

		// Token: 0x0400023D RID: 573
		public const string String193 = "RequestSecurityTokenResponseCollection";

		// Token: 0x0400023E RID: 574
		public const string String194 = "http://schemas.xmlsoap.org/ws/2005/02/trust";

		// Token: 0x0400023F RID: 575
		public const string String195 = "http://schemas.xmlsoap.org/ws/2005/02/trust#BinarySecret";

		// Token: 0x04000240 RID: 576
		public const string String196 = "AUTH-HASH";

		// Token: 0x04000241 RID: 577
		public const string String197 = "RequestSecurityTokenResponse";

		// Token: 0x04000242 RID: 578
		public const string String198 = "KeySize";

		// Token: 0x04000243 RID: 579
		public const string String199 = "RequestedTokenReference";

		// Token: 0x04000244 RID: 580
		public const string String200 = "AppliesTo";

		// Token: 0x04000245 RID: 581
		public const string String201 = "Authenticator";

		// Token: 0x04000246 RID: 582
		public const string String202 = "CombinedHash";

		// Token: 0x04000247 RID: 583
		public const string String203 = "BinaryExchange";

		// Token: 0x04000248 RID: 584
		public const string String204 = "Lifetime";

		// Token: 0x04000249 RID: 585
		public const string String205 = "RequestedSecurityToken";

		// Token: 0x0400024A RID: 586
		public const string String206 = "Entropy";

		// Token: 0x0400024B RID: 587
		public const string String207 = "RequestedProofToken";

		// Token: 0x0400024C RID: 588
		public const string String208 = "ComputedKey";

		// Token: 0x0400024D RID: 589
		public const string String209 = "RequestSecurityToken";

		// Token: 0x0400024E RID: 590
		public const string String210 = "RequestType";

		// Token: 0x0400024F RID: 591
		public const string String211 = "Context";

		// Token: 0x04000250 RID: 592
		public const string String212 = "BinarySecret";

		// Token: 0x04000251 RID: 593
		public const string String213 = "http://schemas.microsoft.com/net/2004/07/secext/WS-SPNego";

		// Token: 0x04000252 RID: 594
		public const string String214 = "http://schemas.microsoft.com/net/2004/07/secext/TLSNego";

		// Token: 0x04000253 RID: 595
		public const string String215 = "t";

		// Token: 0x04000254 RID: 596
		public const string String216 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";

		// Token: 0x04000255 RID: 597
		public const string String217 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue";

		// Token: 0x04000256 RID: 598
		public const string String218 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";

		// Token: 0x04000257 RID: 599
		public const string String219 = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";

		// Token: 0x04000258 RID: 600
		public const string String220 = "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";

		// Token: 0x04000259 RID: 601
		public const string String221 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce";

		// Token: 0x0400025A RID: 602
		public const string String222 = "RenewTarget";

		// Token: 0x0400025B RID: 603
		public const string String223 = "CancelTarget";

		// Token: 0x0400025C RID: 604
		public const string String224 = "RequestedTokenCancelled";

		// Token: 0x0400025D RID: 605
		public const string String225 = "RequestedAttachedReference";

		// Token: 0x0400025E RID: 606
		public const string String226 = "RequestedUnattachedReference";

		// Token: 0x0400025F RID: 607
		public const string String227 = "IssuedTokens";

		// Token: 0x04000260 RID: 608
		public const string String228 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Renew";

		// Token: 0x04000261 RID: 609
		public const string String229 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel";

		// Token: 0x04000262 RID: 610
		public const string String230 = "KeyType";

		// Token: 0x04000263 RID: 611
		public const string String231 = "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey";

		// Token: 0x04000264 RID: 612
		public const string String232 = "Claims";

		// Token: 0x04000265 RID: 613
		public const string String233 = "InvalidRequest";

		// Token: 0x04000266 RID: 614
		public const string String234 = "UseKey";

		// Token: 0x04000267 RID: 615
		public const string String235 = "SignWith";

		// Token: 0x04000268 RID: 616
		public const string String236 = "EncryptWith";

		// Token: 0x04000269 RID: 617
		public const string String237 = "EncryptionAlgorithm";

		// Token: 0x0400026A RID: 618
		public const string String238 = "CanonicalizationAlgorithm";

		// Token: 0x0400026B RID: 619
		public const string String239 = "ComputedKeyAlgorithm";

		// Token: 0x0400026C RID: 620
		public const string String240 = "http://schemas.xmlsoap.org/ws/2005/02/trust/spnego";

		// Token: 0x0400026D RID: 621
		public const string String241 = "http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego";

		// Token: 0x0400026E RID: 622
		public const string String242 = "trust";

		// Token: 0x0400026F RID: 623
		public const string String243 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue";

		// Token: 0x04000270 RID: 624
		public const string String244 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue";

		// Token: 0x04000271 RID: 625
		public const string String245 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue";

		// Token: 0x04000272 RID: 626
		public const string String246 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/AsymmetricKey";

		// Token: 0x04000273 RID: 627
		public const string String247 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey";

		// Token: 0x04000274 RID: 628
		public const string String248 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Nonce";

		// Token: 0x04000275 RID: 629
		public const string String249 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1";

		// Token: 0x04000276 RID: 630
		public const string String250 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey";

		// Token: 0x04000277 RID: 631
		public const string String251 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512";

		// Token: 0x04000278 RID: 632
		public const string String252 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512#BinarySecret";

		// Token: 0x04000279 RID: 633
		public const string String253 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal";

		// Token: 0x0400027A RID: 634
		public const string String254 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew";

		// Token: 0x0400027B RID: 635
		public const string String255 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew";

		// Token: 0x0400027C RID: 636
		public const string String256 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal";

		// Token: 0x0400027D RID: 637
		public const string String257 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel";

		// Token: 0x0400027E RID: 638
		public const string String258 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel";

		// Token: 0x0400027F RID: 639
		public const string String259 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal";

		// Token: 0x04000280 RID: 640
		public const string String260 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew";

		// Token: 0x04000281 RID: 641
		public const string String261 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Cancel";

		// Token: 0x04000282 RID: 642
		public const string String262 = "KeyWrapAlgorithm";

		// Token: 0x04000283 RID: 643
		public const string String263 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer";

		// Token: 0x04000284 RID: 644
		public const string String264 = "SecondaryParameters";

		// Token: 0x04000285 RID: 645
		public const string String265 = "Dialect";

		// Token: 0x04000286 RID: 646
		public const string String266 = "http://schemas.xmlsoap.org/ws/2005/05/identity";

		// Token: 0x04000287 RID: 647
		public const string String267 = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";

		// Token: 0x04000288 RID: 648
		public const string String268 = "sc";

		// Token: 0x04000289 RID: 649
		public const string String269 = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk";

		// Token: 0x0400028A RID: 650
		public const string String270 = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct";

		// Token: 0x0400028B RID: 651
		public const string String271 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT";

		// Token: 0x0400028C RID: 652
		public const string String272 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT";

		// Token: 0x0400028D RID: 653
		public const string String273 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Renew";

		// Token: 0x0400028E RID: 654
		public const string String274 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Renew";

		// Token: 0x0400028F RID: 655
		public const string String275 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Cancel";

		// Token: 0x04000290 RID: 656
		public const string String276 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Cancel";

		// Token: 0x04000291 RID: 657
		public const string String277 = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512";

		// Token: 0x04000292 RID: 658
		public const string String278 = "Instance";
	}
}

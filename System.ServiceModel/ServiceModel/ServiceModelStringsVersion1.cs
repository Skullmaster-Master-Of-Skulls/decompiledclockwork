using System;

namespace System.ServiceModel
{
	// Token: 0x02000051 RID: 81
	internal class ServiceModelStringsVersion1 : ServiceModelStrings
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000B38B File Offset: 0x0000958B
		public override int Count
		{
			get
			{
				return 487;
			}
		}

		// Token: 0x17000069 RID: 105
		public override string this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return "mustUnderstand";
				case 1:
					return "Envelope";
				case 2:
					return "http://www.w3.org/2003/05/soap-envelope";
				case 3:
					return "http://www.w3.org/2005/08/addressing";
				case 4:
					return "Header";
				case 5:
					return "Action";
				case 6:
					return "To";
				case 7:
					return "Body";
				case 8:
					return "Algorithm";
				case 9:
					return "RelatesTo";
				case 10:
					return "http://www.w3.org/2005/08/addressing/anonymous";
				case 11:
					return "URI";
				case 12:
					return "Reference";
				case 13:
					return "MessageID";
				case 14:
					return "Id";
				case 15:
					return "Identifier";
				case 16:
					return "http://schemas.xmlsoap.org/ws/2005/02/rm";
				case 17:
					return "Transforms";
				case 18:
					return "Transform";
				case 19:
					return "DigestMethod";
				case 20:
					return "DigestValue";
				case 21:
					return "Address";
				case 22:
					return "ReplyTo";
				case 23:
					return "SequenceAcknowledgement";
				case 24:
					return "AcknowledgementRange";
				case 25:
					return "Upper";
				case 26:
					return "Lower";
				case 27:
					return "BufferRemaining";
				case 28:
					return "http://schemas.microsoft.com/ws/2006/05/rm";
				case 29:
					return "http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement";
				case 30:
					return "SecurityTokenReference";
				case 31:
					return "Sequence";
				case 32:
					return "MessageNumber";
				case 33:
					return "http://www.w3.org/2000/09/xmldsig#";
				case 34:
					return "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
				case 35:
					return "KeyInfo";
				case 36:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
				case 37:
					return "http://www.w3.org/2001/04/xmlenc#";
				case 38:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc";
				case 39:
					return "DerivedKeyToken";
				case 40:
					return "Nonce";
				case 41:
					return "Signature";
				case 42:
					return "SignedInfo";
				case 43:
					return "CanonicalizationMethod";
				case 44:
					return "SignatureMethod";
				case 45:
					return "SignatureValue";
				case 46:
					return "DataReference";
				case 47:
					return "EncryptedData";
				case 48:
					return "EncryptionMethod";
				case 49:
					return "CipherData";
				case 50:
					return "CipherValue";
				case 51:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
				case 52:
					return "Security";
				case 53:
					return "Timestamp";
				case 54:
					return "Created";
				case 55:
					return "Expires";
				case 56:
					return "Length";
				case 57:
					return "ReferenceList";
				case 58:
					return "ValueType";
				case 59:
					return "Type";
				case 60:
					return "EncryptedHeader";
				case 61:
					return "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd";
				case 62:
					return "RequestSecurityTokenResponseCollection";
				case 63:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust";
				case 64:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust#BinarySecret";
				case 65:
					return "http://schemas.microsoft.com/ws/2006/02/transactions";
				case 66:
					return "s";
				case 67:
					return "Fault";
				case 68:
					return "MustUnderstand";
				case 69:
					return "role";
				case 70:
					return "relay";
				case 71:
					return "Code";
				case 72:
					return "Reason";
				case 73:
					return "Text";
				case 74:
					return "Node";
				case 75:
					return "Role";
				case 76:
					return "Detail";
				case 77:
					return "Value";
				case 78:
					return "Subcode";
				case 79:
					return "NotUnderstood";
				case 80:
					return "qname";
				case 81:
					return "";
				case 82:
					return "From";
				case 83:
					return "FaultTo";
				case 84:
					return "EndpointReference";
				case 85:
					return "PortType";
				case 86:
					return "ServiceName";
				case 87:
					return "PortName";
				case 88:
					return "ReferenceProperties";
				case 89:
					return "RelationshipType";
				case 90:
					return "Reply";
				case 91:
					return "a";
				case 92:
					return "http://schemas.xmlsoap.org/ws/2006/02/addressingidentity";
				case 93:
					return "Identity";
				case 94:
					return "Spn";
				case 95:
					return "Upn";
				case 96:
					return "Rsa";
				case 97:
					return "Dns";
				case 98:
					return "X509v3Certificate";
				case 99:
					return "http://www.w3.org/2005/08/addressing/fault";
				case 100:
					return "ReferenceParameters";
				case 101:
					return "IsReferenceParameter";
				case 102:
					return "http://www.w3.org/2005/08/addressing/reply";
				case 103:
					return "http://www.w3.org/2005/08/addressing/none";
				case 104:
					return "Metadata";
				case 105:
					return "http://schemas.xmlsoap.org/ws/2004/08/addressing";
				case 106:
					return "http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous";
				case 107:
					return "http://schemas.xmlsoap.org/ws/2004/08/addressing/fault";
				case 108:
					return "http://schemas.xmlsoap.org/ws/2004/06/addressingex";
				case 109:
					return "RedirectTo";
				case 110:
					return "Via";
				case 111:
					return "http://www.w3.org/2001/10/xml-exc-c14n#";
				case 112:
					return "PrefixList";
				case 113:
					return "InclusiveNamespaces";
				case 114:
					return "ec";
				case 115:
					return "SecurityContextToken";
				case 116:
					return "Generation";
				case 117:
					return "Label";
				case 118:
					return "Offset";
				case 119:
					return "Properties";
				case 120:
					return "Cookie";
				case 121:
					return "wsc";
				case 122:
					return "http://schemas.xmlsoap.org/ws/2004/04/sc";
				case 123:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/sc/dk";
				case 124:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/sc/sct";
				case 125:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/SCT";
				case 126:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/SCT";
				case 127:
					return "RenewNeeded";
				case 128:
					return "BadContextToken";
				case 129:
					return "c";
				case 130:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc/dk";
				case 131:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc/sct";
				case 132:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT";
				case 133:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT";
				case 134:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew";
				case 135:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew";
				case 136:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel";
				case 137:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel";
				case 138:
					return "http://www.w3.org/2001/04/xmlenc#aes128-cbc";
				case 139:
					return "http://www.w3.org/2001/04/xmlenc#kw-aes128";
				case 140:
					return "http://www.w3.org/2001/04/xmlenc#aes192-cbc";
				case 141:
					return "http://www.w3.org/2001/04/xmlenc#kw-aes192";
				case 142:
					return "http://www.w3.org/2001/04/xmlenc#aes256-cbc";
				case 143:
					return "http://www.w3.org/2001/04/xmlenc#kw-aes256";
				case 144:
					return "http://www.w3.org/2001/04/xmlenc#des-cbc";
				case 145:
					return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
				case 146:
					return "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
				case 147:
					return "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
				case 148:
					return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
				case 149:
					return "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";
				case 150:
					return "http://www.w3.org/2001/04/xmlenc#ripemd160";
				case 151:
					return "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";
				case 152:
					return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
				case 153:
					return "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
				case 154:
					return "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
				case 155:
					return "http://www.w3.org/2000/09/xmldsig#sha1";
				case 156:
					return "http://www.w3.org/2001/04/xmlenc#sha256";
				case 157:
					return "http://www.w3.org/2001/04/xmlenc#sha512";
				case 158:
					return "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";
				case 159:
					return "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
				case 160:
					return "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap";
				case 161:
					return "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap";
				case 162:
					return "http://schemas.microsoft.com/ws/2006/05/security";
				case 163:
					return "dnse";
				case 164:
					return "o";
				case 165:
					return "Password";
				case 166:
					return "PasswordText";
				case 167:
					return "Username";
				case 168:
					return "UsernameToken";
				case 169:
					return "BinarySecurityToken";
				case 170:
					return "EncodingType";
				case 171:
					return "KeyIdentifier";
				case 172:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";
				case 173:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";
				case 174:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text";
				case 175:
					return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier";
				case 176:
					return "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ";
				case 177:
					return "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510";
				case 178:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID";
				case 179:
					return "Assertion";
				case 180:
					return "urn:oasis:names:tc:SAML:1.0:assertion";
				case 181:
					return "http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license";
				case 182:
					return "FailedAuthentication";
				case 183:
					return "InvalidSecurityToken";
				case 184:
					return "InvalidSecurity";
				case 185:
					return "k";
				case 186:
					return "SignatureConfirmation";
				case 187:
					return "TokenType";
				case 188:
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1";
				case 189:
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey";
				case 190:
					return "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1";
				case 191:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";
				case 192:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";
				case 193:
					return "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID";
				case 194:
					return "AUTH-HASH";
				case 195:
					return "RequestSecurityTokenResponse";
				case 196:
					return "KeySize";
				case 197:
					return "RequestedTokenReference";
				case 198:
					return "AppliesTo";
				case 199:
					return "Authenticator";
				case 200:
					return "CombinedHash";
				case 201:
					return "BinaryExchange";
				case 202:
					return "Lifetime";
				case 203:
					return "RequestedSecurityToken";
				case 204:
					return "Entropy";
				case 205:
					return "RequestedProofToken";
				case 206:
					return "ComputedKey";
				case 207:
					return "RequestSecurityToken";
				case 208:
					return "RequestType";
				case 209:
					return "Context";
				case 210:
					return "BinarySecret";
				case 211:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/spnego";
				case 212:
					return " http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego";
				case 213:
					return "wst";
				case 214:
					return "http://schemas.xmlsoap.org/ws/2004/04/trust";
				case 215:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/Issue";
				case 216:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/Issue";
				case 217:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/Issue";
				case 218:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/CK/PSHA1";
				case 219:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/SymmetricKey";
				case 220:
					return "http://schemas.xmlsoap.org/ws/2004/04/security/trust/Nonce";
				case 221:
					return "KeyType";
				case 222:
					return "http://schemas.xmlsoap.org/ws/2004/04/trust/SymmetricKey";
				case 223:
					return "http://schemas.xmlsoap.org/ws/2004/04/trust/PublicKey";
				case 224:
					return "Claims";
				case 225:
					return "InvalidRequest";
				case 226:
					return "RequestFailed";
				case 227:
					return "SignWith";
				case 228:
					return "EncryptWith";
				case 229:
					return "EncryptionAlgorithm";
				case 230:
					return "CanonicalizationAlgorithm";
				case 231:
					return "ComputedKeyAlgorithm";
				case 232:
					return "UseKey";
				case 233:
					return "http://schemas.microsoft.com/net/2004/07/secext/WS-SPNego";
				case 234:
					return "http://schemas.microsoft.com/net/2004/07/secext/TLSNego";
				case 235:
					return "t";
				case 236:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";
				case 237:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue";
				case 238:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";
				case 239:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";
				case 240:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";
				case 241:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce";
				case 242:
					return "RenewTarget";
				case 243:
					return "CancelTarget";
				case 244:
					return "RequestedTokenCancelled";
				case 245:
					return "RequestedAttachedReference";
				case 246:
					return "RequestedUnattachedReference";
				case 247:
					return "IssuedTokens";
				case 248:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Renew";
				case 249:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel";
				case 250:
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey";
				case 251:
					return "Access";
				case 252:
					return "AccessDecision";
				case 253:
					return "Advice";
				case 254:
					return "AssertionID";
				case 255:
					return "AssertionIDReference";
				case 256:
					return "Attribute";
				case 257:
					return "AttributeName";
				case 258:
					return "AttributeNamespace";
				case 259:
					return "AttributeStatement";
				case 260:
					return "AttributeValue";
				case 261:
					return "Audience";
				case 262:
					return "AudienceRestrictionCondition";
				case 263:
					return "AuthenticationInstant";
				case 264:
					return "AuthenticationMethod";
				case 265:
					return "AuthenticationStatement";
				case 266:
					return "AuthorityBinding";
				case 267:
					return "AuthorityKind";
				case 268:
					return "AuthorizationDecisionStatement";
				case 269:
					return "Binding";
				case 270:
					return "Condition";
				case 271:
					return "Conditions";
				case 272:
					return "Decision";
				case 273:
					return "DoNotCacheCondition";
				case 274:
					return "Evidence";
				case 275:
					return "IssueInstant";
				case 276:
					return "Issuer";
				case 277:
					return "Location";
				case 278:
					return "MajorVersion";
				case 279:
					return "MinorVersion";
				case 280:
					return "NameIdentifier";
				case 281:
					return "Format";
				case 282:
					return "NameQualifier";
				case 283:
					return "Namespace";
				case 284:
					return "NotBefore";
				case 285:
					return "NotOnOrAfter";
				case 286:
					return "saml";
				case 287:
					return "Statement";
				case 288:
					return "Subject";
				case 289:
					return "SubjectConfirmation";
				case 290:
					return "SubjectConfirmationData";
				case 291:
					return "ConfirmationMethod";
				case 292:
					return "urn:oasis:names:tc:SAML:1.0:cm:holder-of-key";
				case 293:
					return "urn:oasis:names:tc:SAML:1.0:cm:sender-vouches";
				case 294:
					return "SubjectLocality";
				case 295:
					return "DNSAddress";
				case 296:
					return "IPAddress";
				case 297:
					return "SubjectStatement";
				case 298:
					return "urn:oasis:names:tc:SAML:1.0:am:unspecified";
				case 299:
					return "xmlns";
				case 300:
					return "Resource";
				case 301:
					return "UserName";
				case 302:
					return "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName";
				case 303:
					return "EmailName";
				case 304:
					return "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress";
				case 305:
					return "u";
				case 306:
					return "ChannelInstance";
				case 307:
					return "http://schemas.microsoft.com/ws/2005/02/duplex";
				case 308:
					return "Encoding";
				case 309:
					return "MimeType";
				case 310:
					return "CarriedKeyName";
				case 311:
					return "Recipient";
				case 312:
					return "EncryptedKey";
				case 313:
					return "KeyReference";
				case 314:
					return "e";
				case 315:
					return "http://www.w3.org/2001/04/xmlenc#Element";
				case 316:
					return "http://www.w3.org/2001/04/xmlenc#Content";
				case 317:
					return "KeyName";
				case 318:
					return "MgmtData";
				case 319:
					return "KeyValue";
				case 320:
					return "RSAKeyValue";
				case 321:
					return "Modulus";
				case 322:
					return "Exponent";
				case 323:
					return "X509Data";
				case 324:
					return "X509IssuerSerial";
				case 325:
					return "X509IssuerName";
				case 326:
					return "X509SerialNumber";
				case 327:
					return "X509Certificate";
				case 328:
					return "AckRequested";
				case 329:
					return "http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested";
				case 330:
					return "AcksTo";
				case 331:
					return "Accept";
				case 332:
					return "CreateSequence";
				case 333:
					return "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequence";
				case 334:
					return "CreateSequenceRefused";
				case 335:
					return "CreateSequenceResponse";
				case 336:
					return "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequenceResponse";
				case 337:
					return "FaultCode";
				case 338:
					return "InvalidAcknowledgement";
				case 339:
					return "LastMessage";
				case 340:
					return "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage";
				case 341:
					return "LastMessageNumberExceeded";
				case 342:
					return "MessageNumberRollover";
				case 343:
					return "Nack";
				case 344:
					return "netrm";
				case 345:
					return "Offer";
				case 346:
					return "r";
				case 347:
					return "SequenceFault";
				case 348:
					return "SequenceTerminated";
				case 349:
					return "TerminateSequence";
				case 350:
					return "http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence";
				case 351:
					return "UnknownSequence";
				case 352:
					return "http://schemas.microsoft.com/ws/2006/02/tx/oletx";
				case 353:
					return "oletx";
				case 354:
					return "OleTxTransaction";
				case 355:
					return "PropagationToken";
				case 356:
					return "http://schemas.xmlsoap.org/ws/2004/10/wscoor";
				case 357:
					return "wscoor";
				case 358:
					return "CreateCoordinationContext";
				case 359:
					return "CreateCoordinationContextResponse";
				case 360:
					return "CoordinationContext";
				case 361:
					return "CurrentContext";
				case 362:
					return "CoordinationType";
				case 363:
					return "RegistrationService";
				case 364:
					return "Register";
				case 365:
					return "RegisterResponse";
				case 366:
					return "ProtocolIdentifier";
				case 367:
					return "CoordinatorProtocolService";
				case 368:
					return "ParticipantProtocolService";
				case 369:
					return "http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContext";
				case 370:
					return "http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContextResponse";
				case 371:
					return "http://schemas.xmlsoap.org/ws/2004/10/wscoor/Register";
				case 372:
					return "http://schemas.xmlsoap.org/ws/2004/10/wscoor/RegisterResponse";
				case 373:
					return "http://schemas.xmlsoap.org/ws/2004/10/wscoor/fault";
				case 374:
					return "ActivationCoordinatorPortType";
				case 375:
					return "RegistrationCoordinatorPortType";
				case 376:
					return "InvalidState";
				case 377:
					return "InvalidProtocol";
				case 378:
					return "InvalidParameters";
				case 379:
					return "NoActivity";
				case 380:
					return "ContextRefused";
				case 381:
					return "AlreadyRegistered";
				case 382:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat";
				case 383:
					return "wsat";
				case 384:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Completion";
				case 385:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Durable2PC";
				case 386:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Volatile2PC";
				case 387:
					return "Prepare";
				case 388:
					return "Prepared";
				case 389:
					return "ReadOnly";
				case 390:
					return "Commit";
				case 391:
					return "Rollback";
				case 392:
					return "Committed";
				case 393:
					return "Aborted";
				case 394:
					return "Replay";
				case 395:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Commit";
				case 396:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Rollback";
				case 397:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Committed";
				case 398:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Aborted";
				case 399:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepare";
				case 400:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepared";
				case 401:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/ReadOnly";
				case 402:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/Replay";
				case 403:
					return "http://schemas.xmlsoap.org/ws/2004/10/wsat/fault";
				case 404:
					return "CompletionCoordinatorPortType";
				case 405:
					return "CompletionParticipantPortType";
				case 406:
					return "CoordinatorPortType";
				case 407:
					return "ParticipantPortType";
				case 408:
					return "InconsistentInternalState";
				case 409:
					return "mstx";
				case 410:
					return "Enlistment";
				case 411:
					return "protocol";
				case 412:
					return "LocalTransactionId";
				case 413:
					return "IsolationLevel";
				case 414:
					return "IsolationFlags";
				case 415:
					return "Description";
				case 416:
					return "Loopback";
				case 417:
					return "RegisterInfo";
				case 418:
					return "ContextId";
				case 419:
					return "TokenId";
				case 420:
					return "AccessDenied";
				case 421:
					return "InvalidPolicy";
				case 422:
					return "CoordinatorRegistrationFailed";
				case 423:
					return "TooManyEnlistments";
				case 424:
					return "Disabled";
				case 425:
					return "ActivityId";
				case 426:
					return "http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics";
				case 427:
					return "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1";
				case 428:
					return "http://schemas.xmlsoap.org/ws/2002/12/policy";
				case 429:
					return "FloodMessage";
				case 430:
					return "LinkUtility";
				case 431:
					return "Hops";
				case 432:
					return "http://schemas.microsoft.com/net/2006/05/peer/HopCount";
				case 433:
					return "PeerVia";
				case 434:
					return "http://schemas.microsoft.com/net/2006/05/peer";
				case 435:
					return "PeerFlooder";
				case 436:
					return "PeerTo";
				case 437:
					return "http://schemas.microsoft.com/ws/2005/05/routing";
				case 438:
					return "PacketRoutable";
				case 439:
					return "http://schemas.microsoft.com/ws/2005/05/addressing/none";
				case 440:
					return "http://schemas.microsoft.com/ws/2005/05/envelope/none";
				case 441:
					return "http://www.w3.org/2001/XMLSchema-instance";
				case 442:
					return "http://www.w3.org/2001/XMLSchema";
				case 443:
					return "nil";
				case 444:
					return "type";
				case 445:
					return "char";
				case 446:
					return "boolean";
				case 447:
					return "byte";
				case 448:
					return "unsignedByte";
				case 449:
					return "short";
				case 450:
					return "unsignedShort";
				case 451:
					return "int";
				case 452:
					return "unsignedInt";
				case 453:
					return "long";
				case 454:
					return "unsignedLong";
				case 455:
					return "float";
				case 456:
					return "double";
				case 457:
					return "decimal";
				case 458:
					return "dateTime";
				case 459:
					return "string";
				case 460:
					return "base64Binary";
				case 461:
					return "anyType";
				case 462:
					return "duration";
				case 463:
					return "guid";
				case 464:
					return "anyURI";
				case 465:
					return "QName";
				case 466:
					return "time";
				case 467:
					return "date";
				case 468:
					return "hexBinary";
				case 469:
					return "gYearMonth";
				case 470:
					return "gYear";
				case 471:
					return "gMonthDay";
				case 472:
					return "gDay";
				case 473:
					return "gMonth";
				case 474:
					return "integer";
				case 475:
					return "positiveInteger";
				case 476:
					return "negativeInteger";
				case 477:
					return "nonPositiveInteger";
				case 478:
					return "nonNegativeInteger";
				case 479:
					return "normalizedString";
				case 480:
					return "ConnectionLimitReached";
				case 481:
					return "http://schemas.xmlsoap.org/soap/envelope/";
				case 482:
					return "actor";
				case 483:
					return "faultcode";
				case 484:
					return "faultstring";
				case 485:
					return "faultactor";
				case 486:
					return "detail";
				default:
					return null;
				}
			}
		}

		// Token: 0x040002C5 RID: 709
		public const string String0 = "mustUnderstand";

		// Token: 0x040002C6 RID: 710
		public const string String1 = "Envelope";

		// Token: 0x040002C7 RID: 711
		public const string String2 = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x040002C8 RID: 712
		public const string String3 = "http://www.w3.org/2005/08/addressing";

		// Token: 0x040002C9 RID: 713
		public const string String4 = "Header";

		// Token: 0x040002CA RID: 714
		public const string String5 = "Action";

		// Token: 0x040002CB RID: 715
		public const string String6 = "To";

		// Token: 0x040002CC RID: 716
		public const string String7 = "Body";

		// Token: 0x040002CD RID: 717
		public const string String8 = "Algorithm";

		// Token: 0x040002CE RID: 718
		public const string String9 = "RelatesTo";

		// Token: 0x040002CF RID: 719
		public const string String10 = "http://www.w3.org/2005/08/addressing/anonymous";

		// Token: 0x040002D0 RID: 720
		public const string String11 = "URI";

		// Token: 0x040002D1 RID: 721
		public const string String12 = "Reference";

		// Token: 0x040002D2 RID: 722
		public const string String13 = "MessageID";

		// Token: 0x040002D3 RID: 723
		public const string String14 = "Id";

		// Token: 0x040002D4 RID: 724
		public const string String15 = "Identifier";

		// Token: 0x040002D5 RID: 725
		public const string String16 = "http://schemas.xmlsoap.org/ws/2005/02/rm";

		// Token: 0x040002D6 RID: 726
		public const string String17 = "Transforms";

		// Token: 0x040002D7 RID: 727
		public const string String18 = "Transform";

		// Token: 0x040002D8 RID: 728
		public const string String19 = "DigestMethod";

		// Token: 0x040002D9 RID: 729
		public const string String20 = "DigestValue";

		// Token: 0x040002DA RID: 730
		public const string String21 = "Address";

		// Token: 0x040002DB RID: 731
		public const string String22 = "ReplyTo";

		// Token: 0x040002DC RID: 732
		public const string String23 = "SequenceAcknowledgement";

		// Token: 0x040002DD RID: 733
		public const string String24 = "AcknowledgementRange";

		// Token: 0x040002DE RID: 734
		public const string String25 = "Upper";

		// Token: 0x040002DF RID: 735
		public const string String26 = "Lower";

		// Token: 0x040002E0 RID: 736
		public const string String27 = "BufferRemaining";

		// Token: 0x040002E1 RID: 737
		public const string String28 = "http://schemas.microsoft.com/ws/2006/05/rm";

		// Token: 0x040002E2 RID: 738
		public const string String29 = "http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement";

		// Token: 0x040002E3 RID: 739
		public const string String30 = "SecurityTokenReference";

		// Token: 0x040002E4 RID: 740
		public const string String31 = "Sequence";

		// Token: 0x040002E5 RID: 741
		public const string String32 = "MessageNumber";

		// Token: 0x040002E6 RID: 742
		public const string String33 = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x040002E7 RID: 743
		public const string String34 = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x040002E8 RID: 744
		public const string String35 = "KeyInfo";

		// Token: 0x040002E9 RID: 745
		public const string String36 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x040002EA RID: 746
		public const string String37 = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x040002EB RID: 747
		public const string String38 = "http://schemas.xmlsoap.org/ws/2005/02/sc";

		// Token: 0x040002EC RID: 748
		public const string String39 = "DerivedKeyToken";

		// Token: 0x040002ED RID: 749
		public const string String40 = "Nonce";

		// Token: 0x040002EE RID: 750
		public const string String41 = "Signature";

		// Token: 0x040002EF RID: 751
		public const string String42 = "SignedInfo";

		// Token: 0x040002F0 RID: 752
		public const string String43 = "CanonicalizationMethod";

		// Token: 0x040002F1 RID: 753
		public const string String44 = "SignatureMethod";

		// Token: 0x040002F2 RID: 754
		public const string String45 = "SignatureValue";

		// Token: 0x040002F3 RID: 755
		public const string String46 = "DataReference";

		// Token: 0x040002F4 RID: 756
		public const string String47 = "EncryptedData";

		// Token: 0x040002F5 RID: 757
		public const string String48 = "EncryptionMethod";

		// Token: 0x040002F6 RID: 758
		public const string String49 = "CipherData";

		// Token: 0x040002F7 RID: 759
		public const string String50 = "CipherValue";

		// Token: 0x040002F8 RID: 760
		public const string String51 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		// Token: 0x040002F9 RID: 761
		public const string String52 = "Security";

		// Token: 0x040002FA RID: 762
		public const string String53 = "Timestamp";

		// Token: 0x040002FB RID: 763
		public const string String54 = "Created";

		// Token: 0x040002FC RID: 764
		public const string String55 = "Expires";

		// Token: 0x040002FD RID: 765
		public const string String56 = "Length";

		// Token: 0x040002FE RID: 766
		public const string String57 = "ReferenceList";

		// Token: 0x040002FF RID: 767
		public const string String58 = "ValueType";

		// Token: 0x04000300 RID: 768
		public const string String59 = "Type";

		// Token: 0x04000301 RID: 769
		public const string String60 = "EncryptedHeader";

		// Token: 0x04000302 RID: 770
		public const string String61 = "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd";

		// Token: 0x04000303 RID: 771
		public const string String62 = "RequestSecurityTokenResponseCollection";

		// Token: 0x04000304 RID: 772
		public const string String63 = "http://schemas.xmlsoap.org/ws/2005/02/trust";

		// Token: 0x04000305 RID: 773
		public const string String64 = "http://schemas.xmlsoap.org/ws/2005/02/trust#BinarySecret";

		// Token: 0x04000306 RID: 774
		public const string String65 = "http://schemas.microsoft.com/ws/2006/02/transactions";

		// Token: 0x04000307 RID: 775
		public const string String66 = "s";

		// Token: 0x04000308 RID: 776
		public const string String67 = "Fault";

		// Token: 0x04000309 RID: 777
		public const string String68 = "MustUnderstand";

		// Token: 0x0400030A RID: 778
		public const string String69 = "role";

		// Token: 0x0400030B RID: 779
		public const string String70 = "relay";

		// Token: 0x0400030C RID: 780
		public const string String71 = "Code";

		// Token: 0x0400030D RID: 781
		public const string String72 = "Reason";

		// Token: 0x0400030E RID: 782
		public const string String73 = "Text";

		// Token: 0x0400030F RID: 783
		public const string String74 = "Node";

		// Token: 0x04000310 RID: 784
		public const string String75 = "Role";

		// Token: 0x04000311 RID: 785
		public const string String76 = "Detail";

		// Token: 0x04000312 RID: 786
		public const string String77 = "Value";

		// Token: 0x04000313 RID: 787
		public const string String78 = "Subcode";

		// Token: 0x04000314 RID: 788
		public const string String79 = "NotUnderstood";

		// Token: 0x04000315 RID: 789
		public const string String80 = "qname";

		// Token: 0x04000316 RID: 790
		public const string String81 = "";

		// Token: 0x04000317 RID: 791
		public const string String82 = "From";

		// Token: 0x04000318 RID: 792
		public const string String83 = "FaultTo";

		// Token: 0x04000319 RID: 793
		public const string String84 = "EndpointReference";

		// Token: 0x0400031A RID: 794
		public const string String85 = "PortType";

		// Token: 0x0400031B RID: 795
		public const string String86 = "ServiceName";

		// Token: 0x0400031C RID: 796
		public const string String87 = "PortName";

		// Token: 0x0400031D RID: 797
		public const string String88 = "ReferenceProperties";

		// Token: 0x0400031E RID: 798
		public const string String89 = "RelationshipType";

		// Token: 0x0400031F RID: 799
		public const string String90 = "Reply";

		// Token: 0x04000320 RID: 800
		public const string String91 = "a";

		// Token: 0x04000321 RID: 801
		public const string String92 = "http://schemas.xmlsoap.org/ws/2006/02/addressingidentity";

		// Token: 0x04000322 RID: 802
		public const string String93 = "Identity";

		// Token: 0x04000323 RID: 803
		public const string String94 = "Spn";

		// Token: 0x04000324 RID: 804
		public const string String95 = "Upn";

		// Token: 0x04000325 RID: 805
		public const string String96 = "Rsa";

		// Token: 0x04000326 RID: 806
		public const string String97 = "Dns";

		// Token: 0x04000327 RID: 807
		public const string String98 = "X509v3Certificate";

		// Token: 0x04000328 RID: 808
		public const string String99 = "http://www.w3.org/2005/08/addressing/fault";

		// Token: 0x04000329 RID: 809
		public const string String100 = "ReferenceParameters";

		// Token: 0x0400032A RID: 810
		public const string String101 = "IsReferenceParameter";

		// Token: 0x0400032B RID: 811
		public const string String102 = "http://www.w3.org/2005/08/addressing/reply";

		// Token: 0x0400032C RID: 812
		public const string String103 = "http://www.w3.org/2005/08/addressing/none";

		// Token: 0x0400032D RID: 813
		public const string String104 = "Metadata";

		// Token: 0x0400032E RID: 814
		public const string String105 = "http://schemas.xmlsoap.org/ws/2004/08/addressing";

		// Token: 0x0400032F RID: 815
		public const string String106 = "http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous";

		// Token: 0x04000330 RID: 816
		public const string String107 = "http://schemas.xmlsoap.org/ws/2004/08/addressing/fault";

		// Token: 0x04000331 RID: 817
		public const string String108 = "http://schemas.xmlsoap.org/ws/2004/06/addressingex";

		// Token: 0x04000332 RID: 818
		public const string String109 = "RedirectTo";

		// Token: 0x04000333 RID: 819
		public const string String110 = "Via";

		// Token: 0x04000334 RID: 820
		public const string String111 = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04000335 RID: 821
		public const string String112 = "PrefixList";

		// Token: 0x04000336 RID: 822
		public const string String113 = "InclusiveNamespaces";

		// Token: 0x04000337 RID: 823
		public const string String114 = "ec";

		// Token: 0x04000338 RID: 824
		public const string String115 = "SecurityContextToken";

		// Token: 0x04000339 RID: 825
		public const string String116 = "Generation";

		// Token: 0x0400033A RID: 826
		public const string String117 = "Label";

		// Token: 0x0400033B RID: 827
		public const string String118 = "Offset";

		// Token: 0x0400033C RID: 828
		public const string String119 = "Properties";

		// Token: 0x0400033D RID: 829
		public const string String120 = "Cookie";

		// Token: 0x0400033E RID: 830
		public const string String121 = "wsc";

		// Token: 0x0400033F RID: 831
		public const string String122 = "http://schemas.xmlsoap.org/ws/2004/04/sc";

		// Token: 0x04000340 RID: 832
		public const string String123 = "http://schemas.xmlsoap.org/ws/2004/04/security/sc/dk";

		// Token: 0x04000341 RID: 833
		public const string String124 = "http://schemas.xmlsoap.org/ws/2004/04/security/sc/sct";

		// Token: 0x04000342 RID: 834
		public const string String125 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/SCT";

		// Token: 0x04000343 RID: 835
		public const string String126 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/SCT";

		// Token: 0x04000344 RID: 836
		public const string String127 = "RenewNeeded";

		// Token: 0x04000345 RID: 837
		public const string String128 = "BadContextToken";

		// Token: 0x04000346 RID: 838
		public const string String129 = "c";

		// Token: 0x04000347 RID: 839
		public const string String130 = "http://schemas.xmlsoap.org/ws/2005/02/sc/dk";

		// Token: 0x04000348 RID: 840
		public const string String131 = "http://schemas.xmlsoap.org/ws/2005/02/sc/sct";

		// Token: 0x04000349 RID: 841
		public const string String132 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT";

		// Token: 0x0400034A RID: 842
		public const string String133 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT";

		// Token: 0x0400034B RID: 843
		public const string String134 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew";

		// Token: 0x0400034C RID: 844
		public const string String135 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew";

		// Token: 0x0400034D RID: 845
		public const string String136 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel";

		// Token: 0x0400034E RID: 846
		public const string String137 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel";

		// Token: 0x0400034F RID: 847
		public const string String138 = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		// Token: 0x04000350 RID: 848
		public const string String139 = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		// Token: 0x04000351 RID: 849
		public const string String140 = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		// Token: 0x04000352 RID: 850
		public const string String141 = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		// Token: 0x04000353 RID: 851
		public const string String142 = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x04000354 RID: 852
		public const string String143 = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		// Token: 0x04000355 RID: 853
		public const string String144 = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		// Token: 0x04000356 RID: 854
		public const string String145 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x04000357 RID: 855
		public const string String146 = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x04000358 RID: 856
		public const string String147 = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x04000359 RID: 857
		public const string String148 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x0400035A RID: 858
		public const string String149 = "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";

		// Token: 0x0400035B RID: 859
		public const string String150 = "http://www.w3.org/2001/04/xmlenc#ripemd160";

		// Token: 0x0400035C RID: 860
		public const string String151 = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		// Token: 0x0400035D RID: 861
		public const string String152 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x0400035E RID: 862
		public const string String153 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x0400035F RID: 863
		public const string String154 = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		// Token: 0x04000360 RID: 864
		public const string String155 = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x04000361 RID: 865
		public const string String156 = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x04000362 RID: 866
		public const string String157 = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04000363 RID: 867
		public const string String158 = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		// Token: 0x04000364 RID: 868
		public const string String159 = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		// Token: 0x04000365 RID: 869
		public const string String160 = "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap";

		// Token: 0x04000366 RID: 870
		public const string String161 = "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap";

		// Token: 0x04000367 RID: 871
		public const string String162 = "http://schemas.microsoft.com/ws/2006/05/security";

		// Token: 0x04000368 RID: 872
		public const string String163 = "dnse";

		// Token: 0x04000369 RID: 873
		public const string String164 = "o";

		// Token: 0x0400036A RID: 874
		public const string String165 = "Password";

		// Token: 0x0400036B RID: 875
		public const string String166 = "PasswordText";

		// Token: 0x0400036C RID: 876
		public const string String167 = "Username";

		// Token: 0x0400036D RID: 877
		public const string String168 = "UsernameToken";

		// Token: 0x0400036E RID: 878
		public const string String169 = "BinarySecurityToken";

		// Token: 0x0400036F RID: 879
		public const string String170 = "EncodingType";

		// Token: 0x04000370 RID: 880
		public const string String171 = "KeyIdentifier";

		// Token: 0x04000371 RID: 881
		public const string String172 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

		// Token: 0x04000372 RID: 882
		public const string String173 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

		// Token: 0x04000373 RID: 883
		public const string String174 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text";

		// Token: 0x04000374 RID: 884
		public const string String175 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier";

		// Token: 0x04000375 RID: 885
		public const string String176 = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ";

		// Token: 0x04000376 RID: 886
		public const string String177 = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510";

		// Token: 0x04000377 RID: 887
		public const string String178 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID";

		// Token: 0x04000378 RID: 888
		public const string String179 = "Assertion";

		// Token: 0x04000379 RID: 889
		public const string String180 = "urn:oasis:names:tc:SAML:1.0:assertion";

		// Token: 0x0400037A RID: 890
		public const string String181 = "http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license";

		// Token: 0x0400037B RID: 891
		public const string String182 = "FailedAuthentication";

		// Token: 0x0400037C RID: 892
		public const string String183 = "InvalidSecurityToken";

		// Token: 0x0400037D RID: 893
		public const string String184 = "InvalidSecurity";

		// Token: 0x0400037E RID: 894
		public const string String185 = "k";

		// Token: 0x0400037F RID: 895
		public const string String186 = "SignatureConfirmation";

		// Token: 0x04000380 RID: 896
		public const string String187 = "TokenType";

		// Token: 0x04000381 RID: 897
		public const string String188 = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1";

		// Token: 0x04000382 RID: 898
		public const string String189 = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey";

		// Token: 0x04000383 RID: 899
		public const string String190 = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1";

		// Token: 0x04000384 RID: 900
		public const string String191 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";

		// Token: 0x04000385 RID: 901
		public const string String192 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0";

		// Token: 0x04000386 RID: 902
		public const string String193 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID";

		// Token: 0x04000387 RID: 903
		public const string String194 = "AUTH-HASH";

		// Token: 0x04000388 RID: 904
		public const string String195 = "RequestSecurityTokenResponse";

		// Token: 0x04000389 RID: 905
		public const string String196 = "KeySize";

		// Token: 0x0400038A RID: 906
		public const string String197 = "RequestedTokenReference";

		// Token: 0x0400038B RID: 907
		public const string String198 = "AppliesTo";

		// Token: 0x0400038C RID: 908
		public const string String199 = "Authenticator";

		// Token: 0x0400038D RID: 909
		public const string String200 = "CombinedHash";

		// Token: 0x0400038E RID: 910
		public const string String201 = "BinaryExchange";

		// Token: 0x0400038F RID: 911
		public const string String202 = "Lifetime";

		// Token: 0x04000390 RID: 912
		public const string String203 = "RequestedSecurityToken";

		// Token: 0x04000391 RID: 913
		public const string String204 = "Entropy";

		// Token: 0x04000392 RID: 914
		public const string String205 = "RequestedProofToken";

		// Token: 0x04000393 RID: 915
		public const string String206 = "ComputedKey";

		// Token: 0x04000394 RID: 916
		public const string String207 = "RequestSecurityToken";

		// Token: 0x04000395 RID: 917
		public const string String208 = "RequestType";

		// Token: 0x04000396 RID: 918
		public const string String209 = "Context";

		// Token: 0x04000397 RID: 919
		public const string String210 = "BinarySecret";

		// Token: 0x04000398 RID: 920
		public const string String211 = "http://schemas.xmlsoap.org/ws/2005/02/trust/spnego";

		// Token: 0x04000399 RID: 921
		public const string String212 = " http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego";

		// Token: 0x0400039A RID: 922
		public const string String213 = "wst";

		// Token: 0x0400039B RID: 923
		public const string String214 = "http://schemas.xmlsoap.org/ws/2004/04/trust";

		// Token: 0x0400039C RID: 924
		public const string String215 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/Issue";

		// Token: 0x0400039D RID: 925
		public const string String216 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/Issue";

		// Token: 0x0400039E RID: 926
		public const string String217 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/Issue";

		// Token: 0x0400039F RID: 927
		public const string String218 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/CK/PSHA1";

		// Token: 0x040003A0 RID: 928
		public const string String219 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/SymmetricKey";

		// Token: 0x040003A1 RID: 929
		public const string String220 = "http://schemas.xmlsoap.org/ws/2004/04/security/trust/Nonce";

		// Token: 0x040003A2 RID: 930
		public const string String221 = "KeyType";

		// Token: 0x040003A3 RID: 931
		public const string String222 = "http://schemas.xmlsoap.org/ws/2004/04/trust/SymmetricKey";

		// Token: 0x040003A4 RID: 932
		public const string String223 = "http://schemas.xmlsoap.org/ws/2004/04/trust/PublicKey";

		// Token: 0x040003A5 RID: 933
		public const string String224 = "Claims";

		// Token: 0x040003A6 RID: 934
		public const string String225 = "InvalidRequest";

		// Token: 0x040003A7 RID: 935
		public const string String226 = "RequestFailed";

		// Token: 0x040003A8 RID: 936
		public const string String227 = "SignWith";

		// Token: 0x040003A9 RID: 937
		public const string String228 = "EncryptWith";

		// Token: 0x040003AA RID: 938
		public const string String229 = "EncryptionAlgorithm";

		// Token: 0x040003AB RID: 939
		public const string String230 = "CanonicalizationAlgorithm";

		// Token: 0x040003AC RID: 940
		public const string String231 = "ComputedKeyAlgorithm";

		// Token: 0x040003AD RID: 941
		public const string String232 = "UseKey";

		// Token: 0x040003AE RID: 942
		public const string String233 = "http://schemas.microsoft.com/net/2004/07/secext/WS-SPNego";

		// Token: 0x040003AF RID: 943
		public const string String234 = "http://schemas.microsoft.com/net/2004/07/secext/TLSNego";

		// Token: 0x040003B0 RID: 944
		public const string String235 = "t";

		// Token: 0x040003B1 RID: 945
		public const string String236 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";

		// Token: 0x040003B2 RID: 946
		public const string String237 = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue";

		// Token: 0x040003B3 RID: 947
		public const string String238 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";

		// Token: 0x040003B4 RID: 948
		public const string String239 = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";

		// Token: 0x040003B5 RID: 949
		public const string String240 = "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";

		// Token: 0x040003B6 RID: 950
		public const string String241 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce";

		// Token: 0x040003B7 RID: 951
		public const string String242 = "RenewTarget";

		// Token: 0x040003B8 RID: 952
		public const string String243 = "CancelTarget";

		// Token: 0x040003B9 RID: 953
		public const string String244 = "RequestedTokenCancelled";

		// Token: 0x040003BA RID: 954
		public const string String245 = "RequestedAttachedReference";

		// Token: 0x040003BB RID: 955
		public const string String246 = "RequestedUnattachedReference";

		// Token: 0x040003BC RID: 956
		public const string String247 = "IssuedTokens";

		// Token: 0x040003BD RID: 957
		public const string String248 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Renew";

		// Token: 0x040003BE RID: 958
		public const string String249 = "http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel";

		// Token: 0x040003BF RID: 959
		public const string String250 = "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey";

		// Token: 0x040003C0 RID: 960
		public const string String251 = "Access";

		// Token: 0x040003C1 RID: 961
		public const string String252 = "AccessDecision";

		// Token: 0x040003C2 RID: 962
		public const string String253 = "Advice";

		// Token: 0x040003C3 RID: 963
		public const string String254 = "AssertionID";

		// Token: 0x040003C4 RID: 964
		public const string String255 = "AssertionIDReference";

		// Token: 0x040003C5 RID: 965
		public const string String256 = "Attribute";

		// Token: 0x040003C6 RID: 966
		public const string String257 = "AttributeName";

		// Token: 0x040003C7 RID: 967
		public const string String258 = "AttributeNamespace";

		// Token: 0x040003C8 RID: 968
		public const string String259 = "AttributeStatement";

		// Token: 0x040003C9 RID: 969
		public const string String260 = "AttributeValue";

		// Token: 0x040003CA RID: 970
		public const string String261 = "Audience";

		// Token: 0x040003CB RID: 971
		public const string String262 = "AudienceRestrictionCondition";

		// Token: 0x040003CC RID: 972
		public const string String263 = "AuthenticationInstant";

		// Token: 0x040003CD RID: 973
		public const string String264 = "AuthenticationMethod";

		// Token: 0x040003CE RID: 974
		public const string String265 = "AuthenticationStatement";

		// Token: 0x040003CF RID: 975
		public const string String266 = "AuthorityBinding";

		// Token: 0x040003D0 RID: 976
		public const string String267 = "AuthorityKind";

		// Token: 0x040003D1 RID: 977
		public const string String268 = "AuthorizationDecisionStatement";

		// Token: 0x040003D2 RID: 978
		public const string String269 = "Binding";

		// Token: 0x040003D3 RID: 979
		public const string String270 = "Condition";

		// Token: 0x040003D4 RID: 980
		public const string String271 = "Conditions";

		// Token: 0x040003D5 RID: 981
		public const string String272 = "Decision";

		// Token: 0x040003D6 RID: 982
		public const string String273 = "DoNotCacheCondition";

		// Token: 0x040003D7 RID: 983
		public const string String274 = "Evidence";

		// Token: 0x040003D8 RID: 984
		public const string String275 = "IssueInstant";

		// Token: 0x040003D9 RID: 985
		public const string String276 = "Issuer";

		// Token: 0x040003DA RID: 986
		public const string String277 = "Location";

		// Token: 0x040003DB RID: 987
		public const string String278 = "MajorVersion";

		// Token: 0x040003DC RID: 988
		public const string String279 = "MinorVersion";

		// Token: 0x040003DD RID: 989
		public const string String280 = "NameIdentifier";

		// Token: 0x040003DE RID: 990
		public const string String281 = "Format";

		// Token: 0x040003DF RID: 991
		public const string String282 = "NameQualifier";

		// Token: 0x040003E0 RID: 992
		public const string String283 = "Namespace";

		// Token: 0x040003E1 RID: 993
		public const string String284 = "NotBefore";

		// Token: 0x040003E2 RID: 994
		public const string String285 = "NotOnOrAfter";

		// Token: 0x040003E3 RID: 995
		public const string String286 = "saml";

		// Token: 0x040003E4 RID: 996
		public const string String287 = "Statement";

		// Token: 0x040003E5 RID: 997
		public const string String288 = "Subject";

		// Token: 0x040003E6 RID: 998
		public const string String289 = "SubjectConfirmation";

		// Token: 0x040003E7 RID: 999
		public const string String290 = "SubjectConfirmationData";

		// Token: 0x040003E8 RID: 1000
		public const string String291 = "ConfirmationMethod";

		// Token: 0x040003E9 RID: 1001
		public const string String292 = "urn:oasis:names:tc:SAML:1.0:cm:holder-of-key";

		// Token: 0x040003EA RID: 1002
		public const string String293 = "urn:oasis:names:tc:SAML:1.0:cm:sender-vouches";

		// Token: 0x040003EB RID: 1003
		public const string String294 = "SubjectLocality";

		// Token: 0x040003EC RID: 1004
		public const string String295 = "DNSAddress";

		// Token: 0x040003ED RID: 1005
		public const string String296 = "IPAddress";

		// Token: 0x040003EE RID: 1006
		public const string String297 = "SubjectStatement";

		// Token: 0x040003EF RID: 1007
		public const string String298 = "urn:oasis:names:tc:SAML:1.0:am:unspecified";

		// Token: 0x040003F0 RID: 1008
		public const string String299 = "xmlns";

		// Token: 0x040003F1 RID: 1009
		public const string String300 = "Resource";

		// Token: 0x040003F2 RID: 1010
		public const string String301 = "UserName";

		// Token: 0x040003F3 RID: 1011
		public const string String302 = "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName";

		// Token: 0x040003F4 RID: 1012
		public const string String303 = "EmailName";

		// Token: 0x040003F5 RID: 1013
		public const string String304 = "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress";

		// Token: 0x040003F6 RID: 1014
		public const string String305 = "u";

		// Token: 0x040003F7 RID: 1015
		public const string String306 = "ChannelInstance";

		// Token: 0x040003F8 RID: 1016
		public const string String307 = "http://schemas.microsoft.com/ws/2005/02/duplex";

		// Token: 0x040003F9 RID: 1017
		public const string String308 = "Encoding";

		// Token: 0x040003FA RID: 1018
		public const string String309 = "MimeType";

		// Token: 0x040003FB RID: 1019
		public const string String310 = "CarriedKeyName";

		// Token: 0x040003FC RID: 1020
		public const string String311 = "Recipient";

		// Token: 0x040003FD RID: 1021
		public const string String312 = "EncryptedKey";

		// Token: 0x040003FE RID: 1022
		public const string String313 = "KeyReference";

		// Token: 0x040003FF RID: 1023
		public const string String314 = "e";

		// Token: 0x04000400 RID: 1024
		public const string String315 = "http://www.w3.org/2001/04/xmlenc#Element";

		// Token: 0x04000401 RID: 1025
		public const string String316 = "http://www.w3.org/2001/04/xmlenc#Content";

		// Token: 0x04000402 RID: 1026
		public const string String317 = "KeyName";

		// Token: 0x04000403 RID: 1027
		public const string String318 = "MgmtData";

		// Token: 0x04000404 RID: 1028
		public const string String319 = "KeyValue";

		// Token: 0x04000405 RID: 1029
		public const string String320 = "RSAKeyValue";

		// Token: 0x04000406 RID: 1030
		public const string String321 = "Modulus";

		// Token: 0x04000407 RID: 1031
		public const string String322 = "Exponent";

		// Token: 0x04000408 RID: 1032
		public const string String323 = "X509Data";

		// Token: 0x04000409 RID: 1033
		public const string String324 = "X509IssuerSerial";

		// Token: 0x0400040A RID: 1034
		public const string String325 = "X509IssuerName";

		// Token: 0x0400040B RID: 1035
		public const string String326 = "X509SerialNumber";

		// Token: 0x0400040C RID: 1036
		public const string String327 = "X509Certificate";

		// Token: 0x0400040D RID: 1037
		public const string String328 = "AckRequested";

		// Token: 0x0400040E RID: 1038
		public const string String329 = "http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested";

		// Token: 0x0400040F RID: 1039
		public const string String330 = "AcksTo";

		// Token: 0x04000410 RID: 1040
		public const string String331 = "Accept";

		// Token: 0x04000411 RID: 1041
		public const string String332 = "CreateSequence";

		// Token: 0x04000412 RID: 1042
		public const string String333 = "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequence";

		// Token: 0x04000413 RID: 1043
		public const string String334 = "CreateSequenceRefused";

		// Token: 0x04000414 RID: 1044
		public const string String335 = "CreateSequenceResponse";

		// Token: 0x04000415 RID: 1045
		public const string String336 = "http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequenceResponse";

		// Token: 0x04000416 RID: 1046
		public const string String337 = "FaultCode";

		// Token: 0x04000417 RID: 1047
		public const string String338 = "InvalidAcknowledgement";

		// Token: 0x04000418 RID: 1048
		public const string String339 = "LastMessage";

		// Token: 0x04000419 RID: 1049
		public const string String340 = "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage";

		// Token: 0x0400041A RID: 1050
		public const string String341 = "LastMessageNumberExceeded";

		// Token: 0x0400041B RID: 1051
		public const string String342 = "MessageNumberRollover";

		// Token: 0x0400041C RID: 1052
		public const string String343 = "Nack";

		// Token: 0x0400041D RID: 1053
		public const string String344 = "netrm";

		// Token: 0x0400041E RID: 1054
		public const string String345 = "Offer";

		// Token: 0x0400041F RID: 1055
		public const string String346 = "r";

		// Token: 0x04000420 RID: 1056
		public const string String347 = "SequenceFault";

		// Token: 0x04000421 RID: 1057
		public const string String348 = "SequenceTerminated";

		// Token: 0x04000422 RID: 1058
		public const string String349 = "TerminateSequence";

		// Token: 0x04000423 RID: 1059
		public const string String350 = "http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence";

		// Token: 0x04000424 RID: 1060
		public const string String351 = "UnknownSequence";

		// Token: 0x04000425 RID: 1061
		public const string String352 = "http://schemas.microsoft.com/ws/2006/02/tx/oletx";

		// Token: 0x04000426 RID: 1062
		public const string String353 = "oletx";

		// Token: 0x04000427 RID: 1063
		public const string String354 = "OleTxTransaction";

		// Token: 0x04000428 RID: 1064
		public const string String355 = "PropagationToken";

		// Token: 0x04000429 RID: 1065
		public const string String356 = "http://schemas.xmlsoap.org/ws/2004/10/wscoor";

		// Token: 0x0400042A RID: 1066
		public const string String357 = "wscoor";

		// Token: 0x0400042B RID: 1067
		public const string String358 = "CreateCoordinationContext";

		// Token: 0x0400042C RID: 1068
		public const string String359 = "CreateCoordinationContextResponse";

		// Token: 0x0400042D RID: 1069
		public const string String360 = "CoordinationContext";

		// Token: 0x0400042E RID: 1070
		public const string String361 = "CurrentContext";

		// Token: 0x0400042F RID: 1071
		public const string String362 = "CoordinationType";

		// Token: 0x04000430 RID: 1072
		public const string String363 = "RegistrationService";

		// Token: 0x04000431 RID: 1073
		public const string String364 = "Register";

		// Token: 0x04000432 RID: 1074
		public const string String365 = "RegisterResponse";

		// Token: 0x04000433 RID: 1075
		public const string String366 = "ProtocolIdentifier";

		// Token: 0x04000434 RID: 1076
		public const string String367 = "CoordinatorProtocolService";

		// Token: 0x04000435 RID: 1077
		public const string String368 = "ParticipantProtocolService";

		// Token: 0x04000436 RID: 1078
		public const string String369 = "http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContext";

		// Token: 0x04000437 RID: 1079
		public const string String370 = "http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContextResponse";

		// Token: 0x04000438 RID: 1080
		public const string String371 = "http://schemas.xmlsoap.org/ws/2004/10/wscoor/Register";

		// Token: 0x04000439 RID: 1081
		public const string String372 = "http://schemas.xmlsoap.org/ws/2004/10/wscoor/RegisterResponse";

		// Token: 0x0400043A RID: 1082
		public const string String373 = "http://schemas.xmlsoap.org/ws/2004/10/wscoor/fault";

		// Token: 0x0400043B RID: 1083
		public const string String374 = "ActivationCoordinatorPortType";

		// Token: 0x0400043C RID: 1084
		public const string String375 = "RegistrationCoordinatorPortType";

		// Token: 0x0400043D RID: 1085
		public const string String376 = "InvalidState";

		// Token: 0x0400043E RID: 1086
		public const string String377 = "InvalidProtocol";

		// Token: 0x0400043F RID: 1087
		public const string String378 = "InvalidParameters";

		// Token: 0x04000440 RID: 1088
		public const string String379 = "NoActivity";

		// Token: 0x04000441 RID: 1089
		public const string String380 = "ContextRefused";

		// Token: 0x04000442 RID: 1090
		public const string String381 = "AlreadyRegistered";

		// Token: 0x04000443 RID: 1091
		public const string String382 = "http://schemas.xmlsoap.org/ws/2004/10/wsat";

		// Token: 0x04000444 RID: 1092
		public const string String383 = "wsat";

		// Token: 0x04000445 RID: 1093
		public const string String384 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Completion";

		// Token: 0x04000446 RID: 1094
		public const string String385 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Durable2PC";

		// Token: 0x04000447 RID: 1095
		public const string String386 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Volatile2PC";

		// Token: 0x04000448 RID: 1096
		public const string String387 = "Prepare";

		// Token: 0x04000449 RID: 1097
		public const string String388 = "Prepared";

		// Token: 0x0400044A RID: 1098
		public const string String389 = "ReadOnly";

		// Token: 0x0400044B RID: 1099
		public const string String390 = "Commit";

		// Token: 0x0400044C RID: 1100
		public const string String391 = "Rollback";

		// Token: 0x0400044D RID: 1101
		public const string String392 = "Committed";

		// Token: 0x0400044E RID: 1102
		public const string String393 = "Aborted";

		// Token: 0x0400044F RID: 1103
		public const string String394 = "Replay";

		// Token: 0x04000450 RID: 1104
		public const string String395 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Commit";

		// Token: 0x04000451 RID: 1105
		public const string String396 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Rollback";

		// Token: 0x04000452 RID: 1106
		public const string String397 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Committed";

		// Token: 0x04000453 RID: 1107
		public const string String398 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Aborted";

		// Token: 0x04000454 RID: 1108
		public const string String399 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepare";

		// Token: 0x04000455 RID: 1109
		public const string String400 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepared";

		// Token: 0x04000456 RID: 1110
		public const string String401 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/ReadOnly";

		// Token: 0x04000457 RID: 1111
		public const string String402 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/Replay";

		// Token: 0x04000458 RID: 1112
		public const string String403 = "http://schemas.xmlsoap.org/ws/2004/10/wsat/fault";

		// Token: 0x04000459 RID: 1113
		public const string String404 = "CompletionCoordinatorPortType";

		// Token: 0x0400045A RID: 1114
		public const string String405 = "CompletionParticipantPortType";

		// Token: 0x0400045B RID: 1115
		public const string String406 = "CoordinatorPortType";

		// Token: 0x0400045C RID: 1116
		public const string String407 = "ParticipantPortType";

		// Token: 0x0400045D RID: 1117
		public const string String408 = "InconsistentInternalState";

		// Token: 0x0400045E RID: 1118
		public const string String409 = "mstx";

		// Token: 0x0400045F RID: 1119
		public const string String410 = "Enlistment";

		// Token: 0x04000460 RID: 1120
		public const string String411 = "protocol";

		// Token: 0x04000461 RID: 1121
		public const string String412 = "LocalTransactionId";

		// Token: 0x04000462 RID: 1122
		public const string String413 = "IsolationLevel";

		// Token: 0x04000463 RID: 1123
		public const string String414 = "IsolationFlags";

		// Token: 0x04000464 RID: 1124
		public const string String415 = "Description";

		// Token: 0x04000465 RID: 1125
		public const string String416 = "Loopback";

		// Token: 0x04000466 RID: 1126
		public const string String417 = "RegisterInfo";

		// Token: 0x04000467 RID: 1127
		public const string String418 = "ContextId";

		// Token: 0x04000468 RID: 1128
		public const string String419 = "TokenId";

		// Token: 0x04000469 RID: 1129
		public const string String420 = "AccessDenied";

		// Token: 0x0400046A RID: 1130
		public const string String421 = "InvalidPolicy";

		// Token: 0x0400046B RID: 1131
		public const string String422 = "CoordinatorRegistrationFailed";

		// Token: 0x0400046C RID: 1132
		public const string String423 = "TooManyEnlistments";

		// Token: 0x0400046D RID: 1133
		public const string String424 = "Disabled";

		// Token: 0x0400046E RID: 1134
		public const string String425 = "ActivityId";

		// Token: 0x0400046F RID: 1135
		public const string String426 = "http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics";

		// Token: 0x04000470 RID: 1136
		public const string String427 = "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1";

		// Token: 0x04000471 RID: 1137
		public const string String428 = "http://schemas.xmlsoap.org/ws/2002/12/policy";

		// Token: 0x04000472 RID: 1138
		public const string String429 = "FloodMessage";

		// Token: 0x04000473 RID: 1139
		public const string String430 = "LinkUtility";

		// Token: 0x04000474 RID: 1140
		public const string String431 = "Hops";

		// Token: 0x04000475 RID: 1141
		public const string String432 = "http://schemas.microsoft.com/net/2006/05/peer/HopCount";

		// Token: 0x04000476 RID: 1142
		public const string String433 = "PeerVia";

		// Token: 0x04000477 RID: 1143
		public const string String434 = "http://schemas.microsoft.com/net/2006/05/peer";

		// Token: 0x04000478 RID: 1144
		public const string String435 = "PeerFlooder";

		// Token: 0x04000479 RID: 1145
		public const string String436 = "PeerTo";

		// Token: 0x0400047A RID: 1146
		public const string String437 = "http://schemas.microsoft.com/ws/2005/05/routing";

		// Token: 0x0400047B RID: 1147
		public const string String438 = "PacketRoutable";

		// Token: 0x0400047C RID: 1148
		public const string String439 = "http://schemas.microsoft.com/ws/2005/05/addressing/none";

		// Token: 0x0400047D RID: 1149
		public const string String440 = "http://schemas.microsoft.com/ws/2005/05/envelope/none";

		// Token: 0x0400047E RID: 1150
		public const string String441 = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x0400047F RID: 1151
		public const string String442 = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x04000480 RID: 1152
		public const string String443 = "nil";

		// Token: 0x04000481 RID: 1153
		public const string String444 = "type";

		// Token: 0x04000482 RID: 1154
		public const string String445 = "char";

		// Token: 0x04000483 RID: 1155
		public const string String446 = "boolean";

		// Token: 0x04000484 RID: 1156
		public const string String447 = "byte";

		// Token: 0x04000485 RID: 1157
		public const string String448 = "unsignedByte";

		// Token: 0x04000486 RID: 1158
		public const string String449 = "short";

		// Token: 0x04000487 RID: 1159
		public const string String450 = "unsignedShort";

		// Token: 0x04000488 RID: 1160
		public const string String451 = "int";

		// Token: 0x04000489 RID: 1161
		public const string String452 = "unsignedInt";

		// Token: 0x0400048A RID: 1162
		public const string String453 = "long";

		// Token: 0x0400048B RID: 1163
		public const string String454 = "unsignedLong";

		// Token: 0x0400048C RID: 1164
		public const string String455 = "float";

		// Token: 0x0400048D RID: 1165
		public const string String456 = "double";

		// Token: 0x0400048E RID: 1166
		public const string String457 = "decimal";

		// Token: 0x0400048F RID: 1167
		public const string String458 = "dateTime";

		// Token: 0x04000490 RID: 1168
		public const string String459 = "string";

		// Token: 0x04000491 RID: 1169
		public const string String460 = "base64Binary";

		// Token: 0x04000492 RID: 1170
		public const string String461 = "anyType";

		// Token: 0x04000493 RID: 1171
		public const string String462 = "duration";

		// Token: 0x04000494 RID: 1172
		public const string String463 = "guid";

		// Token: 0x04000495 RID: 1173
		public const string String464 = "anyURI";

		// Token: 0x04000496 RID: 1174
		public const string String465 = "QName";

		// Token: 0x04000497 RID: 1175
		public const string String466 = "time";

		// Token: 0x04000498 RID: 1176
		public const string String467 = "date";

		// Token: 0x04000499 RID: 1177
		public const string String468 = "hexBinary";

		// Token: 0x0400049A RID: 1178
		public const string String469 = "gYearMonth";

		// Token: 0x0400049B RID: 1179
		public const string String470 = "gYear";

		// Token: 0x0400049C RID: 1180
		public const string String471 = "gMonthDay";

		// Token: 0x0400049D RID: 1181
		public const string String472 = "gDay";

		// Token: 0x0400049E RID: 1182
		public const string String473 = "gMonth";

		// Token: 0x0400049F RID: 1183
		public const string String474 = "integer";

		// Token: 0x040004A0 RID: 1184
		public const string String475 = "positiveInteger";

		// Token: 0x040004A1 RID: 1185
		public const string String476 = "negativeInteger";

		// Token: 0x040004A2 RID: 1186
		public const string String477 = "nonPositiveInteger";

		// Token: 0x040004A3 RID: 1187
		public const string String478 = "nonNegativeInteger";

		// Token: 0x040004A4 RID: 1188
		public const string String479 = "normalizedString";

		// Token: 0x040004A5 RID: 1189
		public const string String480 = "ConnectionLimitReached";

		// Token: 0x040004A6 RID: 1190
		public const string String481 = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x040004A7 RID: 1191
		public const string String482 = "actor";

		// Token: 0x040004A8 RID: 1192
		public const string String483 = "faultcode";

		// Token: 0x040004A9 RID: 1193
		public const string String484 = "faultstring";

		// Token: 0x040004AA RID: 1194
		public const string String485 = "faultactor";

		// Token: 0x040004AB RID: 1195
		public const string String486 = "detail";
	}
}

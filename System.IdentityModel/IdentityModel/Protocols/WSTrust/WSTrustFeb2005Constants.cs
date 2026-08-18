using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x0200020C RID: 524
	internal static class WSTrustFeb2005Constants
	{
		// Token: 0x04000EBA RID: 3770
		public const string NamespaceURI = "http://schemas.xmlsoap.org/ws/2005/02/trust";

		// Token: 0x04000EBB RID: 3771
		public const string Prefix = "t";

		// Token: 0x04000EBC RID: 3772
		public const string SchemaLocation = "http://schemas.xmlsoap.org/ws/2005/02/trust/ws-trust.xsd";

		// Token: 0x04000EBD RID: 3773
		public const string Schema = "<?xml version='1.0' encoding='utf-8'?>\r\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\r\n           xmlns:wst='http://schemas.xmlsoap.org/ws/2005/02/trust'\r\n           targetNamespace='http://schemas.xmlsoap.org/ws/2005/02/trust'\r\n           elementFormDefault='qualified' >\r\n\r\n<xs:element name='RequestSecurityToken' type='wst:RequestSecurityTokenType' />\r\n  <xs:complexType name='RequestSecurityTokenType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n<xs:element name='RequestSecurityTokenResponse' type='wst:RequestSecurityTokenResponseType' />\r\n  <xs:complexType name='RequestSecurityTokenResponseType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n        </xs:schema>";

		// Token: 0x020002C3 RID: 707
		public static class Actions
		{
			// Token: 0x0400121B RID: 4635
			public const string Issue = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";

			// Token: 0x0400121C RID: 4636
			public const string IssueResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue";

			// Token: 0x0400121D RID: 4637
			public const string Renew = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew";

			// Token: 0x0400121E RID: 4638
			public const string RenewResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew";

			// Token: 0x0400121F RID: 4639
			public const string Validate = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate";

			// Token: 0x04001220 RID: 4640
			public const string ValidateResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate";

			// Token: 0x04001221 RID: 4641
			public const string Cancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel";

			// Token: 0x04001222 RID: 4642
			public const string CancelResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel";

			// Token: 0x04001223 RID: 4643
			public const string RequestSecurityContextToken = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT";

			// Token: 0x04001224 RID: 4644
			public const string RequestSecurityContextTokenResponse = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT";

			// Token: 0x04001225 RID: 4645
			public const string RequestSecurityContextTokenCancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT-Cancel";

			// Token: 0x04001226 RID: 4646
			public const string RequestSecurityContextTokenResponseCancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT-Cancel";
		}

		// Token: 0x020002C4 RID: 708
		public static class AttributeNames
		{
			// Token: 0x04001227 RID: 4647
			public const string Allow = "Allow";

			// Token: 0x04001228 RID: 4648
			public const string Context = "Context";

			// Token: 0x04001229 RID: 4649
			public const string EncodingType = "EncodingType";

			// Token: 0x0400122A RID: 4650
			public const string OK = "OK";

			// Token: 0x0400122B RID: 4651
			public const string Type = "Type";

			// Token: 0x0400122C RID: 4652
			public const string ValueType = "ValueType";

			// Token: 0x0400122D RID: 4653
			public const string Dialect = "Dialect";
		}

		// Token: 0x020002C5 RID: 709
		public static class ElementNames
		{
			// Token: 0x0400122E RID: 4654
			public const string AllowPostdating = "AllowPostdating";

			// Token: 0x0400122F RID: 4655
			public const string AuthenticationType = "AuthenticationType";

			// Token: 0x04001230 RID: 4656
			public const string BinarySecret = "BinarySecret";

			// Token: 0x04001231 RID: 4657
			public const string BinaryExchange = "BinaryExchange";

			// Token: 0x04001232 RID: 4658
			public const string Code = "Code";

			// Token: 0x04001233 RID: 4659
			public const string Delegatable = "Delegatable";

			// Token: 0x04001234 RID: 4660
			public const string DelegateTo = "DelegateTo";

			// Token: 0x04001235 RID: 4661
			public const string Encryption = "Encryption";

			// Token: 0x04001236 RID: 4662
			public const string EncryptionAlgorithm = "EncryptionAlgorithm";

			// Token: 0x04001237 RID: 4663
			public const string EncryptWith = "EncryptWith";

			// Token: 0x04001238 RID: 4664
			public const string Entropy = "Entropy";

			// Token: 0x04001239 RID: 4665
			public const string Forwardable = "Forwardable";

			// Token: 0x0400123A RID: 4666
			public const string Lifetime = "Lifetime";

			// Token: 0x0400123B RID: 4667
			public const string Claims = "Claims";

			// Token: 0x0400123C RID: 4668
			public const string ComputedKey = "ComputedKey";

			// Token: 0x0400123D RID: 4669
			public const string ComputedKeyAlgorithm = "ComputedKeyAlgorithm";

			// Token: 0x0400123E RID: 4670
			public const string CanonicalizationAlgorithm = "CanonicalizationAlgorithm";

			// Token: 0x0400123F RID: 4671
			public const string CancelTarget = "CancelTarget";

			// Token: 0x04001240 RID: 4672
			public const string Issuer = "Issuer";

			// Token: 0x04001241 RID: 4673
			public const string KeyType = "KeyType";

			// Token: 0x04001242 RID: 4674
			public const string KeySize = "KeySize";

			// Token: 0x04001243 RID: 4675
			public const string OnBehalfOf = "OnBehalfOf";

			// Token: 0x04001244 RID: 4676
			public const string Participant = "Participant";

			// Token: 0x04001245 RID: 4677
			public const string Participants = "Participants";

			// Token: 0x04001246 RID: 4678
			public const string Primary = "Primary";

			// Token: 0x04001247 RID: 4679
			public const string ProofEncryption = "ProofEncryption";

			// Token: 0x04001248 RID: 4680
			public const string Reason = "Reason";

			// Token: 0x04001249 RID: 4681
			public const string Renewing = "Renewing";

			// Token: 0x0400124A RID: 4682
			public const string RenewTarget = "RenewTarget";

			// Token: 0x0400124B RID: 4683
			public const string RequestType = "RequestType";

			// Token: 0x0400124C RID: 4684
			public const string RequestSecurityTokenResponse = "RequestSecurityTokenResponse";

			// Token: 0x0400124D RID: 4685
			public const string RequestSecurityToken = "RequestSecurityToken";

			// Token: 0x0400124E RID: 4686
			public const string RequestedSecurityToken = "RequestedSecurityToken";

			// Token: 0x0400124F RID: 4687
			public const string RequestedProofToken = "RequestedProofToken";

			// Token: 0x04001250 RID: 4688
			public const string RequestKeySize = "RequestKeySize";

			// Token: 0x04001251 RID: 4689
			public const string RequestedAttachedReference = "RequestedAttachedReference";

			// Token: 0x04001252 RID: 4690
			public const string RequestedUnattachedReference = "RequestedUnattachedReference";

			// Token: 0x04001253 RID: 4691
			public const string RequestedTokenCancelled = "RequestedTokenCancelled";

			// Token: 0x04001254 RID: 4692
			public const string SecurityContextToken = "SecurityContextToken";

			// Token: 0x04001255 RID: 4693
			public const string SignatureAlgorithm = "SignatureAlgorithm";

			// Token: 0x04001256 RID: 4694
			public const string SignWith = "SignWith";

			// Token: 0x04001257 RID: 4695
			public const string Status = "Status";

			// Token: 0x04001258 RID: 4696
			public const string TokenType = "TokenType";

			// Token: 0x04001259 RID: 4697
			public const string UseKey = "UseKey";
		}

		// Token: 0x020002C6 RID: 710
		public static class FaultCodeValues
		{
			// Token: 0x0400125A RID: 4698
			public const string AuthenticationBadElements = "AuthenticationBadElements";

			// Token: 0x0400125B RID: 4699
			public const string BadRequest = "BadRequest";

			// Token: 0x0400125C RID: 4700
			public const string ExpiredData = "ExpiredData";

			// Token: 0x0400125D RID: 4701
			public const string FailedAuthentication = "FailedAuthentication";

			// Token: 0x0400125E RID: 4702
			public const string InvalidRequest = "InvalidRequest";

			// Token: 0x0400125F RID: 4703
			public const string InvalidScope = "InvalidScope";

			// Token: 0x04001260 RID: 4704
			public const string InvalidSecurityToken = "InvalidSecurityToken";

			// Token: 0x04001261 RID: 4705
			public const string InvalidTimeRange = "InvalidTimeRange";

			// Token: 0x04001262 RID: 4706
			public const string RenewNeeded = "RenewNeeded";

			// Token: 0x04001263 RID: 4707
			public const string RequestFailed = "RequestFailed";

			// Token: 0x04001264 RID: 4708
			public const string UnableToRenew = "UnableToRenew";
		}

		// Token: 0x020002C7 RID: 711
		public static class RequestTypes
		{
			// Token: 0x04001265 RID: 4709
			public const string Issue = "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";

			// Token: 0x04001266 RID: 4710
			public const string Renew = "http://schemas.xmlsoap.org/ws/2005/02/trust/Renew";

			// Token: 0x04001267 RID: 4711
			public const string Validate = "http://schemas.xmlsoap.org/ws/2005/02/trust/Validate";

			// Token: 0x04001268 RID: 4712
			public const string Cancel = "http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel";
		}

		// Token: 0x020002C8 RID: 712
		public static class KeyTypes
		{
			// Token: 0x04001269 RID: 4713
			public const string Asymmetric = "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey";

			// Token: 0x0400126A RID: 4714
			public const string Symmetric = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";

			// Token: 0x0400126B RID: 4715
			public const string Bearer = "http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey";
		}

		// Token: 0x020002C9 RID: 713
		public static class ComputedKeyAlgorithms
		{
			// Token: 0x0400126C RID: 4716
			public const string PSHA1 = "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";
		}
	}
}

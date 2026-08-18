using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000206 RID: 518
	internal static class WSTrust13Constants
	{
		// Token: 0x04000EA9 RID: 3753
		public const string NamespaceURI = "http://docs.oasis-open.org/ws-sx/ws-trust/200512";

		// Token: 0x04000EAA RID: 3754
		public const string Prefix = "trust";

		// Token: 0x04000EAB RID: 3755
		public const string SchemaLocation = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/ws-trust-1.3.xsd";

		// Token: 0x04000EAC RID: 3756
		public const string Schema = "<?xml version='1.0' encoding='utf-8'?>\r\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\r\n           xmlns:trust='http://docs.oasis-open.org/ws-sx/ws-trust/200512'\r\n           targetNamespace='http://docs.oasis-open.org/ws-sx/ws-trust/200512'\r\n           elementFormDefault='qualified' >\r\n\r\n<xs:element name='RequestSecurityToken' type='trust:RequestSecurityTokenType' />\r\n  <xs:complexType name='RequestSecurityTokenType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n<xs:element name='RequestSecurityTokenResponse' type='trust:RequestSecurityTokenResponseType' />\r\n  <xs:complexType name='RequestSecurityTokenResponseType' >\r\n    <xs:choice minOccurs='0' maxOccurs='unbounded' >\r\n        <xs:any namespace='##any' processContents='lax' minOccurs='0' maxOccurs='unbounded' />\r\n    </xs:choice>\r\n    <xs:attribute name='Context' type='xs:anyURI' use='optional' />\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n  <xs:element name='RequestSecurityTokenResponseCollection' type='trust:RequestSecurityTokenResponseCollectionType' />\r\n  <xs:complexType name='RequestSecurityTokenResponseCollectionType' >\r\n    <xs:sequence>\r\n      <xs:element ref='trust:RequestSecurityTokenResponse' minOccurs='1' maxOccurs='unbounded' />\r\n    </xs:sequence>\r\n    <xs:anyAttribute namespace='##other' processContents='lax' />\r\n  </xs:complexType>\r\n\r\n        </xs:schema>";

		// Token: 0x020002AF RID: 687
		public static class Actions
		{
			// Token: 0x04001175 RID: 4469
			public const string Issue = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue";

			// Token: 0x04001176 RID: 4470
			public const string IssueFinalResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal";

			// Token: 0x04001177 RID: 4471
			public const string IssueResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue";

			// Token: 0x04001178 RID: 4472
			public const string Renew = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew";

			// Token: 0x04001179 RID: 4473
			public const string RenewFinalResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal";

			// Token: 0x0400117A RID: 4474
			public const string RenewResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew";

			// Token: 0x0400117B RID: 4475
			public const string Validate = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate";

			// Token: 0x0400117C RID: 4476
			public const string ValidateFinalResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/ValidateFinal";

			// Token: 0x0400117D RID: 4477
			public const string ValidateResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Validate";

			// Token: 0x0400117E RID: 4478
			public const string Cancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel";

			// Token: 0x0400117F RID: 4479
			public const string CancelFinalResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal";

			// Token: 0x04001180 RID: 4480
			public const string CancelResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel";

			// Token: 0x04001181 RID: 4481
			public const string RequestSecurityContextToken = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT";

			// Token: 0x04001182 RID: 4482
			public const string RequestSecurityContextTokenResponse = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT";

			// Token: 0x04001183 RID: 4483
			public const string RequestSecurityContextTokenCancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT-Cancel";

			// Token: 0x04001184 RID: 4484
			public const string RequestSecurityContextTokenResponseCancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT-Cancel";
		}

		// Token: 0x020002B0 RID: 688
		public static class AttributeNames
		{
			// Token: 0x04001185 RID: 4485
			public const string Allow = "Allow";

			// Token: 0x04001186 RID: 4486
			public const string Context = "Context";

			// Token: 0x04001187 RID: 4487
			public const string EncodingType = "EncodingType";

			// Token: 0x04001188 RID: 4488
			public const string OK = "OK";

			// Token: 0x04001189 RID: 4489
			public const string Type = "Type";

			// Token: 0x0400118A RID: 4490
			public const string ValueType = "ValueType";

			// Token: 0x0400118B RID: 4491
			public const string Dialect = "Dialect";
		}

		// Token: 0x020002B1 RID: 689
		public static class ElementNames
		{
			// Token: 0x0400118C RID: 4492
			public const string AllowPostdating = "AllowPostdating";

			// Token: 0x0400118D RID: 4493
			public const string AuthenticationType = "AuthenticationType";

			// Token: 0x0400118E RID: 4494
			public const string BinarySecret = "BinarySecret";

			// Token: 0x0400118F RID: 4495
			public const string BinaryExchange = "BinaryExchange";

			// Token: 0x04001190 RID: 4496
			public const string Delegatable = "Delegatable";

			// Token: 0x04001191 RID: 4497
			public const string DelegateTo = "DelegateTo";

			// Token: 0x04001192 RID: 4498
			public const string Encryption = "Encryption";

			// Token: 0x04001193 RID: 4499
			public const string EncryptionAlgorithm = "EncryptionAlgorithm";

			// Token: 0x04001194 RID: 4500
			public const string EncryptWith = "EncryptWith";

			// Token: 0x04001195 RID: 4501
			public const string Entropy = "Entropy";

			// Token: 0x04001196 RID: 4502
			public const string Forwardable = "Forwardable";

			// Token: 0x04001197 RID: 4503
			public const string Lifetime = "Lifetime";

			// Token: 0x04001198 RID: 4504
			public const string Claims = "Claims";

			// Token: 0x04001199 RID: 4505
			public const string ComputedKey = "ComputedKey";

			// Token: 0x0400119A RID: 4506
			public const string ComputedKeyAlgorithm = "ComputedKeyAlgorithm";

			// Token: 0x0400119B RID: 4507
			public const string CanonicalizationAlgorithm = "CanonicalizationAlgorithm";

			// Token: 0x0400119C RID: 4508
			public const string CancelTarget = "CancelTarget";

			// Token: 0x0400119D RID: 4509
			public const string Code = "Code";

			// Token: 0x0400119E RID: 4510
			public const string Issuer = "Issuer";

			// Token: 0x0400119F RID: 4511
			public const string KeyType = "KeyType";

			// Token: 0x040011A0 RID: 4512
			public const string KeySize = "KeySize";

			// Token: 0x040011A1 RID: 4513
			public const string KeyWrapAlgorithm = "KeyWrapAlgorithm";

			// Token: 0x040011A2 RID: 4514
			public const string OnBehalfOf = "OnBehalfOf";

			// Token: 0x040011A3 RID: 4515
			public const string Participant = "Participant";

			// Token: 0x040011A4 RID: 4516
			public const string Participants = "Participants";

			// Token: 0x040011A5 RID: 4517
			public const string Primary = "Primary";

			// Token: 0x040011A6 RID: 4518
			public const string ProofEncryption = "ProofEncryption";

			// Token: 0x040011A7 RID: 4519
			public const string Reason = "Reason";

			// Token: 0x040011A8 RID: 4520
			public const string Renewing = "Renewing";

			// Token: 0x040011A9 RID: 4521
			public const string RenewTarget = "RenewTarget";

			// Token: 0x040011AA RID: 4522
			public const string RequestType = "RequestType";

			// Token: 0x040011AB RID: 4523
			public const string RequestSecurityTokenResponse = "RequestSecurityTokenResponse";

			// Token: 0x040011AC RID: 4524
			public const string RequestSecurityToken = "RequestSecurityToken";

			// Token: 0x040011AD RID: 4525
			public const string RequestSecurityTokenResponseCollection = "RequestSecurityTokenResponseCollection";

			// Token: 0x040011AE RID: 4526
			public const string RequestedSecurityToken = "RequestedSecurityToken";

			// Token: 0x040011AF RID: 4527
			public const string RequestedProofToken = "RequestedProofToken";

			// Token: 0x040011B0 RID: 4528
			public const string RequestKeySize = "RequestKeySize";

			// Token: 0x040011B1 RID: 4529
			public const string RequestedAttachedReference = "RequestedAttachedReference";

			// Token: 0x040011B2 RID: 4530
			public const string RequestedUnattachedReference = "RequestedUnattachedReference";

			// Token: 0x040011B3 RID: 4531
			public const string RequestedTokenCancelled = "RequestedTokenCancelled";

			// Token: 0x040011B4 RID: 4532
			public const string SecondaryParameters = "SecondaryParameters";

			// Token: 0x040011B5 RID: 4533
			public const string SecurityContextToken = "SecurityContextToken";

			// Token: 0x040011B6 RID: 4534
			public const string SignatureAlgorithm = "SignatureAlgorithm";

			// Token: 0x040011B7 RID: 4535
			public const string SignWith = "SignWith";

			// Token: 0x040011B8 RID: 4536
			public const string Status = "Status";

			// Token: 0x040011B9 RID: 4537
			public const string TokenType = "TokenType";

			// Token: 0x040011BA RID: 4538
			public const string UseKey = "UseKey";

			// Token: 0x040011BB RID: 4539
			public const string ValidateTarget = "ValidateTarget";
		}

		// Token: 0x020002B2 RID: 690
		public static class FaultCodeValues
		{
			// Token: 0x040011BC RID: 4540
			public const string AuthenticationBadElements = "AuthenticationBadElements";

			// Token: 0x040011BD RID: 4541
			public const string BadRequest = "BadRequest";

			// Token: 0x040011BE RID: 4542
			public const string ExpiredData = "ExpiredData";

			// Token: 0x040011BF RID: 4543
			public const string FailedAuthentication = "FailedAuthentication";

			// Token: 0x040011C0 RID: 4544
			public const string InvalidRequest = "InvalidRequest";

			// Token: 0x040011C1 RID: 4545
			public const string InvalidScope = "InvalidScope";

			// Token: 0x040011C2 RID: 4546
			public const string InvalidSecurityToken = "InvalidSecurityToken";

			// Token: 0x040011C3 RID: 4547
			public const string InvalidTimeRange = "InvalidTimeRange";

			// Token: 0x040011C4 RID: 4548
			public const string RenewNeeded = "RenewNeeded";

			// Token: 0x040011C5 RID: 4549
			public const string RequestFailed = "RequestFailed";

			// Token: 0x040011C6 RID: 4550
			public const string UnableToRenew = "UnableToRenew";
		}

		// Token: 0x020002B3 RID: 691
		public static class RequestTypes
		{
			// Token: 0x040011C7 RID: 4551
			public const string Issue = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue";

			// Token: 0x040011C8 RID: 4552
			public const string Renew = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew";

			// Token: 0x040011C9 RID: 4553
			public const string Validate = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Validate";

			// Token: 0x040011CA RID: 4554
			public const string Cancel = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Cancel";
		}

		// Token: 0x020002B4 RID: 692
		public static class KeyTypes
		{
			// Token: 0x040011CB RID: 4555
			public const string Asymmetric = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey";

			// Token: 0x040011CC RID: 4556
			public const string Symmetric = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey";

			// Token: 0x040011CD RID: 4557
			public const string Bearer = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer";
		}

		// Token: 0x020002B5 RID: 693
		public static class ComputedKeyAlgorithms
		{
			// Token: 0x040011CE RID: 4558
			public const string PSHA1 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1";
		}
	}
}

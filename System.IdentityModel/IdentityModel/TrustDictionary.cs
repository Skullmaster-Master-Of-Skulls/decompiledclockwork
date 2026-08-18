using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000CD RID: 205
	internal class TrustDictionary
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00004469 File Offset: 0x00002669
		public TrustDictionary()
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00004469 File Offset: 0x00002669
		public TrustDictionary(IdentityModelDictionary dictionary)
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00004469 File Offset: 0x00002669
		public TrustDictionary(IXmlDictionary dictionary)
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000179F4 File Offset: 0x00015BF4
		private XmlDictionaryString LookupDictionaryString(IXmlDictionary dictionary, string value)
		{
			XmlDictionaryString result;
			if (!dictionary.TryLookup(value, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("XDCannotFindValueInDictionaryString", new object[]
				{
					value
				}));
			}
			return result;
		}

		// Token: 0x040005A7 RID: 1447
		public XmlDictionaryString RequestSecurityTokenResponseCollection;

		// Token: 0x040005A8 RID: 1448
		public XmlDictionaryString Namespace;

		// Token: 0x040005A9 RID: 1449
		public XmlDictionaryString BinarySecretClauseType;

		// Token: 0x040005AA RID: 1450
		public XmlDictionaryString CombinedHashLabel;

		// Token: 0x040005AB RID: 1451
		public XmlDictionaryString RequestSecurityTokenResponse;

		// Token: 0x040005AC RID: 1452
		public XmlDictionaryString TokenType;

		// Token: 0x040005AD RID: 1453
		public XmlDictionaryString KeySize;

		// Token: 0x040005AE RID: 1454
		public XmlDictionaryString RequestedTokenReference;

		// Token: 0x040005AF RID: 1455
		public XmlDictionaryString AppliesTo;

		// Token: 0x040005B0 RID: 1456
		public XmlDictionaryString Authenticator;

		// Token: 0x040005B1 RID: 1457
		public XmlDictionaryString CombinedHash;

		// Token: 0x040005B2 RID: 1458
		public XmlDictionaryString BinaryExchange;

		// Token: 0x040005B3 RID: 1459
		public XmlDictionaryString Lifetime;

		// Token: 0x040005B4 RID: 1460
		public XmlDictionaryString RequestedSecurityToken;

		// Token: 0x040005B5 RID: 1461
		public XmlDictionaryString Entropy;

		// Token: 0x040005B6 RID: 1462
		public XmlDictionaryString RequestedProofToken;

		// Token: 0x040005B7 RID: 1463
		public XmlDictionaryString ComputedKey;

		// Token: 0x040005B8 RID: 1464
		public XmlDictionaryString RequestSecurityToken;

		// Token: 0x040005B9 RID: 1465
		public XmlDictionaryString RequestType;

		// Token: 0x040005BA RID: 1466
		public XmlDictionaryString Context;

		// Token: 0x040005BB RID: 1467
		public XmlDictionaryString BinarySecret;

		// Token: 0x040005BC RID: 1468
		public XmlDictionaryString Type;

		// Token: 0x040005BD RID: 1469
		public XmlDictionaryString SpnegoValueTypeUri;

		// Token: 0x040005BE RID: 1470
		public XmlDictionaryString TlsnegoValueTypeUri;

		// Token: 0x040005BF RID: 1471
		public XmlDictionaryString Prefix;

		// Token: 0x040005C0 RID: 1472
		public XmlDictionaryString RequestSecurityTokenIssuance;

		// Token: 0x040005C1 RID: 1473
		public XmlDictionaryString RequestSecurityTokenIssuanceResponse;

		// Token: 0x040005C2 RID: 1474
		public XmlDictionaryString RequestTypeIssue;

		// Token: 0x040005C3 RID: 1475
		public XmlDictionaryString SymmetricKeyBinarySecret;

		// Token: 0x040005C4 RID: 1476
		public XmlDictionaryString Psha1ComputedKeyUri;

		// Token: 0x040005C5 RID: 1477
		public XmlDictionaryString NonceBinarySecret;

		// Token: 0x040005C6 RID: 1478
		public XmlDictionaryString RenewTarget;

		// Token: 0x040005C7 RID: 1479
		public XmlDictionaryString CloseTarget;

		// Token: 0x040005C8 RID: 1480
		public XmlDictionaryString RequestedTokenClosed;

		// Token: 0x040005C9 RID: 1481
		public XmlDictionaryString RequestedAttachedReference;

		// Token: 0x040005CA RID: 1482
		public XmlDictionaryString RequestedUnattachedReference;

		// Token: 0x040005CB RID: 1483
		public XmlDictionaryString IssuedTokensHeader;

		// Token: 0x040005CC RID: 1484
		public XmlDictionaryString RequestTypeRenew;

		// Token: 0x040005CD RID: 1485
		public XmlDictionaryString RequestTypeClose;

		// Token: 0x040005CE RID: 1486
		public XmlDictionaryString KeyType;

		// Token: 0x040005CF RID: 1487
		public XmlDictionaryString SymmetricKeyType;

		// Token: 0x040005D0 RID: 1488
		public XmlDictionaryString PublicKeyType;

		// Token: 0x040005D1 RID: 1489
		public XmlDictionaryString Claims;

		// Token: 0x040005D2 RID: 1490
		public XmlDictionaryString InvalidRequestFaultCode;

		// Token: 0x040005D3 RID: 1491
		public XmlDictionaryString FailedAuthenticationFaultCode;

		// Token: 0x040005D4 RID: 1492
		public XmlDictionaryString UseKey;

		// Token: 0x040005D5 RID: 1493
		public XmlDictionaryString SignWith;

		// Token: 0x040005D6 RID: 1494
		public XmlDictionaryString EncryptWith;

		// Token: 0x040005D7 RID: 1495
		public XmlDictionaryString EncryptionAlgorithm;

		// Token: 0x040005D8 RID: 1496
		public XmlDictionaryString CanonicalizationAlgorithm;

		// Token: 0x040005D9 RID: 1497
		public XmlDictionaryString ComputedKeyAlgorithm;

		// Token: 0x040005DA RID: 1498
		public XmlDictionaryString AsymmetricKeyBinarySecret;

		// Token: 0x040005DB RID: 1499
		public XmlDictionaryString RequestSecurityTokenCollectionIssuanceFinalResponse;

		// Token: 0x040005DC RID: 1500
		public XmlDictionaryString RequestSecurityTokenRenewal;

		// Token: 0x040005DD RID: 1501
		public XmlDictionaryString RequestSecurityTokenRenewalResponse;

		// Token: 0x040005DE RID: 1502
		public XmlDictionaryString RequestSecurityTokenCollectionRenewalFinalResponse;

		// Token: 0x040005DF RID: 1503
		public XmlDictionaryString RequestSecurityTokenCancellation;

		// Token: 0x040005E0 RID: 1504
		public XmlDictionaryString RequestSecurityTokenCancellationResponse;

		// Token: 0x040005E1 RID: 1505
		public XmlDictionaryString RequestSecurityTokenCollectionCancellationFinalResponse;

		// Token: 0x040005E2 RID: 1506
		public XmlDictionaryString KeyWrapAlgorithm;

		// Token: 0x040005E3 RID: 1507
		public XmlDictionaryString BearerKeyType;

		// Token: 0x040005E4 RID: 1508
		public XmlDictionaryString SecondaryParameters;

		// Token: 0x040005E5 RID: 1509
		public XmlDictionaryString Dialect;

		// Token: 0x040005E6 RID: 1510
		public XmlDictionaryString DialectType;
	}
}

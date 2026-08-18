using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F8 RID: 760
	internal sealed class SctClaimDictionary : XmlDictionary
	{
		// Token: 0x06001992 RID: 6546 RVA: 0x0005F878 File Offset: 0x0005DA78
		private SctClaimDictionary()
		{
			this.securityContextToken = this.Add("SecurityContextSecurityToken");
			this.version = this.Add("Version");
			this.contextId = this.Add("ContextId");
			this.id = this.Add("Id");
			this.key = this.Add("Key");
			this.isCookieMode = this.Add("IsCookieMode");
			this.serviceContractId = this.Add("ServiceContractId");
			this.effectiveTime = this.Add("EffectiveTime");
			this.expiryTime = this.Add("ExpiryTime");
			this.keyGeneration = this.Add("KeyGeneration");
			this.keyEffectiveTime = this.Add("KeyEffectiveTime");
			this.keyExpiryTime = this.Add("KeyExpiryTime");
			this.claim = this.Add("Claim");
			this.claimSets = this.Add("ClaimSets");
			this.claimSet = this.Add("ClaimSet");
			this.identities = this.Add("Identities");
			this.primaryIdentity = this.Add("PrimaryIdentity");
			this.primaryIssuer = this.Add("PrimaryIssuer");
			this.x509CertificateClaimSet = this.Add("X509CertificateClaimSet");
			this.systemClaimSet = this.Add("SystemClaimSet");
			this.windowsClaimSet = this.Add("WindowsClaimSet");
			this.anonymousClaimSet = this.Add("AnonymousClaimSet");
			this.binaryClaim = this.Add("BinaryClaim");
			this.dnsClaim = this.Add("DnsClaim");
			this.genericIdentity = this.Add("GenericIdentity");
			this.authenticationType = this.Add("AuthenticationType");
			this.right = this.Add("Right");
			this.hashClaim = this.Add("HashClaim");
			this.mailAddressClaim = this.Add("MailAddressClaim");
			this.nameClaim = this.Add("NameClaim");
			this.rsaClaim = this.Add("RsaClaim");
			this.spnClaim = this.Add("SpnClaim");
			this.systemClaim = this.Add("SystemClaim");
			this.upnClaim = this.Add("UpnClaim");
			this.urlClaim = this.Add("UrlClaim");
			this.windowsSidClaim = this.Add("WindowsSidClaim");
			this.denyOnlySidClaim = this.Add("DenyOnlySidClaim");
			this.windowsSidIdentity = this.Add("WindowsSidIdentity");
			this.x500DistinguishedNameClaim = this.Add("X500DistinguishedClaim");
			this.x509ThumbprintClaim = this.Add("X509ThumbprintClaim");
			this.name = this.Add("Name");
			this.sid = this.Add("Sid");
			this.value = this.Add("Value");
			this.nullValue = this.Add("Null");
			this.genericXmlToken = this.Add("GenericXmlSecurityToken");
			this.tokenType = this.Add("TokenType");
			this.internalTokenReference = this.Add("InternalTokenReference");
			this.externalTokenReference = this.Add("ExternalTokenReference");
			this.tokenXml = this.Add("TokenXml");
			this.emptyString = this.Add(string.Empty);
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0005FBDD File Offset: 0x0005DDDD
		public static SctClaimDictionary Instance
		{
			get
			{
				return SctClaimDictionary.instance;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0005FBE4 File Offset: 0x0005DDE4
		public XmlDictionaryString Claim
		{
			get
			{
				return this.claim;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x0005FBEC File Offset: 0x0005DDEC
		public XmlDictionaryString ClaimSets
		{
			get
			{
				return this.claimSets;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x0005FBF4 File Offset: 0x0005DDF4
		public XmlDictionaryString ClaimSet
		{
			get
			{
				return this.claimSet;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x0005FBFC File Offset: 0x0005DDFC
		public XmlDictionaryString PrimaryIssuer
		{
			get
			{
				return this.primaryIssuer;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0005FC04 File Offset: 0x0005DE04
		public XmlDictionaryString Identities
		{
			get
			{
				return this.identities;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x0005FC0C File Offset: 0x0005DE0C
		public XmlDictionaryString PrimaryIdentity
		{
			get
			{
				return this.primaryIdentity;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0005FC14 File Offset: 0x0005DE14
		public XmlDictionaryString X509CertificateClaimSet
		{
			get
			{
				return this.x509CertificateClaimSet;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0005FC1C File Offset: 0x0005DE1C
		public XmlDictionaryString SystemClaimSet
		{
			get
			{
				return this.systemClaimSet;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0005FC24 File Offset: 0x0005DE24
		public XmlDictionaryString WindowsClaimSet
		{
			get
			{
				return this.windowsClaimSet;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0005FC2C File Offset: 0x0005DE2C
		public XmlDictionaryString AnonymousClaimSet
		{
			get
			{
				return this.anonymousClaimSet;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0005FC34 File Offset: 0x0005DE34
		public XmlDictionaryString ContextId
		{
			get
			{
				return this.contextId;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0005FC3C File Offset: 0x0005DE3C
		public XmlDictionaryString BinaryClaim
		{
			get
			{
				return this.binaryClaim;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0005FC44 File Offset: 0x0005DE44
		public XmlDictionaryString DnsClaim
		{
			get
			{
				return this.dnsClaim;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0005FC4C File Offset: 0x0005DE4C
		public XmlDictionaryString GenericIdentity
		{
			get
			{
				return this.genericIdentity;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0005FC54 File Offset: 0x0005DE54
		public XmlDictionaryString AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x0005FC5C File Offset: 0x0005DE5C
		public XmlDictionaryString Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0005FC64 File Offset: 0x0005DE64
		public XmlDictionaryString HashClaim
		{
			get
			{
				return this.hashClaim;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0005FC6C File Offset: 0x0005DE6C
		public XmlDictionaryString MailAddressClaim
		{
			get
			{
				return this.mailAddressClaim;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0005FC74 File Offset: 0x0005DE74
		public XmlDictionaryString NameClaim
		{
			get
			{
				return this.nameClaim;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0005FC7C File Offset: 0x0005DE7C
		public XmlDictionaryString RsaClaim
		{
			get
			{
				return this.rsaClaim;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0005FC84 File Offset: 0x0005DE84
		public XmlDictionaryString SpnClaim
		{
			get
			{
				return this.spnClaim;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0005FC8C File Offset: 0x0005DE8C
		public XmlDictionaryString SystemClaim
		{
			get
			{
				return this.systemClaim;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0005FC94 File Offset: 0x0005DE94
		public XmlDictionaryString UpnClaim
		{
			get
			{
				return this.upnClaim;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0005FC9C File Offset: 0x0005DE9C
		public XmlDictionaryString UrlClaim
		{
			get
			{
				return this.urlClaim;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x0005FCA4 File Offset: 0x0005DEA4
		public XmlDictionaryString WindowsSidClaim
		{
			get
			{
				return this.windowsSidClaim;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0005FCAC File Offset: 0x0005DEAC
		public XmlDictionaryString DenyOnlySidClaim
		{
			get
			{
				return this.denyOnlySidClaim;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0005FCB4 File Offset: 0x0005DEB4
		public XmlDictionaryString WindowsSidIdentity
		{
			get
			{
				return this.windowsSidIdentity;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0005FCBC File Offset: 0x0005DEBC
		public XmlDictionaryString X500DistinguishedNameClaim
		{
			get
			{
				return this.x500DistinguishedNameClaim;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x0005FCC4 File Offset: 0x0005DEC4
		public XmlDictionaryString X509ThumbprintClaim
		{
			get
			{
				return this.x509ThumbprintClaim;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0005FCCC File Offset: 0x0005DECC
		public XmlDictionaryString EffectiveTime
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x0005FCD4 File Offset: 0x0005DED4
		public XmlDictionaryString ExpiryTime
		{
			get
			{
				return this.expiryTime;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0005FCDC File Offset: 0x0005DEDC
		public XmlDictionaryString Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0005FCE4 File Offset: 0x0005DEE4
		public XmlDictionaryString IsCookieMode
		{
			get
			{
				return this.isCookieMode;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0005FCEC File Offset: 0x0005DEEC
		public XmlDictionaryString Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0005FCF4 File Offset: 0x0005DEF4
		public XmlDictionaryString Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0005FCFC File Offset: 0x0005DEFC
		public XmlDictionaryString Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0005FD04 File Offset: 0x0005DF04
		public XmlDictionaryString NullValue
		{
			get
			{
				return this.nullValue;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0005FD0C File Offset: 0x0005DF0C
		public XmlDictionaryString SecurityContextSecurityToken
		{
			get
			{
				return this.securityContextToken;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0005FD14 File Offset: 0x0005DF14
		public XmlDictionaryString ServiceContractId
		{
			get
			{
				return this.serviceContractId;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0005FD1C File Offset: 0x0005DF1C
		public XmlDictionaryString Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0005FD24 File Offset: 0x0005DF24
		public XmlDictionaryString Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0005FD2C File Offset: 0x0005DF2C
		public XmlDictionaryString GenericXmlSecurityToken
		{
			get
			{
				return this.genericXmlToken;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x0005FD34 File Offset: 0x0005DF34
		public XmlDictionaryString TokenType
		{
			get
			{
				return this.tokenType;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0005FD3C File Offset: 0x0005DF3C
		public XmlDictionaryString TokenXml
		{
			get
			{
				return this.tokenXml;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0005FD44 File Offset: 0x0005DF44
		public XmlDictionaryString InternalTokenReference
		{
			get
			{
				return this.internalTokenReference;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0005FD4C File Offset: 0x0005DF4C
		public XmlDictionaryString ExternalTokenReference
		{
			get
			{
				return this.externalTokenReference;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0005FD54 File Offset: 0x0005DF54
		public XmlDictionaryString EmptyString
		{
			get
			{
				return this.emptyString;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0005FD5C File Offset: 0x0005DF5C
		public XmlDictionaryString KeyGeneration
		{
			get
			{
				return this.keyGeneration;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0005FD64 File Offset: 0x0005DF64
		public XmlDictionaryString KeyEffectiveTime
		{
			get
			{
				return this.keyEffectiveTime;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0005FD6C File Offset: 0x0005DF6C
		public XmlDictionaryString KeyExpiryTime
		{
			get
			{
				return this.keyExpiryTime;
			}
		}

		// Token: 0x04001CA6 RID: 7334
		private static readonly SctClaimDictionary instance = new SctClaimDictionary();

		// Token: 0x04001CA7 RID: 7335
		private XmlDictionaryString claim;

		// Token: 0x04001CA8 RID: 7336
		private XmlDictionaryString claimSets;

		// Token: 0x04001CA9 RID: 7337
		private XmlDictionaryString claimSet;

		// Token: 0x04001CAA RID: 7338
		private XmlDictionaryString identities;

		// Token: 0x04001CAB RID: 7339
		private XmlDictionaryString primaryIdentity;

		// Token: 0x04001CAC RID: 7340
		private XmlDictionaryString primaryIssuer;

		// Token: 0x04001CAD RID: 7341
		private XmlDictionaryString x509CertificateClaimSet;

		// Token: 0x04001CAE RID: 7342
		private XmlDictionaryString systemClaimSet;

		// Token: 0x04001CAF RID: 7343
		private XmlDictionaryString windowsClaimSet;

		// Token: 0x04001CB0 RID: 7344
		private XmlDictionaryString anonymousClaimSet;

		// Token: 0x04001CB1 RID: 7345
		private XmlDictionaryString binaryClaim;

		// Token: 0x04001CB2 RID: 7346
		private XmlDictionaryString dnsClaim;

		// Token: 0x04001CB3 RID: 7347
		private XmlDictionaryString hashClaim;

		// Token: 0x04001CB4 RID: 7348
		private XmlDictionaryString mailAddressClaim;

		// Token: 0x04001CB5 RID: 7349
		private XmlDictionaryString nameClaim;

		// Token: 0x04001CB6 RID: 7350
		private XmlDictionaryString rsaClaim;

		// Token: 0x04001CB7 RID: 7351
		private XmlDictionaryString spnClaim;

		// Token: 0x04001CB8 RID: 7352
		private XmlDictionaryString systemClaim;

		// Token: 0x04001CB9 RID: 7353
		private XmlDictionaryString upnClaim;

		// Token: 0x04001CBA RID: 7354
		private XmlDictionaryString urlClaim;

		// Token: 0x04001CBB RID: 7355
		private XmlDictionaryString windowsSidClaim;

		// Token: 0x04001CBC RID: 7356
		private XmlDictionaryString denyOnlySidClaim;

		// Token: 0x04001CBD RID: 7357
		private XmlDictionaryString x500DistinguishedNameClaim;

		// Token: 0x04001CBE RID: 7358
		private XmlDictionaryString x509ThumbprintClaim;

		// Token: 0x04001CBF RID: 7359
		private XmlDictionaryString right;

		// Token: 0x04001CC0 RID: 7360
		private XmlDictionaryString windowsSidIdentity;

		// Token: 0x04001CC1 RID: 7361
		private XmlDictionaryString genericIdentity;

		// Token: 0x04001CC2 RID: 7362
		private XmlDictionaryString authenticationType;

		// Token: 0x04001CC3 RID: 7363
		private XmlDictionaryString contextId;

		// Token: 0x04001CC4 RID: 7364
		private XmlDictionaryString effectiveTime;

		// Token: 0x04001CC5 RID: 7365
		private XmlDictionaryString expiryTime;

		// Token: 0x04001CC6 RID: 7366
		private XmlDictionaryString id;

		// Token: 0x04001CC7 RID: 7367
		private XmlDictionaryString isCookieMode;

		// Token: 0x04001CC8 RID: 7368
		private XmlDictionaryString key;

		// Token: 0x04001CC9 RID: 7369
		private XmlDictionaryString name;

		// Token: 0x04001CCA RID: 7370
		private XmlDictionaryString sid;

		// Token: 0x04001CCB RID: 7371
		private XmlDictionaryString nullValue;

		// Token: 0x04001CCC RID: 7372
		private XmlDictionaryString securityContextToken;

		// Token: 0x04001CCD RID: 7373
		private XmlDictionaryString serviceContractId;

		// Token: 0x04001CCE RID: 7374
		private XmlDictionaryString value;

		// Token: 0x04001CCF RID: 7375
		private XmlDictionaryString version;

		// Token: 0x04001CD0 RID: 7376
		private XmlDictionaryString genericXmlToken;

		// Token: 0x04001CD1 RID: 7377
		private XmlDictionaryString tokenType;

		// Token: 0x04001CD2 RID: 7378
		private XmlDictionaryString tokenXml;

		// Token: 0x04001CD3 RID: 7379
		private XmlDictionaryString internalTokenReference;

		// Token: 0x04001CD4 RID: 7380
		private XmlDictionaryString externalTokenReference;

		// Token: 0x04001CD5 RID: 7381
		private XmlDictionaryString keyGeneration;

		// Token: 0x04001CD6 RID: 7382
		private XmlDictionaryString keyEffectiveTime;

		// Token: 0x04001CD7 RID: 7383
		private XmlDictionaryString keyExpiryTime;

		// Token: 0x04001CD8 RID: 7384
		private XmlDictionaryString emptyString;
	}
}

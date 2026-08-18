using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Policy;
using System.IO;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000122 RID: 290
	public class GenericXmlSecurityToken : SecurityToken
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00021410 File Offset: 0x0001F610
		public GenericXmlSecurityToken(XmlElement tokenXml, SecurityToken proofToken, DateTime effectiveTime, DateTime expirationTime, SecurityKeyIdentifierClause internalTokenReference, SecurityKeyIdentifierClause externalTokenReference, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (tokenXml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenXml");
			}
			this.id = GenericXmlSecurityToken.GetId(tokenXml);
			this.tokenXml = tokenXml;
			this.proofToken = proofToken;
			this.effectiveTime = effectiveTime.ToUniversalTime();
			this.expirationTime = expirationTime.ToUniversalTime();
			this.internalTokenReference = internalTokenReference;
			this.externalTokenReference = externalTokenReference;
			this.authorizationPolicies = (authorizationPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance);
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002148B File Offset: 0x0001F68B
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00021493 File Offset: 0x0001F693
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002149B File Offset: 0x0001F69B
		public override DateTime ValidTo
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000214A3 File Offset: 0x0001F6A3
		public SecurityKeyIdentifierClause InternalTokenReference
		{
			get
			{
				return this.internalTokenReference;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000214AB File Offset: 0x0001F6AB
		public SecurityKeyIdentifierClause ExternalTokenReference
		{
			get
			{
				return this.externalTokenReference;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000214B3 File Offset: 0x0001F6B3
		public XmlElement TokenXml
		{
			get
			{
				return this.tokenXml;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000214BB File Offset: 0x0001F6BB
		public SecurityToken ProofToken
		{
			get
			{
				return this.proofToken;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000214C3 File Offset: 0x0001F6C3
		public ReadOnlyCollection<IAuthorizationPolicy> AuthorizationPolicies
		{
			get
			{
				return this.authorizationPolicies;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x000214CB File Offset: 0x0001F6CB
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				if (this.proofToken != null)
				{
					return this.proofToken.SecurityKeys;
				}
				return EmptyReadOnlyCollection<SecurityKey>.Instance;
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000214E8 File Offset: 0x0001F6E8
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			stringWriter.WriteLine("Generic XML token:");
			stringWriter.WriteLine("   validFrom: {0}", this.ValidFrom);
			stringWriter.WriteLine("   validTo: {0}", this.ValidTo);
			if (this.internalTokenReference != null)
			{
				stringWriter.WriteLine("   InternalTokenReference: {0}", this.internalTokenReference);
			}
			if (this.externalTokenReference != null)
			{
				stringWriter.WriteLine("   ExternalTokenReference: {0}", this.externalTokenReference);
			}
			stringWriter.WriteLine("   Token Element: ({0}, {1})", this.tokenXml.LocalName, this.tokenXml.NamespaceURI);
			return stringWriter.ToString();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00021590 File Offset: 0x0001F790
		private static string GetId(XmlElement tokenXml)
		{
			if (tokenXml != null)
			{
				string attribute = tokenXml.GetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				if (string.IsNullOrEmpty(attribute))
				{
					attribute = tokenXml.GetAttribute("AssertionID");
					if (string.IsNullOrEmpty(attribute))
					{
						attribute = tokenXml.GetAttribute("Id");
					}
					if (string.IsNullOrEmpty(attribute))
					{
						attribute = tokenXml.GetAttribute("ID");
					}
				}
				if (!string.IsNullOrEmpty(attribute))
				{
					return attribute;
				}
			}
			return null;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000215F8 File Offset: 0x0001F7F8
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return (this.internalTokenReference != null && typeof(T) == this.internalTokenReference.GetType()) || (this.externalTokenReference != null && typeof(T) == this.externalTokenReference.GetType());
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00021654 File Offset: 0x0001F854
		public override T CreateKeyIdentifierClause<T>()
		{
			if (this.internalTokenReference != null && typeof(T) == this.internalTokenReference.GetType())
			{
				return (T)((object)this.internalTokenReference);
			}
			if (this.externalTokenReference != null && typeof(T) == this.externalTokenReference.GetType())
			{
				return (T)((object)this.externalTokenReference);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("UnableToCreateTokenReference")));
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000216DA File Offset: 0x0001F8DA
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return (this.internalTokenReference != null && this.internalTokenReference.Matches(keyIdentifierClause)) || (this.externalTokenReference != null && this.externalTokenReference.Matches(keyIdentifierClause));
		}

		// Token: 0x04000AE6 RID: 2790
		private const int SupportedPersistanceVersion = 1;

		// Token: 0x04000AE7 RID: 2791
		private string id;

		// Token: 0x04000AE8 RID: 2792
		private SecurityToken proofToken;

		// Token: 0x04000AE9 RID: 2793
		private SecurityKeyIdentifierClause internalTokenReference;

		// Token: 0x04000AEA RID: 2794
		private SecurityKeyIdentifierClause externalTokenReference;

		// Token: 0x04000AEB RID: 2795
		private XmlElement tokenXml;

		// Token: 0x04000AEC RID: 2796
		private ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;

		// Token: 0x04000AED RID: 2797
		private DateTime effectiveTime;

		// Token: 0x04000AEE RID: 2798
		private DateTime expirationTime;
	}
}

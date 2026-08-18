using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200039C RID: 924
	public class IssuedSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x0600226C RID: 8812 RVA: 0x0007D984 File Offset: 0x0007BB84
		protected IssuedSecurityTokenParameters(IssuedSecurityTokenParameters other) : base(other)
		{
			this.defaultMessageSecurityVersion = other.defaultMessageSecurityVersion;
			this.issuerAddress = other.issuerAddress;
			this.keyType = other.keyType;
			this.tokenType = other.tokenType;
			this.keySize = other.keySize;
			this.useStrTransform = other.useStrTransform;
			foreach (XmlElement xmlElement in other.additionalRequestParameters)
			{
				this.additionalRequestParameters.Add((XmlElement)xmlElement.Clone());
			}
			foreach (ClaimTypeRequirement item in other.claimTypeRequirements)
			{
				this.claimTypeRequirements.Add(item);
			}
			if (other.issuerBinding != null)
			{
				this.issuerBinding = new CustomBinding(other.issuerBinding);
			}
			this.issuerMetadataAddress = other.issuerMetadataAddress;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0007DAB8 File Offset: 0x0007BCB8
		public IssuedSecurityTokenParameters() : this(null, null, null)
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0007DAC3 File Offset: 0x0007BCC3
		public IssuedSecurityTokenParameters(string tokenType) : this(tokenType, null, null)
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0007DACE File Offset: 0x0007BCCE
		public IssuedSecurityTokenParameters(string tokenType, EndpointAddress issuerAddress) : this(tokenType, issuerAddress, null)
		{
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0007DAD9 File Offset: 0x0007BCD9
		public IssuedSecurityTokenParameters(string tokenType, EndpointAddress issuerAddress, Binding issuerBinding)
		{
			this.tokenType = tokenType;
			this.issuerAddress = issuerAddress;
			this.issuerBinding = issuerBinding;
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x0007DB17 File Offset: 0x0007BD17
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return this.KeyType == SecurityKeyType.AsymmetricKey;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0007DB22 File Offset: 0x0007BD22
		public Collection<XmlElement> AdditionalRequestParameters
		{
			get
			{
				return this.additionalRequestParameters;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x0007DB2A File Offset: 0x0007BD2A
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x0007DB32 File Offset: 0x0007BD32
		public MessageSecurityVersion DefaultMessageSecurityVersion
		{
			get
			{
				return this.defaultMessageSecurityVersion;
			}
			set
			{
				this.defaultMessageSecurityVersion = value;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x0007DB3B File Offset: 0x0007BD3B
		internal Collection<IssuedSecurityTokenParameters.AlternativeIssuerEndpoint> AlternativeIssuerEndpoints
		{
			get
			{
				return this.alternativeIssuerEndpoints;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0007DB43 File Offset: 0x0007BD43
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x0007DB4B File Offset: 0x0007BD4B
		public EndpointAddress IssuerAddress
		{
			get
			{
				return this.issuerAddress;
			}
			set
			{
				this.issuerAddress = value;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0007DB54 File Offset: 0x0007BD54
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x0007DB5C File Offset: 0x0007BD5C
		public EndpointAddress IssuerMetadataAddress
		{
			get
			{
				return this.issuerMetadataAddress;
			}
			set
			{
				this.issuerMetadataAddress = value;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0007DB65 File Offset: 0x0007BD65
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x0007DB6D File Offset: 0x0007BD6D
		public Binding IssuerBinding
		{
			get
			{
				return this.issuerBinding;
			}
			set
			{
				this.issuerBinding = value;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0007DB76 File Offset: 0x0007BD76
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x0007DB7E File Offset: 0x0007BD7E
		public SecurityKeyType KeyType
		{
			get
			{
				return this.keyType;
			}
			set
			{
				SecurityKeyTypeHelper.Validate(value);
				this.keyType = value;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0007DB8D File Offset: 0x0007BD8D
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0007DB95 File Offset: 0x0007BD95
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeNonNegative")));
				}
				this.keySize = value;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x0007DBC1 File Offset: 0x0007BDC1
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x0007DBC9 File Offset: 0x0007BDC9
		public bool UseStrTransform
		{
			get
			{
				return this.useStrTransform;
			}
			set
			{
				this.useStrTransform = value;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x0007DBD2 File Offset: 0x0007BDD2
		public Collection<ClaimTypeRequirement> ClaimTypeRequirements
		{
			get
			{
				return this.claimTypeRequirements;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0007DBDA File Offset: 0x0007BDDA
		// (set) Token: 0x06002284 RID: 8836 RVA: 0x0007DBE2 File Offset: 0x0007BDE2
		public string TokenType
		{
			get
			{
				return this.tokenType;
			}
			set
			{
				this.tokenType = value;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0007DBEB File Offset: 0x0007BDEB
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x0007DBEE File Offset: 0x0007BDEE
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0007DBF1 File Offset: 0x0007BDF1
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0007DBF4 File Offset: 0x0007BDF4
		protected override SecurityTokenParameters CloneCore()
		{
			return new IssuedSecurityTokenParameters(this);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0007DBFC File Offset: 0x0007BDFC
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			if (token is GenericXmlSecurityToken)
			{
				return base.CreateGenericXmlTokenKeyIdentifierClause(token, referenceStyle);
			}
			return base.CreateKeyIdentifierClause<SamlAssertionKeyIdentifierClause, SamlAssertionKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0007DC18 File Offset: 0x0007BE18
		internal void SetRequestParameters(Collection<XmlElement> requestParameters, TrustDriver trustDriver)
		{
			if (requestParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestParameters");
			}
			if (trustDriver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustDriver");
			}
			Collection<XmlElement> collection = new Collection<XmlElement>();
			foreach (XmlElement xmlElement in requestParameters)
			{
				int num;
				SecurityKeyType securityKeyType;
				string text;
				if (trustDriver.TryParseKeySizeElement(xmlElement, out num))
				{
					this.keySize = num;
				}
				else if (trustDriver.TryParseKeyTypeElement(xmlElement, out securityKeyType))
				{
					this.KeyType = securityKeyType;
				}
				else if (trustDriver.TryParseTokenTypeElement(xmlElement, out text))
				{
					this.TokenType = text;
				}
				else if (trustDriver.StandardsManager.TrustVersion == TrustVersion.WSTrustFeb2005)
				{
					Collection<XmlElement> collection2;
					if (trustDriver.TryParseRequiredClaimsElement(xmlElement, out collection2))
					{
						Collection<XmlElement> collection3 = new Collection<XmlElement>();
						foreach (XmlElement xmlElement2 in collection2)
						{
							if (xmlElement2.LocalName == "ClaimType" && xmlElement2.NamespaceURI == "http://schemas.xmlsoap.org/ws/2005/05/identity")
							{
								string attribute = xmlElement2.GetAttribute("Uri", string.Empty);
								if (!string.IsNullOrEmpty(attribute))
								{
									string attribute2 = xmlElement2.GetAttribute("Optional", string.Empty);
									ClaimTypeRequirement item;
									if (string.IsNullOrEmpty(attribute2))
									{
										item = new ClaimTypeRequirement(attribute);
									}
									else
									{
										item = new ClaimTypeRequirement(attribute, XmlConvert.ToBoolean(attribute2));
									}
									this.claimTypeRequirements.Add(item);
								}
							}
							else
							{
								collection3.Add(xmlElement2);
							}
						}
						if (collection3.Count > 0)
						{
							collection.Add(trustDriver.CreateRequiredClaimsElement(collection3));
						}
					}
					else
					{
						collection.Add(xmlElement);
					}
				}
			}
			collection = trustDriver.ProcessUnknownRequestParameters(collection, requestParameters);
			if (collection.Count > 0)
			{
				for (int i = 0; i < collection.Count; i++)
				{
					this.AdditionalRequestParameters.Add(collection[i]);
				}
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x0007DE40 File Offset: 0x0007C040
		public Collection<XmlElement> CreateRequestParameters(MessageSecurityVersion messageSecurityVersion, SecurityTokenSerializer securityTokenSerializer)
		{
			return this.CreateRequestParameters(SecurityUtils.CreateSecurityStandardsManager(messageSecurityVersion, securityTokenSerializer).TrustDriver);
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0007DE54 File Offset: 0x0007C054
		internal Collection<XmlElement> CreateRequestParameters(TrustDriver driver)
		{
			if (driver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("driver");
			}
			Collection<XmlElement> collection = new Collection<XmlElement>();
			if (this.tokenType != null)
			{
				collection.Add(driver.CreateTokenTypeElement(this.tokenType));
			}
			collection.Add(driver.CreateKeyTypeElement(this.keyType));
			if (this.keySize != 0)
			{
				collection.Add(driver.CreateKeySizeElement(this.keySize));
			}
			if (this.claimTypeRequirements.Count > 0)
			{
				Collection<XmlElement> collection2 = new Collection<XmlElement>();
				XmlDocument xmlDocument = new XmlDocument();
				foreach (ClaimTypeRequirement claimTypeRequirement in this.claimTypeRequirements)
				{
					XmlElement xmlElement = xmlDocument.CreateElement("wsid", "ClaimType", "http://schemas.xmlsoap.org/ws/2005/05/identity");
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("Uri");
					xmlAttribute.Value = claimTypeRequirement.ClaimType;
					xmlElement.Attributes.Append(xmlAttribute);
					if (claimTypeRequirement.IsOptional)
					{
						xmlAttribute = xmlDocument.CreateAttribute("Optional");
						xmlAttribute.Value = XmlConvert.ToString(claimTypeRequirement.IsOptional);
						xmlElement.Attributes.Append(xmlAttribute);
					}
					collection2.Add(xmlElement);
				}
				collection.Add(driver.CreateRequiredClaimsElement(collection2));
			}
			if (this.additionalRequestParameters.Count > 0)
			{
				Collection<XmlElement> collection3 = this.NormalizeAdditionalParameters(this.additionalRequestParameters, driver, this.claimTypeRequirements.Count > 0);
				foreach (XmlElement item in collection3)
				{
					collection.Add(item);
				}
			}
			return collection;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0007E018 File Offset: 0x0007C218
		private Collection<XmlElement> NormalizeAdditionalParameters(Collection<XmlElement> additionalParameters, TrustDriver driver, bool clientSideClaimTypeRequirementsSpecified)
		{
			Collection<XmlElement> collection = new Collection<XmlElement>();
			foreach (XmlElement item in additionalParameters)
			{
				collection.Add(item);
			}
			if (driver.StandardsManager.TrustVersion == TrustVersion.WSTrust13)
			{
				XmlElement xmlElement = null;
				XmlElement xmlElement2 = null;
				XmlElement xmlElement3 = null;
				XmlElement xmlElement4 = null;
				for (int i = 0; i < collection.Count; i++)
				{
					string text;
					if (driver.IsEncryptionAlgorithmElement(collection[i], out text))
					{
						xmlElement = collection[i];
					}
					else if (driver.IsCanonicalizationAlgorithmElement(collection[i], out text))
					{
						xmlElement2 = collection[i];
					}
					else if (driver.IsKeyWrapAlgorithmElement(collection[i], out text))
					{
						xmlElement3 = collection[i];
					}
					else if (((WSTrustDec2005.DriverDec2005)driver).IsSecondaryParametersElement(collection[i]))
					{
						xmlElement4 = collection[i];
					}
				}
				if (xmlElement4 != null)
				{
					foreach (object obj in xmlElement4.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						XmlElement xmlElement5 = xmlNode as XmlElement;
						if (xmlElement5 != null)
						{
							string text2 = null;
							if (driver.IsEncryptionAlgorithmElement(xmlElement5, out text2) && xmlElement != null)
							{
								collection.Remove(xmlElement);
							}
							else if (driver.IsCanonicalizationAlgorithmElement(xmlElement5, out text2) && xmlElement2 != null)
							{
								collection.Remove(xmlElement2);
							}
							else if (driver.IsKeyWrapAlgorithmElement(xmlElement5, out text2) && xmlElement3 != null)
							{
								collection.Remove(xmlElement3);
							}
						}
					}
				}
			}
			if ((driver.StandardsManager.TrustVersion != TrustVersion.WSTrustFeb2005 || this.CollectionContainsElementsWithTrustNamespace(additionalParameters, "http://schemas.xmlsoap.org/ws/2005/02/trust")) && (driver.StandardsManager.TrustVersion != TrustVersion.WSTrust13 || this.CollectionContainsElementsWithTrustNamespace(additionalParameters, "http://docs.oasis-open.org/ws-sx/ws-trust/200512")))
			{
				return collection;
			}
			if (driver.StandardsManager.TrustVersion == TrustVersion.WSTrust13)
			{
				SecurityStandardsManager defaultInstance = SecurityStandardsManager.DefaultInstance;
				WSTrustFeb2005.DriverFeb2005 driverFeb = (WSTrustFeb2005.DriverFeb2005)defaultInstance.TrustDriver;
				for (int j = 0; j < collection.Count; j++)
				{
					string empty = string.Empty;
					if (driverFeb.IsSignWithElement(collection[j], out empty))
					{
						collection[j] = driver.CreateSignWithElement(empty);
					}
					else if (driverFeb.IsEncryptWithElement(collection[j], out empty))
					{
						collection[j] = driver.CreateEncryptWithElement(empty);
					}
					else if (driverFeb.IsEncryptionAlgorithmElement(collection[j], out empty))
					{
						collection[j] = driver.CreateEncryptionAlgorithmElement(empty);
					}
					else if (driverFeb.IsCanonicalizationAlgorithmElement(collection[j], out empty))
					{
						collection[j] = driver.CreateCanonicalizationAlgorithmElement(empty);
					}
				}
			}
			else
			{
				Collection<XmlElement> collection2 = null;
				WSSecurityTokenSerializer tokenSerializer = new WSSecurityTokenSerializer(SecurityVersion.WSSecurity11, TrustVersion.WSTrust13, SecureConversationVersion.WSSecureConversation13, true, null, null, null);
				SecurityStandardsManager securityStandardsManager = new SecurityStandardsManager(MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12, tokenSerializer);
				WSTrustDec2005.DriverDec2005 driverDec = (WSTrustDec2005.DriverDec2005)securityStandardsManager.TrustDriver;
				foreach (XmlElement xmlElement6 in collection)
				{
					if (driverDec.IsSecondaryParametersElement(xmlElement6))
					{
						collection2 = new Collection<XmlElement>();
						foreach (object obj2 in xmlElement6.ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj2;
							XmlElement xmlElement7 = xmlNode2 as XmlElement;
							if (xmlElement7 != null && this.CanPromoteToRoot(xmlElement7, driverDec, clientSideClaimTypeRequirementsSpecified))
							{
								collection2.Add(xmlElement7);
							}
						}
						collection.Remove(xmlElement6);
						break;
					}
				}
				if (collection2 != null && collection2.Count > 0)
				{
					XmlElement xmlElement8 = null;
					string empty2 = string.Empty;
					XmlElement xmlElement9 = null;
					string empty3 = string.Empty;
					XmlElement xmlElement10 = null;
					Collection<XmlElement> claimsList = null;
					Collection<XmlElement> collection3 = new Collection<XmlElement>();
					foreach (XmlElement xmlElement11 in collection2)
					{
						if (xmlElement8 == null && driverDec.IsEncryptionAlgorithmElement(xmlElement11, out empty2))
						{
							xmlElement8 = driver.CreateEncryptionAlgorithmElement(empty2);
							collection3.Add(xmlElement11);
						}
						else if (xmlElement9 == null && driverDec.IsCanonicalizationAlgorithmElement(xmlElement11, out empty3))
						{
							xmlElement9 = driver.CreateCanonicalizationAlgorithmElement(empty3);
							collection3.Add(xmlElement11);
						}
						else if (xmlElement10 == null && driverDec.TryParseRequiredClaimsElement(xmlElement11, out claimsList))
						{
							xmlElement10 = driver.CreateRequiredClaimsElement(claimsList);
							collection3.Add(xmlElement11);
						}
					}
					for (int k = 0; k < collection3.Count; k++)
					{
						collection2.Remove(collection3[k]);
					}
					XmlElement xmlElement12 = null;
					for (int l = 0; l < collection.Count; l++)
					{
						string text3;
						Collection<XmlElement> collection4;
						if (driverDec.IsSignWithElement(collection[l], out text3))
						{
							collection[l] = driver.CreateSignWithElement(text3);
						}
						else if (driverDec.IsEncryptWithElement(collection[l], out text3))
						{
							collection[l] = driver.CreateEncryptWithElement(text3);
						}
						else if (driverDec.IsEncryptionAlgorithmElement(collection[l], out text3) && xmlElement8 != null)
						{
							collection[l] = xmlElement8;
							xmlElement8 = null;
						}
						else if (driverDec.IsCanonicalizationAlgorithmElement(collection[l], out text3) && xmlElement9 != null)
						{
							collection[l] = xmlElement9;
							xmlElement9 = null;
						}
						else if (driverDec.IsKeyWrapAlgorithmElement(collection[l], out text3) && xmlElement12 == null)
						{
							xmlElement12 = collection[l];
						}
						else if (driverDec.TryParseRequiredClaimsElement(collection[l], out collection4) && xmlElement10 != null)
						{
							collection[l] = xmlElement10;
							xmlElement10 = null;
						}
					}
					if (xmlElement12 != null)
					{
						collection.Remove(xmlElement12);
					}
					if (xmlElement8 != null)
					{
						collection.Add(xmlElement8);
					}
					if (xmlElement9 != null)
					{
						collection.Add(xmlElement9);
					}
					if (xmlElement10 != null)
					{
						collection.Add(xmlElement10);
					}
					if (collection2.Count > 0)
					{
						for (int m = 0; m < collection2.Count; m++)
						{
							collection.Add(collection2[m]);
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0007E660 File Offset: 0x0007C860
		private bool CollectionContainsElementsWithTrustNamespace(Collection<XmlElement> collection, string trustNamespace)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				if (collection[i] != null && collection[i].NamespaceURI == trustNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0007E6A0 File Offset: 0x0007C8A0
		private bool CanPromoteToRoot(XmlElement innerElement, WSTrustDec2005.DriverDec2005 trust13Driver, bool clientSideClaimTypeRequirementsSpecified)
		{
			Collection<XmlElement> collection = null;
			if (trust13Driver.TryParseRequiredClaimsElement(innerElement, out collection))
			{
				return !clientSideClaimTypeRequirementsSpecified;
			}
			SecurityKeyType securityKeyType;
			int num;
			string text;
			return !trust13Driver.TryParseKeyTypeElement(innerElement, out securityKeyType) && !trust13Driver.TryParseKeySizeElement(innerElement, out num) && !trust13Driver.TryParseTokenTypeElement(innerElement, out text) && !trust13Driver.IsSignWithElement(innerElement, out text) && !trust13Driver.IsEncryptWithElement(innerElement, out text) && !trust13Driver.IsKeyWrapAlgorithmElement(innerElement, out text);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0007E704 File Offset: 0x0007C904
		internal void AddAlgorithmParameters(SecurityAlgorithmSuite algorithmSuite, SecurityStandardsManager standardsManager, SecurityKeyType issuedKeyType)
		{
			this.additionalRequestParameters.Insert(0, standardsManager.TrustDriver.CreateEncryptionAlgorithmElement(algorithmSuite.DefaultEncryptionAlgorithm));
			this.additionalRequestParameters.Insert(0, standardsManager.TrustDriver.CreateCanonicalizationAlgorithmElement(algorithmSuite.DefaultCanonicalizationAlgorithm));
			if (this.keyType == SecurityKeyType.BearerKey)
			{
				return;
			}
			string signatureAlgorithm = (this.keyType == SecurityKeyType.SymmetricKey) ? algorithmSuite.DefaultSymmetricSignatureAlgorithm : algorithmSuite.DefaultAsymmetricSignatureAlgorithm;
			this.additionalRequestParameters.Insert(0, standardsManager.TrustDriver.CreateSignWithElement(signatureAlgorithm));
			string encryptionAlgorithm;
			if (issuedKeyType == SecurityKeyType.SymmetricKey)
			{
				encryptionAlgorithm = algorithmSuite.DefaultEncryptionAlgorithm;
			}
			else
			{
				encryptionAlgorithm = algorithmSuite.DefaultAsymmetricKeyWrapAlgorithm;
			}
			this.additionalRequestParameters.Insert(0, standardsManager.TrustDriver.CreateEncryptWithElement(encryptionAlgorithm));
			if (standardsManager.TrustVersion != TrustVersion.WSTrustFeb2005)
			{
				this.additionalRequestParameters.Insert(0, ((WSTrustDec2005.DriverDec2005)standardsManager.TrustDriver).CreateKeyWrapAlgorithmElement(algorithmSuite.DefaultAsymmetricKeyWrapAlgorithm));
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0007E7E0 File Offset: 0x0007C9E0
		internal bool DoAlgorithmsMatch(SecurityAlgorithmSuite algorithmSuite, SecurityStandardsManager standardsManager, out Collection<XmlElement> otherRequestParameters)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			otherRequestParameters = new Collection<XmlElement>();
			bool flag6 = false;
			Collection<XmlElement> collection;
			if (standardsManager.TrustVersion == TrustVersion.WSTrust13 && this.AdditionalRequestParameters.Count == 1 && ((WSTrustDec2005.DriverDec2005)standardsManager.TrustDriver).IsSecondaryParametersElement(this.AdditionalRequestParameters[0]))
			{
				flag6 = true;
				collection = new Collection<XmlElement>();
				using (IEnumerator enumerator = this.AdditionalRequestParameters[0].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlElement item = (XmlElement)obj;
						collection.Add(item);
					}
					goto IL_AF;
				}
			}
			collection = this.AdditionalRequestParameters;
			IL_AF:
			for (int i = 0; i < collection.Count; i++)
			{
				XmlElement xmlElement = collection[i];
				string text;
				if (standardsManager.TrustDriver.IsCanonicalizationAlgorithmElement(xmlElement, out text))
				{
					if (algorithmSuite.DefaultCanonicalizationAlgorithm != text)
					{
						return false;
					}
					flag4 = true;
				}
				else if (standardsManager.TrustDriver.IsSignWithElement(xmlElement, out text))
				{
					if ((this.keyType == SecurityKeyType.SymmetricKey && text != algorithmSuite.DefaultSymmetricSignatureAlgorithm) || (this.keyType == SecurityKeyType.AsymmetricKey && text != algorithmSuite.DefaultAsymmetricSignatureAlgorithm))
					{
						return false;
					}
					flag = true;
				}
				else if (standardsManager.TrustDriver.IsEncryptWithElement(xmlElement, out text))
				{
					if ((this.keyType == SecurityKeyType.SymmetricKey && text != algorithmSuite.DefaultEncryptionAlgorithm) || (this.keyType == SecurityKeyType.AsymmetricKey && text != algorithmSuite.DefaultAsymmetricKeyWrapAlgorithm))
					{
						return false;
					}
					flag2 = true;
				}
				else if (standardsManager.TrustDriver.IsEncryptionAlgorithmElement(xmlElement, out text))
				{
					if (text != algorithmSuite.DefaultEncryptionAlgorithm)
					{
						return false;
					}
					flag3 = true;
				}
				else if (standardsManager.TrustDriver.IsKeyWrapAlgorithmElement(xmlElement, out text))
				{
					if (text != algorithmSuite.DefaultAsymmetricKeyWrapAlgorithm)
					{
						return false;
					}
					flag5 = true;
				}
				else
				{
					otherRequestParameters.Add(xmlElement);
				}
			}
			if (flag6)
			{
				otherRequestParameters = this.AdditionalRequestParameters;
			}
			if (this.keyType == SecurityKeyType.BearerKey)
			{
				return true;
			}
			if (standardsManager.TrustVersion == TrustVersion.WSTrustFeb2005)
			{
				return flag && flag4 && flag3 && flag2;
			}
			return flag && flag4 && flag3 && flag2 && flag5;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0007EA18 File Offset: 0x0007CC18
		internal static IssuedSecurityTokenParameters CreateInfoCardParameters(SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithm)
		{
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = new IssuedSecurityTokenParameters("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1");
			issuedSecurityTokenParameters.KeyType = SecurityKeyType.AsymmetricKey;
			issuedSecurityTokenParameters.ClaimTypeRequirements.Add(new ClaimTypeRequirement(IssuedSecurityTokenParameters.wsidPPIClaim));
			issuedSecurityTokenParameters.IssuerAddress = null;
			issuedSecurityTokenParameters.AddAlgorithmParameters(algorithm, standardsManager, issuedSecurityTokenParameters.KeyType);
			return issuedSecurityTokenParameters;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0007EA64 File Offset: 0x0007CC64
		internal static bool IsInfoCardParameters(IssuedSecurityTokenParameters parameters, SecurityStandardsManager standardsManager)
		{
			if (parameters == null)
			{
				return false;
			}
			if (parameters.TokenType != "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1")
			{
				return false;
			}
			if (parameters.KeyType != SecurityKeyType.AsymmetricKey)
			{
				return false;
			}
			if (parameters.ClaimTypeRequirements.Count == 1)
			{
				ClaimTypeRequirement claimTypeRequirement = parameters.ClaimTypeRequirements[0];
				if (claimTypeRequirement == null)
				{
					return false;
				}
				if (claimTypeRequirement.ClaimType != IssuedSecurityTokenParameters.wsidPPIClaim)
				{
					return false;
				}
			}
			else
			{
				if (parameters.AdditionalRequestParameters == null || parameters.AdditionalRequestParameters.Count <= 0)
				{
					return false;
				}
				bool flag = false;
				XmlElement claimTypeRequirement2 = IssuedSecurityTokenParameters.GetClaimTypeRequirement(parameters.AdditionalRequestParameters, standardsManager);
				if (claimTypeRequirement2 != null && claimTypeRequirement2.ChildNodes.Count == 1)
				{
					XmlElement xmlElement = claimTypeRequirement2.ChildNodes[0] as XmlElement;
					if (xmlElement != null)
					{
						XmlNode namedItem = xmlElement.Attributes.GetNamedItem("Uri");
						if (namedItem != null && namedItem.Value == IssuedSecurityTokenParameters.wsidPPIClaim)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return !(parameters.IssuerAddress != null) && (parameters.AlternativeIssuerEndpoints == null || parameters.AlternativeIssuerEndpoints.Count <= 0);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0007EB70 File Offset: 0x0007CD70
		internal static XmlElement GetClaimTypeRequirement(Collection<XmlElement> additionalRequestParameters, SecurityStandardsManager standardsManager)
		{
			foreach (XmlElement xmlElement in additionalRequestParameters)
			{
				if (xmlElement.LocalName == ((WSTrust.Driver)standardsManager.TrustDriver).DriverDictionary.Claims.Value && xmlElement.NamespaceURI == ((WSTrust.Driver)standardsManager.TrustDriver).DriverDictionary.Namespace.Value)
				{
					return xmlElement;
				}
				if (xmlElement.LocalName == DXD.TrustDec2005Dictionary.SecondaryParameters.Value && xmlElement.NamespaceURI == DXD.TrustDec2005Dictionary.Namespace.Value)
				{
					Collection<XmlElement> collection = new Collection<XmlElement>();
					foreach (object obj in xmlElement.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						XmlElement xmlElement2 = xmlNode as XmlElement;
						if (xmlElement2 != null)
						{
							collection.Add(xmlElement2);
						}
					}
					XmlElement claimTypeRequirement = IssuedSecurityTokenParameters.GetClaimTypeRequirement(collection, standardsManager);
					if (claimTypeRequirement != null)
					{
						return claimTypeRequirement;
					}
				}
			}
			return null;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0007ECD8 File Offset: 0x0007CED8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "TokenType: {0}", new object[]
			{
				(this.tokenType == null) ? "null" : this.tokenType
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "KeyType: {0}", new object[]
			{
				this.keyType.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "KeySize: {0}", new object[]
			{
				this.keySize.ToString(CultureInfo.InvariantCulture)
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "IssuerAddress: {0}", new object[]
			{
				(this.issuerAddress == null) ? "null" : this.issuerAddress.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "IssuerMetadataAddress: {0}", new object[]
			{
				(this.issuerMetadataAddress == null) ? "null" : this.issuerMetadataAddress.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "DefaultMessgeSecurityVersion: {0}", new object[]
			{
				(this.defaultMessageSecurityVersion == null) ? "null" : this.defaultMessageSecurityVersion.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "UseStrTransform: {0}", new object[]
			{
				this.useStrTransform.ToString()
			}));
			if (this.issuerBinding == null)
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "IssuerBinding: null", new object[0]));
			}
			else
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "IssuerBinding:", new object[0]));
				BindingElementCollection bindingElementCollection = this.issuerBinding.CreateBindingElements();
				for (int i = 0; i < bindingElementCollection.Count; i++)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "  BindingElement[{0}]:", new object[]
					{
						i.ToString(CultureInfo.InvariantCulture)
					}));
					stringBuilder.AppendLine("    " + bindingElementCollection[i].ToString().Trim().Replace("\n", "\n    "));
				}
			}
			if (this.claimTypeRequirements.Count == 0)
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "ClaimTypeRequirements: none", new object[0]));
			}
			else
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "ClaimTypeRequirements:", new object[0]));
				for (int j = 0; j < this.claimTypeRequirements.Count; j++)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "  {0}, optional={1}", new object[]
					{
						this.claimTypeRequirements[j].ClaimType,
						this.claimTypeRequirements[j].IsOptional
					}));
				}
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0007EFDC File Offset: 0x0007D1DC
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = this.TokenType;
			requirement.RequireCryptographicToken = true;
			requirement.KeyType = this.KeyType;
			ServiceModelSecurityTokenRequirement serviceModelSecurityTokenRequirement = requirement as ServiceModelSecurityTokenRequirement;
			if (serviceModelSecurityTokenRequirement != null)
			{
				serviceModelSecurityTokenRequirement.DefaultMessageSecurityVersion = this.DefaultMessageSecurityVersion;
			}
			else
			{
				requirement.Properties[ServiceModelSecurityTokenRequirement.DefaultMessageSecurityVersionProperty] = this.DefaultMessageSecurityVersion;
			}
			if (this.KeySize > 0)
			{
				requirement.KeySize = this.KeySize;
			}
			requirement.Properties[ServiceModelSecurityTokenRequirement.IssuerAddressProperty] = this.IssuerAddress;
			if (this.IssuerBinding != null)
			{
				requirement.Properties[ServiceModelSecurityTokenRequirement.IssuerBindingProperty] = this.IssuerBinding;
			}
			requirement.Properties[ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty] = base.Clone();
		}

		// Token: 0x04001FA3 RID: 8099
		private const string wsidPrefix = "wsid";

		// Token: 0x04001FA4 RID: 8100
		private const string wsidNamespace = "http://schemas.xmlsoap.org/ws/2005/05/identity";

		// Token: 0x04001FA5 RID: 8101
		private static readonly string wsidPPIClaim = string.Format(CultureInfo.InvariantCulture, "{0}/claims/privatepersonalidentifier", new object[]
		{
			"http://schemas.xmlsoap.org/ws/2005/05/identity"
		});

		// Token: 0x04001FA6 RID: 8102
		internal const SecurityKeyType defaultKeyType = SecurityKeyType.SymmetricKey;

		// Token: 0x04001FA7 RID: 8103
		internal const bool defaultUseStrTransform = false;

		// Token: 0x04001FA8 RID: 8104
		private Collection<XmlElement> additionalRequestParameters = new Collection<XmlElement>();

		// Token: 0x04001FA9 RID: 8105
		private Collection<IssuedSecurityTokenParameters.AlternativeIssuerEndpoint> alternativeIssuerEndpoints = new Collection<IssuedSecurityTokenParameters.AlternativeIssuerEndpoint>();

		// Token: 0x04001FAA RID: 8106
		private MessageSecurityVersion defaultMessageSecurityVersion;

		// Token: 0x04001FAB RID: 8107
		private EndpointAddress issuerAddress;

		// Token: 0x04001FAC RID: 8108
		private EndpointAddress issuerMetadataAddress;

		// Token: 0x04001FAD RID: 8109
		private Binding issuerBinding;

		// Token: 0x04001FAE RID: 8110
		private int keySize;

		// Token: 0x04001FAF RID: 8111
		private SecurityKeyType keyType;

		// Token: 0x04001FB0 RID: 8112
		private Collection<ClaimTypeRequirement> claimTypeRequirements = new Collection<ClaimTypeRequirement>();

		// Token: 0x04001FB1 RID: 8113
		private bool useStrTransform;

		// Token: 0x04001FB2 RID: 8114
		private string tokenType;

		// Token: 0x02000B9D RID: 2973
		internal struct AlternativeIssuerEndpoint
		{
			// Token: 0x04004173 RID: 16755
			public EndpointAddress IssuerAddress;

			// Token: 0x04004174 RID: 16756
			public EndpointAddress IssuerMetadataAddress;

			// Token: 0x04004175 RID: 16757
			public Binding IssuerBinding;
		}
	}
}

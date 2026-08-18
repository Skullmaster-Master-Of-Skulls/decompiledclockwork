using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000287 RID: 647
	internal abstract class TrustDriver
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0004417F File Offset: 0x0004237F
		public virtual bool IsIssuedTokensSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00044182 File Offset: 0x00042382
		public virtual string IssuedTokensHeaderName
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TrustDriverVersionDoesNotSupportIssuedTokens")));
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0004419D File Offset: 0x0004239D
		public virtual string IssuedTokensHeaderNamespace
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TrustDriverVersionDoesNotSupportIssuedTokens")));
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x000441B8 File Offset: 0x000423B8
		public virtual bool IsSessionSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060012AD RID: 4781
		public abstract XmlDictionaryString RequestSecurityTokenAction { get; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060012AE RID: 4782
		public abstract XmlDictionaryString RequestSecurityTokenResponseAction { get; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060012AF RID: 4783
		public abstract XmlDictionaryString RequestSecurityTokenResponseFinalAction { get; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x000441BB File Offset: 0x000423BB
		public virtual string RequestTypeClose
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TrustDriverVersionDoesNotSupportSession")));
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060012B1 RID: 4785
		public abstract string RequestTypeIssue { get; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x000441D6 File Offset: 0x000423D6
		public virtual string RequestTypeRenew
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TrustDriverVersionDoesNotSupportSession")));
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060012B3 RID: 4787
		public abstract string ComputedKeyAlgorithm { get; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060012B4 RID: 4788
		public abstract SecurityStandardsManager StandardsManager { get; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060012B5 RID: 4789
		public abstract XmlDictionaryString Namespace { get; }

		// Token: 0x060012B6 RID: 4790
		public abstract RequestSecurityToken CreateRequestSecurityToken(XmlReader reader);

		// Token: 0x060012B7 RID: 4791
		public abstract RequestSecurityTokenResponse CreateRequestSecurityTokenResponse(XmlReader reader);

		// Token: 0x060012B8 RID: 4792
		public abstract RequestSecurityTokenResponseCollection CreateRequestSecurityTokenResponseCollection(XmlReader xmlReader);

		// Token: 0x060012B9 RID: 4793
		public abstract bool IsAtRequestSecurityTokenResponse(XmlReader reader);

		// Token: 0x060012BA RID: 4794
		public abstract bool IsAtRequestSecurityTokenResponseCollection(XmlReader reader);

		// Token: 0x060012BB RID: 4795
		public abstract bool IsRequestedSecurityTokenElement(string name, string nameSpace);

		// Token: 0x060012BC RID: 4796
		public abstract bool IsRequestedProofTokenElement(string name, string nameSpace);

		// Token: 0x060012BD RID: 4797
		public abstract T GetAppliesTo<T>(RequestSecurityToken rst, XmlObjectSerializer serializer);

		// Token: 0x060012BE RID: 4798
		public abstract T GetAppliesTo<T>(RequestSecurityTokenResponse rstr, XmlObjectSerializer serializer);

		// Token: 0x060012BF RID: 4799
		public abstract void GetAppliesToQName(RequestSecurityToken rst, out string localName, out string namespaceUri);

		// Token: 0x060012C0 RID: 4800
		public abstract void GetAppliesToQName(RequestSecurityTokenResponse rstr, out string localName, out string namespaceUri);

		// Token: 0x060012C1 RID: 4801
		public abstract bool IsAppliesTo(string localName, string namespaceUri);

		// Token: 0x060012C2 RID: 4802
		public abstract byte[] GetAuthenticator(RequestSecurityTokenResponse rstr);

		// Token: 0x060012C3 RID: 4803
		public abstract BinaryNegotiation GetBinaryNegotiation(RequestSecurityToken rst);

		// Token: 0x060012C4 RID: 4804
		public abstract BinaryNegotiation GetBinaryNegotiation(RequestSecurityTokenResponse rstr);

		// Token: 0x060012C5 RID: 4805
		public abstract SecurityToken GetEntropy(RequestSecurityToken rst, SecurityTokenResolver resolver);

		// Token: 0x060012C6 RID: 4806
		public abstract SecurityToken GetEntropy(RequestSecurityTokenResponse rstr, SecurityTokenResolver resolver);

		// Token: 0x060012C7 RID: 4807
		public abstract GenericXmlSecurityToken GetIssuedToken(RequestSecurityTokenResponse rstr, SecurityTokenResolver resolver, IList<SecurityTokenAuthenticator> allowedAuthenticators, SecurityKeyEntropyMode keyEntropyMode, byte[] requestorEntropy, string expectedTokenType, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, int defaultKeySize, bool isBearerKeyType);

		// Token: 0x060012C8 RID: 4808
		public abstract GenericXmlSecurityToken GetIssuedToken(RequestSecurityTokenResponse rstr, string expectedTokenType, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, RSA clientKey);

		// Token: 0x060012C9 RID: 4809
		public abstract void OnRSTRorRSTRCMissingException();

		// Token: 0x060012CA RID: 4810
		public abstract void WriteRequestSecurityToken(RequestSecurityToken rst, XmlWriter w);

		// Token: 0x060012CB RID: 4811
		public abstract void WriteRequestSecurityTokenResponse(RequestSecurityTokenResponse rstr, XmlWriter w);

		// Token: 0x060012CC RID: 4812
		public abstract void WriteRequestSecurityTokenResponseCollection(RequestSecurityTokenResponseCollection rstrCollection, XmlWriter writer);

		// Token: 0x060012CD RID: 4813
		public abstract IChannelFactory<IRequestChannel> CreateFederationProxy(EndpointAddress address, Binding binding, KeyedByTypeCollection<IEndpointBehavior> channelBehaviors);

		// Token: 0x060012CE RID: 4814
		public abstract XmlElement CreateKeySizeElement(int keySize);

		// Token: 0x060012CF RID: 4815
		public abstract XmlElement CreateKeyTypeElement(SecurityKeyType keyType);

		// Token: 0x060012D0 RID: 4816
		public abstract XmlElement CreateTokenTypeElement(string tokenTypeUri);

		// Token: 0x060012D1 RID: 4817
		public abstract XmlElement CreateRequiredClaimsElement(IEnumerable<XmlElement> claimsList);

		// Token: 0x060012D2 RID: 4818
		public abstract XmlElement CreateUseKeyElement(SecurityKeyIdentifier keyIdentifier, SecurityStandardsManager standardsManager);

		// Token: 0x060012D3 RID: 4819
		public abstract XmlElement CreateSignWithElement(string signatureAlgorithm);

		// Token: 0x060012D4 RID: 4820
		public abstract XmlElement CreateEncryptWithElement(string encryptionAlgorithm);

		// Token: 0x060012D5 RID: 4821
		public abstract XmlElement CreateEncryptionAlgorithmElement(string encryptionAlgorithm);

		// Token: 0x060012D6 RID: 4822
		public abstract XmlElement CreateCanonicalizationAlgorithmElement(string canonicalicationAlgorithm);

		// Token: 0x060012D7 RID: 4823
		public abstract XmlElement CreateComputedKeyAlgorithmElement(string computedKeyAlgorithm);

		// Token: 0x060012D8 RID: 4824
		public abstract Collection<XmlElement> ProcessUnknownRequestParameters(Collection<XmlElement> unknownRequestParameters, Collection<XmlElement> originalRequestParameters);

		// Token: 0x060012D9 RID: 4825
		public abstract bool TryParseKeySizeElement(XmlElement element, out int keySize);

		// Token: 0x060012DA RID: 4826
		public abstract bool TryParseKeyTypeElement(XmlElement element, out SecurityKeyType keyType);

		// Token: 0x060012DB RID: 4827
		public abstract bool TryParseTokenTypeElement(XmlElement element, out string tokenType);

		// Token: 0x060012DC RID: 4828
		public abstract bool TryParseRequiredClaimsElement(XmlElement element, out Collection<XmlElement> requiredClaims);

		// Token: 0x060012DD RID: 4829 RVA: 0x000441F1 File Offset: 0x000423F1
		internal virtual bool IsSignWithElement(XmlElement element, out string signatureAlgorithm)
		{
			signatureAlgorithm = null;
			return false;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000441F7 File Offset: 0x000423F7
		internal virtual bool IsEncryptWithElement(XmlElement element, out string encryptWithAlgorithm)
		{
			encryptWithAlgorithm = null;
			return false;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000441FD File Offset: 0x000423FD
		internal virtual bool IsEncryptionAlgorithmElement(XmlElement element, out string encryptionAlgorithm)
		{
			encryptionAlgorithm = null;
			return false;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00044203 File Offset: 0x00042403
		internal virtual bool IsCanonicalizationAlgorithmElement(XmlElement element, out string canonicalizationAlgorithm)
		{
			canonicalizationAlgorithm = null;
			return false;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00044209 File Offset: 0x00042409
		internal virtual bool IsKeyWrapAlgorithmElement(XmlElement element, out string keyWrapAlgorithm)
		{
			keyWrapAlgorithm = null;
			return false;
		}
	}
}

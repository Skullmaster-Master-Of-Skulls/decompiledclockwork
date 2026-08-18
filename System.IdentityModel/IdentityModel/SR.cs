using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.IdentityModel
{
	// Token: 0x020000EA RID: 234
	internal sealed class SR
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001A19E File Offset: 0x0001839E
		internal SR()
		{
			this.resources = new ResourceManager("System.IdentityModel", base.GetType().Assembly);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001A1C4 File Offset: 0x000183C4
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR value = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, value, null);
			}
			return SR.loader;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00003459 File Offset: 0x00001659
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001A1F0 File Offset: 0x000183F0
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001A1FC File Offset: 0x000183FC
		public static string GetString(string name, params object[] args)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			string @string = sr.resources.GetString(name, SR.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001A27C File Offset: 0x0001847C
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001A2A5 File Offset: 0x000184A5
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001A2B0 File Offset: 0x000184B0
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x0400079E RID: 1950
		internal const string SspiLoginPromptHeaderMessage = "SspiLoginPromptHeaderMessage";

		// Token: 0x0400079F RID: 1951
		internal const string ValueMustBeNonNegative = "ValueMustBeNonNegative";

		// Token: 0x040007A0 RID: 1952
		internal const string ValueMustBeInRange = "ValueMustBeInRange";

		// Token: 0x040007A1 RID: 1953
		internal const string ValueMustBeOne = "ValueMustBeOne";

		// Token: 0x040007A2 RID: 1954
		internal const string ValueMustBeZero = "ValueMustBeZero";

		// Token: 0x040007A3 RID: 1955
		internal const string ValueMustBeGreaterThanZero = "ValueMustBeGreaterThanZero";

		// Token: 0x040007A4 RID: 1956
		internal const string ValueMustBeOf2Types = "ValueMustBeOf2Types";

		// Token: 0x040007A5 RID: 1957
		internal const string LengthOfArrayToConvertMustGreaterThanZero = "LengthOfArrayToConvertMustGreaterThanZero";

		// Token: 0x040007A6 RID: 1958
		internal const string LengthMustBeGreaterThanZero = "LengthMustBeGreaterThanZero";

		// Token: 0x040007A7 RID: 1959
		internal const string ArgumentCannotBeEmptyString = "ArgumentCannotBeEmptyString";

		// Token: 0x040007A8 RID: 1960
		internal const string AuthorizationContextCreated = "AuthorizationContextCreated";

		// Token: 0x040007A9 RID: 1961
		internal const string AuthorizationPolicyEvaluated = "AuthorizationPolicyEvaluated";

		// Token: 0x040007AA RID: 1962
		internal const string RequiredAttributeMissing = "RequiredAttributeMissing";

		// Token: 0x040007AB RID: 1963
		internal const string UnsupportedCryptoAlgorithm = "UnsupportedCryptoAlgorithm";

		// Token: 0x040007AC RID: 1964
		internal const string CustomCryptoAlgorithmIsNotValidSymmetricAlgorithm = "CustomCryptoAlgorithmIsNotValidSymmetricAlgorithm";

		// Token: 0x040007AD RID: 1965
		internal const string CustomCryptoAlgorithmIsNotValidHashAlgorithm = "CustomCryptoAlgorithmIsNotValidHashAlgorithm";

		// Token: 0x040007AE RID: 1966
		internal const string CustomCryptoAlgorithmIsNotValidKeyedHashAlgorithm = "CustomCryptoAlgorithmIsNotValidKeyedHashAlgorithm";

		// Token: 0x040007AF RID: 1967
		internal const string CryptoAlgorithmIsNotFipsCompliant = "CryptoAlgorithmIsNotFipsCompliant";

		// Token: 0x040007B0 RID: 1968
		internal const string InvalidCustomKeyWrapAlgorithm = "InvalidCustomKeyWrapAlgorithm";

		// Token: 0x040007B1 RID: 1969
		internal const string AlgorithmMismatchForTransform = "AlgorithmMismatchForTransform";

		// Token: 0x040007B2 RID: 1970
		internal const string SecurityTokenRequirementDoesNotContainProperty = "SecurityTokenRequirementDoesNotContainProperty";

		// Token: 0x040007B3 RID: 1971
		internal const string SecurityTokenRequirementHasInvalidTypeForProperty = "SecurityTokenRequirementHasInvalidTypeForProperty";

		// Token: 0x040007B4 RID: 1972
		internal const string UnableToResolveKeyReference = "UnableToResolveKeyReference";

		// Token: 0x040007B5 RID: 1973
		internal const string UnableToResolveTokenReference = "UnableToResolveTokenReference";

		// Token: 0x040007B6 RID: 1974
		internal const string NoInputIsSetForCanonicalization = "NoInputIsSetForCanonicalization";

		// Token: 0x040007B7 RID: 1975
		internal const string RequiredTargetNotSigned = "RequiredTargetNotSigned";

		// Token: 0x040007B8 RID: 1976
		internal const string XmlBufferQuotaExceeded = "XmlBufferQuotaExceeded";

		// Token: 0x040007B9 RID: 1977
		internal const string XmlLangAttributeMissing = "XmlLangAttributeMissing";

		// Token: 0x040007BA RID: 1978
		internal const string InvalidXmlQualifiedName = "InvalidXmlQualifiedName";

		// Token: 0x040007BB RID: 1979
		internal const string UnboundPrefixInQName = "UnboundPrefixInQName";

		// Token: 0x040007BC RID: 1980
		internal const string ExpectedElementMissing = "ExpectedElementMissing";

		// Token: 0x040007BD RID: 1981
		internal const string UnexpectedXmlChildNode = "UnexpectedXmlChildNode";

		// Token: 0x040007BE RID: 1982
		internal const string TokenCancellationNotSupported = "TokenCancellationNotSupported";

		// Token: 0x040007BF RID: 1983
		internal const string TokenRenewalNotSupported = "TokenRenewalNotSupported";

		// Token: 0x040007C0 RID: 1984
		internal const string TokenProviderUnableToGetToken = "TokenProviderUnableToGetToken";

		// Token: 0x040007C1 RID: 1985
		internal const string TokenProviderUnableToRenewToken = "TokenProviderUnableToRenewToken";

		// Token: 0x040007C2 RID: 1986
		internal const string CannotValidateSecurityTokenType = "CannotValidateSecurityTokenType";

		// Token: 0x040007C3 RID: 1987
		internal const string UserNameCannotBeEmpty = "UserNameCannotBeEmpty";

		// Token: 0x040007C4 RID: 1988
		internal const string LocalIdCannotBeEmpty = "LocalIdCannotBeEmpty";

		// Token: 0x040007C5 RID: 1989
		internal const string ProvidedNetworkCredentialsForKerberosHasInvalidUserName = "ProvidedNetworkCredentialsForKerberosHasInvalidUserName";

		// Token: 0x040007C6 RID: 1990
		internal const string UnableToCreateKerberosCredentials = "UnableToCreateKerberosCredentials";

		// Token: 0x040007C7 RID: 1991
		internal const string UnsupportedTransformAlgorithm = "UnsupportedTransformAlgorithm";

		// Token: 0x040007C8 RID: 1992
		internal const string AtLeastOneReferenceRequired = "AtLeastOneReferenceRequired";

		// Token: 0x040007C9 RID: 1993
		internal const string DigestVerificationFailedForReference = "DigestVerificationFailedForReference";

		// Token: 0x040007CA RID: 1994
		internal const string SignatureVerificationFailed = "SignatureVerificationFailed";

		// Token: 0x040007CB RID: 1995
		internal const string AtLeastOneTransformRequired = "AtLeastOneTransformRequired";

		// Token: 0x040007CC RID: 1996
		internal const string AsyncCallbackException = "AsyncCallbackException";

		// Token: 0x040007CD RID: 1997
		internal const string UserNameAuthenticationFailed = "UserNameAuthenticationFailed";

		// Token: 0x040007CE RID: 1998
		internal const string ObjectIsReadOnly = "ObjectIsReadOnly";

		// Token: 0x040007CF RID: 1999
		internal const string UnsupportedKeyDerivationAlgorithm = "UnsupportedKeyDerivationAlgorithm";

		// Token: 0x040007D0 RID: 2000
		internal const string UnsupportedNodeTypeInReader = "UnsupportedNodeTypeInReader";

		// Token: 0x040007D1 RID: 2001
		internal const string UnknownICryptoType = "UnknownICryptoType";

		// Token: 0x040007D2 RID: 2002
		internal const string NoPrivateKeyAvailable = "NoPrivateKeyAvailable";

		// Token: 0x040007D3 RID: 2003
		internal const string UnsupportedAlgorithmForCryptoOperation = "UnsupportedAlgorithmForCryptoOperation";

		// Token: 0x040007D4 RID: 2004
		internal const string KeyIdentifierCannotCreateKey = "KeyIdentifierCannotCreateKey";

		// Token: 0x040007D5 RID: 2005
		internal const string KeyIdentifierClauseDoesNotSupportKeyCreation = "KeyIdentifierClauseDoesNotSupportKeyCreation";

		// Token: 0x040007D6 RID: 2006
		internal const string TokenDoesNotSupportKeyIdentifierClauseCreation = "TokenDoesNotSupportKeyIdentifierClauseCreation";

		// Token: 0x040007D7 RID: 2007
		internal const string XmlTokenBufferIsEmpty = "XmlTokenBufferIsEmpty";

		// Token: 0x040007D8 RID: 2008
		internal const string NoKeyIdentifierClauseFound = "NoKeyIdentifierClauseFound";

		// Token: 0x040007D9 RID: 2009
		internal const string UnableToCreateKeyedHashAlgorithm = "UnableToCreateKeyedHashAlgorithm";

		// Token: 0x040007DA RID: 2010
		internal const string UnableToCreateKeyedHashAlgorithmFromSymmetricCrypto = "UnableToCreateKeyedHashAlgorithmFromSymmetricCrypto";

		// Token: 0x040007DB RID: 2011
		internal const string UnableToCreateHashAlgorithmFromAsymmetricCrypto = "UnableToCreateHashAlgorithmFromAsymmetricCrypto";

		// Token: 0x040007DC RID: 2012
		internal const string UnableToCreateSignatureDeformatterFromAsymmetricCrypto = "UnableToCreateSignatureDeformatterFromAsymmetricCrypto";

		// Token: 0x040007DD RID: 2013
		internal const string UnableToCreateSignatureFormatterFromAsymmetricCrypto = "UnableToCreateSignatureFormatterFromAsymmetricCrypto";

		// Token: 0x040007DE RID: 2014
		internal const string InnerReaderMustBeAtElement = "InnerReaderMustBeAtElement";

		// Token: 0x040007DF RID: 2015
		internal const string CannotFindCert = "CannotFindCert";

		// Token: 0x040007E0 RID: 2016
		internal const string FoundMultipleCerts = "FoundMultipleCerts";

		// Token: 0x040007E1 RID: 2017
		internal const string X509FindValueMismatch = "X509FindValueMismatch";

		// Token: 0x040007E2 RID: 2018
		internal const string X509FindValueMismatchMulti = "X509FindValueMismatchMulti";

		// Token: 0x040007E3 RID: 2019
		internal const string X509CertStoreLocationNotValid = "X509CertStoreLocationNotValid";

		// Token: 0x040007E4 RID: 2020
		internal const string EmptyTransformChainNotSupported = "EmptyTransformChainNotSupported";

		// Token: 0x040007E5 RID: 2021
		internal const string UnsupportedInputTypeForTransform = "UnsupportedInputTypeForTransform";

		// Token: 0x040007E6 RID: 2022
		internal const string UnsupportedLastTransform = "UnsupportedLastTransform";

		// Token: 0x040007E7 RID: 2023
		internal const string UnableToResolveReferenceUriForSignature = "UnableToResolveReferenceUriForSignature";

		// Token: 0x040007E8 RID: 2024
		internal const string UnsupportedEncryptionAlgorithm = "UnsupportedEncryptionAlgorithm";

		// Token: 0x040007E9 RID: 2025
		internal const string UnsupportedKeyWrapAlgorithm = "UnsupportedKeyWrapAlgorithm";

		// Token: 0x040007EA RID: 2026
		internal const string InvalidAsyncResult = "InvalidAsyncResult";

		// Token: 0x040007EB RID: 2027
		internal const string UnableToCreateTokenReference = "UnableToCreateTokenReference";

		// Token: 0x040007EC RID: 2028
		internal const string BadEncryptionState = "BadEncryptionState";

		// Token: 0x040007ED RID: 2029
		internal const string XmlBufferInInvalidState = "XmlBufferInInvalidState";

		// Token: 0x040007EE RID: 2030
		internal const string ReferenceListCannotBeEmpty = "ReferenceListCannotBeEmpty";

		// Token: 0x040007EF RID: 2031
		internal const string InvalidDataReferenceInReferenceList = "InvalidDataReferenceInReferenceList";

		// Token: 0x040007F0 RID: 2032
		internal const string InvalidX509RawData = "InvalidX509RawData";

		// Token: 0x040007F1 RID: 2033
		internal const string CannotReadKeyIdentifierClause = "CannotReadKeyIdentifierClause";

		// Token: 0x040007F2 RID: 2034
		internal const string StandardsManagerCannotWriteObject = "StandardsManagerCannotWriteObject";

		// Token: 0x040007F3 RID: 2035
		internal const string UnknownEncodingInKeyIdentifier = "UnknownEncodingInKeyIdentifier";

		// Token: 0x040007F4 RID: 2036
		internal const string MultipleSamlAuthorityBindingsInReference = "MultipleSamlAuthorityBindingsInReference";

		// Token: 0x040007F5 RID: 2037
		internal const string MultipleKeyIdentifiersInReference = "MultipleKeyIdentifiersInReference";

		// Token: 0x040007F6 RID: 2038
		internal const string DidNotFindKeyIdentifierInReference = "DidNotFindKeyIdentifierInReference";

		// Token: 0x040007F7 RID: 2039
		internal const string SamlUriCannotBeNullOrEmpty = "SamlUriCannotBeNullOrEmpty";

		// Token: 0x040007F8 RID: 2040
		internal const string CannotFindMatchingCrypto = "CannotFindMatchingCrypto";

		// Token: 0x040007F9 RID: 2041
		internal const string AssertionIdCannotBeNullOrEmpty = "AssertionIdCannotBeNullOrEmpty";

		// Token: 0x040007FA RID: 2042
		internal const string BufferQuotaExceededReadingBase64 = "BufferQuotaExceededReadingBase64";

		// Token: 0x040007FB RID: 2043
		internal const string CannotReadToken = "CannotReadToken";

		// Token: 0x040007FC RID: 2044
		internal const string ErrorDeserializingKeyIdentifierClause = "ErrorDeserializingKeyIdentifierClause";

		// Token: 0x040007FD RID: 2045
		internal const string ErrorSerializingKeyIdentifier = "ErrorSerializingKeyIdentifier";

		// Token: 0x040007FE RID: 2046
		internal const string ErrorSerializingKeyIdentifierClause = "ErrorSerializingKeyIdentifierClause";

		// Token: 0x040007FF RID: 2047
		internal const string NoKeyInfoClausesToWrite = "NoKeyInfoClausesToWrite";

		// Token: 0x04000800 RID: 2048
		internal const string CollectionWasModified = "CollectionWasModified";

		// Token: 0x04000801 RID: 2049
		internal const string XDCannotFindValueInDictionaryString = "XDCannotFindValueInDictionaryString";

		// Token: 0x04000802 RID: 2050
		internal const string AlgorithmAndKeyMisMatch = "AlgorithmAndKeyMisMatch";

		// Token: 0x04000803 RID: 2051
		internal const string AlgorithmAndPrivateKeyMisMatch = "AlgorithmAndPrivateKeyMisMatch";

		// Token: 0x04000804 RID: 2052
		internal const string AlgorithmAndPublicKeyMisMatch = "AlgorithmAndPublicKeyMisMatch";

		// Token: 0x04000805 RID: 2053
		internal const string ArgumentInvalidCertificate = "ArgumentInvalidCertificate";

		// Token: 0x04000806 RID: 2054
		internal const string X509ChainBuildFail = "X509ChainBuildFail";

		// Token: 0x04000807 RID: 2055
		internal const string X509IsInUntrustedStore = "X509IsInUntrustedStore";

		// Token: 0x04000808 RID: 2056
		internal const string X509IsNotInTrustedStore = "X509IsNotInTrustedStore";

		// Token: 0x04000809 RID: 2057
		internal const string X509InvalidUsageTime = "X509InvalidUsageTime";

		// Token: 0x0400080A RID: 2058
		internal const string IncorrectUserNameFormat = "IncorrectUserNameFormat";

		// Token: 0x0400080B RID: 2059
		internal const string InvalidNtMapping = "InvalidNtMapping";

		// Token: 0x0400080C RID: 2060
		internal const string ImpersonationLevelNotSupported = "ImpersonationLevelNotSupported";

		// Token: 0x0400080D RID: 2061
		internal const string MissingPrivateKey = "MissingPrivateKey";

		// Token: 0x0400080E RID: 2062
		internal const string PrivateKeyExchangeNotSupported = "PrivateKeyExchangeNotSupported";

		// Token: 0x0400080F RID: 2063
		internal const string PrivateKeyNotDSA = "PrivateKeyNotDSA";

		// Token: 0x04000810 RID: 2064
		internal const string PrivateKeyNotRSA = "PrivateKeyNotRSA";

		// Token: 0x04000811 RID: 2065
		internal const string PublicKeyNotDSA = "PublicKeyNotDSA";

		// Token: 0x04000812 RID: 2066
		internal const string PublicKeyNotRSA = "PublicKeyNotRSA";

		// Token: 0x04000813 RID: 2067
		internal const string InclusiveNamespacePrefixRequiresSignatureReader = "InclusiveNamespacePrefixRequiresSignatureReader";

		// Token: 0x04000814 RID: 2068
		internal const string FailedToDeleteKeyContainerFile = "FailedToDeleteKeyContainerFile";

		// Token: 0x04000815 RID: 2069
		internal const string EmptyOrNullArgumentString = "EmptyOrNullArgumentString";

		// Token: 0x04000816 RID: 2070
		internal const string SecurityChannelBindingMissing = "SecurityChannelBindingMissing";

		// Token: 0x04000817 RID: 2071
		internal const string ServiceNameFromClient = "ServiceNameFromClient";

		// Token: 0x04000818 RID: 2072
		internal const string ServiceNameOnService = "ServiceNameOnService";

		// Token: 0x04000819 RID: 2073
		internal const string SamlSerializerRequiresExternalSerializers = "SamlSerializerRequiresExternalSerializers";

		// Token: 0x0400081A RID: 2074
		internal const string SamlSerializerUnableToWriteSecurityKeyIdentifier = "SamlSerializerUnableToWriteSecurityKeyIdentifier";

		// Token: 0x0400081B RID: 2075
		internal const string SamlSerializerUnableToReadSecurityKeyIdentifier = "SamlSerializerUnableToReadSecurityKeyIdentifier";

		// Token: 0x0400081C RID: 2076
		internal const string SamlAssertionMissingSigningCredentials = "SamlAssertionMissingSigningCredentials";

		// Token: 0x0400081D RID: 2077
		internal const string SamlTokenMissingSignature = "SamlTokenMissingSignature";

		// Token: 0x0400081E RID: 2078
		internal const string SamlInvalidSigningToken = "SamlInvalidSigningToken";

		// Token: 0x0400081F RID: 2079
		internal const string SamlSigningTokenNotFound = "SamlSigningTokenNotFound";

		// Token: 0x04000820 RID: 2080
		internal const string SamlSigningTokenMissing = "SamlSigningTokenMissing";

		// Token: 0x04000821 RID: 2081
		internal const string SamlTokenAuthenticatorCanOnlyProcessSamlTokens = "SamlTokenAuthenticatorCanOnlyProcessSamlTokens";

		// Token: 0x04000822 RID: 2082
		internal const string SamlUnableToExtractSubjectKey = "SamlUnableToExtractSubjectKey";

		// Token: 0x04000823 RID: 2083
		internal const string SamlAttributeClaimResourceShouldBeAString = "SamlAttributeClaimResourceShouldBeAString";

		// Token: 0x04000824 RID: 2084
		internal const string SamlAttributeClaimRightShouldBePossessProperty = "SamlAttributeClaimRightShouldBePossessProperty";

		// Token: 0x04000825 RID: 2085
		internal const string SAMLAssertionIDIsInvalid = "SAMLAssertionIDIsInvalid";

		// Token: 0x04000826 RID: 2086
		internal const string SAMLAssertionIdRequired = "SAMLAssertionIdRequired";

		// Token: 0x04000827 RID: 2087
		internal const string SAMLAssertionMissingMajorVersionAttributeOnRead = "SAMLAssertionMissingMajorVersionAttributeOnRead";

		// Token: 0x04000828 RID: 2088
		internal const string SAMLAssertionMissingMinorVersionAttributeOnRead = "SAMLAssertionMissingMinorVersionAttributeOnRead";

		// Token: 0x04000829 RID: 2089
		internal const string SAMLAssertionIssuerRequired = "SAMLAssertionIssuerRequired";

		// Token: 0x0400082A RID: 2090
		internal const string SAMLAssertionMissingIssuerAttributeOnRead = "SAMLAssertionMissingIssuerAttributeOnRead";

		// Token: 0x0400082B RID: 2091
		internal const string SAMLAssertionRequireOneStatement = "SAMLAssertionRequireOneStatement";

		// Token: 0x0400082C RID: 2092
		internal const string SAMLAssertionRequireOneStatementOnRead = "SAMLAssertionRequireOneStatementOnRead";

		// Token: 0x0400082D RID: 2093
		internal const string SAMLAttributeValueCannotBeNull = "SAMLAttributeValueCannotBeNull";

		// Token: 0x0400082E RID: 2094
		internal const string SAMLAttributeShouldHaveOneValue = "SAMLAttributeShouldHaveOneValue";

		// Token: 0x0400082F RID: 2095
		internal const string SAMLAttributeNameAttributeRequired = "SAMLAttributeNameAttributeRequired";

		// Token: 0x04000830 RID: 2096
		internal const string SAMLAttributeMissingNameAttributeOnRead = "SAMLAttributeMissingNameAttributeOnRead";

		// Token: 0x04000831 RID: 2097
		internal const string SAMLAttributeNamespaceAttributeRequired = "SAMLAttributeNamespaceAttributeRequired";

		// Token: 0x04000832 RID: 2098
		internal const string SAMLAttributeMissingNamespaceAttributeOnRead = "SAMLAttributeMissingNamespaceAttributeOnRead";

		// Token: 0x04000833 RID: 2099
		internal const string SAMLAudienceRestrictionShouldHaveOneAudience = "SAMLAudienceRestrictionShouldHaveOneAudience";

		// Token: 0x04000834 RID: 2100
		internal const string SAMLAudienceRestrictionShouldHaveOneAudienceOnRead = "SAMLAudienceRestrictionShouldHaveOneAudienceOnRead";

		// Token: 0x04000835 RID: 2101
		internal const string SAMLAudienceRestrictionInvalidAudienceValueOnRead = "SAMLAudienceRestrictionInvalidAudienceValueOnRead";

		// Token: 0x04000836 RID: 2102
		internal const string SAMLAudienceUrisNotFound = "SAMLAudienceUrisNotFound";

		// Token: 0x04000837 RID: 2103
		internal const string SAMLAudienceUriValidationFailed = "SAMLAudienceUriValidationFailed";

		// Token: 0x04000838 RID: 2104
		internal const string SAMLAuthorizationDecisionShouldHaveOneAction = "SAMLAuthorizationDecisionShouldHaveOneAction";

		// Token: 0x04000839 RID: 2105
		internal const string SAMLAuthorizationDecisionShouldHaveOneActionOnRead = "SAMLAuthorizationDecisionShouldHaveOneActionOnRead";

		// Token: 0x0400083A RID: 2106
		internal const string SAMLActionNameRequired = "SAMLActionNameRequired";

		// Token: 0x0400083B RID: 2107
		internal const string SAMLActionNameRequiredOnRead = "SAMLActionNameRequiredOnRead";

		// Token: 0x0400083C RID: 2108
		internal const string SAMLAuthorizationDecisionHasMoreThanOneEvidence = "SAMLAuthorizationDecisionHasMoreThanOneEvidence";

		// Token: 0x0400083D RID: 2109
		internal const string SAMLAuthorizationDecisionResourceRequired = "SAMLAuthorizationDecisionResourceRequired";

		// Token: 0x0400083E RID: 2110
		internal const string SAMLAuthenticationStatementMissingAuthenticationInstanceOnRead = "SAMLAuthenticationStatementMissingAuthenticationInstanceOnRead";

		// Token: 0x0400083F RID: 2111
		internal const string SAMLAuthenticationStatementMissingAuthenticationMethod = "SAMLAuthenticationStatementMissingAuthenticationMethod";

		// Token: 0x04000840 RID: 2112
		internal const string SAMLAuthenticationStatementMissingAuthenticationMethodOnRead = "SAMLAuthenticationStatementMissingAuthenticationMethodOnRead";

		// Token: 0x04000841 RID: 2113
		internal const string SAMLAuthenticationStatementMissingSubject = "SAMLAuthenticationStatementMissingSubject";

		// Token: 0x04000842 RID: 2114
		internal const string SAMLAuthorityBindingInvalidAuthorityKind = "SAMLAuthorityBindingInvalidAuthorityKind";

		// Token: 0x04000843 RID: 2115
		internal const string SAMLAuthorityBindingMissingAuthorityKind = "SAMLAuthorityBindingMissingAuthorityKind";

		// Token: 0x04000844 RID: 2116
		internal const string SAMLAuthorityBindingMissingAuthorityKindOnRead = "SAMLAuthorityBindingMissingAuthorityKindOnRead";

		// Token: 0x04000845 RID: 2117
		internal const string SAMLAuthorityKindMissingName = "SAMLAuthorityKindMissingName";

		// Token: 0x04000846 RID: 2118
		internal const string SAMLAuthorityBindingRequiresBinding = "SAMLAuthorityBindingRequiresBinding";

		// Token: 0x04000847 RID: 2119
		internal const string SAMLAuthorityBindingMissingBindingOnRead = "SAMLAuthorityBindingMissingBindingOnRead";

		// Token: 0x04000848 RID: 2120
		internal const string SAMLAuthorityBindingRequiresLocation = "SAMLAuthorityBindingRequiresLocation";

		// Token: 0x04000849 RID: 2121
		internal const string SAMLAuthorityBindingMissingLocationOnRead = "SAMLAuthorityBindingMissingLocationOnRead";

		// Token: 0x0400084A RID: 2122
		internal const string SAMLAuthorizationDecisionStatementMissingResourceAttributeOnRead = "SAMLAuthorizationDecisionStatementMissingResourceAttributeOnRead";

		// Token: 0x0400084B RID: 2123
		internal const string SAMLAuthorizationDecisionStatementMissingDecisionAttributeOnRead = "SAMLAuthorizationDecisionStatementMissingDecisionAttributeOnRead";

		// Token: 0x0400084C RID: 2124
		internal const string SAMLAuthorizationDecisionStatementMissingSubjectOnRead = "SAMLAuthorizationDecisionStatementMissingSubjectOnRead";

		// Token: 0x0400084D RID: 2125
		internal const string SAMLAttributeStatementMissingSubjectOnRead = "SAMLAttributeStatementMissingSubjectOnRead";

		// Token: 0x0400084E RID: 2126
		internal const string SAMLSubjectStatementRequiresSubject = "SAMLSubjectStatementRequiresSubject";

		// Token: 0x0400084F RID: 2127
		internal const string SAMLAttributeStatementMissingAttributeOnRead = "SAMLAttributeStatementMissingAttributeOnRead";

		// Token: 0x04000850 RID: 2128
		internal const string SAMLBadSchema = "SAMLBadSchema";

		// Token: 0x04000851 RID: 2129
		internal const string SAMLElementNotRecognized = "SAMLElementNotRecognized";

		// Token: 0x04000852 RID: 2130
		internal const string SAMLEntityCannotBeNullOrEmpty = "SAMLEntityCannotBeNullOrEmpty";

		// Token: 0x04000853 RID: 2131
		internal const string SAMLEvidenceShouldHaveOneAssertion = "SAMLEvidenceShouldHaveOneAssertion";

		// Token: 0x04000854 RID: 2132
		internal const string SAMLEvidenceShouldHaveOneAssertionOnRead = "SAMLEvidenceShouldHaveOneAssertionOnRead";

		// Token: 0x04000855 RID: 2133
		internal const string SAMLNameIdentifierMissingIdentifierValueOnRead = "SAMLNameIdentifierMissingIdentifierValueOnRead";

		// Token: 0x04000856 RID: 2134
		internal const string SAMLSubjectNameIdentifierRequiresNameValue = "SAMLSubjectNameIdentifierRequiresNameValue";

		// Token: 0x04000857 RID: 2135
		internal const string SAMLSubjectRequiresNameIdentifierOrConfirmationMethod = "SAMLSubjectRequiresNameIdentifierOrConfirmationMethod";

		// Token: 0x04000858 RID: 2136
		internal const string SAMLSubjectRequiresNameIdentifierOrConfirmationMethodOnRead = "SAMLSubjectRequiresNameIdentifierOrConfirmationMethodOnRead";

		// Token: 0x04000859 RID: 2137
		internal const string SAMLSubjectRequiresConfirmationMethodWhenConfirmationDataOrKeyInfoIsSpecified = "SAMLSubjectRequiresConfirmationMethodWhenConfirmationDataOrKeyInfoIsSpecified";

		// Token: 0x0400085A RID: 2138
		internal const string SAMLSubjectConfirmationClauseMissingConfirmationMethodOnRead = "SAMLSubjectConfirmationClauseMissingConfirmationMethodOnRead";

		// Token: 0x0400085B RID: 2139
		internal const string SAMLTokenNotSerialized = "SAMLTokenNotSerialized";

		// Token: 0x0400085C RID: 2140
		internal const string SAMLTokenTimeInvalid = "SAMLTokenTimeInvalid";

		// Token: 0x0400085D RID: 2141
		internal const string SAMLTokenVersionNotSupported = "SAMLTokenVersionNotSupported";

		// Token: 0x0400085E RID: 2142
		internal const string SAMLSignatureAlreadyRead = "SAMLSignatureAlreadyRead";

		// Token: 0x0400085F RID: 2143
		internal const string SAMLUnableToLoadUnknownElement = "SAMLUnableToLoadUnknownElement";

		// Token: 0x04000860 RID: 2144
		internal const string SAMLUnableToResolveSignatureKey = "SAMLUnableToResolveSignatureKey";

		// Token: 0x04000861 RID: 2145
		internal const string SAMLUnableToLoadAssertion = "SAMLUnableToLoadAssertion";

		// Token: 0x04000862 RID: 2146
		internal const string SAMLUnableToLoadCondtion = "SAMLUnableToLoadCondtion";

		// Token: 0x04000863 RID: 2147
		internal const string SAMLUnableToLoadCondtions = "SAMLUnableToLoadCondtions";

		// Token: 0x04000864 RID: 2148
		internal const string SAMLUnableToLoadAdvice = "SAMLUnableToLoadAdvice";

		// Token: 0x04000865 RID: 2149
		internal const string SAMLUnableToLoadStatement = "SAMLUnableToLoadStatement";

		// Token: 0x04000866 RID: 2150
		internal const string SAMLUnableToLoadAttribute = "SAMLUnableToLoadAttribute";

		// Token: 0x04000867 RID: 2151
		internal const string SymmetricKeyLengthTooShort = "SymmetricKeyLengthTooShort";

		// Token: 0x04000868 RID: 2152
		internal const string InvalidHexString = "InvalidHexString";

		// Token: 0x04000869 RID: 2153
		internal const string FailInitializeSecurityContext = "FailInitializeSecurityContext";

		// Token: 0x0400086A RID: 2154
		internal const string FailAcceptSecurityContext = "FailAcceptSecurityContext";

		// Token: 0x0400086B RID: 2155
		internal const string FailLogonUser = "FailLogonUser";

		// Token: 0x0400086C RID: 2156
		internal const string KerberosMultilegsNotSupported = "KerberosMultilegsNotSupported";

		// Token: 0x0400086D RID: 2157
		internal const string KerberosApReqInvalidOrOutOfMemory = "KerberosApReqInvalidOrOutOfMemory";

		// Token: 0x0400086E RID: 2158
		internal const string SspiPayloadNotEncrypted = "SspiPayloadNotEncrypted";

		// Token: 0x0400086F RID: 2159
		internal const string SSPIPackageNotSupported = "SSPIPackageNotSupported";

		// Token: 0x04000870 RID: 2160
		internal const string SspiWrapperEncryptDecryptAssert1 = "SspiWrapperEncryptDecryptAssert1";

		// Token: 0x04000871 RID: 2161
		internal const string SspiWrapperEncryptDecryptAssert2 = "SspiWrapperEncryptDecryptAssert2";

		// Token: 0x04000872 RID: 2162
		internal const string RevertingPrivilegeFailed = "RevertingPrivilegeFailed";

		// Token: 0x04000873 RID: 2163
		internal const string InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched = "InvalidServiceBindingInSspiNegotiationServiceBindingNotMatched";

		// Token: 0x04000874 RID: 2164
		internal const string InvalidServiceBindingInSspiNegotiationNoServiceBinding = "InvalidServiceBindingInSspiNegotiationNoServiceBinding";

		// Token: 0x04000875 RID: 2165
		internal const string AESCipherModeNotSupported = "AESCipherModeNotSupported";

		// Token: 0x04000876 RID: 2166
		internal const string AESKeyLengthNotSupported = "AESKeyLengthNotSupported";

		// Token: 0x04000877 RID: 2167
		internal const string AESIVLengthNotSupported = "AESIVLengthNotSupported";

		// Token: 0x04000878 RID: 2168
		internal const string AESPaddingModeNotSupported = "AESPaddingModeNotSupported";

		// Token: 0x04000879 RID: 2169
		internal const string AESCryptAcquireContextFailed = "AESCryptAcquireContextFailed";

		// Token: 0x0400087A RID: 2170
		internal const string AESCryptImportKeyFailed = "AESCryptImportKeyFailed";

		// Token: 0x0400087B RID: 2171
		internal const string AESCryptGetKeyParamFailed = "AESCryptGetKeyParamFailed";

		// Token: 0x0400087C RID: 2172
		internal const string AESCryptSetKeyParamFailed = "AESCryptSetKeyParamFailed";

		// Token: 0x0400087D RID: 2173
		internal const string AESCryptEncryptFailed = "AESCryptEncryptFailed";

		// Token: 0x0400087E RID: 2174
		internal const string AESCryptDecryptFailed = "AESCryptDecryptFailed";

		// Token: 0x0400087F RID: 2175
		internal const string AESInvalidInputBlockSize = "AESInvalidInputBlockSize";

		// Token: 0x04000880 RID: 2176
		internal const string AESInsufficientOutputBuffer = "AESInsufficientOutputBuffer";

		// Token: 0x04000881 RID: 2177
		internal const string ID0001 = "ID0001";

		// Token: 0x04000882 RID: 2178
		internal const string ID0002 = "ID0002";

		// Token: 0x04000883 RID: 2179
		internal const string ID0003 = "ID0003";

		// Token: 0x04000884 RID: 2180
		internal const string ID0005 = "ID0005";

		// Token: 0x04000885 RID: 2181
		internal const string ID0006 = "ID0006";

		// Token: 0x04000886 RID: 2182
		internal const string ID0008 = "ID0008";

		// Token: 0x04000887 RID: 2183
		internal const string ID0009 = "ID0009";

		// Token: 0x04000888 RID: 2184
		internal const string ID0011 = "ID0011";

		// Token: 0x04000889 RID: 2185
		internal const string ID0012 = "ID0012";

		// Token: 0x0400088A RID: 2186
		internal const string ID0013 = "ID0013";

		// Token: 0x0400088B RID: 2187
		internal const string ID0014 = "ID0014";

		// Token: 0x0400088C RID: 2188
		internal const string ID0016 = "ID0016";

		// Token: 0x0400088D RID: 2189
		internal const string ID0018 = "ID0018";

		// Token: 0x0400088E RID: 2190
		internal const string ID0019 = "ID0019";

		// Token: 0x0400088F RID: 2191
		internal const string ID0021 = "ID0021";

		// Token: 0x04000890 RID: 2192
		internal const string ID0022 = "ID0022";

		// Token: 0x04000891 RID: 2193
		internal const string ID0023 = "ID0023";

		// Token: 0x04000892 RID: 2194
		internal const string ID1001 = "ID1001";

		// Token: 0x04000893 RID: 2195
		internal const string ID1002 = "ID1002";

		// Token: 0x04000894 RID: 2196
		internal const string ID1005 = "ID1005";

		// Token: 0x04000895 RID: 2197
		internal const string ID1006 = "ID1006";

		// Token: 0x04000896 RID: 2198
		internal const string ID1007 = "ID1007";

		// Token: 0x04000897 RID: 2199
		internal const string ID1008 = "ID1008";

		// Token: 0x04000898 RID: 2200
		internal const string ID1009 = "ID1009";

		// Token: 0x04000899 RID: 2201
		internal const string ID1012 = "ID1012";

		// Token: 0x0400089A RID: 2202
		internal const string ID1013 = "ID1013";

		// Token: 0x0400089B RID: 2203
		internal const string ID1014 = "ID1014";

		// Token: 0x0400089C RID: 2204
		internal const string ID1024 = "ID1024";

		// Token: 0x0400089D RID: 2205
		internal const string ID1025 = "ID1025";

		// Token: 0x0400089E RID: 2206
		internal const string ID1029 = "ID1029";

		// Token: 0x0400089F RID: 2207
		internal const string ID1032 = "ID1032";

		// Token: 0x040008A0 RID: 2208
		internal const string ID1033 = "ID1033";

		// Token: 0x040008A1 RID: 2209
		internal const string ID1034 = "ID1034";

		// Token: 0x040008A2 RID: 2210
		internal const string ID1035 = "ID1035";

		// Token: 0x040008A3 RID: 2211
		internal const string ID1036 = "ID1036";

		// Token: 0x040008A4 RID: 2212
		internal const string ID1037 = "ID1037";

		// Token: 0x040008A5 RID: 2213
		internal const string ID1038 = "ID1038";

		// Token: 0x040008A6 RID: 2214
		internal const string ID1039 = "ID1039";

		// Token: 0x040008A7 RID: 2215
		internal const string ID1043 = "ID1043";

		// Token: 0x040008A8 RID: 2216
		internal const string ID1053 = "ID1053";

		// Token: 0x040008A9 RID: 2217
		internal const string ID1054 = "ID1054";

		// Token: 0x040008AA RID: 2218
		internal const string ID1062 = "ID1062";

		// Token: 0x040008AB RID: 2219
		internal const string ID1063 = "ID1063";

		// Token: 0x040008AC RID: 2220
		internal const string ID1064 = "ID1064";

		// Token: 0x040008AD RID: 2221
		internal const string ID1065 = "ID1065";

		// Token: 0x040008AE RID: 2222
		internal const string ID1066 = "ID1066";

		// Token: 0x040008AF RID: 2223
		internal const string ID1067 = "ID1067";

		// Token: 0x040008B0 RID: 2224
		internal const string ID1068 = "ID1068";

		// Token: 0x040008B1 RID: 2225
		internal const string ID1069 = "ID1069";

		// Token: 0x040008B2 RID: 2226
		internal const string ID1070 = "ID1070";

		// Token: 0x040008B3 RID: 2227
		internal const string ID1072 = "ID1072";

		// Token: 0x040008B4 RID: 2228
		internal const string ID1073 = "ID1073";

		// Token: 0x040008B5 RID: 2229
		internal const string ID1074 = "ID1074";

		// Token: 0x040008B6 RID: 2230
		internal const string ID2000 = "ID2000";

		// Token: 0x040008B7 RID: 2231
		internal const string ID2001 = "ID2001";

		// Token: 0x040008B8 RID: 2232
		internal const string ID2002 = "ID2002";

		// Token: 0x040008B9 RID: 2233
		internal const string ID2003 = "ID2003";

		// Token: 0x040008BA RID: 2234
		internal const string ID2004 = "ID2004";

		// Token: 0x040008BB RID: 2235
		internal const string ID2005 = "ID2005";

		// Token: 0x040008BC RID: 2236
		internal const string ID2008 = "ID2008";

		// Token: 0x040008BD RID: 2237
		internal const string ID2009 = "ID2009";

		// Token: 0x040008BE RID: 2238
		internal const string ID2011 = "ID2011";

		// Token: 0x040008BF RID: 2239
		internal const string ID2012 = "ID2012";

		// Token: 0x040008C0 RID: 2240
		internal const string ID2013 = "ID2013";

		// Token: 0x040008C1 RID: 2241
		internal const string ID2014 = "ID2014";

		// Token: 0x040008C2 RID: 2242
		internal const string ID2015 = "ID2015";

		// Token: 0x040008C3 RID: 2243
		internal const string ID2016 = "ID2016";

		// Token: 0x040008C4 RID: 2244
		internal const string ID2050 = "ID2050";

		// Token: 0x040008C5 RID: 2245
		internal const string ID2051 = "ID2051";

		// Token: 0x040008C6 RID: 2246
		internal const string ID2052 = "ID2052";

		// Token: 0x040008C7 RID: 2247
		internal const string ID2053 = "ID2053";

		// Token: 0x040008C8 RID: 2248
		internal const string ID2055 = "ID2055";

		// Token: 0x040008C9 RID: 2249
		internal const string ID2056 = "ID2056";

		// Token: 0x040008CA RID: 2250
		internal const string ID2057 = "ID2057";

		// Token: 0x040008CB RID: 2251
		internal const string ID2058 = "ID2058";

		// Token: 0x040008CC RID: 2252
		internal const string ID2059 = "ID2059";

		// Token: 0x040008CD RID: 2253
		internal const string ID2064 = "ID2064";

		// Token: 0x040008CE RID: 2254
		internal const string ID2069 = "ID2069";

		// Token: 0x040008CF RID: 2255
		internal const string ID2070 = "ID2070";

		// Token: 0x040008D0 RID: 2256
		internal const string ID2073 = "ID2073";

		// Token: 0x040008D1 RID: 2257
		internal const string ID2074 = "ID2074";

		// Token: 0x040008D2 RID: 2258
		internal const string ID2079 = "ID2079";

		// Token: 0x040008D3 RID: 2259
		internal const string ID2080 = "ID2080";

		// Token: 0x040008D4 RID: 2260
		internal const string ID2072 = "ID2072";

		// Token: 0x040008D5 RID: 2261
		internal const string ID2075 = "ID2075";

		// Token: 0x040008D6 RID: 2262
		internal const string ID2076 = "ID2076";

		// Token: 0x040008D7 RID: 2263
		internal const string ID2077 = "ID2077";

		// Token: 0x040008D8 RID: 2264
		internal const string ID2078 = "ID2078";

		// Token: 0x040008D9 RID: 2265
		internal const string ID2081 = "ID2081";

		// Token: 0x040008DA RID: 2266
		internal const string ID2082 = "ID2082";

		// Token: 0x040008DB RID: 2267
		internal const string ID2083 = "ID2083";

		// Token: 0x040008DC RID: 2268
		internal const string ID2084 = "ID2084";

		// Token: 0x040008DD RID: 2269
		internal const string ID2100 = "ID2100";

		// Token: 0x040008DE RID: 2270
		internal const string ID3006 = "ID3006";

		// Token: 0x040008DF RID: 2271
		internal const string ID3007 = "ID3007";

		// Token: 0x040008E0 RID: 2272
		internal const string ID3009 = "ID3009";

		// Token: 0x040008E1 RID: 2273
		internal const string ID3010 = "ID3010";

		// Token: 0x040008E2 RID: 2274
		internal const string ID3011 = "ID3011";

		// Token: 0x040008E3 RID: 2275
		internal const string ID3012 = "ID3012";

		// Token: 0x040008E4 RID: 2276
		internal const string ID3013 = "ID3013";

		// Token: 0x040008E5 RID: 2277
		internal const string ID3017 = "ID3017";

		// Token: 0x040008E6 RID: 2278
		internal const string ID3020 = "ID3020";

		// Token: 0x040008E7 RID: 2279
		internal const string ID3021 = "ID3021";

		// Token: 0x040008E8 RID: 2280
		internal const string ID3025 = "ID3025";

		// Token: 0x040008E9 RID: 2281
		internal const string ID3026 = "ID3026";

		// Token: 0x040008EA RID: 2282
		internal const string ID3027 = "ID3027";

		// Token: 0x040008EB RID: 2283
		internal const string ID3032 = "ID3032";

		// Token: 0x040008EC RID: 2284
		internal const string ID3057 = "ID3057";

		// Token: 0x040008ED RID: 2285
		internal const string ID3061 = "ID3061";

		// Token: 0x040008EE RID: 2286
		internal const string ID3063 = "ID3063";

		// Token: 0x040008EF RID: 2287
		internal const string ID3064 = "ID3064";

		// Token: 0x040008F0 RID: 2288
		internal const string ID3089 = "ID3089";

		// Token: 0x040008F1 RID: 2289
		internal const string ID3091 = "ID3091";

		// Token: 0x040008F2 RID: 2290
		internal const string ID3092 = "ID3092";

		// Token: 0x040008F3 RID: 2291
		internal const string ID3130 = "ID3130";

		// Token: 0x040008F4 RID: 2292
		internal const string ID3135 = "ID3135";

		// Token: 0x040008F5 RID: 2293
		internal const string ID3136 = "ID3136";

		// Token: 0x040008F6 RID: 2294
		internal const string ID3141 = "ID3141";

		// Token: 0x040008F7 RID: 2295
		internal const string ID3151 = "ID3151";

		// Token: 0x040008F8 RID: 2296
		internal const string ID3152 = "ID3152";

		// Token: 0x040008F9 RID: 2297
		internal const string ID3153 = "ID3153";

		// Token: 0x040008FA RID: 2298
		internal const string ID3154 = "ID3154";

		// Token: 0x040008FB RID: 2299
		internal const string ID3155 = "ID3155";

		// Token: 0x040008FC RID: 2300
		internal const string ID3158 = "ID3158";

		// Token: 0x040008FD RID: 2301
		internal const string ID3159 = "ID3159";

		// Token: 0x040008FE RID: 2302
		internal const string ID3160 = "ID3160";

		// Token: 0x040008FF RID: 2303
		internal const string ID3161 = "ID3161";

		// Token: 0x04000900 RID: 2304
		internal const string ID3162 = "ID3162";

		// Token: 0x04000901 RID: 2305
		internal const string ID3164 = "ID3164";

		// Token: 0x04000902 RID: 2306
		internal const string ID3165 = "ID3165";

		// Token: 0x04000903 RID: 2307
		internal const string ID3166 = "ID3166";

		// Token: 0x04000904 RID: 2308
		internal const string ID3198 = "ID3198";

		// Token: 0x04000905 RID: 2309
		internal const string ID3199 = "ID3199";

		// Token: 0x04000906 RID: 2310
		internal const string ID3200 = "ID3200";

		// Token: 0x04000907 RID: 2311
		internal const string ID3201 = "ID3201";

		// Token: 0x04000908 RID: 2312
		internal const string ID3202 = "ID3202";

		// Token: 0x04000909 RID: 2313
		internal const string ID3203 = "ID3203";

		// Token: 0x0400090A RID: 2314
		internal const string ID3207 = "ID3207";

		// Token: 0x0400090B RID: 2315
		internal const string ID3215 = "ID3215";

		// Token: 0x0400090C RID: 2316
		internal const string ID3216 = "ID3216";

		// Token: 0x0400090D RID: 2317
		internal const string ID3217 = "ID3217";

		// Token: 0x0400090E RID: 2318
		internal const string ID3218 = "ID3218";

		// Token: 0x0400090F RID: 2319
		internal const string ID3219 = "ID3219";

		// Token: 0x04000910 RID: 2320
		internal const string ID3220 = "ID3220";

		// Token: 0x04000911 RID: 2321
		internal const string ID3221 = "ID3221";

		// Token: 0x04000912 RID: 2322
		internal const string ID3222 = "ID3222";

		// Token: 0x04000913 RID: 2323
		internal const string ID3223 = "ID3223";

		// Token: 0x04000914 RID: 2324
		internal const string ID3249 = "ID3249";

		// Token: 0x04000915 RID: 2325
		internal const string ID3257 = "ID3257";

		// Token: 0x04000916 RID: 2326
		internal const string ID3258 = "ID3258";

		// Token: 0x04000917 RID: 2327
		internal const string ID3260 = "ID3260";

		// Token: 0x04000918 RID: 2328
		internal const string ID3264 = "ID3264";

		// Token: 0x04000919 RID: 2329
		internal const string ID3265 = "ID3265";

		// Token: 0x0400091A RID: 2330
		internal const string ID3268 = "ID3268";

		// Token: 0x0400091B RID: 2331
		internal const string ID3274 = "ID3274";

		// Token: 0x0400091C RID: 2332
		internal const string ID3275 = "ID3275";

		// Token: 0x0400091D RID: 2333
		internal const string ID3276 = "ID3276";

		// Token: 0x0400091E RID: 2334
		internal const string ID3284 = "ID3284";

		// Token: 0x0400091F RID: 2335
		internal const string ID4001 = "ID4001";

		// Token: 0x04000920 RID: 2336
		internal const string ID4002 = "ID4002";

		// Token: 0x04000921 RID: 2337
		internal const string ID4003 = "ID4003";

		// Token: 0x04000922 RID: 2338
		internal const string ID4004 = "ID4004";

		// Token: 0x04000923 RID: 2339
		internal const string ID4005 = "ID4005";

		// Token: 0x04000924 RID: 2340
		internal const string ID4007 = "ID4007";

		// Token: 0x04000925 RID: 2341
		internal const string ID4008 = "ID4008";

		// Token: 0x04000926 RID: 2342
		internal const string ID4010 = "ID4010";

		// Token: 0x04000927 RID: 2343
		internal const string ID4011 = "ID4011";

		// Token: 0x04000928 RID: 2344
		internal const string ID4013 = "ID4013";

		// Token: 0x04000929 RID: 2345
		internal const string ID4014 = "ID4014";

		// Token: 0x0400092A RID: 2346
		internal const string ID4020 = "ID4020";

		// Token: 0x0400092B RID: 2347
		internal const string ID4022 = "ID4022";

		// Token: 0x0400092C RID: 2348
		internal const string ID4023 = "ID4023";

		// Token: 0x0400092D RID: 2349
		internal const string ID4024 = "ID4024";

		// Token: 0x0400092E RID: 2350
		internal const string ID4025 = "ID4025";

		// Token: 0x0400092F RID: 2351
		internal const string ID4026 = "ID4026";

		// Token: 0x04000930 RID: 2352
		internal const string ID4034 = "ID4034";

		// Token: 0x04000931 RID: 2353
		internal const string ID4036 = "ID4036";

		// Token: 0x04000932 RID: 2354
		internal const string ID4037 = "ID4037";

		// Token: 0x04000933 RID: 2355
		internal const string ID4038 = "ID4038";

		// Token: 0x04000934 RID: 2356
		internal const string ID4046 = "ID4046";

		// Token: 0x04000935 RID: 2357
		internal const string ID4050 = "ID4050";

		// Token: 0x04000936 RID: 2358
		internal const string ID4051 = "ID4051";

		// Token: 0x04000937 RID: 2359
		internal const string ID4052 = "ID4052";

		// Token: 0x04000938 RID: 2360
		internal const string ID4059 = "ID4059";

		// Token: 0x04000939 RID: 2361
		internal const string ID4060 = "ID4060";

		// Token: 0x0400093A RID: 2362
		internal const string ID4061 = "ID4061";

		// Token: 0x0400093B RID: 2363
		internal const string ID4062 = "ID4062";

		// Token: 0x0400093C RID: 2364
		internal const string ID4063 = "ID4063";

		// Token: 0x0400093D RID: 2365
		internal const string ID4065 = "ID4065";

		// Token: 0x0400093E RID: 2366
		internal const string ID4066 = "ID4066";

		// Token: 0x0400093F RID: 2367
		internal const string ID4067 = "ID4067";

		// Token: 0x04000940 RID: 2368
		internal const string ID4068 = "ID4068";

		// Token: 0x04000941 RID: 2369
		internal const string ID4070 = "ID4070";

		// Token: 0x04000942 RID: 2370
		internal const string ID4073 = "ID4073";

		// Token: 0x04000943 RID: 2371
		internal const string ID4075 = "ID4075";

		// Token: 0x04000944 RID: 2372
		internal const string ID4076 = "ID4076";

		// Token: 0x04000945 RID: 2373
		internal const string ID4077 = "ID4077";

		// Token: 0x04000946 RID: 2374
		internal const string ID4078 = "ID4078";

		// Token: 0x04000947 RID: 2375
		internal const string ID4079 = "ID4079";

		// Token: 0x04000948 RID: 2376
		internal const string ID4080 = "ID4080";

		// Token: 0x04000949 RID: 2377
		internal const string ID4081 = "ID4081";

		// Token: 0x0400094A RID: 2378
		internal const string ID4082 = "ID4082";

		// Token: 0x0400094B RID: 2379
		internal const string ID4083 = "ID4083";

		// Token: 0x0400094C RID: 2380
		internal const string ID4084 = "ID4084";

		// Token: 0x0400094D RID: 2381
		internal const string ID4085 = "ID4085";

		// Token: 0x0400094E RID: 2382
		internal const string ID4086 = "ID4086";

		// Token: 0x0400094F RID: 2383
		internal const string ID4087 = "ID4087";

		// Token: 0x04000950 RID: 2384
		internal const string ID4088 = "ID4088";

		// Token: 0x04000951 RID: 2385
		internal const string ID4089 = "ID4089";

		// Token: 0x04000952 RID: 2386
		internal const string ID4090 = "ID4090";

		// Token: 0x04000953 RID: 2387
		internal const string ID4091 = "ID4091";

		// Token: 0x04000954 RID: 2388
		internal const string ID4092 = "ID4092";

		// Token: 0x04000955 RID: 2389
		internal const string ID4093 = "ID4093";

		// Token: 0x04000956 RID: 2390
		internal const string ID4094 = "ID4094";

		// Token: 0x04000957 RID: 2391
		internal const string ID4095 = "ID4095";

		// Token: 0x04000958 RID: 2392
		internal const string ID4096 = "ID4096";

		// Token: 0x04000959 RID: 2393
		internal const string ID4097 = "ID4097";

		// Token: 0x0400095A RID: 2394
		internal const string ID4098 = "ID4098";

		// Token: 0x0400095B RID: 2395
		internal const string ID4099 = "ID4099";

		// Token: 0x0400095C RID: 2396
		internal const string ID4100 = "ID4100";

		// Token: 0x0400095D RID: 2397
		internal const string ID4102 = "ID4102";

		// Token: 0x0400095E RID: 2398
		internal const string ID4104 = "ID4104";

		// Token: 0x0400095F RID: 2399
		internal const string ID4105 = "ID4105";

		// Token: 0x04000960 RID: 2400
		internal const string ID4106 = "ID4106";

		// Token: 0x04000961 RID: 2401
		internal const string ID4107 = "ID4107";

		// Token: 0x04000962 RID: 2402
		internal const string ID4108 = "ID4108";

		// Token: 0x04000963 RID: 2403
		internal const string ID4110 = "ID4110";

		// Token: 0x04000964 RID: 2404
		internal const string ID4111 = "ID4111";

		// Token: 0x04000965 RID: 2405
		internal const string ID4112 = "ID4112";

		// Token: 0x04000966 RID: 2406
		internal const string ID4113 = "ID4113";

		// Token: 0x04000967 RID: 2407
		internal const string ID4114 = "ID4114";

		// Token: 0x04000968 RID: 2408
		internal const string ID4115 = "ID4115";

		// Token: 0x04000969 RID: 2409
		internal const string ID4116 = "ID4116";

		// Token: 0x0400096A RID: 2410
		internal const string ID4117 = "ID4117";

		// Token: 0x0400096B RID: 2411
		internal const string ID4118 = "ID4118";

		// Token: 0x0400096C RID: 2412
		internal const string ID4119 = "ID4119";

		// Token: 0x0400096D RID: 2413
		internal const string ID4120 = "ID4120";

		// Token: 0x0400096E RID: 2414
		internal const string ID4121 = "ID4121";

		// Token: 0x0400096F RID: 2415
		internal const string ID4122 = "ID4122";

		// Token: 0x04000970 RID: 2416
		internal const string ID4123 = "ID4123";

		// Token: 0x04000971 RID: 2417
		internal const string ID4124 = "ID4124";

		// Token: 0x04000972 RID: 2418
		internal const string ID4125 = "ID4125";

		// Token: 0x04000973 RID: 2419
		internal const string ID4126 = "ID4126";

		// Token: 0x04000974 RID: 2420
		internal const string ID4127 = "ID4127";

		// Token: 0x04000975 RID: 2421
		internal const string ID4128 = "ID4128";

		// Token: 0x04000976 RID: 2422
		internal const string ID4129 = "ID4129";

		// Token: 0x04000977 RID: 2423
		internal const string ID4130 = "ID4130";

		// Token: 0x04000978 RID: 2424
		internal const string ID4131 = "ID4131";

		// Token: 0x04000979 RID: 2425
		internal const string ID4132 = "ID4132";

		// Token: 0x0400097A RID: 2426
		internal const string ID4133 = "ID4133";

		// Token: 0x0400097B RID: 2427
		internal const string ID4134 = "ID4134";

		// Token: 0x0400097C RID: 2428
		internal const string ID4136 = "ID4136";

		// Token: 0x0400097D RID: 2429
		internal const string ID4138 = "ID4138";

		// Token: 0x0400097E RID: 2430
		internal const string ID4139 = "ID4139";

		// Token: 0x0400097F RID: 2431
		internal const string ID4140 = "ID4140";

		// Token: 0x04000980 RID: 2432
		internal const string ID4141 = "ID4141";

		// Token: 0x04000981 RID: 2433
		internal const string ID4142 = "ID4142";

		// Token: 0x04000982 RID: 2434
		internal const string ID4147 = "ID4147";

		// Token: 0x04000983 RID: 2435
		internal const string ID4148 = "ID4148";

		// Token: 0x04000984 RID: 2436
		internal const string ID4149 = "ID4149";

		// Token: 0x04000985 RID: 2437
		internal const string ID4150 = "ID4150";

		// Token: 0x04000986 RID: 2438
		internal const string ID4151 = "ID4151";

		// Token: 0x04000987 RID: 2439
		internal const string ID4152 = "ID4152";

		// Token: 0x04000988 RID: 2440
		internal const string ID4153 = "ID4153";

		// Token: 0x04000989 RID: 2441
		internal const string ID4154 = "ID4154";

		// Token: 0x0400098A RID: 2442
		internal const string ID4157 = "ID4157";

		// Token: 0x0400098B RID: 2443
		internal const string ID4158 = "ID4158";

		// Token: 0x0400098C RID: 2444
		internal const string ID4159 = "ID4159";

		// Token: 0x0400098D RID: 2445
		internal const string ID4160 = "ID4160";

		// Token: 0x0400098E RID: 2446
		internal const string ID4161 = "ID4161";

		// Token: 0x0400098F RID: 2447
		internal const string ID4162 = "ID4162";

		// Token: 0x04000990 RID: 2448
		internal const string ID4172 = "ID4172";

		// Token: 0x04000991 RID: 2449
		internal const string ID4173 = "ID4173";

		// Token: 0x04000992 RID: 2450
		internal const string ID4175 = "ID4175";

		// Token: 0x04000993 RID: 2451
		internal const string ID4176 = "ID4176";

		// Token: 0x04000994 RID: 2452
		internal const string ID4177 = "ID4177";

		// Token: 0x04000995 RID: 2453
		internal const string ID4178 = "ID4178";

		// Token: 0x04000996 RID: 2454
		internal const string ID4179 = "ID4179";

		// Token: 0x04000997 RID: 2455
		internal const string ID4180 = "ID4180";

		// Token: 0x04000998 RID: 2456
		internal const string ID4181 = "ID4181";

		// Token: 0x04000999 RID: 2457
		internal const string ID4182 = "ID4182";

		// Token: 0x0400099A RID: 2458
		internal const string ID4183 = "ID4183";

		// Token: 0x0400099B RID: 2459
		internal const string ID4184 = "ID4184";

		// Token: 0x0400099C RID: 2460
		internal const string ID4185 = "ID4185";

		// Token: 0x0400099D RID: 2461
		internal const string ID4187 = "ID4187";

		// Token: 0x0400099E RID: 2462
		internal const string ID4188 = "ID4188";

		// Token: 0x0400099F RID: 2463
		internal const string ID4189 = "ID4189";

		// Token: 0x040009A0 RID: 2464
		internal const string ID4190 = "ID4190";

		// Token: 0x040009A1 RID: 2465
		internal const string ID4191 = "ID4191";

		// Token: 0x040009A2 RID: 2466
		internal const string ID4192 = "ID4192";

		// Token: 0x040009A3 RID: 2467
		internal const string ID4193 = "ID4193";

		// Token: 0x040009A4 RID: 2468
		internal const string ID4194 = "ID4194";

		// Token: 0x040009A5 RID: 2469
		internal const string ID4200 = "ID4200";

		// Token: 0x040009A6 RID: 2470
		internal const string ID4201 = "ID4201";

		// Token: 0x040009A7 RID: 2471
		internal const string ID4202 = "ID4202";

		// Token: 0x040009A8 RID: 2472
		internal const string ID4203 = "ID4203";

		// Token: 0x040009A9 RID: 2473
		internal const string ID4204 = "ID4204";

		// Token: 0x040009AA RID: 2474
		internal const string ID4205 = "ID4205";

		// Token: 0x040009AB RID: 2475
		internal const string ID4206 = "ID4206";

		// Token: 0x040009AC RID: 2476
		internal const string ID4207 = "ID4207";

		// Token: 0x040009AD RID: 2477
		internal const string ID4208 = "ID4208";

		// Token: 0x040009AE RID: 2478
		internal const string ID4209 = "ID4209";

		// Token: 0x040009AF RID: 2479
		internal const string ID4210 = "ID4210";

		// Token: 0x040009B0 RID: 2480
		internal const string ID4211 = "ID4211";

		// Token: 0x040009B1 RID: 2481
		internal const string ID4212 = "ID4212";

		// Token: 0x040009B2 RID: 2482
		internal const string ID4213 = "ID4213";

		// Token: 0x040009B3 RID: 2483
		internal const string ID4216 = "ID4216";

		// Token: 0x040009B4 RID: 2484
		internal const string ID4217 = "ID4217";

		// Token: 0x040009B5 RID: 2485
		internal const string ID4218 = "ID4218";

		// Token: 0x040009B6 RID: 2486
		internal const string ID4220 = "ID4220";

		// Token: 0x040009B7 RID: 2487
		internal const string ID4221 = "ID4221";

		// Token: 0x040009B8 RID: 2488
		internal const string ID4222 = "ID4222";

		// Token: 0x040009B9 RID: 2489
		internal const string ID4223 = "ID4223";

		// Token: 0x040009BA RID: 2490
		internal const string ID4224 = "ID4224";

		// Token: 0x040009BB RID: 2491
		internal const string ID4225 = "ID4225";

		// Token: 0x040009BC RID: 2492
		internal const string ID4227 = "ID4227";

		// Token: 0x040009BD RID: 2493
		internal const string ID4229 = "ID4229";

		// Token: 0x040009BE RID: 2494
		internal const string ID4230 = "ID4230";

		// Token: 0x040009BF RID: 2495
		internal const string ID4232 = "ID4232";

		// Token: 0x040009C0 RID: 2496
		internal const string ID4237 = "ID4237";

		// Token: 0x040009C1 RID: 2497
		internal const string ID4239 = "ID4239";

		// Token: 0x040009C2 RID: 2498
		internal const string ID4242 = "ID4242";

		// Token: 0x040009C3 RID: 2499
		internal const string ID4243 = "ID4243";

		// Token: 0x040009C4 RID: 2500
		internal const string ID4248 = "ID4248";

		// Token: 0x040009C5 RID: 2501
		internal const string ID4249 = "ID4249";

		// Token: 0x040009C6 RID: 2502
		internal const string ID4250 = "ID4250";

		// Token: 0x040009C7 RID: 2503
		internal const string ID4251 = "ID4251";

		// Token: 0x040009C8 RID: 2504
		internal const string ID4252 = "ID4252";

		// Token: 0x040009C9 RID: 2505
		internal const string ID4254 = "ID4254";

		// Token: 0x040009CA RID: 2506
		internal const string ID4255 = "ID4255";

		// Token: 0x040009CB RID: 2507
		internal const string ID4256 = "ID4256";

		// Token: 0x040009CC RID: 2508
		internal const string ID4257 = "ID4257";

		// Token: 0x040009CD RID: 2509
		internal const string ID4258 = "ID4258";

		// Token: 0x040009CE RID: 2510
		internal const string ID4259 = "ID4259";

		// Token: 0x040009CF RID: 2511
		internal const string ID4261 = "ID4261";

		// Token: 0x040009D0 RID: 2512
		internal const string ID4262 = "ID4262";

		// Token: 0x040009D1 RID: 2513
		internal const string ID4263 = "ID4263";

		// Token: 0x040009D2 RID: 2514
		internal const string ID4264 = "ID4264";

		// Token: 0x040009D3 RID: 2515
		internal const string ID4265 = "ID4265";

		// Token: 0x040009D4 RID: 2516
		internal const string ID4269 = "ID4269";

		// Token: 0x040009D5 RID: 2517
		internal const string ID4270 = "ID4270";

		// Token: 0x040009D6 RID: 2518
		internal const string ID4272 = "ID4272";

		// Token: 0x040009D7 RID: 2519
		internal const string ID4274 = "ID4274";

		// Token: 0x040009D8 RID: 2520
		internal const string ID4275 = "ID4275";

		// Token: 0x040009D9 RID: 2521
		internal const string ID4276 = "ID4276";

		// Token: 0x040009DA RID: 2522
		internal const string ID4277 = "ID4277";

		// Token: 0x040009DB RID: 2523
		internal const string ID4278 = "ID4278";

		// Token: 0x040009DC RID: 2524
		internal const string ID4279 = "ID4279";

		// Token: 0x040009DD RID: 2525
		internal const string ID4280 = "ID4280";

		// Token: 0x040009DE RID: 2526
		internal const string ID4283 = "ID4283";

		// Token: 0x040009DF RID: 2527
		internal const string ID4289 = "ID4289";

		// Token: 0x040009E0 RID: 2528
		internal const string ID4290 = "ID4290";

		// Token: 0x040009E1 RID: 2529
		internal const string ID4291 = "ID4291";

		// Token: 0x040009E2 RID: 2530
		internal const string ID4292 = "ID4292";

		// Token: 0x040009E3 RID: 2531
		internal const string ID4294 = "ID4294";

		// Token: 0x040009E4 RID: 2532
		internal const string ID4296 = "ID4296";

		// Token: 0x040009E5 RID: 2533
		internal const string ID6000 = "ID6000";

		// Token: 0x040009E6 RID: 2534
		internal const string ID6001 = "ID6001";

		// Token: 0x040009E7 RID: 2535
		internal const string ID6002 = "ID6002";

		// Token: 0x040009E8 RID: 2536
		internal const string ID6005 = "ID6005";

		// Token: 0x040009E9 RID: 2537
		internal const string ID6019 = "ID6019";

		// Token: 0x040009EA RID: 2538
		internal const string ID6029 = "ID6029";

		// Token: 0x040009EB RID: 2539
		internal const string ID6030 = "ID6030";

		// Token: 0x040009EC RID: 2540
		internal const string ID6031 = "ID6031";

		// Token: 0x040009ED RID: 2541
		internal const string ID6033 = "ID6033";

		// Token: 0x040009EE RID: 2542
		internal const string ID6034 = "ID6034";

		// Token: 0x040009EF RID: 2543
		internal const string ID6035 = "ID6035";

		// Token: 0x040009F0 RID: 2544
		internal const string ID6036 = "ID6036";

		// Token: 0x040009F1 RID: 2545
		internal const string ID6037 = "ID6037";

		// Token: 0x040009F2 RID: 2546
		internal const string ID6039 = "ID6039";

		// Token: 0x040009F3 RID: 2547
		internal const string ID6040 = "ID6040";

		// Token: 0x040009F4 RID: 2548
		internal const string ID6041 = "ID6041";

		// Token: 0x040009F5 RID: 2549
		internal const string ID6042 = "ID6042";

		// Token: 0x040009F6 RID: 2550
		internal const string ID6043 = "ID6043";

		// Token: 0x040009F7 RID: 2551
		internal const string ID6044 = "ID6044";

		// Token: 0x040009F8 RID: 2552
		internal const string ID6045 = "ID6045";

		// Token: 0x040009F9 RID: 2553
		internal const string ID6046 = "ID6046";

		// Token: 0x040009FA RID: 2554
		internal const string ID6047 = "ID6047";

		// Token: 0x040009FB RID: 2555
		internal const string ID6048 = "ID6048";

		// Token: 0x040009FC RID: 2556
		internal const string ID7000 = "ID7000";

		// Token: 0x040009FD RID: 2557
		internal const string ID7001 = "ID7001";

		// Token: 0x040009FE RID: 2558
		internal const string ID7002 = "ID7002";

		// Token: 0x040009FF RID: 2559
		internal const string ID7004 = "ID7004";

		// Token: 0x04000A00 RID: 2560
		internal const string ID7007 = "ID7007";

		// Token: 0x04000A01 RID: 2561
		internal const string ID7009 = "ID7009";

		// Token: 0x04000A02 RID: 2562
		internal const string ID7010 = "ID7010";

		// Token: 0x04000A03 RID: 2563
		internal const string ID7011 = "ID7011";

		// Token: 0x04000A04 RID: 2564
		internal const string ID7012 = "ID7012";

		// Token: 0x04000A05 RID: 2565
		internal const string ID7013 = "ID7013";

		// Token: 0x04000A06 RID: 2566
		internal const string ID7017 = "ID7017";

		// Token: 0x04000A07 RID: 2567
		internal const string ID7018 = "ID7018";

		// Token: 0x04000A08 RID: 2568
		internal const string ID7019 = "ID7019";

		// Token: 0x04000A09 RID: 2569
		internal const string ID7022 = "ID7022";

		// Token: 0x04000A0A RID: 2570
		internal const string ID7026 = "ID7026";

		// Token: 0x04000A0B RID: 2571
		internal const string ID7027 = "ID7027";

		// Token: 0x04000A0C RID: 2572
		internal const string ID7028 = "ID7028";

		// Token: 0x04000A0D RID: 2573
		internal const string ID7029 = "ID7029";

		// Token: 0x04000A0E RID: 2574
		internal const string ID8003 = "ID8003";

		// Token: 0x04000A0F RID: 2575
		internal const string ID8004 = "ID8004";

		// Token: 0x04000A10 RID: 2576
		internal const string ID8005 = "ID8005";

		// Token: 0x04000A11 RID: 2577
		internal const string ID8006 = "ID8006";

		// Token: 0x04000A12 RID: 2578
		internal const string ID8007 = "ID8007";

		// Token: 0x04000A13 RID: 2579
		internal const string ID8023 = "ID8023";

		// Token: 0x04000A14 RID: 2580
		internal const string ID8024 = "ID8024";

		// Token: 0x04000A15 RID: 2581
		internal const string ID8025 = "ID8025";

		// Token: 0x04000A16 RID: 2582
		internal const string ID8026 = "ID8026";

		// Token: 0x04000A17 RID: 2583
		internal const string ID8027 = "ID8027";

		// Token: 0x04000A18 RID: 2584
		internal const string ID8028 = "ID8028";

		// Token: 0x04000A19 RID: 2585
		internal const string ID8029 = "ID8029";

		// Token: 0x04000A1A RID: 2586
		internal const string ID8030 = "ID8030";

		// Token: 0x04000A1B RID: 2587
		internal const string KeyLengthMustBeMultipleOfEight = "KeyLengthMustBeMultipleOfEight";

		// Token: 0x04000A1C RID: 2588
		internal const string GivenNameText = "GivenNameText";

		// Token: 0x04000A1D RID: 2589
		internal const string SurnameText = "SurnameText";

		// Token: 0x04000A1E RID: 2590
		internal const string EmailAddressText = "EmailAddressText";

		// Token: 0x04000A1F RID: 2591
		internal const string StreetAddressText = "StreetAddressText";

		// Token: 0x04000A20 RID: 2592
		internal const string LocalityText = "LocalityText";

		// Token: 0x04000A21 RID: 2593
		internal const string StateOrProvinceText = "StateOrProvinceText";

		// Token: 0x04000A22 RID: 2594
		internal const string PostalCodeText = "PostalCodeText";

		// Token: 0x04000A23 RID: 2595
		internal const string CountryText = "CountryText";

		// Token: 0x04000A24 RID: 2596
		internal const string HomePhoneText = "HomePhoneText";

		// Token: 0x04000A25 RID: 2597
		internal const string OtherPhoneText = "OtherPhoneText";

		// Token: 0x04000A26 RID: 2598
		internal const string MobilePhoneText = "MobilePhoneText";

		// Token: 0x04000A27 RID: 2599
		internal const string DateOfBirthText = "DateOfBirthText";

		// Token: 0x04000A28 RID: 2600
		internal const string GenderText = "GenderText";

		// Token: 0x04000A29 RID: 2601
		internal const string PPIDText = "PPIDText";

		// Token: 0x04000A2A RID: 2602
		internal const string WebPageText = "WebPageText";

		// Token: 0x04000A2B RID: 2603
		internal const string NameText = "NameText";

		// Token: 0x04000A2C RID: 2604
		internal const string RoleText = "RoleText";

		// Token: 0x04000A2D RID: 2605
		internal const string GivenNameDescription = "GivenNameDescription";

		// Token: 0x04000A2E RID: 2606
		internal const string SurnameDescription = "SurnameDescription";

		// Token: 0x04000A2F RID: 2607
		internal const string EmailAddressDescription = "EmailAddressDescription";

		// Token: 0x04000A30 RID: 2608
		internal const string StreetAddressDescription = "StreetAddressDescription";

		// Token: 0x04000A31 RID: 2609
		internal const string LocalityDescription = "LocalityDescription";

		// Token: 0x04000A32 RID: 2610
		internal const string StateOrProvinceDescription = "StateOrProvinceDescription";

		// Token: 0x04000A33 RID: 2611
		internal const string PostalCodeDescription = "PostalCodeDescription";

		// Token: 0x04000A34 RID: 2612
		internal const string CountryDescription = "CountryDescription";

		// Token: 0x04000A35 RID: 2613
		internal const string HomePhoneDescription = "HomePhoneDescription";

		// Token: 0x04000A36 RID: 2614
		internal const string OtherPhoneDescription = "OtherPhoneDescription";

		// Token: 0x04000A37 RID: 2615
		internal const string MobilePhoneDescription = "MobilePhoneDescription";

		// Token: 0x04000A38 RID: 2616
		internal const string DateOfBirthDescription = "DateOfBirthDescription";

		// Token: 0x04000A39 RID: 2617
		internal const string GenderDescription = "GenderDescription";

		// Token: 0x04000A3A RID: 2618
		internal const string PPIDDescription = "PPIDDescription";

		// Token: 0x04000A3B RID: 2619
		internal const string WebPageDescription = "WebPageDescription";

		// Token: 0x04000A3C RID: 2620
		internal const string NameDescription = "NameDescription";

		// Token: 0x04000A3D RID: 2621
		internal const string RoleDescription = "RoleDescription";

		// Token: 0x04000A3E RID: 2622
		internal const string TraceCodeIdentityModel = "TraceCodeIdentityModel";

		// Token: 0x04000A3F RID: 2623
		internal const string TraceCodeDiagnostics = "TraceCodeDiagnostics";

		// Token: 0x04000A40 RID: 2624
		internal const string TraceCodeServiceBindingCheck = "TraceCodeServiceBindingCheck";

		// Token: 0x04000A41 RID: 2625
		internal const string TraceCodeChannelBindingCheck = "TraceCodeChannelBindingCheck";

		// Token: 0x04000A42 RID: 2626
		internal const string TraceSetPrincipalOnEvaluationContext = "TraceSetPrincipalOnEvaluationContext";

		// Token: 0x04000A43 RID: 2627
		internal const string TraceUnableToWriteToken = "TraceUnableToWriteToken";

		// Token: 0x04000A44 RID: 2628
		internal const string TraceValidateToken = "TraceValidateToken";

		// Token: 0x04000A45 RID: 2629
		internal const string TraceDeflateCookieEncode = "TraceDeflateCookieEncode";

		// Token: 0x04000A46 RID: 2630
		internal const string PrivateKeyNotSupported = "PrivateKeyNotSupported";

		// Token: 0x04000A47 RID: 2631
		internal const string PublicKeyNotSupported = "PublicKeyNotSupported";

		// Token: 0x04000A48 RID: 2632
		private static SR loader;

		// Token: 0x04000A49 RID: 2633
		private ResourceManager resources;
	}
}

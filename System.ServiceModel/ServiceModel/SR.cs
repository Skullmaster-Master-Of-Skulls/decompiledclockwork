using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x02000180 RID: 384
	internal sealed class SR
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x00029203 File Offset: 0x00027403
		internal SR()
		{
			this.resources = new ResourceManager("System.ServiceModel", base.GetType().Assembly);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00029228 File Offset: 0x00027428
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR value = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, value, null);
			}
			return SR.loader;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00029254 File Offset: 0x00027454
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00029257 File Offset: 0x00027457
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00029264 File Offset: 0x00027464
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

		// Token: 0x06000B3E RID: 2878 RVA: 0x000292E4 File Offset: 0x000274E4
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002930D File Offset: 0x0002750D
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00029318 File Offset: 0x00027518
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x04000C0C RID: 3084
		internal const string NoIPEndpointsFoundForHost = "NoIPEndpointsFoundForHost";

		// Token: 0x04000C0D RID: 3085
		internal const string DnsResolveFailed = "DnsResolveFailed";

		// Token: 0x04000C0E RID: 3086
		internal const string RequiredAttributeMissing = "RequiredAttributeMissing";

		// Token: 0x04000C0F RID: 3087
		internal const string UnsupportedCryptoAlgorithm = "UnsupportedCryptoAlgorithm";

		// Token: 0x04000C10 RID: 3088
		internal const string CustomCryptoAlgorithmIsNotValidHashAlgorithm = "CustomCryptoAlgorithmIsNotValidHashAlgorithm";

		// Token: 0x04000C11 RID: 3089
		internal const string InvalidClientCredentials = "InvalidClientCredentials";

		// Token: 0x04000C12 RID: 3090
		internal const string SspiErrorOrInvalidClientCredentials = "SspiErrorOrInvalidClientCredentials";

		// Token: 0x04000C13 RID: 3091
		internal const string CustomCryptoAlgorithmIsNotValidAsymmetricSignature = "CustomCryptoAlgorithmIsNotValidAsymmetricSignature";

		// Token: 0x04000C14 RID: 3092
		internal const string TokenSerializerNotSetonFederationProvider = "TokenSerializerNotSetonFederationProvider";

		// Token: 0x04000C15 RID: 3093
		internal const string IssuerBindingNotPresentInTokenRequirement = "IssuerBindingNotPresentInTokenRequirement";

		// Token: 0x04000C16 RID: 3094
		internal const string IssuerChannelBehaviorsCannotContainSecurityCredentialsManager = "IssuerChannelBehaviorsCannotContainSecurityCredentialsManager";

		// Token: 0x04000C17 RID: 3095
		internal const string ServiceBusyCountTrace = "ServiceBusyCountTrace";

		// Token: 0x04000C18 RID: 3096
		internal const string SecurityTokenManagerCannotCreateProviderForRequirement = "SecurityTokenManagerCannotCreateProviderForRequirement";

		// Token: 0x04000C19 RID: 3097
		internal const string SecurityTokenManagerCannotCreateAuthenticatorForRequirement = "SecurityTokenManagerCannotCreateAuthenticatorForRequirement";

		// Token: 0x04000C1A RID: 3098
		internal const string FailedSignatureVerification = "FailedSignatureVerification";

		// Token: 0x04000C1B RID: 3099
		internal const string SecurityTokenManagerCannotCreateSerializerForVersion = "SecurityTokenManagerCannotCreateSerializerForVersion";

		// Token: 0x04000C1C RID: 3100
		internal const string SupportingSignatureIsNotDerivedFrom = "SupportingSignatureIsNotDerivedFrom";

		// Token: 0x04000C1D RID: 3101
		internal const string PrimarySignatureWasNotSignedByDerivedKey = "PrimarySignatureWasNotSignedByDerivedKey";

		// Token: 0x04000C1E RID: 3102
		internal const string PrimarySignatureWasNotSignedByDerivedWrappedKey = "PrimarySignatureWasNotSignedByDerivedWrappedKey";

		// Token: 0x04000C1F RID: 3103
		internal const string MessageWasNotEncryptedByDerivedWrappedKey = "MessageWasNotEncryptedByDerivedWrappedKey";

		// Token: 0x04000C20 RID: 3104
		internal const string SecurityStateEncoderDecodingFailure = "SecurityStateEncoderDecodingFailure";

		// Token: 0x04000C21 RID: 3105
		internal const string SecurityStateEncoderEncodingFailure = "SecurityStateEncoderEncodingFailure";

		// Token: 0x04000C22 RID: 3106
		internal const string MessageWasNotEncryptedByDerivedEncryptionToken = "MessageWasNotEncryptedByDerivedEncryptionToken";

		// Token: 0x04000C23 RID: 3107
		internal const string TokenAuthenticatorRequiresSecurityBindingElement = "TokenAuthenticatorRequiresSecurityBindingElement";

		// Token: 0x04000C24 RID: 3108
		internal const string TokenProviderRequiresSecurityBindingElement = "TokenProviderRequiresSecurityBindingElement";

		// Token: 0x04000C25 RID: 3109
		internal const string UnexpectedSecuritySessionCloseResponse = "UnexpectedSecuritySessionCloseResponse";

		// Token: 0x04000C26 RID: 3110
		internal const string UnexpectedSecuritySessionClose = "UnexpectedSecuritySessionClose";

		// Token: 0x04000C27 RID: 3111
		internal const string CannotObtainSslConnectionInfo = "CannotObtainSslConnectionInfo";

		// Token: 0x04000C28 RID: 3112
		internal const string HeaderEncryptionNotSupportedInWsSecurityJan2004 = "HeaderEncryptionNotSupportedInWsSecurityJan2004";

		// Token: 0x04000C29 RID: 3113
		internal const string EncryptedHeaderNotSigned = "EncryptedHeaderNotSigned";

		// Token: 0x04000C2A RID: 3114
		internal const string EncodingBindingElementDoesNotHandleReaderQuotas = "EncodingBindingElementDoesNotHandleReaderQuotas";

		// Token: 0x04000C2B RID: 3115
		internal const string HeaderDecryptionNotSupportedInWsSecurityJan2004 = "HeaderDecryptionNotSupportedInWsSecurityJan2004";

		// Token: 0x04000C2C RID: 3116
		internal const string DecryptionFailed = "DecryptionFailed";

		// Token: 0x04000C2D RID: 3117
		internal const string AuthenticationManagerShouldNotReturnNull = "AuthenticationManagerShouldNotReturnNull";

		// Token: 0x04000C2E RID: 3118
		internal const string ErrorSerializingSecurityToken = "ErrorSerializingSecurityToken";

		// Token: 0x04000C2F RID: 3119
		internal const string ErrorDeserializingKeyIdentifierClauseFromTokenXml = "ErrorDeserializingKeyIdentifierClauseFromTokenXml";

		// Token: 0x04000C30 RID: 3120
		internal const string ErrorDeserializingTokenXml = "ErrorDeserializingTokenXml";

		// Token: 0x04000C31 RID: 3121
		internal const string TokenRequirementDoesNotSpecifyTargetAddress = "TokenRequirementDoesNotSpecifyTargetAddress";

		// Token: 0x04000C32 RID: 3122
		internal const string DerivedKeyNotInitialized = "DerivedKeyNotInitialized";

		// Token: 0x04000C33 RID: 3123
		internal const string IssuedKeySizeNotCompatibleWithAlgorithmSuite = "IssuedKeySizeNotCompatibleWithAlgorithmSuite";

		// Token: 0x04000C34 RID: 3124
		internal const string IssuedTokenAuthenticationModeRequiresSymmetricIssuedKey = "IssuedTokenAuthenticationModeRequiresSymmetricIssuedKey";

		// Token: 0x04000C35 RID: 3125
		internal const string InvalidBearerKeyUsage = "InvalidBearerKeyUsage";

		// Token: 0x04000C36 RID: 3126
		internal const string MultipleIssuerEndpointsFound = "MultipleIssuerEndpointsFound";

		// Token: 0x04000C37 RID: 3127
		internal const string MultipleAuthenticationManagersInServiceBindingParameters = "MultipleAuthenticationManagersInServiceBindingParameters";

		// Token: 0x04000C38 RID: 3128
		internal const string MultipleAuthenticationSchemesInServiceBindingParameters = "MultipleAuthenticationSchemesInServiceBindingParameters";

		// Token: 0x04000C39 RID: 3129
		internal const string NoSecurityBindingElementFound = "NoSecurityBindingElementFound";

		// Token: 0x04000C3A RID: 3130
		internal const string MultipleSecurityCredentialsManagersInServiceBindingParameters = "MultipleSecurityCredentialsManagersInServiceBindingParameters";

		// Token: 0x04000C3B RID: 3131
		internal const string MultipleSecurityCredentialsManagersInChannelBindingParameters = "MultipleSecurityCredentialsManagersInChannelBindingParameters";

		// Token: 0x04000C3C RID: 3132
		internal const string NoClientCertificate = "NoClientCertificate";

		// Token: 0x04000C3D RID: 3133
		internal const string SecurityTokenParametersHasIncompatibleInclusionMode = "SecurityTokenParametersHasIncompatibleInclusionMode";

		// Token: 0x04000C3E RID: 3134
		internal const string CannotCreateTwoWayListenerForNegotiation = "CannotCreateTwoWayListenerForNegotiation";

		// Token: 0x04000C3F RID: 3135
		internal const string NegotiationQuotasExceededFaultReason = "NegotiationQuotasExceededFaultReason";

		// Token: 0x04000C40 RID: 3136
		internal const string PendingSessionsExceededFaultReason = "PendingSessionsExceededFaultReason";

		// Token: 0x04000C41 RID: 3137
		internal const string RequestSecurityTokenDoesNotMatchEndpointFilters = "RequestSecurityTokenDoesNotMatchEndpointFilters";

		// Token: 0x04000C42 RID: 3138
		internal const string SecuritySessionRequiresIssuanceAuthenticator = "SecuritySessionRequiresIssuanceAuthenticator";

		// Token: 0x04000C43 RID: 3139
		internal const string SecuritySessionRequiresSecurityContextTokenCache = "SecuritySessionRequiresSecurityContextTokenCache";

		// Token: 0x04000C44 RID: 3140
		internal const string SessionTokenIsNotSecurityContextToken = "SessionTokenIsNotSecurityContextToken";

		// Token: 0x04000C45 RID: 3141
		internal const string SessionTokenIsNotGenericXmlToken = "SessionTokenIsNotGenericXmlToken";

		// Token: 0x04000C46 RID: 3142
		internal const string SecurityStandardsManagerNotSet = "SecurityStandardsManagerNotSet";

		// Token: 0x04000C47 RID: 3143
		internal const string SecurityNegotiationMessageTooLarge = "SecurityNegotiationMessageTooLarge";

		// Token: 0x04000C48 RID: 3144
		internal const string PreviousChannelDemuxerOpenFailed = "PreviousChannelDemuxerOpenFailed";

		// Token: 0x04000C49 RID: 3145
		internal const string SecurityChannelListenerNotSet = "SecurityChannelListenerNotSet";

		// Token: 0x04000C4A RID: 3146
		internal const string SecurityChannelListenerChannelExtendedProtectionNotSupported = "SecurityChannelListenerChannelExtendedProtectionNotSupported";

		// Token: 0x04000C4B RID: 3147
		internal const string SecurityChannelBindingMissing = "SecurityChannelBindingMissing";

		// Token: 0x04000C4C RID: 3148
		internal const string SecuritySettingsLifetimeManagerNotSet = "SecuritySettingsLifetimeManagerNotSet";

		// Token: 0x04000C4D RID: 3149
		internal const string SecurityListenerClosing = "SecurityListenerClosing";

		// Token: 0x04000C4E RID: 3150
		internal const string SecurityListenerClosingFaultReason = "SecurityListenerClosingFaultReason";

		// Token: 0x04000C4F RID: 3151
		internal const string SslCipherKeyTooSmall = "SslCipherKeyTooSmall";

		// Token: 0x04000C50 RID: 3152
		internal const string DerivedKeyTokenNonceTooLong = "DerivedKeyTokenNonceTooLong";

		// Token: 0x04000C51 RID: 3153
		internal const string DerivedKeyTokenLabelTooLong = "DerivedKeyTokenLabelTooLong";

		// Token: 0x04000C52 RID: 3154
		internal const string DerivedKeyTokenOffsetTooHigh = "DerivedKeyTokenOffsetTooHigh";

		// Token: 0x04000C53 RID: 3155
		internal const string DerivedKeyTokenGenerationAndLengthTooHigh = "DerivedKeyTokenGenerationAndLengthTooHigh";

		// Token: 0x04000C54 RID: 3156
		internal const string DerivedKeyLimitExceeded = "DerivedKeyLimitExceeded";

		// Token: 0x04000C55 RID: 3157
		internal const string WrappedKeyLimitExceeded = "WrappedKeyLimitExceeded";

		// Token: 0x04000C56 RID: 3158
		internal const string BufferQuotaExceededReadingBase64 = "BufferQuotaExceededReadingBase64";

		// Token: 0x04000C57 RID: 3159
		internal const string MessageSecurityDoesNotWorkWithManualAddressing = "MessageSecurityDoesNotWorkWithManualAddressing";

		// Token: 0x04000C58 RID: 3160
		internal const string TargetAddressIsNotSet = "TargetAddressIsNotSet";

		// Token: 0x04000C59 RID: 3161
		internal const string IssuedTokenCacheNotSet = "IssuedTokenCacheNotSet";

		// Token: 0x04000C5A RID: 3162
		internal const string SecurityAlgorithmSuiteNotSet = "SecurityAlgorithmSuiteNotSet";

		// Token: 0x04000C5B RID: 3163
		internal const string SecurityTokenFoundOutsideSecurityHeader = "SecurityTokenFoundOutsideSecurityHeader";

		// Token: 0x04000C5C RID: 3164
		internal const string SecurityTokenNotResolved = "SecurityTokenNotResolved";

		// Token: 0x04000C5D RID: 3165
		internal const string SecureConversationCancelNotAllowedFaultReason = "SecureConversationCancelNotAllowedFaultReason";

		// Token: 0x04000C5E RID: 3166
		internal const string BootstrapSecurityBindingElementNotSet = "BootstrapSecurityBindingElementNotSet";

		// Token: 0x04000C5F RID: 3167
		internal const string IssuerBuildContextNotSet = "IssuerBuildContextNotSet";

		// Token: 0x04000C60 RID: 3168
		internal const string StsBindingNotSet = "StsBindingNotSet";

		// Token: 0x04000C61 RID: 3169
		internal const string SslCertMayNotDoKeyExchange = "SslCertMayNotDoKeyExchange";

		// Token: 0x04000C62 RID: 3170
		internal const string SslCertMustHavePrivateKey = "SslCertMustHavePrivateKey";

		// Token: 0x04000C63 RID: 3171
		internal const string NoOutgoingEndpointAddressAvailableForDoingIdentityCheck = "NoOutgoingEndpointAddressAvailableForDoingIdentityCheck";

		// Token: 0x04000C64 RID: 3172
		internal const string NoOutgoingEndpointAddressAvailableForDoingIdentityCheckOnReply = "NoOutgoingEndpointAddressAvailableForDoingIdentityCheckOnReply";

		// Token: 0x04000C65 RID: 3173
		internal const string NoSigningTokenAvailableToDoIncomingIdentityCheck = "NoSigningTokenAvailableToDoIncomingIdentityCheck";

		// Token: 0x04000C66 RID: 3174
		internal const string Psha1KeyLengthInvalid = "Psha1KeyLengthInvalid";

		// Token: 0x04000C67 RID: 3175
		internal const string CloneNotImplementedCorrectly = "CloneNotImplementedCorrectly";

		// Token: 0x04000C68 RID: 3176
		internal const string BadIssuedTokenType = "BadIssuedTokenType";

		// Token: 0x04000C69 RID: 3177
		internal const string OperationDoesNotAllowImpersonation = "OperationDoesNotAllowImpersonation";

		// Token: 0x04000C6A RID: 3178
		internal const string RstrHasMultipleIssuedTokens = "RstrHasMultipleIssuedTokens";

		// Token: 0x04000C6B RID: 3179
		internal const string RstrHasMultipleProofTokens = "RstrHasMultipleProofTokens";

		// Token: 0x04000C6C RID: 3180
		internal const string ProofTokenXmlUnexpectedInRstr = "ProofTokenXmlUnexpectedInRstr";

		// Token: 0x04000C6D RID: 3181
		internal const string InvalidKeyLengthRequested = "InvalidKeyLengthRequested";

		// Token: 0x04000C6E RID: 3182
		internal const string IssuedSecurityTokenParametersNotSet = "IssuedSecurityTokenParametersNotSet";

		// Token: 0x04000C6F RID: 3183
		internal const string InvalidOrUnrecognizedAction = "InvalidOrUnrecognizedAction";

		// Token: 0x04000C70 RID: 3184
		internal const string UnsupportedTokenInclusionMode = "UnsupportedTokenInclusionMode";

		// Token: 0x04000C71 RID: 3185
		internal const string CannotImportProtectionLevelForContract = "CannotImportProtectionLevelForContract";

		// Token: 0x04000C72 RID: 3186
		internal const string OnlyOneOfEncryptedKeyOrSymmetricBindingCanBeSelected = "OnlyOneOfEncryptedKeyOrSymmetricBindingCanBeSelected";

		// Token: 0x04000C73 RID: 3187
		internal const string ClientCredentialTypeMustBeSpecifiedForMixedMode = "ClientCredentialTypeMustBeSpecifiedForMixedMode";

		// Token: 0x04000C74 RID: 3188
		internal const string SecuritySessionIdAlreadyPresentInFilterTable = "SecuritySessionIdAlreadyPresentInFilterTable";

		// Token: 0x04000C75 RID: 3189
		internal const string SupportingTokenNotProvided = "SupportingTokenNotProvided";

		// Token: 0x04000C76 RID: 3190
		internal const string SupportingTokenIsNotEndorsing = "SupportingTokenIsNotEndorsing";

		// Token: 0x04000C77 RID: 3191
		internal const string SupportingTokenIsNotSigned = "SupportingTokenIsNotSigned";

		// Token: 0x04000C78 RID: 3192
		internal const string SupportingTokenIsNotEncrypted = "SupportingTokenIsNotEncrypted";

		// Token: 0x04000C79 RID: 3193
		internal const string BasicTokenNotExpected = "BasicTokenNotExpected";

		// Token: 0x04000C7A RID: 3194
		internal const string FailedAuthenticationTrustFaultCode = "FailedAuthenticationTrustFaultCode";

		// Token: 0x04000C7B RID: 3195
		internal const string AuthenticationOfClientFailed = "AuthenticationOfClientFailed";

		// Token: 0x04000C7C RID: 3196
		internal const string InvalidRequestTrustFaultCode = "InvalidRequestTrustFaultCode";

		// Token: 0x04000C7D RID: 3197
		internal const string SignedSupportingTokenNotExpected = "SignedSupportingTokenNotExpected";

		// Token: 0x04000C7E RID: 3198
		internal const string SenderSideSupportingTokensMustSpecifySecurityTokenParameters = "SenderSideSupportingTokensMustSpecifySecurityTokenParameters";

		// Token: 0x04000C7F RID: 3199
		internal const string SignatureAndEncryptionTokenMismatch = "SignatureAndEncryptionTokenMismatch";

		// Token: 0x04000C80 RID: 3200
		internal const string RevertingPrivilegeFailed = "RevertingPrivilegeFailed";

		// Token: 0x04000C81 RID: 3201
		internal const string UnknownSupportingToken = "UnknownSupportingToken";

		// Token: 0x04000C82 RID: 3202
		internal const string MoreThanOneSupportingSignature = "MoreThanOneSupportingSignature";

		// Token: 0x04000C83 RID: 3203
		internal const string UnsecuredMessageFaultReceived = "UnsecuredMessageFaultReceived";

		// Token: 0x04000C84 RID: 3204
		internal const string FailedAuthenticationFaultReason = "FailedAuthenticationFaultReason";

		// Token: 0x04000C85 RID: 3205
		internal const string BadContextTokenOrActionFaultReason = "BadContextTokenOrActionFaultReason";

		// Token: 0x04000C86 RID: 3206
		internal const string BadContextTokenFaultReason = "BadContextTokenFaultReason";

		// Token: 0x04000C87 RID: 3207
		internal const string NegotiationFailedIO = "NegotiationFailedIO";

		// Token: 0x04000C88 RID: 3208
		internal const string SecurityNegotiationCannotProtectConfidentialEndpointHeader = "SecurityNegotiationCannotProtectConfidentialEndpointHeader";

		// Token: 0x04000C89 RID: 3209
		internal const string InvalidSecurityTokenFaultReason = "InvalidSecurityTokenFaultReason";

		// Token: 0x04000C8A RID: 3210
		internal const string InvalidSecurityFaultReason = "InvalidSecurityFaultReason";

		// Token: 0x04000C8B RID: 3211
		internal const string AnonymousLogonsAreNotAllowed = "AnonymousLogonsAreNotAllowed";

		// Token: 0x04000C8C RID: 3212
		internal const string UnableToObtainIssuerMetadata = "UnableToObtainIssuerMetadata";

		// Token: 0x04000C8D RID: 3213
		internal const string ErrorImportingIssuerMetadata = "ErrorImportingIssuerMetadata";

		// Token: 0x04000C8E RID: 3214
		internal const string MultipleCorrelationTokensFound = "MultipleCorrelationTokensFound";

		// Token: 0x04000C8F RID: 3215
		internal const string NoCorrelationTokenFound = "NoCorrelationTokenFound";

		// Token: 0x04000C90 RID: 3216
		internal const string MultipleSupportingAuthenticatorsOfSameType = "MultipleSupportingAuthenticatorsOfSameType";

		// Token: 0x04000C91 RID: 3217
		internal const string TooManyIssuedSecurityTokenParameters = "TooManyIssuedSecurityTokenParameters";

		// Token: 0x04000C92 RID: 3218
		internal const string UnknownTokenAuthenticatorUsedInTokenProcessing = "UnknownTokenAuthenticatorUsedInTokenProcessing";

		// Token: 0x04000C93 RID: 3219
		internal const string TokenMustBeNullWhenTokenParametersAre = "TokenMustBeNullWhenTokenParametersAre";

		// Token: 0x04000C94 RID: 3220
		internal const string SecurityTokenParametersCloneInvalidResult = "SecurityTokenParametersCloneInvalidResult";

		// Token: 0x04000C95 RID: 3221
		internal const string CertificateUnsupportedForHttpTransportCredentialOnly = "CertificateUnsupportedForHttpTransportCredentialOnly";

		// Token: 0x04000C96 RID: 3222
		internal const string BasicHttpMessageSecurityRequiresCertificate = "BasicHttpMessageSecurityRequiresCertificate";

		// Token: 0x04000C97 RID: 3223
		internal const string EntropyModeRequiresRequestorEntropy = "EntropyModeRequiresRequestorEntropy";

		// Token: 0x04000C98 RID: 3224
		internal const string BearerKeyTypeCannotHaveProofKey = "BearerKeyTypeCannotHaveProofKey";

		// Token: 0x04000C99 RID: 3225
		internal const string BearerKeyIncompatibleWithWSFederationHttpBinding = "BearerKeyIncompatibleWithWSFederationHttpBinding";

		// Token: 0x04000C9A RID: 3226
		internal const string UnableToCreateKeyTypeElementForUnknownKeyType = "UnableToCreateKeyTypeElementForUnknownKeyType";

		// Token: 0x04000C9B RID: 3227
		internal const string EntropyModeCannotHaveProofTokenOrIssuerEntropy = "EntropyModeCannotHaveProofTokenOrIssuerEntropy";

		// Token: 0x04000C9C RID: 3228
		internal const string EntropyModeCannotHaveRequestorEntropy = "EntropyModeCannotHaveRequestorEntropy";

		// Token: 0x04000C9D RID: 3229
		internal const string EntropyModeRequiresProofToken = "EntropyModeRequiresProofToken";

		// Token: 0x04000C9E RID: 3230
		internal const string EntropyModeRequiresComputedKey = "EntropyModeRequiresComputedKey";

		// Token: 0x04000C9F RID: 3231
		internal const string EntropyModeRequiresIssuerEntropy = "EntropyModeRequiresIssuerEntropy";

		// Token: 0x04000CA0 RID: 3232
		internal const string EntropyModeCannotHaveComputedKey = "EntropyModeCannotHaveComputedKey";

		// Token: 0x04000CA1 RID: 3233
		internal const string UnknownComputedKeyAlgorithm = "UnknownComputedKeyAlgorithm";

		// Token: 0x04000CA2 RID: 3234
		internal const string NoncesCachedInfinitely = "NoncesCachedInfinitely";

		// Token: 0x04000CA3 RID: 3235
		internal const string ChannelMustBeOpenedToGetSessionId = "ChannelMustBeOpenedToGetSessionId";

		// Token: 0x04000CA4 RID: 3236
		internal const string SecurityVersionDoesNotSupportEncryptedKeyBinding = "SecurityVersionDoesNotSupportEncryptedKeyBinding";

		// Token: 0x04000CA5 RID: 3237
		internal const string SecurityVersionDoesNotSupportThumbprintX509KeyIdentifierClause = "SecurityVersionDoesNotSupportThumbprintX509KeyIdentifierClause";

		// Token: 0x04000CA6 RID: 3238
		internal const string SecurityBindingSupportsOneWayOnly = "SecurityBindingSupportsOneWayOnly";

		// Token: 0x04000CA7 RID: 3239
		internal const string DownlevelNameCannotMapToUpn = "DownlevelNameCannotMapToUpn";

		// Token: 0x04000CA8 RID: 3240
		internal const string ResolvingExternalTokensRequireSecurityTokenParameters = "ResolvingExternalTokensRequireSecurityTokenParameters";

		// Token: 0x04000CA9 RID: 3241
		internal const string SecurityRenewFaultReason = "SecurityRenewFaultReason";

		// Token: 0x04000CAA RID: 3242
		internal const string ClientSecurityOutputSessionCloseTimeout = "ClientSecurityOutputSessionCloseTimeout";

		// Token: 0x04000CAB RID: 3243
		internal const string ClientSecurityNegotiationTimeout = "ClientSecurityNegotiationTimeout";

		// Token: 0x04000CAC RID: 3244
		internal const string ClientSecuritySessionRequestTimeout = "ClientSecuritySessionRequestTimeout";

		// Token: 0x04000CAD RID: 3245
		internal const string ServiceSecurityCloseOutputSessionTimeout = "ServiceSecurityCloseOutputSessionTimeout";

		// Token: 0x04000CAE RID: 3246
		internal const string ServiceSecurityCloseTimeout = "ServiceSecurityCloseTimeout";

		// Token: 0x04000CAF RID: 3247
		internal const string ClientSecurityCloseTimeout = "ClientSecurityCloseTimeout";

		// Token: 0x04000CB0 RID: 3248
		internal const string UnableToRenewSessionKey = "UnableToRenewSessionKey";

		// Token: 0x04000CB1 RID: 3249
		internal const string SessionKeyRenewalNotSupported = "SessionKeyRenewalNotSupported";

		// Token: 0x04000CB2 RID: 3250
		internal const string SctCookieXmlParseError = "SctCookieXmlParseError";

		// Token: 0x04000CB3 RID: 3251
		internal const string SctCookieValueMissingOrIncorrect = "SctCookieValueMissingOrIncorrect";

		// Token: 0x04000CB4 RID: 3252
		internal const string SctCookieBlobDecodeFailure = "SctCookieBlobDecodeFailure";

		// Token: 0x04000CB5 RID: 3253
		internal const string SctCookieNotSupported = "SctCookieNotSupported";

		// Token: 0x04000CB6 RID: 3254
		internal const string CannotImportSupportingTokensForOperationWithoutRequestAction = "CannotImportSupportingTokensForOperationWithoutRequestAction";

		// Token: 0x04000CB7 RID: 3255
		internal const string SignatureConfirmationsNotExpected = "SignatureConfirmationsNotExpected";

		// Token: 0x04000CB8 RID: 3256
		internal const string SignatureConfirmationsOccursAfterPrimarySignature = "SignatureConfirmationsOccursAfterPrimarySignature";

		// Token: 0x04000CB9 RID: 3257
		internal const string SignatureConfirmationWasExpected = "SignatureConfirmationWasExpected";

		// Token: 0x04000CBA RID: 3258
		internal const string SecurityVersionDoesNotSupportSignatureConfirmation = "SecurityVersionDoesNotSupportSignatureConfirmation";

		// Token: 0x04000CBB RID: 3259
		internal const string SignatureConfirmationRequiresRequestReply = "SignatureConfirmationRequiresRequestReply";

		// Token: 0x04000CBC RID: 3260
		internal const string NotAllSignaturesConfirmed = "NotAllSignaturesConfirmed";

		// Token: 0x04000CBD RID: 3261
		internal const string FoundUnexpectedSignatureConfirmations = "FoundUnexpectedSignatureConfirmations";

		// Token: 0x04000CBE RID: 3262
		internal const string TooManyPendingSessionKeys = "TooManyPendingSessionKeys";

		// Token: 0x04000CBF RID: 3263
		internal const string SecuritySessionKeyIsStale = "SecuritySessionKeyIsStale";

		// Token: 0x04000CC0 RID: 3264
		internal const string MultipleMatchingCryptosFound = "MultipleMatchingCryptosFound";

		// Token: 0x04000CC1 RID: 3265
		internal const string CannotFindMatchingCrypto = "CannotFindMatchingCrypto";

		// Token: 0x04000CC2 RID: 3266
		internal const string SymmetricSecurityBindingElementNeedsProtectionTokenParameters = "SymmetricSecurityBindingElementNeedsProtectionTokenParameters";

		// Token: 0x04000CC3 RID: 3267
		internal const string AsymmetricSecurityBindingElementNeedsInitiatorTokenParameters = "AsymmetricSecurityBindingElementNeedsInitiatorTokenParameters";

		// Token: 0x04000CC4 RID: 3268
		internal const string AsymmetricSecurityBindingElementNeedsRecipientTokenParameters = "AsymmetricSecurityBindingElementNeedsRecipientTokenParameters";

		// Token: 0x04000CC5 RID: 3269
		internal const string CachedNegotiationStateQuotaReached = "CachedNegotiationStateQuotaReached";

		// Token: 0x04000CC6 RID: 3270
		internal const string LsaAuthorityNotContacted = "LsaAuthorityNotContacted";

		// Token: 0x04000CC7 RID: 3271
		internal const string KeyRolloverGreaterThanKeyRenewal = "KeyRolloverGreaterThanKeyRenewal";

		// Token: 0x04000CC8 RID: 3272
		internal const string AtLeastOneContractOperationRequestRequiresProtectionLevelNotSupportedByBinding = "AtLeastOneContractOperationRequestRequiresProtectionLevelNotSupportedByBinding";

		// Token: 0x04000CC9 RID: 3273
		internal const string AtLeastOneContractOperationResponseRequiresProtectionLevelNotSupportedByBinding = "AtLeastOneContractOperationResponseRequiresProtectionLevelNotSupportedByBinding";

		// Token: 0x04000CCA RID: 3274
		internal const string UnknownHeaderCannotProtected = "UnknownHeaderCannotProtected";

		// Token: 0x04000CCB RID: 3275
		internal const string NoStreamingWithSecurity = "NoStreamingWithSecurity";

		// Token: 0x04000CCC RID: 3276
		internal const string CurrentSessionTokenNotRenewed = "CurrentSessionTokenNotRenewed";

		// Token: 0x04000CCD RID: 3277
		internal const string IncorrectSpnOrUpnSpecified = "IncorrectSpnOrUpnSpecified";

		// Token: 0x04000CCE RID: 3278
		internal const string IncomingSigningTokenMustBeAnEncryptedKey = "IncomingSigningTokenMustBeAnEncryptedKey";

		// Token: 0x04000CCF RID: 3279
		internal const string SecuritySessionAbortedFaultReason = "SecuritySessionAbortedFaultReason";

		// Token: 0x04000CD0 RID: 3280
		internal const string NoAppliesToPresent = "NoAppliesToPresent";

		// Token: 0x04000CD1 RID: 3281
		internal const string UnsupportedKeyLength = "UnsupportedKeyLength";

		// Token: 0x04000CD2 RID: 3282
		internal const string ForReplayDetectionToBeDoneRequireIntegrityMustBeSet = "ForReplayDetectionToBeDoneRequireIntegrityMustBeSet";

		// Token: 0x04000CD3 RID: 3283
		internal const string CantInferReferenceForToken = "CantInferReferenceForToken";

		// Token: 0x04000CD4 RID: 3284
		internal const string TrustDriverIsUnableToCreatedNecessaryAttachedOrUnattachedReferences = "TrustDriverIsUnableToCreatedNecessaryAttachedOrUnattachedReferences";

		// Token: 0x04000CD5 RID: 3285
		internal const string TrustDriverVersionDoesNotSupportSession = "TrustDriverVersionDoesNotSupportSession";

		// Token: 0x04000CD6 RID: 3286
		internal const string TrustDriverVersionDoesNotSupportIssuedTokens = "TrustDriverVersionDoesNotSupportIssuedTokens";

		// Token: 0x04000CD7 RID: 3287
		internal const string CannotPerformS4UImpersonationOnPlatform = "CannotPerformS4UImpersonationOnPlatform";

		// Token: 0x04000CD8 RID: 3288
		internal const string CannotPerformImpersonationOnUsernameToken = "CannotPerformImpersonationOnUsernameToken";

		// Token: 0x04000CD9 RID: 3289
		internal const string SecureConversationRequiredByReliableSession = "SecureConversationRequiredByReliableSession";

		// Token: 0x04000CDA RID: 3290
		internal const string RevertImpersonationFailure = "RevertImpersonationFailure";

		// Token: 0x04000CDB RID: 3291
		internal const string TransactionFlowRequiredIssuedTokens = "TransactionFlowRequiredIssuedTokens";

		// Token: 0x04000CDC RID: 3292
		internal const string SignatureConfirmationNotSupported = "SignatureConfirmationNotSupported";

		// Token: 0x04000CDD RID: 3293
		internal const string SecureConversationDriverVersionDoesNotSupportSession = "SecureConversationDriverVersionDoesNotSupportSession";

		// Token: 0x04000CDE RID: 3294
		internal const string SoapSecurityNegotiationFailed = "SoapSecurityNegotiationFailed";

		// Token: 0x04000CDF RID: 3295
		internal const string SoapSecurityNegotiationFailedForIssuerAndTarget = "SoapSecurityNegotiationFailedForIssuerAndTarget";

		// Token: 0x04000CE0 RID: 3296
		internal const string OneWayOperationReturnedFault = "OneWayOperationReturnedFault";

		// Token: 0x04000CE1 RID: 3297
		internal const string OneWayOperationReturnedLargeFault = "OneWayOperationReturnedLargeFault";

		// Token: 0x04000CE2 RID: 3298
		internal const string OneWayOperationReturnedMessage = "OneWayOperationReturnedMessage";

		// Token: 0x04000CE3 RID: 3299
		internal const string CannotFindSecuritySession = "CannotFindSecuritySession";

		// Token: 0x04000CE4 RID: 3300
		internal const string SecurityContextKeyExpired = "SecurityContextKeyExpired";

		// Token: 0x04000CE5 RID: 3301
		internal const string SecurityContextKeyExpiredNoKeyGeneration = "SecurityContextKeyExpiredNoKeyGeneration";

		// Token: 0x04000CE6 RID: 3302
		internal const string SecuritySessionRequiresMessageIntegrity = "SecuritySessionRequiresMessageIntegrity";

		// Token: 0x04000CE7 RID: 3303
		internal const string RequiredTimestampMissingInSecurityHeader = "RequiredTimestampMissingInSecurityHeader";

		// Token: 0x04000CE8 RID: 3304
		internal const string ReceivedMessageInRequestContextNull = "ReceivedMessageInRequestContextNull";

		// Token: 0x04000CE9 RID: 3305
		internal const string KeyLifetimeNotWithinTokenLifetime = "KeyLifetimeNotWithinTokenLifetime";

		// Token: 0x04000CEA RID: 3306
		internal const string EffectiveGreaterThanExpiration = "EffectiveGreaterThanExpiration";

		// Token: 0x04000CEB RID: 3307
		internal const string NoSessionTokenPresentInMessage = "NoSessionTokenPresentInMessage";

		// Token: 0x04000CEC RID: 3308
		internal const string KeyLengthMustBeMultipleOfEight = "KeyLengthMustBeMultipleOfEight";

		// Token: 0x04000CED RID: 3309
		internal const string InvalidX509RawData = "InvalidX509RawData";

		// Token: 0x04000CEE RID: 3310
		internal const string ExportOfBindingWithTransportSecurityBindingElementAndNoTransportSecurityNotSupported = "ExportOfBindingWithTransportSecurityBindingElementAndNoTransportSecurityNotSupported";

		// Token: 0x04000CEF RID: 3311
		internal const string UnsupportedSecureConversationBootstrapProtectionRequirements = "UnsupportedSecureConversationBootstrapProtectionRequirements";

		// Token: 0x04000CF0 RID: 3312
		internal const string UnsupportedBooleanAttribute = "UnsupportedBooleanAttribute";

		// Token: 0x04000CF1 RID: 3313
		internal const string NoTransportTokenAssertionProvided = "NoTransportTokenAssertionProvided";

		// Token: 0x04000CF2 RID: 3314
		internal const string PolicyRequiresConfidentialityWithoutIntegrity = "PolicyRequiresConfidentialityWithoutIntegrity";

		// Token: 0x04000CF3 RID: 3315
		internal const string PrimarySignatureIsRequiredToBeEncrypted = "PrimarySignatureIsRequiredToBeEncrypted";

		// Token: 0x04000CF4 RID: 3316
		internal const string TokenCannotCreateSymmetricCrypto = "TokenCannotCreateSymmetricCrypto";

		// Token: 0x04000CF5 RID: 3317
		internal const string TokenDoesNotMeetKeySizeRequirements = "TokenDoesNotMeetKeySizeRequirements";

		// Token: 0x04000CF6 RID: 3318
		internal const string MessageProtectionOrderMismatch = "MessageProtectionOrderMismatch";

		// Token: 0x04000CF7 RID: 3319
		internal const string PrimarySignatureMustBeComputedBeforeSupportingTokenSignatures = "PrimarySignatureMustBeComputedBeforeSupportingTokenSignatures";

		// Token: 0x04000CF8 RID: 3320
		internal const string ElementToSignMustHaveId = "ElementToSignMustHaveId";

		// Token: 0x04000CF9 RID: 3321
		internal const string StandardsManagerCannotWriteObject = "StandardsManagerCannotWriteObject";

		// Token: 0x04000CFA RID: 3322
		internal const string SigningWithoutPrimarySignatureRequiresTimestamp = "SigningWithoutPrimarySignatureRequiresTimestamp";

		// Token: 0x04000CFB RID: 3323
		internal const string OperationCannotBeDoneAfterProcessingIsStarted = "OperationCannotBeDoneAfterProcessingIsStarted";

		// Token: 0x04000CFC RID: 3324
		internal const string MaximumPolicyRedirectionsExceeded = "MaximumPolicyRedirectionsExceeded";

		// Token: 0x04000CFD RID: 3325
		internal const string InvalidAttributeInSignedHeader = "InvalidAttributeInSignedHeader";

		// Token: 0x04000CFE RID: 3326
		internal const string StsAddressNotSet = "StsAddressNotSet";

		// Token: 0x04000CFF RID: 3327
		internal const string MoreThanOneSecurityBindingElementInTheBinding = "MoreThanOneSecurityBindingElementInTheBinding";

		// Token: 0x04000D00 RID: 3328
		internal const string ClientCredentialsUnableToCreateLocalTokenProvider = "ClientCredentialsUnableToCreateLocalTokenProvider";

		// Token: 0x04000D01 RID: 3329
		internal const string SecurityBindingElementCannotBeExpressedInConfig = "SecurityBindingElementCannotBeExpressedInConfig";

		// Token: 0x04000D02 RID: 3330
		internal const string ConfigurationSchemaInsuffientForSecurityBindingElementInstance = "ConfigurationSchemaInsuffientForSecurityBindingElementInstance";

		// Token: 0x04000D03 RID: 3331
		internal const string ConfigurationSchemaContainsX509IssuerSerialReference = "ConfigurationSchemaContainsX509IssuerSerialReference";

		// Token: 0x04000D04 RID: 3332
		internal const string SecurityProtocolCannotDoReplayDetection = "SecurityProtocolCannotDoReplayDetection";

		// Token: 0x04000D05 RID: 3333
		internal const string UnableToFindSecurityHeaderInMessage = "UnableToFindSecurityHeaderInMessage";

		// Token: 0x04000D06 RID: 3334
		internal const string UnableToFindSecurityHeaderInMessageNoActor = "UnableToFindSecurityHeaderInMessageNoActor";

		// Token: 0x04000D07 RID: 3335
		internal const string NoPrimarySignatureAvailableForSupportingTokenSignatureVerification = "NoPrimarySignatureAvailableForSupportingTokenSignatureVerification";

		// Token: 0x04000D08 RID: 3336
		internal const string SupportingTokenSignaturesNotExpected = "SupportingTokenSignaturesNotExpected";

		// Token: 0x04000D09 RID: 3337
		internal const string CannotReadToken = "CannotReadToken";

		// Token: 0x04000D0A RID: 3338
		internal const string ExpectedElementMissing = "ExpectedElementMissing";

		// Token: 0x04000D0B RID: 3339
		internal const string ExpectedOneOfTwoElementsFromNamespace = "ExpectedOneOfTwoElementsFromNamespace";

		// Token: 0x04000D0C RID: 3340
		internal const string RstDirectDoesNotExpectRstr = "RstDirectDoesNotExpectRstr";

		// Token: 0x04000D0D RID: 3341
		internal const string RequireNonCookieMode = "RequireNonCookieMode";

		// Token: 0x04000D0E RID: 3342
		internal const string RequiredSignatureMissing = "RequiredSignatureMissing";

		// Token: 0x04000D0F RID: 3343
		internal const string RequiredMessagePartNotSigned = "RequiredMessagePartNotSigned";

		// Token: 0x04000D10 RID: 3344
		internal const string RequiredMessagePartNotSignedNs = "RequiredMessagePartNotSignedNs";

		// Token: 0x04000D11 RID: 3345
		internal const string RequiredMessagePartNotEncrypted = "RequiredMessagePartNotEncrypted";

		// Token: 0x04000D12 RID: 3346
		internal const string RequiredMessagePartNotEncryptedNs = "RequiredMessagePartNotEncryptedNs";

		// Token: 0x04000D13 RID: 3347
		internal const string SignatureVerificationFailed = "SignatureVerificationFailed";

		// Token: 0x04000D14 RID: 3348
		internal const string CannotIssueRstTokenType = "CannotIssueRstTokenType";

		// Token: 0x04000D15 RID: 3349
		internal const string NoNegotiationMessageToSend = "NoNegotiationMessageToSend";

		// Token: 0x04000D16 RID: 3350
		internal const string InvalidIssuedTokenKeySize = "InvalidIssuedTokenKeySize";

		// Token: 0x04000D17 RID: 3351
		internal const string CannotObtainIssuedTokenKeySize = "CannotObtainIssuedTokenKeySize";

		// Token: 0x04000D18 RID: 3352
		internal const string NegotiationIsNotCompleted = "NegotiationIsNotCompleted";

		// Token: 0x04000D19 RID: 3353
		internal const string NegotiationIsCompleted = "NegotiationIsCompleted";

		// Token: 0x04000D1A RID: 3354
		internal const string MissingMessageID = "MissingMessageID";

		// Token: 0x04000D1B RID: 3355
		internal const string SecuritySessionLimitReached = "SecuritySessionLimitReached";

		// Token: 0x04000D1C RID: 3356
		internal const string SecuritySessionAlreadyPending = "SecuritySessionAlreadyPending";

		// Token: 0x04000D1D RID: 3357
		internal const string SecuritySessionNotPending = "SecuritySessionNotPending";

		// Token: 0x04000D1E RID: 3358
		internal const string SecuritySessionListenerNotFound = "SecuritySessionListenerNotFound";

		// Token: 0x04000D1F RID: 3359
		internal const string SessionTokenWasNotClosed = "SessionTokenWasNotClosed";

		// Token: 0x04000D20 RID: 3360
		internal const string ProtocolMustBeInitiator = "ProtocolMustBeInitiator";

		// Token: 0x04000D21 RID: 3361
		internal const string ProtocolMustBeRecipient = "ProtocolMustBeRecipient";

		// Token: 0x04000D22 RID: 3362
		internal const string SendingOutgoingmessageOnRecipient = "SendingOutgoingmessageOnRecipient";

		// Token: 0x04000D23 RID: 3363
		internal const string OnlyBodyReturnValuesSupported = "OnlyBodyReturnValuesSupported";

		// Token: 0x04000D24 RID: 3364
		internal const string UnknownTokenAttachmentMode = "UnknownTokenAttachmentMode";

		// Token: 0x04000D25 RID: 3365
		internal const string ProtocolMisMatch = "ProtocolMisMatch";

		// Token: 0x04000D26 RID: 3366
		internal const string AttemptToCreateMultipleRequestContext = "AttemptToCreateMultipleRequestContext";

		// Token: 0x04000D27 RID: 3367
		internal const string ServerReceivedCloseMessageStateIsCreated = "ServerReceivedCloseMessageStateIsCreated";

		// Token: 0x04000D28 RID: 3368
		internal const string ShutdownRequestWasNotReceived = "ShutdownRequestWasNotReceived";

		// Token: 0x04000D29 RID: 3369
		internal const string UnknownFilterType = "UnknownFilterType";

		// Token: 0x04000D2A RID: 3370
		internal const string StandardsManagerDoesNotMatch = "StandardsManagerDoesNotMatch";

		// Token: 0x04000D2B RID: 3371
		internal const string FilterStrictModeDifferent = "FilterStrictModeDifferent";

		// Token: 0x04000D2C RID: 3372
		internal const string SSSSCreateAcceptor = "SSSSCreateAcceptor";

		// Token: 0x04000D2D RID: 3373
		internal const string TransactionFlowBadOption = "TransactionFlowBadOption";

		// Token: 0x04000D2E RID: 3374
		internal const string TokenManagerCouldNotReadToken = "TokenManagerCouldNotReadToken";

		// Token: 0x04000D2F RID: 3375
		internal const string InvalidActionForNegotiationMessage = "InvalidActionForNegotiationMessage";

		// Token: 0x04000D30 RID: 3376
		internal const string InvalidKeySizeSpecifiedInNegotiation = "InvalidKeySizeSpecifiedInNegotiation";

		// Token: 0x04000D31 RID: 3377
		internal const string GetTokenInfoFailed = "GetTokenInfoFailed";

		// Token: 0x04000D32 RID: 3378
		internal const string UnexpectedEndOfFile = "UnexpectedEndOfFile";

		// Token: 0x04000D33 RID: 3379
		internal const string TimeStampHasCreationAheadOfExpiry = "TimeStampHasCreationAheadOfExpiry";

		// Token: 0x04000D34 RID: 3380
		internal const string TimeStampHasExpiryTimeInPast = "TimeStampHasExpiryTimeInPast";

		// Token: 0x04000D35 RID: 3381
		internal const string TimeStampHasCreationTimeInFuture = "TimeStampHasCreationTimeInFuture";

		// Token: 0x04000D36 RID: 3382
		internal const string TimeStampWasCreatedTooLongAgo = "TimeStampWasCreatedTooLongAgo";

		// Token: 0x04000D37 RID: 3383
		internal const string InvalidOrReplayedNonce = "InvalidOrReplayedNonce";

		// Token: 0x04000D38 RID: 3384
		internal const string MessagePartSpecificationMustBeImmutable = "MessagePartSpecificationMustBeImmutable";

		// Token: 0x04000D39 RID: 3385
		internal const string UnsupportedIssuerEntropyType = "UnsupportedIssuerEntropyType";

		// Token: 0x04000D3A RID: 3386
		internal const string NoRequestSecurityTokenResponseElements = "NoRequestSecurityTokenResponseElements";

		// Token: 0x04000D3B RID: 3387
		internal const string NoCookieInSct = "NoCookieInSct";

		// Token: 0x04000D3C RID: 3388
		internal const string TokenProviderReturnedBadToken = "TokenProviderReturnedBadToken";

		// Token: 0x04000D3D RID: 3389
		internal const string ItemNotAvailableInDeserializedRST = "ItemNotAvailableInDeserializedRST";

		// Token: 0x04000D3E RID: 3390
		internal const string ItemAvailableInDeserializedRSTOnly = "ItemAvailableInDeserializedRSTOnly";

		// Token: 0x04000D3F RID: 3391
		internal const string ItemNotAvailableInDeserializedRSTR = "ItemNotAvailableInDeserializedRSTR";

		// Token: 0x04000D40 RID: 3392
		internal const string ItemAvailableInDeserializedRSTROnly = "ItemAvailableInDeserializedRSTROnly";

		// Token: 0x04000D41 RID: 3393
		internal const string MoreThanOneRSTRInRSTRC = "MoreThanOneRSTRInRSTRC";

		// Token: 0x04000D42 RID: 3394
		internal const string Hosting_VirtualPathExtenstionCanNotBeDetached = "Hosting_VirtualPathExtenstionCanNotBeDetached";

		// Token: 0x04000D43 RID: 3395
		internal const string Hosting_NotSupportedProtocol = "Hosting_NotSupportedProtocol";

		// Token: 0x04000D44 RID: 3396
		internal const string Hosting_BaseUriDeserializedNotValid = "Hosting_BaseUriDeserializedNotValid";

		// Token: 0x04000D45 RID: 3397
		internal const string Hosting_RelativeAddressFormatError = "Hosting_RelativeAddressFormatError";

		// Token: 0x04000D46 RID: 3398
		internal const string Hosting_NoAbsoluteRelativeAddress = "Hosting_NoAbsoluteRelativeAddress";

		// Token: 0x04000D47 RID: 3399
		internal const string SecureConversationNeedsBootstrapSecurity = "SecureConversationNeedsBootstrapSecurity";

		// Token: 0x04000D48 RID: 3400
		internal const string Hosting_MemoryGatesCheckFailedUnderPartialTrust = "Hosting_MemoryGatesCheckFailedUnderPartialTrust";

		// Token: 0x04000D49 RID: 3401
		internal const string Hosting_CompatibilityServiceNotHosted = "Hosting_CompatibilityServiceNotHosted";

		// Token: 0x04000D4A RID: 3402
		internal const string Hosting_MisformattedPort = "Hosting_MisformattedPort";

		// Token: 0x04000D4B RID: 3403
		internal const string Hosting_MisformattedBinding = "Hosting_MisformattedBinding";

		// Token: 0x04000D4C RID: 3404
		internal const string Hosting_MisformattedBindingData = "Hosting_MisformattedBindingData";

		// Token: 0x04000D4D RID: 3405
		internal const string Hosting_NoHttpTransportManagerForUri = "Hosting_NoHttpTransportManagerForUri";

		// Token: 0x04000D4E RID: 3406
		internal const string Hosting_NoTcpPipeTransportManagerForUri = "Hosting_NoTcpPipeTransportManagerForUri";

		// Token: 0x04000D4F RID: 3407
		internal const string Hosting_ProcessNotExecutingUnderHostedContext = "Hosting_ProcessNotExecutingUnderHostedContext";

		// Token: 0x04000D50 RID: 3408
		internal const string Hosting_ServiceActivationFailed = "Hosting_ServiceActivationFailed";

		// Token: 0x04000D51 RID: 3409
		internal const string Hosting_ServiceTypeNotProvided = "Hosting_ServiceTypeNotProvided";

		// Token: 0x04000D52 RID: 3410
		internal const string SharedEndpointReadDenied = "SharedEndpointReadDenied";

		// Token: 0x04000D53 RID: 3411
		internal const string SharedEndpointReadNotFound = "SharedEndpointReadNotFound";

		// Token: 0x04000D54 RID: 3412
		internal const string SharedManagerBase = "SharedManagerBase";

		// Token: 0x04000D55 RID: 3413
		internal const string SharedManagerServiceStartFailure = "SharedManagerServiceStartFailure";

		// Token: 0x04000D56 RID: 3414
		internal const string SharedManagerServiceStartFailureDisabled = "SharedManagerServiceStartFailureDisabled";

		// Token: 0x04000D57 RID: 3415
		internal const string SharedManagerServiceStartFailureNoError = "SharedManagerServiceStartFailureNoError";

		// Token: 0x04000D58 RID: 3416
		internal const string SharedManagerServiceLookupFailure = "SharedManagerServiceLookupFailure";

		// Token: 0x04000D59 RID: 3417
		internal const string SharedManagerServiceSidLookupFailure = "SharedManagerServiceSidLookupFailure";

		// Token: 0x04000D5A RID: 3418
		internal const string SharedManagerServiceEndpointReadFailure = "SharedManagerServiceEndpointReadFailure";

		// Token: 0x04000D5B RID: 3419
		internal const string SharedManagerServiceSecurityFailed = "SharedManagerServiceSecurityFailed";

		// Token: 0x04000D5C RID: 3420
		internal const string SharedManagerUserSidLookupFailure = "SharedManagerUserSidLookupFailure";

		// Token: 0x04000D5D RID: 3421
		internal const string SharedManagerCurrentUserSidLookupFailure = "SharedManagerCurrentUserSidLookupFailure";

		// Token: 0x04000D5E RID: 3422
		internal const string SharedManagerLogonSidLookupFailure = "SharedManagerLogonSidLookupFailure";

		// Token: 0x04000D5F RID: 3423
		internal const string SharedManagerDataConnectionFailure = "SharedManagerDataConnectionFailure";

		// Token: 0x04000D60 RID: 3424
		internal const string SharedManagerDataConnectionCreateFailure = "SharedManagerDataConnectionCreateFailure";

		// Token: 0x04000D61 RID: 3425
		internal const string SharedManagerDataConnectionPipeFailed = "SharedManagerDataConnectionPipeFailed";

		// Token: 0x04000D62 RID: 3426
		internal const string SharedManagerVersionUnsupported = "SharedManagerVersionUnsupported";

		// Token: 0x04000D63 RID: 3427
		internal const string SharedManagerAllowDupHandleFailed = "SharedManagerAllowDupHandleFailed";

		// Token: 0x04000D64 RID: 3428
		internal const string SharedManagerPathTooLong = "SharedManagerPathTooLong";

		// Token: 0x04000D65 RID: 3429
		internal const string SharedManagerRegistrationQuotaExceeded = "SharedManagerRegistrationQuotaExceeded";

		// Token: 0x04000D66 RID: 3430
		internal const string SharedManagerProtocolUnsupported = "SharedManagerProtocolUnsupported";

		// Token: 0x04000D67 RID: 3431
		internal const string SharedManagerConflictingRegistration = "SharedManagerConflictingRegistration";

		// Token: 0x04000D68 RID: 3432
		internal const string SharedManagerFailedToListen = "SharedManagerFailedToListen";

		// Token: 0x04000D69 RID: 3433
		internal const string Sharing_ConnectionDispatchFailed = "Sharing_ConnectionDispatchFailed";

		// Token: 0x04000D6A RID: 3434
		internal const string Sharing_EndpointUnavailable = "Sharing_EndpointUnavailable";

		// Token: 0x04000D6B RID: 3435
		internal const string Sharing_EmptyListenerEndpoint = "Sharing_EmptyListenerEndpoint";

		// Token: 0x04000D6C RID: 3436
		internal const string Sharing_ListenerProxyStopped = "Sharing_ListenerProxyStopped";

		// Token: 0x04000D6D RID: 3437
		internal const string UnexpectedEmptyElementExpectingClaim = "UnexpectedEmptyElementExpectingClaim";

		// Token: 0x04000D6E RID: 3438
		internal const string UnexpectedElementExpectingElement = "UnexpectedElementExpectingElement";

		// Token: 0x04000D6F RID: 3439
		internal const string UnexpectedDuplicateElement = "UnexpectedDuplicateElement";

		// Token: 0x04000D70 RID: 3440
		internal const string UnsupportedSecurityPolicyAssertion = "UnsupportedSecurityPolicyAssertion";

		// Token: 0x04000D71 RID: 3441
		internal const string MultipleIdentities = "MultipleIdentities";

		// Token: 0x04000D72 RID: 3442
		internal const string InvalidUriValue = "InvalidUriValue";

		// Token: 0x04000D73 RID: 3443
		internal const string BindingDoesNotSupportProtectionForRst = "BindingDoesNotSupportProtectionForRst";

		// Token: 0x04000D74 RID: 3444
		internal const string TransportDoesNotProtectMessage = "TransportDoesNotProtectMessage";

		// Token: 0x04000D75 RID: 3445
		internal const string BindingDoesNotSupportWindowsIdenityForImpersonation = "BindingDoesNotSupportWindowsIdenityForImpersonation";

		// Token: 0x04000D76 RID: 3446
		internal const string ListenUriNotSet = "ListenUriNotSet";

		// Token: 0x04000D77 RID: 3447
		internal const string UnsupportedChannelInterfaceType = "UnsupportedChannelInterfaceType";

		// Token: 0x04000D78 RID: 3448
		internal const string TransportManagerOpen = "TransportManagerOpen";

		// Token: 0x04000D79 RID: 3449
		internal const string TransportManagerNotOpen = "TransportManagerNotOpen";

		// Token: 0x04000D7A RID: 3450
		internal const string UnrecognizedIdentityType = "UnrecognizedIdentityType";

		// Token: 0x04000D7B RID: 3451
		internal const string InvalidIdentityElement = "InvalidIdentityElement";

		// Token: 0x04000D7C RID: 3452
		internal const string UnableToLoadCertificateIdentity = "UnableToLoadCertificateIdentity";

		// Token: 0x04000D7D RID: 3453
		internal const string UnrecognizedClaimTypeForIdentity = "UnrecognizedClaimTypeForIdentity";

		// Token: 0x04000D7E RID: 3454
		internal const string AsyncCallbackException = "AsyncCallbackException";

		// Token: 0x04000D7F RID: 3455
		internal const string SendCannotBeCalledAfterCloseOutputSession = "SendCannotBeCalledAfterCloseOutputSession";

		// Token: 0x04000D80 RID: 3456
		internal const string CommunicationObjectCannotBeModifiedInState = "CommunicationObjectCannotBeModifiedInState";

		// Token: 0x04000D81 RID: 3457
		internal const string CommunicationObjectCannotBeModified = "CommunicationObjectCannotBeModified";

		// Token: 0x04000D82 RID: 3458
		internal const string CommunicationObjectCannotBeUsed = "CommunicationObjectCannotBeUsed";

		// Token: 0x04000D83 RID: 3459
		internal const string CommunicationObjectFaulted1 = "CommunicationObjectFaulted1";

		// Token: 0x04000D84 RID: 3460
		internal const string CommunicationObjectFaultedStack2 = "CommunicationObjectFaultedStack2";

		// Token: 0x04000D85 RID: 3461
		internal const string CommunicationObjectAborted1 = "CommunicationObjectAborted1";

		// Token: 0x04000D86 RID: 3462
		internal const string CommunicationObjectAbortedStack2 = "CommunicationObjectAbortedStack2";

		// Token: 0x04000D87 RID: 3463
		internal const string CommunicationObjectBaseClassMethodNotCalled = "CommunicationObjectBaseClassMethodNotCalled";

		// Token: 0x04000D88 RID: 3464
		internal const string CommunicationObjectInInvalidState = "CommunicationObjectInInvalidState";

		// Token: 0x04000D89 RID: 3465
		internal const string CommunicationObjectCloseInterrupted1 = "CommunicationObjectCloseInterrupted1";

		// Token: 0x04000D8A RID: 3466
		internal const string ChannelFactoryCannotBeUsedToCreateChannels = "ChannelFactoryCannotBeUsedToCreateChannels";

		// Token: 0x04000D8B RID: 3467
		internal const string ChannelParametersCannotBeModified = "ChannelParametersCannotBeModified";

		// Token: 0x04000D8C RID: 3468
		internal const string ChannelParametersCannotBePropagated = "ChannelParametersCannotBePropagated";

		// Token: 0x04000D8D RID: 3469
		internal const string OneWayInternalTypeNotSupported = "OneWayInternalTypeNotSupported";

		// Token: 0x04000D8E RID: 3470
		internal const string ChannelTypeNotSupported = "ChannelTypeNotSupported";

		// Token: 0x04000D8F RID: 3471
		internal const string SecurityContextMissing = "SecurityContextMissing";

		// Token: 0x04000D90 RID: 3472
		internal const string SecurityContextDoesNotAllowImpersonation = "SecurityContextDoesNotAllowImpersonation";

		// Token: 0x04000D91 RID: 3473
		internal const string InvalidEnumValue = "InvalidEnumValue";

		// Token: 0x04000D92 RID: 3474
		internal const string InvalidDecoderStateMachine = "InvalidDecoderStateMachine";

		// Token: 0x04000D93 RID: 3475
		internal const string OperationPropertyIsRequiredForAttributeGeneration = "OperationPropertyIsRequiredForAttributeGeneration";

		// Token: 0x04000D94 RID: 3476
		internal const string InvalidMembershipProviderSpecifiedInConfig = "InvalidMembershipProviderSpecifiedInConfig";

		// Token: 0x04000D95 RID: 3477
		internal const string InvalidRoleProviderSpecifiedInConfig = "InvalidRoleProviderSpecifiedInConfig";

		// Token: 0x04000D96 RID: 3478
		internal const string ObjectDisposed = "ObjectDisposed";

		// Token: 0x04000D97 RID: 3479
		internal const string InvalidReaderPositionOnCreateMessage = "InvalidReaderPositionOnCreateMessage";

		// Token: 0x04000D98 RID: 3480
		internal const string DuplicateMessageProperty = "DuplicateMessageProperty";

		// Token: 0x04000D99 RID: 3481
		internal const string MessagePropertyNotFound = "MessagePropertyNotFound";

		// Token: 0x04000D9A RID: 3482
		internal const string HeaderAlreadyUnderstood = "HeaderAlreadyUnderstood";

		// Token: 0x04000D9B RID: 3483
		internal const string HeaderAlreadyNotUnderstood = "HeaderAlreadyNotUnderstood";

		// Token: 0x04000D9C RID: 3484
		internal const string MultipleMessageHeaders = "MultipleMessageHeaders";

		// Token: 0x04000D9D RID: 3485
		internal const string MultipleMessageHeadersWithActor = "MultipleMessageHeadersWithActor";

		// Token: 0x04000D9E RID: 3486
		internal const string MultipleRelatesToHeaders = "MultipleRelatesToHeaders";

		// Token: 0x04000D9F RID: 3487
		internal const string ExtraContentIsPresentInFaultDetail = "ExtraContentIsPresentInFaultDetail";

		// Token: 0x04000DA0 RID: 3488
		internal const string MessageIsEmpty = "MessageIsEmpty";

		// Token: 0x04000DA1 RID: 3489
		internal const string MessageClosed = "MessageClosed";

		// Token: 0x04000DA2 RID: 3490
		internal const string StreamClosed = "StreamClosed";

		// Token: 0x04000DA3 RID: 3491
		internal const string BodyWriterReturnedIsNotBuffered = "BodyWriterReturnedIsNotBuffered";

		// Token: 0x04000DA4 RID: 3492
		internal const string BodyWriterCanOnlyBeWrittenOnce = "BodyWriterCanOnlyBeWrittenOnce";

		// Token: 0x04000DA5 RID: 3493
		internal const string RstrKeySizeNotProvided = "RstrKeySizeNotProvided";

		// Token: 0x04000DA6 RID: 3494
		internal const string RequestMessageDoesNotHaveAMessageID = "RequestMessageDoesNotHaveAMessageID";

		// Token: 0x04000DA7 RID: 3495
		internal const string HeaderNotFound = "HeaderNotFound";

		// Token: 0x04000DA8 RID: 3496
		internal const string MessageBufferIsClosed = "MessageBufferIsClosed";

		// Token: 0x04000DA9 RID: 3497
		internal const string MessageTextEncodingNotSupported = "MessageTextEncodingNotSupported";

		// Token: 0x04000DAA RID: 3498
		internal const string AtLeastOneFaultReasonMustBeSpecified = "AtLeastOneFaultReasonMustBeSpecified";

		// Token: 0x04000DAB RID: 3499
		internal const string NoNullTranslations = "NoNullTranslations";

		// Token: 0x04000DAC RID: 3500
		internal const string FaultDoesNotHaveAnyDetail = "FaultDoesNotHaveAnyDetail";

		// Token: 0x04000DAD RID: 3501
		internal const string InvalidXmlQualifiedName = "InvalidXmlQualifiedName";

		// Token: 0x04000DAE RID: 3502
		internal const string UnboundPrefixInQName = "UnboundPrefixInQName";

		// Token: 0x04000DAF RID: 3503
		internal const string MessageBodyIsUnknown = "MessageBodyIsUnknown";

		// Token: 0x04000DB0 RID: 3504
		internal const string MessageBodyIsStream = "MessageBodyIsStream";

		// Token: 0x04000DB1 RID: 3505
		internal const string MessageBodyToStringError = "MessageBodyToStringError";

		// Token: 0x04000DB2 RID: 3506
		internal const string NoMatchingTranslationFoundForFaultText = "NoMatchingTranslationFoundForFaultText";

		// Token: 0x04000DB3 RID: 3507
		internal const string CannotDetermineSPNBasedOnAddress = "CannotDetermineSPNBasedOnAddress";

		// Token: 0x04000DB4 RID: 3508
		internal const string XmlLangAttributeMissing = "XmlLangAttributeMissing";

		// Token: 0x04000DB5 RID: 3509
		internal const string EncoderUnrecognizedCharSet = "EncoderUnrecognizedCharSet";

		// Token: 0x04000DB6 RID: 3510
		internal const string EncoderUnrecognizedContentType = "EncoderUnrecognizedContentType";

		// Token: 0x04000DB7 RID: 3511
		internal const string EncoderBadContentType = "EncoderBadContentType";

		// Token: 0x04000DB8 RID: 3512
		internal const string EncoderEnvelopeVersionMismatch = "EncoderEnvelopeVersionMismatch";

		// Token: 0x04000DB9 RID: 3513
		internal const string EncoderMessageVersionMismatch = "EncoderMessageVersionMismatch";

		// Token: 0x04000DBA RID: 3514
		internal const string MtomEncoderBadMessageVersion = "MtomEncoderBadMessageVersion";

		// Token: 0x04000DBB RID: 3515
		internal const string ReadNotSupported = "ReadNotSupported";

		// Token: 0x04000DBC RID: 3516
		internal const string SeekNotSupported = "SeekNotSupported";

		// Token: 0x04000DBD RID: 3517
		internal const string WriterAsyncWritePending = "WriterAsyncWritePending";

		// Token: 0x04000DBE RID: 3518
		internal const string ChannelInitializationTimeout = "ChannelInitializationTimeout";

		// Token: 0x04000DBF RID: 3519
		internal const string SocketCloseReadTimeout = "SocketCloseReadTimeout";

		// Token: 0x04000DC0 RID: 3520
		internal const string SocketCloseReadReceivedData = "SocketCloseReadReceivedData";

		// Token: 0x04000DC1 RID: 3521
		internal const string PipeCantCloseWithPendingWrite = "PipeCantCloseWithPendingWrite";

		// Token: 0x04000DC2 RID: 3522
		internal const string PipeShutdownWriteError = "PipeShutdownWriteError";

		// Token: 0x04000DC3 RID: 3523
		internal const string PipeShutdownReadError = "PipeShutdownReadError";

		// Token: 0x04000DC4 RID: 3524
		internal const string PipeNameCanNotBeAccessed = "PipeNameCanNotBeAccessed";

		// Token: 0x04000DC5 RID: 3525
		internal const string PipeNameCanNotBeAccessed2 = "PipeNameCanNotBeAccessed2";

		// Token: 0x04000DC6 RID: 3526
		internal const string PipeModeChangeFailed = "PipeModeChangeFailed";

		// Token: 0x04000DC7 RID: 3527
		internal const string PipeCloseFailed = "PipeCloseFailed";

		// Token: 0x04000DC8 RID: 3528
		internal const string PipeAlreadyShuttingDown = "PipeAlreadyShuttingDown";

		// Token: 0x04000DC9 RID: 3529
		internal const string PipeSignalExpected = "PipeSignalExpected";

		// Token: 0x04000DCA RID: 3530
		internal const string PipeAlreadyClosing = "PipeAlreadyClosing";

		// Token: 0x04000DCB RID: 3531
		internal const string PipeAcceptFailed = "PipeAcceptFailed";

		// Token: 0x04000DCC RID: 3532
		internal const string PipeListenFailed = "PipeListenFailed";

		// Token: 0x04000DCD RID: 3533
		internal const string PipeNameInUse = "PipeNameInUse";

		// Token: 0x04000DCE RID: 3534
		internal const string PipeNameCantBeReserved = "PipeNameCantBeReserved";

		// Token: 0x04000DCF RID: 3535
		internal const string PipeListenerDisposed = "PipeListenerDisposed";

		// Token: 0x04000DD0 RID: 3536
		internal const string PipeListenerNotListening = "PipeListenerNotListening";

		// Token: 0x04000DD1 RID: 3537
		internal const string PipeConnectAddressFailed = "PipeConnectAddressFailed";

		// Token: 0x04000DD2 RID: 3538
		internal const string PipeConnectFailed = "PipeConnectFailed";

		// Token: 0x04000DD3 RID: 3539
		internal const string PipeConnectTimedOut = "PipeConnectTimedOut";

		// Token: 0x04000DD4 RID: 3540
		internal const string PipeConnectTimedOutServerTooBusy = "PipeConnectTimedOutServerTooBusy";

		// Token: 0x04000DD5 RID: 3541
		internal const string PipeEndpointNotFound = "PipeEndpointNotFound";

		// Token: 0x04000DD6 RID: 3542
		internal const string PipeUriSchemeWrong = "PipeUriSchemeWrong";

		// Token: 0x04000DD7 RID: 3543
		internal const string PipeWriteIncomplete = "PipeWriteIncomplete";

		// Token: 0x04000DD8 RID: 3544
		internal const string PipeClosed = "PipeClosed";

		// Token: 0x04000DD9 RID: 3545
		internal const string PipeReadTimedOut = "PipeReadTimedOut";

		// Token: 0x04000DDA RID: 3546
		internal const string PipeWriteTimedOut = "PipeWriteTimedOut";

		// Token: 0x04000DDB RID: 3547
		internal const string PipeConnectionAbortedReadTimedOut = "PipeConnectionAbortedReadTimedOut";

		// Token: 0x04000DDC RID: 3548
		internal const string PipeConnectionAbortedWriteTimedOut = "PipeConnectionAbortedWriteTimedOut";

		// Token: 0x04000DDD RID: 3549
		internal const string PipeWriteError = "PipeWriteError";

		// Token: 0x04000DDE RID: 3550
		internal const string PipeReadError = "PipeReadError";

		// Token: 0x04000DDF RID: 3551
		internal const string PipeUnknownWin32Error = "PipeUnknownWin32Error";

		// Token: 0x04000DE0 RID: 3552
		internal const string PipeKnownWin32Error = "PipeKnownWin32Error";

		// Token: 0x04000DE1 RID: 3553
		internal const string PipeWritePending = "PipeWritePending";

		// Token: 0x04000DE2 RID: 3554
		internal const string PipeReadPending = "PipeReadPending";

		// Token: 0x04000DE3 RID: 3555
		internal const string PipeDuplicationFailed = "PipeDuplicationFailed";

		// Token: 0x04000DE4 RID: 3556
		internal const string SessionValueInvalid = "SessionValueInvalid";

		// Token: 0x04000DE5 RID: 3557
		internal const string PackageFullNameInvalid = "PackageFullNameInvalid";

		// Token: 0x04000DE6 RID: 3558
		internal const string SocketAbortedReceiveTimedOut = "SocketAbortedReceiveTimedOut";

		// Token: 0x04000DE7 RID: 3559
		internal const string SocketAbortedSendTimedOut = "SocketAbortedSendTimedOut";

		// Token: 0x04000DE8 RID: 3560
		internal const string OperationInvalidBeforeSecurityNegotiation = "OperationInvalidBeforeSecurityNegotiation";

		// Token: 0x04000DE9 RID: 3561
		internal const string FramingError = "FramingError";

		// Token: 0x04000DEA RID: 3562
		internal const string FramingPrematureEOF = "FramingPrematureEOF";

		// Token: 0x04000DEB RID: 3563
		internal const string FramingRecordTypeMismatch = "FramingRecordTypeMismatch";

		// Token: 0x04000DEC RID: 3564
		internal const string FramingVersionNotSupported = "FramingVersionNotSupported";

		// Token: 0x04000DED RID: 3565
		internal const string FramingModeNotSupported = "FramingModeNotSupported";

		// Token: 0x04000DEE RID: 3566
		internal const string FramingSizeTooLarge = "FramingSizeTooLarge";

		// Token: 0x04000DEF RID: 3567
		internal const string FramingViaTooLong = "FramingViaTooLong";

		// Token: 0x04000DF0 RID: 3568
		internal const string FramingViaNotUri = "FramingViaNotUri";

		// Token: 0x04000DF1 RID: 3569
		internal const string FramingFaultTooLong = "FramingFaultTooLong";

		// Token: 0x04000DF2 RID: 3570
		internal const string FramingContentTypeTooLong = "FramingContentTypeTooLong";

		// Token: 0x04000DF3 RID: 3571
		internal const string FramingValueNotAvailable = "FramingValueNotAvailable";

		// Token: 0x04000DF4 RID: 3572
		internal const string FramingAtEnd = "FramingAtEnd";

		// Token: 0x04000DF5 RID: 3573
		internal const string RemoteSecurityNotNegotiatedOnStreamUpgrade = "RemoteSecurityNotNegotiatedOnStreamUpgrade";

		// Token: 0x04000DF6 RID: 3574
		internal const string BinaryEncoderSessionTooLarge = "BinaryEncoderSessionTooLarge";

		// Token: 0x04000DF7 RID: 3575
		internal const string BinaryEncoderSessionInvalid = "BinaryEncoderSessionInvalid";

		// Token: 0x04000DF8 RID: 3576
		internal const string BinaryEncoderSessionMalformed = "BinaryEncoderSessionMalformed";

		// Token: 0x04000DF9 RID: 3577
		internal const string ReceiveShutdownReturnedFault = "ReceiveShutdownReturnedFault";

		// Token: 0x04000DFA RID: 3578
		internal const string ReceiveShutdownReturnedLargeFault = "ReceiveShutdownReturnedLargeFault";

		// Token: 0x04000DFB RID: 3579
		internal const string ReceiveShutdownReturnedMessage = "ReceiveShutdownReturnedMessage";

		// Token: 0x04000DFC RID: 3580
		internal const string MaxReceivedMessageSizeExceeded = "MaxReceivedMessageSizeExceeded";

		// Token: 0x04000DFD RID: 3581
		internal const string MaxSentMessageSizeExceeded = "MaxSentMessageSizeExceeded";

		// Token: 0x04000DFE RID: 3582
		internal const string FramingMaxMessageSizeExceeded = "FramingMaxMessageSizeExceeded";

		// Token: 0x04000DFF RID: 3583
		internal const string StreamDoesNotSupportTimeout = "StreamDoesNotSupportTimeout";

		// Token: 0x04000E00 RID: 3584
		internal const string FilterExists = "FilterExists";

		// Token: 0x04000E01 RID: 3585
		internal const string FilterUnexpectedError = "FilterUnexpectedError";

		// Token: 0x04000E02 RID: 3586
		internal const string FilterNodeQuotaExceeded = "FilterNodeQuotaExceeded";

		// Token: 0x04000E03 RID: 3587
		internal const string FilterCapacityNegative = "FilterCapacityNegative";

		// Token: 0x04000E04 RID: 3588
		internal const string ActionFilterEmptyList = "ActionFilterEmptyList";

		// Token: 0x04000E05 RID: 3589
		internal const string FilterUndefinedPrefix = "FilterUndefinedPrefix";

		// Token: 0x04000E06 RID: 3590
		internal const string FilterMultipleMatches = "FilterMultipleMatches";

		// Token: 0x04000E07 RID: 3591
		internal const string FilterTableTypeMismatch = "FilterTableTypeMismatch";

		// Token: 0x04000E08 RID: 3592
		internal const string FilterTableInvalidForLookup = "FilterTableInvalidForLookup";

		// Token: 0x04000E09 RID: 3593
		internal const string FilterBadTableType = "FilterBadTableType";

		// Token: 0x04000E0A RID: 3594
		internal const string FilterQuotaRange = "FilterQuotaRange";

		// Token: 0x04000E0B RID: 3595
		internal const string FilterEmptyString = "FilterEmptyString";

		// Token: 0x04000E0C RID: 3596
		internal const string FilterInvalidInner = "FilterInvalidInner";

		// Token: 0x04000E0D RID: 3597
		internal const string FilterInvalidAttribute = "FilterInvalidAttribute";

		// Token: 0x04000E0E RID: 3598
		internal const string FilterInvalidDialect = "FilterInvalidDialect";

		// Token: 0x04000E0F RID: 3599
		internal const string FilterCouldNotCompile = "FilterCouldNotCompile";

		// Token: 0x04000E10 RID: 3600
		internal const string FilterReaderNotStartElem = "FilterReaderNotStartElem";

		// Token: 0x04000E11 RID: 3601
		internal const string SeekableMessageNavInvalidPosition = "SeekableMessageNavInvalidPosition";

		// Token: 0x04000E12 RID: 3602
		internal const string SeekableMessageNavNonAtomized = "SeekableMessageNavNonAtomized";

		// Token: 0x04000E13 RID: 3603
		internal const string SeekableMessageNavIDNotSupported = "SeekableMessageNavIDNotSupported";

		// Token: 0x04000E14 RID: 3604
		internal const string SeekableMessageNavBodyForbidden = "SeekableMessageNavBodyForbidden";

		// Token: 0x04000E15 RID: 3605
		internal const string SeekableMessageNavOverrideForbidden = "SeekableMessageNavOverrideForbidden";

		// Token: 0x04000E16 RID: 3606
		internal const string QueryNotImplemented = "QueryNotImplemented";

		// Token: 0x04000E17 RID: 3607
		internal const string QueryNotSortable = "QueryNotSortable";

		// Token: 0x04000E18 RID: 3608
		internal const string QueryMustBeSeekable = "QueryMustBeSeekable";

		// Token: 0x04000E19 RID: 3609
		internal const string QueryContextNotSupportedInSequences = "QueryContextNotSupportedInSequences";

		// Token: 0x04000E1A RID: 3610
		internal const string QueryFunctionTypeNotSupported = "QueryFunctionTypeNotSupported";

		// Token: 0x04000E1B RID: 3611
		internal const string QueryVariableTypeNotSupported = "QueryVariableTypeNotSupported";

		// Token: 0x04000E1C RID: 3612
		internal const string QueryVariableNull = "QueryVariableNull";

		// Token: 0x04000E1D RID: 3613
		internal const string QueryFunctionStringArg = "QueryFunctionStringArg";

		// Token: 0x04000E1E RID: 3614
		internal const string QueryItemAlreadyExists = "QueryItemAlreadyExists";

		// Token: 0x04000E1F RID: 3615
		internal const string QueryBeforeNodes = "QueryBeforeNodes";

		// Token: 0x04000E20 RID: 3616
		internal const string QueryAfterNodes = "QueryAfterNodes";

		// Token: 0x04000E21 RID: 3617
		internal const string QueryIteratorOutOfScope = "QueryIteratorOutOfScope";

		// Token: 0x04000E22 RID: 3618
		internal const string QueryCantGetStringForMovedIterator = "QueryCantGetStringForMovedIterator";

		// Token: 0x04000E23 RID: 3619
		internal const string MessageVersionToStringFormat = "MessageVersionToStringFormat";

		// Token: 0x04000E24 RID: 3620
		internal const string Addressing10ToStringFormat = "Addressing10ToStringFormat";

		// Token: 0x04000E25 RID: 3621
		internal const string Addressing200408ToStringFormat = "Addressing200408ToStringFormat";

		// Token: 0x04000E26 RID: 3622
		internal const string AddressingNoneToStringFormat = "AddressingNoneToStringFormat";

		// Token: 0x04000E27 RID: 3623
		internal const string AddressingVersionNotSupported = "AddressingVersionNotSupported";

		// Token: 0x04000E28 RID: 3624
		internal const string SupportedAddressingModeNotSupported = "SupportedAddressingModeNotSupported";

		// Token: 0x04000E29 RID: 3625
		internal const string Soap11ToStringFormat = "Soap11ToStringFormat";

		// Token: 0x04000E2A RID: 3626
		internal const string Soap12ToStringFormat = "Soap12ToStringFormat";

		// Token: 0x04000E2B RID: 3627
		internal const string EnvelopeNoneToStringFormat = "EnvelopeNoneToStringFormat";

		// Token: 0x04000E2C RID: 3628
		internal const string MessagePropertyReturnedNullCopy = "MessagePropertyReturnedNullCopy";

		// Token: 0x04000E2D RID: 3629
		internal const string MessageVersionUnknown = "MessageVersionUnknown";

		// Token: 0x04000E2E RID: 3630
		internal const string EnvelopeVersionUnknown = "EnvelopeVersionUnknown";

		// Token: 0x04000E2F RID: 3631
		internal const string EnvelopeVersionNotSupported = "EnvelopeVersionNotSupported";

		// Token: 0x04000E30 RID: 3632
		internal const string CannotDetectAddressingVersion = "CannotDetectAddressingVersion";

		// Token: 0x04000E31 RID: 3633
		internal const string HeadersCannotBeAddedToEnvelopeVersion = "HeadersCannotBeAddedToEnvelopeVersion";

		// Token: 0x04000E32 RID: 3634
		internal const string AddressingHeadersCannotBeAddedToAddressingVersion = "AddressingHeadersCannotBeAddedToAddressingVersion";

		// Token: 0x04000E33 RID: 3635
		internal const string AddressingExtensionInBadNS = "AddressingExtensionInBadNS";

		// Token: 0x04000E34 RID: 3636
		internal const string MessageHeaderVersionNotSupported = "MessageHeaderVersionNotSupported";

		// Token: 0x04000E35 RID: 3637
		internal const string MessageHasBeenCopied = "MessageHasBeenCopied";

		// Token: 0x04000E36 RID: 3638
		internal const string MessageHasBeenWritten = "MessageHasBeenWritten";

		// Token: 0x04000E37 RID: 3639
		internal const string MessageHasBeenRead = "MessageHasBeenRead";

		// Token: 0x04000E38 RID: 3640
		internal const string InvalidMessageState = "InvalidMessageState";

		// Token: 0x04000E39 RID: 3641
		internal const string MessageBodyReaderInvalidReadState = "MessageBodyReaderInvalidReadState";

		// Token: 0x04000E3A RID: 3642
		internal const string XmlBufferQuotaExceeded = "XmlBufferQuotaExceeded";

		// Token: 0x04000E3B RID: 3643
		internal const string XmlBufferInInvalidState = "XmlBufferInInvalidState";

		// Token: 0x04000E3C RID: 3644
		internal const string MessageBodyMissing = "MessageBodyMissing";

		// Token: 0x04000E3D RID: 3645
		internal const string MessageHeaderVersionMismatch = "MessageHeaderVersionMismatch";

		// Token: 0x04000E3E RID: 3646
		internal const string ManualAddressingRequiresAddressedMessages = "ManualAddressingRequiresAddressedMessages";

		// Token: 0x04000E3F RID: 3647
		internal const string OneWayHeaderNotFound = "OneWayHeaderNotFound";

		// Token: 0x04000E40 RID: 3648
		internal const string ReceiveTimedOut = "ReceiveTimedOut";

		// Token: 0x04000E41 RID: 3649
		internal const string ReceiveTimedOut2 = "ReceiveTimedOut2";

		// Token: 0x04000E42 RID: 3650
		internal const string WaitForMessageTimedOut = "WaitForMessageTimedOut";

		// Token: 0x04000E43 RID: 3651
		internal const string ReceiveTimedOutNoLocalAddress = "ReceiveTimedOutNoLocalAddress";

		// Token: 0x04000E44 RID: 3652
		internal const string ReceiveRequestTimedOutNoLocalAddress = "ReceiveRequestTimedOutNoLocalAddress";

		// Token: 0x04000E45 RID: 3653
		internal const string ReceiveRequestTimedOut = "ReceiveRequestTimedOut";

		// Token: 0x04000E46 RID: 3654
		internal const string SendToViaTimedOut = "SendToViaTimedOut";

		// Token: 0x04000E47 RID: 3655
		internal const string CloseTimedOut = "CloseTimedOut";

		// Token: 0x04000E48 RID: 3656
		internal const string OpenTimedOutEstablishingTransportSession = "OpenTimedOutEstablishingTransportSession";

		// Token: 0x04000E49 RID: 3657
		internal const string RequestTimedOutEstablishingTransportSession = "RequestTimedOutEstablishingTransportSession";

		// Token: 0x04000E4A RID: 3658
		internal const string TcpConnectingToViaTimedOut = "TcpConnectingToViaTimedOut";

		// Token: 0x04000E4B RID: 3659
		internal const string RequestChannelSendTimedOut = "RequestChannelSendTimedOut";

		// Token: 0x04000E4C RID: 3660
		internal const string RequestChannelWaitForReplyTimedOut = "RequestChannelWaitForReplyTimedOut";

		// Token: 0x04000E4D RID: 3661
		internal const string HttpTransportCannotHaveMultipleAuthenticationSchemes = "HttpTransportCannotHaveMultipleAuthenticationSchemes";

		// Token: 0x04000E4E RID: 3662
		internal const string MultipleCCbesInParameters = "MultipleCCbesInParameters";

		// Token: 0x04000E4F RID: 3663
		internal const string CookieContainerBindingElementNeedsHttp = "CookieContainerBindingElementNeedsHttp";

		// Token: 0x04000E50 RID: 3664
		internal const string HttpIfModifiedSinceParseError = "HttpIfModifiedSinceParseError";

		// Token: 0x04000E51 RID: 3665
		internal const string HttpSoapActionMismatch = "HttpSoapActionMismatch";

		// Token: 0x04000E52 RID: 3666
		internal const string HttpSoapActionMismatchContentType = "HttpSoapActionMismatchContentType";

		// Token: 0x04000E53 RID: 3667
		internal const string HttpSoapActionMismatchFault = "HttpSoapActionMismatchFault";

		// Token: 0x04000E54 RID: 3668
		internal const string HttpContentTypeFormatException = "HttpContentTypeFormatException";

		// Token: 0x04000E55 RID: 3669
		internal const string HttpServerTooBusy = "HttpServerTooBusy";

		// Token: 0x04000E56 RID: 3670
		internal const string HttpRequestAborted = "HttpRequestAborted";

		// Token: 0x04000E57 RID: 3671
		internal const string HttpRequestTimedOut = "HttpRequestTimedOut";

		// Token: 0x04000E58 RID: 3672
		internal const string HttpResponseTimedOut = "HttpResponseTimedOut";

		// Token: 0x04000E59 RID: 3673
		internal const string HttpTransferError = "HttpTransferError";

		// Token: 0x04000E5A RID: 3674
		internal const string HttpReceiveFailure = "HttpReceiveFailure";

		// Token: 0x04000E5B RID: 3675
		internal const string HttpSendFailure = "HttpSendFailure";

		// Token: 0x04000E5C RID: 3676
		internal const string HttpAuthDoesNotSupportRequestStreaming = "HttpAuthDoesNotSupportRequestStreaming";

		// Token: 0x04000E5D RID: 3677
		internal const string ReplyAlreadySent = "ReplyAlreadySent";

		// Token: 0x04000E5E RID: 3678
		internal const string HttpInvalidListenURI = "HttpInvalidListenURI";

		// Token: 0x04000E5F RID: 3679
		internal const string RequestContextAborted = "RequestContextAborted";

		// Token: 0x04000E60 RID: 3680
		internal const string ReceiveContextCannotBeUsed = "ReceiveContextCannotBeUsed";

		// Token: 0x04000E61 RID: 3681
		internal const string ReceiveContextInInvalidState = "ReceiveContextInInvalidState";

		// Token: 0x04000E62 RID: 3682
		internal const string ReceiveContextFaulted = "ReceiveContextFaulted";

		// Token: 0x04000E63 RID: 3683
		internal const string UnrecognizedHostNameComparisonMode = "UnrecognizedHostNameComparisonMode";

		// Token: 0x04000E64 RID: 3684
		internal const string BadData = "BadData";

		// Token: 0x04000E65 RID: 3685
		internal const string InvalidRenewResponseAction = "InvalidRenewResponseAction";

		// Token: 0x04000E66 RID: 3686
		internal const string InvalidCloseResponseAction = "InvalidCloseResponseAction";

		// Token: 0x04000E67 RID: 3687
		internal const string IncompatibleBehaviors = "IncompatibleBehaviors";

		// Token: 0x04000E68 RID: 3688
		internal const string NullSessionRequestMessage = "NullSessionRequestMessage";

		// Token: 0x04000E69 RID: 3689
		internal const string IssueSessionTokenHandlerNotSet = "IssueSessionTokenHandlerNotSet";

		// Token: 0x04000E6A RID: 3690
		internal const string RenewSessionTokenHandlerNotSet = "RenewSessionTokenHandlerNotSet";

		// Token: 0x04000E6B RID: 3691
		internal const string WrongIdentityRenewingToken = "WrongIdentityRenewingToken";

		// Token: 0x04000E6C RID: 3692
		internal const string InvalidRstRequestType = "InvalidRstRequestType";

		// Token: 0x04000E6D RID: 3693
		internal const string NoCloseTargetSpecified = "NoCloseTargetSpecified";

		// Token: 0x04000E6E RID: 3694
		internal const string FailedSspiNegotiation = "FailedSspiNegotiation";

		// Token: 0x04000E6F RID: 3695
		internal const string BadCloseTarget = "BadCloseTarget";

		// Token: 0x04000E70 RID: 3696
		internal const string RenewSessionMissingSupportingToken = "RenewSessionMissingSupportingToken";

		// Token: 0x04000E71 RID: 3697
		internal const string NoRenewTargetSpecified = "NoRenewTargetSpecified";

		// Token: 0x04000E72 RID: 3698
		internal const string BadRenewTarget = "BadRenewTarget";

		// Token: 0x04000E73 RID: 3699
		internal const string BadEncryptedBody = "BadEncryptedBody";

		// Token: 0x04000E74 RID: 3700
		internal const string BadEncryptionState = "BadEncryptionState";

		// Token: 0x04000E75 RID: 3701
		internal const string NoSignaturePartsSpecified = "NoSignaturePartsSpecified";

		// Token: 0x04000E76 RID: 3702
		internal const string NoEncryptionPartsSpecified = "NoEncryptionPartsSpecified";

		// Token: 0x04000E77 RID: 3703
		internal const string SecuritySessionFaultReplyWasSent = "SecuritySessionFaultReplyWasSent";

		// Token: 0x04000E78 RID: 3704
		internal const string InnerListenerFactoryNotSet = "InnerListenerFactoryNotSet";

		// Token: 0x04000E79 RID: 3705
		internal const string SecureConversationBootstrapCannotUseSecureConversation = "SecureConversationBootstrapCannotUseSecureConversation";

		// Token: 0x04000E7A RID: 3706
		internal const string InnerChannelFactoryWasNotSet = "InnerChannelFactoryWasNotSet";

		// Token: 0x04000E7B RID: 3707
		internal const string SecurityProtocolFactoryDoesNotSupportDuplex = "SecurityProtocolFactoryDoesNotSupportDuplex";

		// Token: 0x04000E7C RID: 3708
		internal const string SecurityProtocolFactoryDoesNotSupportRequestReply = "SecurityProtocolFactoryDoesNotSupportRequestReply";

		// Token: 0x04000E7D RID: 3709
		internal const string SecurityProtocolFactoryShouldBeSetBeforeThisOperation = "SecurityProtocolFactoryShouldBeSetBeforeThisOperation";

		// Token: 0x04000E7E RID: 3710
		internal const string SecuritySessionProtocolFactoryShouldBeSetBeforeThisOperation = "SecuritySessionProtocolFactoryShouldBeSetBeforeThisOperation";

		// Token: 0x04000E7F RID: 3711
		internal const string SecureConversationSecurityTokenParametersRequireBootstrapBinding = "SecureConversationSecurityTokenParametersRequireBootstrapBinding";

		// Token: 0x04000E80 RID: 3712
		internal const string PropertySettingErrorOnProtocolFactory = "PropertySettingErrorOnProtocolFactory";

		// Token: 0x04000E81 RID: 3713
		internal const string ProtocolFactoryCouldNotCreateProtocol = "ProtocolFactoryCouldNotCreateProtocol";

		// Token: 0x04000E82 RID: 3714
		internal const string IdentityCheckFailedForOutgoingMessage = "IdentityCheckFailedForOutgoingMessage";

		// Token: 0x04000E83 RID: 3715
		internal const string IdentityCheckFailedForIncomingMessage = "IdentityCheckFailedForIncomingMessage";

		// Token: 0x04000E84 RID: 3716
		internal const string DnsIdentityCheckFailedForIncomingMessageLackOfDnsClaim = "DnsIdentityCheckFailedForIncomingMessageLackOfDnsClaim";

		// Token: 0x04000E85 RID: 3717
		internal const string DnsIdentityCheckFailedForOutgoingMessageLackOfDnsClaim = "DnsIdentityCheckFailedForOutgoingMessageLackOfDnsClaim";

		// Token: 0x04000E86 RID: 3718
		internal const string DnsIdentityCheckFailedForIncomingMessage = "DnsIdentityCheckFailedForIncomingMessage";

		// Token: 0x04000E87 RID: 3719
		internal const string DnsIdentityCheckFailedForOutgoingMessage = "DnsIdentityCheckFailedForOutgoingMessage";

		// Token: 0x04000E88 RID: 3720
		internal const string SerializedTokenVersionUnsupported = "SerializedTokenVersionUnsupported";

		// Token: 0x04000E89 RID: 3721
		internal const string AuthenticatorNotPresentInRSTRCollection = "AuthenticatorNotPresentInRSTRCollection";

		// Token: 0x04000E8A RID: 3722
		internal const string RSTRAuthenticatorHasBadContext = "RSTRAuthenticatorHasBadContext";

		// Token: 0x04000E8B RID: 3723
		internal const string ServerCertificateNotProvided = "ServerCertificateNotProvided";

		// Token: 0x04000E8C RID: 3724
		internal const string RSTRAuthenticatorNotPresent = "RSTRAuthenticatorNotPresent";

		// Token: 0x04000E8D RID: 3725
		internal const string RSTRAuthenticatorIncorrect = "RSTRAuthenticatorIncorrect";

		// Token: 0x04000E8E RID: 3726
		internal const string ClientCertificateNotProvided = "ClientCertificateNotProvided";

		// Token: 0x04000E8F RID: 3727
		internal const string ClientCertificateNotProvidedOnServiceCredentials = "ClientCertificateNotProvidedOnServiceCredentials";

		// Token: 0x04000E90 RID: 3728
		internal const string ClientCertificateNotProvidedOnClientCredentials = "ClientCertificateNotProvidedOnClientCredentials";

		// Token: 0x04000E91 RID: 3729
		internal const string ServiceCertificateNotProvidedOnServiceCredentials = "ServiceCertificateNotProvidedOnServiceCredentials";

		// Token: 0x04000E92 RID: 3730
		internal const string ServiceCertificateNotProvidedOnClientCredentials = "ServiceCertificateNotProvidedOnClientCredentials";

		// Token: 0x04000E93 RID: 3731
		internal const string UserNamePasswordNotProvidedOnClientCredentials = "UserNamePasswordNotProvidedOnClientCredentials";

		// Token: 0x04000E94 RID: 3732
		internal const string ObjectIsReadOnly = "ObjectIsReadOnly";

		// Token: 0x04000E95 RID: 3733
		internal const string EmptyXmlElementError = "EmptyXmlElementError";

		// Token: 0x04000E96 RID: 3734
		internal const string UnexpectedXmlChildNode = "UnexpectedXmlChildNode";

		// Token: 0x04000E97 RID: 3735
		internal const string ContextAlreadyRegistered = "ContextAlreadyRegistered";

		// Token: 0x04000E98 RID: 3736
		internal const string ContextAlreadyRegisteredNoKeyGeneration = "ContextAlreadyRegisteredNoKeyGeneration";

		// Token: 0x04000E99 RID: 3737
		internal const string ContextNotPresent = "ContextNotPresent";

		// Token: 0x04000E9A RID: 3738
		internal const string ContextNotPresentNoKeyGeneration = "ContextNotPresentNoKeyGeneration";

		// Token: 0x04000E9B RID: 3739
		internal const string InvalidSecurityContextCookie = "InvalidSecurityContextCookie";

		// Token: 0x04000E9C RID: 3740
		internal const string SecurityContextNotRegistered = "SecurityContextNotRegistered";

		// Token: 0x04000E9D RID: 3741
		internal const string SecurityContextExpired = "SecurityContextExpired";

		// Token: 0x04000E9E RID: 3742
		internal const string SecurityContextExpiredNoKeyGeneration = "SecurityContextExpiredNoKeyGeneration";

		// Token: 0x04000E9F RID: 3743
		internal const string NoSecurityContextIdentifier = "NoSecurityContextIdentifier";

		// Token: 0x04000EA0 RID: 3744
		internal const string MessageMustHaveViaOrToSetForSendingOnServerSideCompositeDuplexChannels = "MessageMustHaveViaOrToSetForSendingOnServerSideCompositeDuplexChannels";

		// Token: 0x04000EA1 RID: 3745
		internal const string MessageViaCannotBeAddressedToAnonymousOnServerSideCompositeDuplexChannels = "MessageViaCannotBeAddressedToAnonymousOnServerSideCompositeDuplexChannels";

		// Token: 0x04000EA2 RID: 3746
		internal const string MessageToCannotBeAddressedToAnonymousOnServerSideCompositeDuplexChannels = "MessageToCannotBeAddressedToAnonymousOnServerSideCompositeDuplexChannels";

		// Token: 0x04000EA3 RID: 3747
		internal const string SecurityBindingNotSetUpToProcessOutgoingMessages = "SecurityBindingNotSetUpToProcessOutgoingMessages";

		// Token: 0x04000EA4 RID: 3748
		internal const string SecurityBindingNotSetUpToProcessIncomingMessages = "SecurityBindingNotSetUpToProcessIncomingMessages";

		// Token: 0x04000EA5 RID: 3749
		internal const string TokenProviderCannotGetTokensForTarget = "TokenProviderCannotGetTokensForTarget";

		// Token: 0x04000EA6 RID: 3750
		internal const string UnsupportedKeyDerivationAlgorithm = "UnsupportedKeyDerivationAlgorithm";

		// Token: 0x04000EA7 RID: 3751
		internal const string CannotFindCorrelationStateForApplyingSecurity = "CannotFindCorrelationStateForApplyingSecurity";

		// Token: 0x04000EA8 RID: 3752
		internal const string ReplyWasNotSignedWithRequiredSigningToken = "ReplyWasNotSignedWithRequiredSigningToken";

		// Token: 0x04000EA9 RID: 3753
		internal const string EncryptionNotExpected = "EncryptionNotExpected";

		// Token: 0x04000EAA RID: 3754
		internal const string SignatureNotExpected = "SignatureNotExpected";

		// Token: 0x04000EAB RID: 3755
		internal const string InvalidQName = "InvalidQName";

		// Token: 0x04000EAC RID: 3756
		internal const string UnknownICryptoType = "UnknownICryptoType";

		// Token: 0x04000EAD RID: 3757
		internal const string SameProtocolFactoryCannotBeSetForBothDuplexDirections = "SameProtocolFactoryCannotBeSetForBothDuplexDirections";

		// Token: 0x04000EAE RID: 3758
		internal const string SuiteDoesNotAcceptAlgorithm = "SuiteDoesNotAcceptAlgorithm";

		// Token: 0x04000EAF RID: 3759
		internal const string TokenDoesNotSupportKeyIdentifierClauseCreation = "TokenDoesNotSupportKeyIdentifierClauseCreation";

		// Token: 0x04000EB0 RID: 3760
		internal const string UnableToCreateICryptoFromTokenForSignatureVerification = "UnableToCreateICryptoFromTokenForSignatureVerification";

		// Token: 0x04000EB1 RID: 3761
		internal const string MessageSecurityVerificationFailed = "MessageSecurityVerificationFailed";

		// Token: 0x04000EB2 RID: 3762
		internal const string TransportSecurityRequireToHeader = "TransportSecurityRequireToHeader";

		// Token: 0x04000EB3 RID: 3763
		internal const string TransportSecuredMessageMissingToHeader = "TransportSecuredMessageMissingToHeader";

		// Token: 0x04000EB4 RID: 3764
		internal const string UnsignedToHeaderInTransportSecuredMessage = "UnsignedToHeaderInTransportSecuredMessage";

		// Token: 0x04000EB5 RID: 3765
		internal const string TransportSecuredMessageHasMoreThanOneToHeader = "TransportSecuredMessageHasMoreThanOneToHeader";

		// Token: 0x04000EB6 RID: 3766
		internal const string TokenNotExpectedInSecurityHeader = "TokenNotExpectedInSecurityHeader";

		// Token: 0x04000EB7 RID: 3767
		internal const string CannotFindCert = "CannotFindCert";

		// Token: 0x04000EB8 RID: 3768
		internal const string CannotFindCertForTarget = "CannotFindCertForTarget";

		// Token: 0x04000EB9 RID: 3769
		internal const string FoundMultipleCerts = "FoundMultipleCerts";

		// Token: 0x04000EBA RID: 3770
		internal const string FoundMultipleCertsForTarget = "FoundMultipleCertsForTarget";

		// Token: 0x04000EBB RID: 3771
		internal const string MissingKeyInfoInEncryptedKey = "MissingKeyInfoInEncryptedKey";

		// Token: 0x04000EBC RID: 3772
		internal const string EncryptedKeyWasNotEncryptedWithTheRequiredEncryptingToken = "EncryptedKeyWasNotEncryptedWithTheRequiredEncryptingToken";

		// Token: 0x04000EBD RID: 3773
		internal const string MessageWasNotEncryptedWithTheRequiredEncryptingToken = "MessageWasNotEncryptedWithTheRequiredEncryptingToken";

		// Token: 0x04000EBE RID: 3774
		internal const string TimestampMustOccurFirstInSecurityHeaderLayout = "TimestampMustOccurFirstInSecurityHeaderLayout";

		// Token: 0x04000EBF RID: 3775
		internal const string TimestampMustOccurLastInSecurityHeaderLayout = "TimestampMustOccurLastInSecurityHeaderLayout";

		// Token: 0x04000EC0 RID: 3776
		internal const string AtMostOnePrimarySignatureInReceiveSecurityHeader = "AtMostOnePrimarySignatureInReceiveSecurityHeader";

		// Token: 0x04000EC1 RID: 3777
		internal const string SigningTokenHasNoKeys = "SigningTokenHasNoKeys";

		// Token: 0x04000EC2 RID: 3778
		internal const string SigningTokenHasNoKeysSupportingTheAlgorithmSuite = "SigningTokenHasNoKeysSupportingTheAlgorithmSuite";

		// Token: 0x04000EC3 RID: 3779
		internal const string DelayedSecurityApplicationAlreadyCompleted = "DelayedSecurityApplicationAlreadyCompleted";

		// Token: 0x04000EC4 RID: 3780
		internal const string UnableToResolveKeyInfoClauseInDerivedKeyToken = "UnableToResolveKeyInfoClauseInDerivedKeyToken";

		// Token: 0x04000EC5 RID: 3781
		internal const string UnableToDeriveKeyFromKeyInfoClause = "UnableToDeriveKeyFromKeyInfoClause";

		// Token: 0x04000EC6 RID: 3782
		internal const string UnableToResolveKeyInfoForVerifyingSignature = "UnableToResolveKeyInfoForVerifyingSignature";

		// Token: 0x04000EC7 RID: 3783
		internal const string UnableToResolveKeyInfoForUnwrappingToken = "UnableToResolveKeyInfoForUnwrappingToken";

		// Token: 0x04000EC8 RID: 3784
		internal const string UnableToResolveKeyInfoForDecryption = "UnableToResolveKeyInfoForDecryption";

		// Token: 0x04000EC9 RID: 3785
		internal const string EmptyBase64Attribute = "EmptyBase64Attribute";

		// Token: 0x04000ECA RID: 3786
		internal const string RequiredSecurityHeaderElementNotSigned = "RequiredSecurityHeaderElementNotSigned";

		// Token: 0x04000ECB RID: 3787
		internal const string RequiredSecurityTokenNotSigned = "RequiredSecurityTokenNotSigned";

		// Token: 0x04000ECC RID: 3788
		internal const string RequiredSecurityTokenNotEncrypted = "RequiredSecurityTokenNotEncrypted";

		// Token: 0x04000ECD RID: 3789
		internal const string MessageBodyOperationNotValidInBodyState = "MessageBodyOperationNotValidInBodyState";

		// Token: 0x04000ECE RID: 3790
		internal const string EncryptedKeyWithReferenceListNotAllowed = "EncryptedKeyWithReferenceListNotAllowed";

		// Token: 0x04000ECF RID: 3791
		internal const string UnableToFindTokenAuthenticator = "UnableToFindTokenAuthenticator";

		// Token: 0x04000ED0 RID: 3792
		internal const string NoPartsOfMessageMatchedPartsToSign = "NoPartsOfMessageMatchedPartsToSign";

		// Token: 0x04000ED1 RID: 3793
		internal const string BasicTokenCannotBeWrittenWithoutEncryption = "BasicTokenCannotBeWrittenWithoutEncryption";

		// Token: 0x04000ED2 RID: 3794
		internal const string DuplicateIdInMessageToBeVerified = "DuplicateIdInMessageToBeVerified";

		// Token: 0x04000ED3 RID: 3795
		internal const string UnsupportedCanonicalizationAlgorithm = "UnsupportedCanonicalizationAlgorithm";

		// Token: 0x04000ED4 RID: 3796
		internal const string NoKeyInfoInEncryptedItemToFindDecryptingToken = "NoKeyInfoInEncryptedItemToFindDecryptingToken";

		// Token: 0x04000ED5 RID: 3797
		internal const string NoKeyInfoInSignatureToFindVerificationToken = "NoKeyInfoInSignatureToFindVerificationToken";

		// Token: 0x04000ED6 RID: 3798
		internal const string SecurityHeaderIsEmpty = "SecurityHeaderIsEmpty";

		// Token: 0x04000ED7 RID: 3799
		internal const string EncryptionMethodMissingInEncryptedData = "EncryptionMethodMissingInEncryptedData";

		// Token: 0x04000ED8 RID: 3800
		internal const string EncryptedHeaderAttributeMismatch = "EncryptedHeaderAttributeMismatch";

		// Token: 0x04000ED9 RID: 3801
		internal const string AtMostOneReferenceListIsSupportedWithDefaultPolicyCheck = "AtMostOneReferenceListIsSupportedWithDefaultPolicyCheck";

		// Token: 0x04000EDA RID: 3802
		internal const string AtMostOneSignatureIsSupportedWithDefaultPolicyCheck = "AtMostOneSignatureIsSupportedWithDefaultPolicyCheck";

		// Token: 0x04000EDB RID: 3803
		internal const string UnexpectedEncryptedElementInSecurityHeader = "UnexpectedEncryptedElementInSecurityHeader";

		// Token: 0x04000EDC RID: 3804
		internal const string MissingIdInEncryptedElement = "MissingIdInEncryptedElement";

		// Token: 0x04000EDD RID: 3805
		internal const string TokenManagerCannotCreateTokenReference = "TokenManagerCannotCreateTokenReference";

		// Token: 0x04000EDE RID: 3806
		internal const string TimestampToSignHasNoId = "TimestampToSignHasNoId";

		// Token: 0x04000EDF RID: 3807
		internal const string EncryptedHeaderXmlMustHaveId = "EncryptedHeaderXmlMustHaveId";

		// Token: 0x04000EE0 RID: 3808
		internal const string UnableToResolveDataReference = "UnableToResolveDataReference";

		// Token: 0x04000EE1 RID: 3809
		internal const string TimestampAlreadySetForSecurityHeader = "TimestampAlreadySetForSecurityHeader";

		// Token: 0x04000EE2 RID: 3810
		internal const string DuplicateTimestampInSecurityHeader = "DuplicateTimestampInSecurityHeader";

		// Token: 0x04000EE3 RID: 3811
		internal const string MismatchInSecurityOperationToken = "MismatchInSecurityOperationToken";

		// Token: 0x04000EE4 RID: 3812
		internal const string UnableToCreateSymmetricAlgorithmFromToken = "UnableToCreateSymmetricAlgorithmFromToken";

		// Token: 0x04000EE5 RID: 3813
		internal const string UnknownEncodingInBinarySecurityToken = "UnknownEncodingInBinarySecurityToken";

		// Token: 0x04000EE6 RID: 3814
		internal const string UnableToResolveReferenceUriForSignature = "UnableToResolveReferenceUriForSignature";

		// Token: 0x04000EE7 RID: 3815
		internal const string NoTimestampAvailableInSecurityHeaderToDoReplayDetection = "NoTimestampAvailableInSecurityHeaderToDoReplayDetection";

		// Token: 0x04000EE8 RID: 3816
		internal const string NoSignatureAvailableInSecurityHeaderToDoReplayDetection = "NoSignatureAvailableInSecurityHeaderToDoReplayDetection";

		// Token: 0x04000EE9 RID: 3817
		internal const string CouldNotFindNamespaceForPrefix = "CouldNotFindNamespaceForPrefix";

		// Token: 0x04000EEA RID: 3818
		internal const string DerivedKeyCannotDeriveFromSecret = "DerivedKeyCannotDeriveFromSecret";

		// Token: 0x04000EEB RID: 3819
		internal const string DerivedKeyPosAndGenBothSpecified = "DerivedKeyPosAndGenBothSpecified";

		// Token: 0x04000EEC RID: 3820
		internal const string DerivedKeyPosAndGenNotSpecified = "DerivedKeyPosAndGenNotSpecified";

		// Token: 0x04000EED RID: 3821
		internal const string DerivedKeyTokenRequiresTokenReference = "DerivedKeyTokenRequiresTokenReference";

		// Token: 0x04000EEE RID: 3822
		internal const string DerivedKeyLengthTooLong = "DerivedKeyLengthTooLong";

		// Token: 0x04000EEF RID: 3823
		internal const string DerivedKeyLengthSpecifiedInImplicitDerivedKeyClauseTooLong = "DerivedKeyLengthSpecifiedInImplicitDerivedKeyClauseTooLong";

		// Token: 0x04000EF0 RID: 3824
		internal const string DerivedKeyInvalidOffsetSpecified = "DerivedKeyInvalidOffsetSpecified";

		// Token: 0x04000EF1 RID: 3825
		internal const string DerivedKeyInvalidGenerationSpecified = "DerivedKeyInvalidGenerationSpecified";

		// Token: 0x04000EF2 RID: 3826
		internal const string ChildNodeTypeMissing = "ChildNodeTypeMissing";

		// Token: 0x04000EF3 RID: 3827
		internal const string NoLicenseXml = "NoLicenseXml";

		// Token: 0x04000EF4 RID: 3828
		internal const string UnsupportedBinaryEncoding = "UnsupportedBinaryEncoding";

		// Token: 0x04000EF5 RID: 3829
		internal const string BadKeyEncryptionAlgorithm = "BadKeyEncryptionAlgorithm";

		// Token: 0x04000EF6 RID: 3830
		internal const string InvalidAsyncResult = "InvalidAsyncResult";

		// Token: 0x04000EF7 RID: 3831
		internal const string UnableToCreateTokenReference = "UnableToCreateTokenReference";

		// Token: 0x04000EF8 RID: 3832
		internal const string ConfigNull = "ConfigNull";

		// Token: 0x04000EF9 RID: 3833
		internal const string NonceLengthTooShort = "NonceLengthTooShort";

		// Token: 0x04000EFA RID: 3834
		internal const string NoBinaryNegoToSend = "NoBinaryNegoToSend";

		// Token: 0x04000EFB RID: 3835
		internal const string BadSecurityNegotiationContext = "BadSecurityNegotiationContext";

		// Token: 0x04000EFC RID: 3836
		internal const string NoBinaryNegoToReceive = "NoBinaryNegoToReceive";

		// Token: 0x04000EFD RID: 3837
		internal const string ProofTokenWasNotWrappedCorrectly = "ProofTokenWasNotWrappedCorrectly";

		// Token: 0x04000EFE RID: 3838
		internal const string NoServiceTokenReceived = "NoServiceTokenReceived";

		// Token: 0x04000EFF RID: 3839
		internal const string InvalidSspiNegotiation = "InvalidSspiNegotiation";

		// Token: 0x04000F00 RID: 3840
		internal const string CannotAuthenticateServer = "CannotAuthenticateServer";

		// Token: 0x04000F01 RID: 3841
		internal const string IncorrectBinaryNegotiationValueType = "IncorrectBinaryNegotiationValueType";

		// Token: 0x04000F02 RID: 3842
		internal const string ChannelNotOpen = "ChannelNotOpen";

		// Token: 0x04000F03 RID: 3843
		internal const string FailToRecieveReplyFromNegotiation = "FailToRecieveReplyFromNegotiation";

		// Token: 0x04000F04 RID: 3844
		internal const string MessageSecurityVersionOutOfRange = "MessageSecurityVersionOutOfRange";

		// Token: 0x04000F05 RID: 3845
		internal const string CreationTimeUtcIsAfterExpiryTime = "CreationTimeUtcIsAfterExpiryTime";

		// Token: 0x04000F06 RID: 3846
		internal const string NegotiationStateAlreadyPresent = "NegotiationStateAlreadyPresent";

		// Token: 0x04000F07 RID: 3847
		internal const string CannotFindNegotiationState = "CannotFindNegotiationState";

		// Token: 0x04000F08 RID: 3848
		internal const string OutputNotExpected = "OutputNotExpected";

		// Token: 0x04000F09 RID: 3849
		internal const string SessionClosedBeforeDone = "SessionClosedBeforeDone";

		// Token: 0x04000F0A RID: 3850
		internal const string CacheQuotaReached = "CacheQuotaReached";

		// Token: 0x04000F0B RID: 3851
		internal const string NoServerX509TokenProvider = "NoServerX509TokenProvider";

		// Token: 0x04000F0C RID: 3852
		internal const string UnexpectedBinarySecretType = "UnexpectedBinarySecretType";

		// Token: 0x04000F0D RID: 3853
		internal const string UnsupportedPasswordType = "UnsupportedPasswordType";

		// Token: 0x04000F0E RID: 3854
		internal const string UnrecognizedIdentityPropertyType = "UnrecognizedIdentityPropertyType";

		// Token: 0x04000F0F RID: 3855
		internal const string UnableToDemuxChannel = "UnableToDemuxChannel";

		// Token: 0x04000F10 RID: 3856
		internal const string EndpointNotFound = "EndpointNotFound";

		// Token: 0x04000F11 RID: 3857
		internal const string MaxReceivedMessageSizeMustBeInIntegerRange = "MaxReceivedMessageSizeMustBeInIntegerRange";

		// Token: 0x04000F12 RID: 3858
		internal const string MaxBufferSizeMustMatchMaxReceivedMessageSize = "MaxBufferSizeMustMatchMaxReceivedMessageSize";

		// Token: 0x04000F13 RID: 3859
		internal const string MaxBufferSizeMustNotExceedMaxReceivedMessageSize = "MaxBufferSizeMustNotExceedMaxReceivedMessageSize";

		// Token: 0x04000F14 RID: 3860
		internal const string MessageSizeMustBeInIntegerRange = "MessageSizeMustBeInIntegerRange";

		// Token: 0x04000F15 RID: 3861
		internal const string UriLengthExceedsMaxSupportedSize = "UriLengthExceedsMaxSupportedSize";

		// Token: 0x04000F16 RID: 3862
		internal const string InValidateIdPrefix = "InValidateIdPrefix";

		// Token: 0x04000F17 RID: 3863
		internal const string InValidateId = "InValidateId";

		// Token: 0x04000F18 RID: 3864
		internal const string HttpRegistrationAlreadyExists = "HttpRegistrationAlreadyExists";

		// Token: 0x04000F19 RID: 3865
		internal const string HttpRegistrationAccessDenied = "HttpRegistrationAccessDenied";

		// Token: 0x04000F1A RID: 3866
		internal const string HttpRegistrationPortInUse = "HttpRegistrationPortInUse";

		// Token: 0x04000F1B RID: 3867
		internal const string HttpRegistrationLimitExceeded = "HttpRegistrationLimitExceeded";

		// Token: 0x04000F1C RID: 3868
		internal const string UnexpectedHttpResponseCode = "UnexpectedHttpResponseCode";

		// Token: 0x04000F1D RID: 3869
		internal const string HttpContentLengthIncorrect = "HttpContentLengthIncorrect";

		// Token: 0x04000F1E RID: 3870
		internal const string OneWayUnexpectedResponse = "OneWayUnexpectedResponse";

		// Token: 0x04000F1F RID: 3871
		internal const string MissingContentType = "MissingContentType";

		// Token: 0x04000F20 RID: 3872
		internal const string DuplexChannelAbortedDuringOpen = "DuplexChannelAbortedDuringOpen";

		// Token: 0x04000F21 RID: 3873
		internal const string OperationAbortedDuringConnectionEstablishment = "OperationAbortedDuringConnectionEstablishment";

		// Token: 0x04000F22 RID: 3874
		internal const string HttpAddressingNoneHeaderOnWire = "HttpAddressingNoneHeaderOnWire";

		// Token: 0x04000F23 RID: 3875
		internal const string MessageXmlProtocolError = "MessageXmlProtocolError";

		// Token: 0x04000F24 RID: 3876
		internal const string TcpV4AddressInvalid = "TcpV4AddressInvalid";

		// Token: 0x04000F25 RID: 3877
		internal const string TcpV6AddressInvalid = "TcpV6AddressInvalid";

		// Token: 0x04000F26 RID: 3878
		internal const string UniquePortNotAvailable = "UniquePortNotAvailable";

		// Token: 0x04000F27 RID: 3879
		internal const string TcpAddressInUse = "TcpAddressInUse";

		// Token: 0x04000F28 RID: 3880
		internal const string TcpConnectNoBufs = "TcpConnectNoBufs";

		// Token: 0x04000F29 RID: 3881
		internal const string InsufficentMemory = "InsufficentMemory";

		// Token: 0x04000F2A RID: 3882
		internal const string TcpConnectError = "TcpConnectError";

		// Token: 0x04000F2B RID: 3883
		internal const string TcpConnectErrorWithTimeSpan = "TcpConnectErrorWithTimeSpan";

		// Token: 0x04000F2C RID: 3884
		internal const string TcpListenError = "TcpListenError";

		// Token: 0x04000F2D RID: 3885
		internal const string TcpTransferError = "TcpTransferError";

		// Token: 0x04000F2E RID: 3886
		internal const string TcpTransferErrorWithIP = "TcpTransferErrorWithIP";

		// Token: 0x04000F2F RID: 3887
		internal const string TcpLocalConnectionAborted = "TcpLocalConnectionAborted";

		// Token: 0x04000F30 RID: 3888
		internal const string HttpResponseAborted = "HttpResponseAborted";

		// Token: 0x04000F31 RID: 3889
		internal const string TcpConnectionResetError = "TcpConnectionResetError";

		// Token: 0x04000F32 RID: 3890
		internal const string TcpConnectionResetErrorWithIP = "TcpConnectionResetErrorWithIP";

		// Token: 0x04000F33 RID: 3891
		internal const string TcpConnectionTimedOut = "TcpConnectionTimedOut";

		// Token: 0x04000F34 RID: 3892
		internal const string TcpConnectionTimedOutWithIP = "TcpConnectionTimedOutWithIP";

		// Token: 0x04000F35 RID: 3893
		internal const string SocketConnectionDisposed = "SocketConnectionDisposed";

		// Token: 0x04000F36 RID: 3894
		internal const string SocketListenerDisposed = "SocketListenerDisposed";

		// Token: 0x04000F37 RID: 3895
		internal const string SocketListenerNotListening = "SocketListenerNotListening";

		// Token: 0x04000F38 RID: 3896
		internal const string DuplexSessionListenerNotFound = "DuplexSessionListenerNotFound";

		// Token: 0x04000F39 RID: 3897
		internal const string HttpTargetNameDictionaryConflict = "HttpTargetNameDictionaryConflict";

		// Token: 0x04000F3A RID: 3898
		internal const string HttpContentTypeHeaderRequired = "HttpContentTypeHeaderRequired";

		// Token: 0x04000F3B RID: 3899
		internal const string ContentTypeMismatch = "ContentTypeMismatch";

		// Token: 0x04000F3C RID: 3900
		internal const string ResponseContentTypeMismatch = "ResponseContentTypeMismatch";

		// Token: 0x04000F3D RID: 3901
		internal const string ResponseContentTypeNotSupported = "ResponseContentTypeNotSupported";

		// Token: 0x04000F3E RID: 3902
		internal const string HttpToMustEqualVia = "HttpToMustEqualVia";

		// Token: 0x04000F3F RID: 3903
		internal const string NullReferenceOnHttpResponse = "NullReferenceOnHttpResponse";

		// Token: 0x04000F40 RID: 3904
		internal const string FramingContentTypeMismatch = "FramingContentTypeMismatch";

		// Token: 0x04000F41 RID: 3905
		internal const string FramingFaultUnrecognized = "FramingFaultUnrecognized";

		// Token: 0x04000F42 RID: 3906
		internal const string FramingContentTypeTooLongFault = "FramingContentTypeTooLongFault";

		// Token: 0x04000F43 RID: 3907
		internal const string FramingViaTooLongFault = "FramingViaTooLongFault";

		// Token: 0x04000F44 RID: 3908
		internal const string FramingModeNotSupportedFault = "FramingModeNotSupportedFault";

		// Token: 0x04000F45 RID: 3909
		internal const string FramingVersionNotSupportedFault = "FramingVersionNotSupportedFault";

		// Token: 0x04000F46 RID: 3910
		internal const string FramingUpgradeInvalid = "FramingUpgradeInvalid";

		// Token: 0x04000F47 RID: 3911
		internal const string SecurityServerTooBusy = "SecurityServerTooBusy";

		// Token: 0x04000F48 RID: 3912
		internal const string SecurityEndpointNotFound = "SecurityEndpointNotFound";

		// Token: 0x04000F49 RID: 3913
		internal const string ServerTooBusy = "ServerTooBusy";

		// Token: 0x04000F4A RID: 3914
		internal const string UpgradeProtocolNotSupported = "UpgradeProtocolNotSupported";

		// Token: 0x04000F4B RID: 3915
		internal const string UpgradeRequestToNonupgradableService = "UpgradeRequestToNonupgradableService";

		// Token: 0x04000F4C RID: 3916
		internal const string PreambleAckIncorrect = "PreambleAckIncorrect";

		// Token: 0x04000F4D RID: 3917
		internal const string PreambleAckIncorrectMaybeHttp = "PreambleAckIncorrectMaybeHttp";

		// Token: 0x04000F4E RID: 3918
		internal const string StreamError = "StreamError";

		// Token: 0x04000F4F RID: 3919
		internal const string ServerRejectedUpgradeRequest = "ServerRejectedUpgradeRequest";

		// Token: 0x04000F50 RID: 3920
		internal const string ServerRejectedSessionPreamble = "ServerRejectedSessionPreamble";

		// Token: 0x04000F51 RID: 3921
		internal const string UnableToResolveHost = "UnableToResolveHost";

		// Token: 0x04000F52 RID: 3922
		internal const string HttpRequiresSingleAuthScheme = "HttpRequiresSingleAuthScheme";

		// Token: 0x04000F53 RID: 3923
		internal const string HttpAuthSchemeCannotBeNone = "HttpAuthSchemeCannotBeNone";

		// Token: 0x04000F54 RID: 3924
		internal const string HttpProxyRequiresSingleAuthScheme = "HttpProxyRequiresSingleAuthScheme";

		// Token: 0x04000F55 RID: 3925
		internal const string HttpMutualAuthNotSatisfied = "HttpMutualAuthNotSatisfied";

		// Token: 0x04000F56 RID: 3926
		internal const string HttpAuthorizationFailed = "HttpAuthorizationFailed";

		// Token: 0x04000F57 RID: 3927
		internal const string HttpAuthenticationFailed = "HttpAuthenticationFailed";

		// Token: 0x04000F58 RID: 3928
		internal const string HttpAuthorizationForbidden = "HttpAuthorizationForbidden";

		// Token: 0x04000F59 RID: 3929
		internal const string InvalidUriScheme = "InvalidUriScheme";

		// Token: 0x04000F5A RID: 3930
		internal const string HttpAuthSchemeAndClientCert = "HttpAuthSchemeAndClientCert";

		// Token: 0x04000F5B RID: 3931
		internal const string NoTransportManagerForUri = "NoTransportManagerForUri";

		// Token: 0x04000F5C RID: 3932
		internal const string ListenerFactoryNotRegistered = "ListenerFactoryNotRegistered";

		// Token: 0x04000F5D RID: 3933
		internal const string HttpsExplicitIdentity = "HttpsExplicitIdentity";

		// Token: 0x04000F5E RID: 3934
		internal const string HttpsIdentityMultipleCerts = "HttpsIdentityMultipleCerts";

		// Token: 0x04000F5F RID: 3935
		internal const string HttpsServerCertThumbprintMismatch = "HttpsServerCertThumbprintMismatch";

		// Token: 0x04000F60 RID: 3936
		internal const string DuplicateRegistration = "DuplicateRegistration";

		// Token: 0x04000F61 RID: 3937
		internal const string SecureChannelFailure = "SecureChannelFailure";

		// Token: 0x04000F62 RID: 3938
		internal const string TrustFailure = "TrustFailure";

		// Token: 0x04000F63 RID: 3939
		internal const string NoCompatibleTransportManagerForUri = "NoCompatibleTransportManagerForUri";

		// Token: 0x04000F64 RID: 3940
		internal const string HttpSpnNotFound = "HttpSpnNotFound";

		// Token: 0x04000F65 RID: 3941
		internal const string StreamMutualAuthNotSatisfied = "StreamMutualAuthNotSatisfied";

		// Token: 0x04000F66 RID: 3942
		internal const string TransferModeNotSupported = "TransferModeNotSupported";

		// Token: 0x04000F67 RID: 3943
		internal const string InvalidTokenProvided = "InvalidTokenProvided";

		// Token: 0x04000F68 RID: 3944
		internal const string NoUserNameTokenProvided = "NoUserNameTokenProvided";

		// Token: 0x04000F69 RID: 3945
		internal const string RemoteIdentityFailedVerification = "RemoteIdentityFailedVerification";

		// Token: 0x04000F6A RID: 3946
		internal const string UseDefaultWebProxyCantBeUsedWithExplicitProxyAddress = "UseDefaultWebProxyCantBeUsedWithExplicitProxyAddress";

		// Token: 0x04000F6B RID: 3947
		internal const string ProxyImpersonationLevelMismatch = "ProxyImpersonationLevelMismatch";

		// Token: 0x04000F6C RID: 3948
		internal const string ProxyAuthenticationLevelMismatch = "ProxyAuthenticationLevelMismatch";

		// Token: 0x04000F6D RID: 3949
		internal const string CredentialDisallowsNtlm = "CredentialDisallowsNtlm";

		// Token: 0x04000F6E RID: 3950
		internal const string DigestExplicitCredsImpersonationLevel = "DigestExplicitCredsImpersonationLevel";

		// Token: 0x04000F6F RID: 3951
		internal const string UriGeneratorSchemeMustNotBeEmpty = "UriGeneratorSchemeMustNotBeEmpty";

		// Token: 0x04000F70 RID: 3952
		internal const string UnsupportedSslProtectionLevel = "UnsupportedSslProtectionLevel";

		// Token: 0x04000F71 RID: 3953
		internal const string HttpNoTrackingService = "HttpNoTrackingService";

		// Token: 0x04000F72 RID: 3954
		internal const string HttpNetnameDeleted = "HttpNetnameDeleted";

		// Token: 0x04000F73 RID: 3955
		internal const string TimeoutServiceChannelConcurrentOpen1 = "TimeoutServiceChannelConcurrentOpen1";

		// Token: 0x04000F74 RID: 3956
		internal const string TimeoutServiceChannelConcurrentOpen2 = "TimeoutServiceChannelConcurrentOpen2";

		// Token: 0x04000F75 RID: 3957
		internal const string TimeSpanMustbeGreaterThanTimeSpanZero = "TimeSpanMustbeGreaterThanTimeSpanZero";

		// Token: 0x04000F76 RID: 3958
		internal const string TimeSpanCannotBeLessThanTimeSpanZero = "TimeSpanCannotBeLessThanTimeSpanZero";

		// Token: 0x04000F77 RID: 3959
		internal const string ValueMustBeNonNegative = "ValueMustBeNonNegative";

		// Token: 0x04000F78 RID: 3960
		internal const string ValueMustBePositive = "ValueMustBePositive";

		// Token: 0x04000F79 RID: 3961
		internal const string ValueMustBeGreaterThanZero = "ValueMustBeGreaterThanZero";

		// Token: 0x04000F7A RID: 3962
		internal const string ValueMustBeInRange = "ValueMustBeInRange";

		// Token: 0x04000F7B RID: 3963
		internal const string OffsetExceedsBufferBound = "OffsetExceedsBufferBound";

		// Token: 0x04000F7C RID: 3964
		internal const string OffsetExceedsBufferSize = "OffsetExceedsBufferSize";

		// Token: 0x04000F7D RID: 3965
		internal const string SizeExceedsRemainingBufferSpace = "SizeExceedsRemainingBufferSpace";

		// Token: 0x04000F7E RID: 3966
		internal const string SpaceNeededExceedsMessageFrameOffset = "SpaceNeededExceedsMessageFrameOffset";

		// Token: 0x04000F7F RID: 3967
		internal const string FaultConverterDidNotCreateFaultMessage = "FaultConverterDidNotCreateFaultMessage";

		// Token: 0x04000F80 RID: 3968
		internal const string FaultConverterCreatedFaultMessage = "FaultConverterCreatedFaultMessage";

		// Token: 0x04000F81 RID: 3969
		internal const string FaultConverterDidNotCreateException = "FaultConverterDidNotCreateException";

		// Token: 0x04000F82 RID: 3970
		internal const string FaultConverterCreatedException = "FaultConverterCreatedException";

		// Token: 0x04000F83 RID: 3971
		internal const string InfoCardInvalidChain = "InfoCardInvalidChain";

		// Token: 0x04000F84 RID: 3972
		internal const string FullTrustOnlyBindingElementSecurityCheck1 = "FullTrustOnlyBindingElementSecurityCheck1";

		// Token: 0x04000F85 RID: 3973
		internal const string FullTrustOnlyBindingElementSecurityCheckWSHttpBinding1 = "FullTrustOnlyBindingElementSecurityCheckWSHttpBinding1";

		// Token: 0x04000F86 RID: 3974
		internal const string FullTrustOnlyBindingSecurityCheck1 = "FullTrustOnlyBindingSecurityCheck1";

		// Token: 0x04000F87 RID: 3975
		internal const string PartialTrustServiceCtorNotVisible = "PartialTrustServiceCtorNotVisible";

		// Token: 0x04000F88 RID: 3976
		internal const string PartialTrustServiceMethodNotVisible = "PartialTrustServiceMethodNotVisible";

		// Token: 0x04000F89 RID: 3977
		internal const string PartialTrustPerformanceCountersNotEnabled = "PartialTrustPerformanceCountersNotEnabled";

		// Token: 0x04000F8A RID: 3978
		internal const string EnsureCategoriesExistFailedPermission = "EnsureCategoriesExistFailedPermission";

		// Token: 0x04000F8B RID: 3979
		internal const string PartialTrustWMINotEnabled = "PartialTrustWMINotEnabled";

		// Token: 0x04000F8C RID: 3980
		internal const string PartialTrustMessageLoggingNotEnabled = "PartialTrustMessageLoggingNotEnabled";

		// Token: 0x04000F8D RID: 3981
		internal const string ScopeNameMustBeSpecified = "ScopeNameMustBeSpecified";

		// Token: 0x04000F8E RID: 3982
		internal const string ProviderCannotBeEmptyString = "ProviderCannotBeEmptyString";

		// Token: 0x04000F8F RID: 3983
		internal const string CannotSetNameOnTheInvalidKey = "CannotSetNameOnTheInvalidKey";

		// Token: 0x04000F90 RID: 3984
		internal const string UnsupportedMessageQueryResultType = "UnsupportedMessageQueryResultType";

		// Token: 0x04000F91 RID: 3985
		internal const string CannotRepresentResultAsNodeset = "CannotRepresentResultAsNodeset";

		// Token: 0x04000F92 RID: 3986
		internal const string MessageNotInLockedState = "MessageNotInLockedState";

		// Token: 0x04000F93 RID: 3987
		internal const string MessageValidityExpired = "MessageValidityExpired";

		// Token: 0x04000F94 RID: 3988
		internal const string UnsupportedUpgradeInitiator = "UnsupportedUpgradeInitiator";

		// Token: 0x04000F95 RID: 3989
		internal const string UnsupportedUpgradeAcceptor = "UnsupportedUpgradeAcceptor";

		// Token: 0x04000F96 RID: 3990
		internal const string StreamUpgradeUnsupportedChannelBindingKind = "StreamUpgradeUnsupportedChannelBindingKind";

		// Token: 0x04000F97 RID: 3991
		internal const string ExtendedProtectionNotSupported = "ExtendedProtectionNotSupported";

		// Token: 0x04000F98 RID: 3992
		internal const string ExtendedProtectionPolicyBasicAuthNotSupported = "ExtendedProtectionPolicyBasicAuthNotSupported";

		// Token: 0x04000F99 RID: 3993
		internal const string ExtendedProtectionPolicyCustomChannelBindingNotSupported = "ExtendedProtectionPolicyCustomChannelBindingNotSupported";

		// Token: 0x04000F9A RID: 3994
		internal const string HttpClientCredentialTypeInvalid = "HttpClientCredentialTypeInvalid";

		// Token: 0x04000F9B RID: 3995
		internal const string SecurityTokenProviderIncludeWindowsGroupsInconsistent = "SecurityTokenProviderIncludeWindowsGroupsInconsistent";

		// Token: 0x04000F9C RID: 3996
		internal const string AuthenticationSchemesCannotBeInheritedFromHost = "AuthenticationSchemesCannotBeInheritedFromHost";

		// Token: 0x04000F9D RID: 3997
		internal const string AuthenticationSchemes_BindingAndHostConflict = "AuthenticationSchemes_BindingAndHostConflict";

		// Token: 0x04000F9E RID: 3998
		internal const string FlagEnumTypeExpected = "FlagEnumTypeExpected";

		// Token: 0x04000F9F RID: 3999
		internal const string InvalidFlagEnumType = "InvalidFlagEnumType";

		// Token: 0x04000FA0 RID: 4000
		internal const string NoAsyncWritePending = "NoAsyncWritePending";

		// Token: 0x04000FA1 RID: 4001
		internal const string FlushBufferAlreadyInUse = "FlushBufferAlreadyInUse";

		// Token: 0x04000FA2 RID: 4002
		internal const string WriteAsyncWithoutFreeBuffer = "WriteAsyncWithoutFreeBuffer";

		// Token: 0x04000FA3 RID: 4003
		internal const string TransportDoesNotSupportCompression = "TransportDoesNotSupportCompression";

		// Token: 0x04000FA4 RID: 4004
		internal const string UnsupportedSecuritySetting = "UnsupportedSecuritySetting";

		// Token: 0x04000FA5 RID: 4005
		internal const string UnsupportedBindingProperty = "UnsupportedBindingProperty";

		// Token: 0x04000FA6 RID: 4006
		internal const string HttpMaxPendingAcceptsTooLargeError = "HttpMaxPendingAcceptsTooLargeError";

		// Token: 0x04000FA7 RID: 4007
		internal const string RequestInitializationTimeoutReached = "RequestInitializationTimeoutReached";

		// Token: 0x04000FA8 RID: 4008
		internal const string UnsupportedTokenImpersonationLevel = "UnsupportedTokenImpersonationLevel";

		// Token: 0x04000FA9 RID: 4009
		internal const string AcksToMustBeSameAsRemoteAddress = "AcksToMustBeSameAsRemoteAddress";

		// Token: 0x04000FAA RID: 4010
		internal const string AcksToMustBeSameAsRemoteAddressReason = "AcksToMustBeSameAsRemoteAddressReason";

		// Token: 0x04000FAB RID: 4011
		internal const string AssertionNotSupported = "AssertionNotSupported";

		// Token: 0x04000FAC RID: 4012
		internal const string CloseOutputSessionErrorReason = "CloseOutputSessionErrorReason";

		// Token: 0x04000FAD RID: 4013
		internal const string ConflictingAddress = "ConflictingAddress";

		// Token: 0x04000FAE RID: 4014
		internal const string ConflictingOffer = "ConflictingOffer";

		// Token: 0x04000FAF RID: 4015
		internal const string CouldNotParseWithAction = "CouldNotParseWithAction";

		// Token: 0x04000FB0 RID: 4016
		internal const string CSRefused = "CSRefused";

		// Token: 0x04000FB1 RID: 4017
		internal const string CSRefusedAcksToMustEqualEndpoint = "CSRefusedAcksToMustEqualEndpoint";

		// Token: 0x04000FB2 RID: 4018
		internal const string CSRefusedAcksToMustEqualReplyTo = "CSRefusedAcksToMustEqualReplyTo";

		// Token: 0x04000FB3 RID: 4019
		internal const string CSRefusedDuplexNoOffer = "CSRefusedDuplexNoOffer";

		// Token: 0x04000FB4 RID: 4020
		internal const string CSRefusedInputOffer = "CSRefusedInputOffer";

		// Token: 0x04000FB5 RID: 4021
		internal const string CSRefusedInvalidIncompleteSequenceBehavior = "CSRefusedInvalidIncompleteSequenceBehavior";

		// Token: 0x04000FB6 RID: 4022
		internal const string CSRefusedNoSTRWSSecurity = "CSRefusedNoSTRWSSecurity";

		// Token: 0x04000FB7 RID: 4023
		internal const string CSRefusedReplyNoOffer = "CSRefusedReplyNoOffer";

		// Token: 0x04000FB8 RID: 4024
		internal const string CSRefusedRequiredSecurityElementMissing = "CSRefusedRequiredSecurityElementMissing";

		// Token: 0x04000FB9 RID: 4025
		internal const string CSRefusedSSLNotSupported = "CSRefusedSSLNotSupported";

		// Token: 0x04000FBA RID: 4026
		internal const string CSRefusedSTRNoWSSecurity = "CSRefusedSTRNoWSSecurity";

		// Token: 0x04000FBB RID: 4027
		internal const string CSRefusedUnexpectedElementAtEndOfCSMessage = "CSRefusedUnexpectedElementAtEndOfCSMessage";

		// Token: 0x04000FBC RID: 4028
		internal const string CSResponseOfferRejected = "CSResponseOfferRejected";

		// Token: 0x04000FBD RID: 4029
		internal const string CSResponseOfferRejectedReason = "CSResponseOfferRejectedReason";

		// Token: 0x04000FBE RID: 4030
		internal const string CSResponseWithInvalidIncompleteSequenceBehavior = "CSResponseWithInvalidIncompleteSequenceBehavior";

		// Token: 0x04000FBF RID: 4031
		internal const string CSResponseWithOffer = "CSResponseWithOffer";

		// Token: 0x04000FC0 RID: 4032
		internal const string CSResponseWithOfferReason = "CSResponseWithOfferReason";

		// Token: 0x04000FC1 RID: 4033
		internal const string CSResponseWithoutOffer = "CSResponseWithoutOffer";

		// Token: 0x04000FC2 RID: 4034
		internal const string CSResponseWithoutOfferReason = "CSResponseWithoutOfferReason";

		// Token: 0x04000FC3 RID: 4035
		internal const string DeliveryAssuranceRequiredNothingFound = "DeliveryAssuranceRequiredNothingFound";

		// Token: 0x04000FC4 RID: 4036
		internal const string DeliveryAssuranceRequired = "DeliveryAssuranceRequired";

		// Token: 0x04000FC5 RID: 4037
		internal const string EarlyRequestTerminateSequence = "EarlyRequestTerminateSequence";

		// Token: 0x04000FC6 RID: 4038
		internal const string EarlySecurityClose = "EarlySecurityClose";

		// Token: 0x04000FC7 RID: 4039
		internal const string EarlySecurityFaulted = "EarlySecurityFaulted";

		// Token: 0x04000FC8 RID: 4040
		internal const string EarlyTerminateSequence = "EarlyTerminateSequence";

		// Token: 0x04000FC9 RID: 4041
		internal const string ElementFound = "ElementFound";

		// Token: 0x04000FCA RID: 4042
		internal const string ElementRequired = "ElementRequired";

		// Token: 0x04000FCB RID: 4043
		internal const string InconsistentLastMsgNumberExceptionString = "InconsistentLastMsgNumberExceptionString";

		// Token: 0x04000FCC RID: 4044
		internal const string InvalidAcknowledgementFaultReason = "InvalidAcknowledgementFaultReason";

		// Token: 0x04000FCD RID: 4045
		internal const string InvalidAcknowledgementReceived = "InvalidAcknowledgementReceived";

		// Token: 0x04000FCE RID: 4046
		internal const string InvalidBufferRemaining = "InvalidBufferRemaining";

		// Token: 0x04000FCF RID: 4047
		internal const string InvalidSequenceNumber = "InvalidSequenceNumber";

		// Token: 0x04000FD0 RID: 4048
		internal const string InvalidSequenceRange = "InvalidSequenceRange";

		// Token: 0x04000FD1 RID: 4049
		internal const string InvalidWsrmResponseChannelNotOpened = "InvalidWsrmResponseChannelNotOpened";

		// Token: 0x04000FD2 RID: 4050
		internal const string InvalidWsrmResponseSessionFaultedExceptionString = "InvalidWsrmResponseSessionFaultedExceptionString";

		// Token: 0x04000FD3 RID: 4051
		internal const string InvalidWsrmResponseSessionFaultedFaultString = "InvalidWsrmResponseSessionFaultedFaultString";

		// Token: 0x04000FD4 RID: 4052
		internal const string LastMessageNumberExceeded = "LastMessageNumberExceeded";

		// Token: 0x04000FD5 RID: 4053
		internal const string LastMessageNumberExceededFaultReason = "LastMessageNumberExceededFaultReason";

		// Token: 0x04000FD6 RID: 4054
		internal const string ManualAddressingNotSupported = "ManualAddressingNotSupported";

		// Token: 0x04000FD7 RID: 4055
		internal const string MaximumRetryCountExceeded = "MaximumRetryCountExceeded";

		// Token: 0x04000FD8 RID: 4056
		internal const string MessageExceptionOccurred = "MessageExceptionOccurred";

		// Token: 0x04000FD9 RID: 4057
		internal const string MessageNumberRollover = "MessageNumberRollover";

		// Token: 0x04000FDA RID: 4058
		internal const string MessageNumberRolloverFaultReason = "MessageNumberRolloverFaultReason";

		// Token: 0x04000FDB RID: 4059
		internal const string MillisecondsNotConvertibleToBindingRange = "MillisecondsNotConvertibleToBindingRange";

		// Token: 0x04000FDC RID: 4060
		internal const string MissingFinalAckExceptionString = "MissingFinalAckExceptionString";

		// Token: 0x04000FDD RID: 4061
		internal const string MissingMessageIdOnWsrmRequest = "MissingMessageIdOnWsrmRequest";

		// Token: 0x04000FDE RID: 4062
		internal const string MissingRelatesToOnWsrmResponseReason = "MissingRelatesToOnWsrmResponseReason";

		// Token: 0x04000FDF RID: 4063
		internal const string MissingReplyToOnWsrmRequest = "MissingReplyToOnWsrmRequest";

		// Token: 0x04000FE0 RID: 4064
		internal const string MultipleVersionsFoundInPolicy = "MultipleVersionsFoundInPolicy";

		// Token: 0x04000FE1 RID: 4065
		internal const string NoActionNoSequenceHeaderReason = "NoActionNoSequenceHeaderReason";

		// Token: 0x04000FE2 RID: 4066
		internal const string NonEmptyWsrmMessageIsEmpty = "NonEmptyWsrmMessageIsEmpty";

		// Token: 0x04000FE3 RID: 4067
		internal const string NonWsrmFeb2005ActionNotSupported = "NonWsrmFeb2005ActionNotSupported";

		// Token: 0x04000FE4 RID: 4068
		internal const string NotAllRepliesAcknowledgedExceptionString = "NotAllRepliesAcknowledgedExceptionString";

		// Token: 0x04000FE5 RID: 4069
		internal const string ReceivedResponseBeforeRequestExceptionString = "ReceivedResponseBeforeRequestExceptionString";

		// Token: 0x04000FE6 RID: 4070
		internal const string ReceivedResponseBeforeRequestFaultString = "ReceivedResponseBeforeRequestFaultString";

		// Token: 0x04000FE7 RID: 4071
		internal const string ReplyMissingAcknowledgement = "ReplyMissingAcknowledgement";

		// Token: 0x04000FE8 RID: 4072
		internal const string ReliableRequestContextAborted = "ReliableRequestContextAborted";

		// Token: 0x04000FE9 RID: 4073
		internal const string RequiredAttributeIsMissing = "RequiredAttributeIsMissing";

		// Token: 0x04000FEA RID: 4074
		internal const string RequiredMillisecondsAttributeIncorrect = "RequiredMillisecondsAttributeIncorrect";

		// Token: 0x04000FEB RID: 4075
		internal const string RMEndpointNotFoundReason = "RMEndpointNotFoundReason";

		// Token: 0x04000FEC RID: 4076
		internal const string SequenceClosedFaultString = "SequenceClosedFaultString";

		// Token: 0x04000FED RID: 4077
		internal const string SequenceTerminatedAddLastToWindowTimedOut = "SequenceTerminatedAddLastToWindowTimedOut";

		// Token: 0x04000FEE RID: 4078
		internal const string SequenceTerminatedBeforeReplySequenceAcked = "SequenceTerminatedBeforeReplySequenceAcked";

		// Token: 0x04000FEF RID: 4079
		internal const string SequenceTerminatedEarlyTerminateSequence = "SequenceTerminatedEarlyTerminateSequence";

		// Token: 0x04000FF0 RID: 4080
		internal const string SequenceTerminatedInactivityTimeoutExceeded = "SequenceTerminatedInactivityTimeoutExceeded";

		// Token: 0x04000FF1 RID: 4081
		internal const string SequenceTerminatedInconsistentLastMsgNumber = "SequenceTerminatedInconsistentLastMsgNumber";

		// Token: 0x04000FF2 RID: 4082
		internal const string SequenceTerminatedMaximumRetryCountExceeded = "SequenceTerminatedMaximumRetryCountExceeded";

		// Token: 0x04000FF3 RID: 4083
		internal const string SequenceTerminatedMissingFinalAck = "SequenceTerminatedMissingFinalAck";

		// Token: 0x04000FF4 RID: 4084
		internal const string SequenceTerminatedOnAbort = "SequenceTerminatedOnAbort";

		// Token: 0x04000FF5 RID: 4085
		internal const string SequenceTerminatedQuotaExceededException = "SequenceTerminatedQuotaExceededException";

		// Token: 0x04000FF6 RID: 4086
		internal const string SequenceTerminatedReliableRequestThrew = "SequenceTerminatedReliableRequestThrew";

		// Token: 0x04000FF7 RID: 4087
		internal const string SequenceTerminatedReplyMissingAcknowledgement = "SequenceTerminatedReplyMissingAcknowledgement";

		// Token: 0x04000FF8 RID: 4088
		internal const string SequenceTerminatedNotAllRepliesAcknowledged = "SequenceTerminatedNotAllRepliesAcknowledged";

		// Token: 0x04000FF9 RID: 4089
		internal const string SequenceTerminatedSessionClosedBeforeDone = "SequenceTerminatedSessionClosedBeforeDone";

		// Token: 0x04000FFA RID: 4090
		internal const string SequenceTerminatedSmallLastMsgNumber = "SequenceTerminatedSmallLastMsgNumber";

		// Token: 0x04000FFB RID: 4091
		internal const string SequenceTerminatedUnexpectedAcknowledgement = "SequenceTerminatedUnexpectedAcknowledgement";

		// Token: 0x04000FFC RID: 4092
		internal const string SequenceTerminatedUnexpectedAckRequested = "SequenceTerminatedUnexpectedAckRequested";

		// Token: 0x04000FFD RID: 4093
		internal const string SequenceTerminatedUnexpectedCloseSequence = "SequenceTerminatedUnexpectedCloseSequence";

		// Token: 0x04000FFE RID: 4094
		internal const string SequenceTerminatedUnexpectedCloseSequenceResponse = "SequenceTerminatedUnexpectedCloseSequenceResponse";

		// Token: 0x04000FFF RID: 4095
		internal const string SequenceTerminatedUnexpectedCS = "SequenceTerminatedUnexpectedCS";

		// Token: 0x04001000 RID: 4096
		internal const string SequenceTerminatedUnexpectedCSOfferId = "SequenceTerminatedUnexpectedCSOfferId";

		// Token: 0x04001001 RID: 4097
		internal const string SequenceTerminatedUnexpectedCSR = "SequenceTerminatedUnexpectedCSR";

		// Token: 0x04001002 RID: 4098
		internal const string SequenceTerminatedUnexpectedCSROfferId = "SequenceTerminatedUnexpectedCSROfferId";

		// Token: 0x04001003 RID: 4099
		internal const string SequenceTerminatedUnexpectedTerminateSequence = "SequenceTerminatedUnexpectedTerminateSequence";

		// Token: 0x04001004 RID: 4100
		internal const string SequenceTerminatedUnexpectedTerminateSequenceResponse = "SequenceTerminatedUnexpectedTerminateSequenceResponse";

		// Token: 0x04001005 RID: 4101
		internal const string SequenceTerminatedUnsupportedClose = "SequenceTerminatedUnsupportedClose";

		// Token: 0x04001006 RID: 4102
		internal const string SequenceTerminatedUnsupportedTerminateSequence = "SequenceTerminatedUnsupportedTerminateSequence";

		// Token: 0x04001007 RID: 4103
		internal const string SequenceTerminatedUnknownAddToWindowError = "SequenceTerminatedUnknownAddToWindowError";

		// Token: 0x04001008 RID: 4104
		internal const string SmallLastMsgNumberExceptionString = "SmallLastMsgNumberExceptionString";

		// Token: 0x04001009 RID: 4105
		internal const string TimeoutOnAddToWindow = "TimeoutOnAddToWindow";

		// Token: 0x0400100A RID: 4106
		internal const string TimeoutOnClose = "TimeoutOnClose";

		// Token: 0x0400100B RID: 4107
		internal const string TimeoutOnOpen = "TimeoutOnOpen";

		// Token: 0x0400100C RID: 4108
		internal const string TimeoutOnOperation = "TimeoutOnOperation";

		// Token: 0x0400100D RID: 4109
		internal const string TimeoutOnRequest = "TimeoutOnRequest";

		// Token: 0x0400100E RID: 4110
		internal const string TimeoutOnSend = "TimeoutOnSend";

		// Token: 0x0400100F RID: 4111
		internal const string UnexpectedAcknowledgement = "UnexpectedAcknowledgement";

		// Token: 0x04001010 RID: 4112
		internal const string UnexpectedAckRequested = "UnexpectedAckRequested";

		// Token: 0x04001011 RID: 4113
		internal const string UnexpectedCloseSequence = "UnexpectedCloseSequence";

		// Token: 0x04001012 RID: 4114
		internal const string UnexpectedCloseSequenceResponse = "UnexpectedCloseSequenceResponse";

		// Token: 0x04001013 RID: 4115
		internal const string UnexpectedCS = "UnexpectedCS";

		// Token: 0x04001014 RID: 4116
		internal const string UnexpectedCSR = "UnexpectedCSR";

		// Token: 0x04001015 RID: 4117
		internal const string UnexpectedCSOfferId = "UnexpectedCSOfferId";

		// Token: 0x04001016 RID: 4118
		internal const string UnexpectedCSROfferId = "UnexpectedCSROfferId";

		// Token: 0x04001017 RID: 4119
		internal const string UnexpectedTerminateSequence = "UnexpectedTerminateSequence";

		// Token: 0x04001018 RID: 4120
		internal const string UnexpectedTerminateSequenceResponse = "UnexpectedTerminateSequenceResponse";

		// Token: 0x04001019 RID: 4121
		internal const string UnparsableCSResponse = "UnparsableCSResponse";

		// Token: 0x0400101A RID: 4122
		internal const string UnknownSequenceFaultReason = "UnknownSequenceFaultReason";

		// Token: 0x0400101B RID: 4123
		internal const string UnknownSequenceFaultReceived = "UnknownSequenceFaultReceived";

		// Token: 0x0400101C RID: 4124
		internal const string UnknownSequenceMessageReceived = "UnknownSequenceMessageReceived";

		// Token: 0x0400101D RID: 4125
		internal const string UnrecognizedFaultReceived = "UnrecognizedFaultReceived";

		// Token: 0x0400101E RID: 4126
		internal const string UnrecognizedFaultReceivedOnOpen = "UnrecognizedFaultReceivedOnOpen";

		// Token: 0x0400101F RID: 4127
		internal const string UnsupportedCloseExceptionString = "UnsupportedCloseExceptionString";

		// Token: 0x04001020 RID: 4128
		internal const string UnsupportedTerminateSequenceExceptionString = "UnsupportedTerminateSequenceExceptionString";

		// Token: 0x04001021 RID: 4129
		internal const string WrongIdentifierFault = "WrongIdentifierFault";

		// Token: 0x04001022 RID: 4130
		internal const string WSHttpDoesNotSupportRMWithHttps = "WSHttpDoesNotSupportRMWithHttps";

		// Token: 0x04001023 RID: 4131
		internal const string WsrmFaultReceived = "WsrmFaultReceived";

		// Token: 0x04001024 RID: 4132
		internal const string WsrmMessageProcessingError = "WsrmMessageProcessingError";

		// Token: 0x04001025 RID: 4133
		internal const string WsrmMessageWithWrongRelatesToExceptionString = "WsrmMessageWithWrongRelatesToExceptionString";

		// Token: 0x04001026 RID: 4134
		internal const string WsrmMessageWithWrongRelatesToFaultString = "WsrmMessageWithWrongRelatesToFaultString";

		// Token: 0x04001027 RID: 4135
		internal const string WsrmRequestIncorrectReplyToExceptionString = "WsrmRequestIncorrectReplyToExceptionString";

		// Token: 0x04001028 RID: 4136
		internal const string WsrmRequestIncorrectReplyToFaultString = "WsrmRequestIncorrectReplyToFaultString";

		// Token: 0x04001029 RID: 4137
		internal const string WsrmRequiredExceptionString = "WsrmRequiredExceptionString";

		// Token: 0x0400102A RID: 4138
		internal const string WsrmRequiredFaultString = "WsrmRequiredFaultString";

		// Token: 0x0400102B RID: 4139
		internal const string SFxActionDemuxerDuplicate = "SFxActionDemuxerDuplicate";

		// Token: 0x0400102C RID: 4140
		internal const string SFxActionMismatch = "SFxActionMismatch";

		// Token: 0x0400102D RID: 4141
		internal const string SFxAnonymousTypeNotSupported = "SFxAnonymousTypeNotSupported";

		// Token: 0x0400102E RID: 4142
		internal const string SFxAsyncResultsDontMatch0 = "SFxAsyncResultsDontMatch0";

		// Token: 0x0400102F RID: 4143
		internal const string SFXBindingNameCannotBeNullOrEmpty = "SFXBindingNameCannotBeNullOrEmpty";

		// Token: 0x04001030 RID: 4144
		internal const string SFXUnvalidNamespaceValue = "SFXUnvalidNamespaceValue";

		// Token: 0x04001031 RID: 4145
		internal const string SFXUnvalidNamespaceParam = "SFXUnvalidNamespaceParam";

		// Token: 0x04001032 RID: 4146
		internal const string SFXHeaderNameCannotBeNullOrEmpty = "SFXHeaderNameCannotBeNullOrEmpty";

		// Token: 0x04001033 RID: 4147
		internal const string SFxEndpointNoMatchingScheme = "SFxEndpointNoMatchingScheme";

		// Token: 0x04001034 RID: 4148
		internal const string SFxBindingSchemeDoesNotMatch = "SFxBindingSchemeDoesNotMatch";

		// Token: 0x04001035 RID: 4149
		internal const string SFxGetChannelDispatcherDoesNotSupportScheme = "SFxGetChannelDispatcherDoesNotSupportScheme";

		// Token: 0x04001036 RID: 4150
		internal const string SFxIncorrectMessageVersion = "SFxIncorrectMessageVersion";

		// Token: 0x04001037 RID: 4151
		internal const string SFxBindingNotSupportedForMetadataHttpGet = "SFxBindingNotSupportedForMetadataHttpGet";

		// Token: 0x04001038 RID: 4152
		internal const string SFxBadByReferenceParameterMetadata = "SFxBadByReferenceParameterMetadata";

		// Token: 0x04001039 RID: 4153
		internal const string SFxBadByValueParameterMetadata = "SFxBadByValueParameterMetadata";

		// Token: 0x0400103A RID: 4154
		internal const string SFxBadMetadataMustBePolicy = "SFxBadMetadataMustBePolicy";

		// Token: 0x0400103B RID: 4155
		internal const string SFxBadMetadataLocationUri = "SFxBadMetadataLocationUri";

		// Token: 0x0400103C RID: 4156
		internal const string SFxBadMetadataLocationNoAppropriateBaseAddress = "SFxBadMetadataLocationNoAppropriateBaseAddress";

		// Token: 0x0400103D RID: 4157
		internal const string SFxBadMetadataDialect = "SFxBadMetadataDialect";

		// Token: 0x0400103E RID: 4158
		internal const string SFxBadMetadataReference = "SFxBadMetadataReference";

		// Token: 0x0400103F RID: 4159
		internal const string SFxMaximumResolvedReferencesOutOfRange = "SFxMaximumResolvedReferencesOutOfRange";

		// Token: 0x04001040 RID: 4160
		internal const string SFxMetadataExchangeClientNoMetadataAddress = "SFxMetadataExchangeClientNoMetadataAddress";

		// Token: 0x04001041 RID: 4161
		internal const string SFxMetadataExchangeClientCouldNotCreateChannelFactory = "SFxMetadataExchangeClientCouldNotCreateChannelFactory";

		// Token: 0x04001042 RID: 4162
		internal const string SFxMetadataExchangeClientCouldNotCreateWebRequest = "SFxMetadataExchangeClientCouldNotCreateWebRequest";

		// Token: 0x04001043 RID: 4163
		internal const string SFxMetadataExchangeClientCouldNotCreateChannelFactoryBadScheme = "SFxMetadataExchangeClientCouldNotCreateChannelFactoryBadScheme";

		// Token: 0x04001044 RID: 4164
		internal const string SFxBadTransactionProtocols = "SFxBadTransactionProtocols";

		// Token: 0x04001045 RID: 4165
		internal const string SFxMetadataResolverKnownContractsArgumentCannotBeEmpty = "SFxMetadataResolverKnownContractsArgumentCannotBeEmpty";

		// Token: 0x04001046 RID: 4166
		internal const string SFxMetadataResolverKnownContractsUniqueQNames = "SFxMetadataResolverKnownContractsUniqueQNames";

		// Token: 0x04001047 RID: 4167
		internal const string SFxMetadataResolverKnownContractsCannotContainNull = "SFxMetadataResolverKnownContractsCannotContainNull";

		// Token: 0x04001048 RID: 4168
		internal const string SFxBindingDoesNotHaveATransportBindingElement = "SFxBindingDoesNotHaveATransportBindingElement";

		// Token: 0x04001049 RID: 4169
		internal const string SFxBindingMustContainTransport2 = "SFxBindingMustContainTransport2";

		// Token: 0x0400104A RID: 4170
		internal const string SFxBodyCannotBeNull = "SFxBodyCannotBeNull";

		// Token: 0x0400104B RID: 4171
		internal const string SFxBodyObjectTypeCannotBeInherited = "SFxBodyObjectTypeCannotBeInherited";

		// Token: 0x0400104C RID: 4172
		internal const string SFxBodyObjectTypeCannotBeInterface = "SFxBodyObjectTypeCannotBeInterface";

		// Token: 0x0400104D RID: 4173
		internal const string SFxCallbackBehaviorAttributeOnlyOnDuplex = "SFxCallbackBehaviorAttributeOnlyOnDuplex";

		// Token: 0x0400104E RID: 4174
		internal const string SFxCallbackRequestReplyInOrder1 = "SFxCallbackRequestReplyInOrder1";

		// Token: 0x0400104F RID: 4175
		internal const string SfxCallbackTypeCannotBeNull = "SfxCallbackTypeCannotBeNull";

		// Token: 0x04001050 RID: 4176
		internal const string SFxCannotActivateCallbackInstace = "SFxCannotActivateCallbackInstace";

		// Token: 0x04001051 RID: 4177
		internal const string SFxCannotCallAddBaseAddress = "SFxCannotCallAddBaseAddress";

		// Token: 0x04001052 RID: 4178
		internal const string SFxCannotCallAutoOpenWhenExplicitOpenCalled = "SFxCannotCallAutoOpenWhenExplicitOpenCalled";

		// Token: 0x04001053 RID: 4179
		internal const string SFxCannotGetMetadataFromRelativeAddress = "SFxCannotGetMetadataFromRelativeAddress";

		// Token: 0x04001054 RID: 4180
		internal const string SFxCannotHttpGetMetadataFromAddress = "SFxCannotHttpGetMetadataFromAddress";

		// Token: 0x04001055 RID: 4181
		internal const string SFxCannotGetMetadataFromLocation = "SFxCannotGetMetadataFromLocation";

		// Token: 0x04001056 RID: 4182
		internal const string SFxCannotHaveDifferentTransactionProtocolsInOneBinding = "SFxCannotHaveDifferentTransactionProtocolsInOneBinding";

		// Token: 0x04001057 RID: 4183
		internal const string SFxCannotImportAsParameters_Bare = "SFxCannotImportAsParameters_Bare";

		// Token: 0x04001058 RID: 4184
		internal const string SFxCannotImportAsParameters_DifferentWrapperNs = "SFxCannotImportAsParameters_DifferentWrapperNs";

		// Token: 0x04001059 RID: 4185
		internal const string SFxCannotImportAsParameters_DifferentWrapperName = "SFxCannotImportAsParameters_DifferentWrapperName";

		// Token: 0x0400105A RID: 4186
		internal const string SFxCannotImportAsParameters_ElementIsNotNillable = "SFxCannotImportAsParameters_ElementIsNotNillable";

		// Token: 0x0400105B RID: 4187
		internal const string SFxCannotImportAsParameters_MessageHasProtectionLevel = "SFxCannotImportAsParameters_MessageHasProtectionLevel";

		// Token: 0x0400105C RID: 4188
		internal const string SFxCannotImportAsParameters_HeadersAreIgnoredInEncoded = "SFxCannotImportAsParameters_HeadersAreIgnoredInEncoded";

		// Token: 0x0400105D RID: 4189
		internal const string SFxCannotImportAsParameters_HeadersAreUnsupported = "SFxCannotImportAsParameters_HeadersAreUnsupported";

		// Token: 0x0400105E RID: 4190
		internal const string SFxCannotImportAsParameters_Message = "SFxCannotImportAsParameters_Message";

		// Token: 0x0400105F RID: 4191
		internal const string SFxCannotImportAsParameters_NamespaceMismatch = "SFxCannotImportAsParameters_NamespaceMismatch";

		// Token: 0x04001060 RID: 4192
		internal const string SFxCannotRequireBothSessionAndDatagram3 = "SFxCannotRequireBothSessionAndDatagram3";

		// Token: 0x04001061 RID: 4193
		internal const string SFxCannotSetExtensionsByIndex = "SFxCannotSetExtensionsByIndex";

		// Token: 0x04001062 RID: 4194
		internal const string SFxChannelDispatcherDifferentHost0 = "SFxChannelDispatcherDifferentHost0";

		// Token: 0x04001063 RID: 4195
		internal const string SFxChannelDispatcherMultipleHost0 = "SFxChannelDispatcherMultipleHost0";

		// Token: 0x04001064 RID: 4196
		internal const string SFxChannelDispatcherNoHost0 = "SFxChannelDispatcherNoHost0";

		// Token: 0x04001065 RID: 4197
		internal const string SFxChannelDispatcherNoMessageVersion = "SFxChannelDispatcherNoMessageVersion";

		// Token: 0x04001066 RID: 4198
		internal const string SFxChannelDispatcherUnableToOpen1 = "SFxChannelDispatcherUnableToOpen1";

		// Token: 0x04001067 RID: 4199
		internal const string SFxChannelDispatcherUnableToOpen2 = "SFxChannelDispatcherUnableToOpen2";

		// Token: 0x04001068 RID: 4200
		internal const string SFxChannelFactoryTypeMustBeInterface = "SFxChannelFactoryTypeMustBeInterface";

		// Token: 0x04001069 RID: 4201
		internal const string SFxChannelFactoryCannotApplyConfigurationWithoutEndpoint = "SFxChannelFactoryCannotApplyConfigurationWithoutEndpoint";

		// Token: 0x0400106A RID: 4202
		internal const string SFxChannelFactoryCannotCreateFactoryWithoutDescription = "SFxChannelFactoryCannotCreateFactoryWithoutDescription";

		// Token: 0x0400106B RID: 4203
		internal const string SFxChannelTerminated0 = "SFxChannelTerminated0";

		// Token: 0x0400106C RID: 4204
		internal const string SFxClientOutputSessionAutoClosed = "SFxClientOutputSessionAutoClosed";

		// Token: 0x0400106D RID: 4205
		internal const string SFxCodeGenArrayTypeIsNotSupported = "SFxCodeGenArrayTypeIsNotSupported";

		// Token: 0x0400106E RID: 4206
		internal const string SFxCodeGenCanOnlyStoreIntoArgOrLocGot0 = "SFxCodeGenCanOnlyStoreIntoArgOrLocGot0";

		// Token: 0x0400106F RID: 4207
		internal const string SFxCodeGenExpectingEnd = "SFxCodeGenExpectingEnd";

		// Token: 0x04001070 RID: 4208
		internal const string SFxCodeGenIsNotAssignableFrom = "SFxCodeGenIsNotAssignableFrom";

		// Token: 0x04001071 RID: 4209
		internal const string SFxCodeGenNoConversionPossibleTo = "SFxCodeGenNoConversionPossibleTo";

		// Token: 0x04001072 RID: 4210
		internal const string SFxCodeGenWarning = "SFxCodeGenWarning";

		// Token: 0x04001073 RID: 4211
		internal const string SFxCodeGenUnknownConstantType = "SFxCodeGenUnknownConstantType";

		// Token: 0x04001074 RID: 4212
		internal const string SFxCollectionDoesNotSupportSet0 = "SFxCollectionDoesNotSupportSet0";

		// Token: 0x04001075 RID: 4213
		internal const string SFxCollectionReadOnly = "SFxCollectionReadOnly";

		// Token: 0x04001076 RID: 4214
		internal const string SFxCollectionWrongType2 = "SFxCollectionWrongType2";

		// Token: 0x04001077 RID: 4215
		internal const string SFxConflictingGlobalElement = "SFxConflictingGlobalElement";

		// Token: 0x04001078 RID: 4216
		internal const string SFxConflictingGlobalType = "SFxConflictingGlobalType";

		// Token: 0x04001079 RID: 4217
		internal const string SFxContextModifiedInsideScope0 = "SFxContextModifiedInsideScope0";

		// Token: 0x0400107A RID: 4218
		internal const string SFxContractDescriptionNameCannotBeEmpty = "SFxContractDescriptionNameCannotBeEmpty";

		// Token: 0x0400107B RID: 4219
		internal const string SFxContractHasZeroOperations = "SFxContractHasZeroOperations";

		// Token: 0x0400107C RID: 4220
		internal const string SFxContractHasZeroInitiatingOperations = "SFxContractHasZeroInitiatingOperations";

		// Token: 0x0400107D RID: 4221
		internal const string SFxContractInheritanceRequiresInterfaces = "SFxContractInheritanceRequiresInterfaces";

		// Token: 0x0400107E RID: 4222
		internal const string SFxContractInheritanceRequiresInterfaces2 = "SFxContractInheritanceRequiresInterfaces2";

		// Token: 0x0400107F RID: 4223
		internal const string SFxCopyToRequiresICollection = "SFxCopyToRequiresICollection";

		// Token: 0x04001080 RID: 4224
		internal const string SFxCreateDuplexChannel1 = "SFxCreateDuplexChannel1";

		// Token: 0x04001081 RID: 4225
		internal const string SFxCreateDuplexChannelNoCallback = "SFxCreateDuplexChannelNoCallback";

		// Token: 0x04001082 RID: 4226
		internal const string SFxCreateDuplexChannelNoCallback1 = "SFxCreateDuplexChannelNoCallback1";

		// Token: 0x04001083 RID: 4227
		internal const string SFxCreateDuplexChannelNoCallbackUserObject = "SFxCreateDuplexChannelNoCallbackUserObject";

		// Token: 0x04001084 RID: 4228
		internal const string SFxCreateDuplexChannelBadCallbackUserObject = "SFxCreateDuplexChannelBadCallbackUserObject";

		// Token: 0x04001085 RID: 4229
		internal const string SFxCreateNonDuplexChannel1 = "SFxCreateNonDuplexChannel1";

		// Token: 0x04001086 RID: 4230
		internal const string SFxCustomBindingNeedsTransport1 = "SFxCustomBindingNeedsTransport1";

		// Token: 0x04001087 RID: 4231
		internal const string SFxCustomBindingWithoutTransport = "SFxCustomBindingWithoutTransport";

		// Token: 0x04001088 RID: 4232
		internal const string SFxDeserializationFailed1 = "SFxDeserializationFailed1";

		// Token: 0x04001089 RID: 4233
		internal const string SFxDictionaryIsEmpty = "SFxDictionaryIsEmpty";

		// Token: 0x0400108A RID: 4234
		internal const string SFxDisallowedAttributeCombination = "SFxDisallowedAttributeCombination";

		// Token: 0x0400108B RID: 4235
		internal const string SFxEndpointAddressNotSpecified = "SFxEndpointAddressNotSpecified";

		// Token: 0x0400108C RID: 4236
		internal const string SFxEndpointContractNotSpecified = "SFxEndpointContractNotSpecified";

		// Token: 0x0400108D RID: 4237
		internal const string SFxEndpointBindingNotSpecified = "SFxEndpointBindingNotSpecified";

		// Token: 0x0400108E RID: 4238
		internal const string SFxInitializationUINotCalled = "SFxInitializationUINotCalled";

		// Token: 0x0400108F RID: 4239
		internal const string SFxInitializationUIDisallowed = "SFxInitializationUIDisallowed";

		// Token: 0x04001090 RID: 4240
		internal const string SFxDocExt_NoMetadataSection1 = "SFxDocExt_NoMetadataSection1";

		// Token: 0x04001091 RID: 4241
		internal const string SFxDocExt_NoMetadataSection2 = "SFxDocExt_NoMetadataSection2";

		// Token: 0x04001092 RID: 4242
		internal const string SFxDocExt_NoMetadataSection3 = "SFxDocExt_NoMetadataSection3";

		// Token: 0x04001093 RID: 4243
		internal const string SFxDocExt_NoMetadataSection4 = "SFxDocExt_NoMetadataSection4";

		// Token: 0x04001094 RID: 4244
		internal const string SFxDocExt_NoMetadataSection5 = "SFxDocExt_NoMetadataSection5";

		// Token: 0x04001095 RID: 4245
		internal const string SFxDocExt_NoMetadataConfigComment1 = "SFxDocExt_NoMetadataConfigComment1";

		// Token: 0x04001096 RID: 4246
		internal const string SFxDocExt_NoMetadataConfigComment2 = "SFxDocExt_NoMetadataConfigComment2";

		// Token: 0x04001097 RID: 4247
		internal const string SFxDocExt_NoMetadataConfigComment3 = "SFxDocExt_NoMetadataConfigComment3";

		// Token: 0x04001098 RID: 4248
		internal const string SFxDocExt_NoMetadataConfigComment4 = "SFxDocExt_NoMetadataConfigComment4";

		// Token: 0x04001099 RID: 4249
		internal const string SFxDocExt_CS = "SFxDocExt_CS";

		// Token: 0x0400109A RID: 4250
		internal const string SFxDocExt_VB = "SFxDocExt_VB";

		// Token: 0x0400109B RID: 4251
		internal const string SFxDocExt_MainPageTitleNoServiceName = "SFxDocExt_MainPageTitleNoServiceName";

		// Token: 0x0400109C RID: 4252
		internal const string SFxDocExt_MainPageTitle = "SFxDocExt_MainPageTitle";

		// Token: 0x0400109D RID: 4253
		internal const string SFxDocExt_MainPageIntro1a = "SFxDocExt_MainPageIntro1a";

		// Token: 0x0400109E RID: 4254
		internal const string SFxDocExt_MainPageIntro1b = "SFxDocExt_MainPageIntro1b";

		// Token: 0x0400109F RID: 4255
		internal const string SFxDocExt_MainPageIntro2 = "SFxDocExt_MainPageIntro2";

		// Token: 0x040010A0 RID: 4256
		internal const string SFxDocExt_MainPageComment = "SFxDocExt_MainPageComment";

		// Token: 0x040010A1 RID: 4257
		internal const string SFxDocExt_MainPageComment2 = "SFxDocExt_MainPageComment2";

		// Token: 0x040010A2 RID: 4258
		internal const string SFxDocExt_Error = "SFxDocExt_Error";

		// Token: 0x040010A3 RID: 4259
		internal const string SFxDocEncodedNotSupported = "SFxDocEncodedNotSupported";

		// Token: 0x040010A4 RID: 4260
		internal const string SFxDocEncodedFaultNotSupported = "SFxDocEncodedFaultNotSupported";

		// Token: 0x040010A5 RID: 4261
		internal const string SFxDuplicateMessageParts = "SFxDuplicateMessageParts";

		// Token: 0x040010A6 RID: 4262
		internal const string SFxDuplicateInitiatingActionAtSameVia = "SFxDuplicateInitiatingActionAtSameVia";

		// Token: 0x040010A7 RID: 4263
		internal const string SFXEndpointBehaviorUsedOnWrongSide = "SFXEndpointBehaviorUsedOnWrongSide";

		// Token: 0x040010A8 RID: 4264
		internal const string SFxEndpointDispatcherMultipleChannelDispatcher0 = "SFxEndpointDispatcherMultipleChannelDispatcher0";

		// Token: 0x040010A9 RID: 4265
		internal const string SFxEndpointDispatcherDifferentChannelDispatcher0 = "SFxEndpointDispatcherDifferentChannelDispatcher0";

		// Token: 0x040010AA RID: 4266
		internal const string SFxErrorCreatingMtomReader = "SFxErrorCreatingMtomReader";

		// Token: 0x040010AB RID: 4267
		internal const string SFxErrorDeserializingRequestBody = "SFxErrorDeserializingRequestBody";

		// Token: 0x040010AC RID: 4268
		internal const string SFxErrorDeserializingRequestBodyMore = "SFxErrorDeserializingRequestBodyMore";

		// Token: 0x040010AD RID: 4269
		internal const string SFxErrorDeserializingReplyBody = "SFxErrorDeserializingReplyBody";

		// Token: 0x040010AE RID: 4270
		internal const string SFxErrorDeserializingReplyBodyMore = "SFxErrorDeserializingReplyBodyMore";

		// Token: 0x040010AF RID: 4271
		internal const string SFxErrorSerializingBody = "SFxErrorSerializingBody";

		// Token: 0x040010B0 RID: 4272
		internal const string SFxErrorDeserializingHeader = "SFxErrorDeserializingHeader";

		// Token: 0x040010B1 RID: 4273
		internal const string SFxErrorSerializingHeader = "SFxErrorSerializingHeader";

		// Token: 0x040010B2 RID: 4274
		internal const string SFxErrorDeserializingFault = "SFxErrorDeserializingFault";

		// Token: 0x040010B3 RID: 4275
		internal const string SFxErrorReflectingOnType2 = "SFxErrorReflectingOnType2";

		// Token: 0x040010B4 RID: 4276
		internal const string SFxErrorReflectingOnMethod3 = "SFxErrorReflectingOnMethod3";

		// Token: 0x040010B5 RID: 4277
		internal const string SFxErrorReflectingOnParameter4 = "SFxErrorReflectingOnParameter4";

		// Token: 0x040010B6 RID: 4278
		internal const string SFxErrorReflectionOnUnknown1 = "SFxErrorReflectionOnUnknown1";

		// Token: 0x040010B7 RID: 4279
		internal const string SFxExceptionDetailEndOfInner = "SFxExceptionDetailEndOfInner";

		// Token: 0x040010B8 RID: 4280
		internal const string SFxExceptionDetailFormat = "SFxExceptionDetailFormat";

		// Token: 0x040010B9 RID: 4281
		internal const string SFxExpectedIMethodCallMessage = "SFxExpectedIMethodCallMessage";

		// Token: 0x040010BA RID: 4282
		internal const string SFxExportMustHaveType = "SFxExportMustHaveType";

		// Token: 0x040010BB RID: 4283
		internal const string SFxFaultCannotBeImported = "SFxFaultCannotBeImported";

		// Token: 0x040010BC RID: 4284
		internal const string SFxFaultContractDuplicateDetailType = "SFxFaultContractDuplicateDetailType";

		// Token: 0x040010BD RID: 4285
		internal const string SFxFaultContractDuplicateElement = "SFxFaultContractDuplicateElement";

		// Token: 0x040010BE RID: 4286
		internal const string SFxFaultExceptionToString3 = "SFxFaultExceptionToString3";

		// Token: 0x040010BF RID: 4287
		internal const string SFxFaultReason = "SFxFaultReason";

		// Token: 0x040010C0 RID: 4288
		internal const string SFxFaultTypeAnonymous = "SFxFaultTypeAnonymous";

		// Token: 0x040010C1 RID: 4289
		internal const string SFxHeaderNameMismatchInMessageContract = "SFxHeaderNameMismatchInMessageContract";

		// Token: 0x040010C2 RID: 4290
		internal const string SFxHeaderNameMismatchInOperation = "SFxHeaderNameMismatchInOperation";

		// Token: 0x040010C3 RID: 4291
		internal const string SFxHeaderNamespaceMismatchInMessageContract = "SFxHeaderNamespaceMismatchInMessageContract";

		// Token: 0x040010C4 RID: 4292
		internal const string SFxHeaderNamespaceMismatchInOperation = "SFxHeaderNamespaceMismatchInOperation";

		// Token: 0x040010C5 RID: 4293
		internal const string SFxHeaderNotUnderstood = "SFxHeaderNotUnderstood";

		// Token: 0x040010C6 RID: 4294
		internal const string SFxHeadersAreNotSupportedInEncoded = "SFxHeadersAreNotSupportedInEncoded";

		// Token: 0x040010C7 RID: 4295
		internal const string SFxImmutableServiceHostBehavior0 = "SFxImmutableServiceHostBehavior0";

		// Token: 0x040010C8 RID: 4296
		internal const string SFxImmutableChannelFactoryBehavior0 = "SFxImmutableChannelFactoryBehavior0";

		// Token: 0x040010C9 RID: 4297
		internal const string SFxImmutableClientBaseCacheSetting = "SFxImmutableClientBaseCacheSetting";

		// Token: 0x040010CA RID: 4298
		internal const string SFxImmutableThrottle1 = "SFxImmutableThrottle1";

		// Token: 0x040010CB RID: 4299
		internal const string SFxInconsistentBindingBodyParts = "SFxInconsistentBindingBodyParts";

		// Token: 0x040010CC RID: 4300
		internal const string SFxInconsistentWsdlOperationStyleInHeader = "SFxInconsistentWsdlOperationStyleInHeader";

		// Token: 0x040010CD RID: 4301
		internal const string SFxInconsistentWsdlOperationStyleInMessageParts = "SFxInconsistentWsdlOperationStyleInMessageParts";

		// Token: 0x040010CE RID: 4302
		internal const string SFxInconsistentWsdlOperationStyleInOperationMessages = "SFxInconsistentWsdlOperationStyleInOperationMessages";

		// Token: 0x040010CF RID: 4303
		internal const string SFxInconsistentWsdlOperationUseAndStyleInBinding = "SFxInconsistentWsdlOperationUseAndStyleInBinding";

		// Token: 0x040010D0 RID: 4304
		internal const string SFxInconsistentWsdlOperationUseInBindingExtensions = "SFxInconsistentWsdlOperationUseInBindingExtensions";

		// Token: 0x040010D1 RID: 4305
		internal const string SFxInconsistentWsdlOperationUseInBindingMessages = "SFxInconsistentWsdlOperationUseInBindingMessages";

		// Token: 0x040010D2 RID: 4306
		internal const string SFxInconsistentWsdlOperationUseInBindingFaults = "SFxInconsistentWsdlOperationUseInBindingFaults";

		// Token: 0x040010D3 RID: 4307
		internal const string SFxInputParametersToServiceInvalid = "SFxInputParametersToServiceInvalid";

		// Token: 0x040010D4 RID: 4308
		internal const string SFxInputParametersToServiceNull = "SFxInputParametersToServiceNull";

		// Token: 0x040010D5 RID: 4309
		internal const string SFxInstanceNotInitialized = "SFxInstanceNotInitialized";

		// Token: 0x040010D6 RID: 4310
		internal const string SFxInterleavedContextScopes0 = "SFxInterleavedContextScopes0";

		// Token: 0x040010D7 RID: 4311
		internal const string SFxInternalServerError = "SFxInternalServerError";

		// Token: 0x040010D8 RID: 4312
		internal const string SFxInternalCallbackError = "SFxInternalCallbackError";

		// Token: 0x040010D9 RID: 4313
		internal const string SFxInvalidAsyncResultState0 = "SFxInvalidAsyncResultState0";

		// Token: 0x040010DA RID: 4314
		internal const string SFxInvalidCallbackIAsyncResult = "SFxInvalidCallbackIAsyncResult";

		// Token: 0x040010DB RID: 4315
		internal const string SFxInvalidCallbackContractType = "SFxInvalidCallbackContractType";

		// Token: 0x040010DC RID: 4316
		internal const string SFxInvalidChannelToOperationContext = "SFxInvalidChannelToOperationContext";

		// Token: 0x040010DD RID: 4317
		internal const string SFxInvalidContextScopeThread0 = "SFxInvalidContextScopeThread0";

		// Token: 0x040010DE RID: 4318
		internal const string SFxInvalidMessageBody = "SFxInvalidMessageBody";

		// Token: 0x040010DF RID: 4319
		internal const string SFxInvalidMessageBodyEmptyMessage = "SFxInvalidMessageBodyEmptyMessage";

		// Token: 0x040010E0 RID: 4320
		internal const string SFxInvalidMessageBodyErrorSerializingParameter = "SFxInvalidMessageBodyErrorSerializingParameter";

		// Token: 0x040010E1 RID: 4321
		internal const string SFxInvalidMessageBodyErrorDeserializingParameter = "SFxInvalidMessageBodyErrorDeserializingParameter";

		// Token: 0x040010E2 RID: 4322
		internal const string SFxInvalidMessageBodyErrorDeserializingParameterMore = "SFxInvalidMessageBodyErrorDeserializingParameterMore";

		// Token: 0x040010E3 RID: 4323
		internal const string SFxInvalidMessageContractSignature = "SFxInvalidMessageContractSignature";

		// Token: 0x040010E4 RID: 4324
		internal const string SFxInvalidMessageHeaderArrayType = "SFxInvalidMessageHeaderArrayType";

		// Token: 0x040010E5 RID: 4325
		internal const string SFxInvalidRequestAction = "SFxInvalidRequestAction";

		// Token: 0x040010E6 RID: 4326
		internal const string SFxInvalidReplyAction = "SFxInvalidReplyAction";

		// Token: 0x040010E7 RID: 4327
		internal const string SFxInvalidStreamInTypedMessage = "SFxInvalidStreamInTypedMessage";

		// Token: 0x040010E8 RID: 4328
		internal const string SFxInvalidStreamInRequest = "SFxInvalidStreamInRequest";

		// Token: 0x040010E9 RID: 4329
		internal const string SFxInvalidStreamInResponse = "SFxInvalidStreamInResponse";

		// Token: 0x040010EA RID: 4330
		internal const string SFxInvalidStreamOffsetLength = "SFxInvalidStreamOffsetLength";

		// Token: 0x040010EB RID: 4331
		internal const string SFxInvalidUseOfPrimitiveOperationFormatter = "SFxInvalidUseOfPrimitiveOperationFormatter";

		// Token: 0x040010EC RID: 4332
		internal const string SFxInvalidStaticOverloadCalledForDuplexChannelFactory1 = "SFxInvalidStaticOverloadCalledForDuplexChannelFactory1";

		// Token: 0x040010ED RID: 4333
		internal const string SFxInvalidSoapAttribute = "SFxInvalidSoapAttribute";

		// Token: 0x040010EE RID: 4334
		internal const string SFxInvalidXmlAttributeInBare = "SFxInvalidXmlAttributeInBare";

		// Token: 0x040010EF RID: 4335
		internal const string SFxInvalidXmlAttributeInWrapped = "SFxInvalidXmlAttributeInWrapped";

		// Token: 0x040010F0 RID: 4336
		internal const string SFxKnownTypeAttributeInvalid1 = "SFxKnownTypeAttributeInvalid1";

		// Token: 0x040010F1 RID: 4337
		internal const string SFxKnownTypeAttributeReturnType3 = "SFxKnownTypeAttributeReturnType3";

		// Token: 0x040010F2 RID: 4338
		internal const string SFxKnownTypeAttributeUnknownMethod3 = "SFxKnownTypeAttributeUnknownMethod3";

		// Token: 0x040010F3 RID: 4339
		internal const string SFxKnownTypeNull = "SFxKnownTypeNull";

		// Token: 0x040010F4 RID: 4340
		internal const string SFxMessageContractBaseTypeNotValid = "SFxMessageContractBaseTypeNotValid";

		// Token: 0x040010F5 RID: 4341
		internal const string SFxMessageContractRequiresDefaultConstructor = "SFxMessageContractRequiresDefaultConstructor";

		// Token: 0x040010F6 RID: 4342
		internal const string SFxMessageOperationFormatterCannotSerializeFault = "SFxMessageOperationFormatterCannotSerializeFault";

		// Token: 0x040010F7 RID: 4343
		internal const string SFxMetadataReferenceInvalidLocation = "SFxMetadataReferenceInvalidLocation";

		// Token: 0x040010F8 RID: 4344
		internal const string SFxMethodNotSupported1 = "SFxMethodNotSupported1";

		// Token: 0x040010F9 RID: 4345
		internal const string SFxMethodNotSupportedOnCallback1 = "SFxMethodNotSupportedOnCallback1";

		// Token: 0x040010FA RID: 4346
		internal const string SFxMethodNotSupportedByType2 = "SFxMethodNotSupportedByType2";

		// Token: 0x040010FB RID: 4347
		internal const string SFxMismatchedOperationParent = "SFxMismatchedOperationParent";

		// Token: 0x040010FC RID: 4348
		internal const string SFxMissingActionHeader = "SFxMissingActionHeader";

		// Token: 0x040010FD RID: 4349
		internal const string SFxMultipleCallbackFromSynchronizationContext = "SFxMultipleCallbackFromSynchronizationContext";

		// Token: 0x040010FE RID: 4350
		internal const string SFxMultipleCallbackFromAsyncOperation = "SFxMultipleCallbackFromAsyncOperation";

		// Token: 0x040010FF RID: 4351
		internal const string SFxMultipleUnknownHeaders = "SFxMultipleUnknownHeaders";

		// Token: 0x04001100 RID: 4352
		internal const string SFxMultipleContractStarOperations0 = "SFxMultipleContractStarOperations0";

		// Token: 0x04001101 RID: 4353
		internal const string SFxMultipleContractsWithSameName = "SFxMultipleContractsWithSameName";

		// Token: 0x04001102 RID: 4354
		internal const string SFxMultiplePartsNotAllowedInEncoded = "SFxMultiplePartsNotAllowedInEncoded";

		// Token: 0x04001103 RID: 4355
		internal const string SFxNameCannotBeEmpty = "SFxNameCannotBeEmpty";

		// Token: 0x04001104 RID: 4356
		internal const string SFxConfigurationNameCannotBeEmpty = "SFxConfigurationNameCannotBeEmpty";

		// Token: 0x04001105 RID: 4357
		internal const string SFxNeedProxyBehaviorOperationSelector2 = "SFxNeedProxyBehaviorOperationSelector2";

		// Token: 0x04001106 RID: 4358
		internal const string SFxNoDefaultConstructor = "SFxNoDefaultConstructor";

		// Token: 0x04001107 RID: 4359
		internal const string SFxNoMostDerivedContract = "SFxNoMostDerivedContract";

		// Token: 0x04001108 RID: 4360
		internal const string SFxNullReplyFromExtension2 = "SFxNullReplyFromExtension2";

		// Token: 0x04001109 RID: 4361
		internal const string SFxNullReplyFromFormatter2 = "SFxNullReplyFromFormatter2";

		// Token: 0x0400110A RID: 4362
		internal const string SFxServiceChannelIdleAborted = "SFxServiceChannelIdleAborted";

		// Token: 0x0400110B RID: 4363
		internal const string SFxServiceMetadataBehaviorUrlMustBeHttpOrRelative = "SFxServiceMetadataBehaviorUrlMustBeHttpOrRelative";

		// Token: 0x0400110C RID: 4364
		internal const string SFxServiceMetadataBehaviorNoHttpBaseAddress = "SFxServiceMetadataBehaviorNoHttpBaseAddress";

		// Token: 0x0400110D RID: 4365
		internal const string SFxServiceMetadataBehaviorNoHttpsBaseAddress = "SFxServiceMetadataBehaviorNoHttpsBaseAddress";

		// Token: 0x0400110E RID: 4366
		internal const string SFxServiceMetadataBehaviorInstancingError = "SFxServiceMetadataBehaviorInstancingError";

		// Token: 0x0400110F RID: 4367
		internal const string SFxServiceTypeNotCreatable = "SFxServiceTypeNotCreatable";

		// Token: 0x04001110 RID: 4368
		internal const string SFxSetEnableFaultsOnChannelDispatcher0 = "SFxSetEnableFaultsOnChannelDispatcher0";

		// Token: 0x04001111 RID: 4369
		internal const string SFxSetManualAddresssingOnChannelDispatcher0 = "SFxSetManualAddresssingOnChannelDispatcher0";

		// Token: 0x04001112 RID: 4370
		internal const string SFxNoBatchingForSession = "SFxNoBatchingForSession";

		// Token: 0x04001113 RID: 4371
		internal const string SFxNoBatchingForReleaseOnComplete = "SFxNoBatchingForReleaseOnComplete";

		// Token: 0x04001114 RID: 4372
		internal const string SFxNoServiceObject = "SFxNoServiceObject";

		// Token: 0x04001115 RID: 4373
		internal const string SFxNone2004 = "SFxNone2004";

		// Token: 0x04001116 RID: 4374
		internal const string SFxNonExceptionThrown = "SFxNonExceptionThrown";

		// Token: 0x04001117 RID: 4375
		internal const string SFxNonInitiatingOperation1 = "SFxNonInitiatingOperation1";

		// Token: 0x04001118 RID: 4376
		internal const string SfxNoTypeSpecifiedForParameter = "SfxNoTypeSpecifiedForParameter";

		// Token: 0x04001119 RID: 4377
		internal const string SFxOneWayAndTransactionsIncompatible = "SFxOneWayAndTransactionsIncompatible";

		// Token: 0x0400111A RID: 4378
		internal const string SFxOneWayMessageToTwoWayMethod0 = "SFxOneWayMessageToTwoWayMethod0";

		// Token: 0x0400111B RID: 4379
		internal const string SFxOperationBehaviorAttributeOnlyOnServiceClass = "SFxOperationBehaviorAttributeOnlyOnServiceClass";

		// Token: 0x0400111C RID: 4380
		internal const string SFxOperationBehaviorAttributeReleaseInstanceModeDoesNotApplyToCallback = "SFxOperationBehaviorAttributeReleaseInstanceModeDoesNotApplyToCallback";

		// Token: 0x0400111D RID: 4381
		internal const string SFxOperationContractOnNonServiceContract = "SFxOperationContractOnNonServiceContract";

		// Token: 0x0400111E RID: 4382
		internal const string SFxOperationContractProviderOnNonServiceContract = "SFxOperationContractProviderOnNonServiceContract";

		// Token: 0x0400111F RID: 4383
		internal const string SFxOperationDescriptionNameCannotBeEmpty = "SFxOperationDescriptionNameCannotBeEmpty";

		// Token: 0x04001120 RID: 4384
		internal const string SFxParameterNameCannotBeNull = "SFxParameterNameCannotBeNull";

		// Token: 0x04001121 RID: 4385
		internal const string SFxOperationMustHaveOneOrTwoMessages = "SFxOperationMustHaveOneOrTwoMessages";

		// Token: 0x04001122 RID: 4386
		internal const string SFxParameterCountMismatch = "SFxParameterCountMismatch";

		// Token: 0x04001123 RID: 4387
		internal const string SFxParameterMustBeMessage = "SFxParameterMustBeMessage";

		// Token: 0x04001124 RID: 4388
		internal const string SFxParametersMustBeEmpty = "SFxParametersMustBeEmpty";

		// Token: 0x04001125 RID: 4389
		internal const string SFxParameterMustBeArrayOfOneElement = "SFxParameterMustBeArrayOfOneElement";

		// Token: 0x04001126 RID: 4390
		internal const string SFxPartNameMustBeUniqueInRpc = "SFxPartNameMustBeUniqueInRpc";

		// Token: 0x04001127 RID: 4391
		internal const string SFxReceiveContextSettingsPropertyMissing = "SFxReceiveContextSettingsPropertyMissing";

		// Token: 0x04001128 RID: 4392
		internal const string SFxReceiveContextPropertyMissing = "SFxReceiveContextPropertyMissing";

		// Token: 0x04001129 RID: 4393
		internal const string SFxRequestHasInvalidReplyToOnClient = "SFxRequestHasInvalidReplyToOnClient";

		// Token: 0x0400112A RID: 4394
		internal const string SFxRequestHasInvalidFaultToOnClient = "SFxRequestHasInvalidFaultToOnClient";

		// Token: 0x0400112B RID: 4395
		internal const string SFxRequestHasInvalidFromOnClient = "SFxRequestHasInvalidFromOnClient";

		// Token: 0x0400112C RID: 4396
		internal const string SFxRequestHasInvalidReplyToOnServer = "SFxRequestHasInvalidReplyToOnServer";

		// Token: 0x0400112D RID: 4397
		internal const string SFxRequestHasInvalidFaultToOnServer = "SFxRequestHasInvalidFaultToOnServer";

		// Token: 0x0400112E RID: 4398
		internal const string SFxRequestHasInvalidFromOnServer = "SFxRequestHasInvalidFromOnServer";

		// Token: 0x0400112F RID: 4399
		internal const string SFxRequestReplyNone = "SFxRequestReplyNone";

		// Token: 0x04001130 RID: 4400
		internal const string SFxRequestTimedOut1 = "SFxRequestTimedOut1";

		// Token: 0x04001131 RID: 4401
		internal const string SFxRequestTimedOut2 = "SFxRequestTimedOut2";

		// Token: 0x04001132 RID: 4402
		internal const string SFxReplyActionMismatch3 = "SFxReplyActionMismatch3";

		// Token: 0x04001133 RID: 4403
		internal const string SFxRequiredRuntimePropertyMissing = "SFxRequiredRuntimePropertyMissing";

		// Token: 0x04001134 RID: 4404
		internal const string SFxResolvedMaxResolvedReferences = "SFxResolvedMaxResolvedReferences";

		// Token: 0x04001135 RID: 4405
		internal const string SFxResultMustBeMessage = "SFxResultMustBeMessage";

		// Token: 0x04001136 RID: 4406
		internal const string SFxRevertImpersonationFailed0 = "SFxRevertImpersonationFailed0";

		// Token: 0x04001137 RID: 4407
		internal const string SFxRpcMessageBodyPartNameInvalid = "SFxRpcMessageBodyPartNameInvalid";

		// Token: 0x04001138 RID: 4408
		internal const string SFxRpcMessageMustHaveASingleBody = "SFxRpcMessageMustHaveASingleBody";

		// Token: 0x04001139 RID: 4409
		internal const string SFxSchemaDoesNotContainElement = "SFxSchemaDoesNotContainElement";

		// Token: 0x0400113A RID: 4410
		internal const string SFxSchemaDoesNotContainType = "SFxSchemaDoesNotContainType";

		// Token: 0x0400113B RID: 4411
		internal const string SFxWsdlMessageDoesNotContainPart3 = "SFxWsdlMessageDoesNotContainPart3";

		// Token: 0x0400113C RID: 4412
		internal const string SFxSchemaNotFound = "SFxSchemaNotFound";

		// Token: 0x0400113D RID: 4413
		internal const string SFxSecurityContextPropertyMissingFromRequestMessage = "SFxSecurityContextPropertyMissingFromRequestMessage";

		// Token: 0x0400113E RID: 4414
		internal const string SFxServerDidNotReply = "SFxServerDidNotReply";

		// Token: 0x0400113F RID: 4415
		internal const string SFxServiceHostBaseCannotAddEndpointAfterOpen = "SFxServiceHostBaseCannotAddEndpointAfterOpen";

		// Token: 0x04001140 RID: 4416
		internal const string SFxServiceHostBaseCannotAddEndpointWithoutDescription = "SFxServiceHostBaseCannotAddEndpointWithoutDescription";

		// Token: 0x04001141 RID: 4417
		internal const string SFxServiceHostBaseCannotApplyConfigurationWithoutDescription = "SFxServiceHostBaseCannotApplyConfigurationWithoutDescription";

		// Token: 0x04001142 RID: 4418
		internal const string SFxServiceHostBaseCannotLoadConfigurationSectionWithoutDescription = "SFxServiceHostBaseCannotLoadConfigurationSectionWithoutDescription";

		// Token: 0x04001143 RID: 4419
		internal const string SFxServiceHostBaseCannotInitializeRuntimeWithoutDescription = "SFxServiceHostBaseCannotInitializeRuntimeWithoutDescription";

		// Token: 0x04001144 RID: 4420
		internal const string SFxServiceHostCannotCreateDescriptionWithoutServiceType = "SFxServiceHostCannotCreateDescriptionWithoutServiceType";

		// Token: 0x04001145 RID: 4421
		internal const string SFxStaticMessageHeaderPropertiesNotAllowed = "SFxStaticMessageHeaderPropertiesNotAllowed";

		// Token: 0x04001146 RID: 4422
		internal const string SFxStreamIOException = "SFxStreamIOException";

		// Token: 0x04001147 RID: 4423
		internal const string SFxStreamRequestMessageClosed = "SFxStreamRequestMessageClosed";

		// Token: 0x04001148 RID: 4424
		internal const string SFxStreamResponseMessageClosed = "SFxStreamResponseMessageClosed";

		// Token: 0x04001149 RID: 4425
		internal const string SFxTerminatingOperationAlreadyCalled1 = "SFxTerminatingOperationAlreadyCalled1";

		// Token: 0x0400114A RID: 4426
		internal const string SFxThrottleLimitMustBeGreaterThanZero0 = "SFxThrottleLimitMustBeGreaterThanZero0";

		// Token: 0x0400114B RID: 4427
		internal const string SFxTimeoutInvalidStringFormat = "SFxTimeoutInvalidStringFormat";

		// Token: 0x0400114C RID: 4428
		internal const string SFxTimeoutOutOfRange0 = "SFxTimeoutOutOfRange0";

		// Token: 0x0400114D RID: 4429
		internal const string SFxTimeoutOutOfRangeTooBig = "SFxTimeoutOutOfRangeTooBig";

		// Token: 0x0400114E RID: 4430
		internal const string SFxTooManyPartsWithSameName = "SFxTooManyPartsWithSameName";

		// Token: 0x0400114F RID: 4431
		internal const string SFxTraceCodeElementIgnored = "SFxTraceCodeElementIgnored";

		// Token: 0x04001150 RID: 4432
		internal const string SfxTransactedBindingNeeded = "SfxTransactedBindingNeeded";

		// Token: 0x04001151 RID: 4433
		internal const string SFxTransactionNonConcurrentOrAutoComplete2 = "SFxTransactionNonConcurrentOrAutoComplete2";

		// Token: 0x04001152 RID: 4434
		internal const string SFxTransactionNonConcurrentOrReleaseServiceInstanceOnTxComplete = "SFxTransactionNonConcurrentOrReleaseServiceInstanceOnTxComplete";

		// Token: 0x04001153 RID: 4435
		internal const string SFxNonConcurrentOrEnsureOrderedDispatch = "SFxNonConcurrentOrEnsureOrderedDispatch";

		// Token: 0x04001154 RID: 4436
		internal const string SfxDispatchRuntimeNonConcurrentOrEnsureOrderedDispatch = "SfxDispatchRuntimeNonConcurrentOrEnsureOrderedDispatch";

		// Token: 0x04001155 RID: 4437
		internal const string SFxTransactionsNotSupported = "SFxTransactionsNotSupported";

		// Token: 0x04001156 RID: 4438
		internal const string SFxTransactionAsyncAborted = "SFxTransactionAsyncAborted";

		// Token: 0x04001157 RID: 4439
		internal const string SFxTransactionInvalidSetTransactionComplete = "SFxTransactionInvalidSetTransactionComplete";

		// Token: 0x04001158 RID: 4440
		internal const string SFxMultiSetTransactionComplete = "SFxMultiSetTransactionComplete";

		// Token: 0x04001159 RID: 4441
		internal const string SFxTransactionFlowAndMSMQ = "SFxTransactionFlowAndMSMQ";

		// Token: 0x0400115A RID: 4442
		internal const string SFxTransactionAutoCompleteFalseAndInstanceContextMode = "SFxTransactionAutoCompleteFalseAndInstanceContextMode";

		// Token: 0x0400115B RID: 4443
		internal const string SFxTransactionAutoCompleteFalseOnCallbackContract = "SFxTransactionAutoCompleteFalseOnCallbackContract";

		// Token: 0x0400115C RID: 4444
		internal const string SFxTransactionAutoCompleteFalseAndSupportsSession = "SFxTransactionAutoCompleteFalseAndSupportsSession";

		// Token: 0x0400115D RID: 4445
		internal const string SFxTransactionAutoCompleteOnSessionCloseNoSession = "SFxTransactionAutoCompleteOnSessionCloseNoSession";

		// Token: 0x0400115E RID: 4446
		internal const string SFxTransactionTransactionTimeoutNeedsScope = "SFxTransactionTransactionTimeoutNeedsScope";

		// Token: 0x0400115F RID: 4447
		internal const string SFxTransactionIsolationLevelNeedsScope = "SFxTransactionIsolationLevelNeedsScope";

		// Token: 0x04001160 RID: 4448
		internal const string SFxTransactionReleaseServiceInstanceOnTransactionCompleteNeedsScope = "SFxTransactionReleaseServiceInstanceOnTransactionCompleteNeedsScope";

		// Token: 0x04001161 RID: 4449
		internal const string SFxTransactionTransactionAutoCompleteOnSessionCloseNeedsScope = "SFxTransactionTransactionAutoCompleteOnSessionCloseNeedsScope";

		// Token: 0x04001162 RID: 4450
		internal const string SFxTransactionFlowRequired = "SFxTransactionFlowRequired";

		// Token: 0x04001163 RID: 4451
		internal const string SFxTransactionUnmarshalFailed = "SFxTransactionUnmarshalFailed";

		// Token: 0x04001164 RID: 4452
		internal const string SFxTransactionDeserializationFailed = "SFxTransactionDeserializationFailed";

		// Token: 0x04001165 RID: 4453
		internal const string SFxTransactionHeaderNotUnderstood = "SFxTransactionHeaderNotUnderstood";

		// Token: 0x04001166 RID: 4454
		internal const string SFxTryAddMultipleTransactionsOnMessage = "SFxTryAddMultipleTransactionsOnMessage";

		// Token: 0x04001167 RID: 4455
		internal const string SFxTypedMessageCannotBeNull = "SFxTypedMessageCannotBeNull";

		// Token: 0x04001168 RID: 4456
		internal const string SFxTypedMessageCannotBeRpcLiteral = "SFxTypedMessageCannotBeRpcLiteral";

		// Token: 0x04001169 RID: 4457
		internal const string SFxTypedOrUntypedMessageCannotBeMixedWithParameters = "SFxTypedOrUntypedMessageCannotBeMixedWithParameters";

		// Token: 0x0400116A RID: 4458
		internal const string SFxTypedOrUntypedMessageCannotBeMixedWithVoidInRpc = "SFxTypedOrUntypedMessageCannotBeMixedWithVoidInRpc";

		// Token: 0x0400116B RID: 4459
		internal const string SFxUnknownFaultNoMatchingTranslation1 = "SFxUnknownFaultNoMatchingTranslation1";

		// Token: 0x0400116C RID: 4460
		internal const string SFxUnknownFaultNullReason0 = "SFxUnknownFaultNullReason0";

		// Token: 0x0400116D RID: 4461
		internal const string SFxUnknownFaultZeroReasons0 = "SFxUnknownFaultZeroReasons0";

		// Token: 0x0400116E RID: 4462
		internal const string SFxUserCodeThrewException = "SFxUserCodeThrewException";

		// Token: 0x0400116F RID: 4463
		internal const string SfxUseTypedMessageForCustomAttributes = "SfxUseTypedMessageForCustomAttributes";

		// Token: 0x04001170 RID: 4464
		internal const string SFxWellKnownNonSingleton0 = "SFxWellKnownNonSingleton0";

		// Token: 0x04001171 RID: 4465
		internal const string SFxVersionMismatchInOperationContextAndMessage2 = "SFxVersionMismatchInOperationContextAndMessage2";

		// Token: 0x04001172 RID: 4466
		internal const string SFxWhenMultipleEndpointsShareAListenUriTheyMustHaveSameIdentity = "SFxWhenMultipleEndpointsShareAListenUriTheyMustHaveSameIdentity";

		// Token: 0x04001173 RID: 4467
		internal const string SFxWrapperNameCannotBeEmpty = "SFxWrapperNameCannotBeEmpty";

		// Token: 0x04001174 RID: 4468
		internal const string SFxWrapperTypeHasMultipleNamespaces = "SFxWrapperTypeHasMultipleNamespaces";

		// Token: 0x04001175 RID: 4469
		internal const string SFxWsdlPartMustHaveElementOrType = "SFxWsdlPartMustHaveElementOrType";

		// Token: 0x04001176 RID: 4470
		internal const string SFxDataContractSerializerDoesNotSupportBareArray = "SFxDataContractSerializerDoesNotSupportBareArray";

		// Token: 0x04001177 RID: 4471
		internal const string SFxDataContractSerializerDoesNotSupportEncoded = "SFxDataContractSerializerDoesNotSupportEncoded";

		// Token: 0x04001178 RID: 4472
		internal const string SFxXmlArrayNotAllowedForMultiple = "SFxXmlArrayNotAllowedForMultiple";

		// Token: 0x04001179 RID: 4473
		internal const string SFxConfigContractNotFound = "SFxConfigContractNotFound";

		// Token: 0x0400117A RID: 4474
		internal const string SFxConfigChannelConfigurationNotFound = "SFxConfigChannelConfigurationNotFound";

		// Token: 0x0400117B RID: 4475
		internal const string SFxChannelFactoryEndpointAddressUri = "SFxChannelFactoryEndpointAddressUri";

		// Token: 0x0400117C RID: 4476
		internal const string SFxServiceContractGeneratorConfigRequired = "SFxServiceContractGeneratorConfigRequired";

		// Token: 0x0400117D RID: 4477
		internal const string SFxCloseTimedOut1 = "SFxCloseTimedOut1";

		// Token: 0x0400117E RID: 4478
		internal const string SfxCloseTimedOutWaitingForDispatchToComplete = "SfxCloseTimedOutWaitingForDispatchToComplete";

		// Token: 0x0400117F RID: 4479
		internal const string SFxInvalidWsdlBindingOpMismatch2 = "SFxInvalidWsdlBindingOpMismatch2";

		// Token: 0x04001180 RID: 4480
		internal const string SFxInvalidWsdlBindingOpNoName = "SFxInvalidWsdlBindingOpNoName";

		// Token: 0x04001181 RID: 4481
		internal const string SFxChannelFactoryNoBindingFoundInConfig1 = "SFxChannelFactoryNoBindingFoundInConfig1";

		// Token: 0x04001182 RID: 4482
		internal const string SFxChannelFactoryNoBindingFoundInConfigOrCode = "SFxChannelFactoryNoBindingFoundInConfigOrCode";

		// Token: 0x04001183 RID: 4483
		internal const string SFxConfigLoaderMultipleEndpointMatchesSpecified2 = "SFxConfigLoaderMultipleEndpointMatchesSpecified2";

		// Token: 0x04001184 RID: 4484
		internal const string SFxConfigLoaderMultipleEndpointMatchesWildcard1 = "SFxConfigLoaderMultipleEndpointMatchesWildcard1";

		// Token: 0x04001185 RID: 4485
		internal const string SFxProxyRuntimeMessageCannotBeNull = "SFxProxyRuntimeMessageCannotBeNull";

		// Token: 0x04001186 RID: 4486
		internal const string SFxDispatchRuntimeMessageCannotBeNull = "SFxDispatchRuntimeMessageCannotBeNull";

		// Token: 0x04001187 RID: 4487
		internal const string SFxServiceHostNeedsClass = "SFxServiceHostNeedsClass";

		// Token: 0x04001188 RID: 4488
		internal const string SfxReflectedContractKeyNotFound2 = "SfxReflectedContractKeyNotFound2";

		// Token: 0x04001189 RID: 4489
		internal const string SfxReflectedContractKeyNotFoundEmpty = "SfxReflectedContractKeyNotFoundEmpty";

		// Token: 0x0400118A RID: 4490
		internal const string SfxReflectedContractKeyNotFoundIMetadataExchange = "SfxReflectedContractKeyNotFoundIMetadataExchange";

		// Token: 0x0400118B RID: 4491
		internal const string SfxServiceContractAttributeNotFound = "SfxServiceContractAttributeNotFound";

		// Token: 0x0400118C RID: 4492
		internal const string SfxReflectedContractsNotInitialized1 = "SfxReflectedContractsNotInitialized1";

		// Token: 0x0400118D RID: 4493
		internal const string SFxMessagePartDescriptionMissingType = "SFxMessagePartDescriptionMissingType";

		// Token: 0x0400118E RID: 4494
		internal const string SFxWsdlOperationInputNeedsMessageAttribute2 = "SFxWsdlOperationInputNeedsMessageAttribute2";

		// Token: 0x0400118F RID: 4495
		internal const string SFxWsdlOperationOutputNeedsMessageAttribute2 = "SFxWsdlOperationOutputNeedsMessageAttribute2";

		// Token: 0x04001190 RID: 4496
		internal const string SFxWsdlOperationFaultNeedsMessageAttribute2 = "SFxWsdlOperationFaultNeedsMessageAttribute2";

		// Token: 0x04001191 RID: 4497
		internal const string SFxMessageContractAttributeRequired = "SFxMessageContractAttributeRequired";

		// Token: 0x04001192 RID: 4498
		internal const string AChannelServiceEndpointIsNull0 = "AChannelServiceEndpointIsNull0";

		// Token: 0x04001193 RID: 4499
		internal const string AChannelServiceEndpointSBindingIsNull0 = "AChannelServiceEndpointSBindingIsNull0";

		// Token: 0x04001194 RID: 4500
		internal const string AChannelServiceEndpointSContractIsNull0 = "AChannelServiceEndpointSContractIsNull0";

		// Token: 0x04001195 RID: 4501
		internal const string AChannelServiceEndpointSContractSNameIsNull0 = "AChannelServiceEndpointSContractSNameIsNull0";

		// Token: 0x04001196 RID: 4502
		internal const string AChannelServiceEndpointSContractSNamespace0 = "AChannelServiceEndpointSContractSNamespace0";

		// Token: 0x04001197 RID: 4503
		internal const string ServiceHasZeroAppEndpoints = "ServiceHasZeroAppEndpoints";

		// Token: 0x04001198 RID: 4504
		internal const string BindingRequirementsAttributeRequiresQueuedDelivery1 = "BindingRequirementsAttributeRequiresQueuedDelivery1";

		// Token: 0x04001199 RID: 4505
		internal const string BindingRequirementsAttributeDisallowsQueuedDelivery1 = "BindingRequirementsAttributeDisallowsQueuedDelivery1";

		// Token: 0x0400119A RID: 4506
		internal const string SinceTheBindingForDoesnTSupportIBindingCapabilities1_1 = "SinceTheBindingForDoesnTSupportIBindingCapabilities1_1";

		// Token: 0x0400119B RID: 4507
		internal const string SinceTheBindingForDoesnTSupportIBindingCapabilities2_1 = "SinceTheBindingForDoesnTSupportIBindingCapabilities2_1";

		// Token: 0x0400119C RID: 4508
		internal const string TheBindingForDoesnTSupportOrderedDelivery1 = "TheBindingForDoesnTSupportOrderedDelivery1";

		// Token: 0x0400119D RID: 4509
		internal const string ChannelHasAtLeastOneOperationWithTransactionFlowEnabled = "ChannelHasAtLeastOneOperationWithTransactionFlowEnabled";

		// Token: 0x0400119E RID: 4510
		internal const string ServiceHasAtLeastOneOperationWithTransactionFlowEnabled = "ServiceHasAtLeastOneOperationWithTransactionFlowEnabled";

		// Token: 0x0400119F RID: 4511
		internal const string SFxNoEndpointMatchingContract = "SFxNoEndpointMatchingContract";

		// Token: 0x040011A0 RID: 4512
		internal const string SFxNoEndpointMatchingAddress = "SFxNoEndpointMatchingAddress";

		// Token: 0x040011A1 RID: 4513
		internal const string SFxNoEndpointMatchingAddressForConnectionOpeningMessage = "SFxNoEndpointMatchingAddressForConnectionOpeningMessage";

		// Token: 0x040011A2 RID: 4514
		internal const string SFxServiceChannelCannotBeCalledBecauseIsSessionOpenNotificationEnabled = "SFxServiceChannelCannotBeCalledBecauseIsSessionOpenNotificationEnabled";

		// Token: 0x040011A3 RID: 4515
		internal const string EndMethodsCannotBeDecoratedWithOperationContractAttribute = "EndMethodsCannotBeDecoratedWithOperationContractAttribute";

		// Token: 0x040011A4 RID: 4516
		internal const string WsatMessagingInitializationFailed = "WsatMessagingInitializationFailed";

		// Token: 0x040011A5 RID: 4517
		internal const string WsatProxyCreationFailed = "WsatProxyCreationFailed";

		// Token: 0x040011A6 RID: 4518
		internal const string DispatchRuntimeRequiresFormatter0 = "DispatchRuntimeRequiresFormatter0";

		// Token: 0x040011A7 RID: 4519
		internal const string ClientRuntimeRequiresFormatter0 = "ClientRuntimeRequiresFormatter0";

		// Token: 0x040011A8 RID: 4520
		internal const string RuntimeRequiresInvoker0 = "RuntimeRequiresInvoker0";

		// Token: 0x040011A9 RID: 4521
		internal const string CouldnTCreateChannelForType2 = "CouldnTCreateChannelForType2";

		// Token: 0x040011AA RID: 4522
		internal const string CouldnTCreateChannelForChannelType2 = "CouldnTCreateChannelForChannelType2";

		// Token: 0x040011AB RID: 4523
		internal const string EndpointListenerRequirementsCannotBeMetBy3 = "EndpointListenerRequirementsCannotBeMetBy3";

		// Token: 0x040011AC RID: 4524
		internal const string UnknownListenerType1 = "UnknownListenerType1";

		// Token: 0x040011AD RID: 4525
		internal const string BindingDoesnTSupportSessionButContractRequires1 = "BindingDoesnTSupportSessionButContractRequires1";

		// Token: 0x040011AE RID: 4526
		internal const string BindingDoesntSupportDatagramButContractRequires = "BindingDoesntSupportDatagramButContractRequires";

		// Token: 0x040011AF RID: 4527
		internal const string BindingDoesnTSupportOneWayButContractRequires1 = "BindingDoesnTSupportOneWayButContractRequires1";

		// Token: 0x040011B0 RID: 4528
		internal const string BindingDoesnTSupportTwoWayButContractRequires1 = "BindingDoesnTSupportTwoWayButContractRequires1";

		// Token: 0x040011B1 RID: 4529
		internal const string BindingDoesnTSupportRequestReplyButContract1 = "BindingDoesnTSupportRequestReplyButContract1";

		// Token: 0x040011B2 RID: 4530
		internal const string BindingDoesnTSupportDuplexButContractRequires1 = "BindingDoesnTSupportDuplexButContractRequires1";

		// Token: 0x040011B3 RID: 4531
		internal const string BindingDoesnTSupportAnyChannelTypes1 = "BindingDoesnTSupportAnyChannelTypes1";

		// Token: 0x040011B4 RID: 4532
		internal const string ContractIsNotSelfConsistentItHasOneOrMore2 = "ContractIsNotSelfConsistentItHasOneOrMore2";

		// Token: 0x040011B5 RID: 4533
		internal const string ContractIsNotSelfConsistentWhenIsSessionOpenNotificationEnabled = "ContractIsNotSelfConsistentWhenIsSessionOpenNotificationEnabled";

		// Token: 0x040011B6 RID: 4534
		internal const string InstanceSettingsMustHaveTypeOrWellKnownObject0 = "InstanceSettingsMustHaveTypeOrWellKnownObject0";

		// Token: 0x040011B7 RID: 4535
		internal const string TheServiceMetadataExtensionInstanceCouldNot2_0 = "TheServiceMetadataExtensionInstanceCouldNot2_0";

		// Token: 0x040011B8 RID: 4536
		internal const string TheServiceMetadataExtensionInstanceCouldNot3_0 = "TheServiceMetadataExtensionInstanceCouldNot3_0";

		// Token: 0x040011B9 RID: 4537
		internal const string TheServiceMetadataExtensionInstanceCouldNot4_0 = "TheServiceMetadataExtensionInstanceCouldNot4_0";

		// Token: 0x040011BA RID: 4538
		internal const string SynchronizedCollectionWrongType1 = "SynchronizedCollectionWrongType1";

		// Token: 0x040011BB RID: 4539
		internal const string SynchronizedCollectionWrongTypeNull = "SynchronizedCollectionWrongTypeNull";

		// Token: 0x040011BC RID: 4540
		internal const string CannotAddTwoItemsWithTheSameKeyToSynchronizedKeyedCollection0 = "CannotAddTwoItemsWithTheSameKeyToSynchronizedKeyedCollection0";

		// Token: 0x040011BD RID: 4541
		internal const string ItemDoesNotExistInSynchronizedKeyedCollection0 = "ItemDoesNotExistInSynchronizedKeyedCollection0";

		// Token: 0x040011BE RID: 4542
		internal const string SuppliedMessageIsNotAReplyItHasNoRelatesTo0 = "SuppliedMessageIsNotAReplyItHasNoRelatesTo0";

		// Token: 0x040011BF RID: 4543
		internal const string channelIsNotAvailable0 = "channelIsNotAvailable0";

		// Token: 0x040011C0 RID: 4544
		internal const string channelDoesNotHaveADuplexSession0 = "channelDoesNotHaveADuplexSession0";

		// Token: 0x040011C1 RID: 4545
		internal const string EndpointsMustHaveAValidBinding1 = "EndpointsMustHaveAValidBinding1";

		// Token: 0x040011C2 RID: 4546
		internal const string ABindingInstanceHasAlreadyBeenAssociatedTo1 = "ABindingInstanceHasAlreadyBeenAssociatedTo1";

		// Token: 0x040011C3 RID: 4547
		internal const string UnabletoImportPolicy = "UnabletoImportPolicy";

		// Token: 0x040011C4 RID: 4548
		internal const string UnImportedAssertionList = "UnImportedAssertionList";

		// Token: 0x040011C5 RID: 4549
		internal const string XPathUnavailable = "XPathUnavailable";

		// Token: 0x040011C6 RID: 4550
		internal const string DuplicatePolicyInWsdlSkipped = "DuplicatePolicyInWsdlSkipped";

		// Token: 0x040011C7 RID: 4551
		internal const string DuplicatePolicyDocumentSkipped = "DuplicatePolicyDocumentSkipped";

		// Token: 0x040011C8 RID: 4552
		internal const string PolicyDocumentMustHaveIdentifier = "PolicyDocumentMustHaveIdentifier";

		// Token: 0x040011C9 RID: 4553
		internal const string XPathPointer = "XPathPointer";

		// Token: 0x040011CA RID: 4554
		internal const string UnableToFindPolicyWithId = "UnableToFindPolicyWithId";

		// Token: 0x040011CB RID: 4555
		internal const string PolicyReferenceInvalidId = "PolicyReferenceInvalidId";

		// Token: 0x040011CC RID: 4556
		internal const string PolicyReferenceMissingURI = "PolicyReferenceMissingURI";

		// Token: 0x040011CD RID: 4557
		internal const string ExceededMaxPolicyComplexity = "ExceededMaxPolicyComplexity";

		// Token: 0x040011CE RID: 4558
		internal const string ExceededMaxPolicySize = "ExceededMaxPolicySize";

		// Token: 0x040011CF RID: 4559
		internal const string UnrecognizedPolicyElementInNamespace = "UnrecognizedPolicyElementInNamespace";

		// Token: 0x040011D0 RID: 4560
		internal const string UnsupportedPolicyDocumentRoot = "UnsupportedPolicyDocumentRoot";

		// Token: 0x040011D1 RID: 4561
		internal const string UnrecognizedPolicyDocumentNamespace = "UnrecognizedPolicyDocumentNamespace";

		// Token: 0x040011D2 RID: 4562
		internal const string NoUsablePolicyAssertions = "NoUsablePolicyAssertions";

		// Token: 0x040011D3 RID: 4563
		internal const string PolicyInWsdlMustHaveFragmentId = "PolicyInWsdlMustHaveFragmentId";

		// Token: 0x040011D4 RID: 4564
		internal const string FailedImportOfWsdl = "FailedImportOfWsdl";

		// Token: 0x040011D5 RID: 4565
		internal const string OptionalWSDLExtensionIgnored = "OptionalWSDLExtensionIgnored";

		// Token: 0x040011D6 RID: 4566
		internal const string RequiredWSDLExtensionIgnored = "RequiredWSDLExtensionIgnored";

		// Token: 0x040011D7 RID: 4567
		internal const string UnknownWSDLExtensionIgnored = "UnknownWSDLExtensionIgnored";

		// Token: 0x040011D8 RID: 4568
		internal const string WsdlExporterIsFaulted = "WsdlExporterIsFaulted";

		// Token: 0x040011D9 RID: 4569
		internal const string WsdlImporterIsFaulted = "WsdlImporterIsFaulted";

		// Token: 0x040011DA RID: 4570
		internal const string WsdlImporterContractMustBeInKnownContracts = "WsdlImporterContractMustBeInKnownContracts";

		// Token: 0x040011DB RID: 4571
		internal const string WsdlItemAlreadyFaulted = "WsdlItemAlreadyFaulted";

		// Token: 0x040011DC RID: 4572
		internal const string InvalidPolicyExtensionTypeInConfig = "InvalidPolicyExtensionTypeInConfig";

		// Token: 0x040011DD RID: 4573
		internal const string PolicyExtensionTypeRequiresDefaultConstructor = "PolicyExtensionTypeRequiresDefaultConstructor";

		// Token: 0x040011DE RID: 4574
		internal const string PolicyExtensionImportError = "PolicyExtensionImportError";

		// Token: 0x040011DF RID: 4575
		internal const string PolicyExtensionExportError = "PolicyExtensionExportError";

		// Token: 0x040011E0 RID: 4576
		internal const string MultipleCallsToExportContractWithSameContract = "MultipleCallsToExportContractWithSameContract";

		// Token: 0x040011E1 RID: 4577
		internal const string DuplicateContractQNameNameOnExport = "DuplicateContractQNameNameOnExport";

		// Token: 0x040011E2 RID: 4578
		internal const string WarnDuplicateBindingQNameNameOnExport = "WarnDuplicateBindingQNameNameOnExport";

		// Token: 0x040011E3 RID: 4579
		internal const string WarnSkippingOpertationWithWildcardAction = "WarnSkippingOpertationWithWildcardAction";

		// Token: 0x040011E4 RID: 4580
		internal const string WarnSkippingOpertationWithSessionOpenNotificationEnabled = "WarnSkippingOpertationWithSessionOpenNotificationEnabled";

		// Token: 0x040011E5 RID: 4581
		internal const string InvalidWsdlExtensionTypeInConfig = "InvalidWsdlExtensionTypeInConfig";

		// Token: 0x040011E6 RID: 4582
		internal const string WsdlExtensionTypeRequiresDefaultConstructor = "WsdlExtensionTypeRequiresDefaultConstructor";

		// Token: 0x040011E7 RID: 4583
		internal const string WsdlExtensionContractExportError = "WsdlExtensionContractExportError";

		// Token: 0x040011E8 RID: 4584
		internal const string WsdlExtensionEndpointExportError = "WsdlExtensionEndpointExportError";

		// Token: 0x040011E9 RID: 4585
		internal const string WsdlExtensionBeforeImportError = "WsdlExtensionBeforeImportError";

		// Token: 0x040011EA RID: 4586
		internal const string WsdlExtensionImportError = "WsdlExtensionImportError";

		// Token: 0x040011EB RID: 4587
		internal const string WsdlImportErrorMessageDetail = "WsdlImportErrorMessageDetail";

		// Token: 0x040011EC RID: 4588
		internal const string WsdlImportErrorDependencyDetail = "WsdlImportErrorDependencyDetail";

		// Token: 0x040011ED RID: 4589
		internal const string UnsupportedEnvelopeVersion = "UnsupportedEnvelopeVersion";

		// Token: 0x040011EE RID: 4590
		internal const string NoValue0 = "NoValue0";

		// Token: 0x040011EF RID: 4591
		internal const string UnsupportedBindingElementClone = "UnsupportedBindingElementClone";

		// Token: 0x040011F0 RID: 4592
		internal const string UnrecognizedBindingAssertions1 = "UnrecognizedBindingAssertions1";

		// Token: 0x040011F1 RID: 4593
		internal const string ServicesWithoutAServiceContractAttributeCan2 = "ServicesWithoutAServiceContractAttributeCan2";

		// Token: 0x040011F2 RID: 4594
		internal const string tooManyAttributesOfTypeOn2 = "tooManyAttributesOfTypeOn2";

		// Token: 0x040011F3 RID: 4595
		internal const string couldnTFindRequiredAttributeOfTypeOn2 = "couldnTFindRequiredAttributeOfTypeOn2";

		// Token: 0x040011F4 RID: 4596
		internal const string AttemptedToGetContractTypeForButThatTypeIs1 = "AttemptedToGetContractTypeForButThatTypeIs1";

		// Token: 0x040011F5 RID: 4597
		internal const string NoEndMethodFoundForAsyncBeginMethod3 = "NoEndMethodFoundForAsyncBeginMethod3";

		// Token: 0x040011F6 RID: 4598
		internal const string MoreThanOneEndMethodFoundForAsyncBeginMethod3 = "MoreThanOneEndMethodFoundForAsyncBeginMethod3";

		// Token: 0x040011F7 RID: 4599
		internal const string InvalidAsyncEndMethodSignatureForMethod2 = "InvalidAsyncEndMethodSignatureForMethod2";

		// Token: 0x040011F8 RID: 4600
		internal const string InvalidAsyncBeginMethodSignatureForMethod2 = "InvalidAsyncBeginMethodSignatureForMethod2";

		// Token: 0x040011F9 RID: 4601
		internal const string InAContractInheritanceHierarchyIfParentHasCallbackChildMustToo = "InAContractInheritanceHierarchyIfParentHasCallbackChildMustToo";

		// Token: 0x040011FA RID: 4602
		internal const string InAContractInheritanceHierarchyTheServiceContract3_2 = "InAContractInheritanceHierarchyTheServiceContract3_2";

		// Token: 0x040011FB RID: 4603
		internal const string CannotHaveTwoOperationsWithTheSameName3 = "CannotHaveTwoOperationsWithTheSameName3";

		// Token: 0x040011FC RID: 4604
		internal const string CannotHaveTwoOperationsWithTheSameElement5 = "CannotHaveTwoOperationsWithTheSameElement5";

		// Token: 0x040011FD RID: 4605
		internal const string CannotInheritTwoOperationsWithTheSameName3 = "CannotInheritTwoOperationsWithTheSameName3";

		// Token: 0x040011FE RID: 4606
		internal const string SyncAsyncMatchConsistency_Parameters5 = "SyncAsyncMatchConsistency_Parameters5";

		// Token: 0x040011FF RID: 4607
		internal const string SyncTaskMatchConsistency_Parameters5 = "SyncTaskMatchConsistency_Parameters5";

		// Token: 0x04001200 RID: 4608
		internal const string TaskAsyncMatchConsistency_Parameters5 = "TaskAsyncMatchConsistency_Parameters5";

		// Token: 0x04001201 RID: 4609
		internal const string SyncAsyncMatchConsistency_ReturnType5 = "SyncAsyncMatchConsistency_ReturnType5";

		// Token: 0x04001202 RID: 4610
		internal const string SyncTaskMatchConsistency_ReturnType5 = "SyncTaskMatchConsistency_ReturnType5";

		// Token: 0x04001203 RID: 4611
		internal const string TaskAsyncMatchConsistency_ReturnType5 = "TaskAsyncMatchConsistency_ReturnType5";

		// Token: 0x04001204 RID: 4612
		internal const string SyncAsyncMatchConsistency_Attributes6 = "SyncAsyncMatchConsistency_Attributes6";

		// Token: 0x04001205 RID: 4613
		internal const string SyncTaskMatchConsistency_Attributes6 = "SyncTaskMatchConsistency_Attributes6";

		// Token: 0x04001206 RID: 4614
		internal const string TaskAsyncMatchConsistency_Attributes6 = "TaskAsyncMatchConsistency_Attributes6";

		// Token: 0x04001207 RID: 4615
		internal const string SyncAsyncMatchConsistency_Property6 = "SyncAsyncMatchConsistency_Property6";

		// Token: 0x04001208 RID: 4616
		internal const string SyncTaskMatchConsistency_Property6 = "SyncTaskMatchConsistency_Property6";

		// Token: 0x04001209 RID: 4617
		internal const string TaskAsyncMatchConsistency_Property6 = "TaskAsyncMatchConsistency_Property6";

		// Token: 0x0400120A RID: 4618
		internal const string ServiceOperationsMarkedWithIsOneWayTrueMust0 = "ServiceOperationsMarkedWithIsOneWayTrueMust0";

		// Token: 0x0400120B RID: 4619
		internal const string OneWayOperationShouldNotSpecifyAReplyAction1 = "OneWayOperationShouldNotSpecifyAReplyAction1";

		// Token: 0x0400120C RID: 4620
		internal const string OneWayAndFaultsIncompatible2 = "OneWayAndFaultsIncompatible2";

		// Token: 0x0400120D RID: 4621
		internal const string OnlyMalformedMessagesAreSupported = "OnlyMalformedMessagesAreSupported";

		// Token: 0x0400120E RID: 4622
		internal const string UnableToLocateOperation2 = "UnableToLocateOperation2";

		// Token: 0x0400120F RID: 4623
		internal const string UnsupportedWSDLOnlyOneMessage = "UnsupportedWSDLOnlyOneMessage";

		// Token: 0x04001210 RID: 4624
		internal const string UnsupportedWSDLTheFault = "UnsupportedWSDLTheFault";

		// Token: 0x04001211 RID: 4625
		internal const string AsyncEndCalledOnWrongChannel = "AsyncEndCalledOnWrongChannel";

		// Token: 0x04001212 RID: 4626
		internal const string AsyncEndCalledWithAnIAsyncResult = "AsyncEndCalledWithAnIAsyncResult";

		// Token: 0x04001213 RID: 4627
		internal const string IsolationLevelMismatch2 = "IsolationLevelMismatch2";

		// Token: 0x04001214 RID: 4628
		internal const string MessageHeaderIsNull0 = "MessageHeaderIsNull0";

		// Token: 0x04001215 RID: 4629
		internal const string MessagePropertiesArraySize0 = "MessagePropertiesArraySize0";

		// Token: 0x04001216 RID: 4630
		internal const string DuplicateBehavior1 = "DuplicateBehavior1";

		// Token: 0x04001217 RID: 4631
		internal const string CantCreateChannelWithManualAddressing = "CantCreateChannelWithManualAddressing";

		// Token: 0x04001218 RID: 4632
		internal const string XsdMissingRequiredAttribute1 = "XsdMissingRequiredAttribute1";

		// Token: 0x04001219 RID: 4633
		internal const string IgnoreSoapHeaderBinding3 = "IgnoreSoapHeaderBinding3";

		// Token: 0x0400121A RID: 4634
		internal const string IgnoreSoapFaultBinding3 = "IgnoreSoapFaultBinding3";

		// Token: 0x0400121B RID: 4635
		internal const string IgnoreMessagePart3 = "IgnoreMessagePart3";

		// Token: 0x0400121C RID: 4636
		internal const string CannotImportPrivacyNoticeElementWithoutVersionAttribute = "CannotImportPrivacyNoticeElementWithoutVersionAttribute";

		// Token: 0x0400121D RID: 4637
		internal const string PrivacyNoticeElementVersionAttributeInvalid = "PrivacyNoticeElementVersionAttributeInvalid";

		// Token: 0x0400121E RID: 4638
		internal const string MsmqActiveDirectoryRequiresNativeTransfer = "MsmqActiveDirectoryRequiresNativeTransfer";

		// Token: 0x0400121F RID: 4639
		internal const string MsmqAdvancedPoisonHandlingRequired = "MsmqAdvancedPoisonHandlingRequired";

		// Token: 0x04001220 RID: 4640
		internal const string MsmqAmbientTransactionInactive = "MsmqAmbientTransactionInactive";

		// Token: 0x04001221 RID: 4641
		internal const string MsmqAuthCertificateRequiresProtectionSign = "MsmqAuthCertificateRequiresProtectionSign";

		// Token: 0x04001222 RID: 4642
		internal const string MsmqAuthNoneRequiresProtectionNone = "MsmqAuthNoneRequiresProtectionNone";

		// Token: 0x04001223 RID: 4643
		internal const string MsmqAuthWindowsRequiresProtectionNotNone = "MsmqAuthWindowsRequiresProtectionNotNone";

		// Token: 0x04001224 RID: 4644
		internal const string MsmqBadCertificate = "MsmqBadCertificate";

		// Token: 0x04001225 RID: 4645
		internal const string MsmqBadContentType = "MsmqBadContentType";

		// Token: 0x04001226 RID: 4646
		internal const string MsmqBadFrame = "MsmqBadFrame";

		// Token: 0x04001227 RID: 4647
		internal const string MsmqBadXml = "MsmqBadXml";

		// Token: 0x04001228 RID: 4648
		internal const string MsmqBatchRequiresTransactionScope = "MsmqBatchRequiresTransactionScope";

		// Token: 0x04001229 RID: 4649
		internal const string MsmqByteArrayBodyExpected = "MsmqByteArrayBodyExpected";

		// Token: 0x0400122A RID: 4650
		internal const string MsmqCannotDeserializeActiveXMessage = "MsmqCannotDeserializeActiveXMessage";

		// Token: 0x0400122B RID: 4651
		internal const string MsmqCannotDeserializeXmlMessage = "MsmqCannotDeserializeXmlMessage";

		// Token: 0x0400122C RID: 4652
		internal const string MsmqCannotUseBodyTypeWithActiveXSerialization = "MsmqCannotUseBodyTypeWithActiveXSerialization";

		// Token: 0x0400122D RID: 4653
		internal const string MsmqCertificateNotFound = "MsmqCertificateNotFound";

		// Token: 0x0400122E RID: 4654
		internal const string MsmqCustomRequiresPerAppDLQ = "MsmqCustomRequiresPerAppDLQ";

		// Token: 0x0400122F RID: 4655
		internal const string MsmqDeserializationError = "MsmqDeserializationError";

		// Token: 0x04001230 RID: 4656
		internal const string MsmqDirectFormatNameRequiredForPoison = "MsmqDirectFormatNameRequiredForPoison";

		// Token: 0x04001231 RID: 4657
		internal const string MsmqDLQNotLocal = "MsmqDLQNotLocal";

		// Token: 0x04001232 RID: 4658
		internal const string MsmqDLQNotWriteable = "MsmqDLQNotWriteable";

		// Token: 0x04001233 RID: 4659
		internal const string MsmqEncryptRequiresUseAD = "MsmqEncryptRequiresUseAD";

		// Token: 0x04001234 RID: 4660
		internal const string MsmqExactlyOnceNeededForReceiveContext = "MsmqExactlyOnceNeededForReceiveContext";

		// Token: 0x04001235 RID: 4661
		internal const string MsmqGetPrivateComputerInformationError = "MsmqGetPrivateComputerInformationError";

		// Token: 0x04001236 RID: 4662
		internal const string MsmqInvalidMessageId = "MsmqInvalidMessageId";

		// Token: 0x04001237 RID: 4663
		internal const string MsmqInvalidScheme = "MsmqInvalidScheme";

		// Token: 0x04001238 RID: 4664
		internal const string MsmqInvalidServiceOperationForMsmqIntegrationBinding = "MsmqInvalidServiceOperationForMsmqIntegrationBinding";

		// Token: 0x04001239 RID: 4665
		internal const string MsmqInvalidTypeDeserialization = "MsmqInvalidTypeDeserialization";

		// Token: 0x0400123A RID: 4666
		internal const string MsmqInvalidTypeSerialization = "MsmqInvalidTypeSerialization";

		// Token: 0x0400123B RID: 4667
		internal const string MsmqKnownWin32Error = "MsmqKnownWin32Error";

		// Token: 0x0400123C RID: 4668
		internal const string MsmqMessageDoesntHaveIntegrationProperty = "MsmqMessageDoesntHaveIntegrationProperty";

		// Token: 0x0400123D RID: 4669
		internal const string MsmqNoAssurancesForVolatile = "MsmqNoAssurancesForVolatile";

		// Token: 0x0400123E RID: 4670
		internal const string MsmqNonNegativeArgumentExpected = "MsmqNonNegativeArgumentExpected";

		// Token: 0x0400123F RID: 4671
		internal const string MsmqNonTransactionalQueueNeeded = "MsmqNonTransactionalQueueNeeded";

		// Token: 0x04001240 RID: 4672
		internal const string MsmqNoMoveForSubqueues = "MsmqNoMoveForSubqueues";

		// Token: 0x04001241 RID: 4673
		internal const string MsmqNoSid = "MsmqNoSid";

		// Token: 0x04001242 RID: 4674
		internal const string MsmqOpenError = "MsmqOpenError";

		// Token: 0x04001243 RID: 4675
		internal const string MsmqPathLookupError = "MsmqPathLookupError";

		// Token: 0x04001244 RID: 4676
		internal const string MsmqPerAppDLQRequiresCustom = "MsmqPerAppDLQRequiresCustom";

		// Token: 0x04001245 RID: 4677
		internal const string MsmqPerAppDLQRequiresExactlyOnce = "MsmqPerAppDLQRequiresExactlyOnce";

		// Token: 0x04001246 RID: 4678
		internal const string MsmqPerAppDLQRequiresMsmq4 = "MsmqPerAppDLQRequiresMsmq4";

		// Token: 0x04001247 RID: 4679
		internal const string MsmqPoisonMessage = "MsmqPoisonMessage";

		// Token: 0x04001248 RID: 4680
		internal const string MsmqQueueNotReadable = "MsmqQueueNotReadable";

		// Token: 0x04001249 RID: 4681
		internal const string MsmqReceiveContextMessageNotReceived = "MsmqReceiveContextMessageNotReceived";

		// Token: 0x0400124A RID: 4682
		internal const string MsmqReceiveContextMessageNotMoved = "MsmqReceiveContextMessageNotMoved";

		// Token: 0x0400124B RID: 4683
		internal const string MsmqReceiveContextSubqueuesNotSupported = "MsmqReceiveContextSubqueuesNotSupported";

		// Token: 0x0400124C RID: 4684
		internal const string MsmqReceiveError = "MsmqReceiveError";

		// Token: 0x0400124D RID: 4685
		internal const string MsmqSameTransactionExpected = "MsmqSameTransactionExpected";

		// Token: 0x0400124E RID: 4686
		internal const string MsmqSendError = "MsmqSendError";

		// Token: 0x0400124F RID: 4687
		internal const string MsmqSerializationTableFull = "MsmqSerializationTableFull";

		// Token: 0x04001250 RID: 4688
		internal const string MsmqSessionChannelAbort = "MsmqSessionChannelAbort";

		// Token: 0x04001251 RID: 4689
		internal const string MsmqSessionChannelHasPendingItems = "MsmqSessionChannelHasPendingItems";

		// Token: 0x04001252 RID: 4690
		internal const string MsmqSessionChannelsMustBeClosed = "MsmqSessionChannelsMustBeClosed";

		// Token: 0x04001253 RID: 4691
		internal const string MsmqSessionGramSizeMustBeInIntegerRange = "MsmqSessionGramSizeMustBeInIntegerRange";

		// Token: 0x04001254 RID: 4692
		internal const string MsmqSessionMessagesNotConsumed = "MsmqSessionMessagesNotConsumed";

		// Token: 0x04001255 RID: 4693
		internal const string MsmqSessionPrematureClose = "MsmqSessionPrematureClose";

		// Token: 0x04001256 RID: 4694
		internal const string MsmqStreamBodyExpected = "MsmqStreamBodyExpected";

		// Token: 0x04001257 RID: 4695
		internal const string MsmqTimeSpanTooLarge = "MsmqTimeSpanTooLarge";

		// Token: 0x04001258 RID: 4696
		internal const string MsmqTokenProviderNeededForCertificates = "MsmqTokenProviderNeededForCertificates";

		// Token: 0x04001259 RID: 4697
		internal const string MsmqTransactionNotActive = "MsmqTransactionNotActive";

		// Token: 0x0400125A RID: 4698
		internal const string MsmqTransactionalQueueNeeded = "MsmqTransactionalQueueNeeded";

		// Token: 0x0400125B RID: 4699
		internal const string MsmqTransactionCurrentRequired = "MsmqTransactionCurrentRequired";

		// Token: 0x0400125C RID: 4700
		internal const string MsmqTransactionRequired = "MsmqTransactionRequired";

		// Token: 0x0400125D RID: 4701
		internal const string MsmqTransactedDLQExpected = "MsmqTransactedDLQExpected";

		// Token: 0x0400125E RID: 4702
		internal const string MsmqUnexpectedPort = "MsmqUnexpectedPort";

		// Token: 0x0400125F RID: 4703
		internal const string MsmqUnknownWin32Error = "MsmqUnknownWin32Error";

		// Token: 0x04001260 RID: 4704
		internal const string MsmqUnsupportedSerializationFormat = "MsmqUnsupportedSerializationFormat";

		// Token: 0x04001261 RID: 4705
		internal const string MsmqWindowsAuthnRequiresAD = "MsmqWindowsAuthnRequiresAD";

		// Token: 0x04001262 RID: 4706
		internal const string MsmqWrongPrivateQueueSyntax = "MsmqWrongPrivateQueueSyntax";

		// Token: 0x04001263 RID: 4707
		internal const string MsmqWrongUri = "MsmqWrongUri";

		// Token: 0x04001264 RID: 4708
		internal const string MsmqCannotReacquireLock = "MsmqCannotReacquireLock";

		// Token: 0x04001265 RID: 4709
		internal const string XDCannotFindValueInDictionaryString = "XDCannotFindValueInDictionaryString";

		// Token: 0x04001266 RID: 4710
		internal const string WmiGetObject = "WmiGetObject";

		// Token: 0x04001267 RID: 4711
		internal const string WmiPutInstance = "WmiPutInstance";

		// Token: 0x04001268 RID: 4712
		internal const string ObjectMustBeOpenedToDequeue = "ObjectMustBeOpenedToDequeue";

		// Token: 0x04001269 RID: 4713
		internal const string NoChannelBuilderAvailable = "NoChannelBuilderAvailable";

		// Token: 0x0400126A RID: 4714
		internal const string InvalidBindingScheme = "InvalidBindingScheme";

		// Token: 0x0400126B RID: 4715
		internal const string CustomBindingRequiresTransport = "CustomBindingRequiresTransport";

		// Token: 0x0400126C RID: 4716
		internal const string TransportBindingElementMustBeLast = "TransportBindingElementMustBeLast";

		// Token: 0x0400126D RID: 4717
		internal const string MessageVersionMissingFromBinding = "MessageVersionMissingFromBinding";

		// Token: 0x0400126E RID: 4718
		internal const string NotAllBindingElementsBuilt = "NotAllBindingElementsBuilt";

		// Token: 0x0400126F RID: 4719
		internal const string MultipleMebesInParameters = "MultipleMebesInParameters";

		// Token: 0x04001270 RID: 4720
		internal const string MultipleStreamUpgradeProvidersInParameters = "MultipleStreamUpgradeProvidersInParameters";

		// Token: 0x04001271 RID: 4721
		internal const string MultiplePeerResolverBindingElementsinParameters = "MultiplePeerResolverBindingElementsinParameters";

		// Token: 0x04001272 RID: 4722
		internal const string MultiplePeerCustomResolverBindingElementsInParameters = "MultiplePeerCustomResolverBindingElementsInParameters";

		// Token: 0x04001273 RID: 4723
		internal const string SecurityCapabilitiesMismatched = "SecurityCapabilitiesMismatched";

		// Token: 0x04001274 RID: 4724
		internal const string BaseAddressMustBeAbsolute = "BaseAddressMustBeAbsolute";

		// Token: 0x04001275 RID: 4725
		internal const string BaseAddressDuplicateScheme = "BaseAddressDuplicateScheme";

		// Token: 0x04001276 RID: 4726
		internal const string BaseAddressCannotHaveUserInfo = "BaseAddressCannotHaveUserInfo";

		// Token: 0x04001277 RID: 4727
		internal const string TransportBindingElementNotFound = "TransportBindingElementNotFound";

		// Token: 0x04001278 RID: 4728
		internal const string ChannelDemuxerBindingElementNotFound = "ChannelDemuxerBindingElementNotFound";

		// Token: 0x04001279 RID: 4729
		internal const string BaseAddressCannotHaveQuery = "BaseAddressCannotHaveQuery";

		// Token: 0x0400127A RID: 4730
		internal const string BaseAddressCannotHaveFragment = "BaseAddressCannotHaveFragment";

		// Token: 0x0400127B RID: 4731
		internal const string UriMustBeAbsolute = "UriMustBeAbsolute";

		// Token: 0x0400127C RID: 4732
		internal const string BindingProtocolMappingNotDefined = "BindingProtocolMappingNotDefined";

		// Token: 0x0400127D RID: 4733
		internal const string ConfigBindingCannotBeConfigured = "ConfigBindingCannotBeConfigured";

		// Token: 0x0400127E RID: 4734
		internal const string ConfigBindingExtensionNotFound = "ConfigBindingExtensionNotFound";

		// Token: 0x0400127F RID: 4735
		internal const string ConfigBindingReferenceCycleDetected = "ConfigBindingReferenceCycleDetected";

		// Token: 0x04001280 RID: 4736
		internal const string ConfigBindingTypeCannotBeNullOrEmpty = "ConfigBindingTypeCannotBeNullOrEmpty";

		// Token: 0x04001281 RID: 4737
		internal const string ConfigCannotParseXPathFilter = "ConfigCannotParseXPathFilter";

		// Token: 0x04001282 RID: 4738
		internal const string ConfigEndpointExtensionNotFound = "ConfigEndpointExtensionNotFound";

		// Token: 0x04001283 RID: 4739
		internal const string ConfigEndpointReferenceCycleDetected = "ConfigEndpointReferenceCycleDetected";

		// Token: 0x04001284 RID: 4740
		internal const string ConfigEndpointTypeCannotBeNullOrEmpty = "ConfigEndpointTypeCannotBeNullOrEmpty";

		// Token: 0x04001285 RID: 4741
		internal const string ConfigXPathFilterMustNotBeEmpty = "ConfigXPathFilterMustNotBeEmpty";

		// Token: 0x04001286 RID: 4742
		internal const string ConfigDuplicateItem = "ConfigDuplicateItem";

		// Token: 0x04001287 RID: 4743
		internal const string ConfigDuplicateExtensionName = "ConfigDuplicateExtensionName";

		// Token: 0x04001288 RID: 4744
		internal const string ConfigDuplicateExtensionType = "ConfigDuplicateExtensionType";

		// Token: 0x04001289 RID: 4745
		internal const string ConfigDuplicateKey = "ConfigDuplicateKey";

		// Token: 0x0400128A RID: 4746
		internal const string ConfigDuplicateKeyAtSameScope = "ConfigDuplicateKeyAtSameScope";

		// Token: 0x0400128B RID: 4747
		internal const string ConfigElementKeyNull = "ConfigElementKeyNull";

		// Token: 0x0400128C RID: 4748
		internal const string ConfigElementKeysNull = "ConfigElementKeysNull";

		// Token: 0x0400128D RID: 4749
		internal const string ConfigElementTypeNotAllowed = "ConfigElementTypeNotAllowed";

		// Token: 0x0400128E RID: 4750
		internal const string ConfigExtensionCollectionNotFound = "ConfigExtensionCollectionNotFound";

		// Token: 0x0400128F RID: 4751
		internal const string ConfigExtensionTypeNotRegisteredInCollection = "ConfigExtensionTypeNotRegisteredInCollection";

		// Token: 0x04001290 RID: 4752
		internal const string ConfigInvalidServiceAuthenticationManagerType = "ConfigInvalidServiceAuthenticationManagerType";

		// Token: 0x04001291 RID: 4753
		internal const string ConfigInvalidAuthorizationPolicyType = "ConfigInvalidAuthorizationPolicyType";

		// Token: 0x04001292 RID: 4754
		internal const string ConfigInvalidBindingConfigurationName = "ConfigInvalidBindingConfigurationName";

		// Token: 0x04001293 RID: 4755
		internal const string ConfigInvalidBindingName = "ConfigInvalidBindingName";

		// Token: 0x04001294 RID: 4756
		internal const string ConfigInvalidCommonEndpointBehaviorType = "ConfigInvalidCommonEndpointBehaviorType";

		// Token: 0x04001295 RID: 4757
		internal const string ConfigInvalidCommonServiceBehaviorType = "ConfigInvalidCommonServiceBehaviorType";

		// Token: 0x04001296 RID: 4758
		internal const string ConfigInvalidCertificateValidatorType = "ConfigInvalidCertificateValidatorType";

		// Token: 0x04001297 RID: 4759
		internal const string ConfigInvalidClientCredentialsType = "ConfigInvalidClientCredentialsType";

		// Token: 0x04001298 RID: 4760
		internal const string ConfigInvalidClassFactoryValue = "ConfigInvalidClassFactoryValue";

		// Token: 0x04001299 RID: 4761
		internal const string ConfigInvalidClassInstanceValue = "ConfigInvalidClassInstanceValue";

		// Token: 0x0400129A RID: 4762
		internal const string ConfigInvalidEncodingValue = "ConfigInvalidEncodingValue";

		// Token: 0x0400129B RID: 4763
		internal const string ConfigInvalidEndpointBehavior = "ConfigInvalidEndpointBehavior";

		// Token: 0x0400129C RID: 4764
		internal const string ConfigInvalidEndpointBehaviorType = "ConfigInvalidEndpointBehaviorType";

		// Token: 0x0400129D RID: 4765
		internal const string ConfigInvalidEndpointName = "ConfigInvalidEndpointName";

		// Token: 0x0400129E RID: 4766
		internal const string ConfigInvalidAttribute = "ConfigInvalidAttribute";

		// Token: 0x0400129F RID: 4767
		internal const string ConfigNoEndpointCreated = "ConfigNoEndpointCreated";

		// Token: 0x040012A0 RID: 4768
		internal const string ConfigInvalidExtensionElement = "ConfigInvalidExtensionElement";

		// Token: 0x040012A1 RID: 4769
		internal const string ConfigInvalidExtensionElementName = "ConfigInvalidExtensionElementName";

		// Token: 0x040012A2 RID: 4770
		internal const string ConfigInvalidExtensionType = "ConfigInvalidExtensionType";

		// Token: 0x040012A3 RID: 4771
		internal const string ConfigInvalidKeyType = "ConfigInvalidKeyType";

		// Token: 0x040012A4 RID: 4772
		internal const string ConfigInvalidReliableMessagingVersionValue = "ConfigInvalidReliableMessagingVersionValue";

		// Token: 0x040012A5 RID: 4773
		internal const string ConfigInvalidSamlSerializerType = "ConfigInvalidSamlSerializerType";

		// Token: 0x040012A6 RID: 4774
		internal const string ConfigInvalidSection = "ConfigInvalidSection";

		// Token: 0x040012A7 RID: 4775
		internal const string ConfigInvalidServiceCredentialsType = "ConfigInvalidServiceCredentialsType";

		// Token: 0x040012A8 RID: 4776
		internal const string ConfigInvalidSecurityStateEncoderType = "ConfigInvalidSecurityStateEncoderType";

		// Token: 0x040012A9 RID: 4777
		internal const string ConfigInvalidUserNamePasswordValidatorType = "ConfigInvalidUserNamePasswordValidatorType";

		// Token: 0x040012AA RID: 4778
		internal const string ConfigInvalidServiceAuthorizationManagerType = "ConfigInvalidServiceAuthorizationManagerType";

		// Token: 0x040012AB RID: 4779
		internal const string ConfigInvalidServiceBehavior = "ConfigInvalidServiceBehavior";

		// Token: 0x040012AC RID: 4780
		internal const string ConfigInvalidServiceBehaviorType = "ConfigInvalidServiceBehaviorType";

		// Token: 0x040012AD RID: 4781
		internal const string ConfigInvalidStartValue = "ConfigInvalidStartValue";

		// Token: 0x040012AE RID: 4782
		internal const string ConfigInvalidTransactionFlowProtocolValue = "ConfigInvalidTransactionFlowProtocolValue";

		// Token: 0x040012AF RID: 4783
		internal const string ConfigInvalidType = "ConfigInvalidType";

		// Token: 0x040012B0 RID: 4784
		internal const string ConfigInvalidTypeForBinding = "ConfigInvalidTypeForBinding";

		// Token: 0x040012B1 RID: 4785
		internal const string ConfigInvalidTypeForBindingElement = "ConfigInvalidTypeForBindingElement";

		// Token: 0x040012B2 RID: 4786
		internal const string ConfigInvalidTypeForEndpoint = "ConfigInvalidTypeForEndpoint";

		// Token: 0x040012B3 RID: 4787
		internal const string ConfigKeyNotFoundInElementCollection = "ConfigKeyNotFoundInElementCollection";

		// Token: 0x040012B4 RID: 4788
		internal const string ConfigKeysDoNotMatch = "ConfigKeysDoNotMatch";

		// Token: 0x040012B5 RID: 4789
		internal const string ConfigMessageEncodingAlreadyInBinding = "ConfigMessageEncodingAlreadyInBinding";

		// Token: 0x040012B6 RID: 4790
		internal const string ConfigNoExtensionCollectionAssociatedWithType = "ConfigNoExtensionCollectionAssociatedWithType";

		// Token: 0x040012B7 RID: 4791
		internal const string ConfigNullIssuerAddress = "ConfigNullIssuerAddress";

		// Token: 0x040012B8 RID: 4792
		internal const string ConfigReadOnly = "ConfigReadOnly";

		// Token: 0x040012B9 RID: 4793
		internal const string ConfigSectionNotFound = "ConfigSectionNotFound";

		// Token: 0x040012BA RID: 4794
		internal const string ConfigStreamUpgradeElementAlreadyInBinding = "ConfigStreamUpgradeElementAlreadyInBinding";

		// Token: 0x040012BB RID: 4795
		internal const string ConfigTransportAlreadyInBinding = "ConfigTransportAlreadyInBinding";

		// Token: 0x040012BC RID: 4796
		internal const string ConfigXmlElementMustBeSet = "ConfigXmlElementMustBeSet";

		// Token: 0x040012BD RID: 4797
		internal const string ConfigXPathFilterIsNull = "ConfigXPathFilterIsNull";

		// Token: 0x040012BE RID: 4798
		internal const string ConfigXPathNamespacePrefixNotFound = "ConfigXPathNamespacePrefixNotFound";

		// Token: 0x040012BF RID: 4799
		internal const string Default = "Default";

		// Token: 0x040012C0 RID: 4800
		internal const string AdminMTAWorkerThreadException = "AdminMTAWorkerThreadException";

		// Token: 0x040012C1 RID: 4801
		internal const string InternalError = "InternalError";

		// Token: 0x040012C2 RID: 4802
		internal const string ClsidNotInApplication = "ClsidNotInApplication";

		// Token: 0x040012C3 RID: 4803
		internal const string ClsidNotInConfiguration = "ClsidNotInConfiguration";

		// Token: 0x040012C4 RID: 4804
		internal const string EndpointNotAnIID = "EndpointNotAnIID";

		// Token: 0x040012C5 RID: 4805
		internal const string ServiceStringFormatError = "ServiceStringFormatError";

		// Token: 0x040012C6 RID: 4806
		internal const string ContractTypeNotAnIID = "ContractTypeNotAnIID";

		// Token: 0x040012C7 RID: 4807
		internal const string ApplicationNotFound = "ApplicationNotFound";

		// Token: 0x040012C8 RID: 4808
		internal const string NoVoteIssued = "NoVoteIssued";

		// Token: 0x040012C9 RID: 4809
		internal const string FailedToConvertTypelibraryToAssembly = "FailedToConvertTypelibraryToAssembly";

		// Token: 0x040012CA RID: 4810
		internal const string BadInterfaceVersion = "BadInterfaceVersion";

		// Token: 0x040012CB RID: 4811
		internal const string FailedToLoadTypeLibrary = "FailedToLoadTypeLibrary";

		// Token: 0x040012CC RID: 4812
		internal const string NativeTypeLibraryNotAllowed = "NativeTypeLibraryNotAllowed";

		// Token: 0x040012CD RID: 4813
		internal const string InterfaceNotFoundInAssembly = "InterfaceNotFoundInAssembly";

		// Token: 0x040012CE RID: 4814
		internal const string UdtNotFoundInAssembly = "UdtNotFoundInAssembly";

		// Token: 0x040012CF RID: 4815
		internal const string UnknownMonikerKeyword = "UnknownMonikerKeyword";

		// Token: 0x040012D0 RID: 4816
		internal const string MonikerIncorectSerializer = "MonikerIncorectSerializer";

		// Token: 0x040012D1 RID: 4817
		internal const string NoEqualSignFound = "NoEqualSignFound";

		// Token: 0x040012D2 RID: 4818
		internal const string KewordMissingValue = "KewordMissingValue";

		// Token: 0x040012D3 RID: 4819
		internal const string BadlyTerminatedValue = "BadlyTerminatedValue";

		// Token: 0x040012D4 RID: 4820
		internal const string MissingQuote = "MissingQuote";

		// Token: 0x040012D5 RID: 4821
		internal const string RepeatedKeyword = "RepeatedKeyword";

		// Token: 0x040012D6 RID: 4822
		internal const string InterfaceNotFoundInConfig = "InterfaceNotFoundInConfig";

		// Token: 0x040012D7 RID: 4823
		internal const string CannotHaveNullOrEmptyNameOrNamespaceForIID = "CannotHaveNullOrEmptyNameOrNamespaceForIID";

		// Token: 0x040012D8 RID: 4824
		internal const string MethodGivenInConfigNotFoundOnInterface = "MethodGivenInConfigNotFoundOnInterface";

		// Token: 0x040012D9 RID: 4825
		internal const string MonikerIncorrectServerIdentityForMex = "MonikerIncorrectServerIdentityForMex";

		// Token: 0x040012DA RID: 4826
		internal const string MonikerAddressNotSpecified = "MonikerAddressNotSpecified";

		// Token: 0x040012DB RID: 4827
		internal const string MonikerMexBindingSectionNameNotSpecified = "MonikerMexBindingSectionNameNotSpecified";

		// Token: 0x040012DC RID: 4828
		internal const string MonikerMexAddressNotSpecified = "MonikerMexAddressNotSpecified";

		// Token: 0x040012DD RID: 4829
		internal const string MonikerContractNotSpecified = "MonikerContractNotSpecified";

		// Token: 0x040012DE RID: 4830
		internal const string MonikerBindingNotSpecified = "MonikerBindingNotSpecified";

		// Token: 0x040012DF RID: 4831
		internal const string MonikerBindingNamespacetNotSpecified = "MonikerBindingNamespacetNotSpecified";

		// Token: 0x040012E0 RID: 4832
		internal const string MonikerFailedToDoMexRetrieve = "MonikerFailedToDoMexRetrieve";

		// Token: 0x040012E1 RID: 4833
		internal const string MonikerContractNotFoundInRetreivedMex = "MonikerContractNotFoundInRetreivedMex";

		// Token: 0x040012E2 RID: 4834
		internal const string MonikerNoneOfTheBindingMatchedTheSpecifiedBinding = "MonikerNoneOfTheBindingMatchedTheSpecifiedBinding";

		// Token: 0x040012E3 RID: 4835
		internal const string MonikerMissingColon = "MonikerMissingColon";

		// Token: 0x040012E4 RID: 4836
		internal const string MonikerIncorrectServerIdentity = "MonikerIncorrectServerIdentity";

		// Token: 0x040012E5 RID: 4837
		internal const string NoInterface = "NoInterface";

		// Token: 0x040012E6 RID: 4838
		internal const string DuplicateTokenExFailed = "DuplicateTokenExFailed";

		// Token: 0x040012E7 RID: 4839
		internal const string AccessCheckFailed = "AccessCheckFailed";

		// Token: 0x040012E8 RID: 4840
		internal const string ImpersonateAnonymousTokenFailed = "ImpersonateAnonymousTokenFailed";

		// Token: 0x040012E9 RID: 4841
		internal const string OnlyByRefVariantSafeArraysAllowed = "OnlyByRefVariantSafeArraysAllowed";

		// Token: 0x040012EA RID: 4842
		internal const string OnlyOneDimensionalSafeArraysAllowed = "OnlyOneDimensionalSafeArraysAllowed";

		// Token: 0x040012EB RID: 4843
		internal const string OnlyVariantTypeElementsAllowed = "OnlyVariantTypeElementsAllowed";

		// Token: 0x040012EC RID: 4844
		internal const string OnlyZeroLBoundAllowed = "OnlyZeroLBoundAllowed";

		// Token: 0x040012ED RID: 4845
		internal const string OpenThreadTokenFailed = "OpenThreadTokenFailed";

		// Token: 0x040012EE RID: 4846
		internal const string OpenProcessTokenFailed = "OpenProcessTokenFailed";

		// Token: 0x040012EF RID: 4847
		internal const string InvalidIsolationLevelValue = "InvalidIsolationLevelValue";

		// Token: 0x040012F0 RID: 4848
		internal const string UnsupportedConversion = "UnsupportedConversion";

		// Token: 0x040012F1 RID: 4849
		internal const string FailedProxyProviderCreation = "FailedProxyProviderCreation";

		// Token: 0x040012F2 RID: 4850
		internal const string UnableToLoadDll = "UnableToLoadDll";

		// Token: 0x040012F3 RID: 4851
		internal const string InterfaceNotRegistered = "InterfaceNotRegistered";

		// Token: 0x040012F4 RID: 4852
		internal const string BadInterfaceRegistration = "BadInterfaceRegistration";

		// Token: 0x040012F5 RID: 4853
		internal const string NotAComObject = "NotAComObject";

		// Token: 0x040012F6 RID: 4854
		internal const string NoTypeLibraryFoundForInterface = "NoTypeLibraryFoundForInterface";

		// Token: 0x040012F7 RID: 4855
		internal const string CannotFindClsidInApplication = "CannotFindClsidInApplication";

		// Token: 0x040012F8 RID: 4856
		internal const string ComActivationAccessDenied = "ComActivationAccessDenied";

		// Token: 0x040012F9 RID: 4857
		internal const string ComActivationFailure = "ComActivationFailure";

		// Token: 0x040012FA RID: 4858
		internal const string ComDllHostInitializerFoundNoServices = "ComDllHostInitializerFoundNoServices";

		// Token: 0x040012FB RID: 4859
		internal const string ComRequiresWindowsSecurity = "ComRequiresWindowsSecurity";

		// Token: 0x040012FC RID: 4860
		internal const string ComInconsistentSessionRequirements = "ComInconsistentSessionRequirements";

		// Token: 0x040012FD RID: 4861
		internal const string ComMessageAccessDenied = "ComMessageAccessDenied";

		// Token: 0x040012FE RID: 4862
		internal const string VariantArrayNull = "VariantArrayNull";

		// Token: 0x040012FF RID: 4863
		internal const string UnableToRetrievepUnk = "UnableToRetrievepUnk";

		// Token: 0x04001300 RID: 4864
		internal const string PersistWrapperIsNull = "PersistWrapperIsNull";

		// Token: 0x04001301 RID: 4865
		internal const string UnexpectedThreadingModel = "UnexpectedThreadingModel";

		// Token: 0x04001302 RID: 4866
		internal const string NoneOfTheMethodsForInterfaceFoundInConfig = "NoneOfTheMethodsForInterfaceFoundInConfig";

		// Token: 0x04001303 RID: 4867
		internal const string ComOperationNotFound = "ComOperationNotFound";

		// Token: 0x04001304 RID: 4868
		internal const string InvalidWebServiceInterface = "InvalidWebServiceInterface";

		// Token: 0x04001305 RID: 4869
		internal const string InvalidWebServiceParameter = "InvalidWebServiceParameter";

		// Token: 0x04001306 RID: 4870
		internal const string InvalidWebServiceReturnValue = "InvalidWebServiceReturnValue";

		// Token: 0x04001307 RID: 4871
		internal const string OnlyClsidsAllowedForServiceType = "OnlyClsidsAllowedForServiceType";

		// Token: 0x04001308 RID: 4872
		internal const string OperationNotFound = "OperationNotFound";

		// Token: 0x04001309 RID: 4873
		internal const string BadDispID = "BadDispID";

		// Token: 0x0400130A RID: 4874
		internal const string ComNoAsyncOperationsAllowed = "ComNoAsyncOperationsAllowed";

		// Token: 0x0400130B RID: 4875
		internal const string ComDuplicateOperation = "ComDuplicateOperation";

		// Token: 0x0400130C RID: 4876
		internal const string BadParamCount = "BadParamCount";

		// Token: 0x0400130D RID: 4877
		internal const string BindingNotFoundInConfig = "BindingNotFoundInConfig";

		// Token: 0x0400130E RID: 4878
		internal const string AddressNotSpecified = "AddressNotSpecified";

		// Token: 0x0400130F RID: 4879
		internal const string BindingNotSpecified = "BindingNotSpecified";

		// Token: 0x04001310 RID: 4880
		internal const string OnlyVariantAllowedByRef = "OnlyVariantAllowedByRef";

		// Token: 0x04001311 RID: 4881
		internal const string CannotResolveTypeForParamInMessageDescription = "CannotResolveTypeForParamInMessageDescription";

		// Token: 0x04001312 RID: 4882
		internal const string TooLate = "TooLate";

		// Token: 0x04001313 RID: 4883
		internal const string RequireConfiguredMethods = "RequireConfiguredMethods";

		// Token: 0x04001314 RID: 4884
		internal const string RequireConfiguredInterfaces = "RequireConfiguredInterfaces";

		// Token: 0x04001315 RID: 4885
		internal const string CannotCreateChannelOption = "CannotCreateChannelOption";

		// Token: 0x04001316 RID: 4886
		internal const string NoTransactionInContext = "NoTransactionInContext";

		// Token: 0x04001317 RID: 4887
		internal const string IssuedTokenFlowNotAllowed = "IssuedTokenFlowNotAllowed";

		// Token: 0x04001318 RID: 4888
		internal const string GeneralSchemaValidationError = "GeneralSchemaValidationError";

		// Token: 0x04001319 RID: 4889
		internal const string SchemaValidationError = "SchemaValidationError";

		// Token: 0x0400131A RID: 4890
		internal const string ContractBindingAddressCannotBeNull = "ContractBindingAddressCannotBeNull";

		// Token: 0x0400131B RID: 4891
		internal const string TypeLoadForContractTypeIIDFailedWith = "TypeLoadForContractTypeIIDFailedWith";

		// Token: 0x0400131C RID: 4892
		internal const string BindingLoadFromConfigFailedWith = "BindingLoadFromConfigFailedWith";

		// Token: 0x0400131D RID: 4893
		internal const string PooledApplicationNotSupportedForComplusHostedScenarios = "PooledApplicationNotSupportedForComplusHostedScenarios";

		// Token: 0x0400131E RID: 4894
		internal const string RecycledApplicationNotSupportedForComplusHostedScenarios = "RecycledApplicationNotSupportedForComplusHostedScenarios";

		// Token: 0x0400131F RID: 4895
		internal const string BadImpersonationLevelForOutOfProcWas = "BadImpersonationLevelForOutOfProcWas";

		// Token: 0x04001320 RID: 4896
		internal const string ComPlusInstanceProviderRequiresMessage0 = "ComPlusInstanceProviderRequiresMessage0";

		// Token: 0x04001321 RID: 4897
		internal const string ComPlusInstanceCreationRequestSchema = "ComPlusInstanceCreationRequestSchema";

		// Token: 0x04001322 RID: 4898
		internal const string ComPlusMethodCallSchema = "ComPlusMethodCallSchema";

		// Token: 0x04001323 RID: 4899
		internal const string ComPlusServiceSchema = "ComPlusServiceSchema";

		// Token: 0x04001324 RID: 4900
		internal const string ComPlusServiceSchemaDllHost = "ComPlusServiceSchemaDllHost";

		// Token: 0x04001325 RID: 4901
		internal const string ComPlusTLBImportSchema = "ComPlusTLBImportSchema";

		// Token: 0x04001326 RID: 4902
		internal const string ComPlusServiceHostStartingServiceErrorNoQFE = "ComPlusServiceHostStartingServiceErrorNoQFE";

		// Token: 0x04001327 RID: 4903
		internal const string ComIntegrationManifestCreationFailed = "ComIntegrationManifestCreationFailed";

		// Token: 0x04001328 RID: 4904
		internal const string TempDirectoryNotFound = "TempDirectoryNotFound";

		// Token: 0x04001329 RID: 4905
		internal const string CannotAccessDirectory = "CannotAccessDirectory";

		// Token: 0x0400132A RID: 4906
		internal const string CLSIDDoesNotSupportIPersistStream = "CLSIDDoesNotSupportIPersistStream";

		// Token: 0x0400132B RID: 4907
		internal const string CLSIDOfTypeDoesNotMatch = "CLSIDOfTypeDoesNotMatch";

		// Token: 0x0400132C RID: 4908
		internal const string TargetObjectDoesNotSupportIPersistStream = "TargetObjectDoesNotSupportIPersistStream";

		// Token: 0x0400132D RID: 4909
		internal const string TargetTypeIsAnIntefaceButCorrespoindingTypeIsNotPersistStreamTypeWrapper = "TargetTypeIsAnIntefaceButCorrespoindingTypeIsNotPersistStreamTypeWrapper";

		// Token: 0x0400132E RID: 4910
		internal const string NotAllowedPersistableCLSID = "NotAllowedPersistableCLSID";

		// Token: 0x0400132F RID: 4911
		internal const string TransferringToComplus = "TransferringToComplus";

		// Token: 0x04001330 RID: 4912
		internal const string NamedArgsNotSupported = "NamedArgsNotSupported";

		// Token: 0x04001331 RID: 4913
		internal const string MexBindingNotFoundInConfig = "MexBindingNotFoundInConfig";

		// Token: 0x04001332 RID: 4914
		internal const string ClaimTypeCannotBeEmpty = "ClaimTypeCannotBeEmpty";

		// Token: 0x04001333 RID: 4915
		internal const string X509ChainIsEmpty = "X509ChainIsEmpty";

		// Token: 0x04001334 RID: 4916
		internal const string MissingCustomCertificateValidator = "MissingCustomCertificateValidator";

		// Token: 0x04001335 RID: 4917
		internal const string MissingMembershipProvider = "MissingMembershipProvider";

		// Token: 0x04001336 RID: 4918
		internal const string MissingCustomUserNamePasswordValidator = "MissingCustomUserNamePasswordValidator";

		// Token: 0x04001337 RID: 4919
		internal const string SpnegoImpersonationLevelCannotBeSetToNone = "SpnegoImpersonationLevelCannotBeSetToNone";

		// Token: 0x04001338 RID: 4920
		internal const string PublicKeyNotRSA = "PublicKeyNotRSA";

		// Token: 0x04001339 RID: 4921
		internal const string SecurityAuditFailToLoadDll = "SecurityAuditFailToLoadDll";

		// Token: 0x0400133A RID: 4922
		internal const string SecurityAuditPlatformNotSupported = "SecurityAuditPlatformNotSupported";

		// Token: 0x0400133B RID: 4923
		internal const string NoPrincipalSpecifiedInAuthorizationContext = "NoPrincipalSpecifiedInAuthorizationContext";

		// Token: 0x0400133C RID: 4924
		internal const string AccessDenied = "AccessDenied";

		// Token: 0x0400133D RID: 4925
		internal const string SecurityAuditNotSupportedOnChannelFactory = "SecurityAuditNotSupportedOnChannelFactory";

		// Token: 0x0400133E RID: 4926
		internal const string ExpiredTokenInChannelParameters = "ExpiredTokenInChannelParameters";

		// Token: 0x0400133F RID: 4927
		internal const string NoTokenInChannelParameters = "NoTokenInChannelParameters";

		// Token: 0x04001340 RID: 4928
		internal const string PeerMessageMustHaveVia = "PeerMessageMustHaveVia";

		// Token: 0x04001341 RID: 4929
		internal const string PeerLinkUtilityInvalidValues = "PeerLinkUtilityInvalidValues";

		// Token: 0x04001342 RID: 4930
		internal const string PeerNeighborInvalidState = "PeerNeighborInvalidState";

		// Token: 0x04001343 RID: 4931
		internal const string PeerMaxReceivedMessageSizeConflict = "PeerMaxReceivedMessageSizeConflict";

		// Token: 0x04001344 RID: 4932
		internal const string PeerConflictingPeerNodeSettings = "PeerConflictingPeerNodeSettings";

		// Token: 0x04001345 RID: 4933
		internal const string ArgumentOutOfRange = "ArgumentOutOfRange";

		// Token: 0x04001346 RID: 4934
		internal const string PeerChannelViaTooLong = "PeerChannelViaTooLong";

		// Token: 0x04001347 RID: 4935
		internal const string PeerNodeAborted = "PeerNodeAborted";

		// Token: 0x04001348 RID: 4936
		internal const string PeerPnrpNotAvailable = "PeerPnrpNotAvailable";

		// Token: 0x04001349 RID: 4937
		internal const string PeerPnrpNotInstalled = "PeerPnrpNotInstalled";

		// Token: 0x0400134A RID: 4938
		internal const string PeerResolverBindingElementRequired = "PeerResolverBindingElementRequired";

		// Token: 0x0400134B RID: 4939
		internal const string PeerResolverRequired = "PeerResolverRequired";

		// Token: 0x0400134C RID: 4940
		internal const string PeerResolverInvalid = "PeerResolverInvalid";

		// Token: 0x0400134D RID: 4941
		internal const string PeerResolverSettingsInvalid = "PeerResolverSettingsInvalid";

		// Token: 0x0400134E RID: 4942
		internal const string PeerListenIPAddressInvalid = "PeerListenIPAddressInvalid";

		// Token: 0x0400134F RID: 4943
		internal const string PeerFlooderDisposed = "PeerFlooderDisposed";

		// Token: 0x04001350 RID: 4944
		internal const string PeerPnrpIllegalUri = "PeerPnrpIllegalUri";

		// Token: 0x04001351 RID: 4945
		internal const string PeerInvalidRegistrationId = "PeerInvalidRegistrationId";

		// Token: 0x04001352 RID: 4946
		internal const string PeerConflictingHeader = "PeerConflictingHeader";

		// Token: 0x04001353 RID: 4947
		internal const string PnrpNoClouds = "PnrpNoClouds";

		// Token: 0x04001354 RID: 4948
		internal const string PnrpAddressesUnsupported = "PnrpAddressesUnsupported";

		// Token: 0x04001355 RID: 4949
		internal const string InsufficientCryptoSupport = "InsufficientCryptoSupport";

		// Token: 0x04001356 RID: 4950
		internal const string InsufficientCredentials = "InsufficientCredentials";

		// Token: 0x04001357 RID: 4951
		internal const string UnexpectedSecurityTokensDuringHandshake = "UnexpectedSecurityTokensDuringHandshake";

		// Token: 0x04001358 RID: 4952
		internal const string PnrpAddressesExceedLimit = "PnrpAddressesExceedLimit";

		// Token: 0x04001359 RID: 4953
		internal const string InsufficientResolverSettings = "InsufficientResolverSettings";

		// Token: 0x0400135A RID: 4954
		internal const string InvalidResolverMode = "InvalidResolverMode";

		// Token: 0x0400135B RID: 4955
		internal const string MustOverrideInitialize = "MustOverrideInitialize";

		// Token: 0x0400135C RID: 4956
		internal const string NotValidWhenOpen = "NotValidWhenOpen";

		// Token: 0x0400135D RID: 4957
		internal const string NotValidWhenClosed = "NotValidWhenClosed";

		// Token: 0x0400135E RID: 4958
		internal const string PeerNullRegistrationInfo = "PeerNullRegistrationInfo";

		// Token: 0x0400135F RID: 4959
		internal const string PeerNullResolveInfo = "PeerNullResolveInfo";

		// Token: 0x04001360 RID: 4960
		internal const string PeerNullRefreshInfo = "PeerNullRefreshInfo";

		// Token: 0x04001361 RID: 4961
		internal const string PeerInvalidMessageBody = "PeerInvalidMessageBody";

		// Token: 0x04001362 RID: 4962
		internal const string DuplicatePeerRegistration = "DuplicatePeerRegistration";

		// Token: 0x04001363 RID: 4963
		internal const string PeerNodeToStringFormat = "PeerNodeToStringFormat";

		// Token: 0x04001364 RID: 4964
		internal const string MessagePropagationException = "MessagePropagationException";

		// Token: 0x04001365 RID: 4965
		internal const string NotificationException = "NotificationException";

		// Token: 0x04001366 RID: 4966
		internal const string ResolverException = "ResolverException";

		// Token: 0x04001367 RID: 4967
		internal const string PnrpCloudNotFound = "PnrpCloudNotFound";

		// Token: 0x04001368 RID: 4968
		internal const string PnrpCloudDisabled = "PnrpCloudDisabled";

		// Token: 0x04001369 RID: 4969
		internal const string PnrpCloudResolveOnly = "PnrpCloudResolveOnly";

		// Token: 0x0400136A RID: 4970
		internal const string PnrpPortBlocked = "PnrpPortBlocked";

		// Token: 0x0400136B RID: 4971
		internal const string PnrpDuplicatePeerName = "PnrpDuplicatePeerName";

		// Token: 0x0400136C RID: 4972
		internal const string RefreshIntervalMustBeGreaterThanZero = "RefreshIntervalMustBeGreaterThanZero";

		// Token: 0x0400136D RID: 4973
		internal const string CleanupIntervalMustBeGreaterThanZero = "CleanupIntervalMustBeGreaterThanZero";

		// Token: 0x0400136E RID: 4974
		internal const string AmbiguousConnectivitySpec = "AmbiguousConnectivitySpec";

		// Token: 0x0400136F RID: 4975
		internal const string MustRegisterMoreThanZeroAddresses = "MustRegisterMoreThanZeroAddresses";

		// Token: 0x04001370 RID: 4976
		internal const string PeerCertGenFailure = "PeerCertGenFailure";

		// Token: 0x04001371 RID: 4977
		internal const string PeerThrottleWaiting = "PeerThrottleWaiting";

		// Token: 0x04001372 RID: 4978
		internal const string PeerThrottlePruning = "PeerThrottlePruning";

		// Token: 0x04001373 RID: 4979
		internal const string PeerMaintainerStarting = "PeerMaintainerStarting";

		// Token: 0x04001374 RID: 4980
		internal const string PeerMaintainerConnect = "PeerMaintainerConnect";

		// Token: 0x04001375 RID: 4981
		internal const string PeerMaintainerConnectFailure = "PeerMaintainerConnectFailure";

		// Token: 0x04001376 RID: 4982
		internal const string PeerMaintainerInitialConnect = "PeerMaintainerInitialConnect";

		// Token: 0x04001377 RID: 4983
		internal const string PeerMaintainerPruneMode = "PeerMaintainerPruneMode";

		// Token: 0x04001378 RID: 4984
		internal const string PeerMaintainerConnectMode = "PeerMaintainerConnectMode";

		// Token: 0x04001379 RID: 4985
		internal const string BasicHttpContextBindingRequiresAllowCookie = "BasicHttpContextBindingRequiresAllowCookie";

		// Token: 0x0400137A RID: 4986
		internal const string CallbackContextOnlySupportedInWSAddressing10 = "CallbackContextOnlySupportedInWSAddressing10";

		// Token: 0x0400137B RID: 4987
		internal const string ListenAddressAlreadyContainsContext = "ListenAddressAlreadyContainsContext";

		// Token: 0x0400137C RID: 4988
		internal const string MultipleContextHeadersFoundInCallbackAddress = "MultipleContextHeadersFoundInCallbackAddress";

		// Token: 0x0400137D RID: 4989
		internal const string CallbackContextNotExpectedOnIncomingMessageAtClient = "CallbackContextNotExpectedOnIncomingMessageAtClient";

		// Token: 0x0400137E RID: 4990
		internal const string CallbackContextOnlySupportedInSoap = "CallbackContextOnlySupportedInSoap";

		// Token: 0x0400137F RID: 4991
		internal const string ContextBindingElementCannotProvideChannelFactory = "ContextBindingElementCannotProvideChannelFactory";

		// Token: 0x04001380 RID: 4992
		internal const string ContextBindingElementCannotProvideChannelListener = "ContextBindingElementCannotProvideChannelListener";

		// Token: 0x04001381 RID: 4993
		internal const string InvalidCookieContent = "InvalidCookieContent";

		// Token: 0x04001382 RID: 4994
		internal const string SchemaViolationInsideContextHeader = "SchemaViolationInsideContextHeader";

		// Token: 0x04001383 RID: 4995
		internal const string CallbackContextNotExpectedOnOutgoingMessageAtServer = "CallbackContextNotExpectedOnOutgoingMessageAtServer";

		// Token: 0x04001384 RID: 4996
		internal const string ChannelIsOpen = "ChannelIsOpen";

		// Token: 0x04001385 RID: 4997
		internal const string ContextManagementNotEnabled = "ContextManagementNotEnabled";

		// Token: 0x04001386 RID: 4998
		internal const string CachedContextIsImmutable = "CachedContextIsImmutable";

		// Token: 0x04001387 RID: 4999
		internal const string InvalidMessageContext = "InvalidMessageContext";

		// Token: 0x04001388 RID: 5000
		internal const string InvalidContextReceived = "InvalidContextReceived";

		// Token: 0x04001389 RID: 5001
		internal const string BehaviorRequiresContextProtocolSupportInBinding = "BehaviorRequiresContextProtocolSupportInBinding";

		// Token: 0x0400138A RID: 5002
		internal const string HttpCookieContextExchangeMechanismNotCompatibleWithTransportType = "HttpCookieContextExchangeMechanismNotCompatibleWithTransportType";

		// Token: 0x0400138B RID: 5003
		internal const string HttpCookieContextExchangeMechanismNotCompatibleWithTransportCookieSetting = "HttpCookieContextExchangeMechanismNotCompatibleWithTransportCookieSetting";

		// Token: 0x0400138C RID: 5004
		internal const string PolicyImportContextBindingElementCollectionIsNull = "PolicyImportContextBindingElementCollectionIsNull";

		// Token: 0x0400138D RID: 5005
		internal const string ContextChannelFactoryChannelCreatedDetail = "ContextChannelFactoryChannelCreatedDetail";

		// Token: 0x0400138E RID: 5006
		internal const string XmlFormatViolationInContextHeader = "XmlFormatViolationInContextHeader";

		// Token: 0x0400138F RID: 5007
		internal const string XmlFormatViolationInCallbackContextHeader = "XmlFormatViolationInCallbackContextHeader";

		// Token: 0x04001390 RID: 5008
		internal const string OleTxHeaderCorrupt = "OleTxHeaderCorrupt";

		// Token: 0x04001391 RID: 5009
		internal const string WsatHeaderCorrupt = "WsatHeaderCorrupt";

		// Token: 0x04001392 RID: 5010
		internal const string FailedToDeserializeIssuedToken = "FailedToDeserializeIssuedToken";

		// Token: 0x04001393 RID: 5011
		internal const string InvalidPropagationToken = "InvalidPropagationToken";

		// Token: 0x04001394 RID: 5012
		internal const string InvalidWsatExtendedInfo = "InvalidWsatExtendedInfo";

		// Token: 0x04001395 RID: 5013
		internal const string TMCommunicationError = "TMCommunicationError";

		// Token: 0x04001396 RID: 5014
		internal const string UnmarshalTransactionFaulted = "UnmarshalTransactionFaulted";

		// Token: 0x04001397 RID: 5015
		internal const string InvalidRegistrationHeaderTransactionId = "InvalidRegistrationHeaderTransactionId";

		// Token: 0x04001398 RID: 5016
		internal const string InvalidRegistrationHeaderIdentifier = "InvalidRegistrationHeaderIdentifier";

		// Token: 0x04001399 RID: 5017
		internal const string InvalidRegistrationHeaderTokenId = "InvalidRegistrationHeaderTokenId";

		// Token: 0x0400139A RID: 5018
		internal const string InvalidCoordinationContextTransactionId = "InvalidCoordinationContextTransactionId";

		// Token: 0x0400139B RID: 5019
		internal const string WsatRegistryValueReadError = "WsatRegistryValueReadError";

		// Token: 0x0400139C RID: 5020
		internal const string WsatProtocolServiceDisabled = "WsatProtocolServiceDisabled";

		// Token: 0x0400139D RID: 5021
		internal const string InboundTransactionsDisabled = "InboundTransactionsDisabled";

		// Token: 0x0400139E RID: 5022
		internal const string SourceTransactionsDisabled = "SourceTransactionsDisabled";

		// Token: 0x0400139F RID: 5023
		internal const string WsatUriCreationFailed = "WsatUriCreationFailed";

		// Token: 0x040013A0 RID: 5024
		internal const string WhereaboutsReadFailed = "WhereaboutsReadFailed";

		// Token: 0x040013A1 RID: 5025
		internal const string WhereaboutsSignatureMissing = "WhereaboutsSignatureMissing";

		// Token: 0x040013A2 RID: 5026
		internal const string WhereaboutsImplausibleProtocolCount = "WhereaboutsImplausibleProtocolCount";

		// Token: 0x040013A3 RID: 5027
		internal const string WhereaboutsImplausibleHostNameByteCount = "WhereaboutsImplausibleHostNameByteCount";

		// Token: 0x040013A4 RID: 5028
		internal const string WhereaboutsInvalidHostName = "WhereaboutsInvalidHostName";

		// Token: 0x040013A5 RID: 5029
		internal const string WhereaboutsNoHostName = "WhereaboutsNoHostName";

		// Token: 0x040013A6 RID: 5030
		internal const string InvalidWsatProtocolVersion = "InvalidWsatProtocolVersion";

		// Token: 0x040013A7 RID: 5031
		internal const string ParameterCannotBeEmpty = "ParameterCannotBeEmpty";

		// Token: 0x040013A8 RID: 5032
		internal const string RedirectCache = "RedirectCache";

		// Token: 0x040013A9 RID: 5033
		internal const string RedirectResource = "RedirectResource";

		// Token: 0x040013AA RID: 5034
		internal const string RedirectUseIntermediary = "RedirectUseIntermediary";

		// Token: 0x040013AB RID: 5035
		internal const string RedirectGenericMessage = "RedirectGenericMessage";

		// Token: 0x040013AC RID: 5036
		internal const string RedirectMustProvideLocation = "RedirectMustProvideLocation";

		// Token: 0x040013AD RID: 5037
		internal const string RedirectCacheNoLocationAllowed = "RedirectCacheNoLocationAllowed";

		// Token: 0x040013AE RID: 5038
		internal const string RedirectionInfoStringFormatWithNamespace = "RedirectionInfoStringFormatWithNamespace";

		// Token: 0x040013AF RID: 5039
		internal const string RedirectionInfoStringFormatNoNamespace = "RedirectionInfoStringFormatNoNamespace";

		// Token: 0x040013B0 RID: 5040
		internal const string RetryGenericMessage = "RetryGenericMessage";

		// Token: 0x040013B1 RID: 5041
		internal const string ActivityCallback = "ActivityCallback";

		// Token: 0x040013B2 RID: 5042
		internal const string ActivityClose = "ActivityClose";

		// Token: 0x040013B3 RID: 5043
		internal const string ActivityConstructChannelFactory = "ActivityConstructChannelFactory";

		// Token: 0x040013B4 RID: 5044
		internal const string ActivityConstructServiceHost = "ActivityConstructServiceHost";

		// Token: 0x040013B5 RID: 5045
		internal const string ActivityExecuteMethod = "ActivityExecuteMethod";

		// Token: 0x040013B6 RID: 5046
		internal const string ActivityExecuteAsyncMethod = "ActivityExecuteAsyncMethod";

		// Token: 0x040013B7 RID: 5047
		internal const string ActivityCloseChannelFactory = "ActivityCloseChannelFactory";

		// Token: 0x040013B8 RID: 5048
		internal const string ActivityCloseClientBase = "ActivityCloseClientBase";

		// Token: 0x040013B9 RID: 5049
		internal const string ActivityCloseServiceHost = "ActivityCloseServiceHost";

		// Token: 0x040013BA RID: 5050
		internal const string ActivityListenAt = "ActivityListenAt";

		// Token: 0x040013BB RID: 5051
		internal const string ActivityOpen = "ActivityOpen";

		// Token: 0x040013BC RID: 5052
		internal const string ActivityOpenServiceHost = "ActivityOpenServiceHost";

		// Token: 0x040013BD RID: 5053
		internal const string ActivityOpenChannelFactory = "ActivityOpenChannelFactory";

		// Token: 0x040013BE RID: 5054
		internal const string ActivityOpenClientBase = "ActivityOpenClientBase";

		// Token: 0x040013BF RID: 5055
		internal const string ActivityProcessAction = "ActivityProcessAction";

		// Token: 0x040013C0 RID: 5056
		internal const string ActivityProcessingMessage = "ActivityProcessingMessage";

		// Token: 0x040013C1 RID: 5057
		internal const string ActivityReceiveBytes = "ActivityReceiveBytes";

		// Token: 0x040013C2 RID: 5058
		internal const string ActivitySecuritySetup = "ActivitySecuritySetup";

		// Token: 0x040013C3 RID: 5059
		internal const string ActivitySecurityRenew = "ActivitySecurityRenew";

		// Token: 0x040013C4 RID: 5060
		internal const string ActivitySecurityClose = "ActivitySecurityClose";

		// Token: 0x040013C5 RID: 5061
		internal const string ActivitySharedListenerConnection = "ActivitySharedListenerConnection";

		// Token: 0x040013C6 RID: 5062
		internal const string ActivitySocketConnection = "ActivitySocketConnection";

		// Token: 0x040013C7 RID: 5063
		internal const string ActivityReadOnConnection = "ActivityReadOnConnection";

		// Token: 0x040013C8 RID: 5064
		internal const string ActivityReceiveAtVia = "ActivityReceiveAtVia";

		// Token: 0x040013C9 RID: 5065
		internal const string TraceCodeBeginExecuteMethod = "TraceCodeBeginExecuteMethod";

		// Token: 0x040013CA RID: 5066
		internal const string TraceCodeChannelCreated = "TraceCodeChannelCreated";

		// Token: 0x040013CB RID: 5067
		internal const string TraceCodeChannelDisposed = "TraceCodeChannelDisposed";

		// Token: 0x040013CC RID: 5068
		internal const string TraceCodeChannelMessageSent = "TraceCodeChannelMessageSent";

		// Token: 0x040013CD RID: 5069
		internal const string TraceCodeChannelPreparedMessage = "TraceCodeChannelPreparedMessage";

		// Token: 0x040013CE RID: 5070
		internal const string TraceCodeComIntegrationChannelCreated = "TraceCodeComIntegrationChannelCreated";

		// Token: 0x040013CF RID: 5071
		internal const string TraceCodeComIntegrationDispatchMethod = "TraceCodeComIntegrationDispatchMethod";

		// Token: 0x040013D0 RID: 5072
		internal const string TraceCodeComIntegrationDllHostInitializerAddingHost = "TraceCodeComIntegrationDllHostInitializerAddingHost";

		// Token: 0x040013D1 RID: 5073
		internal const string TraceCodeComIntegrationDllHostInitializerStarted = "TraceCodeComIntegrationDllHostInitializerStarted";

		// Token: 0x040013D2 RID: 5074
		internal const string TraceCodeComIntegrationDllHostInitializerStarting = "TraceCodeComIntegrationDllHostInitializerStarting";

		// Token: 0x040013D3 RID: 5075
		internal const string TraceCodeComIntegrationDllHostInitializerStopped = "TraceCodeComIntegrationDllHostInitializerStopped";

		// Token: 0x040013D4 RID: 5076
		internal const string TraceCodeComIntegrationDllHostInitializerStopping = "TraceCodeComIntegrationDllHostInitializerStopping";

		// Token: 0x040013D5 RID: 5077
		internal const string TraceCodeComIntegrationEnteringActivity = "TraceCodeComIntegrationEnteringActivity";

		// Token: 0x040013D6 RID: 5078
		internal const string TraceCodeComIntegrationExecutingCall = "TraceCodeComIntegrationExecutingCall";

		// Token: 0x040013D7 RID: 5079
		internal const string TraceCodeComIntegrationInstanceCreationRequest = "TraceCodeComIntegrationInstanceCreationRequest";

		// Token: 0x040013D8 RID: 5080
		internal const string TraceCodeComIntegrationInstanceCreationSuccess = "TraceCodeComIntegrationInstanceCreationSuccess";

		// Token: 0x040013D9 RID: 5081
		internal const string TraceCodeComIntegrationInstanceReleased = "TraceCodeComIntegrationInstanceReleased";

		// Token: 0x040013DA RID: 5082
		internal const string TraceCodeComIntegrationInvokedMethod = "TraceCodeComIntegrationInvokedMethod";

		// Token: 0x040013DB RID: 5083
		internal const string TraceCodeComIntegrationInvokingMethod = "TraceCodeComIntegrationInvokingMethod";

		// Token: 0x040013DC RID: 5084
		internal const string TraceCodeComIntegrationInvokingMethodContextTransaction = "TraceCodeComIntegrationInvokingMethodContextTransaction";

		// Token: 0x040013DD RID: 5085
		internal const string TraceCodeComIntegrationInvokingMethodNewTransaction = "TraceCodeComIntegrationInvokingMethodNewTransaction";

		// Token: 0x040013DE RID: 5086
		internal const string TraceCodeComIntegrationLeftActivity = "TraceCodeComIntegrationLeftActivity";

		// Token: 0x040013DF RID: 5087
		internal const string TraceCodeComIntegrationMexChannelBuilderLoaded = "TraceCodeComIntegrationMexChannelBuilderLoaded";

		// Token: 0x040013E0 RID: 5088
		internal const string TraceCodeComIntegrationMexMonikerMetadataExchangeComplete = "TraceCodeComIntegrationMexMonikerMetadataExchangeComplete";

		// Token: 0x040013E1 RID: 5089
		internal const string TraceCodeComIntegrationServiceHostCreatedServiceContract = "TraceCodeComIntegrationServiceHostCreatedServiceContract";

		// Token: 0x040013E2 RID: 5090
		internal const string TraceCodeComIntegrationServiceHostCreatedServiceEndpoint = "TraceCodeComIntegrationServiceHostCreatedServiceEndpoint";

		// Token: 0x040013E3 RID: 5091
		internal const string TraceCodeComIntegrationServiceHostStartedService = "TraceCodeComIntegrationServiceHostStartedService";

		// Token: 0x040013E4 RID: 5092
		internal const string TraceCodeComIntegrationServiceHostStartedServiceDetails = "TraceCodeComIntegrationServiceHostStartedServiceDetails";

		// Token: 0x040013E5 RID: 5093
		internal const string TraceCodeComIntegrationServiceHostStartingService = "TraceCodeComIntegrationServiceHostStartingService";

		// Token: 0x040013E6 RID: 5094
		internal const string TraceCodeComIntegrationServiceHostStoppedService = "TraceCodeComIntegrationServiceHostStoppedService";

		// Token: 0x040013E7 RID: 5095
		internal const string TraceCodeComIntegrationServiceHostStoppingService = "TraceCodeComIntegrationServiceHostStoppingService";

		// Token: 0x040013E8 RID: 5096
		internal const string TraceCodeComIntegrationServiceMonikerParsed = "TraceCodeComIntegrationServiceMonikerParsed";

		// Token: 0x040013E9 RID: 5097
		internal const string TraceCodeComIntegrationTLBImportConverterEvent = "TraceCodeComIntegrationTLBImportConverterEvent";

		// Token: 0x040013EA RID: 5098
		internal const string TraceCodeComIntegrationTLBImportFinished = "TraceCodeComIntegrationTLBImportFinished";

		// Token: 0x040013EB RID: 5099
		internal const string TraceCodeComIntegrationTLBImportFromAssembly = "TraceCodeComIntegrationTLBImportFromAssembly";

		// Token: 0x040013EC RID: 5100
		internal const string TraceCodeComIntegrationTLBImportFromTypelib = "TraceCodeComIntegrationTLBImportFromTypelib";

		// Token: 0x040013ED RID: 5101
		internal const string TraceCodeComIntegrationTLBImportStarting = "TraceCodeComIntegrationTLBImportStarting";

		// Token: 0x040013EE RID: 5102
		internal const string TraceCodeComIntegrationTxProxyTxAbortedByContext = "TraceCodeComIntegrationTxProxyTxAbortedByContext";

		// Token: 0x040013EF RID: 5103
		internal const string TraceCodeComIntegrationTxProxyTxAbortedByTM = "TraceCodeComIntegrationTxProxyTxAbortedByTM";

		// Token: 0x040013F0 RID: 5104
		internal const string TraceCodeComIntegrationTxProxyTxCommitted = "TraceCodeComIntegrationTxProxyTxCommitted";

		// Token: 0x040013F1 RID: 5105
		internal const string TraceCodeComIntegrationTypedChannelBuilderLoaded = "TraceCodeComIntegrationTypedChannelBuilderLoaded";

		// Token: 0x040013F2 RID: 5106
		internal const string TraceCodeComIntegrationWsdlChannelBuilderLoaded = "TraceCodeComIntegrationWsdlChannelBuilderLoaded";

		// Token: 0x040013F3 RID: 5107
		internal const string TraceCodeCommunicationObjectAborted = "TraceCodeCommunicationObjectAborted";

		// Token: 0x040013F4 RID: 5108
		internal const string TraceCodeCommunicationObjectAbortFailed = "TraceCodeCommunicationObjectAbortFailed";

		// Token: 0x040013F5 RID: 5109
		internal const string TraceCodeCommunicationObjectCloseFailed = "TraceCodeCommunicationObjectCloseFailed";

		// Token: 0x040013F6 RID: 5110
		internal const string TraceCodeCommunicationObjectClosed = "TraceCodeCommunicationObjectClosed";

		// Token: 0x040013F7 RID: 5111
		internal const string TraceCodeCommunicationObjectCreated = "TraceCodeCommunicationObjectCreated";

		// Token: 0x040013F8 RID: 5112
		internal const string TraceCodeCommunicationObjectClosing = "TraceCodeCommunicationObjectClosing";

		// Token: 0x040013F9 RID: 5113
		internal const string TraceCodeCommunicationObjectDisposing = "TraceCodeCommunicationObjectDisposing";

		// Token: 0x040013FA RID: 5114
		internal const string TraceCodeCommunicationObjectFaultReason = "TraceCodeCommunicationObjectFaultReason";

		// Token: 0x040013FB RID: 5115
		internal const string TraceCodeCommunicationObjectFaulted = "TraceCodeCommunicationObjectFaulted";

		// Token: 0x040013FC RID: 5116
		internal const string TraceCodeCommunicationObjectOpenFailed = "TraceCodeCommunicationObjectOpenFailed";

		// Token: 0x040013FD RID: 5117
		internal const string TraceCodeCommunicationObjectOpened = "TraceCodeCommunicationObjectOpened";

		// Token: 0x040013FE RID: 5118
		internal const string TraceCodeCommunicationObjectOpening = "TraceCodeCommunicationObjectOpening";

		// Token: 0x040013FF RID: 5119
		internal const string TraceCodeConfigurationIsReadOnly = "TraceCodeConfigurationIsReadOnly";

		// Token: 0x04001400 RID: 5120
		internal const string TraceCodeConfiguredExtensionTypeNotFound = "TraceCodeConfiguredExtensionTypeNotFound";

		// Token: 0x04001401 RID: 5121
		internal const string TraceCodeConnectionAbandoned = "TraceCodeConnectionAbandoned";

		// Token: 0x04001402 RID: 5122
		internal const string TraceCodeConnectToIPEndpoint = "TraceCodeConnectToIPEndpoint";

		// Token: 0x04001403 RID: 5123
		internal const string TraceCodeConnectionPoolCloseException = "TraceCodeConnectionPoolCloseException";

		// Token: 0x04001404 RID: 5124
		internal const string TraceCodeConnectionPoolIdleTimeoutReached = "TraceCodeConnectionPoolIdleTimeoutReached";

		// Token: 0x04001405 RID: 5125
		internal const string TraceCodeConnectionPoolLeaseTimeoutReached = "TraceCodeConnectionPoolLeaseTimeoutReached";

		// Token: 0x04001406 RID: 5126
		internal const string TraceCodeConnectionPoolMaxOutboundConnectionsPerEndpointQuotaReached = "TraceCodeConnectionPoolMaxOutboundConnectionsPerEndpointQuotaReached";

		// Token: 0x04001407 RID: 5127
		internal const string TraceCodeServerMaxPooledConnectionsQuotaReached = "TraceCodeServerMaxPooledConnectionsQuotaReached";

		// Token: 0x04001408 RID: 5128
		internal const string TraceCodeDefaultEndpointsAdded = "TraceCodeDefaultEndpointsAdded";

		// Token: 0x04001409 RID: 5129
		internal const string TraceCodeDiagnosticsFailedMessageTrace = "TraceCodeDiagnosticsFailedMessageTrace";

		// Token: 0x0400140A RID: 5130
		internal const string TraceCodeDidNotUnderstandMessageHeader = "TraceCodeDidNotUnderstandMessageHeader";

		// Token: 0x0400140B RID: 5131
		internal const string TraceCodeDroppedAMessage = "TraceCodeDroppedAMessage";

		// Token: 0x0400140C RID: 5132
		internal const string TraceCodeCannotBeImportedInCurrentFormat = "TraceCodeCannotBeImportedInCurrentFormat";

		// Token: 0x0400140D RID: 5133
		internal const string TraceCodeElementTypeDoesntMatchConfiguredType = "TraceCodeElementTypeDoesntMatchConfiguredType";

		// Token: 0x0400140E RID: 5134
		internal const string TraceCodeEndExecuteMethod = "TraceCodeEndExecuteMethod";

		// Token: 0x0400140F RID: 5135
		internal const string TraceCodeEndpointListenerClose = "TraceCodeEndpointListenerClose";

		// Token: 0x04001410 RID: 5136
		internal const string TraceCodeEndpointListenerOpen = "TraceCodeEndpointListenerOpen";

		// Token: 0x04001411 RID: 5137
		internal const string TraceCodeErrorInvokingUserCode = "TraceCodeErrorInvokingUserCode";

		// Token: 0x04001412 RID: 5138
		internal const string TraceCodeEvaluationContextNotFound = "TraceCodeEvaluationContextNotFound";

		// Token: 0x04001413 RID: 5139
		internal const string TraceCodeExportSecurityChannelBindingEntry = "TraceCodeExportSecurityChannelBindingEntry";

		// Token: 0x04001414 RID: 5140
		internal const string TraceCodeExportSecurityChannelBindingExit = "TraceCodeExportSecurityChannelBindingExit";

		// Token: 0x04001415 RID: 5141
		internal const string TraceCodeExtensionCollectionDoesNotExist = "TraceCodeExtensionCollectionDoesNotExist";

		// Token: 0x04001416 RID: 5142
		internal const string TraceCodeExtensionCollectionIsEmpty = "TraceCodeExtensionCollectionIsEmpty";

		// Token: 0x04001417 RID: 5143
		internal const string TraceCodeExtensionCollectionNameNotFound = "TraceCodeExtensionCollectionNameNotFound";

		// Token: 0x04001418 RID: 5144
		internal const string TraceCodeExtensionElementAlreadyExistsInCollection = "TraceCodeExtensionElementAlreadyExistsInCollection";

		// Token: 0x04001419 RID: 5145
		internal const string TraceCodeExtensionTypeNotFound = "TraceCodeExtensionTypeNotFound";

		// Token: 0x0400141A RID: 5146
		internal const string TraceCodeFailedToAddAnActivityIdHeader = "TraceCodeFailedToAddAnActivityIdHeader";

		// Token: 0x0400141B RID: 5147
		internal const string TraceCodeFailedToReadAnActivityIdHeader = "TraceCodeFailedToReadAnActivityIdHeader";

		// Token: 0x0400141C RID: 5148
		internal const string TraceCodeFilterNotMatchedNodeQuotaExceeded = "TraceCodeFilterNotMatchedNodeQuotaExceeded";

		// Token: 0x0400141D RID: 5149
		internal const string TraceCodeGetBehaviorElement = "TraceCodeGetBehaviorElement";

		// Token: 0x0400141E RID: 5150
		internal const string TraceCodeGetChannelEndpointElement = "TraceCodeGetChannelEndpointElement";

		// Token: 0x0400141F RID: 5151
		internal const string TraceCodeGetCommonBehaviors = "TraceCodeGetCommonBehaviors";

		// Token: 0x04001420 RID: 5152
		internal const string TraceCodeGetConfigurationSection = "TraceCodeGetConfigurationSection";

		// Token: 0x04001421 RID: 5153
		internal const string TraceCodeGetConfiguredBinding = "TraceCodeGetConfiguredBinding";

		// Token: 0x04001422 RID: 5154
		internal const string TraceCodeGetDefaultConfiguredBinding = "TraceCodeGetDefaultConfiguredBinding";

		// Token: 0x04001423 RID: 5155
		internal const string TraceCodeGetConfiguredEndpoint = "TraceCodeGetConfiguredEndpoint";

		// Token: 0x04001424 RID: 5156
		internal const string TraceCodeGetDefaultConfiguredEndpoint = "TraceCodeGetDefaultConfiguredEndpoint";

		// Token: 0x04001425 RID: 5157
		internal const string TraceCodeGetServiceElement = "TraceCodeGetServiceElement";

		// Token: 0x04001426 RID: 5158
		internal const string TraceCodeHttpAuthFailed = "TraceCodeHttpAuthFailed";

		// Token: 0x04001427 RID: 5159
		internal const string TraceCodeHttpActionMismatch = "TraceCodeHttpActionMismatch";

		// Token: 0x04001428 RID: 5160
		internal const string TraceCodeHttpChannelMessageReceiveFailed = "TraceCodeHttpChannelMessageReceiveFailed";

		// Token: 0x04001429 RID: 5161
		internal const string TraceCodeHttpChannelRequestAborted = "TraceCodeHttpChannelRequestAborted";

		// Token: 0x0400142A RID: 5162
		internal const string TraceCodeHttpChannelResponseAborted = "TraceCodeHttpChannelResponseAborted";

		// Token: 0x0400142B RID: 5163
		internal const string TraceCodeHttpChannelUnexpectedResponse = "TraceCodeHttpChannelUnexpectedResponse";

		// Token: 0x0400142C RID: 5164
		internal const string TraceCodeHttpResponseReceived = "TraceCodeHttpResponseReceived";

		// Token: 0x0400142D RID: 5165
		internal const string TraceCodeHttpChannelConcurrentReceiveQuotaReached = "TraceCodeHttpChannelConcurrentReceiveQuotaReached";

		// Token: 0x0400142E RID: 5166
		internal const string TraceCodeHttpsClientCertificateInvalid = "TraceCodeHttpsClientCertificateInvalid";

		// Token: 0x0400142F RID: 5167
		internal const string TraceCodeHttpsClientCertificateInvalid1 = "TraceCodeHttpsClientCertificateInvalid1";

		// Token: 0x04001430 RID: 5168
		internal const string TraceCodeHttpsClientCertificateNotPresent = "TraceCodeHttpsClientCertificateNotPresent";

		// Token: 0x04001431 RID: 5169
		internal const string TraceCodeImportSecurityChannelBindingEntry = "TraceCodeImportSecurityChannelBindingEntry";

		// Token: 0x04001432 RID: 5170
		internal const string TraceCodeImportSecurityChannelBindingExit = "TraceCodeImportSecurityChannelBindingExit";

		// Token: 0x04001433 RID: 5171
		internal const string TraceCodeIncompatibleExistingTransportManager = "TraceCodeIncompatibleExistingTransportManager";

		// Token: 0x04001434 RID: 5172
		internal const string TraceCodeInitiatingNamedPipeConnection = "TraceCodeInitiatingNamedPipeConnection";

		// Token: 0x04001435 RID: 5173
		internal const string TraceCodeInitiatingTcpConnection = "TraceCodeInitiatingTcpConnection";

		// Token: 0x04001436 RID: 5174
		internal const string TraceCodeIssuanceTokenProviderBeginSecurityNegotiation = "TraceCodeIssuanceTokenProviderBeginSecurityNegotiation";

		// Token: 0x04001437 RID: 5175
		internal const string TraceCodeIssuanceTokenProviderEndSecurityNegotiation = "TraceCodeIssuanceTokenProviderEndSecurityNegotiation";

		// Token: 0x04001438 RID: 5176
		internal const string TraceCodeIssuanceTokenProviderRedirectApplied = "TraceCodeIssuanceTokenProviderRedirectApplied";

		// Token: 0x04001439 RID: 5177
		internal const string TraceCodeIssuanceTokenProviderRemovedCachedToken = "TraceCodeIssuanceTokenProviderRemovedCachedToken";

		// Token: 0x0400143A RID: 5178
		internal const string TraceCodeIssuanceTokenProviderServiceTokenCacheFull = "TraceCodeIssuanceTokenProviderServiceTokenCacheFull";

		// Token: 0x0400143B RID: 5179
		internal const string TraceCodeIssuanceTokenProviderUsingCachedToken = "TraceCodeIssuanceTokenProviderUsingCachedToken";

		// Token: 0x0400143C RID: 5180
		internal const string TraceCodeListenerCreated = "TraceCodeListenerCreated";

		// Token: 0x0400143D RID: 5181
		internal const string TraceCodeListenerDisposed = "TraceCodeListenerDisposed";

		// Token: 0x0400143E RID: 5182
		internal const string TraceCodeMaxPendingConnectionsReached = "TraceCodeMaxPendingConnectionsReached";

		// Token: 0x0400143F RID: 5183
		internal const string TraceCodeMaxAcceptedChannelsReached = "TraceCodeMaxAcceptedChannelsReached";

		// Token: 0x04001440 RID: 5184
		internal const string TraceCodeMessageClosed = "TraceCodeMessageClosed";

		// Token: 0x04001441 RID: 5185
		internal const string TraceCodeMessageClosedAgain = "TraceCodeMessageClosedAgain";

		// Token: 0x04001442 RID: 5186
		internal const string TraceCodeMessageCopied = "TraceCodeMessageCopied";

		// Token: 0x04001443 RID: 5187
		internal const string TraceCodeMessageCountLimitExceeded = "TraceCodeMessageCountLimitExceeded";

		// Token: 0x04001444 RID: 5188
		internal const string TraceCodeMessageNotLoggedQuotaExceeded = "TraceCodeMessageNotLoggedQuotaExceeded";

		// Token: 0x04001445 RID: 5189
		internal const string TraceCodeMessageRead = "TraceCodeMessageRead";

		// Token: 0x04001446 RID: 5190
		internal const string TraceCodeMessageSent = "TraceCodeMessageSent";

		// Token: 0x04001447 RID: 5191
		internal const string TraceCodeMessageReceived = "TraceCodeMessageReceived";

		// Token: 0x04001448 RID: 5192
		internal const string TraceCodeMessageWritten = "TraceCodeMessageWritten";

		// Token: 0x04001449 RID: 5193
		internal const string TraceCodeMessageProcessingPaused = "TraceCodeMessageProcessingPaused";

		// Token: 0x0400144A RID: 5194
		internal const string TraceCodeMsmqCannotPeekOnQueue = "TraceCodeMsmqCannotPeekOnQueue";

		// Token: 0x0400144B RID: 5195
		internal const string TraceCodeMsmqCannotReadQueues = "TraceCodeMsmqCannotReadQueues";

		// Token: 0x0400144C RID: 5196
		internal const string TraceCodeMsmqDatagramReceived = "TraceCodeMsmqDatagramReceived";

		// Token: 0x0400144D RID: 5197
		internal const string TraceCodeMsmqDatagramSent = "TraceCodeMsmqDatagramSent";

		// Token: 0x0400144E RID: 5198
		internal const string TraceCodeMsmqDetected = "TraceCodeMsmqDetected";

		// Token: 0x0400144F RID: 5199
		internal const string TraceCodeMsmqEnteredBatch = "TraceCodeMsmqEnteredBatch";

		// Token: 0x04001450 RID: 5200
		internal const string TraceCodeMsmqExpectedException = "TraceCodeMsmqExpectedException";

		// Token: 0x04001451 RID: 5201
		internal const string TraceCodeMsmqFoundBaseAddress = "TraceCodeMsmqFoundBaseAddress";

		// Token: 0x04001452 RID: 5202
		internal const string TraceCodeMsmqLeftBatch = "TraceCodeMsmqLeftBatch";

		// Token: 0x04001453 RID: 5203
		internal const string TraceCodeMsmqMatchedApplicationFound = "TraceCodeMsmqMatchedApplicationFound";

		// Token: 0x04001454 RID: 5204
		internal const string TraceCodeMsmqMessageLockedUnderTheTransaction = "TraceCodeMsmqMessageLockedUnderTheTransaction";

		// Token: 0x04001455 RID: 5205
		internal const string TraceCodeMsmqMessageDropped = "TraceCodeMsmqMessageDropped";

		// Token: 0x04001456 RID: 5206
		internal const string TraceCodeMsmqMessageRejected = "TraceCodeMsmqMessageRejected";

		// Token: 0x04001457 RID: 5207
		internal const string TraceCodeMsmqMoveOrDeleteAttemptFailed = "TraceCodeMsmqMoveOrDeleteAttemptFailed";

		// Token: 0x04001458 RID: 5208
		internal const string TraceCodeMsmqPoisonMessageMovedPoison = "TraceCodeMsmqPoisonMessageMovedPoison";

		// Token: 0x04001459 RID: 5209
		internal const string TraceCodeMsmqPoisonMessageMovedRetry = "TraceCodeMsmqPoisonMessageMovedRetry";

		// Token: 0x0400145A RID: 5210
		internal const string TraceCodeMsmqPoisonMessageRejected = "TraceCodeMsmqPoisonMessageRejected";

		// Token: 0x0400145B RID: 5211
		internal const string TraceCodeMsmqPoolFull = "TraceCodeMsmqPoolFull";

		// Token: 0x0400145C RID: 5212
		internal const string TraceCodeMsmqPotentiallyPoisonMessageDetected = "TraceCodeMsmqPotentiallyPoisonMessageDetected";

		// Token: 0x0400145D RID: 5213
		internal const string TraceCodeMsmqQueueClosed = "TraceCodeMsmqQueueClosed";

		// Token: 0x0400145E RID: 5214
		internal const string TraceCodeMsmqQueueOpened = "TraceCodeMsmqQueueOpened";

		// Token: 0x0400145F RID: 5215
		internal const string TraceCodeMsmqQueueTransactionalStatusUnknown = "TraceCodeMsmqQueueTransactionalStatusUnknown";

		// Token: 0x04001460 RID: 5216
		internal const string TraceCodeMsmqScanStarted = "TraceCodeMsmqScanStarted";

		// Token: 0x04001461 RID: 5217
		internal const string TraceCodeMsmqSessiongramReceived = "TraceCodeMsmqSessiongramReceived";

		// Token: 0x04001462 RID: 5218
		internal const string TraceCodeMsmqSessiongramSent = "TraceCodeMsmqSessiongramSent";

		// Token: 0x04001463 RID: 5219
		internal const string TraceCodeMsmqStartingApplication = "TraceCodeMsmqStartingApplication";

		// Token: 0x04001464 RID: 5220
		internal const string TraceCodeMsmqStartingService = "TraceCodeMsmqStartingService";

		// Token: 0x04001465 RID: 5221
		internal const string TraceCodeMsmqUnexpectedAcknowledgment = "TraceCodeMsmqUnexpectedAcknowledgment";

		// Token: 0x04001466 RID: 5222
		internal const string TraceCodeNamedPipeChannelMessageReceiveFailed = "TraceCodeNamedPipeChannelMessageReceiveFailed";

		// Token: 0x04001467 RID: 5223
		internal const string TraceCodeNamedPipeChannelMessageReceived = "TraceCodeNamedPipeChannelMessageReceived";

		// Token: 0x04001468 RID: 5224
		internal const string TraceCodeNegotiationAuthenticatorAttached = "TraceCodeNegotiationAuthenticatorAttached";

		// Token: 0x04001469 RID: 5225
		internal const string TraceCodeNegotiationTokenProviderAttached = "TraceCodeNegotiationTokenProviderAttached";

		// Token: 0x0400146A RID: 5226
		internal const string TraceCodeNoExistingTransportManager = "TraceCodeNoExistingTransportManager";

		// Token: 0x0400146B RID: 5227
		internal const string TraceCodeOpenedListener = "TraceCodeOpenedListener";

		// Token: 0x0400146C RID: 5228
		internal const string TraceCodeOverridingDuplicateConfigurationKey = "TraceCodeOverridingDuplicateConfigurationKey";

		// Token: 0x0400146D RID: 5229
		internal const string TraceCodePeerChannelMessageReceived = "TraceCodePeerChannelMessageReceived";

		// Token: 0x0400146E RID: 5230
		internal const string TraceCodePeerChannelMessageSent = "TraceCodePeerChannelMessageSent";

		// Token: 0x0400146F RID: 5231
		internal const string TraceCodePeerFloodedMessageNotMatched = "TraceCodePeerFloodedMessageNotMatched";

		// Token: 0x04001470 RID: 5232
		internal const string TraceCodePeerFloodedMessageNotPropagated = "TraceCodePeerFloodedMessageNotPropagated";

		// Token: 0x04001471 RID: 5233
		internal const string TraceCodePeerFloodedMessageReceived = "TraceCodePeerFloodedMessageReceived";

		// Token: 0x04001472 RID: 5234
		internal const string TraceCodePeerFlooderReceiveMessageQuotaExceeded = "TraceCodePeerFlooderReceiveMessageQuotaExceeded";

		// Token: 0x04001473 RID: 5235
		internal const string TraceCodePeerNeighborCloseFailed = "TraceCodePeerNeighborCloseFailed";

		// Token: 0x04001474 RID: 5236
		internal const string TraceCodePeerNeighborClosingFailed = "TraceCodePeerNeighborClosingFailed";

		// Token: 0x04001475 RID: 5237
		internal const string TraceCodePeerNeighborManagerOffline = "TraceCodePeerNeighborManagerOffline";

		// Token: 0x04001476 RID: 5238
		internal const string TraceCodePeerNeighborManagerOnline = "TraceCodePeerNeighborManagerOnline";

		// Token: 0x04001477 RID: 5239
		internal const string TraceCodePeerNeighborMessageReceived = "TraceCodePeerNeighborMessageReceived";

		// Token: 0x04001478 RID: 5240
		internal const string TraceCodePeerNeighborNotAccepted = "TraceCodePeerNeighborNotAccepted";

		// Token: 0x04001479 RID: 5241
		internal const string TraceCodePeerNeighborNotFound = "TraceCodePeerNeighborNotFound";

		// Token: 0x0400147A RID: 5242
		internal const string TraceCodePeerNeighborOpenFailed = "TraceCodePeerNeighborOpenFailed";

		// Token: 0x0400147B RID: 5243
		internal const string TraceCodePeerNeighborStateChangeFailed = "TraceCodePeerNeighborStateChangeFailed";

		// Token: 0x0400147C RID: 5244
		internal const string TraceCodePeerNeighborStateChanged = "TraceCodePeerNeighborStateChanged";

		// Token: 0x0400147D RID: 5245
		internal const string TraceCodePeerNodeAddressChanged = "TraceCodePeerNodeAddressChanged";

		// Token: 0x0400147E RID: 5246
		internal const string TraceCodePeerNodeAuthenticationFailure = "TraceCodePeerNodeAuthenticationFailure";

		// Token: 0x0400147F RID: 5247
		internal const string TraceCodePeerNodeAuthenticationTimeout = "TraceCodePeerNodeAuthenticationTimeout";

		// Token: 0x04001480 RID: 5248
		internal const string TraceCodePeerNodeClosed = "TraceCodePeerNodeClosed";

		// Token: 0x04001481 RID: 5249
		internal const string TraceCodePeerNodeClosing = "TraceCodePeerNodeClosing";

		// Token: 0x04001482 RID: 5250
		internal const string TraceCodePeerNodeOpenFailed = "TraceCodePeerNodeOpenFailed";

		// Token: 0x04001483 RID: 5251
		internal const string TraceCodePeerNodeOpened = "TraceCodePeerNodeOpened";

		// Token: 0x04001484 RID: 5252
		internal const string TraceCodePeerNodeOpening = "TraceCodePeerNodeOpening";

		// Token: 0x04001485 RID: 5253
		internal const string TraceCodePeerReceiveMessageAuthenticationFailure = "TraceCodePeerReceiveMessageAuthenticationFailure";

		// Token: 0x04001486 RID: 5254
		internal const string TraceCodePeerServiceOpened = "TraceCodePeerServiceOpened";

		// Token: 0x04001487 RID: 5255
		internal const string TraceCodePerformanceCounterFailedToLoad = "TraceCodePerformanceCounterFailedToLoad";

		// Token: 0x04001488 RID: 5256
		internal const string TraceCodePerformanceCountersFailed = "TraceCodePerformanceCountersFailed";

		// Token: 0x04001489 RID: 5257
		internal const string TraceCodePerformanceCountersFailedDuringUpdate = "TraceCodePerformanceCountersFailedDuringUpdate";

		// Token: 0x0400148A RID: 5258
		internal const string TraceCodePerformanceCountersFailedForService = "TraceCodePerformanceCountersFailedForService";

		// Token: 0x0400148B RID: 5259
		internal const string TraceCodePerformanceCountersFailedOnRelease = "TraceCodePerformanceCountersFailedOnRelease";

		// Token: 0x0400148C RID: 5260
		internal const string TraceCodePnrpRegisteredAddresses = "TraceCodePnrpRegisteredAddresses";

		// Token: 0x0400148D RID: 5261
		internal const string TraceCodePnrpResolvedAddresses = "TraceCodePnrpResolvedAddresses";

		// Token: 0x0400148E RID: 5262
		internal const string TraceCodePnrpResolveException = "TraceCodePnrpResolveException";

		// Token: 0x0400148F RID: 5263
		internal const string TraceCodePnrpUnregisteredAddresses = "TraceCodePnrpUnregisteredAddresses";

		// Token: 0x04001490 RID: 5264
		internal const string TraceCodePrematureDatagramEof = "TraceCodePrematureDatagramEof";

		// Token: 0x04001491 RID: 5265
		internal const string TraceCodePeerMaintainerActivity = "TraceCodePeerMaintainerActivity";

		// Token: 0x04001492 RID: 5266
		internal const string TraceCodeReliableChannelOpened = "TraceCodeReliableChannelOpened";

		// Token: 0x04001493 RID: 5267
		internal const string TraceCodeRemoveBehavior = "TraceCodeRemoveBehavior";

		// Token: 0x04001494 RID: 5268
		internal const string TraceCodeRequestChannelReplyReceived = "TraceCodeRequestChannelReplyReceived";

		// Token: 0x04001495 RID: 5269
		internal const string TraceCodeSecurity = "TraceCodeSecurity";

		// Token: 0x04001496 RID: 5270
		internal const string TraceCodeSecurityActiveServerSessionRemoved = "TraceCodeSecurityActiveServerSessionRemoved";

		// Token: 0x04001497 RID: 5271
		internal const string TraceCodeSecurityAuditWrittenFailure = "TraceCodeSecurityAuditWrittenFailure";

		// Token: 0x04001498 RID: 5272
		internal const string TraceCodeSecurityAuditWrittenSuccess = "TraceCodeSecurityAuditWrittenSuccess";

		// Token: 0x04001499 RID: 5273
		internal const string TraceCodeSecurityBindingIncomingMessageVerified = "TraceCodeSecurityBindingIncomingMessageVerified";

		// Token: 0x0400149A RID: 5274
		internal const string TraceCodeSecurityBindingOutgoingMessageSecured = "TraceCodeSecurityBindingOutgoingMessageSecured";

		// Token: 0x0400149B RID: 5275
		internal const string TraceCodeSecurityBindingSecureOutgoingMessageFailure = "TraceCodeSecurityBindingSecureOutgoingMessageFailure";

		// Token: 0x0400149C RID: 5276
		internal const string TraceCodeSecurityBindingVerifyIncomingMessageFailure = "TraceCodeSecurityBindingVerifyIncomingMessageFailure";

		// Token: 0x0400149D RID: 5277
		internal const string TraceCodeSecurityClientSessionKeyRenewed = "TraceCodeSecurityClientSessionKeyRenewed";

		// Token: 0x0400149E RID: 5278
		internal const string TraceCodeSecurityClientSessionCloseSent = "TraceCodeSecurityClientSessionCloseSent";

		// Token: 0x0400149F RID: 5279
		internal const string TraceCodeSecurityClientSessionCloseResponseSent = "TraceCodeSecurityClientSessionCloseResponseSent";

		// Token: 0x040014A0 RID: 5280
		internal const string TraceCodeSecurityClientSessionCloseMessageReceived = "TraceCodeSecurityClientSessionCloseMessageReceived";

		// Token: 0x040014A1 RID: 5281
		internal const string TraceCodeSecurityClientSessionPreviousKeyDiscarded = "TraceCodeSecurityClientSessionPreviousKeyDiscarded";

		// Token: 0x040014A2 RID: 5282
		internal const string TraceCodeSecurityContextTokenCacheFull = "TraceCodeSecurityContextTokenCacheFull";

		// Token: 0x040014A3 RID: 5283
		internal const string TraceCodeSecurityIdentityDeterminationFailure = "TraceCodeSecurityIdentityDeterminationFailure";

		// Token: 0x040014A4 RID: 5284
		internal const string TraceCodeSecurityIdentityDeterminationSuccess = "TraceCodeSecurityIdentityDeterminationSuccess";

		// Token: 0x040014A5 RID: 5285
		internal const string TraceCodeSecurityIdentityHostNameNormalizationFailure = "TraceCodeSecurityIdentityHostNameNormalizationFailure";

		// Token: 0x040014A6 RID: 5286
		internal const string TraceCodeSecurityIdentityVerificationFailure = "TraceCodeSecurityIdentityVerificationFailure";

		// Token: 0x040014A7 RID: 5287
		internal const string TraceCodeSecurityIdentityVerificationSuccess = "TraceCodeSecurityIdentityVerificationSuccess";

		// Token: 0x040014A8 RID: 5288
		internal const string TraceCodeSecurityImpersonationFailure = "TraceCodeSecurityImpersonationFailure";

		// Token: 0x040014A9 RID: 5289
		internal const string TraceCodeSecurityImpersonationSuccess = "TraceCodeSecurityImpersonationSuccess";

		// Token: 0x040014AA RID: 5290
		internal const string TraceCodeSecurityInactiveSessionFaulted = "TraceCodeSecurityInactiveSessionFaulted";

		// Token: 0x040014AB RID: 5291
		internal const string TraceCodeSecurityNegotiationProcessingFailure = "TraceCodeSecurityNegotiationProcessingFailure";

		// Token: 0x040014AC RID: 5292
		internal const string TraceCodeSecurityNewServerSessionKeyIssued = "TraceCodeSecurityNewServerSessionKeyIssued";

		// Token: 0x040014AD RID: 5293
		internal const string TraceCodeSecurityPendingServerSessionAdded = "TraceCodeSecurityPendingServerSessionAdded";

		// Token: 0x040014AE RID: 5294
		internal const string TraceCodeSecurityPendingServerSessionClosed = "TraceCodeSecurityPendingServerSessionClosed";

		// Token: 0x040014AF RID: 5295
		internal const string TraceCodeSecurityPendingServerSessionActivated = "TraceCodeSecurityPendingServerSessionActivated";

		// Token: 0x040014B0 RID: 5296
		internal const string TraceCodeSecurityServerSessionCloseReceived = "TraceCodeSecurityServerSessionCloseReceived";

		// Token: 0x040014B1 RID: 5297
		internal const string TraceCodeSecurityServerSessionCloseResponseReceived = "TraceCodeSecurityServerSessionCloseResponseReceived";

		// Token: 0x040014B2 RID: 5298
		internal const string TraceCodeSecurityServerSessionAbortedFaultSent = "TraceCodeSecurityServerSessionAbortedFaultSent";

		// Token: 0x040014B3 RID: 5299
		internal const string TraceCodeSecurityServerSessionKeyUpdated = "TraceCodeSecurityServerSessionKeyUpdated";

		// Token: 0x040014B4 RID: 5300
		internal const string TraceCodeSecurityServerSessionRenewalFaultSent = "TraceCodeSecurityServerSessionRenewalFaultSent";

		// Token: 0x040014B5 RID: 5301
		internal const string TraceCodeSecuritySessionCloseResponseSent = "TraceCodeSecuritySessionCloseResponseSent";

		// Token: 0x040014B6 RID: 5302
		internal const string TraceCodeSecuritySessionServerCloseSent = "TraceCodeSecuritySessionServerCloseSent";

		// Token: 0x040014B7 RID: 5303
		internal const string TraceCodeSecuritySessionAbortedFaultReceived = "TraceCodeSecuritySessionAbortedFaultReceived";

		// Token: 0x040014B8 RID: 5304
		internal const string TraceCodeSecuritySessionAbortedFaultSendFailure = "TraceCodeSecuritySessionAbortedFaultSendFailure";

		// Token: 0x040014B9 RID: 5305
		internal const string TraceCodeSecuritySessionClosedResponseReceived = "TraceCodeSecuritySessionClosedResponseReceived";

		// Token: 0x040014BA RID: 5306
		internal const string TraceCodeSecuritySessionClosedResponseSendFailure = "TraceCodeSecuritySessionClosedResponseSendFailure";

		// Token: 0x040014BB RID: 5307
		internal const string TraceCodeSecuritySessionServerCloseSendFailure = "TraceCodeSecuritySessionServerCloseSendFailure";

		// Token: 0x040014BC RID: 5308
		internal const string TraceCodeSecuritySessionKeyRenewalFaultReceived = "TraceCodeSecuritySessionKeyRenewalFaultReceived";

		// Token: 0x040014BD RID: 5309
		internal const string TraceCodeSecuritySessionRedirectApplied = "TraceCodeSecuritySessionRedirectApplied";

		// Token: 0x040014BE RID: 5310
		internal const string TraceCodeSecuritySessionRenewFaultSendFailure = "TraceCodeSecuritySessionRenewFaultSendFailure";

		// Token: 0x040014BF RID: 5311
		internal const string TraceCodeSecuritySessionRequestorOperationFailure = "TraceCodeSecuritySessionRequestorOperationFailure";

		// Token: 0x040014C0 RID: 5312
		internal const string TraceCodeSecuritySessionRequestorOperationSuccess = "TraceCodeSecuritySessionRequestorOperationSuccess";

		// Token: 0x040014C1 RID: 5313
		internal const string TraceCodeSecuritySessionRequestorStartOperation = "TraceCodeSecuritySessionRequestorStartOperation";

		// Token: 0x040014C2 RID: 5314
		internal const string TraceCodeSecuritySessionResponderOperationFailure = "TraceCodeSecuritySessionResponderOperationFailure";

		// Token: 0x040014C3 RID: 5315
		internal const string TraceCodeSecuritySpnToSidMappingFailure = "TraceCodeSecuritySpnToSidMappingFailure";

		// Token: 0x040014C4 RID: 5316
		internal const string TraceCodeSecurityTokenAuthenticatorClosed = "TraceCodeSecurityTokenAuthenticatorClosed";

		// Token: 0x040014C5 RID: 5317
		internal const string TraceCodeSecurityTokenAuthenticatorOpened = "TraceCodeSecurityTokenAuthenticatorOpened";

		// Token: 0x040014C6 RID: 5318
		internal const string TraceCodeSecurityTokenProviderClosed = "TraceCodeSecurityTokenProviderClosed";

		// Token: 0x040014C7 RID: 5319
		internal const string TraceCodeSecurityTokenProviderOpened = "TraceCodeSecurityTokenProviderOpened";

		// Token: 0x040014C8 RID: 5320
		internal const string TraceCodeServiceChannelLifetime = "TraceCodeServiceChannelLifetime";

		// Token: 0x040014C9 RID: 5321
		internal const string TraceCodeServiceHostBaseAddresses = "TraceCodeServiceHostBaseAddresses";

		// Token: 0x040014CA RID: 5322
		internal const string TraceCodeServiceHostTimeoutOnClose = "TraceCodeServiceHostTimeoutOnClose";

		// Token: 0x040014CB RID: 5323
		internal const string TraceCodeServiceHostFaulted = "TraceCodeServiceHostFaulted";

		// Token: 0x040014CC RID: 5324
		internal const string TraceCodeServiceHostErrorOnReleasePerformanceCounter = "TraceCodeServiceHostErrorOnReleasePerformanceCounter";

		// Token: 0x040014CD RID: 5325
		internal const string TraceCodeServiceThrottleLimitReached = "TraceCodeServiceThrottleLimitReached";

		// Token: 0x040014CE RID: 5326
		internal const string TraceCodeServiceThrottleLimitReachedInternal = "TraceCodeServiceThrottleLimitReachedInternal";

		// Token: 0x040014CF RID: 5327
		internal const string TraceCodeManualFlowThrottleLimitReached = "TraceCodeManualFlowThrottleLimitReached";

		// Token: 0x040014D0 RID: 5328
		internal const string TraceCodeProcessMessage2Paused = "TraceCodeProcessMessage2Paused";

		// Token: 0x040014D1 RID: 5329
		internal const string TraceCodeProcessMessage3Paused = "TraceCodeProcessMessage3Paused";

		// Token: 0x040014D2 RID: 5330
		internal const string TraceCodeProcessMessage31Paused = "TraceCodeProcessMessage31Paused";

		// Token: 0x040014D3 RID: 5331
		internal const string TraceCodeProcessMessage4Paused = "TraceCodeProcessMessage4Paused";

		// Token: 0x040014D4 RID: 5332
		internal const string TraceCodeServiceOperationExceptionOnReply = "TraceCodeServiceOperationExceptionOnReply";

		// Token: 0x040014D5 RID: 5333
		internal const string TraceCodeServiceOperationMissingReply = "TraceCodeServiceOperationMissingReply";

		// Token: 0x040014D6 RID: 5334
		internal const string TraceCodeServiceOperationMissingReplyContext = "TraceCodeServiceOperationMissingReplyContext";

		// Token: 0x040014D7 RID: 5335
		internal const string TraceCodeServiceSecurityNegotiationCompleted = "TraceCodeServiceSecurityNegotiationCompleted";

		// Token: 0x040014D8 RID: 5336
		internal const string TraceCodeSecuritySessionDemuxFailure = "TraceCodeSecuritySessionDemuxFailure";

		// Token: 0x040014D9 RID: 5337
		internal const string TraceCodeServiceHostCreation = "TraceCodeServiceHostCreation";

		// Token: 0x040014DA RID: 5338
		internal const string TraceCodePortSharingClosed = "TraceCodePortSharingClosed";

		// Token: 0x040014DB RID: 5339
		internal const string TraceCodePortSharingDuplicatedPipe = "TraceCodePortSharingDuplicatedPipe";

		// Token: 0x040014DC RID: 5340
		internal const string TraceCodePortSharingDuplicatedSocket = "TraceCodePortSharingDuplicatedSocket";

		// Token: 0x040014DD RID: 5341
		internal const string TraceCodePortSharingDupHandleGranted = "TraceCodePortSharingDupHandleGranted";

		// Token: 0x040014DE RID: 5342
		internal const string TraceCodePortSharingListening = "TraceCodePortSharingListening";

		// Token: 0x040014DF RID: 5343
		internal const string TraceCodeSkipBehavior = "TraceCodeSkipBehavior";

		// Token: 0x040014E0 RID: 5344
		internal const string TraceCodeFailedAcceptFromPool = "TraceCodeFailedAcceptFromPool";

		// Token: 0x040014E1 RID: 5345
		internal const string TraceCodeFailedPipeConnect = "TraceCodeFailedPipeConnect";

		// Token: 0x040014E2 RID: 5346
		internal const string TraceCodeSystemTimeResolution = "TraceCodeSystemTimeResolution";

		// Token: 0x040014E3 RID: 5347
		internal const string TraceCodeRequestContextAbort = "TraceCodeRequestContextAbort";

		// Token: 0x040014E4 RID: 5348
		internal const string TraceCodePipeConnectionAbort = "TraceCodePipeConnectionAbort";

		// Token: 0x040014E5 RID: 5349
		internal const string TraceCodeSharedManagerServiceEndpointNotExist = "TraceCodeSharedManagerServiceEndpointNotExist";

		// Token: 0x040014E6 RID: 5350
		internal const string TraceCodeSocketConnectionAbort = "TraceCodeSocketConnectionAbort";

		// Token: 0x040014E7 RID: 5351
		internal const string TraceCodeSocketConnectionAbortClose = "TraceCodeSocketConnectionAbortClose";

		// Token: 0x040014E8 RID: 5352
		internal const string TraceCodeSocketConnectionClose = "TraceCodeSocketConnectionClose";

		// Token: 0x040014E9 RID: 5353
		internal const string TraceCodeSocketConnectionCreate = "TraceCodeSocketConnectionCreate";

		// Token: 0x040014EA RID: 5354
		internal const string TraceCodeSpnegoClientNegotiationCompleted = "TraceCodeSpnegoClientNegotiationCompleted";

		// Token: 0x040014EB RID: 5355
		internal const string TraceCodeSpnegoServiceNegotiationCompleted = "TraceCodeSpnegoServiceNegotiationCompleted";

		// Token: 0x040014EC RID: 5356
		internal const string TraceCodeSpnegoClientNegotiation = "TraceCodeSpnegoClientNegotiation";

		// Token: 0x040014ED RID: 5357
		internal const string TraceCodeSpnegoServiceNegotiation = "TraceCodeSpnegoServiceNegotiation";

		// Token: 0x040014EE RID: 5358
		internal const string TraceCodeSslClientCertMissing = "TraceCodeSslClientCertMissing";

		// Token: 0x040014EF RID: 5359
		internal const string TraceCodeStreamSecurityUpgradeAccepted = "TraceCodeStreamSecurityUpgradeAccepted";

		// Token: 0x040014F0 RID: 5360
		internal const string TraceCodeTcpChannelMessageReceiveFailed = "TraceCodeTcpChannelMessageReceiveFailed";

		// Token: 0x040014F1 RID: 5361
		internal const string TraceCodeTcpChannelMessageReceived = "TraceCodeTcpChannelMessageReceived";

		// Token: 0x040014F2 RID: 5362
		internal const string TraceCodeUnderstoodMessageHeader = "TraceCodeUnderstoodMessageHeader";

		// Token: 0x040014F3 RID: 5363
		internal const string TraceCodeUnhandledAction = "TraceCodeUnhandledAction";

		// Token: 0x040014F4 RID: 5364
		internal const string TraceCodeUnhandledExceptionInUserOperation = "TraceCodeUnhandledExceptionInUserOperation";

		// Token: 0x040014F5 RID: 5365
		internal const string TraceCodeWebHostFailedToActivateService = "TraceCodeWebHostFailedToActivateService";

		// Token: 0x040014F6 RID: 5366
		internal const string TraceCodeWebHostFailedToCompile = "TraceCodeWebHostFailedToCompile";

		// Token: 0x040014F7 RID: 5367
		internal const string TraceCodeWmiPut = "TraceCodeWmiPut";

		// Token: 0x040014F8 RID: 5368
		internal const string TraceCodeWsmexNonCriticalWsdlExportError = "TraceCodeWsmexNonCriticalWsdlExportError";

		// Token: 0x040014F9 RID: 5369
		internal const string TraceCodeWsmexNonCriticalWsdlImportError = "TraceCodeWsmexNonCriticalWsdlImportError";

		// Token: 0x040014FA RID: 5370
		internal const string TraceCodeFailedToOpenIncomingChannel = "TraceCodeFailedToOpenIncomingChannel";

		// Token: 0x040014FB RID: 5371
		internal const string TraceCodeTransportListen = "TraceCodeTransportListen";

		// Token: 0x040014FC RID: 5372
		internal const string TraceCodeWsrmInvalidCreateSequence = "TraceCodeWsrmInvalidCreateSequence";

		// Token: 0x040014FD RID: 5373
		internal const string TraceCodeWsrmInvalidMessage = "TraceCodeWsrmInvalidMessage";

		// Token: 0x040014FE RID: 5374
		internal const string TraceCodeWsrmMaxPendingChannelsReached = "TraceCodeWsrmMaxPendingChannelsReached";

		// Token: 0x040014FF RID: 5375
		internal const string TraceCodeWsrmMessageDropped = "TraceCodeWsrmMessageDropped";

		// Token: 0x04001500 RID: 5376
		internal const string TraceCodeWsrmNegativeElapsedTimeDetected = "TraceCodeWsrmNegativeElapsedTimeDetected";

		// Token: 0x04001501 RID: 5377
		internal const string TraceCodeWsrmReceiveAcknowledgement = "TraceCodeWsrmReceiveAcknowledgement";

		// Token: 0x04001502 RID: 5378
		internal const string TraceCodeWsrmReceiveLastSequenceMessage = "TraceCodeWsrmReceiveLastSequenceMessage";

		// Token: 0x04001503 RID: 5379
		internal const string TraceCodeWsrmReceiveSequenceMessage = "TraceCodeWsrmReceiveSequenceMessage";

		// Token: 0x04001504 RID: 5380
		internal const string TraceCodeWsrmSendAcknowledgement = "TraceCodeWsrmSendAcknowledgement";

		// Token: 0x04001505 RID: 5381
		internal const string TraceCodeWsrmSendLastSequenceMessage = "TraceCodeWsrmSendLastSequenceMessage";

		// Token: 0x04001506 RID: 5382
		internal const string TraceCodeWsrmSendSequenceMessage = "TraceCodeWsrmSendSequenceMessage";

		// Token: 0x04001507 RID: 5383
		internal const string TraceCodeWsrmSequenceFaulted = "TraceCodeWsrmSequenceFaulted";

		// Token: 0x04001508 RID: 5384
		internal const string TraceCodeChannelConnectionDropped = "TraceCodeChannelConnectionDropped";

		// Token: 0x04001509 RID: 5385
		internal const string TraceCodeAsyncCallbackThrewException = "TraceCodeAsyncCallbackThrewException";

		// Token: 0x0400150A RID: 5386
		internal const string TraceCodeMetadataExchangeClientSendRequest = "TraceCodeMetadataExchangeClientSendRequest";

		// Token: 0x0400150B RID: 5387
		internal const string TraceCodeMetadataExchangeClientReceiveReply = "TraceCodeMetadataExchangeClientReceiveReply";

		// Token: 0x0400150C RID: 5388
		internal const string TraceCodeWarnHelpPageEnabledNoBaseAddress = "TraceCodeWarnHelpPageEnabledNoBaseAddress";

		// Token: 0x0400150D RID: 5389
		internal const string TraceCodeTcpConnectError = "TraceCodeTcpConnectError";

		// Token: 0x0400150E RID: 5390
		internal const string TraceCodeTxSourceTxScopeRequiredIsTransactedTransport = "TraceCodeTxSourceTxScopeRequiredIsTransactedTransport";

		// Token: 0x0400150F RID: 5391
		internal const string TraceCodeTxSourceTxScopeRequiredIsTransactionFlow = "TraceCodeTxSourceTxScopeRequiredIsTransactionFlow";

		// Token: 0x04001510 RID: 5392
		internal const string TraceCodeTxSourceTxScopeRequiredIsAttachedTransaction = "TraceCodeTxSourceTxScopeRequiredIsAttachedTransaction";

		// Token: 0x04001511 RID: 5393
		internal const string TraceCodeTxSourceTxScopeRequiredUsingExistingTransaction = "TraceCodeTxSourceTxScopeRequiredUsingExistingTransaction";

		// Token: 0x04001512 RID: 5394
		internal const string TraceCodeTxCompletionStatusCompletedForAutocomplete = "TraceCodeTxCompletionStatusCompletedForAutocomplete";

		// Token: 0x04001513 RID: 5395
		internal const string TraceCodeTxCompletionStatusCompletedForError = "TraceCodeTxCompletionStatusCompletedForError";

		// Token: 0x04001514 RID: 5396
		internal const string TraceCodeTxCompletionStatusCompletedForSetComplete = "TraceCodeTxCompletionStatusCompletedForSetComplete";

		// Token: 0x04001515 RID: 5397
		internal const string TraceCodeTxCompletionStatusCompletedForTACOSC = "TraceCodeTxCompletionStatusCompletedForTACOSC";

		// Token: 0x04001516 RID: 5398
		internal const string TraceCodeTxCompletionStatusCompletedForAsyncAbort = "TraceCodeTxCompletionStatusCompletedForAsyncAbort";

		// Token: 0x04001517 RID: 5399
		internal const string TraceCodeTxCompletionStatusRemainsAttached = "TraceCodeTxCompletionStatusRemainsAttached";

		// Token: 0x04001518 RID: 5400
		internal const string TraceCodeTxCompletionStatusAbortedOnSessionClose = "TraceCodeTxCompletionStatusAbortedOnSessionClose";

		// Token: 0x04001519 RID: 5401
		internal const string TraceCodeTxReleaseServiceInstanceOnCompletion = "TraceCodeTxReleaseServiceInstanceOnCompletion";

		// Token: 0x0400151A RID: 5402
		internal const string TraceCodeTxAsyncAbort = "TraceCodeTxAsyncAbort";

		// Token: 0x0400151B RID: 5403
		internal const string TraceCodeTxFailedToNegotiateOleTx = "TraceCodeTxFailedToNegotiateOleTx";

		// Token: 0x0400151C RID: 5404
		internal const string TraceCodeTxSourceTxScopeRequiredIsCreateNewTransaction = "TraceCodeTxSourceTxScopeRequiredIsCreateNewTransaction";

		// Token: 0x0400151D RID: 5405
		internal const string TraceCodeActivatingMessageReceived = "TraceCodeActivatingMessageReceived";

		// Token: 0x0400151E RID: 5406
		internal const string TraceCodeDICPInstanceContextCached = "TraceCodeDICPInstanceContextCached";

		// Token: 0x0400151F RID: 5407
		internal const string TraceCodeDICPInstanceContextRemovedFromCache = "TraceCodeDICPInstanceContextRemovedFromCache";

		// Token: 0x04001520 RID: 5408
		internal const string TraceCodeInstanceContextBoundToDurableInstance = "TraceCodeInstanceContextBoundToDurableInstance";

		// Token: 0x04001521 RID: 5409
		internal const string TraceCodeInstanceContextDetachedFromDurableInstance = "TraceCodeInstanceContextDetachedFromDurableInstance";

		// Token: 0x04001522 RID: 5410
		internal const string TraceCodeContextChannelFactoryChannelCreated = "TraceCodeContextChannelFactoryChannelCreated";

		// Token: 0x04001523 RID: 5411
		internal const string TraceCodeContextChannelListenerChannelAccepted = "TraceCodeContextChannelListenerChannelAccepted";

		// Token: 0x04001524 RID: 5412
		internal const string TraceCodeContextProtocolContextAddedToMessage = "TraceCodeContextProtocolContextAddedToMessage";

		// Token: 0x04001525 RID: 5413
		internal const string TraceCodeContextProtocolContextRetrievedFromMessage = "TraceCodeContextProtocolContextRetrievedFromMessage";

		// Token: 0x04001526 RID: 5414
		internal const string TraceCodeWorkflowServiceHostCreated = "TraceCodeWorkflowServiceHostCreated";

		// Token: 0x04001527 RID: 5415
		internal const string TraceCodeServiceDurableInstanceDeleted = "TraceCodeServiceDurableInstanceDeleted";

		// Token: 0x04001528 RID: 5416
		internal const string TraceCodeServiceDurableInstanceDisposed = "TraceCodeServiceDurableInstanceDisposed";

		// Token: 0x04001529 RID: 5417
		internal const string TraceCodeServiceDurableInstanceLoaded = "TraceCodeServiceDurableInstanceLoaded";

		// Token: 0x0400152A RID: 5418
		internal const string TraceCodeServiceDurableInstanceSaved = "TraceCodeServiceDurableInstanceSaved";

		// Token: 0x0400152B RID: 5419
		internal const string TraceCodeWorkflowDurableInstanceLoaded = "TraceCodeWorkflowDurableInstanceLoaded";

		// Token: 0x0400152C RID: 5420
		internal const string TraceCodeWorkflowDurableInstanceActivated = "TraceCodeWorkflowDurableInstanceActivated";

		// Token: 0x0400152D RID: 5421
		internal const string TraceCodeWorkflowDurableInstanceAborted = "TraceCodeWorkflowDurableInstanceAborted";

		// Token: 0x0400152E RID: 5422
		internal const string TraceCodeWorkflowOperationInvokerItemQueued = "TraceCodeWorkflowOperationInvokerItemQueued";

		// Token: 0x0400152F RID: 5423
		internal const string TraceCodeWorkflowRequestContextReplySent = "TraceCodeWorkflowRequestContextReplySent";

		// Token: 0x04001530 RID: 5424
		internal const string TraceCodeWorkflowRequestContextFaultSent = "TraceCodeWorkflowRequestContextFaultSent";

		// Token: 0x04001531 RID: 5425
		internal const string TraceCodeSqlPersistenceProviderSQLCallStart = "TraceCodeSqlPersistenceProviderSQLCallStart";

		// Token: 0x04001532 RID: 5426
		internal const string TraceCodeSqlPersistenceProviderSQLCallEnd = "TraceCodeSqlPersistenceProviderSQLCallEnd";

		// Token: 0x04001533 RID: 5427
		internal const string TraceCodeSqlPersistenceProviderOpenParameters = "TraceCodeSqlPersistenceProviderOpenParameters";

		// Token: 0x04001534 RID: 5428
		internal const string TraceCodeSyncContextSchedulerServiceTimerCancelled = "TraceCodeSyncContextSchedulerServiceTimerCancelled";

		// Token: 0x04001535 RID: 5429
		internal const string TraceCodeSyncContextSchedulerServiceTimerCreated = "TraceCodeSyncContextSchedulerServiceTimerCreated";

		// Token: 0x04001536 RID: 5430
		internal const string TraceCodeSyndicationReadFeedBegin = "TraceCodeSyndicationReadFeedBegin";

		// Token: 0x04001537 RID: 5431
		internal const string TraceCodeSyndicationReadFeedEnd = "TraceCodeSyndicationReadFeedEnd";

		// Token: 0x04001538 RID: 5432
		internal const string TraceCodeSyndicationReadItemBegin = "TraceCodeSyndicationReadItemBegin";

		// Token: 0x04001539 RID: 5433
		internal const string TraceCodeSyndicationReadItemEnd = "TraceCodeSyndicationReadItemEnd";

		// Token: 0x0400153A RID: 5434
		internal const string TraceCodeSyndicationWriteFeedBegin = "TraceCodeSyndicationWriteFeedBegin";

		// Token: 0x0400153B RID: 5435
		internal const string TraceCodeSyndicationWriteFeedEnd = "TraceCodeSyndicationWriteFeedEnd";

		// Token: 0x0400153C RID: 5436
		internal const string TraceCodeSyndicationWriteItemBegin = "TraceCodeSyndicationWriteItemBegin";

		// Token: 0x0400153D RID: 5437
		internal const string TraceCodeSyndicationWriteItemEnd = "TraceCodeSyndicationWriteItemEnd";

		// Token: 0x0400153E RID: 5438
		internal const string TraceCodeSyndicationProtocolElementIgnoredOnWrite = "TraceCodeSyndicationProtocolElementIgnoredOnWrite";

		// Token: 0x0400153F RID: 5439
		internal const string TraceCodeSyndicationProtocolElementInvalid = "TraceCodeSyndicationProtocolElementInvalid";

		// Token: 0x04001540 RID: 5440
		internal const string TraceCodeWebUnknownQueryParameterIgnored = "TraceCodeWebUnknownQueryParameterIgnored";

		// Token: 0x04001541 RID: 5441
		internal const string TraceCodeWebRequestMatchesOperation = "TraceCodeWebRequestMatchesOperation";

		// Token: 0x04001542 RID: 5442
		internal const string TraceCodeWebRequestDoesNotMatchOperations = "TraceCodeWebRequestDoesNotMatchOperations";

		// Token: 0x04001543 RID: 5443
		internal const string UTTMustBeAbsolute = "UTTMustBeAbsolute";

		// Token: 0x04001544 RID: 5444
		internal const string UTTBaseAddressMustBeAbsolute = "UTTBaseAddressMustBeAbsolute";

		// Token: 0x04001545 RID: 5445
		internal const string UTTCannotChangeBaseAddress = "UTTCannotChangeBaseAddress";

		// Token: 0x04001546 RID: 5446
		internal const string UTTMultipleMatches = "UTTMultipleMatches";

		// Token: 0x04001547 RID: 5447
		internal const string UTTBaseAddressNotSet = "UTTBaseAddressNotSet";

		// Token: 0x04001548 RID: 5448
		internal const string UTTEmptyKeyValuePairs = "UTTEmptyKeyValuePairs";

		// Token: 0x04001549 RID: 5449
		internal const string UTBindByPositionWrongCount = "UTBindByPositionWrongCount";

		// Token: 0x0400154A RID: 5450
		internal const string UTBadBaseAddress = "UTBadBaseAddress";

		// Token: 0x0400154B RID: 5451
		internal const string UTQueryNamesMustBeUnique = "UTQueryNamesMustBeUnique";

		// Token: 0x0400154C RID: 5452
		internal const string UTQueryCannotEndInAmpersand = "UTQueryCannotEndInAmpersand";

		// Token: 0x0400154D RID: 5453
		internal const string UTQueryCannotHaveEmptyName = "UTQueryCannotHaveEmptyName";

		// Token: 0x0400154E RID: 5454
		internal const string UTVarNamesMustBeUnique = "UTVarNamesMustBeUnique";

		// Token: 0x0400154F RID: 5455
		internal const string UTTAmbiguousQueries = "UTTAmbiguousQueries";

		// Token: 0x04001550 RID: 5456
		internal const string UTTOtherAmbiguousQueries = "UTTOtherAmbiguousQueries";

		// Token: 0x04001551 RID: 5457
		internal const string UTTDuplicate = "UTTDuplicate";

		// Token: 0x04001552 RID: 5458
		internal const string UTInvalidFormatSegmentOrQueryPart = "UTInvalidFormatSegmentOrQueryPart";

		// Token: 0x04001553 RID: 5459
		internal const string BindUriTemplateToNullOrEmptyPathParam = "BindUriTemplateToNullOrEmptyPathParam";

		// Token: 0x04001554 RID: 5460
		internal const string UTBindByPositionNoVariables = "UTBindByPositionNoVariables";

		// Token: 0x04001555 RID: 5461
		internal const string UTCSRLookupBeforeMatch = "UTCSRLookupBeforeMatch";

		// Token: 0x04001556 RID: 5462
		internal const string UTDoesNotSupportAdjacentVarsInCompoundSegment = "UTDoesNotSupportAdjacentVarsInCompoundSegment";

		// Token: 0x04001557 RID: 5463
		internal const string UTQueryCannotHaveCompoundValue = "UTQueryCannotHaveCompoundValue";

		// Token: 0x04001558 RID: 5464
		internal const string UTQueryMustHaveLiteralNames = "UTQueryMustHaveLiteralNames";

		// Token: 0x04001559 RID: 5465
		internal const string UTAdditionalDefaultIsInvalid = "UTAdditionalDefaultIsInvalid";

		// Token: 0x0400155A RID: 5466
		internal const string UTDefaultValuesAreImmutable = "UTDefaultValuesAreImmutable";

		// Token: 0x0400155B RID: 5467
		internal const string UTDefaultValueToCompoundSegmentVar = "UTDefaultValueToCompoundSegmentVar";

		// Token: 0x0400155C RID: 5468
		internal const string UTDefaultValueToQueryVar = "UTDefaultValueToQueryVar";

		// Token: 0x0400155D RID: 5469
		internal const string UTInvalidDefaultPathValue = "UTInvalidDefaultPathValue";

		// Token: 0x0400155E RID: 5470
		internal const string UTInvalidVarDeclaration = "UTInvalidVarDeclaration";

		// Token: 0x0400155F RID: 5471
		internal const string UTInvalidWildcardInVariableOrLiteral = "UTInvalidWildcardInVariableOrLiteral";

		// Token: 0x04001560 RID: 5472
		internal const string UTStarVariableWithDefaults = "UTStarVariableWithDefaults";

		// Token: 0x04001561 RID: 5473
		internal const string UTDefaultValueToCompoundSegmentVarFromAdditionalDefaults = "UTDefaultValueToCompoundSegmentVarFromAdditionalDefaults";

		// Token: 0x04001562 RID: 5474
		internal const string UTDefaultValueToQueryVarFromAdditionalDefaults = "UTDefaultValueToQueryVarFromAdditionalDefaults";

		// Token: 0x04001563 RID: 5475
		internal const string UTNullableDefaultAtAdditionalDefaults = "UTNullableDefaultAtAdditionalDefaults";

		// Token: 0x04001564 RID: 5476
		internal const string UTNullableDefaultMustBeFollowedWithNullables = "UTNullableDefaultMustBeFollowedWithNullables";

		// Token: 0x04001565 RID: 5477
		internal const string UTNullableDefaultMustNotBeFollowedWithLiteral = "UTNullableDefaultMustNotBeFollowedWithLiteral";

		// Token: 0x04001566 RID: 5478
		internal const string UTNullableDefaultMustNotBeFollowedWithWildcard = "UTNullableDefaultMustNotBeFollowedWithWildcard";

		// Token: 0x04001567 RID: 5479
		internal const string UTStarVariableWithDefaultsFromAdditionalDefaults = "UTStarVariableWithDefaultsFromAdditionalDefaults";

		// Token: 0x04001568 RID: 5480
		internal const string UTTInvalidTemplateKey = "UTTInvalidTemplateKey";

		// Token: 0x04001569 RID: 5481
		internal const string UTTNullTemplateKey = "UTTNullTemplateKey";

		// Token: 0x0400156A RID: 5482
		internal const string UTBindByNameCalledWithEmptyKey = "UTBindByNameCalledWithEmptyKey";

		// Token: 0x0400156B RID: 5483
		internal const string UTBothLiteralAndNameValueCollectionKey = "UTBothLiteralAndNameValueCollectionKey";

		// Token: 0x0400156C RID: 5484
		internal const string ExtensionNameNotSpecified = "ExtensionNameNotSpecified";

		// Token: 0x0400156D RID: 5485
		internal const string UnsupportedRssVersion = "UnsupportedRssVersion";

		// Token: 0x0400156E RID: 5486
		internal const string Atom10SpecRequiresTextConstruct = "Atom10SpecRequiresTextConstruct";

		// Token: 0x0400156F RID: 5487
		internal const string ErrorInLine = "ErrorInLine";

		// Token: 0x04001570 RID: 5488
		internal const string ErrorParsingFeed = "ErrorParsingFeed";

		// Token: 0x04001571 RID: 5489
		internal const string ErrorParsingDocument = "ErrorParsingDocument";

		// Token: 0x04001572 RID: 5490
		internal const string ErrorParsingItem = "ErrorParsingItem";

		// Token: 0x04001573 RID: 5491
		internal const string ErrorParsingDateTime = "ErrorParsingDateTime";

		// Token: 0x04001574 RID: 5492
		internal const string OuterElementNameNotSpecified = "OuterElementNameNotSpecified";

		// Token: 0x04001575 RID: 5493
		internal const string UnknownFeedXml = "UnknownFeedXml";

		// Token: 0x04001576 RID: 5494
		internal const string UnknownDocumentXml = "UnknownDocumentXml";

		// Token: 0x04001577 RID: 5495
		internal const string UnknownItemXml = "UnknownItemXml";

		// Token: 0x04001578 RID: 5496
		internal const string FeedFormatterDoesNotHaveFeed = "FeedFormatterDoesNotHaveFeed";

		// Token: 0x04001579 RID: 5497
		internal const string DocumentFormatterDoesNotHaveDocument = "DocumentFormatterDoesNotHaveDocument";

		// Token: 0x0400157A RID: 5498
		internal const string ItemFormatterDoesNotHaveItem = "ItemFormatterDoesNotHaveItem";

		// Token: 0x0400157B RID: 5499
		internal const string UnbufferedItemsCannotBeCloned = "UnbufferedItemsCannotBeCloned";

		// Token: 0x0400157C RID: 5500
		internal const string FeedHasNonContiguousItems = "FeedHasNonContiguousItems";

		// Token: 0x0400157D RID: 5501
		internal const string FeedCreatedNullCategory = "FeedCreatedNullCategory";

		// Token: 0x0400157E RID: 5502
		internal const string ItemCreatedNullCategory = "ItemCreatedNullCategory";

		// Token: 0x0400157F RID: 5503
		internal const string FeedCreatedNullPerson = "FeedCreatedNullPerson";

		// Token: 0x04001580 RID: 5504
		internal const string ItemCreatedNullPerson = "ItemCreatedNullPerson";

		// Token: 0x04001581 RID: 5505
		internal const string FeedCreatedNullItem = "FeedCreatedNullItem";

		// Token: 0x04001582 RID: 5506
		internal const string TraceCodeSyndicationFeedReadBegin = "TraceCodeSyndicationFeedReadBegin";

		// Token: 0x04001583 RID: 5507
		internal const string TraceCodeSyndicationFeedReadEnd = "TraceCodeSyndicationFeedReadEnd";

		// Token: 0x04001584 RID: 5508
		internal const string TraceCodeSyndicationItemReadBegin = "TraceCodeSyndicationItemReadBegin";

		// Token: 0x04001585 RID: 5509
		internal const string TraceCodeSyndicationItemReadEnd = "TraceCodeSyndicationItemReadEnd";

		// Token: 0x04001586 RID: 5510
		internal const string TraceCodeSyndicationFeedWriteBegin = "TraceCodeSyndicationFeedWriteBegin";

		// Token: 0x04001587 RID: 5511
		internal const string TraceCodeSyndicationFeedWriteEnd = "TraceCodeSyndicationFeedWriteEnd";

		// Token: 0x04001588 RID: 5512
		internal const string TraceCodeSyndicationItemWriteBegin = "TraceCodeSyndicationItemWriteBegin";

		// Token: 0x04001589 RID: 5513
		internal const string TraceCodeSyndicationItemWriteEnd = "TraceCodeSyndicationItemWriteEnd";

		// Token: 0x0400158A RID: 5514
		internal const string TraceCodeSyndicationProtocolElementIgnoredOnRead = "TraceCodeSyndicationProtocolElementIgnoredOnRead";

		// Token: 0x0400158B RID: 5515
		internal const string TraceCodeSyndicationReadServiceDocumentBegin = "TraceCodeSyndicationReadServiceDocumentBegin";

		// Token: 0x0400158C RID: 5516
		internal const string TraceCodeSyndicationReadServiceDocumentEnd = "TraceCodeSyndicationReadServiceDocumentEnd";

		// Token: 0x0400158D RID: 5517
		internal const string TraceCodeSyndicationWriteServiceDocumentBegin = "TraceCodeSyndicationWriteServiceDocumentBegin";

		// Token: 0x0400158E RID: 5518
		internal const string TraceCodeSyndicationWriteServiceDocumentEnd = "TraceCodeSyndicationWriteServiceDocumentEnd";

		// Token: 0x0400158F RID: 5519
		internal const string TraceCodeSyndicationReadCategoriesDocumentBegin = "TraceCodeSyndicationReadCategoriesDocumentBegin";

		// Token: 0x04001590 RID: 5520
		internal const string TraceCodeSyndicationReadCategoriesDocumentEnd = "TraceCodeSyndicationReadCategoriesDocumentEnd";

		// Token: 0x04001591 RID: 5521
		internal const string TraceCodeSyndicationWriteCategoriesDocumentBegin = "TraceCodeSyndicationWriteCategoriesDocumentBegin";

		// Token: 0x04001592 RID: 5522
		internal const string TraceCodeSyndicationWriteCategoriesDocumentEnd = "TraceCodeSyndicationWriteCategoriesDocumentEnd";

		// Token: 0x04001593 RID: 5523
		internal const string FeedAuthorsIgnoredOnWrite = "FeedAuthorsIgnoredOnWrite";

		// Token: 0x04001594 RID: 5524
		internal const string FeedContributorsIgnoredOnWrite = "FeedContributorsIgnoredOnWrite";

		// Token: 0x04001595 RID: 5525
		internal const string FeedIdIgnoredOnWrite = "FeedIdIgnoredOnWrite";

		// Token: 0x04001596 RID: 5526
		internal const string FeedLinksIgnoredOnWrite = "FeedLinksIgnoredOnWrite";

		// Token: 0x04001597 RID: 5527
		internal const string ItemAuthorsIgnoredOnWrite = "ItemAuthorsIgnoredOnWrite";

		// Token: 0x04001598 RID: 5528
		internal const string ItemContributorsIgnoredOnWrite = "ItemContributorsIgnoredOnWrite";

		// Token: 0x04001599 RID: 5529
		internal const string ItemLinksIgnoredOnWrite = "ItemLinksIgnoredOnWrite";

		// Token: 0x0400159A RID: 5530
		internal const string ItemCopyrightIgnoredOnWrite = "ItemCopyrightIgnoredOnWrite";

		// Token: 0x0400159B RID: 5531
		internal const string ItemContentIgnoredOnWrite = "ItemContentIgnoredOnWrite";

		// Token: 0x0400159C RID: 5532
		internal const string ItemLastUpdatedTimeIgnoredOnWrite = "ItemLastUpdatedTimeIgnoredOnWrite";

		// Token: 0x0400159D RID: 5533
		internal const string OuterNameOfElementExtensionEmpty = "OuterNameOfElementExtensionEmpty";

		// Token: 0x0400159E RID: 5534
		internal const string InvalidObjectTypePassed = "InvalidObjectTypePassed";

		// Token: 0x0400159F RID: 5535
		internal const string UnableToImpersonateWhileSerializingReponse = "UnableToImpersonateWhileSerializingReponse";

		// Token: 0x040015A0 RID: 5536
		internal const string XmlLineInfo = "XmlLineInfo";

		// Token: 0x040015A1 RID: 5537
		internal const string XmlFoundEndOfFile = "XmlFoundEndOfFile";

		// Token: 0x040015A2 RID: 5538
		internal const string XmlFoundElement = "XmlFoundElement";

		// Token: 0x040015A3 RID: 5539
		internal const string XmlFoundEndElement = "XmlFoundEndElement";

		// Token: 0x040015A4 RID: 5540
		internal const string XmlFoundText = "XmlFoundText";

		// Token: 0x040015A5 RID: 5541
		internal const string XmlFoundCData = "XmlFoundCData";

		// Token: 0x040015A6 RID: 5542
		internal const string XmlFoundComment = "XmlFoundComment";

		// Token: 0x040015A7 RID: 5543
		internal const string XmlFoundNodeType = "XmlFoundNodeType";

		// Token: 0x040015A8 RID: 5544
		internal const string XmlStartElementExpected = "XmlStartElementExpected";

		// Token: 0x040015A9 RID: 5545
		internal const string SingleWsdlNotGenerated = "SingleWsdlNotGenerated";

		// Token: 0x040015AA RID: 5546
		internal const string SFxDocExt_MainPageIntroSingleWsdl = "SFxDocExt_MainPageIntroSingleWsdl";

		// Token: 0x040015AB RID: 5547
		internal const string TaskMethodParameterNotSupported = "TaskMethodParameterNotSupported";

		// Token: 0x040015AC RID: 5548
		internal const string TaskMethodMustNotHaveOutParameter = "TaskMethodMustNotHaveOutParameter";

		// Token: 0x040015AD RID: 5549
		internal const string SFxCannotImportAsParameters_OutputParameterAndTask = "SFxCannotImportAsParameters_OutputParameterAndTask";

		// Token: 0x040015AE RID: 5550
		internal const string ID0020 = "ID0020";

		// Token: 0x040015AF RID: 5551
		internal const string ID2004 = "ID2004";

		// Token: 0x040015B0 RID: 5552
		internal const string ID3002 = "ID3002";

		// Token: 0x040015B1 RID: 5553
		internal const string ID3004 = "ID3004";

		// Token: 0x040015B2 RID: 5554
		internal const string ID3022 = "ID3022";

		// Token: 0x040015B3 RID: 5555
		internal const string ID3023 = "ID3023";

		// Token: 0x040015B4 RID: 5556
		internal const string ID3097 = "ID3097";

		// Token: 0x040015B5 RID: 5557
		internal const string ID3112 = "ID3112";

		// Token: 0x040015B6 RID: 5558
		internal const string ID3113 = "ID3113";

		// Token: 0x040015B7 RID: 5559
		internal const string ID3114 = "ID3114";

		// Token: 0x040015B8 RID: 5560
		internal const string ID3137 = "ID3137";

		// Token: 0x040015B9 RID: 5561
		internal const string ID3138 = "ID3138";

		// Token: 0x040015BA RID: 5562
		internal const string ID3139 = "ID3139";

		// Token: 0x040015BB RID: 5563
		internal const string ID3140 = "ID3140";

		// Token: 0x040015BC RID: 5564
		internal const string ID3141 = "ID3141";

		// Token: 0x040015BD RID: 5565
		internal const string ID3144 = "ID3144";

		// Token: 0x040015BE RID: 5566
		internal const string ID3146 = "ID3146";

		// Token: 0x040015BF RID: 5567
		internal const string ID3147 = "ID3147";

		// Token: 0x040015C0 RID: 5568
		internal const string ID3148 = "ID3148";

		// Token: 0x040015C1 RID: 5569
		internal const string ID3149 = "ID3149";

		// Token: 0x040015C2 RID: 5570
		internal const string ID3150 = "ID3150";

		// Token: 0x040015C3 RID: 5571
		internal const string ID3190 = "ID3190";

		// Token: 0x040015C4 RID: 5572
		internal const string ID3191 = "ID3191";

		// Token: 0x040015C5 RID: 5573
		internal const string ID3192 = "ID3192";

		// Token: 0x040015C6 RID: 5574
		internal const string ID3193 = "ID3193";

		// Token: 0x040015C7 RID: 5575
		internal const string ID3194 = "ID3194";

		// Token: 0x040015C8 RID: 5576
		internal const string ID3269 = "ID3269";

		// Token: 0x040015C9 RID: 5577
		internal const string ID3270 = "ID3270";

		// Token: 0x040015CA RID: 5578
		internal const string ID3285 = "ID3285";

		// Token: 0x040015CB RID: 5579
		internal const string ID3286 = "ID3286";

		// Token: 0x040015CC RID: 5580
		internal const string ID3287 = "ID3287";

		// Token: 0x040015CD RID: 5581
		internal const string ID4008 = "ID4008";

		// Token: 0x040015CE RID: 5582
		internal const string ID4039 = "ID4039";

		// Token: 0x040015CF RID: 5583
		internal const string ID4041 = "ID4041";

		// Token: 0x040015D0 RID: 5584
		internal const string ID4053 = "ID4053";

		// Token: 0x040015D1 RID: 5585
		internal const string ID4072 = "ID4072";

		// Token: 0x040015D2 RID: 5586
		internal const string ID4101 = "ID4101";

		// Token: 0x040015D3 RID: 5587
		internal const string ID4192 = "ID4192";

		// Token: 0x040015D4 RID: 5588
		internal const string ID4240 = "ID4240";

		// Token: 0x040015D5 RID: 5589
		internal const string ID4244 = "ID4244";

		// Token: 0x040015D6 RID: 5590
		internal const string ID4245 = "ID4245";

		// Token: 0x040015D7 RID: 5591
		internal const string ID4268 = "ID4268";

		// Token: 0x040015D8 RID: 5592
		internal const string ID4271 = "ID4271";

		// Token: 0x040015D9 RID: 5593
		internal const string ID4274 = "ID4274";

		// Token: 0x040015DA RID: 5594
		internal const string ID4285 = "ID4285";

		// Token: 0x040015DB RID: 5595
		internal const string ID4287 = "ID4287";

		// Token: 0x040015DC RID: 5596
		internal const string ID5004 = "ID5004";

		// Token: 0x040015DD RID: 5597
		internal const string TraceAuthorize = "TraceAuthorize";

		// Token: 0x040015DE RID: 5598
		internal const string TraceOnAuthorizeRequestFailed = "TraceOnAuthorizeRequestFailed";

		// Token: 0x040015DF RID: 5599
		internal const string TraceOnAuthorizeRequestSucceed = "TraceOnAuthorizeRequestSucceed";

		// Token: 0x040015E0 RID: 5600
		internal const string AuthFailed = "AuthFailed";

		// Token: 0x040015E1 RID: 5601
		internal const string DuplicateFederatedClientCredentialsParameters = "DuplicateFederatedClientCredentialsParameters";

		// Token: 0x040015E2 RID: 5602
		internal const string UnsupportedTrustVersion = "UnsupportedTrustVersion";

		// Token: 0x040015E3 RID: 5603
		internal const string InputMustBeDelegatingHandlerElementError = "InputMustBeDelegatingHandlerElementError";

		// Token: 0x040015E4 RID: 5604
		internal const string InputTypeListEmptyError = "InputTypeListEmptyError";

		// Token: 0x040015E5 RID: 5605
		internal const string DelegatingHandlerArrayHasNonNullInnerHandler = "DelegatingHandlerArrayHasNonNullInnerHandler";

		// Token: 0x040015E6 RID: 5606
		internal const string DelegatingHandlerArrayFromFuncContainsNullItem = "DelegatingHandlerArrayFromFuncContainsNullItem";

		// Token: 0x040015E7 RID: 5607
		internal const string HttpMessageHandlerFactoryConfigInvalid_WithBothTypeAndHandlerList = "HttpMessageHandlerFactoryConfigInvalid_WithBothTypeAndHandlerList";

		// Token: 0x040015E8 RID: 5608
		internal const string HttpMessageHandlerFactoryWithFuncCannotGenerateConfig = "HttpMessageHandlerFactoryWithFuncCannotGenerateConfig";

		// Token: 0x040015E9 RID: 5609
		internal const string HttpMessageHandlerTypeNotSupported = "HttpMessageHandlerTypeNotSupported";

		// Token: 0x040015EA RID: 5610
		internal const string HttpMessageHandlerChannelFactoryNullPipeline = "HttpMessageHandlerChannelFactoryNullPipeline";

		// Token: 0x040015EB RID: 5611
		internal const string HttpPipelineOperationCanceledError = "HttpPipelineOperationCanceledError";

		// Token: 0x040015EC RID: 5612
		internal const string HttpPipelineMessagePropertyMissingError = "HttpPipelineMessagePropertyMissingError";

		// Token: 0x040015ED RID: 5613
		internal const string HttpPipelineMessagePropertyTypeError = "HttpPipelineMessagePropertyTypeError";

		// Token: 0x040015EE RID: 5614
		internal const string InvalidContentTypeError = "InvalidContentTypeError";

		// Token: 0x040015EF RID: 5615
		internal const string HttpPipelineNotSupportedOnClientSide = "HttpPipelineNotSupportedOnClientSide";

		// Token: 0x040015F0 RID: 5616
		internal const string CanNotLoadTypeGotFromConfig = "CanNotLoadTypeGotFromConfig";

		// Token: 0x040015F1 RID: 5617
		internal const string HttpPipelineNotSupportNullResponseMessage = "HttpPipelineNotSupportNullResponseMessage";

		// Token: 0x040015F2 RID: 5618
		internal const string WebSocketInvalidProtocolNoHeader = "WebSocketInvalidProtocolNoHeader";

		// Token: 0x040015F3 RID: 5619
		internal const string WebSocketInvalidProtocolNotInClientList = "WebSocketInvalidProtocolNotInClientList";

		// Token: 0x040015F4 RID: 5620
		internal const string WebSocketInvalidProtocolInvalidCharInProtocolString = "WebSocketInvalidProtocolInvalidCharInProtocolString";

		// Token: 0x040015F5 RID: 5621
		internal const string WebSocketInvalidProtocolContainsMultipleSubProtocolString = "WebSocketInvalidProtocolContainsMultipleSubProtocolString";

		// Token: 0x040015F6 RID: 5622
		internal const string WebSocketInvalidProtocolEmptySubprotocolString = "WebSocketInvalidProtocolEmptySubprotocolString";

		// Token: 0x040015F7 RID: 5623
		internal const string WebSocketOpaqueStreamContentNotSupportError = "WebSocketOpaqueStreamContentNotSupportError";

		// Token: 0x040015F8 RID: 5624
		internal const string WebSocketElementConfigInvalidHttpMessageHandlerFactoryType = "WebSocketElementConfigInvalidHttpMessageHandlerFactoryType";

		// Token: 0x040015F9 RID: 5625
		internal const string WebSocketEndpointOnlySupportWebSocketError = "WebSocketEndpointOnlySupportWebSocketError";

		// Token: 0x040015FA RID: 5626
		internal const string WebSocketEndpointDoesNotSupportWebSocketError = "WebSocketEndpointDoesNotSupportWebSocketError";

		// Token: 0x040015FB RID: 5627
		internal const string WebSocketUpgradeFailedError = "WebSocketUpgradeFailedError";

		// Token: 0x040015FC RID: 5628
		internal const string WebSocketUpgradeFailedHeaderMissingError = "WebSocketUpgradeFailedHeaderMissingError";

		// Token: 0x040015FD RID: 5629
		internal const string WebSocketUpgradeFailedWrongHeaderError = "WebSocketUpgradeFailedWrongHeaderError";

		// Token: 0x040015FE RID: 5630
		internal const string WebSocketUpgradeFailedInvalidProtocolError = "WebSocketUpgradeFailedInvalidProtocolError";

		// Token: 0x040015FF RID: 5631
		internal const string WebSocketContextWebSocketCannotBeAccessedError = "WebSocketContextWebSocketCannotBeAccessedError";

		// Token: 0x04001600 RID: 5632
		internal const string WebSocketTransportError = "WebSocketTransportError";

		// Token: 0x04001601 RID: 5633
		internal const string WebSocketUnexpectedCloseMessageError = "WebSocketUnexpectedCloseMessageError";

		// Token: 0x04001602 RID: 5634
		internal const string WebSocketStreamWriteCalledAfterEOMSent = "WebSocketStreamWriteCalledAfterEOMSent";

		// Token: 0x04001603 RID: 5635
		internal const string WebSocketCannotCreateRequestClientChannelWithCertainWebSocketTransportUsage = "WebSocketCannotCreateRequestClientChannelWithCertainWebSocketTransportUsage";

		// Token: 0x04001604 RID: 5636
		internal const string WebSocketMaxPendingConnectionsReached = "WebSocketMaxPendingConnectionsReached";

		// Token: 0x04001605 RID: 5637
		internal const string WebSocketOpeningHandshakePropertiesNotAvailable = "WebSocketOpeningHandshakePropertiesNotAvailable";

		// Token: 0x04001606 RID: 5638
		internal const string AcceptWebSocketTimedOutError = "AcceptWebSocketTimedOutError";

		// Token: 0x04001607 RID: 5639
		internal const string TaskCancelledError = "TaskCancelledError";

		// Token: 0x04001608 RID: 5640
		internal const string ClientWebSocketFactory_GetWebSocketVersionFailed = "ClientWebSocketFactory_GetWebSocketVersionFailed";

		// Token: 0x04001609 RID: 5641
		internal const string ClientWebSocketFactory_InvalidWebSocketVersion = "ClientWebSocketFactory_InvalidWebSocketVersion";

		// Token: 0x0400160A RID: 5642
		internal const string ClientWebSocketFactory_CreateWebSocketFailed = "ClientWebSocketFactory_CreateWebSocketFailed";

		// Token: 0x0400160B RID: 5643
		internal const string ClientWebSocketFactory_InvalidWebSocket = "ClientWebSocketFactory_InvalidWebSocket";

		// Token: 0x0400160C RID: 5644
		internal const string ClientWebSocketFactory_InvalidSubProtocol = "ClientWebSocketFactory_InvalidSubProtocol";

		// Token: 0x0400160D RID: 5645
		internal const string MultipleClientWebSocketFactoriesSpecified = "MultipleClientWebSocketFactoriesSpecified";

		// Token: 0x0400160E RID: 5646
		internal const string WebSocketSendTimedOut = "WebSocketSendTimedOut";

		// Token: 0x0400160F RID: 5647
		internal const string WebSocketReceiveTimedOut = "WebSocketReceiveTimedOut";

		// Token: 0x04001610 RID: 5648
		internal const string WebSocketOperationTimedOut = "WebSocketOperationTimedOut";

		// Token: 0x04001611 RID: 5649
		internal const string WebSocketsServerSideNotSupported = "WebSocketsServerSideNotSupported";

		// Token: 0x04001612 RID: 5650
		internal const string WebSocketsClientSideNotSupported = "WebSocketsClientSideNotSupported";

		// Token: 0x04001613 RID: 5651
		internal const string WebSocketsNotSupportedInClassicPipeline = "WebSocketsNotSupportedInClassicPipeline";

		// Token: 0x04001614 RID: 5652
		internal const string WebSocketModuleNotLoaded = "WebSocketModuleNotLoaded";

		// Token: 0x04001615 RID: 5653
		internal const string WebSocketTransportPolicyAssertionInvalid = "WebSocketTransportPolicyAssertionInvalid";

		// Token: 0x04001616 RID: 5654
		internal const string WebSocketVersionMismatchFromServer = "WebSocketVersionMismatchFromServer";

		// Token: 0x04001617 RID: 5655
		internal const string WebSocketSubProtocolMismatchFromServer = "WebSocketSubProtocolMismatchFromServer";

		// Token: 0x04001618 RID: 5656
		internal const string WebSocketContentTypeMismatchFromServer = "WebSocketContentTypeMismatchFromServer";

		// Token: 0x04001619 RID: 5657
		internal const string WebSocketContentTypeAndTransferModeMismatchFromServer = "WebSocketContentTypeAndTransferModeMismatchFromServer";

		// Token: 0x0400161A RID: 5658
		internal const string ResponseHeaderWithRequestHeadersCollection = "ResponseHeaderWithRequestHeadersCollection";

		// Token: 0x0400161B RID: 5659
		internal const string RequestHeaderWithResponseHeadersCollection = "RequestHeaderWithResponseHeadersCollection";

		// Token: 0x0400161C RID: 5660
		internal const string MessageVersionNoneRequiredForHttpMessageSupport = "MessageVersionNoneRequiredForHttpMessageSupport";

		// Token: 0x0400161D RID: 5661
		internal const string WebHeaderEnumOperationCantHappen = "WebHeaderEnumOperationCantHappen";

		// Token: 0x0400161E RID: 5662
		internal const string WebHeaderEmptyStringCall = "WebHeaderEmptyStringCall";

		// Token: 0x0400161F RID: 5663
		internal const string WebHeaderInvalidControlChars = "WebHeaderInvalidControlChars";

		// Token: 0x04001620 RID: 5664
		internal const string WebHeaderInvalidCRLFChars = "WebHeaderInvalidCRLFChars";

		// Token: 0x04001621 RID: 5665
		internal const string WebHeaderInvalidHeaderChars = "WebHeaderInvalidHeaderChars";

		// Token: 0x04001622 RID: 5666
		internal const string WebHeaderInvalidNonAsciiChars = "WebHeaderInvalidNonAsciiChars";

		// Token: 0x04001623 RID: 5667
		internal const string WebHeaderArgumentOutOfRange = "WebHeaderArgumentOutOfRange";

		// Token: 0x04001624 RID: 5668
		internal const string CopyHttpHeaderFailed = "CopyHttpHeaderFailed";

		// Token: 0x04001625 RID: 5669
		internal const string X509ChainBuildFail = "X509ChainBuildFail";

		// Token: 0x04001626 RID: 5670
		internal const string TraceCodeWarnServiceHealthPageEnabledNoBaseAddress = "TraceCodeWarnServiceHealthPageEnabledNoBaseAddress";

		// Token: 0x04001627 RID: 5671
		internal const string ServiceHealthBehavior_Address = "ServiceHealthBehavior_Address";

		// Token: 0x04001628 RID: 5672
		internal const string ServiceHealthBehavior_Available = "ServiceHealthBehavior_Available";

		// Token: 0x04001629 RID: 5673
		internal const string ServiceHealthBehavior_BaseAddresses = "ServiceHealthBehavior_BaseAddresses";

		// Token: 0x0400162A RID: 5674
		internal const string ServiceHealthBehavior_Binding = "ServiceHealthBehavior_Binding";

		// Token: 0x0400162B RID: 5675
		internal const string ServiceHealthBehavior_ChannelDispatchers = "ServiceHealthBehavior_ChannelDispatchers";

		// Token: 0x0400162C RID: 5676
		internal const string ServiceHealthBehavior_ChannelTimeouts = "ServiceHealthBehavior_ChannelTimeouts";

		// Token: 0x0400162D RID: 5677
		internal const string ServiceHealthBehavior_Close = "ServiceHealthBehavior_Close";

		// Token: 0x0400162E RID: 5678
		internal const string ServiceHealthBehavior_CompletionPortThreads = "ServiceHealthBehavior_CompletionPortThreads";

		// Token: 0x0400162F RID: 5679
		internal const string ServiceHealthBehavior_ConcurrencyMode = "ServiceHealthBehavior_ConcurrencyMode";

		// Token: 0x04001630 RID: 5680
		internal const string ServiceHealthBehavior_ConcurrentCalls = "ServiceHealthBehavior_ConcurrentCalls";

		// Token: 0x04001631 RID: 5681
		internal const string ServiceHealthBehavior_Contract = "ServiceHealthBehavior_Contract";

		// Token: 0x04001632 RID: 5682
		internal const string ServiceHealthBehavior_EndpointBehaviors = "ServiceHealthBehavior_EndpointBehaviors";

		// Token: 0x04001633 RID: 5683
		internal const string ServiceHealthBehavior_Endpoints = "ServiceHealthBehavior_Endpoints";

		// Token: 0x04001634 RID: 5684
		internal const string ServiceHealthBehavior_GCMode = "ServiceHealthBehavior_GCMode";

		// Token: 0x04001635 RID: 5685
		internal const string ServiceHealthBehavior_InstanceContextMode = "ServiceHealthBehavior_InstanceContextMode";

		// Token: 0x04001636 RID: 5686
		internal const string ServiceHealthBehavior_Instances = "ServiceHealthBehavior_Instances";

		// Token: 0x04001637 RID: 5687
		internal const string ServiceHealthBehavior_IsSystemEndpoint = "ServiceHealthBehavior_IsSystemEndpoint";

		// Token: 0x04001638 RID: 5688
		internal const string ServiceHealthBehavior_ListenerState = "ServiceHealthBehavior_ListenerState";

		// Token: 0x04001639 RID: 5689
		internal const string ServiceHealthBehavior_ListenerUri = "ServiceHealthBehavior_ListenerUri";

		// Token: 0x0400163A RID: 5690
		internal const string ServiceHealthBehavior_MaxLimit = "ServiceHealthBehavior_MaxLimit";

		// Token: 0x0400163B RID: 5691
		internal const string ServiceHealthBehavior_MessageEncoder = "ServiceHealthBehavior_MessageEncoder";

		// Token: 0x0400163C RID: 5692
		internal const string ServiceHealthBehavior_MessageInspectors = "ServiceHealthBehavior_MessageInspectors";

		// Token: 0x0400163D RID: 5693
		internal const string ServiceHealthBehavior_MinLimit = "ServiceHealthBehavior_MinLimit";

		// Token: 0x0400163E RID: 5694
		internal const string ServiceHealthBehavior_NativeThreadCount = "ServiceHealthBehavior_NativeThreadCount";

		// Token: 0x0400163F RID: 5695
		internal const string ServiceHealthBehavior_Open = "ServiceHealthBehavior_Open";

		// Token: 0x04001640 RID: 5696
		internal const string ServiceHealthBehavior_ProcessBitness = "ServiceHealthBehavior_ProcessBitness";

		// Token: 0x04001641 RID: 5697
		internal const string ServiceHealthBehavior_ProcessInformation = "ServiceHealthBehavior_ProcessInformation";

		// Token: 0x04001642 RID: 5698
		internal const string ServiceHealthBehavior_ProcessName = "ServiceHealthBehavior_ProcessName";

		// Token: 0x04001643 RID: 5699
		internal const string ServiceHealthBehavior_ProcessRunningSince = "ServiceHealthBehavior_ProcessRunningSince";

		// Token: 0x04001644 RID: 5700
		internal const string ServiceHealthBehavior_Receive = "ServiceHealthBehavior_Receive";

		// Token: 0x04001645 RID: 5701
		internal const string ServiceHealthBehavior_Send = "ServiceHealthBehavior_Send";

		// Token: 0x04001646 RID: 5702
		internal const string ServiceHealthBehavior_ServiceBehaviors = "ServiceHealthBehavior_ServiceBehaviors";

		// Token: 0x04001647 RID: 5703
		internal const string ServiceHealthBehavior_ServiceName = "ServiceHealthBehavior_ServiceName";

		// Token: 0x04001648 RID: 5704
		internal const string ServiceHealthBehavior_ServiceRunningSince = "ServiceHealthBehavior_ServiceRunningSince";

		// Token: 0x04001649 RID: 5705
		internal const string ServiceHealthBehavior_ServiceThrottles = "ServiceHealthBehavior_ServiceThrottles";

		// Token: 0x0400164A RID: 5706
		internal const string ServiceHealthBehavior_ServiceType = "ServiceHealthBehavior_ServiceType";

		// Token: 0x0400164B RID: 5707
		internal const string ServiceHealthBehavior_Sessions = "ServiceHealthBehavior_Sessions";

		// Token: 0x0400164C RID: 5708
		internal const string ServiceHealthBehavior_State = "ServiceHealthBehavior_State";

		// Token: 0x0400164D RID: 5709
		internal const string ServiceHealthBehavior_Threads = "ServiceHealthBehavior_Threads";

		// Token: 0x0400164E RID: 5710
		internal const string ServiceHealthBehavior_Uptime = "ServiceHealthBehavior_Uptime";

		// Token: 0x0400164F RID: 5711
		internal const string ServiceHealthBehavior_WCFServiceProperties = "ServiceHealthBehavior_WCFServiceProperties";

		// Token: 0x04001650 RID: 5712
		internal const string ServiceHealthBehavior_WorkerThreads = "ServiceHealthBehavior_WorkerThreads";

		// Token: 0x04001651 RID: 5713
		private static SR loader;

		// Token: 0x04001652 RID: 5714
		private ResourceManager resources;
	}
}

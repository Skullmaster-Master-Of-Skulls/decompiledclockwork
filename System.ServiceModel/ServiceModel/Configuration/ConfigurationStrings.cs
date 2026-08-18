using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200060D RID: 1549
	internal static class ConfigurationStrings
	{
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000E40D9 File Offset: 0x000E22D9
		internal static string BehaviorsSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("behaviors");
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x000E40E5 File Offset: 0x000E22E5
		internal static string BindingsSectionGroupPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("bindings");
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x000E40F1 File Offset: 0x000E22F1
		internal static string ClientSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("client");
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x000E40FD File Offset: 0x000E22FD
		internal static string ComContractsSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("comContracts");
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x000E4109 File Offset: 0x000E2309
		internal static string CommonBehaviorsSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("commonBehaviors");
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x000E4115 File Offset: 0x000E2315
		internal static string DiagnosticSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("diagnostics");
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000E4121 File Offset: 0x000E2321
		internal static string ExtensionsSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("extensions");
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000E412D File Offset: 0x000E232D
		internal static string ProtocolMappingSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("protocolMapping");
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000E4139 File Offset: 0x000E2339
		internal static string ServiceHostingEnvironmentSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("serviceHostingEnvironment");
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x000E4145 File Offset: 0x000E2345
		internal static string ServicesSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("services");
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000E4151 File Offset: 0x000E2351
		internal static string StandardEndpointsSectionPath
		{
			get
			{
				return ConfigurationHelpers.GetSectionPath("standardEndpoints");
			}
		}

		// Token: 0x04002A89 RID: 10889
		internal const string AcknowledgementInterval = "acknowledgementInterval";

		// Token: 0x04002A8A RID: 10890
		internal const string ActivityTracing = "activityTracing";

		// Token: 0x04002A8B RID: 10891
		internal const string Add = "add";

		// Token: 0x04002A8C RID: 10892
		internal const string AdditionalRequestParameters = "additionalRequestParameters";

		// Token: 0x04002A8D RID: 10893
		internal const string Address = "address";

		// Token: 0x04002A8E RID: 10894
		internal const string AlgorithmSuite = "algorithmSuite";

		// Token: 0x04002A8F RID: 10895
		internal const string AllowAnonymousLogons = "allowAnonymousLogons";

		// Token: 0x04002A90 RID: 10896
		internal const string AllowCookies = "allowCookies";

		// Token: 0x04002A91 RID: 10897
		internal const string AllowedAudienceUris = "allowedAudienceUris";

		// Token: 0x04002A92 RID: 10898
		internal const string AllowedAudienceUri = "allowedAudienceUri";

		// Token: 0x04002A93 RID: 10899
		internal const string AllowedImpersonationLevel = "allowedImpersonationLevel";

		// Token: 0x04002A94 RID: 10900
		internal const string AllowInsecureTransport = "allowInsecureTransport";

		// Token: 0x04002A95 RID: 10901
		internal const string AllowNtlm = "allowNtlm";

		// Token: 0x04002A96 RID: 10902
		internal const string AllowSerializedSigningTokenOnReply = "allowSerializedSigningTokenOnReply";

		// Token: 0x04002A97 RID: 10903
		internal const string AllowUntrustedRsaIssuers = "allowUntrustedRsaIssuers";

		// Token: 0x04002A98 RID: 10904
		internal const string AlternativeIssuedTokenParameters = "alternativeIssuedTokenParameters";

		// Token: 0x04002A99 RID: 10905
		internal const string ApplicationContainerSettings = "applicationContainerSettings";

		// Token: 0x04002A9A RID: 10906
		internal const string AspNetCompatibilityEnabled = "aspNetCompatibilityEnabled";

		// Token: 0x04002A9B RID: 10907
		internal const string AsynchronousSendEnabled = "asynchronousSendEnabled";

		// Token: 0x04002A9C RID: 10908
		internal const string AudienceUriMode = "audienceUriMode";

		// Token: 0x04002A9D RID: 10909
		internal const string AuditLogLocation = "auditLogLocation";

		// Token: 0x04002A9E RID: 10910
		internal const string Authentication = "authentication";

		// Token: 0x04002A9F RID: 10911
		internal const string AuthenticationMode = "authenticationMode";

		// Token: 0x04002AA0 RID: 10912
		internal const string AuthenticationScheme = "authenticationScheme";

		// Token: 0x04002AA1 RID: 10913
		internal const string AuthenticationSchemes = "authenticationSchemes";

		// Token: 0x04002AA2 RID: 10914
		internal const string AuthorizationPolicies = "authorizationPolicies";

		// Token: 0x04002AA3 RID: 10915
		internal const string BaseAddress = "baseAddress";

		// Token: 0x04002AA4 RID: 10916
		internal const string BaseAddresses = "baseAddresses";

		// Token: 0x04002AA5 RID: 10917
		internal const string BaseAddressPrefixFilters = "baseAddressPrefixFilters";

		// Token: 0x04002AA6 RID: 10918
		internal const string Basic128 = "Basic128";

		// Token: 0x04002AA7 RID: 10919
		internal const string Basic192 = "Basic192";

		// Token: 0x04002AA8 RID: 10920
		internal const string Basic256 = "Basic256";

		// Token: 0x04002AA9 RID: 10921
		internal const string Basic128Rsa15 = "Basic128Rsa15";

		// Token: 0x04002AAA RID: 10922
		internal const string Basic192Rsa15 = "Basic192Rsa15";

		// Token: 0x04002AAB RID: 10923
		internal const string Basic256Rsa15 = "Basic256Rsa15";

		// Token: 0x04002AAC RID: 10924
		internal const string Basic128Sha256 = "Basic128Sha256";

		// Token: 0x04002AAD RID: 10925
		internal const string Basic192Sha256 = "Basic192Sha256";

		// Token: 0x04002AAE RID: 10926
		internal const string Basic256Sha256 = "Basic256Sha256";

		// Token: 0x04002AAF RID: 10927
		internal const string Basic128Sha256Rsa15 = "Basic128Sha256Rsa15";

		// Token: 0x04002AB0 RID: 10928
		internal const string Basic192Sha256Rsa15 = "Basic192Sha256Rsa15";

		// Token: 0x04002AB1 RID: 10929
		internal const string Basic256Sha256Rsa15 = "Basic256Sha256Rsa15";

		// Token: 0x04002AB2 RID: 10930
		internal const string BasicHttpBindingCollectionElementName = "basicHttpBinding";

		// Token: 0x04002AB3 RID: 10931
		internal const string BasicHttpsBindingCollectionElementName = "basicHttpsBinding";

		// Token: 0x04002AB4 RID: 10932
		internal const string Behavior = "behavior";

		// Token: 0x04002AB5 RID: 10933
		internal const string BehaviorConfiguration = "behaviorConfiguration";

		// Token: 0x04002AB6 RID: 10934
		internal const string BehaviorExtensions = "behaviorExtensions";

		// Token: 0x04002AB7 RID: 10935
		internal const string BehaviorsSectionName = "behaviors";

		// Token: 0x04002AB8 RID: 10936
		internal const string BinaryMessageEncodingSectionName = "binaryMessageEncoding";

		// Token: 0x04002AB9 RID: 10937
		internal const string Binding = "binding";

		// Token: 0x04002ABA RID: 10938
		internal const string BindingConfiguration = "bindingConfiguration";

		// Token: 0x04002ABB RID: 10939
		internal const string BindingElementExtensions = "bindingElementExtensions";

		// Token: 0x04002ABC RID: 10940
		internal const string BindingExtensions = "bindingExtensions";

		// Token: 0x04002ABD RID: 10941
		internal const string BindingName = "bindingName";

		// Token: 0x04002ABE RID: 10942
		internal const string BindingNamespace = "bindingNamespace";

		// Token: 0x04002ABF RID: 10943
		internal const string BindingsSectionGroupName = "bindings";

		// Token: 0x04002AC0 RID: 10944
		internal const string BypassProxyOnLocal = "bypassProxyOnLocal";

		// Token: 0x04002AC1 RID: 10945
		internal const string CacheCookies = "cacheCookies";

		// Token: 0x04002AC2 RID: 10946
		internal const string CachedLogonTokenLifetime = "cachedLogonTokenLifetime";

		// Token: 0x04002AC3 RID: 10947
		internal const string CacheIssuedTokens = "cacheIssuedTokens";

		// Token: 0x04002AC4 RID: 10948
		internal const string CacheLogonTokens = "cacheLogonTokens";

		// Token: 0x04002AC5 RID: 10949
		internal const string CallbackDebugSectionName = "callbackDebug";

		// Token: 0x04002AC6 RID: 10950
		internal const string CallbackTimeouts = "callbackTimeouts";

		// Token: 0x04002AC7 RID: 10951
		internal const string CanRenewSecurityContextToken = "canRenewSecurityContextToken";

		// Token: 0x04002AC8 RID: 10952
		internal const string Certificate = "certificate";

		// Token: 0x04002AC9 RID: 10953
		internal const string CertificateReference = "certificateReference";

		// Token: 0x04002ACA RID: 10954
		internal const string CertificateValidationMode = "certificateValidationMode";

		// Token: 0x04002ACB RID: 10955
		internal const string Channel = "channel";

		// Token: 0x04002ACC RID: 10956
		internal const string ChannelInitializationTimeout = "channelInitializationTimeout";

		// Token: 0x04002ACD RID: 10957
		internal const string ChannelPoolSettings = "channelPoolSettings";

		// Token: 0x04002ACE RID: 10958
		internal const string ClaimType = "claimType";

		// Token: 0x04002ACF RID: 10959
		internal const string ClaimTypeRequirements = "claimTypeRequirements";

		// Token: 0x04002AD0 RID: 10960
		internal const string Clear = "clear";

		// Token: 0x04002AD1 RID: 10961
		internal const string ClientBaseAddress = "clientBaseAddress";

		// Token: 0x04002AD2 RID: 10962
		internal const string ClientCallbackAddressName = "clientCallbackAddress";

		// Token: 0x04002AD3 RID: 10963
		internal const string ClientCertificate = "clientCertificate";

		// Token: 0x04002AD4 RID: 10964
		internal const string ClientCredentials = "clientCredentials";

		// Token: 0x04002AD5 RID: 10965
		internal const string ClientCredentialType = "clientCredentialType";

		// Token: 0x04002AD6 RID: 10966
		internal const string ClientSectionName = "client";

		// Token: 0x04002AD7 RID: 10967
		internal const string ClientViaSectionName = "clientVia";

		// Token: 0x04002AD8 RID: 10968
		internal const string CloseIdleServicesAtLowMemory = "closeIdleServicesAtLowMemory";

		// Token: 0x04002AD9 RID: 10969
		internal const string CloseTimeout = "closeTimeout";

		// Token: 0x04002ADA RID: 10970
		internal const string ComContract = "comContract";

		// Token: 0x04002ADB RID: 10971
		internal const string ComContractName = "name";

		// Token: 0x04002ADC RID: 10972
		internal const string ComContractNamespace = "namespace";

		// Token: 0x04002ADD RID: 10973
		internal const string ComContractsSectionName = "comContracts";

		// Token: 0x04002ADE RID: 10974
		internal const string ComMethod = "exposedMethod";

		// Token: 0x04002ADF RID: 10975
		internal const string ComMethodCollection = "exposedMethods";

		// Token: 0x04002AE0 RID: 10976
		internal const string CommonBehaviorsSectionName = "commonBehaviors";

		// Token: 0x04002AE1 RID: 10977
		internal const string ComPersistableTypes = "persistableTypes";

		// Token: 0x04002AE2 RID: 10978
		internal const string CompositeDuplexSectionName = "compositeDuplex";

		// Token: 0x04002AE3 RID: 10979
		internal const string CompressionFormat = "compressionFormat";

		// Token: 0x04002AE4 RID: 10980
		internal const string ComSessionRequired = "requiresSession";

		// Token: 0x04002AE5 RID: 10981
		internal const string ComUdt = "userDefinedType";

		// Token: 0x04002AE6 RID: 10982
		internal const string ComUdtCollection = "userDefinedTypes";

		// Token: 0x04002AE7 RID: 10983
		internal const string ConnectionBufferSize = "connectionBufferSize";

		// Token: 0x04002AE8 RID: 10984
		internal const string ConnectionPoolSettings = "connectionPoolSettings";

		// Token: 0x04002AE9 RID: 10985
		internal const string Contract = "contract";

		// Token: 0x04002AEA RID: 10986
		internal const string Cookie = "Cookie";

		// Token: 0x04002AEB RID: 10987
		internal const string CookieRenewalThresholdPercentage = "cookieRenewalThresholdPercentage";

		// Token: 0x04002AEC RID: 10988
		internal const string CreateNotificationOnConnection = "createNotificationOnConnection";

		// Token: 0x04002AED RID: 10989
		internal const string Custom = "custom";

		// Token: 0x04002AEE RID: 10990
		internal const string CustomBindingCollectionElementName = "customBinding";

		// Token: 0x04002AEF RID: 10991
		internal const string CustomCertificateValidatorType = "customCertificateValidatorType";

		// Token: 0x04002AF0 RID: 10992
		internal const string CustomDeadLetterQueue = "customDeadLetterQueue";

		// Token: 0x04002AF1 RID: 10993
		internal const string CustomUserNamePasswordValidatorType = "customUserNamePasswordValidatorType";

		// Token: 0x04002AF2 RID: 10994
		internal const string DataContractSerializerSectionName = "dataContractSerializer";

		// Token: 0x04002AF3 RID: 10995
		internal const string DeadLetterQueue = "deadLetterQueue";

		// Token: 0x04002AF4 RID: 10996
		internal const string DecompressionEnabled = "decompressionEnabled";

		// Token: 0x04002AF5 RID: 10997
		internal const string Default = "Default";

		// Token: 0x04002AF6 RID: 10998
		internal const string DefaultAlgorithmSuite = "defaultAlgorithmSuite";

		// Token: 0x04002AF7 RID: 10999
		internal const string DefaultCertificate = "defaultCertificate";

		// Token: 0x04002AF8 RID: 11000
		internal const string DefaultCollectionName = "";

		// Token: 0x04002AF9 RID: 11001
		internal const string DefaultKeyEntropyMode = "defaultKeyEntropyMode";

		// Token: 0x04002AFA RID: 11002
		internal const string DefaultMessageSecurityVersion = "defaultMessageSecurityVersion";

		// Token: 0x04002AFB RID: 11003
		internal const string DefaultName = "";

		// Token: 0x04002AFC RID: 11004
		internal const string DefaultPorts = "defaultPorts";

		// Token: 0x04002AFD RID: 11005
		internal const string DetectReplays = "detectReplays";

		// Token: 0x04002AFE RID: 11006
		internal const string DiagnosticSectionName = "diagnostics";

		// Token: 0x04002AFF RID: 11007
		internal const string DisablePayloadMasking = "disablePayloadMasking";

		// Token: 0x04002B00 RID: 11008
		internal const string Dns = "dns";

		// Token: 0x04002B01 RID: 11009
		internal const string Durable = "durable";

		// Token: 0x04002B02 RID: 11010
		internal const string Enabled = "enabled";

		// Token: 0x04002B03 RID: 11011
		internal const string EnableUnsecuredResponse = "enableUnsecuredResponse";

		// Token: 0x04002B04 RID: 11012
		internal const string EncodedValue = "encodedValue";

		// Token: 0x04002B05 RID: 11013
		internal const string Endpoint = "endpoint";

		// Token: 0x04002B06 RID: 11014
		internal const string EndpointBehaviors = "endpointBehaviors";

		// Token: 0x04002B07 RID: 11015
		internal const string EndpointConfiguration = "endpointConfiguration";

		// Token: 0x04002B08 RID: 11016
		internal const string EndpointExtensions = "endpointExtensions";

		// Token: 0x04002B09 RID: 11017
		internal const string EndToEndTracing = "endToEndTracing";

		// Token: 0x04002B0A RID: 11018
		internal const string EstablishSecurityContext = "establishSecurityContext";

		// Token: 0x04002B0B RID: 11019
		internal const string EtwProviderId = "etwProviderId";

		// Token: 0x04002B0C RID: 11020
		internal const string ExactlyOnce = "exactlyOnce";

		// Token: 0x04002B0D RID: 11021
		internal const string ExposedMethod = "exposedMethod";

		// Token: 0x04002B0E RID: 11022
		internal const string ExtendedProtectionPolicy = "extendedProtectionPolicy";

		// Token: 0x04002B0F RID: 11023
		internal const string Extension = "extension";

		// Token: 0x04002B10 RID: 11024
		internal const string Extensions = "extensions";

		// Token: 0x04002B11 RID: 11025
		internal const string ExternalMetadataLocation = "externalMetadataLocation";

		// Token: 0x04002B12 RID: 11026
		internal const string Factory = "factory";

		// Token: 0x04002B13 RID: 11027
		internal const string Filter = "filter";

		// Token: 0x04002B14 RID: 11028
		internal const string Filters = "filters";

		// Token: 0x04002B15 RID: 11029
		internal const string FindValue = "findValue";

		// Token: 0x04002B16 RID: 11030
		internal const string FlowControlEnabled = "flowControlEnabled";

		// Token: 0x04002B17 RID: 11031
		internal const string GroupName = "groupName";

		// Token: 0x04002B18 RID: 11032
		internal const string Handler = "handler";

		// Token: 0x04002B19 RID: 11033
		internal const string Handlers = "handlers";

		// Token: 0x04002B1A RID: 11034
		internal const string Header = "header";

		// Token: 0x04002B1B RID: 11035
		internal const string Headers = "headers";

		// Token: 0x04002B1C RID: 11036
		internal const string Host = "host";

		// Token: 0x04002B1D RID: 11037
		internal const string HostNameComparisonMode = "hostNameComparisonMode";

		// Token: 0x04002B1E RID: 11038
		internal const string HttpDigest = "httpDigest";

		// Token: 0x04002B1F RID: 11039
		internal const string HttpGetEnabled = "httpGetEnabled";

		// Token: 0x04002B20 RID: 11040
		internal const string HttpGetUrl = "httpGetUrl";

		// Token: 0x04002B21 RID: 11041
		internal const string HttpsGetEnabled = "httpsGetEnabled";

		// Token: 0x04002B22 RID: 11042
		internal const string HttpsGetUrl = "httpsGetUrl";

		// Token: 0x04002B23 RID: 11043
		internal const string HealthDetailsEnabled = "healthDetailsEnabled";

		// Token: 0x04002B24 RID: 11044
		internal const string HttpHelpPageEnabled = "httpHelpPageEnabled";

		// Token: 0x04002B25 RID: 11045
		internal const string HttpHelpPageUrl = "httpHelpPageUrl";

		// Token: 0x04002B26 RID: 11046
		internal const string HttpsHelpPageEnabled = "httpsHelpPageEnabled";

		// Token: 0x04002B27 RID: 11047
		internal const string HttpsHelpPageUrl = "httpsHelpPageUrl";

		// Token: 0x04002B28 RID: 11048
		internal const string HttpHelpPageBinding = "httpHelpPageBinding";

		// Token: 0x04002B29 RID: 11049
		internal const string HttpHelpPageBindingConfiguration = "httpHelpPageBindingConfiguration";

		// Token: 0x04002B2A RID: 11050
		internal const string HttpsHelpPageBinding = "httpsHelpPageBinding";

		// Token: 0x04002B2B RID: 11051
		internal const string HttpsHelpPageBindingConfiguration = "httpsHelpPageBindingConfiguration";

		// Token: 0x04002B2C RID: 11052
		internal const string HttpGetBinding = "httpGetBinding";

		// Token: 0x04002B2D RID: 11053
		internal const string HttpGetBindingConfiguration = "httpGetBindingConfiguration";

		// Token: 0x04002B2E RID: 11054
		internal const string HttpsGetBinding = "httpsGetBinding";

		// Token: 0x04002B2F RID: 11055
		internal const string HttpsGetBindingConfiguration = "httpsGetBindingConfiguration";

		// Token: 0x04002B30 RID: 11056
		internal const string MexHttpBindingCollectionElementName = "mexHttpBinding";

		// Token: 0x04002B31 RID: 11057
		internal const string HttpsTransportSectionName = "httpsTransport";

		// Token: 0x04002B32 RID: 11058
		internal const string HttpTransportSectionName = "httpTransport";

		// Token: 0x04002B33 RID: 11059
		internal const string MexHttpsBindingCollectionElementName = "mexHttpsBinding";

		// Token: 0x04002B34 RID: 11060
		internal const string ID = "ID";

		// Token: 0x04002B35 RID: 11061
		internal const string Identity = "identity";

		// Token: 0x04002B36 RID: 11062
		internal const string IdentityConfiguration = "identityConfiguration";

		// Token: 0x04002B37 RID: 11063
		internal const string IdleTimeout = "idleTimeout";

		// Token: 0x04002B38 RID: 11064
		internal const string IgnoreExtensionDataObject = "ignoreExtensionDataObject";

		// Token: 0x04002B39 RID: 11065
		internal const string ImpersonateCallerForAllOperations = "impersonateCallerForAllOperations";

		// Token: 0x04002B3A RID: 11066
		internal const string ImpersonateOnSerializingReply = "impersonateOnSerializingReply";

		// Token: 0x04002B3B RID: 11067
		internal const string ImpersonationLevel = "impersonationLevel";

		// Token: 0x04002B3C RID: 11068
		internal const string InactivityTimeout = "inactivityTimeout";

		// Token: 0x04002B3D RID: 11069
		internal const string IncludeExceptionDetailInFaults = "includeExceptionDetailInFaults";

		// Token: 0x04002B3E RID: 11070
		internal const string IncludeTimestamp = "includeTimestamp";

		// Token: 0x04002B3F RID: 11071
		internal const string IncludeWindowsGroups = "includeWindowsGroups";

		// Token: 0x04002B40 RID: 11072
		internal const string IsChainIncluded = "isChainIncluded";

		// Token: 0x04002B41 RID: 11073
		internal const string IsOptional = "isOptional";

		// Token: 0x04002B42 RID: 11074
		internal const string IssuedCookieLifetime = "issuedCookieLifetime";

		// Token: 0x04002B43 RID: 11075
		internal const string IssuedKeyType = "issuedKeyType";

		// Token: 0x04002B44 RID: 11076
		internal const string IssuedToken = "issuedToken";

		// Token: 0x04002B45 RID: 11077
		internal const string IssuedTokenAuthentication = "issuedTokenAuthentication";

		// Token: 0x04002B46 RID: 11078
		internal const string IssuedTokenParameters = "issuedTokenParameters";

		// Token: 0x04002B47 RID: 11079
		internal const string IssuedTokenRenewalThresholdPercentage = "issuedTokenRenewalThresholdPercentage";

		// Token: 0x04002B48 RID: 11080
		internal const string IssuedTokenType = "issuedTokenType";

		// Token: 0x04002B49 RID: 11081
		internal const string Issuer = "issuer";

		// Token: 0x04002B4A RID: 11082
		internal const string IssuerAddress = "issuerAddress";

		// Token: 0x04002B4B RID: 11083
		internal const string IssuerChannelBehaviors = "issuerChannelBehaviors";

		// Token: 0x04002B4C RID: 11084
		internal const string IssuerMetadata = "issuerMetadata";

		// Token: 0x04002B4D RID: 11085
		internal const string IsSystemEndpoint = "isSystemEndpoint";

		// Token: 0x04002B4E RID: 11086
		internal const string KeepAliveEnabled = "keepAliveEnabled";

		// Token: 0x04002B4F RID: 11087
		internal const string KeepAliveInterval = "keepAliveInterval";

		// Token: 0x04002B50 RID: 11088
		internal const string KeyEntropyMode = "keyEntropyMode";

		// Token: 0x04002B51 RID: 11089
		internal const string KeySize = "keySize";

		// Token: 0x04002B52 RID: 11090
		internal const string KeyType = "keyType";

		// Token: 0x04002B53 RID: 11091
		internal const string Kind = "kind";

		// Token: 0x04002B54 RID: 11092
		internal const string KnownCertificates = "knownCertificates";

		// Token: 0x04002B55 RID: 11093
		internal const string LeaseTimeout = "leaseTimeout";

		// Token: 0x04002B56 RID: 11094
		internal const string ListenBacklog = "listenBacklog";

		// Token: 0x04002B57 RID: 11095
		internal const string ListenIPAddress = "listenIPAddress";

		// Token: 0x04002B58 RID: 11096
		internal const string ListenUri = "listenUri";

		// Token: 0x04002B59 RID: 11097
		internal const string ListenUriMode = "listenUriMode";

		// Token: 0x04002B5A RID: 11098
		internal const string LocalClientSettings = "localClientSettings";

		// Token: 0x04002B5B RID: 11099
		internal const string LocalIssuer = "localIssuer";

		// Token: 0x04002B5C RID: 11100
		internal const string LocalIssuerChannelBehaviors = "localIssuerChannelBehaviors";

		// Token: 0x04002B5D RID: 11101
		internal const string LocalServiceSettings = "localServiceSettings";

		// Token: 0x04002B5E RID: 11102
		internal const string LogEntireMessage = "logEntireMessage";

		// Token: 0x04002B5F RID: 11103
		internal const string LogKnownPii = "logKnownPii";

		// Token: 0x04002B60 RID: 11104
		internal const string LogMalformedMessages = "logMalformedMessages";

		// Token: 0x04002B61 RID: 11105
		internal const string LogMessagesAtServiceLevel = "logMessagesAtServiceLevel";

		// Token: 0x04002B62 RID: 11106
		internal const string LogMessagesAtTransportLevel = "logMessagesAtTransportLevel";

		// Token: 0x04002B63 RID: 11107
		internal const string ManualAddressing = "manualAddressing";

		// Token: 0x04002B64 RID: 11108
		internal const string MapClientCertificateToWindowsAccount = "mapClientCertificateToWindowsAccount";

		// Token: 0x04002B65 RID: 11109
		internal const string MaxAcceptedChannels = "maxAcceptedChannels";

		// Token: 0x04002B66 RID: 11110
		internal const string MaxArrayLength = "maxArrayLength";

		// Token: 0x04002B67 RID: 11111
		internal const string MaxBatchSize = "maxBatchSize";

		// Token: 0x04002B68 RID: 11112
		internal const string MaxBufferPoolSize = "maxBufferPoolSize";

		// Token: 0x04002B69 RID: 11113
		internal const string MaxBufferSize = "maxBufferSize";

		// Token: 0x04002B6A RID: 11114
		internal const string MaxBytesPerRead = "maxBytesPerRead";

		// Token: 0x04002B6B RID: 11115
		internal const string MaxCachedCookies = "maxCachedCookies";

		// Token: 0x04002B6C RID: 11116
		internal const string MaxCachedLogonTokens = "maxCachedLogonTokens";

		// Token: 0x04002B6D RID: 11117
		internal const string MaxClockSkew = "maxClockSkew";

		// Token: 0x04002B6E RID: 11118
		internal const string MaxConcurrentCalls = "maxConcurrentCalls";

		// Token: 0x04002B6F RID: 11119
		internal const string MaxConcurrentInstances = "maxConcurrentInstances";

		// Token: 0x04002B70 RID: 11120
		internal const string MaxConcurrentSessions = "maxConcurrentSessions";

		// Token: 0x04002B71 RID: 11121
		internal const string MaxConnections = "maxConnections";

		// Token: 0x04002B72 RID: 11122
		internal const string MaxCookieCachingTime = "maxCookieCachingTime";

		// Token: 0x04002B73 RID: 11123
		internal const string MaxDepth = "maxDepth";

		// Token: 0x04002B74 RID: 11124
		internal const string MaxIssuedTokenCachingTime = "maxIssuedTokenCachingTime";

		// Token: 0x04002B75 RID: 11125
		internal const string MaxItemsInObjectGraph = "maxItemsInObjectGraph";

		// Token: 0x04002B76 RID: 11126
		internal const string MaxMessagesToLog = "maxMessagesToLog";

		// Token: 0x04002B77 RID: 11127
		internal const string MaxNameTableCharCount = "maxNameTableCharCount";

		// Token: 0x04002B78 RID: 11128
		internal const string MaxOutboundChannelsPerEndpoint = "maxOutboundChannelsPerEndpoint";

		// Token: 0x04002B79 RID: 11129
		internal const string MaxOutboundConnectionsPerEndpoint = "maxOutboundConnectionsPerEndpoint";

		// Token: 0x04002B7A RID: 11130
		internal const string MaxOutputDelay = "maxOutputDelay";

		// Token: 0x04002B7B RID: 11131
		internal const string MaxPendingAccepts = "maxPendingAccepts";

		// Token: 0x04002B7C RID: 11132
		internal const string MaxPendingChannels = "maxPendingChannels";

		// Token: 0x04002B7D RID: 11133
		internal const string MaxPendingConnections = "maxPendingConnections";

		// Token: 0x04002B7E RID: 11134
		internal const string MaxPendingReceives = "maxPendingReceives";

		// Token: 0x04002B7F RID: 11135
		internal const string MaxPendingSessions = "maxPendingSessions";

		// Token: 0x04002B80 RID: 11136
		internal const string MaxPoolSize = "maxPoolSize";

		// Token: 0x04002B81 RID: 11137
		internal const string MaxReadPoolSize = "maxReadPoolSize";

		// Token: 0x04002B82 RID: 11138
		internal const string MaxReceivedMessageSize = "maxReceivedMessageSize";

		// Token: 0x04002B83 RID: 11139
		internal const string MaxRetryCount = "maxRetryCount";

		// Token: 0x04002B84 RID: 11140
		internal const string MaxRetryCycles = "maxRetryCycles";

		// Token: 0x04002B85 RID: 11141
		internal const string MaxSessionSize = "maxSessionSize";

		// Token: 0x04002B86 RID: 11142
		internal const string MaxSizeOfMessageToLog = "maxSizeOfMessageToLog";

		// Token: 0x04002B87 RID: 11143
		internal const string MaxStatefulNegotiations = "maxStatefulNegotiations";

		// Token: 0x04002B88 RID: 11144
		internal const string MaxStringContentLength = "maxStringContentLength";

		// Token: 0x04002B89 RID: 11145
		internal const string MaxTransferWindowSize = "maxTransferWindowSize";

		// Token: 0x04002B8A RID: 11146
		internal const string MaxWritePoolSize = "maxWritePoolSize";

		// Token: 0x04002B8B RID: 11147
		internal const string MembershipProviderName = "membershipProviderName";

		// Token: 0x04002B8C RID: 11148
		internal const string Message = "message";

		// Token: 0x04002B8D RID: 11149
		internal const string MessageAuthenticationAuditLevel = "messageAuthenticationAuditLevel";

		// Token: 0x04002B8E RID: 11150
		internal const string MessageEncoding = "messageEncoding";

		// Token: 0x04002B8F RID: 11151
		internal const string MessageFlowTracing = "messageFlowTracing";

		// Token: 0x04002B90 RID: 11152
		internal const string MessageHandlerFactory = "messageHandlerFactory";

		// Token: 0x04002B91 RID: 11153
		internal const string MessageLogging = "messageLogging";

		// Token: 0x04002B92 RID: 11154
		internal const string MessageProtectionOrder = "messageProtectionOrder";

		// Token: 0x04002B93 RID: 11155
		internal const string MessageSecurityVersion = "messageSecurityVersion";

		// Token: 0x04002B94 RID: 11156
		internal const string MessageSenderAuthentication = "messageSenderAuthentication";

		// Token: 0x04002B95 RID: 11157
		internal const string MessageVersion = "messageVersion";

		// Token: 0x04002B96 RID: 11158
		internal const string Metadata = "metadata";

		// Token: 0x04002B97 RID: 11159
		internal const string MinFreeMemoryPercentageToActivateService = "minFreeMemoryPercentageToActivateService";

		// Token: 0x04002B98 RID: 11160
		internal const string Mode = "mode";

		// Token: 0x04002B99 RID: 11161
		internal const string MsmqAuthenticationMode = "msmqAuthenticationMode";

		// Token: 0x04002B9A RID: 11162
		internal const string MsmqEncryptionAlgorithm = "msmqEncryptionAlgorithm";

		// Token: 0x04002B9B RID: 11163
		internal const string MsmqIntegrationBindingCollectionElementName = "msmqIntegrationBinding";

		// Token: 0x04002B9C RID: 11164
		internal const string MsmqIntegrationSectionName = "msmqIntegration";

		// Token: 0x04002B9D RID: 11165
		internal const string MsmqProtectionLevel = "msmqProtectionLevel";

		// Token: 0x04002B9E RID: 11166
		internal const string MsmqSecureHashAlgorithm = "msmqSecureHashAlgorithm";

		// Token: 0x04002B9F RID: 11167
		internal const string MsmqTransportSectionName = "msmqTransport";

		// Token: 0x04002BA0 RID: 11168
		internal const string MsmqTransportSecurity = "msmqTransportSecurity";

		// Token: 0x04002BA1 RID: 11169
		internal const string MtomMessageEncodingSectionName = "mtomMessageEncoding";

		// Token: 0x04002BA2 RID: 11170
		internal const string MultipleSiteBindingsEnabled = "multipleSiteBindingsEnabled";

		// Token: 0x04002BA3 RID: 11171
		internal const string Name = "name";

		// Token: 0x04002BA4 RID: 11172
		internal const string NamedPipeTransportSectionName = "namedPipeTransport";

		// Token: 0x04002BA5 RID: 11173
		internal const string NegotiateServiceCredential = "negotiateServiceCredential";

		// Token: 0x04002BA6 RID: 11174
		internal const string NegotiationTimeout = "negotiationTimeout";

		// Token: 0x04002BA7 RID: 11175
		internal const string NetMsmqBindingCollectionElementName = "netMsmqBinding";

		// Token: 0x04002BA8 RID: 11176
		internal const string NetNamedPipeBindingCollectionElementName = "netNamedPipeBinding";

		// Token: 0x04002BA9 RID: 11177
		internal const string MexNamedPipeBindingCollectionElementName = "mexNamedPipeBinding";

		// Token: 0x04002BAA RID: 11178
		internal const string NetPeerTcpBindingCollectionElementName = "netPeerTcpBinding";

		// Token: 0x04002BAB RID: 11179
		internal const string NetTcpBindingCollectionElementName = "netTcpBinding";

		// Token: 0x04002BAC RID: 11180
		internal const string NetHttpBindingCollectionElementName = "netHttpBinding";

		// Token: 0x04002BAD RID: 11181
		internal const string NetHttpsBindingCollectionElementName = "netHttpsBinding";

		// Token: 0x04002BAE RID: 11182
		internal const string NodeQuota = "nodeQuota";

		// Token: 0x04002BAF RID: 11183
		internal const string None = "None";

		// Token: 0x04002BB0 RID: 11184
		internal const string OleTransactions = "OleTransactions";

		// Token: 0x04002BB1 RID: 11185
		internal const string OneWaySectionName = "oneWay";

		// Token: 0x04002BB2 RID: 11186
		internal const string MexTcpBindingCollectionElementName = "mexTcpBinding";

		// Token: 0x04002BB3 RID: 11187
		internal const string MexStandardEndpointCollectionElementName = "mexEndpoint";

		// Token: 0x04002BB4 RID: 11188
		internal const string OpenTimeout = "openTimeout";

		// Token: 0x04002BB5 RID: 11189
		internal const string Ordered = "ordered";

		// Token: 0x04002BB6 RID: 11190
		internal const string PackageFullName = "packageFullName";

		// Token: 0x04002BB7 RID: 11191
		internal const string PacketRoutable = "packetRoutable";

		// Token: 0x04002BB8 RID: 11192
		internal const string Peer = "peer";

		// Token: 0x04002BB9 RID: 11193
		internal const string PeerAuthentication = "peerAuthentication";

		// Token: 0x04002BBA RID: 11194
		internal const string PeerResolver = "resolver";

		// Token: 0x04002BBB RID: 11195
		internal const string PeerResolverType = "resolverType";

		// Token: 0x04002BBC RID: 11196
		internal const string PeerTransportCredentialType = "credentialType";

		// Token: 0x04002BBD RID: 11197
		internal const string PeerTransportSectionName = "peerTransport";

		// Token: 0x04002BBE RID: 11198
		internal const string PerformanceCounters = "performanceCounters";

		// Token: 0x04002BBF RID: 11199
		internal const string PipeSettings = "pipeSettings";

		// Token: 0x04002BC0 RID: 11200
		internal const string PnrpPeerResolverSectionName = "pnrpPeerResolver";

		// Token: 0x04002BC1 RID: 11201
		internal const string Policy12 = "Policy12";

		// Token: 0x04002BC2 RID: 11202
		internal const string Policy15 = "Policy15";

		// Token: 0x04002BC3 RID: 11203
		internal const string PolicyImporters = "policyImporters";

		// Token: 0x04002BC4 RID: 11204
		internal const string PolicyType = "policyType";

		// Token: 0x04002BC5 RID: 11205
		internal const string PolicyVersion = "policyVersion";

		// Token: 0x04002BC6 RID: 11206
		internal const string Port = "port";

		// Token: 0x04002BC7 RID: 11207
		internal const string PortSharingEnabled = "portSharingEnabled";

		// Token: 0x04002BC8 RID: 11208
		internal const string Prefix = "prefix";

		// Token: 0x04002BC9 RID: 11209
		internal const string PrincipalPermissionMode = "principalPermissionMode";

		// Token: 0x04002BCA RID: 11210
		internal const string PrivacyNoticeAt = "privacyNoticeAt";

		// Token: 0x04002BCB RID: 11211
		internal const string PrivacyNoticeSectionName = "privacyNoticeAt";

		// Token: 0x04002BCC RID: 11212
		internal const string PrivacyNoticeVersion = "privacyNoticeVersion";

		// Token: 0x04002BCD RID: 11213
		internal const string PropagateActivity = "propagateActivity";

		// Token: 0x04002BCE RID: 11214
		internal const string ProtectionLevel = "protectionLevel";

		// Token: 0x04002BCF RID: 11215
		internal const string ProtectTokens = "protectTokens";

		// Token: 0x04002BD0 RID: 11216
		internal const string ProtocolMappingSectionName = "protocolMapping";

		// Token: 0x04002BD1 RID: 11217
		internal const string ProxyAddress = "proxyAddress";

		// Token: 0x04002BD2 RID: 11218
		internal const string ProxyAuthenticationScheme = "proxyAuthenticationScheme";

		// Token: 0x04002BD3 RID: 11219
		internal const string ProxyCredentialType = "proxyCredentialType";

		// Token: 0x04002BD4 RID: 11220
		internal const string QueueTransferProtocol = "queueTransferProtocol";

		// Token: 0x04002BD5 RID: 11221
		internal const string ReaderQuotas = "readerQuotas";

		// Token: 0x04002BD6 RID: 11222
		internal const string Realm = "realm";

		// Token: 0x04002BD7 RID: 11223
		internal const string ReceiveContextEnabled = "receiveContextEnabled";

		// Token: 0x04002BD8 RID: 11224
		internal const string ReceiveErrorHandling = "receiveErrorHandling";

		// Token: 0x04002BD9 RID: 11225
		internal const string ReceiveRetryCount = "receiveRetryCount";

		// Token: 0x04002BDA RID: 11226
		internal const string ReceiveTimeout = "receiveTimeout";

		// Token: 0x04002BDB RID: 11227
		internal const string ReconnectTransportOnFailure = "reconnectTransportOnFailure";

		// Token: 0x04002BDC RID: 11228
		internal const string ReferralPolicy = "referralPolicy";

		// Token: 0x04002BDD RID: 11229
		internal const string ReliableMessagingVersion = "reliableMessagingVersion";

		// Token: 0x04002BDE RID: 11230
		internal const string RelativeAddress = "relativeAddress";

		// Token: 0x04002BDF RID: 11231
		internal const string ReliableSession = "reliableSession";

		// Token: 0x04002BE0 RID: 11232
		internal const string ReliableSessionSectionName = "reliableSession";

		// Token: 0x04002BE1 RID: 11233
		internal const string Remove = "remove";

		// Token: 0x04002BE2 RID: 11234
		internal const string ReplayCacheSize = "replayCacheSize";

		// Token: 0x04002BE3 RID: 11235
		internal const string ReplayWindow = "replayWindow";

		// Token: 0x04002BE4 RID: 11236
		internal const string RequestInitializationTimeout = "requestInitializationTimeout";

		// Token: 0x04002BE5 RID: 11237
		internal const string RequireClientCertificate = "requireClientCertificate";

		// Token: 0x04002BE6 RID: 11238
		internal const string RequireDerivedKeys = "requireDerivedKeys";

		// Token: 0x04002BE7 RID: 11239
		internal const string RequireSecurityContextCancellation = "requireSecurityContextCancellation";

		// Token: 0x04002BE8 RID: 11240
		internal const string RequireSignatureConfirmation = "requireSignatureConfirmation";

		// Token: 0x04002BE9 RID: 11241
		internal const string RetryCycleDelay = "retryCycleDelay";

		// Token: 0x04002BEA RID: 11242
		internal const string RevocationMode = "revocationMode";

		// Token: 0x04002BEB RID: 11243
		internal const string RoleProviderName = "roleProviderName";

		// Token: 0x04002BEC RID: 11244
		internal const string Rsa = "rsa";

		// Token: 0x04002BED RID: 11245
		internal const string SamlSerializerType = "samlSerializerType";

		// Token: 0x04002BEE RID: 11246
		internal const string Scheme = "scheme";

		// Token: 0x04002BEF RID: 11247
		internal const string ScopedCertificates = "scopedCertificates";

		// Token: 0x04002BF0 RID: 11248
		internal const string SectionGroupName = "system.serviceModel";

		// Token: 0x04002BF1 RID: 11249
		internal const string SecureConversationAuthentication = "secureConversationAuthentication";

		// Token: 0x04002BF2 RID: 11250
		internal const string SecureConversationBootstrap = "secureConversationBootstrap";

		// Token: 0x04002BF3 RID: 11251
		internal const string Security = "security";

		// Token: 0x04002BF4 RID: 11252
		internal const string SecurityHeaderLayout = "securityHeaderLayout";

		// Token: 0x04002BF5 RID: 11253
		internal const string SecuritySectionName = "security";

		// Token: 0x04002BF6 RID: 11254
		internal const string SecurityStateEncoderType = "securityStateEncoderType";

		// Token: 0x04002BF7 RID: 11255
		internal const string SendTimeout = "sendTimeout";

		// Token: 0x04002BF8 RID: 11256
		internal const string SerializationFormat = "serializationFormat";

		// Token: 0x04002BF9 RID: 11257
		internal const string Service = "service";

		// Token: 0x04002BFA RID: 11258
		internal const string ServiceActivations = "serviceActivations";

		// Token: 0x04002BFB RID: 11259
		internal const string ServiceAuthenticationManagerSectionName = "serviceAuthenticationManager";

		// Token: 0x04002BFC RID: 11260
		internal const string ServiceAuthenticationManagerType = "serviceAuthenticationManagerType";

		// Token: 0x04002BFD RID: 11261
		internal const string ServiceAuthorizationAuditLevel = "serviceAuthorizationAuditLevel";

		// Token: 0x04002BFE RID: 11262
		internal const string ServiceAuthorizationManagerType = "serviceAuthorizationManagerType";

		// Token: 0x04002BFF RID: 11263
		internal const string ServiceAuthorizationSectionName = "serviceAuthorization";

		// Token: 0x04002C00 RID: 11264
		internal const string ServiceBehaviors = "serviceBehaviors";

		// Token: 0x04002C01 RID: 11265
		internal const string ServiceCertificate = "serviceCertificate";

		// Token: 0x04002C02 RID: 11266
		internal const string ServiceCredentials = "serviceCredentials";

		// Token: 0x04002C03 RID: 11267
		internal const string ServiceDebugSectionName = "serviceDebug";

		// Token: 0x04002C04 RID: 11268
		internal const string ServiceHealthSectionName = "serviceHealth";

		// Token: 0x04002C05 RID: 11269
		internal const string ServiceHostingEnvironmentSectionName = "serviceHostingEnvironment";

		// Token: 0x04002C06 RID: 11270
		internal const string ServiceMetadataPublishingSectionName = "serviceMetadata";

		// Token: 0x04002C07 RID: 11271
		internal const string ServicePrincipalName = "servicePrincipalName";

		// Token: 0x04002C08 RID: 11272
		internal const string ServiceSecurityAuditSectionName = "serviceSecurityAudit";

		// Token: 0x04002C09 RID: 11273
		internal const string ServicesSectionName = "services";

		// Token: 0x04002C0A RID: 11274
		internal const string ServiceThrottlingSectionName = "serviceThrottling";

		// Token: 0x04002C0B RID: 11275
		internal const string ServiceTimeouts = "serviceTimeouts";

		// Token: 0x04002C0C RID: 11276
		internal const string Session = "Session";

		// Token: 0x04002C0D RID: 11277
		internal const string SessionIdAttribute = "sessionId";

		// Token: 0x04002C0E RID: 11278
		internal const string SessionKeyRenewalInterval = "sessionKeyRenewalInterval";

		// Token: 0x04002C0F RID: 11279
		internal const string SessionKeyRolloverInterval = "sessionKeyRolloverInterval";

		// Token: 0x04002C10 RID: 11280
		internal const string Soap11 = "Soap11";

		// Token: 0x04002C11 RID: 11281
		internal const string Soap11WSAddressing10 = "Soap11WSAddressing10";

		// Token: 0x04002C12 RID: 11282
		internal const string Soap11WSAddressingAugust2004 = "Soap11WSAddressingAugust2004";

		// Token: 0x04002C13 RID: 11283
		internal const string Soap12 = "Soap12";

		// Token: 0x04002C14 RID: 11284
		internal const string Soap12WSAddressing10 = "Soap12WSAddressing10";

		// Token: 0x04002C15 RID: 11285
		internal const string Soap12WSAddressingAugust2004 = "Soap12WSAddressingAugust2004";

		// Token: 0x04002C16 RID: 11286
		internal const string SslCertificateAuthentication = "sslCertificateAuthentication";

		// Token: 0x04002C17 RID: 11287
		internal const string SslProtocols = "sslProtocols";

		// Token: 0x04002C18 RID: 11288
		internal const string SslStreamSecuritySectionName = "sslStreamSecurity";

		// Token: 0x04002C19 RID: 11289
		internal const string StandardEndpoint = "standardEndpoint";

		// Token: 0x04002C1A RID: 11290
		internal const string StandardEndpointsSectionName = "standardEndpoints";

		// Token: 0x04002C1B RID: 11291
		internal const string StoreLocation = "storeLocation";

		// Token: 0x04002C1C RID: 11292
		internal const string StoreName = "storeName";

		// Token: 0x04002C1D RID: 11293
		internal const string SubProtocol = "subProtocol";

		// Token: 0x04002C1E RID: 11294
		internal const string SupportInteractive = "supportInteractive";

		// Token: 0x04002C1F RID: 11295
		internal const string SuppressAuditFailure = "suppressAuditFailure";

		// Token: 0x04002C20 RID: 11296
		internal const string SynchronousReceiveSectionName = "synchronousReceive";

		// Token: 0x04002C21 RID: 11297
		internal const string DispatcherSynchronizationSectionName = "dispatcherSynchronization";

		// Token: 0x04002C22 RID: 11298
		internal const string TargetUri = "targetUri";

		// Token: 0x04002C23 RID: 11299
		internal const string TcpTransportSectionName = "tcpTransport";

		// Token: 0x04002C24 RID: 11300
		internal const string TeredoEnabled = "teredoEnabled";

		// Token: 0x04002C25 RID: 11301
		internal const string TextEncoding = "textEncoding";

		// Token: 0x04002C26 RID: 11302
		internal const string TextMessageEncodingSectionName = "textMessageEncoding";

		// Token: 0x04002C27 RID: 11303
		internal const string Timeouts = "timeouts";

		// Token: 0x04002C28 RID: 11304
		internal const string TimeSpanInfinite = "-00:00:00.001";

		// Token: 0x04002C29 RID: 11305
		internal const string TimeSpanOneTick = "00:00:00.0000001";

		// Token: 0x04002C2A RID: 11306
		internal const string TimeSpanZero = "00:00:00";

		// Token: 0x04002C2B RID: 11307
		internal const string TimestampValidityDuration = "timestampValidityDuration";

		// Token: 0x04002C2C RID: 11308
		internal const string TimeToLive = "timeToLive";

		// Token: 0x04002C2D RID: 11309
		internal const string TokenRequestParameters = "tokenRequestParameters";

		// Token: 0x04002C2E RID: 11310
		internal const string TokenType = "tokenType";

		// Token: 0x04002C2F RID: 11311
		internal const string TransactedBatchingSectionName = "transactedBatching";

		// Token: 0x04002C30 RID: 11312
		internal const string TransactionFlow = "transactionFlow";

		// Token: 0x04002C31 RID: 11313
		internal const string TransactionFlowSectionName = "transactionFlow";

		// Token: 0x04002C32 RID: 11314
		internal const string TransactionProtocol = "transactionProtocol";

		// Token: 0x04002C33 RID: 11315
		internal const string TransactionTimeout = "transactionTimeout";

		// Token: 0x04002C34 RID: 11316
		internal const string TransactionAllowWildcardAction = "allowWildcardAction";

		// Token: 0x04002C35 RID: 11317
		internal const string TransferMode = "transferMode";

		// Token: 0x04002C36 RID: 11318
		internal const string Transport = "transport";

		// Token: 0x04002C37 RID: 11319
		internal const string TransportConfigurationType = "transportConfigurationType";

		// Token: 0x04002C38 RID: 11320
		internal const string TransportUsage = "transportUsage";

		// Token: 0x04002C39 RID: 11321
		internal const string TripleDes = "TripleDes";

		// Token: 0x04002C3A RID: 11322
		internal const string TripleDesRsa15 = "TripleDesRsa15";

		// Token: 0x04002C3B RID: 11323
		internal const string TripleDesSha256 = "TripleDesSha256";

		// Token: 0x04002C3C RID: 11324
		internal const string TripleDesSha256Rsa15 = "TripleDesSha256Rsa15";

		// Token: 0x04002C3D RID: 11325
		internal const string TrustedStoreLocation = "trustedStoreLocation";

		// Token: 0x04002C3E RID: 11326
		internal const string Type = "type";

		// Token: 0x04002C3F RID: 11327
		internal const string TypeDefID = "typeDefID";

		// Token: 0x04002C40 RID: 11328
		internal const string TypeLibID = "typeLibID";

		// Token: 0x04002C41 RID: 11329
		internal const string TypeLibVersion = "typeLibVersion";

		// Token: 0x04002C42 RID: 11330
		internal const string UdpBindingCollectionElementName = "udpBinding";

		// Token: 0x04002C43 RID: 11331
		internal const string UdpBindingCollectionElementType = "System.ServiceModel.Configuration.UdpBindingCollectionElement, System.ServiceModel.Channels, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04002C44 RID: 11332
		internal const string UdpTransportElementType = "System.ServiceModel.Configuration.UdpTransportElement, System.ServiceModel.Channels, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04002C45 RID: 11333
		internal const string UdpTransportImporterType = "System.ServiceModel.Channels.UdpTransportImporter, System.ServiceModel.Channels, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04002C46 RID: 11334
		internal const string UdpTransportSectionName = "udpTransport";

		// Token: 0x04002C47 RID: 11335
		internal const string UnrecognizedPolicyAssertionSectionName = "unrecognizedPolicyAssertions";

		// Token: 0x04002C48 RID: 11336
		internal const string UnsafeConnectionNtlmAuthentication = "unsafeConnectionNtlmAuthentication";

		// Token: 0x04002C49 RID: 11337
		internal const string Url = "url";

		// Token: 0x04002C4A RID: 11338
		internal const string UseActiveDirectory = "useActiveDirectory";

		// Token: 0x04002C4B RID: 11339
		internal const string UseDefaultWebProxy = "useDefaultWebProxy";

		// Token: 0x04002C4C RID: 11340
		internal const string UseIdentityConfiguration = "useIdentityConfiguration";

		// Token: 0x04002C4D RID: 11341
		internal const string UseManagedPresentationSectionName = "useManagedPresentation";

		// Token: 0x04002C4E RID: 11342
		internal const string UseMsmqTracing = "useMsmqTracing";

		// Token: 0x04002C4F RID: 11343
		internal const string UserNameAuthentication = "userNameAuthentication";

		// Token: 0x04002C50 RID: 11344
		internal const string UserNamePasswordValidationMode = "userNamePasswordValidationMode";

		// Token: 0x04002C51 RID: 11345
		internal const string UserPrincipalName = "userPrincipalName";

		// Token: 0x04002C52 RID: 11346
		internal const string UseRequestHeadersForMetadataAddress = "useRequestHeadersForMetadataAddress";

		// Token: 0x04002C53 RID: 11347
		internal const string UseSourceJournal = "useSourceJournal";

		// Token: 0x04002C54 RID: 11348
		internal const string UseStrTransform = "useStrTransform";

		// Token: 0x04002C55 RID: 11349
		internal const string ValidityDuration = "validityDuration";

		// Token: 0x04002C56 RID: 11350
		internal const string Value = "value";

		// Token: 0x04002C57 RID: 11351
		internal const string Version = "version";

		// Token: 0x04002C58 RID: 11352
		internal const string ViaUri = "viaUri";

		// Token: 0x04002C59 RID: 11353
		internal const string WebSocketSettingsSectionName = "webSocketSettings";

		// Token: 0x04002C5A RID: 11354
		internal const string Windows = "windows";

		// Token: 0x04002C5B RID: 11355
		internal const string WindowsAuthentication = "windowsAuthentication";

		// Token: 0x04002C5C RID: 11356
		internal const string WindowsStreamSecuritySectionName = "windowsStreamSecurity";

		// Token: 0x04002C5D RID: 11357
		internal const string WmiProviderEnabled = "wmiProviderEnabled";

		// Token: 0x04002C5E RID: 11358
		internal const string WriteEncoding = "writeEncoding";

		// Token: 0x04002C5F RID: 11359
		internal const string WSAtomicTransactionOctober2004 = "WSAtomicTransactionOctober2004";

		// Token: 0x04002C60 RID: 11360
		internal const string WSAtomicTransaction11 = "WSAtomicTransaction11";

		// Token: 0x04002C61 RID: 11361
		internal const string WsdlImporters = "wsdlImporters";

		// Token: 0x04002C62 RID: 11362
		internal const string WSDualHttpBindingCollectionElementName = "wsDualHttpBinding";

		// Token: 0x04002C63 RID: 11363
		internal const string WSFederationHttpBindingCollectionElementName = "wsFederationHttpBinding";

		// Token: 0x04002C64 RID: 11364
		internal const string WS2007FederationHttpBindingCollectionElementName = "ws2007FederationHttpBinding";

		// Token: 0x04002C65 RID: 11365
		internal const string WS2007HttpBindingCollectionElementName = "ws2007HttpBinding";

		// Token: 0x04002C66 RID: 11366
		internal const string WSHttpBindingCollectionElementName = "wsHttpBinding";

		// Token: 0x04002C67 RID: 11367
		internal const string WSReliableMessaging11 = "WSReliableMessaging11";

		// Token: 0x04002C68 RID: 11368
		internal const string WSReliableMessagingFebruary2005 = "WSReliableMessagingFebruary2005";

		// Token: 0x04002C69 RID: 11369
		internal const string WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10 = "WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10";

		// Token: 0x04002C6A RID: 11370
		internal const string WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11 = "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11";

		// Token: 0x04002C6B RID: 11371
		internal const string WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10 = "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10";

		// Token: 0x04002C6C RID: 11372
		internal const string WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10 = "WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10";

		// Token: 0x04002C6D RID: 11373
		internal const string WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12 = "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12";

		// Token: 0x04002C6E RID: 11374
		internal const string WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10 = "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10";

		// Token: 0x04002C6F RID: 11375
		internal const string X509FindType = "x509FindType";

		// Token: 0x04002C70 RID: 11376
		internal const string XmlElement = "xmlElement";
	}
}

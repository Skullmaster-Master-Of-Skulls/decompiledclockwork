using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200068D RID: 1677
	public class ExtensionsSection : ConfigurationSection
	{
		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x000F5F44 File Offset: 0x000F4144
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("behaviorExtensions", typeof(ExtensionElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("bindingElementExtensions", typeof(ExtensionElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("bindingExtensions", typeof(ExtensionElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("endpointExtensions", typeof(ExtensionElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x000F5FE7 File Offset: 0x000F41E7
		[ConfigurationProperty("behaviorExtensions")]
		public ExtensionElementCollection BehaviorExtensions
		{
			get
			{
				return (ExtensionElementCollection)base["behaviorExtensions"];
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x000F5FF9 File Offset: 0x000F41F9
		[ConfigurationProperty("bindingElementExtensions")]
		public ExtensionElementCollection BindingElementExtensions
		{
			get
			{
				return (ExtensionElementCollection)base["bindingElementExtensions"];
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x000F600B File Offset: 0x000F420B
		[ConfigurationProperty("bindingExtensions")]
		public ExtensionElementCollection BindingExtensions
		{
			get
			{
				return (ExtensionElementCollection)base["bindingExtensions"];
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x000F601D File Offset: 0x000F421D
		[ConfigurationProperty("endpointExtensions")]
		public ExtensionElementCollection EndpointExtensions
		{
			get
			{
				return (ExtensionElementCollection)base["endpointExtensions"];
			}
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x000F6030 File Offset: 0x000F4230
		private void InitializeBehaviorElementExtensions()
		{
			this.BehaviorExtensions.Add(new ExtensionElement("clientCredentials", typeof(ClientCredentialsElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceCredentials", typeof(ServiceCredentialsElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("callbackDebug", typeof(CallbackDebugElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("clientVia", typeof(ClientViaElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("synchronousReceive", typeof(SynchronousReceiveElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("dispatcherSynchronization", typeof(DispatcherSynchronizationElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceMetadata", typeof(ServiceMetadataPublishingElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceDebug", typeof(ServiceDebugElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceHealth", typeof(ServiceHealthElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceAuthenticationManager", typeof(ServiceAuthenticationElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceAuthorization", typeof(ServiceAuthorizationElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceSecurityAudit", typeof(ServiceSecurityAuditElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceThrottling", typeof(ServiceThrottlingElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("transactedBatching", typeof(TransactedBatchingElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("dataContractSerializer", typeof(DataContractSerializerElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("serviceTimeouts", typeof(ServiceTimeoutsElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("callbackTimeouts", typeof(CallbackTimeoutsElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("useRequestHeadersForMetadataAddress", typeof(UseRequestHeadersForMetadataAddressElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("clear", typeof(ClearBehaviorElement).AssemblyQualifiedName));
			this.BehaviorExtensions.Add(new ExtensionElement("remove", typeof(RemoveBehaviorElement).AssemblyQualifiedName));
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x000F6310 File Offset: 0x000F4510
		private void InitializeBindingElementExtenions()
		{
			this.BindingElementExtensions.Add(new ExtensionElement("binaryMessageEncoding", typeof(BinaryMessageEncodingElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("compositeDuplex", typeof(CompositeDuplexElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("oneWay", typeof(OneWayElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("transactionFlow", typeof(TransactionFlowElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("httpsTransport", typeof(HttpsTransportElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("httpTransport", typeof(HttpTransportElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("msmqIntegration", typeof(MsmqIntegrationElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("msmqTransport", typeof(MsmqTransportElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("mtomMessageEncoding", typeof(MtomMessageEncodingElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("namedPipeTransport", typeof(NamedPipeTransportElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("peerTransport", typeof(PeerTransportElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("pnrpPeerResolver", typeof(PnrpPeerResolverElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("privacyNoticeAt", typeof(PrivacyNoticeElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("reliableSession", typeof(ReliableSessionElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("security", typeof(SecurityElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("sslStreamSecurity", typeof(SslStreamSecurityElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("tcpTransport", typeof(TcpTransportElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("textMessageEncoding", typeof(TextMessageEncodingElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("unrecognizedPolicyAssertions", typeof(UnrecognizedPolicyAssertionElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("useManagedPresentation", typeof(UseManagedPresentationElement).AssemblyQualifiedName));
			this.BindingElementExtensions.Add(new ExtensionElement("windowsStreamSecurity", typeof(WindowsStreamSecurityElement).AssemblyQualifiedName));
			if (OSEnvironmentHelper.IsApplicationTargeting45)
			{
				this.BindingElementExtensions.Add(new ExtensionElement("udpTransport", "System.ServiceModel.Configuration.UdpTransportElement, System.ServiceModel.Channels, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
			}
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x000F6634 File Offset: 0x000F4834
		private void InitializeBindingExtensions()
		{
			this.BindingExtensions.Add(new ExtensionElement("basicHttpBinding", typeof(BasicHttpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("customBinding", typeof(CustomBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("msmqIntegrationBinding", typeof(MsmqIntegrationBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("netMsmqBinding", typeof(NetMsmqBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("netNamedPipeBinding", typeof(NetNamedPipeBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("netPeerTcpBinding", typeof(NetPeerTcpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("netTcpBinding", typeof(NetTcpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("wsDualHttpBinding", typeof(WSDualHttpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("wsFederationHttpBinding", typeof(WSFederationHttpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("ws2007FederationHttpBinding", typeof(WS2007FederationHttpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("wsHttpBinding", typeof(WSHttpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("ws2007HttpBinding", typeof(WS2007HttpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("mexHttpBinding", typeof(MexHttpBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("mexHttpsBinding", typeof(MexHttpsBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("mexNamedPipeBinding", typeof(MexNamedPipeBindingCollectionElement).AssemblyQualifiedName));
			this.BindingExtensions.Add(new ExtensionElement("mexTcpBinding", typeof(MexTcpBindingCollectionElement).AssemblyQualifiedName));
			if (OSEnvironmentHelper.IsApplicationTargeting45)
			{
				this.BindingExtensions.Add(new ExtensionElement("udpBinding", "System.ServiceModel.Configuration.UdpBindingCollectionElement, System.ServiceModel.Channels, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
				this.BindingExtensions.Add(new ExtensionElement("netHttpBinding", typeof(NetHttpBindingCollectionElement).AssemblyQualifiedName));
				this.BindingExtensions.Add(new ExtensionElement("netHttpsBinding", typeof(NetHttpsBindingCollectionElement).AssemblyQualifiedName));
				this.BindingExtensions.Add(new ExtensionElement("basicHttpsBinding", typeof(BasicHttpsBindingCollectionElement).AssemblyQualifiedName));
			}
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x000F6911 File Offset: 0x000F4B11
		private void InitializeEndpointExtensions()
		{
			this.EndpointExtensions.Add(new ExtensionElement("mexEndpoint", typeof(ServiceMetadataEndpointCollectionElement).AssemblyQualifiedName));
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x000F6937 File Offset: 0x000F4B37
		protected override void InitializeDefault()
		{
			this.InitializeBehaviorElementExtensions();
			this.InitializeBindingElementExtenions();
			this.InitializeBindingExtensions();
			this.InitializeEndpointExtensions();
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x000F6951 File Offset: 0x000F4B51
		internal static ExtensionElementCollection LookupAssociatedCollection(Type extensionType, ContextInformation evaluationContext, out string collectionName)
		{
			collectionName = ExtensionsSection.GetExtensionType(extensionType);
			return ExtensionsSection.LookupCollection(collectionName, evaluationContext);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x000F6963 File Offset: 0x000F4B63
		[SecurityCritical]
		internal static ExtensionElementCollection UnsafeLookupAssociatedCollection(Type extensionType, ContextInformation evaluationContext, out string collectionName)
		{
			collectionName = ExtensionsSection.GetExtensionType(extensionType);
			return ExtensionsSection.UnsafeLookupCollection(collectionName, evaluationContext);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000F6978 File Offset: 0x000F4B78
		private static string GetExtensionType(Type extensionType)
		{
			string result = string.Empty;
			if (extensionType.IsSubclassOf(typeof(BehaviorExtensionElement)))
			{
				result = "behaviorExtensions";
			}
			else if (extensionType.IsSubclassOf(typeof(BindingElementExtensionElement)))
			{
				result = "bindingElementExtensions";
			}
			else if (extensionType.IsSubclassOf(typeof(BindingCollectionElement)))
			{
				result = "bindingExtensions";
			}
			else if (extensionType.IsSubclassOf(typeof(EndpointCollectionElement)))
			{
				result = "endpointExtensions";
			}
			else
			{
				DiagnosticUtility.FailFast(string.Format(CultureInfo.InvariantCulture, "{0} is not a type supported by the ServiceModelExtensionsSection collections.", new object[]
				{
					extensionType.AssemblyQualifiedName
				}));
			}
			return result;
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x000F6A18 File Offset: 0x000F4C18
		internal static ExtensionElementCollection LookupCollection(string collectionName, ContextInformation evaluationContext)
		{
			ExtensionElementCollection result = null;
			ExtensionsSection extensionsSection;
			if (evaluationContext != null)
			{
				extensionsSection = (ExtensionsSection)ConfigurationHelpers.GetAssociatedSection(evaluationContext, ConfigurationStrings.ExtensionsSectionPath);
			}
			else
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524312, SR.GetString("TraceCodeEvaluationContextNotFound"), null, null);
				}
				extensionsSection = (ExtensionsSection)ConfigurationHelpers.GetSection(ConfigurationStrings.ExtensionsSectionPath);
			}
			if (!(collectionName == "behaviorExtensions"))
			{
				if (!(collectionName == "bindingElementExtensions"))
				{
					if (!(collectionName == "bindingExtensions"))
					{
						if (!(collectionName == "endpointExtensions"))
						{
							DiagnosticUtility.FailFast(string.Format(CultureInfo.InvariantCulture, "{0} is not a valid ServiceModelExtensionsSection collection name.", new object[]
							{
								collectionName
							}));
						}
						else
						{
							result = extensionsSection.EndpointExtensions;
						}
					}
					else
					{
						result = extensionsSection.BindingExtensions;
					}
				}
				else
				{
					result = extensionsSection.BindingElementExtensions;
				}
			}
			else
			{
				result = extensionsSection.BehaviorExtensions;
			}
			return result;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000F6AE8 File Offset: 0x000F4CE8
		[SecurityCritical]
		internal static ExtensionElementCollection UnsafeLookupCollection(string collectionName, ContextInformation evaluationContext)
		{
			ExtensionElementCollection result = null;
			ExtensionsSection extensionsSection;
			if (evaluationContext != null)
			{
				extensionsSection = (ExtensionsSection)ConfigurationHelpers.UnsafeGetAssociatedSection(evaluationContext, ConfigurationStrings.ExtensionsSectionPath);
			}
			else
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524312, SR.GetString("TraceCodeEvaluationContextNotFound"), null, null);
				}
				extensionsSection = (ExtensionsSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.ExtensionsSectionPath);
			}
			if (!(collectionName == "behaviorExtensions"))
			{
				if (!(collectionName == "bindingElementExtensions"))
				{
					if (!(collectionName == "bindingExtensions"))
					{
						if (!(collectionName == "endpointExtensions"))
						{
							DiagnosticUtility.FailFast(string.Format(CultureInfo.InvariantCulture, "{0} is not a valid ServiceModelExtensionsSection collection name.", new object[]
							{
								collectionName
							}));
						}
						else
						{
							result = extensionsSection.EndpointExtensions;
						}
					}
					else
					{
						result = extensionsSection.BindingExtensions;
					}
				}
				else
				{
					result = extensionsSection.BindingElementExtensions;
				}
			}
			else
			{
				result = extensionsSection.BehaviorExtensions;
			}
			return result;
		}

		// Token: 0x04002CDA RID: 11482
		private ConfigurationPropertyCollection properties;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.Net;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.Text;

namespace System.ServiceModel.Description
{
	// Token: 0x020003FC RID: 1020
	internal class ConfigLoader
	{
		// Token: 0x060026C3 RID: 9923 RVA: 0x0008CCAF File Offset: 0x0008AEAF
		public ConfigLoader() : this(null)
		{
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x0008CCB8 File Offset: 0x0008AEB8
		public ConfigLoader(ContextInformation configurationContext) : this(null)
		{
			this.configurationContext = configurationContext;
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x0008CCC8 File Offset: 0x0008AEC8
		public ConfigLoader(IContractResolver contractResolver)
		{
			this.contractResolver = contractResolver;
			this.bindingTable = new Dictionary<string, Binding>();
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0008CCE4 File Offset: 0x0008AEE4
		[SecuritySafeCritical]
		internal static EndpointIdentity LoadIdentity(IdentityElement element)
		{
			EndpointIdentity result = null;
			PropertyInformationCollection properties = element.ElementInformation.Properties;
			if (properties["userPrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
			{
				result = EndpointIdentity.CreateUpnIdentity(element.UserPrincipalName.Value);
			}
			else if (properties["servicePrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
			{
				result = EndpointIdentity.CreateSpnIdentity(element.ServicePrincipalName.Value);
			}
			else if (properties["dns"].ValueOrigin != PropertyValueOrigin.Default)
			{
				result = EndpointIdentity.CreateDnsIdentity(element.Dns.Value);
			}
			else if (properties["rsa"].ValueOrigin != PropertyValueOrigin.Default)
			{
				result = EndpointIdentity.CreateRsaIdentity(element.Rsa.Value);
			}
			else if (properties["certificate"].ValueOrigin != PropertyValueOrigin.Default)
			{
				X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
				x509Certificate2Collection.Import(Convert.FromBase64String(element.Certificate.EncodedValue));
				if (x509Certificate2Collection.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToLoadCertificateIdentity")));
				}
				X509Certificate2 primaryCertificate = x509Certificate2Collection[0];
				x509Certificate2Collection.RemoveAt(0);
				result = EndpointIdentity.CreateX509CertificateIdentity(primaryCertificate, x509Certificate2Collection);
			}
			else if (properties["certificateReference"].ValueOrigin != PropertyValueOrigin.Default)
			{
				X509CertificateStore x509CertificateStore = new X509CertificateStore(element.CertificateReference.StoreName, element.CertificateReference.StoreLocation);
				X509Certificate2Collection x509Certificate2Collection2 = null;
				try
				{
					x509CertificateStore.Open(OpenFlags.ReadOnly);
					x509Certificate2Collection2 = x509CertificateStore.Find(element.CertificateReference.X509FindType, element.CertificateReference.FindValue, false);
					if (x509Certificate2Collection2.Count == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToLoadCertificateIdentity")));
					}
					X509Certificate2 certificate = new X509Certificate2(x509Certificate2Collection2[0]);
					if (element.CertificateReference.IsChainIncluded)
					{
						X509Chain x509Chain = new X509Chain();
						x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
						x509Chain.Build(certificate);
						result = EndpointIdentity.CreateX509CertificateIdentity(x509Chain);
					}
					else
					{
						result = EndpointIdentity.CreateX509CertificateIdentity(certificate);
					}
				}
				finally
				{
					SecurityUtils.ResetAllCertificates(x509Certificate2Collection2);
					x509CertificateStore.Close();
				}
			}
			return result;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0008CEFC File Offset: 0x0008B0FC
		[SecuritySafeCritical]
		internal void LoadChannelBehaviors(ServiceEndpoint serviceEndpoint, string configurationName)
		{
			bool flag = ConfigLoader.IsWildcardMatch(configurationName);
			ServiceEndpoint serviceEndpoint2;
			ChannelEndpointElement channelEndpointElement = ConfigLoader.LookupChannel(this.configurationContext, configurationName, serviceEndpoint.Contract, null, flag, false, out serviceEndpoint2);
			if (channelEndpointElement != null)
			{
				if (serviceEndpoint.Binding == null && !string.IsNullOrEmpty(channelEndpointElement.Binding))
				{
					serviceEndpoint.Binding = ConfigLoader.LookupBinding(channelEndpointElement.Binding, channelEndpointElement.BindingConfiguration, ConfigurationHelpers.GetEvaluationContext(channelEndpointElement));
				}
				if (serviceEndpoint.Address == null && channelEndpointElement.Address != null && channelEndpointElement.Address.OriginalString.Length > 0)
				{
					serviceEndpoint.Address = new EndpointAddress(channelEndpointElement.Address, ConfigLoader.LoadIdentity(channelEndpointElement.Identity), channelEndpointElement.Headers.Headers);
				}
				CommonBehaviorsSection commonBehaviorsSection = ConfigLoader.LookupCommonBehaviors(ConfigurationHelpers.GetEvaluationContext(channelEndpointElement));
				if (commonBehaviorsSection != null && commonBehaviorsSection.EndpointBehaviors != null)
				{
					ConfigLoader.LoadBehaviors<IEndpointBehavior>(commonBehaviorsSection.EndpointBehaviors, serviceEndpoint.Behaviors, true);
				}
				EndpointBehaviorElement endpointBehaviorElement = ConfigLoader.LookupEndpointBehaviors(channelEndpointElement.BehaviorConfiguration, ConfigurationHelpers.GetEvaluationContext(channelEndpointElement));
				if (endpointBehaviorElement != null)
				{
					ConfigLoader.LoadBehaviors<IEndpointBehavior>(endpointBehaviorElement, serviceEndpoint.Behaviors, false);
				}
				return;
			}
			if (flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxConfigContractNotFound", new object[]
				{
					serviceEndpoint.Contract.ConfigurationName
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxConfigChannelConfigurationNotFound", new object[]
			{
				configurationName,
				serviceEndpoint.Contract.ConfigurationName
			})));
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0008D064 File Offset: 0x0008B264
		[SecuritySafeCritical]
		internal void LoadCommonClientBehaviors(ServiceEndpoint serviceEndpoint)
		{
			CommonBehaviorsSection commonBehaviorsSection = ConfigLoader.LookupCommonBehaviors(this.configurationContext);
			if (commonBehaviorsSection != null && commonBehaviorsSection.EndpointBehaviors != null)
			{
				ConfigLoader.LoadBehaviors<IEndpointBehavior>(commonBehaviorsSection.EndpointBehaviors, serviceEndpoint.Behaviors, true);
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0008D09C File Offset: 0x0008B29C
		[SecuritySafeCritical]
		private static void LoadBehaviors<T>(ServiceModelExtensionCollectionElement<BehaviorExtensionElement> behaviorElement, KeyedByTypeCollection<T> behaviors, bool commonBehaviors)
		{
			bool? flag = null;
			KeyedByTypeCollection<T> keyedByTypeCollection = new KeyedByTypeCollection<T>();
			for (int i = 0; i < behaviorElement.Count; i++)
			{
				BehaviorExtensionElement behaviorExtensionElement = behaviorElement[i];
				object obj = behaviorExtensionElement.CreateBehavior();
				if (obj != null)
				{
					Type type = obj.GetType();
					if (!typeof(T).IsAssignableFrom(type))
					{
						ConfigLoader.TraceBehaviorWarning(behaviorExtensionElement, 524341, SR.GetString("TraceCodeSkipBehavior"), type, typeof(T));
					}
					else if (commonBehaviors && ConfigLoader.ShouldSkipCommonBehavior(type, ref flag))
					{
						ConfigLoader.TraceBehaviorWarning(behaviorExtensionElement, 524341, SR.GetString("TraceCodeSkipBehavior"), type, typeof(T));
					}
					else
					{
						keyedByTypeCollection.Add((T)((object)obj));
						if (behaviors.Contains(type))
						{
							ConfigLoader.TraceBehaviorWarning(behaviorExtensionElement, 524330, SR.GetString("TraceCodeRemoveBehavior"), type, typeof(T));
							behaviors.Remove(type);
						}
						behaviors.Add((T)((object)obj));
					}
				}
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0008D1A4 File Offset: 0x0008B3A4
		[SecurityCritical]
		private static bool ShouldSkipCommonBehavior(Type behaviorType, ref bool? isPT)
		{
			bool result = false;
			if (isPT == null)
			{
				if (!PartialTrustHelpers.IsTypeAptca(behaviorType))
				{
					isPT = new bool?(!ConfigLoader.ThreadHasConfigurationPermission());
					result = isPT.Value;
				}
			}
			else if (isPT.Value)
			{
				result = !PartialTrustHelpers.IsTypeAptca(behaviorType);
			}
			return result;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0008D1F4 File Offset: 0x0008B3F4
		[SecuritySafeCritical]
		private static void TraceBehaviorWarning(BehaviorExtensionElement behaviorExtension, int traceCode, string traceDescription, Type type, Type behaviorType)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				Hashtable dictionary = new Hashtable(3)
				{
					{
						"ConfigurationElementName",
						behaviorExtension.ConfigurationElementName
					},
					{
						"ConfigurationType",
						type.AssemblyQualifiedName
					},
					{
						"BehaviorType",
						behaviorType.AssemblyQualifiedName
					}
				};
				TraceUtility.TraceEvent(TraceEventType.Warning, traceCode, traceDescription, new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0008D253 File Offset: 0x0008B453
		[SecuritySafeCritical]
		private static void LoadChannelBehaviors(EndpointBehaviorElement behaviorElement, KeyedByTypeCollection<IEndpointBehavior> channelBehaviors)
		{
			if (behaviorElement != null)
			{
				ConfigLoader.LoadBehaviors<IEndpointBehavior>(behaviorElement, channelBehaviors, false);
			}
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0008D260 File Offset: 0x0008B460
		[SecuritySafeCritical]
		internal static void LoadChannelBehaviors(string behaviorName, ContextInformation context, KeyedByTypeCollection<IEndpointBehavior> channelBehaviors)
		{
			ConfigLoader.LoadChannelBehaviors(ConfigLoader.LookupEndpointBehaviors(behaviorName, context), channelBehaviors);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0008D270 File Offset: 0x0008B470
		[SecuritySafeCritical]
		internal static Collection<IWsdlImportExtension> LoadWsdlImporters(WsdlImporterElementCollection wsdlImporterElements, ContextInformation context)
		{
			Collection<IWsdlImportExtension> collection = new Collection<IWsdlImportExtension>();
			foreach (object obj in wsdlImporterElements)
			{
				WsdlImporterElement wsdlImporterElement = (WsdlImporterElement)obj;
				Type type = Type.GetType(wsdlImporterElement.Type, true, true);
				if (!typeof(IWsdlImportExtension).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidWsdlExtensionTypeInConfig", new object[]
					{
						type.AssemblyQualifiedName
					})));
				}
				ConstructorInfo constructor = type.GetConstructor(ConfigLoader.emptyTypeArray);
				if (constructor == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlExtensionTypeRequiresDefaultConstructor", new object[]
					{
						type.AssemblyQualifiedName
					})));
				}
				collection.Add((IWsdlImportExtension)constructor.Invoke(ConfigLoader.emptyObjectArray));
			}
			return collection;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0008D370 File Offset: 0x0008B570
		[SecuritySafeCritical]
		internal static Collection<IPolicyImportExtension> LoadPolicyImporters(PolicyImporterElementCollection policyImporterElements, ContextInformation context)
		{
			Collection<IPolicyImportExtension> collection = new Collection<IPolicyImportExtension>();
			foreach (object obj in policyImporterElements)
			{
				PolicyImporterElement policyImporterElement = (PolicyImporterElement)obj;
				Type type = Type.GetType(policyImporterElement.Type, true, true);
				if (!typeof(IPolicyImportExtension).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidPolicyExtensionTypeInConfig", new object[]
					{
						type.AssemblyQualifiedName
					})));
				}
				ConstructorInfo constructor = type.GetConstructor(ConfigLoader.emptyTypeArray);
				if (constructor == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PolicyExtensionTypeRequiresDefaultConstructor", new object[]
					{
						type.AssemblyQualifiedName
					})));
				}
				collection.Add((IPolicyImportExtension)constructor.Invoke(ConfigLoader.emptyObjectArray));
			}
			return collection;
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0008D470 File Offset: 0x0008B670
		[SecuritySafeCritical]
		internal static EndpointAddress LoadEndpointAddress(EndpointAddressElementBase element)
		{
			return new EndpointAddress(element.Address, ConfigLoader.LoadIdentity(element.Identity), element.Headers.Headers);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0008D494 File Offset: 0x0008B694
		[SecuritySafeCritical]
		public void LoadHostConfig(ServiceElement serviceElement, ServiceHostBase host, Action<Uri> addBaseAddress)
		{
			HostElement host2 = serviceElement.Host;
			if (host2 != null)
			{
				if (!AspNetEnvironment.Enabled)
				{
					foreach (object obj in host2.BaseAddresses)
					{
						BaseAddressElement baseAddressElement = (BaseAddressElement)obj;
						string text = null;
						string baseAddress = baseAddressElement.BaseAddress;
						int num = baseAddress.IndexOf(':');
						if (num != -1 && baseAddress.Length >= num + 4 && baseAddress[num + 1] == '/' && baseAddress[num + 2] == '/' && baseAddress[num + 3] == '*')
						{
							string value = baseAddress.Substring(0, num + 3);
							string value2 = baseAddress.Substring(num + 4);
							StringBuilder stringBuilder = new StringBuilder(value);
							stringBuilder.Append(Dns.GetHostName());
							stringBuilder.Append(value2);
							text = stringBuilder.ToString();
						}
						if (text == null)
						{
							text = baseAddress;
						}
						Uri obj2;
						if (!Uri.TryCreate(text, UriKind.Absolute, out obj2))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("BaseAddressMustBeAbsolute")));
						}
						addBaseAddress(obj2);
					}
				}
				HostTimeoutsElement timeouts = host2.Timeouts;
				if (timeouts != null)
				{
					if (timeouts.OpenTimeout != TimeSpan.Zero)
					{
						host.OpenTimeout = timeouts.OpenTimeout;
					}
					if (timeouts.CloseTimeout != TimeSpan.Zero)
					{
						host.CloseTimeout = timeouts.CloseTimeout;
					}
				}
			}
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0008D61C File Offset: 0x0008B81C
		[SecuritySafeCritical]
		public void LoadServiceDescription(ServiceHostBase host, ServiceDescription description, ServiceElement serviceElement, Action<Uri> addBaseAddress, bool skipHost = false)
		{
			CommonBehaviorsSection commonBehaviorsSection = ConfigLoader.LookupCommonBehaviors((serviceElement == null) ? null : ConfigurationHelpers.GetEvaluationContext(serviceElement));
			if (commonBehaviorsSection != null && commonBehaviorsSection.ServiceBehaviors != null)
			{
				ConfigLoader.LoadBehaviors<IServiceBehavior>(commonBehaviorsSection.ServiceBehaviors, description.Behaviors, true);
			}
			string behaviorName = "";
			if (serviceElement != null)
			{
				if (!skipHost)
				{
					this.LoadHostConfig(serviceElement, host, addBaseAddress);
				}
				behaviorName = serviceElement.BehaviorConfiguration;
			}
			ServiceBehaviorElement serviceBehaviorElement = ConfigLoader.LookupServiceBehaviors(behaviorName, ConfigurationHelpers.GetEvaluationContext(serviceElement));
			if (serviceBehaviorElement != null)
			{
				ConfigLoader.LoadBehaviors<IServiceBehavior>(serviceBehaviorElement, description.Behaviors, false);
			}
			ServiceHostBase.ServiceAndBehaviorsContractResolver serviceAndBehaviorsContractResolver = this.contractResolver as ServiceHostBase.ServiceAndBehaviorsContractResolver;
			if (serviceAndBehaviorsContractResolver != null)
			{
				serviceAndBehaviorsContractResolver.AddBehaviorContractsToResolver(description.Behaviors);
			}
			if (serviceElement != null)
			{
				foreach (object obj in serviceElement.Endpoints)
				{
					ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj;
					if (string.IsNullOrEmpty(serviceEndpointElement.Kind))
					{
						ContractDescription contract = this.LookupContract(serviceEndpointElement.Contract, description.Name);
						string key = serviceEndpointElement.Binding + ":" + serviceEndpointElement.BindingConfiguration;
						Binding binding;
						if (!this.bindingTable.TryGetValue(key, out binding))
						{
							binding = ConfigLoader.LookupBinding(serviceEndpointElement.Binding, serviceEndpointElement.BindingConfiguration, ConfigurationHelpers.GetEvaluationContext(serviceElement));
							this.bindingTable.Add(key, binding);
						}
						if (!string.IsNullOrEmpty(serviceEndpointElement.BindingName))
						{
							binding.Name = serviceEndpointElement.BindingName;
						}
						if (!string.IsNullOrEmpty(serviceEndpointElement.BindingNamespace))
						{
							binding.Namespace = serviceEndpointElement.BindingNamespace;
						}
						Uri address = serviceEndpointElement.Address;
						ServiceEndpoint serviceEndpoint;
						if (null == address)
						{
							serviceEndpoint = new ServiceEndpoint(contract);
							serviceEndpoint.Binding = binding;
						}
						else
						{
							Uri uri = ServiceHostBase.MakeAbsoluteUri(address, binding, host.InternalBaseAddresses);
							serviceEndpoint = new ServiceEndpoint(contract, binding, new EndpointAddress(uri, ConfigLoader.LoadIdentity(serviceEndpointElement.Identity), serviceEndpointElement.Headers.Headers));
							serviceEndpoint.UnresolvedAddress = serviceEndpointElement.Address;
						}
						if (serviceEndpointElement.ListenUri != null)
						{
							serviceEndpoint.ListenUri = ServiceHostBase.MakeAbsoluteUri(serviceEndpointElement.ListenUri, binding, host.InternalBaseAddresses);
							serviceEndpoint.UnresolvedListenUri = serviceEndpointElement.ListenUri;
						}
						serviceEndpoint.ListenUriMode = serviceEndpointElement.ListenUriMode;
						if (!string.IsNullOrEmpty(serviceEndpointElement.Name))
						{
							serviceEndpoint.Name = serviceEndpointElement.Name;
						}
						KeyedByTypeCollection<IEndpointBehavior> behaviors = serviceEndpoint.Behaviors;
						EndpointBehaviorElement endpointBehaviorElement = ConfigLoader.LookupEndpointBehaviors(serviceEndpointElement.BehaviorConfiguration, ConfigurationHelpers.GetEvaluationContext(serviceEndpointElement));
						if (endpointBehaviorElement != null)
						{
							ConfigLoader.LoadBehaviors<IEndpointBehavior>(endpointBehaviorElement, behaviors, false);
						}
						if (serviceEndpointElement.ElementInformation.Properties["isSystemEndpoint"].ValueOrigin != PropertyValueOrigin.Default)
						{
							serviceEndpoint.IsSystemEndpoint = serviceEndpointElement.IsSystemEndpoint;
						}
						description.Endpoints.Add(serviceEndpoint);
					}
					else
					{
						ServiceEndpoint item = this.LookupEndpoint(serviceEndpointElement, ConfigurationHelpers.GetEvaluationContext(serviceElement), host, description, false);
						description.Endpoints.Add(item);
					}
				}
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0008D920 File Offset: 0x0008BB20
		[SecuritySafeCritical]
		public static void LoadDefaultEndpointBehaviors(ServiceEndpoint endpoint)
		{
			EndpointBehaviorElement endpointBehaviorElement = ConfigLoader.LookupEndpointBehaviors("", ConfigurationHelpers.GetEvaluationContext(null));
			if (endpointBehaviorElement != null)
			{
				ConfigLoader.LoadBehaviors<IEndpointBehavior>(endpointBehaviorElement, endpoint.Behaviors, false);
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0008D950 File Offset: 0x0008BB50
		[SecurityCritical]
		private static EndpointCollectionElement LookupEndpointCollectionElement(string endpointSectionName, ContextInformation context)
		{
			if (string.IsNullOrEmpty(endpointSectionName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointTypeCannotBeNullOrEmpty")));
			}
			EndpointCollectionElement result;
			if (context == null)
			{
				result = ConfigurationHelpers.UnsafeGetEndpointCollectionElement(endpointSectionName);
			}
			else
			{
				result = ConfigurationHelpers.UnsafeGetAssociatedEndpointCollectionElement(context, endpointSectionName);
			}
			return result;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0008D996 File Offset: 0x0008BB96
		[SecuritySafeCritical]
		internal static ServiceEndpoint LookupEndpoint(string configurationName, EndpointAddress address, ContractDescription contract)
		{
			return ConfigLoader.LookupEndpoint(configurationName, address, contract, null);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0008D9A4 File Offset: 0x0008BBA4
		[SecuritySafeCritical]
		internal static ServiceEndpoint LookupEndpoint(string configurationName, EndpointAddress address, ContractDescription contract, ContextInformation configurationContext)
		{
			bool wildcard = ConfigLoader.IsWildcardMatch(configurationName);
			ServiceEndpoint result;
			ConfigLoader.LookupChannel(configurationContext, configurationName, contract, address, wildcard, true, out result);
			return result;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0008D9C7 File Offset: 0x0008BBC7
		internal static ServiceEndpoint LookupEndpoint(ChannelEndpointElement channelEndpointElement, ContextInformation context)
		{
			return ConfigLoader.LookupEndpoint(channelEndpointElement, context, null, null);
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0008D9D4 File Offset: 0x0008BBD4
		[SecuritySafeCritical]
		private static ServiceEndpoint LookupEndpoint(ChannelEndpointElement channelEndpointElement, ContextInformation context, EndpointAddress address, ContractDescription contract)
		{
			EndpointCollectionElement endpointCollectionElement = ConfigLoader.LookupEndpointCollectionElement(channelEndpointElement.Kind, context);
			ServiceEndpoint serviceEndpoint = null;
			string text = channelEndpointElement.EndpointConfiguration ?? string.Empty;
			bool flag = false;
			foreach (StandardEndpointElement standardEndpointElement in endpointCollectionElement.ConfiguredEndpoints)
			{
				if (standardEndpointElement.Name.Equals(text, StringComparison.Ordinal))
				{
					if (ConfigLoader.resolvedEndpoints == null)
					{
						ConfigLoader.resolvedEndpoints = new List<string>();
					}
					string text2 = channelEndpointElement.Kind + "/" + text;
					if (ConfigLoader.resolvedEndpoints.Contains(text2))
					{
						ConfigurationElement configurationElement = standardEndpointElement;
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string arg in ConfigLoader.resolvedEndpoints)
						{
							stringBuilder = stringBuilder.AppendFormat("{0}, ", arg);
						}
						stringBuilder = stringBuilder.Append(text2);
						ConfigLoader.resolvedEndpoints = null;
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointReferenceCycleDetected", new object[]
						{
							stringBuilder.ToString()
						}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
					}
					try
					{
						ConfigLoader.CheckAccess(standardEndpointElement);
						ConfigLoader.resolvedEndpoints.Add(text2);
						ConfigLoader.ConfigureEndpoint(standardEndpointElement, channelEndpointElement, address, context, contract, out serviceEndpoint);
						ConfigLoader.resolvedEndpoints.Remove(text2);
					}
					catch
					{
						if (ConfigLoader.resolvedEndpoints != null)
						{
							ConfigLoader.resolvedBindings = null;
						}
						throw;
					}
					if (ConfigLoader.resolvedEndpoints != null && ConfigLoader.resolvedEndpoints.Count == 0)
					{
						ConfigLoader.resolvedEndpoints = null;
					}
					flag = true;
				}
			}
			if (!flag)
			{
				serviceEndpoint = null;
			}
			if (serviceEndpoint == null && string.IsNullOrEmpty(text))
			{
				StandardEndpointElement defaultStandardEndpointElement = endpointCollectionElement.GetDefaultStandardEndpointElement();
				ConfigLoader.ConfigureEndpoint(defaultStandardEndpointElement, channelEndpointElement, address, context, contract, out serviceEndpoint);
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(3);
				dictionary["FoundEndpoint"] = (serviceEndpoint != null);
				bool flag2 = string.IsNullOrEmpty(text);
				int traceCode;
				string @string;
				if (flag2)
				{
					traceCode = 524356;
					@string = SR.GetString("TraceCodeGetDefaultConfiguredEndpoint");
				}
				else
				{
					traceCode = 524355;
					@string = SR.GetString("TraceCodeGetConfiguredEndpoint");
					dictionary["Name"] = text;
				}
				dictionary["Endpoint"] = channelEndpointElement.Kind;
				TraceUtility.TraceEvent(TraceEventType.Verbose, traceCode, @string, new DictionaryTraceRecord(dictionary), null, null);
			}
			if (serviceEndpoint != null)
			{
				serviceEndpoint.IsFullyConfigured = true;
			}
			return serviceEndpoint;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0008DC84 File Offset: 0x0008BE84
		[SecuritySafeCritical]
		private static void ConfigureEndpoint(StandardEndpointElement standardEndpointElement, ChannelEndpointElement channelEndpointElement, EndpointAddress address, ContextInformation context, ContractDescription contract, out ServiceEndpoint endpoint)
		{
			ChannelEndpointElement channelEndpointElement2 = new ChannelEndpointElement();
			channelEndpointElement2.Copy(channelEndpointElement);
			standardEndpointElement.InitializeAndValidate(channelEndpointElement2);
			endpoint = standardEndpointElement.CreateServiceEndpoint(contract);
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ConfigNoEndpointCreated", new object[]
				{
					standardEndpointElement.GetType().AssemblyQualifiedName,
					(standardEndpointElement.EndpointType == null) ? string.Empty : standardEndpointElement.EndpointType.AssemblyQualifiedName
				})));
			}
			if (!string.IsNullOrEmpty(channelEndpointElement2.Binding))
			{
				endpoint.Binding = ConfigLoader.LookupBinding(channelEndpointElement2.Binding, channelEndpointElement2.BindingConfiguration, ConfigurationHelpers.GetEvaluationContext(channelEndpointElement));
			}
			if (!string.IsNullOrEmpty(channelEndpointElement2.Name))
			{
				endpoint.Name = channelEndpointElement2.Name;
			}
			if (address != null)
			{
				endpoint.Address = address;
			}
			if (endpoint.Address == null && channelEndpointElement2.Address != null && channelEndpointElement2.Address.OriginalString.Length > 0)
			{
				endpoint.Address = new EndpointAddress(channelEndpointElement2.Address, ConfigLoader.LoadIdentity(channelEndpointElement2.Identity), channelEndpointElement2.Headers.Headers);
			}
			CommonBehaviorsSection commonBehaviorsSection = ConfigLoader.LookupCommonBehaviors(ConfigurationHelpers.GetEvaluationContext(channelEndpointElement));
			if (commonBehaviorsSection != null && commonBehaviorsSection.EndpointBehaviors != null)
			{
				ConfigLoader.LoadBehaviors<IEndpointBehavior>(commonBehaviorsSection.EndpointBehaviors, endpoint.Behaviors, true);
			}
			EndpointBehaviorElement endpointBehaviorElement = ConfigLoader.LookupEndpointBehaviors(channelEndpointElement2.BehaviorConfiguration, ConfigurationHelpers.GetEvaluationContext(channelEndpointElement));
			if (endpointBehaviorElement != null)
			{
				ConfigLoader.LoadBehaviors<IEndpointBehavior>(endpointBehaviorElement, endpoint.Behaviors, false);
			}
			standardEndpointElement.ApplyConfiguration(endpoint, channelEndpointElement2);
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0008DE14 File Offset: 0x0008C014
		[SecuritySafeCritical]
		internal ServiceEndpoint LookupEndpoint(ServiceEndpointElement serviceEndpointElement, ContextInformation context, ServiceHostBase host, ServiceDescription description, bool omitSettingEndpointAddress = false)
		{
			EndpointCollectionElement endpointCollectionElement = ConfigLoader.LookupEndpointCollectionElement(serviceEndpointElement.Kind, context);
			ServiceEndpoint serviceEndpoint = null;
			string text = serviceEndpointElement.EndpointConfiguration ?? string.Empty;
			bool flag = false;
			foreach (StandardEndpointElement standardEndpointElement in endpointCollectionElement.ConfiguredEndpoints)
			{
				if (standardEndpointElement.Name.Equals(text, StringComparison.Ordinal))
				{
					if (ConfigLoader.resolvedEndpoints == null)
					{
						ConfigLoader.resolvedEndpoints = new List<string>();
					}
					string text2 = serviceEndpointElement.Kind + "/" + text;
					if (ConfigLoader.resolvedEndpoints.Contains(text2))
					{
						ConfigurationElement configurationElement = standardEndpointElement;
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string arg in ConfigLoader.resolvedEndpoints)
						{
							stringBuilder = stringBuilder.AppendFormat("{0}, ", arg);
						}
						stringBuilder = stringBuilder.Append(text2);
						ConfigLoader.resolvedEndpoints = null;
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointReferenceCycleDetected", new object[]
						{
							stringBuilder.ToString()
						}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
					}
					try
					{
						ConfigLoader.CheckAccess(standardEndpointElement);
						ConfigLoader.resolvedEndpoints.Add(text2);
						this.ConfigureEndpoint(standardEndpointElement, serviceEndpointElement, context, host, description, out serviceEndpoint, false);
						ConfigLoader.resolvedEndpoints.Remove(text2);
					}
					catch
					{
						if (ConfigLoader.resolvedEndpoints != null)
						{
							ConfigLoader.resolvedBindings = null;
						}
						throw;
					}
					if (ConfigLoader.resolvedEndpoints != null && ConfigLoader.resolvedEndpoints.Count == 0)
					{
						ConfigLoader.resolvedEndpoints = null;
					}
					flag = true;
				}
			}
			if (!flag)
			{
				serviceEndpoint = null;
			}
			if (serviceEndpoint == null && string.IsNullOrEmpty(text))
			{
				StandardEndpointElement defaultStandardEndpointElement = endpointCollectionElement.GetDefaultStandardEndpointElement();
				this.ConfigureEndpoint(defaultStandardEndpointElement, serviceEndpointElement, context, host, description, out serviceEndpoint, omitSettingEndpointAddress);
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(3);
				dictionary["FoundEndpoint"] = (serviceEndpoint != null);
				bool flag2 = string.IsNullOrEmpty(text);
				int traceCode;
				string @string;
				if (flag2)
				{
					traceCode = 524356;
					@string = SR.GetString("TraceCodeGetDefaultConfiguredEndpoint");
				}
				else
				{
					traceCode = 524355;
					@string = SR.GetString("TraceCodeGetConfiguredEndpoint");
					dictionary["Name"] = text;
				}
				dictionary["Endpoint"] = serviceEndpointElement.Kind;
				TraceUtility.TraceEvent(TraceEventType.Verbose, traceCode, @string, new DictionaryTraceRecord(dictionary), null, null);
			}
			return serviceEndpoint;
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0008E0C0 File Offset: 0x0008C2C0
		[SecuritySafeCritical]
		private void ConfigureEndpoint(StandardEndpointElement standardEndpointElement, ServiceEndpointElement serviceEndpointElement, ContextInformation context, ServiceHostBase host, ServiceDescription description, out ServiceEndpoint endpoint, bool omitSettingEndpointAddress = false)
		{
			ServiceEndpointElement serviceEndpointElement2 = new ServiceEndpointElement();
			serviceEndpointElement2.Copy(serviceEndpointElement);
			standardEndpointElement.InitializeAndValidate(serviceEndpointElement2);
			ContractDescription contractDescription = null;
			if (!string.IsNullOrEmpty(serviceEndpointElement2.Contract))
			{
				contractDescription = this.LookupContractForStandardEndpoint(serviceEndpointElement2.Contract, description.Name);
			}
			endpoint = standardEndpointElement.CreateServiceEndpoint(contractDescription);
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ConfigNoEndpointCreated", new object[]
				{
					standardEndpointElement.GetType().AssemblyQualifiedName,
					(standardEndpointElement.EndpointType == null) ? string.Empty : standardEndpointElement.EndpointType.AssemblyQualifiedName
				})));
			}
			Binding binding = null;
			if (!string.IsNullOrEmpty(serviceEndpointElement2.Binding))
			{
				string key = serviceEndpointElement2.Binding + ":" + serviceEndpointElement2.BindingConfiguration;
				if (!this.bindingTable.TryGetValue(key, out binding))
				{
					binding = ConfigLoader.LookupBinding(serviceEndpointElement2.Binding, serviceEndpointElement2.BindingConfiguration, context);
					this.bindingTable.Add(key, binding);
				}
			}
			else
			{
				binding = endpoint.Binding;
			}
			if (binding != null)
			{
				if (!string.IsNullOrEmpty(serviceEndpointElement2.BindingName))
				{
					binding.Name = serviceEndpointElement2.BindingName;
				}
				if (!string.IsNullOrEmpty(serviceEndpointElement2.BindingNamespace))
				{
					binding.Namespace = serviceEndpointElement2.BindingNamespace;
				}
				endpoint.Binding = binding;
				if (!omitSettingEndpointAddress)
				{
					ConfigLoader.ConfigureEndpointAddress(serviceEndpointElement2, host, endpoint);
					ConfigLoader.ConfigureEndpointListenUri(serviceEndpointElement2, host, endpoint);
				}
			}
			endpoint.ListenUriMode = serviceEndpointElement2.ListenUriMode;
			if (!string.IsNullOrEmpty(serviceEndpointElement2.Name))
			{
				endpoint.Name = serviceEndpointElement2.Name;
			}
			KeyedByTypeCollection<IEndpointBehavior> behaviors = endpoint.Behaviors;
			EndpointBehaviorElement endpointBehaviorElement = ConfigLoader.LookupEndpointBehaviors(serviceEndpointElement2.BehaviorConfiguration, ConfigurationHelpers.GetEvaluationContext(serviceEndpointElement));
			if (endpointBehaviorElement != null)
			{
				ConfigLoader.LoadBehaviors<IEndpointBehavior>(endpointBehaviorElement, behaviors, false);
			}
			if (serviceEndpointElement2.ElementInformation.Properties["isSystemEndpoint"].ValueOrigin != PropertyValueOrigin.Default)
			{
				endpoint.IsSystemEndpoint = serviceEndpointElement2.IsSystemEndpoint;
			}
			standardEndpointElement.ApplyConfiguration(endpoint, serviceEndpointElement2);
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x0008E2A8 File Offset: 0x0008C4A8
		internal static void ConfigureEndpointAddress(ServiceEndpointElement serviceEndpointElement, ServiceHostBase host, ServiceEndpoint endpoint)
		{
			if (serviceEndpointElement.Address != null)
			{
				Uri uri = ServiceHostBase.MakeAbsoluteUri(serviceEndpointElement.Address, endpoint.Binding, host.InternalBaseAddresses);
				endpoint.Address = new EndpointAddress(uri, ConfigLoader.LoadIdentity(serviceEndpointElement.Identity), serviceEndpointElement.Headers.Headers);
				endpoint.UnresolvedAddress = serviceEndpointElement.Address;
			}
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0008E309 File Offset: 0x0008C509
		internal static void ConfigureEndpointListenUri(ServiceEndpointElement serviceEndpointElement, ServiceHostBase host, ServiceEndpoint endpoint)
		{
			if (serviceEndpointElement.ListenUri != null)
			{
				endpoint.ListenUri = ServiceHostBase.MakeAbsoluteUri(serviceEndpointElement.ListenUri, endpoint.Binding, host.InternalBaseAddresses);
				endpoint.UnresolvedListenUri = serviceEndpointElement.ListenUri;
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0008E342 File Offset: 0x0008C542
		internal static Binding LookupBinding(string bindingSectionName, string configurationName)
		{
			return ConfigLoader.LookupBinding(bindingSectionName, configurationName, null);
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0008E34C File Offset: 0x0008C54C
		internal static ComContractElement LookupComContract(Guid contractIID)
		{
			ComContractsSection comContractsSection = (ComContractsSection)ConfigurationHelpers.GetSection(ConfigurationStrings.ComContractsSectionPath);
			foreach (object obj in comContractsSection.ComContracts)
			{
				ComContractElement comContractElement = (ComContractElement)obj;
				Guid a;
				if (DiagnosticUtility.Utility.TryCreateGuid(comContractElement.Contract, out a) && a == contractIID)
				{
					return comContractElement;
				}
			}
			return null;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0008E3D8 File Offset: 0x0008C5D8
		[SecuritySafeCritical]
		internal static ProtocolMappingItem LookupProtocolMapping(string scheme)
		{
			ProtocolMappingSection protocolMappingSection = (ProtocolMappingSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.ProtocolMappingSectionPath);
			foreach (object obj in protocolMappingSection.ProtocolMappingCollection)
			{
				ProtocolMappingElement protocolMappingElement = (ProtocolMappingElement)obj;
				if (protocolMappingElement.Scheme == scheme)
				{
					return new ProtocolMappingItem(protocolMappingElement.Binding, protocolMappingElement.BindingConfiguration);
				}
			}
			return null;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0008E464 File Offset: 0x0008C664
		[SecurityCritical]
		private static BindingCollectionElement GetBindingCollectionElement(string bindingSectionName, ContextInformation context)
		{
			if (string.IsNullOrEmpty(bindingSectionName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigBindingTypeCannotBeNullOrEmpty")));
			}
			if (context == null)
			{
				return ConfigurationHelpers.UnsafeGetBindingCollectionElement(bindingSectionName);
			}
			return ConfigurationHelpers.UnsafeGetAssociatedBindingCollectionElement(context, bindingSectionName);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0008E49C File Offset: 0x0008C69C
		[SecuritySafeCritical]
		internal static Binding LookupBinding(string bindingSectionName, string configurationName, ContextInformation context)
		{
			BindingCollectionElement bindingCollectionElement = ConfigLoader.GetBindingCollectionElement(bindingSectionName, context);
			Binding binding;
			if (configurationName == null)
			{
				binding = bindingCollectionElement.GetDefault();
			}
			else
			{
				Binding @default = bindingCollectionElement.GetDefault();
				binding = ConfigLoader.LookupBinding(bindingSectionName, configurationName, bindingCollectionElement, @default);
				if (binding == null && configurationName == "")
				{
					binding = @default;
				}
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(3);
				dictionary["FoundBinding"] = (binding != null);
				bool flag = string.IsNullOrEmpty(configurationName);
				int traceCode;
				string @string;
				if (flag)
				{
					traceCode = 524325;
					@string = SR.GetString("TraceCodeGetDefaultConfiguredBinding");
				}
				else
				{
					traceCode = 524322;
					@string = SR.GetString("TraceCodeGetConfiguredBinding");
					dictionary["Name"] = (string.IsNullOrEmpty(configurationName) ? SR.GetString("Default") : configurationName);
				}
				dictionary["Binding"] = bindingSectionName;
				TraceUtility.TraceEvent(TraceEventType.Verbose, traceCode, @string, new DictionaryTraceRecord(dictionary), null, null);
			}
			return binding;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0008E57C File Offset: 0x0008C77C
		private static Binding LookupBinding(string bindingSectionName, string configurationName, BindingCollectionElement bindingCollectionElement, Binding defaultBinding)
		{
			Binding binding = defaultBinding;
			if (configurationName != null)
			{
				bool flag = false;
				foreach (object obj in bindingCollectionElement.ConfiguredBindings)
				{
					IBindingConfigurationElement bindingConfigurationElement = obj as IBindingConfigurationElement;
					if (bindingConfigurationElement != null && bindingConfigurationElement.Name.Equals(configurationName, StringComparison.Ordinal))
					{
						if (ConfigLoader.resolvedBindings == null)
						{
							ConfigLoader.resolvedBindings = new List<string>();
						}
						string text = bindingSectionName + "/" + configurationName;
						if (ConfigLoader.resolvedBindings.Contains(text))
						{
							ConfigurationElement configurationElement = (ConfigurationElement)obj;
							StringBuilder stringBuilder = new StringBuilder();
							foreach (string arg in ConfigLoader.resolvedBindings)
							{
								stringBuilder = stringBuilder.AppendFormat("{0}, ", arg);
							}
							stringBuilder = stringBuilder.Append(text);
							ConfigLoader.resolvedBindings = null;
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigBindingReferenceCycleDetected", new object[]
							{
								stringBuilder.ToString()
							}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
						}
						try
						{
							ConfigLoader.CheckAccess(obj as IConfigurationContextProviderInternal);
							ConfigLoader.resolvedBindings.Add(text);
							bindingConfigurationElement.ApplyConfiguration(binding);
							ConfigLoader.resolvedBindings.Remove(text);
						}
						catch
						{
							if (ConfigLoader.resolvedBindings != null)
							{
								ConfigLoader.resolvedBindings = null;
							}
							throw;
						}
						if (ConfigLoader.resolvedBindings != null && ConfigLoader.resolvedBindings.Count == 0)
						{
							ConfigLoader.resolvedBindings = null;
						}
						flag = true;
					}
				}
				if (!flag)
				{
					binding = null;
				}
			}
			return binding;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x0008E764 File Offset: 0x0008C964
		[SecurityCritical]
		private static EndpointBehaviorElement LookupEndpointBehaviors(string behaviorName, ContextInformation context)
		{
			EndpointBehaviorElement endpointBehaviorElement = null;
			if (behaviorName != null)
			{
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524320, SR.GetString("TraceCodeGetBehaviorElement"), new StringTraceRecord("BehaviorName", behaviorName), null, null);
				}
				BehaviorsSection behaviorsSection;
				if (context == null)
				{
					behaviorsSection = BehaviorsSection.UnsafeGetSection();
				}
				else
				{
					behaviorsSection = BehaviorsSection.UnsafeGetAssociatedSection(context);
				}
				if (behaviorsSection.EndpointBehaviors.ContainsKey(behaviorName))
				{
					endpointBehaviorElement = behaviorsSection.EndpointBehaviors[behaviorName];
				}
			}
			if (endpointBehaviorElement != null)
			{
				ConfigLoader.CheckAccess(endpointBehaviorElement);
			}
			return endpointBehaviorElement;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x0008E7DC File Offset: 0x0008C9DC
		[SecurityCritical]
		private static ServiceBehaviorElement LookupServiceBehaviors(string behaviorName, ContextInformation context)
		{
			ServiceBehaviorElement serviceBehaviorElement = null;
			if (behaviorName != null)
			{
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524320, SR.GetString("TraceCodeGetBehaviorElement"), new StringTraceRecord("BehaviorName", behaviorName), null, null);
				}
				BehaviorsSection behaviorsSection;
				if (context == null)
				{
					behaviorsSection = BehaviorsSection.UnsafeGetSection();
				}
				else
				{
					behaviorsSection = BehaviorsSection.UnsafeGetAssociatedSection(context);
				}
				if (behaviorsSection.ServiceBehaviors.ContainsKey(behaviorName))
				{
					serviceBehaviorElement = behaviorsSection.ServiceBehaviors[behaviorName];
				}
			}
			if (serviceBehaviorElement != null)
			{
				ConfigLoader.CheckAccess(serviceBehaviorElement);
			}
			return serviceBehaviorElement;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x0008E851 File Offset: 0x0008CA51
		[SecurityCritical]
		private static CommonBehaviorsSection LookupCommonBehaviors(ContextInformation context)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 524321, SR.GetString("TraceCodeGetCommonBehaviors"), null);
			}
			if (context != null)
			{
				return CommonBehaviorsSection.UnsafeGetAssociatedSection(context);
			}
			return CommonBehaviorsSection.UnsafeGetSection();
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x0008E880 File Offset: 0x0008CA80
		private static bool IsChannelElementMatch(ChannelEndpointElement channelElement, ContractDescription contract, EndpointAddress address, bool useChannelElementKind, out ServiceEndpoint serviceEndpoint)
		{
			serviceEndpoint = null;
			if (string.IsNullOrEmpty(channelElement.Kind))
			{
				return channelElement.Contract == contract.ConfigurationName;
			}
			if (!useChannelElementKind)
			{
				return false;
			}
			serviceEndpoint = ConfigLoader.LookupEndpoint(channelElement, null, address, contract);
			if (serviceEndpoint == null)
			{
				return false;
			}
			if (serviceEndpoint.Contract.ConfigurationName == contract.ConfigurationName && (string.IsNullOrEmpty(channelElement.Contract) || contract.ConfigurationName == channelElement.Contract))
			{
				return true;
			}
			serviceEndpoint = null;
			return false;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x0008E90C File Offset: 0x0008CB0C
		[SecurityCritical]
		private static ChannelEndpointElement LookupChannel(ContextInformation configurationContext, string configurationName, ContractDescription contract, EndpointAddress address, bool wildcard, bool useChannelElementKind, out ServiceEndpoint serviceEndpoint)
		{
			serviceEndpoint = null;
			ClientSection clientSection = (configurationContext == null) ? ClientSection.UnsafeGetSection() : ClientSection.UnsafeGetSection(configurationContext);
			ChannelEndpointElement channelEndpointElement = null;
			foreach (object obj in clientSection.Endpoints)
			{
				ChannelEndpointElement channelEndpointElement2 = (ChannelEndpointElement)obj;
				ServiceEndpoint serviceEndpoint2;
				if (ConfigLoader.IsChannelElementMatch(channelEndpointElement2, contract, address, useChannelElementKind, out serviceEndpoint2) && (channelEndpointElement2.Name == configurationName || wildcard))
				{
					if (channelEndpointElement != null)
					{
						if (wildcard)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxConfigLoaderMultipleEndpointMatchesWildcard1", new object[]
							{
								contract.ConfigurationName
							})));
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxConfigLoaderMultipleEndpointMatchesSpecified2", new object[]
						{
							contract.ConfigurationName,
							configurationName
						})));
					}
					else
					{
						channelEndpointElement = channelEndpointElement2;
						serviceEndpoint = serviceEndpoint2;
					}
				}
			}
			if (channelEndpointElement != null)
			{
				ConfigLoader.CheckAccess(channelEndpointElement);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(8);
				dictionary["FoundChannelElement"] = (channelEndpointElement != null);
				dictionary["Name"] = configurationName;
				dictionary["ContractName"] = contract.ConfigurationName;
				if (channelEndpointElement != null)
				{
					if (!string.IsNullOrEmpty(channelEndpointElement.Binding))
					{
						dictionary["Binding"] = channelEndpointElement.Binding;
					}
					if (!string.IsNullOrEmpty(channelEndpointElement.BindingConfiguration))
					{
						dictionary["BindingConfiguration"] = channelEndpointElement.BindingConfiguration;
					}
					if (channelEndpointElement.Address != null)
					{
						dictionary["RemoteEndpointUri"] = channelEndpointElement.Address.ToString();
					}
					if (!string.IsNullOrEmpty(channelEndpointElement.ElementInformation.Source))
					{
						dictionary["ConfigurationFileSource"] = channelEndpointElement.ElementInformation.Source;
						dictionary["ConfigurationFileLineNumber"] = channelEndpointElement.ElementInformation.LineNumber;
					}
				}
				TraceUtility.TraceEvent(TraceEventType.Information, 524323, SR.GetString("TraceCodeGetChannelEndpointElement"), new DictionaryTraceRecord(dictionary), null, null);
			}
			return channelEndpointElement;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x0008EB20 File Offset: 0x0008CD20
		internal ContractDescription LookupContract(string contractName, string serviceName)
		{
			ContractDescription contractDescription = this.LookupContractForStandardEndpoint(contractName, serviceName);
			if (contractDescription != null)
			{
				return contractDescription;
			}
			if (contractName == string.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxReflectedContractKeyNotFoundEmpty", new object[]
				{
					serviceName
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxReflectedContractKeyNotFound2", new object[]
			{
				contractName,
				serviceName
			})));
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x0008EB94 File Offset: 0x0008CD94
		internal ContractDescription LookupContractForStandardEndpoint(string contractName, string serviceName)
		{
			ContractDescription contractDescription = this.contractResolver.ResolveContract(contractName);
			if (contractDescription == null && contractName == "IMetadataExchange")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxReflectedContractKeyNotFoundIMetadataExchange", new object[]
				{
					serviceName
				})));
			}
			return contractDescription;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x0008EBE4 File Offset: 0x0008CDE4
		[SecurityCritical]
		public ServiceElement LookupService(string serviceConfigurationName)
		{
			ServicesSection servicesSection = ServicesSection.UnsafeGetSection();
			return this.LookupService(serviceConfigurationName, servicesSection);
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x0008EC00 File Offset: 0x0008CE00
		public ServiceElement LookupService(string serviceConfigurationName, ServicesSection servicesSection)
		{
			ServiceElement serviceElement = null;
			ServiceElementCollection services = servicesSection.Services;
			for (int i = 0; i < services.Count; i++)
			{
				ServiceElement serviceElement2 = services[i];
				if (serviceElement2.Name == serviceConfigurationName)
				{
					serviceElement = serviceElement2;
				}
			}
			if (serviceElement != null)
			{
				ConfigLoader.CheckAccess(serviceElement);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 524326, SR.GetString("TraceCodeGetServiceElement"), new ServiceConfigurationTraceRecord(serviceElement), null, null);
			}
			return serviceElement;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x0008EC6D File Offset: 0x0008CE6D
		private static bool IsWildcardMatch(string endpointConfigurationName)
		{
			return string.Equals(endpointConfigurationName, "*", StringComparison.Ordinal);
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0008EC7C File Offset: 0x0008CE7C
		private static bool IsConfigAboveApplication(ContextInformation contextInformation)
		{
			if (contextInformation == null)
			{
				return true;
			}
			if (contextInformation.IsMachineLevel)
			{
				return true;
			}
			bool flag = contextInformation.HostingContext is ExeContext;
			return !flag && ConfigLoader.IsWebConfigAboveApplication(contextInformation);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0008ECB2 File Offset: 0x0008CEB2
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsWebConfigAboveApplication(ContextInformation contextInformation)
		{
			return AspNetEnvironment.Current.IsWebConfigAboveApplication(contextInformation.HostingContext);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0008ECC4 File Offset: 0x0008CEC4
		private static void CheckAccess(IConfigurationContextProviderInternal element)
		{
			if (ConfigLoader.IsConfigAboveApplication(ConfigurationHelpers.GetOriginalEvaluationContext(element)))
			{
				ConfigLoader.ConfigurationPermission.Demand();
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x0008ECDD File Offset: 0x0008CEDD
		private static ConfigurationPermission ConfigurationPermission
		{
			[SecuritySafeCritical]
			get
			{
				if (ConfigLoader.configurationPermission == null)
				{
					ConfigLoader.configurationPermission = new ConfigurationPermission(PermissionState.Unrestricted);
				}
				return ConfigLoader.configurationPermission;
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0008ECF8 File Offset: 0x0008CEF8
		[SecurityCritical]
		private static bool ThreadHasConfigurationPermission()
		{
			try
			{
				ConfigLoader.ConfigurationPermission.Demand();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x040021C8 RID: 8648
		[ThreadStatic]
		private static List<string> resolvedBindings;

		// Token: 0x040021C9 RID: 8649
		[ThreadStatic]
		private static List<string> resolvedEndpoints;

		// Token: 0x040021CA RID: 8650
		private static readonly object[] emptyObjectArray = new object[0];

		// Token: 0x040021CB RID: 8651
		private static readonly Type[] emptyTypeArray = new Type[0];

		// Token: 0x040021CC RID: 8652
		private Dictionary<string, Binding> bindingTable;

		// Token: 0x040021CD RID: 8653
		private IContractResolver contractResolver;

		// Token: 0x040021CE RID: 8654
		private ContextInformation configurationContext;

		// Token: 0x040021CF RID: 8655
		[SecurityCritical]
		private static ConfigurationPermission configurationPermission;
	}
}

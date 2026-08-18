using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.MsmqIntegration;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000400 RID: 1024
	internal class DispatcherBuilder
	{
		// Token: 0x0600270C RID: 9996 RVA: 0x0008F534 File Offset: 0x0008D734
		private static void AddMsmqIntegrationContractInformation(ServiceEndpoint endpoint)
		{
			MsmqIntegrationBinding msmqIntegrationBinding = endpoint.Binding as MsmqIntegrationBinding;
			if (msmqIntegrationBinding != null)
			{
				Type[] targetSerializationTypes = DispatcherBuilder.ProcessDescriptionForMsmqIntegration(endpoint, msmqIntegrationBinding.TargetSerializationTypes);
				msmqIntegrationBinding.TargetSerializationTypes = targetSerializationTypes;
				return;
			}
			CustomBinding customBinding = endpoint.Binding as CustomBinding;
			if (customBinding != null)
			{
				MsmqIntegrationBindingElement msmqIntegrationBindingElement = customBinding.Elements.Find<MsmqIntegrationBindingElement>();
				if (msmqIntegrationBindingElement != null)
				{
					Type[] targetSerializationTypes2 = DispatcherBuilder.ProcessDescriptionForMsmqIntegration(endpoint, msmqIntegrationBindingElement.TargetSerializationTypes);
					msmqIntegrationBindingElement.TargetSerializationTypes = targetSerializationTypes2;
				}
			}
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0008F59C File Offset: 0x0008D79C
		private static Type[] ProcessDescriptionForMsmqIntegration(ServiceEndpoint endpoint, Type[] existingSerializationTypes)
		{
			List<Type> list;
			if (existingSerializationTypes == null)
			{
				list = new List<Type>();
			}
			else
			{
				list = new List<Type>(existingSerializationTypes);
			}
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				foreach (Type item in operationDescription.KnownTypes)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					messageDescription.Body.WrapperName = (messageDescription.Body.WrapperNamespace = null);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0008F6A8 File Offset: 0x0008D8A8
		internal static ClientRuntime BuildProxyBehavior(ServiceEndpoint serviceEndpoint, out BindingParameterCollection parameters)
		{
			parameters = new BindingParameterCollection();
			DispatcherBuilder.SecurityContractInformationEndpointBehavior.ClientInstance.AddBindingParameters(serviceEndpoint, parameters);
			DispatcherBuilder.AddBindingParameters(serviceEndpoint, parameters);
			ContractDescription contract = serviceEndpoint.Contract;
			ClientRuntime clientRuntime = new ClientRuntime(contract.Name, contract.Namespace);
			clientRuntime.ContractClientType = contract.ContractType;
			IdentityVerifier property = serviceEndpoint.Binding.GetProperty<IdentityVerifier>(parameters);
			if (property != null)
			{
				clientRuntime.IdentityVerifier = property;
			}
			for (int i = 0; i < contract.Operations.Count; i++)
			{
				OperationDescription operationDescription = contract.Operations[i];
				if (!operationDescription.IsServerInitiated())
				{
					DispatcherBuilder.BuildProxyOperation(operationDescription, clientRuntime);
				}
				else
				{
					DispatcherBuilder.BuildDispatchOperation(operationDescription, clientRuntime.CallbackDispatchRuntime, null);
				}
			}
			DispatcherBuilder.ApplyClientBehavior(serviceEndpoint, clientRuntime);
			return clientRuntime;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0008F75C File Offset: 0x0008D95C
		private void ValidateDescription(ServiceDescription description, ServiceHostBase serviceHost)
		{
			description.EnsureInvariants();
			((IServiceBehavior)PartialTrustValidationBehavior.Instance).Validate(description, serviceHost);
			((IServiceBehavior)PeerValidationBehavior.Instance).Validate(description, serviceHost);
			((IServiceBehavior)TransactionValidationBehavior.Instance).Validate(description, serviceHost);
			((IServiceBehavior)MsmqIntegrationValidationBehavior.Instance).Validate(description, serviceHost);
			((IServiceBehavior)SecurityValidationBehavior.Instance).Validate(description, serviceHost);
			((IServiceBehavior)new UniqueContractNameValidationBehavior()).Validate(description, serviceHost);
			for (int i = 0; i < description.Behaviors.Count; i++)
			{
				IServiceBehavior serviceBehavior = description.Behaviors[i];
				serviceBehavior.Validate(description, serviceHost);
			}
			for (int j = 0; j < description.Endpoints.Count; j++)
			{
				ServiceEndpoint serviceEndpoint = description.Endpoints[j];
				ContractDescription contract = serviceEndpoint.Contract;
				bool flag = false;
				for (int k = 0; k < j; k++)
				{
					if (description.Endpoints[k].Contract == contract)
					{
						flag = true;
						break;
					}
				}
				serviceEndpoint.ValidateForService(!flag);
			}
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0008F848 File Offset: 0x0008DA48
		private static void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection parameters)
		{
			foreach (IContractBehavior contractBehavior in endpoint.Contract.Behaviors)
			{
				contractBehavior.AddBindingParameters(endpoint.Contract, endpoint, parameters);
			}
			foreach (IEndpointBehavior endpointBehavior in endpoint.Behaviors)
			{
				endpointBehavior.AddBindingParameters(endpoint, parameters);
			}
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				foreach (IOperationBehavior operationBehavior in operationDescription.Behaviors)
				{
					operationBehavior.AddBindingParameters(operationDescription, parameters);
				}
			}
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0008F964 File Offset: 0x0008DB64
		private Type BuildChannelListener(DispatcherBuilder.StuffPerListenUriInfo stuff, ServiceHostBase serviceHost, Uri listenUri, ListenUriMode listenUriMode, bool supportContextSession, out IChannelListener result)
		{
			Binding binding = stuff.Endpoints[0].Binding;
			CustomBinding customBinding = new CustomBinding(binding);
			BindingParameterCollection parameters = stuff.Parameters;
			Uri listenUriBaseAddress;
			string listenUriRelativeAddress;
			this.GetBaseAndRelativeAddresses(serviceHost, listenUri, customBinding.Scheme, out listenUriBaseAddress, out listenUriRelativeAddress);
			InternalDuplexBindingElement internalDuplexBindingElement = null;
			InternalDuplexBindingElement.AddDuplexListenerSupport(customBinding, ref internalDuplexBindingElement);
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			bool flag6 = true;
			string text = null;
			string text2 = null;
			for (int i = 0; i < stuff.Endpoints.Count; i++)
			{
				ContractDescription contract = stuff.Endpoints[i].Contract;
				if (contract.SessionMode == SessionMode.Required)
				{
					text = contract.Name;
				}
				if (contract.SessionMode == SessionMode.NotAllowed)
				{
					text2 = contract.Name;
				}
				IList supportedChannelTypes = DispatcherBuilder.GetSupportedChannelTypes(contract);
				if (!supportedChannelTypes.Contains(typeof(IReplyChannel)))
				{
					flag = false;
				}
				if (!supportedChannelTypes.Contains(typeof(IReplySessionChannel)))
				{
					flag2 = false;
				}
				if (!supportedChannelTypes.Contains(typeof(IInputChannel)))
				{
					flag3 = false;
				}
				if (!supportedChannelTypes.Contains(typeof(IInputSessionChannel)))
				{
					flag4 = false;
				}
				if (!supportedChannelTypes.Contains(typeof(IDuplexChannel)))
				{
					flag5 = false;
				}
				if (!supportedChannelTypes.Contains(typeof(IDuplexSessionChannel)))
				{
					flag6 = false;
				}
			}
			if (text != null && text2 != null)
			{
				string @string = SR.GetString("SFxCannotRequireBothSessionAndDatagram3", new object[]
				{
					text2,
					text,
					customBinding.Name
				});
				Exception exception = new InvalidOperationException(@string);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			List<Type> list = new List<Type>();
			if (flag3)
			{
				list.Add(typeof(IInputChannel));
			}
			if (flag4)
			{
				list.Add(typeof(IInputSessionChannel));
			}
			if (flag)
			{
				list.Add(typeof(IReplyChannel));
			}
			if (flag2)
			{
				list.Add(typeof(IReplySessionChannel));
			}
			if (flag5)
			{
				list.Add(typeof(IDuplexChannel));
			}
			if (flag6)
			{
				list.Add(typeof(IDuplexSessionChannel));
			}
			Type result2 = DispatcherBuilder.MaybeCreateListener(true, list.ToArray(), customBinding, parameters, listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, serviceHost.ServiceThrottle, out result, supportContextSession && text != null);
			if (result == null)
			{
				Dictionary<Type, byte> dictionary = new Dictionary<Type, byte>();
				if (customBinding.CanBuildChannelListener<IInputChannel>(new object[0]))
				{
					dictionary.Add(typeof(IInputChannel), 0);
				}
				if (customBinding.CanBuildChannelListener<IReplyChannel>(new object[0]))
				{
					dictionary.Add(typeof(IReplyChannel), 0);
				}
				if (customBinding.CanBuildChannelListener<IDuplexChannel>(new object[0]))
				{
					dictionary.Add(typeof(IDuplexChannel), 0);
				}
				if (customBinding.CanBuildChannelListener<IInputSessionChannel>(new object[0]))
				{
					dictionary.Add(typeof(IInputSessionChannel), 0);
				}
				if (customBinding.CanBuildChannelListener<IReplySessionChannel>(new object[0]))
				{
					dictionary.Add(typeof(IReplySessionChannel), 0);
				}
				if (customBinding.CanBuildChannelListener<IDuplexSessionChannel>(new object[0]))
				{
					dictionary.Add(typeof(IDuplexSessionChannel), 0);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ChannelRequirements.CantCreateListenerException(dictionary.Keys, list, binding.Name));
			}
			return result2;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0008FC90 File Offset: 0x0008DE90
		internal static Type MaybeCreateListener(bool actuallyCreate, Type[] supportedChannels, Binding binding, BindingParameterCollection parameters, Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode, ServiceThrottle throttle, out IChannelListener result)
		{
			return DispatcherBuilder.MaybeCreateListener(actuallyCreate, supportedChannels, binding, parameters, listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, throttle, out result, false);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0008FCB4 File Offset: 0x0008DEB4
		private static Type MaybeCreateListener(bool actuallyCreate, Type[] supportedChannels, Binding binding, BindingParameterCollection parameters, Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode, ServiceThrottle throttle, out IChannelListener result, bool supportContextSession)
		{
			result = null;
			foreach (Type left in supportedChannels)
			{
				if (left == typeof(IInputChannel) && binding.CanBuildChannelListener<IInputChannel>(parameters))
				{
					if (actuallyCreate)
					{
						result = binding.BuildChannelListener<IInputChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
					}
					return typeof(IInputChannel);
				}
				if (left == typeof(IReplyChannel) && binding.CanBuildChannelListener<IReplyChannel>(parameters))
				{
					if (actuallyCreate)
					{
						result = binding.BuildChannelListener<IReplyChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
					}
					return typeof(IReplyChannel);
				}
				if (left == typeof(IDuplexChannel) && binding.CanBuildChannelListener<IDuplexChannel>(parameters))
				{
					if (actuallyCreate)
					{
						result = binding.BuildChannelListener<IDuplexChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
					}
					return typeof(IDuplexChannel);
				}
				if (left == typeof(IInputSessionChannel) && binding.CanBuildChannelListener<IInputSessionChannel>(parameters))
				{
					if (actuallyCreate)
					{
						result = binding.BuildChannelListener<IInputSessionChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
					}
					return typeof(IInputSessionChannel);
				}
				if (left == typeof(IReplySessionChannel) && binding.CanBuildChannelListener<IReplySessionChannel>(parameters))
				{
					if (actuallyCreate)
					{
						result = binding.BuildChannelListener<IReplySessionChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
					}
					return typeof(IReplySessionChannel);
				}
				if (left == typeof(IDuplexSessionChannel) && binding.CanBuildChannelListener<IDuplexSessionChannel>(parameters))
				{
					if (actuallyCreate)
					{
						result = binding.BuildChannelListener<IDuplexSessionChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
					}
					return typeof(IDuplexSessionChannel);
				}
			}
			foreach (Type left2 in supportedChannels)
			{
				if (left2 == typeof(IInputChannel) && binding.CanBuildChannelListener<IInputSessionChannel>(parameters))
				{
					if (actuallyCreate)
					{
						IChannelListener<IInputSessionChannel> inner = binding.BuildChannelListener<IInputSessionChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
						result = DatagramAdapter.GetInputListener(inner, throttle, binding);
					}
					return typeof(IInputSessionChannel);
				}
				if (left2 == typeof(IReplyChannel) && binding.CanBuildChannelListener<IReplySessionChannel>(parameters))
				{
					if (actuallyCreate)
					{
						IChannelListener<IReplySessionChannel> inner2 = binding.BuildChannelListener<IReplySessionChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
						result = DatagramAdapter.GetReplyListener(inner2, throttle, binding);
					}
					return typeof(IReplySessionChannel);
				}
				if (supportContextSession && left2 == typeof(IReplySessionChannel) && binding.CanBuildChannelListener<IReplyChannel>(parameters) && binding.GetProperty<IContextSessionProvider>(parameters) != null)
				{
					if (actuallyCreate)
					{
						result = binding.BuildChannelListener<IReplyChannel>(listenUriBaseAddress, listenUriRelativeAddress, listenUriMode, parameters);
					}
					return typeof(IReplyChannel);
				}
			}
			return null;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0008FF1C File Offset: 0x0008E11C
		private void EnsureThereAreApplicationEndpoints(ServiceDescription description)
		{
			foreach (ServiceEndpoint serviceEndpoint in description.Endpoints)
			{
				if (!serviceEndpoint.InternalIsSystemEndpoint(description))
				{
					return;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ServiceHasZeroAppEndpoints", new object[]
			{
				description.ConfigurationName
			})));
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0008FF98 File Offset: 0x0008E198
		private static Uri EnsureListenUri(ServiceHostBase serviceHost, ServiceEndpoint endpoint)
		{
			Uri uri = endpoint.ListenUri;
			if (uri == null)
			{
				uri = serviceHost.GetVia(endpoint.Binding.Scheme, ServiceHostBase.EmptyUri);
			}
			if (uri == null)
			{
				AspNetEnvironment.Current.ProcessNotMatchedEndpointAddress(uri, endpoint.Binding.Name);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxEndpointNoMatchingScheme", new object[]
				{
					endpoint.Binding.Scheme,
					endpoint.Binding.Name,
					serviceHost.GetBaseAddressSchemes()
				})));
			}
			return uri;
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00090034 File Offset: 0x0008E234
		private void GetBaseAndRelativeAddresses(ServiceHostBase serviceHost, Uri listenUri, string scheme, out Uri listenUriBaseAddress, out string listenUriRelativeAddress)
		{
			listenUriBaseAddress = listenUri;
			listenUriRelativeAddress = string.Empty;
			if (serviceHost.InternalBaseAddresses.Contains(scheme))
			{
				Uri uri = serviceHost.InternalBaseAddresses[scheme];
				if (!uri.AbsoluteUri.EndsWith("/", StringComparison.Ordinal))
				{
					uri = new Uri(uri.AbsoluteUri + "/");
				}
				string text = uri.ToString();
				string text2 = listenUri.ToString();
				if (text2.StartsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					listenUriBaseAddress = uri;
					listenUriRelativeAddress = text2.Substring(text.Length);
				}
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000900BC File Offset: 0x0008E2BC
		private void InitializeServicePerformanceCounters(ServiceHostBase serviceHost)
		{
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				ServicePerformanceCountersBase servicePerformanceCountersBase = PerformanceCountersFactory.CreateServiceCounters(serviceHost);
				if (servicePerformanceCountersBase != null && servicePerformanceCountersBase.Initialized)
				{
					serviceHost.Counters = servicePerformanceCountersBase;
					return;
				}
			}
			else if (PerformanceCounters.MinimalPerformanceCountersEnabled)
			{
				DefaultPerformanceCounters defaultPerformanceCounters = new DefaultPerformanceCounters(serviceHost);
				if (defaultPerformanceCounters.Initialized)
				{
					serviceHost.DefaultCounters = defaultPerformanceCounters;
				}
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00090108 File Offset: 0x0008E308
		internal static BindingParameterCollection GetBindingParameters(ServiceHostBase serviceHost, Collection<ServiceEndpoint> endpoints)
		{
			BindingParameterCollection bindingParameterCollection = new BindingParameterCollection();
			bindingParameterCollection.Add(new ThreadSafeMessageFilterTable<EndpointAddress>());
			foreach (IServiceBehavior serviceBehavior in serviceHost.Description.Behaviors)
			{
				serviceBehavior.AddBindingParameters(serviceHost.Description, serviceHost, endpoints, bindingParameterCollection);
			}
			foreach (ServiceEndpoint endpoint in endpoints)
			{
				DispatcherBuilder.SecurityContractInformationEndpointBehavior.ServerInstance.AddBindingParameters(endpoint, bindingParameterCollection);
				DispatcherBuilder.AddBindingParameters(endpoint, bindingParameterCollection);
			}
			return bindingParameterCollection;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000901BC File Offset: 0x0008E3BC
		internal static DispatcherBuilder.ListenUriInfo GetListenUriInfoForEndpoint(ServiceHostBase host, ServiceEndpoint endpoint)
		{
			Uri listenUri = DispatcherBuilder.EnsureListenUri(host, endpoint);
			return new DispatcherBuilder.ListenUriInfo(listenUri, endpoint.ListenUriMode);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000901E0 File Offset: 0x0008E3E0
		public void InitializeServiceHost(ServiceDescription description, ServiceHostBase serviceHost)
		{
			if (serviceHost.ImplementedContracts != null && serviceHost.ImplementedContracts.Count > 0)
			{
				this.EnsureThereAreApplicationEndpoints(description);
			}
			this.ValidateDescription(description, serviceHost);
			AspNetEnvironment.Current.AddHostingBehavior(serviceHost, description);
			ServiceBehaviorAttribute serviceBehaviorAttribute = description.Behaviors.Find<ServiceBehaviorAttribute>();
			this.InitializeServicePerformanceCounters(serviceHost);
			Dictionary<DispatcherBuilder.ListenUriInfo, DispatcherBuilder.StuffPerListenUriInfo> dictionary = new Dictionary<DispatcherBuilder.ListenUriInfo, DispatcherBuilder.StuffPerListenUriInfo>();
			Dictionary<EndpointAddress, Collection<DispatcherBuilder.EndpointInfo>> dictionary2 = new Dictionary<EndpointAddress, Collection<DispatcherBuilder.EndpointInfo>>();
			for (int i = 0; i < description.Endpoints.Count; i++)
			{
				bool flag = false;
				ServiceEndpoint serviceEndpoint = description.Endpoints[i];
				foreach (OperationDescription operationDescription in serviceEndpoint.Contract.Operations)
				{
					if (operationDescription.Behaviors.Find<ReceiveContextEnabledAttribute>() != null)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					IReceiveContextSettings property = serviceEndpoint.Binding.GetProperty<IReceiveContextSettings>(new BindingParameterCollection());
					if (property == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxReceiveContextSettingsPropertyMissing", new object[]
						{
							serviceEndpoint.Contract.Name,
							typeof(ReceiveContextEnabledAttribute).Name,
							serviceEndpoint.Address.Uri.AbsoluteUri,
							typeof(IReceiveContextSettings).Name
						})));
					}
					property.Enabled = true;
				}
				DispatcherBuilder.ListenUriInfo listenUriInfoForEndpoint = DispatcherBuilder.GetListenUriInfoForEndpoint(serviceHost, serviceEndpoint);
				if (!dictionary.ContainsKey(listenUriInfoForEndpoint))
				{
					dictionary.Add(listenUriInfoForEndpoint, new DispatcherBuilder.StuffPerListenUriInfo());
				}
				dictionary[listenUriInfoForEndpoint].Endpoints.Add(serviceEndpoint);
			}
			foreach (KeyValuePair<DispatcherBuilder.ListenUriInfo, DispatcherBuilder.StuffPerListenUriInfo> keyValuePair in dictionary)
			{
				Uri listenUri = keyValuePair.Key.ListenUri;
				ListenUriMode listenUriMode = keyValuePair.Key.ListenUriMode;
				BindingParameterCollection parameters = keyValuePair.Value.Parameters;
				Binding binding = keyValuePair.Value.Endpoints[0].Binding;
				EndpointIdentity identity = keyValuePair.Value.Endpoints[0].Address.Identity;
				ThreadSafeMessageFilterTable<EndpointAddress> threadSafeMessageFilterTable = new ThreadSafeMessageFilterTable<EndpointAddress>();
				parameters.Add(threadSafeMessageFilterTable);
				bool supportContextSession = false;
				foreach (IServiceBehavior serviceBehavior in description.Behaviors)
				{
					if (serviceBehavior is IContextSessionProvider)
					{
						supportContextSession = true;
					}
					serviceBehavior.AddBindingParameters(description, serviceHost, keyValuePair.Value.Endpoints, parameters);
				}
				for (int j = 0; j < keyValuePair.Value.Endpoints.Count; j++)
				{
					ServiceEndpoint serviceEndpoint2 = keyValuePair.Value.Endpoints[j];
					string absoluteUri = listenUri.AbsoluteUri;
					if (serviceEndpoint2.Binding != binding)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ABindingInstanceHasAlreadyBeenAssociatedTo1", new object[]
						{
							absoluteUri
						})));
					}
					if (!object.Equals(serviceEndpoint2.Address.Identity, identity))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxWhenMultipleEndpointsShareAListenUriTheyMustHaveSameIdentity", new object[]
						{
							absoluteUri
						})));
					}
					DispatcherBuilder.AddMsmqIntegrationContractInformation(serviceEndpoint2);
					DispatcherBuilder.SecurityContractInformationEndpointBehavior.ServerInstance.AddBindingParameters(serviceEndpoint2, parameters);
					DispatcherBuilder.AddBindingParameters(serviceEndpoint2, parameters);
				}
				IChannelListener listener;
				Type type = this.BuildChannelListener(keyValuePair.Value, serviceHost, listenUri, listenUriMode, supportContextSession, out listener);
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(binding.Name, binding.Namespace);
				ChannelDispatcher channelDispatcher = new ChannelDispatcher(listener, xmlQualifiedName.ToString(), binding);
				channelDispatcher.SetEndpointAddressTable(threadSafeMessageFilterTable);
				keyValuePair.Value.ChannelDispatcher = channelDispatcher;
				bool flag2 = false;
				int num = int.MaxValue;
				for (int k = 0; k < keyValuePair.Value.Endpoints.Count; k++)
				{
					ServiceEndpoint serviceEndpoint3 = keyValuePair.Value.Endpoints[k];
					string absoluteUri2 = listenUri.AbsoluteUri;
					EndpointFilterProvider provider = new EndpointFilterProvider(new string[0]);
					EndpointDispatcher endpointDispatcher = DispatcherBuilder.BuildDispatcher(serviceHost, description, serviceEndpoint3, serviceEndpoint3.Contract, provider);
					for (int l = 0; l < serviceEndpoint3.Contract.Operations.Count; l++)
					{
						OperationDescription operationDescription2 = serviceEndpoint3.Contract.Operations[l];
						OperationBehaviorAttribute operationBehaviorAttribute = operationDescription2.Behaviors.Find<OperationBehaviorAttribute>();
						if (operationBehaviorAttribute != null && operationBehaviorAttribute.TransactionScopeRequired)
						{
							flag2 = true;
							break;
						}
					}
					if (!dictionary2.ContainsKey(serviceEndpoint3.Address))
					{
						dictionary2.Add(serviceEndpoint3.Address, new Collection<DispatcherBuilder.EndpointInfo>());
					}
					dictionary2[serviceEndpoint3.Address].Add(new DispatcherBuilder.EndpointInfo(serviceEndpoint3, endpointDispatcher, provider));
					channelDispatcher.Endpoints.Add(endpointDispatcher);
					TransactedBatchingBehavior transactedBatchingBehavior = serviceEndpoint3.Behaviors.Find<TransactedBatchingBehavior>();
					if (transactedBatchingBehavior == null)
					{
						num = 0;
					}
					else
					{
						if (!flag2)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqBatchRequiresTransactionScope")));
						}
						num = Math.Min(num, transactedBatchingBehavior.MaxBatchSize);
					}
					if (PerformanceCounters.PerformanceCountersEnabled || PerformanceCounters.MinimalPerformanceCountersEnabled)
					{
						PerformanceCounters.AddPerformanceCountersForEndpoint(serviceHost, serviceEndpoint3.Contract, endpointDispatcher);
					}
				}
				if ((PerformanceCounters.PerformanceCountersEnabled || PerformanceCounters.MinimalPerformanceCountersEnabled) && ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
				{
					PerformanceCounter.CloseSharedResources();
				}
				if (flag2)
				{
					BindingElementCollection bindingElementCollection = binding.CreateBindingElements();
					foreach (BindingElement bindingElement in bindingElementCollection)
					{
						ITransactedBindingElement transactedBindingElement = bindingElement as ITransactedBindingElement;
						if (transactedBindingElement != null && transactedBindingElement.TransactedReceiveEnabled)
						{
							channelDispatcher.IsTransactedReceive = true;
							channelDispatcher.MaxTransactedBatchSize = num;
							break;
						}
					}
				}
				IReceiveContextSettings property2 = binding.GetProperty<IReceiveContextSettings>(new BindingParameterCollection());
				if (property2 != null)
				{
					channelDispatcher.ReceiveContextEnabled = property2.Enabled;
				}
				serviceHost.ChannelDispatchers.Add(channelDispatcher);
			}
			for (int m = 0; m < description.Behaviors.Count; m++)
			{
				IServiceBehavior serviceBehavior2 = description.Behaviors[m];
				serviceBehavior2.ApplyDispatchBehavior(description, serviceHost);
			}
			foreach (KeyValuePair<DispatcherBuilder.ListenUriInfo, DispatcherBuilder.StuffPerListenUriInfo> keyValuePair2 in dictionary)
			{
				for (int n = 0; n < keyValuePair2.Value.Endpoints.Count; n++)
				{
					ServiceEndpoint serviceEndpoint4 = keyValuePair2.Value.Endpoints[n];
					Collection<DispatcherBuilder.EndpointInfo> collection = dictionary2[serviceEndpoint4.Address];
					DispatcherBuilder.EndpointInfo endpointInfo = null;
					foreach (DispatcherBuilder.EndpointInfo endpointInfo2 in collection)
					{
						if (endpointInfo2.Endpoint == serviceEndpoint4)
						{
							endpointInfo = endpointInfo2;
							break;
						}
					}
					EndpointDispatcher endpointDispatcher2 = endpointInfo.EndpointDispatcher;
					for (int num2 = 0; num2 < serviceEndpoint4.Contract.Behaviors.Count; num2++)
					{
						IContractBehavior contractBehavior = serviceEndpoint4.Contract.Behaviors[num2];
						contractBehavior.ApplyDispatchBehavior(serviceEndpoint4.Contract, serviceEndpoint4, endpointDispatcher2.DispatchRuntime);
					}
					DispatcherBuilder.BindingInformationEndpointBehavior.Instance.ApplyDispatchBehavior(serviceEndpoint4, endpointDispatcher2);
					DispatcherBuilder.TransactionContractInformationEndpointBehavior.Instance.ApplyDispatchBehavior(serviceEndpoint4, endpointDispatcher2);
					for (int num3 = 0; num3 < serviceEndpoint4.Behaviors.Count; num3++)
					{
						IEndpointBehavior endpointBehavior = serviceEndpoint4.Behaviors[num3];
						endpointBehavior.ApplyDispatchBehavior(serviceEndpoint4, endpointDispatcher2);
					}
					DispatcherBuilder.BindOperations(serviceEndpoint4.Contract, null, endpointDispatcher2.DispatchRuntime);
				}
			}
			this.EnsureRequiredRuntimeProperties(dictionary2);
			foreach (Collection<DispatcherBuilder.EndpointInfo> collection2 in dictionary2.Values)
			{
				if (collection2.Count > 1)
				{
					for (int num4 = 0; num4 < collection2.Count; num4++)
					{
						for (int num5 = num4 + 1; num5 < collection2.Count; num5++)
						{
							if (collection2[num4].EndpointDispatcher.ChannelDispatcher == collection2[num5].EndpointDispatcher.ChannelDispatcher)
							{
								EndpointFilterProvider filterProvider = collection2[num4].FilterProvider;
								EndpointFilterProvider filterProvider2 = collection2[num5].FilterProvider;
								string text;
								if (filterProvider != null && filterProvider2 != null && DispatcherBuilder.HaveCommonInitiatingActions(filterProvider, filterProvider2, out text))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDuplicateInitiatingActionAtSameVia", new object[]
									{
										collection2[num4].Endpoint.ListenUri,
										text
									})));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00090B1C File Offset: 0x0008ED1C
		private void EnsureRequiredRuntimeProperties(Dictionary<EndpointAddress, Collection<DispatcherBuilder.EndpointInfo>> endpointInfosPerEndpointAddress)
		{
			foreach (Collection<DispatcherBuilder.EndpointInfo> collection in endpointInfosPerEndpointAddress.Values)
			{
				for (int i = 0; i < collection.Count; i++)
				{
					DispatchRuntime dispatchRuntime = collection[i].EndpointDispatcher.DispatchRuntime;
					if (dispatchRuntime.InstanceContextProvider == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxRequiredRuntimePropertyMissing", new object[]
						{
							"InstanceContextProvider"
						})));
					}
				}
			}
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x00090BBC File Offset: 0x0008EDBC
		private static EndpointDispatcher BuildDispatcher(ServiceHostBase service, ServiceDescription serviceDescription, ServiceEndpoint endpoint, ContractDescription contractDescription, EndpointFilterProvider provider)
		{
			if (service == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("service");
			}
			if (serviceDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceDescription");
			}
			if (contractDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractDescription");
			}
			EndpointAddress address = endpoint.Address;
			EndpointDispatcher endpointDispatcher = new EndpointDispatcher(address, contractDescription.Name, contractDescription.Namespace, endpoint.Id, endpoint.InternalIsSystemEndpoint(serviceDescription));
			DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
			if (contractDescription.CallbackContractType != null)
			{
				dispatchRuntime.CallbackClientRuntime.CallbackClientType = contractDescription.CallbackContractType;
				dispatchRuntime.CallbackClientRuntime.ContractClientType = contractDescription.ContractType;
			}
			for (int i = 0; i < contractDescription.Operations.Count; i++)
			{
				OperationDescription operationDescription = contractDescription.Operations[i];
				if (!operationDescription.IsServerInitiated())
				{
					DispatcherBuilder.BuildDispatchOperation(operationDescription, dispatchRuntime, provider);
				}
				else
				{
					DispatcherBuilder.BuildProxyOperation(operationDescription, dispatchRuntime.CallbackClientRuntime);
				}
			}
			int filterPriority = 0;
			endpointDispatcher.ContractFilter = provider.CreateFilter(out filterPriority);
			endpointDispatcher.FilterPriority = filterPriority;
			return endpointDispatcher;
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00090CC4 File Offset: 0x0008EEC4
		private static void BuildProxyOperation(OperationDescription operation, ClientRuntime parent)
		{
			ClientOperation clientOperation;
			if (operation.Messages.Count == 1)
			{
				clientOperation = new ClientOperation(parent, operation.Name, operation.Messages[0].Action);
			}
			else
			{
				clientOperation = new ClientOperation(parent, operation.Name, operation.Messages[0].Action, operation.Messages[1].Action);
			}
			clientOperation.TaskMethod = operation.TaskMethod;
			clientOperation.TaskTResult = operation.TaskTResult;
			clientOperation.SyncMethod = operation.SyncMethod;
			clientOperation.BeginMethod = operation.BeginMethod;
			clientOperation.EndMethod = operation.EndMethod;
			clientOperation.IsOneWay = operation.IsOneWay;
			clientOperation.IsTerminating = operation.IsTerminating;
			clientOperation.IsInitiating = operation.IsInitiating;
			clientOperation.IsSessionOpenNotificationEnabled = operation.IsSessionOpenNotificationEnabled;
			for (int i = 0; i < operation.Faults.Count; i++)
			{
				FaultDescription faultDescription = operation.Faults[i];
				clientOperation.FaultContractInfos.Add(new FaultContractInfo(faultDescription.Action, faultDescription.DetailType, faultDescription.ElementName, faultDescription.Namespace, operation.KnownTypes));
			}
			parent.Operations.Add(clientOperation);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x00090DF8 File Offset: 0x0008EFF8
		private static void BuildDispatchOperation(OperationDescription operation, DispatchRuntime parent, EndpointFilterProvider provider)
		{
			string action = operation.Messages[0].Action;
			DispatchOperation dispatchOperation;
			if (operation.IsOneWay)
			{
				dispatchOperation = new DispatchOperation(parent, operation.Name, action);
			}
			else
			{
				string action2 = operation.Messages[1].Action;
				dispatchOperation = new DispatchOperation(parent, operation.Name, action, action2);
			}
			dispatchOperation.HasNoDisposableParameters = operation.HasNoDisposableParameters;
			dispatchOperation.IsTerminating = operation.IsTerminating;
			dispatchOperation.IsSessionOpenNotificationEnabled = operation.IsSessionOpenNotificationEnabled;
			for (int i = 0; i < operation.Faults.Count; i++)
			{
				FaultDescription faultDescription = operation.Faults[i];
				dispatchOperation.FaultContractInfos.Add(new FaultContractInfo(faultDescription.Action, faultDescription.DetailType, faultDescription.ElementName, faultDescription.Namespace, operation.KnownTypes));
			}
			dispatchOperation.IsInsideTransactedReceiveScope = operation.IsInsideTransactedReceiveScope;
			if (provider != null && operation.IsInitiating)
			{
				provider.InitiatingActions.Add(action);
			}
			if (action != "*")
			{
				parent.Operations.Add(dispatchOperation);
				return;
			}
			if (parent.HasMatchAllOperation)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMultipleContractStarOperations0")));
			}
			parent.UnhandledDispatchOperation = dispatchOperation;
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00090F34 File Offset: 0x0008F134
		private static void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime)
		{
			ContractDescription contract = serviceEndpoint.Contract;
			for (int i = 0; i < contract.Behaviors.Count; i++)
			{
				IContractBehavior contractBehavior = contract.Behaviors[i];
				contractBehavior.ApplyClientBehavior(contract, serviceEndpoint, clientRuntime);
			}
			DispatcherBuilder.BindingInformationEndpointBehavior.Instance.ApplyClientBehavior(serviceEndpoint, clientRuntime);
			DispatcherBuilder.TransactionContractInformationEndpointBehavior.Instance.ApplyClientBehavior(serviceEndpoint, clientRuntime);
			for (int j = 0; j < serviceEndpoint.Behaviors.Count; j++)
			{
				IEndpointBehavior endpointBehavior = serviceEndpoint.Behaviors[j];
				endpointBehavior.ApplyClientBehavior(serviceEndpoint, clientRuntime);
			}
			DispatcherBuilder.BindOperations(contract, clientRuntime, null);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00090FC4 File Offset: 0x0008F1C4
		private static void BindOperations(ContractDescription contract, ClientRuntime proxy, DispatchRuntime dispatch)
		{
			if (proxy == null == (dispatch == null))
			{
				throw Fx.AssertAndThrowFatal("DispatcherBuilder.BindOperations: ((proxy == null) != (dispatch == null))");
			}
			MessageDirection messageDirection = (proxy == null) ? MessageDirection.Input : MessageDirection.Output;
			for (int i = 0; i < contract.Operations.Count; i++)
			{
				OperationDescription operationDescription = contract.Operations[i];
				MessageDescription messageDescription = operationDescription.Messages[0];
				if (messageDescription.Direction != messageDirection)
				{
					if (proxy == null)
					{
						proxy = dispatch.CallbackClientRuntime;
					}
					ClientOperation clientOperation = proxy.Operations[operationDescription.Name];
					for (int j = 0; j < operationDescription.Behaviors.Count; j++)
					{
						IOperationBehavior operationBehavior = operationDescription.Behaviors[j];
						operationBehavior.ApplyClientBehavior(operationDescription, clientOperation);
					}
				}
				else
				{
					if (dispatch == null)
					{
						dispatch = proxy.CallbackDispatchRuntime;
					}
					DispatchOperation dispatchOperation = null;
					if (dispatch.Operations.Contains(operationDescription.Name))
					{
						dispatchOperation = dispatch.Operations[operationDescription.Name];
					}
					if (dispatchOperation == null && dispatch.UnhandledDispatchOperation != null && dispatch.UnhandledDispatchOperation.Name == operationDescription.Name)
					{
						dispatchOperation = dispatch.UnhandledDispatchOperation;
					}
					if (dispatchOperation != null)
					{
						for (int k = 0; k < operationDescription.Behaviors.Count; k++)
						{
							IOperationBehavior operationBehavior2 = operationDescription.Behaviors[k];
							operationBehavior2.ApplyDispatchBehavior(operationDescription, dispatchOperation);
						}
					}
				}
			}
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0009111C File Offset: 0x0008F31C
		internal static Type[] GetSupportedChannelTypes(ContractDescription contractDescription)
		{
			if (contractDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contractDescription"));
			}
			ChannelRequirements channelRequirements;
			ChannelRequirements.ComputeContractRequirements(contractDescription, out channelRequirements);
			Type[] array = ChannelRequirements.ComputeRequiredChannels(ref channelRequirements);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == typeof(IRequestChannel))
				{
					array[i] = typeof(IReplyChannel);
				}
				else if (array[i] == typeof(IRequestSessionChannel))
				{
					array[i] = typeof(IReplySessionChannel);
				}
				else if (array[i] == typeof(IOutputChannel))
				{
					array[i] = typeof(IInputChannel);
				}
				else if (array[i] == typeof(IOutputSessionChannel))
				{
					array[i] = typeof(IInputSessionChannel);
				}
				else if (!(array[i] == typeof(IDuplexChannel)) && !(array[i] == typeof(IDuplexSessionChannel)))
				{
					throw Fx.AssertAndThrowFatal("DispatcherBuilder.GetSupportedChannelTypes: Unexpected channel type");
				}
			}
			return array;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00091228 File Offset: 0x0008F428
		private static bool HaveCommonInitiatingActions(EndpointFilterProvider x, EndpointFilterProvider y, out string commonAction)
		{
			commonAction = null;
			foreach (string text in x.InitiatingActions)
			{
				if (y.InitiatingActions.Contains(text))
				{
					commonAction = text;
					return true;
				}
			}
			return false;
		}

		// Token: 0x02000BB2 RID: 2994
		private class EndpointInfo
		{
			// Token: 0x0600742F RID: 29743 RVA: 0x001B1F24 File Offset: 0x001B0124
			public EndpointInfo(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher, EndpointFilterProvider provider)
			{
				this.endpoint = endpoint;
				this.endpointDispatcher = endpointDispatcher;
				this.provider = provider;
			}

			// Token: 0x17001ADC RID: 6876
			// (get) Token: 0x06007430 RID: 29744 RVA: 0x001B1F41 File Offset: 0x001B0141
			public ServiceEndpoint Endpoint
			{
				get
				{
					return this.endpoint;
				}
			}

			// Token: 0x17001ADD RID: 6877
			// (get) Token: 0x06007431 RID: 29745 RVA: 0x001B1F49 File Offset: 0x001B0149
			public EndpointFilterProvider FilterProvider
			{
				get
				{
					return this.provider;
				}
			}

			// Token: 0x17001ADE RID: 6878
			// (get) Token: 0x06007432 RID: 29746 RVA: 0x001B1F51 File Offset: 0x001B0151
			public EndpointDispatcher EndpointDispatcher
			{
				get
				{
					return this.endpointDispatcher;
				}
			}

			// Token: 0x040041CE RID: 16846
			private ServiceEndpoint endpoint;

			// Token: 0x040041CF RID: 16847
			private EndpointDispatcher endpointDispatcher;

			// Token: 0x040041D0 RID: 16848
			private EndpointFilterProvider provider;
		}

		// Token: 0x02000BB3 RID: 2995
		internal class ListenUriInfo
		{
			// Token: 0x06007433 RID: 29747 RVA: 0x001B1F59 File Offset: 0x001B0159
			public ListenUriInfo(Uri listenUri, ListenUriMode listenUriMode)
			{
				this.listenUri = listenUri;
				this.listenUriMode = listenUriMode;
			}

			// Token: 0x17001ADF RID: 6879
			// (get) Token: 0x06007434 RID: 29748 RVA: 0x001B1F6F File Offset: 0x001B016F
			public Uri ListenUri
			{
				get
				{
					return this.listenUri;
				}
			}

			// Token: 0x17001AE0 RID: 6880
			// (get) Token: 0x06007435 RID: 29749 RVA: 0x001B1F77 File Offset: 0x001B0177
			public ListenUriMode ListenUriMode
			{
				get
				{
					return this.listenUriMode;
				}
			}

			// Token: 0x06007436 RID: 29750 RVA: 0x001B1F7F File Offset: 0x001B017F
			public override bool Equals(object other)
			{
				return this.Equals(other as DispatcherBuilder.ListenUriInfo);
			}

			// Token: 0x06007437 RID: 29751 RVA: 0x001B1F8D File Offset: 0x001B018D
			public bool Equals(DispatcherBuilder.ListenUriInfo other)
			{
				return other != null && (this == other || (this.listenUriMode == other.listenUriMode && EndpointAddress.UriEquals(this.listenUri, other.listenUri, true, true)));
			}

			// Token: 0x06007438 RID: 29752 RVA: 0x001B1FBD File Offset: 0x001B01BD
			public override int GetHashCode()
			{
				return EndpointAddress.UriGetHashCode(this.listenUri, true);
			}

			// Token: 0x040041D1 RID: 16849
			private Uri listenUri;

			// Token: 0x040041D2 RID: 16850
			private ListenUriMode listenUriMode;
		}

		// Token: 0x02000BB4 RID: 2996
		private class StuffPerListenUriInfo
		{
			// Token: 0x040041D3 RID: 16851
			public BindingParameterCollection Parameters = new BindingParameterCollection();

			// Token: 0x040041D4 RID: 16852
			public Collection<ServiceEndpoint> Endpoints = new Collection<ServiceEndpoint>();

			// Token: 0x040041D5 RID: 16853
			public ChannelDispatcher ChannelDispatcher;
		}

		// Token: 0x02000BB5 RID: 2997
		private class BindingInformationEndpointBehavior : IEndpointBehavior
		{
			// Token: 0x17001AE1 RID: 6881
			// (get) Token: 0x0600743A RID: 29754 RVA: 0x001B1FE9 File Offset: 0x001B01E9
			public static DispatcherBuilder.BindingInformationEndpointBehavior Instance
			{
				get
				{
					if (DispatcherBuilder.BindingInformationEndpointBehavior.instance == null)
					{
						DispatcherBuilder.BindingInformationEndpointBehavior.instance = new DispatcherBuilder.BindingInformationEndpointBehavior();
					}
					return DispatcherBuilder.BindingInformationEndpointBehavior.instance;
				}
			}

			// Token: 0x0600743B RID: 29755 RVA: 0x001B2001 File Offset: 0x001B0201
			public void Validate(ServiceEndpoint serviceEndpoint)
			{
			}

			// Token: 0x0600743C RID: 29756 RVA: 0x001B2003 File Offset: 0x001B0203
			public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection parameters)
			{
			}

			// Token: 0x0600743D RID: 29757 RVA: 0x001B2008 File Offset: 0x001B0208
			public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
			{
				behavior.ManualAddressing = this.IsManualAddressing(serviceEndpoint.Binding);
				behavior.EnableFaults = !this.IsMulticast(serviceEndpoint.Binding);
				if (serviceEndpoint.Contract.IsDuplex())
				{
					behavior.CallbackDispatchRuntime.ChannelDispatcher.MessageVersion = serviceEndpoint.Binding.MessageVersion;
				}
			}

			// Token: 0x0600743E RID: 29758 RVA: 0x001B2064 File Offset: 0x001B0264
			public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
			{
				IBindingRuntimePreferences bindingRuntimePreferences = serviceEndpoint.Binding as IBindingRuntimePreferences;
				if (bindingRuntimePreferences != null)
				{
					endpointDispatcher.ChannelDispatcher.ReceiveSynchronously = bindingRuntimePreferences.ReceiveSynchronously;
				}
				endpointDispatcher.ChannelDispatcher.ManualAddressing = this.IsManualAddressing(serviceEndpoint.Binding);
				endpointDispatcher.ChannelDispatcher.EnableFaults = !this.IsMulticast(serviceEndpoint.Binding);
				endpointDispatcher.ChannelDispatcher.MessageVersion = serviceEndpoint.Binding.MessageVersion;
			}

			// Token: 0x0600743F RID: 29759 RVA: 0x001B20D8 File Offset: 0x001B02D8
			private bool IsManualAddressing(Binding binding)
			{
				TransportBindingElement transportBindingElement = binding.CreateBindingElements().Find<TransportBindingElement>();
				if (transportBindingElement == null)
				{
					string @string = SR.GetString("SFxBindingMustContainTransport2", new object[]
					{
						binding.Name,
						binding.Namespace
					});
					Exception exception = new InvalidOperationException(@string);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
				}
				return transportBindingElement.ManualAddressing;
			}

			// Token: 0x06007440 RID: 29760 RVA: 0x001B2130 File Offset: 0x001B0330
			private bool IsMulticast(Binding binding)
			{
				IBindingMulticastCapabilities property = binding.GetProperty<IBindingMulticastCapabilities>(new BindingParameterCollection());
				return property != null && property.IsMulticast;
			}

			// Token: 0x040041D6 RID: 16854
			private static DispatcherBuilder.BindingInformationEndpointBehavior instance;
		}

		// Token: 0x02000BB6 RID: 2998
		private class TransactionContractInformationEndpointBehavior : IEndpointBehavior
		{
			// Token: 0x17001AE2 RID: 6882
			// (get) Token: 0x06007442 RID: 29762 RVA: 0x001B215C File Offset: 0x001B035C
			public static DispatcherBuilder.TransactionContractInformationEndpointBehavior Instance
			{
				get
				{
					if (DispatcherBuilder.TransactionContractInformationEndpointBehavior.instance == null)
					{
						DispatcherBuilder.TransactionContractInformationEndpointBehavior.instance = new DispatcherBuilder.TransactionContractInformationEndpointBehavior();
					}
					return DispatcherBuilder.TransactionContractInformationEndpointBehavior.instance;
				}
			}

			// Token: 0x06007443 RID: 29763 RVA: 0x001B2174 File Offset: 0x001B0374
			public void Validate(ServiceEndpoint serviceEndpoint)
			{
			}

			// Token: 0x06007444 RID: 29764 RVA: 0x001B2176 File Offset: 0x001B0376
			public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection parameters)
			{
			}

			// Token: 0x06007445 RID: 29765 RVA: 0x001B2178 File Offset: 0x001B0378
			public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
			{
				behavior.AddTransactionFlowProperties = DispatcherBuilder.TransactionContractInformationEndpointBehavior.UsesTransactionFlowProperties(serviceEndpoint.Binding.CreateBindingElements(), serviceEndpoint.Contract);
			}

			// Token: 0x06007446 RID: 29766 RVA: 0x001B2196 File Offset: 0x001B0396
			public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
			{
				endpointDispatcher.DispatchRuntime.IgnoreTransactionMessageProperty = !DispatcherBuilder.TransactionContractInformationEndpointBehavior.UsesTransactionFlowProperties(serviceEndpoint.Binding.CreateBindingElements(), serviceEndpoint.Contract);
			}

			// Token: 0x06007447 RID: 29767 RVA: 0x001B21BC File Offset: 0x001B03BC
			private static bool UsesTransactionFlowProperties(BindingElementCollection bindingElements, ContractDescription contract)
			{
				BindingElementCollection bindingElementCollection = new BindingElementCollection(bindingElements);
				TransactionFlowBindingElement transactionFlowBindingElement = bindingElementCollection.Find<TransactionFlowBindingElement>();
				return transactionFlowBindingElement != null && transactionFlowBindingElement.IsFlowEnabled(contract);
			}

			// Token: 0x040041D7 RID: 16855
			private static DispatcherBuilder.TransactionContractInformationEndpointBehavior instance;
		}

		// Token: 0x02000BB7 RID: 2999
		private class SecurityContractInformationEndpointBehavior : IEndpointBehavior
		{
			// Token: 0x06007449 RID: 29769 RVA: 0x001B21EB File Offset: 0x001B03EB
			private SecurityContractInformationEndpointBehavior(bool isForClient)
			{
				this.isForClient = isForClient;
			}

			// Token: 0x17001AE3 RID: 6883
			// (get) Token: 0x0600744A RID: 29770 RVA: 0x001B21FA File Offset: 0x001B03FA
			public static DispatcherBuilder.SecurityContractInformationEndpointBehavior ServerInstance
			{
				get
				{
					if (DispatcherBuilder.SecurityContractInformationEndpointBehavior.serverInstance == null)
					{
						DispatcherBuilder.SecurityContractInformationEndpointBehavior.serverInstance = new DispatcherBuilder.SecurityContractInformationEndpointBehavior(false);
					}
					return DispatcherBuilder.SecurityContractInformationEndpointBehavior.serverInstance;
				}
			}

			// Token: 0x17001AE4 RID: 6884
			// (get) Token: 0x0600744B RID: 29771 RVA: 0x001B2213 File Offset: 0x001B0413
			public static DispatcherBuilder.SecurityContractInformationEndpointBehavior ClientInstance
			{
				get
				{
					if (DispatcherBuilder.SecurityContractInformationEndpointBehavior.clientInstance == null)
					{
						DispatcherBuilder.SecurityContractInformationEndpointBehavior.clientInstance = new DispatcherBuilder.SecurityContractInformationEndpointBehavior(true);
					}
					return DispatcherBuilder.SecurityContractInformationEndpointBehavior.clientInstance;
				}
			}

			// Token: 0x0600744C RID: 29772 RVA: 0x001B222C File Offset: 0x001B042C
			public void Validate(ServiceEndpoint serviceEndpoint)
			{
			}

			// Token: 0x0600744D RID: 29773 RVA: 0x001B222E File Offset: 0x001B042E
			public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
			{
			}

			// Token: 0x0600744E RID: 29774 RVA: 0x001B2230 File Offset: 0x001B0430
			public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
			{
			}

			// Token: 0x0600744F RID: 29775 RVA: 0x001B2234 File Offset: 0x001B0434
			public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection parameters)
			{
				ISecurityCapabilities securityCapabilities = null;
				BindingElementCollection bindingElementCollection = endpoint.Binding.CreateBindingElements();
				for (int i = 0; i < bindingElementCollection.Count; i++)
				{
					if (!(bindingElementCollection[i] is ITransportTokenAssertionProvider))
					{
						ISecurityCapabilities individualProperty = bindingElementCollection[i].GetIndividualProperty<ISecurityCapabilities>();
						if (individualProperty != null)
						{
							securityCapabilities = individualProperty;
							break;
						}
					}
				}
				if (securityCapabilities != null)
				{
					ChannelProtectionRequirements channelProtectionRequirements = parameters.Find<ChannelProtectionRequirements>();
					if (channelProtectionRequirements == null)
					{
						channelProtectionRequirements = new ChannelProtectionRequirements();
						parameters.Add(channelProtectionRequirements);
					}
					MessageEncodingBindingElement messageEncodingBindingElement = bindingElementCollection.Find<MessageEncodingBindingElement>();
					if (messageEncodingBindingElement != null && messageEncodingBindingElement.MessageVersion.Addressing == AddressingVersion.None)
					{
						channelProtectionRequirements.Add(ChannelProtectionRequirements.CreateFromContractAndUnionResponseProtectionRequirements(endpoint.Contract, securityCapabilities, this.isForClient));
						return;
					}
					channelProtectionRequirements.Add(ChannelProtectionRequirements.CreateFromContract(endpoint.Contract, securityCapabilities, this.isForClient));
				}
			}

			// Token: 0x040041D8 RID: 16856
			private bool isForClient;

			// Token: 0x040041D9 RID: 16857
			private static DispatcherBuilder.SecurityContractInformationEndpointBehavior serverInstance;

			// Token: 0x040041DA RID: 16858
			private static DispatcherBuilder.SecurityContractInformationEndpointBehavior clientInstance;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Activation;
using System.ServiceModel.Administration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x02000100 RID: 256
	public abstract class ServiceHostBase : CommunicationObject, IExtensibleObject<ServiceHostBase>, IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600054B RID: 1355 RVA: 0x00018490 File Offset: 0x00016690
		// (remove) Token: 0x0600054C RID: 1356 RVA: 0x000184C8 File Offset: 0x000166C8
		internal event EventHandler BusyCountIncremented;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600054D RID: 1357 RVA: 0x00018500 File Offset: 0x00016700
		// (remove) Token: 0x0600054E RID: 1358 RVA: 0x00018538 File Offset: 0x00016738
		public event EventHandler<UnknownMessageReceivedEventArgs> UnknownMessageReceived;

		// Token: 0x0600054F RID: 1359 RVA: 0x00018570 File Offset: 0x00016770
		protected ServiceHostBase()
		{
			TraceUtility.SetEtwProviderId();
			this.baseAddresses = new UriSchemeKeyedCollection(base.ThisLock);
			this.channelDispatchers = new ChannelDispatcherCollection(this, base.ThisLock);
			this.extensions = new ExtensionCollection<ServiceHostBase>(this, base.ThisLock);
			this.instances = new InstanceContextManager(base.ThisLock);
			this.serviceThrottle = new ServiceThrottle(this);
			base.TraceOpenAndClose = true;
			base.Faulted += this.OnServiceHostFaulted;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00018609 File Offset: 0x00016809
		internal EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null)
				{
					this.eventTraceActivity = new EventTraceActivity(false);
				}
				return this.eventTraceActivity;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00018625 File Offset: 0x00016825
		public ServiceAuthorizationBehavior Authorization
		{
			get
			{
				if (this.Description == null)
				{
					return null;
				}
				if (base.State == CommunicationState.Created || base.State == CommunicationState.Opening)
				{
					return this.EnsureAuthorization(this.Description);
				}
				return this.readOnlyAuthorization;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00018655 File Offset: 0x00016855
		public ServiceAuthenticationBehavior Authentication
		{
			get
			{
				if (this.Description == null)
				{
					return null;
				}
				if (base.State == CommunicationState.Created || base.State == CommunicationState.Opening)
				{
					return this.EnsureAuthentication(this.Description);
				}
				return this.readOnlyAuthentication;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00018685 File Offset: 0x00016885
		public ReadOnlyCollection<Uri> BaseAddresses
		{
			get
			{
				this.externalBaseAddresses = new ReadOnlyCollection<Uri>(new List<Uri>(this.baseAddresses));
				return this.externalBaseAddresses;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000186A3 File Offset: 0x000168A3
		public ChannelDispatcherCollection ChannelDispatchers
		{
			get
			{
				return this.channelDispatchers;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000186AB File Offset: 0x000168AB
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x000186B4 File Offset: 0x000168B4
		public TimeSpan CloseTimeout
		{
			get
			{
				return this.closeTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					string @string = SR.GetString("SFxTimeoutOutOfRange0");
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", @string));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfClosedOrOpened();
					this.closeTimeout = value;
				}
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00018750 File Offset: 0x00016950
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00018758 File Offset: 0x00016958
		internal ServicePerformanceCountersBase Counters
		{
			get
			{
				return this.servicePerformanceCounters;
			}
			set
			{
				this.servicePerformanceCounters = value;
				this.serviceThrottle.SetServicePerformanceCounters(this.servicePerformanceCounters);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00018772 File Offset: 0x00016972
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001877A File Offset: 0x0001697A
		internal DefaultPerformanceCounters DefaultCounters
		{
			get
			{
				return this.defaultPerformanceCounters;
			}
			set
			{
				this.defaultPerformanceCounters = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00018783 File Offset: 0x00016983
		public ServiceCredentials Credentials
		{
			get
			{
				if (this.Description == null)
				{
					return null;
				}
				if (base.State == CommunicationState.Created || base.State == CommunicationState.Opening)
				{
					return this.EnsureCredentials(this.Description);
				}
				return this.readOnlyCredentials;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000187B3 File Offset: 0x000169B3
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.CloseTimeout;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000187BB File Offset: 0x000169BB
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.OpenTimeout;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000187C3 File Offset: 0x000169C3
		public ServiceDescription Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000187CB File Offset: 0x000169CB
		public IExtensionCollection<ServiceHostBase> Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000187D3 File Offset: 0x000169D3
		protected internal IDictionary<string, ContractDescription> ImplementedContracts
		{
			get
			{
				return this.implementedContracts;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000187DB File Offset: 0x000169DB
		internal UriSchemeKeyedCollection InternalBaseAddresses
		{
			get
			{
				return this.baseAddresses;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000187E3 File Offset: 0x000169E3
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000187F0 File Offset: 0x000169F0
		public int ManualFlowControlLimit
		{
			get
			{
				return this.ServiceThrottle.ManualFlowControlLimit;
			}
			set
			{
				this.ServiceThrottle.ManualFlowControlLimit = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000187FE File Offset: 0x000169FE
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00018808 File Offset: 0x00016A08
		public TimeSpan OpenTimeout
		{
			get
			{
				return this.openTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					string @string = SR.GetString("SFxTimeoutOutOfRange0");
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", @string));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfClosedOrOpened();
					this.openTimeout = value;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000188A4 File Offset: 0x00016AA4
		internal ServiceThrottle ServiceThrottle
		{
			get
			{
				return this.serviceThrottle;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000188AC File Offset: 0x00016AAC
		internal virtual object DisposableInstance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000188AF File Offset: 0x00016AAF
		internal Dictionary<DispatcherBuilder.ListenUriInfo, Collection<ServiceEndpoint>> EndpointsByListenUriInfo
		{
			get
			{
				if (this.endpointsByListenUriInfo == null)
				{
					this.endpointsByListenUriInfo = this.GetEndpointsByListenUriInfo();
				}
				return this.endpointsByListenUriInfo;
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000188CC File Offset: 0x00016ACC
		private Dictionary<DispatcherBuilder.ListenUriInfo, Collection<ServiceEndpoint>> GetEndpointsByListenUriInfo()
		{
			Dictionary<DispatcherBuilder.ListenUriInfo, Collection<ServiceEndpoint>> dictionary = new Dictionary<DispatcherBuilder.ListenUriInfo, Collection<ServiceEndpoint>>();
			foreach (ServiceEndpoint serviceEndpoint in this.Description.Endpoints)
			{
				DispatcherBuilder.ListenUriInfo listenUriInfoForEndpoint = DispatcherBuilder.GetListenUriInfoForEndpoint(this, serviceEndpoint);
				if (!dictionary.ContainsKey(listenUriInfoForEndpoint))
				{
					dictionary.Add(listenUriInfoForEndpoint, new Collection<ServiceEndpoint>());
				}
				dictionary[listenUriInfoForEndpoint].Add(serviceEndpoint);
			}
			return dictionary;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00018948 File Offset: 0x00016B48
		protected void AddBaseAddress(Uri baseAddress)
		{
			if (this.initializeDescriptionHasFinished)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCannotCallAddBaseAddress")));
			}
			this.baseAddresses.Add(baseAddress);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00018978 File Offset: 0x00016B78
		public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, string address)
		{
			return this.AddServiceEndpoint(implementedContract, binding, address, null);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018984 File Offset: 0x00016B84
		public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, string address, Uri listenUri)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("address"));
			}
			ServiceEndpoint serviceEndpoint = this.AddServiceEndpoint(implementedContract, binding, new Uri(address, UriKind.RelativeOrAbsolute));
			if (listenUri != null)
			{
				serviceEndpoint.UnresolvedListenUri = listenUri;
				listenUri = this.MakeAbsoluteUri(listenUri, binding);
				serviceEndpoint.ListenUri = listenUri;
			}
			return serviceEndpoint;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000189DF File Offset: 0x00016BDF
		public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, Uri address)
		{
			return this.AddServiceEndpoint(implementedContract, binding, address, null);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000189EC File Offset: 0x00016BEC
		public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, Uri address, Uri listenUri)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("address"));
			}
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("binding"));
			}
			if (implementedContract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("implementedContract"));
			}
			if (base.State != CommunicationState.Created && base.State != CommunicationState.Opening)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotAddEndpointAfterOpen")));
			}
			if (this.Description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotAddEndpointWithoutDescription")));
			}
			Uri uri = this.MakeAbsoluteUri(address, binding);
			ConfigLoader configLoader = new ConfigLoader(this.GetContractResolver(this.implementedContracts));
			ContractDescription contract = configLoader.LookupContract(implementedContract, this.Description.Name);
			ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contract, binding, new EndpointAddress(uri, new AddressHeader[0]));
			this.Description.Endpoints.Add(serviceEndpoint);
			serviceEndpoint.UnresolvedAddress = address;
			if (listenUri != null)
			{
				serviceEndpoint.UnresolvedListenUri = listenUri;
				listenUri = this.MakeAbsoluteUri(listenUri, binding);
				serviceEndpoint.ListenUri = listenUri;
			}
			return serviceEndpoint;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00018B14 File Offset: 0x00016D14
		public virtual void AddServiceEndpoint(ServiceEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			if (base.State != CommunicationState.Created && base.State != CommunicationState.Opening)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotAddEndpointAfterOpen")));
			}
			if (this.Description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotAddEndpointWithoutDescription")));
			}
			if (endpoint.Address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxEndpointAddressNotSpecified"));
			}
			if (endpoint.Contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxEndpointContractNotSpecified"));
			}
			if (endpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxEndpointBindingNotSpecified"));
			}
			if (!endpoint.IsSystemEndpoint || endpoint.Contract.ContractType == typeof(IMetadataExchange))
			{
				ConfigLoader configLoader = new ConfigLoader(this.GetContractResolver(this.implementedContracts));
				configLoader.LookupContract(endpoint.Contract.ConfigurationName, this.Description.Name);
			}
			this.Description.Endpoints.Add(endpoint);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00018C44 File Offset: 0x00016E44
		public void SetEndpointAddress(ServiceEndpoint endpoint, string relativeAddress)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			if (relativeAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("relativeAddress");
			}
			if (endpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxEndpointBindingNotSpecified"));
			}
			Uri uri = this.MakeAbsoluteUri(new Uri(relativeAddress, UriKind.Relative), endpoint.Binding);
			endpoint.Address = new EndpointAddress(uri, new AddressHeader[0]);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00018CBA File Offset: 0x00016EBA
		internal Uri MakeAbsoluteUri(Uri relativeOrAbsoluteUri, Binding binding)
		{
			return ServiceHostBase.MakeAbsoluteUri(relativeOrAbsoluteUri, binding, this.InternalBaseAddresses);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00018CCC File Offset: 0x00016ECC
		internal static Uri MakeAbsoluteUri(Uri relativeOrAbsoluteUri, Binding binding, UriSchemeKeyedCollection baseAddresses)
		{
			Uri uri = relativeOrAbsoluteUri;
			if (!uri.IsAbsoluteUri)
			{
				if (binding.Scheme == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCustomBindingWithoutTransport")));
				}
				uri = ServiceHostBase.GetVia(binding.Scheme, uri, baseAddresses);
				if (uri == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxEndpointNoMatchingScheme", new object[]
					{
						binding.Scheme,
						binding.Name,
						ServiceHostBase.GetBaseAddressSchemes(baseAddresses)
					})));
				}
			}
			return uri;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00018D64 File Offset: 0x00016F64
		protected virtual void ApplyConfiguration()
		{
			if (this.Description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotApplyConfigurationWithoutDescription")));
			}
			ConfigLoader configLoader = new ConfigLoader(this.GetContractResolver(this.implementedContracts));
			this.LoadConfigurationSectionInternal(configLoader, this.Description, this.Description.ConfigurationName);
			this.EnsureAuthenticationAuthorizationDebug(this.Description);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00018DC9 File Offset: 0x00016FC9
		internal void EnsureAuthenticationAuthorizationDebug(ServiceDescription description)
		{
			this.EnsureAuthentication(description);
			this.EnsureAuthorization(description);
			this.EnsureDebug(description);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00018DE4 File Offset: 0x00016FE4
		public virtual ReadOnlyCollection<ServiceEndpoint> AddDefaultEndpoints()
		{
			List<ServiceEndpoint> list = new List<ServiceEndpoint>();
			foreach (Uri uri in this.InternalBaseAddresses)
			{
				ProtocolMappingItem protocolMappingItem = ConfigLoader.LookupProtocolMapping(uri.Scheme);
				if (protocolMappingItem != null)
				{
					Binding binding = ConfigLoader.LookupBinding(protocolMappingItem.Binding, protocolMappingItem.BindingConfiguration);
					if (binding == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Exception(SR.GetString("BindingProtocolMappingNotDefined", new object[]
						{
							uri.Scheme
						})));
					}
					this.AddDefaultEndpoints(binding, list);
				}
			}
			if (DiagnosticUtility.ShouldTraceInformation && list.Count > 0)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary["ServiceConfigurationName"] = this.description.ConfigurationName;
				TraceUtility.TraceEvent(TraceEventType.Information, 524358, SR.GetString("TraceCodeDefaultEndpointsAdded"), new DictionaryTraceRecord(dictionary));
			}
			return new ReadOnlyCollection<ServiceEndpoint>(list);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00018EDC File Offset: 0x000170DC
		internal virtual void AddDefaultEndpoints(Binding defaultBinding, List<ServiceEndpoint> defaultEndpoints)
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00018EE0 File Offset: 0x000170E0
		internal virtual void BindInstance(InstanceContext instance)
		{
			this.instances.Add(instance);
			if (this.servicePerformanceCounters != null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.servicePerformanceCounters != null)
					{
						this.servicePerformanceCounters.ServiceInstanceCreated();
					}
				}
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00018F44 File Offset: 0x00017144
		void IDisposable.Dispose()
		{
			base.Close();
		}

		// Token: 0x06000579 RID: 1401
		protected abstract ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts);

		// Token: 0x0600057A RID: 1402 RVA: 0x00018F4C File Offset: 0x0001714C
		protected virtual void InitializeRuntime()
		{
			if (this.Description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotInitializeRuntimeWithoutDescription")));
			}
			if (this.Description.Endpoints.Count == 0)
			{
				this.AddDefaultEndpoints();
			}
			this.EnsureAuthenticationSchemes();
			DispatcherBuilder dispatcherBuilder = new DispatcherBuilder();
			dispatcherBuilder.InitializeServiceHost(this.description, this);
			SecurityValidationBehavior.Instance.AfterBuildTimeValidation(this.description);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00018FBD File Offset: 0x000171BD
		internal virtual void AfterInitializeRuntime(TimeSpan timeout)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00018FBF File Offset: 0x000171BF
		internal virtual IAsyncResult BeginAfterInitializeRuntime(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00018FC8 File Offset: 0x000171C8
		internal virtual void EndAfterInitializeRuntime(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00018FD0 File Offset: 0x000171D0
		private ServiceAuthorizationBehavior EnsureAuthorization(ServiceDescription description)
		{
			ServiceAuthorizationBehavior serviceAuthorizationBehavior = description.Behaviors.Find<ServiceAuthorizationBehavior>();
			if (serviceAuthorizationBehavior == null)
			{
				serviceAuthorizationBehavior = new ServiceAuthorizationBehavior();
				description.Behaviors.Add(serviceAuthorizationBehavior);
			}
			return serviceAuthorizationBehavior;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00019000 File Offset: 0x00017200
		private ServiceAuthenticationBehavior EnsureAuthentication(ServiceDescription description)
		{
			ServiceAuthenticationBehavior serviceAuthenticationBehavior = description.Behaviors.Find<ServiceAuthenticationBehavior>();
			if (serviceAuthenticationBehavior == null)
			{
				serviceAuthenticationBehavior = new ServiceAuthenticationBehavior();
				description.Behaviors.Add(serviceAuthenticationBehavior);
			}
			return serviceAuthenticationBehavior;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00019030 File Offset: 0x00017230
		private ServiceDebugBehavior EnsureDebug(ServiceDescription description)
		{
			ServiceDebugBehavior serviceDebugBehavior = description.Behaviors.Find<ServiceDebugBehavior>();
			if (serviceDebugBehavior == null)
			{
				serviceDebugBehavior = new ServiceDebugBehavior();
				description.Behaviors.Add(serviceDebugBehavior);
			}
			return serviceDebugBehavior;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00019060 File Offset: 0x00017260
		private ServiceCredentials EnsureCredentials(ServiceDescription description)
		{
			ServiceCredentials serviceCredentials = description.Behaviors.Find<ServiceCredentials>();
			if (serviceCredentials == null)
			{
				serviceCredentials = new ServiceCredentials();
				description.Behaviors.Add(serviceCredentials);
			}
			return serviceCredentials;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001908F File Offset: 0x0001728F
		internal void FaultInternal()
		{
			base.Fault();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00019097 File Offset: 0x00017297
		internal string GetBaseAddressSchemes()
		{
			return ServiceHostBase.GetBaseAddressSchemes(this.baseAddresses);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000190A4 File Offset: 0x000172A4
		internal static string GetBaseAddressSchemes(UriSchemeKeyedCollection uriSchemeKeyedCollection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (Uri uri in uriSchemeKeyedCollection)
			{
				if (flag)
				{
					stringBuilder.Append(uri.Scheme);
					flag = false;
				}
				else
				{
					stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator).Append(uri.Scheme);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00019128 File Offset: 0x00017328
		internal BindingParameterCollection GetBindingParameters()
		{
			return DispatcherBuilder.GetBindingParameters(this, new Collection<ServiceEndpoint>());
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00019138 File Offset: 0x00017338
		internal BindingParameterCollection GetBindingParameters(ServiceEndpoint inputEndpoint)
		{
			Collection<ServiceEndpoint> collection;
			if (inputEndpoint == null)
			{
				collection = new Collection<ServiceEndpoint>();
			}
			else if (!this.EndpointsByListenUriInfo.TryGetValue(DispatcherBuilder.GetListenUriInfoForEndpoint(this, inputEndpoint), out collection) || !collection.Contains(inputEndpoint))
			{
				collection = new Collection<ServiceEndpoint>();
				collection.Add(inputEndpoint);
			}
			return DispatcherBuilder.GetBindingParameters(this, collection);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00019183 File Offset: 0x00017383
		internal BindingParameterCollection GetBindingParameters(Collection<ServiceEndpoint> endpoints)
		{
			return DispatcherBuilder.GetBindingParameters(this, endpoints);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001918C File Offset: 0x0001738C
		internal ReadOnlyCollection<InstanceContext> GetInstanceContexts()
		{
			return Array.AsReadOnly<InstanceContext>(this.instances.ToArray());
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000191A0 File Offset: 0x000173A0
		internal virtual IContractResolver GetContractResolver(IDictionary<string, ContractDescription> implementedContracts)
		{
			ServiceHostBase.ServiceAndBehaviorsContractResolver serviceAndBehaviorsContractResolver = new ServiceHostBase.ServiceAndBehaviorsContractResolver(new ServiceHostBase.ImplementedContractsContractResolver(implementedContracts));
			serviceAndBehaviorsContractResolver.AddBehaviorContractsToResolver((this.description == null) ? null : this.description.Behaviors);
			return serviceAndBehaviorsContractResolver;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000191D6 File Offset: 0x000173D6
		internal static Uri GetUri(Uri baseUri, Uri relativeUri)
		{
			return ServiceHostBase.GetUri(baseUri, relativeUri.OriginalString);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000191E4 File Offset: 0x000173E4
		internal static Uri GetUri(Uri baseUri, string path)
		{
			if (path.StartsWith("/", StringComparison.Ordinal) || path.StartsWith("\\", StringComparison.Ordinal))
			{
				int num = 1;
				while (num < path.Length && (path[num] == '/' || path[num] == '\\'))
				{
					num++;
				}
				path = path.Substring(num);
			}
			if (path.Length == 0)
			{
				return baseUri;
			}
			if (!baseUri.AbsoluteUri.EndsWith("/", StringComparison.Ordinal))
			{
				baseUri = new Uri(baseUri.AbsoluteUri + "/");
			}
			return new Uri(baseUri, path);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00019278 File Offset: 0x00017478
		internal Uri GetVia(string scheme, Uri address)
		{
			return ServiceHostBase.GetVia(scheme, address, this.InternalBaseAddresses);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00019288 File Offset: 0x00017488
		internal static Uri GetVia(string scheme, Uri address, UriSchemeKeyedCollection baseAddresses)
		{
			Uri uri = address;
			if (!uri.IsAbsoluteUri)
			{
				if (!baseAddresses.Contains(scheme))
				{
					return null;
				}
				uri = ServiceHostBase.GetUri(baseAddresses[scheme], address);
			}
			return uri;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000192B9 File Offset: 0x000174B9
		public int IncrementManualFlowControlLimit(int incrementBy)
		{
			return this.ServiceThrottle.IncrementManualFlowControlLimit(incrementBy);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000192C8 File Offset: 0x000174C8
		protected void InitializeDescription(UriSchemeKeyedCollection baseAddresses)
		{
			foreach (Uri item in baseAddresses)
			{
				this.baseAddresses.Add(item);
			}
			IDictionary<string, ContractDescription> dictionary = null;
			ServiceDescription serviceDescription = this.CreateDescription(out dictionary);
			this.description = serviceDescription;
			this.implementedContracts = dictionary;
			this.ApplyConfiguration();
			this.initializeDescriptionHasFinished = true;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001933C File Offset: 0x0001753C
		protected void LoadConfigurationSection(ServiceElement serviceSection)
		{
			if (serviceSection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceSection");
			}
			if (this.Description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceHostBaseCannotLoadConfigurationSectionWithoutDescription")));
			}
			ConfigLoader configLoader = new ConfigLoader(this.GetContractResolver(this.ImplementedContracts));
			this.LoadConfigurationSectionInternal(configLoader, this.Description, serviceSection);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001939E File Offset: 0x0001759E
		internal void LoadConfigurationSectionHelper(Uri baseAddress)
		{
			this.AddBaseAddress(baseAddress);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000193A8 File Offset: 0x000175A8
		[SecuritySafeCritical]
		private void LoadConfigurationSectionInternal(ConfigLoader configLoader, ServiceDescription description, string configurationName)
		{
			ServiceElement serviceSection = configLoader.LookupService(configurationName);
			this.LoadConfigurationSectionInternal(configLoader, description, serviceSection);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000193C6 File Offset: 0x000175C6
		[SecuritySafeCritical]
		private void LoadConfigurationSectionInternal(ConfigLoader configLoader, ServiceDescription description, ServiceElement serviceSection)
		{
			configLoader.LoadServiceDescription(this, description, serviceSection, new Action<Uri>(this.LoadConfigurationSectionHelper), false);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000193E0 File Offset: 0x000175E0
		protected override void OnAbort()
		{
			this.instances.Abort();
			foreach (ChannelDispatcherBase channelDispatcherBase in this.ChannelDispatchers)
			{
				if (channelDispatcherBase.Listener != null)
				{
					channelDispatcherBase.Listener.Abort();
				}
				channelDispatcherBase.Abort();
			}
			ThreadTrace.StopTracing();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00019450 File Offset: 0x00017650
		internal void OnAddChannelDispatcher(ChannelDispatcherBase channelDispatcher)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosedOrOpened();
				channelDispatcher.AttachInternal(this);
				channelDispatcher.Faulted += this.OnChannelDispatcherFaulted;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000194AC File Offset: 0x000176AC
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ServiceHostBase.CloseAsyncResult(timeout, callback, state, this);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000194B7 File Offset: 0x000176B7
		private void OnBeginOpen()
		{
			this.TraceServiceHostOpenStart();
			this.TraceBaseAddresses();
			MessageLogger.EnsureInitialized();
			this.InitializeRuntime();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000194D0 File Offset: 0x000176D0
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnBeginOpen();
			return new ServiceHostBase.OpenAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000194E1 File Offset: 0x000176E1
		private IAsyncResult BeginOpenChannelDispatchers(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OpenCollectionAsyncResult(timeout, callback, state, this.SnapshotChannelDispatchers());
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000194F4 File Offset: 0x000176F4
		protected override void OnClose(TimeSpan timeout)
		{
			try
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (ManagementExtension.IsEnabled && this.Description != null)
				{
					ManagementExtension.OnServiceClosing(this);
				}
				for (int i = 0; i < this.ChannelDispatchers.Count; i++)
				{
					ChannelDispatcherBase channelDispatcherBase = this.ChannelDispatchers[i];
					if (channelDispatcherBase.Listener != null)
					{
						channelDispatcherBase.Listener.Close(timeoutHelper.RemainingTime());
					}
				}
				for (int j = 0; j < this.ChannelDispatchers.Count; j++)
				{
					ChannelDispatcherBase channelDispatcherBase2 = this.ChannelDispatchers[j];
					channelDispatcherBase2.CloseInput(timeoutHelper.RemainingTime());
				}
				this.instances.CloseInput(timeoutHelper.RemainingTime());
				this.instances.Close(timeoutHelper.RemainingTime());
				for (int k = 0; k < this.ChannelDispatchers.Count; k++)
				{
					ChannelDispatcherBase channelDispatcherBase3 = this.ChannelDispatchers[k];
					channelDispatcherBase3.Close(timeoutHelper.RemainingTime());
				}
				this.ReleasePerformanceCounters();
				this.TraceBaseAddresses();
				ThreadTrace.StopTracing();
			}
			catch (TimeoutException exception)
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(SR.GetString("TraceCodeServiceHostTimeoutOnClose"));
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524334, SR.GetString("TraceCodeServiceHostTimeoutOnClose"), this, exception);
				}
				base.Abort();
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001964C File Offset: 0x0001784C
		protected override void OnClosed()
		{
			try
			{
				for (int i = 0; i < this.ChannelDispatchers.Count; i++)
				{
					ChannelDispatcher channelDispatcher = this.ChannelDispatchers[i] as ChannelDispatcher;
					if (channelDispatcher != null)
					{
						channelDispatcher.ReleasePerformanceCounters();
					}
				}
			}
			finally
			{
				base.OnClosed();
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000196A4 File Offset: 0x000178A4
		private void TraceBaseAddresses()
		{
			if (DiagnosticUtility.ShouldTraceInformation && this.baseAddresses != null && this.baseAddresses.Count > 0)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 524333, SR.GetString("TraceCodeServiceHostBaseAddresses"), new CollectionTraceRecord("BaseAddresses", "Address", this.baseAddresses), this, null);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000196FA File Offset: 0x000178FA
		private void TraceServiceHostOpenStart()
		{
			if (TD.ServiceHostOpenStartIsEnabled())
			{
				TD.ServiceHostOpenStart(this.EventTraceActivity);
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00019710 File Offset: 0x00017910
		protected override void OnEndClose(IAsyncResult result)
		{
			try
			{
				ServiceHostBase.CloseAsyncResult.End(result);
				this.TraceBaseAddresses();
				ThreadTrace.StopTracing();
			}
			catch (TimeoutException exception)
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(SR.GetString("TraceCodeServiceHostTimeoutOnClose"));
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524334, SR.GetString("TraceCodeServiceHostTimeoutOnClose"), this, exception);
				}
				base.Abort();
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00019780 File Offset: 0x00017980
		protected override void OnEndOpen(IAsyncResult result)
		{
			ServiceHostBase.OpenAsyncResult.End(result);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00019788 File Offset: 0x00017988
		private void EndOpenChannelDispatchers(IAsyncResult result)
		{
			OpenCollectionAsyncResult.End(result);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00019790 File Offset: 0x00017990
		private void EnsureAuthenticationSchemes()
		{
			if (this.Authentication == null)
			{
				return;
			}
			if (!AspNetEnvironment.Enabled || this.Extensions.Find<VirtualPathExtension>() == null)
			{
				return;
			}
			foreach (ServiceEndpoint serviceEndpoint in this.Description.Endpoints)
			{
				if (serviceEndpoint.Binding != null && serviceEndpoint.ListenUri != null && ("http".Equals(serviceEndpoint.ListenUri.Scheme, StringComparison.OrdinalIgnoreCase) || "https".Equals(serviceEndpoint.ListenUri.Scheme, StringComparison.OrdinalIgnoreCase)) && this.baseAddresses.Contains(serviceEndpoint.ListenUri.Scheme))
				{
					HttpTransportBindingElement httpTransportBindingElement = serviceEndpoint.Binding.CreateBindingElements().Find<HttpTransportBindingElement>();
					if (httpTransportBindingElement == null)
					{
						break;
					}
					AuthenticationSchemes authenticationSchemes = AspNetEnvironment.Current.GetAuthenticationSchemes(this.baseAddresses[serviceEndpoint.ListenUri.Scheme]);
					if (authenticationSchemes == AuthenticationSchemes.None)
					{
						break;
					}
					if (this.Authentication.AuthenticationSchemes == AuthenticationSchemes.None)
					{
						this.Authentication.AuthenticationSchemes = authenticationSchemes;
						break;
					}
					this.Authentication.AuthenticationSchemes &= authenticationSchemes;
					break;
				}
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000198D0 File Offset: 0x00017AD0
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.OnBeginOpen();
			this.AfterInitializeRuntime(timeoutHelper.RemainingTime());
			for (int i = 0; i < this.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcherBase channelDispatcherBase = this.ChannelDispatchers[i];
				channelDispatcherBase.Open(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00019928 File Offset: 0x00017B28
		protected override void OnOpened()
		{
			if (this.Description != null)
			{
				ServiceCredentials serviceCredentials = this.description.Behaviors.Find<ServiceCredentials>();
				if (serviceCredentials != null)
				{
					ServiceCredentials serviceCredentials2 = serviceCredentials.Clone();
					serviceCredentials2.MakeReadOnly();
					this.readOnlyCredentials = serviceCredentials2;
				}
				ServiceAuthorizationBehavior serviceAuthorizationBehavior = this.description.Behaviors.Find<ServiceAuthorizationBehavior>();
				if (serviceAuthorizationBehavior != null)
				{
					ServiceAuthorizationBehavior serviceAuthorizationBehavior2 = serviceAuthorizationBehavior.Clone();
					serviceAuthorizationBehavior2.MakeReadOnly();
					this.readOnlyAuthorization = serviceAuthorizationBehavior2;
				}
				ServiceAuthenticationBehavior serviceAuthenticationBehavior = this.description.Behaviors.Find<ServiceAuthenticationBehavior>();
				if (serviceAuthenticationBehavior != null)
				{
					ServiceAuthenticationBehavior serviceAuthenticationBehavior2 = serviceAuthenticationBehavior.Clone();
					serviceAuthenticationBehavior.MakeReadOnly();
					this.readOnlyAuthentication = serviceAuthenticationBehavior2;
				}
				if (ManagementExtension.IsEnabled)
				{
					ManagementExtension.OnServiceOpened(this);
				}
				TelemetryTraceLogging.LogSeriveKPIData(this.Description);
			}
			base.OnOpened();
			if (TD.ServiceHostOpenStopIsEnabled())
			{
				TD.ServiceHostOpenStop(this.EventTraceActivity);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000199F0 File Offset: 0x00017BF0
		internal void OnRemoveChannelDispatcher(ChannelDispatcherBase channelDispatcher)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosedOrOpened();
				channelDispatcher.DetachInternal(this);
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00019A38 File Offset: 0x00017C38
		private void OnChannelDispatcherFaulted(object sender, EventArgs e)
		{
			base.Fault();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00019A40 File Offset: 0x00017C40
		private void OnServiceHostFaulted(object sender, EventArgs args)
		{
			if (TD.ServiceHostFaultedIsEnabled())
			{
				TD.ServiceHostFaulted(this.EventTraceActivity, this);
			}
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 524335, SR.GetString("TraceCodeServiceHostFaulted"), this);
			}
			foreach (ICommunicationObject communicationObject in this.SnapshotChannelDispatchers())
			{
				if (communicationObject.State == CommunicationState.Opened)
				{
					communicationObject.Abort();
				}
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00019AA8 File Offset: 0x00017CA8
		internal void RaiseUnknownMessageReceived(Message message)
		{
			try
			{
				EventHandler<UnknownMessageReceivedEventArgs> unknownMessageReceived = this.UnknownMessageReceived;
				if (unknownMessageReceived != null)
				{
					unknownMessageReceived(this, new UnknownMessageReceivedEventArgs(message));
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00019AF8 File Offset: 0x00017CF8
		protected void ReleasePerformanceCounters()
		{
			if (this.servicePerformanceCounters != null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.servicePerformanceCounters != null)
					{
						this.servicePerformanceCounters.Dispose();
						this.servicePerformanceCounters = null;
					}
				}
			}
			if (this.defaultPerformanceCounters != null)
			{
				object thisLock2 = base.ThisLock;
				lock (thisLock2)
				{
					if (this.defaultPerformanceCounters != null)
					{
						this.defaultPerformanceCounters.Dispose();
						this.defaultPerformanceCounters = null;
					}
				}
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00019BA0 File Offset: 0x00017DA0
		private ICommunicationObject[] SnapshotChannelDispatchers()
		{
			object thisLock = base.ThisLock;
			ICommunicationObject[] result;
			lock (thisLock)
			{
				ICommunicationObject[] array = new ICommunicationObject[this.ChannelDispatchers.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.ChannelDispatchers[i];
				}
				result = array;
			}
			return result;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019C10 File Offset: 0x00017E10
		internal virtual void UnbindInstance(InstanceContext instance)
		{
			this.instances.Remove(instance);
			if (this.servicePerformanceCounters != null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.servicePerformanceCounters != null)
					{
						this.servicePerformanceCounters.ServiceInstanceRemoved();
					}
				}
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00019C74 File Offset: 0x00017E74
		internal void IncrementBusyCount()
		{
			if (AspNetEnvironment.Enabled)
			{
				AspNetEnvironment.Current.IncrementBusyCount();
				Interlocked.Increment(ref this.busyCount);
			}
			EventHandler busyCountIncremented = this.BusyCountIncremented;
			if (busyCountIncremented != null)
			{
				try
				{
					busyCountIncremented(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00019CE0 File Offset: 0x00017EE0
		internal void DecrementBusyCount()
		{
			if (AspNetEnvironment.Enabled)
			{
				Interlocked.Decrement(ref this.busyCount);
				AspNetEnvironment.Current.DecrementBusyCount();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00019CFF File Offset: 0x00017EFF
		internal int BusyCount
		{
			get
			{
				return this.busyCount;
			}
		}

		// Token: 0x04000A3D RID: 2621
		internal static readonly Uri EmptyUri = new Uri(string.Empty, UriKind.RelativeOrAbsolute);

		// Token: 0x04000A3E RID: 2622
		private bool initializeDescriptionHasFinished;

		// Token: 0x04000A3F RID: 2623
		private UriSchemeKeyedCollection baseAddresses;

		// Token: 0x04000A40 RID: 2624
		private ChannelDispatcherCollection channelDispatchers;

		// Token: 0x04000A41 RID: 2625
		private TimeSpan closeTimeout = ServiceDefaults.ServiceHostCloseTimeout;

		// Token: 0x04000A42 RID: 2626
		private ServiceDescription description;

		// Token: 0x04000A43 RID: 2627
		private ExtensionCollection<ServiceHostBase> extensions;

		// Token: 0x04000A44 RID: 2628
		private ReadOnlyCollection<Uri> externalBaseAddresses;

		// Token: 0x04000A45 RID: 2629
		private IDictionary<string, ContractDescription> implementedContracts;

		// Token: 0x04000A46 RID: 2630
		private IInstanceContextManager instances;

		// Token: 0x04000A47 RID: 2631
		private TimeSpan openTimeout = ServiceDefaults.OpenTimeout;

		// Token: 0x04000A48 RID: 2632
		private ServicePerformanceCountersBase servicePerformanceCounters;

		// Token: 0x04000A49 RID: 2633
		private DefaultPerformanceCounters defaultPerformanceCounters;

		// Token: 0x04000A4A RID: 2634
		private ServiceThrottle serviceThrottle;

		// Token: 0x04000A4B RID: 2635
		private ServiceCredentials readOnlyCredentials;

		// Token: 0x04000A4C RID: 2636
		private ServiceAuthorizationBehavior readOnlyAuthorization;

		// Token: 0x04000A4D RID: 2637
		private ServiceAuthenticationBehavior readOnlyAuthentication;

		// Token: 0x04000A4E RID: 2638
		private Dictionary<DispatcherBuilder.ListenUriInfo, Collection<ServiceEndpoint>> endpointsByListenUriInfo;

		// Token: 0x04000A4F RID: 2639
		private int busyCount;

		// Token: 0x04000A50 RID: 2640
		private EventTraceActivity eventTraceActivity;

		// Token: 0x02000AD8 RID: 2776
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x06006E84 RID: 28292 RVA: 0x0019BE82 File Offset: 0x0019A082
			public OpenAsyncResult(ServiceHostBase host, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.host = host;
				if (this.ProcessAfterInitializeRuntime())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06006E85 RID: 28293 RVA: 0x0019BEB0 File Offset: 0x0019A0B0
			private bool ProcessAfterInitializeRuntime()
			{
				IAsyncResult result = this.host.BeginAfterInitializeRuntime(this.timeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(ServiceHostBase.OpenAsyncResult.handleEndAfterInitializeRuntime), this);
				return base.SyncContinue(result);
			}

			// Token: 0x06006E86 RID: 28294 RVA: 0x0019BEE8 File Offset: 0x0019A0E8
			private static bool HandleEndAfterInitializeRuntime(IAsyncResult result)
			{
				ServiceHostBase.OpenAsyncResult openAsyncResult = (ServiceHostBase.OpenAsyncResult)result.AsyncState;
				openAsyncResult.host.EndAfterInitializeRuntime(result);
				return openAsyncResult.ProcessOpenChannelDispatchers();
			}

			// Token: 0x06006E87 RID: 28295 RVA: 0x0019BF14 File Offset: 0x0019A114
			private bool ProcessOpenChannelDispatchers()
			{
				IAsyncResult result = this.host.BeginOpenChannelDispatchers(this.timeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(ServiceHostBase.OpenAsyncResult.handleEndOpenChannelDispatchers), this);
				return base.SyncContinue(result);
			}

			// Token: 0x06006E88 RID: 28296 RVA: 0x0019BF4C File Offset: 0x0019A14C
			private static bool HandleEndOpenChannelDispatchers(IAsyncResult result)
			{
				ServiceHostBase.OpenAsyncResult openAsyncResult = (ServiceHostBase.OpenAsyncResult)result.AsyncState;
				openAsyncResult.host.EndOpenChannelDispatchers(result);
				return true;
			}

			// Token: 0x06006E89 RID: 28297 RVA: 0x0019BF72 File Offset: 0x0019A172
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ServiceHostBase.OpenAsyncResult>(result);
			}

			// Token: 0x04003F17 RID: 16151
			private static AsyncResult.AsyncCompletion handleEndAfterInitializeRuntime = new AsyncResult.AsyncCompletion(ServiceHostBase.OpenAsyncResult.HandleEndAfterInitializeRuntime);

			// Token: 0x04003F18 RID: 16152
			private static AsyncResult.AsyncCompletion handleEndOpenChannelDispatchers = new AsyncResult.AsyncCompletion(ServiceHostBase.OpenAsyncResult.HandleEndOpenChannelDispatchers);

			// Token: 0x04003F19 RID: 16153
			private TimeoutHelper timeoutHelper;

			// Token: 0x04003F1A RID: 16154
			private ServiceHostBase host;
		}

		// Token: 0x02000AD9 RID: 2777
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06006E8B RID: 28299 RVA: 0x0019BF9F File Offset: 0x0019A19F
			public CloseAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, ServiceHostBase serviceHost) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.serviceHost = serviceHost;
				if (ManagementExtension.IsEnabled && serviceHost.Description != null)
				{
					ManagementExtension.OnServiceClosing(serviceHost);
				}
				this.CloseListeners(true);
			}

			// Token: 0x06006E8C RID: 28300 RVA: 0x0019BFDC File Offset: 0x0019A1DC
			private void CloseListeners(bool completedSynchronously)
			{
				List<ICommunicationObject> list = new List<ICommunicationObject>();
				for (int i = 0; i < this.serviceHost.ChannelDispatchers.Count; i++)
				{
					if (this.serviceHost.ChannelDispatchers[i].Listener != null)
					{
						list.Add(this.serviceHost.ChannelDispatchers[i].Listener);
					}
				}
				AsyncCallback otherCallback = Fx.ThunkCallback(new AsyncCallback(this.CloseListenersCallback));
				TimeSpan timeout = this.timeoutHelper.RemainingTime();
				Exception ex = null;
				IAsyncResult asyncResult = null;
				try
				{
					asyncResult = new CloseCollectionAsyncResult(timeout, otherCallback, this, list);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.FinishCloseListeners(asyncResult, completedSynchronously);
				}
			}

			// Token: 0x06006E8D RID: 28301 RVA: 0x0019C0B8 File Offset: 0x0019A2B8
			private void CloseListenersCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					((ServiceHostBase.CloseAsyncResult)result.AsyncState).FinishCloseListeners(result, false);
				}
			}

			// Token: 0x06006E8E RID: 28302 RVA: 0x0019C0D4 File Offset: 0x0019A2D4
			private void FinishCloseListeners(IAsyncResult result, bool completedSynchronously)
			{
				Exception ex = null;
				try
				{
					CloseCollectionAsyncResult.End(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				this.CloseInput(completedSynchronously);
			}

			// Token: 0x06006E8F RID: 28303 RVA: 0x0019C120 File Offset: 0x0019A320
			private void CloseInput(bool completedSynchronously)
			{
				AsyncCallback callback = Fx.ThunkCallback(new AsyncCallback(this.CloseInputCallback));
				Exception ex = null;
				IAsyncResult asyncResult = null;
				try
				{
					for (int i = 0; i < this.serviceHost.ChannelDispatchers.Count; i++)
					{
						ChannelDispatcherBase channelDispatcherBase = this.serviceHost.ChannelDispatchers[i];
						channelDispatcherBase.CloseInput(this.timeoutHelper.RemainingTime());
					}
					asyncResult = this.serviceHost.instances.BeginCloseInput(this.timeoutHelper.RemainingTime(), callback, this);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					FxTrace.Exception.AsWarning(ex);
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.FinishCloseInput(asyncResult, completedSynchronously);
				}
			}

			// Token: 0x06006E90 RID: 28304 RVA: 0x0019C1F0 File Offset: 0x0019A3F0
			private void CloseInputCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					((ServiceHostBase.CloseAsyncResult)result.AsyncState).FinishCloseInput(result, false);
				}
			}

			// Token: 0x06006E91 RID: 28305 RVA: 0x0019C20C File Offset: 0x0019A40C
			private void FinishCloseInput(IAsyncResult result, bool completedSynchronously)
			{
				Exception ex = null;
				try
				{
					this.serviceHost.instances.EndCloseInput(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				this.CloseInstances(completedSynchronously);
			}

			// Token: 0x06006E92 RID: 28306 RVA: 0x0019C264 File Offset: 0x0019A464
			private void CloseInstances(bool completedSynchronously)
			{
				AsyncCallback callback = Fx.ThunkCallback(new AsyncCallback(this.CloseInstancesCallback));
				TimeSpan timeout = this.timeoutHelper.RemainingTime();
				Exception ex = null;
				IAsyncResult asyncResult = null;
				try
				{
					asyncResult = this.serviceHost.instances.BeginClose(timeout, callback, this);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.FinishCloseInstances(asyncResult, completedSynchronously);
				}
			}

			// Token: 0x06006E93 RID: 28307 RVA: 0x0019C2EC File Offset: 0x0019A4EC
			private void CloseInstancesCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					((ServiceHostBase.CloseAsyncResult)result.AsyncState).FinishCloseInstances(result, false);
				}
			}

			// Token: 0x06006E94 RID: 28308 RVA: 0x0019C308 File Offset: 0x0019A508
			private void FinishCloseInstances(IAsyncResult result, bool completedSynchronously)
			{
				Exception ex = null;
				try
				{
					this.serviceHost.instances.EndClose(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				this.CloseChannelDispatchers(completedSynchronously);
			}

			// Token: 0x06006E95 RID: 28309 RVA: 0x0019C360 File Offset: 0x0019A560
			private void CloseChannelDispatchers(bool completedSynchronously)
			{
				IList<ICommunicationObject> collection = this.serviceHost.SnapshotChannelDispatchers();
				AsyncCallback otherCallback = Fx.ThunkCallback(new AsyncCallback(this.CloseChannelDispatchersCallback));
				TimeSpan timeout = this.timeoutHelper.RemainingTime();
				Exception ex = null;
				IAsyncResult asyncResult = null;
				try
				{
					asyncResult = new CloseCollectionAsyncResult(timeout, otherCallback, this, collection);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2) || completedSynchronously)
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallComplete(completedSynchronously, ex);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.FinishCloseChannelDispatchers(asyncResult, completedSynchronously);
				}
			}

			// Token: 0x06006E96 RID: 28310 RVA: 0x0019C3EC File Offset: 0x0019A5EC
			private void CloseChannelDispatchersCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					((ServiceHostBase.CloseAsyncResult)result.AsyncState).FinishCloseChannelDispatchers(result, false);
				}
			}

			// Token: 0x06006E97 RID: 28311 RVA: 0x0019C408 File Offset: 0x0019A608
			private void FinishCloseChannelDispatchers(IAsyncResult result, bool completedSynchronously)
			{
				Exception exception = null;
				try
				{
					CloseCollectionAsyncResult.End(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex) || completedSynchronously)
					{
						throw;
					}
					exception = ex;
				}
				this.CallComplete(completedSynchronously, exception);
			}

			// Token: 0x06006E98 RID: 28312 RVA: 0x0019C448 File Offset: 0x0019A648
			private void CallComplete(bool completedSynchronously, Exception exception)
			{
				base.Complete(completedSynchronously, exception);
			}

			// Token: 0x06006E99 RID: 28313 RVA: 0x0019C452 File Offset: 0x0019A652
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ServiceHostBase.CloseAsyncResult>(result);
			}

			// Token: 0x04003F1B RID: 16155
			private ServiceHostBase serviceHost;

			// Token: 0x04003F1C RID: 16156
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000ADA RID: 2778
		private class ImplementedContractsContractResolver : IContractResolver
		{
			// Token: 0x06006E9A RID: 28314 RVA: 0x0019C45B File Offset: 0x0019A65B
			public ImplementedContractsContractResolver(IDictionary<string, ContractDescription> implementedContracts)
			{
				this.implementedContracts = implementedContracts;
			}

			// Token: 0x06006E9B RID: 28315 RVA: 0x0019C46A File Offset: 0x0019A66A
			public ContractDescription ResolveContract(string contractName)
			{
				if (this.implementedContracts == null || !this.implementedContracts.ContainsKey(contractName))
				{
					return null;
				}
				return this.implementedContracts[contractName];
			}

			// Token: 0x04003F1D RID: 16157
			private IDictionary<string, ContractDescription> implementedContracts;
		}

		// Token: 0x02000ADB RID: 2779
		internal class ServiceAndBehaviorsContractResolver : IContractResolver
		{
			// Token: 0x170019D1 RID: 6609
			// (get) Token: 0x06006E9C RID: 28316 RVA: 0x0019C490 File Offset: 0x0019A690
			public Dictionary<string, ContractDescription> BehaviorContracts
			{
				get
				{
					return this.behaviorContracts;
				}
			}

			// Token: 0x06006E9D RID: 28317 RVA: 0x0019C498 File Offset: 0x0019A698
			public ServiceAndBehaviorsContractResolver(IContractResolver serviceResolver)
			{
				this.serviceResolver = serviceResolver;
				this.behaviorContracts = new Dictionary<string, ContractDescription>();
			}

			// Token: 0x06006E9E RID: 28318 RVA: 0x0019C4B4 File Offset: 0x0019A6B4
			public ContractDescription ResolveContract(string contractName)
			{
				ContractDescription contractDescription = this.serviceResolver.ResolveContract(contractName);
				if (contractDescription == null)
				{
					contractDescription = (this.behaviorContracts.ContainsKey(contractName) ? this.behaviorContracts[contractName] : null);
				}
				return contractDescription;
			}

			// Token: 0x06006E9F RID: 28319 RVA: 0x0019C4F0 File Offset: 0x0019A6F0
			public void AddBehaviorContractsToResolver(KeyedByTypeCollection<IServiceBehavior> behaviors)
			{
				if (behaviors != null && behaviors.Contains(typeof(ServiceMetadataBehavior)))
				{
					behaviors.Find<ServiceMetadataBehavior>().AddImplementedContracts(this);
				}
			}

			// Token: 0x04003F1E RID: 16158
			private IContractResolver serviceResolver;

			// Token: 0x04003F1F RID: 16159
			private Dictionary<string, ContractDescription> behaviorContracts;
		}
	}
}

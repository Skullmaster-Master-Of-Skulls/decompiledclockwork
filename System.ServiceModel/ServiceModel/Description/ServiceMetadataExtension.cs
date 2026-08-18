using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Resources;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F7 RID: 1015
	public class ServiceMetadataExtension : IExtension<ServiceHostBase>
	{
		// Token: 0x06002649 RID: 9801 RVA: 0x0008A1DA File Offset: 0x000883DA
		public ServiceMetadataExtension() : this(null)
		{
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0008A1E3 File Offset: 0x000883E3
		internal ServiceMetadataExtension(ServiceMetadataBehavior.MetadataExtensionInitializer initializer)
		{
			this.initializer = initializer;
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x0008A208 File Offset: 0x00088408
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x0008A210 File Offset: 0x00088410
		internal ServiceMetadataBehavior.MetadataExtensionInitializer Initializer
		{
			get
			{
				return this.initializer;
			}
			set
			{
				this.initializer = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0008A219 File Offset: 0x00088419
		public MetadataSet Metadata
		{
			get
			{
				this.EnsureInitialized();
				return this.metadata;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x0008A227 File Offset: 0x00088427
		public ServiceDescription SingleWsdl
		{
			get
			{
				this.EnsureSingleWsdlInitialized();
				return this.singleWsdl;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x0008A235 File Offset: 0x00088435
		// (set) Token: 0x06002650 RID: 9808 RVA: 0x0008A23D File Offset: 0x0008843D
		internal Uri ExternalMetadataLocation
		{
			get
			{
				return this.externalMetadataLocation;
			}
			set
			{
				this.externalMetadataLocation = value;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x0008A246 File Offset: 0x00088446
		// (set) Token: 0x06002652 RID: 9810 RVA: 0x0008A24E File Offset: 0x0008844E
		internal bool MexEnabled
		{
			get
			{
				return this.mexEnabled;
			}
			set
			{
				this.mexEnabled = value;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x0008A257 File Offset: 0x00088457
		// (set) Token: 0x06002654 RID: 9812 RVA: 0x0008A25F File Offset: 0x0008845F
		internal bool HttpGetEnabled
		{
			get
			{
				return this.httpGetEnabled;
			}
			set
			{
				this.httpGetEnabled = value;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x0008A268 File Offset: 0x00088468
		// (set) Token: 0x06002656 RID: 9814 RVA: 0x0008A270 File Offset: 0x00088470
		internal bool HttpsGetEnabled
		{
			get
			{
				return this.httpsGetEnabled;
			}
			set
			{
				this.httpsGetEnabled = value;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x0008A279 File Offset: 0x00088479
		internal bool HelpPageEnabled
		{
			get
			{
				return this.httpHelpPageEnabled || this.httpsHelpPageEnabled;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x0008A28B File Offset: 0x0008848B
		internal bool MetadataEnabled
		{
			get
			{
				return this.mexEnabled || this.httpGetEnabled || this.httpsGetEnabled;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x0008A2A5 File Offset: 0x000884A5
		// (set) Token: 0x0600265A RID: 9818 RVA: 0x0008A2AD File Offset: 0x000884AD
		internal bool HttpHelpPageEnabled
		{
			get
			{
				return this.httpHelpPageEnabled;
			}
			set
			{
				this.httpHelpPageEnabled = value;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x0008A2B6 File Offset: 0x000884B6
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x0008A2BE File Offset: 0x000884BE
		internal bool HttpsHelpPageEnabled
		{
			get
			{
				return this.httpsHelpPageEnabled;
			}
			set
			{
				this.httpsHelpPageEnabled = value;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x0008A2C7 File Offset: 0x000884C7
		// (set) Token: 0x0600265E RID: 9822 RVA: 0x0008A2CF File Offset: 0x000884CF
		internal Uri MexUrl
		{
			get
			{
				return this.mexUrl;
			}
			set
			{
				this.mexUrl = value;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x0008A2D8 File Offset: 0x000884D8
		// (set) Token: 0x06002660 RID: 9824 RVA: 0x0008A2E0 File Offset: 0x000884E0
		internal Uri HttpGetUrl
		{
			get
			{
				return this.httpGetUrl;
			}
			set
			{
				this.httpGetUrl = value;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x0008A2E9 File Offset: 0x000884E9
		// (set) Token: 0x06002662 RID: 9826 RVA: 0x0008A2F1 File Offset: 0x000884F1
		internal Uri HttpsGetUrl
		{
			get
			{
				return this.httpsGetUrl;
			}
			set
			{
				this.httpsGetUrl = value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x0008A2FA File Offset: 0x000884FA
		// (set) Token: 0x06002664 RID: 9828 RVA: 0x0008A302 File Offset: 0x00088502
		internal Uri HttpHelpPageUrl
		{
			get
			{
				return this.httpHelpPageUrl;
			}
			set
			{
				this.httpHelpPageUrl = value;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x0008A30B File Offset: 0x0008850B
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x0008A313 File Offset: 0x00088513
		internal Uri HttpsHelpPageUrl
		{
			get
			{
				return this.httpsHelpPageUrl;
			}
			set
			{
				this.httpsHelpPageUrl = value;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x0008A31C File Offset: 0x0008851C
		// (set) Token: 0x06002668 RID: 9832 RVA: 0x0008A324 File Offset: 0x00088524
		internal System.ServiceModel.Channels.Binding HttpHelpPageBinding
		{
			get
			{
				return this.httpHelpPageBinding;
			}
			set
			{
				this.httpHelpPageBinding = value;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x0008A32D File Offset: 0x0008852D
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x0008A335 File Offset: 0x00088535
		internal System.ServiceModel.Channels.Binding HttpsHelpPageBinding
		{
			get
			{
				return this.httpsHelpPageBinding;
			}
			set
			{
				this.httpsHelpPageBinding = value;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x0008A33E File Offset: 0x0008853E
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x0008A346 File Offset: 0x00088546
		internal System.ServiceModel.Channels.Binding HttpGetBinding
		{
			get
			{
				return this.httpGetBinding;
			}
			set
			{
				this.httpGetBinding = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x0008A34F File Offset: 0x0008854F
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x0008A357 File Offset: 0x00088557
		internal System.ServiceModel.Channels.Binding HttpsGetBinding
		{
			get
			{
				return this.httpsGetBinding;
			}
			set
			{
				this.httpsGetBinding = value;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x0008A360 File Offset: 0x00088560
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x0008A368 File Offset: 0x00088568
		internal bool UpdateAddressDynamically { get; set; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x0008A371 File Offset: 0x00088571
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x0008A379 File Offset: 0x00088579
		internal IDictionary<string, int> UpdatePortsByScheme { get; set; }

		// Token: 0x06002673 RID: 9843 RVA: 0x0008A384 File Offset: 0x00088584
		internal static bool TryGetHttpHostAndPort(Uri listenUri, System.ServiceModel.Channels.Message request, out string host, out int port)
		{
			host = null;
			port = 0;
			object obj;
			if (!request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				return false;
			}
			HttpRequestMessageProperty httpRequestMessageProperty = obj as HttpRequestMessageProperty;
			if (httpRequestMessageProperty == null)
			{
				return false;
			}
			string text = httpRequestMessageProperty.Headers[HttpRequestHeader.Host];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			string uriString = listenUri.Scheme + "://" + text;
			Uri uri;
			if (!Uri.TryCreate(uriString, UriKind.Absolute, out uri))
			{
				return false;
			}
			host = uri.Host;
			port = uri.Port;
			return true;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0008A404 File Offset: 0x00088604
		private void EnsureInitialized()
		{
			if (!this.isInitialized)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!this.isInitialized)
					{
						if (this.initializer != null)
						{
							this.metadata = this.initializer.GenerateMetadata();
						}
						if (this.metadata == null)
						{
							this.metadata = new MetadataSet();
						}
						Thread.MemoryBarrier();
						this.isInitialized = true;
						this.initializer = null;
					}
				}
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0008A490 File Offset: 0x00088690
		private void EnsureSingleWsdlInitialized()
		{
			if (!this.isSingleWsdlInitialized)
			{
				object obj = this.singleWsdlSyncRoot;
				lock (obj)
				{
					if (!this.isSingleWsdlInitialized)
					{
						this.singleWsdl = WsdlHelper.GetSingleWsdl(this.Metadata);
						this.isSingleWsdlInitialized = true;
					}
				}
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0008A4F4 File Offset: 0x000886F4
		void IExtension<ServiceHostBase>.Attach(ServiceHostBase owner)
		{
			if (owner == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("owner"));
			}
			if (this.owner != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TheServiceMetadataExtensionInstanceCouldNot2_0")));
			}
			owner.ThrowIfClosedOrOpened();
			this.owner = owner;
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x0008A548 File Offset: 0x00088748
		void IExtension<ServiceHostBase>.Detach(ServiceHostBase owner)
		{
			if (owner == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("owner");
			}
			if (this.owner == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TheServiceMetadataExtensionInstanceCouldNot3_0")));
			}
			if (this.owner != owner)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("owner", SR.GetString("TheServiceMetadataExtensionInstanceCouldNot4_0"));
			}
			this.owner.ThrowIfClosedOrOpened();
			this.owner = null;
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x0008A5C0 File Offset: 0x000887C0
		internal static ServiceMetadataExtension EnsureServiceMetadataExtension(ServiceDescription description, ServiceHostBase host)
		{
			ServiceMetadataExtension serviceMetadataExtension = host.Extensions.Find<ServiceMetadataExtension>();
			if (serviceMetadataExtension == null)
			{
				serviceMetadataExtension = new ServiceMetadataExtension();
				host.Extensions.Add(serviceMetadataExtension);
			}
			return serviceMetadataExtension;
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0008A5F0 File Offset: 0x000887F0
		internal ChannelDispatcher EnsureGetDispatcher(Uri listenUri)
		{
			ChannelDispatcher channelDispatcher = this.FindGetDispatcher(listenUri);
			if (channelDispatcher == null)
			{
				channelDispatcher = this.CreateGetDispatcher(listenUri);
				this.owner.ChannelDispatchers.Add(channelDispatcher);
			}
			return channelDispatcher;
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0008A624 File Offset: 0x00088824
		internal ChannelDispatcher EnsureGetDispatcher(Uri listenUri, bool isServiceDebugBehavior)
		{
			ChannelDispatcher channelDispatcher = this.FindGetDispatcher(listenUri);
			if (channelDispatcher == null)
			{
				System.ServiceModel.Channels.Binding binding;
				if (listenUri.Scheme == Uri.UriSchemeHttp)
				{
					if (isServiceDebugBehavior)
					{
						binding = (this.httpHelpPageBinding ?? MetadataExchangeBindings.HttpGet);
					}
					else
					{
						binding = (this.httpGetBinding ?? MetadataExchangeBindings.HttpGet);
					}
				}
				else
				{
					if (!(listenUri.Scheme == Uri.UriSchemeHttps))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxGetChannelDispatcherDoesNotSupportScheme", new object[]
						{
							typeof(ChannelDispatcher).Name,
							Uri.UriSchemeHttp,
							Uri.UriSchemeHttps
						})));
					}
					if (isServiceDebugBehavior)
					{
						binding = (this.httpsHelpPageBinding ?? MetadataExchangeBindings.HttpsGet);
					}
					else
					{
						binding = (this.httpsGetBinding ?? MetadataExchangeBindings.HttpsGet);
					}
				}
				channelDispatcher = this.CreateGetDispatcher(listenUri, binding);
				this.owner.ChannelDispatchers.Add(channelDispatcher);
			}
			return channelDispatcher;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0008A714 File Offset: 0x00088914
		internal ChannelDispatcher FindGetDispatcher(Uri listenUri)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in this.owner.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
				if (channelDispatcher != null && channelDispatcher.Listener.Uri == listenUri && channelDispatcher.Endpoints.Count == 1 && channelDispatcher.Endpoints[0].DispatchRuntime.SingletonInstanceContext != null && channelDispatcher.Endpoints[0].DispatchRuntime.SingletonInstanceContext.UserObject is ServiceMetadataExtension.HttpGetImpl)
				{
					return channelDispatcher;
				}
			}
			return null;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0008A7CC File Offset: 0x000889CC
		internal ChannelDispatcher CreateGetDispatcher(Uri listenUri)
		{
			if (listenUri.Scheme == Uri.UriSchemeHttp)
			{
				return this.CreateGetDispatcher(listenUri, MetadataExchangeBindings.HttpGet);
			}
			if (listenUri.Scheme == Uri.UriSchemeHttps)
			{
				return this.CreateGetDispatcher(listenUri, MetadataExchangeBindings.HttpsGet);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxGetChannelDispatcherDoesNotSupportScheme", new object[]
			{
				typeof(ChannelDispatcher).Name,
				Uri.UriSchemeHttp,
				Uri.UriSchemeHttps
			})));
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0008A858 File Offset: 0x00088A58
		internal ChannelDispatcher CreateGetDispatcher(Uri listenUri, System.ServiceModel.Channels.Binding binding)
		{
			return this.CreateGetDispatcher(listenUri, binding, "ServiceMetadataBehaviorHttpGetBinding");
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x0008A868 File Offset: 0x00088A68
		internal ChannelDispatcher CreateGetDispatcher(Uri listenUri, System.ServiceModel.Channels.Binding binding, string bindingName)
		{
			EndpointAddress address = new EndpointAddress(listenUri, new AddressHeader[0]);
			string empty = string.Empty;
			BindingParameterCollection bindingParameters = this.owner.GetBindingParameters();
			AspNetEnvironment.Current.AddMetadataBindingParameters(listenUri, this.owner.Description.Behaviors, bindingParameters);
			if (binding.CanBuildChannelListener<IReplyChannel>(bindingParameters))
			{
				IChannelListener channelListener = binding.BuildChannelListener<IReplyChannel>(listenUri, empty, bindingParameters);
				ChannelDispatcher channelDispatcher = new ChannelDispatcher(channelListener, bindingName, binding);
				channelDispatcher.MessageVersion = binding.MessageVersion;
				EndpointDispatcher endpointDispatcher = new EndpointDispatcher(address, "IHttpGetHelpPageAndMetadataContract", "http://schemas.microsoft.com/2006/04/http/metadata", true);
				DispatchOperation dispatchOperation = new DispatchOperation(endpointDispatcher.DispatchRuntime, "Get", "*", "*");
				dispatchOperation.Formatter = MessageOperationFormatter.Instance;
				MethodInfo method = typeof(ServiceMetadataExtension.IHttpGetMetadata).GetMethod("Get");
				dispatchOperation.Invoker = new SyncMethodInvoker(method);
				endpointDispatcher.DispatchRuntime.Operations.Add(dispatchOperation);
				ServiceMetadataExtension.HttpGetImpl httpGetImpl = new ServiceMetadataExtension.HttpGetImpl(this, channelListener.Uri);
				endpointDispatcher.DispatchRuntime.SingletonInstanceContext = new InstanceContext(this.owner, httpGetImpl, false);
				endpointDispatcher.DispatchRuntime.MessageInspectors.Add(httpGetImpl);
				channelDispatcher.Endpoints.Add(endpointDispatcher);
				endpointDispatcher.ContractFilter = new MatchAllMessageFilter();
				endpointDispatcher.FilterPriority = 0;
				endpointDispatcher.DispatchRuntime.InstanceContextProvider = InstanceContextProviderBase.GetProviderForMode(InstanceContextMode.Single, endpointDispatcher.DispatchRuntime);
				channelDispatcher.ServiceThrottle = this.owner.ServiceThrottle;
				ServiceDebugBehavior serviceDebugBehavior = this.owner.Description.Behaviors.Find<ServiceDebugBehavior>();
				if (serviceDebugBehavior != null)
				{
					channelDispatcher.IncludeExceptionDetailInFaults |= serviceDebugBehavior.IncludeExceptionDetailInFaults;
				}
				ServiceBehaviorAttribute serviceBehaviorAttribute = this.owner.Description.Behaviors.Find<ServiceBehaviorAttribute>();
				if (serviceBehaviorAttribute != null)
				{
					channelDispatcher.IncludeExceptionDetailInFaults |= serviceBehaviorAttribute.IncludeExceptionDetailInFaults;
				}
				return channelDispatcher;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxBindingNotSupportedForMetadataHttpGet")));
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x0008AA5C File Offset: 0x00088C5C
		private ServiceMetadataExtension.WriteFilter GetWriteFilter(System.ServiceModel.Channels.Message request, Uri listenUri, bool removeBaseAddress)
		{
			ServiceMetadataExtension.WriteFilter writeFilter = null;
			if (this.UpdateAddressDynamically)
			{
				writeFilter = this.GetDynamicAddressWriter(request, listenUri, removeBaseAddress);
			}
			if (writeFilter == null)
			{
				if (removeBaseAddress)
				{
					writeFilter = new ServiceMetadataExtension.LocationUpdatingWriter("{%BaseAddress%}", null);
				}
				else
				{
					writeFilter = new ServiceMetadataExtension.LocationUpdatingWriter("{%BaseAddress%}", listenUri.ToString());
				}
			}
			return writeFilter;
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0008AAA4 File Offset: 0x00088CA4
		private ServiceMetadataExtension.DynamicAddressUpdateWriter GetDynamicAddressWriter(System.ServiceModel.Channels.Message request, Uri listenUri, bool removeBaseAddress)
		{
			string host;
			int port;
			if (!ServiceMetadataExtension.TryGetHttpHostAndPort(listenUri, request, out host, out port))
			{
				if (request.Headers.To == null)
				{
					return null;
				}
				host = request.Headers.To.Host;
				port = request.Headers.To.Port;
			}
			if (host == listenUri.Host && port == listenUri.Port && (this.UpdatePortsByScheme == null || this.UpdatePortsByScheme.Count == 0))
			{
				return null;
			}
			return new ServiceMetadataExtension.DynamicAddressUpdateWriter(listenUri, host, port, this.UpdatePortsByScheme, removeBaseAddress);
		}

		// Token: 0x04002188 RID: 8584
		private const string BaseAddressPattern = "{%BaseAddress%}";

		// Token: 0x04002189 RID: 8585
		private static readonly Uri EmptyUri = new Uri(string.Empty, UriKind.Relative);

		// Token: 0x0400218A RID: 8586
		private static readonly Type[] httpGetSupportedChannels = new Type[]
		{
			typeof(IReplyChannel)
		};

		// Token: 0x0400218B RID: 8587
		private ServiceMetadataBehavior.MetadataExtensionInitializer initializer;

		// Token: 0x0400218C RID: 8588
		private MetadataSet metadata;

		// Token: 0x0400218D RID: 8589
		private ServiceDescription singleWsdl;

		// Token: 0x0400218E RID: 8590
		private bool isInitialized;

		// Token: 0x0400218F RID: 8591
		private bool isSingleWsdlInitialized;

		// Token: 0x04002190 RID: 8592
		private Uri externalMetadataLocation;

		// Token: 0x04002191 RID: 8593
		private ServiceHostBase owner;

		// Token: 0x04002192 RID: 8594
		private object syncRoot = new object();

		// Token: 0x04002193 RID: 8595
		private object singleWsdlSyncRoot = new object();

		// Token: 0x04002194 RID: 8596
		private bool mexEnabled;

		// Token: 0x04002195 RID: 8597
		private bool httpGetEnabled;

		// Token: 0x04002196 RID: 8598
		private bool httpsGetEnabled;

		// Token: 0x04002197 RID: 8599
		private bool httpHelpPageEnabled;

		// Token: 0x04002198 RID: 8600
		private bool httpsHelpPageEnabled;

		// Token: 0x04002199 RID: 8601
		private Uri mexUrl;

		// Token: 0x0400219A RID: 8602
		private Uri httpGetUrl;

		// Token: 0x0400219B RID: 8603
		private Uri httpsGetUrl;

		// Token: 0x0400219C RID: 8604
		private Uri httpHelpPageUrl;

		// Token: 0x0400219D RID: 8605
		private Uri httpsHelpPageUrl;

		// Token: 0x0400219E RID: 8606
		private System.ServiceModel.Channels.Binding httpHelpPageBinding;

		// Token: 0x0400219F RID: 8607
		private System.ServiceModel.Channels.Binding httpsHelpPageBinding;

		// Token: 0x040021A0 RID: 8608
		private System.ServiceModel.Channels.Binding httpGetBinding;

		// Token: 0x040021A1 RID: 8609
		private System.ServiceModel.Channels.Binding httpsGetBinding;

		// Token: 0x02000BA7 RID: 2983
		internal class MetadataBindingParameter
		{
		}

		// Token: 0x02000BA8 RID: 2984
		internal class WSMexImpl : IMetadataExchange
		{
			// Token: 0x060073DE RID: 29662 RVA: 0x001B0B24 File Offset: 0x001AED24
			internal WSMexImpl(ServiceMetadataExtension parent, bool isListeningOnHttps, Uri listenUri)
			{
				this.parent = parent;
				this.isListeningOnHttps = isListeningOnHttps;
				this.listenUri = listenUri;
				if (this.parent.ExternalMetadataLocation != null && this.parent.ExternalMetadataLocation != ServiceMetadataExtension.EmptyUri)
				{
					this.metadataLocationSet = new MetadataSet();
					string locationToReturn = this.GetLocationToReturn();
					MetadataSection item = new MetadataSection(MetadataSection.ServiceDescriptionDialect, null, new MetadataLocation(locationToReturn));
					this.metadataLocationSet.MetadataSections.Add(item);
				}
			}

			// Token: 0x17001AD7 RID: 6871
			// (get) Token: 0x060073DF RID: 29663 RVA: 0x001B0BAB File Offset: 0x001AEDAB
			// (set) Token: 0x060073E0 RID: 29664 RVA: 0x001B0BB3 File Offset: 0x001AEDB3
			internal bool IsListeningOnHttps
			{
				get
				{
					return this.isListeningOnHttps;
				}
				set
				{
					this.isListeningOnHttps = value;
				}
			}

			// Token: 0x060073E1 RID: 29665 RVA: 0x001B0BBC File Offset: 0x001AEDBC
			private string GetLocationToReturn()
			{
				Uri uri = this.parent.ExternalMetadataLocation;
				if (!uri.IsAbsoluteUri)
				{
					Uri via = this.parent.owner.GetVia(Uri.UriSchemeHttp, uri);
					Uri via2 = this.parent.owner.GetVia(Uri.UriSchemeHttps, uri);
					if (this.IsListeningOnHttps && via2 != null)
					{
						uri = via2;
					}
					else
					{
						if (!(via != null))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ExternalMetadataLocation", SR.GetString("SFxBadMetadataLocationNoAppropriateBaseAddress", new object[]
							{
								this.parent.ExternalMetadataLocation.OriginalString
							}));
						}
						uri = via;
					}
				}
				return uri.ToString();
			}

			// Token: 0x060073E2 RID: 29666 RVA: 0x001B0C6C File Offset: 0x001AEE6C
			private MetadataSet GatherMetadata(string dialect, string identifier)
			{
				if (this.metadataLocationSet != null)
				{
					return this.metadataLocationSet;
				}
				MetadataSet metadataSet = new MetadataSet();
				foreach (MetadataSection metadataSection in this.parent.Metadata.MetadataSections)
				{
					if ((dialect == null || dialect == metadataSection.Dialect) && (identifier == null || identifier == metadataSection.Identifier))
					{
						metadataSet.MetadataSections.Add(metadataSection);
					}
				}
				return metadataSet;
			}

			// Token: 0x060073E3 RID: 29667 RVA: 0x001B0D00 File Offset: 0x001AEF00
			public System.ServiceModel.Channels.Message Get(System.ServiceModel.Channels.Message request)
			{
				GetResponse getResponse = new GetResponse();
				getResponse.Metadata = this.GatherMetadata(null, null);
				getResponse.Metadata.WriteFilter = this.parent.GetWriteFilter(request, this.listenUri, true);
				if (this.converter == null)
				{
					this.converter = TypedMessageConverter.Create(typeof(GetResponse), "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse");
				}
				return this.converter.ToMessage(getResponse, request.Version);
			}

			// Token: 0x060073E4 RID: 29668 RVA: 0x001B0D73 File Offset: 0x001AEF73
			public IAsyncResult BeginGet(System.ServiceModel.Channels.Message request, AsyncCallback callback, object state)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x060073E5 RID: 29669 RVA: 0x001B0D84 File Offset: 0x001AEF84
			public System.ServiceModel.Channels.Message EndGet(IAsyncResult result)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x04004197 RID: 16791
			internal const string MetadataMexBinding = "ServiceMetadataBehaviorMexBinding";

			// Token: 0x04004198 RID: 16792
			internal const string ContractName = "WS-Transfer";

			// Token: 0x04004199 RID: 16793
			internal const string ContractNamespace = "http://schemas.xmlsoap.org/ws/2004/09/transfer";

			// Token: 0x0400419A RID: 16794
			internal const string GetMethodName = "Get";

			// Token: 0x0400419B RID: 16795
			internal const string RequestAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get";

			// Token: 0x0400419C RID: 16796
			internal const string ReplyAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse";

			// Token: 0x0400419D RID: 16797
			private ServiceMetadataExtension parent;

			// Token: 0x0400419E RID: 16798
			private MetadataSet metadataLocationSet;

			// Token: 0x0400419F RID: 16799
			private TypedMessageConverter converter;

			// Token: 0x040041A0 RID: 16800
			private Uri listenUri;

			// Token: 0x040041A1 RID: 16801
			private bool isListeningOnHttps;
		}

		// Token: 0x02000BA9 RID: 2985
		[ServiceContract]
		internal interface IHttpGetMetadata
		{
			// Token: 0x060073E6 RID: 29670
			[OperationContract(Action = "*", ReplyAction = "*")]
			System.ServiceModel.Channels.Message Get(System.ServiceModel.Channels.Message msg);
		}

		// Token: 0x02000BAA RID: 2986
		internal class HttpGetImpl : ServiceMetadataExtension.IHttpGetMetadata, IDispatchMessageInspector
		{
			// Token: 0x060073E7 RID: 29671 RVA: 0x001B0D95 File Offset: 0x001AEF95
			internal HttpGetImpl(ServiceMetadataExtension parent, Uri listenUri)
			{
				this.parent = parent;
				this.listenUri = listenUri;
			}

			// Token: 0x17001AD8 RID: 6872
			// (get) Token: 0x060073E8 RID: 29672 RVA: 0x001B0DB6 File Offset: 0x001AEFB6
			// (set) Token: 0x060073E9 RID: 29673 RVA: 0x001B0DBE File Offset: 0x001AEFBE
			public ServiceHealthBehaviorBase HealthBehavior { get; set; }

			// Token: 0x17001AD9 RID: 6873
			// (get) Token: 0x060073EA RID: 29674 RVA: 0x001B0DC7 File Offset: 0x001AEFC7
			// (set) Token: 0x060073EB RID: 29675 RVA: 0x001B0DCF File Offset: 0x001AEFCF
			public bool HelpPageEnabled
			{
				get
				{
					return this.helpPageEnabled;
				}
				set
				{
					this.helpPageEnabled = value;
				}
			}

			// Token: 0x17001ADA RID: 6874
			// (get) Token: 0x060073EC RID: 29676 RVA: 0x001B0DD8 File Offset: 0x001AEFD8
			// (set) Token: 0x060073ED RID: 29677 RVA: 0x001B0DE0 File Offset: 0x001AEFE0
			public bool GetWsdlEnabled
			{
				get
				{
					return this.getWsdlEnabled;
				}
				set
				{
					this.getWsdlEnabled = value;
				}
			}

			// Token: 0x060073EE RID: 29678 RVA: 0x001B0DEC File Offset: 0x001AEFEC
			private ServiceMetadataExtension.HttpGetImpl.InitializationData GetInitData()
			{
				if (this.initData == null)
				{
					object obj = this.sync;
					lock (obj)
					{
						if (this.initData == null)
						{
							this.initData = ServiceMetadataExtension.HttpGetImpl.InitializationData.InitializeFrom(this.parent);
						}
					}
				}
				return this.initData;
			}

			// Token: 0x060073EF RID: 29679 RVA: 0x001B0E50 File Offset: 0x001AF050
			private string FindWsdlReference(ServiceMetadataExtension.DynamicAddressUpdateWriter addressUpdater)
			{
				if (this.parent.ExternalMetadataLocation == null || this.parent.ExternalMetadataLocation == ServiceMetadataExtension.EmptyUri)
				{
					return null;
				}
				Uri externalMetadataLocation = this.parent.ExternalMetadataLocation;
				Uri uri = ServiceHostBase.GetUri(this.listenUri, externalMetadataLocation);
				if (addressUpdater != null)
				{
					addressUpdater.UpdateUri(ref uri, false);
				}
				return uri.ToString();
			}

			// Token: 0x060073F0 RID: 29680 RVA: 0x001B0EB4 File Offset: 0x001AF0B4
			private bool TryHandleDocumentationRequest(System.ServiceModel.Channels.Message httpGetRequest, string[] queries, out System.ServiceModel.Channels.Message replyMessage)
			{
				replyMessage = null;
				if (!this.HelpPageEnabled)
				{
					return false;
				}
				if (this.parent.MetadataEnabled)
				{
					string discoUrl = null;
					string singleWsdlUrl = null;
					bool linkMetadata = true;
					ServiceMetadataExtension.DynamicAddressUpdateWriter addressUpdater = null;
					if (this.parent.UpdateAddressDynamically)
					{
						addressUpdater = this.parent.GetDynamicAddressWriter(httpGetRequest, this.listenUri, false);
					}
					string text = this.FindWsdlReference(addressUpdater);
					string httpGetUrl = this.GetHttpGetUrl(addressUpdater);
					if (text == null && httpGetUrl != null)
					{
						text = httpGetUrl + "?wsdl";
						singleWsdlUrl = httpGetUrl + "?singleWsdl";
					}
					if (httpGetUrl != null)
					{
						discoUrl = httpGetUrl + "?disco";
					}
					if (text == null)
					{
						text = this.GetMexUrl(addressUpdater);
						linkMetadata = false;
					}
					replyMessage = new ServiceMetadataExtension.HttpGetImpl.MetadataOnHelpPageMessage(discoUrl, text, singleWsdlUrl, this.GetInitData().ServiceName, this.GetInitData().ClientName, linkMetadata);
				}
				else
				{
					replyMessage = new ServiceMetadataExtension.HttpGetImpl.MetadataOffHelpPageMessage(this.GetInitData().ServiceName);
				}
				ServiceMetadataExtension.HttpGetImpl.AddHttpProperty(replyMessage, HttpStatusCode.OK, "text/html; charset=UTF-8");
				return true;
			}

			// Token: 0x060073F1 RID: 29681 RVA: 0x001B0FA4 File Offset: 0x001AF1A4
			private string GetHttpGetUrl(ServiceMetadataExtension.DynamicAddressUpdateWriter addressUpdater)
			{
				Uri uri = null;
				if (this.listenUri.Scheme == Uri.UriSchemeHttp)
				{
					if (this.parent.HttpGetEnabled)
					{
						uri = this.parent.HttpGetUrl;
					}
					else if (this.parent.HttpsGetEnabled)
					{
						uri = this.parent.HttpsGetUrl;
					}
				}
				else if (this.parent.HttpsGetEnabled)
				{
					uri = this.parent.HttpsGetUrl;
				}
				else if (this.parent.HttpGetEnabled)
				{
					uri = this.parent.HttpGetUrl;
				}
				if (uri != null)
				{
					if (addressUpdater != null)
					{
						addressUpdater.UpdateUri(ref uri, this.listenUri.Scheme != uri.Scheme);
					}
					return uri.ToString();
				}
				return null;
			}

			// Token: 0x060073F2 RID: 29682 RVA: 0x001B1068 File Offset: 0x001AF268
			private string GetMexUrl(ServiceMetadataExtension.DynamicAddressUpdateWriter addressUpdater)
			{
				if (this.parent.MexEnabled)
				{
					Uri mexUrl = this.parent.MexUrl;
					if (addressUpdater != null)
					{
						addressUpdater.UpdateUri(ref mexUrl, false);
					}
					return mexUrl.ToString();
				}
				return null;
			}

			// Token: 0x060073F3 RID: 29683 RVA: 0x001B10A4 File Offset: 0x001AF2A4
			private bool TryHandleHealthRequest(System.ServiceModel.Channels.Message httpGetRequest, string[] queries, out System.ServiceModel.Channels.Message replyMessage)
			{
				replyMessage = null;
				if (this.HealthBehavior == null)
				{
					return false;
				}
				string strA = this.FindQuery(queries);
				if (string.Compare(strA, "health", StringComparison.OrdinalIgnoreCase) != 0)
				{
					return false;
				}
				this.HealthBehavior.HandleHealthRequest(this.parent.owner, httpGetRequest, queries, out replyMessage);
				return true;
			}

			// Token: 0x060073F4 RID: 29684 RVA: 0x001B10F0 File Offset: 0x001AF2F0
			private bool TryHandleMetadataRequest(System.ServiceModel.Channels.Message httpGetRequest, string[] queries, out System.ServiceModel.Channels.Message replyMessage)
			{
				replyMessage = null;
				if (!this.GetWsdlEnabled)
				{
					return false;
				}
				ServiceMetadataExtension.WriteFilter writeFilter = this.parent.GetWriteFilter(httpGetRequest, this.listenUri, false);
				string text = this.FindQuery(queries);
				if (string.IsNullOrEmpty(text))
				{
					if (!this.helpPageEnabled && this.GetInitData().DefaultWsdl != null)
					{
						try
						{
							replyMessage = new ServiceMetadataExtension.HttpGetImpl.ServiceDescriptionMessage(this.GetInitData().DefaultWsdl, writeFilter);
							ServiceMetadataExtension.HttpGetImpl.AddHttpProperty(replyMessage, HttpStatusCode.OK, "text/xml; charset=UTF-8");
							this.GetInitData().FixImportAddresses();
							return true;
						}
						finally
						{
							if (httpGetRequest != null)
							{
								((IDisposable)httpGetRequest).Dispose();
							}
						}
					}
					return false;
				}
				object obj;
				if (this.GetInitData().TryQueryLookup(text, out obj))
				{
					try
					{
						if (obj is ServiceDescription)
						{
							replyMessage = new ServiceMetadataExtension.HttpGetImpl.ServiceDescriptionMessage((ServiceDescription)obj, writeFilter);
						}
						else if (obj is XmlSchema)
						{
							replyMessage = new ServiceMetadataExtension.HttpGetImpl.XmlSchemaMessage((XmlSchema)obj, writeFilter);
						}
						else
						{
							if (!(obj is string))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Bad object in HttpGetImpl docFromQuery table", new object[0])));
							}
							if (!((string)obj == "disco token"))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Bad object in HttpGetImpl docFromQuery table", new object[0])));
							}
							replyMessage = this.CreateDiscoMessage(writeFilter as ServiceMetadataExtension.DynamicAddressUpdateWriter);
						}
						ServiceMetadataExtension.HttpGetImpl.AddHttpProperty(replyMessage, HttpStatusCode.OK, "text/xml; charset=UTF-8");
						this.GetInitData().FixImportAddresses();
						return true;
					}
					finally
					{
						if (httpGetRequest != null)
						{
							((IDisposable)httpGetRequest).Dispose();
						}
					}
				}
				if (string.Compare(text, "wsdl", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (this.GetInitData().DefaultWsdl != null)
					{
						try
						{
							replyMessage = new ServiceMetadataExtension.HttpGetImpl.ServiceDescriptionMessage(this.GetInitData().DefaultWsdl, writeFilter);
							ServiceMetadataExtension.HttpGetImpl.AddHttpProperty(replyMessage, HttpStatusCode.OK, "text/xml; charset=UTF-8");
							this.GetInitData().FixImportAddresses();
							return true;
						}
						finally
						{
							if (httpGetRequest != null)
							{
								((IDisposable)httpGetRequest).Dispose();
							}
						}
					}
					string text2 = this.FindWsdlReference(writeFilter as ServiceMetadataExtension.DynamicAddressUpdateWriter);
					if (text2 != null)
					{
						replyMessage = ServiceMetadataExtension.HttpGetImpl.CreateRedirectMessage(text2);
						return true;
					}
				}
				if (string.Compare(text, "singleWsdl", StringComparison.OrdinalIgnoreCase) == 0)
				{
					ServiceDescription singleWsdl = this.parent.SingleWsdl;
					if (singleWsdl != null)
					{
						try
						{
							replyMessage = new ServiceMetadataExtension.HttpGetImpl.ServiceDescriptionMessage(singleWsdl, writeFilter);
							ServiceMetadataExtension.HttpGetImpl.AddHttpProperty(replyMessage, HttpStatusCode.OK, "text/xml; charset=UTF-8");
							return true;
						}
						finally
						{
							if (httpGetRequest != null)
							{
								((IDisposable)httpGetRequest).Dispose();
							}
						}
					}
				}
				return false;
			}

			// Token: 0x060073F5 RID: 29685 RVA: 0x001B1378 File Offset: 0x001AF578
			private System.ServiceModel.Channels.Message CreateDiscoMessage(ServiceMetadataExtension.DynamicAddressUpdateWriter addressUpdater)
			{
				Uri uri = this.listenUri;
				if (addressUpdater != null)
				{
					addressUpdater.UpdateUri(ref uri, false);
				}
				string wsdlAddress = uri.ToString() + "?wsdl";
				Uri uri2 = null;
				if (this.listenUri.Scheme == Uri.UriSchemeHttp)
				{
					if (this.parent.HttpHelpPageEnabled)
					{
						uri2 = this.parent.HttpHelpPageUrl;
					}
					else if (this.parent.HttpsHelpPageEnabled)
					{
						uri2 = this.parent.HttpsGetUrl;
					}
				}
				else if (this.parent.HttpsHelpPageEnabled)
				{
					uri2 = this.parent.HttpsHelpPageUrl;
				}
				else if (this.parent.HttpHelpPageEnabled)
				{
					uri2 = this.parent.HttpGetUrl;
				}
				if (addressUpdater != null)
				{
					addressUpdater.UpdateUri(ref uri2, false);
				}
				return new ServiceMetadataExtension.HttpGetImpl.DiscoMessage(wsdlAddress, uri2.ToString());
			}

			// Token: 0x060073F6 RID: 29686 RVA: 0x001B1444 File Offset: 0x001AF644
			private string FindQuery(string[] queries)
			{
				string result = null;
				foreach (string text in queries)
				{
					int indexA = (text.Length > 0 && text[0] == '?') ? 1 : 0;
					if (string.Compare(text, indexA, "wsdl", 0, "wsdl".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						result = text;
					}
					else if (string.Compare(text, indexA, "xsd", 0, "xsd".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						result = text;
					}
					else if (string.Compare(text, indexA, "singleWsdl", 0, "singleWsdl".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						result = text;
					}
					else if (string.Compare(text, indexA, "health", 0, "health".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						result = text;
					}
					else if (this.parent.HelpPageEnabled && string.Compare(text, indexA, "disco", 0, "disco".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						result = text;
					}
				}
				return result;
			}

			// Token: 0x060073F7 RID: 29687 RVA: 0x001B1534 File Offset: 0x001AF734
			private System.ServiceModel.Channels.Message ProcessHttpRequest(System.ServiceModel.Channels.Message httpGetRequest)
			{
				string text = httpGetRequest.Properties.Via.Query;
				if (text.Length > 2048)
				{
					return ServiceMetadataExtension.HttpGetImpl.CreateHttpResponseMessage(HttpStatusCode.RequestUriTooLong);
				}
				if (text.StartsWith("?", StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(1);
				}
				string[] queries = (text.Length > 0) ? text.Split(new char[]
				{
					'&'
				}) : ServiceMetadataExtension.HttpGetImpl.NoQueries;
				System.ServiceModel.Channels.Message result = null;
				if (this.TryHandleMetadataRequest(httpGetRequest, queries, out result))
				{
					return result;
				}
				if (this.TryHandleHealthRequest(httpGetRequest, queries, out result))
				{
					return result;
				}
				if (this.TryHandleDocumentationRequest(httpGetRequest, queries, out result))
				{
					return result;
				}
				return ServiceMetadataExtension.HttpGetImpl.CreateHttpResponseMessage(HttpStatusCode.MethodNotAllowed);
			}

			// Token: 0x060073F8 RID: 29688 RVA: 0x001B15D8 File Offset: 0x001AF7D8
			public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
			{
				return request.Version;
			}

			// Token: 0x060073F9 RID: 29689 RVA: 0x001B15E4 File Offset: 0x001AF7E4
			public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
			{
				if (reply != null && reply.IsFault)
				{
					string @string = SR.GetString("SFxInternalServerError");
					ExceptionDetail exceptionDetail = null;
					MessageFault messageFault = MessageFault.CreateFault(reply, 65536);
					if (messageFault.HasDetail)
					{
						exceptionDetail = messageFault.GetDetail<ExceptionDetail>();
						if (exceptionDetail != null)
						{
							@string = SR.GetString("SFxDocExt_Error");
						}
					}
					reply = new ServiceMetadataExtension.HttpGetImpl.MetadataOnHelpPageMessage(@string, exceptionDetail);
					ServiceMetadataExtension.HttpGetImpl.AddHttpProperty(reply, HttpStatusCode.InternalServerError, "text/html; charset=UTF-8");
				}
			}

			// Token: 0x060073FA RID: 29690 RVA: 0x001B164F File Offset: 0x001AF84F
			public System.ServiceModel.Channels.Message Get(System.ServiceModel.Channels.Message message)
			{
				return this.ProcessHttpRequest(message);
			}

			// Token: 0x060073FB RID: 29691 RVA: 0x001B1658 File Offset: 0x001AF858
			private static void AddHttpProperty(System.ServiceModel.Channels.Message message, HttpStatusCode status, string contentType)
			{
				HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty();
				httpResponseMessageProperty.StatusCode = status;
				httpResponseMessageProperty.Headers.Add(HttpResponseHeader.ContentType, contentType);
				message.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
			}

			// Token: 0x060073FC RID: 29692 RVA: 0x001B1694 File Offset: 0x001AF894
			private static System.ServiceModel.Channels.Message CreateRedirectMessage(string redirectedDestination)
			{
				System.ServiceModel.Channels.Message message = ServiceMetadataExtension.HttpGetImpl.CreateHttpResponseMessage(HttpStatusCode.TemporaryRedirect);
				HttpResponseMessageProperty httpResponseMessageProperty = (HttpResponseMessageProperty)message.Properties[HttpResponseMessageProperty.Name];
				httpResponseMessageProperty.Headers["Location"] = redirectedDestination;
				return message;
			}

			// Token: 0x060073FD RID: 29693 RVA: 0x001B16D4 File Offset: 0x001AF8D4
			private static System.ServiceModel.Channels.Message CreateHttpResponseMessage(HttpStatusCode code)
			{
				System.ServiceModel.Channels.Message message = new NullMessage();
				HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty();
				httpResponseMessageProperty.StatusCode = code;
				message.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
				return message;
			}

			// Token: 0x040041A2 RID: 16802
			private const string DiscoToken = "disco token";

			// Token: 0x040041A3 RID: 16803
			private const string DiscoQueryString = "disco";

			// Token: 0x040041A4 RID: 16804
			private const string WsdlQueryString = "wsdl";

			// Token: 0x040041A5 RID: 16805
			private const string XsdQueryString = "xsd";

			// Token: 0x040041A6 RID: 16806
			private const string SingleWsdlQueryString = "singleWsdl";

			// Token: 0x040041A7 RID: 16807
			private const string HealthQueryString = "health";

			// Token: 0x040041A8 RID: 16808
			private const string HtmlContentType = "text/html; charset=UTF-8";

			// Token: 0x040041A9 RID: 16809
			private const string XmlContentType = "text/xml; charset=UTF-8";

			// Token: 0x040041AA RID: 16810
			private const int closeTimeoutInSeconds = 90;

			// Token: 0x040041AB RID: 16811
			private const int maxQueryStringChars = 2048;

			// Token: 0x040041AC RID: 16812
			internal const string MetadataHttpGetBinding = "ServiceMetadataBehaviorHttpGetBinding";

			// Token: 0x040041AD RID: 16813
			internal const string ContractName = "IHttpGetHelpPageAndMetadataContract";

			// Token: 0x040041AE RID: 16814
			internal const string ContractNamespace = "http://schemas.microsoft.com/2006/04/http/metadata";

			// Token: 0x040041AF RID: 16815
			internal const string GetMethodName = "Get";

			// Token: 0x040041B0 RID: 16816
			internal const string RequestAction = "*";

			// Token: 0x040041B1 RID: 16817
			internal const string ReplyAction = "*";

			// Token: 0x040041B2 RID: 16818
			internal const string HtmlBreak = "<BR/>";

			// Token: 0x040041B3 RID: 16819
			private static string[] NoQueries = new string[0];

			// Token: 0x040041B4 RID: 16820
			private ServiceMetadataExtension parent;

			// Token: 0x040041B5 RID: 16821
			private object sync = new object();

			// Token: 0x040041B6 RID: 16822
			private ServiceMetadataExtension.HttpGetImpl.InitializationData initData;

			// Token: 0x040041B7 RID: 16823
			private Uri listenUri;

			// Token: 0x040041B8 RID: 16824
			private bool helpPageEnabled;

			// Token: 0x040041B9 RID: 16825
			private bool getWsdlEnabled;

			// Token: 0x02000F06 RID: 3846
			private class InitializationData
			{
				// Token: 0x060085AD RID: 34221 RVA: 0x001EF414 File Offset: 0x001ED614
				private InitializationData(Dictionary<string, object> docFromQuery, Dictionary<object, string> queryFromDoc, ServiceDescriptionCollection wsdls, XmlSchemaSet xsds)
				{
					this.docFromQuery = docFromQuery;
					this.queryFromDoc = queryFromDoc;
					this.wsdls = wsdls;
					this.xsds = xsds;
				}

				// Token: 0x060085AE RID: 34222 RVA: 0x001EF439 File Offset: 0x001ED639
				public bool TryQueryLookup(string query, out object doc)
				{
					return this.docFromQuery.TryGetValue(query, out doc);
				}

				// Token: 0x060085AF RID: 34223 RVA: 0x001EF448 File Offset: 0x001ED648
				public static ServiceMetadataExtension.HttpGetImpl.InitializationData InitializeFrom(ServiceMetadataExtension extension)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
					Dictionary<object, string> dictionary2 = new Dictionary<object, string>();
					ServiceDescriptionCollection serviceDescriptionCollection = ServiceMetadataExtension.HttpGetImpl.InitializationData.CollectWsdls(extension.Metadata);
					XmlSchemaSet xmlSchemaSet = ServiceMetadataExtension.HttpGetImpl.InitializationData.CollectXsds(extension.Metadata);
					ServiceDescription serviceDescription = null;
					Service anyService = ServiceMetadataExtension.HttpGetImpl.InitializationData.GetAnyService(serviceDescriptionCollection);
					if (anyService != null)
					{
						serviceDescription = anyService.ServiceDescription;
					}
					int num = 0;
					foreach (object obj in serviceDescriptionCollection)
					{
						ServiceDescription serviceDescription2 = (ServiceDescription)obj;
						string text = "wsdl";
						if (serviceDescription2 != serviceDescription)
						{
							text = text + "=wsdl" + num++.ToString(CultureInfo.InvariantCulture);
						}
						dictionary.Add(text, serviceDescription2);
						dictionary2.Add(serviceDescription2, text);
					}
					int num2 = 0;
					foreach (object obj2 in xmlSchemaSet.Schemas())
					{
						XmlSchema xmlSchema = (XmlSchema)obj2;
						string text2 = "xsd=xsd" + num2++.ToString(CultureInfo.InvariantCulture);
						dictionary.Add(text2, xmlSchema);
						dictionary2.Add(xmlSchema, text2);
					}
					if (extension.HelpPageEnabled)
					{
						string text3 = "disco";
						dictionary.Add(text3, "disco token");
						dictionary2.Add("disco token", text3);
					}
					return new ServiceMetadataExtension.HttpGetImpl.InitializationData(dictionary, dictionary2, serviceDescriptionCollection, xmlSchemaSet)
					{
						DefaultWsdl = serviceDescription,
						ServiceName = ServiceMetadataExtension.HttpGetImpl.InitializationData.GetAnyWsdlName(serviceDescriptionCollection),
						ClientName = ClientClassGenerator.GetClientClassName(ServiceMetadataExtension.HttpGetImpl.InitializationData.GetAnyContractName(serviceDescriptionCollection) ?? "IHello")
					};
				}

				// Token: 0x060085B0 RID: 34224 RVA: 0x001EF610 File Offset: 0x001ED810
				private static ServiceDescriptionCollection CollectWsdls(MetadataSet metadata)
				{
					ServiceDescriptionCollection serviceDescriptionCollection = new ServiceDescriptionCollection();
					foreach (MetadataSection metadataSection in metadata.MetadataSections)
					{
						if (metadataSection.Metadata is ServiceDescription)
						{
							serviceDescriptionCollection.Add((ServiceDescription)metadataSection.Metadata);
						}
					}
					return serviceDescriptionCollection;
				}

				// Token: 0x060085B1 RID: 34225 RVA: 0x001EF67C File Offset: 0x001ED87C
				private static XmlSchemaSet CollectXsds(MetadataSet metadata)
				{
					XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
					xmlSchemaSet.XmlResolver = null;
					foreach (MetadataSection metadataSection in metadata.MetadataSections)
					{
						if (metadataSection.Metadata is XmlSchema)
						{
							xmlSchemaSet.Add((XmlSchema)metadataSection.Metadata);
						}
					}
					return xmlSchemaSet;
				}

				// Token: 0x060085B2 RID: 34226 RVA: 0x001EF6F0 File Offset: 0x001ED8F0
				internal void FixImportAddresses()
				{
					foreach (object obj in this.wsdls)
					{
						ServiceDescription wsdlDoc = (ServiceDescription)obj;
						this.FixImportAddresses(wsdlDoc);
					}
					foreach (object obj2 in this.xsds.Schemas())
					{
						XmlSchema xsdDoc = (XmlSchema)obj2;
						this.FixImportAddresses(xsdDoc);
					}
				}

				// Token: 0x060085B3 RID: 34227 RVA: 0x001EF798 File Offset: 0x001ED998
				private void FixImportAddresses(ServiceDescription wsdlDoc)
				{
					foreach (object obj in wsdlDoc.Imports)
					{
						Import import = (Import)obj;
						if (string.IsNullOrEmpty(import.Location))
						{
							ServiceDescription serviceDescription = this.wsdls[import.Namespace ?? string.Empty];
							if (serviceDescription != null)
							{
								string str = this.queryFromDoc[serviceDescription];
								import.Location = "{%BaseAddress%}?" + str;
							}
						}
					}
					if (wsdlDoc.Types != null)
					{
						foreach (object obj2 in wsdlDoc.Types.Schemas)
						{
							XmlSchema xsdDoc = (XmlSchema)obj2;
							this.FixImportAddresses(xsdDoc);
						}
					}
				}

				// Token: 0x060085B4 RID: 34228 RVA: 0x001EF894 File Offset: 0x001EDA94
				private void FixImportAddresses(XmlSchema xsdDoc)
				{
					foreach (XmlSchemaObject xmlSchemaObject in xsdDoc.Includes)
					{
						XmlSchemaExternal xmlSchemaExternal = xmlSchemaObject as XmlSchemaExternal;
						if (xmlSchemaExternal != null && string.IsNullOrEmpty(xmlSchemaExternal.SchemaLocation))
						{
							string text = (xmlSchemaExternal is XmlSchemaImport) ? ((XmlSchemaImport)xmlSchemaExternal).Namespace : xsdDoc.TargetNamespace;
							foreach (object obj in this.xsds.Schemas(text ?? string.Empty))
							{
								XmlSchema xmlSchema = (XmlSchema)obj;
								if (xmlSchema != xsdDoc)
								{
									string str = this.queryFromDoc[xmlSchema];
									xmlSchemaExternal.SchemaLocation = "{%BaseAddress%}?" + str;
									break;
								}
							}
						}
					}
				}

				// Token: 0x060085B5 RID: 34229 RVA: 0x001EF9A4 File Offset: 0x001EDBA4
				private static string GetAnyContractName(ServiceDescriptionCollection wsdls)
				{
					foreach (object obj in wsdls)
					{
						ServiceDescription serviceDescription = (ServiceDescription)obj;
						foreach (object obj2 in serviceDescription.Services)
						{
							Service service = (Service)obj2;
							foreach (object obj3 in service.Ports)
							{
								Port port = (Port)obj3;
								if (!port.Binding.IsEmpty)
								{
									System.Web.Services.Description.Binding binding = wsdls.GetBinding(port.Binding);
									if (!binding.Type.IsEmpty)
									{
										return binding.Type.Name;
									}
								}
							}
						}
					}
					return null;
				}

				// Token: 0x060085B6 RID: 34230 RVA: 0x001EFACC File Offset: 0x001EDCCC
				private static Service GetAnyService(ServiceDescriptionCollection wsdls)
				{
					foreach (object obj in wsdls)
					{
						ServiceDescription serviceDescription = (ServiceDescription)obj;
						if (serviceDescription.Services.Count > 0)
						{
							return serviceDescription.Services[0];
						}
					}
					return null;
				}

				// Token: 0x060085B7 RID: 34231 RVA: 0x001EFB3C File Offset: 0x001EDD3C
				private static string GetAnyWsdlName(ServiceDescriptionCollection wsdls)
				{
					foreach (object obj in wsdls)
					{
						ServiceDescription serviceDescription = (ServiceDescription)obj;
						if (!string.IsNullOrEmpty(serviceDescription.Name))
						{
							return serviceDescription.Name;
						}
					}
					return null;
				}

				// Token: 0x04004D6D RID: 19821
				private readonly Dictionary<string, object> docFromQuery;

				// Token: 0x04004D6E RID: 19822
				private readonly Dictionary<object, string> queryFromDoc;

				// Token: 0x04004D6F RID: 19823
				private ServiceDescriptionCollection wsdls;

				// Token: 0x04004D70 RID: 19824
				private XmlSchemaSet xsds;

				// Token: 0x04004D71 RID: 19825
				public string ServiceName;

				// Token: 0x04004D72 RID: 19826
				public string ClientName;

				// Token: 0x04004D73 RID: 19827
				public ServiceDescription DefaultWsdl;
			}

			// Token: 0x02000F07 RID: 3847
			private class DiscoMessage : ContentOnlyMessage
			{
				// Token: 0x060085B8 RID: 34232 RVA: 0x001EFBA4 File Offset: 0x001EDDA4
				public DiscoMessage(string wsdlAddress, string docAddress)
				{
					this.wsdlAddress = wsdlAddress;
					this.docAddress = docAddress;
				}

				// Token: 0x060085B9 RID: 34233 RVA: 0x001EFBBC File Offset: 0x001EDDBC
				protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
				{
					writer.WriteStartDocument();
					writer.WriteStartElement("discovery", "http://schemas.xmlsoap.org/disco/");
					writer.WriteStartElement("contractRef", "http://schemas.xmlsoap.org/disco/scl/");
					writer.WriteAttributeString("ref", this.wsdlAddress);
					writer.WriteAttributeString("docRef", this.docAddress);
					writer.WriteEndElement();
					writer.WriteEndElement();
					writer.WriteEndDocument();
				}

				// Token: 0x04004D74 RID: 19828
				private string wsdlAddress;

				// Token: 0x04004D75 RID: 19829
				private string docAddress;
			}

			// Token: 0x02000F08 RID: 3848
			private class MetadataOnHelpPageMessage : ContentOnlyMessage
			{
				// Token: 0x060085BA RID: 34234 RVA: 0x001EFC23 File Offset: 0x001EDE23
				public MetadataOnHelpPageMessage(string discoUrl, string metadataUrl, string singleWsdlUrl, string serviceName, string clientName, bool linkMetadata)
				{
					this.discoUrl = discoUrl;
					this.metadataUrl = metadataUrl;
					this.singleWsdlUrl = singleWsdlUrl;
					this.serviceName = serviceName;
					this.clientName = clientName;
					this.linkMetadata = linkMetadata;
				}

				// Token: 0x060085BB RID: 34235 RVA: 0x001EFC58 File Offset: 0x001EDE58
				public MetadataOnHelpPageMessage(string errorMessage, ExceptionDetail exceptionDetail)
				{
					this.errorMessage = errorMessage;
					this.exceptionDetail = exceptionDetail;
				}

				// Token: 0x060085BC RID: 34236 RVA: 0x001EFC70 File Offset: 0x001EDE70
				protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
				{
					ServiceMetadataExtension.HttpGetImpl.MetadataOnHelpPageMessage.HelpPageWriter helpPageWriter = new ServiceMetadataExtension.HttpGetImpl.MetadataOnHelpPageMessage.HelpPageWriter(writer);
					writer.WriteStartElement("HTML");
					writer.WriteAttributeString("lang", this.GetISOLanguageNameFromResourceManager(SR.Resources));
					writer.WriteStartElement("HEAD");
					if (!string.IsNullOrEmpty(this.discoUrl))
					{
						helpPageWriter.WriteDiscoLink(this.discoUrl);
					}
					helpPageWriter.WriteStyleSheet();
					helpPageWriter.WriteTitle((!string.IsNullOrEmpty(this.serviceName)) ? SR.GetString("SFxDocExt_MainPageTitle", new object[]
					{
						this.serviceName
					}) : SR.GetString("SFxDocExt_MainPageTitleNoServiceName"));
					if (!string.IsNullOrEmpty(this.errorMessage))
					{
						helpPageWriter.WriteError(this.errorMessage);
						if (this.exceptionDetail != null)
						{
							helpPageWriter.WriteExceptionDetail(this.exceptionDetail);
						}
					}
					else
					{
						helpPageWriter.WriteToolUsage(this.metadataUrl, this.singleWsdlUrl, this.linkMetadata);
						helpPageWriter.WriteSampleCode(this.clientName);
					}
					writer.WriteEndElement();
					writer.WriteEndElement();
				}

				// Token: 0x060085BD RID: 34237 RVA: 0x001EFD70 File Offset: 0x001EDF70
				private string GetISOLanguageNameFromResourceManager(ResourceManager rm)
				{
					try
					{
						CultureInfo cultureInfo = CultureInfo.CurrentCulture;
						while (cultureInfo.Name.Length > 0)
						{
							if (rm.GetResourceSet(cultureInfo, false, false) != null)
							{
								return cultureInfo.TwoLetterISOLanguageName;
							}
							cultureInfo = cultureInfo.Parent;
						}
					}
					catch (Exception)
					{
					}
					return "en";
				}

				// Token: 0x04004D76 RID: 19830
				private string discoUrl;

				// Token: 0x04004D77 RID: 19831
				private string metadataUrl;

				// Token: 0x04004D78 RID: 19832
				private string singleWsdlUrl;

				// Token: 0x04004D79 RID: 19833
				private string serviceName;

				// Token: 0x04004D7A RID: 19834
				private string clientName;

				// Token: 0x04004D7B RID: 19835
				private bool linkMetadata;

				// Token: 0x04004D7C RID: 19836
				private string errorMessage;

				// Token: 0x04004D7D RID: 19837
				private ExceptionDetail exceptionDetail;

				// Token: 0x02000FC0 RID: 4032
				private struct HelpPageWriter
				{
					// Token: 0x060088B1 RID: 34993 RVA: 0x001FD186 File Offset: 0x001FB386
					public HelpPageWriter(XmlWriter writer)
					{
						this.writer = writer;
					}

					// Token: 0x060088B2 RID: 34994 RVA: 0x001FD18F File Offset: 0x001FB38F
					internal void WriteClass(string className)
					{
						this.writer.WriteStartElement("font");
						this.writer.WriteAttributeString("color", "black");
						this.writer.WriteString(className);
						this.writer.WriteEndElement();
					}

					// Token: 0x060088B3 RID: 34995 RVA: 0x001FD1CD File Offset: 0x001FB3CD
					internal void WriteComment(string comment)
					{
						this.writer.WriteStartElement("font");
						this.writer.WriteAttributeString("color", "darkgreen");
						this.writer.WriteString(comment);
						this.writer.WriteEndElement();
					}

					// Token: 0x060088B4 RID: 34996 RVA: 0x001FD20C File Offset: 0x001FB40C
					internal void WriteDiscoLink(string discoUrl)
					{
						this.writer.WriteStartElement("link");
						this.writer.WriteAttributeString("rel", "alternate");
						this.writer.WriteAttributeString("type", "text/xml");
						this.writer.WriteAttributeString("href", discoUrl);
						this.writer.WriteEndElement();
					}

					// Token: 0x060088B5 RID: 34997 RVA: 0x001FD26F File Offset: 0x001FB46F
					internal void WriteError(string message)
					{
						this.writer.WriteStartElement("P");
						this.writer.WriteAttributeString("class", "intro");
						this.writer.WriteString(message);
						this.writer.WriteEndElement();
					}

					// Token: 0x060088B6 RID: 34998 RVA: 0x001FD2AD File Offset: 0x001FB4AD
					internal void WriteKeyword(string keyword)
					{
						this.writer.WriteStartElement("font");
						this.writer.WriteAttributeString("color", "blue");
						this.writer.WriteString(keyword);
						this.writer.WriteEndElement();
					}

					// Token: 0x060088B7 RID: 34999 RVA: 0x001FD2EC File Offset: 0x001FB4EC
					internal void WriteSampleCode(string clientName)
					{
						this.writer.WriteStartElement("P");
						this.writer.WriteAttributeString("class", "intro");
						this.writer.WriteRaw(SR.GetString("SFxDocExt_MainPageIntro2"));
						this.writer.WriteEndElement();
						this.writer.WriteRaw("<h2 class='intro'>C#</h2><br />");
						this.writer.WriteStartElement("PRE");
						this.WriteKeyword("class ");
						this.WriteClass("Test\n");
						this.writer.WriteString("{\n");
						this.WriteKeyword("    static void ");
						this.writer.WriteString("Main()\n");
						this.writer.WriteString("    {\n");
						this.writer.WriteString("        ");
						this.WriteClass(clientName);
						this.writer.WriteString(" client = ");
						this.WriteKeyword("new ");
						this.WriteClass(clientName);
						this.writer.WriteString("();\n\n");
						this.WriteComment("        // " + SR.GetString("SFxDocExt_MainPageComment") + "\n\n");
						this.WriteComment("        // " + SR.GetString("SFxDocExt_MainPageComment2") + "\n");
						this.writer.WriteString("        client.Close();\n");
						this.writer.WriteString("    }\n");
						this.writer.WriteString("}\n");
						this.writer.WriteEndElement();
						this.writer.WriteRaw("<BR/>");
						this.writer.WriteRaw("<h2 class='intro'>Visual Basic</h2><br />");
						this.writer.WriteStartElement("PRE");
						this.WriteKeyword("Class ");
						this.WriteClass("Test\n");
						this.WriteKeyword("    Shared Sub ");
						this.writer.WriteString("Main()\n");
						this.WriteKeyword("        Dim ");
						this.writer.WriteString("client As ");
						this.WriteClass(clientName);
						this.writer.WriteString(" = ");
						this.WriteKeyword("New ");
						this.WriteClass(clientName);
						this.writer.WriteString("()\n");
						this.WriteComment("        ' " + SR.GetString("SFxDocExt_MainPageComment") + "\n\n");
						this.WriteComment("        ' " + SR.GetString("SFxDocExt_MainPageComment2") + "\n");
						this.writer.WriteString("        client.Close()\n");
						this.WriteKeyword("    End Sub\n");
						this.WriteKeyword("End Class");
						this.writer.WriteEndElement();
					}

					// Token: 0x060088B8 RID: 35000 RVA: 0x001FD595 File Offset: 0x001FB795
					internal void WriteExceptionDetail(ExceptionDetail exceptionDetail)
					{
						this.writer.WriteStartElement("PRE");
						this.writer.WriteString(exceptionDetail.ToString().Replace("\r", ""));
						this.writer.WriteEndElement();
					}

					// Token: 0x060088B9 RID: 35001 RVA: 0x001FD5D4 File Offset: 0x001FB7D4
					internal void WriteStyleSheet()
					{
						this.writer.WriteStartElement("STYLE");
						this.writer.WriteAttributeString("type", "text/css");
						this.writer.WriteString("#content{ FONT-SIZE: 0.7em; PADDING-BOTTOM: 2em; MARGIN-LEFT: 30px}");
						this.writer.WriteString("BODY{MARGIN-TOP: 0px; MARGIN-LEFT: 0px; COLOR: #000000; FONT-FAMILY: Verdana; BACKGROUND-COLOR: white}");
						this.writer.WriteString("P{MARGIN-TOP: 0px; MARGIN-BOTTOM: 12px; COLOR: #000000; FONT-FAMILY: Verdana}");
						this.writer.WriteString("PRE{BORDER-RIGHT: #f0f0e0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #f0f0e0 1px solid; MARGIN-TOP: -5px; PADDING-LEFT: 5px; FONT-SIZE: 1.2em; PADDING-BOTTOM: 5px; BORDER-LEFT: #f0f0e0 1px solid; PADDING-TOP: 5px; BORDER-BOTTOM: #f0f0e0 1px solid; FONT-FAMILY: Courier New; BACKGROUND-COLOR: #e5e5cc}");
						this.writer.WriteString(".heading1{MARGIN-TOP: 0px; PADDING-LEFT: 15px; FONT-WEIGHT: normal; FONT-SIZE: 26px; MARGIN-BOTTOM: 0px; PADDING-BOTTOM: 3px; MARGIN-LEFT: -30px; WIDTH: 100%; COLOR: #ffffff; PADDING-TOP: 10px; FONT-FAMILY: Tahoma; BACKGROUND-COLOR: #003366}");
						this.writer.WriteString(".intro{display: block; font-size: 1em;}");
						this.writer.WriteEndElement();
					}

					// Token: 0x060088BA RID: 35002 RVA: 0x001FD674 File Offset: 0x001FB874
					internal void WriteTitle(string title)
					{
						this.writer.WriteElementString("TITLE", title);
						this.writer.WriteEndElement();
						this.writer.WriteStartElement("BODY");
						this.writer.WriteStartElement("DIV");
						this.writer.WriteAttributeString("id", "content");
						this.writer.WriteAttributeString("role", "main");
						this.writer.WriteStartElement("h1");
						this.writer.WriteAttributeString("class", "heading1");
						this.writer.WriteString(title);
						this.writer.WriteEndElement();
						this.writer.WriteRaw("<BR/>");
					}

					// Token: 0x060088BB RID: 35003 RVA: 0x001FD734 File Offset: 0x001FB934
					internal void WriteToolUsage(string wsdlUrl, string singleWsdlUrl, bool linkMetadata)
					{
						this.writer.WriteStartElement("P");
						this.writer.WriteAttributeString("class", "intro");
						if (wsdlUrl != null)
						{
							this.WriteMetadataAddress("SFxDocExt_MainPageIntro1a", "svcutil.exe ", wsdlUrl, linkMetadata);
							if (singleWsdlUrl != null)
							{
								this.writer.WriteStartElement("P");
								this.WriteMetadataAddress("SFxDocExt_MainPageIntroSingleWsdl", null, singleWsdlUrl, linkMetadata);
								this.writer.WriteEndElement();
							}
						}
						else
						{
							this.writer.WriteRaw(SR.GetString("SFxDocExt_MainPageIntro1b"));
						}
						this.writer.WriteEndElement();
					}

					// Token: 0x060088BC RID: 35004 RVA: 0x001FD7CC File Offset: 0x001FB9CC
					private void WriteMetadataAddress(string introductionText, string clientToolName, string wsdlUrl, bool linkMetadata)
					{
						this.writer.WriteRaw(SR.GetString(introductionText));
						this.writer.WriteRaw("<BR/>");
						this.writer.WriteStartElement("PRE");
						if (!string.IsNullOrEmpty(clientToolName))
						{
							this.writer.WriteString(clientToolName);
						}
						if (linkMetadata)
						{
							this.writer.WriteStartElement("A");
							this.writer.WriteAttributeString("HREF", wsdlUrl);
						}
						this.writer.WriteString(wsdlUrl);
						if (linkMetadata)
						{
							this.writer.WriteEndElement();
						}
						this.writer.WriteEndElement();
					}

					// Token: 0x04005062 RID: 20578
					private XmlWriter writer;
				}
			}

			// Token: 0x02000F09 RID: 3849
			private class MetadataOffHelpPageMessage : ContentOnlyMessage
			{
				// Token: 0x060085BE RID: 34238 RVA: 0x001EFDCC File Offset: 0x001EDFCC
				public MetadataOffHelpPageMessage(string serviceName)
				{
				}

				// Token: 0x060085BF RID: 34239 RVA: 0x001EFDD4 File Offset: 0x001EDFD4
				protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
				{
					writer.WriteStartElement("HTML");
					writer.WriteStartElement("HEAD");
					writer.WriteRaw(string.Format(CultureInfo.InvariantCulture, "<STYLE type=\"text/css\">#content{{ FONT-SIZE: 0.7em; PADDING-BOTTOM: 2em; MARGIN-LEFT: 30px}}BODY{{MARGIN-TOP: 0px; MARGIN-LEFT: 0px; COLOR: #000000; FONT-FAMILY: Verdana; BACKGROUND-COLOR: white}}P{{MARGIN-TOP: 0px; MARGIN-BOTTOM: 12px; COLOR: #000000; FONT-FAMILY: Verdana}}PRE{{BORDER-RIGHT: #f0f0e0 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #f0f0e0 1px solid; MARGIN-TOP: -5px; PADDING-LEFT: 5px; FONT-SIZE: 1.2em; PADDING-BOTTOM: 5px; BORDER-LEFT: #f0f0e0 1px solid; PADDING-TOP: 5px; BORDER-BOTTOM: #f0f0e0 1px solid; FONT-FAMILY: Courier New; BACKGROUND-COLOR: #e5e5cc}}.heading1{{MARGIN-TOP: 0px; PADDING-LEFT: 15px; FONT-WEIGHT: normal; FONT-SIZE: 26px; MARGIN-BOTTOM: 0px; PADDING-BOTTOM: 3px; MARGIN-LEFT: -30px; WIDTH: 100%; COLOR: #ffffff; PADDING-TOP: 10px; FONT-FAMILY: Tahoma; BACKGROUND-COLOR: #003366}}.intro{{MARGIN-LEFT: -15px}}</STYLE>\r\n<TITLE>Service</TITLE>", new object[0]));
					writer.WriteEndElement();
					writer.WriteRaw(string.Format(CultureInfo.InvariantCulture, "<BODY>\r\n<DIV id=\"content\">\r\n<P class=\"heading1\">Service</P>\r\n<BR/>\r\n<P class=\"intro\">{0}</P>\r\n<PRE>\r\n<font color=\"blue\">&lt;<font color=\"darkred\">behaviors</font>&gt;</font>\r\n<font color=\"blue\">    &lt;<font color=\"darkred\">serviceBehaviors</font>&gt;</font>\r\n<font color=\"blue\">        &lt;<font color=\"darkred\">behavior </font><font color=\"red\">name</font>=<font color=\"black\">\"</font>MyServiceTypeBehaviors<font color=\"black\">\" </font>&gt;</font>\r\n<font color=\"blue\">            &lt;<font color=\"darkred\">serviceMetadata </font><font color=\"red\">httpGetEnabled</font>=<font color=\"black\">\"</font>true<font color=\"black\">\" </font>/&gt;</font>\r\n<font color=\"blue\">        &lt;<font color=\"darkred\">/behavior</font>&gt;</font>\r\n<font color=\"blue\">    &lt;<font color=\"darkred\">/serviceBehaviors</font>&gt;</font>\r\n<font color=\"blue\">&lt;<font color=\"darkred\">/behaviors</font>&gt;</font>\r\n</PRE>\r\n<P class=\"intro\">{1}</P>\r\n<PRE>\r\n<font color=\"blue\">&lt;<font color=\"darkred\">service </font><font color=\"red\">name</font>=<font color=\"black\">\"</font><i>MyNamespace.MyServiceType</i><font color=\"black\">\" </font><font color=\"red\">behaviorConfiguration</font>=<font color=\"black\">\"</font><i>MyServiceTypeBehaviors</i><font color=\"black\">\" </font>&gt;</font>\r\n</PRE>\r\n<P class=\"intro\">{2}</P>\r\n<PRE>\r\n<font color=\"blue\">&lt;<font color=\"darkred\">endpoint </font><font color=\"red\">contract</font>=<font color=\"black\">\"</font>IMetadataExchange<font color=\"black\">\" </font><font color=\"red\">binding</font>=<font color=\"black\">\"</font>mexHttpBinding<font color=\"black\">\" </font><font color=\"red\">address</font>=<font color=\"black\">\"</font>mex<font color=\"black\">\" </font>/&gt;</font>\r\n</PRE>\r\n\r\n<P class=\"intro\">{3}</P>\r\n<PRE>\r\n<font color=\"blue\">&lt;<font color=\"darkred\">configuration</font>&gt;</font>\r\n<font color=\"blue\">    &lt;<font color=\"darkred\">system.serviceModel</font>&gt;</font>\r\n \r\n<font color=\"blue\">        &lt;<font color=\"darkred\">services</font>&gt;</font>\r\n<font color=\"blue\">            &lt;!-- <font color=\"green\">{4}</font> --&gt;</font>\r\n<font color=\"blue\">            &lt;<font color=\"darkred\">service </font><font color=\"red\">name</font>=<font color=\"black\">\"</font><i>MyNamespace.MyServiceType</i><font color=\"black\">\" </font><font color=\"red\">behaviorConfiguration</font>=<font color=\"black\">\"</font><i>MyServiceTypeBehaviors</i><font color=\"black\">\" </font>&gt;</font>\r\n<font color=\"blue\">                &lt;!-- <font color=\"green\">{5}</font> --&gt;</font>\r\n<font color=\"blue\">                &lt;!-- <font color=\"green\">{6}</font> --&gt;</font>\r\n<font color=\"blue\">                &lt;<font color=\"darkred\">endpoint </font><font color=\"red\">contract</font>=<font color=\"black\">\"</font>IMetadataExchange<font color=\"black\">\" </font><font color=\"red\">binding</font>=<font color=\"black\">\"</font>mexHttpBinding<font color=\"black\">\" </font><font color=\"red\">address</font>=<font color=\"black\">\"</font>mex<font color=\"black\">\" </font>/&gt;</font>\r\n<font color=\"blue\">            &lt;<font color=\"darkred\">/service</font>&gt;</font>\r\n<font color=\"blue\">        &lt;<font color=\"darkred\">/services</font>&gt;</font>\r\n \r\n<font color=\"blue\">        &lt;<font color=\"darkred\">behaviors</font>&gt;</font>\r\n<font color=\"blue\">            &lt;<font color=\"darkred\">serviceBehaviors</font>&gt;</font>\r\n<font color=\"blue\">                &lt;<font color=\"darkred\">behavior </font><font color=\"red\">name</font>=<font color=\"black\">\"</font><i>MyServiceTypeBehaviors</i><font color=\"black\">\" </font>&gt;</font>\r\n<font color=\"blue\">                    &lt;!-- <font color=\"green\">{7}</font> --&gt;</font>\r\n<font color=\"blue\">                    &lt;<font color=\"darkred\">serviceMetadata </font><font color=\"red\">httpGetEnabled</font>=<font color=\"black\">\"</font>true<font color=\"black\">\" </font>/&gt;</font>\r\n<font color=\"blue\">                &lt;<font color=\"darkred\">/behavior</font>&gt;</font>\r\n<font color=\"blue\">            &lt;<font color=\"darkred\">/serviceBehaviors</font>&gt;</font>\r\n<font color=\"blue\">        &lt;<font color=\"darkred\">/behaviors</font>&gt;</font>\r\n \r\n<font color=\"blue\">    &lt;<font color=\"darkred\">/system.serviceModel</font>&gt;</font>\r\n<font color=\"blue\">&lt;<font color=\"darkred\">/configuration</font>&gt;</font>\r\n</PRE>\r\n<P class=\"intro\">{8}</P>\r\n</DIV>\r\n</BODY>", new object[]
					{
						SR.GetString("SFxDocExt_NoMetadataSection1"),
						SR.GetString("SFxDocExt_NoMetadataSection2"),
						SR.GetString("SFxDocExt_NoMetadataSection3"),
						SR.GetString("SFxDocExt_NoMetadataSection4"),
						SR.GetString("SFxDocExt_NoMetadataConfigComment1"),
						SR.GetString("SFxDocExt_NoMetadataConfigComment2"),
						SR.GetString("SFxDocExt_NoMetadataConfigComment3"),
						SR.GetString("SFxDocExt_NoMetadataConfigComment4"),
						SR.GetString("SFxDocExt_NoMetadataSection5")
					}));
					writer.WriteEndElement();
				}
			}

			// Token: 0x02000F0A RID: 3850
			private class ServiceDescriptionMessage : ContentOnlyMessage
			{
				// Token: 0x060085C0 RID: 34240 RVA: 0x001EFEAF File Offset: 0x001EE0AF
				public ServiceDescriptionMessage(ServiceDescription description, ServiceMetadataExtension.WriteFilter responseWriter)
				{
					this.description = description;
					this.responseWriter = responseWriter;
				}

				// Token: 0x060085C1 RID: 34241 RVA: 0x001EFEC5 File Offset: 0x001EE0C5
				protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
				{
					this.responseWriter.Writer = writer;
					this.description.Write(this.responseWriter);
				}

				// Token: 0x04004D7E RID: 19838
				private ServiceDescription description;

				// Token: 0x04004D7F RID: 19839
				private ServiceMetadataExtension.WriteFilter responseWriter;
			}

			// Token: 0x02000F0B RID: 3851
			private class XmlSchemaMessage : ContentOnlyMessage
			{
				// Token: 0x060085C2 RID: 34242 RVA: 0x001EFEE4 File Offset: 0x001EE0E4
				public XmlSchemaMessage(XmlSchema schema, ServiceMetadataExtension.WriteFilter responseWriter)
				{
					this.schema = schema;
					this.responseWriter = responseWriter;
				}

				// Token: 0x060085C3 RID: 34243 RVA: 0x001EFEFA File Offset: 0x001EE0FA
				protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
				{
					this.responseWriter.Writer = writer;
					this.schema.Write(this.responseWriter);
				}

				// Token: 0x04004D80 RID: 19840
				private XmlSchema schema;

				// Token: 0x04004D81 RID: 19841
				private ServiceMetadataExtension.WriteFilter responseWriter;
			}
		}

		// Token: 0x02000BAB RID: 2987
		internal abstract class WriteFilter : XmlDictionaryWriter
		{
			// Token: 0x060073FF RID: 29695
			public abstract ServiceMetadataExtension.WriteFilter CloneWriteFilter();

			// Token: 0x06007400 RID: 29696 RVA: 0x001B1713 File Offset: 0x001AF913
			public override void Close()
			{
				this.Writer.Close();
			}

			// Token: 0x06007401 RID: 29697 RVA: 0x001B1720 File Offset: 0x001AF920
			public override void Flush()
			{
				this.Writer.Flush();
			}

			// Token: 0x06007402 RID: 29698 RVA: 0x001B172D File Offset: 0x001AF92D
			public override string LookupPrefix(string ns)
			{
				return this.Writer.LookupPrefix(ns);
			}

			// Token: 0x06007403 RID: 29699 RVA: 0x001B173B File Offset: 0x001AF93B
			public override void WriteBase64(byte[] buffer, int index, int count)
			{
				this.Writer.WriteBase64(buffer, index, count);
			}

			// Token: 0x06007404 RID: 29700 RVA: 0x001B174B File Offset: 0x001AF94B
			public override void WriteCData(string text)
			{
				this.Writer.WriteCData(text);
			}

			// Token: 0x06007405 RID: 29701 RVA: 0x001B1759 File Offset: 0x001AF959
			public override void WriteCharEntity(char ch)
			{
				this.Writer.WriteCharEntity(ch);
			}

			// Token: 0x06007406 RID: 29702 RVA: 0x001B1767 File Offset: 0x001AF967
			public override void WriteChars(char[] buffer, int index, int count)
			{
				this.Writer.WriteChars(buffer, index, count);
			}

			// Token: 0x06007407 RID: 29703 RVA: 0x001B1777 File Offset: 0x001AF977
			public override void WriteComment(string text)
			{
				this.Writer.WriteComment(text);
			}

			// Token: 0x06007408 RID: 29704 RVA: 0x001B1785 File Offset: 0x001AF985
			public override void WriteDocType(string name, string pubid, string sysid, string subset)
			{
				this.Writer.WriteDocType(name, pubid, sysid, subset);
			}

			// Token: 0x06007409 RID: 29705 RVA: 0x001B1797 File Offset: 0x001AF997
			public override void WriteEndAttribute()
			{
				this.Writer.WriteEndAttribute();
			}

			// Token: 0x0600740A RID: 29706 RVA: 0x001B17A4 File Offset: 0x001AF9A4
			public override void WriteEndDocument()
			{
				this.Writer.WriteEndDocument();
			}

			// Token: 0x0600740B RID: 29707 RVA: 0x001B17B1 File Offset: 0x001AF9B1
			public override void WriteEndElement()
			{
				this.Writer.WriteEndElement();
			}

			// Token: 0x0600740C RID: 29708 RVA: 0x001B17BE File Offset: 0x001AF9BE
			public override void WriteEntityRef(string name)
			{
				this.Writer.WriteEntityRef(name);
			}

			// Token: 0x0600740D RID: 29709 RVA: 0x001B17CC File Offset: 0x001AF9CC
			public override void WriteFullEndElement()
			{
				this.Writer.WriteFullEndElement();
			}

			// Token: 0x0600740E RID: 29710 RVA: 0x001B17D9 File Offset: 0x001AF9D9
			public override void WriteProcessingInstruction(string name, string text)
			{
				this.Writer.WriteProcessingInstruction(name, text);
			}

			// Token: 0x0600740F RID: 29711 RVA: 0x001B17E8 File Offset: 0x001AF9E8
			public override void WriteRaw(string data)
			{
				this.Writer.WriteRaw(data);
			}

			// Token: 0x06007410 RID: 29712 RVA: 0x001B17F6 File Offset: 0x001AF9F6
			public override void WriteRaw(char[] buffer, int index, int count)
			{
				this.Writer.WriteRaw(buffer, index, count);
			}

			// Token: 0x06007411 RID: 29713 RVA: 0x001B1806 File Offset: 0x001AFA06
			public override void WriteStartAttribute(string prefix, string localName, string ns)
			{
				this.Writer.WriteStartAttribute(prefix, localName, ns);
			}

			// Token: 0x06007412 RID: 29714 RVA: 0x001B1816 File Offset: 0x001AFA16
			public override void WriteStartDocument(bool standalone)
			{
				this.Writer.WriteStartDocument(standalone);
			}

			// Token: 0x06007413 RID: 29715 RVA: 0x001B1824 File Offset: 0x001AFA24
			public override void WriteStartDocument()
			{
				this.Writer.WriteStartDocument();
			}

			// Token: 0x06007414 RID: 29716 RVA: 0x001B1831 File Offset: 0x001AFA31
			public override void WriteStartElement(string prefix, string localName, string ns)
			{
				this.Writer.WriteStartElement(prefix, localName, ns);
			}

			// Token: 0x17001ADB RID: 6875
			// (get) Token: 0x06007415 RID: 29717 RVA: 0x001B1841 File Offset: 0x001AFA41
			public override WriteState WriteState
			{
				get
				{
					return this.Writer.WriteState;
				}
			}

			// Token: 0x06007416 RID: 29718 RVA: 0x001B184E File Offset: 0x001AFA4E
			public override void WriteString(string text)
			{
				this.Writer.WriteString(text);
			}

			// Token: 0x06007417 RID: 29719 RVA: 0x001B185C File Offset: 0x001AFA5C
			public override void WriteSurrogateCharEntity(char lowChar, char highChar)
			{
				this.Writer.WriteSurrogateCharEntity(lowChar, highChar);
			}

			// Token: 0x06007418 RID: 29720 RVA: 0x001B186B File Offset: 0x001AFA6B
			public override void WriteWhitespace(string ws)
			{
				this.Writer.WriteWhitespace(ws);
			}

			// Token: 0x040041BB RID: 16827
			internal XmlWriter Writer;
		}

		// Token: 0x02000BAC RID: 2988
		private class LocationUpdatingWriter : ServiceMetadataExtension.WriteFilter
		{
			// Token: 0x0600741A RID: 29722 RVA: 0x001B1881 File Offset: 0x001AFA81
			internal LocationUpdatingWriter(string oldValue, string newValue)
			{
				this.oldValue = oldValue;
				this.newValue = newValue;
			}

			// Token: 0x0600741B RID: 29723 RVA: 0x001B1897 File Offset: 0x001AFA97
			public override ServiceMetadataExtension.WriteFilter CloneWriteFilter()
			{
				return new ServiceMetadataExtension.LocationUpdatingWriter(this.oldValue, this.newValue);
			}

			// Token: 0x0600741C RID: 29724 RVA: 0x001B18AA File Offset: 0x001AFAAA
			public override void WriteString(string text)
			{
				if (this.newValue != null)
				{
					text = text.Replace(this.oldValue, this.newValue);
				}
				else if (text.StartsWith(this.oldValue, StringComparison.Ordinal))
				{
					text = string.Empty;
				}
				base.WriteString(text);
			}

			// Token: 0x040041BC RID: 16828
			private readonly string oldValue;

			// Token: 0x040041BD RID: 16829
			private readonly string newValue;
		}

		// Token: 0x02000BAD RID: 2989
		private class DynamicAddressUpdateWriter : ServiceMetadataExtension.WriteFilter
		{
			// Token: 0x0600741D RID: 29725 RVA: 0x001B18E7 File Offset: 0x001AFAE7
			internal DynamicAddressUpdateWriter(Uri listenUri, string requestHost, int requestPort, IDictionary<string, int> updatePortsByScheme, bool removeBaseAddress) : this(listenUri.Host, requestHost, removeBaseAddress, listenUri.Scheme, requestPort, updatePortsByScheme)
			{
				this.newBaseAddress = this.UpdateUri(listenUri, false).ToString();
			}

			// Token: 0x0600741E RID: 29726 RVA: 0x001B1914 File Offset: 0x001AFB14
			private DynamicAddressUpdateWriter(string oldHostName, string newHostName, string newBaseAddress, bool removeBaseAddress, string requestScheme, int requestPort, IDictionary<string, int> updatePortsByScheme) : this(oldHostName, newHostName, removeBaseAddress, requestScheme, requestPort, updatePortsByScheme)
			{
				this.newBaseAddress = newBaseAddress;
			}

			// Token: 0x0600741F RID: 29727 RVA: 0x001B192D File Offset: 0x001AFB2D
			private DynamicAddressUpdateWriter(string oldHostName, string newHostName, bool removeBaseAddress, string requestScheme, int requestPort, IDictionary<string, int> updatePortsByScheme)
			{
				this.oldHostName = oldHostName;
				this.newHostName = newHostName;
				this.removeBaseAddress = removeBaseAddress;
				this.requestScheme = requestScheme;
				this.requestPort = requestPort;
				this.updatePortsByScheme = updatePortsByScheme;
			}

			// Token: 0x06007420 RID: 29728 RVA: 0x001B1962 File Offset: 0x001AFB62
			public override ServiceMetadataExtension.WriteFilter CloneWriteFilter()
			{
				return new ServiceMetadataExtension.DynamicAddressUpdateWriter(this.oldHostName, this.newHostName, this.newBaseAddress, this.removeBaseAddress, this.requestScheme, this.requestPort, this.updatePortsByScheme);
			}

			// Token: 0x06007421 RID: 29729 RVA: 0x001B1994 File Offset: 0x001AFB94
			public override void WriteString(string text)
			{
				Uri uri;
				if (this.removeBaseAddress && text.StartsWith("{%BaseAddress%}", StringComparison.Ordinal))
				{
					text = string.Empty;
				}
				else if (!this.removeBaseAddress && text.Contains("{%BaseAddress%}"))
				{
					text = text.Replace("{%BaseAddress%}", this.newBaseAddress);
				}
				else if (Uri.TryCreate(text, UriKind.Absolute, out uri))
				{
					Uri uri2 = this.UpdateUri(uri, false);
					if (uri2 != null)
					{
						text = uri2.ToString();
					}
				}
				base.WriteString(text);
			}

			// Token: 0x06007422 RID: 29730 RVA: 0x001B1A18 File Offset: 0x001AFC18
			public void UpdateUri(ref Uri uri, bool updateBaseAddressOnly = false)
			{
				Uri uri2 = this.UpdateUri(uri, updateBaseAddressOnly);
				if (uri2 != null)
				{
					uri = uri2;
				}
			}

			// Token: 0x06007423 RID: 29731 RVA: 0x001B1A3C File Offset: 0x001AFC3C
			private Uri UpdateUri(Uri uri, bool updateBaseAddressOnly = false)
			{
				if (uri.Host != this.oldHostName)
				{
					return null;
				}
				UriBuilder uriBuilder = new UriBuilder(uri);
				uriBuilder.Host = this.newHostName;
				if (!updateBaseAddressOnly)
				{
					int port;
					if (uri.Scheme == this.requestScheme)
					{
						port = this.requestPort;
					}
					else if (!this.updatePortsByScheme.TryGetValue(uri.Scheme, out port))
					{
						return null;
					}
					uriBuilder.Port = port;
				}
				return uriBuilder.Uri;
			}

			// Token: 0x040041BE RID: 16830
			private readonly string oldHostName;

			// Token: 0x040041BF RID: 16831
			private readonly string newHostName;

			// Token: 0x040041C0 RID: 16832
			private readonly string newBaseAddress;

			// Token: 0x040041C1 RID: 16833
			private readonly bool removeBaseAddress;

			// Token: 0x040041C2 RID: 16834
			private readonly string requestScheme;

			// Token: 0x040041C3 RID: 16835
			private readonly int requestPort;

			// Token: 0x040041C4 RID: 16836
			private readonly IDictionary<string, int> updatePortsByScheme;
		}
	}
}

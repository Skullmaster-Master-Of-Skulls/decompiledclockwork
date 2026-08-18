using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Remoting;
using System.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200099E RID: 2462
	internal abstract class ServiceChannelFactory : ChannelFactoryBase
	{
		// Token: 0x06006085 RID: 24709 RVA: 0x00168A58 File Offset: 0x00166C58
		public ServiceChannelFactory(ClientRuntime clientRuntime, Binding binding)
		{
			if (clientRuntime == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("clientRuntime");
			}
			this.bindingName = binding.Name;
			this.channelsList = new List<IChannel>();
			this.clientRuntime = clientRuntime;
			this.timeouts = new ServiceChannelFactory.DefaultCommunicationTimeouts(binding);
			this.messageVersion = binding.MessageVersion;
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06006086 RID: 24710 RVA: 0x00168ABF File Offset: 0x00166CBF
		public ClientRuntime ClientRuntime
		{
			get
			{
				base.ThrowIfDisposed();
				return this.clientRuntime;
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06006087 RID: 24711 RVA: 0x00168ACD File Offset: 0x00166CCD
		internal RequestReplyCorrelator RequestReplyCorrelator
		{
			get
			{
				base.ThrowIfDisposed();
				return this.requestReplyCorrelator;
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06006088 RID: 24712 RVA: 0x00168ADB File Offset: 0x00166CDB
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.timeouts.CloseTimeout;
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x00168AE8 File Offset: 0x00166CE8
		protected override TimeSpan DefaultReceiveTimeout
		{
			get
			{
				return this.timeouts.ReceiveTimeout;
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x0600608A RID: 24714 RVA: 0x00168AF5 File Offset: 0x00166CF5
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.timeouts.OpenTimeout;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x0600608B RID: 24715 RVA: 0x00168B02 File Offset: 0x00166D02
		protected override TimeSpan DefaultSendTimeout
		{
			get
			{
				return this.timeouts.SendTimeout;
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x0600608C RID: 24716 RVA: 0x00168B0F File Offset: 0x00166D0F
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x00168B18 File Offset: 0x00166D18
		public static ServiceChannelFactory BuildChannelFactory(ChannelBuilder channelBuilder, ClientRuntime clientRuntime)
		{
			if (channelBuilder.CanBuildChannelFactory<IDuplexChannel>())
			{
				return new ServiceChannelFactory.ServiceChannelFactoryOverDuplex(channelBuilder.BuildChannelFactory<IDuplexChannel>(), clientRuntime, channelBuilder.Binding);
			}
			if (channelBuilder.CanBuildChannelFactory<IDuplexSessionChannel>())
			{
				return new ServiceChannelFactory.ServiceChannelFactoryOverDuplexSession(channelBuilder.BuildChannelFactory<IDuplexSessionChannel>(), clientRuntime, channelBuilder.Binding, false);
			}
			return new ServiceChannelFactory.ServiceChannelFactoryOverRequestSession(channelBuilder.BuildChannelFactory<IRequestSessionChannel>(), clientRuntime, channelBuilder.Binding, false);
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x00168B6F File Offset: 0x00166D6F
		public static ServiceChannelFactory BuildChannelFactory(ServiceEndpoint serviceEndpoint)
		{
			return ServiceChannelFactory.BuildChannelFactory(serviceEndpoint, false);
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x00168B78 File Offset: 0x00166D78
		public static ServiceChannelFactory BuildChannelFactory(ServiceEndpoint serviceEndpoint, bool useActiveAutoClose)
		{
			if (serviceEndpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpoint");
			}
			serviceEndpoint.EnsureInvariants();
			serviceEndpoint.ValidateForClient();
			ContractDescription contract = serviceEndpoint.Contract;
			ChannelRequirements channelRequirements;
			ChannelRequirements.ComputeContractRequirements(contract, out channelRequirements);
			BindingParameterCollection parameters;
			ClientRuntime clientRuntime = DispatcherBuilder.BuildProxyBehavior(serviceEndpoint, out parameters);
			Binding binding = serviceEndpoint.Binding;
			Type[] array = ChannelRequirements.ComputeRequiredChannels(ref channelRequirements);
			CustomBinding customBinding = new CustomBinding(binding);
			BindingContext bindingContext = new BindingContext(customBinding, parameters);
			InternalDuplexBindingElement internalDuplexBindingElement = null;
			InternalDuplexBindingElement.AddDuplexFactorySupport(bindingContext, ref internalDuplexBindingElement);
			customBinding = new CustomBinding(bindingContext.RemainingBindingElements);
			customBinding.CopyTimeouts(serviceEndpoint.Binding);
			Type[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				Type left = array2[i];
				if (left == typeof(IOutputChannel) && customBinding.CanBuildChannelFactory<IOutputChannel>(parameters))
				{
					return new ServiceChannelFactory.ServiceChannelFactoryOverOutput(customBinding.BuildChannelFactory<IOutputChannel>(parameters), clientRuntime, binding);
				}
				if (left == typeof(IRequestChannel) && customBinding.CanBuildChannelFactory<IRequestChannel>(parameters))
				{
					return new ServiceChannelFactory.ServiceChannelFactoryOverRequest(customBinding.BuildChannelFactory<IRequestChannel>(parameters), clientRuntime, binding);
				}
				if (left == typeof(IDuplexChannel) && customBinding.CanBuildChannelFactory<IDuplexChannel>(parameters))
				{
					if (channelRequirements.usesReply && binding.CreateBindingElements().Find<TransportBindingElement>().ManualAddressing)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CantCreateChannelWithManualAddressing")));
					}
					return new ServiceChannelFactory.ServiceChannelFactoryOverDuplex(customBinding.BuildChannelFactory<IDuplexChannel>(parameters), clientRuntime, binding);
				}
				else
				{
					if (left == typeof(IOutputSessionChannel) && customBinding.CanBuildChannelFactory<IOutputSessionChannel>(parameters))
					{
						return new ServiceChannelFactory.ServiceChannelFactoryOverOutputSession(customBinding.BuildChannelFactory<IOutputSessionChannel>(parameters), clientRuntime, binding, false);
					}
					if (left == typeof(IRequestSessionChannel) && customBinding.CanBuildChannelFactory<IRequestSessionChannel>(parameters))
					{
						return new ServiceChannelFactory.ServiceChannelFactoryOverRequestSession(customBinding.BuildChannelFactory<IRequestSessionChannel>(parameters), clientRuntime, binding, false);
					}
					if (left == typeof(IDuplexSessionChannel) && customBinding.CanBuildChannelFactory<IDuplexSessionChannel>(parameters))
					{
						if (channelRequirements.usesReply && binding.CreateBindingElements().Find<TransportBindingElement>().ManualAddressing)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CantCreateChannelWithManualAddressing")));
						}
						return new ServiceChannelFactory.ServiceChannelFactoryOverDuplexSession(customBinding.BuildChannelFactory<IDuplexSessionChannel>(parameters), clientRuntime, binding, useActiveAutoClose);
					}
					else
					{
						i++;
					}
				}
			}
			foreach (Type left2 in array)
			{
				if (left2 == typeof(IOutputChannel) && customBinding.CanBuildChannelFactory<IOutputSessionChannel>(parameters))
				{
					return new ServiceChannelFactory.ServiceChannelFactoryOverOutputSession(customBinding.BuildChannelFactory<IOutputSessionChannel>(parameters), clientRuntime, binding, true);
				}
				if (left2 == typeof(IRequestChannel) && customBinding.CanBuildChannelFactory<IRequestSessionChannel>(parameters))
				{
					return new ServiceChannelFactory.ServiceChannelFactoryOverRequestSession(customBinding.BuildChannelFactory<IRequestSessionChannel>(parameters), clientRuntime, binding, true);
				}
				if (left2 == typeof(IRequestSessionChannel) && customBinding.CanBuildChannelFactory<IRequestChannel>(parameters) && customBinding.GetProperty<IContextSessionProvider>(parameters) != null)
				{
					return new ServiceChannelFactory.ServiceChannelFactoryOverRequest(customBinding.BuildChannelFactory<IRequestChannel>(parameters), clientRuntime, binding);
				}
			}
			Dictionary<Type, byte> dictionary = new Dictionary<Type, byte>();
			if (customBinding.CanBuildChannelFactory<IOutputChannel>(parameters))
			{
				dictionary.Add(typeof(IOutputChannel), 0);
			}
			if (customBinding.CanBuildChannelFactory<IRequestChannel>(parameters))
			{
				dictionary.Add(typeof(IRequestChannel), 0);
			}
			if (customBinding.CanBuildChannelFactory<IDuplexChannel>(parameters))
			{
				dictionary.Add(typeof(IDuplexChannel), 0);
			}
			if (customBinding.CanBuildChannelFactory<IOutputSessionChannel>(parameters))
			{
				dictionary.Add(typeof(IOutputSessionChannel), 0);
			}
			if (customBinding.CanBuildChannelFactory<IRequestSessionChannel>(parameters))
			{
				dictionary.Add(typeof(IRequestSessionChannel), 0);
			}
			if (customBinding.CanBuildChannelFactory<IDuplexSessionChannel>(parameters))
			{
				dictionary.Add(typeof(IDuplexSessionChannel), 0);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ChannelRequirements.CantCreateChannelException(dictionary.Keys, array, binding.Name));
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x00168F34 File Offset: 0x00167134
		protected override void OnAbort()
		{
			IChannel channel = null;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				channel = ((this.channelsList.Count > 0) ? this.channelsList[this.channelsList.Count - 1] : null);
				goto IL_A5;
			}
			IL_49:
			channel.Abort();
			object thisLock2 = base.ThisLock;
			lock (thisLock2)
			{
				this.channelsList.Remove(channel);
				channel = ((this.channelsList.Count > 0) ? this.channelsList[this.channelsList.Count - 1] : null);
			}
			IL_A5:
			if (channel == null)
			{
				return;
			}
			goto IL_49;
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x00169008 File Offset: 0x00167208
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			for (;;)
			{
				object thisLock = base.ThisLock;
				IChannel channel;
				lock (thisLock)
				{
					if (this.channelsList.Count == 0)
					{
						break;
					}
					channel = this.channelsList[0];
				}
				channel.Close(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x0016907C File Offset: 0x0016727C
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			object thisLock = base.ThisLock;
			List<ICommunicationObject> list;
			lock (thisLock)
			{
				list = new List<ICommunicationObject>();
				for (int i = 0; i < this.channelsList.Count; i++)
				{
					list.Add(this.channelsList[i]);
				}
			}
			return new CloseCollectionAsyncResult(timeout, callback, state, list);
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x001690F0 File Offset: 0x001672F0
		protected override void OnEndClose(IAsyncResult result)
		{
			CloseCollectionAsyncResult.End(result);
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x001690F8 File Offset: 0x001672F8
		protected override void OnOpened()
		{
			base.OnOpened();
			this.clientRuntime.LockDownProperties();
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x0016910C File Offset: 0x0016730C
		public void ChannelCreated(IChannel channel)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262175, SR.GetString("TraceCodeChannelCreated", new object[]
				{
					TraceUtility.CreateSourceString(channel)
				}), this);
			}
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfDisposed();
				this.channelsList.Add(channel);
			}
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x00169188 File Offset: 0x00167388
		public void ChannelDisposed(IChannel channel)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262176, SR.GetString("TraceCodeChannelDisposed", new object[]
				{
					TraceUtility.CreateSourceString(channel)
				}), this);
			}
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				this.channelsList.Remove(channel);
			}
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x001691FC File Offset: 0x001673FC
		public virtual ServiceChannel CreateServiceChannel(EndpointAddress address, Uri via)
		{
			IChannelBinder channelBinder = this.CreateInnerChannelBinder(address, via);
			ServiceChannel serviceChannel = new ServiceChannel(this, channelBinder);
			if (channelBinder is DuplexChannelBinder)
			{
				DuplexChannelBinder duplexChannelBinder = channelBinder as DuplexChannelBinder;
				duplexChannelBinder.ChannelHandler = new ChannelHandler(this.messageVersion, channelBinder, serviceChannel);
				duplexChannelBinder.DefaultCloseTimeout = this.DefaultCloseTimeout;
				duplexChannelBinder.DefaultSendTimeout = this.DefaultSendTimeout;
				duplexChannelBinder.IdentityVerifier = this.clientRuntime.IdentityVerifier;
			}
			return serviceChannel;
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x00169266 File Offset: 0x00167466
		public TChannel CreateChannel<TChannel>(EndpointAddress address)
		{
			return this.CreateChannel<TChannel>(address, null);
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x00169270 File Offset: 0x00167470
		public TChannel CreateChannel<TChannel>(EndpointAddress address, Uri via)
		{
			if (!this.CanCreateChannel<TChannel>())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CouldnTCreateChannelForChannelType2", new object[]
				{
					this.bindingName,
					typeof(TChannel).Name
				})));
			}
			return (TChannel)((object)this.CreateChannel(typeof(TChannel), address, via));
		}

		// Token: 0x0600609A RID: 24730
		public abstract bool CanCreateChannel<TChannel>();

		// Token: 0x0600609B RID: 24731 RVA: 0x001692D7 File Offset: 0x001674D7
		public object CreateChannel(Type channelType, EndpointAddress address)
		{
			return this.CreateChannel(channelType, address, null);
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x001692E4 File Offset: 0x001674E4
		public object CreateChannel(Type channelType, EndpointAddress address, Uri via)
		{
			if (via == null)
			{
				via = this.ClientRuntime.Via;
				if (via == null)
				{
					via = address.Uri;
				}
			}
			ServiceChannel serviceChannel = this.CreateServiceChannel(address, via);
			serviceChannel.Proxy = ServiceChannelFactory.CreateProxy(channelType, channelType, MessageDirection.Input, serviceChannel);
			serviceChannel.ClientRuntime.GetRuntime().InitializeChannel((IClientChannel)serviceChannel.Proxy);
			OperationContext operationContext = OperationContext.Current;
			if (operationContext != null && operationContext.InstanceContext != null)
			{
				operationContext.InstanceContext.WmiChannels.Add((IChannel)serviceChannel.Proxy);
				serviceChannel.WmiInstanceContext = operationContext.InstanceContext;
			}
			return serviceChannel.Proxy;
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x0016938C File Offset: 0x0016758C
		[SecuritySafeCritical]
		internal static object CreateProxy(Type interfaceType, Type proxiedType, MessageDirection direction, ServiceChannel serviceChannel)
		{
			if (!proxiedType.IsInterface)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryTypeMustBeInterface")));
			}
			ServiceChannelProxy serviceChannelProxy = new ServiceChannelProxy(interfaceType, proxiedType, direction, serviceChannel);
			return serviceChannelProxy.GetTransparentProxy();
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x001693CC File Offset: 0x001675CC
		[SecuritySafeCritical]
		internal static ServiceChannel GetServiceChannel(object transparentProxy)
		{
			IChannelBaseProxy channelBaseProxy = transparentProxy as IChannelBaseProxy;
			if (channelBaseProxy != null)
			{
				return channelBaseProxy.GetServiceChannel();
			}
			ServiceChannelProxy serviceChannelProxy = RemotingServices.GetRealProxy(transparentProxy) as ServiceChannelProxy;
			if (serviceChannelProxy != null)
			{
				return serviceChannelProxy.GetServiceChannel();
			}
			return null;
		}

		// Token: 0x0600609F RID: 24735
		protected abstract IChannelBinder CreateInnerChannelBinder(EndpointAddress address, Uri via);

		// Token: 0x0400388E RID: 14478
		private string bindingName;

		// Token: 0x0400388F RID: 14479
		private List<IChannel> channelsList;

		// Token: 0x04003890 RID: 14480
		private ClientRuntime clientRuntime;

		// Token: 0x04003891 RID: 14481
		private RequestReplyCorrelator requestReplyCorrelator = new RequestReplyCorrelator();

		// Token: 0x04003892 RID: 14482
		private IDefaultCommunicationTimeouts timeouts;

		// Token: 0x04003893 RID: 14483
		private MessageVersion messageVersion;

		// Token: 0x02000E25 RID: 3621
		private abstract class TypedServiceChannelFactory<TChannel> : ServiceChannelFactory where TChannel : class, IChannel
		{
			// Token: 0x06008241 RID: 33345 RVA: 0x001E2598 File Offset: 0x001E0798
			protected TypedServiceChannelFactory(IChannelFactory<TChannel> innerChannelFactory, ClientRuntime clientRuntime, Binding binding) : base(clientRuntime, binding)
			{
				this.innerChannelFactory = innerChannelFactory;
			}

			// Token: 0x17001CB2 RID: 7346
			// (get) Token: 0x06008242 RID: 33346 RVA: 0x001E25A9 File Offset: 0x001E07A9
			protected IChannelFactory<TChannel> InnerChannelFactory
			{
				get
				{
					return this.innerChannelFactory;
				}
			}

			// Token: 0x06008243 RID: 33347 RVA: 0x001E25B1 File Offset: 0x001E07B1
			protected override void OnAbort()
			{
				base.OnAbort();
				this.innerChannelFactory.Abort();
			}

			// Token: 0x06008244 RID: 33348 RVA: 0x001E25C4 File Offset: 0x001E07C4
			protected override void OnOpen(TimeSpan timeout)
			{
				this.innerChannelFactory.Open(timeout);
			}

			// Token: 0x06008245 RID: 33349 RVA: 0x001E25D2 File Offset: 0x001E07D2
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannelFactory.BeginOpen(timeout, callback, state);
			}

			// Token: 0x06008246 RID: 33350 RVA: 0x001E25E2 File Offset: 0x001E07E2
			protected override void OnEndOpen(IAsyncResult result)
			{
				this.innerChannelFactory.EndOpen(result);
			}

			// Token: 0x06008247 RID: 33351 RVA: 0x001E25F0 File Offset: 0x001E07F0
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.OnClose(timeoutHelper.RemainingTime());
				this.innerChannelFactory.Close(timeoutHelper.RemainingTime());
			}

			// Token: 0x06008248 RID: 33352 RVA: 0x001E2624 File Offset: 0x001E0824
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ChainedBeginHandler(this.innerChannelFactory.BeginClose), new ChainedEndHandler(this.innerChannelFactory.EndClose));
			}

			// Token: 0x06008249 RID: 33353 RVA: 0x001E2675 File Offset: 0x001E0875
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x0600824A RID: 33354 RVA: 0x001E2680 File Offset: 0x001E0880
			public override T GetProperty<T>()
			{
				if (typeof(T) == typeof(ServiceChannelFactory.TypedServiceChannelFactory<TChannel>))
				{
					return (T)((object)this);
				}
				T property = base.GetProperty<T>();
				if (property != null)
				{
					return property;
				}
				return this.innerChannelFactory.GetProperty<T>();
			}

			// Token: 0x040049FE RID: 18942
			private IChannelFactory<TChannel> innerChannelFactory;
		}

		// Token: 0x02000E26 RID: 3622
		private class ServiceChannelFactoryOverOutput : ServiceChannelFactory.TypedServiceChannelFactory<IOutputChannel>
		{
			// Token: 0x0600824B RID: 33355 RVA: 0x001E26CB File Offset: 0x001E08CB
			public ServiceChannelFactoryOverOutput(IChannelFactory<IOutputChannel> innerChannelFactory, ClientRuntime clientRuntime, Binding binding) : base(innerChannelFactory, clientRuntime, binding)
			{
			}

			// Token: 0x0600824C RID: 33356 RVA: 0x001E26D6 File Offset: 0x001E08D6
			protected override IChannelBinder CreateInnerChannelBinder(EndpointAddress to, Uri via)
			{
				return new OutputChannelBinder(base.InnerChannelFactory.CreateChannel(to, via));
			}

			// Token: 0x0600824D RID: 33357 RVA: 0x001E26EA File Offset: 0x001E08EA
			public override bool CanCreateChannel<TChannel>()
			{
				return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IRequestChannel);
			}
		}

		// Token: 0x02000E27 RID: 3623
		private class ServiceChannelFactoryOverDuplex : ServiceChannelFactory.TypedServiceChannelFactory<IDuplexChannel>
		{
			// Token: 0x0600824E RID: 33358 RVA: 0x001E2722 File Offset: 0x001E0922
			public ServiceChannelFactoryOverDuplex(IChannelFactory<IDuplexChannel> innerChannelFactory, ClientRuntime clientRuntime, Binding binding) : base(innerChannelFactory, clientRuntime, binding)
			{
			}

			// Token: 0x0600824F RID: 33359 RVA: 0x001E272D File Offset: 0x001E092D
			protected override IChannelBinder CreateInnerChannelBinder(EndpointAddress to, Uri via)
			{
				return new DuplexChannelBinder(base.InnerChannelFactory.CreateChannel(to, via), base.RequestReplyCorrelator);
			}

			// Token: 0x06008250 RID: 33360 RVA: 0x001E2748 File Offset: 0x001E0948
			public override bool CanCreateChannel<TChannel>()
			{
				return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IDuplexChannel);
			}
		}

		// Token: 0x02000E28 RID: 3624
		private class ServiceChannelFactoryOverRequest : ServiceChannelFactory.TypedServiceChannelFactory<IRequestChannel>
		{
			// Token: 0x06008251 RID: 33361 RVA: 0x001E27A6 File Offset: 0x001E09A6
			public ServiceChannelFactoryOverRequest(IChannelFactory<IRequestChannel> innerChannelFactory, ClientRuntime clientRuntime, Binding binding) : base(innerChannelFactory, clientRuntime, binding)
			{
			}

			// Token: 0x06008252 RID: 33362 RVA: 0x001E27B1 File Offset: 0x001E09B1
			protected override IChannelBinder CreateInnerChannelBinder(EndpointAddress to, Uri via)
			{
				return new RequestChannelBinder(base.InnerChannelFactory.CreateChannel(to, via));
			}

			// Token: 0x06008253 RID: 33363 RVA: 0x001E27C5 File Offset: 0x001E09C5
			public override bool CanCreateChannel<TChannel>()
			{
				return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IRequestChannel);
			}
		}

		// Token: 0x02000E29 RID: 3625
		private class ServiceChannelFactoryOverOutputSession : ServiceChannelFactory.TypedServiceChannelFactory<IOutputSessionChannel>
		{
			// Token: 0x06008254 RID: 33364 RVA: 0x001E27FD File Offset: 0x001E09FD
			public ServiceChannelFactoryOverOutputSession(IChannelFactory<IOutputSessionChannel> innerChannelFactory, ClientRuntime clientRuntime, Binding binding, bool datagramAdapter) : base(innerChannelFactory, clientRuntime, binding)
			{
				this.datagramAdapter = datagramAdapter;
			}

			// Token: 0x06008255 RID: 33365 RVA: 0x001E2810 File Offset: 0x001E0A10
			protected override IChannelBinder CreateInnerChannelBinder(EndpointAddress to, Uri via)
			{
				IOutputChannel channel;
				if (this.datagramAdapter)
				{
					channel = DatagramAdapter.GetOutputChannel(() => this.InnerChannelFactory.CreateChannel(to, via), this.timeouts);
				}
				else
				{
					channel = base.InnerChannelFactory.CreateChannel(to, via);
				}
				return new OutputChannelBinder(channel);
			}

			// Token: 0x06008256 RID: 33366 RVA: 0x001E2878 File Offset: 0x001E0A78
			public override bool CanCreateChannel<TChannel>()
			{
				return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IOutputSessionChannel) || typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IRequestSessionChannel);
			}

			// Token: 0x040049FF RID: 18943
			private bool datagramAdapter;
		}

		// Token: 0x02000E2A RID: 3626
		private class ServiceChannelFactoryOverDuplexSession : ServiceChannelFactory.TypedServiceChannelFactory<IDuplexSessionChannel>
		{
			// Token: 0x06008257 RID: 33367 RVA: 0x001E28F1 File Offset: 0x001E0AF1
			public ServiceChannelFactoryOverDuplexSession(IChannelFactory<IDuplexSessionChannel> innerChannelFactory, ClientRuntime clientRuntime, Binding binding, bool useActiveAutoClose) : base(innerChannelFactory, clientRuntime, binding)
			{
				this.useActiveAutoClose = useActiveAutoClose;
			}

			// Token: 0x06008258 RID: 33368 RVA: 0x001E2904 File Offset: 0x001E0B04
			protected override IChannelBinder CreateInnerChannelBinder(EndpointAddress to, Uri via)
			{
				return new DuplexChannelBinder(base.InnerChannelFactory.CreateChannel(to, via), base.RequestReplyCorrelator, this.useActiveAutoClose);
			}

			// Token: 0x06008259 RID: 33369 RVA: 0x001E2924 File Offset: 0x001E0B24
			public override bool CanCreateChannel<TChannel>()
			{
				return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IDuplexChannel) || typeof(TChannel) == typeof(IOutputSessionChannel) || typeof(TChannel) == typeof(IRequestSessionChannel) || typeof(TChannel) == typeof(IDuplexSessionChannel);
			}

			// Token: 0x04004A00 RID: 18944
			private bool useActiveAutoClose;
		}

		// Token: 0x02000E2B RID: 3627
		private class ServiceChannelFactoryOverRequestSession : ServiceChannelFactory.TypedServiceChannelFactory<IRequestSessionChannel>
		{
			// Token: 0x0600825A RID: 33370 RVA: 0x001E29D6 File Offset: 0x001E0BD6
			public ServiceChannelFactoryOverRequestSession(IChannelFactory<IRequestSessionChannel> innerChannelFactory, ClientRuntime clientRuntime, Binding binding, bool datagramAdapter) : base(innerChannelFactory, clientRuntime, binding)
			{
				this.datagramAdapter = datagramAdapter;
			}

			// Token: 0x0600825B RID: 33371 RVA: 0x001E29EC File Offset: 0x001E0BEC
			protected override IChannelBinder CreateInnerChannelBinder(EndpointAddress to, Uri via)
			{
				IRequestChannel channel;
				if (this.datagramAdapter)
				{
					channel = DatagramAdapter.GetRequestChannel(() => this.InnerChannelFactory.CreateChannel(to, via), this.timeouts);
				}
				else
				{
					channel = base.InnerChannelFactory.CreateChannel(to, via);
				}
				return new RequestChannelBinder(channel);
			}

			// Token: 0x0600825C RID: 33372 RVA: 0x001E2A54 File Offset: 0x001E0C54
			public override bool CanCreateChannel<TChannel>()
			{
				return typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IOutputSessionChannel) || typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IRequestSessionChannel);
			}

			// Token: 0x04004A01 RID: 18945
			private bool datagramAdapter;
		}

		// Token: 0x02000E2C RID: 3628
		private class DefaultCommunicationTimeouts : IDefaultCommunicationTimeouts
		{
			// Token: 0x0600825D RID: 33373 RVA: 0x001E2ACD File Offset: 0x001E0CCD
			public DefaultCommunicationTimeouts(IDefaultCommunicationTimeouts timeouts)
			{
				this.closeTimeout = timeouts.CloseTimeout;
				this.openTimeout = timeouts.OpenTimeout;
				this.receiveTimeout = timeouts.ReceiveTimeout;
				this.sendTimeout = timeouts.SendTimeout;
			}

			// Token: 0x17001CB3 RID: 7347
			// (get) Token: 0x0600825E RID: 33374 RVA: 0x001E2B05 File Offset: 0x001E0D05
			public TimeSpan CloseTimeout
			{
				get
				{
					return this.closeTimeout;
				}
			}

			// Token: 0x17001CB4 RID: 7348
			// (get) Token: 0x0600825F RID: 33375 RVA: 0x001E2B0D File Offset: 0x001E0D0D
			public TimeSpan OpenTimeout
			{
				get
				{
					return this.openTimeout;
				}
			}

			// Token: 0x17001CB5 RID: 7349
			// (get) Token: 0x06008260 RID: 33376 RVA: 0x001E2B15 File Offset: 0x001E0D15
			public TimeSpan ReceiveTimeout
			{
				get
				{
					return this.receiveTimeout;
				}
			}

			// Token: 0x17001CB6 RID: 7350
			// (get) Token: 0x06008261 RID: 33377 RVA: 0x001E2B1D File Offset: 0x001E0D1D
			public TimeSpan SendTimeout
			{
				get
				{
					return this.sendTimeout;
				}
			}

			// Token: 0x04004A02 RID: 18946
			private TimeSpan closeTimeout;

			// Token: 0x04004A03 RID: 18947
			private TimeSpan openTimeout;

			// Token: 0x04004A04 RID: 18948
			private TimeSpan receiveTimeout;

			// Token: 0x04004A05 RID: 18949
			private TimeSpan sendTimeout;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000784 RID: 1924
	internal abstract class TransportChannelListener : ChannelListenerBase, ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x06004954 RID: 18772 RVA: 0x0010DF72 File Offset: 0x0010C172
		protected TransportChannelListener(TransportBindingElement bindingElement, BindingContext context) : this(bindingElement, context, TransportDefaults.GetDefaultMessageEncoderFactory())
		{
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x0010DF81 File Offset: 0x0010C181
		protected TransportChannelListener(TransportBindingElement bindingElement, BindingContext context, MessageEncoderFactory defaultMessageEncoderFactory) : this(bindingElement, context, defaultMessageEncoderFactory, HostNameComparisonMode.Exact)
		{
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x0010DF8D File Offset: 0x0010C18D
		protected TransportChannelListener(TransportBindingElement bindingElement, BindingContext context, HostNameComparisonMode hostNameComparisonMode) : this(bindingElement, context, TransportDefaults.GetDefaultMessageEncoderFactory(), hostNameComparisonMode)
		{
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0010DFA0 File Offset: 0x0010C1A0
		protected TransportChannelListener(TransportBindingElement bindingElement, BindingContext context, MessageEncoderFactory defaultMessageEncoderFactory, HostNameComparisonMode hostNameComparisonMode) : base(context.Binding)
		{
			HostNameComparisonModeHelper.Validate(hostNameComparisonMode);
			this.hostNameComparisonMode = hostNameComparisonMode;
			this.manualAddressing = bindingElement.ManualAddressing;
			this.maxBufferPoolSize = bindingElement.MaxBufferPoolSize;
			this.maxReceivedMessageSize = bindingElement.MaxReceivedMessageSize;
			Collection<MessageEncodingBindingElement> collection = context.BindingParameters.FindAll<MessageEncodingBindingElement>();
			if (collection.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleMebesInParameters")));
			}
			if (collection.Count == 1)
			{
				this.messageEncoderFactory = collection[0].CreateMessageEncoderFactory();
				context.BindingParameters.Remove<MessageEncodingBindingElement>();
			}
			else
			{
				this.messageEncoderFactory = defaultMessageEncoderFactory;
			}
			if (this.messageEncoderFactory != null)
			{
				this.messageVersion = this.messageEncoderFactory.MessageVersion;
			}
			else
			{
				this.messageVersion = MessageVersion.None;
			}
			ServiceSecurityAuditBehavior serviceSecurityAuditBehavior = context.BindingParameters.Find<ServiceSecurityAuditBehavior>();
			if (serviceSecurityAuditBehavior != null)
			{
				this.auditBehavior = serviceSecurityAuditBehavior.Clone();
			}
			else
			{
				this.auditBehavior = new ServiceSecurityAuditBehavior();
			}
			if (context.ListenUriMode == ListenUriMode.Unique && context.ListenUriBaseAddress == null)
			{
				context.ListenUriBaseAddress = new UriBuilder(this.Scheme, DnsCache.MachineName)
				{
					Path = this.GeneratedAddressPrefix
				}.Uri;
			}
			UriSchemeKeyedCollection.ValidateBaseAddress(context.ListenUriBaseAddress, "baseAddress");
			if (context.ListenUriBaseAddress.Scheme != this.Scheme && string.Compare(context.ListenUriBaseAddress.Scheme, this.Scheme, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("context.ListenUriBaseAddress", SR.GetString("InvalidUriScheme", new object[]
				{
					context.ListenUriBaseAddress.Scheme,
					this.Scheme
				}));
			}
			if (context.ListenUriMode == ListenUriMode.Explicit)
			{
				this.SetUri(context.ListenUriBaseAddress, context.ListenUriRelativeAddress);
			}
			else
			{
				string text = context.ListenUriRelativeAddress;
				if (text.Length > 0 && !text.EndsWith("/", StringComparison.Ordinal))
				{
					text += "/";
				}
				this.SetUri(context.ListenUriBaseAddress, text + Guid.NewGuid().ToString());
			}
			this.transportManagerContainer = new TransportManagerContainer(this);
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x0010E1C9 File Offset: 0x0010C3C9
		// (set) Token: 0x06004959 RID: 18777 RVA: 0x0010E1D1 File Offset: 0x0010C3D1
		internal ServiceModelActivity Activity
		{
			get
			{
				return this.activity;
			}
			set
			{
				this.activity = value;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x0010E1DA File Offset: 0x0010C3DA
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x0600495B RID: 18779 RVA: 0x0010E1E4 File Offset: 0x0010C3E4
		private string GeneratedAddressPrefix
		{
			get
			{
				TransportChannelListener.EnsureAddressPrefixesInitialized();
				switch (this.hostNameComparisonMode)
				{
				case HostNameComparisonMode.StrongWildcard:
					return TransportChannelListener.strongWildcardGeneratedAddressPrefix;
				case HostNameComparisonMode.Exact:
					return TransportChannelListener.exactGeneratedAddressPrefix;
				case HostNameComparisonMode.WeakWildcard:
					return TransportChannelListener.weakWildcardGeneratedAddressPrefix;
				default:
					return null;
				}
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x0010E22A File Offset: 0x0010C42A
		internal string HostedVirtualPath
		{
			get
			{
				return this.hostedVirtualPath;
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x0010E232 File Offset: 0x0010C432
		// (set) Token: 0x0600495E RID: 18782 RVA: 0x0010E23A File Offset: 0x0010C43A
		internal bool InheritBaseAddressSettings
		{
			get
			{
				return this.inheritBaseAddressSettings;
			}
			set
			{
				this.inheritBaseAddressSettings = value;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x0010E243 File Offset: 0x0010C443
		internal ServiceSecurityAuditBehavior AuditBehavior
		{
			get
			{
				return this.auditBehavior;
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x0010E24B File Offset: 0x0010C44B
		public BufferManager BufferManager
		{
			get
			{
				return this.bufferManager;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06004961 RID: 18785 RVA: 0x0010E253 File Offset: 0x0010C453
		internal HostNameComparisonMode HostNameComparisonModeInternal
		{
			get
			{
				return this.hostNameComparisonMode;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x0010E25B File Offset: 0x0010C45B
		public bool ManualAddressing
		{
			get
			{
				return this.manualAddressing;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x0010E263 File Offset: 0x0010C463
		public long MaxBufferPoolSize
		{
			get
			{
				return this.maxBufferPoolSize;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x0010E26B File Offset: 0x0010C46B
		public virtual long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06004965 RID: 18789 RVA: 0x0010E273 File Offset: 0x0010C473
		public MessageEncoderFactory MessageEncoderFactory
		{
			get
			{
				return this.messageEncoderFactory;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x0010E27B File Offset: 0x0010C47B
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06004967 RID: 18791
		internal abstract UriPrefixTable<ITransportManagerRegistration> TransportManagerTable { get; }

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06004968 RID: 18792
		public abstract string Scheme { get; }

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x0010E283 File Offset: 0x0010C483
		public override Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x0010E28C File Offset: 0x0010C48C
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(MessageVersion))
			{
				return (T)((object)this.MessageVersion);
			}
			if (typeof(T) == typeof(FaultConverter))
			{
				if (this.MessageEncoderFactory == null)
				{
					return default(T);
				}
				return this.MessageEncoderFactory.Encoder.GetProperty<T>();
			}
			else
			{
				if (typeof(T) == typeof(ITransportFactorySettings))
				{
					return (T)((object)this);
				}
				return base.GetProperty<T>();
			}
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x0010E328 File Offset: 0x0010C528
		internal bool IsScopeIdCompatible(HostNameComparisonMode hostNameComparisonMode, Uri uri)
		{
			if (this.hostNameComparisonMode != hostNameComparisonMode)
			{
				return false;
			}
			if (hostNameComparisonMode == HostNameComparisonMode.Exact && uri.HostNameType == UriHostNameType.IPv6)
			{
				if (this.Uri.HostNameType != UriHostNameType.IPv6)
				{
					return false;
				}
				IPAddress ipaddress = IPAddress.Parse(this.Uri.DnsSafeHost);
				IPAddress ipaddress2 = IPAddress.Parse(uri.DnsSafeHost);
				if (ipaddress.ScopeId != ipaddress2.ScopeId)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x0010E38B File Offset: 0x0010C58B
		internal virtual void ApplyHostedContext(string virtualPath, bool isMetadataListener)
		{
			this.hostedVirtualPath = virtualPath;
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x0010E394 File Offset: 0x0010C594
		private static Uri AddSegment(Uri baseUri, Uri fullUri)
		{
			Uri result = null;
			if (baseUri.AbsolutePath.Length < fullUri.AbsolutePath.Length)
			{
				UriBuilder uriBuilder = new UriBuilder(baseUri);
				TcpChannelListener.FixIpv6Hostname(uriBuilder, baseUri);
				if (!uriBuilder.Path.EndsWith("/", StringComparison.Ordinal))
				{
					uriBuilder.Path += "/";
					baseUri = uriBuilder.Uri;
				}
				Uri uri = baseUri.MakeRelativeUri(fullUri);
				string originalString = uri.OriginalString;
				int num = originalString.IndexOf('/');
				string str = (num == -1) ? originalString : originalString.Substring(0, num);
				uriBuilder.Path += str;
				result = uriBuilder.Uri;
			}
			return result;
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x0010E444 File Offset: 0x0010C644
		internal virtual ITransportManagerRegistration CreateTransportManagerRegistration()
		{
			return this.CreateTransportManagerRegistration(this.BaseUri);
		}

		// Token: 0x0600496F RID: 18799
		internal abstract ITransportManagerRegistration CreateTransportManagerRegistration(Uri listenUri);

		// Token: 0x06004970 RID: 18800 RVA: 0x0010E454 File Offset: 0x0010C654
		private static void EnsureAddressPrefixesInitialized()
		{
			if (!TransportChannelListener.addressPrefixesInitialized)
			{
				object obj = TransportChannelListener.staticLock;
				lock (obj)
				{
					if (!TransportChannelListener.addressPrefixesInitialized)
					{
						TransportChannelListener.exactGeneratedAddressPrefix = "Temporary_Listen_Addresses/" + Guid.NewGuid().ToString();
						TransportChannelListener.strongWildcardGeneratedAddressPrefix = "Temporary_Listen_Addresses/" + Guid.NewGuid().ToString();
						TransportChannelListener.weakWildcardGeneratedAddressPrefix = "Temporary_Listen_Addresses/" + Guid.NewGuid().ToString();
						TransportChannelListener.addressPrefixesInitialized = true;
					}
				}
			}
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x0010E518 File Offset: 0x0010C718
		internal virtual int GetMaxBufferSize()
		{
			if (this.MaxReceivedMessageSize > 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)this.MaxReceivedMessageSize;
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x0010E538 File Offset: 0x0010C738
		protected override void OnOpening()
		{
			base.OnOpening();
			if (this.HostedVirtualPath != null)
			{
				BaseUriWithWildcard baseUriWithWildcard = AspNetEnvironment.Current.GetBaseUri(this.Scheme, this.Uri);
				if (baseUriWithWildcard != null)
				{
					this.hostNameComparisonMode = baseUriWithWildcard.HostNameComparisonMode;
				}
			}
			this.bufferManager = BufferManager.CreateBufferManager(this.MaxBufferPoolSize, this.GetMaxBufferSize());
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x0010E590 File Offset: 0x0010C790
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.transportManagerContainer.BeginOpen(new SelectTransportManagersCallback(this.SelectTransportManagers), callback, state);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0010E5AC File Offset: 0x0010C7AC
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.transportManagerContainer.EndOpen(result);
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0010E5BA File Offset: 0x0010C7BA
		protected override void OnOpen(TimeSpan timeout)
		{
			this.transportManagerContainer.Open(new SelectTransportManagersCallback(this.SelectTransportManagers));
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x0010E5D4 File Offset: 0x0010C7D4
		protected override void OnOpened()
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262188, SR.GetString("TraceCodeOpenedListener"), new UriTraceRecord(this.Uri), this, null);
			}
			base.OnOpened();
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x0010E606 File Offset: 0x0010C806
		internal TransportManagerContainer GetTransportManagers()
		{
			return TransportManagerContainer.TransferTransportManagers(this.transportManagerContainer);
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x0010E613 File Offset: 0x0010C813
		protected override void OnAbort()
		{
			this.transportManagerContainer.Abort();
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x0010E620 File Offset: 0x0010C820
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.transportManagerContainer.BeginClose(timeout, callback, state);
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x0010E630 File Offset: 0x0010C830
		protected override void OnEndClose(IAsyncResult result)
		{
			this.transportManagerContainer.EndClose(result);
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x0010E63E File Offset: 0x0010C83E
		protected override void OnClose(TimeSpan timeout)
		{
			this.transportManagerContainer.Close(timeout);
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x0010E64C File Offset: 0x0010C84C
		protected override void OnClosed()
		{
			base.OnClosed();
			if (this.bufferManager != null)
			{
				this.bufferManager.Clear();
			}
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x0010E667 File Offset: 0x0010C867
		private bool TryGetTransportManagerRegistration(out ITransportManagerRegistration registration)
		{
			if (!this.InheritBaseAddressSettings)
			{
				return this.TryGetTransportManagerRegistration(this.hostNameComparisonMode, out registration);
			}
			if (this.TryGetTransportManagerRegistration(HostNameComparisonMode.StrongWildcard, out registration))
			{
				return true;
			}
			if (this.TryGetTransportManagerRegistration(HostNameComparisonMode.Exact, out registration))
			{
				return true;
			}
			if (this.TryGetTransportManagerRegistration(HostNameComparisonMode.WeakWildcard, out registration))
			{
				return true;
			}
			registration = null;
			return false;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x0010E6A7 File Offset: 0x0010C8A7
		protected virtual bool TryGetTransportManagerRegistration(HostNameComparisonMode hostNameComparisonMode, out ITransportManagerRegistration registration)
		{
			return this.TransportManagerTable.TryLookupUri(this.Uri, hostNameComparisonMode, out registration);
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x0010E6BC File Offset: 0x0010C8BC
		internal virtual IList<TransportManager> SelectTransportManagers()
		{
			IList<TransportManager> list = null;
			ITransportManagerRegistration transportManagerRegistration;
			if (!this.TryGetTransportManagerRegistration(out transportManagerRegistration))
			{
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 262184, SR.GetString("TraceCodeNoExistingTransportManager"), new UriTraceRecord(this.Uri), this, null);
				}
				if (this.HostedVirtualPath == null)
				{
					transportManagerRegistration = this.CreateTransportManagerRegistration();
					this.TransportManagerTable.RegisterUri(transportManagerRegistration.ListenUri, this.hostNameComparisonMode, transportManagerRegistration);
				}
			}
			if (transportManagerRegistration != null)
			{
				list = transportManagerRegistration.Select(this);
				if (list == null)
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 262185, SR.GetString("TraceCodeIncompatibleExistingTransportManager"), new UriTraceRecord(this.Uri), this, null);
					}
					if (this.HostedVirtualPath == null)
					{
						Uri uri = TransportChannelListener.AddSegment(transportManagerRegistration.ListenUri, this.Uri);
						if (uri != null)
						{
							transportManagerRegistration = this.CreateTransportManagerRegistration(uri);
							this.TransportManagerTable.RegisterUri(uri, this.hostNameComparisonMode, transportManagerRegistration);
							list = transportManagerRegistration.Select(this);
						}
					}
				}
			}
			if (list == null)
			{
				this.ThrowTransportManagersNotFound();
			}
			return list;
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x0010E7B0 File Offset: 0x0010C9B0
		private void ThrowTransportManagersNotFound()
		{
			if (this.HostedVirtualPath != null)
			{
				if (string.Compare(this.Uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.Uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Hosting_NoHttpTransportManagerForUri", new object[]
					{
						this.Uri
					})));
				}
				if (string.Compare(this.Uri.Scheme, Uri.UriSchemeNetTcp, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.Uri.Scheme, Uri.UriSchemeNetPipe, StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Hosting_NoTcpPipeTransportManagerForUri", new object[]
					{
						this.Uri
					})));
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoCompatibleTransportManagerForUri", new object[]
			{
				this.Uri
			})));
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x0010E8A4 File Offset: 0x0010CAA4
		protected void SetUri(Uri baseAddress, string relativeAddress)
		{
			Uri uri = baseAddress;
			if (relativeAddress != string.Empty)
			{
				if (!baseAddress.AbsolutePath.EndsWith("/", StringComparison.Ordinal))
				{
					UriBuilder uriBuilder = new UriBuilder(baseAddress);
					TcpChannelListener.FixIpv6Hostname(uriBuilder, baseAddress);
					uriBuilder.Path += "/";
					baseAddress = uriBuilder.Uri;
				}
				uri = new Uri(baseAddress, relativeAddress);
				if (!baseAddress.IsBaseOf(uri))
				{
					baseAddress = uri;
				}
			}
			this.baseUri = baseAddress;
			this.ValidateUri(uri);
			this.uri = uri;
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x0010E928 File Offset: 0x0010CB28
		protected virtual void ValidateUri(Uri uri)
		{
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x0010E92A File Offset: 0x0010CB2A
		long ITransportFactorySettings.MaxReceivedMessageSize
		{
			get
			{
				return this.MaxReceivedMessageSize;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x0010E932 File Offset: 0x0010CB32
		BufferManager ITransportFactorySettings.BufferManager
		{
			get
			{
				return this.BufferManager;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x0010E93A File Offset: 0x0010CB3A
		bool ITransportFactorySettings.ManualAddressing
		{
			get
			{
				return this.ManualAddressing;
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x0010E942 File Offset: 0x0010CB42
		MessageEncoderFactory ITransportFactorySettings.MessageEncoderFactory
		{
			get
			{
				return this.MessageEncoderFactory;
			}
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x0010E94A File Offset: 0x0010CB4A
		internal void SetMessageReceivedCallback(Action messageReceivedCallback)
		{
			this.messageReceivedCallback = messageReceivedCallback;
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x0010E954 File Offset: 0x0010CB54
		internal void RaiseMessageReceived()
		{
			Action action = this.messageReceivedCallback;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x04002E27 RID: 11815
		private static volatile bool addressPrefixesInitialized = false;

		// Token: 0x04002E28 RID: 11816
		private static volatile string exactGeneratedAddressPrefix;

		// Token: 0x04002E29 RID: 11817
		private static volatile string strongWildcardGeneratedAddressPrefix;

		// Token: 0x04002E2A RID: 11818
		private static volatile string weakWildcardGeneratedAddressPrefix;

		// Token: 0x04002E2B RID: 11819
		private static object staticLock = new object();

		// Token: 0x04002E2C RID: 11820
		private Uri baseUri;

		// Token: 0x04002E2D RID: 11821
		private BufferManager bufferManager;

		// Token: 0x04002E2E RID: 11822
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x04002E2F RID: 11823
		private bool inheritBaseAddressSettings;

		// Token: 0x04002E30 RID: 11824
		private bool manualAddressing;

		// Token: 0x04002E31 RID: 11825
		private long maxBufferPoolSize;

		// Token: 0x04002E32 RID: 11826
		private long maxReceivedMessageSize;

		// Token: 0x04002E33 RID: 11827
		private MessageEncoderFactory messageEncoderFactory;

		// Token: 0x04002E34 RID: 11828
		private MessageVersion messageVersion;

		// Token: 0x04002E35 RID: 11829
		private Uri uri;

		// Token: 0x04002E36 RID: 11830
		private string hostedVirtualPath;

		// Token: 0x04002E37 RID: 11831
		private Action messageReceivedCallback;

		// Token: 0x04002E38 RID: 11832
		private ServiceSecurityAuditBehavior auditBehavior;

		// Token: 0x04002E39 RID: 11833
		private ServiceModelActivity activity;

		// Token: 0x04002E3A RID: 11834
		private TransportManagerContainer transportManagerContainer;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E3 RID: 995
	public class MetadataExchangeClient
	{
		// Token: 0x0600257B RID: 9595 RVA: 0x0008697C File Offset: 0x00084B7C
		public MetadataExchangeClient()
		{
			this.factory = new ChannelFactory<IMetadataExchange>("*");
			this.maxMessageSize = MetadataExchangeClient.GetMaxMessageSize(this.factory.Endpoint.Binding);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000869E8 File Offset: 0x00084BE8
		public MetadataExchangeClient(Uri address, MetadataExchangeClientMode mode)
		{
			this.Validate(address, mode);
			if (mode == MetadataExchangeClientMode.HttpGet)
			{
				this.ctorUri = address;
			}
			else
			{
				this.ctorEndpointAddress = new EndpointAddress(address, new AddressHeader[0]);
			}
			this.CreateChannelFactory(address.Scheme);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00086A5C File Offset: 0x00084C5C
		public MetadataExchangeClient(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			this.ctorEndpointAddress = address;
			this.CreateChannelFactory(address.Uri.Scheme);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00086AD0 File Offset: 0x00084CD0
		public MetadataExchangeClient(string endpointConfigurationName)
		{
			if (endpointConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
			}
			this.factory = new ChannelFactory<IMetadataExchange>(endpointConfigurationName);
			this.maxMessageSize = MetadataExchangeClient.GetMaxMessageSize(this.factory.Endpoint.Binding);
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00086B4C File Offset: 0x00084D4C
		public MetadataExchangeClient(System.ServiceModel.Channels.Binding mexBinding)
		{
			if (mexBinding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("mexBinding");
			}
			this.factory = new ChannelFactory<IMetadataExchange>(mexBinding);
			this.maxMessageSize = MetadataExchangeClient.GetMaxMessageSize(this.factory.Endpoint.Binding);
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x00086BC7 File Offset: 0x00084DC7
		// (set) Token: 0x06002581 RID: 9601 RVA: 0x00086BD4 File Offset: 0x00084DD4
		public ClientCredentials SoapCredentials
		{
			get
			{
				return this.factory.Credentials;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.factory.Endpoint.Behaviors.RemoveAll<ClientCredentials>();
				this.factory.Endpoint.Behaviors.Add(value);
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x00086C20 File Offset: 0x00084E20
		// (set) Token: 0x06002583 RID: 9603 RVA: 0x00086C28 File Offset: 0x00084E28
		public ICredentials HttpCredentials
		{
			get
			{
				return this.webRequestCredentials;
			}
			set
			{
				this.webRequestCredentials = value;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x00086C31 File Offset: 0x00084E31
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x00086C3C File Offset: 0x00084E3C
		public TimeSpan OperationTimeout
		{
			get
			{
				return this.resolveTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.resolveTimeout = value;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x00086CAF File Offset: 0x00084EAF
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x00086CB7 File Offset: 0x00084EB7
		public int MaximumResolvedReferences
		{
			get
			{
				return this.maximumResolvedReferences;
			}
			set
			{
				if (value < 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxMaximumResolvedReferencesOutOfRange", new object[]
					{
						value
					})));
				}
				this.maximumResolvedReferences = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x00086CF2 File Offset: 0x00084EF2
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x00086CFA File Offset: 0x00084EFA
		public bool ResolveMetadataReferences
		{
			get
			{
				return this.resolveMetadataReferences;
			}
			set
			{
				this.resolveMetadataReferences = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x00086D03 File Offset: 0x00084F03
		internal object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x00086D0B File Offset: 0x00084F0B
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x00086D13 File Offset: 0x00084F13
		internal long MaxMessageSize
		{
			get
			{
				return this.maxMessageSize;
			}
			set
			{
				this.maxMessageSize = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x00086D1C File Offset: 0x00084F1C
		internal XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				if (this.readerQuotas == null)
				{
					if (this.factory != null)
					{
						BindingElementCollection bindingElementCollection = this.factory.Endpoint.Binding.CreateBindingElements();
						if (bindingElementCollection != null)
						{
							MessageEncodingBindingElement messageEncodingBindingElement = bindingElementCollection.Find<MessageEncodingBindingElement>();
							if (messageEncodingBindingElement != null)
							{
								this.readerQuotas = messageEncodingBindingElement.GetIndividualProperty<XmlDictionaryReaderQuotas>();
							}
						}
					}
					this.readerQuotas = (this.readerQuotas ?? EncoderDefaults.ReaderQuotas);
				}
				return this.readerQuotas;
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00086D84 File Offset: 0x00084F84
		[SecuritySafeCritical]
		private bool ClientEndpointExists(string name)
		{
			ClientSection clientSection = ClientSection.UnsafeGetSection();
			if (clientSection == null)
			{
				return false;
			}
			foreach (object obj in clientSection.Endpoints)
			{
				ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
				if (channelEndpointElement.Name == name && channelEndpointElement.Contract == "IMetadataExchange")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00086E0C File Offset: 0x0008500C
		private bool IsHttpOrHttps(Uri address)
		{
			return address.Scheme == Uri.UriSchemeHttp || address.Scheme == Uri.UriSchemeHttps;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00086E34 File Offset: 0x00085034
		private void CreateChannelFactory(string scheme)
		{
			if (this.ClientEndpointExists(scheme))
			{
				this.factory = new ChannelFactory<IMetadataExchange>(scheme);
			}
			else
			{
				System.ServiceModel.Channels.Binding binding = null;
				if (!MetadataExchangeBindings.TryGetBindingForScheme(scheme, out binding))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("scheme", SR.GetString("SFxMetadataExchangeClientCouldNotCreateChannelFactoryBadScheme", new object[]
					{
						scheme
					}));
				}
				this.factory = new ChannelFactory<IMetadataExchange>(binding);
			}
			this.maxMessageSize = MetadataExchangeClient.GetMaxMessageSize(this.factory.Endpoint.Binding);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00086EB4 File Offset: 0x000850B4
		private void Validate(Uri address, MetadataExchangeClientMode mode)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (!address.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("address", SR.GetString("SFxCannotGetMetadataFromRelativeAddress", new object[]
				{
					address
				}));
			}
			if (mode == MetadataExchangeClientMode.HttpGet && !this.IsHttpOrHttps(address))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("address", SR.GetString("SFxCannotHttpGetMetadataFromAddress", new object[]
				{
					address
				}));
			}
			MetadataExchangeClientModeHelper.Validate(mode);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00086F40 File Offset: 0x00085140
		public IAsyncResult BeginGetMetadata(AsyncCallback callback, object asyncState)
		{
			if (this.ctorUri != null)
			{
				return this.BeginGetMetadata(this.ctorUri, MetadataExchangeClientMode.HttpGet, callback, asyncState);
			}
			if (this.ctorEndpointAddress != null)
			{
				return this.BeginGetMetadata(this.ctorEndpointAddress, callback, asyncState);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMetadataExchangeClientNoMetadataAddress")));
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x00086FA1 File Offset: 0x000851A1
		public IAsyncResult BeginGetMetadata(Uri address, MetadataExchangeClientMode mode, AsyncCallback callback, object asyncState)
		{
			this.Validate(address, mode);
			if (mode == MetadataExchangeClientMode.HttpGet)
			{
				return this.BeginGetMetadata(new MetadataExchangeClient.MetadataLocationRetriever(address, this), callback, asyncState);
			}
			return this.BeginGetMetadata(new MetadataExchangeClient.MetadataReferenceRetriever(new EndpointAddress(address, new AddressHeader[0]), this), callback, asyncState);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x00086FDB File Offset: 0x000851DB
		public IAsyncResult BeginGetMetadata(EndpointAddress address, AsyncCallback callback, object asyncState)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			return this.BeginGetMetadata(new MetadataExchangeClient.MetadataReferenceRetriever(address, this), callback, asyncState);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00087008 File Offset: 0x00085208
		private IAsyncResult BeginGetMetadata(MetadataExchangeClient.MetadataRetriever retriever, AsyncCallback callback, object asyncState)
		{
			MetadataExchangeClient.ResolveCallState resolveCallState = new MetadataExchangeClient.ResolveCallState(this.maximumResolvedReferences, this.resolveMetadataReferences, new TimeoutHelper(this.OperationTimeout), this);
			resolveCallState.StackedRetrievers.Push(retriever);
			return new MetadataExchangeClient.AsyncMetadataResolver(resolveCallState, callback, asyncState);
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00087047 File Offset: 0x00085247
		public MetadataSet EndGetMetadata(IAsyncResult result)
		{
			return MetadataExchangeClient.AsyncMetadataResolver.End(result);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x00087050 File Offset: 0x00085250
		public Task<MetadataSet> GetMetadataAsync()
		{
			if (this.ctorUri != null)
			{
				return this.GetMetadataAsync(this.ctorUri, MetadataExchangeClientMode.HttpGet);
			}
			if (this.ctorEndpointAddress != null)
			{
				return this.GetMetadataAsync(this.ctorEndpointAddress);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMetadataExchangeClientNoMetadataAddress")));
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000870B0 File Offset: 0x000852B0
		public Task<MetadataSet> GetMetadataAsync(Uri address, MetadataExchangeClientMode mode)
		{
			this.Validate(address, mode);
			MetadataExchangeClient.MetadataRetriever arg = (mode == MetadataExchangeClientMode.HttpGet) ? new MetadataExchangeClient.MetadataLocationRetriever(address, this) : new MetadataExchangeClient.MetadataReferenceRetriever(new EndpointAddress(address, new AddressHeader[0]), this);
			return Task.Factory.FromAsync<MetadataExchangeClient.MetadataRetriever, MetadataSet>(new Func<MetadataExchangeClient.MetadataRetriever, AsyncCallback, object, IAsyncResult>(this.BeginGetMetadata), new Func<IAsyncResult, MetadataSet>(this.EndGetMetadata), arg, null);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0008710C File Offset: 0x0008530C
		public Task<MetadataSet> GetMetadataAsync(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			return Task.Factory.FromAsync<MetadataExchangeClient.MetadataRetriever, MetadataSet>(new Func<MetadataExchangeClient.MetadataRetriever, AsyncCallback, object, IAsyncResult>(this.BeginGetMetadata), new Func<IAsyncResult, MetadataSet>(this.EndGetMetadata), new MetadataExchangeClient.MetadataReferenceRetriever(address, this), null);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0008715C File Offset: 0x0008535C
		public Task<MetadataSet> GetMetadataAsync(EndpointAddress address, Uri via)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (via == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("via");
			}
			return Task.Factory.FromAsync<MetadataExchangeClient.MetadataRetriever, MetadataSet>(new Func<MetadataExchangeClient.MetadataRetriever, AsyncCallback, object, IAsyncResult>(this.BeginGetMetadata), new Func<IAsyncResult, MetadataSet>(this.EndGetMetadata), new MetadataExchangeClient.MetadataReferenceRetriever(address, via, this), null);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000871C8 File Offset: 0x000853C8
		public MetadataSet GetMetadata()
		{
			if (this.ctorUri != null)
			{
				return this.GetMetadata(this.ctorUri, MetadataExchangeClientMode.HttpGet);
			}
			if (this.ctorEndpointAddress != null)
			{
				return this.GetMetadata(this.ctorEndpointAddress);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMetadataExchangeClientNoMetadataAddress")));
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x00087228 File Offset: 0x00085428
		public MetadataSet GetMetadata(Uri address, MetadataExchangeClientMode mode)
		{
			this.Validate(address, mode);
			MetadataExchangeClient.MetadataRetriever retriever;
			if (mode == MetadataExchangeClientMode.HttpGet)
			{
				retriever = new MetadataExchangeClient.MetadataLocationRetriever(address, this);
			}
			else
			{
				retriever = new MetadataExchangeClient.MetadataReferenceRetriever(new EndpointAddress(address, new AddressHeader[0]), this);
			}
			return this.GetMetadata(retriever);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00087268 File Offset: 0x00085468
		public MetadataSet GetMetadata(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			MetadataExchangeClient.MetadataReferenceRetriever retriever = new MetadataExchangeClient.MetadataReferenceRetriever(address, this);
			return this.GetMetadata(retriever);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000872A0 File Offset: 0x000854A0
		public MetadataSet GetMetadata(EndpointAddress address, Uri via)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (via == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("via");
			}
			MetadataExchangeClient.MetadataReferenceRetriever retriever = new MetadataExchangeClient.MetadataReferenceRetriever(address, via, this);
			return this.GetMetadata(retriever);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000872F0 File Offset: 0x000854F0
		private MetadataSet GetMetadata(MetadataExchangeClient.MetadataRetriever retriever)
		{
			MetadataExchangeClient.ResolveCallState resolveCallState = new MetadataExchangeClient.ResolveCallState(this.maximumResolvedReferences, this.resolveMetadataReferences, new TimeoutHelper(this.OperationTimeout), this);
			resolveCallState.StackedRetrievers.Push(retriever);
			this.ResolveNext(resolveCallState);
			return resolveCallState.MetadataSet;
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00087334 File Offset: 0x00085534
		private void ResolveNext(MetadataExchangeClient.ResolveCallState resolveCallState)
		{
			if (resolveCallState.StackedRetrievers.Count > 0)
			{
				MetadataExchangeClient.MetadataRetriever metadataRetriever = resolveCallState.StackedRetrievers.Pop();
				if (resolveCallState.HasBeenUsed(metadataRetriever))
				{
					this.ResolveNext(resolveCallState);
					return;
				}
				if (resolveCallState.ResolvedMaxResolvedReferences)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxResolvedMaxResolvedReferences")));
				}
				resolveCallState.LogUse(metadataRetriever);
				resolveCallState.HandleSection(metadataRetriever.Retrieve(resolveCallState.TimeoutHelper));
				this.ResolveNext(resolveCallState);
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000873AE File Offset: 0x000855AE
		protected internal virtual ChannelFactory<IMetadataExchange> GetChannelFactory(EndpointAddress metadataAddress, string dialect, string identifier)
		{
			return this.factory;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000873B8 File Offset: 0x000855B8
		private static long GetMaxMessageSize(System.ServiceModel.Channels.Binding mexBinding)
		{
			BindingElementCollection bindingElementCollection = mexBinding.CreateBindingElements();
			TransportBindingElement transportBindingElement = bindingElementCollection.Find<TransportBindingElement>();
			if (transportBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBindingDoesNotHaveATransportBindingElement")));
			}
			return transportBindingElement.MaxReceivedMessageSize;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000873F8 File Offset: 0x000855F8
		protected internal virtual HttpWebRequest GetWebRequest(Uri location, string dialect, string identifier)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(location);
			httpWebRequest.Method = "GET";
			httpWebRequest.Credentials = this.HttpCredentials;
			return httpWebRequest;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x0008742C File Offset: 0x0008562C
		internal static void TraceSendRequest(Uri address)
		{
			MetadataExchangeClient.TraceSendRequest(524379, SR.GetString("TraceCodeMetadataExchangeClientSendRequest"), address.ToString(), MetadataExchangeClientMode.HttpGet.ToString());
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x00087464 File Offset: 0x00085664
		internal static void TraceSendRequest(EndpointAddress address)
		{
			MetadataExchangeClient.TraceSendRequest(524379, SR.GetString("TraceCodeMetadataExchangeClientSendRequest"), address.ToString(), MetadataExchangeClientMode.MetadataExchange.ToString());
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0008749C File Offset: 0x0008569C
		private static void TraceSendRequest(int traceCode, string traceDescription, string address, string mode)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Hashtable dictionary = new Hashtable(2)
				{
					{
						"Address",
						address
					},
					{
						"Mode",
						mode
					}
				};
				TraceUtility.TraceEvent(TraceEventType.Information, traceCode, traceDescription, new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000874E0 File Offset: 0x000856E0
		internal static void TraceReceiveReply(string sourceUrl, Type metadataType)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Hashtable hashtable = new Hashtable(2);
				hashtable.Add("SourceUrl", sourceUrl);
				hashtable.Add("MetadataType", metadataType.ToString());
				TraceUtility.TraceEvent(TraceEventType.Information, 524380, SR.GetString("TraceCodeMetadataExchangeClientReceiveReply"), new DictionaryTraceRecord(hashtable), null, null);
			}
		}

		// Token: 0x040020C6 RID: 8390
		private ChannelFactory<IMetadataExchange> factory;

		// Token: 0x040020C7 RID: 8391
		private ICredentials webRequestCredentials;

		// Token: 0x040020C8 RID: 8392
		private TimeSpan resolveTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040020C9 RID: 8393
		private int maximumResolvedReferences = 10;

		// Token: 0x040020CA RID: 8394
		private bool resolveMetadataReferences = true;

		// Token: 0x040020CB RID: 8395
		private long maxMessageSize;

		// Token: 0x040020CC RID: 8396
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x040020CD RID: 8397
		private EndpointAddress ctorEndpointAddress;

		// Token: 0x040020CE RID: 8398
		private Uri ctorUri;

		// Token: 0x040020CF RID: 8399
		private object thisLock = new object();

		// Token: 0x040020D0 RID: 8400
		internal const string MetadataExchangeClientKey = "MetadataExchangeClientKey";

		// Token: 0x02000BA1 RID: 2977
		private class ResolveCallState
		{
			// Token: 0x060073A9 RID: 29609 RVA: 0x001AFB64 File Offset: 0x001ADD64
			internal ResolveCallState(int maxResolvedReferences, bool resolveMetadataReferences, TimeoutHelper timeoutHelper, MetadataExchangeClient resolver)
			{
				this.maxResolvedReferences = maxResolvedReferences;
				this.resolveMetadataReferences = resolveMetadataReferences;
				this.resolver = resolver;
				this.timeoutHelper = timeoutHelper;
				this.metadataSet = new MetadataSet();
				this.usedRetrievers = new Dictionary<MetadataExchangeClient.MetadataRetriever, MetadataExchangeClient.MetadataRetriever>();
				this.stackedRetrievers = new Stack<MetadataExchangeClient.MetadataRetriever>();
			}

			// Token: 0x17001AD0 RID: 6864
			// (get) Token: 0x060073AA RID: 29610 RVA: 0x001AFBB5 File Offset: 0x001ADDB5
			internal MetadataSet MetadataSet
			{
				get
				{
					return this.metadataSet;
				}
			}

			// Token: 0x17001AD1 RID: 6865
			// (get) Token: 0x060073AB RID: 29611 RVA: 0x001AFBBD File Offset: 0x001ADDBD
			internal Stack<MetadataExchangeClient.MetadataRetriever> StackedRetrievers
			{
				get
				{
					return this.stackedRetrievers;
				}
			}

			// Token: 0x17001AD2 RID: 6866
			// (get) Token: 0x060073AC RID: 29612 RVA: 0x001AFBC5 File Offset: 0x001ADDC5
			internal bool ResolvedMaxResolvedReferences
			{
				get
				{
					return this.usedRetrievers.Count == this.maxResolvedReferences;
				}
			}

			// Token: 0x17001AD3 RID: 6867
			// (get) Token: 0x060073AD RID: 29613 RVA: 0x001AFBDA File Offset: 0x001ADDDA
			internal TimeoutHelper TimeoutHelper
			{
				get
				{
					return this.timeoutHelper;
				}
			}

			// Token: 0x060073AE RID: 29614 RVA: 0x001AFBE4 File Offset: 0x001ADDE4
			internal void HandleSection(MetadataSection section)
			{
				if (section.Metadata is MetadataSet)
				{
					using (IEnumerator<MetadataSection> enumerator = ((MetadataSet)section.Metadata).MetadataSections.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MetadataSection metadataSection = enumerator.Current;
							metadataSection.SourceUrl = section.SourceUrl;
							this.HandleSection(metadataSection);
						}
						return;
					}
				}
				if (section.Metadata is MetadataReference)
				{
					if (this.resolveMetadataReferences)
					{
						EndpointAddress address = ((MetadataReference)section.Metadata).Address;
						MetadataExchangeClient.MetadataRetriever item = new MetadataExchangeClient.MetadataReferenceRetriever(address, this.resolver, section.Dialect, section.Identifier);
						this.stackedRetrievers.Push(item);
						return;
					}
					this.metadataSet.MetadataSections.Add(section);
					return;
				}
				else if (section.Metadata is MetadataLocation)
				{
					if (this.resolveMetadataReferences)
					{
						string location = ((MetadataLocation)section.Metadata).Location;
						MetadataExchangeClient.MetadataRetriever item2 = new MetadataExchangeClient.MetadataLocationRetriever(this.CreateUri(section.SourceUrl, location), this.resolver, section.Dialect, section.Identifier);
						this.stackedRetrievers.Push(item2);
						return;
					}
					this.metadataSet.MetadataSections.Add(section);
					return;
				}
				else
				{
					if (section.Metadata is ServiceDescription)
					{
						if (this.resolveMetadataReferences)
						{
							this.HandleWsdlImports(section);
						}
						this.metadataSet.MetadataSections.Add(section);
						return;
					}
					if (section.Metadata is XmlSchema)
					{
						if (this.resolveMetadataReferences)
						{
							this.HandleSchemaImports(section);
						}
						this.metadataSet.MetadataSections.Add(section);
						return;
					}
					this.metadataSet.MetadataSections.Add(section);
				}
			}

			// Token: 0x060073AF RID: 29615 RVA: 0x001AFD90 File Offset: 0x001ADF90
			private void HandleSchemaImports(MetadataSection section)
			{
				XmlSchema xmlSchema = (XmlSchema)section.Metadata;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Includes)
				{
					XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
					if (!string.IsNullOrEmpty(xmlSchemaExternal.SchemaLocation))
					{
						this.EnqueueRetrieverIfShouldResolve(new MetadataExchangeClient.MetadataLocationRetriever(this.CreateUri(section.SourceUrl, xmlSchemaExternal.SchemaLocation), this.resolver));
					}
				}
			}

			// Token: 0x060073B0 RID: 29616 RVA: 0x001AFE20 File Offset: 0x001AE020
			private void HandleWsdlImports(MetadataSection section)
			{
				ServiceDescription serviceDescription = (ServiceDescription)section.Metadata;
				foreach (object obj in serviceDescription.Imports)
				{
					Import import = (Import)obj;
					if (!string.IsNullOrEmpty(import.Location))
					{
						this.EnqueueRetrieverIfShouldResolve(new MetadataExchangeClient.MetadataLocationRetriever(this.CreateUri(section.SourceUrl, import.Location), this.resolver));
					}
				}
				foreach (object obj2 in serviceDescription.Types.Schemas)
				{
					XmlSchema metadata = (XmlSchema)obj2;
					this.HandleSchemaImports(new MetadataSection(null, null, metadata)
					{
						SourceUrl = section.SourceUrl
					});
				}
			}

			// Token: 0x060073B1 RID: 29617 RVA: 0x001AFF1C File Offset: 0x001AE11C
			private Uri CreateUri(string baseUri, string relativeUri)
			{
				return new Uri(new Uri(baseUri), relativeUri);
			}

			// Token: 0x060073B2 RID: 29618 RVA: 0x001AFF2A File Offset: 0x001AE12A
			private void EnqueueRetrieverIfShouldResolve(MetadataExchangeClient.MetadataRetriever retriever)
			{
				if (this.resolveMetadataReferences)
				{
					this.stackedRetrievers.Push(retriever);
				}
			}

			// Token: 0x060073B3 RID: 29619 RVA: 0x001AFF40 File Offset: 0x001AE140
			internal bool HasBeenUsed(MetadataExchangeClient.MetadataRetriever retriever)
			{
				return this.usedRetrievers.ContainsKey(retriever);
			}

			// Token: 0x060073B4 RID: 29620 RVA: 0x001AFF4E File Offset: 0x001AE14E
			internal void LogUse(MetadataExchangeClient.MetadataRetriever retriever)
			{
				this.usedRetrievers.Add(retriever, retriever);
			}

			// Token: 0x04004187 RID: 16775
			private Dictionary<MetadataExchangeClient.MetadataRetriever, MetadataExchangeClient.MetadataRetriever> usedRetrievers;

			// Token: 0x04004188 RID: 16776
			private MetadataSet metadataSet;

			// Token: 0x04004189 RID: 16777
			private int maxResolvedReferences;

			// Token: 0x0400418A RID: 16778
			private bool resolveMetadataReferences;

			// Token: 0x0400418B RID: 16779
			private Stack<MetadataExchangeClient.MetadataRetriever> stackedRetrievers;

			// Token: 0x0400418C RID: 16780
			private MetadataExchangeClient resolver;

			// Token: 0x0400418D RID: 16781
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000BA2 RID: 2978
		private abstract class MetadataRetriever
		{
			// Token: 0x060073B5 RID: 29621 RVA: 0x001AFF5D File Offset: 0x001AE15D
			public MetadataRetriever(MetadataExchangeClient resolver, string dialect, string identifier)
			{
				this.resolver = resolver;
				this.dialect = dialect;
				this.identifier = identifier;
			}

			// Token: 0x060073B6 RID: 29622 RVA: 0x001AFF7C File Offset: 0x001AE17C
			internal MetadataSection Retrieve(TimeoutHelper timeoutHelper)
			{
				MetadataSection result;
				try
				{
					using (XmlReader xmlReader = this.DownloadMetadata(timeoutHelper))
					{
						result = MetadataExchangeClient.MetadataRetriever.CreateMetadataSection(xmlReader, this.SourceUrl);
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadMetadataReference", new object[]
					{
						this.SourceUrl
					}), ex));
				}
				return result;
			}

			// Token: 0x060073B7 RID: 29623
			internal abstract IAsyncResult BeginRetrieve(TimeoutHelper timeoutHelper, AsyncCallback callback, object state);

			// Token: 0x060073B8 RID: 29624
			internal abstract MetadataSection EndRetrieve(IAsyncResult result);

			// Token: 0x060073B9 RID: 29625 RVA: 0x001AFFFC File Offset: 0x001AE1FC
			internal static MetadataSection CreateMetadataSection(XmlReader reader, string sourceUrl)
			{
				MetadataSection metadataSection;
				Type typeFromHandle;
				if (MetadataExchangeClient.MetadataRetriever.CanReadMetadataSet(reader))
				{
					MetadataSet metadata = MetadataSet.ReadFrom(reader);
					metadataSection = new MetadataSection(MetadataSection.MetadataExchangeDialect, null, metadata);
					typeFromHandle = typeof(MetadataSet);
				}
				else if (ServiceDescription.CanRead(reader))
				{
					ServiceDescription serviceDescription = ServiceDescription.Read(reader);
					metadataSection = MetadataSection.CreateFromServiceDescription(serviceDescription);
					typeFromHandle = typeof(ServiceDescription);
				}
				else if (MetadataExchangeClient.MetadataRetriever.CanReadSchema(reader))
				{
					XmlSchema schema = XmlSchema.Read(reader, null);
					metadataSection = MetadataSection.CreateFromSchema(schema);
					typeFromHandle = typeof(XmlSchema);
				}
				else
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(reader);
					metadataSection = new MetadataSection(null, null, xmlDocument.DocumentElement);
					typeFromHandle = typeof(XmlElement);
				}
				metadataSection.SourceUrl = sourceUrl;
				MetadataExchangeClient.TraceReceiveReply(sourceUrl, typeFromHandle);
				return metadataSection;
			}

			// Token: 0x060073BA RID: 29626
			protected abstract XmlReader DownloadMetadata(TimeoutHelper timeoutHelper);

			// Token: 0x17001AD4 RID: 6868
			// (get) Token: 0x060073BB RID: 29627
			protected abstract string SourceUrl { get; }

			// Token: 0x060073BC RID: 29628 RVA: 0x001B00B7 File Offset: 0x001AE2B7
			private static bool CanReadSchema(XmlReader reader)
			{
				return reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema";
			}

			// Token: 0x060073BD RID: 29629 RVA: 0x001B00DD File Offset: 0x001AE2DD
			private static bool CanReadMetadataSet(XmlReader reader)
			{
				return reader.LocalName == "Metadata" && reader.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/mex";
			}

			// Token: 0x0400418E RID: 16782
			protected MetadataExchangeClient resolver;

			// Token: 0x0400418F RID: 16783
			protected string dialect;

			// Token: 0x04004190 RID: 16784
			protected string identifier;
		}

		// Token: 0x02000BA3 RID: 2979
		private class MetadataLocationRetriever : MetadataExchangeClient.MetadataRetriever
		{
			// Token: 0x060073BE RID: 29630 RVA: 0x001B0103 File Offset: 0x001AE303
			internal MetadataLocationRetriever(Uri location, MetadataExchangeClient resolver) : this(location, resolver, null, null)
			{
			}

			// Token: 0x060073BF RID: 29631 RVA: 0x001B010F File Offset: 0x001AE30F
			internal MetadataLocationRetriever(Uri location, MetadataExchangeClient resolver, string dialect, string identifier) : base(resolver, dialect, identifier)
			{
				MetadataExchangeClient.MetadataLocationRetriever.ValidateLocation(location);
				this.location = location;
				this.responseLocation = location;
			}

			// Token: 0x060073C0 RID: 29632 RVA: 0x001B0130 File Offset: 0x001AE330
			internal static void ValidateLocation(Uri location)
			{
				if (location == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("location");
				}
				if (location.Scheme != Uri.UriSchemeHttp && location.Scheme != Uri.UriSchemeHttps)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("location", SR.GetString("SFxCannotGetMetadataFromLocation", new object[]
					{
						location.ToString()
					}));
				}
			}

			// Token: 0x060073C1 RID: 29633 RVA: 0x001B01A3 File Offset: 0x001AE3A3
			public override bool Equals(object obj)
			{
				return obj is MetadataExchangeClient.MetadataLocationRetriever && ((MetadataExchangeClient.MetadataLocationRetriever)obj).location == this.location;
			}

			// Token: 0x060073C2 RID: 29634 RVA: 0x001B01C5 File Offset: 0x001AE3C5
			public override int GetHashCode()
			{
				return this.location.GetHashCode();
			}

			// Token: 0x060073C3 RID: 29635 RVA: 0x001B01D4 File Offset: 0x001AE3D4
			protected override XmlReader DownloadMetadata(TimeoutHelper timeoutHelper)
			{
				HttpWebRequest webRequest;
				try
				{
					webRequest = this.resolver.GetWebRequest(this.location, this.dialect, this.identifier);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMetadataExchangeClientCouldNotCreateWebRequest", new object[]
					{
						this.location,
						this.dialect,
						this.identifier
					}), ex));
				}
				MetadataExchangeClient.TraceSendRequest(this.location);
				webRequest.Timeout = TimeoutHelper.ToMilliseconds(timeoutHelper.RemainingTime());
				HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
				this.responseLocation = webRequest.Address;
				return MetadataExchangeClient.MetadataLocationRetriever.GetXmlReader(response, this.resolver.MaxMessageSize, this.resolver.ReaderQuotas);
			}

			// Token: 0x060073C4 RID: 29636 RVA: 0x001B02AC File Offset: 0x001AE4AC
			internal static XmlReader GetXmlReader(HttpWebResponse response, long maxMessageSize, XmlDictionaryReaderQuotas readerQuotas)
			{
				readerQuotas = (readerQuotas ?? EncoderDefaults.ReaderQuotas);
				XmlReader xmlReader = XmlDictionaryReader.CreateTextReader(new MaxMessageSizeStream(response.GetResponseStream(), maxMessageSize), MetadataExchangeClient.EncodingHelper.GetDictionaryReaderEncoding(response.ContentType), readerQuotas, null);
				xmlReader.Read();
				xmlReader.MoveToContent();
				return xmlReader;
			}

			// Token: 0x060073C5 RID: 29637 RVA: 0x001B02F4 File Offset: 0x001AE4F4
			internal override IAsyncResult BeginRetrieve(TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				MetadataExchangeClient.MetadataLocationRetriever.AsyncMetadataLocationRetriever result;
				try
				{
					HttpWebRequest webRequest;
					try
					{
						webRequest = this.resolver.GetWebRequest(this.location, this.dialect, this.identifier);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMetadataExchangeClientCouldNotCreateWebRequest", new object[]
						{
							this.location,
							this.dialect,
							this.identifier
						}), ex));
					}
					MetadataExchangeClient.TraceSendRequest(this.location);
					result = new MetadataExchangeClient.MetadataLocationRetriever.AsyncMetadataLocationRetriever(webRequest, this.resolver.MaxMessageSize, this.resolver.ReaderQuotas, timeoutHelper, callback, state);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadMetadataReference", new object[]
					{
						this.SourceUrl
					}), ex2));
				}
				return result;
			}

			// Token: 0x060073C6 RID: 29638 RVA: 0x001B03E8 File Offset: 0x001AE5E8
			internal override MetadataSection EndRetrieve(IAsyncResult result)
			{
				MetadataSection result2;
				try
				{
					result2 = MetadataExchangeClient.MetadataLocationRetriever.AsyncMetadataLocationRetriever.End(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadMetadataReference", new object[]
					{
						this.SourceUrl
					}), ex));
				}
				return result2;
			}

			// Token: 0x17001AD5 RID: 6869
			// (get) Token: 0x060073C7 RID: 29639 RVA: 0x001B0444 File Offset: 0x001AE644
			protected override string SourceUrl
			{
				get
				{
					return this.responseLocation.ToString();
				}
			}

			// Token: 0x04004191 RID: 16785
			private Uri location;

			// Token: 0x04004192 RID: 16786
			private Uri responseLocation;

			// Token: 0x02000F04 RID: 3844
			private class AsyncMetadataLocationRetriever : AsyncResult
			{
				// Token: 0x060085A4 RID: 34212 RVA: 0x001EF110 File Offset: 0x001ED310
				internal AsyncMetadataLocationRetriever(WebRequest request, long maxMessageSize, XmlDictionaryReaderQuotas readerQuotas, TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
				{
					this.maxMessageSize = maxMessageSize;
					this.readerQuotas = readerQuotas;
					IAsyncResult asyncResult = request.BeginGetResponse(Fx.ThunkCallback(new AsyncCallback(this.GetResponseCallback)), request);
					ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, Fx.ThunkCallback(new WaitOrTimerCallback(MetadataExchangeClient.MetadataLocationRetriever.AsyncMetadataLocationRetriever.RetrieveTimeout)), request, TimeoutHelper.ToMilliseconds(timeoutHelper.RemainingTime()), true);
					if (asyncResult.CompletedSynchronously)
					{
						this.HandleResult(asyncResult);
						base.Complete(true);
					}
				}

				// Token: 0x060085A5 RID: 34213 RVA: 0x001EF190 File Offset: 0x001ED390
				private static void RetrieveTimeout(object state, bool timedOut)
				{
					if (timedOut)
					{
						HttpWebRequest httpWebRequest = state as HttpWebRequest;
						if (httpWebRequest != null)
						{
							httpWebRequest.Abort();
						}
					}
				}

				// Token: 0x060085A6 RID: 34214 RVA: 0x001EF1B0 File Offset: 0x001ED3B0
				internal static MetadataSection End(IAsyncResult result)
				{
					MetadataExchangeClient.MetadataLocationRetriever.AsyncMetadataLocationRetriever asyncMetadataLocationRetriever = AsyncResult.End<MetadataExchangeClient.MetadataLocationRetriever.AsyncMetadataLocationRetriever>(result);
					return asyncMetadataLocationRetriever.section;
				}

				// Token: 0x060085A7 RID: 34215 RVA: 0x001EF1CC File Offset: 0x001ED3CC
				internal void GetResponseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					Exception exception = null;
					try
					{
						this.HandleResult(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					base.Complete(false, exception);
				}

				// Token: 0x060085A8 RID: 34216 RVA: 0x001EF214 File Offset: 0x001ED414
				private void HandleResult(IAsyncResult result)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
					using (XmlReader xmlReader = MetadataExchangeClient.MetadataLocationRetriever.GetXmlReader((HttpWebResponse)httpWebRequest.EndGetResponse(result), this.maxMessageSize, this.readerQuotas))
					{
						this.section = MetadataExchangeClient.MetadataRetriever.CreateMetadataSection(xmlReader, httpWebRequest.Address.ToString());
					}
				}

				// Token: 0x04004D68 RID: 19816
				private MetadataSection section;

				// Token: 0x04004D69 RID: 19817
				private long maxMessageSize;

				// Token: 0x04004D6A RID: 19818
				private XmlDictionaryReaderQuotas readerQuotas;
			}
		}

		// Token: 0x02000BA4 RID: 2980
		private class MetadataReferenceRetriever : MetadataExchangeClient.MetadataRetriever
		{
			// Token: 0x060073C8 RID: 29640 RVA: 0x001B0451 File Offset: 0x001AE651
			public MetadataReferenceRetriever(EndpointAddress address, MetadataExchangeClient resolver) : this(address, null, resolver, null, null)
			{
			}

			// Token: 0x060073C9 RID: 29641 RVA: 0x001B045E File Offset: 0x001AE65E
			public MetadataReferenceRetriever(EndpointAddress address, Uri via, MetadataExchangeClient resolver) : this(address, via, resolver, null, null)
			{
			}

			// Token: 0x060073CA RID: 29642 RVA: 0x001B046B File Offset: 0x001AE66B
			public MetadataReferenceRetriever(EndpointAddress address, MetadataExchangeClient resolver, string dialect, string identifier) : this(address, null, resolver, dialect, identifier)
			{
			}

			// Token: 0x060073CB RID: 29643 RVA: 0x001B0479 File Offset: 0x001AE679
			private MetadataReferenceRetriever(EndpointAddress address, Uri via, MetadataExchangeClient resolver, string dialect, string identifier) : base(resolver, dialect, identifier)
			{
				if (address == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
				}
				this.address = address;
				this.via = via;
			}

			// Token: 0x17001AD6 RID: 6870
			// (get) Token: 0x060073CC RID: 29644 RVA: 0x001B04AD File Offset: 0x001AE6AD
			protected override string SourceUrl
			{
				get
				{
					return this.address.Uri.ToString();
				}
			}

			// Token: 0x060073CD RID: 29645 RVA: 0x001B04C0 File Offset: 0x001AE6C0
			internal override IAsyncResult BeginRetrieve(TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				try
				{
					object thisLock = this.resolver.ThisLock;
					IMetadataExchange metadataClient;
					MessageVersion messageVersion;
					lock (thisLock)
					{
						ChannelFactory<IMetadataExchange> channelFactory;
						try
						{
							channelFactory = this.resolver.GetChannelFactory(this.address, this.dialect, this.identifier);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMetadataExchangeClientCouldNotCreateChannelFactory", new object[]
							{
								this.address,
								this.dialect,
								this.identifier
							}), ex));
						}
						metadataClient = this.CreateChannel(channelFactory);
						messageVersion = channelFactory.Endpoint.Binding.MessageVersion;
					}
					MetadataExchangeClient.TraceSendRequest(this.address);
					result = new MetadataExchangeClient.MetadataReferenceRetriever.AsyncMetadataReferenceRetriever(metadataClient, messageVersion, timeoutHelper, callback, state);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadMetadataReference", new object[]
					{
						this.SourceUrl
					}), ex2));
				}
				return result;
			}

			// Token: 0x060073CE RID: 29646 RVA: 0x001B05F0 File Offset: 0x001AE7F0
			private IMetadataExchange CreateChannel(ChannelFactory<IMetadataExchange> channelFactory)
			{
				if (this.via != null)
				{
					return channelFactory.CreateChannel(this.address, this.via);
				}
				return channelFactory.CreateChannel(this.address);
			}

			// Token: 0x060073CF RID: 29647 RVA: 0x001B061F File Offset: 0x001AE81F
			private static System.ServiceModel.Channels.Message CreateGetMessage(MessageVersion messageVersion)
			{
				return System.ServiceModel.Channels.Message.CreateMessage(messageVersion, "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get");
			}

			// Token: 0x060073D0 RID: 29648 RVA: 0x001B062C File Offset: 0x001AE82C
			protected override XmlReader DownloadMetadata(TimeoutHelper timeoutHelper)
			{
				object thisLock = this.resolver.ThisLock;
				IMetadataExchange metadataExchange;
				MessageVersion messageVersion;
				lock (thisLock)
				{
					ChannelFactory<IMetadataExchange> channelFactory;
					try
					{
						channelFactory = this.resolver.GetChannelFactory(this.address, this.dialect, this.identifier);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMetadataExchangeClientCouldNotCreateChannelFactory", new object[]
						{
							this.address,
							this.dialect,
							this.identifier
						}), ex));
					}
					metadataExchange = this.CreateChannel(channelFactory);
					messageVersion = channelFactory.Endpoint.Binding.MessageVersion;
				}
				MetadataExchangeClient.TraceSendRequest(this.address);
				System.ServiceModel.Channels.Message message2;
				try
				{
					using (System.ServiceModel.Channels.Message message = MetadataExchangeClient.MetadataReferenceRetriever.CreateGetMessage(messageVersion))
					{
						((IClientChannel)metadataExchange).OperationTimeout = timeoutHelper.RemainingTime();
						message2 = metadataExchange.Get(message);
					}
					((IClientChannel)metadataExchange).Close();
				}
				finally
				{
					((IClientChannel)metadataExchange).Abort();
				}
				if (message2.IsFault)
				{
					MessageFault messageFault = MessageFault.CreateFault(message2, 65536);
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlWriter xmlWriter = XmlWriter.Create(stringWriter);
					messageFault.WriteTo(xmlWriter, message2.Version.Envelope);
					xmlWriter.Flush();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(stringWriter.ToString()));
				}
				return message2.GetReaderAtBodyContents();
			}

			// Token: 0x060073D1 RID: 29649 RVA: 0x001B07D0 File Offset: 0x001AE9D0
			internal override MetadataSection EndRetrieve(IAsyncResult result)
			{
				MetadataSection result2;
				try
				{
					result2 = MetadataExchangeClient.MetadataReferenceRetriever.AsyncMetadataReferenceRetriever.End(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadMetadataReference", new object[]
					{
						this.SourceUrl
					}), ex));
				}
				return result2;
			}

			// Token: 0x060073D2 RID: 29650 RVA: 0x001B082C File Offset: 0x001AEA2C
			public override bool Equals(object obj)
			{
				return obj is MetadataExchangeClient.MetadataReferenceRetriever && ((MetadataExchangeClient.MetadataReferenceRetriever)obj).address == this.address;
			}

			// Token: 0x060073D3 RID: 29651 RVA: 0x001B084E File Offset: 0x001AEA4E
			public override int GetHashCode()
			{
				return this.address.GetHashCode();
			}

			// Token: 0x04004193 RID: 16787
			private EndpointAddress address;

			// Token: 0x04004194 RID: 16788
			private Uri via;

			// Token: 0x02000F05 RID: 3845
			private class AsyncMetadataReferenceRetriever : AsyncResult
			{
				// Token: 0x060085A9 RID: 34217 RVA: 0x001EF280 File Offset: 0x001ED480
				internal AsyncMetadataReferenceRetriever(IMetadataExchange metadataClient, MessageVersion messageVersion, TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
				{
					this.message = MetadataExchangeClient.MetadataReferenceRetriever.CreateGetMessage(messageVersion);
					((IClientChannel)metadataClient).OperationTimeout = timeoutHelper.RemainingTime();
					IAsyncResult asyncResult = metadataClient.BeginGet(this.message, Fx.ThunkCallback(new AsyncCallback(this.RequestCallback)), metadataClient);
					if (asyncResult.CompletedSynchronously)
					{
						this.HandleResult(asyncResult);
						base.Complete(true);
					}
				}

				// Token: 0x060085AA RID: 34218 RVA: 0x001EF2EC File Offset: 0x001ED4EC
				internal static MetadataSection End(IAsyncResult result)
				{
					MetadataExchangeClient.MetadataReferenceRetriever.AsyncMetadataReferenceRetriever asyncMetadataReferenceRetriever = AsyncResult.End<MetadataExchangeClient.MetadataReferenceRetriever.AsyncMetadataReferenceRetriever>(result);
					return asyncMetadataReferenceRetriever.section;
				}

				// Token: 0x060085AB RID: 34219 RVA: 0x001EF308 File Offset: 0x001ED508
				internal void RequestCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					Exception exception = null;
					try
					{
						this.HandleResult(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					base.Complete(false, exception);
				}

				// Token: 0x060085AC RID: 34220 RVA: 0x001EF350 File Offset: 0x001ED550
				private void HandleResult(IAsyncResult result)
				{
					IMetadataExchange metadataExchange = (IMetadataExchange)result.AsyncState;
					System.ServiceModel.Channels.Message message = metadataExchange.EndGet(result);
					using (this.message)
					{
						if (message.IsFault)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxBadMetadataReference", new object[]
							{
								((IClientChannel)metadataExchange).RemoteAddress.Uri.ToString()
							})));
						}
						using (XmlReader readerAtBodyContents = message.GetReaderAtBodyContents())
						{
							this.section = MetadataExchangeClient.MetadataRetriever.CreateMetadataSection(readerAtBodyContents, ((IClientChannel)metadataExchange).RemoteAddress.Uri.ToString());
						}
					}
				}

				// Token: 0x04004D6B RID: 19819
				private MetadataSection section;

				// Token: 0x04004D6C RID: 19820
				private System.ServiceModel.Channels.Message message;
			}
		}

		// Token: 0x02000BA5 RID: 2981
		private class AsyncMetadataResolver : AsyncResult
		{
			// Token: 0x060073D4 RID: 29652 RVA: 0x001B085C File Offset: 0x001AEA5C
			internal AsyncMetadataResolver(MetadataExchangeClient.ResolveCallState resolveCallState, AsyncCallback callerCallback, object callerAsyncState) : base(callerCallback, callerAsyncState)
			{
				if (resolveCallState == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("resolveCallState");
				}
				this.resolveCallState = resolveCallState;
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.ResolveNext();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
					flag = true;
				}
				if (flag)
				{
					base.Complete(true, exception);
				}
			}

			// Token: 0x060073D5 RID: 29653 RVA: 0x001B08C4 File Offset: 0x001AEAC4
			private bool ResolveNext()
			{
				bool result = false;
				if (this.resolveCallState.StackedRetrievers.Count > 0)
				{
					MetadataExchangeClient.MetadataRetriever metadataRetriever = this.resolveCallState.StackedRetrievers.Pop();
					if (this.resolveCallState.HasBeenUsed(metadataRetriever))
					{
						result = this.ResolveNext();
					}
					else
					{
						if (this.resolveCallState.ResolvedMaxResolvedReferences)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxResolvedMaxResolvedReferences")));
						}
						this.resolveCallState.LogUse(metadataRetriever);
						IAsyncResult asyncResult = metadataRetriever.BeginRetrieve(this.resolveCallState.TimeoutHelper, Fx.ThunkCallback(new AsyncCallback(this.RetrieveCallback)), metadataRetriever);
						if (asyncResult.CompletedSynchronously)
						{
							result = this.HandleResult(asyncResult);
						}
					}
				}
				else
				{
					result = true;
				}
				return result;
			}

			// Token: 0x060073D6 RID: 29654 RVA: 0x001B0980 File Offset: 0x001AEB80
			internal static MetadataSet End(IAsyncResult result)
			{
				MetadataExchangeClient.AsyncMetadataResolver asyncMetadataResolver = AsyncResult.End<MetadataExchangeClient.AsyncMetadataResolver>(result);
				return asyncMetadataResolver.resolveCallState.MetadataSet;
			}

			// Token: 0x060073D7 RID: 29655 RVA: 0x001B09A0 File Offset: 0x001AEBA0
			internal void RetrieveCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleResult(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
					flag = true;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x060073D8 RID: 29656 RVA: 0x001B09F0 File Offset: 0x001AEBF0
			private bool HandleResult(IAsyncResult result)
			{
				MetadataExchangeClient.MetadataRetriever metadataRetriever = (MetadataExchangeClient.MetadataRetriever)result.AsyncState;
				MetadataSection section = metadataRetriever.EndRetrieve(result);
				this.resolveCallState.HandleSection(section);
				return this.ResolveNext();
			}

			// Token: 0x04004195 RID: 16789
			private MetadataExchangeClient.ResolveCallState resolveCallState;
		}

		// Token: 0x02000BA6 RID: 2982
		internal class EncodingHelper
		{
			// Token: 0x060073D9 RID: 29657 RVA: 0x001B0A24 File Offset: 0x001AEC24
			internal static Encoding GetRfcEncoding(string contentTypeStr)
			{
				Encoding encoding = null;
				ContentType contentType = null;
				try
				{
					contentType = new ContentType(contentTypeStr);
					string text = (contentType == null) ? string.Empty : contentType.CharSet;
					if (text != null && text.Length > 0)
					{
						encoding = Encoding.GetEncoding(text);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
				if (!MetadataExchangeClient.EncodingHelper.IsApplication(contentType))
				{
					return encoding;
				}
				if (encoding != null)
				{
					return encoding;
				}
				return new ASCIIEncoding();
			}

			// Token: 0x060073DA RID: 29658 RVA: 0x001B0A94 File Offset: 0x001AEC94
			internal static bool IsApplication(ContentType contentType)
			{
				return string.Compare((contentType == null) ? string.Empty : contentType.MediaType, "application", StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x060073DB RID: 29659 RVA: 0x001B0AB4 File Offset: 0x001AECB4
			internal static Encoding GetDictionaryReaderEncoding(string contentTypeStr)
			{
				if (string.IsNullOrEmpty(contentTypeStr))
				{
					return TextEncoderDefaults.Encoding;
				}
				Encoding rfcEncoding = MetadataExchangeClient.EncodingHelper.GetRfcEncoding(contentTypeStr);
				if (rfcEncoding == null)
				{
					return TextEncoderDefaults.Encoding;
				}
				string webName = rfcEncoding.WebName;
				Encoding[] supportedEncodings = TextEncoderDefaults.SupportedEncodings;
				for (int i = 0; i < supportedEncodings.Length; i++)
				{
					if (webName == supportedEncodings[i].WebName)
					{
						return rfcEncoding;
					}
				}
				return TextEncoderDefaults.Encoding;
			}

			// Token: 0x04004196 RID: 16790
			internal const string ApplicationBase = "application";
		}
	}
}

using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000149 RID: 329
	public class NetNamedPipeBinding : Binding, IBindingRuntimePreferences
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x00024CA9 File Offset: 0x00022EA9
		public NetNamedPipeBinding()
		{
			this.Initialize();
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00024CC2 File Offset: 0x00022EC2
		public NetNamedPipeBinding(NetNamedPipeSecurityMode securityMode) : this()
		{
			this.security.Mode = securityMode;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00024CD6 File Offset: 0x00022ED6
		public NetNamedPipeBinding(string configurationName) : this()
		{
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00024CE5 File Offset: 0x00022EE5
		private NetNamedPipeBinding(NetNamedPipeSecurity security) : this()
		{
			this.security = security;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00024CF4 File Offset: 0x00022EF4
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00024D01 File Offset: 0x00022F01
		[DefaultValue(false)]
		public bool TransactionFlow
		{
			get
			{
				return this.context.Transactions;
			}
			set
			{
				this.context.Transactions = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00024D0F File Offset: 0x00022F0F
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00024D1C File Offset: 0x00022F1C
		public TransactionProtocol TransactionProtocol
		{
			get
			{
				return this.context.TransactionProtocol;
			}
			set
			{
				this.context.TransactionProtocol = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00024D2A File Offset: 0x00022F2A
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00024D37 File Offset: 0x00022F37
		[DefaultValue(TransferMode.Buffered)]
		public TransferMode TransferMode
		{
			get
			{
				return this.namedPipe.TransferMode;
			}
			set
			{
				this.namedPipe.TransferMode = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00024D45 File Offset: 0x00022F45
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00024D52 File Offset: 0x00022F52
		[DefaultValue(HostNameComparisonMode.StrongWildcard)]
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.namedPipe.HostNameComparisonMode;
			}
			set
			{
				this.namedPipe.HostNameComparisonMode = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00024D60 File Offset: 0x00022F60
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00024D6D File Offset: 0x00022F6D
		[DefaultValue(524288L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return this.namedPipe.MaxBufferPoolSize;
			}
			set
			{
				this.namedPipe.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00024D7B File Offset: 0x00022F7B
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x00024D88 File Offset: 0x00022F88
		[DefaultValue(65536)]
		public int MaxBufferSize
		{
			get
			{
				return this.namedPipe.MaxBufferSize;
			}
			set
			{
				this.namedPipe.MaxBufferSize = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00024D96 File Offset: 0x00022F96
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x00024DA3 File Offset: 0x00022FA3
		public int MaxConnections
		{
			get
			{
				return this.namedPipe.MaxPendingConnections;
			}
			set
			{
				this.namedPipe.MaxPendingConnections = value;
				this.namedPipe.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00024DC2 File Offset: 0x00022FC2
		internal bool IsMaxConnectionsSet
		{
			get
			{
				return this.namedPipe.IsMaxPendingConnectionsSet;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00024DCF File Offset: 0x00022FCF
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00024DDC File Offset: 0x00022FDC
		[DefaultValue(65536L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.namedPipe.MaxReceivedMessageSize;
			}
			set
			{
				this.namedPipe.MaxReceivedMessageSize = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00024DEA File Offset: 0x00022FEA
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00024DF7 File Offset: 0x00022FF7
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.encoding.ReaderQuotas;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				value.CopyTo(this.encoding.ReaderQuotas);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00024E1D File Offset: 0x0002301D
		bool IBindingRuntimePreferences.ReceiveSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00024E20 File Offset: 0x00023020
		public override string Scheme
		{
			get
			{
				return this.namedPipe.Scheme;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00024E2D File Offset: 0x0002302D
		public EnvelopeVersion EnvelopeVersion
		{
			get
			{
				return EnvelopeVersion.Soap12;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00024E34 File Offset: 0x00023034
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00024E3C File Offset: 0x0002303C
		public NetNamedPipeSecurity Security
		{
			get
			{
				return this.security;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.security = value;
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00024E58 File Offset: 0x00023058
		private static TransactionFlowBindingElement GetDefaultTransactionFlowBindingElement()
		{
			return new TransactionFlowBindingElement(false);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00024E60 File Offset: 0x00023060
		private void Initialize()
		{
			this.namedPipe = new NamedPipeTransportBindingElement();
			this.encoding = new BinaryMessageEncodingBindingElement();
			this.context = NetNamedPipeBinding.GetDefaultTransactionFlowBindingElement();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00024E84 File Offset: 0x00023084
		private void InitializeFrom(NamedPipeTransportBindingElement namedPipe, BinaryMessageEncodingBindingElement encoding, TransactionFlowBindingElement context)
		{
			this.Initialize();
			this.HostNameComparisonMode = namedPipe.HostNameComparisonMode;
			this.MaxBufferPoolSize = namedPipe.MaxBufferPoolSize;
			this.MaxBufferSize = namedPipe.MaxBufferSize;
			if (namedPipe.IsMaxPendingConnectionsSet)
			{
				this.MaxConnections = namedPipe.MaxPendingConnections;
			}
			this.MaxReceivedMessageSize = namedPipe.MaxReceivedMessageSize;
			this.TransferMode = namedPipe.TransferMode;
			this.ReaderQuotas = encoding.ReaderQuotas;
			this.TransactionFlow = context.Transactions;
			this.TransactionProtocol = context.TransactionProtocol;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00024F0B File Offset: 0x0002310B
		private bool IsBindingElementsMatch(NamedPipeTransportBindingElement namedPipe, BinaryMessageEncodingBindingElement encoding, TransactionFlowBindingElement context)
		{
			return this.namedPipe.IsMatch(namedPipe) && this.encoding.IsMatch(encoding) && this.context.IsMatch(context);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00024F40 File Offset: 0x00023140
		private void ApplyConfiguration(string configurationName)
		{
			NetNamedPipeBindingCollectionElement bindingCollectionElement = NetNamedPipeBindingCollectionElement.GetBindingCollectionElement();
			NetNamedPipeBindingElement netNamedPipeBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (netNamedPipeBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"netNamedPipeBinding"
				})));
			}
			netNamedPipeBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00024F98 File Offset: 0x00023198
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			bindingElementCollection.Add(this.context);
			bindingElementCollection.Add(this.encoding);
			WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = this.CreateTransportSecurity();
			if (windowsStreamSecurityBindingElement != null)
			{
				bindingElementCollection.Add(windowsStreamSecurityBindingElement);
			}
			bindingElementCollection.Add(this.namedPipe);
			return bindingElementCollection.Clone();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00024FE8 File Offset: 0x000231E8
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count > 4)
			{
				return false;
			}
			TransactionFlowBindingElement transactionFlowBindingElement = null;
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = null;
			WindowsStreamSecurityBindingElement wssbe = null;
			NamedPipeTransportBindingElement namedPipeTransportBindingElement = null;
			foreach (BindingElement bindingElement in elements)
			{
				if (bindingElement is TransactionFlowBindingElement)
				{
					transactionFlowBindingElement = (bindingElement as TransactionFlowBindingElement);
				}
				else if (bindingElement is BinaryMessageEncodingBindingElement)
				{
					binaryMessageEncodingBindingElement = (bindingElement as BinaryMessageEncodingBindingElement);
				}
				else if (bindingElement is WindowsStreamSecurityBindingElement)
				{
					wssbe = (bindingElement as WindowsStreamSecurityBindingElement);
				}
				else
				{
					if (!(bindingElement is NamedPipeTransportBindingElement))
					{
						return false;
					}
					namedPipeTransportBindingElement = (bindingElement as NamedPipeTransportBindingElement);
				}
			}
			if (namedPipeTransportBindingElement == null)
			{
				return false;
			}
			if (binaryMessageEncodingBindingElement == null)
			{
				return false;
			}
			if (transactionFlowBindingElement == null)
			{
				transactionFlowBindingElement = NetNamedPipeBinding.GetDefaultTransactionFlowBindingElement();
			}
			NetNamedPipeSecurity netNamedPipeSecurity;
			if (!NetNamedPipeBinding.TryCreateSecurity(wssbe, out netNamedPipeSecurity))
			{
				return false;
			}
			NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding(netNamedPipeSecurity);
			netNamedPipeBinding.InitializeFrom(namedPipeTransportBindingElement, binaryMessageEncodingBindingElement, transactionFlowBindingElement);
			if (!netNamedPipeBinding.IsBindingElementsMatch(namedPipeTransportBindingElement, binaryMessageEncodingBindingElement, transactionFlowBindingElement))
			{
				return false;
			}
			binding = netNamedPipeBinding;
			return true;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000250E0 File Offset: 0x000232E0
		private WindowsStreamSecurityBindingElement CreateTransportSecurity()
		{
			if (this.security.Mode == NetNamedPipeSecurityMode.Transport)
			{
				return this.security.CreateTransportSecurity();
			}
			return null;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00025100 File Offset: 0x00023300
		private static bool TryCreateSecurity(WindowsStreamSecurityBindingElement wssbe, out NetNamedPipeSecurity security)
		{
			NetNamedPipeSecurityMode mode = (wssbe == null) ? NetNamedPipeSecurityMode.None : NetNamedPipeSecurityMode.Transport;
			return NetNamedPipeSecurity.TryCreate(wssbe, mode, out security);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002511D File Offset: 0x0002331D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002512D File Offset: 0x0002332D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.security.Mode != NetNamedPipeSecurityMode.Transport || this.security.Transport.ProtectionLevel != ProtectionLevel.EncryptAndSign;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00025155 File Offset: 0x00023355
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransactionProtocol()
		{
			return this.TransactionProtocol != NetTcpDefaults.TransactionProtocol;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00025167 File Offset: 0x00023367
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMaxConnections()
		{
			return this.namedPipe.ShouldSerializeMaxPendingConnections();
		}

		// Token: 0x04000B70 RID: 2928
		private TransactionFlowBindingElement context;

		// Token: 0x04000B71 RID: 2929
		private BinaryMessageEncodingBindingElement encoding;

		// Token: 0x04000B72 RID: 2930
		private NamedPipeTransportBindingElement namedPipe;

		// Token: 0x04000B73 RID: 2931
		private NetNamedPipeSecurity security = new NetNamedPipeSecurity();
	}
}

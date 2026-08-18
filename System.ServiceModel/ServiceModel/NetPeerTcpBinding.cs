using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.PeerResolvers;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200014D RID: 333
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	public class NetPeerTcpBinding : Binding, IBindingRuntimePreferences
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x00025272 File Offset: 0x00023472
		public NetPeerTcpBinding()
		{
			this.Initialize();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00025280 File Offset: 0x00023480
		public NetPeerTcpBinding(string configurationName) : this()
		{
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0002528F File Offset: 0x0002348F
		public static bool IsPnrpAvailable
		{
			get
			{
				return PnrpPeerResolver.IsPnrpAvailable;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00025296 File Offset: 0x00023496
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x000252A3 File Offset: 0x000234A3
		[DefaultValue(524288L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return this.transport.MaxBufferPoolSize;
			}
			set
			{
				this.transport.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x000252B1 File Offset: 0x000234B1
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x000252BE File Offset: 0x000234BE
		[DefaultValue(65536L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.transport.MaxReceivedMessageSize;
			}
			set
			{
				this.transport.MaxReceivedMessageSize = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x000252CC File Offset: 0x000234CC
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x000252D9 File Offset: 0x000234D9
		[DefaultValue(null)]
		[TypeConverter(typeof(PeerTransportListenAddressConverter))]
		public IPAddress ListenIPAddress
		{
			get
			{
				return this.transport.ListenIPAddress;
			}
			set
			{
				this.transport.ListenIPAddress = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x000252E7 File Offset: 0x000234E7
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x000252EF File Offset: 0x000234EF
		public PeerSecuritySettings Security
		{
			get
			{
				return this.peerSecurity;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.peerSecurity = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0002530B File Offset: 0x0002350B
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00025318 File Offset: 0x00023518
		[DefaultValue(0)]
		public int Port
		{
			get
			{
				return this.transport.Port;
			}
			set
			{
				this.transport.Port = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00025326 File Offset: 0x00023526
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00025333 File Offset: 0x00023533
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

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00025359 File Offset: 0x00023559
		public PeerResolverSettings Resolver
		{
			get
			{
				return this.resolverSettings;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00025361 File Offset: 0x00023561
		bool IBindingRuntimePreferences.ReceiveSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00025364 File Offset: 0x00023564
		public override string Scheme
		{
			get
			{
				return this.transport.Scheme;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00025371 File Offset: 0x00023571
		public EnvelopeVersion EnvelopeVersion
		{
			get
			{
				return EnvelopeVersion.Soap12;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00025378 File Offset: 0x00023578
		private void Initialize()
		{
			this.resolverSettings = new PeerResolverSettings();
			this.transport = new PeerTransportBindingElement();
			this.encoding = new BinaryMessageEncodingBindingElement();
			this.peerSecurity = new PeerSecuritySettings();
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000253A8 File Offset: 0x000235A8
		private void InitializeFrom(PeerTransportBindingElement transport, BinaryMessageEncodingBindingElement encoding)
		{
			this.MaxBufferPoolSize = transport.MaxBufferPoolSize;
			this.MaxReceivedMessageSize = transport.MaxReceivedMessageSize;
			this.ListenIPAddress = transport.ListenIPAddress;
			this.Port = transport.Port;
			this.Security.Mode = transport.Security.Mode;
			this.ReaderQuotas = encoding.ReaderQuotas;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00025407 File Offset: 0x00023607
		private bool IsBindingElementsMatch(PeerTransportBindingElement transport, BinaryMessageEncodingBindingElement encoding)
		{
			return this.transport.IsMatch(transport) && this.encoding.IsMatch(encoding);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002542C File Offset: 0x0002362C
		private void ApplyConfiguration(string configurationName)
		{
			NetPeerTcpBindingCollectionElement bindingCollectionElement = NetPeerTcpBindingCollectionElement.GetBindingCollectionElement();
			NetPeerTcpBindingElement netPeerTcpBindingElement = bindingCollectionElement.Bindings[configurationName];
			this.resolverSettings = new PeerResolverSettings();
			if (netPeerTcpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"netPeerTcpBinding"
				})));
			}
			netPeerTcpBindingElement.ApplyConfiguration(this);
			this.transport.CreateDefaultResolver(this.Resolver);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000254A0 File Offset: 0x000236A0
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			switch (this.Resolver.Mode)
			{
			case PeerResolverMode.Auto:
				if (this.CanUseCustomResolver())
				{
					bindingElementCollection.Add(new PeerCustomResolverBindingElement(this.Resolver.Custom));
				}
				else
				{
					if (!PeerTransportDefaults.ResolverAvailable)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerResolverRequired")));
					}
					bindingElementCollection.Add(new PnrpPeerResolverBindingElement(this.Resolver.ReferralPolicy));
				}
				break;
			case PeerResolverMode.Pnrp:
				if (!PeerTransportDefaults.ResolverAvailable)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerResolverRequired")));
				}
				bindingElementCollection.Add(new PnrpPeerResolverBindingElement(this.Resolver.ReferralPolicy));
				break;
			case PeerResolverMode.Custom:
				if (!this.CanUseCustomResolver())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerResolverSettingsInvalid")));
				}
				bindingElementCollection.Add(new PeerCustomResolverBindingElement(this.Resolver.Custom));
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerResolverRequired")));
			}
			bindingElementCollection.Add(this.encoding);
			bindingElementCollection.Add(this.transport);
			this.transport.Security.Mode = this.Security.Mode;
			this.transport.Security.Transport.CredentialType = this.Security.Transport.CredentialType;
			return bindingElementCollection.Clone();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00025620 File Offset: 0x00023820
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count != 3)
			{
				return false;
			}
			PeerResolverBindingElement peerResolverBindingElement = null;
			PeerTransportBindingElement peerTransportBindingElement = null;
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = null;
			foreach (BindingElement bindingElement in elements)
			{
				if (bindingElement is TransportBindingElement)
				{
					peerTransportBindingElement = (bindingElement as PeerTransportBindingElement);
				}
				else if (bindingElement is BinaryMessageEncodingBindingElement)
				{
					binaryMessageEncodingBindingElement = (bindingElement as BinaryMessageEncodingBindingElement);
				}
				else
				{
					if (!(bindingElement is PeerResolverBindingElement))
					{
						return false;
					}
					peerResolverBindingElement = (bindingElement as PeerResolverBindingElement);
				}
			}
			if (peerTransportBindingElement == null)
			{
				return false;
			}
			if (binaryMessageEncodingBindingElement == null)
			{
				return false;
			}
			if (peerResolverBindingElement == null)
			{
				return false;
			}
			NetPeerTcpBinding netPeerTcpBinding = new NetPeerTcpBinding();
			netPeerTcpBinding.InitializeFrom(peerTransportBindingElement, binaryMessageEncodingBindingElement);
			if (!netPeerTcpBinding.IsBindingElementsMatch(peerTransportBindingElement, binaryMessageEncodingBindingElement))
			{
				return false;
			}
			PeerCustomResolverBindingElement peerCustomResolverBindingElement = peerResolverBindingElement as PeerCustomResolverBindingElement;
			if (peerCustomResolverBindingElement != null)
			{
				netPeerTcpBinding.Resolver.Custom.Address = peerCustomResolverBindingElement.Address;
				netPeerTcpBinding.Resolver.Custom.Binding = peerCustomResolverBindingElement.Binding;
				netPeerTcpBinding.Resolver.Custom.Resolver = peerCustomResolverBindingElement.CreatePeerResolver();
			}
			else if (peerResolverBindingElement is PnrpPeerResolverBindingElement && NetPeerTcpBinding.IsPnrpAvailable)
			{
				netPeerTcpBinding.Resolver.Mode = PeerResolverMode.Pnrp;
			}
			binding = netPeerTcpBinding;
			return true;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002575C File Offset: 0x0002395C
		private bool CanUseCustomResolver()
		{
			return this.Resolver.Custom.Resolver != null || (this.Resolver.Custom.IsBindingSpecified && this.Resolver.Custom.Address != null);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002579C File Offset: 0x0002399C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000257AC File Offset: 0x000239AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x04000B7A RID: 2938
		private PeerTransportBindingElement transport;

		// Token: 0x04000B7B RID: 2939
		private PeerResolverSettings resolverSettings;

		// Token: 0x04000B7C RID: 2940
		private BinaryMessageEncodingBindingElement encoding;

		// Token: 0x04000B7D RID: 2941
		private PeerSecuritySettings peerSecurity;
	}
}

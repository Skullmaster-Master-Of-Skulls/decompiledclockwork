using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Linq;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200002E RID: 46
	public class EndpointDiscoveryMetadata
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000758E File Offset: 0x0000578E
		public EndpointDiscoveryMetadata()
		{
			this.endpointAddress = new EndpointAddress(EndpointAddress.AnonymousUri, new AddressHeader[0]);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000075AC File Offset: 0x000057AC
		public Collection<XmlQualifiedName> ContractTypeNames
		{
			get
			{
				if (this.contractTypeNames == null)
				{
					this.contractTypeNames = new EndpointDiscoveryMetadata.OpenableContractTypeNameCollection(this.isOpen);
				}
				return this.contractTypeNames;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000075CD File Offset: 0x000057CD
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000075D5 File Offset: 0x000057D5
		public EndpointAddress Address
		{
			get
			{
				return this.endpointAddress;
			}
			set
			{
				this.ThrowIfOpen();
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.endpointAddress = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000075FD File Offset: 0x000057FD
		public Collection<XElement> Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new EndpointDiscoveryMetadata.OpenableCollection<XElement>(this.isOpen);
				}
				return this.extensions;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000761E File Offset: 0x0000581E
		public Collection<Uri> ListenUris
		{
			get
			{
				if (this.listenUris == null)
				{
					this.listenUris = new EndpointDiscoveryMetadata.OpenableCollection<Uri>(this.isOpen);
				}
				return this.listenUris;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000763F File Offset: 0x0000583F
		public Collection<Uri> Scopes
		{
			get
			{
				if (this.scopes == null)
				{
					this.scopes = new EndpointDiscoveryMetadata.OpenableScopeCollection(this.isOpen);
				}
				return this.scopes;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007660 File Offset: 0x00005860
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00007668 File Offset: 0x00005868
		public int Version
		{
			get
			{
				return this.metadataVersion;
			}
			set
			{
				this.ThrowIfOpen();
				if (value < 0)
				{
					throw FxTrace.Exception.ArgumentOutOfRange("value", value, SR.DiscoveryMetadataVersionLessThanZero);
				}
				this.metadataVersion = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007698 File Offset: 0x00005898
		internal static XmlQualifiedName MetadataContractName
		{
			get
			{
				if (EndpointDiscoveryMetadata.metadataContractName == null)
				{
					ContractDescription contract = ContractDescription.GetContract(typeof(IMetadataExchange));
					EndpointDiscoveryMetadata.metadataContractName = new XmlQualifiedName(contract.Name, contract.Namespace);
				}
				return EndpointDiscoveryMetadata.metadataContractName;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000076DD File Offset: 0x000058DD
		internal Collection<XmlQualifiedName> InternalContractTypeNames
		{
			get
			{
				return this.contractTypeNames;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000076E5 File Offset: 0x000058E5
		internal string[] CompiledScopes
		{
			get
			{
				return this.compiledScopes;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000076ED File Offset: 0x000058ED
		internal bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000076F5 File Offset: 0x000058F5
		public static EndpointDiscoveryMetadata FromServiceEndpoint(ServiceEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpoint");
			}
			return EndpointDiscoveryMetadata.GetEndpointDiscoveryMetadata(endpoint, endpoint.ListenUri);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007718 File Offset: 0x00005918
		public static EndpointDiscoveryMetadata FromServiceEndpoint(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpoint == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpoint");
			}
			if (endpointDispatcher == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointDispatcher");
			}
			EndpointDiscoveryMetadata endpointDiscoveryMetadata;
			if (endpointDispatcher.ChannelDispatcher != null && endpointDispatcher.ChannelDispatcher.Listener != null)
			{
				endpointDiscoveryMetadata = EndpointDiscoveryMetadata.GetEndpointDiscoveryMetadata(endpoint, endpointDispatcher.ChannelDispatcher.Listener.Uri);
			}
			else
			{
				endpointDiscoveryMetadata = EndpointDiscoveryMetadata.GetEndpointDiscoveryMetadata(endpoint, endpoint.ListenUri);
			}
			if (endpointDiscoveryMetadata != null && EndpointDiscoveryMetadata.IsMetadataEndpoint(endpoint) && EndpointDiscoveryMetadata.CanHaveMetadataEndpoints(endpointDispatcher))
			{
				EndpointDiscoveryMetadata.AddContractTypeScopes(endpointDiscoveryMetadata, endpointDispatcher.ChannelDispatcher.Host.Description);
			}
			return endpointDiscoveryMetadata;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000077B0 File Offset: 0x000059B0
		private static EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(ServiceEndpoint endpoint, Uri listenUri)
		{
			EndpointDiscoveryMetadata endpointDiscoveryMetadata = new EndpointDiscoveryMetadata();
			endpointDiscoveryMetadata.Address = endpoint.Address;
			endpointDiscoveryMetadata.ListenUris.Add(listenUri);
			EndpointDiscoveryBehavior endpointDiscoveryBehavior = endpoint.Behaviors.Find<EndpointDiscoveryBehavior>();
			if (endpointDiscoveryBehavior != null)
			{
				if (!endpointDiscoveryBehavior.Enabled)
				{
					if (TD.EndpointDiscoverabilityDisabledIsEnabled())
					{
						TD.EndpointDiscoverabilityDisabled(endpoint.Address.ToString(), listenUri.ToString());
					}
					return null;
				}
				if (TD.EndpointDiscoverabilityEnabledIsEnabled())
				{
					TD.EndpointDiscoverabilityEnabled(endpoint.Address.ToString(), listenUri.ToString());
				}
				if (endpointDiscoveryBehavior.InternalContractTypeNames != null)
				{
					foreach (XmlQualifiedName item in endpointDiscoveryBehavior.InternalContractTypeNames)
					{
						endpointDiscoveryMetadata.ContractTypeNames.Add(item);
					}
				}
				if (endpointDiscoveryBehavior.InternalScopes != null)
				{
					foreach (Uri item2 in endpointDiscoveryBehavior.InternalScopes)
					{
						endpointDiscoveryMetadata.Scopes.Add(item2);
					}
				}
				if (endpointDiscoveryBehavior.InternalExtensions != null)
				{
					foreach (XElement item3 in endpointDiscoveryBehavior.InternalExtensions)
					{
						endpointDiscoveryMetadata.Extensions.Add(item3);
					}
				}
			}
			XmlQualifiedName item4 = new XmlQualifiedName(endpoint.Contract.Name, endpoint.Contract.Namespace);
			if (!endpointDiscoveryMetadata.ContractTypeNames.Contains(item4))
			{
				endpointDiscoveryMetadata.ContractTypeNames.Add(item4);
			}
			return endpointDiscoveryMetadata;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007958 File Offset: 0x00005B58
		private static void AddContractTypeScopes(EndpointDiscoveryMetadata endpointDiscoveryMetadata, ServiceDescription serviceDescription)
		{
			foreach (ServiceEndpoint serviceEndpoint in serviceDescription.Endpoints)
			{
				if (!EndpointDiscoveryMetadata.IsMetadataEndpoint(serviceEndpoint) && !EndpointDiscoveryMetadata.IsDiscoverySystemEndpoint(serviceEndpoint))
				{
					endpointDiscoveryMetadata.Scopes.Add(FindCriteria.GetContractTypeNameScope(new XmlQualifiedName(serviceEndpoint.Contract.Name, serviceEndpoint.Contract.Namespace)));
				}
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000079DC File Offset: 0x00005BDC
		private static bool CanHaveMetadataEndpoints(EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher.ChannelDispatcher == null || endpointDispatcher.ChannelDispatcher.Host == null)
			{
				return false;
			}
			ServiceDescription description = endpointDispatcher.ChannelDispatcher.Host.Description;
			return (description.Behaviors == null || description.Behaviors.Find<ServiceMetadataBehavior>() != null) && (!(description.ServiceType != null) || !(description.ServiceType.GetInterface(typeof(IMetadataExchange).Name) != null));
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007A5B File Offset: 0x00005C5B
		internal static bool IsDiscoverySystemEndpoint(EndpointDispatcher endpointDispatcher)
		{
			return endpointDispatcher.IsSystemEndpoint && EndpointDiscoveryMetadata.IsDiscoveryContract(endpointDispatcher.ContractName, endpointDispatcher.ContractNamespace);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007A78 File Offset: 0x00005C78
		internal static bool IsDiscoverySystemEndpoint(ServiceEndpoint endpoint)
		{
			return endpoint.IsSystemEndpoint && EndpointDiscoveryMetadata.IsDiscoveryContract(endpoint.Contract.Name, endpoint.Contract.Namespace);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007A9F File Offset: 0x00005C9F
		private static bool IsDiscoveryContract(string contractName, string contractNamespace)
		{
			return EndpointDiscoveryMetadata.IsDiscoveryContractName(contractName) && EndpointDiscoveryMetadata.IsDiscoveryContractNamespace(contractNamespace);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007AB1 File Offset: 0x00005CB1
		private static bool IsDiscoveryContractName(string contractName)
		{
			return string.CompareOrdinal(contractName, "TargetService") == 0 || string.CompareOrdinal(contractName, "DiscoveryProxy") == 0;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007AD0 File Offset: 0x00005CD0
		private static bool IsDiscoveryContractNamespace(string contractNamespace)
		{
			return string.CompareOrdinal(contractNamespace, "http://schemas.xmlsoap.org/ws/2005/04/discovery") == 0 || string.CompareOrdinal(contractNamespace, "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01") == 0 || string.CompareOrdinal(contractNamespace, "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09") == 0;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007AFC File Offset: 0x00005CFC
		internal static bool IsMetadataEndpoint(ServiceEndpoint endpoint)
		{
			return string.CompareOrdinal(endpoint.Contract.Name, EndpointDiscoveryMetadata.MetadataContractName.Name) == 0 && string.CompareOrdinal(endpoint.Contract.Namespace, EndpointDiscoveryMetadata.MetadataContractName.Namespace) == 0;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007B3C File Offset: 0x00005D3C
		internal void ReadFrom(DiscoveryVersion discoveryVersion, XmlReader reader)
		{
			this.ThrowIfOpen();
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			if (reader == null)
			{
				throw FxTrace.Exception.ArgumentNull("reader");
			}
			this.endpointAddress = new EndpointAddress(EndpointAddress.AnonymousUri, new AddressHeader[0]);
			this.contractTypeNames = null;
			this.scopes = null;
			this.listenUris = null;
			this.metadataVersion = 0;
			this.extensions = null;
			this.isOpen = false;
			reader.MoveToContent();
			if (reader.IsEmptyElement)
			{
				throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlEndpointNull));
			}
			int depth = reader.Depth;
			reader.ReadStartElement();
			this.endpointAddress = SerializationUtility.ReadEndpointAddress(discoveryVersion, reader);
			if (reader.IsStartElement("Types", discoveryVersion.Namespace))
			{
				this.contractTypeNames = new EndpointDiscoveryMetadata.OpenableContractTypeNameCollection(false);
				SerializationUtility.ReadContractTypeNames(this.contractTypeNames, reader);
			}
			if (reader.IsStartElement("Scopes", discoveryVersion.Namespace))
			{
				this.scopes = new EndpointDiscoveryMetadata.OpenableScopeCollection(false);
				SerializationUtility.ReadScopes(this.scopes, reader);
			}
			if (reader.IsStartElement("XAddrs", discoveryVersion.Namespace))
			{
				this.listenUris = new EndpointDiscoveryMetadata.OpenableCollection<Uri>(false);
				SerializationUtility.ReadListenUris(this.listenUris, reader);
			}
			if (reader.IsStartElement("MetadataVersion", discoveryVersion.Namespace))
			{
				this.metadataVersion = SerializationUtility.ReadMetadataVersion(reader);
			}
			for (;;)
			{
				reader.MoveToContent();
				if (reader.NodeType == XmlNodeType.EndElement && reader.Depth == depth)
				{
					break;
				}
				if (reader.IsStartElement())
				{
					this.Extensions.Add(XNode.ReadFrom(reader) as XElement);
				}
				else
				{
					reader.Read();
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007CE0 File Offset: 0x00005EE0
		internal void WriteTo(DiscoveryVersion discoveryVersion, XmlWriter writer)
		{
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			if (writer == null)
			{
				throw FxTrace.Exception.ArgumentNull("writer");
			}
			SerializationUtility.WriteEndPointAddress(discoveryVersion, this.endpointAddress, writer);
			SerializationUtility.WriteContractTypeNames(discoveryVersion, this.contractTypeNames, writer);
			SerializationUtility.WriteScopes(discoveryVersion, this.scopes, null, writer);
			SerializationUtility.WriteListenUris(discoveryVersion, this.listenUris, writer);
			SerializationUtility.WriteMetadataVersion(discoveryVersion, this.metadataVersion, writer);
			if (this.extensions != null)
			{
				foreach (XElement xelement in this.Extensions)
				{
					xelement.WriteTo(writer);
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007DA0 File Offset: 0x00005FA0
		internal void Open()
		{
			if (this.contractTypeNames != null)
			{
				this.contractTypeNames.Open();
			}
			if (this.scopes != null)
			{
				this.scopes.Open();
				this.compiledScopes = ScopeCompiler.Compile(this.scopes);
			}
			if (this.listenUris != null)
			{
				this.listenUris.Open();
			}
			if (this.extensions != null)
			{
				this.extensions.Open();
			}
			this.isOpen = true;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007E11 File Offset: 0x00006011
		private void ThrowIfOpen()
		{
			if (this.isOpen)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryMetadataAlreadyOpen));
			}
		}

		// Token: 0x0400008A RID: 138
		private static XmlQualifiedName metadataContractName;

		// Token: 0x0400008B RID: 139
		private EndpointAddress endpointAddress;

		// Token: 0x0400008C RID: 140
		private EndpointDiscoveryMetadata.OpenableContractTypeNameCollection contractTypeNames;

		// Token: 0x0400008D RID: 141
		private EndpointDiscoveryMetadata.OpenableScopeCollection scopes;

		// Token: 0x0400008E RID: 142
		private EndpointDiscoveryMetadata.OpenableCollection<Uri> listenUris;

		// Token: 0x0400008F RID: 143
		private EndpointDiscoveryMetadata.OpenableCollection<XElement> extensions;

		// Token: 0x04000090 RID: 144
		private int metadataVersion;

		// Token: 0x04000091 RID: 145
		private string[] compiledScopes;

		// Token: 0x04000092 RID: 146
		private bool isOpen;

		// Token: 0x020000D0 RID: 208
		private class OpenableCollection<T> : NonNullItemCollection<T>
		{
			// Token: 0x060007EC RID: 2028 RVA: 0x00014DCF File Offset: 0x00012FCF
			public OpenableCollection(bool opened)
			{
				this.isOpen = opened;
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x00014DDE File Offset: 0x00012FDE
			private void ThrowIfOpen()
			{
				if (this.isOpen)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoverySdmCollectionIsOpen(typeof(T).Name)));
				}
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x00014E0C File Offset: 0x0001300C
			internal void Open()
			{
				this.isOpen = true;
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x00014E15 File Offset: 0x00013015
			protected override void ClearItems()
			{
				this.ThrowIfOpen();
				base.ClearItems();
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x00014E23 File Offset: 0x00013023
			protected override void InsertItem(int index, T item)
			{
				this.ThrowIfOpen();
				base.InsertItem(index, item);
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00014E33 File Offset: 0x00013033
			protected override void RemoveItem(int index)
			{
				this.ThrowIfOpen();
				base.RemoveItem(index);
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x00014E42 File Offset: 0x00013042
			protected override void SetItem(int index, T item)
			{
				this.ThrowIfOpen();
				base.SetItem(index, item);
			}

			// Token: 0x0400020D RID: 525
			private bool isOpen;
		}

		// Token: 0x020000D1 RID: 209
		private class OpenableContractTypeNameCollection : EndpointDiscoveryMetadata.OpenableCollection<XmlQualifiedName>
		{
			// Token: 0x060007F3 RID: 2035 RVA: 0x00014E52 File Offset: 0x00013052
			public OpenableContractTypeNameCollection(bool opened) : base(opened)
			{
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x00014E5B File Offset: 0x0001305B
			protected override void InsertItem(int index, XmlQualifiedName item)
			{
				if (item != null && item.Name == string.Empty)
				{
					throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryArgumentEmptyContractTypeName));
				}
				base.InsertItem(index, item);
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00014E95 File Offset: 0x00013095
			protected override void SetItem(int index, XmlQualifiedName item)
			{
				if (item != null && item.Name == string.Empty)
				{
					throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryArgumentEmptyContractTypeName));
				}
				base.SetItem(index, item);
			}
		}

		// Token: 0x020000D2 RID: 210
		private class OpenableScopeCollection : EndpointDiscoveryMetadata.OpenableCollection<Uri>
		{
			// Token: 0x060007F6 RID: 2038 RVA: 0x00014ECF File Offset: 0x000130CF
			public OpenableScopeCollection(bool opened) : base(opened)
			{
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x00014ED8 File Offset: 0x000130D8
			protected override void InsertItem(int index, Uri item)
			{
				if (item != null && !item.IsAbsoluteUri)
				{
					throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryArgumentInvalidScopeUri(item)));
				}
				base.InsertItem(index, item);
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x00014F09 File Offset: 0x00013109
			protected override void SetItem(int index, Uri item)
			{
				if (item != null && !item.IsAbsoluteUri)
				{
					throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryArgumentInvalidScopeUri(item)));
				}
				base.SetItem(index, item);
			}
		}
	}
}

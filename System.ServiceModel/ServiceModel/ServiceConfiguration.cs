using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IdentityModel.Configuration;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel
{
	// Token: 0x02000102 RID: 258
	public class ServiceConfiguration
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0001A617 File Offset: 0x00018817
		internal ServiceConfiguration(ServiceHost host)
		{
			ServiceConfiguration.CheckArgument<ServiceHost>(host, "host");
			this.host = host;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001A631 File Offset: 0x00018831
		public ServiceDescription Description
		{
			get
			{
				return this.host.Description;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001A63E File Offset: 0x0001883E
		public ServiceAuthenticationBehavior Authentication
		{
			get
			{
				return this.host.Authentication;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001A64B File Offset: 0x0001884B
		public ServiceAuthorizationBehavior Authorization
		{
			get
			{
				return this.host.Authorization;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001A658 File Offset: 0x00018858
		public ServiceCredentials Credentials
		{
			get
			{
				return this.host.Credentials;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001A665 File Offset: 0x00018865
		public ReadOnlyCollection<Uri> BaseAddresses
		{
			get
			{
				return this.host.BaseAddresses;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001A672 File Offset: 0x00018872
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0001A67F File Offset: 0x0001887F
		public TimeSpan OpenTimeout
		{
			get
			{
				return this.host.OpenTimeout;
			}
			set
			{
				this.host.OpenTimeout = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001A68D File Offset: 0x0001888D
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0001A69A File Offset: 0x0001889A
		public TimeSpan CloseTimeout
		{
			get
			{
				return this.host.CloseTimeout;
			}
			set
			{
				this.host.CloseTimeout = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001A6A8 File Offset: 0x000188A8
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0001A6B5 File Offset: 0x000188B5
		public bool UseIdentityConfiguration
		{
			get
			{
				return this.Credentials.UseIdentityConfiguration;
			}
			set
			{
				this.Credentials.UseIdentityConfiguration = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001A6C3 File Offset: 0x000188C3
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0001A6D0 File Offset: 0x000188D0
		public IdentityConfiguration IdentityConfiguration
		{
			get
			{
				return this.Credentials.IdentityConfiguration;
			}
			set
			{
				this.Credentials.IdentityConfiguration = value;
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001A6E0 File Offset: 0x000188E0
		public void AddServiceEndpoint(ServiceEndpoint endpoint)
		{
			ServiceConfiguration.CheckArgument<ServiceEndpoint>(endpoint, "endpoint");
			if (this.host.State != CommunicationState.Created && this.host.State != CommunicationState.Opening)
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
				IContractResolver contractResolver = this.host.GetContractResolver(this.host.ImplementedContracts);
				ConfigLoader configLoader = new ConfigLoader(contractResolver);
				configLoader.LookupContract(endpoint.Contract.ConfigurationName, this.Description.Name);
			}
			this.Description.Endpoints.Add(endpoint);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001A81C File Offset: 0x00018A1C
		public ServiceEndpoint AddServiceEndpoint(Type contractType, Binding binding, string address)
		{
			ServiceConfiguration.CheckArgument<string>(address, "address");
			return this.AddServiceEndpoint(contractType, binding, new Uri(address, UriKind.RelativeOrAbsolute));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001A838 File Offset: 0x00018A38
		public ServiceEndpoint AddServiceEndpoint(Type contractType, Binding binding, Uri address)
		{
			ServiceConfiguration.CheckArgument<Type>(contractType, "contractType");
			ServiceConfiguration.CheckArgument<Binding>(binding, "binding");
			ServiceConfiguration.CheckArgument<Uri>(address, "address");
			ContractDescription contractDescription = (this.host.ImplementedContracts == null) ? null : this.host.ImplementedContracts.Values.FirstOrDefault((ContractDescription implementedContract) => implementedContract.ContractType == contractType);
			if (contractDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("contractType", SR.GetString("SFxMethodNotSupportedByType2", new object[]
				{
					this.host.Description.ServiceType,
					contractType
				}));
			}
			ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contractDescription, binding, new EndpointAddress(ServiceHostBase.MakeAbsoluteUri(address, binding, this.host.InternalBaseAddresses), new AddressHeader[0]));
			this.AddServiceEndpoint(serviceEndpoint);
			return serviceEndpoint;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001A918 File Offset: 0x00018B18
		public ServiceEndpoint AddServiceEndpoint(Type contractType, Binding binding, string address, Uri listenUri)
		{
			ServiceConfiguration.CheckArgument<Uri>(listenUri, "listenUri");
			ServiceEndpoint serviceEndpoint = this.AddServiceEndpoint(contractType, binding, address);
			this.SetListenUri(serviceEndpoint, binding, listenUri);
			return serviceEndpoint;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001A948 File Offset: 0x00018B48
		public ServiceEndpoint AddServiceEndpoint(Type contractType, Binding binding, Uri address, Uri listenUri)
		{
			ServiceConfiguration.CheckArgument<Uri>(listenUri, "listenUri");
			ServiceEndpoint serviceEndpoint = this.AddServiceEndpoint(contractType, binding, address);
			this.SetListenUri(serviceEndpoint, binding, listenUri);
			return serviceEndpoint;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001A976 File Offset: 0x00018B76
		public void SetEndpointAddress(ServiceEndpoint endpoint, string relativeAddress)
		{
			ServiceConfiguration.CheckArgument<ServiceEndpoint>(endpoint, "endpoint");
			ServiceConfiguration.CheckArgument<string>(relativeAddress, "relativeAddress");
			this.host.SetEndpointAddress(endpoint, relativeAddress);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001A99C File Offset: 0x00018B9C
		public Collection<ServiceEndpoint> EnableProtocol(Binding protocol)
		{
			ServiceConfiguration.CheckArgument<Binding>(protocol, "protocol");
			Collection<ServiceEndpoint> collection = new Collection<ServiceEndpoint>();
			if (this.host.ImplementedContracts != null)
			{
				IEnumerable<ContractDescription> contracts = this.host.ImplementedContracts.Values;
				IEnumerable<ContractDescription> enumerable = from contract in contracts
				where contracts.All((ContractDescription otherContract) => contract == otherContract || !contract.ContractType.IsAssignableFrom(otherContract.ContractType))
				select contract;
				foreach (Uri uri in this.host.BaseAddresses)
				{
					if (uri.Scheme.Equals(protocol.Scheme))
					{
						foreach (ContractDescription contract2 in enumerable)
						{
							ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contract2, protocol, new EndpointAddress(uri, new AddressHeader[0]));
							this.AddServiceEndpoint(serviceEndpoint);
							collection.Add(serviceEndpoint);
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		public void LoadFromConfiguration()
		{
			this.host.LoadFromConfiguration();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001AABD File Offset: 0x00018CBD
		public void LoadFromConfiguration(Configuration configuration)
		{
			ServiceConfiguration.CheckArgument<Configuration>(configuration, "configuration");
			this.host.LoadFromConfiguration(configuration);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001AAD6 File Offset: 0x00018CD6
		private static void CheckArgument<T>(T argument, string argumentName)
		{
			if (argument == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(argumentName);
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001AAEC File Offset: 0x00018CEC
		private void SetListenUri(ServiceEndpoint endpoint, Binding binding, Uri listenUri)
		{
			endpoint.UnresolvedListenUri = listenUri;
			endpoint.ListenUri = ServiceHostBase.MakeAbsoluteUri(listenUri, binding, this.host.InternalBaseAddresses);
		}

		// Token: 0x04000A57 RID: 2647
		private ServiceHost host;
	}
}

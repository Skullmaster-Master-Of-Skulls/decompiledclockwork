using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200007C RID: 124
	internal class DiscoveryVersionApril2005Implementation : IDiscoveryVersionImplementation
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00010862 File Offset: 0x0000EA62
		public DiscoveryVersionApril2005Implementation()
		{
			this.contractLock = new object();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000F83C File Offset: 0x0000DA3C
		public string WsaNamespace
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2004/08/addressing";
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00010875 File Offset: 0x0000EA75
		public Uri DiscoveryAddress
		{
			get
			{
				if (this.discoveryAddress == null)
				{
					this.discoveryAddress = new Uri("urn:schemas-xmlsoap-org:ws:2005:04:discovery");
				}
				return this.discoveryAddress;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000F869 File Offset: 0x0000DA69
		public MessageVersion MessageVersion
		{
			get
			{
				return MessageVersion.Soap12WSAddressingAugust2004;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001089B File Offset: 0x0000EA9B
		public DiscoveryVersion.SchemaQualifiedNames QualifiedNames
		{
			get
			{
				if (this.qualifiedNames == null)
				{
					this.qualifiedNames = new DiscoveryVersion.SchemaQualifiedNames("http://schemas.xmlsoap.org/ws/2005/04/discovery", this.WsaNamespace);
				}
				return this.qualifiedNames;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000108C1 File Offset: 0x0000EAC1
		public DataContractSerializer EprSerializer
		{
			get
			{
				if (this.eprSerializer == null)
				{
					this.eprSerializer = new DataContractSerializer(typeof(EndpointAddressAugust2004));
				}
				return this.eprSerializer;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000108E8 File Offset: 0x0000EAE8
		public ContractDescription GetDiscoveryContract(ServiceDiscoveryMode discoveryMode)
		{
			if (discoveryMode == ServiceDiscoveryMode.Adhoc)
			{
				if (this.adhocDiscoveryContract == null)
				{
					object obj = this.contractLock;
					lock (obj)
					{
						if (this.adhocDiscoveryContract == null)
						{
							this.adhocDiscoveryContract = DiscoveryUtility.GetContract(typeof(IDiscoveryContractAdhocApril2005));
						}
					}
				}
				return this.adhocDiscoveryContract;
			}
			if (discoveryMode == ServiceDiscoveryMode.Managed)
			{
				if (this.managedDiscoveryContract == null)
				{
					object obj2 = this.contractLock;
					lock (obj2)
					{
						if (this.managedDiscoveryContract == null)
						{
							this.managedDiscoveryContract = DiscoveryUtility.GetContract(typeof(IDiscoveryContractManagedApril2005));
						}
					}
				}
				return this.managedDiscoveryContract;
			}
			throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryIncorrectMode(discoveryMode)));
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000109C4 File Offset: 0x0000EBC4
		public ContractDescription GetAnnouncementContract()
		{
			if (this.announcementContract == null)
			{
				object obj = this.contractLock;
				lock (obj)
				{
					if (this.announcementContract == null)
					{
						this.announcementContract = DiscoveryUtility.GetContract(typeof(IAnnouncementContractApril2005));
					}
				}
			}
			return this.announcementContract;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00010A2C File Offset: 0x0000EC2C
		public IDiscoveryInnerClient CreateDiscoveryInnerClient(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver)
		{
			if (discoveryEndpoint.DiscoveryMode == ServiceDiscoveryMode.Adhoc)
			{
				return new DiscoveryInnerClientApril2005<IDiscoveryContractAdhocApril2005>(discoveryEndpoint, responseReceiver);
			}
			if (discoveryEndpoint.DiscoveryMode == ServiceDiscoveryMode.Managed)
			{
				return new DiscoveryInnerClientApril2005<IDiscoveryContractManagedApril2005>(discoveryEndpoint, responseReceiver);
			}
			throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryIncorrectMode(discoveryEndpoint.DiscoveryMode)));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00010A79 File Offset: 0x0000EC79
		public IAnnouncementInnerClient CreateAnnouncementInnerClient(AnnouncementEndpoint announcementEndpoint)
		{
			return new AnnouncementInnerClientApril2005(announcementEndpoint);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00010A84 File Offset: 0x0000EC84
		public Uri ToVersionIndependentScopeMatchBy(Uri versionDependentScopeMatchBy)
		{
			Uri result = versionDependentScopeMatchBy;
			if (versionDependentScopeMatchBy == DiscoveryVersionApril2005Implementation.ScopeMatchByExact)
			{
				result = FindCriteria.ScopeMatchByExact;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionApril2005Implementation.ScopeMatchByPrefix)
			{
				result = FindCriteria.ScopeMatchByPrefix;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionApril2005Implementation.ScopeMatchByLdap)
			{
				result = FindCriteria.ScopeMatchByLdap;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionApril2005Implementation.ScopeMatchByUuid)
			{
				result = FindCriteria.ScopeMatchByUuid;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionApril2005Implementation.ScopeMatchByNone)
			{
				result = FindCriteria.ScopeMatchByNone;
			}
			return result;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public Uri ToVersionDependentScopeMatchBy(Uri versionIndependentScopeMatchBy)
		{
			Uri result = versionIndependentScopeMatchBy;
			if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByExact)
			{
				result = DiscoveryVersionApril2005Implementation.ScopeMatchByExact;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByPrefix)
			{
				result = DiscoveryVersionApril2005Implementation.ScopeMatchByPrefix;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByLdap)
			{
				result = DiscoveryVersionApril2005Implementation.ScopeMatchByLdap;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByUuid)
			{
				result = DiscoveryVersionApril2005Implementation.ScopeMatchByUuid;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByNone)
			{
				result = DiscoveryVersionApril2005Implementation.ScopeMatchByNone;
			}
			return result;
		}

		// Token: 0x04000165 RID: 357
		private static readonly Uri ScopeMatchByExact = new Uri("http://schemas.xmlsoap.org/ws/2005/04/discovery/strcmp0");

		// Token: 0x04000166 RID: 358
		private static readonly Uri ScopeMatchByLdap = new Uri("http://schemas.xmlsoap.org/ws/2005/04/discovery/ldap");

		// Token: 0x04000167 RID: 359
		private static readonly Uri ScopeMatchByPrefix = new Uri("http://schemas.xmlsoap.org/ws/2005/04/discovery/rfc2396");

		// Token: 0x04000168 RID: 360
		private static readonly Uri ScopeMatchByUuid = new Uri("http://schemas.xmlsoap.org/ws/2005/04/discovery/uuid");

		// Token: 0x04000169 RID: 361
		private static readonly Uri ScopeMatchByNone = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/none");

		// Token: 0x0400016A RID: 362
		private Uri discoveryAddress;

		// Token: 0x0400016B RID: 363
		private DataContractSerializer eprSerializer;

		// Token: 0x0400016C RID: 364
		private DiscoveryVersion.SchemaQualifiedNames qualifiedNames;

		// Token: 0x0400016D RID: 365
		private ContractDescription adhocDiscoveryContract;

		// Token: 0x0400016E RID: 366
		private ContractDescription managedDiscoveryContract;

		// Token: 0x0400016F RID: 367
		private ContractDescription announcementContract;

		// Token: 0x04000170 RID: 368
		private object contractLock;
	}
}

using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000095 RID: 149
	internal class DiscoveryVersion11Implementation : IDiscoveryVersionImplementation
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00011AFD File Offset: 0x0000FCFD
		public DiscoveryVersion11Implementation()
		{
			this.contractLock = new object();
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00011B10 File Offset: 0x0000FD10
		public string WsaNamespace
		{
			get
			{
				return "http://www.w3.org/2005/08/addressing";
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00011B17 File Offset: 0x0000FD17
		public Uri DiscoveryAddress
		{
			get
			{
				if (this.discoveryAddress == null)
				{
					this.discoveryAddress = new Uri("urn:docs-oasis-open-org:ws-dd:ns:discovery:2009:01");
				}
				return this.discoveryAddress;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00011B3D File Offset: 0x0000FD3D
		public MessageVersion MessageVersion
		{
			get
			{
				return MessageVersion.Soap12WSAddressing10;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00011B44 File Offset: 0x0000FD44
		public DiscoveryVersion.SchemaQualifiedNames QualifiedNames
		{
			get
			{
				if (this.qualifiedNames == null)
				{
					this.qualifiedNames = new DiscoveryVersion.SchemaQualifiedNames("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01", this.WsaNamespace);
				}
				return this.qualifiedNames;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00011B6A File Offset: 0x0000FD6A
		public DataContractSerializer EprSerializer
		{
			get
			{
				if (this.eprSerializer == null)
				{
					this.eprSerializer = new DataContractSerializer(typeof(EndpointAddress10));
				}
				return this.eprSerializer;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00011B90 File Offset: 0x0000FD90
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
							this.adhocDiscoveryContract = DiscoveryUtility.GetContract(typeof(IDiscoveryContractAdhoc11));
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
							this.managedDiscoveryContract = DiscoveryUtility.GetContract(typeof(IDiscoveryContractManaged11));
						}
					}
				}
				return this.managedDiscoveryContract;
			}
			throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryIncorrectMode(discoveryMode)));
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public ContractDescription GetAnnouncementContract()
		{
			if (this.announcementContract == null)
			{
				object obj = this.contractLock;
				lock (obj)
				{
					if (this.announcementContract == null)
					{
						this.announcementContract = DiscoveryUtility.GetContract(typeof(IAnnouncementContract11));
					}
				}
			}
			return this.announcementContract;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00011CD4 File Offset: 0x0000FED4
		public IDiscoveryInnerClient CreateDiscoveryInnerClient(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver)
		{
			if (discoveryEndpoint.DiscoveryMode == ServiceDiscoveryMode.Adhoc)
			{
				return new DiscoveryInnerClientAdhoc11(discoveryEndpoint, responseReceiver);
			}
			if (discoveryEndpoint.DiscoveryMode == ServiceDiscoveryMode.Managed)
			{
				return new DiscoveryInnerClientManaged11(discoveryEndpoint, responseReceiver);
			}
			throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryIncorrectMode(discoveryEndpoint.DiscoveryMode)));
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00011D21 File Offset: 0x0000FF21
		public IAnnouncementInnerClient CreateAnnouncementInnerClient(AnnouncementEndpoint announcementEndpoint)
		{
			return new AnnouncementInnerClient11(announcementEndpoint);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00011D2C File Offset: 0x0000FF2C
		public Uri ToVersionIndependentScopeMatchBy(Uri versionDependentScopeMatchBy)
		{
			Uri result = versionDependentScopeMatchBy;
			if (versionDependentScopeMatchBy == DiscoveryVersion11Implementation.ScopeMatchByExact)
			{
				result = FindCriteria.ScopeMatchByExact;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersion11Implementation.ScopeMatchByPrefix)
			{
				result = FindCriteria.ScopeMatchByPrefix;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersion11Implementation.ScopeMatchByLdap)
			{
				result = FindCriteria.ScopeMatchByLdap;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersion11Implementation.ScopeMatchByUuid)
			{
				result = FindCriteria.ScopeMatchByUuid;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersion11Implementation.ScopeMatchByNone)
			{
				result = FindCriteria.ScopeMatchByNone;
			}
			return result;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public Uri ToVersionDependentScopeMatchBy(Uri versionIndependentScopeMatchBy)
		{
			Uri result = versionIndependentScopeMatchBy;
			if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByExact)
			{
				result = DiscoveryVersion11Implementation.ScopeMatchByExact;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByPrefix)
			{
				result = DiscoveryVersion11Implementation.ScopeMatchByPrefix;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByLdap)
			{
				result = DiscoveryVersion11Implementation.ScopeMatchByLdap;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByUuid)
			{
				result = DiscoveryVersion11Implementation.ScopeMatchByUuid;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByNone)
			{
				result = DiscoveryVersion11Implementation.ScopeMatchByNone;
			}
			return result;
		}

		// Token: 0x0400018C RID: 396
		private static readonly Uri ScopeMatchByExact = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/strcmp0");

		// Token: 0x0400018D RID: 397
		private static readonly Uri ScopeMatchByLdap = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/ldap");

		// Token: 0x0400018E RID: 398
		private static readonly Uri ScopeMatchByPrefix = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/rfc3986");

		// Token: 0x0400018F RID: 399
		private static readonly Uri ScopeMatchByUuid = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/uuid");

		// Token: 0x04000190 RID: 400
		private static readonly Uri ScopeMatchByNone = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/none");

		// Token: 0x04000191 RID: 401
		private Uri discoveryAddress;

		// Token: 0x04000192 RID: 402
		private DataContractSerializer eprSerializer;

		// Token: 0x04000193 RID: 403
		private DiscoveryVersion.SchemaQualifiedNames qualifiedNames;

		// Token: 0x04000194 RID: 404
		private ContractDescription adhocDiscoveryContract;

		// Token: 0x04000195 RID: 405
		private ContractDescription managedDiscoveryContract;

		// Token: 0x04000196 RID: 406
		private ContractDescription announcementContract;

		// Token: 0x04000197 RID: 407
		private object contractLock;
	}
}

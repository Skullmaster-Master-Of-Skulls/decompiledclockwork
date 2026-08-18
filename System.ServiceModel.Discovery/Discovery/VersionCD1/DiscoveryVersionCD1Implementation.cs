using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000063 RID: 99
	internal class DiscoveryVersionCD1Implementation : IDiscoveryVersionImplementation
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0000F829 File Offset: 0x0000DA29
		public DiscoveryVersionCD1Implementation()
		{
			this.contractLock = new object();
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000F83C File Offset: 0x0000DA3C
		public string WsaNamespace
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2004/08/addressing";
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000F843 File Offset: 0x0000DA43
		public Uri DiscoveryAddress
		{
			get
			{
				if (this.discoveryAddress == null)
				{
					this.discoveryAddress = new Uri("urn:docs-oasis-open-org:ws-dd:discovery:2008:09");
				}
				return this.discoveryAddress;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000F869 File Offset: 0x0000DA69
		public MessageVersion MessageVersion
		{
			get
			{
				return MessageVersion.Soap12WSAddressingAugust2004;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000F870 File Offset: 0x0000DA70
		public DiscoveryVersion.SchemaQualifiedNames QualifiedNames
		{
			get
			{
				if (this.qualifiedNames == null)
				{
					this.qualifiedNames = new DiscoveryVersion.SchemaQualifiedNames("http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09", this.WsaNamespace);
				}
				return this.qualifiedNames;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000F896 File Offset: 0x0000DA96
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

		// Token: 0x0600051F RID: 1311 RVA: 0x0000F8BC File Offset: 0x0000DABC
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
							this.adhocDiscoveryContract = DiscoveryUtility.GetContract(typeof(IDiscoveryContractAdhocCD1));
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
							this.managedDiscoveryContract = DiscoveryUtility.GetContract(typeof(IDiscoveryContractManagedCD1));
						}
					}
				}
				return this.managedDiscoveryContract;
			}
			throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryIncorrectMode(discoveryMode)));
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000F998 File Offset: 0x0000DB98
		public ContractDescription GetAnnouncementContract()
		{
			if (this.announcementContract == null)
			{
				object obj = this.contractLock;
				lock (obj)
				{
					if (this.announcementContract == null)
					{
						this.announcementContract = DiscoveryUtility.GetContract(typeof(IAnnouncementContractCD1));
					}
				}
			}
			return this.announcementContract;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public IDiscoveryInnerClient CreateDiscoveryInnerClient(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver)
		{
			if (discoveryEndpoint.DiscoveryMode == ServiceDiscoveryMode.Adhoc)
			{
				return new DiscoveryInnerClientAdhocCD1(discoveryEndpoint, responseReceiver);
			}
			if (discoveryEndpoint.DiscoveryMode == ServiceDiscoveryMode.Managed)
			{
				return new DiscoveryInnerClientManagedCD1(discoveryEndpoint, responseReceiver);
			}
			throw FxTrace.Exception.AsError(new ArgumentException(SR.DiscoveryIncorrectMode(discoveryEndpoint.DiscoveryMode)));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000FA4D File Offset: 0x0000DC4D
		public IAnnouncementInnerClient CreateAnnouncementInnerClient(AnnouncementEndpoint announcementEndpoint)
		{
			return new AnnouncementInnerClientCD1(announcementEndpoint);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000FA58 File Offset: 0x0000DC58
		public Uri ToVersionIndependentScopeMatchBy(Uri versionDependentScopeMatchBy)
		{
			Uri result = versionDependentScopeMatchBy;
			if (versionDependentScopeMatchBy == DiscoveryVersionCD1Implementation.ScopeMatchByExact)
			{
				result = FindCriteria.ScopeMatchByExact;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionCD1Implementation.ScopeMatchByPrefix)
			{
				result = FindCriteria.ScopeMatchByPrefix;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionCD1Implementation.ScopeMatchByLdap)
			{
				result = FindCriteria.ScopeMatchByLdap;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionCD1Implementation.ScopeMatchByUuid)
			{
				result = FindCriteria.ScopeMatchByUuid;
			}
			else if (versionDependentScopeMatchBy == DiscoveryVersionCD1Implementation.ScopeMatchByNone)
			{
				result = FindCriteria.ScopeMatchByNone;
			}
			return result;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public Uri ToVersionDependentScopeMatchBy(Uri versionIndependentScopeMatchBy)
		{
			Uri result = versionIndependentScopeMatchBy;
			if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByExact)
			{
				result = DiscoveryVersionCD1Implementation.ScopeMatchByExact;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByPrefix)
			{
				result = DiscoveryVersionCD1Implementation.ScopeMatchByPrefix;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByLdap)
			{
				result = DiscoveryVersionCD1Implementation.ScopeMatchByLdap;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByUuid)
			{
				result = DiscoveryVersionCD1Implementation.ScopeMatchByUuid;
			}
			else if (versionIndependentScopeMatchBy == FindCriteria.ScopeMatchByNone)
			{
				result = DiscoveryVersionCD1Implementation.ScopeMatchByNone;
			}
			return result;
		}

		// Token: 0x0400013F RID: 319
		private static readonly Uri ScopeMatchByExact = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/strcmp0");

		// Token: 0x04000140 RID: 320
		private static readonly Uri ScopeMatchByLdap = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/ldap");

		// Token: 0x04000141 RID: 321
		private static readonly Uri ScopeMatchByPrefix = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/rfc3986");

		// Token: 0x04000142 RID: 322
		private static readonly Uri ScopeMatchByUuid = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09/uuid");

		// Token: 0x04000143 RID: 323
		private static readonly Uri ScopeMatchByNone = new Uri("http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01/none");

		// Token: 0x04000144 RID: 324
		private Uri discoveryAddress;

		// Token: 0x04000145 RID: 325
		private DataContractSerializer eprSerializer;

		// Token: 0x04000146 RID: 326
		private DiscoveryVersion.SchemaQualifiedNames qualifiedNames;

		// Token: 0x04000147 RID: 327
		private ContractDescription adhocDiscoveryContract;

		// Token: 0x04000148 RID: 328
		private ContractDescription managedDiscoveryContract;

		// Token: 0x04000149 RID: 329
		private ContractDescription announcementContract;

		// Token: 0x0400014A RID: 330
		private object contractLock;
	}
}

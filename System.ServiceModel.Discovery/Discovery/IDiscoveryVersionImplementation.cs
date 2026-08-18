using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200003B RID: 59
	internal interface IDiscoveryVersionImplementation
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002EB RID: 747
		string WsaNamespace { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002EC RID: 748
		Uri DiscoveryAddress { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002ED RID: 749
		MessageVersion MessageVersion { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002EE RID: 750
		DiscoveryVersion.SchemaQualifiedNames QualifiedNames { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002EF RID: 751
		DataContractSerializer EprSerializer { get; }

		// Token: 0x060002F0 RID: 752
		ContractDescription GetDiscoveryContract(ServiceDiscoveryMode discoveryMode);

		// Token: 0x060002F1 RID: 753
		ContractDescription GetAnnouncementContract();

		// Token: 0x060002F2 RID: 754
		IDiscoveryInnerClient CreateDiscoveryInnerClient(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver);

		// Token: 0x060002F3 RID: 755
		IAnnouncementInnerClient CreateAnnouncementInnerClient(AnnouncementEndpoint announcementEndpoint);

		// Token: 0x060002F4 RID: 756
		Uri ToVersionIndependentScopeMatchBy(Uri versionDependentScopeMatchBy);

		// Token: 0x060002F5 RID: 757
		Uri ToVersionDependentScopeMatchBy(Uri versionIndependentScopeMatchBy);
	}
}

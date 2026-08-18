using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage
{
	// Token: 0x020008B2 RID: 2226
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetContainerSasUriResp
	{
		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000154F9 File Offset: 0x000136F9
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x00015501 File Offset: 0x00013701
		[DataMember]
		public Uri ContainerSasUri { get; set; }
	}
}

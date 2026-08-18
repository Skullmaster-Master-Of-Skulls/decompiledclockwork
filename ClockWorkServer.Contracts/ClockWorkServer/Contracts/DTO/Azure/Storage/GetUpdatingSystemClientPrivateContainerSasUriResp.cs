using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage
{
	// Token: 0x020008B6 RID: 2230
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUpdatingSystemClientPrivateContainerSasUriResp
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x00015586 File Offset: 0x00013786
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x0001558E File Offset: 0x0001378E
		[DataMember]
		public Uri PrivateContainerSasUri { get; set; }

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x00015597 File Offset: 0x00013797
		// (set) Token: 0x06002D1F RID: 11551 RVA: 0x0001559F File Offset: 0x0001379F
		[DataMember]
		public Uri LogsBlobSasUri { get; set; }
	}
}

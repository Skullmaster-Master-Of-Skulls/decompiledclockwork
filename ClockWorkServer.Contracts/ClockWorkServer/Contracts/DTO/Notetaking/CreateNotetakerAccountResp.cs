using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200043F RID: 1087
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNotetakerAccountResp
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0000AD3A File Offset: 0x00008F3A
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x0000AD42 File Offset: 0x00008F42
		[DataMember]
		public int ServiceProviderId { get; set; }
	}
}

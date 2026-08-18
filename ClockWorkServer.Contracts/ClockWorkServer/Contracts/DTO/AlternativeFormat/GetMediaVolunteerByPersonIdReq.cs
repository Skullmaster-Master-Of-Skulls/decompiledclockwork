using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF7 RID: 3063
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteerByPersonIdReq : BaseMessageReq
	{
		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x0001FABA File Offset: 0x0001DCBA
		// (set) Token: 0x0600408A RID: 16522 RVA: 0x0001FAC2 File Offset: 0x0001DCC2
		[DataMember]
		public int PersonId { get; set; }
	}
}

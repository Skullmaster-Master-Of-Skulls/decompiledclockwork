using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cards.CardInfos
{
	// Token: 0x020008A9 RID: 2217
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(CardInfoVetsApplicationsStudentDTO))]
	public class CardInfoDTO
	{
		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000153CA File Offset: 0x000135CA
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x000153D2 File Offset: 0x000135D2
		[DataMember]
		public CardLayoutDTO Layout { get; set; }
	}
}

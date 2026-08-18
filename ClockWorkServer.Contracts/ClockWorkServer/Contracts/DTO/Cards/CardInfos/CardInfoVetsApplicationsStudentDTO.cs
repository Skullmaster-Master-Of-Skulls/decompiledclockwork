using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cards.CardInfos
{
	// Token: 0x020008AA RID: 2218
	[DataContract(Namespace = "http://tpro.ca")]
	public class CardInfoVetsApplicationsStudentDTO : CardInfoDTO
	{
		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000153DB File Offset: 0x000135DB
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000153E3 File Offset: 0x000135E3
		[DataMember]
		public VetsStudentCardInfoDTO CardInfo { get; set; }
	}
}

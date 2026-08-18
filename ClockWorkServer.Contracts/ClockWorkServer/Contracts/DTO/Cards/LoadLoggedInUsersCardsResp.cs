using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cards.CardInfos;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cards
{
	// Token: 0x020008A7 RID: 2215
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLoggedInUsersCardsResp
	{
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000153A8 File Offset: 0x000135A8
		// (set) Token: 0x06002CDC RID: 11484 RVA: 0x000153B0 File Offset: 0x000135B0
		[DataMember]
		public CardInfoDTO[] CardInfos { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Cards;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cards
{
	// Token: 0x020008A8 RID: 2216
	[DataContract(Namespace = "http://tpro.ca")]
	public class CardLayoutDTO
	{
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000153B9 File Offset: 0x000135B9
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x000153C1 File Offset: 0x000135C1
		[DataMember]
		public eCardType CardType { get; set; }
	}
}

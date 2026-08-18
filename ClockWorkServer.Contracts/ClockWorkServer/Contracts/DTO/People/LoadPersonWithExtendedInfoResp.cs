using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200039D RID: 925
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonWithExtendedInfoResp
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00009C3E File Offset: 0x00007E3E
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x00009C46 File Offset: 0x00007E46
		[DataMember]
		public PersonBaseWithExtendedInfoDTO PersonWithExtendedInfo { get; set; }
	}
}

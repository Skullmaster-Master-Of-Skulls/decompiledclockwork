using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200019C RID: 412
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTutorByPersonIdReq : BaseMessageReq
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0000452C File Offset: 0x0000272C
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x00004534 File Offset: 0x00002734
		[DataMember]
		public int PersonId { get; set; }
	}
}

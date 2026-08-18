using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200019A RID: 410
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTutorWithActiveStatusByIdReq : BaseMessageReq
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0000450A File Offset: 0x0000270A
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x00004512 File Offset: 0x00002712
		[DataMember]
		public int TutorPersonId { get; set; }
	}
}

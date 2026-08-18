using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200019B RID: 411
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTutorWithActiveStatusByIdResp
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0000451B File Offset: 0x0000271B
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x00004523 File Offset: 0x00002723
		[DataMember]
		public TutorWithActiveStatusDTO TutorWithStatus { get; set; }
	}
}

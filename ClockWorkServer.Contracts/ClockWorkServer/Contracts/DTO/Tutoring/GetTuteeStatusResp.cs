using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200018F RID: 399
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTuteeStatusResp
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0000433F File Offset: 0x0000253F
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00004347 File Offset: 0x00002547
		[DataMember]
		public eTuteeStatus Status { get; set; }
	}
}

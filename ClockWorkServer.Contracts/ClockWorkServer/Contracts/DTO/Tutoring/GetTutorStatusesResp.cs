using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000199 RID: 409
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTutorStatusesResp
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x000044F9 File Offset: 0x000026F9
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00004501 File Offset: 0x00002701
		[DataMember]
		public IDictionary<int, eTutorStatus> TutorsWithStatus { get; set; }
	}
}

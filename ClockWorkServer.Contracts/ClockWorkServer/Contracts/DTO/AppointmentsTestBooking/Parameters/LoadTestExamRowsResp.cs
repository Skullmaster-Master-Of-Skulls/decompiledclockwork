using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A80 RID: 2688
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestExamRowsResp
	{
		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x0001B52A File Offset: 0x0001972A
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x0001B532 File Offset: 0x00019732
		[DataMember]
		public IList<TestExamRowDTO> TestExamRows { get; set; }
	}
}

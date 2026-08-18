using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A2A RID: 2602
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllExamStatusesResp
	{
		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x0001A100 File Offset: 0x00018300
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x0001A108 File Offset: 0x00018308
		[DataMember]
		public IList<ExamStatusDTO> ExamStatuses { get; set; }
	}
}

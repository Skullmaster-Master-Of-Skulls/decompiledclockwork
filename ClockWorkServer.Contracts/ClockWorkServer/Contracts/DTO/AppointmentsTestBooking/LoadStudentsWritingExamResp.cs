using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A3E RID: 2622
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsWritingExamResp
	{
		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x0001A386 File Offset: 0x00018586
		// (set) Token: 0x0600361C RID: 13852 RVA: 0x0001A38E File Offset: 0x0001858E
		[DataMember]
		public List<StudentWritingTestDTO> StudentsWritingTests { get; set; }
	}
}

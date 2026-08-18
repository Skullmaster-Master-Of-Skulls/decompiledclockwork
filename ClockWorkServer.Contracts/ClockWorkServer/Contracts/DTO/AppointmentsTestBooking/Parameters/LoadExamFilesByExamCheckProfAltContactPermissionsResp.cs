using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A52 RID: 2642
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFilesByExamCheckProfAltContactPermissionsResp
	{
		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x0001AFEB File Offset: 0x000191EB
		// (set) Token: 0x06003782 RID: 14210 RVA: 0x0001AFF3 File Offset: 0x000191F3
		[DataMember]
		public IList<ExamFileDTO> ExamFiles { get; set; }
	}
}

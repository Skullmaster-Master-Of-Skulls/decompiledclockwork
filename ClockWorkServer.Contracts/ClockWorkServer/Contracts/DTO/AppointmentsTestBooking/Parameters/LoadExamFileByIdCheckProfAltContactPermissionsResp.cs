using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A54 RID: 2644
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFileByIdCheckProfAltContactPermissionsResp
	{
		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x0600378D RID: 14221 RVA: 0x0001B040 File Offset: 0x00019240
		// (set) Token: 0x0600378E RID: 14222 RVA: 0x0001B048 File Offset: 0x00019248
		[DataMember]
		public ExamFileDTO ExamFile { get; set; }
	}
}

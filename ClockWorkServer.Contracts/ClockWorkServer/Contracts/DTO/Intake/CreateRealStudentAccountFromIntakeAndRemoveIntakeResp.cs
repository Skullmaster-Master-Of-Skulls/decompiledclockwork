using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E3 RID: 1507
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRealStudentAccountFromIntakeAndRemoveIntakeResp
	{
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0000DF6E File Offset: 0x0000C16E
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x0000DF76 File Offset: 0x0000C176
		[DataMember]
		public CreateRealStudentAccountFromIntakeResultDTO CreateStudentResult { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E2 RID: 1506
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRealStudentAccountFromIntakeAndRemoveIntakeReq : BaseMessageReq
	{
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0000DF4C File Offset: 0x0000C14C
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x0000DF54 File Offset: 0x0000C154
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0000DF5D File Offset: 0x0000C15D
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x0000DF65 File Offset: 0x0000C165
		[DataMember]
		public int[] GroupIds { get; set; }
	}
}

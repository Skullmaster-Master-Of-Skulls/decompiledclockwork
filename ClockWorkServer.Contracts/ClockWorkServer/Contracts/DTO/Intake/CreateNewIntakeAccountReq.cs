using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005D0 RID: 1488
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewIntakeAccountReq : BaseMessageReq
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0000DE4D File Offset: 0x0000C04D
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x0000DE55 File Offset: 0x0000C055
		[DataMember]
		public IntakeUserAccountDTO UserAccount { get; set; }
	}
}

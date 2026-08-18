using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005D8 RID: 1496
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateActiveIntakeStatusAndNoteReq : BaseMessageReq
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x0000DEB3 File Offset: 0x0000C0B3
		// (set) Token: 0x06001E9A RID: 7834 RVA: 0x0000DEBB File Offset: 0x0000C0BB
		[DataMember]
		public int[] IntakePersonIds { get; set; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0000DECC File Offset: 0x0000C0CC
		[DataMember]
		public string NewNote { get; set; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0000DED5 File Offset: 0x0000C0D5
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x0000DEDD File Offset: 0x0000C0DD
		[DataMember]
		public Guid NewIntakeStatusId { get; set; }
	}
}

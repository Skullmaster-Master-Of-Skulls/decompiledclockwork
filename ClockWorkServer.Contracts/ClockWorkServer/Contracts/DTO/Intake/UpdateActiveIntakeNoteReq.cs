using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005DC RID: 1500
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateActiveIntakeNoteReq : BaseMessageReq
	{
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0000DF08 File Offset: 0x0000C108
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x0000DF10 File Offset: 0x0000C110
		[DataMember]
		public int[] IntakePersonIds { get; set; }

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0000DF19 File Offset: 0x0000C119
		// (set) Token: 0x06001EAA RID: 7850 RVA: 0x0000DF21 File Offset: 0x0000C121
		[DataMember]
		public string NewNote { get; set; }
	}
}

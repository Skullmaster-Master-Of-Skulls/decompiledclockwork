using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004CD RID: 1229
	public class UpdateRequestDetailNotesAndSpecialInstructionsReq : BaseMessageReq
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0000C00F File Offset: 0x0000A20F
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x0000C017 File Offset: 0x0000A217
		[DataMember]
		public LegacyRequestDetailNotesAndSpecialInstructionsDTO NotesAndSpecialInstructions { get; set; }
	}
}

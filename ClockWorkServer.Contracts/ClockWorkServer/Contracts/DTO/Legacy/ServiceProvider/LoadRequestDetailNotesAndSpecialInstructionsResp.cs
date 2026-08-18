using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004CC RID: 1228
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestDetailNotesAndSpecialInstructionsResp
	{
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0000BFFE File Offset: 0x0000A1FE
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0000C006 File Offset: 0x0000A206
		[DataMember]
		public LegacyRequestDetailNotesAndSpecialInstructionsDTO DetailNotesAndSpecialInstructions { get; set; }
	}
}

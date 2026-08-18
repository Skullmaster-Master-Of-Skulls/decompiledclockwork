using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2
{
	// Token: 0x020008D9 RID: 2265
	[DataContract(Namespace = "http://tpro.ca")]
	public class Availability2NoteDTO
	{
		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x00015B50 File Offset: 0x00013D50
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x00015B58 File Offset: 0x00013D58
		[DataMember]
		public int? ColourArgB { get; set; }

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x00015B61 File Offset: 0x00013D61
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x00015B69 File Offset: 0x00013D69
		[DataMember]
		public string Text { get; set; }
	}
}

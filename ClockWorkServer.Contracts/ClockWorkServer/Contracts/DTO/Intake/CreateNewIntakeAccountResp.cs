using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005D1 RID: 1489
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewIntakeAccountResp
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0000DE5E File Offset: 0x0000C05E
		// (set) Token: 0x06001E89 RID: 7817 RVA: 0x0000DE66 File Offset: 0x0000C066
		[DataMember]
		public int NewIntakePersonId { get; set; }
	}
}

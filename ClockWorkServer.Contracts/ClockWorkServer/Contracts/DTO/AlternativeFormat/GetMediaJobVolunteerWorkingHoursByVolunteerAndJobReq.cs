using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C07 RID: 3079
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobVolunteerWorkingHoursByVolunteerAndJobReq : BaseMessageReq
	{
		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x0001FC0E File Offset: 0x0001DE0E
		// (set) Token: 0x060040C2 RID: 16578 RVA: 0x0001FC16 File Offset: 0x0001DE16
		[DataMember]
		public int VolunteerId { get; set; }

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x0001FC1F File Offset: 0x0001DE1F
		// (set) Token: 0x060040C4 RID: 16580 RVA: 0x0001FC27 File Offset: 0x0001DE27
		[DataMember]
		public int MediaJobId { get; set; }
	}
}

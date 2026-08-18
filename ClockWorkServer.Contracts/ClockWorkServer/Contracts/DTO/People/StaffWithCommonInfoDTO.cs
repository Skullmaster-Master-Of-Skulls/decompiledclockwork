using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B6 RID: 950
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(ProctorDTO))]
	[KnownType(typeof(AlternateFormatVolunteerDTO))]
	public class StaffWithCommonInfoDTO
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00009E84 File Offset: 0x00008084
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x00009E8C File Offset: 0x0000808C
		[DataMember]
		public PersonBaseDTO Staff { get; set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00009E95 File Offset: 0x00008095
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x00009E9D File Offset: 0x0000809D
		[DataMember]
		public StaffCommonInfoDTO StaffCommonInfo { get; set; }
	}
}

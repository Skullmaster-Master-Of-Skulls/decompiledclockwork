using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2
{
	// Token: 0x02000A8D RID: 2701
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookAccommodationToUseDTO
	{
		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x0001B80F File Offset: 0x00019A0F
		// (set) Token: 0x060038AD RID: 14509 RVA: 0x0001B817 File Offset: 0x00019A17
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x0001B820 File Offset: 0x00019A20
		// (set) Token: 0x060038AF RID: 14511 RVA: 0x0001B828 File Offset: 0x00019A28
		[DataMember]
		public string Caption { get; set; }

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x0001B831 File Offset: 0x00019A31
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x0001B839 File Offset: 0x00019A39
		[DataMember]
		public string Value { get; set; }
	}
}

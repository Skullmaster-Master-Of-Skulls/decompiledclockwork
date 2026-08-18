using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Veteran
{
	// Token: 0x02000120 RID: 288
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeInBenefitRequestDTO
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0000338D File Offset: 0x0000158D
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x00003395 File Offset: 0x00001595
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0000339E File Offset: 0x0000159E
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x000033A6 File Offset: 0x000015A6
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x000033AF File Offset: 0x000015AF
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x000033B7 File Offset: 0x000015B7
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x000033C0 File Offset: 0x000015C0
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x000033C8 File Offset: 0x000015C8
		[DataMember]
		public eVeteranRequestStatus Status { get; set; }
	}
}

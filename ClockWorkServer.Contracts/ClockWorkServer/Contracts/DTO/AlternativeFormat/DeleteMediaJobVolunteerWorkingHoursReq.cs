using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C0F RID: 3087
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteMediaJobVolunteerWorkingHoursReq : BaseMessageReq
	{
		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x0001FC96 File Offset: 0x0001DE96
		// (set) Token: 0x060040DA RID: 16602 RVA: 0x0001FC9E File Offset: 0x0001DE9E
		[DataMember]
		public int JobVolunteerWorkingHoursInfoId { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BEF RID: 3055
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaJobVolunteerReq : BaseMessageReq
	{
		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x0001FA54 File Offset: 0x0001DC54
		// (set) Token: 0x06004076 RID: 16502 RVA: 0x0001FA5C File Offset: 0x0001DC5C
		[DataMember]
		public AlternateFormatVolunteerDTO Volunteer { get; set; }
	}
}

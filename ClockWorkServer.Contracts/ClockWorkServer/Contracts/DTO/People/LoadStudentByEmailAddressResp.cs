using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003BB RID: 955
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentByEmailAddressResp
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00009F83 File Offset: 0x00008183
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x00009F8B File Offset: 0x0000818B
		[DataMember]
		public PersonBaseDTO Student { get; set; }
	}
}

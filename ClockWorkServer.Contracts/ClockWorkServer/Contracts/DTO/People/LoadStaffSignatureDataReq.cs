using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003AB RID: 939
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStaffSignatureDataReq : BaseMessageReq
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00009DA7 File Offset: 0x00007FA7
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x00009DAF File Offset: 0x00007FAF
		[DataMember]
		public int StaffPersonId { get; set; }
	}
}

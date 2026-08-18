using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A8 RID: 936
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStaffStoredSignatureReq : BaseMessageReq
	{
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00009D74 File Offset: 0x00007F74
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x00009D7C File Offset: 0x00007F7C
		[DataMember]
		public int StaffPersonId { get; set; }
	}
}

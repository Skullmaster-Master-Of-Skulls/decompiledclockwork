using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B4 RID: 1972
	[DataContract(Namespace = "http://tpro.ca")]
	public class GoToTodaysSessionReq : BaseMessageReq
	{
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x0001338E File Offset: 0x0001158E
		// (set) Token: 0x06002884 RID: 10372 RVA: 0x00013396 File Offset: 0x00011596
		[DataMember]
		public SessionDTO Session { get; set; }
	}
}

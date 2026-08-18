using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007D7 RID: 2007
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSessionByDateReq : BaseMessageReq
	{
		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x00013636 File Offset: 0x00011836
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x0001363E File Offset: 0x0001183E
		[DataMember]
		public DateTime Date { get; set; }
	}
}

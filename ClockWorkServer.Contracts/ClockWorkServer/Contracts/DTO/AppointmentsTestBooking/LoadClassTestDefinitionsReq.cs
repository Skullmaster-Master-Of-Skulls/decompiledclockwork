using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009EF RID: 2543
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionsReq : BaseMessageReq
	{
		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x00019C7C File Offset: 0x00017E7C
		// (set) Token: 0x060034F9 RID: 13561 RVA: 0x00019C84 File Offset: 0x00017E84
		[DataMember]
		public int LuCourseId { get; set; }
	}
}

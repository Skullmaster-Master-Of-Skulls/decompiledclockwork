using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E0 RID: 2528
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTestCourseResp
	{
		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x00019B06 File Offset: 0x00017D06
		// (set) Token: 0x060034BE RID: 13502 RVA: 0x00019B0E File Offset: 0x00017D0E
		[DataMember]
		public LookupCourseDTO Course { get; set; }
	}
}

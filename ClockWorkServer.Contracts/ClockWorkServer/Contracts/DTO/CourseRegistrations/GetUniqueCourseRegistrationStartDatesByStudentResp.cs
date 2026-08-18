using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000823 RID: 2083
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUniqueCourseRegistrationStartDatesByStudentResp
	{
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000142A4 File Offset: 0x000124A4
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x000142AC File Offset: 0x000124AC
		[DataMember]
		public IList<DateTime> CourseStartDates { get; set; }
	}
}

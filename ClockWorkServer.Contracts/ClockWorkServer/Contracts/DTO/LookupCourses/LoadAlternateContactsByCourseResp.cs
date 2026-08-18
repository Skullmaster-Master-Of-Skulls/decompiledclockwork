using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000798 RID: 1944
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactsByCourseResp
	{
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x00012CFC File Offset: 0x00010EFC
		// (set) Token: 0x060027F6 RID: 10230 RVA: 0x00012D04 File Offset: 0x00010F04
		[DataMember]
		public IList<AlternateContactDTO> AltContacts { get; set; }
	}
}

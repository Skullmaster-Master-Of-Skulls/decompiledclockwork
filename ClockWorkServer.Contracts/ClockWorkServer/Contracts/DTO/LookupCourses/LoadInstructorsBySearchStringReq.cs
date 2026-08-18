using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E2 RID: 2018
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorsBySearchStringReq : BaseMessageReq
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x00013978 File Offset: 0x00011B78
		// (set) Token: 0x0600293E RID: 10558 RVA: 0x00013980 File Offset: 0x00011B80
		[DataMember]
		public string SearchString { get; set; }
	}
}

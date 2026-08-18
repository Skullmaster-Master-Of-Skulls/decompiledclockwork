using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007BE RID: 1982
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAcademicTermReq : BaseMessageReq
	{
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x00013449 File Offset: 0x00011649
		// (set) Token: 0x060028A4 RID: 10404 RVA: 0x00013451 File Offset: 0x00011651
		[DataMember]
		public DateTime Date { get; set; }
	}
}

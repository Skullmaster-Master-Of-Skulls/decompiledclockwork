using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200078D RID: 1933
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactByEmployeeIdReq : BaseMessageReq
	{
		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00012C1F File Offset: 0x00010E1F
		// (set) Token: 0x060027D1 RID: 10193 RVA: 0x00012C27 File Offset: 0x00010E27
		[DataMember]
		public string EmployeeId { get; set; }
	}
}

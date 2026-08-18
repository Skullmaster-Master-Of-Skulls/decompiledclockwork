using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200079B RID: 1947
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAlternateContactReq : BaseMessageReq
	{
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x00012D2F File Offset: 0x00010F2F
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x00012D37 File Offset: 0x00010F37
		[DataMember]
		public AlternateContactDTO AltContact { get; set; }
	}
}

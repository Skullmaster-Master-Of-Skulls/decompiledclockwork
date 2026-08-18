using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000793 RID: 1939
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAlternateContactReq : BaseMessageReq
	{
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x00012CA7 File Offset: 0x00010EA7
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x00012CAF File Offset: 0x00010EAF
		[DataMember]
		public AlternateContactDTO AltContact { get; set; }
	}
}

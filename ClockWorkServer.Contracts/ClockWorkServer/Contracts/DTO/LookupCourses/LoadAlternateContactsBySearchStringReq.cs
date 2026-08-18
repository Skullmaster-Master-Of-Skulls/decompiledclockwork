using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000799 RID: 1945
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactsBySearchStringReq : BaseMessageReq
	{
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x00012D0D File Offset: 0x00010F0D
		// (set) Token: 0x060027F9 RID: 10233 RVA: 0x00012D15 File Offset: 0x00010F15
		[DataMember]
		public string SearchString { get; set; }
	}
}

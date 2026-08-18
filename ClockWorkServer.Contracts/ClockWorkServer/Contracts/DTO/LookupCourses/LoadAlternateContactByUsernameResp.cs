using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000790 RID: 1936
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactByUsernameResp
	{
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x00012C52 File Offset: 0x00010E52
		// (set) Token: 0x060027DA RID: 10202 RVA: 0x00012C5A File Offset: 0x00010E5A
		[DataMember]
		public AlternateContactDTO AlternateContact { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000377 RID: 887
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonByStudentNumberReq : BaseMessageReq
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00009973 File Offset: 0x00007B73
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x0000997B File Offset: 0x00007B7B
		[DataMember]
		public string Student_no { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00009984 File Offset: 0x00007B84
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x0000998C File Offset: 0x00007B8C
		[DataMember]
		public bool CheckIfWhoAmIIsAllowedToSeeThisStudent { get; set; }
	}
}

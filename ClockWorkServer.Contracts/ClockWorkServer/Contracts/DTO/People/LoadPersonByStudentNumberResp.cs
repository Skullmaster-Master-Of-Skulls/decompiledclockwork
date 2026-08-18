using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000378 RID: 888
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonByStudentNumberResp
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00009995 File Offset: 0x00007B95
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x0000999D File Offset: 0x00007B9D
		[DataMember]
		public PersonBaseDTO Person { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x000099A6 File Offset: 0x00007BA6
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x000099AE File Offset: 0x00007BAE
		[DataMember]
		public bool WhoAmIIsAllowedToSeeThisStudent { get; set; }
	}
}

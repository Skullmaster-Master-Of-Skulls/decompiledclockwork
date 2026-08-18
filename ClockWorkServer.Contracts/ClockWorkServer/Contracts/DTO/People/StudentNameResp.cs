using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000376 RID: 886
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentNameResp
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00009962 File Offset: 0x00007B62
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x0000996A File Offset: 0x00007B6A
		[DataMember]
		public string StudentName { get; set; }
	}
}

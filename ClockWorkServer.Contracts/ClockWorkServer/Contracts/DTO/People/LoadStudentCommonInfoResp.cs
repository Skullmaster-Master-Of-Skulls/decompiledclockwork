using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B8 RID: 952
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentCommonInfoResp
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00009F50 File Offset: 0x00008150
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x00009F58 File Offset: 0x00008158
		[DataMember]
		public StudentCommonInfoDTO Info { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B6 RID: 1974
	[DataContract(Namespace = "http://tpro.ca")]
	public class GotoSessionReq : BaseMessageReq
	{
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x000133B0 File Offset: 0x000115B0
		// (set) Token: 0x0600288A RID: 10378 RVA: 0x000133B8 File Offset: 0x000115B8
		[DataMember]
		public SessionDTO Session { get; set; }

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x000133C1 File Offset: 0x000115C1
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x000133C9 File Offset: 0x000115C9
		[DataMember]
		public AcademicTermDTO AcademicTerm { get; set; }

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000133D2 File Offset: 0x000115D2
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x000133DA File Offset: 0x000115DA
		[DataMember]
		public int Year { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B2 RID: 1970
	[DataContract(Namespace = "http://tpro.ca")]
	public class SubtractSessionReq : BaseMessageReq
	{
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x0001335B File Offset: 0x0001155B
		// (set) Token: 0x0600287C RID: 10364 RVA: 0x00013363 File Offset: 0x00011563
		[DataMember]
		public SessionDTO Session { get; set; }

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0001336C File Offset: 0x0001156C
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x00013374 File Offset: 0x00011574
		[DataMember]
		public int Count { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A7 RID: 1959
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseBasesBySearchStringReq : BaseMessageReq
	{
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x0001323A File Offset: 0x0001143A
		// (set) Token: 0x0600284F RID: 10319 RVA: 0x00013242 File Offset: 0x00011442
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x0001324B File Offset: 0x0001144B
		// (set) Token: 0x06002851 RID: 10321 RVA: 0x00013253 File Offset: 0x00011453
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x0001325C File Offset: 0x0001145C
		// (set) Token: 0x06002853 RID: 10323 RVA: 0x00013264 File Offset: 0x00011464
		[DataMember]
		public string SearchString { get; set; }
	}
}

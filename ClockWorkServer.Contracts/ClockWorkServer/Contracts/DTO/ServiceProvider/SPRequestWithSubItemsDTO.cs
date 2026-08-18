using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x0200027C RID: 636
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestWithSubItemsDTO
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x000072E2 File Offset: 0x000054E2
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x000072EA File Offset: 0x000054EA
		[DataMember]
		public SPRequestDTO Request { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x000072F3 File Offset: 0x000054F3
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x000072FB File Offset: 0x000054FB
		[DataMember]
		public IList<SPRequestCourseDTO> Courses { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00007304 File Offset: 0x00005504
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0000730C File Offset: 0x0000550C
		[DataMember]
		public IList<SPRequestEventDTO> Events { get; set; }
	}
}

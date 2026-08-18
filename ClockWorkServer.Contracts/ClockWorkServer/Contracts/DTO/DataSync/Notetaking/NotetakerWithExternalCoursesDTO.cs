using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking
{
	// Token: 0x0200072B RID: 1835
	[DataContract(Namespace = "http://tpro.ca")]
	public class NotetakerWithExternalCoursesDTO
	{
		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000113B9 File Offset: 0x0000F5B9
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x000113C1 File Offset: 0x0000F5C1
		[DataMember]
		public SPProviderDTO Notetaker { get; set; }

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000113CA File Offset: 0x0000F5CA
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x000113D2 File Offset: 0x0000F5D2
		[DataMember]
		public IList<DataSyncExternalCourseDTO> ExternalCourses { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters
{
	// Token: 0x02000816 RID: 2070
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupInstructorsForManagementReq : BaseMessageReq
	{
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x000140C8 File Offset: 0x000122C8
		// (set) Token: 0x06002A36 RID: 10806 RVA: 0x000140D0 File Offset: 0x000122D0
		[DataMember]
		public int StartIndex { get; set; }

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000140D9 File Offset: 0x000122D9
		// (set) Token: 0x06002A38 RID: 10808 RVA: 0x000140E1 File Offset: 0x000122E1
		[DataMember]
		public int Count { get; set; }
	}
}

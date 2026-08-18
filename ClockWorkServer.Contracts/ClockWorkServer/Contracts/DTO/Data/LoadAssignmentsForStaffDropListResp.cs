using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Data
{
	// Token: 0x020006F1 RID: 1777
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAssignmentsForStaffDropListResp
	{
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x0001093B File Offset: 0x0000EB3B
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x00010943 File Offset: 0x0000EB43
		[DataMember]
		public IList<StaffDropListAssignmentDTO> Assignments { get; set; }
	}
}

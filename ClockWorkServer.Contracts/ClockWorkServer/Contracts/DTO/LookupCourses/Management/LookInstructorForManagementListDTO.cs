using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management
{
	// Token: 0x02000812 RID: 2066
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookInstructorForManagementListDTO
	{
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x00013FB8 File Offset: 0x000121B8
		// (set) Token: 0x06002A12 RID: 10770 RVA: 0x00013FC0 File Offset: 0x000121C0
		[DataMember]
		public IList<LookupInstructorForManagementDTO> Instructors { get; set; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x00013FC9 File Offset: 0x000121C9
		// (set) Token: 0x06002A14 RID: 10772 RVA: 0x00013FD1 File Offset: 0x000121D1
		[DataMember]
		public int StartIndex { get; set; }

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x00013FDA File Offset: 0x000121DA
		// (set) Token: 0x06002A16 RID: 10774 RVA: 0x00013FE2 File Offset: 0x000121E2
		[DataMember]
		public int Count { get; set; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x00013FEB File Offset: 0x000121EB
		// (set) Token: 0x06002A18 RID: 10776 RVA: 0x00013FF3 File Offset: 0x000121F3
		[DataMember]
		public int TotalCount { get; set; }
	}
}

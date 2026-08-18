using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management
{
	// Token: 0x02000815 RID: 2069
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupInstructorForManagementDTO
	{
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000140A6 File Offset: 0x000122A6
		// (set) Token: 0x06002A31 RID: 10801 RVA: 0x000140AE File Offset: 0x000122AE
		[DataMember]
		public LookupInstructorDTO Instructor { get; set; }

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000140B7 File Offset: 0x000122B7
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x000140BF File Offset: 0x000122BF
		[DataMember]
		public IList<LookupInstructorCourseAttachmentForManagementDTO> AttachedCourses { get; set; }
	}
}

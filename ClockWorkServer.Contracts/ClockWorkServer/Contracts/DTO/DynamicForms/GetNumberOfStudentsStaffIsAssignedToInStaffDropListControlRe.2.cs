using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200066E RID: 1646
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlResp
	{
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		[DataMember]
		public int NumberOfStudents { get; set; }
	}
}

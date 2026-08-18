using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000226 RID: 550
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFileCategoryFileDescriptionsDTO
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00005B28 File Offset: 0x00003D28
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00005B30 File Offset: 0x00003D30
		[DataMember]
		public string StudentFileCategoryTitle { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00005B39 File Offset: 0x00003D39
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00005B41 File Offset: 0x00003D41
		[DataMember]
		public IList<DynamicFileDescriptionDTO> FileDescriptions { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000229 RID: 553
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentFileDescriptionsResp
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00005B7D File Offset: 0x00003D7D
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00005B85 File Offset: 0x00003D85
		[DataMember]
		public StudentFileCategoryFileDescriptionsWithColDataDTO[] StudentFileCategoriesWithFileDescriptions { get; set; }
	}
}

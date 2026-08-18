using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000225 RID: 549
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFileCategoryFieldDTO
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00005AE4 File Offset: 0x00003CE4
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00005AEC File Offset: 0x00003CEC
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00005AF5 File Offset: 0x00003CF5
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00005AFD File Offset: 0x00003CFD
		[DataMember]
		public eStudentFileCategoryFormType FormType { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00005B06 File Offset: 0x00003D06
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00005B0E File Offset: 0x00003D0E
		[DataMember]
		public eStudentFileCategoryFieldType FieldType { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00005B17 File Offset: 0x00003D17
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x00005B1F File Offset: 0x00003D1F
		[DataMember]
		public int[] NoteColumns { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x02000227 RID: 551
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentFileCategoryFileDescriptionsWithColDataDTO
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00005B4A File Offset: 0x00003D4A
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00005B52 File Offset: 0x00003D52
		[DataMember]
		public string StudentFileCategoryTitle { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00005B5B File Offset: 0x00003D5B
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00005B63 File Offset: 0x00003D63
		[DataMember]
		public IList<DynamicFileDescriptionWithColDataDTO> FileDescriptions { get; set; }
	}
}

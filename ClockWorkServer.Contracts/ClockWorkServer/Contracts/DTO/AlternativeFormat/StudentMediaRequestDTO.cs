using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C2B RID: 3115
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentMediaRequestDTO
	{
		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x0001FF4F File Offset: 0x0001E14F
		// (set) Token: 0x06004148 RID: 16712 RVA: 0x0001FF57 File Offset: 0x0001E157
		[DataMember]
		public int StudentMediaRequestId { get; set; }

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x0001FF60 File Offset: 0x0001E160
		// (set) Token: 0x0600414A RID: 16714 RVA: 0x0001FF68 File Offset: 0x0001E168
		[DataMember]
		public PersonBaseDTO RequestMadeFromStudent { get; set; }

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x0001FF71 File Offset: 0x0001E171
		// (set) Token: 0x0600414C RID: 16716 RVA: 0x0001FF79 File Offset: 0x0001E179
		[DataMember]
		public DateTime CreatedDatetime { get; set; }

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x0001FF82 File Offset: 0x0001E182
		// (set) Token: 0x0600414E RID: 16718 RVA: 0x0001FF8A File Offset: 0x0001E18A
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> ContentRequestedList { get; set; }

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x0001FF93 File Offset: 0x0001E193
		// (set) Token: 0x06004150 RID: 16720 RVA: 0x0001FF9B File Offset: 0x0001E19B
		[DataMember]
		public DateTime? CompletedDateTime { get; set; }

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x0001FFA4 File Offset: 0x0001E1A4
		// (set) Token: 0x06004152 RID: 16722 RVA: 0x0001FFAC File Offset: 0x0001E1AC
		[DataMember]
		public SchoolCampusDTO Campus { get; set; }
	}
}

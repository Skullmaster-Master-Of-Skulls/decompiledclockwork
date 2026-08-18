using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x02000222 RID: 546
	[DataContract(Namespace = "http://tpro.ca")]
	public class SurveyStatusDTO
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00005A07 File Offset: 0x00003C07
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x00005A0F File Offset: 0x00003C0F
		[DataMember]
		public int PeopleSurveyStatusId { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00005A18 File Offset: 0x00003C18
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00005A20 File Offset: 0x00003C20
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00005A29 File Offset: 0x00003C29
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00005A31 File Offset: 0x00003C31
		[DataMember]
		public eSurveyStatusType StatusType { get; set; }
	}
}

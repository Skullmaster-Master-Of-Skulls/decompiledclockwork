using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200047D RID: 1149
	[DataContract(Namespace = "http://tpro.ca")]
	public class GenerateAccommodationLetterForExternalLogicRulesUserReq : BaseMessageReq
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0000B717 File Offset: 0x00009917
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x0000B71F File Offset: 0x0000991F
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0000B728 File Offset: 0x00009928
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0000B730 File Offset: 0x00009930
		[DataMember]
		public int LuCourseId { get; set; }
	}
}

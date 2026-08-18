using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200047E RID: 1150
	[DataContract(Namespace = "http://tpro.ca")]
	public class GenerateAccommodationLetterForExternalLogicRulesUserResp
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0000B739 File Offset: 0x00009939
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x0000B741 File Offset: 0x00009941
		public BinaryFileDTO AccommodationLetter { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000485 RID: 1157
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoMailMergeAccommodationLetterReq : BaseReportMessageReq
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0000B816 File Offset: 0x00009A16
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x0000B81E File Offset: 0x00009A1E
		[DataMember]
		public AccommodationLetterGenerateContextDTO Context { get; set; }
	}
}

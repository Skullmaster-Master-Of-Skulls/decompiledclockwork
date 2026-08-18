using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200062A RID: 1578
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAppointmentNotesSummaryHtmlResp
	{
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0000E93A File Offset: 0x0000CB3A
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x0000E942 File Offset: 0x0000CB42
		[DataMember]
		public string SummaryHtml { get; set; }
	}
}

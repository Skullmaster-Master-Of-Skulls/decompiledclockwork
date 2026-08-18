using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200041B RID: 1051
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsReq : BaseReportMessageReq
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0000AAC5 File Offset: 0x00008CC5
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x0000AACD File Offset: 0x00008CCD
		[DataMember]
		public DateTime CourseStartDate { get; set; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0000AAD6 File Offset: 0x00008CD6
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x0000AADE File Offset: 0x00008CDE
		[DataMember]
		public DateTime CourseEndDate { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0000AAE7 File Offset: 0x00008CE7
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x0000AAEF File Offset: 0x00008CEF
		[DataMember]
		public bool OnlyReturnNotesMarkedForDeletion { get; set; }
	}
}

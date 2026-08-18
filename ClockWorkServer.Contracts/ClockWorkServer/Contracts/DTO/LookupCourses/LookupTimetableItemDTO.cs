using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200080D RID: 2061
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupTimetableItemDTO
	{
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x00013E5E File Offset: 0x0001205E
		// (set) Token: 0x060029F0 RID: 10736 RVA: 0x00013E66 File Offset: 0x00012066
		[DataMember]
		public int TimetableId { get; set; }

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x00013E6F File Offset: 0x0001206F
		// (set) Token: 0x060029F2 RID: 10738 RVA: 0x00013E77 File Offset: 0x00012077
		[DataMember]
		public char TimetableType { get; set; }

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x00013E80 File Offset: 0x00012080
		// (set) Token: 0x060029F4 RID: 10740 RVA: 0x00013E88 File Offset: 0x00012088
		[DataMember]
		public TimeSpan StartTime { get; set; }

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x00013E91 File Offset: 0x00012091
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x00013E99 File Offset: 0x00012099
		[DataMember]
		public TimeSpan EndTime { get; set; }

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x00013EA2 File Offset: 0x000120A2
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x00013EAA File Offset: 0x000120AA
		[DataMember]
		public string Room { get; set; }

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x00013EB3 File Offset: 0x000120B3
		// (set) Token: 0x060029FA RID: 10746 RVA: 0x00013EBB File Offset: 0x000120BB
		[DataMember]
		public DayOfWeek DayOfWeek { get; set; }
	}
}

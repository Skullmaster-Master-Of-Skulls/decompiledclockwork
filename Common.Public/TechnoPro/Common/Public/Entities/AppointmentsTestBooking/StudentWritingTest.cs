using System;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000509 RID: 1289
	public class StudentWritingTest
	{
		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x00029646 File Offset: 0x00027846
		// (set) Token: 0x06002739 RID: 10041 RVA: 0x0002964E File Offset: 0x0002784E
		public PersonBase Student { get; set; }

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x00029657 File Offset: 0x00027857
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x0002965F File Offset: 0x0002785F
		public int AppointmentId { get; set; }

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x00029668 File Offset: 0x00027868
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x00029670 File Offset: 0x00027870
		public int ExamId { get; set; }

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x00029679 File Offset: 0x00027879
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x00029681 File Offset: 0x00027881
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0002968A File Offset: 0x0002788A
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x00029692 File Offset: 0x00027892
		public DateTime EndDateTime { get; set; }

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x0002969B File Offset: 0x0002789B
		// (set) Token: 0x06002743 RID: 10051 RVA: 0x000296A3 File Offset: 0x000278A3
		public bool IsCancelled { get; set; }

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x000296AC File Offset: 0x000278AC
		// (set) Token: 0x06002745 RID: 10053 RVA: 0x000296B4 File Offset: 0x000278B4
		public bool IsTentative { get; set; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000296BD File Offset: 0x000278BD
		// (set) Token: 0x06002747 RID: 10055 RVA: 0x000296C5 File Offset: 0x000278C5
		public bool? InstructorAcknowledgedValue { get; set; }

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000296CE File Offset: 0x000278CE
		// (set) Token: 0x06002749 RID: 10057 RVA: 0x000296D6 File Offset: 0x000278D6
		public DateTime? InstructorAcknowledgedDate { get; set; }

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x000296DF File Offset: 0x000278DF
		// (set) Token: 0x0600274B RID: 10059 RVA: 0x000296E7 File Offset: 0x000278E7
		public string Location { get; set; }

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x000296F0 File Offset: 0x000278F0
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x000296F8 File Offset: 0x000278F8
		public string SubTitle { get; set; }

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x00029701 File Offset: 0x00027901
		// (set) Token: 0x0600274F RID: 10063 RVA: 0x00029709 File Offset: 0x00027909
		public AppType AppointmentType { get; set; }
	}
}

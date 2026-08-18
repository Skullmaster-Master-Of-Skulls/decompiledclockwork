using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x0200050B RID: 1291
	public class TestForEdit
	{
		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x00029734 File Offset: 0x00027934
		// (set) Token: 0x06002757 RID: 10071 RVA: 0x0002973C File Offset: 0x0002793C
		public Test Test { get; set; }

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x00029745 File Offset: 0x00027945
		// (set) Token: 0x06002759 RID: 10073 RVA: 0x0002974D File Offset: 0x0002794D
		public DateTime? StudentReportedClassStartDateTime { get; set; }

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x00029756 File Offset: 0x00027956
		// (set) Token: 0x0600275B RID: 10075 RVA: 0x0002975E File Offset: 0x0002795E
		public DateTime? StudentReportedClassEndDateTime { get; set; }

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x00029767 File Offset: 0x00027967
		// (set) Token: 0x0600275D RID: 10077 RVA: 0x0002976F File Offset: 0x0002796F
		public bool? InstructorSubmittedTestInfo { get; set; }

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x00029778 File Offset: 0x00027978
		// (set) Token: 0x0600275F RID: 10079 RVA: 0x00029780 File Offset: 0x00027980
		public string TestNote { get; set; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x00029789 File Offset: 0x00027989
		// (set) Token: 0x06002761 RID: 10081 RVA: 0x00029791 File Offset: 0x00027991
		public string BookingNote { get; set; }

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x0002979A File Offset: 0x0002799A
		// (set) Token: 0x06002763 RID: 10083 RVA: 0x000297A2 File Offset: 0x000279A2
		public string PrivateNote { get; set; }

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000297AB File Offset: 0x000279AB
		// (set) Token: 0x06002765 RID: 10085 RVA: 0x000297B3 File Offset: 0x000279B3
		public string TestDeliveryMethod { get; set; }
	}
}

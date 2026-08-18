using System;

namespace ReportFunctions.ClockWorkDataSync.Courses
{
	// Token: 0x02000034 RID: 52
	public class DataSyncCourseAction : DataSyncAction
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0003DDE0 File Offset: 0x0003CDE0
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0003DDF7 File Offset: 0x0003CDF7
		public DataSyncClockWorkCourse ClockWorkCourse { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0003DE00 File Offset: 0x0003CE00
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0003DE17 File Offset: 0x0003CE17
		public DataSyncExternalCourse ExternalCourse { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0003DE20 File Offset: 0x0003CE20
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0003DE37 File Offset: 0x0003CE37
		public DataSyncInstructor ClockWorkInstructor { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0003DE40 File Offset: 0x0003CE40
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0003DE57 File Offset: 0x0003CE57
		public DataSyncInstructor ExternalInstructor { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0003DE60 File Offset: 0x0003CE60
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0003DE77 File Offset: 0x0003CE77
		public DataSyncTermScope TermScope { get; set; }
	}
}

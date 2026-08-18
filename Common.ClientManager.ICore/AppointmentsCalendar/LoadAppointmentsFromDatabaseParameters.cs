using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar
{
	// Token: 0x02000096 RID: 150
	public class LoadAppointmentsFromDatabaseParameters
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00002093 File Offset: 0x00000293
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000209B File Offset: 0x0000029B
		public IList<DateTime> dateTimes { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000020AC File Offset: 0x000002AC
		public int[] overrideAppTypeIdsToShow { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000020B5 File Offset: 0x000002B5
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000020BD File Offset: 0x000002BD
		public IList<int> pids { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000020C6 File Offset: 0x000002C6
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000020CE File Offset: 0x000002CE
		public bool hideCancelledAppointments { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000020D7 File Offset: 0x000002D7
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000020DF File Offset: 0x000002DF
		public bool perStudentShowIconsForFilledOutPerStudentScreensOnAppointments { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x000020F0 File Offset: 0x000002F0
		public bool anonymousShowIconsForFilledOutAnonymousScreensOnAppointments { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00002101 File Offset: 0x00000301
		public IList<int> studentPids { get; set; }
	}
}

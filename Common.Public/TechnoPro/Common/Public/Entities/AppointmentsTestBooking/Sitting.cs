using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000514 RID: 1300
	public class Sitting : SittingBase
	{
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x00029B10 File Offset: 0x00027D10
		// (set) Token: 0x060027BF RID: 10175 RVA: 0x00029B18 File Offset: 0x00027D18
		public PersonBase WhoCreated { get; set; }

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x00029B21 File Offset: 0x00027D21
		// (set) Token: 0x060027C1 RID: 10177 RVA: 0x00029B29 File Offset: 0x00027D29
		public int InvigilatorConfirmed { get; set; }

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x00029B32 File Offset: 0x00027D32
		// (set) Token: 0x060027C3 RID: 10179 RVA: 0x00029B3A File Offset: 0x00027D3A
		public double RateOfPay { get; set; }

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x00029B43 File Offset: 0x00027D43
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x00029B4B File Offset: 0x00027D4B
		public string PrivateNotes { get; set; }

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x00029B54 File Offset: 0x00027D54
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x00029B5C File Offset: 0x00027D5C
		public string InvigilatorNotes { get; set; }

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x00029B65 File Offset: 0x00027D65
		// (set) Token: 0x060027C9 RID: 10185 RVA: 0x00029B6D File Offset: 0x00027D6D
		public DateTime? ActualTimeIn { get; set; }

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x00029B76 File Offset: 0x00027D76
		// (set) Token: 0x060027CB RID: 10187 RVA: 0x00029B7E File Offset: 0x00027D7E
		public DateTime? ActualTimeOut { get; set; }

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x00029B87 File Offset: 0x00027D87
		// (set) Token: 0x060027CD RID: 10189 RVA: 0x00029B8F File Offset: 0x00027D8F
		public DateTime? PayDate { get; set; }

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x00029B98 File Offset: 0x00027D98
		// (set) Token: 0x060027CF RID: 10191 RVA: 0x00029BA0 File Offset: 0x00027DA0
		public DateTime DateCreated { get; set; }

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00029BA9 File Offset: 0x00027DA9
		// (set) Token: 0x060027D1 RID: 10193 RVA: 0x00029BB1 File Offset: 0x00027DB1
		public DateTime? VirtualMinStartDateTimeFromBookings { get; set; }

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x00029BBA File Offset: 0x00027DBA
		// (set) Token: 0x060027D3 RID: 10195 RVA: 0x00029BC2 File Offset: 0x00027DC2
		public DateTime? VirtualMaxEndDateTimeFromBookings { get; set; }
	}
}

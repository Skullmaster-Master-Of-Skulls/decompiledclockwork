using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000325 RID: 805
	public class IntakeEntry
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0001DB30 File Offset: 0x0001BD30
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0001DB38 File Offset: 0x0001BD38
		public int[] PersonIds { get; set; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0001DB41 File Offset: 0x0001BD41
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0001DB49 File Offset: 0x0001BD49
		public string FirstName { get; set; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0001DB52 File Offset: 0x0001BD52
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public string MiddleName { get; set; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0001DB63 File Offset: 0x0001BD63
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0001DB6B File Offset: 0x0001BD6B
		public string LastName { get; set; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0001DB74 File Offset: 0x0001BD74
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x0001DB7C File Offset: 0x0001BD7C
		public string StudentNumber { get; set; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0001DB85 File Offset: 0x0001BD85
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x0001DB8D File Offset: 0x0001BD8D
		public string Email { get; set; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0001DB96 File Offset: 0x0001BD96
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x0001DB9E File Offset: 0x0001BD9E
		public string Ip { get; set; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0001DBA7 File Offset: 0x0001BDA7
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x0001DBAF File Offset: 0x0001BDAF
		public DateTime DateAdded { get; set; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
		public IntakeStatus Status { get; set; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0001DBC9 File Offset: 0x0001BDC9
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x0001DBD1 File Offset: 0x0001BDD1
		public string Note { get; set; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0001DBDA File Offset: 0x0001BDDA
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x0001DBE2 File Offset: 0x0001BDE2
		public int ExistingClockWorkStudentPersonId { get; set; }
	}
}

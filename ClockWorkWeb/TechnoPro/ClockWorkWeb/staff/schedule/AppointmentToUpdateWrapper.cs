using System;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000101 RID: 257
	public class AppointmentToUpdateWrapper
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00038C55 File Offset: 0x00036E55
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00038C5D File Offset: 0x00036E5D
		public int AppointmentId { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00038C66 File Offset: 0x00036E66
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00038C6E File Offset: 0x00036E6E
		public DateTime Date { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00038C77 File Offset: 0x00036E77
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x00038C7F File Offset: 0x00036E7F
		public int StartTimeSeconds { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00038C88 File Offset: 0x00036E88
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00038C90 File Offset: 0x00036E90
		public int EndTimeSeconds { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00038C99 File Offset: 0x00036E99
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x00038CA1 File Offset: 0x00036EA1
		public string Subject { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00038CAA File Offset: 0x00036EAA
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00038CB2 File Offset: 0x00036EB2
		public int AppTypeId { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00038CBB File Offset: 0x00036EBB
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00038CC3 File Offset: 0x00036EC3
		public string MemoPlainText { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00038CCC File Offset: 0x00036ECC
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00038CD4 File Offset: 0x00036ED4
		public bool IsPrivate { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00038CDD File Offset: 0x00036EDD
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00038CE5 File Offset: 0x00036EE5
		public bool IsCancelled { get; set; }
	}
}

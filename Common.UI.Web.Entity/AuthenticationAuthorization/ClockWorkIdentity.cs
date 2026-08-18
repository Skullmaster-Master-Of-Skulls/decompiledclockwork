using System;

namespace TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization
{
	// Token: 0x02000041 RID: 65
	public class ClockWorkIdentity
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000039BE File Offset: 0x00001BBE
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000039C6 File Offset: 0x00001BC6
		public string UserName { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000039CF File Offset: 0x00001BCF
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000039D7 File Offset: 0x00001BD7
		public string StudentNumber { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000039E0 File Offset: 0x00001BE0
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000039E8 File Offset: 0x00001BE8
		public int PersonId { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000039F1 File Offset: 0x00001BF1
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000039F9 File Offset: 0x00001BF9
		public int NotetakerId { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00003A02 File Offset: 0x00001C02
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00003A0A File Offset: 0x00001C0A
		public int InstructorId { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00003A13 File Offset: 0x00001C13
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00003A1B File Offset: 0x00001C1B
		public int AlternateContactId { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00003A24 File Offset: 0x00001C24
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00003A2C File Offset: 0x00001C2C
		public bool IsAuthenticated { get; set; }
	}
}

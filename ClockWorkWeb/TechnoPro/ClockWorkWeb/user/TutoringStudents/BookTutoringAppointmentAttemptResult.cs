using System;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x02000052 RID: 82
	public class BookTutoringAppointmentAttemptResult
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000AF9E File Offset: 0x0000919E
		public BookTutoringAppointmentAttemptResult()
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000CB09 File Offset: 0x0000AD09
		public BookTutoringAppointmentAttemptResult(string failPublicMessage)
		{
			this.PublicMessage = failPublicMessage;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000CB1B File Offset: 0x0000AD1B
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000CB23 File Offset: 0x0000AD23
		public bool PassedChecks { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000CB34 File Offset: 0x0000AD34
		public int AppointmentId { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000CB3D File Offset: 0x0000AD3D
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000CB45 File Offset: 0x0000AD45
		public string PublicMessage { get; set; }
	}
}

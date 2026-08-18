using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000320 RID: 800
	public class CreateRealStudentAccountFromIntakeResult
	{
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		public int PersonId { get; set; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0001DAD9 File Offset: 0x0001BCD9
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x0001DAE1 File Offset: 0x0001BCE1
		public eCreateRealStudentAccountFromIntakeStatus Status { get; set; }
	}
}

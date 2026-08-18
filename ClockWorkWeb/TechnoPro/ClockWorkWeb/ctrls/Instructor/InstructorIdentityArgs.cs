using System;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor
{
	// Token: 0x02000147 RID: 327
	public class InstructorIdentityArgs : EventArgs
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00045F54 File Offset: 0x00044154
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00045F5C File Offset: 0x0004415C
		public int InstructorId { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00045F65 File Offset: 0x00044165
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00045F6D File Offset: 0x0004416D
		public int AlternateContactId { get; set; }
	}
}

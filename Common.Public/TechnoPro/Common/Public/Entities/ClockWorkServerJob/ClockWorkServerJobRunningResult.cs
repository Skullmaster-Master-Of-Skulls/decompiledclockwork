using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x02000458 RID: 1112
	public class ClockWorkServerJobRunningResult
	{
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00025A43 File Offset: 0x00023C43
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x00025A4B File Offset: 0x00023C4B
		public string JobName { get; set; }

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x00025A54 File Offset: 0x00023C54
		// (set) Token: 0x060021DC RID: 8668 RVA: 0x00025A5C File Offset: 0x00023C5C
		public eClockWorkServerJobResult Status { get; set; }

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x00025A65 File Offset: 0x00023C65
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x00025A6D File Offset: 0x00023C6D
		public string Message { get; set; }
	}
}

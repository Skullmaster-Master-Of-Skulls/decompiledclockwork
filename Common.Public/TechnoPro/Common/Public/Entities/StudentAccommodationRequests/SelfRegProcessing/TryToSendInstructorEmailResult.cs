using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x020001AC RID: 428
	public class TryToSendInstructorEmailResult
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00013D6E File Offset: 0x00011F6E
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x00013D76 File Offset: 0x00011F76
		public eTryToSendInstructorEmailStatus Status { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00013D7F File Offset: 0x00011F7F
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00013D87 File Offset: 0x00011F87
		public string ErrorMessage { get; set; }
	}
}

using System;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x0200010D RID: 269
	internal class ExternalExamInfo
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0004881F File Offset: 0x00046A1F
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00048827 File Offset: 0x00046A27
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00048830 File Offset: 0x00046A30
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00048838 File Offset: 0x00046A38
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00048841 File Offset: 0x00046A41
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x00048849 File Offset: 0x00046A49
		public string ExternalExamId { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00048852 File Offset: 0x00046A52
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0004885A File Offset: 0x00046A5A
		public string Location { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00048863 File Offset: 0x00046A63
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0004886B File Offset: 0x00046A6B
		public ClassTestBase ClockWorkClassTestDefinition { get; set; }
	}
}

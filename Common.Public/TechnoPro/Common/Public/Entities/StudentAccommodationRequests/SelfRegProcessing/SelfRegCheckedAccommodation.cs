using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x020001A9 RID: 425
	public class SelfRegCheckedAccommodation
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00013CE6 File Offset: 0x00011EE6
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00013CEE File Offset: 0x00011EEE
		public int ControlId { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00013CF7 File Offset: 0x00011EF7
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00013CFF File Offset: 0x00011EFF
		public bool IsChecked { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00013D08 File Offset: 0x00011F08
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00013D10 File Offset: 0x00011F10
		public string Text { get; set; }
	}
}

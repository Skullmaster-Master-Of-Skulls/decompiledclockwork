using System;

namespace TechnoPro.Common.Public.Entities.Tutoring
{
	// Token: 0x0200015E RID: 350
	public class TutorInfo
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00011A87 File Offset: 0x0000FC87
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00011A8F File Offset: 0x0000FC8F
		public int TutorId { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00011A98 File Offset: 0x0000FC98
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		public bool? IsAuthorized { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00011AA9 File Offset: 0x0000FCA9
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00011AB1 File Offset: 0x0000FCB1
		public DateTime? ConfidentialitySignedDate { get; set; }
	}
}

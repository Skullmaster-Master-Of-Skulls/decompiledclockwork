using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Legacy.Appointment
{
	// Token: 0x020002FD RID: 765
	public class AppointmentModifiedHistoryItem
	{
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0001C51B File Offset: 0x0001A71B
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x0001C523 File Offset: 0x0001A723
		public string Action { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0001C52C File Offset: 0x0001A72C
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x0001C534 File Offset: 0x0001A734
		public DateTime ActionDate { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0001C53D File Offset: 0x0001A73D
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x0001C545 File Offset: 0x0001A745
		public PersonBase ActionBy { get; set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0001C54E File Offset: 0x0001A74E
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x0001C556 File Offset: 0x0001A756
		public string ActionDetails { get; set; }
	}
}

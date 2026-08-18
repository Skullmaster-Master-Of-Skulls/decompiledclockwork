using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000AF RID: 175
	public class TryToBookRoom
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000D2B2 File Offset: 0x0000B4B2
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000D2BA File Offset: 0x0000B4BA
		public string Title { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000D2C3 File Offset: 0x0000B4C3
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000D2CB File Offset: 0x0000B4CB
		public int PersonId { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public eRoomType RoomType { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000D2E5 File Offset: 0x0000B4E5
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000D2ED File Offset: 0x0000B4ED
		public string[] Campuses { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000D2F6 File Offset: 0x0000B4F6
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000D2FE File Offset: 0x0000B4FE
		public IList<string> AssetsSupported { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000D307 File Offset: 0x0000B507
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000D30F File Offset: 0x0000B50F
		public int OrderNum { get; set; }
	}
}

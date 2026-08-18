using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.AppointmentTypes
{
	// Token: 0x020001CE RID: 462
	public class SnapshotWorkshop
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00014E77 File Offset: 0x00013077
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00014E7F File Offset: 0x0001307F
		public int WorkshopId { get; set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00014E88 File Offset: 0x00013088
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00014E90 File Offset: 0x00013090
		public int AppTypeId { get; set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00014E99 File Offset: 0x00013099
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00014EA1 File Offset: 0x000130A1
		public string WorkshopTitle { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00014EAA File Offset: 0x000130AA
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00014EB2 File Offset: 0x000130B2
		public string WorkshopDescription { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00014EBB File Offset: 0x000130BB
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00014EC3 File Offset: 0x000130C3
		public int MaxAttendees { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00014ECC File Offset: 0x000130CC
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00014ED4 File Offset: 0x000130D4
		public double WorkshopFee { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00014EDD File Offset: 0x000130DD
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00014EE5 File Offset: 0x000130E5
		public int PersonId { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00014EEE File Offset: 0x000130EE
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x00014EF6 File Offset: 0x000130F6
		public int PersonId2 { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00014EFF File Offset: 0x000130FF
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x00014F07 File Offset: 0x00013107
		public int PersonId3 { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00014F10 File Offset: 0x00013110
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00014F18 File Offset: 0x00013118
		public string Partners { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00014F21 File Offset: 0x00013121
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00014F29 File Offset: 0x00013129
		public string Note { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00014F32 File Offset: 0x00013132
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00014F3A File Offset: 0x0001313A
		public string Location { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00014F43 File Offset: 0x00013143
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00014F4B File Offset: 0x0001314B
		public bool AvailableForOnlineBooking { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00014F54 File Offset: 0x00013154
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00014F5C File Offset: 0x0001315C
		public int WaitingListMaxUsers { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00014F65 File Offset: 0x00013165
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x00014F6D File Offset: 0x0001316D
		public bool IsActive { get; set; }
	}
}

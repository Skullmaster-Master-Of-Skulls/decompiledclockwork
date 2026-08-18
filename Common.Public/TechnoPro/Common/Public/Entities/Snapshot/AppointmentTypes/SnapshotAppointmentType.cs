using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.AppointmentTypes
{
	// Token: 0x020001CC RID: 460
	public class SnapshotAppointmentType
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00014D45 File Offset: 0x00012F45
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00014D4D File Offset: 0x00012F4D
		public int AppTypeId { get; set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00014D56 File Offset: 0x00012F56
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00014D5E File Offset: 0x00012F5E
		public string Description { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00014D67 File Offset: 0x00012F67
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00014D6F File Offset: 0x00012F6F
		public int DefaultColour { get; set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00014D78 File Offset: 0x00012F78
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00014D80 File Offset: 0x00012F80
		public bool IsBackground { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00014D89 File Offset: 0x00012F89
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00014D91 File Offset: 0x00012F91
		public bool IsWorkshop { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00014D9A File Offset: 0x00012F9A
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00014DA2 File Offset: 0x00012FA2
		public bool IsCourse { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00014DAB File Offset: 0x00012FAB
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00014DB3 File Offset: 0x00012FB3
		public int? DefaultOverrideColour { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00014DBC File Offset: 0x00012FBC
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00014DC4 File Offset: 0x00012FC4
		public int? DefaultIcon { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00014DCD File Offset: 0x00012FCD
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00014DD5 File Offset: 0x00012FD5
		public int AppointmentTypeGroupId { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00014DDE File Offset: 0x00012FDE
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00014DE6 File Offset: 0x00012FE6
		public bool ShowInHighlights { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00014DEF File Offset: 0x00012FEF
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00014DF7 File Offset: 0x00012FF7
		public bool IsActive { get; set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00014E00 File Offset: 0x00013000
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00014E08 File Offset: 0x00013008
		public string PerAppScreenNumsForTabs { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00014E11 File Offset: 0x00013011
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00014E19 File Offset: 0x00013019
		public int PerJustAppScreenNum { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00014E22 File Offset: 0x00013022
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00014E2A File Offset: 0x0001302A
		public int IconIndex { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00014E33 File Offset: 0x00013033
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00014E3B File Offset: 0x0001303B
		public string LongDescription { get; set; }
	}
}

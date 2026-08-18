using System;

namespace System.Windows.Forms
{
	// Token: 0x0200011D RID: 285
	[Flags]
	public enum AccessibleStates
	{
		// Token: 0x040005AB RID: 1451
		None = 0,
		// Token: 0x040005AC RID: 1452
		Unavailable = 1,
		// Token: 0x040005AD RID: 1453
		Selected = 2,
		// Token: 0x040005AE RID: 1454
		Focused = 4,
		// Token: 0x040005AF RID: 1455
		Pressed = 8,
		// Token: 0x040005B0 RID: 1456
		Checked = 16,
		// Token: 0x040005B1 RID: 1457
		Mixed = 32,
		// Token: 0x040005B2 RID: 1458
		Indeterminate = 32,
		// Token: 0x040005B3 RID: 1459
		ReadOnly = 64,
		// Token: 0x040005B4 RID: 1460
		HotTracked = 128,
		// Token: 0x040005B5 RID: 1461
		Default = 256,
		// Token: 0x040005B6 RID: 1462
		Expanded = 512,
		// Token: 0x040005B7 RID: 1463
		Collapsed = 1024,
		// Token: 0x040005B8 RID: 1464
		Busy = 2048,
		// Token: 0x040005B9 RID: 1465
		Floating = 4096,
		// Token: 0x040005BA RID: 1466
		Marqueed = 8192,
		// Token: 0x040005BB RID: 1467
		Animated = 16384,
		// Token: 0x040005BC RID: 1468
		Invisible = 32768,
		// Token: 0x040005BD RID: 1469
		Offscreen = 65536,
		// Token: 0x040005BE RID: 1470
		Sizeable = 131072,
		// Token: 0x040005BF RID: 1471
		Moveable = 262144,
		// Token: 0x040005C0 RID: 1472
		SelfVoicing = 524288,
		// Token: 0x040005C1 RID: 1473
		Focusable = 1048576,
		// Token: 0x040005C2 RID: 1474
		Selectable = 2097152,
		// Token: 0x040005C3 RID: 1475
		Linked = 4194304,
		// Token: 0x040005C4 RID: 1476
		Traversed = 8388608,
		// Token: 0x040005C5 RID: 1477
		MultiSelectable = 16777216,
		// Token: 0x040005C6 RID: 1478
		ExtSelectable = 33554432,
		// Token: 0x040005C7 RID: 1479
		AlertLow = 67108864,
		// Token: 0x040005C8 RID: 1480
		AlertMedium = 134217728,
		// Token: 0x040005C9 RID: 1481
		AlertHigh = 268435456,
		// Token: 0x040005CA RID: 1482
		Protected = 536870912,
		// Token: 0x040005CB RID: 1483
		HasPopup = 1073741824,
		// Token: 0x040005CC RID: 1484
		[Obsolete("This enumeration value has been deprecated. There is no replacement. http://go.microsoft.com/fwlink/?linkid=14202")]
		Valid = 1073741823
	}
}

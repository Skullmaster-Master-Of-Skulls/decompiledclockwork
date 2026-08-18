using System;

namespace System.Windows.Forms
{
	// Token: 0x02000117 RID: 279
	public enum AccessibleEvents
	{
		// Token: 0x0400050D RID: 1293
		SystemSound = 1,
		// Token: 0x0400050E RID: 1294
		SystemAlert,
		// Token: 0x0400050F RID: 1295
		SystemForeground,
		// Token: 0x04000510 RID: 1296
		SystemMenuStart,
		// Token: 0x04000511 RID: 1297
		SystemMenuEnd,
		// Token: 0x04000512 RID: 1298
		SystemMenuPopupStart,
		// Token: 0x04000513 RID: 1299
		SystemMenuPopupEnd,
		// Token: 0x04000514 RID: 1300
		SystemCaptureStart,
		// Token: 0x04000515 RID: 1301
		SystemCaptureEnd,
		// Token: 0x04000516 RID: 1302
		SystemMoveSizeStart,
		// Token: 0x04000517 RID: 1303
		SystemMoveSizeEnd,
		// Token: 0x04000518 RID: 1304
		SystemContextHelpStart,
		// Token: 0x04000519 RID: 1305
		SystemContextHelpEnd,
		// Token: 0x0400051A RID: 1306
		SystemDragDropStart,
		// Token: 0x0400051B RID: 1307
		SystemDragDropEnd,
		// Token: 0x0400051C RID: 1308
		SystemDialogStart,
		// Token: 0x0400051D RID: 1309
		SystemDialogEnd,
		// Token: 0x0400051E RID: 1310
		SystemScrollingStart,
		// Token: 0x0400051F RID: 1311
		SystemScrollingEnd,
		// Token: 0x04000520 RID: 1312
		SystemSwitchStart,
		// Token: 0x04000521 RID: 1313
		SystemSwitchEnd,
		// Token: 0x04000522 RID: 1314
		SystemMinimizeStart,
		// Token: 0x04000523 RID: 1315
		SystemMinimizeEnd,
		// Token: 0x04000524 RID: 1316
		Create = 32768,
		// Token: 0x04000525 RID: 1317
		Destroy,
		// Token: 0x04000526 RID: 1318
		Show,
		// Token: 0x04000527 RID: 1319
		Hide,
		// Token: 0x04000528 RID: 1320
		Reorder,
		// Token: 0x04000529 RID: 1321
		Focus,
		// Token: 0x0400052A RID: 1322
		Selection,
		// Token: 0x0400052B RID: 1323
		SelectionAdd,
		// Token: 0x0400052C RID: 1324
		SelectionRemove,
		// Token: 0x0400052D RID: 1325
		SelectionWithin,
		// Token: 0x0400052E RID: 1326
		StateChange,
		// Token: 0x0400052F RID: 1327
		LocationChange,
		// Token: 0x04000530 RID: 1328
		NameChange,
		// Token: 0x04000531 RID: 1329
		DescriptionChange,
		// Token: 0x04000532 RID: 1330
		ValueChange,
		// Token: 0x04000533 RID: 1331
		ParentChange,
		// Token: 0x04000534 RID: 1332
		HelpChange,
		// Token: 0x04000535 RID: 1333
		DefaultActionChange,
		// Token: 0x04000536 RID: 1334
		AcceleratorChange
	}
}

using System;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200004B RID: 75
	public interface ISparkline
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600050A RID: 1290
		// (set) Token: 0x0600050B RID: 1291
		CellRange DataRange { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600050C RID: 1292
		// (set) Token: 0x0600050D RID: 1293
		CellRange RefRange { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600050E RID: 1294
		int Column { get; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600050F RID: 1295
		int Row { get; }
	}
}

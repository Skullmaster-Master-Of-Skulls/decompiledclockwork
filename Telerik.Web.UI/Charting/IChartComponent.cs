using System;
using System.ComponentModel;

namespace Telerik.Charting
{
	// Token: 0x020016E2 RID: 5858
	public interface IChartComponent
	{
		// Token: 0x1700457E RID: 17790
		// (get) Token: 0x0600E39F RID: 58271
		Chart Chart { get; }

		// Token: 0x1700457F RID: 17791
		// (get) Token: 0x0600E3A0 RID: 58272
		// (set) Token: 0x0600E3A1 RID: 58273
		string TempImagesFolder { get; set; }

		// Token: 0x0600E3A2 RID: 58274
		string MapPath(string filePath);

		// Token: 0x0600E3A3 RID: 58275
		object Clone();

		// Token: 0x17004580 RID: 17792
		// (get) Token: 0x0600E3A4 RID: 58276
		// (set) Token: 0x0600E3A5 RID: 58277
		ISite Site { get; set; }

		// Token: 0x140001C1 RID: 449
		// (add) Token: 0x0600E3A6 RID: 58278
		// (remove) Token: 0x0600E3A7 RID: 58279
		event EventHandler Disposed;
	}
}

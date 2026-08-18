using System;
using Spire.Xls.Core.Interface;

namespace Spire.Xls.Core
{
	// Token: 0x02000202 RID: 514
	public interface IBuiltInDocumentProperties
	{
		// Token: 0x17000AC6 RID: 2758
		IDocumentProperty this[BuiltInPropertyType index]
		{
			get;
		}

		// Token: 0x17000AC7 RID: 2759
		IDocumentProperty this[int iIndex]
		{
			get;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06001D0B RID: 7435
		int Count { get; }

		// Token: 0x06001D0C RID: 7436
		void Clear();

		// Token: 0x06001D0D RID: 7437
		bool Contains(BuiltInPropertyType index);

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06001D0E RID: 7438
		// (set) Token: 0x06001D0F RID: 7439
		string Title { get; set; }

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06001D10 RID: 7440
		// (set) Token: 0x06001D11 RID: 7441
		string Subject { get; set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06001D12 RID: 7442
		// (set) Token: 0x06001D13 RID: 7443
		string Author { get; set; }

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001D14 RID: 7444
		// (set) Token: 0x06001D15 RID: 7445
		string Keywords { get; set; }

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001D16 RID: 7446
		// (set) Token: 0x06001D17 RID: 7447
		string Comments { get; set; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001D18 RID: 7448
		// (set) Token: 0x06001D19 RID: 7449
		string Template { get; set; }

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001D1A RID: 7450
		// (set) Token: 0x06001D1B RID: 7451
		string LastAuthor { get; set; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06001D1C RID: 7452
		// (set) Token: 0x06001D1D RID: 7453
		string RevisionNumber { get; set; }

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06001D1E RID: 7454
		// (set) Token: 0x06001D1F RID: 7455
		TimeSpan EditTime { get; set; }

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06001D20 RID: 7456
		// (set) Token: 0x06001D21 RID: 7457
		DateTime LastPrinted { get; set; }

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001D22 RID: 7458
		// (set) Token: 0x06001D23 RID: 7459
		DateTime CreatedTime { get; set; }

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001D24 RID: 7460
		// (set) Token: 0x06001D25 RID: 7461
		DateTime LastSaveTime { get; set; }

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001D26 RID: 7462
		// (set) Token: 0x06001D27 RID: 7463
		int PageCount { get; set; }

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001D28 RID: 7464
		// (set) Token: 0x06001D29 RID: 7465
		int WordCount { get; set; }

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001D2A RID: 7466
		// (set) Token: 0x06001D2B RID: 7467
		int Characters { get; set; }

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001D2C RID: 7468
		// (set) Token: 0x06001D2D RID: 7469
		string ApplicationName { get; set; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001D2E RID: 7470
		// (set) Token: 0x06001D2F RID: 7471
		string Category { get; set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001D30 RID: 7472
		// (set) Token: 0x06001D31 RID: 7473
		string PresentationTarget { get; set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06001D32 RID: 7474
		// (set) Token: 0x06001D33 RID: 7475
		int Bytes { get; set; }

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001D34 RID: 7476
		// (set) Token: 0x06001D35 RID: 7477
		int LineCount { get; set; }

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001D36 RID: 7478
		// (set) Token: 0x06001D37 RID: 7479
		int ParagraphCount { get; set; }

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001D38 RID: 7480
		// (set) Token: 0x06001D39 RID: 7481
		int SlideCount { get; set; }

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001D3A RID: 7482
		// (set) Token: 0x06001D3B RID: 7483
		int NoteCount { get; set; }

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001D3C RID: 7484
		// (set) Token: 0x06001D3D RID: 7485
		int HiddenCount { get; set; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06001D3E RID: 7486
		// (set) Token: 0x06001D3F RID: 7487
		int MultimediaClipCount { get; set; }

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06001D40 RID: 7488
		// (set) Token: 0x06001D41 RID: 7489
		string Manager { get; set; }

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06001D42 RID: 7490
		// (set) Token: 0x06001D43 RID: 7491
		string Company { get; set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06001D44 RID: 7492
		// (set) Token: 0x06001D45 RID: 7493
		bool LinksDirty { get; set; }
	}
}

using System;

namespace Spire.Xls.Core.Interface
{
	// Token: 0x020001D7 RID: 471
	public interface IDocumentProperty
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001A3A RID: 6714
		bool IsBuiltIn { get; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001A3B RID: 6715
		BuiltInPropertyType PropertyId { get; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001A3C RID: 6716
		string Name { get; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001A3D RID: 6717
		// (set) Token: 0x06001A3E RID: 6718
		object Value { get; set; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001A3F RID: 6719
		// (set) Token: 0x06001A40 RID: 6720
		bool Boolean { get; set; }

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001A41 RID: 6721
		// (set) Token: 0x06001A42 RID: 6722
		int Integer { get; set; }

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001A43 RID: 6723
		// (set) Token: 0x06001A44 RID: 6724
		int Int32 { get; set; }

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001A45 RID: 6725
		// (set) Token: 0x06001A46 RID: 6726
		double Double { get; set; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001A47 RID: 6727
		// (set) Token: 0x06001A48 RID: 6728
		string Text { get; set; }

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001A49 RID: 6729
		// (set) Token: 0x06001A4A RID: 6730
		DateTime DateTime { get; set; }

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001A4B RID: 6731
		// (set) Token: 0x06001A4C RID: 6732
		TimeSpan TimeSpan { get; set; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001A4D RID: 6733
		// (set) Token: 0x06001A4E RID: 6734
		string LinkSource { get; set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001A4F RID: 6735
		// (set) Token: 0x06001A50 RID: 6736
		bool LinkToContent { get; set; }
	}
}

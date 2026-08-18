using System;

namespace Spire.CompoundFile.Doc
{
	// Token: 0x02000451 RID: 1105
	public interface IDocProperty
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06003D5C RID: 15708
		bool IsBuiltIn { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06003D5D RID: 15709
		BuiltInProperty PropertyId { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06003D5E RID: 15710
		string Name { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06003D5F RID: 15711
		// (set) Token: 0x06003D60 RID: 15712
		object Value { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06003D61 RID: 15713
		// (set) Token: 0x06003D62 RID: 15714
		bool Boolean { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06003D63 RID: 15715
		// (set) Token: 0x06003D64 RID: 15716
		int Integer { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06003D65 RID: 15717
		// (set) Token: 0x06003D66 RID: 15718
		int Int32 { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06003D67 RID: 15719
		// (set) Token: 0x06003D68 RID: 15720
		double Double { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06003D69 RID: 15721
		// (set) Token: 0x06003D6A RID: 15722
		string Text { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06003D6B RID: 15723
		// (set) Token: 0x06003D6C RID: 15724
		DateTime DateTime { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06003D6D RID: 15725
		// (set) Token: 0x06003D6E RID: 15726
		TimeSpan TimeSpan { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06003D6F RID: 15727
		// (set) Token: 0x06003D70 RID: 15728
		string LinkSource { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06003D71 RID: 15729
		// (set) Token: 0x06003D72 RID: 15730
		bool LinkToContent { get; set; }
	}
}

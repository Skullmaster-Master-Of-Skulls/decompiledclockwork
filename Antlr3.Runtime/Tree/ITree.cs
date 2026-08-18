using System;
using System.Collections.Generic;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200003E RID: 62
	public interface ITree
	{
		// Token: 0x06000299 RID: 665
		ITree GetChild(int i);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600029A RID: 666
		int ChildCount { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600029B RID: 667
		// (set) Token: 0x0600029C RID: 668
		ITree Parent { get; set; }

		// Token: 0x0600029D RID: 669
		bool HasAncestor(int ttype);

		// Token: 0x0600029E RID: 670
		ITree GetAncestor(int ttype);

		// Token: 0x0600029F RID: 671
		IList<ITree> GetAncestors();

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002A0 RID: 672
		// (set) Token: 0x060002A1 RID: 673
		int ChildIndex { get; set; }

		// Token: 0x060002A2 RID: 674
		void FreshenParentAndChildIndexes();

		// Token: 0x060002A3 RID: 675
		void AddChild(ITree t);

		// Token: 0x060002A4 RID: 676
		void SetChild(int i, ITree t);

		// Token: 0x060002A5 RID: 677
		object DeleteChild(int i);

		// Token: 0x060002A6 RID: 678
		void ReplaceChildren(int startChildIndex, int stopChildIndex, object t);

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A7 RID: 679
		bool IsNil { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A8 RID: 680
		// (set) Token: 0x060002A9 RID: 681
		int TokenStartIndex { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002AA RID: 682
		// (set) Token: 0x060002AB RID: 683
		int TokenStopIndex { get; set; }

		// Token: 0x060002AC RID: 684
		ITree DupNode();

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002AD RID: 685
		int Type { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002AE RID: 686
		string Text { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002AF RID: 687
		int Line { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002B0 RID: 688
		int CharPositionInLine { get; }

		// Token: 0x060002B1 RID: 689
		string ToStringTree();

		// Token: 0x060002B2 RID: 690
		string ToString();
	}
}

using System;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000040 RID: 64
	public interface ITreeAdaptor
	{
		// Token: 0x060002DE RID: 734
		object Create(IToken payload);

		// Token: 0x060002DF RID: 735
		object Create(int tokenType, IToken fromToken);

		// Token: 0x060002E0 RID: 736
		object Create(int tokenType, IToken fromToken, string text);

		// Token: 0x060002E1 RID: 737
		object Create(IToken fromToken, string text);

		// Token: 0x060002E2 RID: 738
		object Create(int tokenType, string text);

		// Token: 0x060002E3 RID: 739
		object DupNode(object treeNode);

		// Token: 0x060002E4 RID: 740
		object DupNode(int type, object treeNode);

		// Token: 0x060002E5 RID: 741
		object DupNode(object treeNode, string text);

		// Token: 0x060002E6 RID: 742
		object DupNode(int type, object treeNode, string text);

		// Token: 0x060002E7 RID: 743
		object DupTree(object tree);

		// Token: 0x060002E8 RID: 744
		object Nil();

		// Token: 0x060002E9 RID: 745
		object ErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException e);

		// Token: 0x060002EA RID: 746
		bool IsNil(object tree);

		// Token: 0x060002EB RID: 747
		void AddChild(object t, object child);

		// Token: 0x060002EC RID: 748
		object BecomeRoot(object newRoot, object oldRoot);

		// Token: 0x060002ED RID: 749
		object RulePostProcessing(object root);

		// Token: 0x060002EE RID: 750
		int GetUniqueID(object node);

		// Token: 0x060002EF RID: 751
		object BecomeRoot(IToken newRoot, object oldRoot);

		// Token: 0x060002F0 RID: 752
		int GetType(object t);

		// Token: 0x060002F1 RID: 753
		void SetType(object t, int type);

		// Token: 0x060002F2 RID: 754
		string GetText(object t);

		// Token: 0x060002F3 RID: 755
		void SetText(object t, string text);

		// Token: 0x060002F4 RID: 756
		IToken GetToken(object t);

		// Token: 0x060002F5 RID: 757
		void SetTokenBoundaries(object t, IToken startToken, IToken stopToken);

		// Token: 0x060002F6 RID: 758
		int GetTokenStartIndex(object t);

		// Token: 0x060002F7 RID: 759
		int GetTokenStopIndex(object t);

		// Token: 0x060002F8 RID: 760
		object GetChild(object t, int i);

		// Token: 0x060002F9 RID: 761
		void SetChild(object t, int i, object child);

		// Token: 0x060002FA RID: 762
		object DeleteChild(object t, int i);

		// Token: 0x060002FB RID: 763
		int GetChildCount(object t);

		// Token: 0x060002FC RID: 764
		object GetParent(object t);

		// Token: 0x060002FD RID: 765
		void SetParent(object t, object parent);

		// Token: 0x060002FE RID: 766
		int GetChildIndex(object t);

		// Token: 0x060002FF RID: 767
		void SetChildIndex(object t, int index);

		// Token: 0x06000300 RID: 768
		void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);
	}
}

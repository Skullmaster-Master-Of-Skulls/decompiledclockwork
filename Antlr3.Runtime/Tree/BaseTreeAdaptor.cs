using System;
using System.Collections.Generic;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000041 RID: 65
	public abstract class BaseTreeAdaptor : ITreeAdaptor
	{
		// Token: 0x06000301 RID: 769 RVA: 0x000082C8 File Offset: 0x000064C8
		public virtual object Nil()
		{
			return this.Create(null);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000082D4 File Offset: 0x000064D4
		public virtual object ErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException e)
		{
			return new CommonErrorNode(input, start, stop, e);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000082ED File Offset: 0x000064ED
		public virtual bool IsNil(object tree)
		{
			return ((ITree)tree).IsNil;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000082FC File Offset: 0x000064FC
		public virtual object DupNode(int type, object treeNode)
		{
			object obj = this.DupNode(treeNode);
			this.SetType(obj, type);
			return obj;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000831C File Offset: 0x0000651C
		public virtual object DupNode(object treeNode, string text)
		{
			object obj = this.DupNode(treeNode);
			this.SetText(obj, text);
			return obj;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000833C File Offset: 0x0000653C
		public virtual object DupNode(int type, object treeNode, string text)
		{
			object obj = this.DupNode(treeNode);
			this.SetType(obj, type);
			this.SetText(obj, text);
			return obj;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00008362 File Offset: 0x00006562
		public virtual object DupTree(object tree)
		{
			return this.DupTree(tree, null);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000836C File Offset: 0x0000656C
		public virtual object DupTree(object t, object parent)
		{
			if (t == null)
			{
				return null;
			}
			object obj = this.DupNode(t);
			this.SetChildIndex(obj, this.GetChildIndex(t));
			this.SetParent(obj, parent);
			int childCount = this.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = this.GetChild(t, i);
				object child2 = this.DupTree(child, t);
				this.AddChild(obj, child2);
			}
			return obj;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000083CD File Offset: 0x000065CD
		public virtual void AddChild(object t, object child)
		{
			if (t != null && child != null)
			{
				((ITree)t).AddChild((ITree)child);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000083E8 File Offset: 0x000065E8
		public virtual object BecomeRoot(object newRoot, object oldRoot)
		{
			ITree tree = (ITree)newRoot;
			ITree t = (ITree)oldRoot;
			if (oldRoot == null)
			{
				return newRoot;
			}
			if (tree.IsNil)
			{
				int childCount = tree.ChildCount;
				if (childCount == 1)
				{
					tree = tree.GetChild(0);
				}
				else if (childCount > 1)
				{
					throw new Exception("more than one node as root (TODO: make exception hierarchy)");
				}
			}
			tree.AddChild(t);
			return tree;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000843C File Offset: 0x0000663C
		public virtual object RulePostProcessing(object root)
		{
			ITree tree = (ITree)root;
			if (tree != null && tree.IsNil)
			{
				if (tree.ChildCount == 0)
				{
					tree = null;
				}
				else if (tree.ChildCount == 1)
				{
					tree = tree.GetChild(0);
					tree.Parent = null;
					tree.ChildIndex = -1;
				}
			}
			return tree;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008487 File Offset: 0x00006687
		public virtual object BecomeRoot(IToken newRoot, object oldRoot)
		{
			return this.BecomeRoot(this.Create(newRoot), oldRoot);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00008498 File Offset: 0x00006698
		public virtual object Create(int tokenType, IToken fromToken)
		{
			fromToken = this.CreateToken(fromToken);
			fromToken.Type = tokenType;
			return this.Create(fromToken);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000084C0 File Offset: 0x000066C0
		public virtual object Create(int tokenType, IToken fromToken, string text)
		{
			if (fromToken == null)
			{
				return this.Create(tokenType, text);
			}
			fromToken = this.CreateToken(fromToken);
			fromToken.Type = tokenType;
			fromToken.Text = text;
			return this.Create(fromToken);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000084FC File Offset: 0x000066FC
		public virtual object Create(IToken fromToken, string text)
		{
			if (fromToken == null)
			{
				throw new ArgumentNullException("fromToken");
			}
			fromToken = this.CreateToken(fromToken);
			fromToken.Text = text;
			return this.Create(fromToken);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008530 File Offset: 0x00006730
		public virtual object Create(int tokenType, string text)
		{
			IToken payload = this.CreateToken(tokenType, text);
			return this.Create(payload);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00008550 File Offset: 0x00006750
		public virtual int GetType(object t)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return 0;
			}
			return tree.Type;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00008570 File Offset: 0x00006770
		public virtual void SetType(object t, int type)
		{
			throw new NotSupportedException("don't know enough about Tree node");
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000857C File Offset: 0x0000677C
		public virtual string GetText(object t)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return null;
			}
			return tree.Text;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000859C File Offset: 0x0000679C
		public virtual void SetText(object t, string text)
		{
			throw new NotSupportedException("don't know enough about Tree node");
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000085A8 File Offset: 0x000067A8
		public virtual object GetChild(object t, int i)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return null;
			}
			return tree.GetChild(i);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000085CC File Offset: 0x000067CC
		public virtual void SetChild(object t, int i, object child)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return;
			}
			ITree tree2 = this.GetTree(child);
			tree.SetChild(i, tree2);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000085F5 File Offset: 0x000067F5
		public virtual object DeleteChild(object t, int i)
		{
			return ((ITree)t).DeleteChild(i);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008604 File Offset: 0x00006804
		public virtual int GetChildCount(object t)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return 0;
			}
			return tree.ChildCount;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008624 File Offset: 0x00006824
		public virtual int GetUniqueID(object node)
		{
			if (this.treeToUniqueIDMap == null)
			{
				this.treeToUniqueIDMap = new Dictionary<object, int>();
			}
			int num;
			if (this.treeToUniqueIDMap.TryGetValue(node, out num))
			{
				return num;
			}
			num = this.uniqueNodeID;
			this.treeToUniqueIDMap[node] = num;
			this.uniqueNodeID++;
			return num;
		}

		// Token: 0x0600031A RID: 794
		public abstract IToken CreateToken(int tokenType, string text);

		// Token: 0x0600031B RID: 795
		public abstract IToken CreateToken(IToken fromToken);

		// Token: 0x0600031C RID: 796
		public abstract object Create(IToken payload);

		// Token: 0x0600031D RID: 797 RVA: 0x0000867C File Offset: 0x0000687C
		public virtual object DupNode(object treeNode)
		{
			ITree tree = this.GetTree(treeNode);
			if (tree == null)
			{
				return null;
			}
			return tree.DupNode();
		}

		// Token: 0x0600031E RID: 798
		public abstract IToken GetToken(object t);

		// Token: 0x0600031F RID: 799 RVA: 0x0000869C File Offset: 0x0000689C
		public virtual void SetTokenBoundaries(object t, IToken startToken, IToken stopToken)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return;
			}
			int tokenStartIndex = 0;
			int tokenStopIndex = 0;
			if (startToken != null)
			{
				tokenStartIndex = startToken.TokenIndex;
			}
			if (stopToken != null)
			{
				tokenStopIndex = stopToken.TokenIndex;
			}
			tree.TokenStartIndex = tokenStartIndex;
			tree.TokenStopIndex = tokenStopIndex;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000086DC File Offset: 0x000068DC
		public virtual int GetTokenStartIndex(object t)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return -1;
			}
			return tree.TokenStartIndex;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000086FC File Offset: 0x000068FC
		public virtual int GetTokenStopIndex(object t)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return -1;
			}
			return tree.TokenStopIndex;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000871C File Offset: 0x0000691C
		public virtual object GetParent(object t)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return null;
			}
			return tree.Parent;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000873C File Offset: 0x0000693C
		public virtual void SetParent(object t, object parent)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return;
			}
			ITree tree2 = this.GetTree(parent);
			tree.Parent = tree2;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00008764 File Offset: 0x00006964
		public virtual int GetChildIndex(object t)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return 0;
			}
			return tree.ChildIndex;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008784 File Offset: 0x00006984
		public virtual void SetChildIndex(object t, int index)
		{
			ITree tree = this.GetTree(t);
			if (tree == null)
			{
				return;
			}
			tree.ChildIndex = index;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000087A4 File Offset: 0x000069A4
		public virtual void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			ITree tree = this.GetTree(parent);
			if (tree == null)
			{
				return;
			}
			tree.ReplaceChildren(startChildIndex, stopChildIndex, t);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000087C8 File Offset: 0x000069C8
		protected virtual ITree GetTree(object t)
		{
			if (t == null)
			{
				return null;
			}
			ITree tree = t as ITree;
			if (tree == null)
			{
				throw new NotSupportedException();
			}
			return tree;
		}

		// Token: 0x04000092 RID: 146
		protected IDictionary<object, int> treeToUniqueIDMap;

		// Token: 0x04000093 RID: 147
		protected int uniqueNodeID = 1;
	}
}

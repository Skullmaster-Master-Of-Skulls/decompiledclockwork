using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D7 RID: 471
	internal class AxisElement
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x000AAE90 File Offset: 0x000A9090
		internal DoubleLinkAxis CurNode
		{
			get
			{
				return this.curNode;
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000AAE98 File Offset: 0x000A9098
		internal AxisElement(DoubleLinkAxis node, int depth)
		{
			this.curNode = node;
			this.curDepth = depth;
			this.rootDepth = depth;
			this.isMatch = false;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000AAECC File Offset: 0x000A90CC
		internal void SetDepth(int depth)
		{
			this.curDepth = depth;
			this.rootDepth = depth;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000AAEEC File Offset: 0x000A90EC
		internal void MoveToParent(int depth, ForwardAxis parent)
		{
			if (depth != this.curDepth - 1)
			{
				if (depth == this.curDepth && this.isMatch)
				{
					this.isMatch = false;
				}
				return;
			}
			if (this.curNode.Input == parent.RootNode && parent.IsDss)
			{
				this.curNode = parent.RootNode;
				this.rootDepth = (this.curDepth = -1);
				return;
			}
			if (this.curNode.Input != null)
			{
				this.curNode = (DoubleLinkAxis)this.curNode.Input;
				this.curDepth--;
				return;
			}
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000AAF88 File Offset: 0x000A9188
		internal bool MoveToChild(string name, string URN, int depth, ForwardAxis parent)
		{
			if (Asttree.IsAttribute(this.curNode))
			{
				return false;
			}
			if (this.isMatch)
			{
				this.isMatch = false;
			}
			if (!AxisStack.Equal(this.curNode.Name, this.curNode.Urn, name, URN))
			{
				return false;
			}
			if (this.curDepth == -1)
			{
				this.SetDepth(depth);
			}
			else if (depth > this.curDepth)
			{
				return false;
			}
			if (this.curNode == parent.TopNode)
			{
				this.isMatch = true;
				return true;
			}
			DoubleLinkAxis ast = (DoubleLinkAxis)this.curNode.Next;
			if (Asttree.IsAttribute(ast))
			{
				this.isMatch = true;
				return false;
			}
			this.curNode = ast;
			this.curDepth++;
			return false;
		}

		// Token: 0x04000D4F RID: 3407
		internal DoubleLinkAxis curNode;

		// Token: 0x04000D50 RID: 3408
		internal int rootDepth;

		// Token: 0x04000D51 RID: 3409
		internal int curDepth;

		// Token: 0x04000D52 RID: 3410
		internal bool isMatch;
	}
}

using System;

namespace System.Xml.Schema
{
	// Token: 0x0200017D RID: 381
	internal class AxisElement
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00056B44 File Offset: 0x00055B44
		internal DoubleLinkAxis CurNode
		{
			get
			{
				return this.curNode;
			}
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00056B4C File Offset: 0x00055B4C
		internal AxisElement(DoubleLinkAxis node, int depth)
		{
			this.curNode = node;
			this.curDepth = depth;
			this.rootDepth = depth;
			this.isMatch = false;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00056B80 File Offset: 0x00055B80
		internal void SetDepth(int depth)
		{
			this.curDepth = depth;
			this.rootDepth = depth;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00056BA0 File Offset: 0x00055BA0
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
			}
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00056C3C File Offset: 0x00055C3C
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

		// Token: 0x04000C55 RID: 3157
		internal DoubleLinkAxis curNode;

		// Token: 0x04000C56 RID: 3158
		internal int rootDepth;

		// Token: 0x04000C57 RID: 3159
		internal int curDepth;

		// Token: 0x04000C58 RID: 3160
		internal bool isMatch;
	}
}

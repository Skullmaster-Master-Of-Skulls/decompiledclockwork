using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001D8 RID: 472
	internal class AxisStack
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x000AB041 File Offset: 0x000A9241
		internal ForwardAxis Subtree
		{
			get
			{
				return this.subtree;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000AB049 File Offset: 0x000A9249
		internal int Length
		{
			get
			{
				return this.stack.Count;
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000AB056 File Offset: 0x000A9256
		public AxisStack(ForwardAxis faxis, ActiveAxis parent)
		{
			this.subtree = faxis;
			this.stack = new ArrayList();
			this.parent = parent;
			if (!faxis.IsDss)
			{
				this.Push(1);
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000AB088 File Offset: 0x000A9288
		internal void Push(int depth)
		{
			AxisElement value = new AxisElement(this.subtree.RootNode, depth);
			this.stack.Add(value);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000AB0B4 File Offset: 0x000A92B4
		internal void Pop()
		{
			this.stack.RemoveAt(this.Length - 1);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000AB0C9 File Offset: 0x000A92C9
		internal static bool Equal(string thisname, string thisURN, string name, string URN)
		{
			if (thisURN == null)
			{
				if (URN != null && URN.Length != 0)
				{
					return false;
				}
			}
			else if (thisURN.Length != 0 && thisURN != URN)
			{
				return false;
			}
			return thisname.Length == 0 || !(thisname != name);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000AB104 File Offset: 0x000A9304
		internal void MoveToParent(string name, string URN, int depth)
		{
			if (this.subtree.IsSelfAxis)
			{
				return;
			}
			for (int i = 0; i < this.stack.Count; i++)
			{
				((AxisElement)this.stack[i]).MoveToParent(depth, this.subtree);
			}
			if (this.subtree.IsDss && AxisStack.Equal(this.subtree.RootNode.Name, this.subtree.RootNode.Urn, name, URN))
			{
				this.Pop();
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000AB190 File Offset: 0x000A9390
		internal bool MoveToChild(string name, string URN, int depth)
		{
			bool result = false;
			if (this.subtree.IsDss && AxisStack.Equal(this.subtree.RootNode.Name, this.subtree.RootNode.Urn, name, URN))
			{
				this.Push(-1);
			}
			for (int i = 0; i < this.stack.Count; i++)
			{
				if (((AxisElement)this.stack[i]).MoveToChild(name, URN, depth, this.subtree))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000AB218 File Offset: 0x000A9418
		internal bool MoveToAttribute(string name, string URN, int depth)
		{
			if (!this.subtree.IsAttribute)
			{
				return false;
			}
			if (!AxisStack.Equal(this.subtree.TopNode.Name, this.subtree.TopNode.Urn, name, URN))
			{
				return false;
			}
			bool result = false;
			if (this.subtree.TopNode.Input == null)
			{
				return this.subtree.IsDss || depth == 1;
			}
			for (int i = 0; i < this.stack.Count; i++)
			{
				AxisElement axisElement = (AxisElement)this.stack[i];
				if (axisElement.isMatch && axisElement.CurNode == this.subtree.TopNode.Input)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000D53 RID: 3411
		private ArrayList stack;

		// Token: 0x04000D54 RID: 3412
		private ForwardAxis subtree;

		// Token: 0x04000D55 RID: 3413
		private ActiveAxis parent;
	}
}

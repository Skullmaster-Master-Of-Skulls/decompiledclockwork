using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200017E RID: 382
	internal class AxisStack
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00056CF5 File Offset: 0x00055CF5
		internal ForwardAxis Subtree
		{
			get
			{
				return this.subtree;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00056CFD File Offset: 0x00055CFD
		internal int Length
		{
			get
			{
				return this.stack.Count;
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00056D0A File Offset: 0x00055D0A
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

		// Token: 0x06001451 RID: 5201 RVA: 0x00056D3C File Offset: 0x00055D3C
		internal void Push(int depth)
		{
			AxisElement value = new AxisElement(this.subtree.RootNode, depth);
			this.stack.Add(value);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00056D68 File Offset: 0x00055D68
		internal void Pop()
		{
			this.stack.RemoveAt(this.Length - 1);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00056D7D File Offset: 0x00055D7D
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

		// Token: 0x06001454 RID: 5204 RVA: 0x00056DB8 File Offset: 0x00055DB8
		internal void MoveToParent(string name, string URN, int depth)
		{
			if (this.subtree.IsSelfAxis)
			{
				return;
			}
			foreach (object obj in this.stack)
			{
				AxisElement axisElement = (AxisElement)obj;
				axisElement.MoveToParent(depth, this.subtree);
			}
			if (this.subtree.IsDss && AxisStack.Equal(this.subtree.RootNode.Name, this.subtree.RootNode.Urn, name, URN))
			{
				this.Pop();
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00056E64 File Offset: 0x00055E64
		internal bool MoveToChild(string name, string URN, int depth)
		{
			bool result = false;
			if (this.subtree.IsDss && AxisStack.Equal(this.subtree.RootNode.Name, this.subtree.RootNode.Urn, name, URN))
			{
				this.Push(-1);
			}
			foreach (object obj in this.stack)
			{
				AxisElement axisElement = (AxisElement)obj;
				if (axisElement.MoveToChild(name, URN, depth, this.subtree))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00056F0C File Offset: 0x00055F0C
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
			foreach (object obj in this.stack)
			{
				AxisElement axisElement = (AxisElement)obj;
				if (axisElement.isMatch && axisElement.CurNode == this.subtree.TopNode.Input)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000C59 RID: 3161
		private ArrayList stack;

		// Token: 0x04000C5A RID: 3162
		private ForwardAxis subtree;

		// Token: 0x04000C5B RID: 3163
		private ActiveAxis parent;
	}
}

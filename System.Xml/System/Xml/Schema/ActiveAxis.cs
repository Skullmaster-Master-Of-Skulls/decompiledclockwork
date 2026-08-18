using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200017F RID: 383
	internal class ActiveAxis
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00056FE4 File Offset: 0x00055FE4
		public int CurrentDepth
		{
			get
			{
				return this.currentDepth;
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00056FEC File Offset: 0x00055FEC
		internal void Reactivate()
		{
			this.isActive = true;
			this.currentDepth = -1;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00056FFC File Offset: 0x00055FFC
		internal ActiveAxis(Asttree axisTree)
		{
			this.axisTree = axisTree;
			this.currentDepth = -1;
			this.axisStack = new ArrayList(axisTree.SubtreeArray.Count);
			foreach (object obj in axisTree.SubtreeArray)
			{
				ForwardAxis faxis = (ForwardAxis)obj;
				AxisStack value = new AxisStack(faxis, this);
				this.axisStack.Add(value);
			}
			this.isActive = true;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00057094 File Offset: 0x00056094
		public bool MoveToStartElement(string localname, string URN)
		{
			if (!this.isActive)
			{
				return false;
			}
			this.currentDepth++;
			bool result = false;
			foreach (object obj in this.axisStack)
			{
				AxisStack axisStack = (AxisStack)obj;
				if (axisStack.Subtree.IsSelfAxis)
				{
					if (axisStack.Subtree.IsDss || this.CurrentDepth == 0)
					{
						result = true;
					}
				}
				else if (this.CurrentDepth != 0 && axisStack.MoveToChild(localname, URN, this.currentDepth))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00057144 File Offset: 0x00056144
		public virtual bool EndElement(string localname, string URN)
		{
			if (this.currentDepth == 0)
			{
				this.isActive = false;
				this.currentDepth--;
			}
			if (!this.isActive)
			{
				return false;
			}
			foreach (object obj in this.axisStack)
			{
				AxisStack axisStack = (AxisStack)obj;
				axisStack.MoveToParent(localname, URN, this.currentDepth);
			}
			this.currentDepth--;
			return false;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000571DC File Offset: 0x000561DC
		public bool MoveToAttribute(string localname, string URN)
		{
			if (!this.isActive)
			{
				return false;
			}
			bool result = false;
			foreach (object obj in this.axisStack)
			{
				AxisStack axisStack = (AxisStack)obj;
				if (axisStack.MoveToAttribute(localname, URN, this.currentDepth + 1))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000C5C RID: 3164
		private int currentDepth;

		// Token: 0x04000C5D RID: 3165
		private bool isActive;

		// Token: 0x04000C5E RID: 3166
		private Asttree axisTree;

		// Token: 0x04000C5F RID: 3167
		private ArrayList axisStack;
	}
}

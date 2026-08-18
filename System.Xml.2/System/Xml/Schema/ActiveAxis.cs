using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001D9 RID: 473
	internal class ActiveAxis
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x000AB2D2 File Offset: 0x000A94D2
		public int CurrentDepth
		{
			get
			{
				return this.currentDepth;
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000AB2DA File Offset: 0x000A94DA
		internal void Reactivate()
		{
			this.isActive = true;
			this.currentDepth = -1;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000AB2EC File Offset: 0x000A94EC
		internal ActiveAxis(Asttree axisTree)
		{
			this.axisTree = axisTree;
			this.currentDepth = -1;
			this.axisStack = new ArrayList(axisTree.SubtreeArray.Count);
			for (int i = 0; i < axisTree.SubtreeArray.Count; i++)
			{
				AxisStack value = new AxisStack((ForwardAxis)axisTree.SubtreeArray[i], this);
				this.axisStack.Add(value);
			}
			this.isActive = true;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x000AB368 File Offset: 0x000A9568
		public bool MoveToStartElement(string localname, string URN)
		{
			if (!this.isActive)
			{
				return false;
			}
			this.currentDepth++;
			bool result = false;
			for (int i = 0; i < this.axisStack.Count; i++)
			{
				AxisStack axisStack = (AxisStack)this.axisStack[i];
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

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000AB3F8 File Offset: 0x000A95F8
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
			for (int i = 0; i < this.axisStack.Count; i++)
			{
				((AxisStack)this.axisStack[i]).MoveToParent(localname, URN, this.currentDepth);
			}
			this.currentDepth--;
			return false;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000AB470 File Offset: 0x000A9670
		public bool MoveToAttribute(string localname, string URN)
		{
			if (!this.isActive)
			{
				return false;
			}
			bool result = false;
			for (int i = 0; i < this.axisStack.Count; i++)
			{
				if (((AxisStack)this.axisStack[i]).MoveToAttribute(localname, URN, this.currentDepth + 1))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000D56 RID: 3414
		private int currentDepth;

		// Token: 0x04000D57 RID: 3415
		private bool isActive;

		// Token: 0x04000D58 RID: 3416
		private Asttree axisTree;

		// Token: 0x04000D59 RID: 3417
		private ArrayList axisStack;
	}
}

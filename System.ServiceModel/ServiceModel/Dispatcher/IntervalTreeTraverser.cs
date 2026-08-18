using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A9 RID: 1193
	internal struct IntervalTreeTraverser
	{
		// Token: 0x06002DAC RID: 11692 RVA: 0x000B22A0 File Offset: 0x000B04A0
		internal IntervalTreeTraverser(double val, IntervalBoundary root)
		{
			this.currentNode = null;
			this.slot = null;
			this.nextNode = root;
			this.val = val;
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000B22BE File Offset: 0x000B04BE
		internal IntervalCollection Slot
		{
			get
			{
				return this.slot;
			}
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000B22C8 File Offset: 0x000B04C8
		internal bool MoveNext()
		{
			while (this.nextNode != null)
			{
				this.currentNode = this.nextNode;
				double value = this.currentNode.Value;
				if (this.val < value)
				{
					this.slot = this.currentNode.LtSlot;
					this.nextNode = this.currentNode.Left;
				}
				else if (this.val > value)
				{
					this.slot = this.currentNode.GtSlot;
					this.nextNode = this.currentNode.Right;
				}
				else
				{
					this.slot = this.currentNode.EqSlot;
					this.nextNode = null;
				}
				if (this.slot != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040024DD RID: 9437
		private IntervalBoundary currentNode;

		// Token: 0x040024DE RID: 9438
		private IntervalBoundary nextNode;

		// Token: 0x040024DF RID: 9439
		private IntervalCollection slot;

		// Token: 0x040024E0 RID: 9440
		private double val;
	}
}

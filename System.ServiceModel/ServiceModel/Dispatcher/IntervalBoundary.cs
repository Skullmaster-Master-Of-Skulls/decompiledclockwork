using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A8 RID: 1192
	internal class IntervalBoundary
	{
		// Token: 0x06002D95 RID: 11669 RVA: 0x000B20FD File Offset: 0x000B02FD
		internal IntervalBoundary(double val, IntervalBoundary parent)
		{
			this.val = val;
			this.parent = parent;
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000B2113 File Offset: 0x000B0313
		internal IntervalCollection EqSlot
		{
			get
			{
				return this.eqSlot;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000B211B File Offset: 0x000B031B
		internal IntervalCollection GtSlot
		{
			get
			{
				return this.gtSlot;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000B2123 File Offset: 0x000B0323
		// (set) Token: 0x06002D99 RID: 11673 RVA: 0x000B212B File Offset: 0x000B032B
		internal IntervalBoundary Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000B2134 File Offset: 0x000B0334
		internal IntervalCollection LtSlot
		{
			get
			{
				return this.ltSlot;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x000B213C File Offset: 0x000B033C
		// (set) Token: 0x06002D9C RID: 11676 RVA: 0x000B2144 File Offset: 0x000B0344
		internal IntervalBoundary Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x000B214D File Offset: 0x000B034D
		// (set) Token: 0x06002D9E RID: 11678 RVA: 0x000B2155 File Offset: 0x000B0355
		internal IntervalBoundary Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000B215E File Offset: 0x000B035E
		// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x000B2166 File Offset: 0x000B0366
		internal double Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000B216F File Offset: 0x000B036F
		internal void AddToEqSlot(Interval interval)
		{
			this.AddToSlot(ref this.eqSlot, interval);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B217E File Offset: 0x000B037E
		internal void AddToGtSlot(Interval interval)
		{
			this.AddToSlot(ref this.gtSlot, interval);
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B218D File Offset: 0x000B038D
		internal void AddToLtSlot(Interval interval)
		{
			this.AddToSlot(ref this.ltSlot, interval);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000B219C File Offset: 0x000B039C
		private void AddToSlot(ref IntervalCollection slot, Interval interval)
		{
			if (slot == null)
			{
				slot = new IntervalCollection();
			}
			slot.AddUnique(interval);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B21B2 File Offset: 0x000B03B2
		internal IntervalBoundary EnsureLeft(double val)
		{
			if (this.left == null)
			{
				this.left = new IntervalBoundary(val, this);
			}
			return this.left;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000B21CF File Offset: 0x000B03CF
		internal IntervalBoundary EnsureRight(double val)
		{
			if (this.right == null)
			{
				this.right = new IntervalBoundary(val, this);
			}
			return this.right;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000B21EC File Offset: 0x000B03EC
		internal void RemoveFromEqSlot(Interval interval)
		{
			this.RemoveFromSlot(ref this.eqSlot, interval);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000B21FB File Offset: 0x000B03FB
		internal void RemoveFromGtSlot(Interval interval)
		{
			this.RemoveFromSlot(ref this.gtSlot, interval);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000B220A File Offset: 0x000B040A
		internal void RemoveFromLtSlot(Interval interval)
		{
			this.RemoveFromSlot(ref this.ltSlot, interval);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B2219 File Offset: 0x000B0419
		private void RemoveFromSlot(ref IntervalCollection slot, Interval interval)
		{
			if (slot != null)
			{
				slot.Remove(interval);
				if (!slot.HasIntervals)
				{
					slot = null;
				}
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000B2234 File Offset: 0x000B0434
		internal void Trim()
		{
			if (this.eqSlot != null)
			{
				this.eqSlot.Trim();
			}
			if (this.gtSlot != null)
			{
				this.gtSlot.Trim();
			}
			if (this.ltSlot != null)
			{
				this.ltSlot.Trim();
			}
			if (this.left != null)
			{
				this.left.Trim();
			}
			if (this.right != null)
			{
				this.right.Trim();
			}
		}

		// Token: 0x040024D6 RID: 9430
		private IntervalCollection eqSlot;

		// Token: 0x040024D7 RID: 9431
		private IntervalCollection gtSlot;

		// Token: 0x040024D8 RID: 9432
		private IntervalBoundary left;

		// Token: 0x040024D9 RID: 9433
		private IntervalCollection ltSlot;

		// Token: 0x040024DA RID: 9434
		private IntervalBoundary parent;

		// Token: 0x040024DB RID: 9435
		private IntervalBoundary right;

		// Token: 0x040024DC RID: 9436
		private double val;
	}
}

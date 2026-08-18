using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004AA RID: 1194
	internal class IntervalTree
	{
		// Token: 0x06002DAF RID: 11695 RVA: 0x000B237A File Offset: 0x000B057A
		internal IntervalTree()
		{
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000B2382 File Offset: 0x000B0582
		internal int Count
		{
			get
			{
				if (this.intervals == null)
				{
					return 0;
				}
				return this.intervals.Count;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000B2399 File Offset: 0x000B0599
		internal IntervalCollection Intervals
		{
			get
			{
				if (this.intervals == null)
				{
					return new IntervalCollection();
				}
				return this.intervals;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000B23AF File Offset: 0x000B05AF
		internal IntervalBoundary Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000B23B7 File Offset: 0x000B05B7
		internal void Add(Interval interval)
		{
			this.AddIntervalToTree(interval);
			this.EnsureIntervals();
			this.intervals.Add(interval);
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000B23D3 File Offset: 0x000B05D3
		private void AddIntervalToTree(Interval interval)
		{
			this.EditLeft(interval, true);
			this.EditRight(interval, true);
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000B23E8 File Offset: 0x000B05E8
		private void EditLeft(Interval interval, bool add)
		{
			if (add)
			{
				this.EnsureRoot(interval.LowerBound);
			}
			IntervalBoundary intervalBoundary = this.root;
			IntervalBoundary intervalBoundary2 = null;
			for (;;)
			{
				double value = intervalBoundary.Value;
				if (value < interval.LowerBound)
				{
					intervalBoundary = (add ? intervalBoundary.EnsureRight(interval.LowerBound) : intervalBoundary.Right);
				}
				else
				{
					if (intervalBoundary2 != null && intervalBoundary2.Value <= interval.UpperBound)
					{
						if (add)
						{
							intervalBoundary.AddToGtSlot(interval);
						}
						else
						{
							intervalBoundary.RemoveFromGtSlot(interval);
						}
					}
					if (value <= interval.LowerBound)
					{
						break;
					}
					if (value < interval.UpperBound)
					{
						if (add)
						{
							intervalBoundary.AddToEqSlot(interval);
						}
						else
						{
							intervalBoundary.RemoveFromEqSlot(interval);
						}
					}
					intervalBoundary2 = intervalBoundary;
					intervalBoundary = (add ? intervalBoundary.EnsureLeft(interval.LowerBound) : intervalBoundary.Left);
				}
			}
			if (IntervalOp.LessThanEquals == interval.LowerOp)
			{
				if (add)
				{
					intervalBoundary.AddToEqSlot(interval);
					return;
				}
				intervalBoundary.RemoveFromEqSlot(interval);
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000B24BC File Offset: 0x000B06BC
		private void EditRight(Interval interval, bool add)
		{
			if (add)
			{
				this.EnsureRoot(interval.UpperBound);
			}
			IntervalBoundary intervalBoundary = this.root;
			IntervalBoundary intervalBoundary2 = null;
			for (;;)
			{
				double value = intervalBoundary.Value;
				if (value > interval.UpperBound)
				{
					intervalBoundary = (add ? intervalBoundary.EnsureLeft(interval.UpperBound) : intervalBoundary.Left);
				}
				else
				{
					if (intervalBoundary2 != null && intervalBoundary2.Value >= interval.LowerBound)
					{
						if (add)
						{
							intervalBoundary.AddToLtSlot(interval);
						}
						else
						{
							intervalBoundary.RemoveFromLtSlot(interval);
						}
					}
					if (value >= interval.UpperBound)
					{
						break;
					}
					if (value > interval.LowerBound)
					{
						if (add)
						{
							intervalBoundary.AddToEqSlot(interval);
						}
						else
						{
							intervalBoundary.RemoveFromEqSlot(interval);
						}
					}
					intervalBoundary2 = intervalBoundary;
					intervalBoundary = (add ? intervalBoundary.EnsureRight(interval.UpperBound) : intervalBoundary.Right);
				}
			}
			if (IntervalOp.LessThanEquals == interval.UpperOp)
			{
				if (add)
				{
					intervalBoundary.AddToEqSlot(interval);
					return;
				}
				intervalBoundary.RemoveFromEqSlot(interval);
			}
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000B258E File Offset: 0x000B078E
		private void EnsureIntervals()
		{
			if (this.intervals == null)
			{
				this.intervals = new IntervalCollection();
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000B25A3 File Offset: 0x000B07A3
		private void EnsureRoot(double val)
		{
			if (this.root == null)
			{
				this.root = new IntervalBoundary(val, null);
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000B25BA File Offset: 0x000B07BA
		internal IntervalBoundary FindBoundaryNode(double val)
		{
			return this.FindBoundaryNode(this.root, val);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000B25CC File Offset: 0x000B07CC
		internal IntervalBoundary FindBoundaryNode(IntervalBoundary root, double val)
		{
			IntervalBoundary result = null;
			if (root != null)
			{
				if (root.Value == val)
				{
					result = root;
				}
				else
				{
					result = (this.FindBoundaryNode(root.Left, val) ?? this.FindBoundaryNode(root.Right, val));
				}
			}
			return result;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000B260B File Offset: 0x000B080B
		internal Interval FindInterval(Interval interval)
		{
			return this.FindInterval(interval.LowerBound, interval.LowerOp, interval.UpperBound, interval.UpperOp);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000B262C File Offset: 0x000B082C
		internal Interval FindInterval(double lowerBound, IntervalOp lowerOp, double upperBound, IntervalOp upperOp)
		{
			int index;
			if (this.intervals != null && -1 != (index = this.intervals.IndexOf(lowerBound, lowerOp, upperBound, upperOp)))
			{
				return this.intervals[index];
			}
			return null;
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000B2664 File Offset: 0x000B0864
		private void PruneTree(Interval intervalRemoved)
		{
			if (-1 == this.intervals.IndexOf(intervalRemoved.LowerBound))
			{
				this.RemoveBoundary(this.FindBoundaryNode(intervalRemoved.LowerBound));
			}
			if (intervalRemoved.LowerBound != intervalRemoved.UpperBound && -1 == this.intervals.IndexOf(intervalRemoved.UpperBound))
			{
				this.RemoveBoundary(this.FindBoundaryNode(intervalRemoved.UpperBound));
			}
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000B26CF File Offset: 0x000B08CF
		internal void Remove(Interval interval)
		{
			this.RemoveIntervalFromTree(interval);
			this.intervals.Remove(interval);
			this.PruneTree(interval);
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000B26EC File Offset: 0x000B08EC
		private void RemoveBoundary(IntervalBoundary boundary)
		{
			IntervalCollection intervalCollection = null;
			int num = 0;
			if (boundary.Left != null && boundary.Right != null)
			{
				IntervalBoundary intervalBoundary = boundary.Left;
				while (intervalBoundary.Right != null)
				{
					intervalBoundary = intervalBoundary.Right;
				}
				intervalCollection = this.intervals.GetIntervalsWithEndPoint(intervalBoundary.Value);
				num = intervalCollection.Count;
				for (int i = 0; i < num; i++)
				{
					this.RemoveIntervalFromTree(intervalCollection[i]);
				}
				double value = boundary.Value;
				boundary.Value = intervalBoundary.Value;
				intervalBoundary.Value = value;
				boundary = intervalBoundary;
			}
			if (boundary.Left != null)
			{
				this.Replace(boundary, boundary.Left);
			}
			else
			{
				this.Replace(boundary, boundary.Right);
			}
			boundary.Parent = null;
			boundary.Left = null;
			boundary.Right = null;
			for (int j = 0; j < num; j++)
			{
				this.AddIntervalToTree(intervalCollection[j]);
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B27D0 File Offset: 0x000B09D0
		private void RemoveIntervalFromTree(Interval interval)
		{
			this.EditLeft(interval, false);
			this.EditRight(interval, false);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B27E4 File Offset: 0x000B09E4
		private void Replace(IntervalBoundary replace, IntervalBoundary with)
		{
			IntervalBoundary parent = replace.Parent;
			if (parent != null)
			{
				if (replace == parent.Left)
				{
					parent.Left = with;
				}
				else if (replace == parent.Right)
				{
					parent.Right = with;
				}
			}
			else
			{
				this.root = with;
			}
			if (with != null)
			{
				with.Parent = parent;
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B2830 File Offset: 0x000B0A30
		internal void Trim()
		{
			this.intervals.Trim();
			this.root.Trim();
		}

		// Token: 0x040024E1 RID: 9441
		private IntervalCollection intervals;

		// Token: 0x040024E2 RID: 9442
		private IntervalBoundary root;
	}
}

using System;

namespace Telerik.Charting
{
	// Token: 0x02001724 RID: 5924
	public class AxisSegmentCollection : ChartingStateManagedCollection<AxisSegment>
	{
		// Token: 0x17004615 RID: 17941
		// (get) Token: 0x0600E620 RID: 58912 RVA: 0x00332B24 File Offset: 0x00330D24
		internal bool IsHaveNegative
		{
			get
			{
				foreach (AxisSegment axisSegment in this)
				{
					if (axisSegment.MaxValue < 0.0)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17004616 RID: 17942
		// (get) Token: 0x0600E621 RID: 58913 RVA: 0x00332B80 File Offset: 0x00330D80
		internal bool IsHavePositive
		{
			get
			{
				foreach (AxisSegment axisSegment in this)
				{
					if (axisSegment.MinValue >= 0.0)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17004617 RID: 17943
		// (get) Token: 0x0600E622 RID: 58914 RVA: 0x00332BDC File Offset: 0x00330DDC
		internal bool IsHaveZero
		{
			get
			{
				foreach (AxisSegment axisSegment in this)
				{
					if (axisSegment.MinValue <= 0.0 && axisSegment.MaxValue >= 0.0)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17004618 RID: 17944
		// (get) Token: 0x0600E623 RID: 58915 RVA: 0x00332C48 File Offset: 0x00330E48
		internal double NearZeroValue
		{
			get
			{
				if (this.IsHaveZero)
				{
					return 0.0;
				}
				double num = double.MaxValue;
				foreach (AxisSegment axisSegment in this)
				{
					double num2 = double.MaxValue;
					if (axisSegment.MinValue > 0.0 && axisSegment.MaxValue > 0.0)
					{
						num2 = axisSegment.MinValue;
					}
					if (axisSegment.MaxValue < 0.0 && axisSegment.MinValue < 0.0)
					{
						num2 = -axisSegment.MaxValue;
					}
					if (num2 < num)
					{
						num = num2;
					}
				}
				return num;
			}
		}

		// Token: 0x0600E624 RID: 58916 RVA: 0x00332D0C File Offset: 0x00330F0C
		internal void CheckedAdd(AxisSegment segment)
		{
			if (segment.axisSegmentItemsCount > 0 && segment.MaxValue != segment.MinValue)
			{
				base.Add(segment);
			}
		}

		// Token: 0x0600E625 RID: 58917 RVA: 0x00332D2C File Offset: 0x00330F2C
		internal AxisSegment Search(double value)
		{
			foreach (AxisSegment axisSegment in this)
			{
				if (axisSegment.MaxValue >= value && axisSegment.MinValue <= value)
				{
					return axisSegment;
				}
			}
			return null;
		}

		// Token: 0x0600E626 RID: 58918 RVA: 0x00332D88 File Offset: 0x00330F88
		internal AxisSegment Search(double value, bool withoutNull)
		{
			AxisSegment axisSegment = this.Search(value);
			if (!withoutNull)
			{
				return axisSegment;
			}
			if (axisSegment != null)
			{
				return axisSegment;
			}
			for (int i = base.Count - 1; i >= 0; i--)
			{
				if (this[i].MinValue <= value)
				{
					axisSegment = this[i];
				}
			}
			return axisSegment;
		}

		// Token: 0x0600E627 RID: 58919 RVA: 0x00332DD4 File Offset: 0x00330FD4
		internal void Sort()
		{
			AxisSegment[] array = new AxisSegment[base.Count];
			int i = 0;
			foreach (AxisSegment axisSegment in this)
			{
				array[i++] = axisSegment;
			}
			AxisSegmentComparer comparer = new AxisSegmentComparer();
			Array.Sort(array, comparer);
			base.Clear();
			for (i = 0; i < array.Length; i++)
			{
				this.Add(array[i]);
			}
		}

		// Token: 0x0600E628 RID: 58920 RVA: 0x00332E5C File Offset: 0x0033105C
		internal bool Test(ChartSeriesItemsCollection items)
		{
			foreach (ChartSeriesItem chartSeriesItem in items)
			{
				if (this.Search(chartSeriesItem.YValue) == null)
				{
					return false;
				}
			}
			return true;
		}
	}
}

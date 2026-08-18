using System;
using System.Collections;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A7 RID: 1191
	internal class IntervalCollection : ArrayList
	{
		// Token: 0x06002D8A RID: 11658 RVA: 0x000B1FD1 File Offset: 0x000B01D1
		internal IntervalCollection() : base(1)
		{
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000B1FDA File Offset: 0x000B01DA
		internal bool HasIntervals
		{
			get
			{
				return this.Count > 0;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		internal Interval this[int index]
		{
			get
			{
				return (Interval)base[index];
			}
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000B1FF3 File Offset: 0x000B01F3
		internal int Add(Interval interval)
		{
			this.Capacity = this.Count + 1;
			return base.Add(interval);
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000B200C File Offset: 0x000B020C
		internal int AddUnique(Interval interval)
		{
			int num = this.IndexOf(interval);
			if (-1 == num)
			{
				return this.Add(interval);
			}
			return num;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000B2030 File Offset: 0x000B0230
		internal IntervalCollection GetIntervalsWithEndPoint(double endPoint)
		{
			IntervalCollection intervalCollection = new IntervalCollection();
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				Interval interval = this[i];
				if (interval.HasMatchingEndPoint(endPoint))
				{
					intervalCollection.Add(interval);
				}
			}
			return intervalCollection;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000B2070 File Offset: 0x000B0270
		internal int IndexOf(Interval interval)
		{
			return base.IndexOf(interval);
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000B207C File Offset: 0x000B027C
		internal int IndexOf(double endPoint)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				Interval interval = this[i];
				if (interval.HasMatchingEndPoint(endPoint))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000B20B0 File Offset: 0x000B02B0
		internal int IndexOf(double lowerBound, IntervalOp lowerOp, double upperBound, IntervalOp upperOp)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				if (this[i].Equals(lowerBound, lowerOp, upperBound, upperOp))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000B20E6 File Offset: 0x000B02E6
		internal void Remove(Interval interval)
		{
			base.Remove(interval);
			this.TrimToSize();
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000B20F5 File Offset: 0x000B02F5
		internal void Trim()
		{
			this.TrimToSize();
		}
	}
}

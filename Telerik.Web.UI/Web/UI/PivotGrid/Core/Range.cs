using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CDC RID: 3292
	internal class Range<T>
	{
		// Token: 0x06007B0B RID: 31499 RVA: 0x001C42D9 File Offset: 0x001C24D9
		public Range(int lowerBound, int upperBound, T value)
		{
			this.LowerBound = lowerBound;
			this.UpperBound = upperBound;
			this.Value = value;
		}

		// Token: 0x17002762 RID: 10082
		// (get) Token: 0x06007B0C RID: 31500 RVA: 0x001C42F6 File Offset: 0x001C24F6
		public int Count
		{
			get
			{
				return this.UpperBound - this.LowerBound + 1;
			}
		}

		// Token: 0x17002763 RID: 10083
		// (get) Token: 0x06007B0D RID: 31501 RVA: 0x001C4307 File Offset: 0x001C2507
		// (set) Token: 0x06007B0E RID: 31502 RVA: 0x001C430F File Offset: 0x001C250F
		public int LowerBound { get; set; }

		// Token: 0x17002764 RID: 10084
		// (get) Token: 0x06007B0F RID: 31503 RVA: 0x001C4318 File Offset: 0x001C2518
		// (set) Token: 0x06007B10 RID: 31504 RVA: 0x001C4320 File Offset: 0x001C2520
		public int UpperBound { get; set; }

		// Token: 0x17002765 RID: 10085
		// (get) Token: 0x06007B11 RID: 31505 RVA: 0x001C4329 File Offset: 0x001C2529
		// (set) Token: 0x06007B12 RID: 31506 RVA: 0x001C4331 File Offset: 0x001C2531
		public T Value { get; set; }

		// Token: 0x06007B13 RID: 31507 RVA: 0x001C433A File Offset: 0x001C253A
		public bool ContainsIndex(int index)
		{
			return this.LowerBound <= index && this.UpperBound >= index;
		}

		// Token: 0x06007B14 RID: 31508 RVA: 0x001C4354 File Offset: 0x001C2554
		public bool ContainsValue(object value)
		{
			if (this.Value == null)
			{
				return value == null;
			}
			T value2 = this.Value;
			return value2.Equals(value);
		}

		// Token: 0x06007B15 RID: 31509 RVA: 0x001C4388 File Offset: 0x001C2588
		public Range<T> Copy()
		{
			return new Range<T>(this.LowerBound, this.UpperBound, this.Value);
		}
	}
}

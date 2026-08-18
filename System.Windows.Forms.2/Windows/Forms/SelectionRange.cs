using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000367 RID: 871
	[TypeConverter(typeof(SelectionRangeConverter))]
	public sealed class SelectionRange
	{
		// Token: 0x0600388C RID: 14476 RVA: 0x000FAB28 File Offset: 0x000F8D28
		public SelectionRange()
		{
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000FAB64 File Offset: 0x000F8D64
		public SelectionRange(DateTime lower, DateTime upper)
		{
			if (lower < upper)
			{
				this.start = lower.Date;
				this.end = upper.Date;
				return;
			}
			this.start = upper.Date;
			this.end = lower.Date;
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000FABDC File Offset: 0x000F8DDC
		public SelectionRange(SelectionRange range)
		{
			this.start = range.start;
			this.end = range.end;
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000FAC2D File Offset: 0x000F8E2D
		// (set) Token: 0x06003890 RID: 14480 RVA: 0x000FAC35 File Offset: 0x000F8E35
		public DateTime End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value.Date;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000FAC44 File Offset: 0x000F8E44
		// (set) Token: 0x06003892 RID: 14482 RVA: 0x000FAC4C File Offset: 0x000F8E4C
		public DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value.Date;
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000FAC5B File Offset: 0x000F8E5B
		public override string ToString()
		{
			return "SelectionRange: Start: " + this.start.ToString() + ", End: " + this.end.ToString();
		}

		// Token: 0x040021D9 RID: 8665
		private DateTime start = DateTime.MinValue.Date;

		// Token: 0x040021DA RID: 8666
		private DateTime end = DateTime.MaxValue.Date;
	}
}

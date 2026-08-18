using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000068 RID: 104
	public class ExtendedPathFilter : PathFilter
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x00016617 File Offset: 0x00015617
		public ExtendedPathFilter(string filter, long minSize, long maxSize) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00016653 File Offset: 0x00015653
		public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate) : base(filter)
		{
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00016690 File Offset: 0x00015690
		public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000166E8 File Offset: 0x000156E8
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			if (flag)
			{
				FileInfo fileInfo = new FileInfo(name);
				flag = (this.MinSize <= fileInfo.Length && this.MaxSize >= fileInfo.Length && this.MinDate <= fileInfo.LastWriteTime && this.MaxDate >= fileInfo.LastWriteTime);
			}
			return flag;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001674C File Offset: 0x0001574C
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00016754 File Offset: 0x00015754
		public long MinSize
		{
			get
			{
				return this.minSize_;
			}
			set
			{
				if (value < 0L || this.maxSize_ < value)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.minSize_ = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00016776 File Offset: 0x00015776
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0001677E File Offset: 0x0001577E
		public long MaxSize
		{
			get
			{
				return this.maxSize_;
			}
			set
			{
				if (value < 0L || this.minSize_ > value)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxSize_ = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000167A0 File Offset: 0x000157A0
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x000167A8 File Offset: 0x000157A8
		public DateTime MinDate
		{
			get
			{
				return this.minDate_;
			}
			set
			{
				if (value > this.maxDate_)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MaxDate");
				}
				this.minDate_ = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000167CF File Offset: 0x000157CF
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x000167D7 File Offset: 0x000157D7
		public DateTime MaxDate
		{
			get
			{
				return this.maxDate_;
			}
			set
			{
				if (this.minDate_ > value)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MinDate");
				}
				this.maxDate_ = value;
			}
		}

		// Token: 0x040002D9 RID: 729
		private long minSize_;

		// Token: 0x040002DA RID: 730
		private long maxSize_ = long.MaxValue;

		// Token: 0x040002DB RID: 731
		private DateTime minDate_ = DateTime.MinValue;

		// Token: 0x040002DC RID: 732
		private DateTime maxDate_ = DateTime.MaxValue;
	}
}

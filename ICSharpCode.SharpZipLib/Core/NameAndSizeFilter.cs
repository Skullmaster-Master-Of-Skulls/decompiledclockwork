using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000069 RID: 105
	[Obsolete("Use ExtendedPathFilter instead")]
	public class NameAndSizeFilter : PathFilter
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x000167FE File Offset: 0x000157FE
		public NameAndSizeFilter(string filter, long minSize, long maxSize) : base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00016824 File Offset: 0x00015824
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			if (flag)
			{
				FileInfo fileInfo = new FileInfo(name);
				long length = fileInfo.Length;
				flag = (this.MinSize <= length && this.MaxSize >= length);
			}
			return flag;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00016864 File Offset: 0x00015864
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0001686C File Offset: 0x0001586C
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001688E File Offset: 0x0001588E
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00016896 File Offset: 0x00015896
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

		// Token: 0x040002DD RID: 733
		private long minSize_;

		// Token: 0x040002DE RID: 734
		private long maxSize_ = long.MaxValue;
	}
}

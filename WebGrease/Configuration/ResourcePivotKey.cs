using System;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x0200002C RID: 44
	public class ResourcePivotKey
	{
		// Token: 0x0600030E RID: 782 RVA: 0x000076FD File Offset: 0x000058FD
		public ResourcePivotKey(string groupKey, string key)
		{
			this.GroupKey = groupKey;
			this.Key = key;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00007713 File Offset: 0x00005913
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000771B File Offset: 0x0000591B
		public string GroupKey { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00007724 File Offset: 0x00005924
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000772C File Offset: 0x0000592C
		public string Key { get; private set; }

		// Token: 0x06000313 RID: 787 RVA: 0x00007738 File Offset: 0x00005938
		public override string ToString()
		{
			return "[{0}:{1}]".InvariantFormat(new object[]
			{
				this.GroupKey,
				this.Key
			});
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000776C File Offset: 0x0000596C
		internal string ToString(string format)
		{
			return format.InvariantFormat(new object[]
			{
				this.GroupKey,
				this.Key
			});
		}
	}
}

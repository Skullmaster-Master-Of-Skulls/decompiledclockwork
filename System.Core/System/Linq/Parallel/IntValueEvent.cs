using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001FD RID: 509
	internal class IntValueEvent : ManualResetEventSlim
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x0003942F File Offset: 0x0003762F
		internal IntValueEvent() : base(false)
		{
			this.Value = 0;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003943F File Offset: 0x0003763F
		internal void Set(int index)
		{
			this.Value = index;
			base.Set();
		}

		// Token: 0x04000930 RID: 2352
		internal int Value;
	}
}

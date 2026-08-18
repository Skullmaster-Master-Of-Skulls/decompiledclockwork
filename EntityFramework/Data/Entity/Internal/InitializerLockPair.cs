using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000785 RID: 1925
	internal class InitializerLockPair : Tuple<Action<DbContext>, bool>
	{
		// Token: 0x06005727 RID: 22311 RVA: 0x001782F3 File Offset: 0x001764F3
		public InitializerLockPair(Action<DbContext> initializerDelegate, bool isLocked) : base(initializerDelegate, isLocked)
		{
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005728 RID: 22312 RVA: 0x001782FD File Offset: 0x001764FD
		public Action<DbContext> InitializerDelegate
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06005729 RID: 22313 RVA: 0x00178305 File Offset: 0x00176505
		public bool IsLocked
		{
			get
			{
				return base.Item2;
			}
		}
	}
}

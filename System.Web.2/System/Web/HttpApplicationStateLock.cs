using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200007D RID: 125
	internal class HttpApplicationStateLock : ReadWriteObjectLock
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x00010A13 File Offset: 0x0000EC13
		internal HttpApplicationStateLock()
		{
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00010A1C File Offset: 0x0000EC1C
		internal override void AcquireRead()
		{
			int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
			if (this._threadId != currentThreadId)
			{
				base.AcquireRead();
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00010A40 File Offset: 0x0000EC40
		internal override void ReleaseRead()
		{
			int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
			if (this._threadId != currentThreadId)
			{
				base.ReleaseRead();
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00010A64 File Offset: 0x0000EC64
		internal override void AcquireWrite()
		{
			int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
			if (this._threadId == currentThreadId)
			{
				this._recursionCount++;
				return;
			}
			base.AcquireWrite();
			this._threadId = currentThreadId;
			this._recursionCount = 1;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		internal override void ReleaseWrite()
		{
			int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
			if (this._threadId == currentThreadId)
			{
				int num = this._recursionCount - 1;
				this._recursionCount = num;
				if (num == 0)
				{
					this._threadId = 0;
					base.ReleaseWrite();
				}
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00010AE0 File Offset: 0x0000ECE0
		internal void EnsureReleaseWrite()
		{
			int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
			if (this._threadId == currentThreadId)
			{
				this._threadId = 0;
				this._recursionCount = 0;
				base.ReleaseWrite();
			}
		}

		// Token: 0x0400028E RID: 654
		private int _recursionCount;

		// Token: 0x0400028F RID: 655
		private int _threadId;
	}
}

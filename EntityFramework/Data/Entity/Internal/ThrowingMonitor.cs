using System;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Data.Entity.Internal
{
	// Token: 0x020002CC RID: 716
	internal class ThrowingMonitor
	{
		// Token: 0x0600194E RID: 6478 RVA: 0x0007E566 File Offset: 0x0007C766
		public void Enter()
		{
			if (Interlocked.CompareExchange(ref this._isInCriticalSection, 1, 0) != 0)
			{
				throw new NotSupportedException(Strings.ConcurrentMethodInvocation);
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0007E582 File Offset: 0x0007C782
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "state", Justification = "Used in the debug build")]
		public void Exit()
		{
			Interlocked.Exchange(ref this._isInCriticalSection, 0);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0007E591 File Offset: 0x0007C791
		public void EnsureNotEntered()
		{
			Thread.MemoryBarrier();
			if (this._isInCriticalSection != 0)
			{
				throw new NotSupportedException(Strings.ConcurrentMethodInvocation);
			}
		}

		// Token: 0x040008AE RID: 2222
		private int _isInCriticalSection;
	}
}

using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000028 RID: 40
	internal class SignalGate
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005D30 File Offset: 0x00003F30
		internal bool IsLocked
		{
			get
			{
				return this.state == 0;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005D3B File Offset: 0x00003F3B
		internal bool IsSignalled
		{
			get
			{
				return this.state == 3;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005D48 File Offset: 0x00003F48
		public bool Signal()
		{
			int num = this.state;
			if (num == 0)
			{
				num = Interlocked.CompareExchange(ref this.state, 1, 0);
			}
			if (num == 2)
			{
				this.state = 3;
				return true;
			}
			if (num != 0)
			{
				this.ThrowInvalidSignalGateState();
			}
			return false;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005D84 File Offset: 0x00003F84
		public bool Unlock()
		{
			int num = this.state;
			if (num == 0)
			{
				num = Interlocked.CompareExchange(ref this.state, 2, 0);
			}
			if (num == 1)
			{
				this.state = 3;
				return true;
			}
			if (num != 0)
			{
				this.ThrowInvalidSignalGateState();
			}
			return false;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005DC0 File Offset: 0x00003FC0
		private void ThrowInvalidSignalGateState()
		{
			throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.InvalidSemaphoreExit));
		}

		// Token: 0x04000098 RID: 152
		private int state;

		// Token: 0x0200007F RID: 127
		private static class GateState
		{
			// Token: 0x04000275 RID: 629
			public const int Locked = 0;

			// Token: 0x04000276 RID: 630
			public const int SignalPending = 1;

			// Token: 0x04000277 RID: 631
			public const int Unlocked = 2;

			// Token: 0x04000278 RID: 632
			public const int Signalled = 3;
		}
	}
}

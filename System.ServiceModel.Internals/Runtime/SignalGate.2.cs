using System;

namespace System.Runtime
{
	// Token: 0x02000029 RID: 41
	internal class SignalGate<T> : SignalGate
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00005DDE File Offset: 0x00003FDE
		public bool Signal(T result)
		{
			this.result = result;
			return base.Signal();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005DED File Offset: 0x00003FED
		public bool Unlock(out T result)
		{
			if (base.Unlock())
			{
				result = this.result;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x04000099 RID: 153
		private T result;
	}
}

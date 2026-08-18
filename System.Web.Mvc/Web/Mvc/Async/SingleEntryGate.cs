using System;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x02000156 RID: 342
	internal sealed class SingleEntryGate
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x0001833C File Offset: 0x0001653C
		public bool TryEnter()
		{
			int num = Interlocked.Exchange(ref this._status, 1);
			return num == 0;
		}

		// Token: 0x04000277 RID: 631
		private const int NotEntered = 0;

		// Token: 0x04000278 RID: 632
		private const int Entered = 1;

		// Token: 0x04000279 RID: 633
		private int _status;
	}
}

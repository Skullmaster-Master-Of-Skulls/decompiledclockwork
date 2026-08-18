using System;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F5 RID: 2293
	internal class SimplePostRollbackErrorStrategy : IPostRollbackErrorStrategy
	{
		// Token: 0x0600576E RID: 22382 RVA: 0x00140F14 File Offset: 0x0013F114
		internal SimplePostRollbackErrorStrategy(long lookupId)
		{
			this.lookupId = lookupId;
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x00140F2C File Offset: 0x0013F12C
		public bool AnotherTryNeeded()
		{
			int num = this.attemptsLeft - 1;
			this.attemptsLeft = num;
			if (num > 0)
			{
				if (this.attemptsLeft == 49)
				{
					MsmqDiagnostics.MessageLockedUnderTheTransaction(this.lookupId);
				}
				Thread.Sleep(TimeSpan.FromMilliseconds(100.0));
				return true;
			}
			MsmqDiagnostics.MoveOrDeleteAttemptFailed(this.lookupId);
			return false;
		}

		// Token: 0x040035C5 RID: 13765
		private const int Attempts = 50;

		// Token: 0x040035C6 RID: 13766
		private const int MillisecondsToSleep = 100;

		// Token: 0x040035C7 RID: 13767
		private int attemptsLeft = 50;

		// Token: 0x040035C8 RID: 13768
		private long lookupId;
	}
}

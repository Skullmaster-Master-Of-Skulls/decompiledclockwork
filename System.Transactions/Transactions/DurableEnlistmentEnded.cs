using System;

namespace System.Transactions
{
	// Token: 0x0200004A RID: 74
	internal class DurableEnlistmentEnded : DurableEnlistmentState
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00031BA4 File Offset: 0x00030FA4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00031BC4 File Offset: 0x00030FC4
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00031BD4 File Offset: 0x00030FD4
		internal override void InDoubt(InternalEnlistment enlistment, Exception e)
		{
		}
	}
}

using System;

namespace System.Transactions
{
	// Token: 0x02000055 RID: 85
	internal class VolatileEnlistmentDone : VolatileEnlistmentEnded
	{
		// Token: 0x06000272 RID: 626 RVA: 0x000328F4 File Offset: 0x00031CF4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00032914 File Offset: 0x00031D14
		internal override void ChangeStatePreparing(InternalEnlistment enlistment)
		{
			enlistment.CheckComplete();
		}
	}
}

using System;

namespace System.Transactions
{
	// Token: 0x0200003B RID: 59
	internal class DurableInternalEnlistment : InternalEnlistment
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00030374 File Offset: 0x0002F774
		internal DurableInternalEnlistment(Enlistment enlistment, Guid resourceManagerIdentifier, InternalTransaction transaction, IEnlistmentNotification twoPhaseNotifications, ISinglePhaseNotification singlePhaseNotifications, Transaction atomicTransaction) : base(enlistment, transaction, twoPhaseNotifications, singlePhaseNotifications, atomicTransaction)
		{
			this.resourceManagerIdentifier = resourceManagerIdentifier;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000303A4 File Offset: 0x0002F7A4
		protected DurableInternalEnlistment(Enlistment enlistment, IEnlistmentNotification twoPhaseNotifications) : base(enlistment, twoPhaseNotifications)
		{
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000303C4 File Offset: 0x0002F7C4
		internal override Guid ResourceManagerIdentifier
		{
			get
			{
				return this.resourceManagerIdentifier;
			}
		}

		// Token: 0x040000EE RID: 238
		internal Guid resourceManagerIdentifier;
	}
}

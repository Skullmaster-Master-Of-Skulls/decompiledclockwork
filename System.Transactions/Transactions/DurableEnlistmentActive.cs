using System;

namespace System.Transactions
{
	// Token: 0x02000046 RID: 70
	internal class DurableEnlistmentActive : DurableEnlistmentState
	{
		// Token: 0x0600021A RID: 538 RVA: 0x000316E4 File Offset: 0x00030AE4
		internal override void EnterState(InternalEnlistment enlistment)
		{
			enlistment.State = this;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00031704 File Offset: 0x00030B04
		internal override void EnlistmentDone(InternalEnlistment enlistment)
		{
			DurableEnlistmentState._DurableEnlistmentEnded.EnterState(enlistment);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00031724 File Offset: 0x00030B24
		internal override void InternalAborted(InternalEnlistment enlistment)
		{
			DurableEnlistmentState._DurableEnlistmentAborting.EnterState(enlistment);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00031744 File Offset: 0x00030B44
		internal override void ChangeStateCommitting(InternalEnlistment enlistment)
		{
			DurableEnlistmentState._DurableEnlistmentCommitting.EnterState(enlistment);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00031764 File Offset: 0x00030B64
		internal override void ChangeStatePromoted(InternalEnlistment enlistment, IPromotedEnlistment promotedEnlistment)
		{
			enlistment.PromotedEnlistment = promotedEnlistment;
			EnlistmentState._EnlistmentStatePromoted.EnterState(enlistment);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00031784 File Offset: 0x00030B84
		internal override void ChangeStateDelegated(InternalEnlistment enlistment)
		{
			DurableEnlistmentState._DurableEnlistmentDelegated.EnterState(enlistment);
		}
	}
}

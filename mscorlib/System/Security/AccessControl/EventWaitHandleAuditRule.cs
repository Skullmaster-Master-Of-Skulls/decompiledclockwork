using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000921 RID: 2337
	public sealed class EventWaitHandleAuditRule : AuditRule
	{
		// Token: 0x06005465 RID: 21605 RVA: 0x0013227A File Offset: 0x0013127A
		public EventWaitHandleAuditRule(IdentityReference identity, EventWaitHandleRights eventRights, AuditFlags flags) : this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x00132288 File Offset: 0x00131288
		internal EventWaitHandleAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06005467 RID: 21607 RVA: 0x00132299 File Offset: 0x00131299
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}

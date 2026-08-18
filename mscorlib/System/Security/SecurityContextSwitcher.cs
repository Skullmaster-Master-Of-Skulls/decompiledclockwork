using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Threading;

namespace System.Security
{
	// Token: 0x0200068D RID: 1677
	internal struct SecurityContextSwitcher : IDisposable
	{
		// Token: 0x06003C86 RID: 15494 RVA: 0x000CF058 File Offset: 0x000CE058
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is SecurityContextSwitcher))
			{
				return false;
			}
			SecurityContextSwitcher securityContextSwitcher = (SecurityContextSwitcher)obj;
			return this.prevSC == securityContextSwitcher.prevSC && this.currSC == securityContextSwitcher.currSC && this.currEC == securityContextSwitcher.currEC && this.cssw == securityContextSwitcher.cssw && this.wic == securityContextSwitcher.wic;
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x000CF0CB File Offset: 0x000CE0CB
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x000CF0DE File Offset: 0x000CE0DE
		public static bool operator ==(SecurityContextSwitcher c1, SecurityContextSwitcher c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x000CF0F3 File Offset: 0x000CE0F3
		public static bool operator !=(SecurityContextSwitcher c1, SecurityContextSwitcher c2)
		{
			return !c1.Equals(c2);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000CF10B File Offset: 0x000CE10B
		void IDisposable.Dispose()
		{
			this.Undo();
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000CF114 File Offset: 0x000CE114
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x000CF144 File Offset: 0x000CE144
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Undo()
		{
			if (this.currEC == null)
			{
				return;
			}
			if (this.currEC != Thread.CurrentThread.GetExecutionContextNoCreate())
			{
				Environment.FailFast(Environment.GetResourceString("InvalidOperation_SwitcherCtxMismatch"));
			}
			if (this.currSC != this.currEC.SecurityContext)
			{
				Environment.FailFast(Environment.GetResourceString("InvalidOperation_SwitcherCtxMismatch"));
			}
			this.currEC.SecurityContext = this.prevSC;
			this.currEC = null;
			bool flag = true;
			try
			{
				if (this.wic != null)
				{
					flag &= this.wic.UndoNoThrow();
				}
			}
			catch
			{
				flag &= this.cssw.UndoNoThrow();
				Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"));
			}
			flag &= this.cssw.UndoNoThrow();
			if (!flag)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"));
			}
		}

		// Token: 0x04001F17 RID: 7959
		internal SecurityContext prevSC;

		// Token: 0x04001F18 RID: 7960
		internal SecurityContext currSC;

		// Token: 0x04001F19 RID: 7961
		internal ExecutionContext currEC;

		// Token: 0x04001F1A RID: 7962
		internal CompressedStackSwitcher cssw;

		// Token: 0x04001F1B RID: 7963
		internal WindowsImpersonationContext wic;
	}
}

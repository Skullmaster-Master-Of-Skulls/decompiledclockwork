using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005D5 RID: 1493
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class DesignerTransaction : IDisposable
	{
		// Token: 0x06003791 RID: 14225 RVA: 0x000F09DD File Offset: 0x000EEBDD
		protected DesignerTransaction() : this("")
		{
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000F09EA File Offset: 0x000EEBEA
		protected DesignerTransaction(string description)
		{
			this.desc = description;
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000F09F9 File Offset: 0x000EEBF9
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000F0A01 File Offset: 0x000EEC01
		public bool Committed
		{
			get
			{
				return this.committed;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000F0A09 File Offset: 0x000EEC09
		public string Description
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000F0A11 File Offset: 0x000EEC11
		public void Cancel()
		{
			if (!this.canceled && !this.committed)
			{
				this.canceled = true;
				GC.SuppressFinalize(this);
				this.suppressedFinalization = true;
				this.OnCancel();
			}
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000F0A3D File Offset: 0x000EEC3D
		public void Commit()
		{
			if (!this.committed && !this.canceled)
			{
				this.committed = true;
				GC.SuppressFinalize(this);
				this.suppressedFinalization = true;
				this.OnCommit();
			}
		}

		// Token: 0x06003798 RID: 14232
		protected abstract void OnCancel();

		// Token: 0x06003799 RID: 14233
		protected abstract void OnCommit();

		// Token: 0x0600379A RID: 14234 RVA: 0x000F0A6C File Offset: 0x000EEC6C
		~DesignerTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000F0A9C File Offset: 0x000EEC9C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			if (!this.suppressedFinalization)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000F0AB3 File Offset: 0x000EECB3
		protected virtual void Dispose(bool disposing)
		{
			this.Cancel();
		}

		// Token: 0x04002AFD RID: 11005
		private bool committed;

		// Token: 0x04002AFE RID: 11006
		private bool canceled;

		// Token: 0x04002AFF RID: 11007
		private bool suppressedFinalization;

		// Token: 0x04002B00 RID: 11008
		private string desc;
	}
}

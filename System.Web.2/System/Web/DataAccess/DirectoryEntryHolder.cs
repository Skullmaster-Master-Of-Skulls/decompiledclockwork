using System;
using System.DirectoryServices;

namespace System.Web.DataAccess
{
	// Token: 0x020001A9 RID: 425
	internal sealed class DirectoryEntryHolder
	{
		// Token: 0x06001642 RID: 5698 RVA: 0x000465BE File Offset: 0x000447BE
		internal DirectoryEntryHolder(DirectoryEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000465CD File Offset: 0x000447CD
		internal void Open(HttpContext context, bool revertImpersonate)
		{
			if (this.opened)
			{
				return;
			}
			if (revertImpersonate)
			{
				this.ctx = new ApplicationImpersonationContext();
			}
			else
			{
				this.ctx = null;
			}
			this.opened = true;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000465F6 File Offset: 0x000447F6
		internal void Close()
		{
			if (!this.opened)
			{
				return;
			}
			this.entry.Dispose();
			this.RestoreImpersonation();
			this.opened = false;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00046619 File Offset: 0x00044819
		internal void RestoreImpersonation()
		{
			if (this.ctx != null)
			{
				this.ctx.Undo();
				this.ctx = null;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00046635 File Offset: 0x00044835
		internal DirectoryEntry DirectoryEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x0400168C RID: 5772
		private ImpersonationContext ctx;

		// Token: 0x0400168D RID: 5773
		private bool opened;

		// Token: 0x0400168E RID: 5774
		private DirectoryEntry entry;
	}
}

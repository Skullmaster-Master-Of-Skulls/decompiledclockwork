using System;
using System.DirectoryServices;

namespace System.Web.DataAccess
{
	// Token: 0x02000274 RID: 628
	internal sealed class DirectoryEntryHolder
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x0008E5D6 File Offset: 0x0008D5D6
		internal DirectoryEntryHolder(DirectoryEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0008E5E5 File Offset: 0x0008D5E5
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

		// Token: 0x060020BF RID: 8383 RVA: 0x0008E60E File Offset: 0x0008D60E
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

		// Token: 0x060020C0 RID: 8384 RVA: 0x0008E631 File Offset: 0x0008D631
		internal void RestoreImpersonation()
		{
			if (this.ctx != null)
			{
				this.ctx.Undo();
				this.ctx = null;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0008E64D File Offset: 0x0008D64D
		internal DirectoryEntry DirectoryEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x04001AC3 RID: 6851
		private ImpersonationContext ctx;

		// Token: 0x04001AC4 RID: 6852
		private bool opened;

		// Token: 0x04001AC5 RID: 6853
		private DirectoryEntry entry;
	}
}

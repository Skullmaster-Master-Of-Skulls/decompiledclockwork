using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000748 RID: 1864
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebPartVerbsEventArgs : EventArgs
	{
		// Token: 0x06005A56 RID: 23126 RVA: 0x0016C81C File Offset: 0x0016B81C
		public WebPartVerbsEventArgs() : this(null)
		{
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x0016C825 File Offset: 0x0016B825
		public WebPartVerbsEventArgs(WebPartVerbCollection verbs)
		{
			this._verbs = verbs;
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x0016C834 File Offset: 0x0016B834
		// (set) Token: 0x06005A59 RID: 23129 RVA: 0x0016C84A File Offset: 0x0016B84A
		public WebPartVerbCollection Verbs
		{
			get
			{
				if (this._verbs == null)
				{
					return WebPartVerbCollection.Empty;
				}
				return this._verbs;
			}
			set
			{
				this._verbs = value;
			}
		}

		// Token: 0x0400308A RID: 12426
		private WebPartVerbCollection _verbs;
	}
}

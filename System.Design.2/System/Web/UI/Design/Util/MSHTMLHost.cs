using System;
using System.Design;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000162 RID: 354
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class MSHTMLHost : Control
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x00051344 File Offset: 0x0004F544
		public NativeMethods.IHTMLDocument2 GetDocument()
		{
			return this.tridentSite.GetDocument();
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00051354 File Offset: 0x0004F554
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 131072;
				return createParams;
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0005137C File Offset: 0x0004F57C
		public bool CreateTrident()
		{
			try
			{
				this.tridentSite = new TridentSite(this);
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000513B0 File Offset: 0x0004F5B0
		public void ActivateTrident()
		{
			this.tridentSite.Activate();
		}

		// Token: 0x040007A5 RID: 1957
		private TridentSite tridentSite;
	}
}

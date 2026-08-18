using System;
using System.Security.Permissions;
using System.Web.UI.Adapters;

namespace System.Web.UI.WebControls.Adapters
{
	// Token: 0x02000697 RID: 1687
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebControlAdapter : ControlAdapter
	{
		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x0014D594 File Offset: 0x0014C594
		protected new WebControl Control
		{
			get
			{
				return (WebControl)base.Control;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x0014D5A1 File Offset: 0x0014C5A1
		protected bool IsEnabled
		{
			get
			{
				return this.Control.IsEnabled;
			}
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x0014D5AE File Offset: 0x0014C5AE
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.Control.RenderBeginTag(writer);
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x0014D5BC File Offset: 0x0014C5BC
		protected virtual void RenderEndTag(HtmlTextWriter writer)
		{
			this.Control.RenderEndTag(writer);
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x0014D5CA File Offset: 0x0014C5CA
		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			this.Control.RenderContents(writer);
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0014D5D8 File Offset: 0x0014C5D8
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}
	}
}

using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Adapters
{
	// Token: 0x020003BF RID: 959
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class ControlAdapter
	{
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000D22B5 File Offset: 0x000D12B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000D22BD File Offset: 0x000D12BD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected Page Page
		{
			get
			{
				if (this.Control != null)
				{
					return this.Control.Page;
				}
				return null;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000D22D4 File Offset: 0x000D12D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected PageAdapter PageAdapter
		{
			get
			{
				if (this.Control != null && this.Control.Page != null)
				{
					return this.Control.Page.PageAdapter;
				}
				return null;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000D2300 File Offset: 0x000D1300
		protected HttpBrowserCapabilities Browser
		{
			get
			{
				if (this._browser == null)
				{
					if (this.Page.RequestInternal != null)
					{
						this._browser = this.Page.RequestInternal.Browser;
					}
					else
					{
						HttpContext httpContext = HttpContext.Current;
						if (httpContext != null && httpContext.Request != null)
						{
							this._browser = httpContext.Request.Browser;
						}
					}
				}
				return this._browser;
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000D2362 File Offset: 0x000D1362
		protected internal virtual void OnInit(EventArgs e)
		{
			this.Control.OnInit(e);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000D2370 File Offset: 0x000D1370
		protected internal virtual void OnLoad(EventArgs e)
		{
			this.Control.OnLoad(e);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000D237E File Offset: 0x000D137E
		protected internal virtual void OnPreRender(EventArgs e)
		{
			this.Control.OnPreRender(e);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000D238C File Offset: 0x000D138C
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			if (this._control != null)
			{
				this._control.Render(writer);
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000D23A2 File Offset: 0x000D13A2
		protected virtual void RenderChildren(HtmlTextWriter writer)
		{
			if (this._control != null)
			{
				this._control.RenderChildren(writer);
			}
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000D23B8 File Offset: 0x000D13B8
		protected internal virtual void OnUnload(EventArgs e)
		{
			this.Control.OnUnload(e);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000D23C6 File Offset: 0x000D13C6
		protected internal virtual void BeginRender(HtmlTextWriter writer)
		{
			writer.BeginRender();
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000D23CE File Offset: 0x000D13CE
		protected internal virtual void CreateChildControls()
		{
			this.Control.CreateChildControls();
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000D23DB File Offset: 0x000D13DB
		protected internal virtual void EndRender(HtmlTextWriter writer)
		{
			writer.EndRender();
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000D23E3 File Offset: 0x000D13E3
		protected internal virtual void LoadAdapterControlState(object state)
		{
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000D23E5 File Offset: 0x000D13E5
		protected internal virtual void LoadAdapterViewState(object state)
		{
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000D23E7 File Offset: 0x000D13E7
		protected internal virtual object SaveAdapterControlState()
		{
			return null;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000D23EA File Offset: 0x000D13EA
		protected internal virtual object SaveAdapterViewState()
		{
			return null;
		}

		// Token: 0x040021BE RID: 8638
		private HttpBrowserCapabilities _browser;

		// Token: 0x040021BF RID: 8639
		internal Control _control;
	}
}

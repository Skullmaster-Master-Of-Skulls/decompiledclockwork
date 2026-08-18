using System;
using System.ComponentModel;

namespace System.Web.UI.Adapters
{
	// Token: 0x02000337 RID: 823
	public abstract class ControlAdapter
	{
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x0007D8AC File Offset: 0x0007BAAC
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

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x0007D8C3 File Offset: 0x0007BAC3
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

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x0007D8EC File Offset: 0x0007BAEC
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

		// Token: 0x06002615 RID: 9749 RVA: 0x0007D94E File Offset: 0x0007BB4E
		protected internal virtual void OnInit(EventArgs e)
		{
			this.Control.OnInit(e);
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0007D95C File Offset: 0x0007BB5C
		protected internal virtual void OnLoad(EventArgs e)
		{
			this.Control.OnLoad(e);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x0007D96A File Offset: 0x0007BB6A
		protected internal virtual void OnPreRender(EventArgs e)
		{
			this.Control.OnPreRender(e);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0007D978 File Offset: 0x0007BB78
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			if (this._control != null)
			{
				this._control.Render(writer);
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x0007D98E File Offset: 0x0007BB8E
		protected virtual void RenderChildren(HtmlTextWriter writer)
		{
			if (this._control != null)
			{
				this._control.RenderChildren(writer);
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x0007D9A4 File Offset: 0x0007BBA4
		protected internal virtual void OnUnload(EventArgs e)
		{
			this.Control.OnUnload(e);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0007D9B2 File Offset: 0x0007BBB2
		protected internal virtual void BeginRender(HtmlTextWriter writer)
		{
			writer.BeginRender();
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0007D9BA File Offset: 0x0007BBBA
		protected internal virtual void CreateChildControls()
		{
			this.Control.CreateChildControls();
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0007D9C7 File Offset: 0x0007BBC7
		protected internal virtual void EndRender(HtmlTextWriter writer)
		{
			writer.EndRender();
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void LoadAdapterControlState(object state)
		{
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void LoadAdapterViewState(object state)
		{
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0000298D File Offset: 0x00000B8D
		protected internal virtual object SaveAdapterControlState()
		{
			return null;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0000298D File Offset: 0x00000B8D
		protected internal virtual object SaveAdapterViewState()
		{
			return null;
		}

		// Token: 0x04001DB5 RID: 7605
		private HttpBrowserCapabilities _browser;

		// Token: 0x04001DB6 RID: 7606
		internal Control _control;
	}
}

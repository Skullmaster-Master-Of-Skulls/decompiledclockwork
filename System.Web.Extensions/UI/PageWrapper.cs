using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace System.Web.UI
{
	// Token: 0x0200005F RID: 95
	internal sealed class PageWrapper : IPage
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00013315 File Offset: 0x00011515
		public PageWrapper(Page page)
		{
			this._page = page;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00013324 File Offset: 0x00011524
		string IPage.AppRelativeVirtualPath
		{
			get
			{
				return this._page.AppRelativeVirtualPath;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00013331 File Offset: 0x00011531
		IDictionary<string, string> IPage.HiddenFieldsToRender
		{
			get
			{
				return this._page._hiddenFieldsToRender;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0001333E File Offset: 0x0001153E
		IClientScriptManager IPage.ClientScript
		{
			get
			{
				return new ClientScriptManagerWrapper(this._page.ClientScript);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00013350 File Offset: 0x00011550
		bool IPage.EnableEventValidation
		{
			get
			{
				return this._page.EnableEventValidation;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001335D File Offset: 0x0001155D
		IHtmlForm IPage.Form
		{
			get
			{
				if (this._page.Form != null)
				{
					return new HtmlFormWrapper(this._page.Form);
				}
				return null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0001337E File Offset: 0x0001157E
		HtmlHead IPage.Header
		{
			get
			{
				return this._page.Header;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0001338B File Offset: 0x0001158B
		bool IPage.IsPostBack
		{
			get
			{
				return this._page.IsPostBack;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00013398 File Offset: 0x00011598
		bool IPage.IsValid
		{
			get
			{
				return this._page.IsValid;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000133A5 File Offset: 0x000115A5
		IDictionary IPage.Items
		{
			get
			{
				return this._page.Items;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000133B2 File Offset: 0x000115B2
		HttpRequestBase IPage.Request
		{
			get
			{
				return new HttpRequestWrapper(this._page.Request);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000133C4 File Offset: 0x000115C4
		HttpResponseInternalBase IPage.Response
		{
			get
			{
				return new HttpResponseInternalWrapper(this._page.Response);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000133D6 File Offset: 0x000115D6
		HttpServerUtilityBase IPage.Server
		{
			get
			{
				return new HttpServerUtilityWrapper(this._page.Server);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000133E8 File Offset: 0x000115E8
		// (set) Token: 0x06000379 RID: 889 RVA: 0x000133F5 File Offset: 0x000115F5
		string IPage.Title
		{
			get
			{
				return this._page.Title;
			}
			set
			{
				this._page.Title = value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600037A RID: 890 RVA: 0x00013403 File Offset: 0x00011603
		// (remove) Token: 0x0600037B RID: 891 RVA: 0x00013411 File Offset: 0x00011611
		event EventHandler IPage.Error
		{
			add
			{
				this._page.Error += value;
			}
			remove
			{
				this._page.Error -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600037C RID: 892 RVA: 0x0001341F File Offset: 0x0001161F
		// (remove) Token: 0x0600037D RID: 893 RVA: 0x0001342D File Offset: 0x0001162D
		event EventHandler IPage.InitComplete
		{
			add
			{
				this._page.InitComplete += value;
			}
			remove
			{
				this._page.InitComplete -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600037E RID: 894 RVA: 0x0001343B File Offset: 0x0001163B
		// (remove) Token: 0x0600037F RID: 895 RVA: 0x00013449 File Offset: 0x00011649
		event EventHandler IPage.LoadComplete
		{
			add
			{
				this._page.LoadComplete += value;
			}
			remove
			{
				this._page.LoadComplete -= value;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00013457 File Offset: 0x00011657
		void IPage.RegisterRequiresViewStateEncryption()
		{
			this._page.RegisterRequiresViewStateEncryption();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00013464 File Offset: 0x00011664
		void IPage.SetFocus(Control control)
		{
			this._page.SetFocus(control);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00013472 File Offset: 0x00011672
		void IPage.SetFocus(string clientID)
		{
			this._page.SetFocus(clientID);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000383 RID: 899 RVA: 0x00013480 File Offset: 0x00011680
		// (remove) Token: 0x06000384 RID: 900 RVA: 0x0001348E File Offset: 0x0001168E
		event EventHandler IPage.PreRender
		{
			add
			{
				this._page.PreRender += value;
			}
			remove
			{
				this._page.PreRender -= value;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000385 RID: 901 RVA: 0x0001349C File Offset: 0x0001169C
		// (remove) Token: 0x06000386 RID: 902 RVA: 0x000134AA File Offset: 0x000116AA
		event EventHandler IPage.PreRenderComplete
		{
			add
			{
				this._page.PreRenderComplete += value;
			}
			remove
			{
				this._page.PreRenderComplete -= value;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000134B8 File Offset: 0x000116B8
		void IPage.SetPostFormRenderDelegate(RenderMethod renderMethod)
		{
			this._page.SetPostFormRenderDelegate(renderMethod);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000134C6 File Offset: 0x000116C6
		void IPage.SetRenderMethodDelegate(RenderMethod renderMethod)
		{
			this._page.SetRenderMethodDelegate(renderMethod);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000134D4 File Offset: 0x000116D4
		void IPage.Validate(string validationGroup)
		{
			this._page.Validate(validationGroup);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000134E2 File Offset: 0x000116E2
		void IPage.VerifyRenderingInServerForm(Control control)
		{
			this._page.VerifyRenderingInServerForm(control);
		}

		// Token: 0x0400014E RID: 334
		private readonly Page _page;
	}
}

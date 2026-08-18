using System;
using System.Web.UI.HtmlControls;

namespace System.Web.UI
{
	// Token: 0x02000051 RID: 81
	internal sealed class HtmlFormWrapper : IHtmlForm
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00012056 File Offset: 0x00010256
		public HtmlFormWrapper(HtmlForm form)
		{
			this._form = form;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00012065 File Offset: 0x00010265
		string IHtmlForm.ClientID
		{
			get
			{
				return this._form.ClientID;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00012072 File Offset: 0x00010272
		string IHtmlForm.Method
		{
			get
			{
				return this._form.Method;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001207F File Offset: 0x0001027F
		void IHtmlForm.RenderControl(HtmlTextWriter writer)
		{
			this._form.RenderControl(writer);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001208D File Offset: 0x0001028D
		void IHtmlForm.SetRenderMethodDelegate(RenderMethod renderMethod)
		{
			this._form.SetRenderMethodDelegate(renderMethod);
		}

		// Token: 0x0400011A RID: 282
		private HtmlForm _form;
	}
}

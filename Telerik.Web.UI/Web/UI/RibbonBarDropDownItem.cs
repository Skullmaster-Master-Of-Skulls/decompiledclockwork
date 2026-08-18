using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E56 RID: 3670
	public abstract class RibbonBarDropDownItem : RibbonBarItem
	{
		// Token: 0x17002BF4 RID: 11252
		// (get) Token: 0x06008B1D RID: 35613
		internal abstract string ItemCssClass { get; }

		// Token: 0x17002BF5 RID: 11253
		// (get) Token: 0x06008B1E RID: 35614
		internal abstract string InnerCssClass { get; }

		// Token: 0x17002BF6 RID: 11254
		// (get) Token: 0x06008B1F RID: 35615
		internal abstract string InputCssClass { get; }

		// Token: 0x17002BF7 RID: 11255
		// (get) Token: 0x06008B20 RID: 35616 RVA: 0x001FABB7 File Offset: 0x001F8DB7
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x06008B21 RID: 35617 RVA: 0x001FABBB File Offset: 0x001F8DBB
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			((RibbonBarItemRenderBase)base.Renderer).RenderDropDown(writer);
		}

		// Token: 0x06008B22 RID: 35618 RVA: 0x001FABD5 File Offset: 0x001F8DD5
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06008B23 RID: 35619 RVA: 0x001FABE3 File Offset: 0x001F8DE3
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.Renderer.RenderContents(writer);
		}

		// Token: 0x17002BF8 RID: 11256
		// (get) Token: 0x06008B24 RID: 35620 RVA: 0x001FABF1 File Offset: 0x001F8DF1
		// (set) Token: 0x06008B25 RID: 35621 RVA: 0x001FAC11 File Offset: 0x001F8E11
		public override string AccessKey
		{
			get
			{
				return (string)(this.ViewState["AccessKey"] ?? string.Empty);
			}
			set
			{
				this.ViewState["AccessKey"] = value;
			}
		}

		// Token: 0x17002BF9 RID: 11257
		// (get) Token: 0x06008B26 RID: 35622 RVA: 0x001FAC24 File Offset: 0x001F8E24
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
		}
	}
}

using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x0200132C RID: 4908
	public class EditorToolBar
	{
		// Token: 0x0600CCEF RID: 52463 RVA: 0x002DAC65 File Offset: 0x002D8E65
		public EditorToolBar()
		{
			this.RenderMode = RenderMode.Auto;
		}

		// Token: 0x170041E8 RID: 16872
		// (get) Token: 0x0600CCF0 RID: 52464 RVA: 0x002DAC8A File Offset: 0x002D8E8A
		// (set) Token: 0x0600CCF1 RID: 52465 RVA: 0x002DAC92 File Offset: 0x002D8E92
		public RenderMode RenderMode { get; set; }

		// Token: 0x170041E9 RID: 16873
		// (get) Token: 0x0600CCF2 RID: 52466 RVA: 0x002DAC9B File Offset: 0x002D8E9B
		protected IEditorRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = RendererFactory.GetRenderer(this);
				}
				return this._renderer;
			}
		}

		// Token: 0x170041EA RID: 16874
		// (get) Token: 0x0600CCF3 RID: 52467 RVA: 0x002DACB7 File Offset: 0x002D8EB7
		public Collection<EditorToolBase> Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new Collection<EditorToolBase>();
				}
				return this._items;
			}
		}

		// Token: 0x170041EB RID: 16875
		// (get) Token: 0x0600CCF4 RID: 52468 RVA: 0x002DACD2 File Offset: 0x002D8ED2
		protected virtual string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x170041EC RID: 16876
		// (get) Token: 0x0600CCF5 RID: 52469 RVA: 0x002DACDF File Offset: 0x002D8EDF
		internal string RuntimeSkin
		{
			get
			{
				if (string.IsNullOrEmpty(this.Skin))
				{
					return "Default";
				}
				return this.Skin;
			}
		}

		// Token: 0x170041ED RID: 16877
		// (get) Token: 0x0600CCF6 RID: 52470 RVA: 0x002DACFA File Offset: 0x002D8EFA
		// (set) Token: 0x0600CCF7 RID: 52471 RVA: 0x002DAD02 File Offset: 0x002D8F02
		public string Skin
		{
			get
			{
				return this._skin;
			}
			set
			{
				this._skin = value;
			}
		}

		// Token: 0x0600CCF8 RID: 52472 RVA: 0x002DAD0B File Offset: 0x002D8F0B
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddListAttributesToRender(writer);
			this.Renderer.RenderBeginTag(writer);
		}

		// Token: 0x0600CCF9 RID: 52473 RVA: 0x002DAD20 File Offset: 0x002D8F20
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			this.Renderer.RenderEndTag(writer);
		}

		// Token: 0x0600CCFA RID: 52474 RVA: 0x002DAD2E File Offset: 0x002D8F2E
		protected virtual void RenderChildren(HtmlTextWriter writer)
		{
			this.Renderer.RenderChildren(writer);
		}

		// Token: 0x0600CCFB RID: 52475 RVA: 0x002DAD3C File Offset: 0x002D8F3C
		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600CCFC RID: 52476 RVA: 0x002DAD4A File Offset: 0x002D8F4A
		public virtual void RenderControl(HtmlTextWriter writer)
		{
			this.Render(writer);
		}

		// Token: 0x0600CCFD RID: 52477 RVA: 0x002DAD53 File Offset: 0x002D8F53
		protected virtual void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderChildren(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		// Token: 0x0600CCFE RID: 52478 RVA: 0x002DAD71 File Offset: 0x002D8F71
		protected virtual void AddListAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x040036A3 RID: 13987
		private Collection<EditorToolBase> _items = new Collection<EditorToolBase>();

		// Token: 0x040036A4 RID: 13988
		private IEditorRenderer _renderer;

		// Token: 0x040036A5 RID: 13989
		private string _skin = string.Empty;
	}
}

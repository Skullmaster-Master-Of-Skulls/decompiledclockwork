using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D9 RID: 729
	public class HeaderToolsToolBar
	{
		// Token: 0x06001950 RID: 6480 RVA: 0x000531CE File Offset: 0x000513CE
		public HeaderToolsToolBar()
		{
			this.RenderMode = RenderMode.Auto;
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000531F3 File Offset: 0x000513F3
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x000531FB File Offset: 0x000513FB
		public RenderMode RenderMode { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00053204 File Offset: 0x00051404
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

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00053220 File Offset: 0x00051420
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

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0005323B File Offset: 0x0005143B
		protected virtual string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00053248 File Offset: 0x00051448
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

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00053263 File Offset: 0x00051463
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0005326B File Offset: 0x0005146B
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

		// Token: 0x06001959 RID: 6489 RVA: 0x00053274 File Offset: 0x00051474
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddListAttributesToRender(writer);
			this.Renderer.RenderBeginTag(writer);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00053289 File Offset: 0x00051489
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			this.Renderer.RenderEndTag(writer);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00053297 File Offset: 0x00051497
		protected virtual void RenderChildren(HtmlTextWriter writer)
		{
			this.Renderer.RenderChildren(writer);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000532A5 File Offset: 0x000514A5
		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000532B3 File Offset: 0x000514B3
		public virtual void RenderControl(HtmlTextWriter writer)
		{
			this.Render(writer);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000532BC File Offset: 0x000514BC
		protected virtual void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderChildren(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000532DA File Offset: 0x000514DA
		protected virtual void AddListAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolBar t-hbox");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
		}

		// Token: 0x04000691 RID: 1681
		private Collection<EditorToolBase> _items = new Collection<EditorToolBase>();

		// Token: 0x04000692 RID: 1682
		private IEditorRenderer _renderer;

		// Token: 0x04000693 RID: 1683
		private string _skin = string.Empty;
	}
}

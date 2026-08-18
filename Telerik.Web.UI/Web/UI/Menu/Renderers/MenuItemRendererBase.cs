using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005D8 RID: 1496
	public abstract class MenuItemRendererBase : IRenderer
	{
		// Token: 0x0600365C RID: 13916 RVA: 0x000B3A88 File Offset: 0x000B1C88
		public MenuItemRendererBase(RadMenuItem owner)
		{
			this.Owner = owner;
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x000B3A97 File Offset: 0x000B1C97
		// (set) Token: 0x0600365E RID: 13918 RVA: 0x000B3A9F File Offset: 0x000B1C9F
		protected RadMenuItem Owner { get; set; }

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000B3AA8 File Offset: 0x000B1CA8
		protected RadMenu Menu
		{
			get
			{
				return this.Owner.Menu;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000B3AB5 File Offset: 0x000B1CB5
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06003661 RID: 13921 RVA: 0x000B3AB9 File Offset: 0x000B1CB9
		public string CssClassFormatString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000B3AC0 File Offset: 0x000B1CC0
		public virtual List<string> CssClass
		{
			get
			{
				return new List<string>
				{
					"rmItem"
				};
			}
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000B3AE0 File Offset: 0x000B1CE0
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = string.Join(" ", this.CssClass.ToArray());
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			if (this.Menu.InDesignMode)
			{
				if (this.Menu.Flow == ItemFlow.Horizontal)
				{
					writer.AddStyleAttribute("display", "inline-block");
				}
				writer.AddStyleAttribute("float", "none");
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000B3B47 File Offset: 0x000B1D47
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			if (this.Owner.Items.Count > 0)
			{
				this.RenderChildItems(writer);
			}
			if (this.Owner.HasContentTemplate)
			{
				this.RenderContentWrapper(writer, new Action<HtmlTextWriter>(this.RenderContentTemplate));
			}
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000B3B84 File Offset: 0x000B1D84
		protected virtual void RenderContentTemplate(HtmlTextWriter writer)
		{
			this.Owner.ContentTemplateContainer.RenderControl(writer);
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000B3B97 File Offset: 0x000B1D97
		protected virtual void RenderToggleButton(HtmlTextWriter writer, Action<HtmlTextWriter> action = null)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmToggle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (action != null)
			{
				action(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000B3BC0 File Offset: 0x000B1DC0
		protected string GetLeftImageClass()
		{
			return ("rmLeftImage " + this.Owner.SpriteCssClass).Trim();
		}

		// Token: 0x06003668 RID: 13928
		protected abstract void RenderChildItems(HtmlTextWriter writer);

		// Token: 0x06003669 RID: 13929
		protected abstract void RenderContentWrapper(HtmlTextWriter writer, Action<HtmlTextWriter> action);

		// Token: 0x0600366A RID: 13930 RVA: 0x000B3BEC File Offset: 0x000B1DEC
		protected static void RenderChildGroup(HtmlTextWriter writer, IList<RadMenuItem> items, string groupCssClass, Action action)
		{
			if (items.Count == 0)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, groupCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			action();
			foreach (RadMenuItem radMenuItem in items)
			{
				radMenuItem.RenderControl(writer);
			}
			writer.RenderEndTag();
		}
	}
}

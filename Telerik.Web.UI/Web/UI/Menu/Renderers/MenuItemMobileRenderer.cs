using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005D9 RID: 1497
	public class MenuItemMobileRenderer : MenuItemRendererBase
	{
		// Token: 0x0600366B RID: 13931 RVA: 0x000B3C5C File Offset: 0x000B1E5C
		public MenuItemMobileRenderer(RadMenuItem owner) : base(owner)
		{
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x0600366C RID: 13932 RVA: 0x000B3C65 File Offset: 0x000B1E65
		public string TextToRender
		{
			get
			{
				if (!base.Menu.EnableTextHTMLEncoding)
				{
					return base.Owner.Text;
				}
				return HttpUtility.HtmlEncode(base.Owner.Text);
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x000B3C9C File Offset: 0x000B1E9C
		public override List<string> CssClass
		{
			get
			{
				List<string> list = new List<string>();
				list.AddRange(base.CssClass);
				if (base.Owner.IsSeparator)
				{
					list.Add("rmSeparator");
				}
				else
				{
					list.Add(base.Owner.CssClass);
					if (!base.Owner.Enabled)
					{
						list.Add("rmDisabled");
						list.Add(base.Owner.DisabledCssClass);
					}
					if (base.Owner.Selected && base.Menu.EnableSelection)
					{
						list.Add("rmSelected");
						list.Add(base.Owner.SelectedCssClass);
					}
				}
				return (from className in list
				where !string.IsNullOrEmpty(className)
				select className).Distinct<string>().ToList<string>();
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x000B3D72 File Offset: 0x000B1F72
		public virtual string ParentItemClassName
		{
			get
			{
				return string.Format("{0} {1}", "rmItem", "rmParentItem");
			}
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000B3D98 File Offset: 0x000B1F98
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!base.Owner.IsSeparator)
			{
				this.RenderLink(writer);
				if (base.Owner.Items.Count > 0 || base.Owner.ExpandMode == MenuItemExpandMode.WebService)
				{
					this.RenderToggleButton(writer, delegate(HtmlTextWriter contentWriter)
					{
						contentWriter.Write("<!-- &nbsp; -->");
					});
				}
				base.RenderContents(writer);
			}
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000B3E20 File Offset: 0x000B2020
		protected override void RenderChildItems(HtmlTextWriter writer)
		{
			IList<RadMenuItem> items = base.Owner.Items;
			MenuItemRendererBase.RenderChildGroup(writer, items, "rmGroup", delegate
			{
				this.RenderParentItem(writer);
			});
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000B3E6A File Offset: 0x000B206A
		protected override void RenderContentWrapper(HtmlTextWriter writer, Action<HtmlTextWriter> action)
		{
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000B3E6C File Offset: 0x000B206C
		protected virtual void RenderLink(HtmlTextWriter writer)
		{
			string cssClass = string.Format("{0} {1}", "rmLink", base.Owner.EnableImageSpriteResolved ? base.GetLeftImageClass() : string.Empty).TrimEnd(new char[0]);
			string cssClass2 = base.Owner.CssClass;
			base.Owner.CssClass = cssClass;
			base.Owner.AddAttributes(writer);
			base.Owner.CssClass = cssClass2;
			if (string.IsNullOrEmpty(base.Owner.NavigateUrl))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			else
			{
				if (base.Owner.Target.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Target, base.Owner.Target);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Owner.ResolveClientUrl(base.Owner.NavigateUrl));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
			}
			writer.Write(this.TextToRender);
			writer.RenderEndTag();
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000B3F57 File Offset: 0x000B2157
		protected virtual void RenderParentItem(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.ParentItemClassName);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.Write(this.TextToRender);
			writer.RenderEndTag();
		}
	}
}

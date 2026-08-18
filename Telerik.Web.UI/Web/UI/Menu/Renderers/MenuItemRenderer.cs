using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005DA RID: 1498
	public abstract class MenuItemRenderer : MenuItemRendererBase
	{
		// Token: 0x06003676 RID: 13942 RVA: 0x000B3F81 File Offset: 0x000B2181
		public MenuItemRenderer(RadMenuItem owner) : base(owner)
		{
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06003677 RID: 13943
		public abstract string TemplateContainerClassName { get; }

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000B3F8C File Offset: 0x000B218C
		public override List<string> CssClass
		{
			get
			{
				List<string> list = new List<string>();
				list.AddRange(base.CssClass);
				if (base.Owner.IsSeparator)
				{
					list.Add("rmSeparator");
					if (!string.IsNullOrEmpty(base.Owner.CssClass))
					{
						list.Add(base.Owner.CssClass);
					}
				}
				else
				{
					list.Add(base.Owner.PositionCssClass);
					if (base.Owner.Templated)
					{
						list.Add("rmTemplate");
					}
				}
				return list;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000B4014 File Offset: 0x000B2214
		public virtual List<string> LinkClassName
		{
			get
			{
				List<string> list = new List<string>();
				if (!base.Owner.IsSeparator)
				{
					list.Add("rmLink");
					if (base.Owner.Level == 0)
					{
						list.Add("rmRootLink");
					}
					if (string.IsNullOrEmpty(base.Owner.Text) && !string.IsNullOrEmpty(base.Owner.CurrentImageUrl))
					{
						list.Add("rmImageOnly");
					}
				}
				return list;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000B4094 File Offset: 0x000B2294
		public virtual List<string> ResolvedStateClasses
		{
			get
			{
				List<string> list = new List<string>();
				if (!base.Owner.Enabled)
				{
					list.Add("rmDisabled");
					list.Add(base.Owner.DisabledCssClass);
				}
				if (!string.IsNullOrEmpty(base.Owner.CssClass))
				{
					list.Add(base.Owner.CssClass);
				}
				if (base.Owner.Selected && base.Menu.EnableSelection)
				{
					list.Add("rmSelected");
					list.Add(base.Owner.SelectedCssClass);
				}
				return (from className in list
				where !string.IsNullOrEmpty(className)
				select className).Distinct<string>().ToList<string>();
			}
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000B4158 File Offset: 0x000B2358
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.Owner.Width != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.Owner.Width.ToString());
				base.Owner.Width = Unit.Empty;
			}
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000B41B4 File Offset: 0x000B23B4
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (base.Owner.Controls.IsReadOnly)
			{
				this.RenderTemplateContent(writer, new Action<HtmlTextWriter>(base.Owner.CallBaseRenderChildren));
				base.RenderContents(writer);
				return;
			}
			if (base.Owner.Templated)
			{
				this.RenderTemplateContent(writer, null);
			}
			else if (!base.Owner.IsSeparator)
			{
				this.RenderLink(writer);
			}
			else if (base.Menu.ResolvedRenderMode == RenderMode.Classic)
			{
				this.RenderTextElement(writer, "");
			}
			if (base.Menu.InDesignMode)
			{
				return;
			}
			base.RenderContents(writer);
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000B4250 File Offset: 0x000B2450
		protected virtual void RenderTemplateContent(HtmlTextWriter writer, Action<HtmlTextWriter> action = null)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.TemplateContainerClassName);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (action != null)
			{
				action(writer);
			}
			else
			{
				foreach (object obj in base.Owner.Controls)
				{
					Control control = (Control)obj;
					if (!(control is RadMenuItem) && !(control is MenuItemContentTemplateContainer))
					{
						control.RenderControl(writer);
					}
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000B42E8 File Offset: 0x000B24E8
		protected virtual void RenderLink(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			base.Owner.CssClass = string.Join(" ", this.LinkClassName.ToArray());
			base.Owner.AddAttributes(writer);
			base.Owner.CssClass = cssClass;
			if (base.Owner.Target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, base.Owner.Target);
			}
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000B435F File Offset: 0x000B255F
		protected virtual void RenderLinkContent(HtmlTextWriter writer, Action<HtmlTextWriter> action)
		{
			this.RenderImageElement(writer);
			action(writer);
			if (base.Owner.ShouldRenderToggleButton)
			{
				this.RenderToggleButton(writer, new Action<HtmlTextWriter>(this.RenderIcon));
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000B4390 File Offset: 0x000B2590
		protected virtual void RenderImageElement(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.CurrentImageUrl))
			{
				this.RenderImage(writer);
				return;
			}
			if (base.Owner.ShouldRenderImagePlaceholder)
			{
				this.RenderImagePlaceholder(writer);
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000B43C0 File Offset: 0x000B25C0
		protected virtual void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, base.Owner.ToolTip);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.Owner.ResolveClientUrl(base.Owner.CurrentImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, base.GetLeftImageClass());
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000B4419 File Offset: 0x000B2619
		protected virtual void RenderImagePlaceholder(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Title, base.Owner.ToolTip);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, base.GetLeftImageClass());
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000B444C File Offset: 0x000B264C
		protected virtual void RenderTextElement(HtmlTextWriter writer, string text = "")
		{
			if (string.IsNullOrEmpty(text))
			{
				text = (base.Menu.EnableTextHTMLEncoding ? HttpUtility.HtmlEncode(base.Owner.Text) : base.Owner.Text);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(text);
			writer.RenderEndTag();
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000B44AF File Offset: 0x000B26AF
		protected virtual void RenderIcon(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("Toggle");
			writer.RenderEndTag();
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000B44D7 File Offset: 0x000B26D7
		protected override void RenderChildItems(HtmlTextWriter writer)
		{
			this.RenderContentWrapper(writer, new Action<HtmlTextWriter>(this.RenderContent));
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000B44ED File Offset: 0x000B26ED
		protected virtual void RenderContent(HtmlTextWriter writer)
		{
			if (base.Owner.ShouldRenderScrollWrap)
			{
				this.RenderScrollWrap(writer, new Action<HtmlTextWriter>(this.RenderChildGroups));
				return;
			}
			this.RenderChildGroups(writer);
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000B4530 File Offset: 0x000B2730
		protected virtual void RenderChildGroups(HtmlTextWriter writer)
		{
			string groupCssClass = base.Owner.GetGroupCssClass();
			List<RadMenuItem> itemsToRender = new List<RadMenuItem>();
			foreach (ControlItem controlItem in base.Owner.Items.VisibleItems)
			{
				RadMenuItem item = (RadMenuItem)controlItem;
				itemsToRender.Add(item);
			}
			if (base.Owner.HasMultipleColumns)
			{
				this.RenderColumns(writer, itemsToRender, groupCssClass);
				return;
			}
			MenuItemRendererBase.RenderChildGroup(writer, itemsToRender, groupCssClass, delegate
			{
				RadMenuItem.UpdatePositionCssClass(itemsToRender);
			});
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000B45F0 File Offset: 0x000B27F0
		protected override void RenderContentWrapper(HtmlTextWriter writer, Action<HtmlTextWriter> action)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmSlide");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			action(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000B4614 File Offset: 0x000B2814
		protected virtual void RenderScrollWrap(HtmlTextWriter writer, Action<HtmlTextWriter> action)
		{
			Unit widthResolved = base.Owner.GroupSettings.WidthResolved;
			Unit heightResolved = base.Owner.GroupSettings.HeightResolved;
			if (!widthResolved.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, widthResolved.ToString());
			}
			if (!heightResolved.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, heightResolved.ToString());
			}
			string value = "rmScrollWrap rmGroup " + base.Owner.GroupLevelCssClass;
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			action(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000B46C8 File Offset: 0x000B28C8
		protected virtual void RenderColumns(HtmlTextWriter writer, IList<RadMenuItem> itemsToRender, string groupCssClass)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmMultiColumn");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			int num = Math.Min(base.Owner.GroupSettings.RepeatColumnsResolved, itemsToRender.Count);
			for (int i = 0; i < num; i++)
			{
				string text = "rmGroupColumn";
				if (i == 0)
				{
					text += " rmFirstGroupColumn";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				int repeatColumnsResolved = base.Owner.GroupSettings.RepeatColumnsResolved;
				string groupCssClass2 = string.Format("{0} {1}", groupCssClass, "rmMultiGroup");
				IList<RadMenuItem> orderedItem;
				if (base.Owner.GroupSettings.RepeatDirectionResolved == MenuRepeatDirection.Vertical)
				{
					orderedItem = ControlItemContainer.Helpers.GetRowItems<RadMenuItem>(i, repeatColumnsResolved, itemsToRender);
				}
				else
				{
					orderedItem = ControlItemContainer.Helpers.GetColumnItems<RadMenuItem>(i, repeatColumnsResolved, itemsToRender);
				}
				MenuItemRendererBase.RenderChildGroup(writer, orderedItem, groupCssClass2, delegate
				{
					RadMenuItem.UpdatePositionCssClass(orderedItem);
				});
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}
	}
}

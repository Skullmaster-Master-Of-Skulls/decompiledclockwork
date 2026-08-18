using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.Navigation
{
	// Token: 0x02000627 RID: 1575
	public class NavigationNodeRendererBase : RendererBase
	{
		// Token: 0x06003958 RID: 14680 RVA: 0x000BC401 File Offset: 0x000BA601
		public NavigationNodeRendererBase(NavigationNode node)
		{
			this.Node = node;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000BC410 File Offset: 0x000BA610
		public NavigationNodeRendererBase()
		{
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x000BC418 File Offset: 0x000BA618
		// (set) Token: 0x0600395B RID: 14683 RVA: 0x000BC420 File Offset: 0x000BA620
		protected NavigationNode Node { get; set; }

		// Token: 0x0600395C RID: 14684 RVA: 0x000BC42C File Offset: 0x000BA62C
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string text = string.Empty;
			if (this.Node.IsFirst)
			{
				text = RadNavigation.Styles.Combine(new string[]
				{
					text,
					"rnvFirst"
				});
			}
			if (this.Node.IsLast)
			{
				text = RadNavigation.Styles.Combine(new string[]
				{
					text,
					"rnvLast"
				});
			}
			if (!this.Node.Enabled)
			{
				text = RadNavigation.Styles.Combine(new string[]
				{
					text,
					"rnvDisabled"
				});
			}
			if (this.Node.Selected)
			{
				text = RadNavigation.Styles.Combine(new string[]
				{
					text,
					"rnvSelected"
				});
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadNavigation.Styles.Combine(new string[]
			{
				"rnvItem",
				text,
				this.Node.CssClass
			}));
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000BC514 File Offset: 0x000BA714
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Node.ContentTemplate != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnvSlide");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				this.RenderContentTemplate(writer);
				writer.RenderEndTag();
				return;
			}
			if (this.Node.Nodes.Count > 0)
			{
				string value = "radPopup rnvPopup";
				if (this.Node.Owner.Attributes["dir"] == "rtl")
				{
					value = RadNavigation.Styles.Combine(new string[]
					{
						"radPopup rnvPopup",
						"rnvPopup_rtl"
					});
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnvSlide");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				this.RenderChildNodes(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000BC5EA File Offset: 0x000BA7EA
		protected virtual void RenderContentTemplate(HtmlTextWriter writer)
		{
			this.Node.ContentTemplateContainer.RenderControl(writer);
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000BC600 File Offset: 0x000BA800
		protected virtual void RenderLink(HtmlTextWriter writer)
		{
			string text = string.Empty;
			if (this.Node.IsRoot)
			{
				text = "rnvRootLink";
			}
			text = RadNavigation.Styles.Combine(new string[]
			{
				text,
				"rnvLink"
			});
			if (this.Node.IsTemplateInstantiated)
			{
				text = RadNavigation.Styles.Combine(new string[]
				{
					text,
					"rnvTemplate"
				});
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			if (!string.IsNullOrEmpty(this.Node.Target))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Node.Target);
			}
			if (!string.IsNullOrEmpty(this.Node.ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Node.ToolTip);
			}
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000BC6BA File Offset: 0x000BA8BA
		protected virtual void RenderTextElement(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Node.Text))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnvText");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.Node.Text);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000BC6FC File Offset: 0x000BA8FC
		protected internal void RenderChildNodes(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnvUL");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			for (int i = 0; i < this.Node.Nodes.Count; i++)
			{
				NavigationNode navigationNode = this.Node.Nodes[i];
				if (i == 0)
				{
					navigationNode.IsFirst = true;
				}
				if (i == this.Node.Nodes.Count - 1)
				{
					navigationNode.IsLast = true;
				}
				navigationNode.Renderer.RenderContents(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000BC783 File Offset: 0x000BA983
		internal virtual void RenderImageElement(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Node.CurrentImageUrl))
			{
				this.RenderImage(writer);
				return;
			}
			if (!string.IsNullOrEmpty(this.Node.SpriteCssClass))
			{
				this.RenderImagePlaceholder(writer);
			}
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000BC7B8 File Offset: 0x000BA9B8
		internal virtual void RenderImagePlaceholder(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Node.ToolTip);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadNavigation.Styles.Combine(new string[]
			{
				"rwzSprite",
				this.Node.SpriteCssClass
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000BC814 File Offset: 0x000BAA14
		internal virtual void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.Node.ToolTip);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this.Node.ResolveClientUrl(this.Node.CurrentImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "radImage");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000BC86C File Offset: 0x000BAA6C
		internal virtual void RenderToggleButton(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadNavigation.Styles.Combine(new string[]
			{
				"rnvToggle",
				"radIcon"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000BC8AC File Offset: 0x000BAAAC
		internal virtual void RenderLinkContent(HtmlTextWriter writer)
		{
			if (this.Node.Owner.ImagePosition == RadNavigationImagePostion.Left)
			{
				this.RenderImageElement(writer);
			}
			this.RenderTextElement(writer);
			if (this.Node.Owner.ImagePosition == RadNavigationImagePostion.Right)
			{
				this.RenderImageElement(writer);
			}
			if (this.Node.ShouldRenderToggleButton)
			{
				this.RenderToggleButton(writer);
			}
		}
	}
}

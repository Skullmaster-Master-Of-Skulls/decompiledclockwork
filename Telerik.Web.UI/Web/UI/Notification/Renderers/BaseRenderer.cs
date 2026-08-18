using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.Notification.Renderers
{
	// Token: 0x0200062A RID: 1578
	public abstract class BaseRenderer : RendererBase
	{
		// Token: 0x0600396F RID: 14703 RVA: 0x000BCA8E File Offset: 0x000BAC8E
		public BaseRenderer(RadNotification notification)
		{
			this.notification = notification;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000BCAA0 File Offset: 0x000BACA0
		public virtual void RenderPopupElement(HtmlTextWriter writer, Action<HtmlTextWriter> renderBaseContent)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.notification.ClientID + "_popup");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
			string popupCssClasses = this.GetPopupCssClasses();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, popupCssClasses);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderTitleBar(writer);
			renderBaseContent(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000BCB10 File Offset: 0x000BAD10
		private string GetPopupCssClasses()
		{
			List<string> list = new List<string>
			{
				"RadNotification",
				"RadNotification_" + this.notification.RuntimeSkin
			};
			if (this.notification.EnableRoundedCorners)
			{
				list.Add("rnRoundedCorners");
			}
			if (this.notification.EnableShadow)
			{
				list.Add("rnShadows");
			}
			if (!string.IsNullOrEmpty(this.notification.CssClass))
			{
				list.Add(this.notification.CssClass);
			}
			string a = this.notification.ContentIcon.ToLower();
			if (a == "none" || a == string.Empty)
			{
				list.Add("rnNoContentIcon");
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000BCBE4 File Offset: 0x000BADE4
		public virtual void RenderTitleBar(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnTitleBar");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.notification.ClientID + "_titlebar");
			if (!this.notification.VisibleTitlebar)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderTitleBarIcon(writer);
			this.RenderTitleBarTitle(writer);
			this.RenderTitleBarCommands(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06003973 RID: 14707
		protected abstract void RenderTitleBarIcon(HtmlTextWriter writer);

		// Token: 0x06003974 RID: 14708 RVA: 0x000BCC58 File Offset: 0x000BAE58
		private void RenderTitleBarTitle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnTitleBarTitle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!string.IsNullOrEmpty(this.notification.Title))
			{
				writer.Write(this.notification.Title);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000BCC98 File Offset: 0x000BAE98
		private void RenderTitleBarCommands(HtmlTextWriter writer)
		{
			if (this.notification.ShowCloseButton || this.notification.ShowTitleMenu)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rnCommands");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				if (this.notification.ShowTitleMenu)
				{
					this.RenderCommand(writer, "rnMenuIcon", this.notification.TitleMenuToolTip);
				}
				if (this.notification.ShowCloseButton)
				{
					this.RenderCommand(writer, "rnCloseIcon", this.notification.CloseButtonToolTip);
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000BCD24 File Offset: 0x000BAF24
		protected void RenderCommand(HtmlTextWriter writer, string className, string tooltipText)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, className);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0);");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, tooltipText);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.notification.ClientID + "_" + className);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000BCD89 File Offset: 0x000BAF89
		public void RenderSimpleContent(HtmlTextWriter writer)
		{
			this.RenderSimpleContentIcon(writer);
			this.notification.RenderSimpleContentContainer(writer);
		}

		// Token: 0x06003978 RID: 14712
		protected abstract void RenderSimpleContentIcon(HtmlTextWriter writer);

		// Token: 0x04000F4E RID: 3918
		protected readonly RadNotification notification;
	}
}

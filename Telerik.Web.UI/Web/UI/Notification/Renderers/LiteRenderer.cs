using System;
using System.IO;
using System.Web.UI;

namespace Telerik.Web.UI.Notification.Renderers
{
	// Token: 0x0200062C RID: 1580
	public class LiteRenderer : BaseRenderer
	{
		// Token: 0x0600397E RID: 14718 RVA: 0x000BCF1A File Offset: 0x000BB11A
		public LiteRenderer(RadNotification notification) : base(notification)
		{
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000BCF23 File Offset: 0x000BB123
		protected override void RenderTitleBarIcon(HtmlTextWriter writer)
		{
			this.RenderIconTag(this.notification.TitleIcon, HtmlTextWriterTag.Span, writer);
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000BCF3C File Offset: 0x000BB13C
		private void RenderIconTag(string iconName, HtmlTextWriterTag iconTag, HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(iconName))
			{
				return;
			}
			this.iconName = iconName;
			string text = "rnIcon";
			if (this.IsIconUrl())
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, this.notification.ResolveUrl(this.iconName));
			}
			else
			{
				text = this.AddIconSpecificCssClass(text);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(iconTag);
			writer.RenderEndTag();
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000BCF9F File Offset: 0x000BB19F
		private bool IsIconUrl()
		{
			return Path.HasExtension(this.iconName);
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000BCFAC File Offset: 0x000BB1AC
		private string AddIconSpecificCssClass(string iconElementCssClass)
		{
			string text = this.IsBuiltInIcon() ? this.CreateBuiltInIconSpecificCssClass() : this.iconName;
			return string.Join(" ", new string[]
			{
				iconElementCssClass,
				text
			});
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000BCFEA File Offset: 0x000BB1EA
		private bool IsBuiltInIcon()
		{
			return Array.IndexOf<string>(RadNotificationSettings.BuiltInIcons, this.iconName) > -1;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000BCFFF File Offset: 0x000BB1FF
		private string CreateBuiltInIconSpecificCssClass()
		{
			return string.Format("rnIcon{0}", char.ToUpper(this.iconName[0]) + this.iconName.Substring(1));
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000BD032 File Offset: 0x000BB232
		protected override void RenderSimpleContentIcon(HtmlTextWriter writer)
		{
			this.RenderIconTag(this.notification.ContentIcon, HtmlTextWriterTag.Div, writer);
		}

		// Token: 0x04000F4F RID: 3919
		private string iconName;
	}
}

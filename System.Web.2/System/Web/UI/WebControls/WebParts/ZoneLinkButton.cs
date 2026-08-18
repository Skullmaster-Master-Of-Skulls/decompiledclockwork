using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005BD RID: 1469
	[SupportsEventValidation]
	internal sealed class ZoneLinkButton : LinkButton
	{
		// Token: 0x06004A98 RID: 19096 RVA: 0x000F7ED4 File Offset: 0x000F60D4
		public ZoneLinkButton(WebZone owner, string eventArgument)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._eventArgument = eventArgument;
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x000F7EF8 File Offset: 0x000F60F8
		// (set) Token: 0x06004A9A RID: 19098 RVA: 0x000F7F0E File Offset: 0x000F610E
		public string ImageUrl
		{
			get
			{
				if (this._imageUrl == null)
				{
					return string.Empty;
				}
				return this._imageUrl;
			}
			set
			{
				this._imageUrl = value;
			}
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x000F7F18 File Offset: 0x000F6118
		protected override PostBackOptions GetPostBackOptions()
		{
			if (!string.IsNullOrEmpty(this._eventArgument) && this._owner.Page != null)
			{
				return new PostBackOptions(this._owner, this._eventArgument)
				{
					RequiresJavaScriptProtocol = true
				};
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x000F7F60 File Offset: 0x000F6160
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			string imageUrl = this.ImageUrl;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				Image image = new Image();
				image.ImageUrl = base.ResolveClientUrl(imageUrl);
				string toolTip = this.ToolTip;
				if (!string.IsNullOrEmpty(toolTip))
				{
					image.ToolTip = toolTip;
				}
				string text = this.Text;
				if (!string.IsNullOrEmpty(text))
				{
					image.AlternateText = text;
				}
				image.Page = this.Page;
				image.RenderControl(writer);
				return;
			}
			base.RenderContents(writer);
		}

		// Token: 0x04002819 RID: 10265
		private WebZone _owner;

		// Token: 0x0400281A RID: 10266
		private string _eventArgument;

		// Token: 0x0400281B RID: 10267
		private string _imageUrl;
	}
}

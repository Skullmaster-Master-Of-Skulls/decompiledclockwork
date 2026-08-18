using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000552 RID: 1362
	internal class ImageGalleryToolbar : WebControl
	{
		// Token: 0x0600303A RID: 12346 RVA: 0x0009E3FC File Offset: 0x0009C5FC
		public ImageGalleryToolbar(RadImageGallery gallery)
		{
			this.Gallery = gallery;
			this.Settings = this.Gallery.ToolbarSettings;
			this.ID = "Toolbar";
			this.CssClass = string.Format("{0} {1}", "rigToolbar", "rigToolbar" + this.Settings.Position.ToString());
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x0009E466 File Offset: 0x0009C666
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x0009E46A File Offset: 0x0009C66A
		// (set) Token: 0x0600303D RID: 12349 RVA: 0x0009E47D File Offset: 0x0009C67D
		public override bool Visible
		{
			get
			{
				return this.Settings.Position != ImageGalleryToolbarPosition.None;
			}
			set
			{
				if (!value)
				{
					this.Settings.Position = ImageGalleryToolbarPosition.None;
				}
			}
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x0009E48E File Offset: 0x0009C68E
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Thumbnails)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0009E4B4 File Offset: 0x0009C6B4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.Gallery.ResolvedRenderMode != RenderMode.Mobile)
			{
				this.Gallery.CreateProgressBarWrapper(this);
			}
			if (this.Settings.ShowItemsCounter && this.Gallery.ResolvedRenderMode != RenderMode.Mobile)
			{
				this.Gallery.CreateItemsCounter(this);
			}
			this.CreateControlSet();
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x0009E510 File Offset: 0x0009C710
		private void CreateControlSet()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "rigControlsSet");
			this.Controls.Add(htmlGenericControl);
			htmlGenericControl.Controls.Add(new Literal
			{
				Text = "&nbsp;"
			});
			if (this.Settings.ShowSlideshowButton)
			{
				htmlGenericControl.Controls.Add(RadImageGallery.CreateButton("Play", this.Settings.PlayButtonText));
				HtmlGenericControl htmlGenericControl2 = RadImageGallery.CreateButton("Pause", this.Settings.PauseButtonText);
				htmlGenericControl2.Style.Add(HtmlTextWriterStyle.Display, "none");
				htmlGenericControl.Controls.Add(htmlGenericControl2);
			}
			if (this.Settings.ShowFullScreenButton)
			{
				htmlGenericControl.Controls.Add(RadImageGallery.CreateButton("FullScr", this.Settings.EnterFullScreenButtonText));
				HtmlGenericControl htmlGenericControl3 = RadImageGallery.CreateButton("ExtFullScr", this.Settings.ExitFullScreenButtonText);
				htmlGenericControl3.Style.Add(HtmlTextWriterStyle.Display, "none");
				htmlGenericControl.Controls.Add(htmlGenericControl3);
			}
			if (this.Settings.ShowThumbnailsToggleButton && this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image && this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.Thumbnails)
			{
				HtmlGenericControl htmlGenericControl4 = RadImageGallery.CreateButton("ShowThumbn", this.Settings.ShowThumbnailsButtonText);
				htmlGenericControl4.Style.Add(HtmlTextWriterStyle.Display, "none");
				htmlGenericControl.Controls.Add(htmlGenericControl4);
				htmlGenericControl.Controls.Add(RadImageGallery.CreateButton("HideThumbn", this.Settings.HideThumbnailsButtonText));
			}
		}

		// Token: 0x04000D11 RID: 3345
		private readonly RadImageGallery Gallery;

		// Token: 0x04000D12 RID: 3346
		private readonly ImageGalleryToolbarSettings Settings;
	}
}

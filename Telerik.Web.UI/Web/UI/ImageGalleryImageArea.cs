using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000525 RID: 1317
	internal class ImageGalleryImageArea : CompositeControl
	{
		// Token: 0x06002F01 RID: 12033 RVA: 0x00099A48 File Offset: 0x00097C48
		public ImageGalleryImageArea(RadImageGallery gallery)
		{
			this.Gallery = gallery;
			this.Settings = this.Gallery.ImageAreaSettings;
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x00099A68 File Offset: 0x00097C68
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x00099A75 File Offset: 0x00097C75
		public override Unit Width
		{
			get
			{
				return this.Settings.Width;
			}
			set
			{
				this.Settings.Width = value;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x00099A83 File Offset: 0x00097C83
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x00099A90 File Offset: 0x00097C90
		public override Unit Height
		{
			get
			{
				return this.Settings.Height;
			}
			set
			{
				this.Settings.Height = value;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x00099A9E File Offset: 0x00097C9E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00099AA4 File Offset: 0x00097CA4
		internal void PopulateItem(int itemIndex)
		{
			ImageGalleryItemBase imageGalleryItemBase = this.Gallery.Items[itemIndex];
			ImageGalleryItem imageGalleryItem = imageGalleryItemBase as ImageGalleryItem;
			if (imageGalleryItem != null)
			{
				this.PopulateItem(this.Gallery.GetImageUrl(itemIndex));
			}
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x00099AE0 File Offset: 0x00097CE0
		private void PopulateItem(string imageUrl)
		{
			Image image = this.FindControl("Image") as Image;
			if (this.Gallery.Items.Count > 0 && this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image)
			{
				ImageGalleryItem imageGalleryItem = this.Gallery.Items[this.Gallery.CurrentItemIndex] as ImageGalleryItem;
				if (imageGalleryItem != null)
				{
					if (string.IsNullOrEmpty(imageGalleryItem.Title) && string.IsNullOrEmpty(imageGalleryItem.Description) && this.descriptionBox != null)
					{
						this.descriptionBox.Style.Add(HtmlTextWriterStyle.Display, "none");
					}
					image.ImageUrl = imageUrl;
					return;
				}
			}
			else if (image != null)
			{
				image.ImageUrl = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D";
				image.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x00099BA4 File Offset: 0x00097DA4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (!this.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
			}
			if (this.Gallery.DisplayAreaMode != ImageGalleryDisplayAreaMode.Image)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			else if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image && this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.Thumbnails && !this.Gallery.ThumbnailsAreaSettings.Width.IsEmpty)
			{
				if (this.Gallery.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left || this.Gallery.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
				}
				if (this.Gallery.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Left)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Left, this.Gallery.ThumbnailsAreaSettings.Width.ToString());
				}
				else if (this.Gallery.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Right)
				{
					writer.AddStyleAttribute("right", this.Gallery.ThumbnailsAreaSettings.Width.ToString());
				}
			}
			if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image && !this.Gallery.Height.IsEmpty && this.Gallery.Height.Type == UnitType.Pixel)
			{
				double num = this.Gallery.Height.Value - (double)this.Gallery.GetPagersHeight();
				if (this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.Thumbnails && (this.Gallery.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Top || this.Gallery.ThumbnailsAreaSettings.Position == ImageGalleryThumbnailsAreaPosition.Bottom))
				{
					num -= this.Gallery.ThumbnailsAreaSettings.Height.Value;
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Unit.Pixel((int)num).ToString());
			}
			else if (!this.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00099DFC File Offset: 0x00097FFC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Thumbnails)
			{
				this.Controls.Add(RadImageGallery.CreateButton("Close", this.Settings.CloseButtonText));
			}
			if (this.Settings.ShowNextPrevImageButtons)
			{
				if (this.Gallery.ResolvedRenderMode == RenderMode.Mobile || this.Gallery.ResolvedRenderMode == RenderMode.Lightweight)
				{
					this.Controls.AddAt(0, RadImageGallery.CreateButton("Prev", this.Settings.PrevImageButtonText));
					this.Controls.Add(RadImageGallery.CreateButton("Next", this.Settings.NextImageButtonText));
				}
				else
				{
					this.Controls.AddAt(0, RadImageGallery.CreatePostbackButton("Prev", this.Settings.PrevImageButtonText, true));
					this.Controls.Add(RadImageGallery.CreatePostbackButton("Next", this.Settings.NextImageButtonText, true));
				}
			}
			this.CreateImagesArea();
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x00099EF8 File Offset: 0x000980F8
		private void CreateImagesArea()
		{
			if (this.Gallery.ToolbarSettings.Position == ImageGalleryToolbarPosition.TopInside)
			{
				this.Controls.Add(new ImageGalleryToolbar(this.Gallery));
			}
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.ID = "ImageWrapper";
			htmlGenericControl.Attributes.Add("class", "rigActiveImage");
			Image image = new Image();
			image.ID = "Image";
			image.AlternateText = "Main Image";
			htmlGenericControl.Controls.Add(image);
			ImageGalleryTemplateItem imageGalleryTemplateItem = this.Gallery.Items[this.Gallery.CurrentItemIndex] as ImageGalleryTemplateItem;
			if (imageGalleryTemplateItem != null)
			{
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
				htmlGenericControl2.ID = "TemplateWrapper";
				htmlGenericControl2.Attributes.Add("class", "rigTemplate");
				if (!imageGalleryTemplateItem.Width.IsEmpty)
				{
					htmlGenericControl2.Style.Add(HtmlTextWriterStyle.Width, imageGalleryTemplateItem.Width.ToString());
				}
				if (!imageGalleryTemplateItem.Height.IsEmpty)
				{
					htmlGenericControl2.Style.Add(HtmlTextWriterStyle.Height, imageGalleryTemplateItem.Height.ToString());
				}
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				imageGalleryTemplateItem.InstantiateTemplate(htmlGenericControl2);
				image.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			if (this.Gallery.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.Gallery.CreateProgressBarWrapper(htmlGenericControl);
			}
			if (this.Gallery.ToolbarSettings.ShowItemsCounter && this.Gallery.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.Gallery.CreateItemsCounter(htmlGenericControl);
			}
			if (this.Gallery.ResolvedRenderMode == RenderMode.Mobile && (this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.ImageSlider || this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.ImageSliderPreview))
			{
				this.CreateDotsList(htmlGenericControl);
			}
			this.Controls.Add(htmlGenericControl);
			this.CreateToolsWrapper(htmlGenericControl);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x0009A0F4 File Offset: 0x000982F4
		private void CreateToolsWrapper(Control container)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "rigToolsWrapper");
			container.Controls.Add(htmlGenericControl);
			if (this.Settings.ShowDescriptionBox)
			{
				this.CreateDescriptionBox(htmlGenericControl);
			}
			if (this.Gallery.ResolvedRenderMode != RenderMode.Mobile && (this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.ImageSlider || this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.ImageSliderPreview))
			{
				this.CreateDotsList(htmlGenericControl);
			}
			if (this.Gallery.ToolbarSettings.Position == ImageGalleryToolbarPosition.BottomInside)
			{
				htmlGenericControl.Controls.Add(new ImageGalleryToolbar(this.Gallery));
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0009A1A8 File Offset: 0x000983A8
		private void CreateDescriptionBox(HtmlGenericControl toolsWrapper)
		{
			this.descriptionBox = new HtmlGenericControl("div");
			this.descriptionBox.Attributes.Add("class", "rigDescriptionBox");
			toolsWrapper.Controls.Add(this.descriptionBox);
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("h4");
			htmlGenericControl.Attributes.Add("class", "rigTitle");
			this.descriptionBox.Controls.Add(htmlGenericControl);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("p");
			htmlGenericControl2.Attributes.Add("class", "rigDescription");
			if (this.Gallery.ResolvedRenderMode == RenderMode.Mobile)
			{
				htmlGenericControl2.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			this.descriptionBox.Controls.Add(htmlGenericControl2);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0009A274 File Offset: 0x00098474
		private void CreateDotsList(HtmlGenericControl wrapper)
		{
			this.Gallery.ThumbnailsArea.EnsureChildControlsCreated();
			if (!string.IsNullOrEmpty(this.Gallery.ThumbnailListView.DataSourceID))
			{
				this.Gallery.ThumbnailListView.DataBind();
			}
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "rigDotList");
			wrapper.Controls.Add(htmlGenericControl);
			int num = this.Gallery.AllowPaging ? Math.Min(this.Gallery.PageSize, this.Gallery.Items.Count) : this.Gallery.Items.Count;
			for (int i = 0; i < num; i++)
			{
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("a");
				htmlGenericControl2.Attributes.Add("href", "#");
				if (i == this.Gallery.CurrentItemIndex)
				{
					htmlGenericControl2.Attributes.Add("class", "rigCurrentItem");
				}
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				HtmlGenericControl child = new HtmlGenericControl("span");
				htmlGenericControl2.Controls.Add(child);
			}
			if (this.Gallery.ThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.ImageSliderPreview)
			{
				HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("div");
				htmlGenericControl3.Attributes.Add("class", "rigTooltip");
				htmlGenericControl3.Style.Add(HtmlTextWriterStyle.Display, "none");
				htmlGenericControl.Controls.Add(htmlGenericControl3);
			}
		}

		// Token: 0x04000C58 RID: 3160
		private readonly RadImageGallery Gallery;

		// Token: 0x04000C59 RID: 3161
		private readonly ImageGalleryImageAreaSettings Settings;

		// Token: 0x04000C5A RID: 3162
		private HtmlGenericControl descriptionBox;
	}
}

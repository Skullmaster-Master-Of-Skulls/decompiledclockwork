using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000551 RID: 1361
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class ImageGalleryThumbnailsAreaLayoutTemplate : ITemplate
	{
		// Token: 0x06003038 RID: 12344 RVA: 0x0009E04A File Offset: 0x0009C24A
		[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
		public ImageGalleryThumbnailsAreaLayoutTemplate(RadImageGallery gallery)
		{
			this.Gallery = gallery;
			this.Settings = this.Gallery.ThumbnailsAreaSettings;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x0009E268 File Offset: 0x0009C468
		[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
		public void InstantiateIn(Control container)
		{
			PlaceHolder placeHolder = new PlaceHolder();
			placeHolder.ID = this.Gallery.ThumbnailsArea.ListView.ItemPlaceholderID;
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Visible = (this.Settings.Mode != ImageGalleryThumbnailsAreaMode.ImageSlider);
			htmlGenericControl.Attributes.Add("class", "rigThumbnailsBox");
			container.Controls.Add(htmlGenericControl);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
			if (this.Settings.ShowScrollbar)
			{
				if (this.Settings.ScrollOrientation == ImageGalleryScrollOrientation.Horizontal)
				{
					htmlGenericControl2.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
				}
				else if (this.Settings.ScrollOrientation == ImageGalleryScrollOrientation.Vertical)
				{
					htmlGenericControl2.Style.Add(HtmlTextWriterStyle.OverflowY, "auto");
				}
			}
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			HtmlGenericControl thumbs = new HtmlGenericControl("ul");
			thumbs.Attributes.Add("class", "rigThumbnailsList");
			if (this.Settings.ThumbnailsSpacing.Value > 0.0)
			{
				thumbs.Style.Add(HtmlTextWriterStyle.Padding, string.Format("{0} 0 0 {0}", this.Settings.ThumbnailsSpacing));
			}
			thumbs.PreRender += delegate(object sender, EventArgs args)
			{
				if ((this.Settings.Mode == ImageGalleryThumbnailsAreaMode.ImageSliderPreview && this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Image) || (this.Settings.ScrollOrientation == ImageGalleryScrollOrientation.Horizontal && ((this.Gallery.DisplayAreaMode != ImageGalleryDisplayAreaMode.Thumbnails && (this.Settings.Position == ImageGalleryThumbnailsAreaPosition.Top || this.Settings.Position == ImageGalleryThumbnailsAreaPosition.Bottom)) || this.Gallery.DisplayAreaMode == ImageGalleryDisplayAreaMode.Thumbnails)))
				{
					double value = this.Settings.Width.Value;
					double num = this.Settings.Height.Value;
					double num2 = this.Settings.ThumbnailHeight.Value + this.Settings.ThumbnailsSpacing.Value;
					if (num2 > num)
					{
						num = num2;
					}
					double num3 = Math.Floor(num / num2);
					double num4 = Math.Ceiling((double)this.Gallery.Items.Count / num3);
					thumbs.Style.Add(HtmlTextWriterStyle.Width, num4 * (this.Settings.ThumbnailWidth.Value + this.Settings.ThumbnailsSpacing.Value) + "px");
				}
				else
				{
					thumbs.Style.Remove("width");
				}
				if (thumbs.Controls.Count == 0)
				{
					HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("li");
					htmlGenericControl3.Style.Add(HtmlTextWriterStyle.Display, "none");
					htmlGenericControl3.Attributes.Add("class", "rigDummy");
					thumbs.Controls.Add(htmlGenericControl3);
				}
			};
			thumbs.Controls.Add(placeHolder);
			htmlGenericControl2.Controls.Add(thumbs);
		}

		// Token: 0x04000D0F RID: 3343
		private readonly RadImageGallery Gallery;

		// Token: 0x04000D10 RID: 3344
		private readonly ImageGalleryThumbnailsAreaSettings Settings;
	}
}

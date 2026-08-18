using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000554 RID: 1364
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class ImageGalleryItem : ImageGalleryItemBase
	{
		// Token: 0x0600305A RID: 12378 RVA: 0x0009E9AB File Offset: 0x0009CBAB
		private string ReturnUrl(string url)
		{
			if (base.Gallery != null)
			{
				return base.Gallery.ResolveClientUrl(url);
			}
			return url;
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x0009E9C3 File Offset: 0x0009CBC3
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x0009E9CB File Offset: 0x0009CBCB
		internal string FileName { get; set; }

		// Token: 0x0600305D RID: 12381 RVA: 0x0009E9D4 File Offset: 0x0009CBD4
		internal string GetImageUrl()
		{
			if (this.ImageDataValue != null && this.ImageDataValue.Length > 0)
			{
				RadBinaryImage radBinaryImage = new RadBinaryImage();
				radBinaryImage.DataValue = this.ImageDataValue;
				radBinaryImage.ProcessImageData();
				return this.ReturnUrl(radBinaryImage.ImageUrl);
			}
			return this.ReturnUrl(this.ImageUrl);
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0009EA28 File Offset: 0x0009CC28
		internal override void InstantiateIn(Control control)
		{
			RadBinaryImage radBinaryImage = this.ThumbnailBinaryImage;
			control.Controls.Add(radBinaryImage);
			radBinaryImage.ID = "BinaryImage";
			radBinaryImage.Width = base.Gallery.ThumbnailsAreaSettings.ThumbnailWidth;
			radBinaryImage.Height = base.Gallery.ThumbnailsAreaSettings.ThumbnailHeight;
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x0009EA7F File Offset: 0x0009CC7F
		internal RadBinaryImage ThumbnailBinaryImage
		{
			get
			{
				if (this.thumbnailBinaryImage == null)
				{
					this.thumbnailBinaryImage = new RadBinaryImage();
					this.thumbnailBinaryImage.AutoAdjustImageControlSize = false;
					this.thumbnailBinaryImage.ResizeMode = BinaryImageResizeMode.Crop;
				}
				return this.thumbnailBinaryImage;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x0009EAB2 File Offset: 0x0009CCB2
		public override ImageGalleryItemType Type
		{
			get
			{
				return ImageGalleryItemType.Image;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x0009EAB5 File Offset: 0x0009CCB5
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x0009EAC8 File Offset: 0x0009CCC8
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string ThumbnailUrl
		{
			get
			{
				return this.ReturnUrl(this.ThumbnailBinaryImage.ImageUrl);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				this.ThumbnailBinaryImage.ImageUrl = value;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x0009EADF File Offset: 0x0009CCDF
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x0009EB00 File Offset: 0x0009CD00
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string ImageUrl
		{
			get
			{
				return (base.ViewState["ImageUrl"] as string) ?? string.Empty;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				base.ViewState["ImageUrl"] = value;
				if (string.IsNullOrEmpty(this.ThumbnailUrl))
				{
					this.ThumbnailUrl = value;
				}
				if (base.LightBoxItem != null)
				{
					base.LightBoxItem.ImageUrl = value;
				}
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x0009EB4F File Offset: 0x0009CD4F
		// (set) Token: 0x06003066 RID: 12390 RVA: 0x0009EB5C File Offset: 0x0009CD5C
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		public virtual byte[] ThumbnailDataValue
		{
			get
			{
				return this.ThumbnailBinaryImage.DataValue;
			}
			set
			{
				this.ThumbnailBinaryImage.DataValue = value;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x0009EB6A File Offset: 0x0009CD6A
		// (set) Token: 0x06003068 RID: 12392 RVA: 0x0009EB72 File Offset: 0x0009CD72
		[Browsable(false)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public virtual byte[] ImageDataValue
		{
			get
			{
				return this.imageDataValue;
			}
			set
			{
				if (this.ThumbnailBinaryImage.DataValue == null)
				{
					this.ThumbnailBinaryImage.DataValue = value;
				}
				this.imageDataValue = value;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x0009EB94 File Offset: 0x0009CD94
		// (set) Token: 0x0600306A RID: 12394 RVA: 0x0009EBB4 File Offset: 0x0009CDB4
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string NavigateUrl
		{
			get
			{
				return (base.ViewState["NavigateUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x04000D15 RID: 3349
		private RadBinaryImage thumbnailBinaryImage;

		// Token: 0x04000D16 RID: 3350
		private byte[] imageDataValue;
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020003FD RID: 1021
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class ImageGalleryTemplateItem : ImageGalleryItemBase
	{
		// Token: 0x06002564 RID: 9572 RVA: 0x0007C46D File Offset: 0x0007A66D
		private string ReturnUrl(string url)
		{
			if (base.Gallery != null)
			{
				return base.Gallery.ResolveClientUrl(url);
			}
			return url;
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x0007C485 File Offset: 0x0007A685
		// (set) Token: 0x06002566 RID: 9574 RVA: 0x0007C48D File Offset: 0x0007A68D
		internal string FileName { get; set; }

		// Token: 0x06002567 RID: 9575 RVA: 0x0007C498 File Offset: 0x0007A698
		internal override void InstantiateIn(Control control)
		{
			RadBinaryImage radBinaryImage = this.ThumbnailBinaryImage;
			control.Controls.Add(radBinaryImage);
			radBinaryImage.ID = "BinaryImage";
			radBinaryImage.Width = base.Gallery.ThumbnailsAreaSettings.ThumbnailWidth;
			radBinaryImage.Height = base.Gallery.ThumbnailsAreaSettings.ThumbnailHeight;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0007C4EF File Offset: 0x0007A6EF
		internal virtual void InstantiateTemplate(Control container)
		{
			if (this.ContentTemplate != null)
			{
				this.ContentTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x0007C505 File Offset: 0x0007A705
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

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x0007C538 File Offset: 0x0007A738
		public override ImageGalleryItemType Type
		{
			get
			{
				return ImageGalleryItemType.Template;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x0007C53B File Offset: 0x0007A73B
		// (set) Token: 0x0600256C RID: 9580 RVA: 0x0007C543 File Offset: 0x0007A743
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ContentTemplate
		{
			get
			{
				return this.template;
			}
			set
			{
				this.template = value;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x0007C54C File Offset: 0x0007A74C
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x0007C55F File Offset: 0x0007A75F
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x0007C576 File Offset: 0x0007A776
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x0007C583 File Offset: 0x0007A783
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

		// Token: 0x04000981 RID: 2433
		private RadBinaryImage thumbnailBinaryImage;

		// Token: 0x04000982 RID: 2434
		private ITemplate template;
	}
}

using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000553 RID: 1363
	public class ImageGalleryToolbarSettings : StateManager
	{
		// Token: 0x06003041 RID: 12353 RVA: 0x0009E6AD File Offset: 0x0009C8AD
		public ImageGalleryToolbarSettings(RadImageGallery gallery)
		{
			this.Gallery = gallery;
			this.Localization = this.Gallery.Localization;
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0009E6D0 File Offset: 0x0009C8D0
		// (set) Token: 0x06003043 RID: 12355 RVA: 0x0009E6F9 File Offset: 0x0009C8F9
		[NotifyParentProperty(true)]
		[DefaultValue(ImageGalleryToolbarPosition.BottomInside)]
		public ImageGalleryToolbarPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj != null)
				{
					return (ImageGalleryToolbarPosition)obj;
				}
				return ImageGalleryToolbarPosition.BottomInside;
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x0009E714 File Offset: 0x0009C914
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x0009E73D File Offset: 0x0009C93D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowItemsCounter
		{
			get
			{
				object obj = base.ViewState["ShowItemsCounter"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowItemsCounter"] = value;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x0009E755 File Offset: 0x0009C955
		// (set) Token: 0x06003047 RID: 12359 RVA: 0x0009E77B File Offset: 0x0009C97B
		[Localizable(true)]
		[DefaultValue("Item {0} of {1}")]
		[NotifyParentProperty(true)]
		public string ItemsCounterFormat
		{
			get
			{
				return (base.ViewState["ItemsCounterFormat"] as string) ?? this.Localization.ItemsCounterFormat;
			}
			set
			{
				base.ViewState["ItemsCounterFormat"] = value;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x0009E790 File Offset: 0x0009C990
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x0009E7B9 File Offset: 0x0009C9B9
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool ShowSlideshowButton
		{
			get
			{
				object obj = base.ViewState["ShowSlideshowButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowSlideshowButton"] = value;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x0009E7D1 File Offset: 0x0009C9D1
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x0009E7F7 File Offset: 0x0009C9F7
		[DefaultValue("Play Slideshow")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string PlayButtonText
		{
			get
			{
				return (base.ViewState["PlayButtonText"] as string) ?? this.Localization.PlayButtonText;
			}
			set
			{
				base.ViewState["PlayButtonText"] = value;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x0009E80A File Offset: 0x0009CA0A
		// (set) Token: 0x0600304D RID: 12365 RVA: 0x0009E830 File Offset: 0x0009CA30
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Pause Slideshow")]
		public string PauseButtonText
		{
			get
			{
				return (base.ViewState["PauseButtonText"] as string) ?? this.Localization.PauseButtonText;
			}
			set
			{
				base.ViewState["PauseButtonText"] = value;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x0009E844 File Offset: 0x0009CA44
		// (set) Token: 0x0600304F RID: 12367 RVA: 0x0009E86D File Offset: 0x0009CA6D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowFullScreenButton
		{
			get
			{
				object obj = base.ViewState["ShowPlayButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowPlayButton"] = value;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x0009E885 File Offset: 0x0009CA85
		// (set) Token: 0x06003051 RID: 12369 RVA: 0x0009E8AB File Offset: 0x0009CAAB
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Enter FullScreen")]
		public string EnterFullScreenButtonText
		{
			get
			{
				return (base.ViewState["EnterFullScreenButtonText"] as string) ?? this.Localization.EnterFullScreenButtonText;
			}
			set
			{
				base.ViewState["EnterFullScreenButtonText"] = value;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x0009E8BE File Offset: 0x0009CABE
		// (set) Token: 0x06003053 RID: 12371 RVA: 0x0009E8E4 File Offset: 0x0009CAE4
		[DefaultValue("Exit FullScreen")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ExitFullScreenButtonText
		{
			get
			{
				return (base.ViewState["ExitFullScreenButtonText"] as string) ?? this.Localization.ExitFullScreenButtonText;
			}
			set
			{
				base.ViewState["ExitFullScreenButtonText"] = value;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0009E8F8 File Offset: 0x0009CAF8
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x0009E921 File Offset: 0x0009CB21
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool ShowThumbnailsToggleButton
		{
			get
			{
				object obj = base.ViewState["ShowThumbnailsToggleButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowThumbnailsToggleButton"] = value;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x0009E939 File Offset: 0x0009CB39
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x0009E95F File Offset: 0x0009CB5F
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Show Thumbnails")]
		public string ShowThumbnailsButtonText
		{
			get
			{
				return (base.ViewState["ShowThumbnailsButtonText"] as string) ?? this.Localization.ShowThumbnailsButtonText;
			}
			set
			{
				base.ViewState["ShowThumbnailsButtonText"] = value;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x0009E972 File Offset: 0x0009CB72
		// (set) Token: 0x06003059 RID: 12377 RVA: 0x0009E998 File Offset: 0x0009CB98
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Hide Thumbnails")]
		public string HideThumbnailsButtonText
		{
			get
			{
				return (base.ViewState["HideThumbnailsButtonText"] as string) ?? this.Localization.HideThumbnailsButtonText;
			}
			set
			{
				base.ViewState["HideThumbnailsButtonText"] = value;
			}
		}

		// Token: 0x04000D13 RID: 3347
		private readonly RadImageGallery Gallery;

		// Token: 0x04000D14 RID: 3348
		private readonly ImageGalleryStrings Localization;
	}
}

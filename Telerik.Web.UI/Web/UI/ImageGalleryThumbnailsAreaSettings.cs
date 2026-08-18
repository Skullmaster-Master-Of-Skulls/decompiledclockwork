using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000559 RID: 1369
	public class ImageGalleryThumbnailsAreaSettings : StateManager
	{
		// Token: 0x0600310E RID: 12558 RVA: 0x000A162F File Offset: 0x0009F82F
		public ImageGalleryThumbnailsAreaSettings(RadImageGallery gallery)
		{
			this.Gallery = gallery;
			this.Localization = this.Gallery.Localization;
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000A164F File Offset: 0x0009F84F
		internal bool IsDefault()
		{
			return false;
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000A1652 File Offset: 0x0009F852
		internal bool IsHeightSet
		{
			get
			{
				return base.ViewState["Height"] != null;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x000A166C File Offset: 0x0009F86C
		// (set) Token: 0x06003112 RID: 12562 RVA: 0x000A16C5 File Offset: 0x0009F8C5
		[Description("Gets or sets the width of the RadImageGallery ThumbnailArea.")]
		[DefaultValue(typeof(Unit), "100%")]
		[NotifyParentProperty(true)]
		public Unit Width
		{
			get
			{
				object obj = base.ViewState["Width"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				if (this.Mode == ImageGalleryThumbnailsAreaMode.Thumbnails && (this.Position == ImageGalleryThumbnailsAreaPosition.Left || this.Position == ImageGalleryThumbnailsAreaPosition.Right))
				{
					return Unit.Pixel(100);
				}
				return Unit.Percentage(100.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000A16E0 File Offset: 0x0009F8E0
		// (set) Token: 0x06003114 RID: 12564 RVA: 0x000A1739 File Offset: 0x0009F939
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "100px")]
		[Description("Gets or sets the height of the RadImageGallery ThumbnailArea.")]
		public Unit Height
		{
			get
			{
				object obj = base.ViewState["Height"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				if (this.Mode == ImageGalleryThumbnailsAreaMode.Thumbnails && (this.Position == ImageGalleryThumbnailsAreaPosition.Left || this.Position == ImageGalleryThumbnailsAreaPosition.Right))
				{
					return Unit.Percentage(100.0);
				}
				return Unit.Pixel(100);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000A1754 File Offset: 0x0009F954
		// (set) Token: 0x06003116 RID: 12566 RVA: 0x000A1783 File Offset: 0x0009F983
		[Description("Gets or sets the width of each thumbnail item in the area.")]
		[DefaultValue(typeof(Unit), "100px")]
		[NotifyParentProperty(true)]
		public Unit ThumbnailWidth
		{
			get
			{
				object obj = base.ViewState["ThumbnailWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(100);
			}
			set
			{
				if (value.Type != UnitType.Pixel)
				{
					throw new ArgumentException("Only UnitType.Pixel is supported");
				}
				base.ViewState["ThumbnailWidth"] = value;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000A17B0 File Offset: 0x0009F9B0
		// (set) Token: 0x06003118 RID: 12568 RVA: 0x000A17DF File Offset: 0x0009F9DF
		[DefaultValue(typeof(Unit), "100px")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the height of each thumbnail item in the area.")]
		public Unit ThumbnailHeight
		{
			get
			{
				object obj = base.ViewState["ThumbnailHeight"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(100);
			}
			set
			{
				if (value.Type != UnitType.Pixel)
				{
					throw new ArgumentException("Only UnitType.Pixel is supported");
				}
				base.ViewState["ThumbnailHeight"] = value;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000A180C File Offset: 0x0009FA0C
		// (set) Token: 0x0600311A RID: 12570 RVA: 0x000A183A File Offset: 0x0009FA3A
		[DefaultValue(typeof(Unit), "0px")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the height of each thumbnail item in the area.")]
		public Unit ThumbnailsSpacing
		{
			get
			{
				object obj = base.ViewState["ThumbnailsSpacing"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(0);
			}
			set
			{
				if (value.Type != UnitType.Pixel)
				{
					throw new ArgumentException("Only UnitType.Pixel is supported");
				}
				base.ViewState["ThumbnailsSpacing"] = value;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000A1868 File Offset: 0x0009FA68
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x000A1891 File Offset: 0x0009FA91
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value determining if the ThumbnailArea will be automatically scrolled when the mouse is close enough from the left or right side of the area.")]
		public bool EnableZoneScroll
		{
			get
			{
				object obj = base.ViewState["EnableZoneScroll"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableZoneScroll"] = value;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000A18AC File Offset: 0x0009FAAC
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x000A18D5 File Offset: 0x0009FAD5
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value determining if a scrollbar will be displayed in the ThumbnailArea.")]
		[DefaultValue(false)]
		public bool ShowScrollbar
		{
			get
			{
				object obj = base.ViewState["ShowScrollbar"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ShowScrollbar"] = value;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000A18F0 File Offset: 0x0009FAF0
		// (set) Token: 0x06003120 RID: 12576 RVA: 0x000A1919 File Offset: 0x0009FB19
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating if the buttons that scroll the ThumbnailArea view will be visible.")]
		[DefaultValue(true)]
		public bool ShowScrollButtons
		{
			get
			{
				object obj = base.ViewState["ShowScrollButtons"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowScrollButtons"] = value;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000A1931 File Offset: 0x0009FB31
		// (set) Token: 0x06003122 RID: 12578 RVA: 0x000A1957 File Offset: 0x0009FB57
		[Description("Gets or sets the tooltip and alternative text for the ScrollPrev button.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Scroll Prev")]
		public string ScrollPrevButtonText
		{
			get
			{
				return (base.ViewState["ScrollPrevButtonText"] as string) ?? this.Localization.ScrollPrevButtonText;
			}
			set
			{
				base.ViewState["ScrollPrevButtonText"] = value;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x000A196A File Offset: 0x0009FB6A
		// (set) Token: 0x06003124 RID: 12580 RVA: 0x000A1990 File Offset: 0x0009FB90
		[Description("Gets or sets the tooltip and alternative text for the ScrollNext button.")]
		[Localizable(true)]
		[DefaultValue("Scroll Next")]
		[NotifyParentProperty(true)]
		public string ScrollNextButtonText
		{
			get
			{
				return (base.ViewState["ScrollNextButtonText"] as string) ?? this.Localization.ScrollNextButtonText;
			}
			set
			{
				base.ViewState["ScrollNextButtonText"] = value;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x000A19A4 File Offset: 0x0009FBA4
		// (set) Token: 0x06003126 RID: 12582 RVA: 0x000A19CD File Offset: 0x0009FBCD
		[DefaultValue(ImageGalleryScrollButtonsTrigger.Click)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets an enumeration value determining when the scroll buttons action will be triggered. Note that scroll amount is changed based on the value specified.")]
		public ImageGalleryScrollButtonsTrigger ScrollButtonsTrigger
		{
			get
			{
				object obj = base.ViewState["ScrollButtonsTrigger"];
				if (obj != null)
				{
					return (ImageGalleryScrollButtonsTrigger)obj;
				}
				return ImageGalleryScrollButtonsTrigger.Click;
			}
			set
			{
				base.ViewState["ScrollButtonsTrigger"] = value;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000A19E8 File Offset: 0x0009FBE8
		// (set) Token: 0x06003128 RID: 12584 RVA: 0x000A1A11 File Offset: 0x0009FC11
		[Description("Gets or sets a value determining where the scrollbar will be positioned and in what direction (horizontally or vertically) the content will be moved.")]
		[DefaultValue(ImageGalleryScrollOrientation.Horizontal)]
		[NotifyParentProperty(true)]
		public ImageGalleryScrollOrientation ScrollOrientation
		{
			get
			{
				object obj = base.ViewState["ScrollOrientation"];
				if (obj != null)
				{
					return (ImageGalleryScrollOrientation)obj;
				}
				return ImageGalleryScrollOrientation.Horizontal;
			}
			set
			{
				base.ViewState["ScrollOrientation"] = value;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000A1A2C File Offset: 0x0009FC2C
		// (set) Token: 0x0600312A RID: 12586 RVA: 0x000A1A55 File Offset: 0x0009FC55
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value determining where the ThumbnailArea will be positioned. The are 8 positions which give freedom in choosing to place the are inside or outside the ContentArea. Note that the position will have effect only when DisplayAreaMode='ContentArea'>")]
		[DefaultValue(ImageGalleryThumbnailsAreaPosition.Bottom)]
		public ImageGalleryThumbnailsAreaPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj != null)
				{
					return (ImageGalleryThumbnailsAreaPosition)obj;
				}
				return ImageGalleryThumbnailsAreaPosition.Bottom;
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000A1A70 File Offset: 0x0009FC70
		// (set) Token: 0x0600312C RID: 12588 RVA: 0x000A1A99 File Offset: 0x0009FC99
		[DefaultValue(ImageGalleryThumbnailsAreaMode.Thumbnails)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets an enumeration determining how the ThumbnailArea will look and function.")]
		public ImageGalleryThumbnailsAreaMode Mode
		{
			get
			{
				object obj = base.ViewState["Mode"];
				if (obj != null)
				{
					return (ImageGalleryThumbnailsAreaMode)obj;
				}
				return ImageGalleryThumbnailsAreaMode.Thumbnails;
			}
			set
			{
				base.ViewState["Mode"] = value;
			}
		}

		// Token: 0x04000D58 RID: 3416
		private readonly RadImageGallery Gallery;

		// Token: 0x04000D59 RID: 3417
		private readonly ImageGalleryStrings Localization;
	}
}

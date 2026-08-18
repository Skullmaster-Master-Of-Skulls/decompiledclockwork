using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000526 RID: 1318
	public class ImageGalleryImageAreaSettings : StateManager
	{
		// Token: 0x06002F0F RID: 12047 RVA: 0x0009A3ED File Offset: 0x000985ED
		public ImageGalleryImageAreaSettings(RadImageGallery gallery)
		{
			this.Gallery = gallery;
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0009A3FC File Offset: 0x000985FC
		internal bool IsDefault()
		{
			return this.Width == Unit.Percentage(100.0) && this.Height == Unit.Pixel(100);
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x0009A42D File Offset: 0x0009862D
		internal bool IsHeightSet
		{
			get
			{
				return base.ViewState["Height"] != null;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x0009A448 File Offset: 0x00098648
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x0009A475 File Offset: 0x00098675
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the width of the RadImageGallery ContentArea. Takes effect only when RadImageGallery.ContentViewMode is ContentArea. In ThumnailArea mode the ContentArea width is determined by the RadImageGallery.ThumbnailAreaSettings.Width property.")]
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
				return Unit.Empty;
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x0009A490 File Offset: 0x00098690
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x0009A4C2 File Offset: 0x000986C2
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "600px")]
		[Description("Gets or sets the height of the RadImageGallery ContentArea. Takes effect only when RadImageGallery.ContentViewMode is ContentArea. In ThumnailArea mode the ContentArea height is determined by the RadImageGallery.ThumbnailAreaSettings.Height property.")]
		public Unit Height
		{
			get
			{
				object obj = base.ViewState["Height"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(600);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x0009A4DC File Offset: 0x000986DC
		// (set) Token: 0x06002F17 RID: 12055 RVA: 0x0009A505 File Offset: 0x00098705
		[DefaultValue(true)]
		[Description("Gets or sets a value determining if the box that holds the item title and description will be visible.")]
		[NotifyParentProperty(true)]
		public bool ShowDescriptionBox
		{
			get
			{
				object obj = base.ViewState["ShowDescriptionBox"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowDescriptionBox"] = value;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x0009A520 File Offset: 0x00098720
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x0009A549 File Offset: 0x00098749
		[Description("Gets or sets a value determining if the Next/Prev navigate buttons will be visible.")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowNextPrevImageButtons
		{
			get
			{
				object obj = base.ViewState["ShowNextPrevImageButtons"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowNextPrevImageButtons"] = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x0009A561 File Offset: 0x00098761
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x0009A58C File Offset: 0x0009878C
		[Localizable(true)]
		[Description("Gets or sets the tooltip and alternative text for the previous image button.")]
		[DefaultValue("Previous Image")]
		[NotifyParentProperty(true)]
		public string PrevImageButtonText
		{
			get
			{
				return (base.ViewState["PrevImageButtonText"] as string) ?? this.Gallery.Localization.PrevImageButtonText;
			}
			set
			{
				base.ViewState["PrevImageButtonText"] = value;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x0009A59F File Offset: 0x0009879F
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x0009A5CA File Offset: 0x000987CA
		[DefaultValue("Next Image")]
		[Description("Gets or sets the tooltip and alternative text for the next image button.")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string NextImageButtonText
		{
			get
			{
				return (base.ViewState["NextImageButtonText"] as string) ?? this.Gallery.Localization.NextImageButtonText;
			}
			set
			{
				base.ViewState["NextImageButtonText"] = value;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x0009A5DD File Offset: 0x000987DD
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x0009A608 File Offset: 0x00098808
		[Description("Gets or sets the tooltip and alternative text for the close button.")]
		[Localizable(true)]
		[DefaultValue("Close")]
		[NotifyParentProperty(true)]
		public string CloseButtonText
		{
			get
			{
				return (base.ViewState["CloseButtonText"] as string) ?? this.Gallery.Localization.CloseButtonText;
			}
			set
			{
				base.ViewState["CloseButtonText"] = value;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x0009A61C File Offset: 0x0009881C
		// (set) Token: 0x06002F21 RID: 12065 RVA: 0x0009A645 File Offset: 0x00098845
		[NotifyParentProperty(true)]
		[DefaultValue(ImageGalleryNavigationMode.Button)]
		[Description("Gets or sets an enumeration determining the way images will be navigated. Either by using the buttons or by just clicking on one side of the ContentArea.")]
		public ImageGalleryNavigationMode NavigationMode
		{
			get
			{
				object obj = base.ViewState["NavigationMode"];
				if (obj != null)
				{
					return (ImageGalleryNavigationMode)obj;
				}
				return ImageGalleryNavigationMode.Button;
			}
			set
			{
				base.ViewState["NavigationMode"] = value;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x0009A660 File Offset: 0x00098860
		// (set) Token: 0x06002F23 RID: 12067 RVA: 0x0009A689 File Offset: 0x00098889
		[DefaultValue(ImageGalleryResizeMode.Fit)]
		[Description("Determines the way the image will be resized when placed in the ImageArea. Fit will scale the image so it is entirely visible. Fill will fill the entire area but will crop part of the image.")]
		[NotifyParentProperty(true)]
		public ImageGalleryResizeMode ResizeMode
		{
			get
			{
				object obj = base.ViewState["ResizeMode"];
				if (obj != null)
				{
					return (ImageGalleryResizeMode)obj;
				}
				return ImageGalleryResizeMode.Fit;
			}
			set
			{
				base.ViewState["ResizeMode"] = value;
			}
		}

		// Token: 0x04000C5B RID: 3163
		private readonly RadImageGallery Gallery;
	}
}

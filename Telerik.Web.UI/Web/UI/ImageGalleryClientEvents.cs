using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200053A RID: 1338
	public class ImageGalleryClientEvents : ImageGallerySettings
	{
		// Token: 0x06002F51 RID: 12113 RVA: 0x0009AB8D File Offset: 0x00098D8D
		public ImageGalleryClientEvents(RadImageGallery gallery) : base(gallery)
		{
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x0009AB96 File Offset: 0x00098D96
		// (set) Token: 0x06002F53 RID: 12115 RVA: 0x0009ABB6 File Offset: 0x00098DB6
		[Description("Client-side event fired when the RadImageGallery client-side object finishes initialization.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string OnImageGalleryCreated
		{
			get
			{
				return (base.ViewState["OnImageGalleryCreated"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnImageGalleryCreated"] = value;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x0009ABC9 File Offset: 0x00098DC9
		// (set) Token: 0x06002F55 RID: 12117 RVA: 0x0009ABE9 File Offset: 0x00098DE9
		[NotifyParentProperty(true)]
		[Description("Client-side event fired before the RadImageGallery enters full screen mode. Event could be canceled in order to prevent RadImageGallery from entering full screen mode.")]
		[DefaultValue("")]
		public string OnFullScreenEntering
		{
			get
			{
				return (base.ViewState["OnFullScreenEntering"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnFullScreenEntering"] = value;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x0009ABFC File Offset: 0x00098DFC
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x0009AC1C File Offset: 0x00098E1C
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Client-side event fired after the RadImageGallery have entered full screen mode.")]
		public string OnFullScreenEntered
		{
			get
			{
				return (base.ViewState["OnFullScreenEntered"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnFullScreenEntered"] = value;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x0009AC2F File Offset: 0x00098E2F
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x0009AC4F File Offset: 0x00098E4F
		[Description("Client-side event fired after RadImageGallery have exited full screen mode.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string OnFullScreenExited
		{
			get
			{
				return (base.ViewState["OnFullScreenExited"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnFullScreenExited"] = value;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x0009AC62 File Offset: 0x00098E62
		// (set) Token: 0x06002F5B RID: 12123 RVA: 0x0009AC82 File Offset: 0x00098E82
		[Description("Client-side event fired just before the slideshow functionality is turned on. Event could be canceled in order to prevent the start of a slideshow.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnSlideshowPlay
		{
			get
			{
				return (base.ViewState["OnSlideshowPlay"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnSlideshowPlay"] = value;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x0009AC95 File Offset: 0x00098E95
		// (set) Token: 0x06002F5D RID: 12125 RVA: 0x0009ACB5 File Offset: 0x00098EB5
		[NotifyParentProperty(true)]
		[Description("Client-side event fired just before the slideshow functionality is turned off. Event could be canceled in order to prevent the pause of a slideshow.")]
		[DefaultValue("")]
		public string OnSlideshowStop
		{
			get
			{
				return (base.ViewState["OnSlideshowStop"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnSlideshowStop"] = value;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x0009ACC8 File Offset: 0x00098EC8
		// (set) Token: 0x06002F5F RID: 12127 RVA: 0x0009ACE8 File Offset: 0x00098EE8
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Client-side event fired before changing the selection of an item and navigating to different one. Event could be canceled in order to prevent navigation.")]
		public string OnNavigating
		{
			get
			{
				return (base.ViewState["OnNavigating"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnNavigating"] = value;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x0009ACFB File Offset: 0x00098EFB
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x0009AD1B File Offset: 0x00098F1B
		[Description("Client-side event fired after a change in the selected item and navigation to a different one.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string OnNavigated
		{
			get
			{
				return (base.ViewState["OnNavigated"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnNavigated"] = value;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x0009AD2E File Offset: 0x00098F2E
		// (set) Token: 0x06002F63 RID: 12131 RVA: 0x0009AD4E File Offset: 0x00098F4E
		[Description("Client-side event fired before an image is requested. Subscribe to the event in order to provide your custom image URL that you want to load.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string OnImageLoading
		{
			get
			{
				return (base.ViewState["OnImageLoading"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnImageLoading"] = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x0009AD61 File Offset: 0x00098F61
		// (set) Token: 0x06002F65 RID: 12133 RVA: 0x0009AD81 File Offset: 0x00098F81
		[NotifyParentProperty(true)]
		[Description("Client-side event fired after an image have been loaded. In the event handler you will have access to the image.")]
		[DefaultValue("")]
		public string OnImageLoaded
		{
			get
			{
				return (base.ViewState["OnImageLoaded"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnImageLoaded"] = value;
			}
		}
	}
}

using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200054E RID: 1358
	public class ImageGalleryPagerStyle : ImageGallerySettings
	{
		// Token: 0x06003021 RID: 12321 RVA: 0x0009D8C5 File Offset: 0x0009BAC5
		public ImageGalleryPagerStyle(RadImageGallery gallery) : base(gallery)
		{
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x0009D8CE File Offset: 0x0009BACE
		private ImageGalleryStrings Localization
		{
			get
			{
				return this.Gallery.Localization;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x0009D8DC File Offset: 0x0009BADC
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x0009D90A File Offset: 0x0009BB0A
		[Description("Gets or sets an enumeration representing where pager items will be created relative to the RadImageGallery position.")]
		[NotifyParentProperty(true)]
		[DefaultValue(ImageGalleryPagerPosition.Bottom)]
		[Category("Appearance")]
		public ImageGalleryPagerPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj == null)
				{
					obj = ImageGalleryPagerPosition.Bottom;
				}
				return (ImageGalleryPagerPosition)obj;
			}
			set
			{
				if (value < ImageGalleryPagerPosition.Bottom || value > ImageGalleryPagerPosition.TopAndBottom)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x0009D938 File Offset: 0x0009BB38
		// (set) Token: 0x06003026 RID: 12326 RVA: 0x0009D962 File Offset: 0x0009BB62
		[NotifyParentProperty(true)]
		[Description("Gets or sets the number of page buttons that will be rendered if the pager is in mode that renders the page buttons")]
		[Category("Appearance")]
		[DefaultValue(10)]
		public int PageButtonCount
		{
			get
			{
				object obj = base.ViewState["PageButtonCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["PageButtonCount"] = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x0009D98C File Offset: 0x0009BB8C
		// (set) Token: 0x06003028 RID: 12328 RVA: 0x0009D9B5 File Offset: 0x0009BBB5
		[Description("Gets or sets if the pager item will be still visible when there is only one page")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool AlwaysVisible
		{
			get
			{
				object obj = base.ViewState["AlwaysVisible"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AlwaysVisible"] = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
		// (set) Token: 0x0600302A RID: 12330 RVA: 0x0009D9F9 File Offset: 0x0009BBF9
		[Description("Gets or sets a value indicating whether the pager text will be visible")]
		[DefaultValue(true)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public bool ShowPagerText
		{
			get
			{
				object obj = base.ViewState["ShowPagerText"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowPagerText"] = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x0009DA11 File Offset: 0x0009BC11
		// (set) Token: 0x0600302C RID: 12332 RVA: 0x0009DA37 File Offset: 0x0009BC37
		[Localizable(true)]
		[Description("The string used to format the description text that appears in a pager item.")]
		[Category("Appearance")]
		[DefaultValue("Page {0} of {1}")]
		[NotifyParentProperty(true)]
		public string PagerTextFormat
		{
			get
			{
				return (base.ViewState["PagerTextFormat"] as string) ?? this.Localization.PagerTextFormat;
			}
			set
			{
				base.ViewState["PagerTextFormat"] = value;
			}
		}
	}
}

using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000545 RID: 1349
	internal class ImageGalleryStrings : LocalizationStrings
	{
		// Token: 0x06002F94 RID: 12180 RVA: 0x0009BE1F File Offset: 0x0009A01F
		public ImageGalleryStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x0009BE2F File Offset: 0x0009A02F
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x0009BE48 File Offset: 0x0009A048
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x0009BE55 File Offset: 0x0009A055
		[DefaultValue("Previous Image")]
		[NotifyParentProperty(true)]
		public string PrevImageButtonText
		{
			get
			{
				return this.GetString("PrevImageButtonText");
			}
			set
			{
				this.SetString("PrevImageButtonText", value);
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x0009BE63 File Offset: 0x0009A063
		// (set) Token: 0x06002F99 RID: 12185 RVA: 0x0009BE70 File Offset: 0x0009A070
		[DefaultValue("Next Image")]
		[NotifyParentProperty(true)]
		public string NextImageButtonText
		{
			get
			{
				return this.GetString("NextImageButtonText");
			}
			set
			{
				this.SetString("NextImageButtonText", value);
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x0009BE7E File Offset: 0x0009A07E
		// (set) Token: 0x06002F9B RID: 12187 RVA: 0x0009BE8B File Offset: 0x0009A08B
		[NotifyParentProperty(true)]
		[DefaultValue("Close")]
		public string CloseButtonText
		{
			get
			{
				return this.GetString("CloseButtonText");
			}
			set
			{
				this.SetString("CloseButtonText", value);
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x0009BE99 File Offset: 0x0009A099
		// (set) Token: 0x06002F9D RID: 12189 RVA: 0x0009BEA6 File Offset: 0x0009A0A6
		[NotifyParentProperty(true)]
		[DefaultValue("Scroll Prev")]
		public string ScrollPrevButtonText
		{
			get
			{
				return this.GetString("ScrollPrevButtonText");
			}
			set
			{
				this.SetString("ScrollPrevButtonText", value);
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x0009BEB4 File Offset: 0x0009A0B4
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x0009BEC1 File Offset: 0x0009A0C1
		[NotifyParentProperty(true)]
		[DefaultValue("Scroll Next")]
		public string ScrollNextButtonText
		{
			get
			{
				return this.GetString("ScrollNextButtonText");
			}
			set
			{
				this.SetString("ScrollNextButtonText", value);
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x0009BECF File Offset: 0x0009A0CF
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x0009BEDC File Offset: 0x0009A0DC
		[NotifyParentProperty(true)]
		[DefaultValue("Item {0} of {1}")]
		public string ItemsCounterFormat
		{
			get
			{
				return this.GetString("ItemsCounterFormat");
			}
			set
			{
				this.SetString("ItemsCounterFormat", value);
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x0009BEEA File Offset: 0x0009A0EA
		// (set) Token: 0x06002FA3 RID: 12195 RVA: 0x0009BEF7 File Offset: 0x0009A0F7
		[NotifyParentProperty(true)]
		[DefaultValue("{0} / {1}")]
		public string MobileItemsCounterFormat
		{
			get
			{
				return this.GetString("MobileItemsCounterFormat");
			}
			set
			{
				this.SetString("MobileItemsCounterFormat", value);
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x0009BF05 File Offset: 0x0009A105
		// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x0009BF12 File Offset: 0x0009A112
		[DefaultValue("Play Slideshow")]
		[NotifyParentProperty(true)]
		public string PlayButtonText
		{
			get
			{
				return this.GetString("PlayButtonText");
			}
			set
			{
				this.SetString("PlayButtonText", value);
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x0009BF20 File Offset: 0x0009A120
		// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x0009BF2D File Offset: 0x0009A12D
		[NotifyParentProperty(true)]
		[DefaultValue("Pause Slideshow")]
		public string PauseButtonText
		{
			get
			{
				return this.GetString("PauseButtonText");
			}
			set
			{
				this.SetString("PauseButtonText", value);
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x0009BF3B File Offset: 0x0009A13B
		// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x0009BF48 File Offset: 0x0009A148
		[NotifyParentProperty(true)]
		[DefaultValue("Enter FullScreen")]
		public string EnterFullScreenButtonText
		{
			get
			{
				return this.GetString("EnterFullScreenButtonText");
			}
			set
			{
				this.SetString("EnterFullScreenButtonText", value);
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x0009BF56 File Offset: 0x0009A156
		// (set) Token: 0x06002FAB RID: 12203 RVA: 0x0009BF63 File Offset: 0x0009A163
		[DefaultValue("Exit FullScreen")]
		[NotifyParentProperty(true)]
		public string ExitFullScreenButtonText
		{
			get
			{
				return this.GetString("ExitFullScreenButtonText");
			}
			set
			{
				this.SetString("ExitFullScreenButtonText", value);
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x0009BF71 File Offset: 0x0009A171
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x0009BF7E File Offset: 0x0009A17E
		[DefaultValue("Show Thumbnails")]
		[NotifyParentProperty(true)]
		public string ShowThumbnailsButtonText
		{
			get
			{
				return this.GetString("ShowThumbnailsButtonText");
			}
			set
			{
				this.SetString("ShowThumbnailsButtonText", value);
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x0009BF8C File Offset: 0x0009A18C
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x0009BF99 File Offset: 0x0009A199
		[DefaultValue("Hide Thumbnails")]
		[NotifyParentProperty(true)]
		public string HideThumbnailsButtonText
		{
			get
			{
				return this.GetString("HideThumbnailsButtonText");
			}
			set
			{
				this.SetString("HideThumbnailsButtonText", value);
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x0009BFA7 File Offset: 0x0009A1A7
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x0009BFB4 File Offset: 0x0009A1B4
		[NotifyParentProperty(true)]
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
		public string PagerTextFormat
		{
			get
			{
				return this.GetString("PagerTextFormat");
			}
			set
			{
				this.SetString("PagerTextFormat", value);
			}
		}

		// Token: 0x04000CCB RID: 3275
		private readonly LocalizationProvider _localizationProvider;
	}
}

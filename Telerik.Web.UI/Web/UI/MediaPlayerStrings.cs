using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020005C5 RID: 1477
	internal class MediaPlayerStrings : LocalizationStrings
	{
		// Token: 0x060034E6 RID: 13542 RVA: 0x000AE9A8 File Offset: 0x000ACBA8
		public MediaPlayerStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000AE9B8 File Offset: 0x000ACBB8
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000AE9D1 File Offset: 0x000ACBD1
		// (set) Token: 0x060034E9 RID: 13545 RVA: 0x000AE9DE File Offset: 0x000ACBDE
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Title
		{
			get
			{
				return this.GetString("Title");
			}
			set
			{
				this.SetString("Title", value);
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000AE9EC File Offset: 0x000ACBEC
		// (set) Token: 0x060034EB RID: 13547 RVA: 0x000AE9F9 File Offset: 0x000ACBF9
		[NotifyParentProperty(true)]
		[DefaultValue("Mute")]
		public string VolumeButtonToolTip
		{
			get
			{
				return this.GetString("VolumeButtonToolTip");
			}
			set
			{
				this.SetString("VolumeButtonToolTip", value);
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000AEA07 File Offset: 0x000ACC07
		// (set) Token: 0x060034ED RID: 13549 RVA: 0x000AEA14 File Offset: 0x000ACC14
		[DefaultValue("HD")]
		[NotifyParentProperty(true)]
		public string HDButtonToolTip
		{
			get
			{
				return this.GetString("HDButtonToolTip");
			}
			set
			{
				this.SetString("HDButtonToolTip", value);
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000AEA22 File Offset: 0x000ACC22
		// (set) Token: 0x060034EF RID: 13551 RVA: 0x000AEA2F File Offset: 0x000ACC2F
		[DefaultValue("Subtitles")]
		[NotifyParentProperty(true)]
		public string SubtitlesButtonToolTip
		{
			get
			{
				return this.GetString("SubtitlesButtonToolTip");
			}
			set
			{
				this.SetString("SubtitlesButtonToolTip", value);
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x000AEA3D File Offset: 0x000ACC3D
		// (set) Token: 0x060034F1 RID: 13553 RVA: 0x000AEA4A File Offset: 0x000ACC4A
		[DefaultValue("Close")]
		[NotifyParentProperty(true)]
		public string BannerCloseButtonToolTip
		{
			get
			{
				return this.GetString("BannerCloseButtonToolTip");
			}
			set
			{
				this.SetString("BannerCloseButtonToolTip", value);
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x000AEA58 File Offset: 0x000ACC58
		// (set) Token: 0x060034F3 RID: 13555 RVA: 0x000AEA65 File Offset: 0x000ACC65
		[NotifyParentProperty(true)]
		[DefaultValue("Full Screen")]
		public string FullScreenButtonToolTip
		{
			get
			{
				return this.GetString("FullScreenButtonToolTip");
			}
			set
			{
				this.SetString("FullScreenButtonToolTip", value);
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000AEA73 File Offset: 0x000ACC73
		// (set) Token: 0x060034F5 RID: 13557 RVA: 0x000AEA80 File Offset: 0x000ACC80
		[NotifyParentProperty(true)]
		[DefaultValue("Share")]
		public string TitleBarShareToolTip
		{
			get
			{
				return this.GetString("TitleBarShareToolTip");
			}
			set
			{
				this.SetString("TitleBarShareToolTip", value);
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000AEA8E File Offset: 0x000ACC8E
		// (set) Token: 0x060034F7 RID: 13559 RVA: 0x000AEA9B File Offset: 0x000ACC9B
		[NotifyParentProperty(true)]
		[DefaultValue("Play")]
		public string PlayButtonToolTip
		{
			get
			{
				return this.GetString("PlayButtonToolTip");
			}
			set
			{
				this.SetString("PlayButtonToolTip", value);
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000AEAA9 File Offset: 0x000ACCA9
		// (set) Token: 0x060034F9 RID: 13561 RVA: 0x000AEAB6 File Offset: 0x000ACCB6
		[NotifyParentProperty(true)]
		[DefaultValue("Pause")]
		public string PauseButtonToolTip
		{
			get
			{
				return this.GetString("PauseButtonToolTip");
			}
			set
			{
				this.SetString("PauseButtonToolTip", value);
			}
		}

		// Token: 0x04000E52 RID: 3666
		private readonly LocalizationProvider _localizationProvider;
	}
}

using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020005CD RID: 1485
	public class MediaPlayerPlaylistSettings : StateManager
	{
		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000B002D File Offset: 0x000AE22D
		// (set) Token: 0x0600352A RID: 13610 RVA: 0x000B004D File Offset: 0x000AE24D
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string YouTubePlaylist
		{
			get
			{
				return (base.ViewState["YouTubePlaylist"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["YouTubePlaylist"] = value;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000B0060 File Offset: 0x000AE260
		// (set) Token: 0x0600352C RID: 13612 RVA: 0x000B0089 File Offset: 0x000AE289
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public int SelectedIndex
		{
			get
			{
				object obj = base.ViewState["SelectedIndex"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["SelectedIndex"] = value;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000B00A4 File Offset: 0x000AE2A4
		// (set) Token: 0x0600352E RID: 13614 RVA: 0x000B00CD File Offset: 0x000AE2CD
		[NotifyParentProperty(true)]
		[DefaultValue(MediaPlayerPlaylistPosition.Vertical)]
		public MediaPlayerPlaylistPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj == null)
				{
					return MediaPlayerPlaylistPosition.Vertical;
				}
				return (MediaPlayerPlaylistPosition)obj;
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000B00E8 File Offset: 0x000AE2E8
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x000B0111 File Offset: 0x000AE311
		[DefaultValue(MediaPlayerPlaylistPosition.Vertical)]
		[NotifyParentProperty(true)]
		public MediaPlayerPlaylistMode Mode
		{
			get
			{
				object obj = base.ViewState["Mode"];
				if (obj == null)
				{
					return MediaPlayerPlaylistMode.Scrollbar;
				}
				return (MediaPlayerPlaylistMode)obj;
			}
			set
			{
				base.ViewState["Mode"] = value;
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000B012C File Offset: 0x000AE32C
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x000B0155 File Offset: 0x000AE355
		[DefaultValue(MediaPlayerPlaylistPosition.Vertical)]
		[NotifyParentProperty(true)]
		public MediaPlayerScrollButtonsTrigger ButtonsTrigger
		{
			get
			{
				object obj = base.ViewState["ButtonsTrigger"];
				if (obj == null)
				{
					return MediaPlayerScrollButtonsTrigger.Hover;
				}
				return (MediaPlayerScrollButtonsTrigger)obj;
			}
			set
			{
				base.ViewState["ButtonsTrigger"] = value;
			}
		}
	}
}

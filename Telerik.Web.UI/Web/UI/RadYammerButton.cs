using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000884 RID: 2180
	public class RadYammerButton : RadSocialButtonBase
	{
		// Token: 0x060050B0 RID: 20656 RVA: 0x000FBD3B File Offset: 0x000F9F3B
		public RadYammerButton() : base(SocialNetType.Yammer)
		{
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x060050B1 RID: 20657 RVA: 0x000FBD45 File Offset: 0x000F9F45
		// (set) Token: 0x060050B2 RID: 20658 RVA: 0x000FBD66 File Offset: 0x000F9F66
		public YammerActionButton ButtonType
		{
			get
			{
				return (YammerActionButton)(base.ViewState["ButtonType"] ?? YammerActionButton.Like);
			}
			set
			{
				base.ViewState["ButtonType"] = value;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x060050B3 RID: 20659 RVA: 0x000FBD7E File Offset: 0x000F9F7E
		// (set) Token: 0x060050B4 RID: 20660 RVA: 0x000FBD95 File Offset: 0x000F9F95
		public string YammerNetwork
		{
			get
			{
				return (string)base.ViewState["YammerNetwork"];
			}
			set
			{
				base.ViewState["YammerNetwork"] = value;
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x060050B5 RID: 20661 RVA: 0x000FBDA8 File Offset: 0x000F9FA8
		// (set) Token: 0x060050B6 RID: 20662 RVA: 0x000FBDB0 File Offset: 0x000F9FB0
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(SocialNetType.Yammer)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override SocialNetType SocialNetType
		{
			get
			{
				return base.SocialNetType;
			}
			set
			{
				base.SocialNetType = value;
			}
		}

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x060050B7 RID: 20663 RVA: 0x000FBDB9 File Offset: 0x000F9FB9
		// (set) Token: 0x060050B8 RID: 20664 RVA: 0x000FBDC1 File Offset: 0x000F9FC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue("")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		public override string UrlToShare
		{
			get
			{
				return base.UrlToShare;
			}
			set
			{
				base.UrlToShare = value;
			}
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x060050B9 RID: 20665 RVA: 0x000FBDCA File Offset: 0x000F9FCA
		// (set) Token: 0x060050BA RID: 20666 RVA: 0x000FBDD2 File Offset: 0x000F9FD2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue("")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public override string TitleToShare
		{
			get
			{
				return base.TitleToShare;
			}
			set
			{
				base.TitleToShare = value;
			}
		}
	}
}

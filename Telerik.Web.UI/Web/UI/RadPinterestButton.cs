using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000882 RID: 2178
	public class RadPinterestButton : RadSocialButtonBase
	{
		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x0600509E RID: 20638 RVA: 0x000FBBCD File Offset: 0x000F9DCD
		// (set) Token: 0x0600509F RID: 20639 RVA: 0x000FBBEE File Offset: 0x000F9DEE
		public PinterestActionButton ButtonType
		{
			get
			{
				return (PinterestActionButton)(base.ViewState["ButtonType"] ?? PinterestActionButton.PinIt);
			}
			set
			{
				base.ViewState["ButtonType"] = value;
			}
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x060050A0 RID: 20640 RVA: 0x000FBC06 File Offset: 0x000F9E06
		// (set) Token: 0x060050A1 RID: 20641 RVA: 0x000FBC0A File Offset: 0x000F9E0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(SocialNetType.Pinterest)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		public override SocialNetType SocialNetType
		{
			get
			{
				return SocialNetType.Pinterest;
			}
			set
			{
				this.SocialNetType = SocialNetType.Pinterest;
			}
		}

		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x060050A2 RID: 20642 RVA: 0x000FBC14 File Offset: 0x000F9E14
		// (set) Token: 0x060050A3 RID: 20643 RVA: 0x000FBC34 File Offset: 0x000F9E34
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Specifies the image's URL to be pinned.")]
		public override string UrlToShare
		{
			get
			{
				return (string)(base.ViewState["UrlToShare"] ?? string.Empty);
			}
			set
			{
				base.ViewState["UrlToShare"] = value;
			}
		}

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x060050A4 RID: 20644 RVA: 0x000FBC47 File Offset: 0x000F9E47
		// (set) Token: 0x060050A5 RID: 20645 RVA: 0x000FBC67 File Offset: 0x000F9E67
		[Description("Specifies the description of the pinned image.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public override string TitleToShare
		{
			get
			{
				return (string)(base.ViewState["TitleToShare"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TitleToShare"] = value;
			}
		}

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x060050A6 RID: 20646 RVA: 0x000FBC7A File Offset: 0x000F9E7A
		// (set) Token: 0x060050A7 RID: 20647 RVA: 0x000FBC9A File Offset: 0x000F9E9A
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Specifies the sender's URL.")]
		public string FromUrl
		{
			get
			{
				return (string)(base.ViewState["FromUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FromUrl"] = value;
			}
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x060050A8 RID: 20648 RVA: 0x000FBCAD File Offset: 0x000F9EAD
		// (set) Token: 0x060050A9 RID: 20649 RVA: 0x000FBCCE File Offset: 0x000F9ECE
		[Category("Behavior")]
		[DefaultValue(TwitterCounterMode.None)]
		[Description("Specifies the counter mode for the button.")]
		public PinterestCounterMode CounterMode
		{
			get
			{
				return (PinterestCounterMode)(base.ViewState["CounterMode"] ?? PinterestCounterMode.None);
			}
			set
			{
				base.ViewState["CounterMode"] = value;
			}
		}
	}
}

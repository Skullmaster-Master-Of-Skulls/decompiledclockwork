using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F0F RID: 3855
	public class RadFacebookButton : RadSocialButtonBase
	{
		// Token: 0x17002E30 RID: 11824
		// (get) Token: 0x06009232 RID: 37426 RVA: 0x0020F0BD File Offset: 0x0020D2BD
		// (set) Token: 0x06009233 RID: 37427 RVA: 0x0020F0DE File Offset: 0x0020D2DE
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Specifies the type of the button.")]
		public FacebookNetType ButtonType
		{
			get
			{
				return (FacebookNetType)(base.ViewState["ButtonType"] ?? FacebookNetType.FacebookLike);
			}
			set
			{
				base.ViewState["ButtonType"] = value;
			}
		}

		// Token: 0x17002E31 RID: 11825
		// (get) Token: 0x06009234 RID: 37428 RVA: 0x0020F0F6 File Offset: 0x0020D2F6
		// (set) Token: 0x06009235 RID: 37429 RVA: 0x0020F0FE File Offset: 0x0020D2FE
		[Browsable(false)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(FacebookNetType.FacebookLike)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override SocialNetType SocialNetType
		{
			get
			{
				return (SocialNetType)this.ButtonType;
			}
			set
			{
				this.SocialNetType = (SocialNetType)this.ButtonType;
			}
		}

		// Token: 0x17002E32 RID: 11826
		// (get) Token: 0x06009236 RID: 37430 RVA: 0x0020F10C File Offset: 0x0020D30C
		// (set) Token: 0x06009237 RID: 37431 RVA: 0x0020F12D File Offset: 0x0020D32D
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Specifies whether profile pictures should be displayed.")]
		public bool ShowFaces
		{
			get
			{
				return (bool)(base.ViewState["ShowFaces"] ?? true);
			}
			set
			{
				base.ViewState["ShowFaces"] = value;
			}
		}

		// Token: 0x17002E33 RID: 11827
		// (get) Token: 0x06009238 RID: 37432 RVA: 0x0020F145 File Offset: 0x0020D345
		// (set) Token: 0x06009239 RID: 37433 RVA: 0x0020F166 File Offset: 0x0020D366
		[Description("Specifies the button layout.")]
		[Category("Behavior")]
		[DefaultValue(FacebookButtonLayout.ButtonCount)]
		public FacebookButtonLayout ButtonLayout
		{
			get
			{
				return (FacebookButtonLayout)(base.ViewState["ButtonLayout"] ?? FacebookButtonLayout.ButtonCount);
			}
			set
			{
				base.ViewState["ButtonLayout"] = value;
			}
		}

		// Token: 0x17002E34 RID: 11828
		// (get) Token: 0x0600923A RID: 37434 RVA: 0x0020F17E File Offset: 0x0020D37E
		// (set) Token: 0x0600923B RID: 37435 RVA: 0x0020F19F File Offset: 0x0020D39F
		[Description("Specifies the color scheme of the button.")]
		[DefaultValue(FacebookColorScheme.Light)]
		[Category("Appearance")]
		public FacebookColorScheme ColorScheme
		{
			get
			{
				return (FacebookColorScheme)(base.ViewState["ColorScheme"] ?? FacebookColorScheme.Light);
			}
			set
			{
				base.ViewState["ColorScheme"] = value;
			}
		}

		// Token: 0x17002E35 RID: 11829
		// (get) Token: 0x0600923C RID: 37436 RVA: 0x0020F1B7 File Offset: 0x0020D3B7
		// (set) Token: 0x0600923D RID: 37437 RVA: 0x0020F1D3 File Offset: 0x0020D3D3
		[Category("Behavior")]
		[Description("Specifies the width of the button.")]
		[DefaultValue(null)]
		public int? Width
		{
			get
			{
				return (int?)(base.ViewState["Width"] ?? null);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17002E36 RID: 11830
		// (get) Token: 0x0600923E RID: 37438 RVA: 0x0020F1EB File Offset: 0x0020D3EB
		// (set) Token: 0x0600923F RID: 37439 RVA: 0x0020F20C File Offset: 0x0020D40C
		[Category("Behavior")]
		[DefaultValue(FacebookFont.Arial)]
		[Description("Specifies the font for the button.")]
		public FacebookFont Font
		{
			get
			{
				return (FacebookFont)(base.ViewState["Font"] ?? FacebookFont.Arial);
			}
			set
			{
				base.ViewState["Font"] = value;
			}
		}

		// Token: 0x17002E37 RID: 11831
		// (get) Token: 0x06009240 RID: 37440 RVA: 0x0020F224 File Offset: 0x0020D424
		// (set) Token: 0x06009241 RID: 37441 RVA: 0x0020F244 File Offset: 0x0020D444
		[Description("Specifies the label for referrals.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string ReferralsLabel
		{
			get
			{
				return (string)(base.ViewState["ReferralsLabel"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ReferralsLabel"] = value;
			}
		}
	}
}

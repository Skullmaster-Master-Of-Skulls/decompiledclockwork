using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200087E RID: 2174
	public class RadSocialButton : RadSocialButtonBase
	{
		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x000FB733 File Offset: 0x000F9933
		// (set) Token: 0x06005070 RID: 20592 RVA: 0x000FB754 File Offset: 0x000F9954
		[Description("Specifies the tooltip of the button.")]
		[Localizable(true)]
		public string ToolTip
		{
			get
			{
				return (string)(base.ViewState["ToolTip"] ?? this.GetDefaultToolTip());
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x000FB768 File Offset: 0x000F9968
		private string GetDefaultToolTip()
		{
			SocialNetType socialNetType = this.SocialNetType;
			string result;
			if (socialNetType != SocialNetType.ShareOnTwitter)
			{
				switch (socialNetType)
				{
				case SocialNetType.ShareOnFacebook:
					return "Share on Facebook";
				case SocialNetType.MailTo:
					return "Tell a friend";
				case SocialNetType.SendEmail:
					return "Email";
				case SocialNetType.CompactButton:
					return "Show more";
				case SocialNetType.ShareOnPinterest:
					return "Pinterest";
				}
				result = "Share on " + this.SocialNetType.ToString();
			}
			else
			{
				result = "Tweet this";
			}
			return result;
		}

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x000FB7FB File Offset: 0x000F99FB
		// (set) Token: 0x06005073 RID: 20595 RVA: 0x000FB81B File Offset: 0x000F9A1B
		[Description("Specifies the text of the button label.")]
		[Localizable(true)]
		[DefaultValue("")]
		public string LabelText
		{
			get
			{
				return (string)(base.ViewState["LabelText"] ?? string.Empty);
			}
			set
			{
				base.ViewState["LabelText"] = value;
			}
		}

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x000FB82E File Offset: 0x000F9A2E
		// (set) Token: 0x06005075 RID: 20597 RVA: 0x000FB84E File Offset: 0x000F9A4E
		[UrlProperty]
		[Description("Specifies the url of a custom icon for the button.")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string CustomIconUrl
		{
			get
			{
				return (string)(base.ViewState["CustomIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CustomIconUrl"] = value;
			}
		}

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x000FB861 File Offset: 0x000F9A61
		// (set) Token: 0x06005077 RID: 20599 RVA: 0x000FB888 File Offset: 0x000F9A88
		[DefaultValue(typeof(Unit), "16")]
		[TypeConverter(typeof(UnitConverter))]
		[Category("Layout")]
		[Description("Specifies the width of thr button's custom icon.")]
		public Unit CustomIconWidth
		{
			get
			{
				return (Unit)(base.ViewState["CustomIconWidth"] ?? new Unit(16));
			}
			set
			{
				base.ViewState["CustomIconWidth"] = value;
			}
		}

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x06005078 RID: 20600 RVA: 0x000FB8A0 File Offset: 0x000F9AA0
		// (set) Token: 0x06005079 RID: 20601 RVA: 0x000FB8C7 File Offset: 0x000F9AC7
		[TypeConverter(typeof(UnitConverter))]
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "16")]
		[Description("Specifies the height of thr button's custom icon.")]
		public Unit CustomIconHeight
		{
			get
			{
				return (Unit)(base.ViewState["CustomIconHeight"] ?? new Unit(16));
			}
			set
			{
				base.ViewState["CustomIconHeight"] = value;
			}
		}

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x0600507A RID: 20602 RVA: 0x000FB8DF File Offset: 0x000F9ADF
		// (set) Token: 0x0600507B RID: 20603 RVA: 0x000FB904 File Offset: 0x000F9B04
		[TypeConverter(typeof(UnitConverter))]
		[Description("Specifies the width of the social dialog popup.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public virtual Unit DialogWidth
		{
			get
			{
				return (Unit)(base.ViewState["DialogWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["DialogWidth"] = value;
			}
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x0600507C RID: 20604 RVA: 0x000FB91C File Offset: 0x000F9B1C
		// (set) Token: 0x0600507D RID: 20605 RVA: 0x000FB941 File Offset: 0x000F9B41
		[TypeConverter(typeof(UnitConverter))]
		[Category("Layout")]
		[Description("Specifies the height of the social dialog popup.")]
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit DialogHeight
		{
			get
			{
				return (Unit)(base.ViewState["DialogHeight"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["DialogHeight"] = value;
			}
		}

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x000FB959 File Offset: 0x000F9B59
		// (set) Token: 0x0600507F RID: 20607 RVA: 0x000FB97E File Offset: 0x000F9B7E
		[Category("Layout")]
		[Description("Specifies the top of the social dialog.")]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		public Unit DialogTop
		{
			get
			{
				return (Unit)(base.ViewState["DialogTop"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["DialogTop"] = value;
			}
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06005080 RID: 20608 RVA: 0x000FB996 File Offset: 0x000F9B96
		// (set) Token: 0x06005081 RID: 20609 RVA: 0x000FB9BB File Offset: 0x000F9BBB
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Specifies the left of the social dialog popup.")]
		public Unit DialogLeft
		{
			get
			{
				return (Unit)(base.ViewState["DialogLeft"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["DialogLeft"] = value;
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06005082 RID: 20610 RVA: 0x000FB9D3 File Offset: 0x000F9BD3
		// (set) Token: 0x06005083 RID: 20611 RVA: 0x000FB9F3 File Offset: 0x000F9BF3
		[DefaultValue("")]
		[Description("Specifies a custom CssClass for the social button.")]
		[Category("Appearance")]
		public string CssClass
		{
			get
			{
				return (string)(base.ViewState["CssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}
	}
}

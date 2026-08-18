using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000881 RID: 2177
	public class RadLinkedInButton : RadSocialButtonBase
	{
		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06005097 RID: 20631 RVA: 0x000FBB45 File Offset: 0x000F9D45
		// (set) Token: 0x06005098 RID: 20632 RVA: 0x000FBB49 File Offset: 0x000F9D49
		[Browsable(false)]
		[DefaultValue(SocialNetType.LinkedInShare)]
		[Category("Behavior")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override SocialNetType SocialNetType
		{
			get
			{
				return SocialNetType.LinkedInShare;
			}
			set
			{
				this.SocialNetType = SocialNetType.LinkedInShare;
			}
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x000FBB53 File Offset: 0x000F9D53
		// (set) Token: 0x0600509A RID: 20634 RVA: 0x000FBB74 File Offset: 0x000F9D74
		[DefaultValue(LinkedInCounterMode.None)]
		[Description("Specifies the counter mode for the button.")]
		[Category("Behavior")]
		public LinkedInCounterMode CounterMode
		{
			get
			{
				return (LinkedInCounterMode)(base.ViewState["CounterMode"] ?? LinkedInCounterMode.None);
			}
			set
			{
				base.ViewState["CounterMode"] = value;
			}
		}

		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x000FBB8C File Offset: 0x000F9D8C
		// (set) Token: 0x0600509C RID: 20636 RVA: 0x000FBBAD File Offset: 0x000F9DAD
		[Description("Specifies whether a counter will be shown if its value will be zero.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool ShowZeroCount
		{
			get
			{
				return (bool)(base.ViewState["ShowZeroCount"] ?? false);
			}
			set
			{
				base.ViewState["ShowZeroCount"] = value;
			}
		}
	}
}
